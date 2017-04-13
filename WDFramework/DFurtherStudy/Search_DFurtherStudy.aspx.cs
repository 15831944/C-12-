/**编写人：方淑云
 * 时间：2014年8月1号
 * 功能:进修学习（派遣）查询界面后台
 * 修改履历： 1.修改人：吕博扬
 *             修改时间：10月10日
 *             修改内容：撤销静态变量page
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
    public partial class Search_DFurtherStudy : System.Web.UI.Page
    {
        BLHelper.BLLDFurtherStudy df = new BLHelper.BLLDFurtherStudy();
        BLHelper.BLLUser user = new BLHelper.BLLUser();
        BLHelper.BLLOperationLog op = new BLHelper.BLLOperationLog();
        OperationLog operate = new OperationLog();
        BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
        private int page;
        protected void Page_Load(object sender, EventArgs e)
        {
            page = ViewState["page"] == null ? 0 : (int)ViewState["page"];
            if (!IsPostBack)
            {
                InitYear();
                InitData();           
                btnAddDFutherStudy.OnClientClick = Window_addDFutherStudy.GetShowReference("Add_DFutherStudy.aspx", "新增进修学习信息");               
            }
        }
        //初始化
        public void InitData()
        {
            try
            {
                ViewState["page"] = 0;

                List<DFurtherStudy> list = df.FindPaged(Convert.ToInt32(Session["SecrecyLevel"]));
                Grid_FurtherStudy.RecordCount = list.Count();
                if (list != null)
                {
                    Grid_FurtherStudy.DataSource = list.Skip(Grid_FurtherStudy.PageIndex * Grid_FurtherStudy.PageSize).Take(Grid_FurtherStudy.PageSize);
                    Grid_FurtherStudy.DataBind();
                }
                else
                {
                    return;
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
            //dCondition.Items[0].Selected = true;
        }
        //将人员ID转化为人员姓名
        public string UserName(int userid)
        {
            return user.FindByUserID(userid);   
        }
        //刷新 
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            btnDelete.Enabled = false;
            dCondition.Reset();
            InitData();
        }
        //按年份搜索
        public void FindByYear()
        {
            try
            {
                ViewState["page"] = 1;
                List<DFurtherStudy> list = df.FindByYear(Convert.ToInt32(dCondition.SelectedText.Trim()), Convert.ToInt32(Session["SecrecyLevel"]));
                Grid_FurtherStudy.RecordCount = list.Count();
                if (list != null)
                {
                    Grid_FurtherStudy.DataSource = list.Skip(Grid_FurtherStudy.PageIndex * Grid_FurtherStudy.PageSize).Take(Grid_FurtherStudy.PageSize);
                    Grid_FurtherStudy.DataBind();
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
            switch (ddl_search.SelectedIndex)
            {
                case 0:
                    Alert.ShowInTop("请选择查询条件!");
                    return;
                case 1:
                    FindByYear();
                    break;
                case 2:
                    FindByName();
                    break;
            }
        }

        //根据人员姓名查询
        private void FindByName()
        {
            try{
                ViewState["page"] = 2;
                Grid_FurtherStudy.PageIndex = 0;
                int userid = user.FindID(tb_content.Text.Trim());
                List<DFurtherStudy> list = df.FindByName(userid, Convert.ToInt32(Session["SecrecyLevel"]));
                Grid_FurtherStudy.RecordCount = list.Count;
                Grid_FurtherStudy.DataSource = list.Skip(Grid_FurtherStudy.PageIndex * Grid_FurtherStudy.PageSize).Take(Grid_FurtherStudy.PageSize);
                Grid_FurtherStudy.DataBind();
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
                    for (int i = 0; i < pm.GridCount(Grid_FurtherStudy, CBoxSelect).Count(); i++)
                    {
                        df.Delete(Convert.ToInt32(Grid_FurtherStudy.DataKeys[pm.GridCount(Grid_FurtherStudy, CBoxSelect)[i]][0].ToString()));
                    }
                    InitData();
                    Alert.ShowInTop("删除数据成功!");
                }
                else
                {
                    for (int i = 0; i < pm.GridCount(Grid_FurtherStudy, CBoxSelect).Count(); i++)
                    {
                        df.ChangePass(Convert.ToInt32(Grid_FurtherStudy.DataKeys[pm.GridCount(Grid_FurtherStudy, CBoxSelect)[i]][0]), false);
                        operate.LoginName = username;
                        operate.OperationTime = DateTime.Now;
                        operate.LoginIP = " ";
                        operate.OperationContent = "DFurtherStudy";
                        operate.OperationType = "删除";
                        operate.OperationDataID = Convert.ToInt32(Grid_FurtherStudy.DataKeys[pm.GridCount(Grid_FurtherStudy, CBoxSelect)[i]][0]);
                        op.Insert(operate);
                    }
                    InitData();
                    Alert.ShowInTop("您的数据已提交，请等待确认!");
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }

        //编辑选中行按钮
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (pm.GridCount(Grid_FurtherStudy, CBoxSelect).Count() != 0)
                {
                    if (pm.GridCount(Grid_FurtherStudy, CBoxSelect).Count() == 1)
                    {
                        int rowID = Convert.ToInt32(Grid_FurtherStudy.DataKeys[pm.GridCount(Grid_FurtherStudy, CBoxSelect)[0]][0]);
                        Session["ID"] = rowID;
                        Alert.Show("你确定要修改该行数据吗!", "确认消息", MessageBoxIcon.Information, Window_UpdateDFutherStudy.GetShowReference("Update_DFurtherStudy.aspx", "编辑进修学习信息"), Target.Top);
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

        //转化等级
        public string ChangeSecrecyLevel(int level)
        {
            string[] SecrecyLevels = new string[] { "四级", "三级", "二级", "一级", "管理员" };
            return SecrecyLevels[level - 1];
        }

        //分页
        protected void ddlGridPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid_FurtherStudy.PageIndex = 0;
            this.Grid_FurtherStudy.PageSize = Convert.ToInt32(ddlGridPageSize.SelectedValue);
            switch (page)
            {
                case 0:
                    InitData();
                    break;
                case 1:
                    FindByYear();
                    break;
                case 2:
                    FindByName();
                    break;
            }
        }

        protected void Grid_FurtherStudy_PageIndexChange(object sender, GridPageEventArgs e)
        {
            Grid_FurtherStudy.PageIndex = e.NewPageIndex;
            switch (page)
            {
                case 0:
                    InitData();
                    break;
                case 1:
                    FindByYear();
                    break;
                case 2:
                    FindByName();
                    break;
            }
        }
        //行点击事件
        protected void Grid_FurtherStudy_RowCommand1(object sender, GridCommandEventArgs e)
        {
            try
            {
                string Person = Grid_FurtherStudy.Rows[e.RowIndex].Values[2].ToString();
                string username = user.FindByLoginName(Session["LoginName"].ToString()).UserName;
                if (Person != username && Convert.ToInt32(Session["SecrecyLevel"]) != 5)
                {
                    string str = "您无对此行操作的权限！此行信息为" + Person + "录入，请与管理员联系!";
                    CBoxSelect.SetCheckedState(e.RowIndex, false);
                    Alert.ShowInTop(str);
                }
                if (pm.GridCount(Grid_FurtherStudy, CBoxSelect).Count == 0)
                {
                    btnDelete.Enabled = false;
                    return;
                }
                if (pm.GridCount(Grid_FurtherStudy, CBoxSelect).Count != 0)
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
                if (page == 0)
                {
                    List<DFurtherStudy> list = df.FindPaged(Convert.ToInt32(Session["SecrecyLevel"]));
                    if (list != null)
                    {
                        Grid_FurtherStudy.DataSource = list;
                        Grid_FurtherStudy.DataBind();
                    }
                }

                if (page == 1)
                {
                    List<DFurtherStudy> list = new List<DFurtherStudy>();
                    if (dCondition.SelectedText.Trim() == "全部")
                    {
                        list = df.FindPaged(Convert.ToInt32(Session["SecrecyLevel"]));
                    }
                    else
                    {
                        list = df.FindByYear(Convert.ToInt32(dCondition.SelectedText.Trim()), Convert.ToInt32(Session["SecrecyLevel"]));
                    }
                    if (list != null)
                    {
                        Grid_FurtherStudy.DataSource = list;
                        Grid_FurtherStudy.DataBind();
                    }

                }

                pm.ExportExcel(3, Grid_FurtherStudy, 0);
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
        //grid序号
        public int RowNumber(int dataItemIndex)
        {
            return dataItemIndex + (Grid_FurtherStudy.PageIndex) * Grid_FurtherStudy.PageSize;
        }

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
    }
} 