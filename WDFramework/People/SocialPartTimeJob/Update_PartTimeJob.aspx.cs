/**编写人：王会会
 * 时间：2014年8月13号
 * 功能：更新人员社会兼职信息的相关操作
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

namespace WDFramework.People.SocialPartTimeJob
{
    public partial class Update_PartTimeJob : System.Web.UI.Page
    {
        BLHelper.BLLUser bllUser = new BLHelper.BLLUser();
        BLHelper.BLLOperationLog bllOperate = new BLHelper.BLLOperationLog();
        BLHelper.BLLSocialPartTime bllSocial = new BLHelper.BLLSocialPartTime();
        BLHelper.BLLBasicCode bllBasicCode = new BLHelper.BLLBasicCode();
        BLCommon.PublicMethod publicmethod = new BLCommon.PublicMethod();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitDropDownListLevelName();
                BindData();
                DatePickerApproveTime.MaxDate = DateTime.Now;
            }

        }
        public void BindData()
        {
            try
            {
                SocialPartTime social = bllSocial.FindBySocialID(Convert.ToInt32(Session["SocialPartTimeID"]), Convert.ToInt32(Session["SecrecyLevel"]), true);
                UserInfoName.Text = bllUser.FindByUserID(Convert.ToInt32(social.UserInfoID));
                DropDownListLevelName.SelectedValue = social.LevelName;
                //LevelName.Text = social.LevelName;
                PartTimeName.Text = social.PartTimeName;
                PartTimeUnit.Text = social.PartUnitName;
                AwardDepartments.Text = social.AwardDepartments;
                Terms.Text = social.Terms;
                DatePickerApproveTime.SelectedDate = social.ApproveTime;
                tprimaryUnit.Text = social.primaryUnit;
                Remark.Text = social.Remark;
                DropDownListSecrecyLevel.SelectedIndex = Convert.ToInt32(social.SecrecyLevel - 1);
                if (social.Sort == "社会兼职")
                    ddl_sort.SelectedIndex = 0;
                else
                    ddl_sort.SelectedIndex = 1;
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }
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
                            if (PartTimeUnit.Text.Trim() == "")
                            {
                                Alert.ShowInTop("兼职单位名称不能为空！");
                                PartTimeUnit.Text = "";
                                return;
                            }
                            if (PartTimeName.Text.Trim() == "")
                            {
                                Alert.ShowInTop("兼职职位名称不能为空！");
                                PartTimeName.Text = "";
                                return;

                            }
                            if (AwardDepartments.Text.Trim() == "")
                            {
                                Alert.ShowInTop("授予部门不能为空！");
                                AwardDepartments.Text = "";
                                return;
                            }
                            if (tprimaryUnit.Text.Trim() == "")
                            {
                                Alert.ShowInTop("职人员原单位不能为空！");
                                tprimaryUnit.Text = "";
                                return;
                            }
                           
                            SocialPartTime social = new SocialPartTime();
                            social.UserInfoID = bllUser.FindID(UserInfoName.Text.Trim());
                            social.LevelName = DropDownListLevelName.SelectedItem.Text;
                            social.PartTimeName = PartTimeName.Text.Trim();
                            social.PartUnitName = PartTimeUnit.Text.Trim();
                            social.Terms = Terms.Text;
                            social.primaryUnit = tprimaryUnit.Text.Trim();
                            social.AwardDepartments = AwardDepartments.Text.Trim();
                            social.ApproveTime = DatePickerApproveTime.SelectedDate;
                            social.Sort = ddl_sort.SelectedText.Trim();
                            social.Remark = Remark.Text.Trim();
                            social.SecrecyLevel = DropDownListSecrecyLevel.SelectedIndex + 1;
                            social.EntryPerson = bllSocial.Find(Convert.ToInt32(Session["SocialPartTimeID"])).EntryPerson;
                            if (Convert.ToInt32(Session["SecrecyLevel"]) != 5)
                            {
                                bllSocial.UpdateIsPass(Convert.ToInt32(Session["SocialPartTimeID"]), false);
                                social.IsPass = false;
                                bllSocial.InsertForPeople(social); //插入社会兼职表
                                OperationLog operate = new OperationLog();
                                operate.LoginName = bllSocial.Find(Convert.ToInt32(Session["SocialPartTimeID"])).EntryPerson;
                                operate.LoginIP = "";
                                operate.OperationType = "更新";
                                operate.OperationContent = "SocialPartTime";
                                operate.OperationDataID = Convert.ToInt32(Session["SocialPartTimeID"]);
                                operate.OperationTime = System.DateTime.Now;
                                operate.Remark = social.SocialPartTimeID.ToString();
                                bllOperate.Insert(operate);//插入操作表
                                PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("人员社会兼职信息已提交审核！"));
                            }
                            else
                            {
                                social.IsPass = true;
                                social.SocialPartTimeID = Convert.ToInt32(Session["SocialPartTimeID"]);
                                bllSocial.Update(social); //更新社会兼职表
                                PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("人员社会兼职信息已修改完成！"));
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
                DropDownListLevelName.Reset();
                PartTimeUnit.Reset();
                PartTimeName.Reset();
                Terms.Reset();
                tprimaryUnit.Reset();
                AwardDepartments.Reset();
                DatePickerApproveTime.Reset();
                Remark.Reset();
                DropDownListSecrecyLevel.Reset();
                ddl_sort.Reset();
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }
        //初始化级别名称下拉框
        public void InitDropDownListLevelName()
        {
            try
            {
                List<BasicCode> list = bllBasicCode.FindALLName("级别");
                for (int i = 0; i < list.Count(); i++)
                {
                    DropDownListLevelName.Items.Add(list[i].CategoryContent.ToString(), list[i].CategoryContent.ToString());
                }
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }
    }
}