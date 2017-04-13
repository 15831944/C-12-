/**编写人：方淑云
 * 时间：2014年11月14号
 * 功能:文件添加界面后台
 * 修改履历：
 **/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common.Entities;
using FineUI;
namespace WDFramework.WithinPost
{
    public partial class AddData : System.Web.UI.Page
    {
        BLHelper.BLLWithinPost wh = new BLHelper.BLLWithinPost();
        BLHelper.BLLUser user = new BLHelper.BLLUser();
        BLHelper.BLLAttachment at = new BLHelper.BLLAttachment();
        BLHelper.BLLOperationLog op = new BLHelper.BLLOperationLog();
        BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
        BLHelper.BLLBasicCode bllBasicCode = new BLHelper.BLLBasicCode();
        OperationLog log = new OperationLog();
       Common.Entities.WithinPost whh = new Common.Entities.WithinPost();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DatePicker_Time.MaxDate = DateTime.Now;
                InitDroplistFile();//初始化文件分类下拉框
                InitDropListAgency();//初始化机构下拉框
                InitSecrecyLevel();//初始化等级下拉框
            }
        }
        //初始化文件下拉框
        public void InitDroplistFile()
        {
            try
            {
                List<BasicCode> list = bllBasicCode.FindALLName("文件分类名称");
                for (int i = 0; i < list.Count(); i++)
                {
                    DropDownListFile.Items.Add(list[i].CategoryContent.ToString(), list[i].CategoryContent.ToString());
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }

        }
        //初始化机构下拉框
        public void InitDropListAgency()
        {
            try
            {
                BLHelper.BLLAgency agency = new BLHelper.BLLAgency();
                List<Common.Entities.Agency> list = agency.FindAllAgencyName();
                for (int i = 0; i < list.Count(); i++)
                {
                    DropDownListAndUnit.Items.Add(list[i].AgencyName.ToString(), list[i].AgencyName.ToString());
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
        //初始化等级下拉框
        public void InitSecrecyLevel()
        {
            string[] SecrecyLevels = new string[] { "四级", "三级", "二级", "一级", "管理员" };
            //string[] SecrecyLevels = new string[] { "公开", "内部", "秘密", "机密", "管理员" };
            for (int i = 0; i < Convert.ToInt32(Session["SecrecyLevel"]); i++)
            {
                DropDownListLevel.Items.Add(SecrecyLevels[i], i.ToString());
            }
        }
        //文件名验证
        protected void FileName_TextChanged(object sender, EventArgs e)
        {
            if (FileName.Text.Trim() != "")
            {
                if (wh.IsFileName(FileName.Text.Trim()) != null)
                {
                    if (wh.IsFileName(FileName.Text.Trim()).IsPass == false)
                    {
                        Alert.Show("该文件名已在审核中");
                        FileName.Text = "";
                        return;
                    }
                    else
                    {
                        Alert.Show("该文件名已存在");
                        FileName.Text = "";
                        return;
                    }
                }
            }
        }
        //保存
        protected void Save_Click(object sender, EventArgs e)
        {
            try
            {
                if (FileName.Text.Trim() == "")
                {
                    Alert.Show("文件名不能为空！");
                    return;
                }
                BLHelper.BLLAgency agency = new BLHelper.BLLAgency();
                whh.FileName = FileName.Text.Trim();
                whh.Time = DatePicker_Time.SelectedDate;
                whh.recipient = Recipentor.Text.Trim();
                string username = user.FindByLoginName(Session["LoginName"].ToString()).UserName;
                whh.EntryPerson = username;
                whh.SecrecyLevel = Convert.ToInt32(DropDownListLevel.SelectedIndex + 1);
                whh.AndUnit = DropDownListAndUnit.SelectedText.Trim();
                whh.FileType = DropDownListFile.SelectedText.Trim();
                if (Convert.ToInt32(Session["SecrecyLevel"]) < 5)
                {
                    log.LoginName = username;
                    log.OperationTime = DateTime.Now;
                    log.LoginIP = " ";
                    log.OperationContent = "WithPost";
                    log.OperationType = "添加";
                    whh.IsPass = false;

                    //文件上传并获取附件id
                    //先进行附件判断

                    int AttachID = pm.UpLoadFile(fileupload).Attachid;
                    switch (AttachID)
                    {
                        case -1:
                            Alert.ShowInTop("文件类型不符，请重新选择！");
                            return;
                        case 0:
                            Alert.ShowInTop("附件名已经存在！");
                            return;
                        case -2:
                            Alert.ShowInTop("文件不能大于150M");
                            return;
                        case -3:
                            Alert.ShowInTop("请上传附件");
                            return;

                    }
                    whh.AttachmentID = AttachID;
                    wh.Insert(whh);//插入文件表
                    log.OperationDataID = whh.WithinPostID;
                    op.Insert(log);
                    PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("您的数据已提交，请等待审核！"));
                }
                else
                {
                    whh.IsPass = true;
                    int AttachID = pm.UpLoadFile(fileupload).Attachid;
                    switch (AttachID)
                    {
                        case -1:
                            Alert.ShowInTop("文件类型不符，请重新选择！");
                            return;
                        case 0:
                            Alert.ShowInTop("附件名已经存在！");
                            return;
                        case -2:
                            Alert.ShowInTop("文件不能大于150M");
                            return;
                        case -3:
                            Alert.ShowInTop("请上传附件");
                            return;
                    }
                    whh.AttachmentID = AttachID;
                    wh.Insert(whh);//插入文件表
                    PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("保存成功"));
                }
            }
            catch (Exception ex)
            {
                int attachid = Convert.ToInt32(whh.AttachmentID);
                string path = at.FindPath(attachid);
                pm.DeleteFile(attachid, path);
                pm.SaveError(ex, this.Request);
            }
        }
        //重置
        protected void DeleteAll_Click(object sender, EventArgs e)
        {
            try
            {
                FileName.Reset();
                DropDownListLevel.Reset();
                DropDownListAndUnit.Reset();
                DropDownListFile.Reset();
                DatePicker_Time.Reset();
                Recipentor.Reset();
                PageContext.RegisterStartupScript("clearFile();");
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
    }
}