/**编写人：王会会
 * 时间：2014年8月16号
 * 功能：添加人员学历信息的相关操作
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

namespace WDFramework.People.Educations
{
    public partial class Add_Education : System.Web.UI.Page
    {
        BLHelper.BLLUser bllUser = new BLHelper.BLLUser();
        BLHelper.BLLOperationLog bllOperate = new BLHelper.BLLOperationLog();
        BLHelper.BLLEducation bllEducation = new BLHelper.BLLEducation();
        BLHelper.BLLBasicCode bllBasicCode = new BLHelper.BLLBasicCode();
        BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitDropDownListDegree();
                DatePickerEduTime.MaxDate = DateTime.Now;
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
                            if (SchoolName.Text.Trim() == "")
                            {
                                Alert.ShowInTop("学校名称不能为空！");
                                SchoolName.Text = "";
                                return;
                            }
                            Education education = new Education();
                            education.UserInfoID = bllUser.FindID(UserInfoName.Text.Trim());
                            education.SchoolName = SchoolName.Text.Trim();
                            education.College = College.Text.Trim();
                            education.EduTime = DatePickerEduTime.SelectedDate;
                            education.Degree = DropDownListDegree.SelectedItem.Text;
                            education.Major = Major.Text.Trim();
                            education.Series = Series.Text.Trim();
                            education.SecrecyLevel = DropDownListSecrecyLevel.SelectedIndex + 1;
                            education.EntryPerson = bllUser.FindByLoginName(Session["LoginName"].ToString()).UserName;
                            education.DegreeNumber = DegreeNumber.Text.Trim();
                            education.GraduateNumber = GraduateNumber.Text.Trim();
                            if (Convert.ToInt32(Session["SecrecyLevel"]) != 5)
                            {
                                education.IsPass = false;
                                bllEducation.InsertForPeople(education);//插入学历表
                                OperationLog operate = new OperationLog();
                                operate.LoginName = bllUser.FindByLoginName(Session["LoginName"].ToString()).UserName;
                                operate.LoginIP = "";
                                operate.OperationType = "添加";
                                operate.OperationContent = "Education";
                                operate.OperationDataID = education.EducationID;
                                operate.OperationTime = System.DateTime.Now;
                                operate.Remark = "";
                                bllOperate.Insert(operate);//插入操作表
                                PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("人员学历信息已提交审核！"));
                            }
                            else
                            {
                                education.IsPass = true;
                                bllEducation.InsertForPeople(education);//插入学历表
                                PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("人员学历已添加完成！"));
                            }
                        }
                        else
                            Alert.ShowInTop("该人员尚未通过审核！");
                    }
                    else
                    {
                        Alert.ShowInTop("人员不存在！");
                        UserInfoName.Text = "";
                        return;
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
                pm.SaveError(ex, this.Request);
            }
        }
        //初始化学位下拉框
        public void InitDropDownListDegree()
        {
            try
            {
                List<BasicCode> list = bllBasicCode.FindALLName("学位");
                for (int i = 0; i < list.Count(); i++)
                {
                    DropDownListDegree.Items.Add(list[i].CategoryContent.ToString(), list[i].CategoryContent.ToString());
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
       //重置
        protected void Reset_Click(object sender, EventArgs e)
        {
            try
            {
                UserInfoName.Reset();
                SchoolName.Reset();
                College.Reset();
                DatePickerEduTime.Reset();
                DropDownListDegree.Reset();
                Major.Reset();
                Series.Reset();
                DropDownListSecrecyLevel.Reset();
                DegreeNumber.Reset();
                GraduateNumber.Reset();
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
    }
}