/**编写人：张凡凡
 * 时间：2014年8月13号
 * 功能：登记支出经费后台的相关操作
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
    public partial class MoneyGiveIn : System.Web.UI.Page
    {
        BLHelper.BLLFundInformation fund = new BLHelper.BLLFundInformation();
        BLHelper.BLLProject pro = new BLHelper.BLLProject();
        BLHelper.BLLOperationLog blop = new BLHelper.BLLOperationLog();
        BLHelper.BLLBasicCode bllBasicCode = new BLHelper.BLLBasicCode();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitDropList();
                Initddl_FundingPurposeSortID();
            }
        }

        //下拉框绑定
        private void InitDropList()
        {
            //string[] arraySecrecyLevel = new string[5] { "公开", "内部", "秘密", "机密", "管理员" };
            string[] arraySecrecyLevel = new string[5] { "四级", "三级", "二级", "一级", "管理员" };
            for (int i = 0; i < Convert.ToInt32(Session["SecrecyLevel"]); i++)
                DropDownListLevel.Items.Add(arraySecrecyLevel[i], i.ToString());
            //List<string> res = fundSort.FindAll();
            //for (int i = 0; i < res.Count; i++)
            //    ddl_FundingPurposeSortID.Items.Add(res[i], i.ToString());
        }
        //初始化经费用途分类名称下拉框
        public void Initddl_FundingPurposeSortID()
        {
            List<BasicCode> list = bllBasicCode.FindALLName("经费用途分类名称");
            for (int i = 0; i < list.Count(); i++)
            {
                ddl_FundingPurposeSortID.Items.Add(list[i].CategoryContent.ToString(), (i + 1).ToString());
            }
            ddl_FundingPurposeSortID.Items[0].Selected = true;
        }
        //清空
        protected void btn_Delete_Click(object sender, EventArgs e)
        {
            tb_MoneyNum.Reset();
            tb_SourceWork.Reset();
            tb_UserInfo.Reset();
        }

        //保存
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
                Common.Entities.Project project = pro.IsNullProject(tb_SourceWork.Text.Trim());
                if (project == null)
                {
                    tb_SourceWork.Text = "";
                    Alert.ShowInTop("无此项目，请检查输入！");
                    return;
                }
                else if (project.IsPass == false)
                {
                    tb_SourceWork.Text = "";
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

                fun.BudgetDirector = tb_UserInfo.Text.Trim ();
                BLHelper.BLLUser user = new BLHelper.BLLUser();
                string username = user.FindByLoginName(Session["LoginName"].ToString()).UserName;
                fun.EntryPerson = username;
                fun.EveItemUseMoney = tb_MoneyNum.Text.Trim ();
                fun.FundingPurposeSortName = ddl_FundingPurposeSortID.Text;
                fun.OperateType = "支出";
                fun.ProjectID = project.ProjectID;
                fun.SecrecyLevel = Convert.ToInt32(DropDownListLevel.SelectedValue) + 1;
                fun.Time = dp_Time.SelectedDate;
                if (Convert.ToInt32(Session["SecrecyLevel"]) == 5)
                {
                    fun.IsPass = true;
                    fund.Insert(fun);
                    PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("保存成功！"));
                }
                else
                {
                    fun.IsPass = false;
                    fund.Insert(fun);
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

        protected void tb_SourceWork_TextChanged(object sender, EventArgs e)
        {
            Common.Entities.Project project = pro.IsNullProject(tb_SourceWork.Text.Trim());
            if (project == null)
            {
                tb_SourceWork.Text = "";
                Alert.ShowInTop("无此项目，请检查输入！");
            }
            else if(project.IsPass == false)
            {
                tb_SourceWork.Text = "";
                Alert.ShowInTop("此项目正在审核中，请联系管理员！");
                return;
            }
            else
                return;
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