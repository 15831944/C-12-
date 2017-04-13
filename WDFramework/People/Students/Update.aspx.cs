/*
 * 作者：未知
 * 修改履历：   1.修改人：吕博扬
 *              修改时间：2015年9月23日
 *              修改内容：取消所有输入项的数据校验
 *             2.修改人：吕博扬
 *              修改时间：2015年10月18日
 *              修改内容：取消入学、毕业时间的限制
 */
using Common.Entities;
using FineUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WDFramework.People
{
    public partial class Update : System.Web.UI.Page
    {
        BLHelper.BLLAgency BLLAgency = new BLHelper.BLLAgency();
        BLHelper.BLLOperationLog bllOperate = new BLHelper.BLLOperationLog();
        BLHelper.BLLStudent bllStudent = new BLHelper.BLLStudent();
        BLHelper.BLLUser bllUser = new BLHelper.BLLUser();
        BLHelper.BLLBasicCode bllBasicCode = new BLHelper.BLLBasicCode();
        BLCommon.PublicMethod publicmethod = new BLCommon.PublicMethod();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitDropDownList_Agency();
                InitDropDownListDocumentType();
                InitDropDownListType();
                BindData();
                //DatePickerEnterTime.MaxDate = DateTime.Now;
                //DatePickerGraduationTime.MaxDate = DateTime.Now;
            }
        }
        public void BindData()
        {
            try
            {
                Student stu = bllStudent.FindStudents(Convert.ToInt32(Session["StudentID"]));
                T_Sno.Text = stu.Sno;
                T_SName.Text = stu.Sname;
                if (stu.Sex == true)
                {
                    rbtnBoy.Checked = true;
                }
                else
                    rbtnGril.Checked = true;
                DropDownListDocumentType.SelectedValue = stu.DocumentType;
                T_DocumentNumber.Text = stu.DocumentNumber;
                T_Contact.Text = stu.Contact;
                if (stu.IsGraduation == true)
                {
                    IsGraduation.Checked = true;
                }
                else
                    NotGraduation.Checked = true;
                T_Specialty.Text = stu.Specialty;
                T_SResearch.Text = stu.SResearch;
                T_SGraduationDirection.Text = stu.SGraduationDirection;
                DropDownListType.SelectedValue = stu.Type;
                T_UserInfoID.Text = bllUser.FindByUserID(Convert.ToInt32(stu.UserInfoID));
                DatePickerEnterTime.SelectedDate = stu.EnterTime;
                DatePickerGraduationTime.SelectedDate = stu.GraduationTime;
                DropDownListSecrecyLevel.SelectedIndex = Convert.ToInt32(stu.SecrecyLevel - 1);
                //DropDownList_Agency.SelectedValue = BLLAgency.FindByid(Convert.ToInt16(stu.AgencyID)).AgencyName;
                //newstudent.EntryPerson = Session["LoginName"].ToString();
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }
        //保存
        protected void Save_Click(object sender, EventArgs e)
        {
            try
            {
                //学号不能修改，更新时不需判断                
                //if (T_UserInfoID.Text.Trim() != "")
                //{
                    if (bllUser.IsUser(T_UserInfoID.Text.Trim()) != null)
                    {
                        if (bllUser.IsUser(T_UserInfoID.Text.Trim()).IsPass == true)
                        {
                            //if (T_Sno.Text.Trim() == "")
                            //{
                            //    Alert.ShowInTop("学号不能为空！");
                            //    T_Sno.Text = "";
                            //    return;
                            //}
                            //if (T_SName.Text.Trim() == "")
                            //{
                            //    Alert.ShowInTop("姓名不能为空！");
                            //    T_SName.Text = "";
                            //    return;
                            //}
                            //if (T_Specialty.Text.Trim() == "")
                            //{
                            //    Alert.ShowInTop("专业不能为空！");
                            //    T_Specialty.Text = "";
                            //    return;
                            //}
                            //if (T_SResearch.Text.Trim() == "")
                            //{
                            //    Alert.ShowInTop("研究方向不能为空！");
                            //    T_SResearch.Text = "";
                            //    return;
                            //}
                            Student newstudent = new Student();
                            newstudent.Sno = T_Sno.Text.Trim();
                            newstudent.Sname = T_SName.Text.Trim();
                            if (rbtnBoy.Checked == true)
                            {
                                newstudent.Sex = true;
                            }
                            else
                                newstudent.Sex = false;
                            newstudent.DocumentType = DropDownListDocumentType.SelectedItem.Text;
                            newstudent.DocumentNumber = T_DocumentNumber.Text.Trim();
                            newstudent.Contact = T_Contact.Text.Trim();
                            if (IsGraduation.Checked == true)
                            {
                                newstudent.IsGraduation = true;
                            }
                            else
                                newstudent.IsGraduation = false;
                            newstudent.Specialty = T_Specialty.Text.Trim();
                            newstudent.SResearch = T_SResearch.Text.Trim();
                            newstudent.SGraduationDirection = T_SGraduationDirection.Text.Trim();
                            newstudent.Type = DropDownListType.SelectedItem.Text.Trim();
                            newstudent.UserInfoID = bllUser.FindID(T_UserInfoID.Text);
                            newstudent.EnterTime = DatePickerEnterTime.SelectedDate;
                            newstudent.AgencyID = BLLAgency.SelectAgencyID(DropDownList_Agency.SelectedText);
                            if (DatePickerGraduationTime.SelectedDate.HasValue)
                            {
                                if (DatePickerGraduationTime.SelectedDate < DatePickerEnterTime.SelectedDate)
                                {
                                    Alert.ShowInTop("毕业时间不能小于入学时间！");
                                    return;
                                }
                                else
                                    newstudent.GraduationTime = DatePickerGraduationTime.SelectedDate;
                            }
                            newstudent.SecrecyLevel = DropDownListSecrecyLevel.SelectedIndex + 1;
                            newstudent.EntryPerson = bllStudent.FindStudents(Convert.ToInt32(Session["StudentID"])).EntryPerson;
                            if (Convert.ToInt32(Session["SecrecyLevel"]) != 5)
                            {
                                bllStudent.UpdateIsPass(Convert.ToInt32(Session["StudentID"]), false);//将被更新的数据状态变为false
                                newstudent.IsPass = false;
                                bllStudent.InsertForPeople(newstudent);//插入学生情况表
                                OperationLog operate = new OperationLog();
                                operate.LoginName = bllStudent.FindStudents(Convert.ToInt32(Session["StudentID"])).EntryPerson;
                                operate.LoginIP = "";
                                operate.OperationType = "更新";
                                operate.OperationContent = "Student";
                                operate.OperationDataID = Convert.ToInt32(Session["StudentID"]);
                                operate.OperationTime = System.DateTime.Now;
                                operate.Remark = newstudent.StudentID.ToString();
                                bllOperate.Insert(operate);//插入操作表
                                PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("学生情况信息已提交审核！"));
                            }
                            else
                            {
                                newstudent.StudentID = Convert.ToInt32(Session["StudentID"]);
                                newstudent.IsPass = true;
                                bllStudent.Update(newstudent);//更新学生情况表
                                PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("学生情况信息已修改完成！"));
                            }
                        }
                        else
                            Alert.ShowInTop("授课老师尚未通过审核！");
                    }
                    else
                    {
                        Alert.ShowInTop("授课老师不存在！");
                        T_UserInfoID.Text = "";
                    }
                //}
                //else
                //{
                //    Alert.ShowInTop("授课老师不能为空！");
                //    T_UserInfoID.Text = "";
                //    return;
                //}
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
                T_Sno.Reset();
                T_SName.Reset();
                rbtnBoy.Reset();
                rbtnGril.Reset();
                DropDownListDocumentType.Reset();
                T_DocumentNumber.Reset();
                T_Contact.Reset();
                IsGraduation.Reset();
                NotGraduation.Reset();
                T_Specialty.Reset();
                T_SResearch.Reset();
                T_SGraduationDirection.Reset();
                DropDownListType.Reset();
                T_UserInfoID.Reset();
                DatePickerEnterTime.Reset();
                DatePickerGraduationTime.Reset();
                DropDownListSecrecyLevel.Reset();
                DropDownList_Agency.Reset();
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }
        //初始化证件类型下拉框
        public void InitDropDownListDocumentType()
        {
            try
            {
                List<BasicCode> list = bllBasicCode.FindALLName("证件类型");
                for (int i = 0; i < list.Count(); i++)
                {

                    DropDownListDocumentType.Items.Add(list[i].CategoryContent.ToString(), list[i].CategoryContent.ToString());
                }
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }
        //初始化所属机构下拉框
        public void InitDropDownList_Agency()
        {
            try
            {
                List<Common.Entities.Agency> list = BLLAgency.FindAll(Convert.ToInt32(Session["SecrecyLevel"]));
                for (int i = 0; i < list.Count(); i++)
                {
                    DropDownList_Agency.Items.Add(list[i].AgencyName.ToString(), (i + 1).ToString());
                }
                
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }
        //初始化学生类型下拉框
        public void InitDropDownListType()
        {
            try
            {
                List<BasicCode> list = bllBasicCode.FindALLName("学生类型");
                for (int i = 0; i < list.Count(); i++)
                {

                    DropDownListType.Items.Add(list[i].CategoryContent.ToString(), list[i].CategoryContent.ToString());
                }
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }
    }
}