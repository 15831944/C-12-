/**编写人：未知
 * 时间：  未知
 * 功能:  平台添加界面后台
 * 修改履历：    1、修改人：吕博杨
 *                 修改时间：2015年11月28日
 *                 修改内容：为Platform新增字段（批复文号、平台负责人、平台成员、批复经费、平台管理（时间、人员、业务、经费）、上传文件）添加支持
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
    public partial class AddPlatform : System.Web.UI.Page
    {
        BLHelper.BLLPlatform BLLPlatform = new BLLPlatform();
        BLLUser BLLUser = new BLLUser();
        BLLOperationLog BLLOp = new BLLOperationLog();
        Common.Entities.OperationLog operationLog = new OperationLog();
        BLHelper.BLLBasicCode bllBasicCode = new BLHelper.BLLBasicCode();
        BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
        //lby ↓
        BLHelper.BLLAttachment at = new BLHelper.BLLAttachment();
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
            }
        }     
        //保存
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Common.Entities.Platform platform = new Common.Entities.Platform();
                if (txtPlatformName.Text.Trim() == "")
                {
                    Alert.ShowInTop("平台名称不能为空！");
                    txtPlatformName.Reset();
                    return;
                }
                if(BLLPlatform.FindByPlatformName(txtPlatformName.Text.Trim())==null)
                {
                    platform.PlatformName = txtPlatformName.Text.Trim();
                    platform.PlatformRank = DropDownListPlatformRank.SelectedText;
                    platform.AgreeUnit = DropDownListAgreeUnit.SelectedText;
                    platform.AgreeTime = DatePicker_AgreeTime.SelectedDate;
                    platform.PlatformType = DropDownListPlatformType.SelectedText;
                    platform.SecrecyLevel = Convert.ToInt32(DropDownList_SecrecyLevel.SelectedValue) + 1;
                    //Lby↓
                    platform.AgreeNumber = txtAgreeNumber.Text.Trim();
                    platform.AgreeExpenditure = txtAgreeExpenditure.Text.Trim();
                    platform.PlatformMember = txtPlatformMember.Text.Trim();
                    platform.PlatformManagement = txtPlatformManagement.Text.Trim();
                    platform.PlatformPrincipal = txtPlatformPrincipal.Text.Trim();                
                    platform.EntryPerson = BLLUser.FindByLoginName(Session["LoginName"].ToString()).UserName;
                    int attachid = pm.UpLoadFile(fileupload).Attachid;

                    if (Convert.ToInt32(Session["SecrecyLevel"]) == 5)
                    {
                        platform.IsPass = true;
                        
                        //lby↓
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
                        if (attachid != -3)
                            platform.AttachmentID = attachid;
                        else
                            platform.AttachmentID = null;
                        PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("平台信息已添加完成！"));
                    }
                    else
                    {
                        platform.IsPass = false;

                        //lby ↓
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
                        if (attachid != -3)
                            platform.AttachmentID = attachid;
                        else
                            platform.AttachmentID = null;
                        operationLog.LoginName = Session["LoginName"].ToString();
                        operationLog.OperationTime = DateTime.Now;
                        operationLog.LoginIP = " ";
                        operationLog.OperationContent = "Platform";
                        operationLog.OperationType = "添加";
                        operationLog.OperationDataID = platform.PlatformID;
                        BLLOp.Insert(operationLog);
                        PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("平台信息已提交审核"));
                    }
                    //向平台表中插入数据
                    BLLPlatform.insert(platform);
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
            catch (Exception ex)
            {
                int attachid = Convert.ToInt32(pm.UpLoadFile(fileupload).Attachid);
                string path = at.FindPath(attachid);
                pm.DeleteFile(attachid, path);
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

            //lby ↓
            txtAgreeExpenditure.Reset();
            txtAgreeNumber.Reset();
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