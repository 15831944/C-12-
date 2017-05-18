/**编写人：方淑云
 * 时间：2014年8月14号
 * 功能:成果应用查询界面后台
 * 修改履历：   修改人;高琪
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

namespace WDFramework.Acheievement.AchievementApply
{
    public partial class Search_AchievementApply : System.Web.UI.Page
    {
        BLHelper.BLLAchievement ach = new BLHelper.BLLAchievement();
        BLHelper.BLLAchievementApply applys = new BLHelper.BLLAchievementApply();
        BLHelper.BLLAttachment at = new BLHelper.BLLAttachment();
        BLHelper.BLLStaffAchieve blst = new BLHelper.BLLStaffAchieve();
        BLHelper.BLLUser user = new BLHelper.BLLUser();
        BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
        BLHelper.BLLAttachment BLLAttachment = new BLHelper.BLLAttachment();
        BLHelper.BLLOperationLog bllOperate = new BLHelper.BLLOperationLog();
       
        Common.Entities.OperationLog operate = new Common.Entities.OperationLog();
        BLHelper.BLLOperationLog op = new BLHelper.BLLOperationLog();

        private int page;
        //AchivementApply apply = new AchivementApply();
        protected void Page_Load(object sender, EventArgs e)
        {
            page = ViewState["page"] == null ? 0 : (int)ViewState["page"];
            btnSelect_All.Text = "全选";
            if (!IsPostBack)
            {
                InitData();
                btnAddAchieveAward.OnClientClick = Window_addAchieveApply.GetShowReference("Add_AchievementApply.aspx", "新增成果应用信息");
            }
        }
        //界面初始化
        public void InitData()
        {
            try
            {
                ViewState["page"] = 0;
                List<AchivementApply> list = applys.FindPage(Convert.ToInt32(Session["SecrecyLevel"]));
                Grid_AchieveApply.RecordCount = list.Count();
                if (list != null)
                {
                    Grid_AchieveApply.DataSource = list.Skip(Grid_AchieveApply.PageIndex * Grid_AchieveApply.PageSize).Take(Grid_AchieveApply.PageSize);
                    Grid_AchieveApply.DataBind();
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
            return ach.FindAchieveName(ah);
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
        protected void btnUpdateAchieveApply_Click(object sender, EventArgs e)
        {
            try
            {
                if (pm.GridCount(Grid_AchieveApply, CBoxSelect).Count() != 0)
                {
                    if (pm.GridCount(Grid_AchieveApply, CBoxSelect).Count() == 1)
                    {
                        int rowID = Convert.ToInt32(Grid_AchieveApply.DataKeys[pm.GridCount(Grid_AchieveApply, CBoxSelect)[0]][0]);
                        Session["AchievementApplyID"] = rowID;
                        Alert.Show("你确定要修改该行数据吗!", "确认消息", MessageBoxIcon.Information, Window_ReviseAchieveApply.GetShowReference("Update_AchievementApply.aspx", "修改成果应用信息"), Target.Top);
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
        //按AchievementApplyID进行删除
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                List<int> selections = pm.GridCount(Grid_AchieveApply, CBoxSelect);
                if (Convert.ToInt32(Session["SecrecyLevel"]) == 5)
                {
                    for (int i = 0; i < selections.Count(); i++)
                    {
                        applys.Delete(Convert.ToInt32(Grid_AchieveApply.DataKeys[selections[i]][0]));
                    }
                    InitData();
                    btnSelect_All.Text = "全选";
                    Alert.ShowInTop("删除数据成功!");
                }
                else
                {
                    for (int i = 0; i < selections.Count(); i++)
                    {
                        applys.UpdateIsPass(Convert.ToInt32(Grid_AchieveApply.DataKeys[selections[i]][0]), false);
                        //向操作日志表中插入
                        OperationLog operate = new OperationLog();
                        operate.LoginName = user.FindByLoginName(Session["LoginName"].ToString()).UserName;
                        operate.LoginIP = "";
                        operate.OperationType = "删除";
                        operate.OperationContent = "ProjectImportantNode";
                        operate.OperationDataID = Convert.ToInt32(Grid_AchieveApply.DataKeys[selections[i]][0]);
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
        //分页
        protected void ddlGridPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Grid_AchieveApply.PageIndex = 0;
                this.Grid_AchieveApply.PageSize = Convert.ToInt32(ddlGridPageSize.SelectedValue);
                switch (page)
                {
                    case 0:
                        InitData();
                        break;
                    case 1:
                        FindByAchievementNmae();
                        break;
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }


        protected void Grid_AchieveApply_PageIndexChange(object sender, FineUI.GridPageEventArgs e)
        {
            try
            {
                Grid_AchieveApply.PageIndex = e.NewPageIndex;
                switch (page)
                {
                    case 0:
                        InitData();
                        break;
                    case 1:
                        FindByAchievementNmae();
                        break;
                    case 2:
                        FindByPeople();
                        break;
                    case 3:
                        FindByTime();
                        break;
                    case 4:
                        FindByUnit();
                        break;
                    case 5:
                        FindBySecrecyLevel();
                        break;
                    case 6:
                        FindByMember();
                        break;
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
        //下载
        protected string GetEditUrl(object ID)
        {
            return DownLoad.GetShowReference("Operate.aspx?id=" + ID, "操作");
        }
        //显示成员
        protected string GetEditUrlm(object ID)
        {
            return Peoplef.GetShowReference("Memeber.aspx?id=" + ID, "成员信息");
        }
        //行点击事件
        protected void Grid_AchieveApply_RowCommand(object sender, FineUI.GridCommandEventArgs e)
        {
            try
            {
                string Person = Grid_AchieveApply.Rows[e.RowIndex].Values[2].ToString();
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
                int Pages = (int)Math.Floor(Convert.ToDouble(Grid_AchieveApply.RecordCount / this.Grid_AchieveApply.PageSize));

                if (Grid_AchieveApply.PageIndex == Pages)
                    m = (Grid_AchieveApply.RecordCount - this.Grid_AchieveApply.PageSize * Grid_AchieveApply.PageIndex);
                else
                    m = this.Grid_AchieveApply.PageSize;
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
        //按成果名称搜索
        public void FindByAchievementNmae()
        {
            try
            {
                ViewState["page"] = 1;
                List<int> achieve = ach.FindByAchievementNamelist(tCondition.Text.Trim());
                List<AchivementApply> list = applys.FindByAchievementName(achieve, Convert.ToInt32(Session["SecrecyLevel"]));
                Grid_AchieveApply.RecordCount = list.Count();
                if (list != null)
                {
                    Grid_AchieveApply.DataSource = list.Skip(Grid_AchieveApply.PageIndex * Grid_AchieveApply.PageSize).Take(Grid_AchieveApply.PageSize);
                    Grid_AchieveApply.DataBind();
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
        //按完成人查询
        public void FindByPeople()
        {
            try
            {
                ViewState["page"] = 2;
                List<int> UserInfoID = user.FindList(tCondition.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                List<int> AchieveID = blst.SelectIDlist(UserInfoID, Convert.ToInt32(Session["SecrecyLevel"]));
                List<Common.Entities.AchivementApply> list = applys.FindByAchievementName(AchieveID, Convert.ToInt32(Session["SecrecyLevel"]));
                Grid_AchieveApply.RecordCount = list.Count();
                if (list != null)
                {
                    Grid_AchieveApply.DataSource = list.Skip(Grid_AchieveApply.PageIndex * Grid_AchieveApply.PageSize).Take(Grid_AchieveApply.PageSize);
                    Grid_AchieveApply.DataBind();
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
        //按开始时间查询
        public void FindByTime()
        {
            try
            {
                ViewState["page"] = 3;
                List<AchivementApply> list = applys.FindByTime(Convert.ToInt32(dCondition.SelectedText.Trim()), Convert.ToInt32(Session["SecrecyLevel"]));
                Grid_AchieveApply.RecordCount = list.Count();
                if (list != null)
                {
                    Grid_AchieveApply.DataSource = list.Skip(Grid_AchieveApply.PageIndex * Grid_AchieveApply.PageSize).Take(Grid_AchieveApply.PageSize);
                    Grid_AchieveApply.DataBind();
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
        //转化等级
        public string ChangeSecrecyLevel(int level)
        {
            string[] SecrecyLevels = new string[] { "四级", "三级", "二级", "一级", "管理员" };
            return SecrecyLevels[level - 1];
        }

        //按应用单位查询
        public void FindByUnit()
        {
            try
            {
                ViewState["page"] = 4;
                Grid_AchieveApply.PageIndex = 0;
                List<AchivementApply> list = applys.FindByUnit(tCondition.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                Grid_AchieveApply.RecordCount = list.Count();
                if (list != null)
                {
                    Grid_AchieveApply.DataSource = list.Skip(Grid_AchieveApply.PageIndex * Grid_AchieveApply.PageSize).Take(Grid_AchieveApply.PageSize);
                    Grid_AchieveApply.DataBind();
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

        //将保密级别转换成int
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
        //根据保密级别查询
        public void FindBySecrecyLevel()
        {
            try
            {
                ViewState["page"] = 5;
                List<AchivementApply> list = applys.FindBySecrecyLevel(exchangesecrecylevel(dCondition.SelectedValue), Convert.ToInt32(Session["SecrecyLevel"]));
                Grid_AchieveApply.RecordCount = list.Count();
                if (list != null)
                {
                    Grid_AchieveApply.DataSource = list.Skip(Grid_AchieveApply.PageIndex * Grid_AchieveApply.PageSize).Take(Grid_AchieveApply.PageSize);
                    Grid_AchieveApply.DataBind();
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

        //根据成员查询
        public void FindByMember()
        {
            try
            {
                ViewState["page"] = 6;
                List<AchivementApply> list = applys.FindByMember(tCondition.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                Grid_AchieveApply.RecordCount = list.Count();
                if (list != null)
                {
                    Grid_AchieveApply.DataSource = list.Skip(Grid_AchieveApply.PageIndex * Grid_AchieveApply.PageSize).Take(Grid_AchieveApply.PageSize);
                    Grid_AchieveApply.DataBind();
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
                Grid_AchieveApply.PageIndex = 0;
                if (dChoose.SelectedText == "全部")
                {
                    InitData();
                }
                if (dChoose.SelectedText == "开始年份")
                {
                    FindByTime();
                }
                if (dChoose.SelectedText == "保密级别")
                {
                    FindBySecrecyLevel();
                }
                if (tCondition.Text != "")
                {
                    if (dChoose.SelectedText == "成果名称")
                    {
                        FindByAchievementNmae();
                    }
                    if (dChoose.SelectedText == "应用单位")
                    {
                        FindByUnit();
                    }
                    if (dChoose.SelectedText == "完成人")
                    {
                        FindByPeople();
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
        //选择框事件
        protected void dChoose_SelectedIndexChanged(object sender, EventArgs e)
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
                case "开始年份":
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
                case "完成人":
                    dCondition.Enabled = false;
                    tCondition.Enabled = true;
                    break;
                case "应用单位":
                    dCondition.Enabled = false;
                    tCondition.Enabled = true;
                    break;
                case "保密级别":
                    dCondition.Items.Clear();
                    string[] secrecylevel = new string[5] { "四级", "三级", "二级", "一级", "管理员" };
                    for (int i = 0; i < 5; i++)
                    {
                        dCondition.Items.Add(secrecylevel[i], secrecylevel[i]);
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
        //导出
        protected void btn_Get_Click(object sender, EventArgs e)
        {
            try
            {
                if (page == 0)
                {
                    List<AchivementApply> list = applys.FindPage(Convert.ToInt32(Session["SecrecyLevel"]));
                    Grid_AchieveApply.RecordCount = list.Count();
                    if (list != null)
                    {
                        Grid_AchieveApply.DataSource = list;
                        Grid_AchieveApply.DataBind();
                    }
                }
                if (page == 1)
                {
                    List<int> AchievementID = ach.FindByAchievementNamelist(tCondition.Text);
                    List<AchivementApply> list = applys.FindByAchievementName(AchievementID, Convert.ToInt32(Session["SecrecyLevel"]));
                    if (list != null)
                    {
                        Grid_AchieveApply.DataSource = list;
                        Grid_AchieveApply.DataBind();
                    }
                }
                if (page == 2)
                {
                    List<int> UserInfoID = user.FindList(tCondition.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                    List<int> AchieveID = blst.SelectIDlist(UserInfoID, Convert.ToInt32(Session["SecrecyLevel"]));
                    List<Common.Entities.AchivementApply> list = applys.FindByAchievementName(AchieveID, Convert.ToInt32(Session["SecrecyLevel"]));
                    if (list != null)
                    {
                        Grid_AchieveApply.DataSource = list;
                        Grid_AchieveApply.DataBind();
                    }
                }
                if (page == 3)
                {
                    List<AchivementApply> list = applys.FindByTime(Convert.ToInt32(dCondition.SelectedText.Trim()), Convert.ToInt32(Session["SecrecyLevel"]));
                    if (list != null)
                    {
                        Grid_AchieveApply.DataSource = list;
                        Grid_AchieveApply.DataBind();
                    }
                }
                if (page == 4)
                {
                    List<AchivementApply> list = applys.FindByUnit(tCondition.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                    if (list != null)
                    {
                        Grid_AchieveApply.DataSource = list;
                        Grid_AchieveApply.DataBind();
                    }
                }
                if (page == 5)
                {
                    List<AchivementApply> list = applys.FindBySecrecyLevel(exchangesecrecylevel(dCondition.SelectedValue), Convert.ToInt32(Session["SecrecyLevel"]));
                    if (list != null)
                    {
                        Grid_AchieveApply.DataSource = list;
                        Grid_AchieveApply.DataBind();
                    }
                }
                if (page == 6)
                {
                    List<AchivementApply> list = applys.FindByMember(tCondition.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                    if (list != null)
                    {
                        Grid_AchieveApply.DataSource = list;
                        Grid_AchieveApply.DataBind();
                    }
                }
                pm.ExportExcel(3, Grid_AchieveApply, 1);
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);

            }
        }
        //全选按钮
        protected void btnSelect_All_Click(object sender, EventArgs e)
        {
            Grid_AchieveApply.SelectAllRows();
            int[] select = Grid_AchieveApply.SelectedRowIndexArray;
            int m;
            //取整数（不是四舍五入，全舍）
            int Pages = (int)Math.Floor(Convert.ToDouble(Grid_AchieveApply.RecordCount / this.Grid_AchieveApply.PageSize));

            if (Grid_AchieveApply.PageIndex == Pages)
                m = (Grid_AchieveApply.RecordCount - this.Grid_AchieveApply.PageSize * Grid_AchieveApply.PageIndex);
            else
                m = this.Grid_AchieveApply.PageSize;
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