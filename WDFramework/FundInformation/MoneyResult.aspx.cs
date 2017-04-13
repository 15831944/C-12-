/**编写人：张凡凡
 * 时间：2014年8月13号
 * 功能：经费结转后台的相关操作
 * 修改履历：
 * 
 */
using FineUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WDFramework
{
    public partial class MoneyResult : System.Web.UI.Page
    {
        BLHelper.BLLProject blpro = new BLHelper.BLLProject();
        BLHelper.BLLFundInformation blfund = new BLHelper.BLLFundInformation();
        static int page;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddl_UnitALl.Items[6].Selected = true;
                ddl_UnitALl.Items[0].Selected = false;
                InitData();
                lb_Change.Text = "";
                lb_Name.Text = "";
                tb_Unit.Enabled = false;
                ddl_PeopleName.Enabled = false;
                ddlGridPageSize.SelectedIndex = 1;
            }
        }

        //初始绑定
        private void InitData()
        {
            page = 0;
           List<Common.Entities.Project> res = blpro.FindPaged(Convert.ToInt32(Session["SecrecyLevel"]));
           gd_UnitAPeople.RecordCount = res.Count;
            var result = res.Skip(gd_UnitAPeople.PageIndex * gd_UnitAPeople.PageSize).Take(gd_UnitAPeople.PageSize).ToList();
           gd_UnitAPeople.DataSource = result;
           gd_UnitAPeople.DataBind();
        }

        //选择条件更改
        protected void ddl_UnitALl_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (ddl_UnitALl.SelectedIndex)
            {
                case 0:
                    lb_Change.Text = "";
                    tb_Unit.Enabled = false;
                    ddl_PeopleName.Items.Clear();
                    for (int i = 1960; i < 2060; i++)
                        ddl_PeopleName.Items.Add(i.ToString(), (i - 1960).ToString());
                    ddl_PeopleName.Items[45].Selected = true;
                    ddl_PeopleName.Enabled = true;
                    lb_Name.Text = "年份:";
                    break;
                case 1:
                    lb_Change.Text = "承担部门:";
                    tb_Unit.Enabled = true;
                    //btn_Show.Enabled = false;
                    lb_Name.Text = "负责人:";
                    ddl_PeopleName.Enabled = false;
                    break;
                case 2:
                    tb_Unit.Enabled = true;
                    //btn_Show.Enabled = false;
                    lb_Name.Text = "项目:";
                    lb_Change.Text = "负责人:";
                    ddl_PeopleName.Enabled = false;
                    break;
                case 3:
                    tb_Unit.Enabled = true;
                    //btn_Show.Enabled = false;
                    lb_Name.Text = "项目:";
                    lb_Change.Text = "项目来源:";
                    ddl_PeopleName.Enabled = false;
                    break;
                case 4:
                    tb_Unit.Enabled = true;
                    //btn_Show.Enabled = false;
                    lb_Name.Text = "项目:";
                    lb_Change.Text = "项目类型:";
                    ddl_PeopleName.Enabled = false;
                    break;
                case 5:
                    tb_Unit.Enabled = true;
                    ddl_PeopleName.Enabled = false;
                    lb_Change.Text = "项目:";
                    lb_Name.Text = "";
                    break;
                case 6:
                    tb_Unit.Enabled = false;
                    //btn_Show.Enabled = false;
                    ddl_PeopleName.Enabled = false;
                    lb_Change.Text = "";
                    lb_Name.Text = "";
                    break;
            }
        }

        //结余经费
        protected string getLostMoney(int id)
        {
            try
            {
                double FundCome = blfund.CountByOperate(id, "进账", Convert.ToInt32(Session["SecrecyLevel"]));
                double FundGive = blfund.CountByOperate(id, "支出", Convert.ToInt32(Session["SecrecyLevel"]));
                return (FundCome - FundGive).ToString();
            }
            catch (Exception ex)
            {
                BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
                pm.SaveError(ex, this.Request);
                return "";
            }
        }

        //ddl_PeopleName数据绑定
        protected void tb_Unit_TextChanged(object sender, EventArgs e)
        {
            List<int> Projectid = new List<int>();
            ddl_PeopleName.Items.Clear();
            ddl_PeopleName.Text = "";
            ddl_PeopleName.Items.Add("请选择", "0");
            ddl_PeopleName.Items[0].Selected = true;
            ddl_PeopleName.SelectedValue = "0";
            switch (ddl_UnitALl.SelectedIndex)
            {
                    
                case 1:
                     Projectid = blpro.FindIDlistByAU(tb_Unit.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                    if (Projectid.Count == 0)
                    {
                        tb_Unit.Text = "";
                        Alert.ShowInTop("未找到任何项目，请检查输入！");
                        return;
                    }
                    ddl_PeopleName.Enabled = true;
                    for (int i = 0; i < Projectid.Count; i++)
                        ddl_PeopleName.Items.Add(blpro.FindPeoById(Projectid[i]), (i + 1).ToString());
                    break;
                case 2:
                    Projectid = blpro.FindIDListByPeople(tb_Unit.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                    if (Projectid.Count == 0)
                    {
                        tb_Unit.Text = "";
                        Alert.ShowInTop("未找到任何项目，请检查输入！");
                        return;
                    }
                    ddl_PeopleName.Enabled = true;
                    for (int i = 0; i < Projectid.Count; i++)
                        ddl_PeopleName.Items.Add(blpro.SelectProjectName(Projectid[i]), (i + 1).ToString());
                    break;
                case 3:
                    Projectid = blpro.FindIDListBySource(tb_Unit.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                    if (Projectid.Count == 0)
                    {
                        tb_Unit.Text = "";
                        Alert.ShowInTop("未找到任何项目，请检查输入！");
                        return;
                    }
                    ddl_PeopleName.Enabled = true;
                    for (int i = 0; i < Projectid.Count; i++)
                        ddl_PeopleName.Items.Add(blpro.SelectProjectName(Projectid[i]), (i + 1).ToString());
                    break;
                case 4:
                    Projectid = blpro.FindIDListByProlevel(tb_Unit.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                    if (Projectid.Count == 0)
                    {
                        tb_Unit.Text = "";
                        Alert.ShowInTop("未找到任何项目，请检查输入！");
                        return;
                    }
                    ddl_PeopleName.Enabled = true;
                    for (int i = 0; i < Projectid.Count; i++)
                        ddl_PeopleName.Items.Add(blpro.SelectProjectName(Projectid[i]), (i + 1).ToString());
                    break;
                case 5:
                    int ProjectId = blpro.SelectProjectID(tb_Unit.Text.Trim());
                    if (ProjectId == 0)
                    {
                        tb_Unit.Text = "";
                        Alert.ShowInTop("未找到此项目，请重新输入");
                    }
                    ddl_PeopleName.Enabled = false;
                    break;
            }
        }

        //搜索
        protected void btn_FindUnitPeople_Click(object sender, EventArgs e)
        {
            if (ddl_UnitALl.SelectedIndex != 0 && tb_Unit.Text.Trim() == "" && ddl_UnitALl.SelectedIndex != 6)
            {
                Alert.ShowInTop("请填写搜索条件!");
                return;
            }
            List<Common.Entities.Project> res = new List<Common.Entities.Project>();
            switch (ddl_UnitALl.SelectedIndex)
            {
                case 0:
                    res = FindByYear();
                    page = 1;
                    break;
                case 1:
                    res = FindByAgen();
                    page = 2;
                    break;
                case 2:
                    res = FindByPeoPro();
                    page = 3;
                    break;
                case 3:
                    res = FindBySource();
                    page = 4;
                    break;
                case 4:
                    res = FindByLevel();
                    page = 5;
                    break;
                case 5:
                    gd_UnitAPeople.PageIndex = 0;
                    List<int> proid = new List<int>();
                    proid.Add(blpro.SelectProjectID(tb_Unit.Text.Trim()));
                    gd_UnitAPeople.RecordCount = proid.Count;
                    res = blpro.FindByIdList(proid).ToList();
                    page = 6;
                    break;
                case 6:
                    res = blpro.FindPaged(Convert.ToInt32(Session["SecrecyLevel"]));
                    page = 0;
                    break;
            }
            gd_UnitAPeople.DataSource = res;
            gd_UnitAPeople.DataBind();
            tb_Unit.Reset();
            gd_UnitAPeople.PageIndex = 0;
        }

        //分年份
        private List<Common.Entities.Project> FindByYear()
        {
            gd_UnitAPeople.PageIndex = 0;
            List<Common.Entities.Project> project = blpro.FindListByTime(Convert.ToInt32(ddl_PeopleName.SelectedText), Convert.ToInt32(Session["SecrecyLevel"]));
            gd_UnitAPeople.RecordCount = project.Count;
            var res = project.Skip(gd_UnitAPeople.PageIndex * gd_UnitAPeople.PageSize).Take(gd_UnitAPeople.PageSize).ToList();
            return res;
        }

        //分承担部门按项目负责人
        private List<Common.Entities.Project> FindByAgen()
        {
            gd_UnitAPeople.PageIndex = 0;
            List<int> Projectid = blpro.FindIDlistByAU(tb_Unit.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
            gd_UnitAPeople.RecordCount = Projectid.Count;
            List<Common.Entities.Project> res = new List<Common.Entities.Project>();
            if (ddl_PeopleName.SelectedValue == "0")
            {
                res = blpro.FindByIdList(Projectid).Skip(gd_UnitAPeople.PageIndex * gd_UnitAPeople.PageSize).Take(gd_UnitAPeople.PageSize).ToList();
            }
            else
            {
                res = blpro.FindByAgPeo(tb_Unit.Text.Trim(), ddl_PeopleName.SelectedText, Convert.ToInt32(Session["SecrecyLevel"])).Skip(gd_UnitAPeople.PageIndex * gd_UnitAPeople.PageSize).Take(gd_UnitAPeople.PageSize).ToList();
            }
            return res;
        }

        //分负责人按项目
        private List<Common.Entities.Project> FindByPeoPro()
        {
            gd_UnitAPeople.PageIndex = 0;
            List<int> Projectid = blpro.FindIDListByPeople(tb_Unit.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
            List<Common.Entities.Project> res = new List<Common.Entities.Project>();
            if (ddl_PeopleName.SelectedValue == "0")
            {
                res = blpro.FindByIdList(Projectid).Skip(gd_UnitAPeople.PageIndex * gd_UnitAPeople.PageSize).Take(gd_UnitAPeople.PageSize).ToList();
            }
            else
            {
                Projectid.Clear();
                Projectid.Add(blpro.SelectProjectID(ddl_PeopleName.SelectedText.Trim()));
                res = blpro.FindByIdList(Projectid).ToList();
            }
            gd_UnitAPeople.RecordCount = Projectid.Count;
            return res;
        }

        //分项目来源按项目
        private List<Common.Entities.Project> FindBySource()
        {
            gd_UnitAPeople.PageIndex = 0;
            List<int> Projectid = blpro.FindIDListBySource(tb_Unit.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
            List<Common.Entities.Project> res = new List<Common.Entities.Project>();
            if (ddl_PeopleName.SelectedValue == "0")
            {
                res = blpro.FindByIdList(Projectid).Skip(gd_UnitAPeople.PageIndex * gd_UnitAPeople.PageSize).Take(gd_UnitAPeople.PageSize).ToList();
            }
            else
            {
                Projectid.Clear();
                Projectid.Add(blpro.SelectProjectID(ddl_PeopleName.SelectedText.Trim()));
                res = blpro.FindByIdList(Projectid).ToList();
            }
            gd_UnitAPeople.RecordCount = Projectid.Count;
            return res;
        }

        //分项目类型按项目
        private List<Common.Entities.Project> FindByLevel()
        {
            gd_UnitAPeople.PageIndex = 0;
            List<int> Projectid = blpro.FindIDListByProlevel(tb_Unit.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
            List<Common.Entities.Project> res = new List<Common.Entities.Project>();
            if (ddl_PeopleName.SelectedValue == "0")
            {
                res = blpro.FindByIdList(Projectid).Skip(gd_UnitAPeople.PageIndex * gd_UnitAPeople.PageSize).Take(gd_UnitAPeople.PageSize).ToList();
            }
            else
            {
                Projectid.Clear();
                Projectid.Add(blpro.SelectProjectID(ddl_PeopleName.SelectedText));
                res = blpro.FindByIdList(Projectid).ToList();
            }
            gd_UnitAPeople.RecordCount = Projectid.Count;
            return res;
        }

        //分页
        protected void ddlGridPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            gd_UnitAPeople.PageIndex = 0;
            gd_UnitAPeople.PageSize = Convert.ToInt32(ddlGridPageSize.SelectedText);
            List<Common.Entities.Project> res = new List<Common.Entities.Project>();
            switch (page)
            {
                case 0:
                    InitData();
                    return;
                case 1:
                    res = FindByYear();
                    break;
                case 2:
                    res = FindByAgen();
                    break;
                case 3:
                    res = FindByPeoPro();
                    break;
                case 4:
                    res = FindBySource();
                    break;
                case 5:
                    res = FindByLevel();
                    break;
                case 6:
                    List<int> proid = new List<int>();
                    proid.Add(blpro.SelectProjectID(tb_Unit.Text.Trim()));
                    gd_UnitAPeople.RecordCount = proid.Count;
                    res = blpro.FindByIdList(proid).ToList();
                    break;
            }
            gd_UnitAPeople.DataSource = res;
            gd_UnitAPeople.DataBind();
        }

        protected void gd_UnitAPeople_PageIndexChange(object sender, GridPageEventArgs e)
        {
            gd_UnitAPeople.PageIndex = e.NewPageIndex;
            List<Common.Entities.Project> res = new List<Common.Entities.Project>();
            switch (page)
            {
                case 0:
                    InitData();
                    return;
                case 1:
                    res = FindByYear();
                    break;
                case 2:
                    res = FindByAgen();
                    break;
                case 3:
                    res = FindByPeoPro();
                    break;
                case 4:
                    res = FindBySource();
                    break;
                case 5:
                    res = FindByLevel();
                    break;
                case 6:
                    List<int> proid = new List<int>();
                    proid.Add(blpro.SelectProjectID(tb_Unit.Text.Trim()));
                    gd_UnitAPeople.RecordCount = proid.Count;
                    res = blpro.FindByIdList(proid).ToList();
                    break;
            }
            gd_UnitAPeople.DataSource = res;
            gd_UnitAPeople.DataBind();
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            InitData();
            ddl_UnitALl.SelectedValue = "6";
            tb_Unit.Text = "";
            tb_Unit.Enabled = false;
            ddl_PeopleName.Enabled = false;
            lb_Change.Text = "";
            lb_Name.Text = "";
        }
        //grid序号
        public int RowNumber(int dataItemIndex)
        {
            return dataItemIndex + (gd_UnitAPeople.PageIndex) * gd_UnitAPeople.PageSize;
        }
    }
}