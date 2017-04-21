/**编写人：张凡凡
 * 时间：2014年8月2号
 * 功能:进修学习（接受）查询界面后台
 * 修改履历：    1.修改人：吕博扬
 *                修改时间：10月10日
 *                修改内容：撤销静态变量page
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
    public partial class 查询进修学习_接受_ : System.Web.UI.Page
    {
        BLHelper.BLLFutherStudy futherstudy = new BLHelper.BLLFutherStudy();
        BLHelper.BLLUser user = new BLHelper.BLLUser();
        BLHelper.BLLAgency agen = new BLHelper.BLLAgency();
        BLHelper.BLLOperationLog op = new BLHelper.BLLOperationLog();
        BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
        OperationLog operate = new OperationLog();
        private int page;

        protected void Page_Load(object sender, EventArgs e)
        {
            page = ViewState["page"] == null ? 0 : (int)ViewState["page"];
            if (!IsPostBack)
            {
                InitData();
                for (int i = 1960; i < 2060; i++)
                    ddl_PeopleName.Items.Add(i.ToString(), (i - 1960).ToString());
                ddl_PeopleName.Items[45].Selected = true;
                btnDelete.Enabled = false;
                //删除数据
                btnDelete.OnClientClick = Grid_FurtherStudy.GetNoSelectionAlertReference("请至少选择一项！");
                btnDelete.ConfirmText = String.Format("你确定要删除该行数据吗？", Grid_FurtherStudy.GetSelectedCellReference());
                btnAddFutherStudy.OnClientClick = Window_addFutherStudy.GetShowReference("Add_FutherStudy.aspx", "新增进修学习信息");
            }
        }


        //初始化界面
        public void InitData()
        {
            List<FutherStudy> list = futherstudy.FindPaged(Convert.ToInt32(Session["SecrecyLevel"]));
            Grid_FurtherStudy.RecordCount = list.Count;
            page = 0;
            Grid_FurtherStudy.DataSource = list.Skip(Grid_FurtherStudy.PageIndex * Grid_FurtherStudy.PageSize).Take(Grid_FurtherStudy.PageSize);
            Grid_FurtherStudy.DataBind();
        }
        //判断男女
        public string getgender(string bx)
        {
            return user.getgender(bx);
        }
        //部门ID转化为部门名称
        public string AgencyName(int agencyID)
        {
            try
            {
                return agen.FindAgenName(agencyID);
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
                return "";
            }
        }

        protected string SecrecyLevel(int level)
        {
            string[] SecrecyLevels = new string[] { "四级", "三级", "二级", "一级", "管理员" };
            return SecrecyLevels[level - 1];
        }

        //刷新
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            InitData();
            btnDelete.Enabled = false;
        }
        //搜索
        protected void Select_Click(object sender, EventArgs e)
        {
            switch(ddl_search.SelectedIndex)
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

        //根据年份查询
        private void FindByYear()
        {
            ViewState["page"] = 1;
            Grid_FurtherStudy.PageIndex = 0;
            List<FutherStudy> list = futherstudy.FindByYear(Convert.ToInt32(ddl_PeopleName.SelectedText.Trim()), Convert.ToInt32(Session["SecrecyLevel"]));
            Grid_FurtherStudy.RecordCount = list.Count;
            Grid_FurtherStudy.DataSource = list.Skip(Grid_FurtherStudy.PageIndex * Grid_FurtherStudy.PageSize).Take(Grid_FurtherStudy.PageSize);
            Grid_FurtherStudy.DataBind();
            btnDelete.Enabled = false;
        }

        //根据人员姓名查询
        private void FindByName()
        {
            ViewState["page"] = 2;
            Grid_FurtherStudy.PageIndex = 0;
            List<FutherStudy> list = futherstudy.FindByName(tb_content.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
            Grid_FurtherStudy.RecordCount = list.Count;
            Grid_FurtherStudy.DataSource = list.Skip(Grid_FurtherStudy.PageIndex * Grid_FurtherStudy.PageSize).Take(Grid_FurtherStudy.PageSize);
            Grid_FurtherStudy.DataBind();
            btnDelete.Enabled = false;

        }

        //删除
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            List<int> selections = pm.GridCount(Grid_FurtherStudy, CBoxSelect);
            if (Convert.ToInt32(Session["SecrecyLevel"]) == 5)
            {
                for (int i = 0; i < selections.Count(); i++)
                {
                    futherstudy.Delete(Convert.ToInt32(Grid_FurtherStudy.DataKeys[selections[i]][0].ToString()));
                }
                InitData();
                Alert.ShowInTop("删除数据成功!");
            }
            else
            {
                for (int i = 0; i < selections.Count(); i++)
                {
                    futherstudy.UpdateIsPass(Convert.ToInt32(Grid_FurtherStudy.DataKeys[selections[i]][0].ToString()), false);
                    operate.LoginName = Session["LoginName"].ToString();
                    operate.OperationTime = DateTime.Now;
                    operate.LoginIP = " ";
                    operate.OperationContent = "FutherStudy";
                    operate.OperationType = "删除";
                    operate.OperationDataID = Convert.ToInt32(Grid_FurtherStudy.DataKeys[selections[i]][0].ToString());
                    op.Insert(operate);
                }
                InitData();
                Alert.ShowInTop("操作已经提交，请等待管理员确认!");
            }
            btnDelete.Enabled = false;
        }

        //行点击事件
        protected void Grid_FurtherStudy_RowCommand(object sender, GridCommandEventArgs e)
        {
            string Person = Grid_FurtherStudy.Rows[e.RowIndex].Values[0].ToString();
            BLHelper.BLLUser user = new BLHelper.BLLUser();
            string username = user.FindByLoginName(Session["LoginName"].ToString()).UserName;

            if (Person != username && Convert.ToInt32(Session["SecrecyLevel"]) != 5)
            {
                string str = "您无对此行操作的权限！此行信息为" + Person + "录入，请与管理员联系!";
                CBoxSelect.SetCheckedState(e.RowIndex, false);
                Alert.ShowInTop(str);
            }
            if (pm.GridCount(Grid_FurtherStudy, CBoxSelect).Count == 0)
                btnDelete.Enabled = false;
            else
                btnDelete.Enabled = true;
        }

        //编辑
        protected void ButtonUpdate_Click(object sender, EventArgs e)
        {
            List<int> selections = new List<int>();
            for (int i = 0; i < Grid_FurtherStudy.RecordCount; i++)
            {
                if (CBoxSelect.GetCheckedState(i))
                    selections.Add(i);
            }
            if (selections.Count() != 0)
            {
                if (selections.Count() == 1)
                {
                    int rowID = Convert.ToInt32(Grid_FurtherStudy.DataKeys[selections[0]][0]);
                    Session["FurID"] = rowID;
                    //ButtonUpdate.OnClientClick = Window_UpdateFutherStudy.GetShowReference("Update_FutherStudy.aspx", "编辑");
                    Alert.Show("你确定要修改该行数据吗!", "确认消息", MessageBoxIcon.Information, Window_UpdateFutherStudy.GetShowReference("Update_FutherStudy.aspx", "编辑"), Target.Top); 
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

        //分页
        protected void Grid_FurtherStudy_PageIndexChange(object sender, GridPageEventArgs e)
        {
            btnDelete.Enabled = false;
            Grid_FurtherStudy.PageIndex = e.NewPageIndex;
            switch (ddl_search.SelectedIndex)
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
        protected void ddlGridPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid_FurtherStudy.PageIndex = 0;
            if (pm.GridCount(Grid_FurtherStudy, CBoxSelect).Count == 0)
                btnDelete.Enabled = false;
            else
                btnDelete.Enabled = true;
            Grid_FurtherStudy.PageSize = Convert.ToInt32(ddlGridPageSize.SelectedText);
            switch (ddl_search.SelectedIndex)
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
        //grid序号
        public int RowNumber(int dataItemIndex)
        {
            return dataItemIndex + (Grid_FurtherStudy.PageIndex) * Grid_FurtherStudy.PageSize;
        }

        //个人简介详情
        protected string GetEditUrlp(object id)
        {
            return Personfile.GetShowReference("Detail.aspx?id=" + id, "个人简介");
        }
        //备注详情
        protected string GetEditUrlc(object id)
        {
            return Contents.GetShowReference("Content.aspx?id=" + id, "备注");
        }

        //查询条件更改
        protected void ddl_search_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (ddl_search.SelectedIndex)
            {
                case 0:
                    ddl_PeopleName.Enabled = false;
                    tb_content.Enabled = false;
                    break;
                case 1:
                    ddl_PeopleName.Enabled = true;
                    tb_content.Enabled = false;
                    break;
                case 2:
                    ddl_PeopleName.Enabled = false;
                    tb_content.Enabled = true;
                    break;
            }
        }
        //全选按钮
        protected void btnSelect_All_Click(object sender, EventArgs e)
        {
            Grid_FurtherStudy.SelectAllRows();
            int[] select = Grid_FurtherStudy.SelectedRowIndexArray;
            int m;
            //取整数（不是四舍五入，全舍）
            int Pages = (int)Math.Floor(Convert.ToDouble(Grid_FurtherStudy.RecordCount / this.Grid_FurtherStudy.PageSize));

            if (Grid_FurtherStudy.PageIndex == Pages)
                m = (Grid_FurtherStudy.RecordCount - this.Grid_FurtherStudy.PageSize * Grid_FurtherStudy.PageIndex);
            else
                m = this.Grid_FurtherStudy.PageSize;
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