/**编写人：方淑云
 * 时间：2014年8月9号
 * 功能:来单位考察查询界面后台
 * 修改履历：2015年10月10日 马睿杰 去除page静态
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
    public partial class 查询考察信息页面 : System.Web.UI.Page
    {
        BLHelper.BLLUnitInspect inspect = new BLHelper.BLLUnitInspect();
        BLHelper.BLLAgency agency = new BLHelper.BLLAgency();
        BLHelper.BLLOperationLog op = new BLHelper.BLLOperationLog();
        BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
        BLHelper.BLLAttachment at = new BLHelper.BLLAttachment();
        BLHelper.BLLUser user = new BLHelper.BLLUser();
        OperationLog operate = new OperationLog();

        private int page;
        protected void Page_Load(object sender, EventArgs e)
        {
            page = ViewState["page"] == null ? 0 : (int)ViewState["page"];
            btnSelect_All.Text = "全选";
            if (!IsPostBack)
            {
               
                InitYear();
                InitData();
                btnAddInspect.OnClientClick = Window_addInspect.GetShowReference("Add_Inspect.aspx", "新增考察信息");
            }
        }
        //初始化
        public void InitData()
        {
            try
            {
                ViewState["page"] = 0;
                List<UnitInspect> list = inspect.FindPaged(Convert.ToInt32(Session["SecrecyLevel"]));
                Grid_UnitInspect.RecordCount = list.Count();
                if (list != null)
                {
                    Grid_UnitInspect.DataSource = list.Skip(Grid_UnitInspect.PageIndex * Grid_UnitInspect.PageSize).Take(Grid_UnitInspect.PageSize);
                    Grid_UnitInspect.DataBind();
                }
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
        //初始化年份下拉框
        public void InitYear()
        {
            dCondition.Items.Add("全部", "全部");
            for (int i = 1960; i <= 2060; i++)
            {
                dCondition.Items.Add(i.ToString(), i.ToString());
            }
        }
        //分页
        protected void Grid_UnitInspect_PageIndexChange(object sender, FineUI.GridPageEventArgs e)
        {
            Grid_UnitInspect.PageIndex = e.NewPageIndex;
            switch (page)
            {
                case 0:
                    InitData();
                    break;
                case 1:
                    FindByYear();
                    break;
                case 2:
                    FindByWorkPlace();
                    break;
            }           
        }

        protected void ddlGridPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid_UnitInspect.PageIndex = 0;
            this.Grid_UnitInspect.PageSize = Convert.ToInt32(ddlGridPageSize.SelectedValue);
            switch (page)
            {
                case 0:
                    InitData();
                    break;
                case 1:
                    FindByYear();
                    break;
                case 2:
                    FindByWorkPlace();
                    break;
            }           
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
        //刷新
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            InitData();
            btnDelete.Enabled = false;
            dCondition.Reset();
        }
        //根据年份搜索
        public void FindByYear()
        {
            try
            {
                ViewState["page"] = 1;
                Grid_UnitInspect.PageIndex = 0;
                List<UnitInspect> list = inspect.FindByInspectTime(Convert.ToInt32(dCondition.SelectedText.Trim()), Convert.ToInt32(Session["SecrecyLevel"]));
                Grid_UnitInspect.RecordCount = list.Count();
                if (list != null)
                {
                    Grid_UnitInspect.DataSource = list.Skip(Grid_UnitInspect.PageIndex * Grid_UnitInspect.PageSize).Take(Grid_UnitInspect.PageSize);
                    Grid_UnitInspect.DataBind();
                }
                btnDelete.Enabled = false;
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
                switch (ddl_search.SelectedIndex)
                {
                    case 0:
                        InitData();
                        break;
                    case 1:
                        FindByYear();
                        break;
                    case 2:
                        FindByWorkPlace();
                        break;
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }

        private void FindByWorkPlace()
        {
            try
            {
                ViewState["page"] = 2;
                Grid_UnitInspect.PageIndex = 0;
                List<UnitInspect> list = inspect.FindByWorkPlace(tb_content.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                Grid_UnitInspect.RecordCount = list.Count();
                if (list != null)
                {
                    Grid_UnitInspect.DataSource = list.Skip(Grid_UnitInspect.PageIndex * Grid_UnitInspect.PageSize).Take(Grid_UnitInspect.PageSize);
                    Grid_UnitInspect.DataBind();
                }
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
        //删除
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string username = user.FindByLoginName(Session["LoginName"].ToString()).UserName;
                if (Convert.ToInt32(Session["SecrecyLevel"]) == 5)
                {
                    for (int i = 0; i < pm.GridCount(Grid_UnitInspect, CBoxSelect).Count(); i++)
                    {
                        int attachid = inspect.Delete(Convert.ToInt32(Grid_UnitInspect.DataKeys[pm.GridCount(Grid_UnitInspect, CBoxSelect)[i]][0].ToString()));
                        string path = at.FindPath(attachid);
                        if (path != "")
                        {
                            pm.DeleteFile(attachid, path);
                        }
                    }
                    InitData();
                    Alert.ShowInTop("删除数据成功!");
                    btnSelect_All.Text = "全选";
                }
                else
                {
                    for (int i = 0; i < pm.GridCount(Grid_UnitInspect, CBoxSelect).Count(); i++)
                    {
                        inspect.ChangePass(Convert.ToInt32(Grid_UnitInspect.DataKeys[pm.GridCount(Grid_UnitInspect, CBoxSelect)[i]][0]), false);
                        operate.LoginName = username;
                        operate.OperationTime = DateTime.Now;
                        operate.LoginIP = " ";
                        operate.OperationContent = "UnitInspect";
                        operate.OperationType = "删除";
                        operate.OperationDataID = Convert.ToInt32(Grid_UnitInspect.DataKeys[pm.GridCount(Grid_UnitInspect, CBoxSelect)[i]][0]);
                        op.Insert(operate);
                    }
                    InitData();
                    Alert.ShowInTop("您的数据已提交，请等待确认!");
                    btnSelect_All.Text = "全选";
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
        //编辑
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {

                if (pm.GridCount(Grid_UnitInspect, CBoxSelect).Count() != 0)
                {
                    if (pm.GridCount(Grid_UnitInspect, CBoxSelect).Count() == 1)
                    {
                        int rowID = Convert.ToInt32(Grid_UnitInspect.DataKeys[pm.GridCount(Grid_UnitInspect, CBoxSelect)[0]][0]);
                        Session["InspectID"] = rowID;
                        Alert.Show("你确定要修改该行数据吗!", "确认消息", MessageBoxIcon.Information, Window_updateInspect.GetShowReference("Update_Inspect.aspx", "编辑来单位考察信息"), Target.Top);
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
        //下载
        protected string GetEditUrlx(object ID)
        {
            return DownLoad.GetShowReference("Operate.aspx?id=" + ID, "操作");
        }
        //转化等级
        public string ChangeSecrecyLevel(int level)
        {
            string[] SecrecyLevels = new string[] { "四级", "三级", "二级", "一级", "管理员" };
            return SecrecyLevels[level - 1];
        }
        //详情界面跳转
        protected string GetEditUrl(object UnitInspectID)
        {
            return Details.GetShowReference("Details.aspx?id=" + UnitInspectID, "考察内容");
        }
        //选中行事件
        protected void Grid_UnitInspect_RowCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                string Person = Grid_UnitInspect.Rows[e.RowIndex].Values[2].ToString();
                string username = user.FindByLoginName(Session["LoginName"].ToString()).UserName;
                if (Person != username && Convert.ToInt32(Session["SecrecyLevel"]) != 5)
                {
                    string str = "您无对此行操作的权限！此行信息为" + Person + "录入，请与管理员联系!";
                    CBoxSelect.SetCheckedState(e.RowIndex, false);
                    Alert.ShowInTop(str);
                }
                if (pm.GridCount(Grid_UnitInspect, CBoxSelect).Count == 0)
                {
                    btnDelete.Enabled = false;
                    return;
                }
                if (pm.GridCount(Grid_UnitInspect, CBoxSelect).Count != 0)
                {
                    btnDelete.Enabled = true;
                    return;
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
                if (page == 1)
                {
                    List<UnitInspect> list = new List<UnitInspect>();
                    if (dCondition.SelectedText.Trim() == "全部")
                        list = inspect.FindPaged(Convert.ToInt32(Session["SecrecyLevel"]));
                    else
                        list = inspect.FindByInspectTime(Convert.ToInt32(dCondition.SelectedText.Trim()), Convert.ToInt32(Session["SecrecyLevel"]));
                    if (list != null)
                    {
                        Grid_UnitInspect.DataSource = list;
                        Grid_UnitInspect.DataBind();
                    }
                }

                if (page == 0)
                {
                    List<UnitInspect> list = inspect.FindPaged(Convert.ToInt32(Session["SecrecyLevel"]));
                    if (list != null)
                    {
                        Grid_UnitInspect.DataSource = list;
                        Grid_UnitInspect.DataBind();
                    }

                }
                pm.ExportExcel(3, Grid_UnitInspect, 2);
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
                return;
            }
        }
        //grid序号
        public int RowNumber(int dataItemIndex)
        {
            return dataItemIndex + (Grid_UnitInspect.PageIndex) * Grid_UnitInspect.PageSize;
        }

        //搜索条件改变
        protected void ddl_search_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (ddl_search.SelectedIndex)
            {
                case 0:
                    dCondition.Enabled = false;
                    tb_content.Enabled = false;
                    break;
                case 1:
                    dCondition.Enabled = true;
                    tb_content.Enabled = false;
                    break;
                case 2:
                    dCondition.Enabled = false;
                    tb_content.Enabled = true;
                    break;
            }
        }

        //全选按钮
        protected void btnSelect_All_Click(object sender, EventArgs e)
        {
            Grid_UnitInspect.SelectAllRows();
            int[] select = Grid_UnitInspect.SelectedRowIndexArray;
            int m;
            //取整数（不是四舍五入，全舍）
            int Pages = (int)Math.Floor(Convert.ToDouble(Grid_UnitInspect.RecordCount / this.Grid_UnitInspect.PageSize));

            if (Grid_UnitInspect.PageIndex == Pages)
                m = (Grid_UnitInspect.RecordCount - this.Grid_UnitInspect.PageSize * Grid_UnitInspect.PageIndex);
            else
                m = this.Grid_UnitInspect.PageSize;
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