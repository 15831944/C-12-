/**编写人：王会会
 * 时间：2014年8月24号
 * 功能：管理员对基本代码表数据的相关操作
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

namespace WDFramework.People.ManageBasicCode
{
    public partial class BasicCodes : System.Web.UI.Page
    {
        BLHelper.BLLBasicCode bllBasicCode = new BLHelper.BLLBasicCode();
        BLCommon.PublicMethod publicmethod = new BLCommon.PublicMethod();
        private int page;
        protected void Page_Load(object sender, EventArgs e)
        {
            page = ViewState["page"] == null ? 0 : (int)ViewState["page"];
            if (!IsPostBack)
            {
                //添加数据
                btnAdd.OnClientClick = WindowAdd.GetShowReference("Add_BasicCode.aspx");               
                BindData();
                btnDelete.Enabled = false;
                DropDownListCategoryName.Reset();
                InitDropListDropDownListCategoryName();
            }
        }
        public void BindData()
        {
            try
            {
                ViewState["page"] = 0;
                List<BasicCode> BasicCodeList = bllBasicCode.FindAll();
                GridBasicCode.RecordCount = BasicCodeList.Count();
                var result = BasicCodeList.Skip(GridBasicCode.PageIndex * GridBasicCode.PageSize).Take(GridBasicCode.PageSize).ToList();
                this.GridBasicCode.DataSource = result;
                this.GridBasicCode.DataBind();
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request); ;
            }
        }
        //根据分类名称查询
        public void FindByCategoryName()
        {
            try
            {
                ViewState["page"] = 1;
                List<BasicCode> BasicCodeList = bllBasicCode.FindByCategoryName(DropDownListCategoryName.SelectedItem.Text);
                GridBasicCode.RecordCount = BasicCodeList.Count();
                var result = BasicCodeList.Skip(GridBasicCode.PageIndex * GridBasicCode.PageSize).Take(GridBasicCode.PageSize).ToList();
                this.GridBasicCode.DataSource = result;
                this.GridBasicCode.DataBind();
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
            GridBasicCode.PageIndex = 0;
            if (DropDownListCategoryName.SelectedItem.Text != "全部")
            {
                FindByCategoryName();
            }
            else
                BindData();
        }
        //刷新
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            BindData();
            btnDelete.Enabled = false;
            DropDownListCategoryName.Reset();
        }
        //删除
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                List<int> selections = publicmethod.GridCount(GridBasicCode, CBoxSelect);
                if (Convert.ToInt32(Session["SecrecyLevel"]) == 5)
                {
                    for (int i = 0; i < selections.Count(); i++)
                    {
                        bllBasicCode.Delete(Convert.ToInt32(GridBasicCode.DataKeys[selections[i]][0]));
                    }
                    BindData();
                    btnSelect_All.Text = "全选";
                    Alert.ShowInTop("删除数据成功!");
                }
                else
                {
                    btnSelect_All.Text = "全选";
                    Alert.ShowInTop("您没有操作权限！");
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
                List<int> selections = publicmethod.GridCount(GridBasicCode, CBoxSelect);
                if (selections.Count() != 0)
                {
                    if (selections.Count() == 1)
                    {
                        int rowID = Convert.ToInt32(GridBasicCode.DataKeys[selections[0]][0]);
                        Session["BasicCodeID"] = rowID;
                        Alert.Show("你确定要修改该行数据吗!", "确认消息", MessageBoxIcon.Information, WindowUpdate.GetShowReference("Update_BasicCode.aspx"), Target.Top);
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
        //GridProjectStudent行命令
        protected void GridBasicCode_RowCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                if (Convert.ToInt32(Session["SecrecyLevel"]) != 5)
                {
                    string str = "您无对此行操作的权限,请与管理员联系!";
                    CBoxSelect.SetCheckedState(e.RowIndex, false);
                    Alert.ShowInTop(str);
                }
                if (publicmethod.GridCount(GridBasicCode, CBoxSelect).Count == 0)
                {
                    btnDelete.Enabled = false;
                    return;
                }
                if (publicmethod.GridCount(GridBasicCode, CBoxSelect).Count != 0)
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
        protected void GridBasicCode_PageIndexChange(object sender, GridPageEventArgs e)
        {
            GridBasicCode.PageIndex = e.NewPageIndex;
            switch (page)
            {
                case 0:
                    BindData();
                    break;
                case 1:
                    FindByCategoryName();
                    break;
            }
        }

        protected void ddlGridPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridBasicCode.PageIndex = 0;
            this.GridBasicCode.PageSize = Convert.ToInt32(ddlGridPageSize.SelectedValue);
            switch (page)
            {
                case 0:
                    BindData();
                    break;
                case 1:
                    FindByCategoryName();
                    break;
            }
        }
        public int RowNumber(int dataItemIndex)
        {
            return dataItemIndex + (GridBasicCode.PageIndex) * GridBasicCode.PageSize;
        }
        //初始化分类名称下拉框
        public void InitDropListDropDownListCategoryName()
        {
            try
            {

                string[] SecrecyLevels = new string[] {"学历","学位","证件类型","政治面貌","学生类型","民族","级别", "项目性质","项目状态"  
                                ,"收录情况","专利类型","通知公告分类名称","机构分类名称","文件分类名称","经费用途分类名称","会议分类名称"
                                  ,"学科分类名称","人员行政级别名称","工作计划计划总结分类","项目来源","合作形式","预期成果","学缘","刊物级别","著作类型","获奖类型","获奖等级","获奖类别","第一作者身份","发表状态",
                                  "平台级别","批复部门","平台类别","报告类别","鉴定级别","鉴定形式","鉴定水平","专利情况","项目等级（一类）","项目等级（二类）","项目等级（三类）"};
                for (int i = 0; i < SecrecyLevels.Count(); i++)
                {
                    DropDownListCategoryName.Items.Add(SecrecyLevels[i], SecrecyLevels[i]);
                }
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }
        //全选按钮
        protected void btnSelect_All_Click(object sender, EventArgs e)
        {
            GridBasicCode.SelectAllRows();
            int[] select = GridBasicCode.SelectedRowIndexArray;
            int m;
            //取整数（不是四舍五入，全舍）
            int Pages = (int)Math.Floor(Convert.ToDouble(GridBasicCode.RecordCount / this.GridBasicCode.PageSize));

            if (GridBasicCode.PageIndex == Pages)
                m = (GridBasicCode.RecordCount - this.GridBasicCode.PageSize * GridBasicCode.PageIndex);
            else
                m = this.GridBasicCode.PageSize;
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