/**编写人：张凡凡
 * 时间：2014年8月13号
 * 功能：支出经费后台的相关操作
 * 修改履历：
 * 
 */
using BLHelper;
using Common.Entities;
using FineUI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WDFramework
{
    public partial class MoneyGive : System.Web.UI.Page
    {
        BLLFundInformation fund = new BLLFundInformation();
        BLLProject blpro = new BLLProject();
        BLLAgency blag = new BLLAgency();
        BLLOperationLog blop = new BLLOperationLog();
        BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
        OperationLog op = new OperationLog();
        static int page;

        protected void Page_Load(object sender, EventArgs e)
        {
            btn_ClickIn.OnClientClick = Window1.GetShowReference("MoneyGiveIn.aspx", "登记经费支出");
            if (!IsPostBack)
            {
                ddl_UnitALl.Items[2].Selected = true;
                ddl_UnitALl.Items[0].Selected = false;
                ddl_Unit.Enabled = false;
                ttb_Work.Enabled = false;
                //btn_Count.Enabled = false;
                lb_Change.Text = "";
                btn_Delete.Enabled = false;
                gd_MoneyGiveData();
                reprot1.OnClientClick = WindowReport.GetShowReference("~/Report/R_FundInformation_Take.aspx", "分承担部门按项目统计经费支出");
            }
        }
        //搜索项目更改
        protected void ddl_UnitALl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_UnitALl.SelectedIndex == 0)
            {
                ddl_Unit.Enabled = true;
                ttb_Work.Enabled = true;
                ttb_Work.Reset();
                lb_Change.Text = "选择部门名";
                List<FundInformation> result = fund.FindPaged(Convert.ToInt32(Session["SecrecyLevel"]), true, "支出");
                ddl_Unit.Items.Clear();
                for(int i = 0; i < result.Count; i++)
                {
                    Common.Entities.Project pro = blpro.FindProject(result[i].ProjectID.Value, Convert.ToInt32(Session["SecrecyLevel"])).FirstOrDefault();
                    string res = pro.AcceptUnit;
                    ddl_Unit.Items.Add(res, i.ToString());
                }
            }
            else if (ddl_UnitALl.SelectedIndex == 1)
            {
                ddl_Unit.Enabled = false;
                ttb_Work.Enabled = true;
                ttb_Work.Reset();
                lb_Change.Text = " ";
            }
            else
            {
                ddl_Unit.Enabled = false;
                ttb_Work.Enabled = false;
                ttb_Work.Reset();
                lb_Change.Text = " ";
            }
        }

        //gd_MoneyGive数据绑定
        protected void gd_MoneyGiveData()
        {
            page = 0;
            try
            {
                List<FundInformation> result = fund.FindPaged(Convert.ToInt32(Session["SecrecyLevel"]), true, "支出");
                gd_MoneyGive.RecordCount = result.Count;
                gd_MoneyGive.DataSource = result.Skip(gd_MoneyGive.PageIndex * gd_MoneyGive.PageSize).Take(gd_MoneyGive.PageSize).ToList();
                gd_MoneyGive.DataBind();
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }

        //承担部门名称
        protected string getAcceptUnit(int id)
        {
            try
            {
                Common.Entities.Project pro = blpro.FindProject(id, Convert.ToInt32(Session["SecrecyLevel"])).FirstOrDefault();
                string res = pro.AcceptUnit;
                return res;
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
                return "";
            }
        }

        //所属项目
        protected string getProjectName(int id)
        {
            try
            {
                Common.Entities.Project pro = blpro.FindProject(id, Convert.ToInt32(Session["SecrecyLevel"])).FirstOrDefault();
                return pro.ProjectName;
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
                return "";
            }
        }


        //分页
        protected void gd_MoneyGive_PageIndexChange(object sender, FineUI.GridPageEventArgs e)
        {
            gd_MoneyGive.PageIndex = e.NewPageIndex;
            List<int> proidlist = new List<int>();
            if (ttb_Work.Text.Trim() != "")
                proidlist = blpro.FindProjectList(ttb_Work.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
            switch (page)
            {
                case 0:
                    gd_MoneyGiveData();
                    break;
                case 1:
                    List<FundInformation> res = new List<FundInformation>();
                    for (int i = 0; i < proidlist.Count; i++)
                        res.AddRange(fund.FindByPO(proidlist[i], "支出", Convert.ToInt32(Session["SecrecyLevel"])));
                    gd_MoneyGive.RecordCount = res.Count;
                    gd_MoneyGive.DataSource = res.Skip(gd_MoneyGive.PageIndex * gd_MoneyGive.PageSize).Take(gd_MoneyGive.PageSize).ToList();
                    gd_MoneyGive.DataBind();
                    break;
                case 2:
                    FindByag(proidlist);
                    break;
            }
        }

        protected void ddlGridPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            gd_MoneyGive.PageIndex = 0;
            gd_MoneyGive.PageSize = Convert.ToInt32(ddlGridPageSize.SelectedValue);
            List<int> proidlist = new List<int>();
            if (ttb_Work.Text.Trim() != "")
                proidlist = blpro.FindProjectList(ttb_Work.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
            switch (page)
            {
                case 0:
                    gd_MoneyGiveData();
                    break;
                case 1:
                    List<FundInformation> res = new List<FundInformation>();
                    for (int i = 0; i < proidlist.Count; i++)
                        res.AddRange(fund.FindByPO(proidlist[i], "支出", Convert.ToInt32(Session["SecrecyLevel"])));
                    gd_MoneyGive.RecordCount = res.Count;
                    gd_MoneyGive.DataSource = res.Skip(gd_MoneyGive.PageIndex * gd_MoneyGive.PageSize).Take(gd_MoneyGive.PageSize).ToList();
                    gd_MoneyGive.DataBind();
                    break;
                case 2:
                    FindByag(proidlist);
                    break;
            }
        }

        //查询
        protected void btn_FindUnitPeople_Click(object sender, EventArgs e)
        {
            if (ddl_UnitALl.SelectedIndex == 2)
                gd_MoneyGiveData();
            List<FundInformation> res = new List<FundInformation>();
            List<int> proidlist = new List<int>();
            if (ttb_Work.Text.Trim() != "")
                proidlist = blpro.FindProjectList(ttb_Work.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
            if (ddl_UnitALl.SelectedIndex == 1)
            {
                //if (proidlist.Count == 0)
                //{
                //    ttb_Work.Text = "";
                //   // Alert.ShowInTop("未查找到该项目，请重新输入！");
                //    return;
                //}
                page = 1;
                gd_MoneyGive.PageIndex = 0;
                for (int i = 0; i < proidlist.Count; i++)
                    res.AddRange(fund.FindByPO(proidlist[i], "支出", Convert.ToInt32(Session["SecrecyLevel"])));
                gd_MoneyGive.RecordCount = res.Count;
                gd_MoneyGive.DataSource = res.Skip(gd_MoneyGive.PageIndex * gd_MoneyGive.PageSize).Take(gd_MoneyGive.PageSize).ToList();
                gd_MoneyGive.DataBind();
            }
            else if (ddl_UnitALl.SelectedIndex == 0)
            {
                FindByag(proidlist);
            }
            btn_Delete.Enabled = false;
            gd_MoneyGive.PageIndex = 0;
        }

        //分承担部门按项目
        private void FindByag(List<int> proid)
        {
            page = 2;
            gd_MoneyGive.PageIndex = 0;
            List<FundInformation> fundlist = new List<FundInformation>();
            List<int> proID = new List<int>();
            if (proid.Count != 0)
            {
                for (int i = 0; i < proid.Count; i++)
                {
                    Common.Entities.Project pro = blpro.FindProject(proid[i], Convert.ToInt32(Session["SecrecyLevel"])).FirstOrDefault();
                    string name = pro.AcceptUnit;
                    if (name == ddl_Unit.SelectedText.Trim())
                        proID.Add(proid[i]);
                }
                for(int i = 0; i < proID.Count; i++)
                    fundlist.AddRange(fund.FindByAPO(proID[i], "支出", Convert.ToInt32(Session["SecrecyLevel"])));
            }
            else
            {
               // int id = blag.SelectAgencyID();
                List<int> ProjectIDList = blpro.FindIDlistByAU(ddl_Unit.SelectedText.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                for (int i = 0; i < ProjectIDList.Count; i++)
                    fundlist.AddRange(fund.FindByAPO(ProjectIDList[i], "支出", Convert.ToInt32(Session["SecrecyLevel"])));
            }
            gd_MoneyGive.RecordCount = fundlist.Count;
            gd_MoneyGive.DataSource = fundlist;
            gd_MoneyGive.DataBind();
        }

        //删除
        protected void btn_Delete_Click(object sender, EventArgs e)
        {
            int sum = 0;
            for (int i = 0; i < gd_MoneyGive.RecordCount; i++)
            {
                if (CBoxSelect.GetCheckedState(i))
                {
                    sum++;
                    if (Convert.ToInt32(Session["SecrecyLevel"]) == 5)
                        fund.Delete(Convert.ToInt32(gd_MoneyGive.DataKeys[i][0].ToString()));
                    else
                    {
                        op.LoginIP = " ";
                        op.LoginName = Session["LoginName"].ToString();
                        op.OperationContent = "FundInformation";
                        op.OperationDataID = Convert.ToInt32(gd_MoneyGive.DataKeys[i][0].ToString());
                        op.OperationTime = DateTime.Now;
                        op.OperationType = "删除";
                        blop.Insert(op);
                        fund.UpdateIsPass(Convert.ToInt32(gd_MoneyGive.DataKeys[i][0].ToString()), false);
                    }
                }
            }
            gd_MoneyGiveData();
                
            if (Convert.ToInt32(Session["SecrecyLevel"]) == 5)
                Alert.ShowInTop("删除成功！");
            else
                Alert.ShowInTop("您的操作已经提交，请等待管理员确认！");
            btn_Delete.Enabled = false;
        }

        protected void gd_MoneyGive_RowClick(object sender, GridRowClickEventArgs e)
        {
            string person = gd_MoneyGive.Rows[e.RowIndex].Values[8].ToString();
            BLHelper.BLLUser user = new BLLUser();
            string username = user.FindByLoginName(Session["LoginName"].ToString()).UserName;
            if (Convert.ToInt32(Session["SecrecyLevel"]) != 5 && person != username)
            {
                string str = "您没有对此行操作的权限!此行为" + person + "录入，请与管理员联系！";
                Alert.ShowInTop(str);
                CBoxSelect.SetCheckedState(e.RowIndex, false);
            }
            if (pm.GridCount(gd_MoneyGive, CBoxSelect).Count == 0)
                btn_Delete.Enabled = false;
            else
                btn_Delete.Enabled = true;
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            gd_MoneyGiveData();
            ddl_UnitALl.SelectedValue = "2";
            ddl_Unit.Enabled = false;
            ttb_Work.Enabled = false;
            ddl_Unit.Items.Clear();
            lb_Change.Text = " ";
        }


        //grid序号
        public int RowNumber(int dataItemIndex)
        {
            return dataItemIndex + (gd_MoneyGive.PageIndex) * gd_MoneyGive.PageSize;
        }

    }
}