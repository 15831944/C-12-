/**编写人：方淑云
 * 时间：2014年8月16号
 * 功能:成果获奖查询界面后台
 * 修改履历：1.时间：2015年3月3日
 *           修改人：李金秋
 *           修改内容：添加获奖删除功能
 *           2.修改人;高琪
 *           修改时间;20151010
 *           内容：撤销page静态变量
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
    public partial class Award : System.Web.UI.Page
    {
        BLHelper.BLLAchievement ach = new BLHelper.BLLAchievement();
        BLHelper.BLLAward award = new BLHelper.BLLAward();
        BLHelper.BLLUser user = new BLHelper.BLLUser();
        BLHelper.BLLBasicCode ba = new BLHelper.BLLBasicCode();
        BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
        BLHelper.BLLAward aw = new BLHelper.BLLAward();
        BLHelper.BLLAttachment BLLAttachment = new BLHelper.BLLAttachment();
        Common.Entities.OperationLog operate = new Common.Entities.OperationLog();
        BLHelper.BLLOperationLog op = new BLHelper.BLLOperationLog();
        private int page;
        protected void Page_Load(object sender, EventArgs e)
        {
            page = ViewState["page"] == null ? 0 : (int)ViewState["page"];
            if (!IsPostBack)
            {
                InitData();
                btnAddAward.OnClientClick = Window_addAward.GetShowReference("Add_Award.aspx", "新增成果获奖信息");
                reprot1.OnClientClick = WindowReport.GetShowReference("~/Report/R_Agency_Award.aspx", "分部门按获奖名称、获奖时间统计获奖情况");
                reprot2.OnClientClick = WindowReport.GetShowReference("~/Report/R_Agency_Project_AchievementAward.aspx", "分部门按项目、项目级别统计成果获奖情况");
            }
        }
        //下载
        protected string GetEditUrlu(object ID)
        {
            return DownLoad.GetShowReference("Operate.aspx?id=" + ID, "操作");
        }
        //成果名称
        protected string GetEditUrla(object ID)
        {
            return DownLoad.GetShowReference("AchievementName.aspx?id=" + ID, "成果名称");
        }
        //获奖人
        protected string GetEditUrlp(object ID)
        {
            return DownLoad.GetShowReference("AwardPeople.aspx?id=" + ID, "获奖人");
        }
        //成员
        protected string GetEditUrlf(object ID)
        {
            return DownLoad.GetShowReference("AwardMember.aspx?id=" + ID, "成员");
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
        //转化等级
        public string ChangeSecrecyLevel(int level)
        {
            //string[] SecrecyLevels = new string[] { "公开", "内部", "秘密", "机密", "管理员" };
            string[] SecrecyLevels = new string[] { "四级", "三级", "二级", "一级", "管理员" };
            return SecrecyLevels[level - 1];
        }

        //备注界面跳转
        protected string GetEditUrl(object AwardID)
        {
            return Remark.GetShowReference("Remark.aspx?id=" + AwardID, "备注");
        }
        //单位界面跳转
        protected string GetEditUrlw(object AwardID)
        {
            return Unit.GetShowReference("Unit.aspx?id=" + AwardID, "单位信息");
        }
        //初始化
        public void InitData()
        {
            try
            {
                ViewState["page"] = 0;
                List<Common.Entities.Award> list = award.FindPaged(Convert.ToInt32(Session["SecrecyLevel"]));
                Grid_Award.RecordCount = list.Count();
                if (list != null)
                {
                    Grid_Award.DataSource = list.Skip(Grid_Award.PageIndex * Grid_Award.PageSize).Take(Grid_Award.PageSize);
                    Grid_Award.DataBind();
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
        
        //按成果名称查询
        public void FindByName()
        {
            try
            {
                ViewState["page"] = 1;
                //List<int> achieve = ach.FindByAchievementNamelist(tCondition.Text.Trim());
                List<Common.Entities.Award> list = award.FindByAchievementName(tCondition.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                Grid_Award.RecordCount = list.Count();
                if (list != null)
                {
                    Grid_Award.DataSource = list.Skip(Grid_Award.PageIndex * Grid_Award.PageSize).Take(Grid_Award.PageSize);
                    Grid_Award.DataBind();
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
        //按级别查询
        public void FindByLevel()
        {
            try
            {
                ViewState["page"] = 2;
                List<Common.Entities.Award> list = award.FindByAchievementRank(dCondition.SelectedText.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                Grid_Award.RecordCount = list.Count();
                if (list != null)
                {
                    Grid_Award.DataSource = list.Skip(Grid_Award.PageIndex * Grid_Award.PageSize).Take(Grid_Award.PageSize);
                    Grid_Award.DataBind();
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
        //按时间查询
        public void FindByTime()
        {
            try
            {
                ViewState["page"] = 3;
                List<Common.Entities.Award> list = award.FindByAchievementTime(Convert.ToInt32(dCondition.SelectedText.Trim()), Convert.ToInt32(Session["SecrecyLevel"]));
                Grid_Award.RecordCount = list.Count();
                if (list != null)
                {
                    Grid_Award.DataSource = list.Skip(Grid_Award.PageIndex * Grid_Award.PageSize).Take(Grid_Award.PageSize);
                    Grid_Award.DataBind();
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
        //按颁奖部门查询
        public void FindByUnit()
        {
            try
            {
                ViewState["page"] = 4;
                List<Common.Entities.Award> list = award.FindByUnit(tCondition.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                Grid_Award.RecordCount = list.Count();
                if (list != null)
                {
                    Grid_Award.DataSource = list.Skip(Grid_Award.PageIndex * Grid_Award.PageSize).Take(Grid_Award.PageSize);
                    Grid_Award.DataBind();
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
        //分获奖名称按等级查看
        public void FindByAwardName()
        {
            try
            {
                ViewState["page"] = 5;
                List<Common.Entities.Award> list = award.FindByAwardName(tCondition.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                Grid_Award.RecordCount = list.Count();
                if (list != null)
                {
                    Grid_Award.DataSource = list.Skip(Grid_Award.PageIndex * Grid_Award.PageSize).Take(Grid_Award.PageSize);
                    Grid_Award.DataBind();
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
        //按获奖部门查看
        public void FindByAwardUnit()
        {
            try
            {
                ViewState["page"] = 6;
                List<Common.Entities.Award> list = award.AwardUnit(tCondition.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                Grid_Award.RecordCount = list.Count();
                if (list != null)
                {
                    Grid_Award.DataSource = list.Skip(Grid_Award.PageIndex * Grid_Award.PageSize).Take(Grid_Award.PageSize);
                    Grid_Award.DataBind();
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
        //按获奖人查看
        public void FindByAwardPeople()
        {
            try
            {
                ViewState["page"] = 7;
                List<Common.Entities.Award> list = award.FindByAwardPeople(tCondition.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                Grid_Award.RecordCount = list.Count();
                if (list != null)
                {
                    Grid_Award.DataSource = list.Skip(Grid_Award.PageIndex * Grid_Award.PageSize).Take(Grid_Award.PageSize);
                    Grid_Award.DataBind();
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }

        //按获奖类型查询
        private void FindByAwardForm()
        {
            try
            {
                ViewState["page"] = 8;
                List<Common.Entities.Award> list = award.FindByAwardForm(tCondition.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                Grid_Award.RecordCount = list.Count();
                if (list != null)
                {
                    Grid_Award.DataSource = list.Skip(Grid_Award.PageIndex * Grid_Award.PageSize).Take(Grid_Award.PageSize);
                    Grid_Award.DataBind();
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
        //转换保密级别类型函数
        private int Secrecyreturnint(string SecrecyLevelss)
        {
            if (SecrecyLevelss == "四级")
                return 1;
            else if (SecrecyLevelss == "三级")
                return 2;
            else if (SecrecyLevelss == "二级")
                return 3;
            else if (SecrecyLevelss == "一级")
                return 4;
            else if (SecrecyLevelss == "管理员")
                return 5;
            else
                return 0;
        }
        //按保密级别查询
        private void FindBySecrecy()
        {
            try
            {
                ViewState["page"] = 9;
                List<Common.Entities.Award> list = award.FindBySecrecyLevel(Secrecyreturnint(dCondition.SelectedText), Convert.ToInt32(Session["SecrecyLevel"]));
                Grid_Award.RecordCount = list.Count();
                if (list != null)
                {
                    Grid_Award.DataSource = list.Skip(Grid_Award.PageIndex * Grid_Award.PageSize).Take(Grid_Award.PageSize);
                    Grid_Award.DataBind();
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
        //按成员查询
        private void FindByMember()
        {
            try
            {
                ViewState["page"] = 10;
                List<Common.Entities.Award> list = award.FindByMember(tCondition.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                Grid_Award.RecordCount = list.Count();
                if (list != null)
                {
                    Grid_Award.DataSource = list.Skip(Grid_Award.PageIndex * Grid_Award.PageSize).Take(Grid_Award.PageSize);
                    Grid_Award.DataBind();
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
                Grid_Award.PageIndex = 0;
                if (dChoose.SelectedText == "全部")
                {
                    InitData();
                }
                if (dChoose.SelectedText == "获奖年份")
                {
                    FindByTime();
                }
                if (dChoose.SelectedText == "获奖等级")
                {
                    FindByLevel();
                }
                if (dChoose.SelectedText == "保密级别")
                {
                    FindBySecrecy();
                }
                if (tCondition.Text.Trim() != "")
                {
                    if (dChoose.SelectedText == "成果名称")
                    {
                        FindByName();
                    }
                    if (dChoose.SelectedText == "颁奖部门")
                    {
                        FindByUnit();
                    }
                   
                    if (dChoose.SelectedText == "获奖名称")
                    {
                        FindByAwardName();
                    }
                    if (dChoose.SelectedText == "获奖人")
                    {
                        FindByAwardPeople();
                    }
                    if (dChoose.SelectedText == "获奖部门")
                    {
                        FindByAwardUnit();
                    }
                    if (dChoose.SelectedText == "获奖类型")
                    {
                        FindByAwardForm();
                    }
                    if (dChoose.SelectedText == "成员")
                    {
                        FindByMember();
                    }
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
      
        //更新
        protected void btnReviseAward_Click(object sender, EventArgs e)
        {
            try
            {
                if (pm.GridCount(Grid_Award, CBoxSelect).Count() != 0)
                {
                    if (pm.GridCount(Grid_Award, CBoxSelect).Count() == 1)
                    {
                        int rowID = Convert.ToInt32(Grid_Award.DataKeys[pm.GridCount(Grid_Award, CBoxSelect)[0]][0]);
                        Session["AwardID"] = rowID;
                        Alert.Show("你确定要修改该行数据吗!", "确认消息", MessageBoxIcon.Information, Window_ReviseAward.GetShowReference("Update_Award.aspx", "修改成果获奖信息"), Target.Top);
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
      //行点击事件
        protected void Grid_Award_RowCommand(object sender, FineUI.GridCommandEventArgs e)
        {
            try
            {
               
                string Person = Grid_Award.Rows[e.RowIndex].Values[2].ToString();
                string username = user.FindByLoginName(Session["LoginName"].ToString()).UserName;

                if (Person != username && Convert.ToInt32(Session["SecrecyLevel"]) != 5)
                {
                    string str = "您无对此行操作的权限！此行信息为" + Person + "录入，请与管理员联系!";
                    CBoxSelect.SetCheckedState(e.RowIndex, false);
                    Alert.ShowInTop(str);
                    return;
                }
                int m;
                //取整数（不是四舍五入，全舍）
                int Pages = (int)Math.Floor(Convert.ToDouble(Grid_Award.RecordCount / this.Grid_Award.PageSize));

                if (Grid_Award.PageIndex == Pages)
                    m = (Grid_Award.RecordCount - this.Grid_Award.PageSize * Grid_Award.PageIndex);
                else
                    m = this.Grid_Award.PageSize;
                List<int> selections = new List<int>();
                for (int i = 0; i < m; i++)
                {
                    if (CBoxSelect.GetCheckedState(i))
                        selections.Add(i);
                }
                if (selections.Count == 0)
                {
                    btnDelete.Enabled = false;
                    //Alert.ShowInTop("请至少选择一项!");
                    return;
                }
                else
                    btnDelete.Enabled = true;
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
        //分页
        protected void Grid_Award_PageIndexChange(object sender, FineUI.GridPageEventArgs e)
        {
            Grid_Award.PageIndex = e.NewPageIndex;
            switch (page)
            {
                case 0:
                    InitData();
                    break;
                case 1:
                    FindByName();
                    break;
                case 2:
                    FindByLevel();
                    break;
                case 3:
                    FindByTime();
                    break;
                case 4:
                    FindByUnit();
                    break;
                case 5:
                    FindByAwardName();
                    break;
                case 6:
                    FindByAwardUnit();
                    break;
                case 7 :
                    FindByAwardPeople();
                    break;
                case 8:
                    FindByAwardForm();
                    break;
                case 9:
                    FindBySecrecy();
                    break;
                case 10:
                    FindByMember();
                    break;
            }
        }

        protected void ddlGridPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid_Award.PageIndex = 0;
            this.Grid_Award.PageSize = Convert.ToInt32(ddlGridPageSize.SelectedValue);
            switch (page)
            {
                case 0:
                    InitData();
                    break;
                case 1:
                    FindByName();
                    break;
                case 2:
                    FindByLevel();
                    break;
                case 3:
                    FindByTime();
                    break;
                case 4:
                    FindByUnit();
                    break;
                case 5:
                    FindByAwardName();
                    break;
                case 6:
                    FindByAwardUnit();
                    break;
                case 7:
                    FindByAwardPeople();
                    break;
                case 8:
                    FindByAwardForm();
                    break;
                case 9:
                    FindBySecrecy();
                    break;
                case 10:
                    FindByMember();
                    break;
            }
        }
        //搜索条件
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
                    case "获奖名称":
                        dCondition.Enabled = false;
                        tCondition.Enabled = true;
                        break;
                    case "获奖人":
                        dCondition.Enabled = false;
                        tCondition.Enabled = true;
                        break;
                    case "获奖部门":
                        dCondition.Enabled = false;
                        tCondition.Enabled = true;
                        break;
                    case "获奖年份":
                        dCondition.Items.Clear();
                        for (int i = 1960; i <= 2060; i++)
                        {
                            dCondition.Items.Add(i.ToString(), i.ToString());
                        }
                        dCondition.Items[0].Selected = true;
                        //dCondition.EnableEdit = false;
                        dCondition.Enabled = true;
                        tCondition.Enabled = false;
                        break;
                    case "颁奖部门":
                        dCondition.Enabled = false;
                        tCondition.Enabled = true;
                        break;
                    case "获奖等级":
                        dCondition.Items.Clear();
                        List<BasicCode> listname = ba.FindByCategoryName("获奖等级");
                        for (int i = 0; i < listname.Count(); i++)
                        {
                            dCondition.Items.Add(listname[i].CategoryContent.ToString(), listname[i].CategoryContent.ToString());
                        }
                        dCondition.Items[0].Selected = true;
                        dCondition.Enabled = true;
                        tCondition.Enabled = false;
                        break;
                    case "保密级别":
                        dCondition.Items.Clear();
                        for (int i = 0; i < Convert.ToInt32(Session["SecrecyLevel"]); i++)
                        {
                            string[] SecrecyLevels = new string[] { "四级", "三级", "二级", "一级", "管理员" };
                            dCondition.Items.Add(SecrecyLevels[i], i.ToString());
                        }
                        dCondition.Items[0].Selected = true;
                        dCondition.Enabled = true;
                        tCondition.Enabled = false;
                        break;
                    case "成员":
                        dCondition.Enabled = false;
                        tCondition.Enabled = true;
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
                    List<Common.Entities.Award> list = award.FindPaged(Convert.ToInt32(Session["SecrecyLevel"]));
                    if (list != null)
                    {
                        Grid_Award.DataSource = list;
                        Grid_Award.DataBind();
                    }
                }
                if (page == 1)
                {
                    List<Common.Entities.Award> list = award.FindByAchievementName(tCondition.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                    if (list != null)
                    {
                        Grid_Award.DataSource = list;
                        Grid_Award.DataBind();
                    }
                }
                if (page == 2)
                {
                    List<Common.Entities.Award> list = award.FindByAchievementRank(tCondition.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                    if (list != null)
                    {
                        Grid_Award.DataSource = list;
                        Grid_Award.DataBind();
                    }
                }
                if (page == 3)
                {
                    List<Common.Entities.Award> list = award.FindByAchievementTime(Convert.ToInt32(dCondition.SelectedText.Trim()), Convert.ToInt32(Session["SecrecyLevel"]));
                    if (list != null)
                    {
                        Grid_Award.DataSource = list;
                        Grid_Award.DataBind();
                    }
                }
                if (page == 4)
                {
                    List<Common.Entities.Award> list = award.FindByUnit(tCondition.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                    if (list != null)
                    {
                        Grid_Award.DataSource = list;
                        Grid_Award.DataBind();
                    }
                }
                if (page == 5)
                {
                    List<Common.Entities.Award> list = award.FindByAwardName(tCondition.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                    if (list != null)
                    {
                        Grid_Award.DataSource = list;
                        Grid_Award.DataBind();
                    }
                }
                if (page == 6)
                {
                    List<Common.Entities.Award> list = award.AwardUnit(tCondition.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                    if (list != null)
                    {
                        Grid_Award.DataSource = list;
                        Grid_Award.DataBind();
                    }
                }
                if (page == 7)
                {
                    List<Common.Entities.Award> list = award.FindByAwardPeople(tCondition.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                    if (list != null)
                    {
                        Grid_Award.DataSource = list;
                        Grid_Award.DataBind();
                    }
                }
                if (page == 9)
                {
                    List<Common.Entities.Award> list = award.FindBySecrecyLevel(Secrecyreturnint(dCondition.SelectedText), Convert.ToInt32(Session["SecrecyLevel"]));
                    if (list != null)
                    {
                        Grid_Award.DataSource = list;
                        Grid_Award.DataBind();
                    }
                }
                if (page == 10)
                {
                    List<Common.Entities.Award> list = award.FindByMember(tCondition.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                    if (list != null)
                    {
                        Grid_Award.DataSource = list;
                        Grid_Award.DataBind();
                    }
                }
                pm.ExportExcel(3, Grid_Award, 0);
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
        //grid序号
        public int RowNumber(int dataItemIndex)
        {
            return dataItemIndex + (Grid_Award.PageIndex) * Grid_Award.PageSize;
        }
       //获取单位
        public string getunit(int id)
        {
            try
            {
                string str = aw.FindUnit(id);
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
        //获取获奖人
        public string getpeople(int id)
        {
            try
            {
                string str = aw.FindWriter(id);
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
        //获取成果名称
        public string getachievement(int id)
        {
            try
            {
                string str = aw.Findmodel(id).Acheivement;
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
                string str = aw.FindMember(id);
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
        //删除获奖信息
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            //如果为超级管理员操作则直接删除，否则将IsPass置为false 向管理员发送删除信息
            try
            {
                int m;
                //取整数（不是四舍五入，全舍）
                int Pages = (int)Math.Floor(Convert.ToDouble(Grid_Award.RecordCount / this.Grid_Award.PageSize));

                if (Grid_Award.PageIndex == Pages)
                    m = (Grid_Award.RecordCount - this.Grid_Award.PageSize * Grid_Award.PageIndex);
                else
                    m = this.Grid_Award.PageSize;
                List<int> selections = new List<int>();
                for (int i = 0; i < m; i++)
                {
                    if (CBoxSelect.GetCheckedState(i))
                        selections.Add(i);
                }
                if (Convert.ToInt32(Session["SecrecyLevel"]) == 5)
                {
                    for (int i = 0; i < selections.Count(); i++)
                    {
                        int AcademicMeetingID = Convert.ToInt32(Grid_Award.DataKeys[selections[i]][0].ToString());

                        //删除获奖附件文件->删除附件表中的信息->删除学术会议信息
                        int AttactID = award.FindAttachmentID(AcademicMeetingID);
                        string strPath;
                        if (AttactID != 0)
                        {
                            strPath = BLLAttachment.FindPath(AttactID);
                            if (strPath != "")
                            {
                                //删除附件文件
                                pm.DeleteFile(AttactID, strPath);
                                //在附件表中删除附件数据
                                BLLAttachment.Delete(AttactID);
                            }
                        }
                        //删除获奖信息
                        award.Delete(Convert.ToInt32(Grid_Award.DataKeys[selections[i]][0].ToString()));
                    }
                    Alert.ShowInTop("删除成功!");
                }
                else
                {
                    //录入人(非管理员)操作
                    for (int i = 0; i < selections.Count(); i++)
                    {

                        award.UpdateIsPass(Convert.ToInt32(Grid_Award.DataKeys[selections[i]][0]), false);
                        //BLLUser.FindByLoginName(Session["LoginName"].ToString()).UserName
                        operate.LoginName = Session["LoginName"].ToString();
                        operate.OperationTime = DateTime.Now;
                        operate.LoginIP = " ";
                        operate.OperationContent = "Award";
                        operate.OperationType = "删除";
                        operate.OperationDataID = Convert.ToInt32(Grid_Award.DataKeys[selections[i]][0]);
                        op.Insert(operate);
                        //BindData();
                        Alert.ShowInTop("操作已经提交，请等待管理员确认!");

                    }
                }
                Grid_Award.PageIndex = 0;
                Grid_Award.PageSize = 20;
                btnDelete.Enabled = false;
                InitData();
            }
            catch (Exception ex)
            {
                BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
                pm.SaveError(ex, this.Request);
                return;
            }
        }
    }
}