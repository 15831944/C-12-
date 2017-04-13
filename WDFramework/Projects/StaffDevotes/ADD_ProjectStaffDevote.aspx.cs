/**编写人：王会会
 * 时间：2014年8月8号
 * 功能：添加项目相关人员投入信息的相关操作
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

namespace WDFramework.Projects
{
    public partial class ADD_ProjectStaffDevote : System.Web.UI.Page
    {
        BLHelper.BLLProject bllProject = new BLHelper.BLLProject();
        BLHelper.BLLStaffDevote bllStaffDevote = new BLHelper.BLLStaffDevote();
        BLHelper.BLLUser bllUser= new BLHelper.BLLUser();
        BLHelper.BLLOperationLog bllOperate = new BLHelper.BLLOperationLog();
        StaffDevote devote = new StaffDevote();
        BLCommon.PublicMethod publicmethod = new BLCommon.PublicMethod();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitSecrecyLevel();
                InitDropListProjectName();
                InitDropDownListSort();
                DatePickerDevoteTime.MaxDate = DateTime.Now;
                DatePickerExitTime.MaxDate = DateTime.Now;
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
                        devote.UserInfoID = bllUser.FindID(UserInfoName.Text.Trim());
                        devote.DevoteTime = DatePickerDevoteTime.SelectedDate;
                        if (DatePickerExitTime.SelectedDate.HasValue)
                        {
                            if (DatePickerExitTime.SelectedDate < DatePickerDevoteTime.SelectedDate)
                            {
                                Alert.ShowInTop("退出时间不能小于开始时间！");
                                return;
                            }
                            else
                                devote.ExitTime = DatePickerExitTime.SelectedDate;
                        }
                        devote.ExitTime = DatePickerExitTime.SelectedDate;
                        devote.ProjectID = bllProject.SelectProjectID(DropDownListProjectID.SelectedText.ToString());
                        devote.SecrecyLevel = DropDownListSecrecyLevel.SelectedIndex + 1;
                        devote.Sort = DropDownListSort.SelectedIndex + 1;
                        devote.EntryPerson = bllUser.FindByLoginName(Session["LoginName"].ToString()).UserName;
                        devote.ProjectCompletion = tb_ProjectCompletion.Text.Trim();
                        if (Convert.ToInt32(Session["SecrecyLevel"]) != 5)
                            devote.IsPass = false;
                        else
                            devote.IsPass = true;
                        if (devote.Sort > 0 && devote.Sort <= 60)
                        {
                            if (bllStaffDevote.IsNullUserInfoID(Convert.ToInt32(devote.UserInfoID.ToString()), Convert.ToInt32(devote.ProjectID.ToString())) == null)
                            {
                                if (bllStaffDevote.IsNullSort(Convert.ToInt32(devote.ProjectID.ToString()), Convert.ToInt32(devote.Sort)) == null)
                                {
                                    if (Convert.ToInt32(Session["SecrecyLevel"]) != 5)
                                    {
                                        bllStaffDevote.Insert(devote);
                                        OperationLog operate = new OperationLog();
                                        operate.LoginName = bllUser.FindByLoginName(Session["LoginName"].ToString()).UserName;
                                        operate.OperationType = "添加";
                                        operate.OperationContent = "StaffDevote";
                                        operate.OperationDataID = bllStaffDevote.FindStaffDevoteID(Convert.ToInt32(devote.UserInfoID.ToString()), Convert.ToInt32(devote.ProjectID.ToString()));
                                        operate.LoginIP = "";
                                        operate.OperationTime = DateTime.Now;
                                        bllOperate.Insert(operate);
                                        PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("您的操作已提交审核，请等待！"));
                                    }
                                    else
                                    {
                                        bllStaffDevote.Insert(devote);
                                        PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("项目人员投入信息添加完成！"));
                                    }
                                }
                                else
                                {
                                    if (bllStaffDevote.IsNullSort(Convert.ToInt32(devote.ProjectID.ToString()), Convert.ToInt32(devote.Sort)).IsPass == true)
                                        Alert.ShowInTop("序号为" + devote.Sort + "的人员已经存在于该项目");
                                    else
                                        Alert.ShowInTop("序号为" + devote.Sort + "的人员正在审核");
                                }
                            }
                            else
                            {
                                if (bllStaffDevote.IsNullUserInfoID(Convert.ToInt32(devote.UserInfoID.ToString()), Convert.ToInt32(devote.ProjectID.ToString())).IsPass == true)
                                    Alert.ShowInTop("人员" + UserInfoName.Text + "已存在项目" + DropDownListProjectID.SelectedText);
                                if (bllStaffDevote.IsNullUserInfoID(Convert.ToInt32(devote.UserInfoID.ToString()), Convert.ToInt32(devote.ProjectID.ToString())).IsPass == false)
                                    Alert.ShowInTop("人员" + UserInfoName.Text + "正在审核");
                            }
                        }
                        else
                            Alert.ShowInTop("序号为1~60");
                    }
                    else
                    {
                        Alert.ShowInTop("人员名称不存在");
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
        //初始化项目涉密等级等级
        public void InitSecrecyLevel()
        {
            try
            {
                string[] SecrecyLevels = new string[] { "四级", "三级", "二级", "一级", "管理员" };
               // string[] SecrecyLevels = new string[] { "公开", "内部", "秘密", "机密", "管理员" };
                for (int i = 0; i < Convert.ToInt32(Session["SecrecyLevel"]); i++)
                {
                    DropDownListSecrecyLevel.Items.Add(SecrecyLevels[i], SecrecyLevels[i]);
                }
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }
        //初始化排序
        public void InitDropDownListSort()
        {
            try
            {
                for (int i = 0; i < 60; i++)
                {
                    DropDownListSort.Items.Add((i + 1).ToString(), (i + 1).ToString());
                }
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }
        //初始化项目名称下拉框
        public void InitDropListProjectName()
        {
            try
            {
                List<Common .Entities .Project > list = bllProject.FindALLName();
                for (int i = 0; i < list.Count(); i++)
                {
                    DropDownListProjectID.Items.Add(list[i].ProjectName.ToString(), list[i].ProjectName.ToString());
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
                DatePickerDevoteTime.Reset();
                DatePickerExitTime.Reset();
                DropDownListProjectID.Reset();
                DropDownListSecrecyLevel.Reset();
                DropDownListSort.Reset();
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }
    }
}