/**编写人：方淑云
 * 时间：2014年8月16号
 * 功能:成果获奖更新界面后台
 * 修改履历：    修改人：吕博扬
 *              修改时间：2015年11月27日
 *              修改内容：新增批复文号、平台负责人、平台成员、批复经费、平台管理（时间、人员、业务、经费）、上传文件等字段的后台代码
 **/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common.Entities;
using BLHelper;
using FineUI;

namespace WDFramework.Platform
{
    public partial class UpdatePlatform : System.Web.UI.Page
    {
        BLLPlatform BLLPlatform = new BLLPlatform();
        BLHelper.BLLBasicCode bllBasicCode = new BLHelper.BLLBasicCode();
        BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
        BLLOperationLog BLLOp = new BLLOperationLog();
        Common.Entities.OperationLog operationLog = new OperationLog();
        //lby↓
        BLHelper.BLLAttachment at = new BLHelper.BLLAttachment();
        BLHelper.BLLUser user = new BLHelper.BLLUser();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                //初始化平台级别
                InitDropDownListPlatformRank();
                //初始化批复部门
                InitDropDownListAgreeUnit();
                //初始化平台类别
                InitDropDownListPlatformType();
                //初始化项目涉密等级等级
                InitDropListSecrecyLevel();
                DatePicker_AgreeTime.MaxDate = DateTime.Now;
                InitData();
            }
        }
        //初始化
        public void InitData()
        {
            try
            {
                if (Session["PlatformID"].ToString() != "")
                {
                    Common.Entities.Platform platform = new Common.Entities.Platform();
                    platform = BLLPlatform.FindByPlatformID(Convert.ToInt32(Session["PlatformID"]));
                    txtPlatformName.Text = platform.PlatformName;
                    DropDownListPlatformRank .SelectedValue = platform.PlatformRank;
                    DropDownListAgreeUnit .SelectedValue = platform.AgreeUnit;
                    DropDownListPlatformType .SelectedValue = platform.PlatformType;
                    DatePicker_AgreeTime.SelectedDate = platform.AgreeTime;
                    DropDownList_SecrecyLevel.SelectedValue = platform.SecrecyLevel.ToString();
                    //lby↓
                    txtAgreeNumber.Text = platform.AgreeNumber;
                    txtAgreeExpenditure.Text = platform.AgreeExpenditure;
                    txtPlatformPrincipal.Text = platform.PlatformPrincipal;
                    txtPlatformMember.Text = platform.PlatformMember;
                    txtPlatformManagement.Text = platform.PlatformManagement;
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
                pm.SaveError(ex, this.Request);
            }

        }
        //保存
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //lby↓
                int AttachmentID = BLLPlatform.FindAttachmentID(Convert.ToInt32(Session["PlatformID"]));
                string path = at.FindPath(AttachmentID);

                Common.Entities.Platform platform = new Common.Entities.Platform();
                string PlatformName = BLLPlatform.FindByPlatformID(Convert.ToInt32(Session["PlatformID"])).PlatformName;//原平台名称
                if (txtPlatformName.Text.Trim() == "")
                {
                    Alert.ShowInTop("平台名称不能为空！");
                    txtPlatformName.Reset();
                    return;
                }
                //新更新的数据是否与原数据的平台名称相同
                if (txtPlatformName.Text.Trim() != PlatformName)//不同
                {
                    if (BLLPlatform.FindByPlatformName(txtPlatformName.Text.Trim()) == null)
                    {
                        platform.PlatformName = txtPlatformName.Text.Trim();
                    }
                    else
                    {
                        if (BLLPlatform.FindByPlatformName(txtPlatformName.Text.Trim()).IsPass == false)
                        {
                            Alert.ShowInTop("该平台名称正在审核中，请等待！");
                            txtPlatformName.Text = "";
                            return;
                        }
                        if (BLLPlatform.FindByPlatformName(txtPlatformName.Text.Trim()).IsPass == true)
                        {
                            Alert.ShowInTop("平台名称已存在！");
                            txtPlatformName.Text = "";
                            return;
                        }
                    }
                }
                else//相同
                {
                    if (txtPlatformName.Text.Trim() == PlatformName)//新更新的数据与原数据的平台名称相同
                    {
                        platform.PlatformName = txtPlatformName.Text.Trim();
                    }
                    else
                    {
                        if (BLLPlatform.FindByPlatformName(txtPlatformName.Text.Trim()).IsPass == false)
                        {
                            Alert.ShowInTop("该平台名称正在审核中，请等待！");
                            txtPlatformName.Text = "";
                            return;
                        }
                        if (BLLPlatform.FindByPlatformName(txtPlatformName.Text.Trim()).IsPass == true)
                        {
                            Alert.ShowInTop("平台名称已存在！");
                            txtPlatformName.Text = "";
                            return;
                        }
                    }
                }

                // platform.PlatformName = txtPlatformName.Text.Trim();
                platform.PlatformRank = DropDownListPlatformRank.SelectedText;
                platform.AgreeUnit = DropDownListAgreeUnit.SelectedText;
                platform.AgreeTime = DatePicker_AgreeTime.SelectedDate;
                platform.PlatformType = DropDownListPlatformType.SelectedText;
                platform.SecrecyLevel = DropDownList_SecrecyLevel.SelectedIndex + 1;
                platform.EntryPerson = BLLPlatform.FindByPlatformID(Convert.ToInt32(Session["PlatformID"])).EntryPerson;
                //lby↓
                platform.PlatformManagement = txtPlatformManagement.Text.Trim();
                platform.AgreeNumber = txtAgreeNumber.Text.Trim();
                platform.AgreeExpenditure = txtAgreeExpenditure.Text.Trim();
                platform.PlatformPrincipal = txtPlatformPrincipal.Text.Trim();
                platform.PlatformMember = txtPlatformMember.Text.Trim();

                //向操作日志表中
                if (Convert.ToInt32(Session["SecrecyLevel"]) != 5)
                {
                    platform.IsPass = false;
                    operationLog.LoginIP = " ";
                    operationLog.LoginName = platform.EntryPerson;
                    operationLog.OperationType = "更新";
                    operationLog.OperationContent = "Platform";
                    operationLog.OperationTime = DateTime.Now;
                    //operationLog.OperationDataID = Convert.ToInt32(Session["PlatformID"]);
                    //operationLog.Remark = platform.PlatformID.ToString();
                    //PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("平台信息已提交审核！"));

                    //lby↓
                    int attachid = pm.UpLoadFile(fileupload).Attachid;
                    if (attachid != -3)//有值
                    {
                        switch (attachid)
                        {
                            case -1:
                                Alert.ShowInTop("文件类型不符，请重新选择！");
                                return;
                            case 0:
                                Alert.ShowInTop("文件名已经存在！");
                                return;
                            case -2:
                                Alert.ShowInTop("文件不能大于150M");
                                return;
                        }
                        platform.AttachmentID = attachid;//附件为新插入的附件ID              
                    }
                    else//上传控件没有值
                    {
                        if (AttachmentID != 0)//原来有附件
                            platform.AttachmentID = AttachmentID;
                    }
                    BLLPlatform.insert(platform);//插入
                    operationLog.OperationDataID = Convert.ToInt32(Session["PlatformID"]);
                    operationLog.Remark = platform.PlatformID.ToString();  
                    BLLOp.Insert(operationLog);//将成果更新插入操作表    
                    BLLPlatform.UpdateIsPass(Convert.ToInt32(Session["PlatformID"]), false);
                    PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("平台信息已提交，正在等待审核！"));
                }
                else
                {
                    platform.IsPass = true;
                    platform.PlatformID = Convert.ToInt32(Session["PlatformID"]);
                    //lby↓
                    int attachid = pm.UpLoadFile(fileupload).Attachid;
                    if (attachid != -3)//上传控件是否有值
                    {
                        switch (attachid)
                        {
                            case -1:
                                Alert.ShowInTop("文件类型不符，请重新选择！");
                                return;
                            case 0:
                                Alert.ShowInTop("文件名已经存在！");
                                return;
                            case -2:
                                Alert.ShowInTop("文件不能大于150M");
                                return;
                        }
                        platform.AttachmentID = attachid;//附件为新插入的附件ID
                        pm.DeleteFile(AttachmentID, path);
                    }
                    else //上传空间没有值
                    {
                        if (AttachmentID != 0)
                            platform.AttachmentID = AttachmentID;
                    }
                    BLLPlatform.Update(platform);//5级直接更新平台表
                    PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("平台信息已更新完成！"));
                }

            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
        //重置
        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtPlatformName.Reset();
            DropDownListPlatformRank.Reset();
            DropDownListAgreeUnit.Reset();
            DropDownListPlatformType.Reset();
            DropDownList_SecrecyLevel.Reset();
            DatePicker_AgreeTime.Reset();
            //lby↓
            txtAgreeNumber.Reset();
            txtAgreeExpenditure.Reset();
            txtPlatformPrincipal.Reset();
            txtPlatformMember.Reset();
            txtPlatformManagement.Reset();
        }
        //初始化平台级别
        public void InitDropDownListPlatformRank()
        {
            try
            {
                List<BasicCode> list = bllBasicCode.FindALLName("平台级别");
                for (int i = 0; i < list.Count(); i++)
                {
                    DropDownListPlatformRank.Items.Add(list[i].CategoryContent.ToString(), list[i].CategoryContent.ToString());
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
        //初始化批复部门
        public void InitDropDownListAgreeUnit()
        {
            try
            {
                List<BasicCode> list = bllBasicCode.FindALLName("批复部门");
                for (int i = 0; i < list.Count(); i++)
                {
                    DropDownListAgreeUnit.Items.Add(list[i].CategoryContent.ToString(), list[i].CategoryContent.ToString());
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
        //初始化平台类别
        public void InitDropDownListPlatformType()
        {
            try
            {
                List<BasicCode> list = bllBasicCode.FindALLName("平台类别");
                for (int i = 0; i < list.Count(); i++)
                {
                    DropDownListPlatformType.Items.Add(list[i].CategoryContent.ToString(), list[i].CategoryContent.ToString());
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
        //初始化项目涉密等级等级
        public void InitDropListSecrecyLevel()
        {
            string[] SecrecyLevels = new string[] { "四级", "三级", "二级", "一级", "管理员" };
            //string[] SecrecyLevels = new string[] { "公开", "内部", "秘密", "机密", "管理员" };
            for (int i = 0; i < Convert.ToInt32(Session["SecrecyLevel"]); i++)
            {
                DropDownList_SecrecyLevel.Items.Add(SecrecyLevels[i], i.ToString());
            }
        }
    }
}