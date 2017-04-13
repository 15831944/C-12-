/**编写人：张凡凡
 * 时间：2014年8月18号
 * 功能：登记进账经费后台的相关操作
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
    public partial class MoneyComeIn : System.Web.UI.Page
    {
        BLHelper.BLLProject blpro = new BLHelper.BLLProject();
        BLHelper.BLLFundInformation blfund = new BLHelper.BLLFundInformation();
        BLHelper.BLLOperationLog blop = new BLHelper.BLLOperationLog();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
                string[] arraySecrecyLevel = new string[5] { "四级", "三级", "二级", "一级", "管理员" };
                for (int i = 0; i < Convert.ToInt32(Session["SecrecyLevel"]); i++)
                {
                    ddl_level.Items.Add(arraySecrecyLevel[i], i.ToString());
                }
            }
        }

        protected void btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                if (tb_MoneyNum.Text.Trim() == "")
                {
                    Alert.ShowInTop("每项用途所用金额不能为空！");
                    tb_MoneyNum.Text = "";
                    return;
                }
                Common.Entities.Project project = blpro.IsNullProject(tb_ProjectName.Text.Trim());
                if (project == null)
                {
                    tb_ProjectName.Text = "";
                    Alert.ShowInTop("无此项目，请检查输入！");
                    return;
                }
                else if (project.IsPass == false)
                {
                    tb_ProjectName.Text = "";
                    Alert.ShowInTop("此项目正在审核中，请联系管理员！");
                    return;
                }
                double num = 0.0;
                if (!double.TryParse(tb_MoneyNum.Text.Trim(), out num))
                {
                    tb_MoneyNum.Reset();
                    Alert.ShowInTop("请输入数字！");
                    return;
                }
                if (dp_Time.SelectedDate == null)
                {
                    Alert.ShowInTop("请选择日期！");
                    return;
                }
                Common.Entities.FundInformation fun = new Common.Entities.FundInformation();
                Common.Entities.OperationLog op = new Common.Entities.OperationLog();


                fun.BudgetDirector = tb_BudgetDirector.Text.Trim ();

                BLHelper.BLLUser user = new BLHelper.BLLUser();
                string username = user.FindByLoginName(Session["LoginName"].ToString()).UserName;
                fun.EntryPerson = username;
                fun.EveItemUseMoney = tb_MoneyNum.Text.Trim ();
                fun.OperateType = "进账";
                fun.ProjectID = blpro.SelectProjectID(tb_ProjectName.Text);
                fun.SecrecyLevel = Convert.ToInt32(ddl_level.SelectedValue) + 1;
                fun.Time = dp_Time.SelectedDate;
                if (Convert.ToInt32(Session["SecrecyLevel"]) == 5)
                {
                    fun.IsPass = true;
                    blfund.Insert(fun);
                    PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("保存成功！"));
                }
                else
                {
                    fun.IsPass = false;
                    blfund.Insert(fun);
                    op.LoginIP = " ";
                    op.LoginName = Session["LoginName"].ToString();
                    op.OperationContent = "FundInformation";
                    op.OperationDataID = fun.FundInformationID;
                    op.OperationTime = DateTime.Now;
                    op.OperationType = "添加";
                    blop.Insert(op);
                    PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHidePostBackReference() + Alert.GetShowInTopReference("数据已经提交，请等待管理员确认！"));
                }
            }
            catch (Exception ex)
            {
                BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
                pm.SaveError(ex, this.Request);
            }
        }

        protected void tb_ProjectName_TextChanged(object sender, EventArgs e)
        {
            Common.Entities.Project project = blpro.IsNullProject(tb_ProjectName.Text.Trim());
            if (project == null)
            {
                tb_ProjectName.Text = "";
                Alert.ShowInTop("无此项目，请检查输入！");
            }
            else if (project.IsPass == false)
            {
                tb_ProjectName.Text = "";
                Alert.ShowInTop("此项目正在审核中，请联系管理员！");
                return;
            }
            else
                return;
        }

        protected void btn_Delete_Click(object sender, EventArgs e)
        {
            tb_BudgetDirector.Reset();
            tb_MoneyNum.Reset();
            tb_ProjectName.Reset();
            ddl_level.Reset();
            dp_Time.Reset();
        }

        protected void tb_MoneyNum_TextChanged(object sender, EventArgs e)
        {
            double num = 0.0;
            if (!double.TryParse(tb_MoneyNum.Text.Trim(), out num))
            {
                tb_MoneyNum.Reset();
                Alert.ShowInTop("请输入数字！");
                return;
            }
        }

    }
}