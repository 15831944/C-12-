/**编写人：王会会
 * 时间：2014年8月13号
 * 功能：人员社会兼职信息的相关操作
 * 修改履历：1.修改人：陈起明
 *            修改时间：10月10日
 *            修改内容：撤消了静态变量page
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
    public partial class part_time_job : System.Web.UI.Page
    {
        BLHelper.BLLUser bllUser = new BLHelper.BLLUser();
        BLHelper.BLLSocialPartTime bllSocial = new BLHelper.BLLSocialPartTime();
        BLHelper.BLLOperationLog bllOperate = new BLHelper.BLLOperationLog();
        BLCommon.PublicMethod publicmethod = new BLCommon.PublicMethod();
        BLHelper.BLLBasicCode bllBasicCode = new BLHelper.BLLBasicCode();
        private int page;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                page = ViewState["page"] == null ? 0 : (int)ViewState["page"];
                //添加
                btnAdd.OnClientClick = WindowAdd.GetShowReference("Add_PartTimeJob.aspx");
                //删除数据
                //btnDelete.OnClientClick = GridSocialPartTime.GetNoSelectionAlertReference("请至少选择一项！");
                //btnDelete.ConfirmText = String.Format("你确定要删除该行数据吗？", GridSocialPartTime.GetSelectedCellReference());
                //修改数据
                Btnchange.OnClientClick = GridSocialPartTime.GetNoSelectionAlertReference("请至少选择一项！");
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
                List<SocialPartTime> SocialList = bllSocial.FindPaged(Convert.ToInt32(Session["SecrecyLevel"])).ToList();
                GridSocialPartTime.RecordCount = SocialList.Count();
                var result = SocialList.Skip(GridSocialPartTime.PageIndex * GridSocialPartTime.PageSize).Take(GridSocialPartTime.PageSize).ToList();
                this.GridSocialPartTime.DataSource = result;
                this.GridSocialPartTime.DataBind();
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }
        //根据人员姓名查看社会兼职信息(模糊查询)
        public void SelectByName()
        {
            try
            {
                ViewState["page"] = 1;
                List<int> UserIDlist = new List<int>();
                //根据人员名称模糊查找人员ID
                UserIDlist = bllUser.FindList(TBNameandAgency.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                List<SocialPartTime> alist = new List<SocialPartTime>();
                for (int i = 0; i < UserIDlist.Count(); i++)
                {
                    //根据人员ID查找社会兼职
                    List<SocialPartTime> SocialList = bllSocial.SelectByID(UserIDlist[i], Convert.ToInt32(Session["SecrecyLevel"])).ToList();
                    for (int j = 0; j < SocialList.Count(); j++)
                    {
                        alist.Add(SocialList[j]);
                    }
                }

                GridSocialPartTime.RecordCount = alist.Count();
                var result = alist.Skip(GridSocialPartTime.PageIndex * GridSocialPartTime.PageSize).Take(GridSocialPartTime.PageSize).ToList();
                this.GridSocialPartTime.DataSource = result;
                this.GridSocialPartTime.DataBind();
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }
        //根据时间查询社会兼职信息
        public void SelectByApproveTime()
        {
            try
            {
                ViewState["page"] = 2;
                List<SocialPartTime> SocialList = bllSocial.SelectByApproveTime(Convert.ToInt32(DropDownListYearandLevel.SelectedItem.Text), Convert.ToInt32(Session["SecrecyLevel"])).ToList();
                GridSocialPartTime.RecordCount = SocialList.Count();
                var result = SocialList.Skip(GridSocialPartTime.PageIndex * GridSocialPartTime.PageSize).Take(GridSocialPartTime.PageSize).ToList();
                this.GridSocialPartTime.DataSource = result;
                this.GridSocialPartTime.DataBind();
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }
        //根据授予部门查询社会兼职信息(模糊查询)
        public void SelectByDepartments()
        {
            try
            {
                ViewState["page"] = 3;
                List<SocialPartTime> SocialList = bllSocial.SelectByDepartment(TBNameandAgency.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"])).ToList();
                GridSocialPartTime.RecordCount = SocialList.Count();
                var result = SocialList.Skip(GridSocialPartTime.PageIndex * GridSocialPartTime.PageSize).Take(GridSocialPartTime.PageSize).ToList();
                this.GridSocialPartTime.DataSource = result;
                this.GridSocialPartTime.DataBind();
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }
        //根据级别名称查询社会兼职信息
        public void SelectByLevel()
        {
            try
            {
                ViewState["page"] = 4;
                List<SocialPartTime> SocialList = bllSocial.SelectByLevel(DropDownListYearandLevel.SelectedItem.Text, Convert.ToInt32(Session["SecrecyLevel"])).ToList();
                GridSocialPartTime.RecordCount = SocialList.Count();
                var result = SocialList.Skip(GridSocialPartTime.PageIndex * GridSocialPartTime.PageSize).Take(GridSocialPartTime.PageSize).ToList();
                this.GridSocialPartTime.DataSource = result;
                this.GridSocialPartTime.DataBind();
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }
        //查询
        protected void Find_Click(object sender, EventArgs e)
        {
            GridSocialPartTime.PageIndex = 0;
            switch (DropDownListFind.SelectedItem.Text)
            {
                case "全部":
                    //Alert.ShowInTop("请填写查询条件");
                    BindData();
                    break;
                case "人员姓名":
                    if (!string.IsNullOrEmpty(TBNameandAgency.Text.Trim()))
                    {
                        if (TBNameandAgency.Text.Trim().Length <= 20)
                        {
                            //if (bllUser.IsUser(TBNameandAgency.Text.ToString()) != null)
                                SelectByName();
                            //else
                            //    Alert.ShowInTop("人员姓名不存在！");
                        }
                        else
                            Alert.ShowInTop("最多输入20个字符！");
                    }
                    else
                        Alert.ShowInTop("请填写查询条件！");
                    break;
                case "时间":
                    SelectByApproveTime();
                    break;
                case "授予部门":
                    if (!string.IsNullOrEmpty(TBNameandAgency.Text.Trim()))
                    {
                        if (TBNameandAgency.Text.Trim().Length <= 20)
                        {
                            SelectByDepartments();
                        }
                        else
                            Alert.ShowInTop("最多输入20个字符！");
                    }
                    else
                        Alert.ShowInTop("请填写查询条件！");
                    break;
                case "级别名称":
                    SelectByLevel();
                    break;
            }
        }
        //删除
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                List<int> selections = publicmethod.GridCount(GridSocialPartTime, CBoxSelect);
                if (Convert.ToInt32(Session["SecrecyLevel"]) == 5)
                {
                    for (int i = 0; i < selections.Count(); i++)
                    {
                        bllSocial.Delete(Convert.ToInt32(GridSocialPartTime.DataKeys[selections[i]][0]));
                    }
                    BindData();
                    Alert.ShowInTop("删除数据成功!");
                }
                else
                {
                    for (int i = 0; i < selections.Count(); i++)
                    {
                        bllSocial.UpdateIsPass(Convert.ToInt32(GridSocialPartTime.DataKeys[selections[i]][0]), false);
                        //向操作日志表中插入
                        OperationLog operate = new OperationLog();
                        operate.LoginName = bllUser.FindByLoginName(Session["LoginName"].ToString()).UserName;
                        operate.LoginIP = "";
                        operate.OperationType = "删除";
                        operate.OperationContent = "SocialPartTime";
                        operate.OperationDataID = Convert.ToInt32(GridSocialPartTime.DataKeys[selections[i]][0]);
                        operate.OperationTime = System.DateTime.Now;
                        operate.Remark = "";
                        bllOperate.Insert(operate);                      
                    }
                    Alert.ShowInTop("您的操作已提交，请等待审核！");
                    BindData();
                }
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }
        //修改
        protected void Btnchange_Click(object sender, EventArgs e)
        {
            try
            {
                List<int> selections = publicmethod.GridCount(GridSocialPartTime, CBoxSelect);
                if (selections.Count() != 0)
                {
                    if (selections.Count() == 1)
                    {
                        int rowID = Convert.ToInt32(GridSocialPartTime.DataKeys[selections[0]][0]);
                        Session["SocialPartTimeID"] = rowID;

                        Alert.Show("你确定要修改该行数据吗!", "确认消息", MessageBoxIcon.Information, WindowUpdate.GetShowReference("Update_PartTimeJob.aspx"), Target.Top);
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
        //根据人员ID找人员名称
        public string UserName(int UserID)
        {
            try
            {
                if (UserID != 0)
                    return bllUser.FindByUserID(UserID);
                else
                    return "";
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
                return "";
            }
        }
       // GridSocialPartTime行命令
        protected void GridSocialPartTime_RowCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                List<int> selections = publicmethod.GridCount(GridSocialPartTime, CBoxSelect);
                string Person = GridSocialPartTime.Rows[e.RowIndex].Values[2].ToString();
                string strs = bllUser.FindByLoginName(Session["LoginName"].ToString()).UserName;

                if (Person != strs && Convert.ToInt32(Session["SecrecyLevel"]) != 5)
                {
                    string str = "您无对此行操作的权限！此行信息为" + Person + "录入，请与管理员联系!";
                    CBoxSelect.SetCheckedState(e.RowIndex, false);
                    Alert.ShowInTop(str);
                }
                if (selections.Count == 0)
                {
                    //Alert.ShowInTop("请选中需删除的数据！");
                    btnDelete.Enabled = false;
                    return;
                }
                if (selections.Count != 0)
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
        //分页页数
        protected void GridSocialPartTime_PageIndexChange(object sender, GridPageEventArgs e)
        {
            GridSocialPartTime.PageIndex = e.NewPageIndex;
            switch (page )
            {
                case 0:
                    BindData();
                    break;
                case 1:
                    SelectByName();
                    break;
                case 2:
                    SelectByApproveTime();
                    break;
                case 3:
                    SelectByDepartments();
                    break;
                case 4:
                    SelectByLevel();
                    break;
            }
        }
        //分页每页项的个数
        protected void ddlGridPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridSocialPartTime.PageIndex = 0;
            this.GridSocialPartTime.PageSize = Convert.ToInt32(ddlGridPageSize.SelectedValue);
            switch (page)
            {
                case 0:
                    BindData();
                    break;
                case 1:
                    SelectByName();
                    break;
                case 2:
                    SelectByApproveTime();
                    break;
                case 3:
                    SelectByDepartments();
                    break;
                case 4:
                    SelectByLevel();
                    break;
            }
            
        }
        //刷新
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            BindData();
            DropDownListFind.SelectedValue = "全部";
            TBNameandAgency.Reset();
            DropDownListYearandLevel.Items.Clear();
            TBNameandAgency.Enabled = false;
            DropDownListYearandLevel.Enabled = false;
        }
        //备注界面跳转
        protected string GetEditUrl(object SocialPartTimeID)
        {
            return Remark.GetShowReference("Remark_Window.aspx?id=" + SocialPartTimeID, "备注");
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
                case "时间":
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
                        List<SocialPartTime> SocialList = bllSocial.FindPaged(Convert.ToInt32(Session["SecrecyLevel"])).ToList();

                        this.GridSocialPartTime.DataSource = SocialList;
                        this.GridSocialPartTime.DataBind();
                        break;
                    case 1:
                        List<int> UserIDlist = new List<int>();
                        //根据人员名称模糊查找人员ID
                        UserIDlist = bllUser.FindList(TBNameandAgency.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                        List<SocialPartTime> alist = new List<SocialPartTime>();
                        for (int i = 0; i < UserIDlist.Count(); i++)
                        {
                            //根据人员ID查找社会兼职
                            List<SocialPartTime> SocialLists = bllSocial.SelectByID(UserIDlist[i], Convert.ToInt32(Session["SecrecyLevel"])).ToList();
                            for (int j = 0; j < SocialLists.Count(); j++)
                            {
                                alist.Add(SocialLists[j]);
                            }
                        }
                        this.GridSocialPartTime.DataSource = alist;
                        this.GridSocialPartTime.DataBind();
                        break;
                    case 2:
                        List<SocialPartTime> saocialList = bllSocial.SelectByApproveTime(Convert.ToInt32(DropDownListYearandLevel.SelectedItem.Text), Convert.ToInt32(Session["SecrecyLevel"])).ToList();
                        this.GridSocialPartTime.DataSource = saocialList;
                        this.GridSocialPartTime.DataBind();
                        break;
                    case 3:
                        List<SocialPartTime> List1 = bllSocial.SelectByDepartment(TBNameandAgency.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"])).ToList();
                        this.GridSocialPartTime.DataSource = List1;
                        this.GridSocialPartTime.DataBind();
                        break;
                    case 4:
                        List<SocialPartTime> List = bllSocial.SelectByLevel(DropDownListYearandLevel.SelectedItem.Text, Convert.ToInt32(Session["SecrecyLevel"])).ToList();
                        this.GridSocialPartTime.DataSource = List;
                        this.GridSocialPartTime.DataBind();
                        break;
                }
                publicmethod.ExportExcel(3, GridSocialPartTime, 1);
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }
        public int RowNumber(int dataItemIndex)
        {
            return dataItemIndex + (GridSocialPartTime.PageIndex) * GridSocialPartTime.PageSize;
        }
    }
}