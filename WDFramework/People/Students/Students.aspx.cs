/**编写人：王会会
 * 时间：2014年8月12号
 * 功能：学生情况基本信息的相关操作
 * 修改履历：   1.修改人：吕博扬
 *             修改内容： 添加了按入学年份、毕业年份、学生类型、所属部门等方面查询的功能
 *             2.修改人：陈起明
 *             修改时间：10月10日
 *             修改内容：撤消了静态变量page
 **/
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
    public partial class Students : System.Web.UI.Page
    {
        BLHelper.BLLAgency BLLAgency = new BLHelper.BLLAgency();
        BLHelper.BLLOperationLog bllOperate = new BLHelper.BLLOperationLog();
        BLHelper.BLLUser bllUser = new BLHelper.BLLUser();
        BLHelper.BLLStudent bllStudent = new BLHelper.BLLStudent();
        BLCommon.PublicMethod publicmethod = new BLCommon.PublicMethod();
        BLHelper.BLLBasicCode bllBasicCode = new BLHelper.BLLBasicCode();
        private int page;
        protected void Page_Load(object sender, EventArgs e)
        {
            page = ViewState["page"] == null ? 0 : (int)ViewState["page"];
            if (!IsPostBack)
            {
                //添加数据
                btnAddProject.OnClientClick = WindowStudent.GetShowReference("Add_Students.aspx");
                BindData();
                btnDelete.Enabled = false;
                TriggerBox.Enabled = false;
                DropDownList.Enabled = false;
            }
        }
        public void BindData()
        {
            try
            {
                ViewState["page"] = 0;
                List<Student> StudentList = bllStudent.FindPaged(Convert.ToInt32(Session["SecrecyLevel"]));
                GridProjectStudent.RecordCount = StudentList.Count();
                var result = StudentList.Skip(GridProjectStudent.PageIndex * GridProjectStudent.PageSize).Take(GridProjectStudent.PageSize).ToList();
                this.GridProjectStudent.DataSource = result;
                this.GridProjectStudent.DataBind();
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }
        //根据是否毕业和学生类型查询
        public void FindSdudent(string type, bool? IsGratudation)
        {
            try
            {
                ViewState["page"] = 1;
                List<Student> StudentList = bllStudent.FindStudent(type, IsGratudation);
                GridProjectStudent.RecordCount = StudentList.Count();
                var result = StudentList.Skip(GridProjectStudent.PageIndex * GridProjectStudent.PageSize).Take(GridProjectStudent.PageSize).ToList();
                this.GridProjectStudent.DataSource = result;
                this.GridProjectStudent.DataBind();
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }
        //根据是否毕业，学生类型和学生姓名查询(学生姓名是模糊查)
        public void FindStudentName(string type, bool? IsGratudation, string StudentName)
        {
            try
            {
                ViewState["page"] = 2;
                List<Student> StudentList = bllStudent.FindStudentName(type, IsGratudation, TriggerBox.Text.Trim());
                GridProjectStudent.RecordCount = StudentList.Count();
                var result = StudentList.Skip(GridProjectStudent.PageIndex * GridProjectStudent.PageSize).Take(GridProjectStudent.PageSize).ToList();
                this.GridProjectStudent.DataSource = result;
                this.GridProjectStudent.DataBind();
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }

        //根据指导教师查询
        public void FindByTeacher()
        {
            try
            {
                if (TriggerBox.Text.Trim() == "")
                {
                    Alert.ShowInTop("请输入搜索内容");
                    return;
                }
                ViewState["page"] = 3;
                int teanum = bllUser.FindByUserName(TriggerBox.Text.Trim()).UserInfoID;
                List<Student> StudentList = bllStudent.FindByTeacher(teanum);
                GridProjectStudent.RecordCount = StudentList.Count();
                var result = StudentList.Skip(GridProjectStudent.PageIndex * GridProjectStudent.PageSize).Take(GridProjectStudent.PageSize).ToList();
                this.GridProjectStudent.DataSource = result;
                this.GridProjectStudent.DataBind();
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }

        //根据学生姓名查询
        public void FindByName()
        {
            try
            {
                if (TriggerBox.Text.Trim() == "")
                {
                    Alert.ShowInTop("请输入搜索内容");
                    return;
                }
                ViewState["page"] = 4;
                List<Student> StudentList = bllStudent.FindByName(TriggerBox.Text.Trim());
                GridProjectStudent.RecordCount = StudentList.Count();
                var result = StudentList.Skip(GridProjectStudent.PageIndex * GridProjectStudent.PageSize).Take(GridProjectStudent.PageSize).ToList();
                this.GridProjectStudent.DataSource = result;
                this.GridProjectStudent.DataBind();
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }

        //根据学生专业查询
        public void FindByMajor()
        {
            try
            {
                if (TriggerBox.Text.Trim() == "")
                {
                    Alert.ShowInTop("请输入搜索内容");
                    return;
                }
                ViewState["page"] = 5;
                List<Student> StudentList = bllStudent.FindByName(TriggerBox.Text.Trim());
                GridProjectStudent.RecordCount = StudentList.Count();
                var result = StudentList.Skip(GridProjectStudent.PageIndex * GridProjectStudent.PageSize).Take(GridProjectStudent.PageSize).ToList();
                this.GridProjectStudent.DataSource = result;
                this.GridProjectStudent.DataBind();
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }

        //根据所属部门查询
        public void FindByDepartment()
        {
            try
            {
                ViewState["page"] = 6;
                BLHelper.BLLAgency BLLAgency = new BLHelper.BLLAgency();
                List<Student> StudentList = bllStudent.FindByDepartment(BLLAgency.SelectAgencyID(DropDownList.SelectedItem.Text));
                GridProjectStudent.RecordCount = StudentList.Count();
                var result = StudentList.Skip(GridProjectStudent.PageIndex * GridProjectStudent.PageSize).Take(GridProjectStudent.PageSize).ToList();
                this.GridProjectStudent.DataSource = result;
                this.GridProjectStudent.DataBind();
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }

        //根据学生类型查询
        public void FindByStudentType()
        {
            try
            {
                ViewState["page"] = 7;
                List<Student> StudentList = bllStudent.FindByStudentType(DropDownList.SelectedItem.Text);
                GridProjectStudent.RecordCount = StudentList.Count();
                var result = StudentList.Skip(GridProjectStudent.PageIndex * GridProjectStudent.PageSize).Take(GridProjectStudent.PageSize).ToList();
                this.GridProjectStudent.DataSource = result;
                this.GridProjectStudent.DataBind();
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }

        //根据入学年份查询
        public void FindByEnterYear()
        {
            try
            {
                if (TriggerBox.Text.Trim() == "")
                {
                    Alert.ShowInTop("请输入搜索内容");
                    return;
                }
                ViewState["page"] = 8;
                DataBase.DataBaseContext dbcontext = new DataBase.DataBaseContext();
                List<Student> StudentList = dbcontext.StudentContext.ToList();
                for (int i = 0; i < StudentList.Count; ++i)
                    if (StudentList[i].EnterTime.ToString().Substring(0,4) != TriggerBox.Text.Trim())
                    {
                        StudentList.RemoveAt(i);
                        --i;
                    }
                GridProjectStudent.RecordCount = StudentList.Count();
                var result = StudentList.Skip(GridProjectStudent.PageIndex * GridProjectStudent.PageSize).Take(GridProjectStudent.PageSize).ToList();
                this.GridProjectStudent.DataSource = result;
                this.GridProjectStudent.DataBind();
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }

        //根据毕业年份查询
        public void FindByGraduationYear()
        {
            try
            {
                if (TriggerBox.Text.Trim() == "")
                {
                    Alert.ShowInTop("请输入搜索内容");
                    return;
                }
                ViewState["page"] = 9;
                DataBase.DataBaseContext dbcontext = new DataBase.DataBaseContext();
                List<Student> StudentList = dbcontext.StudentContext.ToList();
                for (int i = 0; i < StudentList.Count; ++i)
                    if (StudentList[i].GraduationTime.ToString().Substring(0, 4) != TriggerBox.Text.Trim())
                    {
                        StudentList.RemoveAt(i);
                        --i;
                    }
                GridProjectStudent.RecordCount = StudentList.Count();
                var result = StudentList.Skip(GridProjectStudent.PageIndex * GridProjectStudent.PageSize).Take(GridProjectStudent.PageSize).ToList();
                this.GridProjectStudent.DataSource = result;
                this.GridProjectStudent.DataBind();
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }

        //刷新
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            BindData();
            TriggerBox.Reset();
            DropDownStudentType.SelectedValue = "全部";
            TriggerBox.Enabled = false;
            DropDownList.Enabled = false;
        }
      
        //GridProjectStudent行命令
        protected void GridProjectStudent_RowCommand(object sender, FineUI.GridCommandEventArgs e)
        {
            try
            {
                if (Session["LoginName"].ToString() == "")
                {
                    Response.Redirect("login.aspx");
                    Alert.Show("登录超时！");
                }
                string Person = GridProjectStudent.Rows[e.RowIndex].Values[2].ToString();
                string strs = bllUser.FindByLoginName(Session["LoginName"].ToString()).UserName;

                if (Person != strs && Convert.ToInt32(Session["SecrecyLevel"]) != 5)
                {
                    string str = "您无对此行操作的权限！此行信息为" + Person + "录入，请与管理员联系!";
                    CBoxSelect.SetCheckedState(e.RowIndex, false);
                    Alert.ShowInTop(str);
                }
                if (publicmethod.GridCount(GridProjectStudent, CBoxSelect).Count == 0)
                {
                    btnDelete.Enabled = false;
                    return;
                }
                if (publicmethod.GridCount(GridProjectStudent, CBoxSelect).Count != 0)
                {
                    btnDelete.Enabled = true;
                    return;
                }
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }
        //分页页数
        protected void GridProjectStudent_PageIndexChange(object sender, FineUI.GridPageEventArgs e)
        {
            GridProjectStudent.PageIndex = e.NewPageIndex;
            switch (page)
            {
                case 0:
                    BindData();
                    break;
                case 1:
                    {
                        bool IsGraduate = false;
                        if (DropDownStudentType.SelectedItem.Text == "全部")
                        {
                            BindData();
                        }
                        else
                        {
                            if (DropDownStudentType.SelectedItem.Text == "在读")
                                IsGraduate = false;
                            else if (DropDownStudentType.SelectedItem.Text == "毕业")
                                IsGraduate = true;
                            FindSdudent(DropDownList.SelectedItem.Text, IsGraduate);
                        }
                        //switch (DropDownStudentType.SelectedItem.Text)
                        //{
                        //    case "全部":
                        //        BindData();
                        //        break;
                        //    case "在读本科生":
                        //        FindSdudent("本科生", false);
                        //        break;
                        //    case "毕业本科生":
                        //        FindSdudent("本科生", true);
                        //        break;
                        //    case "在读研究生":
                        //        FindSdudent("研究生", false);
                        //        break;
                        //    case "毕业研究生":
                        //        FindSdudent("研究生", true);
                        //        break;
                        //    case "在读博士生":
                        //        FindSdudent("博士生", false);
                        //        break;
                        //    case "毕业博士生":
                        //        FindSdudent("博士生", true);
                        //        break;
                        //    case "在读博士后":
                        //        FindSdudent("博士后", false);
                        //        break;
                        //    case "毕业博士后":
                        //        FindSdudent("博士后", true);
                        //        break;
                        //}
                    }
                    break;
                case 2:
                    {
                        bool IsGraduate = false;
                        if (DropDownStudentType.SelectedItem.Text == "全部")
                        {
                            BindData();
                        }
                        else
                        {
                            if (DropDownStudentType.SelectedItem.Text == "在读")
                                IsGraduate = false;
                            else if (DropDownStudentType.SelectedItem.Text == "毕业")
                                IsGraduate = true;
                            if (!string.IsNullOrEmpty(TriggerBox.Text.Trim()))
                                FindStudentName(DropDownList.SelectedItem.Text, IsGraduate, TriggerBox.Text.Trim());
                        }
                    }
                    break;
                case 3:
                    FindByTeacher();
                    break;
                case 4:
                    FindByName();
                    break;
                case 5:
                    FindByMajor();
                    break;
                case 6:
                    FindByDepartment();
                    break;
                case 7:
                    FindByStudentType();
                    break;
                case 8:
                    FindByEnterYear();
                    break;
                case 9:
                    FindByGraduationYear();
                    break;
            }
        }
        //分页每页项的个数
        protected void ddlGridPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridProjectStudent.PageIndex = 0;
            this.GridProjectStudent.PageSize = Convert.ToInt32(ddlGridPageSize.SelectedValue);
           switch (page)
            {
                case 0:
                    BindData();
                    break;
                case 1:
                    {
                        bool IsGraduate = false;
                        if (DropDownStudentType.SelectedItem.Text == "全部")
                        {
                            BindData();
                        }
                        else
                        {
                            if (DropDownStudentType.SelectedItem.Text == "在读")
                                IsGraduate = false;
                            else if (DropDownStudentType.SelectedItem.Text == "毕业")
                                IsGraduate = true;
                            FindSdudent(DropDownList.SelectedItem.Text, IsGraduate);
                        }                   
                    }
                    break;
                case 2:
                    {
                        bool IsGraduate = false;
                        if (DropDownStudentType.SelectedItem.Text == "全部")
                        {
                            BindData();
                        }
                        else
                        {
                            if (DropDownStudentType.SelectedItem.Text == "在读")
                                IsGraduate = false;
                            else if (DropDownStudentType.SelectedItem.Text == "毕业")
                                IsGraduate = true;
                            if (!string.IsNullOrEmpty(TriggerBox.Text.Trim()))
                                FindStudentName(DropDownList.SelectedItem.Text, IsGraduate, TriggerBox.Text.Trim());
                        }
                    }
                    break;
                case 3:
                    FindByTeacher();
                    break;
                case 4:
                    FindByName();
                    break;
                case 5:
                    FindByMajor();
                    break;
               case 6:
                    FindByDepartment();
                    break;
               case 7:
                    FindByStudentType();
                    break;
               case 8:
                    FindByEnterYear();
                    break;
               case 9:
                    FindByGraduationYear();
                    break;
            }
        }
        //判断男女
        public string getgender(string bx)
        {
            try
            {
                return bllUser.getgender(bx);
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
                return "";
            }
        }
        //根据UserID查找Username
        public string UserName(int UserID)
        {
            try
            {
                if (UserID != 0)
                    return bllUser.FindUserName(UserID);
                else
                    return "";
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
                return "";
            }
        }
        //根据StudentID查找所属部门
        public string FindAgency(int StudentID)
        {
            try
            {
                Student student= bllStudent.FindStudents(StudentID);
                return BLLAgency.FindByid(Convert.ToInt16(student.AgencyID)).AgencyName;
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
                return "";
            }
        }
        //判断是否毕业
        public string IsNullGraduation(string xb)
        {
            try
            {
                if (xb == "True")
                    return "是";
                else
                    return "否";
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
                return "";
            }
        }
        //查询
        protected void FindDevoteTime_Click(object sender, EventArgs e)
        {
            bool IsGraduate = false;
            GridProjectStudent.PageIndex = 0;

            switch (DropDownStudentType.SelectedItem.Text)
            {
                case "全部":
                    break;
                case "指导教师":
                    FindByTeacher();
                    break;
                case "姓名":
                    FindByName();
                    break;
                case "专业":
                    FindByMajor();
                    break;
                case "学生类型":
                    FindByStudentType();
                    break;
                case "所属部门":
                    FindByDepartment();
                    break;
                case "入学年份":
                    FindByEnterYear();
                    break;
                case "毕业年份":
                    FindByGraduationYear();
                    break;
                default:
                     if (DropDownStudentType.SelectedItem.Text == "在读")
                    IsGraduate = false;
                else if (DropDownStudentType.SelectedItem.Text == "毕业")
                    IsGraduate = true;
                if (!string.IsNullOrEmpty(TriggerBox.Text.Trim()))
                    FindStudentName(DropDownList.SelectedItem.Text, IsGraduate, TriggerBox.Text.Trim());
                else
                    FindSdudent(DropDownList.SelectedItem.Text, IsGraduate);
                    break;
            }

            if (DropDownStudentType.SelectedItem.Text == "全部")
            {
                BindData();
            }
            else if (DropDownStudentType.SelectedItem.Text == "在读" || DropDownStudentType.SelectedItem.Text == "毕业")
            {
                if (DropDownStudentType.SelectedItem.Text == "在读")
                    IsGraduate = false;
                else if (DropDownStudentType.SelectedItem.Text == "毕业")
                    IsGraduate = true;
                if (!string.IsNullOrEmpty(TriggerBox.Text.Trim()))
                    FindStudentName(DropDownList.SelectedItem.Text, IsGraduate, TriggerBox.Text.Trim());
                else
                    FindSdudent(DropDownList.SelectedItem.Text, IsGraduate);
            }
            //switch (DropDownStudentType.SelectedItem.Text)
            //{
            //    case "全部":
            //        BindData();
            //        break;
            //    case "在读本科生":
            //        if (!string.IsNullOrEmpty(TriggerBox.Text.Trim()))
            //            FindStudentName("本科生", false, TriggerBox.Text.Trim());
            //        else
            //            FindSdudent("本科生", false);
            //        break;
            //    case "毕业本科生":
            //        if (!string.IsNullOrEmpty(TriggerBox.Text.Trim()))
            //            FindStudentName("本科生", true, TriggerBox.Text.Trim());
            //        else
            //            FindSdudent("本科生", true);
            //        break;
            //    case "在读研究生":
            //        if (!string.IsNullOrEmpty(TriggerBox.Text.Trim()))
            //            FindStudentName("研究生", false, TriggerBox.Text.Trim());
            //        else
            //            FindSdudent("研究生", false);
            //        break;
            //    case "毕业研究生":
            //        if (!string.IsNullOrEmpty(TriggerBox.Text.Trim()))
            //            FindStudentName("研究生", true, TriggerBox.Text.Trim());
            //        else
            //            FindSdudent("研究生", true);
            //        break;
            //    case "在读博士生":
            //        if (!string.IsNullOrEmpty(TriggerBox.Text.Trim()))
            //            FindStudentName("博士生", false, TriggerBox.Text.Trim());
            //        else
            //            FindSdudent("博士生", false);
            //        break;
            //    case "毕业博士生":
            //        if (!string.IsNullOrEmpty(TriggerBox.Text.Trim()))
            //            FindStudentName("博士生", true, TriggerBox.Text.Trim());
            //        else
            //            FindSdudent("博士生", true);
            //        break;
            //    case "在读博士后":
            //        if (!string.IsNullOrEmpty(TriggerBox.Text.Trim()))
            //            FindStudentName("博士后", false, TriggerBox.Text.Trim());
            //        else
            //            FindSdudent("博士后", false);
            //        break;
            //    case "毕业博士后":
            //        if (!string.IsNullOrEmpty(TriggerBox.Text.Trim()))
            //            FindStudentName("博士后", true, TriggerBox.Text.Trim());
            //        else
            //            FindSdudent("博士后", true);
            //        break;
            //}
        }
        //删除
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                List<int> selections = publicmethod.GridCount(GridProjectStudent, CBoxSelect);
                if (Convert.ToInt32(Session["SecrecyLevel"]) == 5)
                {
                    for (int i = 0; i < selections.Count(); i++)
                    {
                        bllStudent.Delete(Convert.ToInt32(GridProjectStudent.DataKeys[selections[i]][0]));
                    }
                    BindData();
                    Alert.ShowInTop("删除成功!");
                    btnSelect_All.Text = "全选";
                }
                else
                {
                    for (int i = 0; i < selections.Count(); i++)
                    {
                        bllStudent.UpdateIsPass(Convert.ToInt32(GridProjectStudent.DataKeys[selections[i]][0]), false);
                        //向操作日志表中插入
                        OperationLog operate = new OperationLog();
                        operate.LoginName = bllUser.FindByLoginName(Session["LoginName"].ToString()).UserName;
                        operate.LoginIP = "";
                        operate.OperationType = "删除";
                        operate.OperationContent = "Student";
                        operate.OperationDataID = Convert.ToInt32(GridProjectStudent.DataKeys[selections[i]][0]);
                        operate.OperationTime = System.DateTime.Now;
                        operate.Remark = "";
                        bllOperate.Insert(operate);                       
                    }
                    Alert.ShowInTop("您的操作已提交，请等待审核！");
                    BindData();
                    btnSelect_All.Text = "全选";
                }
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }
        //修改
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                List<int> selections = publicmethod.GridCount(GridProjectStudent, CBoxSelect);
                if (selections.Count() != 0)
                {
                    if (selections.Count() == 1)
                    {
                        int rowID = Convert.ToInt32(GridProjectStudent.DataKeys[selections[0]][0]);
                        Session["StudentID"] = rowID;
                        Alert.Show("你确定要修改该行数据吗!", "确认消息", MessageBoxIcon.Information, WindowUpdate.GetShowReference("Update.aspx"), Target.Top);
                    }
                    else
                    {
                        Alert.Show("一次仅可以对一行进行编辑！");
                    }
                }
                else
                {
                    Alert.Show("请选择一行！");
                }
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }
        //等级名称
        public string SecrecyLevelName(int level)
        {
            try
            {
                string[] SecrecyLevels = new string[] { "四级", "三级", "二级", "一级", "管理员" };
                //string[] SecrecyLevels = new string[] { "公开", "内部", "秘密", "机密", "管理员" };
                return SecrecyLevels[level - 1];
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
                return "";
            }
        }
        //导出
        protected void btn_Get_Click(object sender, EventArgs e)
        {
            try
            {
                switch (page)
                {
                    case 0:
                        List<Student> StudentList = bllStudent.FindPaged(Convert.ToInt32(Session["SecrecyLevel"]));
                        this.GridProjectStudent.DataSource = StudentList;
                        this.GridProjectStudent.DataBind();
                        break;
                    case 1:
                        {
                            bool IsGraduate = false;
                            if (DropDownStudentType.SelectedItem.Text == "全部")
                            {
                                List<Student> List2 = bllStudent.FindPaged(Convert.ToInt32(Session["SecrecyLevel"]));
                                this.GridProjectStudent.DataSource = List2;
                                this.GridProjectStudent.DataBind();
                            }
                            else
                            {
                                if (DropDownStudentType.SelectedItem.Text == "在读")
                                    IsGraduate = false;
                                else if (DropDownStudentType.SelectedItem.Text == "毕业")
                                    IsGraduate = true;
                                FindSdudents(DropDownList.SelectedItem.Text, IsGraduate);
                            }
                            //switch (DropDownStudentType.SelectedItem.Text)
                            //{
                            //    case "全部":
                            //        List<Student> List2 = bllStudent.FindPaged(Convert.ToInt32(Session["SecrecyLevel"]));
                            //        this.GridProjectStudent.DataSource = List2;
                            //        this.GridProjectStudent.DataBind();
                            //        break;
                            //    case "在读本科生":
                            //        FindSdudents("本科生", false);
                            //        break;
                            //    case "毕业本科生":
                            //        FindSdudents("本科生", true);
                            //        break;
                            //    case "在读研究生":
                            //        FindSdudents("研究生", false);
                            //        break;
                            //    case "毕业研究生":
                            //        FindSdudents("研究生", true);
                            //        break;
                            //    case "在读博士生":
                            //        FindSdudents("博士生", false);
                            //        break;
                            //    case "毕业博士生":
                            //        FindSdudents("博士生", true);
                            //        break;
                            //    case "在读博士后":
                            //        FindSdudents("博士后", false);
                            //        break;
                            //    case "毕业博士后":
                            //        FindSdudents("博士后", true);
                            //        break;
                            //}
                        }
                        break;
                    case 2:
                        {
                            bool IsGraduate = false;
                            if (DropDownStudentType.SelectedItem.Text == "全部")
                            {
                                List<Student> List1 = bllStudent.FindPaged(Convert.ToInt32(Session["SecrecyLevel"]));
                                this.GridProjectStudent.DataSource = List1;
                                this.GridProjectStudent.DataBind();
                            }
                            else
                            {
                                if (DropDownStudentType.SelectedItem.Text == "在读")
                                    IsGraduate = false;
                                else if (DropDownStudentType.SelectedItem.Text == "毕业")
                                    IsGraduate = true;
                                if (!string.IsNullOrEmpty(TriggerBox.Text.Trim()))
                                    FindStudentNames(DropDownList.SelectedItem.Text, IsGraduate, TriggerBox.Text.Trim());
                            }
                        }
                        //switch (DropDownStudentType.SelectedItem.Text)
                        //{
                        //    case "全部":
                        //        List<Student> List1 = bllStudent.FindPaged(Convert.ToInt32(Session["SecrecyLevel"]));
                        //        this.GridProjectStudent.DataSource = List1;
                        //        this.GridProjectStudent.DataBind();
                        //        break;
                        //    case "在读本科生":
                        //        if (!string.IsNullOrEmpty(TriggerBox.Text.Trim()))
                        //            FindStudentNames("本科生", false, TriggerBox.Text.Trim());
                        //        break;
                        //    case "毕业本科生":
                        //        if (!string.IsNullOrEmpty(TriggerBox.Text.Trim()))
                        //            FindStudentNames("本科生", true, TriggerBox.Text.Trim());
                        //        break;
                        //    case "在读研究生":
                        //        if (!string.IsNullOrEmpty(TriggerBox.Text.Trim()))
                        //            FindStudentNames("研究生", false, TriggerBox.Text.Trim());
                        //        break;
                        //    case "毕业研究生":
                        //        if (!string.IsNullOrEmpty(TriggerBox.Text.Trim()))
                        //            FindStudentNames("研究生", true, TriggerBox.Text.Trim());
                        //        break;
                        //    case "在读博士生":
                        //        if (!string.IsNullOrEmpty(TriggerBox.Text.Trim()))
                        //            FindStudentNames("博士生", false, TriggerBox.Text.Trim());
                        //        break;
                        //    case "毕业博士生":
                        //        if (!string.IsNullOrEmpty(TriggerBox.Text.Trim()))
                        //            FindStudentNames("博士生", true, TriggerBox.Text.Trim());
                        //        break;
                        //    case "在读博士后":
                        //        if (!string.IsNullOrEmpty(TriggerBox.Text.Trim()))
                        //            FindStudentNames("博士后", false, TriggerBox.Text.Trim());
                        //        break;
                        //    case "毕业博士后":
                        //        if (!string.IsNullOrEmpty(TriggerBox.Text.Trim()))
                        //            FindStudentNames("博士后", true, TriggerBox.Text.Trim());
                        //        break;
                        //}
                        break;
                }
                publicmethod.ExportExcel(3, GridProjectStudent, 0);
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }
      //根据是否毕业和学生类型查询
        public void FindSdudents(string type, bool? IsGratudation)
        {
            try
            {
                List<Student> StudentList = bllStudent.FindStudent(type, IsGratudation);
                this.GridProjectStudent.DataSource = StudentList;
                this.GridProjectStudent.DataBind();
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }
    
        //根据是否毕业，学生类型和学生姓名查询
        public void FindStudentNames(string type, bool? IsGratudation, string StudentName)
        {
            try
            {

                List<Student> StudentList = bllStudent.FindStudentName(type, IsGratudation, TriggerBox.Text.Trim());
                this.GridProjectStudent.DataSource = StudentList;
                this.GridProjectStudent.DataBind();
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }
        //搜索框变化
        protected void DropDownStudentType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownStudentType.SelectedItem.Text == "在读" || DropDownStudentType.SelectedItem.Text == "毕业")
            {
                TriggerBox.Text = "";
                TriggerBox.Enabled = true;
                DropDownList.Enabled = true;
                DropDownList.Items.Clear();
                StudentType();
                DropDownList.EnableEdit = false;

            }
            else if (DropDownStudentType.SelectedItem.Text == "全部")
            {
                TriggerBox.Enabled = false;
                DropDownList.Enabled = false;
                DropDownList.EnableEdit = false;
            }
            else if (DropDownStudentType.SelectedItem.Text == "所属部门")
            {
                TriggerBox.Enabled = false;
                DropDownList.Enabled = true;
                DropDownList.Items.Clear();
                DropDownList.EnableEdit = false;
                try
                {
                    List<Common.Entities.Agency> list = BLLAgency.FindAll(Convert.ToInt32(Session["SecrecyLevel"]));
                    for (int i = 0; i < list.Count(); i++)
                    {
                        DropDownList.Items.Add(list[i].AgencyName.ToString(), (i + 1).ToString());
                    }
                }
                catch (Exception ex)
                {
                    publicmethod.SaveError(ex, this.Request);
                }
            }
            else if(DropDownStudentType.SelectedItem.Text == "学生类型")
            {
                TriggerBox.Enabled = false;
                DropDownList.Enabled = true;
                DropDownList.Items.Clear();
                DropDownList.EnableEdit = false;
                try
                {
                    List<BasicCode> list = bllBasicCode.FindALLName("学生类型");
                    for (int i = 0; i < list.Count(); i++)
                    {
                        DropDownList.Items.Add(list[i].CategoryContent.ToString(), list[i].CategoryContent.ToString());
                    }
                }
                catch (Exception ex)
                {
                    publicmethod.SaveError(ex, this.Request);
                }
            }
            else
            {
                TriggerBox.Enabled = true;
                DropDownList.Enabled = false; 
                DropDownList.EnableEdit = false;
            }
        }
        public int RowNumber(int dataItemIndex)
        {
            return dataItemIndex + (GridProjectStudent.PageIndex) * GridProjectStudent.PageSize;
        }
        public void StudentType()
        {
            List<BasicCode> list = bllBasicCode.FindALLName("学生类型");
            for (int i = 0; i < list.Count(); i++)
            {
                DropDownList.Items.Add(list[i].CategoryContent.ToString(), list[i].CategoryContent.ToString());
            }
        }
        //全选按钮
        protected void btnSelect_All_Click(object sender, EventArgs e)
        {
            GridProjectStudent.SelectAllRows();
            int[] select = GridProjectStudent.SelectedRowIndexArray;
            int m;
            //取整数（不是四舍五入，全舍）
            int Pages = (int)Math.Floor(Convert.ToDouble(GridProjectStudent.RecordCount / this.GridProjectStudent.PageSize));

            if (GridProjectStudent.PageIndex == Pages)
                m = (GridProjectStudent.RecordCount - this.GridProjectStudent.PageSize * GridProjectStudent.PageIndex);
            else
                m = this.GridProjectStudent.PageSize;
            bool isCheck = false;
            for (int i = 0; i < m; i++)
            {
                if (CBoxSelect.GetCheckedState(i) == false)
                    isCheck = true;
            }
            if (isCheck)
            {
                foreach (int item in select)
                {
                    CBoxSelect.SetCheckedState(item, true);
                }
                btnDelete.Enabled = true;
                btnSelect_All.Text = "取消全选";
            }
            else
            {
                foreach (int item in select)
                {
                    CBoxSelect.SetCheckedState(item, false);
                }
                btnDelete.Enabled = false;
                btnSelect_All.Text = "全选";
            }
        }
    }
}