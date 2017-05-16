/**编写人：方淑云
 * 时间：2014年8月24号
 * 功能:专著查询界面
 * 修改履历：修改人;高琪
 *           修改时间;20151010
 *           内容：撤销page静态变量
 **/
using Common.Entities;
using FineUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class Search_Monograph : System.Web.UI.Page
    {
        BLHelper.BLLAgency agency = new BLHelper.BLLAgency();
        BLHelper.BLLAchievement ach = new BLHelper.BLLAchievement();
        BLHelper.BLLMonograph mo = new BLHelper.BLLMonograph();
        BLHelper.BLLOperationLog bllOperate = new BLHelper.BLLOperationLog();
        BLHelper.BLLUser user = new BLHelper.BLLUser();
        //BLHelper.BLLStaffMonograph st = new BLHelper.BLLStaffMonograph();
        BLHelper.BLLBasicCode ba = new BLHelper.BLLBasicCode();
        BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
      
        private int page;
        protected void Page_Load(object sender, EventArgs e)
        {
            page = ViewState["page"] == null ? 0 : (int)ViewState["page"];
            if (!IsPostBack)
            {
                InitData();
                btn_AddMonograph.OnClientClick = Window_addMonograph.GetShowReference("Add_Monograph.aspx", "新增专著信息");
                btnExcel.OnClientClick = Window_Import.GetShowReference("~/AcademicMeeting/ImportExcel.aspx?name=专著情况表", "工具");
                //reprot1.OnClientClick = WindowReport.GetShowReference("~/Report/R_Agency_Monograph.aspx", "分部门按著作名称统计专著情况");
            }
        }
        //下载
        protected string GetEditUrlf(object ID)
        {
            return DownLoadf.GetShowReference("Down_Photo.aspx?id=" + ID, "操作");
        }
        protected string GetEditUrlb(object ID)
        {
            return DownLoadb.GetShowReference("OperateB.aspx?id=" + ID, "操作");
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
        //备注界面跳转
        protected string GetEditUrl(object ID)
        {
            return Remark.GetShowReference("Remark.aspx?id=" + ID, "备注");
        }
        //作者界面跳转
        protected string GetEditUrlw(object ID)
        {
            return Writer.GetShowReference("Writer.aspx?id=" + ID, "作者信息");
        }
        //转化等级
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
                List<Monograph> list = mo.FindPaged(Convert.ToInt32(Session["SecrecyLevel"]));
                Grid_Monograph.RecordCount = list.Count();
                if (list != null)
                {
                    Grid_Monograph.DataSource = list.Skip(Grid_Monograph.PageIndex * Grid_Monograph.PageSize).Take(Grid_Monograph.PageSize);
                    Grid_Monograph.DataBind();
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

       
        //按时间查询
        public void FindByTime()
        {
            try
            {
                ViewState["page"] = 1;
                List<Monograph> list = mo.FindByPublicTime(Convert.ToInt32(dCondition.SelectedText.Trim()), Convert.ToInt32(Session["SecrecyLevel"]));
                Grid_Monograph.RecordCount = list.Count();
                if (list != null)
                {
                    Grid_Monograph.DataSource = list.Skip(Grid_Monograph.PageIndex * Grid_Monograph.PageSize).Take(Grid_Monograph.PageSize);
                    Grid_Monograph.DataBind();
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
        //按作者查询
        public void FindByWriter()
        {
            try
            {
                ViewState["page"] = 2;
                List<Monograph> list = mo.FindByMonographPeople(tCondition.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                Grid_Monograph.RecordCount = list.Count();
                if (list != null)
                {
                    Grid_Monograph.DataSource = list.Skip(Grid_Monograph.PageIndex * Grid_Monograph.PageSize).Take(Grid_Monograph.PageSize);
                    Grid_Monograph.DataBind();
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
        //按类别查询
        public void FindBySort()
        {
            try
            {
                ViewState["page"] = 3;
                List<Monograph> list = mo.FindBySort(tCondition.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                Grid_Monograph.RecordCount = list.Count();
                if (list != null)
                {
                    Grid_Monograph.DataSource = list.Skip(Grid_Monograph.PageIndex * Grid_Monograph.PageSize).Take(Grid_Monograph.PageSize);
                    Grid_Monograph.DataBind();
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

        //按著作名称查询
        private void FindByMonographName()
        {
            try
            {
                ViewState["page"] = 4;
                List<Monograph> list = mo.FindByMonographName(tCondition.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                Grid_Monograph.RecordCount = list.Count();
                if (list != null)
                {
                    Grid_Monograph.DataSource = list.Skip(Grid_Monograph.PageIndex * Grid_Monograph.PageSize).Take(Grid_Monograph.PageSize);
                    Grid_Monograph.DataBind();
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

        //按第一作者查询
        private void FindByFirstWriter()
        {
            try
            {
                ViewState["page"] = 5;
                List<Monograph> list = mo.FindByFirstWriter(tCondition.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                Grid_Monograph.RecordCount = list.Count();
                if (list != null)
                {
                    Grid_Monograph.DataSource = list.Skip(Grid_Monograph.PageIndex * Grid_Monograph.PageSize).Take(Grid_Monograph.PageSize);
                    Grid_Monograph.DataBind();
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
        //按第一作者身份查询
        private void FindByFirstWriterPosition()
        {
            try
            {
                ViewState["page"] = 6;
                List<Monograph> list = mo.FindByFirstWriterPosition(dCondition.SelectedText.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));  //dCondition.SelectedText.Trim()
                Grid_Monograph.RecordCount = list.Count();
                if (list != null)
                {
                    Grid_Monograph.DataSource = list.Skip(Grid_Monograph.PageIndex * Grid_Monograph.PageSize).Take(Grid_Monograph.PageSize);
                    Grid_Monograph.DataBind();
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
        //按部门查询
        private void FindByAgency()
        {
            try
            {
                ViewState["page"] = 7;
                List<Monograph> list = mo.FindByAgency(dCondition.SelectedText.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));  //dCondition.SelectedText.Trim()
                Grid_Monograph.RecordCount = list.Count();
                if (list != null)
                {
                    Grid_Monograph.DataSource = list.Skip(Grid_Monograph.PageIndex * Grid_Monograph.PageSize).Take(Grid_Monograph.PageSize);
                    Grid_Monograph.DataBind();
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
                Grid_Monograph.PageIndex = 0;
                if (dChoose.SelectedText == "全部")
                {
                    InitData();
                }
                if (dChoose.SelectedText == "出版年份")
                {
                    FindByTime();
                    return;
                }
                if (dChoose.SelectedText == "第一作者身份")
                {
                    FindByFirstWriterPosition();
                    return;
                }
                if (dChoose.SelectedText == "部门")
                {
                    FindByAgency();
                    return;
                }
                if (tCondition.Text.Trim() != "")
                {

                    if (dChoose.SelectedText == "作者")
                    {
                        FindByWriter();
                        return;
                    }
                    /*if (dChoose.SelectedText == "类别")
                    {
                        FindBySort();
                        return;
                    }*/
                    if (dChoose.SelectedText == "著作名称")
                    {
                        FindByMonographName();
                        return;
                    }
                   
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
            Delete.Enabled = false;
        }

        //删除
        protected void Delete_Click(object sender, EventArgs e)
        {
            try
            {
                List<int> selections = pm.GridCount(Grid_Monograph, CBoxSelect);
                if (Convert.ToInt32(Session["SecrecyLevel"]) == 5)
                {
                    for (int i = 0; i < selections.Count(); i++)
                    {
                        mo.Delete(Convert.ToInt32(Grid_Monograph.DataKeys[selections[i]][0]));
                    }
                    InitData();
                    btnSelect_All.Text = "全选";
                    Alert.ShowInTop("删除数据成功!");
                    
                }
                else
                {
                    for (int i = 0; i < selections.Count(); i++)
                    {
                        mo.UpdateIsPass(Convert.ToInt32(Grid_Monograph.DataKeys[selections[i]][0]), false);
                        //向操作日志表中插入
                        OperationLog operate = new OperationLog();
                        operate.LoginName = user.FindByLoginName(Session["LoginName"].ToString()).UserName;
                        operate.LoginIP = "";
                        operate.OperationType = "删除";
                        operate.OperationContent = "ProjectImportantNode";
                        operate.OperationDataID = Convert.ToInt32(Grid_Monograph.DataKeys[selections[i]][0]);
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
        //更新
        protected void btn_UpdateInspect_Click(object sender, EventArgs e)
        {
            try
            {
                if (pm.GridCount(Grid_Monograph, CBoxSelect).Count() != 0)
                {
                    if (pm.GridCount(Grid_Monograph, CBoxSelect).Count() == 1)
                    {
                        int rowID = Convert.ToInt32(Grid_Monograph.DataKeys[pm.GridCount(Grid_Monograph, CBoxSelect)[0]][0]);
                        Session["MonographID"] = rowID;
                        Alert.Show("你确定要修改该行数据吗!", "确认消息", MessageBoxIcon.Information, Window_UpdateMonograph.GetShowReference("Update_Monograph.aspx", "修改专著信息"), Target.Top);
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
        protected void Grid_Monograph_PageIndexChange(object sender, FineUI.GridPageEventArgs e)
        {
            Grid_Monograph.PageIndex = e.NewPageIndex;
            switch (page)
            {
                case 0:
                    InitData();
                    break;
                case 1:
                    FindByTime();
                    break;
                case 2:
                    FindByWriter();
                    break;
                case 3:
                    FindBySort();
                    break;
                case 4:
                    FindByMonographName();
                    break;

                case 5:
                    FindByFirstWriterPosition();
                    break;
                case 6:
                    FindByAgency();
                    break;
            }
        }
        //搜索
        protected void ddlGridPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid_Monograph.PageIndex = 0;
            this.Grid_Monograph.PageSize = Convert.ToInt32(ddlGridPageSize.SelectedValue);
            switch (page)
            {
                case 0:
                    InitData();
                    break;
                case 1:
                    FindByTime();
                    break;
                case 2:
                    FindByWriter();
                    break;
                case 3:
                    FindBySort();
                    break;
                case 4:
                    FindByMonographName();
                    break;
                case 5:
                    FindByFirstWriterPosition();
                    break;
                case 6:
                    FindByAgency();
                    break;
            }
        }
        //行点击事件
        protected void Grid_Monograph_RowCommand(object sender, FineUI.GridCommandEventArgs e)
        {
            try
            {
                string Person = Grid_Monograph.Rows[e.RowIndex].Values[2].ToString();
                string username = user.FindByLoginName(Session["LoginName"].ToString()).UserName;

                if (Person != username && Convert.ToInt32(Session["SecrecyLevel"]) != 5)
                {
                    string str = "您无对此行操作的权限！此行信息为" + Person + "录入，请与管理员联系!";
                    CBoxSelect.SetCheckedState(e.RowIndex, false);
                    Alert.ShowInTop(str);
                }
                int m;
                //取整数（不是四舍五入，全舍）
                int Pages = (int)Math.Floor(Convert.ToDouble(Grid_Monograph.RecordCount / this.Grid_Monograph.PageSize));

                if (Grid_Monograph.PageIndex == Pages)
                    m = (Grid_Monograph.RecordCount - this.Grid_Monograph.PageSize * Grid_Monograph.PageIndex);
                else
                    m = this.Grid_Monograph.PageSize;
                bool isCheck = false;
                for (int i = 0; i < m; i++)
                {
                    if (CBoxSelect.GetCheckedState(i))
                        isCheck = true;
                }
                if (isCheck)
                    Delete.Enabled = true;
                else
                    Delete.Enabled = false;
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
        //搜索条件即搜索框变化
        protected void dChoose_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (dChoose.SelectedValue)
            {
                case "全部":
                    dCondition.Enabled = false;
                    tCondition.Enabled = false;
                    break;
                /*case "类别":
                    dCondition.Enabled = false;
                    tCondition.Enabled = true;
                    break;*/
                case "作者":
                      dCondition.Enabled = false;
                    tCondition.Enabled = true;
                    break;
                case "出版年份":
                    dCondition.Items.Clear();
                    for (int i = 1960; i <= 2060; i++)
                    {
                        dCondition.Items.Add(i.ToString(), i.ToString());
                    }
                    dCondition.Items[0].Selected = true;
                    dCondition.Enabled = true;
                    tCondition.Enabled = false;
                    break;
                case"第一作者身份":
                     dCondition.Items.Clear();
                    List<BasicCode> list1 = ba.FindByCategoryName("第一作者身份");
                    for (int i = 0; i < list1.Count(); i++)
                    {
                        dCondition.Items.Add(list1[i].CategoryContent.ToString(), list1[i].CategoryContent.ToString());
                    }
                    dCondition.Items[0].Selected = true;
                    dCondition.EnableEdit = false;
                    tCondition.Enabled = false;
                    dCondition.Enabled = true;
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
                    
            }
        }
        //导出
        protected void btn_Get_Click(object sender, EventArgs e)
        {
            try
            {
                if (page == 0)
                {
                    List<Monograph> list = mo.FindPaged(Convert.ToInt32(Session["SecrecyLevel"]));
                    if (list != null)
                    {
                        Grid_Monograph.DataSource = list;
                        Grid_Monograph.DataBind();
                    }
                }
                if (page == 1)
                {
                    List<Monograph> list = mo.FindByPublicTime(Convert.ToInt32(dCondition.SelectedText.Trim()), Convert.ToInt32(Session["SecrecyLevel"]));
                    if (list != null)
                    {
                        Grid_Monograph.DataSource = list;
                        Grid_Monograph.DataBind();
                    }
                }
                if (page == 2)
                {
                    List<Monograph> list = mo.FindByMonographPeople(tCondition.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                    if (list != null)
                    {
                        Grid_Monograph.DataSource = list;
                        Grid_Monograph.DataBind();
                    }
                }
                if (page == 3)
                {
                    List<Monograph> list = mo.FindBySort(tCondition.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                    if (list != null)
                    {
                        Grid_Monograph.DataSource = list;
                        Grid_Monograph.DataBind();
                    }
                }

                pm.ExportExcel(3, Grid_Monograph, 4);
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
        //获取作者
        public string getpeople(int id)
        {
            try
            {
                string str = mo.FindWriter(id);
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
            return dataItemIndex + (Grid_Monograph.PageIndex) * Grid_Monograph.PageSize;
        }

        protected void Window_Import_Close(object sender, WindowCloseEventArgs e)
        {
            this.btnRefresh_Click(null, null);
        }
        //全选按钮
        protected void btnSelect_All_Click(object sender, EventArgs e)
        {
            Grid_Monograph.SelectAllRows();
            int[] select = Grid_Monograph.SelectedRowIndexArray;
            int m;
            //取整数（不是四舍五入，全舍）
            int Pages = (int)Math.Floor(Convert.ToDouble(Grid_Monograph.RecordCount / this.Grid_Monograph.PageSize));

            if (Grid_Monograph.PageIndex == Pages)
                m = (Grid_Monograph.RecordCount - this.Grid_Monograph.PageSize * Grid_Monograph.PageIndex);
            else
                m = this.Grid_Monograph.PageSize;
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
                Delete.Enabled = true;
                btnSelect_All.Text = "取消全选";
            }
            else
            {
                foreach (int item in select)
                {
                    CBoxSelect.SetCheckedState(item, false);
                }
                Delete.Enabled = false;
                btnSelect_All.Text = "全选";
            }
        }
    }
}