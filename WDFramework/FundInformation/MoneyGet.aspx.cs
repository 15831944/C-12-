/**编写人：张凡凡
 * 时间：2014年8月18号
 * 功能：提取经费的相关操作
 * 修改履历：
 * 
 */
using BLHelper;
using Common.Entities;
using FineUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WDFramework
{
    public partial class MoneyGet : System.Web.UI.Page
    {
        BLLFundInformation blfund = new BLLFundInformation();
        BLLProject blpro = new BLLProject();
        BLLAgency blag = new BLLAgency();
        BLLUser bluser = new BLLUser();
        BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
        BLLOperationLog blop = new BLLOperationLog();
        static int page; 

        protected void Page_Load(object sender, EventArgs e)
        {
            btn_ClickIn.OnClientClick = Window1.GetShowReference("MoneyGetIn.aspx", "登记提取经费");
            if (!IsPostBack)
            {
                ddl_UnitALl.Items[3].Selected = true;
                ddl_UnitALl.Items[0].Selected = false;
                tbAgency.Enabled = false;
                lb_Change.Text = "";
                Labttb.Text = "";
                ttb_Work.Enabled = false;
                btn_Delete.Enabled = false;
                InitData();
                reprot1.OnClientClick = WindowReport.GetShowReference("~/Report/R_FundInformation_Get.aspx", "分承担部门按项目统计提取经费");
            }
        }

        //数据绑定
        private void InitData()
        {
            page = 0;
            List<FundInformation> result = blfund.FindPaged(Convert.ToInt32(Session["SecrecyLevel"]), true, "提取");
            gd_UnitAPeople.RecordCount = result.Count;
            gd_UnitAPeople.DataSource = result.Skip(gd_UnitAPeople.PageIndex * gd_UnitAPeople.PageSize).Take(gd_UnitAPeople.PageSize).ToList();
            gd_UnitAPeople.DataBind();
        }

        //部门名称
        protected string getAgencyName(int id)
        {
            try
            {
                Common.Entities.Project pro = blpro.FindProject(id, Convert.ToInt32(Session["SecrecyLevel"])).FirstOrDefault();
                string res = blag.FindAgenName(pro.AgencyID);
                return res;
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
                return "";
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

        //提取人姓名
        protected string getUserName(int id)
        {
            try
            {
                return bluser.FindUserName(id);
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

        ////用途
        //protected string getuse(int id)
        //{
        //    string res = blfunsort.FindByID(id);
        //    return res;
        //}


        protected void ddl_UnitALl_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(ddl_UnitALl.SelectedIndex)
            {
                case 0:
                    Labttb.Text = "请输入提取人姓名：  ";
                    tbAgency.Enabled = true;
                    //btn_Print.Enabled = true;
                    //btn_Show.Enabled = true;
                    lb_Change.Text = "请输入部门名：";
                    ttb_Work.Enabled = true;
                    break;
                case 1:
                    Labttb.Text = "请输入项目名称：";
                    //btn_Print.Enabled = false;
                    //btn_Show.Enabled = true;
                    lb_Change.Text = "请输入承担部门：";
                    tbAgency.Enabled = true;
                    ttb_Work.Enabled = true;
                    break;
                case 2:
                    tbAgency.Enabled = false;
                    //btn_Print.Enabled = false;
                    //btn_Show.Enabled = false;
                    lb_Change.Text = "";
                    Labttb.Text = "请输入项目名称：";
                    ttb_Work.Enabled = true;
                    break;
                case 3:
                    tbAgency.Enabled = false;
                    //btn_Print.Enabled = false;
                    //btn_Show.Enabled = false;
                    lb_Change.Text = "";
                    Labttb.Text = "";
                    ttb_Work.Enabled = false;
                    break;
            }
        }

        //部门名称合法性判定
        protected void tbAgency_TextChanged(object sender, EventArgs e)
        {
            if (ddl_UnitALl.SelectedIndex == 0)
            {
                if (blag.FindByName(tbAgency.Text.Trim()) == null)
                {
                    tbAgency.Text = "";
                    Alert.ShowInTop("此机构不存在，请重新填写！");
                }
                else
                {
                    if (blag.FindByName(tbAgency.Text.Trim()).IsPass == false)
                    {
                        tbAgency.Text = "";
                        Alert.ShowInTop("此机构信息正在审核中，请联系管理员！");
                    }
                    else
                        return;
                }
            }
            else
                return;
        }

        //分页
        protected void gd_UnitAPeople_PageIndexChange(object sender, GridPageEventArgs e)
        {
            gd_UnitAPeople.PageIndex = e.NewPageIndex;
            List<FundInformation> res = new List<FundInformation>();
            switch (page)
            {
                case 0:
                    InitData();
                    return;
                case 1:
                    res = FindByAgen();
                    break;
                case 2:
                    res = FindByAcceptUnit();
                    break;
                case 3:
                    int proid = blpro.SelectProjectID(ttb_Work.Text.Trim());
                    res = blfund.FindByPO(proid, "提取", Convert.ToInt32(Session["SecrecyLevel"]));
                    break;
            }
            gd_UnitAPeople.RecordCount = res.Count;
            gd_UnitAPeople.DataSource = res.Skip(gd_UnitAPeople.PageIndex * gd_UnitAPeople.PageSize).Take(gd_UnitAPeople.PageSize).ToList();
            gd_UnitAPeople.DataBind();
        }
        protected void ddlGridPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            gd_UnitAPeople.PageIndex = 0;
            gd_UnitAPeople.PageSize = Convert.ToInt32(ddlGridPageSize.SelectedValue);
            List<FundInformation> res = new List<FundInformation>();
            switch (page)
            {
                case 0:
                    InitData();
                    return;
                case 1:
                    res = FindByAgen();
                    break;
                case 2:
                    res = FindByAcceptUnit();
                    break;
                case 3:
                    int proid = blpro.SelectProjectID(ttb_Work.Text.Trim());
                    res = blfund.FindByPO(proid, "提取", Convert.ToInt32(Session["SecrecyLevel"]));
                    break;
            }
            gd_UnitAPeople.RecordCount = res.Count;
            gd_UnitAPeople.DataSource = res.Skip(gd_UnitAPeople.PageIndex * gd_UnitAPeople.PageSize).Take(gd_UnitAPeople.PageSize).ToList();
            gd_UnitAPeople.DataBind();
        }

        //搜索
        protected void btn_FindUnitPeople_Click(object sender, EventArgs e)
        {
            gd_UnitAPeople.PageIndex = 0;
            List<FundInformation> result = new List<FundInformation>();
            switch (ddl_UnitALl.SelectedIndex)
            {
                case 0:
                    if (tbAgency.Text == "")
                    {
                        Alert.ShowInTop("请填写部门名称！");
                        return;
                    }
                    else
                    {
                        //分部门按提取人
                        result = FindByAgen();
                    }
                    break;
                case 1:
                    if (tbAgency.Text == "")
                    {
                        Alert.ShowInTop("请填写承担部门名称！");
                        return;
                    }
                    else
                    {
                        //分承担部门按项目
                        result = FindByAcceptUnit();
                    }
                    break;
                case 2:
                    if (ttb_Work.Text.Trim() == "")
                    {
                        Alert.ShowInTop("请填写项目名称！");
                        return;
                    }
                    int proid = blpro.SelectProjectID(ttb_Work.Text.Trim());
                    if (proid == 0)
                    {
                        Alert.ShowInTop("未找到此项目名，请检查！");
                        ttb_Work.Text = "";
                        return;
                    }
                    else
                    {
                        page = 3;
                        gd_UnitAPeople.PageIndex = 0;
                        result = blfund.FindByPO(proid, "提取", Convert.ToInt32(Session["SecrecyLevel"]));
                    }
                    break;
                case 3:
                    InitData();
                    return;
            }
            gd_UnitAPeople.RecordCount = result.Count;
            gd_UnitAPeople.DataSource = result.Skip(gd_UnitAPeople.PageIndex * gd_UnitAPeople.PageSize).Take(gd_UnitAPeople.PageSize).ToList();
            gd_UnitAPeople.DataBind();
            btn_Delete.Enabled = false;
        }

        //分部门按提取人
        private List<FundInformation> FindByAgen()
        {
            page = 1;
            gd_UnitAPeople.PageIndex = 0;
            List<int> PjID = blpro.FindIDlistByAgency(blag.SelectAgencyID( tbAgency.Text.Trim()), Convert.ToInt32(Session["SecrecyLevel"]));
            List<FundInformation> list = new List<FundInformation>();
            for (int i = 0; i < PjID.Count; i++)
                list.AddRange(blfund.FindByPO(PjID[i], "提取", Convert.ToInt32(Session["SecrecyLevel"])));
            if (ttb_Work.Text.Trim() == "")
            {
                return list;
            }
            else
            {
                if (bluser.FindByName(ttb_Work.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"])) == null)
                {
                    ttb_Work.Text = "";
                    Alert.ShowInTop("未查到此人员！");
                    return null;
                }
                int UserID = bluser.FindByName(ttb_Work.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"])).FirstOrDefault().UserInfoID;
                List<FundInformation> res = new List<FundInformation>();
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].UserInfoID == UserID)
                        res.Add(list[i]);
                }
                return res;
            }
        }

        //分承担部门按项目
        private List<FundInformation> FindByAcceptUnit()
        {
            page = 2;
            gd_UnitAPeople.PageIndex = 0;
            if (ttb_Work.Text.Trim() == "")
            {
                List<int> ProjectID = blpro.FindIDlistByAU(tbAgency.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                List<FundInformation> lis = new List<FundInformation>();
                for (int i = 0; i < ProjectID.Count; i++)
                    lis.AddRange(blfund.FindByPO(ProjectID[i], "提取", Convert.ToInt32(Session["SecrecyLevel"])));
                return lis;

            }
            else
            {
                int projectid = blpro.SelectProjectID(ttb_Work.Text.Trim());
                if (projectid == 0)
                {
                    Alert.ShowInTop("未找到此项目名，请检查！");
                    ttb_Work.Text = "";
                    return null;
                }
                else
                {
                    Common.Entities.Project pro = blpro.FindProject(projectid, Convert.ToInt32(Session["SecrecyLevel"])).FirstOrDefault();
                    if (pro.AcceptUnit != tbAgency.Text.Trim())
                    {
                        string str = "此项目由" + pro.AcceptUnit + " 承接！请重新输入！";
                        Alert.ShowInTop(str);
                        return null;
                    }
                    else
                    {
                        List<FundInformation> list = blfund.FindByPO(projectid, "提取", Convert.ToInt32(Session["SecrecyLevel"]));
                        return list;
                    }
                }
            }
        }

        //删除
        protected void btn_Delete_Click(object sender, EventArgs e)
        {
            OperationLog op = new OperationLog();
            int sum = 0;
            for (int i = 0; i < gd_UnitAPeople.RecordCount; i++)
            {
                if (CBoxSelect.GetCheckedState(i))
                {
                    sum++;
                    if (Convert.ToInt32(Session["SecrecyLevel"]) == 5)
                        blfund.Delete(Convert.ToInt32(gd_UnitAPeople.DataKeys[i][0].ToString()));
                    else
                    {
                        op.LoginIP = " ";
                        op.LoginName = Session["LoginName"].ToString();
                        op.OperationContent = "FundInformation";
                        op.OperationDataID = Convert.ToInt32(gd_UnitAPeople.DataKeys[i][0].ToString());
                        op.OperationTime = DateTime.Now;
                        op.OperationType = "删除";
                        blop.Insert(op);
                        blfund.UpdateIsPass(Convert.ToInt32(gd_UnitAPeople.DataKeys[i][0].ToString()), false);
                    }
                }
            }
            InitData();
            if (Convert.ToInt32(Session["SecrecyLevel"]) == 5)
                Alert.ShowInTop("删除成功！");
            else
                Alert.ShowInTop("您的操作已经提交，请等待管理员确认！");
            btn_Delete.Enabled = false;
        }

        protected void gd_UnitAPeople_RowClick(object sender, GridRowClickEventArgs e)
        {
            string person = gd_UnitAPeople.Rows[e.RowIndex].Values[0].ToString();
            BLHelper.BLLUser user = new BLLUser();
            string username = user.FindByLoginName(Session["LoginName"].ToString()).UserName;
            if (Convert.ToInt32(Session["SecrecyLevel"]) != 5 && person != username)
            {
                string str = "您没有对此行操作的权限!此行为" + person + "录入，请与管理员联系！";
                Alert.ShowInTop(str);
                CBoxSelect.SetCheckedState(e.RowIndex, false);
            }

            if (pm.GridCount(gd_UnitAPeople, CBoxSelect).Count == 0)
            {
                btn_Delete.Enabled = false;
                return;
            }
            else
            {
                btn_Delete.Enabled = true;
                return;
            }


        }

        //刷新
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            InitData();
            ddl_UnitALl.SelectedValue = "3";
            tbAgency.Enabled = false;
            lb_Change.Text = "";
            Labttb.Text = "";
            ttb_Work.Enabled = false;
        }

        protected void btn_Print_Click(object sender, EventArgs e)
        {
            
        }
        //grid序号
        public int RowNumber(int dataItemIndex)
        {
            return dataItemIndex + (gd_UnitAPeople.PageIndex) * gd_UnitAPeople.PageSize;
        }

    }
}