/**编写人：王会会
 * 时间：2014年8月15号
 * 功能：人员荣誉称号信息的相关操作
 * 修改履历：1.修改人：陈起明
 *          2.修改时间：10月10日
 *          3.修改内容：撤消了静态变量page
 **/
using Common.Entities;
using FineUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WDFramework.People
{
    public partial class honorarytitle : System.Web.UI.Page
    {
        BLHelper.BLLHonor bllHonor = new BLHelper.BLLHonor();
        BLHelper.BLLOperationLog bllOperate = new BLHelper.BLLOperationLog();
        BLHelper.BLLUser bllUser = new BLHelper.BLLUser();
        BLCommon.PublicMethod publicmethod = new BLCommon.PublicMethod();
        BLHelper.BLLBasicCode bllBasicCode = new BLHelper.BLLBasicCode();
        private int page;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                page = ViewState["page"] == null ? 0 : (int)ViewState["page"];
                //添加数据
                btnAddProject.OnClientClick = WindowHonor.GetShowReference("Add_HonorTitle.aspx");
                //修改数据
                btnUpdate.OnClientClick = GridHonor.GetNoSelectionAlertReference("请至少选择一项！");
                BindData();
                btnDelete.Enabled = false;
                DropDownListYearandLevel.Enabled = false;
                TBNameandAgency.Enabled = false;
            }
        }
        public void BindData()
        {
            try
            {
                ViewState["page"] = 0;
                List<Common.Entities.Honor> HonorList = bllHonor.FindPaged(Convert.ToInt32(Session["SecrecyLevel"]));
                GridHonor.RecordCount = HonorList.Count();
                var result = HonorList.Skip(GridHonor.PageIndex * GridHonor.PageSize).Take(GridHonor.PageSize).ToList();
                this.GridHonor.DataSource = result;
                this.GridHonor.DataBind();
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request); ;
            }
        }
        //根据人员ID查找荣誉称号（模糊查询）
        public void SelectByID()
        {
            try
            {
                ViewState["page"] = 1;
                List<int> UserIDlist = new List<int>();
                //根据人员名称模糊查找人员ID
                UserIDlist = bllUser.FindList(TBNameandAgency.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                List<Common.Entities.Honor> alist = new List<Common.Entities.Honor>();
                for (int i = 0; i < UserIDlist.Count(); i++)
                {
                    //根据人员ID查找荣誉称号
                    List<Common.Entities.Honor> HonorList = bllHonor.SelectByID(UserIDlist[i], Convert.ToInt32(Session["SecrecyLevel"]));
                    for (int j = 0; j < HonorList.Count(); j++)
                    {
                        alist.Add(HonorList[j]);
                    }
                }
                GridHonor.RecordCount = alist.Count();
                var result = alist.Skip(GridHonor.PageIndex * GridHonor.PageSize).Take(GridHonor.PageSize).ToList();
                this.GridHonor.DataSource = result;
                this.GridHonor.DataBind();
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }
        //根据授予时间查询
        public void SelectByTime()
        {
            try
            {
                ViewState["page"] = 2;
                List<Common.Entities.Honor> HonorList = bllHonor.SelectByTime(Convert.ToInt32(DropDownListYearandLevel.SelectedItem.Text), Convert.ToInt32(Session["SecrecyLevel"]));
                GridHonor.RecordCount = HonorList.Count();
                var result = HonorList.Skip(GridHonor.PageIndex * GridHonor.PageSize).Take(GridHonor.PageSize).ToList();
                this.GridHonor.DataSource = result;
                this.GridHonor.DataBind();
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }
        //根据授予部门查询（模糊查询）
        public void SelectByDivision()
        {
            try
            {
                ViewState["page"] = 3;
                List<Common.Entities.Honor> HonorList = bllHonor.SelectByDivision(TBNameandAgency.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                GridHonor.RecordCount = HonorList.Count();
                var result = HonorList.Skip(GridHonor.PageIndex * GridHonor.PageSize).Take(GridHonor.PageSize).ToList();
                this.GridHonor.DataSource = result;
                this.GridHonor.DataBind();
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }
        //根据(分类)级别查询
        public void SelectBySort()
        {
            try
            {
                ViewState["page"] = 4;
                List<Common.Entities.Honor> HonorList = bllHonor.SelectBySort(DropDownListYearandLevel.SelectedItem.Text, Convert.ToInt32(Session["SecrecyLevel"]));
                GridHonor.RecordCount = HonorList.Count();
                var result = HonorList.Skip(GridHonor.PageIndex * GridHonor.PageSize).Take(GridHonor.PageSize).ToList();
                this.GridHonor.DataSource = result;
                this.GridHonor.DataBind();
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }
        //查询
        protected void FindDevoteTime_Click(object sender, EventArgs e)
        {
            GridHonor.PageIndex = 0;
            switch (DropDownListFind.SelectedItem.Text)
            {
                case "全部":
                    BindData();
                    //Alert.ShowInTop("请选择查询条件！");
                    break;
                case "人员姓名":
                    if (!string.IsNullOrEmpty(TBNameandAgency.Text.Trim().Trim()))
                    {
                        if (TBNameandAgency.Text.Trim().Length < 20)
                        {
                            //if (bllUser.IsUser(TBNameandAgency.Text.ToString()) != null)
                                SelectByID();
                            //else
                            //    Alert.ShowInTop("人员姓名不存在！");
                        }
                        else
                            Alert.ShowInTop("最多输入20个字符！");
                    }
                    else
                        Alert.ShowInTop("请填写查询条件");
                    break;
                case "授予时间":
                    SelectByTime();
                    break;
                case "级别名称":
                    SelectBySort();
                    break;
                case "授予部门":
                    if (!string.IsNullOrEmpty(TBNameandAgency.Text.Trim()))
                    {
                        if (TBNameandAgency.Text.Trim().Length < 20)
                            SelectByDivision();
                        else
                            Alert.ShowInTop("最多输入20个字符！");
                    }
                    else
                        Alert.ShowInTop("请填写查询条件");
                    break;
            }

        }
        //刷新
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            BindData();
            btnDelete.Enabled = false;
            TBNameandAgency.Reset();
            DropDownListYearandLevel.Reset();
            DropDownListFind.SelectedValue = "全部";
            TBNameandAgency.Enabled = false;
            DropDownListYearandLevel.Enabled = false;
        }
        //删除
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                List<int> selections = publicmethod.GridCount(GridHonor, CBoxSelect);
                if (Convert.ToInt32(Session["SecrecyLevel"]) == 5)
                {
                    for (int i = 0; i < selections.Count(); i++)
                    {
                        bllHonor.Delete(Convert.ToInt32(GridHonor.DataKeys[selections[i]][0]));
                    }
                    BindData();
                    btnSelect_All.Text = "全选";
                    Alert.ShowInTop("删除数据成功!");
                }
                else
                {
                    for (int i = 0; i < selections.Count(); i++)
                    {
                        bllHonor.UpdateIsPass(Convert.ToInt32(GridHonor.DataKeys[selections[i]][0]), false);
                        //向操作日志表中插入
                        OperationLog operate = new OperationLog();
                        operate.LoginName = bllUser.FindByLoginName(Session["LoginName"].ToString()).UserName;
                        operate.LoginIP = "";
                        operate.OperationType = "删除";
                        operate.OperationContent = "Honor";
                        operate.OperationDataID = Convert.ToInt32(GridHonor.DataKeys[selections[i]][0]);
                        operate.OperationTime = System.DateTime.Now;
                        operate.Remark = "";
                        bllOperate.Insert(operate);                        
                    }
                    btnSelect_All.Text = "全选";
                    Alert.ShowInTop("您的操作已提交，请等待审核！");
                    BindData();
                }
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }
        //更新
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                List<int> selections = publicmethod.GridCount(GridHonor, CBoxSelect);
                if (selections.Count() != 0)
                {
                    if (selections.Count() == 1)
                    {
                        int rowID = Convert.ToInt32(GridHonor.DataKeys[selections[0]][0]);
                        Session["HonorID"] = rowID;
                        Alert.Show("你确定要修改该行数据吗!", "确认消息", MessageBoxIcon.Information, WindowUpdate.GetShowReference("Update_HonorTitle.aspx"), Target.Top);
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
                publicmethod.SaveError(ex, this.Request);
            }
        }
        //根据UserID查找Username
        public string UserName(int UserID)
        {
            try
            {
                if (UserID != 0)
                    return bllUser.FindUserName(UserID);
                else
                    return "";
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
                return "";
            }
        }
        //GridProjectStudent行命令
        protected void GridHonor_RowCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                string Person = GridHonor.Rows[e.RowIndex].Values[2].ToString();
                string strs = bllUser.FindByLoginName(Session["LoginName"].ToString()).UserName;

                if (Person != strs && Convert.ToInt32(Session["SecrecyLevel"]) != 5)
                {
                    string str = "您无对此行操作的权限！此行信息为" + Person + "录入，请与管理员联系!";
                    CBoxSelect.SetCheckedState(e.RowIndex, false);
                    Alert.ShowInTop(str);
                }
                if (publicmethod.GridCount(GridHonor, CBoxSelect).Count == 0)
                {
                    //Alert.ShowInTop("请选中需删除的数据！");
                    btnDelete.Enabled = false;
                    return;
                }
                if (publicmethod.GridCount(GridHonor, CBoxSelect).Count != 0)
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
        //分页
        protected void GridHonor_PageIndexChange(object sender, GridPageEventArgs e)
        {
            GridHonor.PageIndex = e.NewPageIndex;
            switch (page)
            {
                case 0:
                    BindData();
                    break;
                case 1:
                    SelectByID ();
                    break;
                case 2:
                    SelectByTime();
                    break;
                case 3:
                    SelectBySort();
                    break;
                case 4:
                    SelectByDivision();
                    break;
            }
        }

        protected void ddlGridPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridHonor.PageIndex = 0;
            this.GridHonor.PageSize = Convert.ToInt32(ddlGridPageSize.SelectedValue);
            switch (page)
            {
                case 0:
                    BindData();
                    break;
                case 1:
                    SelectByID();
                    break;
                case 2:
                    SelectByTime();
                    break;
                case 3:
                    SelectBySort();
                    break;
                case 4:
                    SelectByDivision();
                    break;
            }
        }
        //备注界面跳转
        protected string GetEditUrl(object HonorID)
        {
            return Remark.GetShowReference("Remark_Window.aspx?id=" + HonorID, "备注");
        }
        //涉密等级名称
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
        //搜索框变化
        protected void DropDownListFind_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (DropDownListFind.SelectedItem.Text)
            {
                case "全部":
                    TBNameandAgency.Text = "";
                    DropDownListYearandLevel.Items.Clear();
                    DropDownListYearandLevel.Enabled = false;
                    TBNameandAgency.Enabled = false;
                    break;
                case "人员姓名":
                    TBNameandAgency.Text = "";
                    DropDownListYearandLevel.Items.Clear();
                    DropDownListYearandLevel.Enabled = false;
                    TBNameandAgency.Enabled = true;
                    break;
                case "授予时间":
                    TBNameandAgency.Text = "";
                    DropDownListYearandLevel.Items.Clear();
                    for (int i = 1960; i <= 2060; i++)
                    {
                        DropDownListYearandLevel.Items.Add(i.ToString(), i.ToString());
                    }
                    DropDownListYearandLevel.EnableEdit = false;
                    DropDownListYearandLevel.Enabled = true;
                    TBNameandAgency.Enabled = false;
                    break;
                case "授予部门":
                    TBNameandAgency.Text = "";
                    DropDownListYearandLevel.Items.Clear();
                    DropDownListYearandLevel.Enabled = false;
                    TBNameandAgency.Enabled = true;
                    break;
                case "级别名称":
                    TBNameandAgency.Text = "";
                    DropDownListYearandLevel.Items.Clear();
                    List<BasicCode> list = bllBasicCode.FindALLName("级别");
                    for (int i = 0; i < list.Count(); i++)
                    {
                        DropDownListYearandLevel.Items.Add(list[i].CategoryContent.ToString(), list[i].CategoryContent.ToString());
                    }
                    DropDownListYearandLevel.EnableEdit = false;
                    DropDownListYearandLevel.Enabled = true;
                    TBNameandAgency.Enabled = false;
                    break;
            }
        }
        //导出
        protected void btn_Get_Click(object sender, EventArgs e)
        {
            try
            {
                switch (page)
                {
                    case 0:
                        List<Common.Entities.Honor> HonorList = bllHonor.FindPaged(Convert.ToInt32(Session["SecrecyLevel"]));
                        this.GridHonor.DataSource = HonorList;
                        this.GridHonor.DataBind();
                        break;
                    case 1:
                        List<int> UserIDlist = new List<int>();
                        //根据人员名称模糊查找人员ID
                        UserIDlist = bllUser.FindList(TBNameandAgency.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                        List<Common.Entities.Honor> alist = new List<Common.Entities.Honor>();
                        for (int i = 0; i < UserIDlist.Count(); i++)
                        {
                            //根据人员ID查找荣誉称号
                            List<Common.Entities.Honor> HonorLists = bllHonor.SelectByID(UserIDlist[i], Convert.ToInt32(Session["SecrecyLevel"]));
                            for (int j = 0; j < HonorLists.Count(); j++)
                            {
                                alist.Add(HonorLists[j]);
                            }
                        }
                        this.GridHonor.DataSource = alist;
                        this.GridHonor.DataBind();
                        break;
                    case 2:
                        List<Common.Entities.Honor> Honorlist = bllHonor.SelectByTime(Convert.ToInt32(DropDownListYearandLevel.SelectedItem.Text), Convert.ToInt32(Session["SecrecyLevel"]));
                        this.GridHonor.DataSource = Honorlist;
                        this.GridHonor.DataBind();
                        break;
                    case 3:
                        List<Common.Entities.Honor> List = bllHonor.SelectByDivision(TBNameandAgency.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                        this.GridHonor.DataSource = List;
                        this.GridHonor.DataBind();
                        break;
                    case 4:
                        List<Common.Entities.Honor> list = bllHonor.SelectBySort(DropDownListYearandLevel.SelectedItem.Text, Convert.ToInt32(Session["SecrecyLevel"]));
                        this.GridHonor.DataSource = list;
                        this.GridHonor.DataBind();
                        break;
                }
                publicmethod.ExportExcel(3, GridHonor, 1);
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }
        public int RowNumber(int dataItemIndex)
        {
            return dataItemIndex + (GridHonor.PageIndex) * GridHonor.PageSize;
        }

        //全选按钮
        protected void btnSelect_All_Click(object sender, EventArgs e)
        {
            GridHonor.SelectAllRows();
            int[] select = GridHonor.SelectedRowIndexArray;
            int m;
            //取整数（不是四舍五入，全舍）
            int Pages = (int)Math.Floor(Convert.ToDouble(GridHonor.RecordCount / this.GridHonor.PageSize));

            if (GridHonor.PageIndex == Pages)
                m = (GridHonor.RecordCount - this.GridHonor.PageSize * GridHonor.PageIndex);
            else
                m = this.GridHonor.PageSize;
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