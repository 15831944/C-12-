/**编写人：方淑云
 * 时间：2014年8月24号
 * 功能:成果鉴定查询界面
 * 修改履历：
 *               修改人;马睿杰
 *              修改时间;20150925
 *              内容：增加保密级别搜索，成员字段
 *               修改人;高琪
 *              修改时间;20151010
 *              内容：撤销page静态变量
 **/
using Common.Entities;
using FineUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WDFramework.Achievement.AchievementInfo
{
    public partial class Search_AchievementInfoes : System.Web.UI.Page
    {
        BLHelper.BLLAgency agency = new BLHelper.BLLAgency();
        BLHelper.BLLProject project = new BLHelper.BLLProject();
        BLHelper.BLLAchievement achieve = new BLHelper.BLLAchievement();   
        BLHelper.BLLUser user = new BLHelper.BLLUser();
        BLHelper.BLLStaffAchieve sp = new BLHelper.BLLStaffAchieve();
        BLHelper.BLLStaffAchieve blst = new BLHelper.BLLStaffAchieve();
        BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
        BLHelper.BLLOperationLog bllOperate = new BLHelper.BLLOperationLog();

        private int page; 
        Common.Entities.Achievement ach = new Common.Entities.Achievement();
        protected void Page_Load(object sender, EventArgs e)
        {
            page = ViewState["page"] == null ? 0 : (int)ViewState["page"];
            if (!IsPostBack)
            {
                InitData();
                btn_AddAchievement.OnClientClick = Window_addAchievement.GetShowReference("Add_Achievement.aspx", "新增成果鉴定信息");
                //reprot1.OnClientClick = WindowReport.GetShowReference("~/Report/R_Agency_Project_Achievement.aspx", "分部门按项目统计项目成果情况");
                //reprot2.OnClientClick = WindowReport.GetShowReference("~/Report/R_Agency_Appraisal.aspx", "分部门按鉴定组织部门、鉴定评语级别、鉴定时间统计鉴定情况");       
            }
        } 
        //界面初始化
        public void InitData()
        {
            try
            {
                ViewState["page"] = 0;
                List<Common.Entities.Achievement> list = achieve.FindPaged(Convert.ToInt32(Session["SecrecyLevel"]));
                Grid_Achievementt.RecordCount = list.Count();
                if (list != null)
                {
                    Grid_Achievementt.DataSource = list.Skip(Grid_Achievementt.PageIndex * Grid_Achievementt.PageSize).Take(Grid_Achievementt.PageSize);
                    Grid_Achievementt.DataBind();
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
        //下载
        protected string GetEditUrlu(object ID)
        {
            return DownLoad.GetShowReference("Operate.aspx?id=" + ID, "操作");
        }
        //保密级别转化
        public string ChangeSecrecyLevel(int level)
        {
            string[] SecrecyLevels = new string[] { "四级", "三级", "二级", "一级", "管理员" };
            return SecrecyLevels[level - 1];
        }
        //将机构ID转化为机构名
        protected string FindAgencyName(int ag)
        {
            try
            {
                return agency.FindAgenName(ag);
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
                return "";
            }
        }
        //完成人界面跳转
        protected string GetEditUrl(object ID)
        {
            return Peoplef.GetShowReference("Peoples.aspx?id=" + ID, "作者信息");
        }
        //成员界面跳转
        protected string GetEditUrlm(object ID)
        {
            return Peoplef.GetShowReference("Member.aspx?id=" + ID, "成员信息");
        }
    
        
        //将项目ID转化为项目名称
        public string FindProjectName(int projectid)
        {
            try
            {
                if (projectid != 0)
                {
                    return project.SelectProjectName(projectid);
                }
                else
                    return "";
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
                return "";
            }
        }
        //刷新
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            dChoose.SelectedValue = "全部";
            dCondition.Reset();
            tCondition.Reset();
            dCondition.Enabled = false;
            tCondition.Enabled = false;
            InitData();
        }
        //按AchievementApplyID进行删除
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                List<int> selections = pm.GridCount(Grid_Achievementt, CBoxSelect);
                if (Convert.ToInt32(Session["SecrecyLevel"]) == 5)
                {
                    for (int i = 0; i < selections.Count(); i++)
                    {
                        achieve.Delete(Convert.ToInt32(Grid_Achievementt.DataKeys[selections[i]][0]));
                    }
                    InitData();
                    btnSelect_All.Text = "全选";
                    Alert.ShowInTop("删除数据成功!");
                }
                else
                {
                    for (int i = 0; i < selections.Count(); i++)
                    {
                        achieve.UpdateIsPass(Convert.ToInt32(Grid_Achievementt.DataKeys[selections[i]][0]), false);
                        //向操作日志表中插入
                        OperationLog operate = new OperationLog();
                        operate.LoginName = user.FindByLoginName(Session["LoginName"].ToString()).UserName;
                        operate.LoginIP = "";
                        operate.OperationType = "删除";
                        operate.OperationContent = "ProjectImportantNode";
                        operate.OperationDataID = Convert.ToInt32(Grid_Achievementt.DataKeys[selections[i]][0]);
                        operate.OperationTime = System.DateTime.Now;
                        operate.Remark = "";
                        bllOperate.Insert(operate);
                    }
                    btnSelect_All.Text = "全选";
                    Alert.ShowInTop("您的操作已提交，请等待审核！");
                    InitData();
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
        //修改
        protected void btn_UpdateAchievement_Click(object sender, EventArgs e)
        {
            try
            {
                if (pm.GridCount(Grid_Achievementt, CBoxSelect).Count() != 0)
                {
                    if (pm.GridCount(Grid_Achievementt, CBoxSelect).Count() == 1)
                    {
                        int rowID = Convert.ToInt32(Grid_Achievementt.DataKeys[pm.GridCount(Grid_Achievementt, CBoxSelect)[0]][0]);
                        Session["AchievementID"] = rowID;
                        Alert.Show("你确定要修改该行数据吗!", "确认消息", MessageBoxIcon.Information, Window_UpdateAchievement.GetShowReference("Update_Achievement.aspx", "修改成果鉴定信息"), Target.Top);
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
                pm.SaveError(ex, this.Request);
            }

        }
        //搜索
        protected void Select_Click(object sender, EventArgs e)
        {
            try
            {
                Grid_Achievementt.PageIndex = 0;
                switch (dChoose.SelectedText)
                {
                    case "全部":
                        InitData();
                        return;

                    case "成果名称":
                        FindByName();
                        return;

                    case "项目名称":
                        FindByProject();
                        return;

                    case "完成人":
                        FindByPeople();
                        return;

                    case "鉴定年份":
                        FindByTime();
                        return;

                    case "鉴定组织部门":
                        FindByUnit();
                        return;

                    case "鉴定评语级别":
                        FindByLevel();
                        return;

                    case "所属机构":
                        FindByAgencyName();
                        break;
                    case "鉴定级别":
                        FindByProjectRank();
                        break;
                    case"保密级别":
                        FindBySecrecyLevel();
                        break;
                    case"成员":
                        FindByMember();
                        break;
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }

        }

        //根据鉴定级别查询
        private void FindByProjectRank()
        {
            try
            {
                ViewState["page"] = 8;

                List<Common.Entities.Achievement> list = achieve.FindByProjectRank(tCondition.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                Grid_Achievementt.RecordCount = list.Count();
                if (list != null)
                {
                    Grid_Achievementt.DataSource = list.Skip(Grid_Achievementt.PageIndex * Grid_Achievementt.PageSize).Take(Grid_Achievementt.PageSize);
                    Grid_Achievementt.DataBind();
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
        public int LevelChange(string text)
        {
            int selevel = 0;
            if (dCondition.SelectedText.Trim() == "四级")
                selevel = 1;
            if (dCondition.SelectedText.Trim() == "三级")
                selevel = 2;
            if (dCondition.SelectedText.Trim() == "二级")
                selevel = 3;
            if (dCondition.SelectedText.Trim() == "一级")
                selevel = 4;
            if (dCondition.SelectedText.Trim() == "管理员")
                selevel = 5;
            return selevel;
        }
        
        //根据保密级别查询
        private void FindBySecrecyLevel()
        {
            try
            {
                ViewState["page"] = 9;
                int selevel = LevelChange(dCondition.SelectedText.Trim());
                List<Common.Entities.Achievement> list = achieve.FindBySecrecyLevel(selevel, Convert.ToInt32(Session["SecrecyLevel"]));
                Grid_Achievementt.RecordCount = list.Count();
                if (list != null)
                {
                    Grid_Achievementt.DataSource = list.Skip(Grid_Achievementt.PageIndex * Grid_Achievementt.PageSize).Take(Grid_Achievementt.PageSize);
                    Grid_Achievementt.DataBind();
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
        //根据成员模糊查询
        private void FindByMember()
        {
            try
            {
                ViewState["page"] = 10;

                List<Common.Entities.Achievement> list = achieve.FindByMember(tCondition.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                Grid_Achievementt.RecordCount = list.Count();
                if (list != null)
                {
                    Grid_Achievementt.DataSource = list.Skip(Grid_Achievementt.PageIndex * Grid_Achievementt.PageSize).Take(Grid_Achievementt.PageSize);
                    Grid_Achievementt.DataBind();
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
        //根据所属机构名称查询
        private void FindByAgencyName()
        {
            try
            {
                ViewState["page"] = 7;
                int agencyid = agency.SelectAgencyID(tCondition.Text.Trim());
                List<Common.Entities.Achievement> list = achieve.FindByAgencyName(agencyid, Convert.ToInt32(Session["SecrecyLevel"]));
                Grid_Achievementt.RecordCount = list.Count();
                if (list != null)
                {
                    Grid_Achievementt.DataSource = list.Skip(Grid_Achievementt.PageIndex * Grid_Achievementt.PageSize).Take(Grid_Achievementt.PageSize);
                    Grid_Achievementt.DataBind();
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
     
        //根据项目查询
        public void FindByProject()
        {
            try
            {
                ViewState["page"] = 1;
                //List<int> ProjectID = project.FindProjectList(tCondition.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                List<Common.Entities.Achievement> list = achieve.FindByProject(tCondition.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                Grid_Achievementt.RecordCount = list.Count();
                if (list != null)
                {
                    Grid_Achievementt.DataSource = list.Skip(Grid_Achievementt.PageIndex * Grid_Achievementt.PageSize).Take(Grid_Achievementt.PageSize);
                    Grid_Achievementt.DataBind();
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
        //根据人员查找
        public void FindByPeople()
        {
            try
            {
                ViewState["page"] = 2;
                List<int> UserInfoID = user.FindList(tCondition.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                List<int> AchieveID = blst.SelectIDlist(UserInfoID, Convert.ToInt32(Session["SecrecyLevel"]));
                List<Common.Entities.Achievement> list = achieve.SelectAll(AchieveID, Convert.ToInt32(Session["SecrecyLevel"]));
                Grid_Achievementt.RecordCount = list.Count();
                if (list != null)
                {
                    Grid_Achievementt.DataSource = list.Skip(Grid_Achievementt.PageIndex * Grid_Achievementt.PageSize).Take(Grid_Achievementt.PageSize);
                    Grid_Achievementt.DataBind();
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
        //根据成果名称查询
        public void FindByName()
        {
            try
            {
                ViewState["page"] = 3;
                List<Common.Entities.Achievement> list = achieve.FindByName(tCondition.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                Grid_Achievementt.RecordCount = list.Count();
                if (list != null)
                {
                    Grid_Achievementt.DataSource = list.Skip(Grid_Achievementt.PageIndex * Grid_Achievementt.PageSize).Take(Grid_Achievementt.PageSize);
                    Grid_Achievementt.DataBind();
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
        //根据鉴定时间
        public void FindByTime()
        {
            try
            {
                ViewState["page"] = 4;
                List<Common.Entities.Achievement> list = achieve.FindByTime(Convert.ToInt32(dCondition.SelectedText.Trim()), Convert.ToInt32(Session["SecrecyLevel"]));
                Grid_Achievementt.RecordCount = list.Count();
                if (list != null)
                {
                    Grid_Achievementt.DataSource = list.Skip(Grid_Achievementt.PageIndex * Grid_Achievementt.PageSize).Take(Grid_Achievementt.PageSize);
                    Grid_Achievementt.DataBind();
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
        //根据鉴定组织部门查询
        public void FindByUnit()
        {
            try
            {
                ViewState["page"] = 5;
                List<Common.Entities.Achievement> list = achieve.FindByUnit(tCondition.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                Grid_Achievementt.RecordCount = list.Count();
                if (list != null)
                {
                    Grid_Achievementt.DataSource = list.Skip(Grid_Achievementt.PageIndex * Grid_Achievementt.PageSize).Take(Grid_Achievementt.PageSize);
                    Grid_Achievementt.DataBind();
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
        //根据鉴定评语级别查询
        public void FindByLevel()
        {
            try
            {
                ViewState["page"] = 6;
                List<Common.Entities.Achievement> list = achieve.FindByLevel(tCondition.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                Grid_Achievementt.RecordCount = list.Count();
                if (list != null)
                {
                    Grid_Achievementt.DataSource = list.Skip(Grid_Achievementt.PageIndex * Grid_Achievementt.PageSize).Take(Grid_Achievementt.PageSize);
                    Grid_Achievementt.DataBind();
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
        //分页（改）
        protected void Grid_Achievement_PageIndexChange(object sender, GridPageEventArgs e)
        {
            Grid_Achievementt.PageIndex = e.NewPageIndex;
            switch (page)
            {
                case 0:
                    InitData();
                    break;
                case 1:
                    FindByProject();
                    break;
                case 2:
                    FindByPeople();
                    break;
                case 3:
                    FindByName();
                    break;
                case 4:
                    FindByTime();
                    break;
                case 5:
                    FindByUnit();
                    break;
                case 6:
                    FindByLevel();
                    break;
                case 7:
                    FindByAgencyName();
                    break;
                case 8:
                    FindByProjectRank();
                    break;
                case 9:
                    FindBySecrecyLevel();
                    break;
                case 10:
                    FindByMember();
                    break;
            }
        }

        protected void ddlGridPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid_Achievementt.PageIndex = 0;
            this.Grid_Achievementt.PageSize = Convert.ToInt32(ddlGridPageSize.SelectedValue);
            switch (page)
            {
                case 0:
                    InitData();
                    break;
                case 1:
                    FindByProject();
                    break;
                case 2:
                    FindByPeople();
                    break;
                case 3:
                    FindByName();
                    break;
                case 4:
                    FindByTime();
                    break;
                case 5:
                    FindByUnit();
                    break;
                case 6:
                    FindByLevel();
                    break;
                case 7:
                    FindByAgencyName();
                    break;
                case 8:
                    FindByProjectRank();
                    break;
                case 9:
                    FindBySecrecyLevel();
                    break;
                case 10:
                    FindByMember();
                    break;
            }
        }
        //行点击事件
        protected void Grid_Achievement_RowCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                string Person = Grid_Achievementt.Rows[e.RowIndex].Values[2].ToString();
                string username = user.FindByLoginName(Session["LoginName"].ToString()).UserName;

                if (Person != username && Convert.ToInt32(Session["SecrecyLevel"]) != 5)
                {
                    string str = "您无对此行操作的权限！此行信息为" + Person + "录入，请与管理员联系!";
                    CBoxSelect.SetCheckedState(e.RowIndex, false);
                    Alert.ShowInTop(str);
                }
                int m;
                //取整数（不是四舍五入，全舍）
                int Pages = (int)Math.Floor(Convert.ToDouble(Grid_Achievementt.RecordCount / this.Grid_Achievementt.PageSize));

                if (Grid_Achievementt.PageIndex == Pages)
                    m = (Grid_Achievementt.RecordCount - this.Grid_Achievementt.PageSize * Grid_Achievementt.PageIndex);
                else
                    m = this.Grid_Achievementt.PageSize;
                bool isCheck = false;
                for (int i = 0; i < m; i++)
                {
                    if (CBoxSelect.GetCheckedState(i))
                        isCheck = true;
                }
                if (isCheck)
                    btnDelete.Enabled = true;
                else
                    btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }

        }
        //搜索事件
        protected void dChoose_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                switch (dChoose.SelectedValue)
                {
                    case "全部":
                        dCondition.Enabled = false;
                        tCondition.Enabled = false;
                        break;
                    case "成果名称":
                        dCondition.Enabled = false;
                        tCondition.Enabled = true;
                        break;
                    case "鉴定年份":
                        dCondition.Items.Clear();
                        for (int i = 1960; i <= 2060; i++)
                        {
                            dCondition.Items.Add(i.ToString(), i.ToString());
                        }
                        dCondition.Items[0].Selected = true;
                        dCondition.Enabled = true;
                        tCondition.Enabled = false;
                        break;
                    case "完成人":
                        dCondition.Enabled = false;
                        tCondition.Enabled = true;
                        break;
                    case "项目名称":
                        dCondition.Enabled = false;
                        tCondition.Enabled = true;
                        break;
                    case "鉴定组织部门":
                        dCondition.Enabled = false;
                        tCondition.Enabled = true;
                        break;
                    case "鉴定评语级别":
                        dCondition.Enabled = false;
                        tCondition.Enabled = true;
                        break;
                    case"成员":
                        dCondition.Enabled = false;
                        tCondition.Enabled = true;
                        break;
                    case"保密级别":
                        dCondition.Items.Clear();
                        string[] selevel= new string[]{"四级","三级","二级","一级","管理员"};
                        for (int i =0; i <=4; i++)
                        {
                            dCondition.Items.Add(selevel[i].ToString(), selevel[i].ToString());
                        }
                        dCondition.Items[0].Selected = true;
                        dCondition.Enabled = true;
                        tCondition.Enabled = false;
                        break;
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
          //导出
        protected void btn_Get_Click(object sender, EventArgs e)
        {
            try
            {
                if (page == 0)
                {
                    List<Common.Entities.Achievement> list = achieve.FindPaged(Convert.ToInt32(Session["SecrecyLevel"]));
                    if (list != null)
                    {
                        Grid_Achievementt.DataSource = list;
                        Grid_Achievementt.DataBind();
                    }
                }
                if (page == 1)
                {
                   // List<int> ProjectID = project.FindProjectList(tCondition.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                    List<Common.Entities.Achievement> list = achieve.FindByProject(tCondition.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                    if (list != null)
                    {
                        Grid_Achievementt.DataSource = list;
                        Grid_Achievementt.DataBind();
                    }
                }
                if (page == 2)
                {
                    List<int> UserInfoID = user.FindList(tCondition.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                    List<int> AchieveID = blst.SelectIDlist(UserInfoID, Convert.ToInt32(Session["SecrecyLevel"]));
                    List<Common.Entities.Achievement> list = achieve.SelectAll(AchieveID, Convert.ToInt32(Session["SecrecyLevel"]));
                    if (list != null)
                    {
                        Grid_Achievementt.DataSource = list;
                        Grid_Achievementt.DataBind();
                    }
                }
                if (page == 3)
                {
                    List<Common.Entities.Achievement> list = achieve.FindByName(tCondition.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                    if (list != null)
                    {
                        Grid_Achievementt.DataSource = list;
                        Grid_Achievementt.DataBind();
                    }
                }
                if (page == 4)
                {
                    List<Common.Entities.Achievement> list = achieve.FindByTime(Convert.ToInt32(dCondition.SelectedText.Trim()), Convert.ToInt32(Session["SecrecyLevel"]));
                    if (list != null)
                    {
                        Grid_Achievementt.DataSource = list;
                        Grid_Achievementt.DataBind();
                    }
                }
                if (page == 5)
                {
                    List<Common.Entities.Achievement> list = achieve.FindByUnit(tCondition.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                    if (list != null)
                    {
                        Grid_Achievementt.DataSource = list;
                        Grid_Achievementt.DataBind();
                    }
                }
                if (page == 6)
                {
                    List<Common.Entities.Achievement> list = achieve.FindByLevel(tCondition.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                    if (list != null)
                    {
                        Grid_Achievementt.DataSource = list;
                        Grid_Achievementt.DataBind();
                    }
                }
                if (page == 9)
                {
               int selevel = LevelChange(dCondition.SelectedText.Trim());
                    List<Common.Entities.Achievement> list = achieve.FindBySecrecyLevel( selevel, Convert.ToInt32(Session["SecrecyLevel"]));
                    if (list != null)
                    {
                        Grid_Achievementt.DataSource = list;
                        Grid_Achievementt.DataBind();
                    }
                }
                if (page == 10)
                {
                    List<Common.Entities.Achievement> list = achieve.FindByMember(tCondition.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                    if (list != null)
                    {
                        Grid_Achievementt.DataSource = list;
                        Grid_Achievementt.DataBind();
                    }
                }
                pm.ExportExcel(3, Grid_Achievementt, 2);
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
        //grid序号
        public int RowNumber(int dataItemIndex)
        {
            return dataItemIndex + (Grid_Achievementt.PageIndex) * Grid_Achievementt.PageSize;
        }
        //获取完成人
        public string getpeople(int id)
        {
            try
            {
                List<int> list = sp.FindStaName(id);
                string writername = "";
                for (int i = 0; i < list.Count; i++)
                {
                    writername += user.FindUserName(list[i]);
                    if (i == list.Count() - 1)
                    {
                        break;
                    }
                    writername += ",";
                }
                if (writername != "" || writername != null)
                {
                    return writername;
                }
                else
                {
                    return " ";
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
                return "";
            }
        }
        //全选按钮
        protected void btnSelect_All_Click(object sender, EventArgs e)
        {
            Grid_Achievementt.SelectAllRows();
            int[] select = Grid_Achievementt.SelectedRowIndexArray;
            int m;
            //取整数（不是四舍五入，全舍）
            int Pages = (int)Math.Floor(Convert.ToDouble(Grid_Achievementt.RecordCount / this.Grid_Achievementt.PageSize));

            if (Grid_Achievementt.PageIndex == Pages)
                m = (Grid_Achievementt.RecordCount - this.Grid_Achievementt.PageSize * Grid_Achievementt.PageIndex);
            else
                m = this.Grid_Achievementt.PageSize;
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
