﻿/**编写人：王会会
 * 时间：2014年8月16号
 * 功能：工作经历信息的相关操作
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

namespace WDFramework.People.WorkExperiences
{
    public partial class Add_WorkExperience : System.Web.UI.Page
    {
        BLHelper.BLLUser bllUser = new BLHelper.BLLUser();
        BLHelper.BLLOperationLog bllOperate = new BLHelper.BLLOperationLog();
        BLHelper.BLLWorkExperience bllWork = new BLHelper.BLLWorkExperience();
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
                            if (WorkUnit.Text.Trim() == "")
                            {
                                Alert.ShowInTop("工作单位不能为空！");
                                WorkUnit.Text = "";
                                return;
                            }
                            if (Post.Text.Trim() == "")
                            {
                                Alert.ShowInTop("职务不能为空！");
                                Post.Text = "";
                                return;
                            }
                            if (JobTitle.Text.Trim() == "")
                            {
                                Alert.ShowInTop("职称不能为空！");
                                JobTitle.Text = "";
                                return;
                            }
                            
                            WorkExperience work = new WorkExperience();
                            work.UserInfoID = bllUser.FindID(UserInfoName.Text.Trim());
                            work.StartTime = DatePickerStartTime.SelectedDate;
                            if (DatePickerEndTime.SelectedDate.HasValue)
                            {
                                if (DatePickerEndTime.SelectedDate < DatePickerStartTime.SelectedDate)
                                {
                                    Alert.ShowInTop("结束时间不能小于开始时间！");
                                    return;
                                }
                                else
                                    work.EndTime = DatePickerEndTime.SelectedDate;
                            }
                            work.PartTimeUnit = PartTimeUnit.Text.Trim();
                            work.JobTitle = JobTitle.Text.Trim();
                            work.Post = Post.Text.Trim();
                            work.WorkUnit = WorkUnit.Text.Trim();
                            work.SecrecyLevel = DropDownListSecrecyLevel.SelectedIndex + 1;
                            work.EntryPerson = bllUser.FindByLoginName(Session["LoginName"].ToString()).UserName;
                            work.Remark = Remark.Text.Trim();
                            if (Convert.ToInt32(Session["SecrecyLevel"]) != 5)
                            {
                                work.IsPass = false;
                                bllWork.InsertForPeople(work);//插入工作经历表
                                OperationLog operate = new OperationLog();
                                operate.LoginName = bllUser.FindByLoginName(Session["LoginName"].ToString()).UserName;
                                operate.LoginIP = "";
                                operate.OperationType = "添加";
                                operate.OperationContent = "WorkExperience";
                                operate.OperationDataID = work.WorkExperienceID;
                                operate.OperationTime = System.DateTime.Now;
                                operate.Remark = "";
                                bllOperate.Insert(operate);//插入操作表
                                PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("人员工作经历信息已提交审核！"));
                            }
                            else
                            {
                                work.IsPass = true;
                                bllWork.InsertForPeople(work);//插入工作经历表
                                PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("人员工作经历信息已添加完成！"));
                            }
                        }
                        else
                            Alert.ShowInTop("该人员正在审核中！");
                    }
                    else
                        Alert.ShowInTop("人员不存在！");
                }
                else
                {
                    Alert.ShowInTop("人员不能为空！");
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
                JobTitle.Reset();
                Post.Reset();
                WorkUnit.Reset();
                DropDownListSecrecyLevel.Reset();
                PartTimeUnit.Reset();
                Remark.Reset();
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }
    }
}