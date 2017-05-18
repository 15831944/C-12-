/**编写人：王会会
 * 时间：2014年8月16号
 * 功能：人员学历信息的相关操作
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

namespace WDFramework.People.Educations
{
    public partial class Educations : System.Web.UI.Page
    {
        BLHelper.BLLUser bllUser = new BLHelper.BLLUser();
        BLHelper.BLLEducation bllEducation = new BLHelper.BLLEducation();
        BLHelper.BLLOperationLog bllOperate = new BLHelper.BLLOperationLog();
        BLCommon.PublicMethod publicmethod = new BLCommon.PublicMethod();
        private int page;
        protected void Page_Load(object sender, EventArgs e)
        {
            page = ViewState["page"] == null ? 0 : (int)ViewState["page"];
            btnSelect_All.Text = "全选";
            if (!IsPostBack)
            {
                //添加
                btnAdd.OnClientClick = WindowAdd.GetShowReference("Add_Education.aspx");
                BindData();
                btnDelete.Enabled = false;
            }
        }
        public void BindData()
        {
            try
            {
                ViewState["page"] = 0;
                List<Education> EducationsList = bllEducation.FindPaged(Convert.ToInt32(Session["SecrecyLevel"])).ToList();
                GridEducation.RecordCount = EducationsList.Count();
                var result = EducationsList.Skip(GridEducation.PageIndex * GridEducation.PageSize).Take(GridEducation.PageSize).ToList();
                this.GridEducation.DataSource = result;
                this.GridEducation.DataBind();
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request); ;
            }
        }
        //根据人员名称模糊查找学历
        public void SelectByID()
        {
            try
            {
                ViewState["page"] = 1;
                List<int> UserIDlist = new List<int>();
                //根据人员名称模糊查找人员ID
                UserIDlist = bllUser.FindList(TriggerBox.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                List<Education> alist = new List<Education>();
                for (int i = 0; i < UserIDlist.Count(); i++)
                {
                    //根据人员ID查找学历
                    List<Education> EducationsList = bllEducation.SelectByID(UserIDlist[i], Convert.ToInt32(Session["SecrecyLevel"])).ToList();
                    for (int j = 0; j < EducationsList.Count(); j++)
                    {
                        alist.Add(EducationsList[j]);
                    }
                }
                GridEducation.RecordCount = alist.Count();
                var result = alist.Skip(GridEducation.PageIndex * GridEducation.PageSize).Take(GridEducation.PageSize).ToList();
                this.GridEducation.DataSource = result;
                this.GridEducation.DataBind();
                btnDelete.Enabled = false;
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
       // GridEducation行命令
        protected void GridEducation_RowCommand(object sender, FineUI.GridCommandEventArgs e)
        {
            try
            {
                string Person = GridEducation.Rows[e.RowIndex].Values[2].ToString();
                string strs = bllUser.FindByLoginName(Session["LoginName"].ToString()).UserName;

                if (Person != strs && Convert.ToInt32(Session["SecrecyLevel"]) != 5)
                {
                    string str = "您无对此行操作的权限！此行信息为" + Person + "录入，请与管理员联系!";
                    CBoxSelect.SetCheckedState(e.RowIndex, false);
                    Alert.ShowInTop(str);
                }
                if (publicmethod.GridCount(GridEducation, CBoxSelect).Count == 0)
                {
                    //Alert.ShowInTop("请选中需删除的数据！");
                    btnDelete.Enabled = false;
                    return;
                }
                if (publicmethod.GridCount(GridEducation, CBoxSelect).Count != 0)
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
        protected void GridEducation_PageIndexChange(object sender, FineUI.GridPageEventArgs e)
        {
            GridEducation.PageIndex = e.NewPageIndex;
            switch (page)
            {
                case 0:
                    BindData();
                    break;
                case 1:
                    SelectByID();
                    break;
            }
        }
        protected void ddlGridPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridEducation.PageIndex = 0;
            this.GridEducation.PageSize = Convert.ToInt32(ddlGridPageSize.SelectedValue);
            switch (page)
            {
                case 0:
                    BindData();
                    break;
                case 1:
                    SelectByID();
                    break;           
            }
        }
        //查询
        protected void Find_Click(object sender, EventArgs e)
        {
            GridEducation.PageIndex = 0;
            if (!string.IsNullOrEmpty(TriggerBox.Text.Trim()))
            {
                if (TriggerBox.Text.Trim().Length < 20)
                {
                    SelectByID();
                }
                else
                    Alert.ShowInTop("最多输入20个字符！");
            }
            else
                Alert.ShowInTop("请填写查询条件！");
        }
        //删除
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                List<int> selections = publicmethod.GridCount(GridEducation, CBoxSelect);
                if (Convert.ToInt32(Session["SecrecyLevel"]) == 5)
                {
                    for (int i = 0; i < selections.Count(); i++)
                    {
                        bllEducation.Delete(Convert.ToInt32(GridEducation.DataKeys[selections[i]][0]));
                    }
                    BindData();
                    btnSelect_All.Text = "全选";
                    Alert.ShowInTop("删除成功!");
                }
                else
                {
                    for (int i = 0; i < selections.Count(); i++)
                    {
                        bllEducation.UpdateIsPass(Convert.ToInt32(GridEducation.DataKeys[selections[i]][0]), false);
                        //向操作日志表中插入
                        OperationLog operate = new OperationLog();
                        operate.LoginName = bllUser.FindByLoginName(Session["LoginName"].ToString()).UserName;
                        operate.LoginIP = "";
                        operate.OperationType = "删除";
                        operate.OperationContent = "Education";
                        operate.OperationDataID = Convert.ToInt32(GridEducation.DataKeys[selections[i]][0]);
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
        //修改
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                List<int> selections = publicmethod.GridCount(GridEducation, CBoxSelect);
                if (selections.Count() != 0)
                {
                    if (selections.Count() == 1)
                    {
                        int rowID = Convert.ToInt32(GridEducation.DataKeys[selections[0]][0]);
                        Session["EducationID"] = rowID;

                        Alert.Show("你确定要修改该行数据吗!", "确认消息", MessageBoxIcon.Information, WindowUpdate.GetShowReference("Update_Education.aspx"), Target.Top);
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
        //刷新
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            BindData();
            btnDelete.Enabled = false;
            TriggerBox.Text = "";
        }
        //涉密等级名称
        public string SecrecyLevelName(int level)
        {
            try
            {
                string[] SecrecyLevels = new string[] { "四级", "三级", "二级", "一级", "管理员" };
               // string[] SecrecyLevels = new string[] { "公开", "内部", "秘密", "机密", "管理员" };
                return SecrecyLevels[level - 1];
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
                return "";
            }
        }
        //导出
        protected void btn_Get_Click(object sender, EventArgs e)
        {
            try
            {
                if (page == 0)
                {
                    List<Education> EducationsList = bllEducation.FindPaged(Convert.ToInt32(Session["SecrecyLevel"])).ToList();
                    this.GridEducation.DataSource = EducationsList;
                    this.GridEducation.DataBind();
                }
                if (page == 1)
                {
                    List<int> UserIDlist = new List<int>();
                    //根据人员名称模糊查找人员ID
                    UserIDlist = bllUser.FindList(TriggerBox.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                    List<Education> alist = new List<Education>();
                    for (int i = 0; i < UserIDlist.Count(); i++)
                    {
                        //根据人员ID查找学历
                        List<Education> EducationsList = bllEducation.SelectByID(UserIDlist[i], Convert.ToInt32(Session["SecrecyLevel"])).ToList();
                        for (int j = 0; j < EducationsList.Count(); j++)
                        {
                            alist.Add(EducationsList[j]);
                        }
                    }
                    GridEducation.RecordCount = alist.Count();
                    var result = alist.Skip(GridEducation.PageIndex * GridEducation.PageSize).Take(GridEducation.PageSize).ToList();
                    this.GridEducation.DataSource = result;
                    this.GridEducation.DataBind();
                }
                publicmethod.ExportExcel(3, GridEducation, 0);
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }
        public int RowNumber(int dataItemIndex)
        {
            return dataItemIndex + (GridEducation.PageIndex) * GridEducation.PageSize;
        }
        //全选按钮
        protected void btnSelect_All_Click(object sender, EventArgs e)
        {
            GridEducation.SelectAllRows();
            int[] select = GridEducation.SelectedRowIndexArray;
            int m;
            //取整数（不是四舍五入，全舍）
            int Pages = (int)Math.Floor(Convert.ToDouble(GridEducation.RecordCount / this.GridEducation.PageSize));

            if (GridEducation.PageIndex == Pages)
                m = (GridEducation.RecordCount - this.GridEducation.PageSize * GridEducation.PageIndex);
            else
                m = this.GridEducation.PageSize;
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