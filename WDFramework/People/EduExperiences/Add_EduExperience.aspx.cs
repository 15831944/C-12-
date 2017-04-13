/**编写人：王会会
 * 时间：2014年8月16号
 * 功能：添加教育经历信息的相关操作
 * 修改履历：
 **/
using Common.Entities;
using FineUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WDFramework.People.EduExperiences
{
    public partial class Add_EduExperience : System.Web.UI.Page
    {
        BLHelper.BLLUser bllUser = new BLHelper.BLLUser();
        BLHelper.BLLOperationLog bllOperate = new BLHelper.BLLOperationLog();
        BLHelper.BLLEduExperience bllEdu = new BLHelper.BLLEduExperience();
        BLCommon.PublicMethod publicmethod = new BLCommon.PublicMethod();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DatePickerEndTime.MaxDate = DateTime.Now;
                DatePickerStartTime.MaxDate = DateTime.Now;
            }
        }
        //保存
        protected void Save_Click(object sender, EventArgs e)
        {

            try
            {
                if (UserInfoName.Text.Trim() != "")
                {
                    if (bllUser.IsUser(UserInfoName.Text.Trim()) != null)
                    {
                        if (bllUser.IsUser(UserInfoName.Text.Trim()).IsPass == true)
                        {
                            if (EHoldOffice.Text.Trim() == "")
                            {
                                Alert.ShowInTop("教育单位不能为空！");
                                EHoldOffice.Text = "";
                                return;
                            }
                            if (Major.Text.Trim() == "")
                            {
                                Alert.ShowInTop("所学专业不能为空！");
                                Major.Text = "";
                                return;
                            }
                           
                            EduExperience eduE = new EduExperience();
                            eduE.UserInfoID = bllUser.FindID(UserInfoName.Text.Trim());
                            eduE.StartTime = DatePickerStartTime.SelectedDate;
                            if (DatePickerEndTime.SelectedDate.HasValue)
                            {
                                if (DatePickerEndTime.SelectedDate < DatePickerStartTime.SelectedDate)
                                {
                                    Alert.ShowInTop("结束时间不能小于开始时间！");
                                    return;
                                }
                                else
                                    eduE.EndTime = DatePickerEndTime.SelectedDate;
                            }
                            eduE.Remark = Remark.Text.Trim();
                            eduE.Major = Major.Text.Trim();
                            eduE.EHoldOffice = EHoldOffice.Text.Trim();
                            eduE.SecrecyLevel = DropDownListSecrecyLevel.SelectedIndex + 1;
                            eduE.EntryPerson = bllUser.FindByLoginName(Session["LoginName"].ToString()).UserName;
                            if (Convert.ToInt32(Session["SecrecyLevel"]) != 5)
                            {
                                eduE.IsPass = false;
                                bllEdu.InsertForPeople(eduE); //插入教育经历表
                                OperationLog operate = new OperationLog();
                                operate.LoginName = bllUser.FindByLoginName(Session["LoginName"].ToString()).UserName;
                                operate.LoginIP = "";
                                operate.OperationType = "添加";
                                operate.OperationContent = "EduExperience";
                                operate.OperationDataID = eduE.EduExperienceID;
                                operate.OperationTime = System.DateTime.Now;
                                operate.Remark = "";
                                bllOperate.Insert(operate);//插入操作表
                                PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("人员教育经历信息已提交审核！"));
                            }
                            else
                            {
                                eduE.IsPass = true;
                                bllEdu.InsertForPeople(eduE); //插入教育经历表
                                PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("人员教育经历信息已添加完成！"));
                            }
                        }
                        else
                            Alert.ShowInTop("该人员尚未通过审核！");
                    }
                    else
                    {
                        Alert.ShowInTop("人员不存在！");
                        UserInfoName.Text = "";
                    }
                }
                else
                {
                    Alert.ShowInTop("人员名称不能为空！");
                    UserInfoName.Text = "";
                    return;
                }
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }     
       
        //重置
        protected void Reset_Click(object sender, EventArgs e)
        {
            try
            {
                UserInfoName.Reset();
                DatePickerStartTime.Reset();
                DatePickerEndTime.Reset();
                Major.Reset();
                EHoldOffice.Reset();
                DropDownListSecrecyLevel.Reset();
                Remark.Reset();
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }
    }
}