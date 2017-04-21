/**编写人：王会会
 * 时间：2014年8月8号
 * 功能：项目相关人员基本信息的相关操作
 * 修改履历：
 **/
using BLHelper;
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
    public partial class ProjectStaffDevote : System.Web.UI.Page
    {
        BLHelper.BLLProject bllProject = new BLHelper.BLLProject();
        BLHelper.BLLStaffDevote bllStaffDevote = new BLLStaffDevote();
        BLHelper.BLLOperationLog bllOperate = new BLHelper.BLLOperationLog();
        BLHelper.BLLUser bllUser = new BLLUser();
        BLCommon.PublicMethod publicmethod = new BLCommon.PublicMethod();
        BLHelper.BLLStaffDevote bsd = new BLLStaffDevote();
       private int page;
        protected void Page_Load(object sender, EventArgs e)
        {
            page = ViewState["page"] == null ? 0 : (int)ViewState["page"];
            if (!IsPostBack)
            {
                //添加数据
                btnAddProject.OnClientClick = WindowProjectImportant.GetShowReference("ADD_ProjectStaffDevote.aspx", "添加项目人员投入信息");
                BindData();
                btnDelete.Enabled = false;
                reprot1.OnClientClick = WindowReport.GetShowReference("~/Report/Project.aspx", "按项目统计科研人员信息");
                if (ddlsearch.SelectedText == "全部")
                    TriProjectNames.Enabled = false;
                else
                    TriProjectNames.Enabled = true;
            }
        }
        //查询
        protected void FindDevoteTime_Click(object sender, EventArgs e)
        {
            GridProjectAndPeople.PageIndex = 0;
            if (!string.IsNullOrEmpty(TriProjectNames.Text.Trim()))
            {
                FindBindData();
            }
            else
                Alert.ShowInTop("请选择查询条件！");
        }
        public void BindData()
        {
            try
            {
                ViewState["page"] = 0;
                List<StaffDevote> List = bllStaffDevote.FindAll(Convert.ToInt32(Session["SecrecyLevel"]));
                GridProjectAndPeople.RecordCount = List.Count();
                this.GridProjectAndPeople.DataSource = List.Skip(GridProjectAndPeople.PageIndex * GridProjectAndPeople.PageSize).Take(GridProjectAndPeople.PageSize).ToList();
                this.GridProjectAndPeople.DataBind();
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }
        //查询(模糊查询)
        public void FindBindData()
        {
            try
            {
                if (ddlsearch.SelectedText == "项目名称")
                {
                    ViewState["page"] = 1;
                    List<int> Projectlist = bllProject.FindProjectList(TriProjectNames.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                    List<StaffDevote> staffdevote = new List<StaffDevote>();
                    for (int i = 0; i < Projectlist.Count(); i++)
                    {
                        List<StaffDevote> List = bllStaffDevote.FindPaged(Projectlist[i], Convert.ToInt32(Session["SecrecyLevel"]));
                        for (int j = 0; j < List.Count(); j++)
                            staffdevote.Add(List[j]);
                    }
                    GridProjectAndPeople.RecordCount = staffdevote.Count();
                    var result = staffdevote.Skip(GridProjectAndPeople.PageIndex * GridProjectAndPeople.PageSize).Take(GridProjectAndPeople.PageSize).ToList();
                    this.GridProjectAndPeople.DataSource = result;
                    this.GridProjectAndPeople.DataBind();
                    btnDelete.Enabled = false;
                }
                else if (ddlsearch.SelectedText == "人员姓名")
                {
                    FindByName();
                }
                else
                    return;

            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }

        //根据人员姓名查询
        public void FindByName()
        {
            try
            {
                if (TriProjectNames.Text.Trim() == "")
                {
                    TriProjectNames.Text = "";
                    Alert.ShowInTop("请输入搜索内容！");
                    return;
                }
                ViewState["page"] = 2;
                int userid = bllUser.FindByUserName(TriProjectNames.Text.Trim()).UserInfoID;
                if (userid == 0)
                {
                    Alert.ShowInTop("此人不存在，请检查输入");
                    return;
                }
                List<StaffDevote> staffdevote = bllStaffDevote.FindByName(userid);
                GridProjectAndPeople.RecordCount = staffdevote.Count();
                var result = staffdevote.Skip(GridProjectAndPeople.PageIndex * GridProjectAndPeople.PageSize).Take(GridProjectAndPeople.PageSize).ToList();
                this.GridProjectAndPeople.DataSource = result;
                this.GridProjectAndPeople.DataBind();
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }

        //分页每页项的个数
        protected void ddlGridPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridProjectAndPeople.PageIndex = 0;
            this.GridProjectAndPeople.PageSize = Convert.ToInt32(ddlGridPageSize.SelectedValue);
            switch (page)
            {
                case 0:
                    BindData();
                    break;
                case 1:
                    FindBindData();
                    break;
                case 2:
                    FindByName();
                    break;
            }     
        }
        //分页页数
        protected void GridProjectAndPeople_PageIndexChange(object sender, GridPageEventArgs e)
        {
            GridProjectAndPeople.PageIndex = e.NewPageIndex;
            switch (page)
            {
                case 0:
                    BindData();
                    break;
                case 1:
                    FindBindData();
                    break;
                case 2:
                    FindByName();
                    break;
            }     
        }
        //删除
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                List<int> selections = publicmethod.GridCount(GridProjectAndPeople, CBoxSelect);
                if (Convert.ToInt32(Session["SecrecyLevel"]) == 5)
                {
                    for (int i = 0; i < selections.Count(); i++)
                    {
                        bllStaffDevote.Delete(Convert.ToInt32(GridProjectAndPeople.DataKeys[selections[i]][0]));
                    }
                    BindData();
                    Alert.ShowInTop("删除数据成功!");
                }
                else
                {
                    for (int i = 0; i < selections.Count(); i++)
                    {
                        bllStaffDevote.ChangePass(Convert.ToInt32(GridProjectAndPeople.DataKeys[selections[i]][0]), false);
                        //向操作日志表中插入
                        OperationLog operate = new OperationLog();
                        operate.LoginName = bllUser.FindByLoginName(Session["LoginName"].ToString()).UserName;
                        operate.LoginIP = "";
                        operate.OperationType = "删除";
                        operate.OperationContent = "StaffDevote";
                        operate.OperationDataID = Convert.ToInt32(GridProjectAndPeople.DataKeys[selections[i]][0]);
                        operate.OperationTime = System.DateTime.Now;
                        operate.Remark = "";
                        bllOperate.Insert(operate);                      
                    }
                    Alert.ShowInTop("删除项目重大节点信息已提交待审核！");
                    BindData();
                }
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }
        //更新
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                List<int> selections = publicmethod.GridCount(GridProjectAndPeople, CBoxSelect);
                if (selections.Count() != 0)
                {
                    if (selections.Count() == 1)
                    {
                        int rowID = Convert.ToInt32(GridProjectAndPeople.DataKeys[selections[0]][0]);
                        Session["StaffDevoteID"] = rowID;

                        Alert.Show("你确定要修改该行数据吗!", "确认消息", MessageBoxIcon.Information, WindowUpdate.GetShowReference("Update_ProjectStaffDevote.aspx"), Target.Top);
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
        //GridProjectAndPeople行命令
        protected void GridProjectAndPeople_RowCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                List<int> selections = publicmethod.GridCount(GridProjectAndPeople, CBoxSelect);
                string Person = GridProjectAndPeople.Rows[e.RowIndex].Values[2].ToString();
                string strs = bllUser.FindByLoginName(Session["LoginName"].ToString()).UserName;

                if (Person != strs && Convert.ToInt32(Session["SecrecyLevel"]) != 5)
                {
                    string str = "您无对此行操作的权限！此行信息为" + Person + "录入，请与管理员联系!";
                    CBoxSelect.SetCheckedState(e.RowIndex, false);
                    Alert.ShowInTop(str);
                }
                if (selections.Count == 0)
                {
                    btnDelete.Enabled = false;
                    return;
                }
                if (selections.Count != 0)
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
        //根据ProjectId查找Name
        public string ProjectName(int ProjectID)
        {
            try
            {
                if (ProjectID != 0)
                {
                    return bllProject.SelectProjectName(ProjectID);
                }
                else
                    return "";
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
                return "";
            }
        }
        //刷新
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            BindData();
            btnDelete.Enabled = false;
            TriProjectNames.Reset();
            ddlsearch.Reset();

        }
        //涉密等级名称
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
                if (page == 0)
                {
                    List<StaffDevote> List = bllStaffDevote.FindAll(Convert.ToInt32(Session["SecrecyLevel"]));
                    this.GridProjectAndPeople.DataSource = List;
                    this.GridProjectAndPeople.DataBind();
                }
                if (page == 1)
                {
                    List<int> Projectlist = bllProject.FindProjectList(TriProjectNames.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                    List<StaffDevote> staffdevote = new List<StaffDevote>();
                    for (int i = 0; i < Projectlist.Count(); i++)
                    {
                        List<StaffDevote> List = bllStaffDevote.FindPaged(Projectlist[i], Convert.ToInt32(Session["SecrecyLevel"]));
                        for (int j = 0; j < List.Count(); j++)
                            staffdevote.Add(List[j]);
                    }
                    this.GridProjectAndPeople.DataSource = staffdevote;
                    this.GridProjectAndPeople.DataBind();
                }
                publicmethod.ExportExcel(3, GridProjectAndPeople, 0);
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }
        public int RowNumber(int dataItemIndex)
        {
            return dataItemIndex + (GridProjectAndPeople.PageIndex) * GridProjectAndPeople.PageSize;
        }

        protected void ddlsearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlsearch.SelectedValue != "0")
                TriProjectNames.Enabled = true;
            else
                TriProjectNames.Enabled = false;
        }


        //全选按钮
        protected void btnSelect_All_Click(object sender, EventArgs e)
        {
            GridProjectAndPeople.SelectAllRows();
            int[] select = GridProjectAndPeople.SelectedRowIndexArray;
            int m;
            //取整数（不是四舍五入，全舍）
            int Pages = (int)Math.Floor(Convert.ToDouble(GridProjectAndPeople.RecordCount / this.GridProjectAndPeople.PageSize));

            if (GridProjectAndPeople.PageIndex == Pages)
                m = (GridProjectAndPeople.RecordCount - this.GridProjectAndPeople.PageSize * GridProjectAndPeople.PageIndex);
            else
                m = this.GridProjectAndPeople.PageSize;
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