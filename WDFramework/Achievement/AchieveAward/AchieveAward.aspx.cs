/**编写人：方淑云
 * 时间：2014年8月14号
 * 功能:成果报奖查询界面后台
 * 修改履历：    修改人：吕博扬
 *              修改时间：20150919
 *              内容：增加“保密级别”查询条件
 *              修改人;高琪
 *              修改时间;20151010
 *              内容：撤销page静态变量
 **/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common.Entities;
using FineUI;

namespace WebApplication1
{
    public partial class AchieveAward : System.Web.UI.Page
    {

        BLHelper.BLLAchievement ach = new BLHelper.BLLAchievement();
        BLHelper.BLLAchieveAward award = new BLHelper.BLLAchieveAward();
        BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
        BLHelper.BLLUser user = new BLHelper.BLLUser();
        BLHelper.BLLOperationLog bllOperate = new BLHelper.BLLOperationLog();

        private int page;
        protected void Page_Load(object sender, EventArgs e)
        {
            page = ViewState["page"] == null ? 0 : (int)ViewState["page"];
            btnSelect_All.Text = "全选";
            if (ddl_search.SelectedIndex == 0)
                tAchieveName.Enabled = false;
            if (ddl_search.SelectedIndex != 4)
                secrecyLevel.Enabled = false;
            if (!IsPostBack)
            {
                InitData();
                btnAddAchieveAward.OnClientClick = Window_addAchieveAward.GetShowReference("Add_AchieveAward.aspx", "新增成果报奖信息");
                //reprot1.OnClientClick = WindowReport.GetShowReference("~/Report/R_Agency_User_AchieveAwards.aspx", "分部门按人员统计成果报奖情况");             
            }

        }
        //初始化界面 
        public void InitData()
        {
            try
            {
                ViewState["page"] = 0;
                List<Common.Entities.AchieveAward> list = award.FindPaged(Convert.ToInt32(Session["secrecyLevel"]));
                Grid_AchieveAward.RecordCount = list.Count();
                if (list != null)
                {
                    Grid_AchieveAward.DataSource = list.Skip(Grid_AchieveAward.PageIndex * Grid_AchieveAward.PageSize).Take(Grid_AchieveAward.PageSize);
                    Grid_AchieveAward.DataBind();
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
        //将成果ID转化为成果名称
        protected string FindName(int ah)
        {
            try
            {
                return ach.FindAchieveName(ah);
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
                return "";
            }
        }
        //报奖人
        protected string GetEditUrl(object ID)
        {
            try
            {
                return AchieveAwardPeople.GetShowReference("AchieveAwardPeople.aspx?id=" + ID, "报奖人");
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
                return "";
            }
        }
        //成员
        protected string GetEditUrlMember(object ID)
        {
            try
            {
                return AchieveAwardMember.GetShowReference("AchieveAwardMamber.aspx?id=" + ID, "成员");
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
            try
            {
                tAchieveName.Reset();
                InitData();
                ddl_search.Reset();
                tAchieveName.Enabled = false;
                secrecyLevel.Enabled = false;
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);

            }
        }
        //按achieveAwardID进行删除
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                List<int> selections = pm.GridCount(Grid_AchieveAward, CBoxSelect);
                if (Convert.ToInt32(Session["SecrecyLevel"]) == 5)
                {
                    for (int i = 0; i < selections.Count(); i++)
                    {
                        award.Delete(Convert.ToInt32(Grid_AchieveAward.DataKeys[selections[i]][0]));
                    }
                    InitData();
                    btnSelect_All.Text = "全选";
                    Alert.ShowInTop("删除数据成功!");
                }
                else
                {
                    for (int i = 0; i < selections.Count(); i++)
                    {
                        award.UpdateIsPass(Convert.ToInt32(Grid_AchieveAward.DataKeys[selections[i]][0]), false);
                        //向操作日志表中插入
                        OperationLog operate = new OperationLog();
                        operate.LoginName = user.FindByLoginName(Session["LoginName"].ToString()).UserName;
                        operate.LoginIP = "";
                        operate.OperationType = "删除";
                        operate.OperationContent = "ProjectImportantNode";
                        operate.OperationDataID = Convert.ToInt32(Grid_AchieveAward.DataKeys[selections[i]][0]);
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
        protected void btnReviseAchieveAward_Click(object sender, EventArgs e)
        {
            try
            {
                if (pm.GridCount(Grid_AchieveAward, CBoxSelect).Count() != 0)
                {
                    if (pm.GridCount(Grid_AchieveAward, CBoxSelect).Count() == 1)
                    {
                        int rowID = Convert.ToInt32(Grid_AchieveAward.DataKeys[pm.GridCount(Grid_AchieveAward, CBoxSelect)[0]][0]);
                        Session["AchieveAwardID"] = rowID;
                        Alert.Show("你确定要修改该行数据吗!", "确认消息", MessageBoxIcon.Information, Window_ReviseAchieveAward.GetShowReference("Revise_AchieveAward.aspx", "修改成果报奖信息"), Target.Top);
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
        //分页
        protected void Grid_AchieveAward_PageIndexChange(object sender, GridPageEventArgs e)
        {
            try
            {
                Grid_AchieveAward.PageIndex = e.NewPageIndex;
                switch (page)
                {
                    case 0:
                        InitData();
                        break;
                    case 1:
                        FindByAward();
                        break;
                    case 2:
                        FindByAwardUnit();
                        break;
                    case 3:
                        FindByMember();
                        break;
                    case 4:
                        FindBySecrecyLevel();
                        break;
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);

            }
        }

        protected void ddlGridPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Grid_AchieveAward.PageIndex = 0;
                this.Grid_AchieveAward.PageSize = Convert.ToInt32(ddlGridPageSize.SelectedValue);
                switch (page)
                {
                    case 0:
                        InitData();
                        break;
                    case 1:
                        FindByAward();
                        break;
                    case 2:
                        FindByAwardUnit();
                        break;
                    case 3:
                        FindByMember();
                        break;
                    case 4:
                        FindBySecrecyLevel();
                        break;
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);

            }
        }
        //行点击事件
        protected void Grid_AchieveAward_RowCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                string Person = Grid_AchieveAward.Rows[e.RowIndex].Values[2].ToString();
                string username = user.FindByLoginName(Session["LoginName"].ToString()).UserName;

                if (Person != username && Convert.ToInt32(Session["SecrecyLevel"]) != 5)
                {
                    string str = "您无对此行操作的权限！此行信息为" + Person + "录入，请与管理员联系!";
                    CBoxSelect.SetCheckedState(e.RowIndex, false);
                    Alert.ShowInTop(str);

                }

                int m;
                //取整数（不是四舍五入，全舍）
                int Pages = (int)Math.Floor(Convert.ToDouble(Grid_AchieveAward.RecordCount / this.Grid_AchieveAward.PageSize));

                if (Grid_AchieveAward.PageIndex == Pages)
                    m = (Grid_AchieveAward.RecordCount - this.Grid_AchieveAward.PageSize * Grid_AchieveAward.PageIndex);
                else
                    m = this.Grid_AchieveAward.PageSize;
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
        //转化等级
        public string ChangeSecrecyLevel(int level)
        {
            try
            {
                string[] SecrecyLevels = new string[] { "四级", "三级", "二级", "一级", "管理员" };
                return SecrecyLevels[level - 1];
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
                return "";
            }
        }
        //分奖项按等级查看
        public void FindByAward()
        {
            try
            {
                ViewState["page"] = 1;
                List<Common.Entities.AchieveAward> list = award.FindByNameAndGrade(tAchieveName.Text.Trim(), Convert.ToInt32(Session["secrecyLevel"]));
                Grid_AchieveAward.RecordCount = list.Count();
                if (list != null)
                {
                    Grid_AchieveAward.DataSource = list.Skip(Grid_AchieveAward.PageIndex * Grid_AchieveAward.PageSize).Take(Grid_AchieveAward.PageSize);
                    Grid_AchieveAward.DataBind();
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

        //按照报奖单位查找报奖信息
        private void FindByAwardUnit()
        {
            try
            {
                ViewState["page"] = 2;
                List<Common.Entities.AchieveAward> list = award.FindByAwardUnit(tAchieveName.Text.Trim(), Convert.ToInt32(Session["secrecyLevel"]));
                Grid_AchieveAward.RecordCount = list.Count();
                if (list != null)
                {
                    Grid_AchieveAward.DataSource = list.Skip(Grid_AchieveAward.PageIndex * Grid_AchieveAward.PageSize).Take(Grid_AchieveAward.PageSize);
                    Grid_AchieveAward.DataBind();
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

        //按照项目成员查找报奖信息
        private void FindByMember()
        {
            try
            {
                ViewState["page"] = 3;
                List<Common.Entities.AchieveAward> list = award.FindByMember(tAchieveName.Text.Trim(), Convert.ToInt32(Session["secrecyLevel"]));
                Grid_AchieveAward.RecordCount = list.Count();
                if (list != null)
                {
                    Grid_AchieveAward.DataSource = list.Skip(Grid_AchieveAward.PageIndex * Grid_AchieveAward.PageSize).Take(Grid_AchieveAward.PageSize);
                    Grid_AchieveAward.DataBind();
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

        //按照保密级别查找报奖信息
        private void FindBySecrecyLevel()
        {
            try
            {
                ViewState["page"] = 4;
                List<Common.Entities.AchieveAward> list = award.FindBySecrecyLevel(secrecyLevel.SelectedIndex + 1, Convert.ToInt32(Session["secrecyLevel"]));
                Grid_AchieveAward.RecordCount = list.Count();
                if (list != null)
                {
                    Grid_AchieveAward.DataSource = list.Skip(Grid_AchieveAward.PageIndex * Grid_AchieveAward.PageSize).Take(Grid_AchieveAward.PageSize);
                    Grid_AchieveAward.DataBind();
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

        //搜索
        protected void Select_Click(object sender, EventArgs e)
        {
            try
            {
                Grid_AchieveAward.PageIndex = 0;
                if (tAchieveName.Text.Trim() != "" || ddl_search.SelectedIndex == 0 || ddl_search.SelectedIndex == 4)
                {
                    switch(ddl_search.SelectedIndex)
                    {
                        case 0:
                            //Alert.ShowInTop("请选择搜索条件！");
                            InitData();
                            return;
                        case 1:
                            FindByAward();
                            break;
                        case 2:
                            FindByAwardUnit();
                            break;
                        case 3:
                            FindByMember();
                            break;
                        case 4:
                            FindBySecrecyLevel();
                            break;
                    }
                }
                else
                {
                    Alert.Show("请输入搜索条件！");
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
                    List<Common.Entities.AchieveAward> list = award.FindPaged(Convert.ToInt32(Session["secrecyLevel"]));
                    if (list != null)
                    {
                        Grid_AchieveAward.DataSource = list;
                        Grid_AchieveAward.DataBind();
                    }
                }
                else if (page == 1)
                {
                    List<Common.Entities.AchieveAward> list = award.FindByNameAndGrade(tAchieveName.Text.Trim(), Convert.ToInt32(Session["secrecyLevel"]));
                    if (list != null)
                    {
                        Grid_AchieveAward.DataSource = list;
                        Grid_AchieveAward.DataBind();
                    }
                }
                else if (page == 2)
                {
                    List<Common.Entities.AchieveAward> list = award.FindByAwardUnit(tAchieveName.Text.Trim(), Convert.ToInt32(Session["secrecyLevel"]));
                    if (list != null)
                    {
                        Grid_AchieveAward.DataSource = list;
                        Grid_AchieveAward.DataBind();
                    }
                }
                else if (page == 3)
                {
                    List<Common.Entities.AchieveAward> list = award.FindByMember(tAchieveName.Text.Trim(), Convert.ToInt32(Session["secrecyLevel"]));
                    if (list != null)
                    {
                        Grid_AchieveAward.DataSource = list;
                        Grid_AchieveAward.DataBind();
                    }
                }
                else if (page == 4)
                {
                    List<Common.Entities.AchieveAward> list = award.FindBySecrecyLevel(secrecyLevel.SelectedIndex + 1, Convert.ToInt32(Session["secrecyLevel"]));
                    if (list != null)
                    {
                        Grid_AchieveAward.DataSource = list;
                        Grid_AchieveAward.DataBind();
                    }
                }
                else
                    return;
                pm.ExportExcel(3, Grid_AchieveAward, 1);
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);

            }
        }
        //获取报奖人
        public string getAchieveAwardPeople(int id)
        {
            try
            {
                string str = award.FindAchieveAwardPeople(id);
                if (str != "" || str != null)
                {
                    return str;
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
        //获取成员
        public string getMember(int id)
        {
            try
            {
                string str = award.FindMember(id);
                if (str != "" || str != null)
                {
                    return str;
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

        //查询条件更改
        protected void ddl_search_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_search.SelectedIndex == 0)
            {
                tAchieveName.Enabled = false;
                secrecyLevel.Enabled = false;
            }
            else if (ddl_search.SelectedIndex == 4)
            {
                tAchieveName.Enabled = false;
                secrecyLevel.Enabled = true;
            }
            else
            {
                tAchieveName.Enabled = true;
                secrecyLevel.Enabled = false;
            }
        }
        //全选按钮
        protected void btnSelect_All_Click(object sender, EventArgs e)
        {
            Grid_AchieveAward.SelectAllRows();
            int[] select = Grid_AchieveAward.SelectedRowIndexArray;
            int m;
            //取整数（不是四舍五入，全舍）
            int Pages = (int)Math.Floor(Convert.ToDouble(Grid_AchieveAward.RecordCount / this.Grid_AchieveAward.PageSize));

            if (Grid_AchieveAward.PageIndex == Pages)
                m = (Grid_AchieveAward.RecordCount - this.Grid_AchieveAward.PageSize * Grid_AchieveAward.PageIndex);
            else
                m = this.Grid_AchieveAward.PageSize;
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