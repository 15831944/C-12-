/**编写人：张凡凡
 * 时间：2014年8月18号
 * 功能：进账经费的相关操作
 * 修改履历：
 * 
 */
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
    public partial class MoneyCome : System.Web.UI.Page
    {
        BLHelper.BLLProject blpro = new BLHelper.BLLProject();
        BLHelper.BLLFundInformation blfund = new BLHelper.BLLFundInformation();
        BLHelper.BLLOperationLog blop = new BLHelper.BLLOperationLog();
        BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
        static int page;

        protected void Page_Load(object sender, EventArgs e)
        {
            btn_ClickIn.OnClientClick = Window1.GetShowReference("MoneyComeIn.aspx", "登记经费进账");
            if (!IsPostBack)
            {
                ddl_UnitALl.Items[6].Selected = true;
                ddl_UnitALl.Items[0].Selected = false;
                btn_Delete.Enabled = false;
                tb_people.Enabled = false;
                ddl_ProName.Enabled = false;
                Lb_Change4.Text = "";
                lb_Change3.Text = "";
                DataGrid();
                reprot1.OnClientClick = WindowReport.GetShowReference("~/Report/Accept_PName.aspx", "分承担部门按项目统计进账经费");
                reprot2.OnClientClick = WindowReport.GetShowReference("~/Report/Accept_Principal.aspx", "分承担部门按项目负责人统计进账经费");
                reprot3.OnClientClick = WindowReport.GetShowReference("~/Report/Source_Unit.aspx", "分项目来源按承担部门统计进账经费");
                reprot4.OnClientClick = WindowReport.GetShowReference("~/Report/Type_Unit.aspx", "分项目类型按承担部门统计进账经费");
            }
        }

        //查询条件控制
        protected void ddl_UnitALl_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (ddl_UnitALl.SelectedIndex)
            {
                case 0:
                    lb_Change3.Enabled = true;
                    lb_Change3.Text = "来款单位名称: ";
                    tb_people.Enabled = true;
                    tb_people.Reset();
                    ddl_ProName.Reset();
                    Lb_Change4.Text = "项目名称: ";
                    ddl_ProName.Enabled = true;
                    Lb_Content.Text = "";
                    break;
                case 1:
                    lb_Change3.Visible = true;
                    lb_Change3.Text = "承担部门名称: ";
                    Lb_Content.Text = "";
                    tb_people.Enabled = true;
                    tb_people.Reset();
                    ddl_ProName.Reset();
                    Lb_Change4.Text = "项目负责人: ";
                    ddl_ProName.Enabled = true;
                    break;
                case 2:
                    lb_Change3.Visible = true;
                    lb_Change3.Text = "项目负责人: ";
                    Lb_Content.Text = "";
                    tb_people.Enabled = true;
                    tb_people.Reset();
                    ddl_ProName.Reset();
                    Lb_Change4.Text = "项目名称: ";
                    ddl_ProName.Enabled = true;
                    break;
                case 3:
                    lb_Change3.Visible = true;
                    lb_Change3.Text = "项目类型: ";
                    tb_people.Enabled = true;
                    tb_people.Reset();
                    ddl_ProName.Reset();
                    Lb_Change4.Text = "项目名称: ";
                    Lb_Content.Text = "查询内容: ";
                    ddl_ProName.Enabled = true;
                    break;
                case 4:
                    lb_Change3.Visible = true;
                    lb_Change3.Text = "项目来源: ";
                    ddl_ProName.Enabled = true;
                    tb_people.Enabled = true;
                    tb_people.Reset();
                    ddl_ProName.Reset();
                    Lb_Change4.Text = "项目名称: ";
                    Lb_Content.Text = "查询内容: ";
                    break;
                case 5:
                    lb_Change3.Text = "项目名称: ";
                    Lb_Content.Text = "";
                    ddl_ProName.Enabled = false;
                    tb_people.Enabled = true;
                    tb_people.Reset();
                    ddl_ProName.Reset();
                    Lb_Change4.Text = "";
                    break;
                case 6:  
                    btn_Delete.Enabled = false;
                    tb_people.Enabled = false;
                    tb_people.Reset();
                    ddl_ProName.Reset();
                    ddl_ProName.Enabled = false;
                    Lb_Change4.Text = "";
                    lb_Change3.Text = "";
                    break;
            }
        }
 

        //承担部门
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

        //项目负责人
        protected string getProjectHeads(int id)
        {
            try
            {
                Common.Entities.Project pro = blpro.FindProject(id, Convert.ToInt32(Session["SecrecyLevel"])).FirstOrDefault();
                return pro.ProjectHeads;
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
                return "";
            }
        }

        //项目来源
        protected string getSourceUnit(int id)
        {
            try
            {
                Common.Entities.Project pro = blpro.FindProject(id, Convert.ToInt32(Session["SecrecyLevel"])).FirstOrDefault();
                return pro.SourceUnit;
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
                return "";
            }
        }

        //项目性质
        protected string getProjectNature(int id)
        {
            try
            {
                Common.Entities.Project pro = blpro.FindProject(id, Convert.ToInt32(Session["SecrecyLevel"])).FirstOrDefault();
                return pro.ProjectNature;
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
                return "";
            }
        }

        //来款单位
        protected string getGivenMoneyUnits(int id)
        {
            try
            {
                Common.Entities.Project pro = blpro.FindProject(id, Convert.ToInt32(Session["SecrecyLevel"])).FirstOrDefault();
                return pro.GivenMoneyUnits;
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
                return "";
            }
        }

        //管理费
        protected string getManageMoney(int id)
        {
            try
            {
                Common.Entities.Project pro = blpro.FindProject(id, Convert.ToInt32(Session["SecrecyLevel"])).FirstOrDefault();
                int ManageMoney = Convert.ToInt32(pro.ManageMoney);
                double MangMoney = blfund.Count(id, "进账", Convert.ToInt32(Session["SecrecyLevel"])) * ManageMoney / 100;
                return MangMoney.ToString();
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
                return "";
            }
        }


        //初始化绑定
        private void DataGrid()
        {
            page = 6;
            List<Common.Entities.FundInformation> fund = blfund.FindPaged(Convert.ToInt32(Session["SecrecyLevel"]), true, "进账");
            gd_UnitAPeople.DataSource = fund.Skip(gd_UnitAPeople.PageIndex * gd_UnitAPeople.PageSize).Take(gd_UnitAPeople.PageSize).ToList();
            gd_UnitAPeople.RecordCount = fund.Count;
            gd_UnitAPeople.DataBind();
            gd_UnitAPeople.PageSize = Convert.ToInt32(ddlGridPageSize.SelectedText);
        }

        //搜索
        protected void btn_Find_Click(object sender, EventArgs e)
        {
            switch (ddl_UnitALl.SelectedIndex)
            {
                case 0:
                    FindByMoneyUnit();
                    break;
                case 1:
                    FindByAU();
                    break;
                case 2:
                    FindByPeoPro();
                    break;
                case 3:
                    FindByProType();
                    break;
                case 4:
                    FindBySource();
                    break;
                case 5:
                    FindByPro();
                    break;
                case 6:
                    DataGrid();
                    break;
            }
            btn_Delete.Enabled = false;
            gd_UnitAPeople.PageIndex = 0;
        }

        //分来款单位按项目
        private void FindByMoneyUnit()
        {
            List<Common.Entities.FundInformation> fund = new List<Common.Entities.FundInformation>();
            page = 0;
            gd_UnitAPeople.PageIndex = 0;
            if (ddl_ProName.SelectedValue == "0")
            {
                List<int> projectId = new List<int>();
                projectId = blpro.FindIDlistByGMUnits(tb_people.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                for (int i = 0; i < projectId.Count; i++)
                    fund.AddRange(blfund.FindByPO(projectId[i], "进账", Convert.ToInt32(Session["SecrecyLevel"])));
            }
            else
            {
                int ProjectId = Convert.ToInt32(blpro.SelectProjectID(ddl_ProName.SelectedText));
                fund.AddRange(blfund.FindByPO(ProjectId, "进账", Convert.ToInt32(Session["SecrecyLevel"])));
            }
            gd_UnitAPeople.DataSource = fund.Skip(gd_UnitAPeople.PageIndex * gd_UnitAPeople.PageSize).Take(gd_UnitAPeople.PageSize).ToList();
            gd_UnitAPeople.RecordCount = fund.Count;
            gd_UnitAPeople.DataBind();
        }

        //分承担部门按项目负责人
        private void FindByAU()
        {
            page = 1;
            gd_UnitAPeople.PageIndex = 0;
            List<Common.Entities.FundInformation> fund = new List<Common.Entities.FundInformation>();
            if (ddl_ProName.SelectedValue == "0")
            {
                List<int> projectId = new List<int>();
                projectId = blpro.FindIDlistByAU(tb_people.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                for (int i = 0; i < projectId.Count; i++)
                    fund.AddRange(blfund.FindByPO(projectId[i], "进账", Convert.ToInt32(Session["SecrecyLevel"])));
            }
            else
            {
                List<int> Projectid = new List<int>();
                Projectid = blpro.FindIDListByPeople(ddl_ProName.SelectedText, Convert.ToInt32(Session["SecrecyLevel"]));
                for (int i = 0; i < Projectid.Count; i++)
                    fund.AddRange(blfund.FindByPO(Projectid[i], "进账", Convert.ToInt32(Session["SecrecyLevel"])));
            }
            gd_UnitAPeople.DataSource = fund.Skip(gd_UnitAPeople.PageIndex * gd_UnitAPeople.PageSize).Take(gd_UnitAPeople.PageSize).ToList();
            gd_UnitAPeople.RecordCount = fund.Count;
            gd_UnitAPeople.DataBind();
        }

        //分负责人按项目查看
        private void FindByPeoPro()
        {
            List<Common.Entities.FundInformation> fund = new List<Common.Entities.FundInformation>();
            page = 2;
            gd_UnitAPeople.PageIndex = 0;
            if (ddl_ProName.SelectedValue == "0")
            {
                List<int> projectId = new List<int>();
                projectId = blpro.FindIDListByPeople(tb_people.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                for (int i = 0; i < projectId.Count; i++)
                    fund.AddRange(blfund.FindByPO(projectId[i], "进账", Convert.ToInt32(Session["SecrecyLevel"])));
            }
            else
            {
                int ProjectId = Convert.ToInt32(blpro.SelectProjectID(ddl_ProName.SelectedText));
                fund.AddRange(blfund.FindByPO(ProjectId, "进账", Convert.ToInt32(Session["SecrecyLevel"])));
            }
            gd_UnitAPeople.DataSource = fund.Skip(gd_UnitAPeople.PageIndex * gd_UnitAPeople.PageSize).Take(gd_UnitAPeople.PageSize).ToList();
            gd_UnitAPeople.RecordCount = fund.Count;
            gd_UnitAPeople.DataBind();
        }

        //分项目类型按项目
        private void FindByProType()
        {
            List<Common.Entities.FundInformation> fund = new List<Common.Entities.FundInformation>();
            page = 3;
            gd_UnitAPeople.PageIndex = 0;
            if (ddl_ProName.SelectedValue == "0")
            {
                List<int> projectId = new List<int>();
                projectId = blpro.FindIDListByProlevel(tb_people.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                for (int i = 0; i < projectId.Count; i++)
                    fund.AddRange(blfund.FindByPO(projectId[i], "进账", Convert.ToInt32(Session["SecrecyLevel"])));
            }
            else
            {
                int ProjectId = Convert.ToInt32(blpro.SelectProjectID(ddl_ProName.SelectedText));
                fund.AddRange(blfund.FindByPO(ProjectId, "进账", Convert.ToInt32(Session["SecrecyLevel"])));
            }
            gd_UnitAPeople.DataSource = fund.Skip(gd_UnitAPeople.PageIndex * gd_UnitAPeople.PageSize).Take(gd_UnitAPeople.PageSize).ToList();
            gd_UnitAPeople.RecordCount = fund.Count;
            gd_UnitAPeople.DataBind();
        }

        //分项目来源按项目
        private void FindBySource()
        {
            List<Common.Entities.FundInformation> fund = new List<Common.Entities.FundInformation>();
            page = 4;
            gd_UnitAPeople.PageIndex = 0;
            if (ddl_ProName.SelectedValue == "0")
            {
                List<int> projectId = new List<int>();
                projectId = blpro.FindIDListBySource(tb_people.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                for (int i = 0; i < projectId.Count; i++)
                    fund.AddRange(blfund.FindByPO(projectId[i], "进账", Convert.ToInt32(Session["SecrecyLevel"])));
            }
            else
            {
                int ProjectId = Convert.ToInt32(blpro.SelectProjectID(ddl_ProName.SelectedText));
                fund.AddRange(blfund.FindByPO(ProjectId, "进账", Convert.ToInt32(Session["SecrecyLevel"])));
            }
            gd_UnitAPeople.DataSource = fund.Skip(gd_UnitAPeople.PageIndex * gd_UnitAPeople.PageSize).Take(gd_UnitAPeople.PageSize).ToList();
            gd_UnitAPeople.RecordCount = fund.Count;
            gd_UnitAPeople.DataBind();
        }

        //分项目查看进账经费
        private void FindByPro()
        {
            page = 5;
            gd_UnitAPeople.PageIndex = 0;
            List<Common.Entities.FundInformation> fund = new List<Common.Entities.FundInformation>();
            int ProjectId = blpro.SelectProjectID(tb_people.Text.Trim());
            fund.AddRange(blfund.FindByPO(ProjectId, "进账", Convert.ToInt32(Session["SecrecyLevel"])));
            gd_UnitAPeople.DataSource = fund.Skip(gd_UnitAPeople.PageIndex * gd_UnitAPeople.PageSize).Take(gd_UnitAPeople.PageSize).ToList();
            gd_UnitAPeople.RecordCount = fund.Count;
            gd_UnitAPeople.DataBind();
        }

        //ddl_ProName数据绑定
        protected void tb_people_TextChanged(object sender, EventArgs e)
        {
            List<int> Projectid = new List<int>();
            switch (ddl_UnitALl.SelectedIndex)
            {
                case 0:
                    Projectid = blpro.FindIDlistByGMUnits(tb_people.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                    if (Projectid.Count == 0)
                    {
                        Alert.ShowInTop("未找到任何项目，请检查输入！");
                        return;
                    }
                    ddl_ProName.Items.Clear();
                    ddl_ProName.Items.Add("请选择", "0");
                    for (int i = 0; i < Projectid.Count; i++)
                        ddl_ProName.Items.Add(blpro.SelectProjectName(Projectid[i]), (i + 1).ToString());
                    break;
                case 1:
                    Projectid = blpro.FindIDlistByAU(tb_people.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                    if (Projectid.Count == 0)
                    {
                        Alert.ShowInTop("未找到任何项目，请检查输入！");
                        return;
                    }
                    ddl_ProName.Items.Clear();
                    ddl_ProName.Items.Add("请选择", "0");
                    for (int i = 0; i < Projectid.Count; i++)
                        ddl_ProName.Items.Add(blpro.FindPeoById(Projectid[i]), (i + 1).ToString());
                    break;
                case 2:
                    Projectid = blpro.FindIDListByPeople(tb_people.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                    if (Projectid.Count == 0)
                    {
                        Alert.ShowInTop("未找到任何项目，请检查输入！");
                        return;
                    }
                    ddl_ProName.Items.Clear();
                    ddl_ProName.Items.Add("请选择", "0");
                    for (int i = 0; i < Projectid.Count; i++)
                        ddl_ProName.Items.Add(blpro.SelectProjectName(Projectid[i]), (i + 1).ToString());
                    break;
                case 3:
                    Projectid = blpro.FindIDListByProlevel(tb_people.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                    if (Projectid.Count == 0)
                    {
                        Alert.ShowInTop("未找到任何项目，请检查输入！");
                        return;
                    }
                    ddl_ProName.Items.Clear();
                    ddl_ProName.Items.Add("请选择", "0");
                    for (int i = 0; i < Projectid.Count; i++)
                        ddl_ProName.Items.Add(blpro.SelectProjectName(Projectid[i]), (i + 1).ToString());
                    break;
                case 4:
                    Projectid = blpro.FindIDListBySource(tb_people.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                    if (Projectid.Count == 0)
                    {
                        Alert.ShowInTop("未找到任何项目，请检查输入！");
                        return;
                    }
                    ddl_ProName.Items.Clear();
                    ddl_ProName.Items.Add("请选择", "0");
                    for (int i = 0; i < Projectid.Count; i++)
                        ddl_ProName.Items.Add(blpro.SelectProjectName(Projectid[i]), (i + 1).ToString());
                    break;
                case 5:
                    int ProjectId = blpro.SelectProjectID(tb_people.Text.Trim());
                    if (ProjectId == 0)
                    {
                        tb_people.Text = "";
                        Alert.ShowInTop("未找到此项目，请重新输入");
                    }
                    break;
            }
        }

        protected void btn_Delete_Click(object sender, EventArgs e)
        {
            OperationLog op = new OperationLog();
            List<int> checkid = pm.GridCount(gd_UnitAPeople, CBoxSelect);
            for (int i = 0; i < checkid.Count; i++)
            {
                int id = checkid[i];
                    if (Convert.ToInt32(Session["SecrecyLevel"]) == 5)
                        blfund.Delete(Convert.ToInt32(gd_UnitAPeople.DataKeys[id][0].ToString()));
                    else
                    {
                        op.LoginIP = " ";
                        op.LoginName = Session["LoginName"].ToString();
                        op.OperationContent = "FundInformation";
                        op.OperationDataID = Convert.ToInt32(gd_UnitAPeople.DataKeys[id][0].ToString());
                        op.OperationTime = DateTime.Now;
                        op.OperationType = "删除";
                        blop.Insert(op);
                        blfund.UpdateIsPass(Convert.ToInt32(gd_UnitAPeople.DataKeys[id][0].ToString()), false);
                    }
            }
            DataGrid();

            if (Convert.ToInt32(Session["SecrecyLevel"]) == 5)
                Alert.ShowInTop("删除成功！");
            else
                Alert.ShowInTop("您的操作已经提交，请等待管理员确认！");
            btn_Delete.Enabled = false;
        }

        //分页
        protected void gd_UnitAPeople_PageIndexChange(object sender, GridPageEventArgs e)
        {
            gd_UnitAPeople.PageIndex = e.NewPageIndex;
            switch (page)
            {
                case 0:
                    FindByMoneyUnit();
                    break;
                case 1:
                    FindByAU();
                    break;
                case 2:
                    FindByPeoPro();
                    break;
                case 3:
                    FindByProType();
                    break;
                case 4:
                    FindBySource();
                    break;
                case 5:
                    FindByPro();
                    break;
                case 6:
                    DataGrid();
                    break;
            }
        }
        protected void ddlGridPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            gd_UnitAPeople.PageIndex = 0;
            gd_UnitAPeople.PageSize = Convert.ToInt32(ddlGridPageSize.SelectedText);
            switch (page)
            {
                case 0:
                    FindByMoneyUnit();
                    break;
                case 1:
                    FindByAU();
                    break;
                case 2:
                    FindByPeoPro();
                    break;
                case 3:
                    FindByProType();
                    break;
                case 4:
                    FindBySource();
                    break;
                case 5:
                    FindByPro();
                    break;
                case 6:
                    DataGrid();
                    break;
            }
        }

        //行点击
        protected void gd_UnitAPeople_RowCommand(object sender, GridCommandEventArgs e)
        {
            string person = gd_UnitAPeople.Rows[e.RowIndex].Values[0].ToString();
            BLHelper.BLLUser user = new BLHelper.BLLUser();
            string username = user.FindByLoginName(Session["LoginName"].ToString()).UserName;
            if (Convert.ToInt32(Session["SecrecyLevel"]) != 5 && person != username)
            {
                string str = "您没有对此行操作的权限!此行为" + person + "录入，请与管理员联系！";
                Alert.ShowInTop(str);
                CBoxSelect.SetCheckedState(e.RowIndex, false);
            }
            if (pm.GridCount(gd_UnitAPeople, CBoxSelect).Count == 0)
                btn_Delete.Enabled = false;
            else
                btn_Delete.Enabled = true;
        }

        protected void Btn_Refresh_Click(object sender, EventArgs e)
        {
            DataGrid();
            ddl_UnitALl.SelectedValue = "6";
            btn_Delete.Enabled = false;
            tb_people.Enabled = false;
            ddl_ProName.Enabled = false;
            Lb_Change4.Text = "";
            lb_Change3.Text = "";
        }
        //grid序号
        public int RowNumber(int dataItemIndex)
        {
            return dataItemIndex + (gd_UnitAPeople.PageIndex) * gd_UnitAPeople.PageSize;
        }
    }
}