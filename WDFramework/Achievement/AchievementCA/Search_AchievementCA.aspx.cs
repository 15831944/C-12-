/**编写人：方淑云
 * 时间：2014年8月14号
 * 功能:项目验收查询界面后台
 * 修改履历：   修改人;高琪
 *              修改时间;20151010
 *              内容：撤销page静态变量
 **/
using Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common.Entities;
using FineUI;

namespace WebApplication1.Achievement.AchievementCA
{
    public partial class Search_AchievementCA : System.Web.UI.Page
    {
        BLHelper.BLLAchievement ach = new BLHelper.BLLAchievement();
        BLHelper.BLLAchievementCA ca = new BLHelper.BLLAchievementCA();
        BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
        BLHelper.BLLUser user = new BLHelper.BLLUser();
        BLHelper.BLLStaffAchieve blst = new BLHelper.BLLStaffAchieve();
        BLHelper.BLLOperationLog bllOperate = new BLHelper.BLLOperationLog();
        private int page;
        protected void Page_Load(object sender, EventArgs e)
        {
            page = ViewState["page"] == null ? 0 : (int)ViewState["page"];
            if (!IsPostBack)
            {
                InitData();
                btn_AddAchievementCA.OnClientClick = Window_addAchievementCA.GetShowReference("Add_AchievementCA.aspx", "新增项目验收信息");
                //reprot1.OnClientClick = WindowReport.GetShowReference("~/Report/R_Agency_AchievementCA.aspx", "分部门按验收部门、验收时间、验收评语级别统计项目验收情况");
            }
        }
        //下载
        protected string GetEditUrl(object ID)
        {
            return DownLoad.GetShowReference("Operate.aspx?id=" + ID, "操作");
        }
        //成员
        protected string GetEditUrlm(object ID)
        {
            return Member.GetShowReference("Member.aspx?id=" + ID, "成员");
        }
        //将项目ID转化为项目名称
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
        //保密级别转化
        public string ChangeSecrecyLevel(int level)
        {
            string[] SecrecyLevels = new string[] { "四级", "三级", "二级", "一级", "管理员" };
            return SecrecyLevels[level - 1];
        }
        //初始化
        public void InitData()
        {
            try
            {
                ViewState["page"] = 0;
                List<Common.Entities.AchievementCA> list = ca.FindPaged(Convert.ToInt32(Session["SecrecyLevel"]));
                Grid_AchievementCA.RecordCount = list.Count();
                if (list != null)
                {
                    Grid_AchievementCA.DataSource = list.Skip(Grid_AchievementCA.PageIndex * Grid_AchievementCA.PageSize).Take(Grid_AchievementCA.PageSize);
                    Grid_AchievementCA.DataBind();
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

        //删除
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                List<int> selections = pm.GridCount(Grid_AchievementCA, CBoxSelect);
                if (Convert.ToInt32(Session["SecrecyLevel"]) == 5)
                {
                    for (int i = 0; i < selections.Count(); i++)
                    {
                        ca.Delete(Convert.ToInt32(Grid_AchievementCA.DataKeys[selections[i]][0]));
                    }
                    InitData();
                    Alert.ShowInTop("删除数据成功!");
                }
                else
                {
                    for (int i = 0; i < selections.Count(); i++)
                    {
                        ca.UpdateIsPass(Convert.ToInt32(Grid_AchievementCA.DataKeys[selections[i]][0]), false);
                        //向操作日志表中插入
                        OperationLog operate = new OperationLog();
                        operate.LoginName = user.FindByLoginName(Session["LoginName"].ToString()).UserName;
                        operate.LoginIP = "";
                        operate.OperationType = "删除";
                        operate.OperationContent = "ProjectImportantNode";
                        operate.OperationDataID = Convert.ToInt32(Grid_AchievementCA.DataKeys[selections[i]][0]);
                        operate.OperationTime = System.DateTime.Now;
                        operate.Remark = "";
                        bllOperate.Insert(operate);
                    }
                    Alert.ShowInTop("您的操作已提交，请等待审核！");
                    InitData();
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
   
        //更新
        protected void btn_UpdateAchievementCA_Click(object sender, EventArgs e)
        {
            try
            {
                if (pm.GridCount(Grid_AchievementCA, CBoxSelect).Count() != 0)
                {
                    if (pm.GridCount(Grid_AchievementCA, CBoxSelect).Count() == 1)
                    {
                        int rowID = Convert.ToInt32(Grid_AchievementCA.DataKeys[pm.GridCount(Grid_AchievementCA, CBoxSelect)[0]][0]);
                        Session["AchievementCAID"] = rowID;
                        Alert.Show("你确定要修改该行数据吗!", "确认消息", MessageBoxIcon.Information, Window_UpdateAchievementCA.GetShowReference("Update_AchievementCA.aspx", "修改项目验收信息"), Target.Top);
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
        //按名称搜索
        public void FindByName()
        {
            try
            {
                ViewState["page"] = 1;
                List<int> achieve = ach.FindByAchievementNamelist(tCondition.Text.Trim());
                List<Common.Entities.AchievementCA> list = ca.FindByAchievementName(achieve, Convert.ToInt32(Session["SecrecyLevel"])); Convert.ToInt32(Session["SecrecyLevel"]);
                Grid_AchievementCA.RecordCount = list.Count();
                if (list != null)
                {
                    Grid_AchievementCA.DataSource = list.Skip(Grid_AchievementCA.PageIndex * Grid_AchievementCA.PageSize).Take(Grid_AchievementCA.PageSize);
                    Grid_AchievementCA.DataBind();
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
        //按验收时间搜索
        public void FindByTime()
        {
            try
            {
                ViewState["page"] = 2;
                List<Common.Entities.AchievementCA> list = ca.FindByCATime(Convert.ToInt32(dCondition.SelectedText.Trim()), Convert.ToInt32(Session["SecrecyLevel"]));
                Grid_AchievementCA.RecordCount = list.Count();
                if (list != null)
                {
                    Grid_AchievementCA.DataSource = list.Skip(Grid_AchievementCA.PageIndex * Grid_AchievementCA.PageSize).Take(Grid_AchievementCA.PageSize);
                    Grid_AchievementCA.DataBind();
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
        //按验收组织部门搜索
        public void FindByCAUnit()
        {
            try
            {
                ViewState["page"] = 3;
                List<Common.Entities.AchievementCA> list = ca.FindByCAUnit(tCondition.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                Grid_AchievementCA.RecordCount = list.Count();
                if (list != null)
                {
                    Grid_AchievementCA.DataSource = list.Skip(Grid_AchievementCA.PageIndex * Grid_AchievementCA.PageSize).Take(Grid_AchievementCA.PageSize);
                    Grid_AchievementCA.DataBind();
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
        //按完成人搜索
        public void FindByPeople()
        {
            try
            {
                ViewState["page"] = 4;
                List<int> UserInfoID = user.FindList(tCondition.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                List<int> AchieveID = blst.SelectIDlist(UserInfoID, Convert.ToInt32(Session["SecrecyLevel"]));
                List<Common.Entities.AchievementCA> list = ca.FindByAchievementName(AchieveID, Convert.ToInt32(Session["SecrecyLevel"]));
                Grid_AchievementCA.RecordCount = list.Count();
                if (list != null)
                {
                    Grid_AchievementCA.DataSource = list.Skip(Grid_AchievementCA.PageIndex * Grid_AchievementCA.PageSize).Take(Grid_AchievementCA.PageSize);
                    Grid_AchievementCA.DataBind();
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
        //按验收评语级别
        public void FindByCACommnetLevel()
        {
            try
            {
                //保存当前的搜索状态
                ViewState["page"] = 5;
                //获取到查询结果
                List<Common.Entities.AchievementCA> list = ca.FindByCACommnetLevel(tCondition.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                //结果数量传给前台
                Grid_AchievementCA.RecordCount = list.Count();
                if (list != null)
                {
                    Grid_AchievementCA.DataSource = list.Skip(Grid_AchievementCA.PageIndex * Grid_AchievementCA.PageSize).Take(Grid_AchievementCA.PageSize);
                    Grid_AchievementCA.DataBind();
                }
            }   
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
        //按保密级别搜索
        public void FindBySecret()
        {
            try
            {
                ViewState["page"] = 6;
                List<Common.Entities.AchievementCA> list = ca.FindByCACommnetLevel(tCondition.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                Grid_AchievementCA.RecordCount = list.Count();
                if (list != null)
                {
                    Grid_AchievementCA.DataSource = list.Skip(Grid_AchievementCA.PageIndex * Grid_AchievementCA.PageSize).Take(Grid_AchievementCA.PageSize);
                    Grid_AchievementCA.DataBind();
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
        //按成员搜索
        public void FindByMember()
        {
            try
            {
                ViewState["page"] = 7;
                List<Common.Entities.AchievementCA> list = ca.FindByMember(tCondition.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                Grid_AchievementCA.RecordCount = list.Count();
                if (list != null)
                {
                    Grid_AchievementCA.DataSource = list.Skip(Grid_AchievementCA.PageIndex * Grid_AchievementCA.PageSize).Take(Grid_AchievementCA.PageSize);
                    Grid_AchievementCA.DataBind();
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
                Grid_AchievementCA.PageIndex = 0;
                if (dChoose.SelectedText == "全部")
                {
                    InitData();
                }
                if (dChoose.SelectedText == "验收年份")
                {
                    FindByTime();
                }
                if (dChoose.SelectedText == "保密级别")
                {
                    FindBySecret();
                }
                if (tCondition.Text.Trim() != "")
                {
                    if (dChoose.SelectedText == "项目名称")
                    {
                        FindByName();
                    }
                    if (dChoose.SelectedText == "验收组织部门")
                    {
                        FindByCAUnit();
                    }
                    if (dChoose.SelectedText == "完成人")
                    {
                        FindByPeople();
                    }
                    if (dChoose.SelectedText == "验收评语级别")
                    {
                        FindByCACommnetLevel();
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
        //行点击事件
        protected void Grid_AchievementCA_RowCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                string Person = Grid_AchievementCA.Rows[e.RowIndex].Values[2].ToString();
                string username = user.FindByLoginName(Session["LoginName"].ToString()).UserName;

                if (Person != username && Convert.ToInt32(Session["SecrecyLevel"]) != 5)
                {
                    string str = "您无对此行操作的权限！此行信息为" + Person + "录入，请与管理员联系!";
                    CBoxSelect.SetCheckedState(e.RowIndex, false);
                    Alert.ShowInTop(str);
                    return;
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }

        protected void Grid_AchievementCA_PageIndexChange(object sender, GridPageEventArgs e)
        {
            Grid_AchievementCA.PageIndex = e.NewPageIndex;
            switch (page)
            {
                case 0:
                    InitData();
                    break;
                case 1:
                     FindByName();
                    break;
                case 2:
                     FindByTime();
                     break;
                case 3:
                     FindByCAUnit();
                     break;
                case 4:
                     FindByPeople();
                     break;
                case 5:
                     FindByCACommnetLevel();
                     break;
                case 6:
                     FindBySecret();
                     break;
                case 7:
                     FindByMember();
                     break;
            }
        }

        protected void ddlGridPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid_AchievementCA.PageIndex = 0;
            this.Grid_AchievementCA.PageSize = Convert.ToInt32(ddlGridPageSize.SelectedValue);
            switch (page)
            {
                case 0:
                    InitData();
                    break;
                case 1:
                    FindByName();
                    break;
                case 2:
                    FindByTime();
                    break;
                case 3:
                    FindByCAUnit();
                    break;
                case 4:
                    FindByPeople();
                    break;
                case 5:
                    FindByCACommnetLevel();
                    break;
                case 6:
                    FindBySecret();
                    break;
                case 7:
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
                    case "项目名称":
                        dCondition.Enabled = false;
                        tCondition.Enabled = true;
                        break;
                    case "验收年份":
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
                    case "验收组织部门":
                        dCondition.Enabled = false;
                        tCondition.Enabled = true;
                        break;
                    case "验收评语级别":
                        dCondition.Enabled = false;
                        tCondition.Enabled = true;
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
                    List<Common.Entities.AchievementCA> list = ca.FindPaged(Convert.ToInt32(Session["SecrecyLevel"]));
                    if (list != null)
                    {
                        Grid_AchievementCA.DataSource = list;
                        Grid_AchievementCA.DataBind();
                    }
                }
                if (page == 1)
                {
                    List<int> achieve = ach.FindByAchievementNamelist(tCondition.Text.Trim());
                    List<Common.Entities.AchievementCA> list = ca.FindByAchievementName(achieve, Convert.ToInt32(Session["SecrecyLevel"])); Convert.ToInt32(Session["SecrecyLevel"]);
                    if (list != null)
                    {
                        Grid_AchievementCA.DataSource = list;
                        Grid_AchievementCA.DataBind();
                    }
                }
                if (page == 2)
                {
                    List<Common.Entities.AchievementCA> list = ca.FindByCATime(Convert.ToInt32(dCondition.SelectedText.Trim()), Convert.ToInt32(Session["SecrecyLevel"]));
                    if (list != null)
                    {
                        Grid_AchievementCA.DataSource = list;
                        Grid_AchievementCA.DataBind();
                    }
                }
                if (page == 3)
                {
                    List<Common.Entities.AchievementCA> list = ca.FindByCAUnit(tCondition.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                    if (list != null)
                    {
                        Grid_AchievementCA.DataSource = list;
                        Grid_AchievementCA.DataBind();
                    }
                }
                if (page == 4)
                {
                    List<int> UserInfoID = user.FindList(tCondition.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                    List<int> AchieveID = blst.SelectIDlist(UserInfoID, Convert.ToInt32(Session["SecrecyLevel"]));
                    List<Common.Entities.AchievementCA> list = ca.FindByAchievementName(AchieveID, Convert.ToInt32(Session["SecrecyLevel"]));
                    if (list != null)
                    {
                        Grid_AchievementCA.DataSource = list;
                        Grid_AchievementCA.DataBind();
                    }
                }
                if (page == 5)
                {
                    List<Common.Entities.AchievementCA> list = ca.FindByCACommnetLevel(tCondition.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                    if (list != null)
                    {
                        Grid_AchievementCA.DataSource = list;
                        Grid_AchievementCA.DataBind();
                    }
                }
                if (page == 6)
                {
                    List<Common.Entities.AchievementCA> list = ca.FindPaged(Convert.ToInt32(Session["SecrecyLevel"]));
                    if (list != null)
                    {
                        Grid_AchievementCA.DataSource = list;
                        Grid_AchievementCA.DataBind();
                    }
                }
                if (page == 7)
                {
                    List<Common.Entities.AchievementCA> list = ca.FindByMember(tCondition.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                    if (list != null)
                    {
                        Grid_AchievementCA.DataSource = list;
                        Grid_AchievementCA.DataBind();
                    }
                }
                pm.ExportExcel(3, Grid_AchievementCA, 1);
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
        //grid序号
       
        public int RowNumber(int dataItemIndex)
        {
            return dataItemIndex + (Grid_AchievementCA.PageIndex) * Grid_AchievementCA.PageSize;
        }

        //全选按钮
        protected void btnSelect_All_Click(object sender, EventArgs e)
        {
            Grid_AchievementCA.SelectAllRows();
            int[] select = Grid_AchievementCA.SelectedRowIndexArray;
            int m;
            //取整数（不是四舍五入，全舍）
            int Pages = (int)Math.Floor(Convert.ToDouble(Grid_AchievementCA.RecordCount / this.Grid_AchievementCA.PageSize));

            if (Grid_AchievementCA.PageIndex == Pages)
                m = (Grid_AchievementCA.RecordCount - this.Grid_AchievementCA.PageSize * Grid_AchievementCA.PageIndex);
            else
                m = this.Grid_AchievementCA.PageSize;
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