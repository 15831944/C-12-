/**编写人：方淑云
 * 时间：2014年8月1号
 * 功能:文件增加界面后台
 * 修改履历：1.8月4号，为textbox添加验证.添加上传文件大小验证
 **/
using Common.Entities;
using FineUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WDFramework
{
    public partial class ADD : System.Web.UI.Page
    {
        BLHelper.BLLFiles file = new BLHelper.BLLFiles();
        BLHelper.BLLUser user = new BLHelper.BLLUser();
        BLHelper.BLLAttachment at = new BLHelper.BLLAttachment();
        BLHelper.BLLOperationLog op = new BLHelper.BLLOperationLog();
        BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
        BLHelper.BLLBasicCode bllBasicCode = new BLHelper.BLLBasicCode();
        OperationLog log = new OperationLog();
        Files files = new Files();
      
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
           {
                LevinTime.MaxDate = DateTime.Now;
                InitDroplistFile();//初始化文件分类下拉框
                InitDropListAgency();//初始化机构下拉框
                InitSecrecyLevel();//初始化等级下拉框
            }
        }       
        //保存按钮
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (FileName.Text.Trim() == "")
                {
                    Alert.Show("文件名不能为空！");
                    return;
                }
                BLHelper.BLLAgency agency = new BLHelper.BLLAgency();
                files.FileName = FileName.Text.Trim();
                files.LevinTime = LevinTime.SelectedDate;
                files.LevinUnit = LevinUnit.Text.Trim();
                files.FileRecipient = FileRecipient.Text.Trim();
                string username = user.FindByLoginName(Session["LoginName"].ToString()).UserName;
                files.EntryPerson = username;
                files.SecrecyLevel = Convert.ToInt32(DropDownListLevel.SelectedIndex + 1);
                files.AgencyID = agency.SelectAgencyID(DropDownListAgency.SelectedText.Trim());
                files.DocumentCategoryID = DropDownListFile.SelectedText.Trim();
                if (Convert.ToInt32(Session["SecrecyLevel"]) < 5)
                {
                    log.LoginName = username;
                    log.OperationTime = DateTime.Now;
                    log.LoginIP = " ";
                    log.OperationContent = "Files";
                    log.OperationType = "添加";
                    files.IsPass = false;

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
                    files.AttachmentID = AttachID;
                    file.Insert(files);//插入文件表
                    log.OperationDataID = files.FilesID;
                    op.Insert(log);
                    PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("您的数据已提交，请等待审核！"));
                }
                else
                {
                    files.IsPass = true;
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
                    files.AttachmentID = AttachID;
                    file.Insert(files);//插入文件表
                    PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("保存成功"));
                }
            }
            catch (Exception ex)
            {
                int attachid = Convert.ToInt32(files.AttachmentID);
                string path = at.FindPath(attachid);
                pm.DeleteFile(attachid, path);
                pm.SaveError(ex, this.Request);
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
                    DropDownListAgency.Items.Add(list[i].AgencyName.ToString(), list[i].AgencyName.ToString());
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
        //判断文件名输入框不可输入特殊字符
        protected void FileName_TextChanged(object sender, EventArgs e)
        {
            if (FileName.Text.Trim() != "")
            {
                if (file.IsFileName(FileName.Text.Trim()) != null)
                {
                    if (file.IsFileName(FileName.Text.Trim()).IsPass == false)
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
        //初始化等级下拉框
        public void InitSecrecyLevel()
        {
            string[] SecrecyLevels = new string[] { "四级", "三级", "二级", "一级", "管理员" };
            for (int i = 0; i < Convert.ToInt32(Session["SecrecyLevel"]); i++)
            {               
                DropDownListLevel.Items.Add(SecrecyLevels[i], i.ToString());
            }
        }
        //重置
        protected void DeleteAll_Click(object sender, EventArgs e)
        {
            try
            {
                FileName.Reset();
                DropDownListLevel.Reset();
                DropDownListAgency.Reset();
                DropDownListFile.Reset();
                LevinTime.Reset();
                LevinUnit.Reset();
                FileRecipient.Reset();
                PageContext.RegisterStartupScript("clearFile();");
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }

        

    
     
    }
}