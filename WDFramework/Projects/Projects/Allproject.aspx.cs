/**编写人：王会会
 * 时间：2014年8月3号
 * 功能：项目全部信息的相关操作
 * 修改履历： 20150824 郝瑞 添加项目成员字段，以及针对其的相关功能，变量page改为非静态
 *           20150923 郝瑞 修复按项目负责人查询的bug
 *           2015/12/5 高琪 增加RowNumber_two函数
 *           4、修改人：吕博杨
 *              修改时间：2015年12月7日
 *              修改内容：为项目相关文档功能提供代码支持
 **/
using Common.Entities;
using FineUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WDFramework.Project
{
    public partial class Allproject : System.Web.UI.Page
    {
        BLHelper.BLLProjectFile projectFile = new BLHelper.BLLProjectFile();
        BLHelper.BLLAgency bllAgency = new BLHelper.BLLAgency();
        BLHelper.BLLProject bllProject = new BLHelper.BLLProject();
        BLHelper.BLLUser bllUser = new BLHelper.BLLUser();
        BLHelper.BLLOperationLog bllOperate = new BLHelper.BLLOperationLog();
        BLHelper.BLLProjectImportantNode bpi = new BLHelper.BLLProjectImportantNode();
        BLHelper.BLLAttachment blat = new BLHelper.BLLAttachment();
        BLCommon.PublicMethod publicmethod = new BLCommon.PublicMethod();
        BLHelper.BLLBasicCode bllBasicCode = new BLHelper.BLLBasicCode();
        BLHelper.BLLStaffDevote bllDevote = new BLHelper.BLLStaffDevote();
        BLHelper.BLLProjectImportantNode bllImportant = new BLHelper.BLLProjectImportantNode();
        BLHelper.BLLFundInformation bllFund = new BLHelper.BLLFundInformation();
        private int page;
        protected void Page_Load(object sender, EventArgs e)
        {
            page = ViewState["page"] == null ? 0 : (int)ViewState["page"];
            if (!IsPostBack)
            {
                //添加数据
                btnAddProject.OnClientClick = WindowProject.GetShowReference("ADD_Projects.aspx");
                reprot1.OnClientClick = WindowReport.GetShowReference("~/Report/State.aspx", "按状态统计项目信息");
                reprot2.OnClientClick = WindowReport.GetShowReference("~/Report/Accept.aspx", "分承担部门按负责人统计项目信息");
                reprot3.OnClientClick = WindowReport.GetShowReference("~/Report/ProjectType.aspx ", "分项目类型按年份统计项目信息");
                reprot4.OnClientClick = WindowReport.GetShowReference("~/Report/Statistic1.aspx ", "分项目来源按年份统计项目信息");
                reprot5.OnClientClick = WindowReport.GetShowReference("~/Report/Year.aspx ", "分年份按承担部门统计项目信息");
                reprot6.OnClientClick = WindowReport.GetShowReference("~/Report/ZHType.aspx ", "分横向/纵向按年份统计项目信息");
                reprot7.OnClientClick = WindowReport.GetShowReference("~/Report/R_Agency_Projects.aspx ", "分部门统计项目情况");
                reprot8.OnClientClick = WindowReport.GetShowReference("~/Report/Year_souse.aspx ", "分年份按项目来源管理费");
                reprot9.OnClientClick = WindowReport.GetShowReference("~/Report/Year_Type.aspx ", "分年份按项目类型统计管理费");
                BindData();
                btnDelete.Enabled = false;
                //lby ↓
                btnDelete_two.Enabled = false;
                btnUpdate_two.Enabled = false;

                DropDownListYearandLevel.Enabled = false;
                SourceUnit.Enabled = false;
                AN.Enabled = false;
                ProjectNature.Enabled = false;
                ProjectNatureBind();
                btnAdd_two.OnClientClick = WindowAddDocument.GetShowReference("Add_Document.aspx", "新增项目文档");
                btnUpdate_two.OnClientClick = WindowUpdateDocument.GetShowReference("Update_Document.aspx", "编辑项目文档");
            }
        }
        //绑定数据
        public void BindData()
        {
            try
            {
                ViewState["page"] = 0;
                List<Common.Entities.Project> ProjectList = bllProject.FindPaged(Convert.ToInt32(Session["SecrecyLevel"]));
                GridProjectAll.RecordCount = ProjectList.Count;
                this.GridProjectAll.DataSource = ProjectList.Skip(GridProjectAll.PageIndex * GridProjectAll.PageSize).Take(GridProjectAll.PageSize).ToList();
                this.GridProjectAll.DataBind();
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }

        //lby ↓ 绑定文件列表数据
        public void FileListDataBind()
        {
            try
            {
                List<ProjectFile> projectFiles = new BLHelper.BLLProjectFile().FindByProjectId(Convert.ToInt32(Session["ProjectID"]), Convert.ToInt32(Session["SecrecyLevel"]));
                GridProjectAllTwo.RecordCount = projectFiles.Count;
                GridProjectAllTwo.DataSource = projectFiles.Skip(GridProjectAllTwo.PageIndex * GridProjectAllTwo.PageSize).Take(GridProjectAllTwo.PageSize).ToList();
                GridProjectAllTwo.DataBind();
                btnDelete_two.Enabled = false;
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }

        //按项目名称查找(模糊查询)
        public void FindByName()
        {
            try
            {
                ViewState["page"] = 1;
                List<int> Projectlist = bllProject.FindProjectList(SourceUnit.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                List<Common.Entities.Project> project = new List<Common.Entities.Project>();
                for (int i = 0; i < Projectlist.Count(); i++)
                {
                    List<Common.Entities.Project> list = bllProject.FindProject(Projectlist[i], Convert.ToInt32(Session["SecrecyLevel"]));
                    for (int j = 0; j < list.Count(); j++)
                        project.Add(list[j]);
                }
                GridProjectAll.RecordCount = project.Count();
                this.GridProjectAll.DataSource = project.Skip(GridProjectAll.PageIndex * GridProjectAll.PageSize).Take(GridProjectAll.PageSize).ToList();
                this.GridProjectAll.DataBind();
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }
        //分年份按项目来源查找
        public void FindYP()
        {
            try
            {
                ViewState["page"] = 2;
                List<Common.Entities.Project> ProjectList = bllProject.FindYP(Convert.ToInt32(DropDownListYearandLevel.SelectedItem.Text), ProjectNature.SelectedText, Convert.ToInt32(Session["SecrecyLevel"]));
                GridProjectAll.RecordCount = ProjectList.Count();
                this.GridProjectAll.DataSource = ProjectList.Skip(GridProjectAll.PageIndex * GridProjectAll.PageSize).Take(GridProjectAll.PageSize).ToList();
                this.GridProjectAll.DataBind();
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }
        //分年份按项目级别查找
        public void FindTP()
        {
            try
            {
                ViewState["page"] = 3;
                List<Common.Entities.Project> ProjectList = bllProject.FindTP(Convert.ToInt32(DropDownListYearandLevel.SelectedItem.Text), ProjectNature.SelectedText, Convert.ToInt32(Session["SecrecyLevel"]));
                GridProjectAll.RecordCount = ProjectList.Count();
                this.GridProjectAll.DataSource = ProjectList.Skip(GridProjectAll.PageIndex * GridProjectAll.PageSize).Take(GridProjectAll.PageSize).ToList();
                this.GridProjectAll.DataBind();
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }

        }
        //分年份按承担部门查找
        public void FindACU()
        {
            try
            {
                ViewState["page"] = 4;
                List<Common.Entities.Project> ProjectList = bllProject.FindACU(Convert.ToInt32(DropDownListYearandLevel.SelectedItem.Text), ProjectNature.SelectedText, Convert.ToInt32(Session["SecrecyLevel"]));
                GridProjectAll.RecordCount = ProjectList.Count();
                this.GridProjectAll.DataSource = ProjectList.Skip(GridProjectAll.PageIndex * GridProjectAll.PageSize).Take(GridProjectAll.PageSize).ToList();
                this.GridProjectAll.DataBind();
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }
        //按保密等级查找
        public void FindTA()
        {
            try
            {
                ViewState["page"] = 13;
                List<Common.Entities.Project> ProjectList = bllProject.FindTA(exchangesecrecylevel(DropDownListYearandLevel.SelectedValue), ProjectNature.SelectedText, Convert.ToInt32(Session["SecrecyLevel"]));
                GridProjectAll.RecordCount = ProjectList.Count();
                this.GridProjectAll.DataSource = ProjectList.Skip(GridProjectAll.PageIndex * GridProjectAll.PageSize).Take(GridProjectAll.PageSize).ToList();
                this.GridProjectAll.DataBind();
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }
        //将保密等级转换成int
        public int exchangesecrecylevel(string secrecylevel)
        {
            if (secrecylevel == "四级")
                return 1;
            else if (secrecylevel == "三级")
                return 2;
            else if (secrecylevel == "二级")
                return 3;
            else if (secrecylevel == "一级")
                return 4;
            else if (secrecylevel == "管理员")
                return 5;
            else
                return 0;
        }
        //分项目来源按年份查找（模糊查询）
        public void FindSU()
        {
            try
            {
                ViewState["page"] = 5;
                List<Common.Entities.Project> ProjectList = bllProject.FindSU(SourceUnit.Text.Trim(), ProjectNature.SelectedText, Convert.ToInt32(Session["SecrecyLevel"]));
                GridProjectAll.RecordCount = ProjectList.Count();
                this.GridProjectAll.DataSource = ProjectList.Skip(GridProjectAll.PageIndex * GridProjectAll.PageSize).Take(GridProjectAll.PageSize).ToList();
                this.GridProjectAll.DataBind();
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }
        //分项目级别按年份查找
        public void FindPT()
        {
            try
            {
                ViewState["page"] = 6;
                List<Common.Entities.Project> ProjectList = bllProject.FindPT(DropDownListYearandLevel.SelectedItem.Text, ProjectNature.SelectedText, Convert.ToInt32(Session["SecrecyLevel"]));
                GridProjectAll.RecordCount = ProjectList.Count();
                this.GridProjectAll.DataSource = ProjectList.Skip(GridProjectAll.PageIndex * GridProjectAll.PageSize).Take(GridProjectAll.PageSize).ToList();
                this.GridProjectAll.DataBind();
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }
        //分承担部门按年份查找（模糊查询）
        public void FindTime()
        {
            try
            {
                ViewState["page"] = 7;
                List<Common.Entities.Project> ProjectList = bllProject.FindTime(SourceUnit.Text.Trim(), ProjectNature.SelectedText, Convert.ToInt32(Session["SecrecyLevel"]));
                GridProjectAll.RecordCount = ProjectList.Count();
                this.GridProjectAll.DataSource = ProjectList.Skip(GridProjectAll.PageIndex * GridProjectAll.PageSize).Take(GridProjectAll.PageSize).ToList();
                this.GridProjectAll.DataBind();
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }
        //分承担部门按负责人查找（模糊查询）
        public void FindPH()
        {
            try
            {
                ViewState["page"] = 8;
                List<Common.Entities.Project> ProjectList = bllProject.FindPH(SourceUnit.Text.Trim(), ProjectNature.SelectedText, Convert.ToInt32(Session["SecrecyLevel"]));
                GridProjectAll.RecordCount = ProjectList.Count();
                this.GridProjectAll.DataSource = ProjectList.Skip(GridProjectAll.PageIndex * GridProjectAll.PageSize).Take(GridProjectAll.PageSize).ToList();
                this.GridProjectAll.DataBind();
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }
        //根据项目状态查找
        public void FindCS()
        {
            try
            {
                ViewState["page"] = 9;
                List<Common.Entities.Project> ProjectList = bllProject.FindCS(DropDownListYearandLevel.SelectedText.ToString(), Convert.ToInt32(Session["SecrecyLevel"]));
                GridProjectAll.RecordCount = ProjectList.Count;
                this.GridProjectAll.DataSource = ProjectList.Skip(GridProjectAll.PageIndex * GridProjectAll.PageSize).Take(GridProjectAll.PageSize).ToList();
                this.GridProjectAll.DataBind();
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }

        //按照项目负责人查询
        public void FindByProjectHeads()
        {
            try
            {
                if (SourceUnit.Text.Trim() == "")
                {
                    Alert.ShowInTop("请输入搜索条件");
                    return;
                }
                ViewState["page"] = 10;
                List<Common.Entities.Project> ProjectList = bllProject.FindPM(SourceUnit.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                GridProjectAll.RecordCount = ProjectList.Count;
                this.GridProjectAll.DataSource = ProjectList.Skip(GridProjectAll.PageIndex * GridProjectAll.PageSize).Take(GridProjectAll.PageSize).ToList();
                this.GridProjectAll.DataBind();
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }

        //按照项目性质查询
        public void FindByProjectNature()
        {
            try
            {
                if (SourceUnit.Text.Trim() == "")
                {
                    Alert.ShowInTop("请输入搜索条件");
                    return;
                }
                ViewState["page"] = 11;
                List<Common.Entities.Project> ProjectList = bllProject.FindByProjectNature(SourceUnit.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                GridProjectAll.RecordCount = ProjectList.Count;
                this.GridProjectAll.DataSource = ProjectList.Skip(GridProjectAll.PageIndex * GridProjectAll.PageSize).Take(GridProjectAll.PageSize).ToList();
                this.GridProjectAll.DataBind();
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }


        public void FindByProjectMember()
        {
            try
            {
                if (SourceUnit.Text.Trim() == "")
                {
                    Alert.ShowInTop("请输入搜索条件");
                    return;
                }
                ViewState["page"] = 12;
                List<Common.Entities.Project> ProjectList = bllProject.FindByProjectMember(SourceUnit.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                GridProjectAll.RecordCount = ProjectList.Count;
                this.GridProjectAll.DataSource = ProjectList.Skip(GridProjectAll.PageIndex * GridProjectAll.PageSize).Take(GridProjectAll.PageSize).ToList();
                this.GridProjectAll.DataBind();
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }

        //查询按钮
        protected void FindObjectAll_Click(object sender, EventArgs e)
        {
            GridProjectAll.PageIndex = 0;
            try
            {
                switch (FEN.SelectedItem.Text)
                {
                    case "全部":
                        BindData();
                        break;
                    case "项目名称":
                        if (!string.IsNullOrEmpty(SourceUnit.Text.Trim()))
                            if (SourceUnit.Text.Trim().Length < 20)
                            {
                                FindByName();
                            }
                            else
                                Alert.ShowInTop("最多输入20个字符");
                        else
                            Alert.ShowInTop("请添加查询条件!");
                        break;
                    case "项目状态":
                        if (!string.IsNullOrEmpty(DropDownListYearandLevel.SelectedItem.Text))
                            FindCS();
                        else
                            Alert.ShowInTop("请选择查询条件!");
                        break;
                    case "年份":
                        if (!string.IsNullOrEmpty(DropDownListYearandLevel.SelectedItem.Text))
                        {
                            switch (AN.SelectedItem.Text)
                            {
                                case "项目来源":
                                    FindYP();
                                    break;
                                case "项目级别":
                                    FindTP();
                                    break;
                                case "承担部门":
                                    FindACU();
                                    break;
                            }
                        }
                        else
                            Alert.ShowInTop("请添加查询条件!");
                        break;
                    case "项目来源":
                        if (!string.IsNullOrEmpty(SourceUnit.Text.Trim()))
                            if (SourceUnit.Text.Trim().Length < 20)
                            {
                                FindSU();
                            }
                            else
                                Alert.ShowInTop("最多输入20个字符");
                        else
                            Alert.ShowInTop("请添加查询条件!");
                        break;
                    case "项目级别":
                        if (!string.IsNullOrEmpty(DropDownListYearandLevel.SelectedItem.Text))
                            FindPT();
                        else
                            Alert.ShowInTop("请选择查询条件!");
                        break;
                    case "承担部门":
                        if (!string.IsNullOrEmpty(SourceUnit.Text.Trim()))
                        {
                            if (SourceUnit.Text.Trim().Length < 20)
                            {
                                switch (AN.SelectedItem.Text)
                                {
                                    case ("年份"):
                                        FindTime();

                                        break;
                                    case ("负责人"):
                                        FindPH();
                                        break;
                                }
                            }
                            else
                                Alert.ShowInTop("最多输入20个字符");
                        }
                        else
                            Alert.ShowInTop("请添加查询条件!");
                        break;
                    case "项目负责人":
                        FindByProjectHeads();
                        break;

                    case "项目性质":
                        FindByProjectNature();
                        break;
                    case "项目成员":
                        FindByProjectMember();
                        break;
                    case "保密等级":
                        FindTA();
                        break;
                }
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }
        //删除的Button事件
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                List<int> selections = publicmethod.GridCount(GridProjectAll, CBoxSelect);
                if (Convert.ToInt32(Session["SecrecyLevel"]) == 5)
                {
                    for (int i = 0; i < selections.Count(); i++)
                    {
                        //删除项目相关人员投入表的信息
                        List<int> staffslist = bllDevote.FindStaffDevoteIDList(Convert.ToInt32(GridProjectAll.DataKeys[selections[i]][0]));
                        for (int j = 0; j < staffslist.Count(); j++)
                        {
                            bllDevote.Delete(staffslist[j]);
                        }
                        //删除项目相关重大节点表的信息
                        List<int> Importantlist = bllImportant.FindImprotIDList(Convert.ToInt32(GridProjectAll.DataKeys[selections[i]][0]));
                        for (int n = 0; n < Importantlist.Count(); n++)
                        {
                            bllImportant.Delete(Importantlist[n]);
                        }
                        //删除项目相关经费基本表的信息
                        List<int> Fundlist = bllFund.FindFundIDlist(Convert.ToInt32(GridProjectAll.DataKeys[selections[i]][0]));
                        for (int m = 0; m < Fundlist.Count(); m++)
                        {
                            bllFund.Delete(Fundlist[m]);
                        }
                        //删除合同
                        BLHelper.BLLPact bllPact = new BLHelper.BLLPact();
                        List<int> Pactlist = bllPact.FindPactIDList(Convert.ToInt32(GridProjectAll.DataKeys[selections[i]][0]));
                        for (int m = 0; m < Pactlist.Count(); m++)
                        {
                            bllPact.Delete(Pactlist[m]);
                        }
                        //lby ↓ 删除相关文档
                        BLHelper.BLLProjectFile bllProjectFile = new BLHelper.BLLProjectFile();
                        List<int> fileList = bllProjectFile.FindProjectFileID(Convert.ToInt32(GridProjectAll.DataKeys[selections[i]][0]), 5);
                        if (fileList != null)
                        {
                            int attachid;
                            foreach (int id in fileList)
                            {
                                attachid = bllProjectFile.Delete(id);
                                publicmethod.DeleteFile(attachid, blat.FindPath(attachid));
                            }
                        }

                        //删除项目
                        bllProject.Delete(Convert.ToInt32(GridProjectAll.DataKeys[selections[i]][0]));
                        BindData();
                        Alert.ShowInTop("删除数据成功!");
                        btnSelect_All.Text = "全选";
                    }
                }
                else
                {
                    for (int i = 0; i < selections.Count(); i++)
                    {
                        bllProject.ChangePass(Convert.ToInt32(GridProjectAll.DataKeys[selections[i]][0]), false);
                        //向操作日志表中插入
                        OperationLog operate = new OperationLog();
                        operate.LoginName = bllUser.FindByLoginName(Session["LoginName"].ToString()).UserName;
                        operate.LoginIP = "";
                        operate.OperationType = "删除";
                        operate.OperationContent = "Project";
                        operate.OperationDataID = Convert.ToInt32(GridProjectAll.DataKeys[selections[i]][0]);
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

        //lby ↓ 删除相关文档事件
        protected void btnDeleteTwo_Click(object sender, EventArgs e)
        {
            try
            {
                List<int> selections = publicmethod.GridCount(GridProjectAllTwo, CBSelect_Two);
                if (Convert.ToInt32(Session["SecrecyLevel"]) == 5)
                {
                    int attachid;
                    for (int i = 0; i < selections.Count(); i++)
                    {
                        attachid = projectFile.Delete(Convert.ToInt32(GridProjectAllTwo.DataKeys[selections[i]][0]));
                        publicmethod.DeleteFile(attachid, blat.FindPath(attachid));
                    }
                    Alert.ShowInTop("删除文件成功!");
                }
                else
                {
                    for (int i = 0; i < selections.Count(); i++)
                    {
                        projectFile.ChangePass(Convert.ToInt32(GridProjectAllTwo.DataKeys[selections[i]][0]), false);
                        OperationLog operate = new OperationLog();
                        operate.LoginName = bllUser.FindByLoginName(Session["LoginName"].ToString()).UserName;
                        operate.LoginIP = "";
                        operate.OperationType = "删除";
                        operate.OperationContent = "ProjectFile";
                        operate.OperationDataID = Convert.ToInt32(GridProjectAllTwo.DataKeys[selections[i]][0]);
                        operate.OperationTime = System.DateTime.Now;
                        operate.Remark = "";
                        bllOperate.Insert(operate);
                    }
                    Alert.ShowInTop("您的操作已提交，请等待审核！");
                }
                FileListDataBind();
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }

        //GridProjectAll行命令
        protected void GridProjectAll_RowCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                List<int> selections = publicmethod.GridCount(GridProjectAll, CBoxSelect);
                string Person = GridProjectAll.Rows[e.RowIndex].Values[2].ToString();
                string strs = bllUser.FindByLoginName(Session["LoginName"].ToString()).UserName;

                if (Person != strs && Convert.ToInt32(Session["SecrecyLevel"]) != 5)
                {
                    string str = "您无对此行操作的权限！此行信息为" + Person + "录入，请与管理员联系!";
                    CBoxSelect.SetCheckedState(e.RowIndex, false);
                    Alert.ShowInTop(str);
                }

                if (e.CommandName == "ActionDownBenefit")
                {
                    int benefitID = bllProject.FindBenefit(Convert.ToInt32(GridProjectAll.DataKeys[e.RowIndex][0]));
                    string path = blat.FindPath(benefitID);
                    publicmethod.DownloadFile(path);
                }
                if (e.CommandName == "ActionDownBudget")
                {
                    int budgetID = bllProject.FindBudget(Convert.ToInt32(GridProjectAll.DataKeys[e.RowIndex][0]));
                    string path = blat.FindPath(budgetID);
                    publicmethod.DownloadFile(path);
                }
                //添加选中行在下分页显示
                //if (e.CommandName == "CBoxSelect")
                //{
                //    projectFile.FindByProjectFileId(Convert.ToInt32(GridProjectAll.DataKeys[e.RowIndex][0]));

                //}
                if (selections.Count == 0)
                {
                    btnDelete.Enabled = false;
                    //lby ↓
                    btnUpdate.Enabled = false;
                    btnAdd_two.Enabled = false;
                    Session["ProjectID"] = 0;
                }
                else
                {
                    btnDelete.Enabled = true;
                    //lby ↓
                    btnUpdate.Enabled = true;
                    btnAdd_two.Enabled = true;
                    if (selections.Count == 1)
                        Session["ProjectID"] = Convert.ToInt32(GridProjectAll.DataKeys[e.RowIndex][0]);
                    else
                        Session["ProjectID"] = 0;
                    List<Common.Entities.ProjectFile> projectfile = projectFile.FindByProjectId(Convert.ToInt32(GridProjectAll.DataKeys[e.RowIndex][0]), Convert.ToInt32(Session["SecrecyLevel"]));
                    GridProjectAllTwo.RecordCount = projectfile.Count;
                    this.GridProjectAllTwo.DataSource = projectfile.Skip(GridProjectAllTwo.PageIndex * GridProjectAllTwo.PageSize).Take(GridProjectAllTwo.PageSize).ToList();
                    this.GridProjectAllTwo.DataBind();
                }
                //lby ↓
                FileListDataBind();
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }
        //添加GridProjectAllTwo行命令
        protected void GridProjectAll_Two_RowCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                List<int> selections = publicmethod.GridCount(GridProjectAllTwo, CBSelect_Two);
                string Person = GridProjectAll.Rows[e.RowIndex].Values[2].ToString();
                string strs = bllUser.FindByLoginName(Session["LoginName"].ToString()).UserName;

                if (Person != strs && Convert.ToInt32(Session["SecrecyLevel"]) != 5)
                {
                    string str = "您无对此行操作的权限！此行信息为" + Person + "录入，请与管理员联系!";
                    CBSelect_Two.SetCheckedState(e.RowIndex, false);
                    Alert.ShowInTop(str);
                }
                if (selections.Count == 0)
                {
                    //lby ↓
                    btnDelete_two.Enabled = false;
                    btnUpdate_two.Enabled = false;
                    Session["ProjectFileID"] = 0;
                    return;
                }
                if (selections.Count != 0)
                {
                    //lby ↓
                    btnDelete_two.Enabled = true;
                    if (selections.Count == 1)
                        btnUpdate_two.Enabled = true;
                    else
                        btnUpdate_two.Enabled = false;
                    Session["ProjectFileID"] = Convert.ToInt32(GridProjectAllTwo.DataKeys[e.RowIndex][0]);
                    return;
                }
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }
        //刷新按钮
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            BindData();
            FEN.SelectedValue = "全部";
            DropDownListYearandLevel.Items.Clear();
            SourceUnit.Text = "";
            DropDownListYearandLevel.Enabled = false;
            SourceUnit.Enabled = false;
            AN.Enabled = false;
            ProjectNature.Enabled = false;
        }
        //修改按钮
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                List<int> selections = publicmethod.GridCount(GridProjectAll, CBoxSelect);
                if (selections.Count() != 0)
                {
                    if (selections.Count() == 1)
                    {
                        int rowID = Convert.ToInt32(GridProjectAll.DataKeys[selections[0]][0]);
                        Session["ProjectID"] = rowID;
                        Alert.Show("你确定要修改该行数据吗!", "确认消息", MessageBoxIcon.Information, WindowUpdate.GetShowReference("Update_Project.aspx"), Target.Top);
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
        //根据机构ID找机构名称
        public string AgencyName(int AgencyID)
        {
            try
            {
                if (AgencyID != 0)
                    return bllAgency.FindAgenName(AgencyID);
                else
                    return "";
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
                return "";
            }
        }
        //备注界面跳转
        protected string GetEditUrl(object ProjectID)
        {
            return Remark.GetShowReference("Remark_Window.aspx?id=" + ProjectID, "备注");
        }
        //项目成员界面跳转
        protected string GetEditUrlProjectMember(object ProjectID)
        {
            return ProjectMember.GetShowReference("ProjectMember.aspx?id=" + ProjectID, "项目成员");
        }
        //经济效益附件下载界面跳转
        protected string GetEditUrlBenefit(object ProjectID)
        {
            return Benefit.GetShowReference("OperateBenefit.aspx?id=" + ProjectID, "操作");
        }
        //经费预算附件下载界面跳转
        protected string GetEditUrlBudget(object ProjectID)
        {
            return Budget.GetShowReference("OperateBudget.aspx?id=" + ProjectID, "操作");
        }
        //下载界面跳转
        protected string GetEditUrlDownload(object ProjectFileID)
        {
            return WindowDownloadFile.GetShowReference("DownloadFile.aspx?id=" + ProjectFileID, "操作");
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
        //项目性质
        public void ProjectNatureBind()
        {
            try
            {
                List<BasicCode> lists = bllBasicCode.FindALLName("项目性质");
                ProjectNature.Items.Add("全部", "全部");
                for (int i = 0; i < lists.Count(); i++)
                {
                    ProjectNature.Items.Add(lists[i].CategoryContent.ToString(), lists[i].CategoryContent.ToString());
                }
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
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
                        List<Common.Entities.Project> ProjectList = bllProject.FindPaged(Convert.ToInt32(Session["SecrecyLevel"]));
                        this.GridProjectAll.DataSource = ProjectList;
                        this.GridProjectAll.DataBind();
                        break;
                    case 1:
                        List<int> Projectlist = bllProject.FindProjectList(SourceUnit.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                        List<Common.Entities.Project> project = new List<Common.Entities.Project>();
                        for (int i = 0; i < Projectlist.Count(); i++)
                        {
                            List<Common.Entities.Project> list = bllProject.FindProject(Projectlist[i], Convert.ToInt32(Session["SecrecyLevel"]));
                            for (int j = 0; j < list.Count(); j++)
                                project.Add(list[j]);
                        }
                        this.GridProjectAll.DataSource = project;
                        this.GridProjectAll.DataBind();
                        break;
                    case 2:
                        List<Common.Entities.Project> findYP = bllProject.FindYP(Convert.ToInt32(DropDownListYearandLevel.SelectedItem.Text), ProjectNature.SelectedText, Convert.ToInt32(Session["SecrecyLevel"]));
                        this.GridProjectAll.DataSource = findYP;
                        this.GridProjectAll.DataBind();

                        break;
                    case 3:
                        List<Common.Entities.Project> findTP = bllProject.FindTP(Convert.ToInt32(DropDownListYearandLevel.SelectedItem.Text), ProjectNature.SelectedText, Convert.ToInt32(Session["SecrecyLevel"]));
                        this.GridProjectAll.DataSource = findTP;
                        this.GridProjectAll.DataBind();

                        break;
                    case 4:
                        List<Common.Entities.Project> findACU = bllProject.FindACU(Convert.ToInt32(DropDownListYearandLevel.SelectedItem.Text), ProjectNature.SelectedText, Convert.ToInt32(Session["SecrecyLevel"]));
                        this.GridProjectAll.DataSource = findACU;
                        this.GridProjectAll.DataBind();
                        break;
                    case 5:
                        this.GridProjectAll.DataSource = bllProject.FindSU(SourceUnit.Text.Trim(), ProjectNature.SelectedText, Convert.ToInt32(Session["SecrecyLevel"]));
                        this.GridProjectAll.DataBind();
                        break;
                    case 6:
                        this.GridProjectAll.DataSource = bllProject.FindPT(DropDownListYearandLevel.SelectedItem.Text, ProjectNature.SelectedText, Convert.ToInt32(Session["SecrecyLevel"]));
                        this.GridProjectAll.DataBind();
                        break;
                    case 7:
                        this.GridProjectAll.DataSource = bllProject.FindTime(SourceUnit.Text.Trim(), ProjectNature.SelectedText, Convert.ToInt32(Session["SecrecyLevel"]));
                        this.GridProjectAll.DataBind();
                        break;
                    case 8:
                        this.GridProjectAll.DataSource = bllProject.FindPH(SourceUnit.Text.Trim(), ProjectNature.SelectedText, Convert.ToInt32(Session["SecrecyLevel"]));
                        this.GridProjectAll.DataBind();
                        break;
                    case 9:
                        this.GridProjectAll.DataSource = bllProject.FindCS(DropDownListYearandLevel.SelectedText.ToString(), Convert.ToInt32(Session["SecrecyLevel"]));
                        this.GridProjectAll.DataBind();
                        break;
                }
                publicmethod.ExportExcel(3, GridProjectAll, 3);
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }
        //分页每页项的个数
        protected void ddlGridPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridProjectAll.PageIndex = 0;
            this.GridProjectAll.PageSize = Convert.ToInt32(ddlGridPageSize.SelectedValue);
            switch (page)
            {
                case 0:
                    BindData();
                    break;
                case 1:
                    FindByName();
                    break;
                case 2:
                    FindYP();
                    break;
                case 3:
                    FindTP();
                    break;
                case 4:
                    FindACU();
                    break;
                case 5:
                    FindSU();
                    break;
                case 6:
                    FindPT();
                    break;
                case 7:
                    FindTime();
                    break;
                case 8:
                    FindPH();
                    break;
                case 9:
                    FindCS();
                    break;
                case 10:
                    FindByProjectHeads();
                    break;
                case 11:
                    FindByProjectNature();
                    break;
                case 12:
                    FindByProjectMember();
                    break;
                case 13:
                    FindTA();
                    break;
            }
        }
        //分页页数
        protected void GridProjectAll_PageIndexChange(object sender, GridPageEventArgs e)
        {
            GridProjectAll.PageIndex = e.NewPageIndex;
            switch (page)
            {
                case 0:
                    BindData();
                    break;
                case 1:
                    FindByName();
                    break;
                case 2:
                    FindYP();
                    break;
                case 3:
                    FindTP();
                    break;
                case 4:
                    FindACU();
                    break;
                case 5:
                    FindSU();
                    break;
                case 6:
                    FindPT();
                    break;
                case 7:
                    FindTime();
                    break;
                case 8:
                    FindPH();
                    break;
                case 9:
                    FindCS();
                    break;
                case 10:
                    FindByProjectHeads();
                    break;
                case 11:
                    FindByProjectNature();
                    break;
                case 12:
                    FindByProjectMember();
                    break;
                case 13:
                    FindTA();
                    break;
            }
        }
        //搜索框变化
        protected void FEN_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (FEN.SelectedItem.Text)
            {
                case "全部":
                    DropDownListYearandLevel.Items.Clear();
                    SourceUnit.Text = "";
                    DropDownListYearandLevel.Enabled = false;
                    SourceUnit.Enabled = false;
                    AN.Enabled = false;
                    ProjectNature.Enabled = false;
                    break;
                case "项目名称":
                    DropDownListYearandLevel.Items.Clear();
                    SourceUnit.Text = "";
                    DropDownListYearandLevel.Enabled = false;
                    SourceUnit.Enabled = true;
                    AN.Enabled = false;
                    ProjectNature.Enabled = true;
                    break;
                case "项目状态":
                    DropDownListYearandLevel.Items.Clear();
                    SourceUnit.Text = "";
                    DropDownListYearandLevel.Enabled = false;
                    DropDownListYearandLevel.Enabled = true;
                    DropDownListYearandLevel.EnableEdit = false;
                    SourceUnit.Enabled = false;
                    AN.Enabled = false;
                    ProjectNature.Enabled = false;
                    List<BasicCode> lists = bllBasicCode.FindALLName("项目状态");
                    for (int i = 0; i < lists.Count(); i++)
                    {
                        DropDownListYearandLevel.Items.Add(lists[i].CategoryContent.ToString(), lists[i].CategoryContent.ToString());
                    }
                    DropDownListYearandLevel.SelectedValue = lists[0].CategoryContent.ToString();
                    break;
                case "年份":
                    SourceUnit.Text = "";
                    DropDownListYearandLevel.Items.Clear();
                    for (int i = 1960; i <= 2060; i++)
                    {
                        DropDownListYearandLevel.Items.Add(i.ToString(), i.ToString());
                    }
                    DropDownListYearandLevel.EnableEdit = false;
                    DropDownListYearandLevel.Enabled = true;
                    SourceUnit.Enabled = false;
                    ProjectNature.Enabled = true;
                    AN.Enabled = true;
                    AN.Items.Clear();
                    AN.Items.Add("项目来源", "项目来源");
                    AN.Items.Add("项目级别", "项目级别");
                    AN.Items.Add("承担部门", "承担部门");
                    break;
                case "项目来源":
                    SourceUnit.Text = "";
                    DropDownListYearandLevel.Items.Clear();
                    DropDownListYearandLevel.Enabled = false;
                    SourceUnit.Enabled = true;
                    ProjectNature.Enabled = true;
                    AN.Enabled = true;
                    AN.Items.Clear();
                    AN.Items.Add("年份", "年份");
                    break;
                case "项目级别":
                    SourceUnit.Text = "";
                    DropDownListYearandLevel.Items.Clear();
                    DropDownListYearandLevel.Enabled = false;
                    DropDownListYearandLevel.EnableEdit = false;
                    DropDownListYearandLevel.Enabled = true;
                    SourceUnit.Enabled = false;
                    ProjectNature.Enabled = true;
                    AN.Enabled = true;
                    AN.Items.Clear();
                    AN.Items.Add("年份", "年份");
                    List<BasicCode> list = bllBasicCode.FindALLName("级别");
                    for (int i = 0; i < list.Count(); i++)
                    {
                        DropDownListYearandLevel.Items.Add(list[i].CategoryContent.ToString(), list[i].CategoryContent.ToString());
                    }
                    DropDownListYearandLevel.SelectedValue = list[0].CategoryContent.ToString();
                    break;
                case "承担部门":
                    SourceUnit.Text = "";
                    DropDownListYearandLevel.Items.Clear();
                    DropDownListYearandLevel.Enabled = false;
                    SourceUnit.Enabled = true;
                    ProjectNature.Enabled = true;
                    AN.Enabled = true;
                    AN.Items.Clear();
                    AN.Items.Add("年份", "年份");
                    AN.Items.Add("负责人", "负责人");
                    break;
                case "项目负责人":
                    SourceUnit.Text = "";
                    DropDownListYearandLevel.Items.Clear();
                    DropDownListYearandLevel.Enabled = false;
                    SourceUnit.Enabled = true;
                    ProjectNature.Enabled = true;
                    break;
                case "项目性质":
                    SourceUnit.Text = "";
                    DropDownListYearandLevel.Items.Clear();
                    DropDownListYearandLevel.Enabled = false;
                    SourceUnit.Enabled = true;
                    ProjectNature.Enabled = true;
                    break;
                case "项目成员":
                    DropDownListYearandLevel.Items.Clear();
                    SourceUnit.Text = "";
                    DropDownListYearandLevel.Enabled = false;
                    SourceUnit.Enabled = true;
                    AN.Enabled = false;
                    ProjectNature.Enabled = true;
                    break;
                case "保密等级":
                    DropDownListYearandLevel.Items.Clear();
                    SourceUnit.Text = "";
                    DropDownListYearandLevel.Enabled = false;
                    DropDownListYearandLevel.Enabled = true;
                    DropDownListYearandLevel.EnableEdit = false;
                    SourceUnit.Enabled = false;
                    AN.Enabled = false;
                    ProjectNature.Enabled = true;
                    DropDownListYearandLevel.Items.Add("四级", "四级");
                    DropDownListYearandLevel.Items.Add("三级", "三级");
                    DropDownListYearandLevel.Items.Add("二级", "二级");
                    DropDownListYearandLevel.Items.Add("一级", "一级");
                    DropDownListYearandLevel.Items.Add("管理员", "管理员");
                    DropDownListYearandLevel.Items[0].Selected = true;
                    //DropDownListYearandLevel.SelectedValue = lists[0].CategoryContent.ToString();
                    break;
            }
        }
        public int RowNumber(int dataItemIndex)
        {
            return dataItemIndex + (GridProjectAll.PageIndex) * GridProjectAll.PageSize;
        }
        public int RowNumber_two(int dataItemIndextwo)
        {
            return dataItemIndextwo + (GridProjectAllTwo.PageIndex) * GridProjectAllTwo.PageSize;
        }

        //全选按钮
        protected void btnSelect_All_Click(object sender, EventArgs e)
        {
            GridProjectAll.SelectAllRows();
            int[] select = GridProjectAll.SelectedRowIndexArray;
            int m;
            //取整数（不是四舍五入，全舍）
            int Pages = (int)Math.Floor(Convert.ToDouble(GridProjectAll.RecordCount / this.GridProjectAll.PageSize));

            if (GridProjectAll.PageIndex == Pages)
                m = (GridProjectAll.RecordCount - this.GridProjectAll.PageSize * GridProjectAll.PageIndex);
            else
                m = this.GridProjectAll.PageSize;
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

        //全选按钮
        protected void btnSelect_All_Click_Two(object sender, EventArgs e)
        {
            GridProjectAllTwo.SelectAllRows();
            int[] select = GridProjectAllTwo.SelectedRowIndexArray;
            int m;
            //取整数（不是四舍五入，全舍）
            int Pages = (int)Math.Floor(Convert.ToDouble(GridProjectAllTwo.RecordCount / this.GridProjectAllTwo.PageSize));

            if (GridProjectAllTwo.PageIndex == Pages)
                m = (GridProjectAllTwo.RecordCount - this.GridProjectAllTwo.PageSize * GridProjectAllTwo.PageIndex);
            else
                m = this.GridProjectAllTwo.PageSize;
            bool isCheck = false;
            for (int i = 0; i < m; i++)
            {
                if (CBSelect_Two.GetCheckedState(i) == false)
                    isCheck = true;
            }
            if (isCheck)
            {
                foreach (int item in select)
                {
                    CBSelect_Two.SetCheckedState(item, true);
                }
                btnDelete_two.Enabled = true;
                btnSelect_All_Two.Text = "取消全选";
            }
            else
            {
                foreach (int item in select)
                {
                    CBSelect_Two.SetCheckedState(item, false);
                }
                btnDelete_two.Enabled = false;
                btnSelect_All_Two.Text = "全选";
            }
        }
    }
}