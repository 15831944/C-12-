/**编写人：未知
 * 时间：未知
 * 功能: 办公-平台
 * 修改履历： 1.修改人：吕博扬
 *             修改时间：10月10日
 *             修改内容：撤销静态变量
 *           2.修改人：吕博杨
 *             修改时间：2015年11月28日
 *             修改内容：为新增加的平台属性——批复文号、平台负责人、平台成员、批复经费、平台管理（时间、人员、业务、经费）、上传文件ID添加代码支持
 **/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLHelper;
using Common.Entities;
using FineUI;

namespace WDFramework.Platform
{
    public partial class SearchPlatform : System.Web.UI.Page
    {
        BLLPlatform BLLPlatform = new BLLPlatform();
        BLLUser BLLUser = new BLLUser();
        BLCommon.PublicMethod publicmethod = new BLCommon.PublicMethod();
        Common.Entities.OperationLog operate = new OperationLog();
        BLLOperationLog op = new BLLOperationLog();
        BLHelper.BLLBasicCode bllBasicCode = new BLHelper.BLLBasicCode();
        private int page;
        protected void Page_Load(object sender, EventArgs e)
        {
            page = ViewState["page"] == null ? 0 : (int)ViewState["page"];
            btnSelect_All.Text = "全选";
            if (!IsPostBack)
            {
                btn_AddPlatform.OnClientClick = Window_AddPlatform.GetShowReference("AddPlatform.aspx", "新增平台信息");
                BindData();
                btnDelete.Enabled = false;
                TriggerBox.Enabled = false;
                DropDownListPlatformType.Enabled = false;
            }
        }
        //数据绑定
        public void BindData()
        {
            try
            {
                ViewState["page"] = 0;
                List<Common.Entities.Platform> list = BLLPlatform.FindPaged(Convert.ToInt32(Session["SecrecyLevel"]));
                var res = list.Skip(Grid_Platform.PageIndex * Grid_Platform.PageSize).Take(Grid_Platform.PageSize).ToList();
                Grid_Platform.RecordCount = list.Count();
                this.Grid_Platform.DataSource = res;
                this.Grid_Platform.DataBind();
                btnDelete.Enabled = false;
                TriggerBox.Enabled = false;            
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }
        //根据平台名称搜索
        public void FindByPlatformName()
        {
            try
            {
                ViewState["page"] = 1;
                List<Common.Entities.Platform> list = BLLPlatform.FindPlatformList(TriggerBox.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                Grid_Platform.RecordCount = list.Count();
                Grid_Platform.DataSource = list;
                Grid_Platform.DataBind();
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }
        //根据平台级别查询
        public void FindByPlatformType()
        {
            try
            {
                ViewState["page"] = 2;
                List<Common.Entities.Platform> list = BLLPlatform.FindPlatformRank(DropDownListPlatformType.SelectedText, Convert.ToInt32(Session["SecrecyLevel"]));
                Grid_Platform.RecordCount = list.Count();
                Grid_Platform.DataSource = list;
                Grid_Platform.DataBind();
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }
        protected void Grid_Platform_PageIndexChange(object sender, FineUI.GridPageEventArgs e)
        {
            this.Grid_Platform.PageIndex = e.NewPageIndex;
            switch (page)
            {
                case 0:
                    BindData();
                    break;
                case 1:
                    FindByPlatformName();
                    break;
                case 2:
                    FindByPlatformType();
                    break;
            }
        }
        protected void ddlGridPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid_Platform.PageIndex = 0;
            this.Grid_Platform.PageSize = Convert.ToInt32(ddlGridPageSize.SelectedValue);
            //BindData();
            switch (page)
            {
                case 0:
                    BindData();
                    break;
                case 1:
                    FindByPlatformName();
                    break;
                case 2:
                    FindByPlatformType();
                    break;
            }
        }
        protected void Grid_Platform_RowCommand(object sender, FineUI.GridCommandEventArgs e)
        {
            try
            {
                string strs = BLLUser.FindByLoginName(Session["LoginName"].ToString()).UserName;
                string Person = Grid_Platform.Rows[e.RowIndex].Values[2].ToString();
                if (Person != strs && Convert.ToInt32(Session["SecrecyLevel"]) != 5)
                {
                    string str = "您无对此行操作的权限！此行信息为" + Person + "录入，请与管理员联系!";
                    CBoxSelect.SetCheckedState(e.RowIndex, false);
                    Alert.ShowInTop(str);
                }
                if (publicmethod.GridCount(Grid_Platform, CBoxSelect).Count == 0)
                {
                    //Alert.ShowInTop("请选中需删除的数据！");
                    btnDelete.Enabled = false;
                    return;
                }
                if (publicmethod.GridCount(Grid_Platform, CBoxSelect).Count != 0)
                {
                    btnDelete.Enabled = true;
                    return;
                }
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }

        //编辑选中行
        protected void ButtonUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                List<int> selections = publicmethod.GridCount(Grid_Platform, CBoxSelect);
                if (selections.Count() != 0)
                {
                    if (selections.Count() == 1)
                    {
                        int rowID = Convert.ToInt32(Grid_Platform.DataKeys[selections[0]][0]);
                        Session["PlatformID"] = rowID;
                        Alert.Show("你确定要修改该行数据吗!", "确认消息", MessageBoxIcon.Information, Window_Update.GetShowReference("UpdatePlatform.aspx", "编辑平台信息"), Target.Top);
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
                BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
                pm.SaveError(ex, this.Request);
            }

        }
        //grid序号
        public int RowNumber(int dataItemIndex)
        {
            return dataItemIndex + (Grid_Platform.PageIndex) * Grid_Platform.PageSize;
        }
        //转化等级
        public string SecrecyLevelName(int level)
        {
            try
            {
                string[] SecrecyLevels = new string[] { "四级", "三级", "二级", "一级", "管理员" };
                //string[] SecrecyLevels = new string[] { "公开", "内部", "秘密", "机密", "管理员" };
                return SecrecyLevels[level - 1];
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
                return "";
            }
        }
        //搜索
        protected void btnCheck_Click(object sender, EventArgs e)
        {
            try
            {
                Grid_Platform.PageIndex = 0;
                switch (ddlsearch.SelectedText)
                {
                    case "全部":
                        BindData();
                        break;
                    case "平台名称":
                        if (!string.IsNullOrEmpty(TriggerBox.Text.Trim()))
                        {
                            FindByPlatformName();
                        }
                        else
                            Alert.ShowInTop("请填写查询条件！");
                        break ;
                    case "平台级别":
                            FindByPlatformType();
                        break;
                }
            }
            catch (Exception ex)
            {
                BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
                pm.SaveError(ex, this.Request);
            }
        }
        //刷新
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            btnDelete.Enabled = false;
            TriggerBox.Text = "";
            TriggerBox.Enabled = false;
            BindData();
            ddlsearch.SelectedValue  = "全部";
            DropDownListPlatformType.Enabled = false;
        }
        //删除
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                List<int> selections = publicmethod.GridCount(Grid_Platform, CBoxSelect);
                if (Convert.ToInt32(Session["SecrecyLevel"]) == 5)
                {
                    for (int i = 0; i < selections.Count(); i++)
                    {
                        BLLPlatform.Delete(Convert.ToInt32(Grid_Platform.DataKeys[selections[i]][0].ToString())); //平台表删除
                    }
                    Alert.ShowInTop("删除数据成功!");
                    btnSelect_All.Text = "全选";

                }
                else
                {
                    for (int i = 0; i < selections.Count(); i++)
                    {
                        BLLPlatform.UpdateIsPass(Convert.ToInt32(Grid_Platform.DataKeys[selections[i]][0]), false);
                        operate.LoginName = Session["LoginName"].ToString();
                        operate.OperationTime = DateTime.Now;
                        operate.LoginIP = " ";
                        operate.OperationContent = "Platform";
                        operate.OperationType = "删除";
                        operate.OperationDataID = Convert.ToInt32(Grid_Platform.DataKeys[selections[i]][0]);
                        op.Insert(operate);
                    }
                    Alert.ShowInTop("操作已经提交，请等待管理员确认!");
                    btnSelect_All.Text = "全选";
                }
                BindData();
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }
        //搜素框变化
        protected void ddlsearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (ddlsearch.SelectedText)
            {
                case "全部":
                    TriggerBox.Text = "";
                    TriggerBox.Enabled = false;
                    DropDownListPlatformType.Items.Clear();
                    DropDownListPlatformType.Enabled = false;
                    break;
                case "平台名称":
                    TriggerBox.Text = "";
                    TriggerBox.Enabled = true;
                    break;
                case "平台级别":
                    TriggerBox.Text = "";
                    TriggerBox.Enabled = false;
                    DropDownListPlatformType.Enabled = true;
                    DropDownListPlatformType.Items.Clear();
                    List<BasicCode> list = bllBasicCode.FindALLName("平台级别");
                    for (int i = 0; i < list.Count(); i++)
                    {
                        DropDownListPlatformType.Items.Add(list[i].CategoryContent.ToString(), list[i].CategoryContent.ToString());
                    }
                    break;
            }
        }

        //lby 下载相关文档
        protected string GetEditUrl(object ID)
        {
            return DownLoad.GetShowReference("Operate.aspx?id=" + ID, "操作");
        }

        //lby 显示成员
        protected string GetEditUrlmem(object ID)
        {
            return PlatformMemberWindow.GetShowReference("PlatformMember.aspx?id=" + ID, "平台成员信息");
        }

        //lby 显示管理
        protected string GetEditUrlmana(object ID)
        {
            return PlatformManagementWindow.GetShowReference("PlatformManagement.aspx?id=" + ID, "平台管理信息");
        }

        //全选按钮
        protected void btnSelect_All_Click(object sender, EventArgs e)
        {
            Grid_Platform.SelectAllRows();
            int[] select = Grid_Platform.SelectedRowIndexArray;
            int m;
            //取整数（不是四舍五入，全舍）
            int Pages = (int)Math.Floor(Convert.ToDouble(Grid_Platform.RecordCount / this.Grid_Platform.PageSize));

            if (Grid_Platform.PageIndex == Pages)
                m = (Grid_Platform.RecordCount - this.Grid_Platform.PageSize * Grid_Platform.PageIndex);
            else
                m = this.Grid_Platform.PageSize;
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