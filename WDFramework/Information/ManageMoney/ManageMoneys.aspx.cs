using BLHelper;
using Common.Entities;
using FineUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WDFramework.Information.ManageMoney
{
    public partial class ManageMoneys : System.Web.UI.Page
    {
        BLHelper.BLLOperationLog bllOperate = new BLHelper.BLLOperationLog();
        BLHelper.BLLAgency bllAgency = new BLLAgency();
        BLCommon.PublicMethod publicmethod = new BLCommon.PublicMethod();
        BLCommon.Encrypt encrypt = new BLCommon.Encrypt();
        BLHelper.BLLProject bllProject = new BLLProject();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
            }
        }
        //绑定数据
        public void BindData()
        {
            try
            {
                List<Common.Entities.Project> ProjectList = bllProject.FindPaged(Convert.ToInt32(Session["SecrecyLevel"]));
                People_Info.RecordCount = ProjectList.Count;
                this.People_Info.DataSource = ProjectList.Skip(People_Info.PageIndex * People_Info.PageSize).Take(People_Info.PageSize).ToList();
                this.People_Info.DataBind();
                //btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }
        //根据机构ID找机构名称
        public string AgencyName(int AgencyID)
        {
            try
            {
                if (AgencyID != 0)
                    return bllAgency.FindAgenName(AgencyID);
                else
                    return "";
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
                return "";
            }
        }
        //涉密等级名称
        public string SecrecyLevelName(int level)
        {
            try
            {
                //string[] SecrecyLevels = new string[] { "公开", "内部", "秘密", "机密", "管理员" };
                string[] SecrecyLevels = new string[] { "四级", "三级", "二级", "一级", "管理员" };
                return SecrecyLevels[level - 1];
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
                return "";
            }
        }
        //行数
        public int RowNumber(int dataItemIndex)
        {
            return dataItemIndex + (People_Info.PageIndex) * People_Info.PageSize;
        }
        //分页
        protected void People_Info_PageIndexChange(object sender, GridPageEventArgs e)
        {
            People_Info.PageIndex = e.NewPageIndex;
            BindData();
        }
        protected void ddlGridPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            People_Info.PageIndex = 0;
            this.People_Info.PageSize = Convert.ToInt32(ddlGridPageSize.SelectedValue);
            BindData();
        }
        //设置管理费比例
        protected void btnMoneyManege_Click(object sender, EventArgs e)
        {
            try
            {
                Alert.Show("确定设置管理费比例!", "确认消息", MessageBoxIcon.Information, WindowUpdate.GetShowReference("~/FundInformation/MoneyWeb.aspx"), Target.Top);
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }
    }
}