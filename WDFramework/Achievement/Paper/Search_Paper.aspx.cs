/**编写人：方淑云
 * 时间：2014年8月12号
 * 功能：论文查询界面的相关操作
 * 修改履历：1.时间：8月19号
 *            修改人：方淑云
 *            修改内容：去掉删除功能
 *            2.修改人;高琪
 *           修改时间;20151010
 *           内容：撤销page静态变量
 **/
using Common.Entities;
using FineUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class Search_Paper : System.Web.UI.Page
    {
        BLHelper.BLLPaper paper = new BLHelper.BLLPaper();
        BLHelper.BLLUser user = new BLHelper.BLLUser();
        //BLHelper.BLLStaffPaper sp = new BLHelper.BLLStaffPaper();
        BLHelper.BLLBasicCode ba = new BLHelper.BLLBasicCode();
        BLHelper.BLLAchievement ach = new BLHelper.BLLAchievement();
        BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
        private int page;
        protected void Page_Load(object sender, EventArgs e)
        {
            page = ViewState["page"] == null ? 0 : (int)ViewState["page"];
            if (!IsPostBack)
            {
                InitData();
                btn_AddPaper.OnClientClick = Window_addPaper.GetShowReference("Add_Paper.aspx", "新增论文信息");
                //reprot1.OnClientClick = WindowReport.GetShowReference("~/Report/R_Agency_Paper.aspx", "分部门按题目统计论文情况");
            }
        }
        //初始化
        public void InitData()
        {
            try
            {
                ViewState["page"] = 0;
                List<Paper> list = paper.FindPaged(Convert.ToInt32(Session["SecrecyLevel"]));
                Grid_Paper.RecordCount = list.Count;
                if (list != null)
                {
                    Grid_Paper.DataSource = list.Skip(Grid_Paper.PageIndex * Grid_Paper.PageSize).Take(Grid_Paper.PageSize);
                    Grid_Paper.DataBind();
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
            dChoose.Reset();
            dCondition.Reset();
            tCondition.Reset();
            dCondition.Enabled = false;
            tCondition.Enabled = false;
            InitData();
            btn_Delete.Enabled = false;
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
     
        //更新
        protected void btn_UpdatePaper_Click(object sender, EventArgs e)
        {
            try
            {
                if (pm.GridCount(Grid_Paper, CBoxSelect).Count() != 0)
                {
                    if (pm.GridCount(Grid_Paper, CBoxSelect).Count() == 1)
                    {
                        int rowID = Convert.ToInt32(Grid_Paper.DataKeys[pm.GridCount(Grid_Paper, CBoxSelect)[0]][0]);
                        Session["PaperID"] = rowID;
                        Alert.Show("你确定要修改该行数据吗?", "确认消息", MessageBoxIcon.Information, Window_UpdatePaper.GetShowReference("Update_Paper.aspx", "修改论文信息"), Target.Top);
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
        protected void Grid_Paper_PageIndexChange(object sender, GridPageEventArgs e)
        {
            Grid_Paper.PageIndex = e.NewPageIndex;
            switch (page)
            {
                case 0:
                    InitData();
                    break;
                case 1:
                    FindByAgency();
                    break;
                case 2:
                    FindByPaperRank();
                    break;
               
                case 3:
                    FindByPublicDate();
                    break;
                case 4:
                    FindByWriter();
                    break;
                case 5:
                    FindByRS();
                    break;
                case 6:
                    FindByFirstWriter();
                    break;
                case 7:
                    FindByMessageWriter();
                    break;
            }

        }

        protected void ddlGridPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid_Paper.PageIndex = 0;
            this.Grid_Paper.PageSize = Convert.ToInt32(ddlGridPageSize.SelectedValue);
            switch (page)
            {
                case 0:
                    InitData();
                    break;
                case 1:
                    FindByAgency();
                    break;
                case 2:
                    FindByPaperRank();
                    break;
               
                case 3:
                    FindByPublicDate();
                    break;
                case 4:
                    FindByWriter();
                    break;
                case 5:
                    FindByRS();
                    break;
                case 6:
                    FindByFirstWriter();
                    break;
                case 7:
                    FindByMessageWriter();
                    break;

            }
        }
        //行点击事件
        protected void Grid_Paper_RowCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                string Person = Grid_Paper.Rows[e.RowIndex].Values[2].ToString();
                string username = user.FindByLoginName(Session["LoginName"].ToString()).UserName;

                if (Person != username && Convert.ToInt32(Session["SecrecyLevel"]) != 5)
                {
                    string str = "您无对此行操作的权限！此行信息为" + Person + "录入，请与管理员联系!";
                    CBoxSelect.SetCheckedState(e.RowIndex, false);
                    Alert.ShowInTop(str);
                }
                int m;
                //取整数（不是四舍五入，全舍）
                int Pages = (int)Math.Floor(Convert.ToDouble(Grid_Paper.RecordCount / this.Grid_Paper.PageSize));

                if (Grid_Paper.PageIndex == Pages)
                    m = (Grid_Paper.RecordCount - this.Grid_Paper.PageSize * Grid_Paper.PageIndex);
                else
                    m = this.Grid_Paper.PageSize;
                bool isCheck = false;
                for (int i = 0; i < m; i++)
                {
                    if (CBoxSelect.GetCheckedState(i))
                        isCheck = true;
                }
                if (isCheck)
                    btn_Delete.Enabled = true;
                else
                    btn_Delete.Enabled = false;
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }

        //详情界面跳转
        protected string GetEditUrl(object PaperID)
        {
            return Details.GetShowReference("Detail.aspx?id=" + PaperID, "备注");
        }
        //作者界面跳转
        protected string GetEditUrlw(object PaperID)
        {
            return Writer.GetShowReference("Writer.aspx?id=" + PaperID, "作者信息");
        }
        //收录号界面跳转
        protected string GetEditUrlr(object PaperID)
        {
            return Writer.GetShowReference("Record.aspx?id=" + PaperID, "收录号");
        }
        //lby 相关文档界面跳转
        protected string GetEditUrlf(object PaperID)
        {
            return Writer.GetShowReference("File.aspx?id=" + PaperID, "收录号");
        }
      
        //按部门查询
        public void FindByAgency()
        {
            try
            {
                ViewState["page"] = 1;
                List<Paper> list = paper.FindByAgency(dCondition.SelectedText.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                Grid_Paper.RecordCount = list.Count;
                if (list != null)
                {
                    Grid_Paper.DataSource = list.Skip(Grid_Paper.PageIndex * Grid_Paper.PageSize).Take(Grid_Paper.PageSize);
                    Grid_Paper.DataBind();
                }
                else
                {
                    Grid_Paper.DataSource = null;
                    Grid_Paper.DataBind();
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
        //按刊物级别查询
        public void FindByPaperRank()
        {
            try
            {
                ViewState["page"] = 2;
                List<Paper> list = paper.FindByPaperRank(dCondition.SelectedText.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                Grid_Paper.RecordCount = list.Count;
                if (list != null)
                {
                    Grid_Paper.DataSource = list.Skip(Grid_Paper.PageIndex * Grid_Paper.PageSize).Take(Grid_Paper.PageSize);
                    Grid_Paper.DataBind();
                }
                else
                {
                    Grid_Paper.DataSource = null;
                    Grid_Paper.DataBind();
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
        
        //按时间查询
        public void FindByPublicDate()
        {
            try
            {
                ViewState["page"] = 3;
                List<Paper> list = paper.FindByPublicDate(Convert.ToInt32(dCondition.SelectedText.Trim()), Convert.ToInt32(Session["SecrecyLevel"]));
                Grid_Paper.RecordCount = list.Count;
                if (list != null)
                {
                    Grid_Paper.DataSource = list.Skip(Grid_Paper.PageIndex * Grid_Paper.PageSize).Take(Grid_Paper.PageSize);
                    Grid_Paper.DataBind();
                }
                else
                {
                    Grid_Paper.DataSource = null;
                    Grid_Paper.DataBind();
                }

            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
        //按作者查询
        public void FindByWriter()
        {
            try
            {
                ViewState["page"] = 4;
                List<Paper> list = paper.FindByPaperPeople(tCondition.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                Grid_Paper.RecordCount = list.Count;
                if (list != null)
                {
                    Grid_Paper.DataSource = list.Skip(Grid_Paper.PageIndex * Grid_Paper.PageSize).Take(Grid_Paper.PageSize);
                    Grid_Paper.DataBind();
                }
                else
                {
                    Grid_Paper.DataSource = null;
                    Grid_Paper.DataBind();
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
        //按收录情况查询
        public void FindByRS()
        {
            try
            {
                ViewState["page"] = 5;
                List<Paper> list = paper.FindByRS(dCondition.SelectedText.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                Grid_Paper.RecordCount = list.Count;
                if (list != null)
                {
                    Grid_Paper.DataSource = list.Skip(Grid_Paper.PageIndex * Grid_Paper.PageSize).Take(Grid_Paper.PageSize);
                    Grid_Paper.DataBind();
                }
                else
                {
                    Grid_Paper.DataSource = null;
                    Grid_Paper.DataBind();
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
       
        //按第一作者查询
        public void FindByFirstWriter()
        {
            try
            {
                ViewState["page"] = 6;
                List<Paper> list = paper.FindByFirstWriter(dCondition.SelectedText.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                Grid_Paper.RecordCount = list.Count;
                if (list != null)
                {
                    Grid_Paper.DataSource = list.Skip(Grid_Paper.PageIndex * Grid_Paper.PageSize).Take(Grid_Paper.PageSize);
                    Grid_Paper.DataBind();
                }
                else
                {
                    Grid_Paper.DataSource = null;
                    Grid_Paper.DataBind();
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
        
        //按通讯作者查询
        public void FindByMessageWriter()
        {
            try
            {
                ViewState["page"] = 7;
                List<Paper> list = paper.FindByMessageWriter(dCondition.SelectedText.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                Grid_Paper.RecordCount = list.Count;
                if (list != null)
                {
                    Grid_Paper.DataSource = list.Skip(Grid_Paper.PageIndex * Grid_Paper.PageSize).Take(Grid_Paper.PageSize);
                    Grid_Paper.DataBind();
                }
                else
                {
                    Grid_Paper.DataSource = null;
                    Grid_Paper.DataBind();
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
        //按第一作者身份查询
        public void FindByFirstWriterPosition()
        {
            try
            {
                ViewState["page"] = 8;
                List<Paper> list = paper.FindByFirstWriterPosition(dCondition.SelectedText.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                Grid_Paper.RecordCount = list.Count;
                if (list != null)
                {
                    Grid_Paper.DataSource = list.Skip(Grid_Paper.PageIndex * Grid_Paper.PageSize).Take(Grid_Paper.PageSize);
                    Grid_Paper.DataBind();
                }
                else
                {
                    Grid_Paper.DataSource = null;
                    Grid_Paper.DataBind();
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
        //按发表状态查询
        public void FindByPublishState()
        {
            try
            {
                ViewState["page"] = 8;
                List<Paper> list = paper.FindByPublishState(dCondition.SelectedText.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                Grid_Paper.RecordCount = list.Count;
                if (list != null)
                {
                    Grid_Paper.DataSource = list.Skip(Grid_Paper.PageIndex * Grid_Paper.PageSize).Take(Grid_Paper.PageSize);
                    Grid_Paper.DataBind();
                }
                else
                {
                    Grid_Paper.DataSource = null;
                    Grid_Paper.DataBind();
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
                Grid_Paper.PageIndex = 0;
                if (dChoose.SelectedText == "全部")
                {
                    InitData();
                }
                if (dChoose.SelectedText == "部门")
                {
                    FindByAgency();
                    return;
                }
                if (dChoose.SelectedText == "收录情况")
                {
                    FindByRS();
                    return;
                }
                if (dChoose.SelectedText == "发表年份")
                {
                    FindByPublicDate();
                    return;
                }
                if (dChoose.SelectedText == "刊物级别")
                {
                    FindByPaperRank();
                    return;
                }
                if (tCondition.Text.Trim() != "")
                {

                    if (dChoose.SelectedText == "作者")
                    {
                        FindByWriter();
                        return;
                    }
                    if (dChoose.SelectedText == "第一作者")
                    {
                        FindByFirstWriter();
                        return;
                    }
                    if (dChoose.SelectedText == "通讯作者")
                    {
                        FindByMessageWriter();
                        return;
                    }
                }
               if (dChoose.SelectedText == "第一作者身份")
               {
                   FindByFirstWriterPosition();
                   return;
               }
               if (dChoose.SelectedText == "发表状态")
               {
                   FindByPublishState();
                   return;
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
        //保密级别转化
        public string ChangeSecrecyLevel(int level)
        {
            string[] SecrecyLevels = new string[] { "四级", "三级", "二级", "一级", "管理员" };
            return SecrecyLevels[level - 1];

        }
        //搜索框变化
        protected void dChoose_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (dChoose.SelectedValue)
            {
                case "全部":
                    dCondition.Enabled = false;
                    tCondition.Enabled = false;
                    break;
                case "部门":
                    dCondition.Items.Clear();
                    BLHelper.BLLAgency agency = new BLHelper.BLLAgency();
                    List<Common.Entities.Agency> list = agency.FindAllAgencyName();
                    for (int i = 0; i < list.Count(); i++)
                    {
                        dCondition.Items.Add(list[i].AgencyName.ToString(), list[i].AgencyName.ToString());
                    }
                    dCondition.Items[0].Selected = true;
                    dCondition.EnableEdit = false;
                    tCondition.Enabled = false;
                    dCondition.Enabled = true;
                    break;
                case "发表年份":
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
                case "第一作者身份":
                    dCondition.Items.Clear();
                    List<BasicCode> list1 = ba.FindByCategoryName("第一作者身份");
                    //List<Common.Entities.Paper> list1 = paper.FindByFirstWriterPosition();
                    for (int i = 0; i < list1.Count(); i++)
                    {
                        dCondition.Items.Add(list1[i].CategoryContent.ToString(), list1[i].CategoryContent.ToString());
                    }
                    dCondition.Items[0].Selected = true;
                    dCondition.EnableEdit = false;
                    tCondition.Enabled = false;
                    dCondition.Enabled = true;
                    break;
                case"发表状态":
                    dCondition.Items.Clear();
                    List<BasicCode> list2 = ba.FindByCategoryName("发表状态");
                    for (int i = 0; i < list2.Count(); i++)
                    {
                        dCondition.Items.Add(list2[i].CategoryContent.ToString(), list2[i].CategoryContent.ToString());
                    }
                    dCondition.Items[0].Selected = true;
                    dCondition.EnableEdit = false;
                    tCondition.Enabled = false;
                    dCondition.Enabled = true;
                    break;
                case "作者":
                    dCondition.Enabled = false;
                    tCondition.Enabled = true;
                    break;
                case "刊物级别":
                   dCondition.Items.Clear();
                    List<BasicCode> listname = ba.FindByCategoryName("刊物级别");
                    for (int i = 0; i < listname.Count(); i++)
                    {
                        dCondition.Items.Add(listname[i].CategoryContent.ToString(), listname[i].CategoryContent.ToString());
                    }
                    dCondition.Items[0].Selected = true;
                    dCondition.EnableEdit = false;
                    dCondition.Enabled = true;
                    tCondition.Enabled = false;
                    break;
                case "收录情况":
                    dCondition.Items.Clear();
                    List<BasicCode> listname1 = ba.FindByCategoryName("收录情况");
                    for (int i = 0; i < listname1.Count(); i++)
                    {
                        dCondition.Items.Add(listname1[i].CategoryContent.ToString(), listname1[i].CategoryContent.ToString());
                    }
                    dCondition.Items[0].Selected = true;
                    dCondition.EnableEdit = false;
                    dCondition.Enabled = true;
                    tCondition.Enabled = false;
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
                    List<Paper> list = paper.FindPaged(Convert.ToInt32(Session["SecrecyLevel"]));
                    if (list != null)
                    {
                        Grid_Paper.DataSource = list;
                        Grid_Paper.DataBind();
                    }
                }
                if (page == 1)
                {
                    List<Paper> list = paper.FindByAgency(dCondition.SelectedText.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                    if (list != null)
                    {
                        Grid_Paper.DataSource = list;
                        Grid_Paper.DataBind();
                    }
                }
             
                if (page == 3)
                {
                    List<Paper> list = paper.FindByPublicDate(Convert.ToInt32(dCondition.SelectedText.Trim()), Convert.ToInt32(Session["SecrecyLevel"]));
                    if (list != null)
                    {
                        Grid_Paper.DataSource = list;
                        Grid_Paper.DataBind();
                    }
                }
                if (page == 4)
                {
                    List<Paper> list = paper.FindByPaperPeople(tCondition.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                    if (list != null)
                    {
                        Grid_Paper.DataSource = list;
                        Grid_Paper.DataBind();
                    }
                }
                if (page == 5)
                {
                    List<Paper> list = paper.FindByRS(dCondition.SelectedText.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                    if (list != null)
                    {
                        Grid_Paper.DataSource = list;
                        Grid_Paper.DataBind();
                    }
                }
                pm.ExportExcel(3, Grid_Paper, 3);
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
        //获取作者
        public string getwriter(int writerid)
        {
            try
            {
                string str = paper.FindWriter(writerid);
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
        //获取收录号
        public string getincludenum(int id)
        {
            try
            {
                string str = paper.FindIncludeNum(id);
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
        //grid序号
        public int RowNumber(int dataItemIndex)
        {
            return dataItemIndex + (Grid_Paper.PageIndex) * Grid_Paper.PageSize;
        }

        //删除
        protected void btn_Delete_Click(object sender, EventArgs e)
        {
            BLHelper.BLLPaper paper = new BLHelper.BLLPaper();
            BLHelper.BLLAttachment blat = new BLHelper.BLLAttachment();
            BLHelper.BLLOperationLog op = new BLHelper.BLLOperationLog();
            try
            {
                int count = pm.GridCount(Grid_Paper, CBoxSelect).Count();
                if (count <= 0)
                {
                    Alert.ShowInTop("请至少选择一项！");
                    return;
                }
                string username = user.FindByLoginName(Session["LoginName"].ToString()).UserName;
                if (Convert.ToInt32(Session["SecrecyLevel"]) == 5)
                {
                    for (int i = 0; i < pm.GridCount(Grid_Paper, CBoxSelect).Count(); i++)
                    {
                        paper.Delete(Convert.ToInt32(Grid_Paper.DataKeys[pm.GridCount(Grid_Paper, CBoxSelect)[i]][0].ToString()));
                    }
                    InitData();
                    Alert.ShowInTop("删除数据成功!");
                    btn_Delete.Enabled = false;
                }
                else
                {
                    Common.Entities.OperationLog operate = new OperationLog();
                    for (int i = 0; i < pm.GridCount(Grid_Paper, CBoxSelect).Count(); i++)
                    {
                        paper.UpdateIsPass(Convert.ToInt32(Grid_Paper.DataKeys[pm.GridCount(Grid_Paper, CBoxSelect)[i]][0]), false);
                        operate.LoginName = username;
                        operate.OperationTime = DateTime.Now;
                        operate.LoginIP = " ";
                        operate.OperationContent = "Files";
                        operate.OperationType = "删除";
                        operate.OperationDataID = Convert.ToInt32(Grid_Paper.DataKeys[pm.GridCount(Grid_Paper, CBoxSelect)[i]][0]);
                        op.Insert(operate);
                    }
                    InitData();
                    Alert.ShowInTop("您的数据已提交，请等待确认!");
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
                Alert.ShowInTop("删除失败，请联系管理员！");
            }
        }
        //全选按钮
        protected void btnSelect_All_Click(object sender, EventArgs e)
        {
            Grid_Paper.SelectAllRows();
            int[] select = Grid_Paper.SelectedRowIndexArray;
            int m;
            //取整数（不是四舍五入，全舍）
            int Pages = (int)Math.Floor(Convert.ToDouble(Grid_Paper.RecordCount / this.Grid_Paper.PageSize));

            if (Grid_Paper.PageIndex == Pages)
                m = (Grid_Paper.RecordCount - this.Grid_Paper.PageSize * Grid_Paper.PageIndex);
            else
                m = this.Grid_Paper.PageSize;
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
                btn_Delete.Enabled = true;
                btnSelect_All.Text = "取消全选";
            }
            else
            {
                foreach (int item in select)
                {
                    CBoxSelect.SetCheckedState(item, false);
                }
                btn_Delete.Enabled = false;
                btnSelect_All.Text = "全选";
            }
        }
    }
}