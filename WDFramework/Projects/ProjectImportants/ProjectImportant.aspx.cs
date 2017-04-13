/**编写人：王会会
 * 时间：2014年8月9号
 * 功能：项目重大节点基本信息的相关操作
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
    public partial class ProjectImportant : System.Web.UI.Page
    {
        BLHelper.BLLProject bllProject = new BLHelper.BLLProject();
        BLHelper.BLLProjectImportantNode bllProjectImport = new BLHelper.BLLProjectImportantNode();
        BLHelper.BLLOperationLog bllOperate = new BLHelper.BLLOperationLog();
        BLCommon.PublicMethod publicmethod = new BLCommon.PublicMethod();
        BLHelper.BLLUser bllUser = new BLHelper.BLLUser();
       private int page;
        protected void Page_Load(object sender, EventArgs e)
        {
            page = ViewState["page"] == null ? 0 : (int)ViewState["page"];
            if (!IsPostBack)
            {               
                //添加数据
                btnAddProject.OnClientClick = WindowProjectImportant.GetShowReference("ADD_ProjectImportant.aspx", "添加项目重大节点信息");               
                BindData();
                btnDelete.Enabled = false;
            }
        }        
        public void BindData()
        {
            try
            {
                ViewState["page"] = 0;
                List<ProjectImportantNode> List = bllProjectImport.FindAll(Convert.ToInt32(Session["SecrecyLevel"]));
                GridProjectAndTime.RecordCount = List.Count();
                var result = List.Skip(GridProjectAndTime.PageIndex * GridProjectAndTime.PageSize).Take(GridProjectAndTime.PageSize).ToList();
                this.GridProjectAndTime.DataSource = result;
                this.GridProjectAndTime.DataBind();
                btnDelete.Enabled = false;
                TriProjectNames.Reset();
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
                ViewState["page"] = 1;
                List<int> Projectlist = bllProject.FindProjectList(TriProjectNames.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                List<ProjectImportantNode> project = new List<ProjectImportantNode>();
                for (int i = 0; i < Projectlist.Count(); i++)
                {
                    List<ProjectImportantNode> List = bllProjectImport.FindTime(Projectlist[i], Convert.ToInt32(Session["SecrecyLevel"]));
                    for (int j = 0; j < List.Count(); j++)
                        project.Add(List[j]);
                }

                GridProjectAndTime.RecordCount = project.Count();
                this.GridProjectAndTime.DataSource = project.Skip(GridProjectAndTime.PageIndex * GridProjectAndTime.PageSize).Take(GridProjectAndTime.PageSize).ToList();
                this.GridProjectAndTime.DataBind();
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }
        //查询按钮
        protected void FindObjectAndTime_Click(object sender, EventArgs e)
        {
            GridProjectAndTime.PageIndex = 0;
            if (!string.IsNullOrEmpty(TriProjectNames.Text.Trim()))
            {
                switch(Convert.ToInt32( ddl_search.SelectedValue))
                {
                    case 0:
                        Alert.ShowInTop("请选择查询条件！");
                        return;
                    case 1:
                        FindBindData();
                        break;
                    case 2:
                        FindByPersonCharge();
                        break;
                    case 3:
                        FindByResearchCharge();
                        break;
                    case 4:
                        FindByProjectCompletion();
                        break;
                    case 5:
                        FindByActualComleption();
                        break;
                }
            }
            else
                Alert.ShowInTop("请填写查询条件！");
        }


        //根据实际完成情况查询项目重要节点信息
        private void FindByActualComleption()
        {
            try
            {
                ViewState["page"] = 5;
                List<ProjectImportantNode> res = bllProjectImport.FindByActualComleption(TriProjectNames.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                GridProjectAndTime.RecordCount = res.Count();
                this.GridProjectAndTime.DataSource = res.Skip(GridProjectAndTime.PageIndex * GridProjectAndTime.PageSize).Take(GridProjectAndTime.PageSize).ToList();
                this.GridProjectAndTime.DataBind();
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }
        //根据负责研究室查询项目重要节点信息
        private void FindByResearchCharge()
        {
            try
            {
                ViewState["page"] = 3;
                List<ProjectImportantNode> res = bllProjectImport.FindByResearchCharge(TriProjectNames.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                GridProjectAndTime.RecordCount = res.Count();
                this.GridProjectAndTime.DataSource = res.Skip(GridProjectAndTime.PageIndex * GridProjectAndTime.PageSize).Take(GridProjectAndTime.PageSize).ToList();
                this.GridProjectAndTime.DataBind();
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }

        //根据负责人查询项目重要节点信息
        private void FindByPersonCharge()
        {
            try
            {
                ViewState["page"] = 2;
                List<ProjectImportantNode> res = bllProjectImport.FindByPersonCharge(TriProjectNames.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                GridProjectAndTime.RecordCount = res.Count();
                this.GridProjectAndTime.DataSource = res.Skip(GridProjectAndTime.PageIndex * GridProjectAndTime.PageSize).Take(GridProjectAndTime.PageSize).ToList();
                this.GridProjectAndTime.DataBind();
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }
        //根据项目完成情况项目重要节点信息
        private void FindByProjectCompletion()
        {
            try
            {
                ViewState["page"] = 4;
                List<ProjectImportantNode> res = bllProjectImport.FindByProjectCompletion(TriProjectNames.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                GridProjectAndTime.RecordCount = res.Count();
                this.GridProjectAndTime.DataSource = res.Skip(GridProjectAndTime.PageIndex * GridProjectAndTime.PageSize).Take(GridProjectAndTime.PageSize).ToList();
                this.GridProjectAndTime.DataBind();
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }
        //删除
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                List<int> selections = publicmethod.GridCount(GridProjectAndTime, CBoxSelect);
                if (Convert.ToInt32(Session["SecrecyLevel"]) == 5)
                {
                    for (int i = 0; i < selections.Count(); i++)
                    {
                        bllProjectImport.Delete(Convert.ToInt32(GridProjectAndTime.DataKeys[selections[i]][0]));
                    }
                    BindData();
                    Alert.ShowInTop("删除数据成功!");
                }
                else
                {
                    for (int i = 0; i < selections.Count(); i++)
                    {
                        bllProjectImport.ChangePass(Convert.ToInt32(GridProjectAndTime.DataKeys[selections[i]][0]), false);
                        //向操作日志表中插入
                        OperationLog operate = new OperationLog();
                        operate.LoginName = bllUser.FindByLoginName(Session["LoginName"].ToString()).UserName;
                        operate.LoginIP = "";
                        operate.OperationType = "删除";
                        operate.OperationContent = "ProjectImportantNode";
                        operate.OperationDataID = Convert.ToInt32(GridProjectAndTime.DataKeys[selections[i]][0]);
                        operate.OperationTime = System.DateTime.Now;
                        operate.Remark = "";
                        bllOperate.Insert(operate);                      
                    }
                    Alert.ShowInTop("您的操作已提交，请等待审核！");
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
                List<int> selections = publicmethod.GridCount(GridProjectAndTime, CBoxSelect);
                if (selections.Count() != 0)
                {
                    if (selections.Count() == 1)
                    {
                        int rowID = Convert.ToInt32(GridProjectAndTime.DataKeys[selections[0]][0]);
                        Session["ProjectImportantID"] = rowID;
                        Alert.Show("你确定要修改该行数据吗!", "确认消息", MessageBoxIcon.Information, WindowUpdate.GetShowReference("Update_ProjectImportant.aspx"), Target.Top);
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
       
        //GridProjectAndTime的行命令
        protected void GridProjectAndTime_RowCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                List<int> selections = publicmethod.GridCount(GridProjectAndTime, CBoxSelect);
                string Person = GridProjectAndTime.Rows[e.RowIndex].Values[2].ToString();
                string strs = bllUser.FindByLoginName(Session["LoginName"].ToString()).UserName;

                if (Person != strs && Convert.ToInt32(Session["SecrecyLevel"]) != 5)
                {
                    string str = "您无对此行操作的权限！此行信息为" + Person + "录入，请与管理员联系!";
                    CBoxSelect.SetCheckedState(e.RowIndex, false);
                    Alert.ShowInTop(str);
                }
                if (selections.Count == 0)
                {
                    //Alert.ShowInTop("请选中需删除的数据！");
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
            TriProjectNames.Text = "";
            TriProjectNames.Enabled = false;
            ddl_search.Reset();
        }
        //备注界面跳转
        protected string GetEditUrl(object ProjectImportantID)
        {
            return Remark.GetShowReference("Remark_Window.aspx?id=" + ProjectImportantID, "备注");
        }
        //节点名称界面跳转
        protected string GetEditUrlW(object ProjectImportantID)
        {
            return MissionName.GetShowReference("MissionName_Window.aspx?id=" + ProjectImportantID, "节点名称");
        }
        //涉密等级名称
        public string SecrecyLevelName(int level)
        {
            try
            {
                string[] SecrecyLevels = new string[] { "四级", "三级", "二级", "一级", "管理员" };
               // string[] SecrecyLevels = new string[] { "公开", "内部", "秘密", "机密", "管理员" };
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
                    List<ProjectImportantNode> List = bllProjectImport.FindAll(Convert.ToInt32(Session["SecrecyLevel"]));
                    this.GridProjectAndTime.DataSource = List;
                    this.GridProjectAndTime.DataBind();
                }
                if (page == 1)
                {
                    List<int> Projectlist = bllProject.FindProjectList(TriProjectNames.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                    List<ProjectImportantNode> project = new List<ProjectImportantNode>();
                    for (int i = 0; i < Projectlist.Count(); i++)
                    {
                        List<ProjectImportantNode> List = bllProjectImport.FindTime(Projectlist[i], Convert.ToInt32(Session["SecrecyLevel"]));
                        for (int j = 0; j < List.Count(); j++)
                            project.Add(List[j]);
                    }
                    this.GridProjectAndTime.DataSource = project;
                    this.GridProjectAndTime.DataBind();
                }
                publicmethod.ExportExcel(3, GridProjectAndTime, 2);
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }
        //分页每页项的个数
        protected void ddlGridPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridProjectAndTime.PageIndex = 0;
            this.GridProjectAndTime.PageSize = Convert.ToInt32(ddlGridPageSize.SelectedValue);
            switch (page)
            {
                case 0:
                    BindData();
                    break;
                case 1:
                    FindBindData();
                    break;
                case 2:
                    FindByPersonCharge();
                    break;
                case 3:
                    FindByResearchCharge();
                    break;
                case 4:
                    FindByProjectCompletion();
                    break;
                case 5:
                    FindByActualComleption();
                    break;

            }
        }
        //分页页数
        protected void GridProjectAndTime_PageIndexChange(object sender, GridPageEventArgs e)
        {
            GridProjectAndTime.PageIndex = e.NewPageIndex;
            switch (page)
            {
                case 0:
                    BindData();
                    break;
                case 1:
                    FindBindData();
                    break;
                case 2:
                    FindByPersonCharge();
                    break;
                case 3:
                    FindByResearchCharge();
                    break;
                case 4:
                       FindByProjectCompletion();
                       break;
                case 5:
                       FindByActualComleption();
                       break;
            }
        }
        public int  RowNumber( int dataItemIndex)
        {
            return dataItemIndex + (GridProjectAndTime.PageIndex) * GridProjectAndTime.PageSize; 
        }

        protected void ddl_search_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_search.SelectedValue != "0")
                TriProjectNames.Enabled = true;
            else
                TriProjectNames.Enabled = false;
        }
    }
}