/**编写人：李金秋
 * 时间：2014年8月16号
 * 功能：合同借阅记录查询界面
 * 修改履历：
 **/
using FineUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1.ContractAndPact.Pact
{
    public partial class Update_Pact : System.Web.UI.Page
    {
        BLHelper.BLLPact BLLPact = new BLHelper.BLLPact();
        BLHelper.BLLLibraryRecord BLLLibraryRecord = new BLHelper.BLLLibraryRecord();
        BLHelper.BLLUser BLLUser = new BLHelper.BLLUser();
        BLHelper.BLLOperationLog op = new BLHelper.BLLOperationLog();
        Common.Entities.OperationLog operate = new Common.Entities.OperationLog();
        BLCommon.PublicMethod publicMethod = new BLCommon.PublicMethod();
        static int page;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //删除数据
                //btnDelete.OnClientClick = Grid_LibraryRecord_Pact.GetNoSelectionAlertReference("请至少选择一项！");
                //初始化合同编号下拉框
                BindDropDownListContract();
                BindData();
                //reprotLibraryRecord.OnClientClick = WindowReport.GetShowReference("~/Report/R_Pact_LibraryRecord.aspx", "借阅统计");
                //report.OnClientClick = WindowReport.GetShowReference("~/Report/R_Pact_addsub.aspx", "增减统计");
                //登陆用户为管理员则删除、更新按钮显示
                if (Convert.ToInt32(Session["SecrecyLevel"]) == 5)
                {
                    btnUpdate.Hidden = false;
                    btnDelete.Hidden = false;
                }
            }
        }
        public void BindData()
        {
            try
            {
                page = 0;
                List<Common.Entities.LibraryRecord> list = new List<Common.Entities.LibraryRecord>();
                if (Convert.ToInt32(Session["SecrecyLevel"]) == 5)
                {
                    list = BLLLibraryRecord.FindAll("合同");
                }
                else
                {
                    int UserInfoID = BLLUser.FindID(Session["LoginName"].ToString());
                    list = BLLLibraryRecord.FindByBorrowPeopel(UserInfoID, "合同");
                }
                var res = list.Skip(Grid_LibraryRecord_Pact.PageIndex * Grid_LibraryRecord_Pact.PageSize).Take(Grid_LibraryRecord_Pact.PageSize).ToList();
                Grid_LibraryRecord_Pact.RecordCount = list.Count();
                Grid_LibraryRecord_Pact.DataSource = res;
                Grid_LibraryRecord_Pact.DataBind();
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                publicMethod.SaveError(ex, this.Request);
            }

        }
        //初始化合同编号下拉框
        public void BindDropDownListContract()
        {
            List<string> listName = BLLPact.FindPactNumBySecrecyLevel(Convert.ToInt32(Session["SecrecyLevel"]));
            if (listName != null)
            {
                for (int i = 0; i < listName.Count(); i++)
                    DropDownListPact.Items.Add(listName[i].ToString(), (i + 1).ToString());
            }
        }
        //将PactID转换为资料名称
        public string FindByPactIDAndSort(int LibraryRecordID, string Sort)
        {
            try
            {
                int? ContractID = BLLLibraryRecord.FindByLibreryRecordID(LibraryRecordID).ContractID;
                string strName = BLLLibraryRecord.FindByContractIDAndSort(ContractID, Sort);
                if (strName == null)
                    return " ";
                else
                    return strName;
            }
            catch (Exception ex)
            {
                BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
                pm.SaveError(ex, this.Request);
                return "";
            }
        }
        //将人员ID转换为人名
        public string FindBorrowPeople(int LibraryRecordID)
        {
            try
            {
                int? UserInfoID = BLLLibraryRecord.FindByLibreryRecordID(LibraryRecordID).UserInfoID;
                string strName = BLLUser.FindUserName(UserInfoID);
                if (strName != null)
                    return strName;
                else
                    return " ";
            }
            catch (Exception ex)
            {
                BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
                pm.SaveError(ex, this.Request);
                return "";
            }
        }
        //分页
        protected void Grid_LibraryRecord_Pact_PageIndexChange(object sender, FineUI.GridPageEventArgs e)
        {
            this.Grid_LibraryRecord_Pact.PageIndex = e.NewPageIndex;
            //BindData();
            switch (page)
            {
                case 0:
                    BindData();
                    break;
                case 1:
                    check();
                    break;
            }
        }
        protected void Grid_LibraryRecord_Pact_RowCommand(object sender, FineUI.GridCommandEventArgs e)
        {
            int LibraryRecordID = Convert.ToInt32(Grid_LibraryRecord_Pact.DataKeys[e.RowIndex][0].ToString());
            //string Person = BLLLibraryRecord.FindByLibreryRecordID(LibraryRecordID).EntryPerson;
            string Person = Grid_LibraryRecord_Pact.Rows[e.RowIndex].Values[2].ToString();
            string strs = Session["LoginName"].ToString();
            strs = BLLUser.FindByLoginName(Session["LoginName"].ToString()).UserName;
            if (Person != strs && Convert.ToInt32(Session["SecrecyLevel"]) != 5)
            {
                string str = "您无对此行操作的权限！此行信息为" + Person + "录入，请与管理员联系!";
                BoxSelect_PactRecord.SetCheckedState(e.RowIndex, false);
                Alert.ShowInTop(str);
            }
            int m;
            //取整数（不是四舍五入，全舍）
            int Pages = (int)Math.Floor(Convert.ToDouble(Grid_LibraryRecord_Pact.RecordCount / this.Grid_LibraryRecord_Pact.PageSize));

            if (Grid_LibraryRecord_Pact.PageIndex == Pages)
                m = (Grid_LibraryRecord_Pact.RecordCount - this.Grid_LibraryRecord_Pact.PageSize * Grid_LibraryRecord_Pact.PageIndex);
            else
                m = this.Grid_LibraryRecord_Pact.PageSize;
            List<int> selections = new List<int>();
            for (int i = 0; i < m; i++)
            {
                if (BoxSelect_PactRecord.GetCheckedState(i))
                    selections.Add(i);
            }
            if (selections.Count == 0)
            {
                btnDelete.Enabled = false;
                //Alert.ShowInTop("请至少选择一项!");
                return;
            }
            else
                btnDelete.Enabled = true;
        }
        protected void ddlGridPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid_LibraryRecord_Pact.PageIndex = 0;
            this.Grid_LibraryRecord_Pact.PageSize = Convert.ToInt32(ddlGridPageSize.SelectedValue);
            //BindData();
            switch (page)
            {
                case 0:
                    BindData();
                    break;
                case 1:
                    check();
                    break;
            }
        }
        //搜索
        protected void btnCheck_Click(object sender, EventArgs e)
        {
            check();
            btnDelete.Enabled = false;
            Grid_LibraryRecord_Pact.PageIndex = 0;
        }
        public void check()
        {
            try
            {
                if (DropDownListPact.SelectedText == "全部")
                {
                    BindData();
                    return;
                }
                else
                {
                    page = 1;
                    Grid_LibraryRecord_Pact.PageIndex = 0;
                    string PactNum = DropDownListPact.SelectedText;
                    //int PactID = BLLPact.FindByPactNum(ContractHeadline).PactID;
                    //根据资料编号和用户名(非管理员)查询借阅信息
                    //string UserName = BLLUser.FindByLoginName(Session["LoginName"].ToString()).UserName;
                    int UserID = BLLUser.FindByLoginName(Session["LoginName"].ToString()).UserInfoID;
                    List<Common.Entities.LibraryRecord> list = new List<Common.Entities.LibraryRecord>();
                    if (Convert.ToInt32(Session["SecrecyLevel"]) != 5)
                    {
                        //根据资料编号和用户名(非管理员)查询借阅信息
                        //string UserName = BLLUser.FindByLoginName(Session["LoginName"].ToString()).UserName;
                        list = BLLLibraryRecord.FindRecordByPactNum(PactNum, UserID);
                    }
                    //管理员查询
                    else
                    {
                        list = BLLLibraryRecord.FindByPactNum(DropDownListPact.SelectedText);
                    }
                    if (list != null)
                    {
                        var res = list.Skip(Grid_LibraryRecord_Pact.PageIndex * Grid_LibraryRecord_Pact.PageSize).Take(Grid_LibraryRecord_Pact.PageSize).ToList();
                        Grid_LibraryRecord_Pact.RecordCount = list.Count();
                        Grid_LibraryRecord_Pact.DataSource = res;
                        Grid_LibraryRecord_Pact.DataBind();
                        btnDelete.Enabled = false;
                    }
                    else
                        return;
                }
            }
            catch (Exception ex)
            {
                publicMethod.SaveError(ex, this.Request);
            }
        }
        //刷新
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            btnDelete.Enabled = false;
            DropDownListPact.SelectedValue = "0";
            BindData();
        }
        //删除借阅记录
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int m;
                //取整数（不是四舍五入，全舍）
                int Pages = (int)Math.Floor(Convert.ToDouble(Grid_LibraryRecord_Pact.RecordCount / this.Grid_LibraryRecord_Pact.PageSize));
                List<int> selections = new List<int>();
                if (Grid_LibraryRecord_Pact.PageIndex == Pages)
                    m = (Grid_LibraryRecord_Pact.RecordCount - this.Grid_LibraryRecord_Pact.PageSize * Grid_LibraryRecord_Pact.PageIndex);
                else
                    m = this.Grid_LibraryRecord_Pact.PageSize;
                for (int i = 0; i < m; i++)
                {
                    if (BoxSelect_PactRecord.GetCheckedState(i))
                        selections.Add(i);
                }

                if (Convert.ToInt32(Session["SecrecyLevel"]) == 5)
                {
                    for (int i = 0; i < selections.Count(); i++)
                    {
                        BLLLibraryRecord.Delete(Convert.ToInt32(Grid_LibraryRecord_Pact.DataKeys[selections[i]][0].ToString()));
                    }
                    //刷新
                    btnDelete.Enabled = false;
                    DropDownListPact.SelectedValue = "0";
                    Grid_LibraryRecord_Pact.PageIndex = 0;
                    Grid_LibraryRecord_Pact.PageSize = 20;
                    BindData();
                    Alert.ShowInTop("删除数据成功!");
                }
                else
                {
                    for (int i = 0; i < selections.Count(); i++)
                    {
                        BLLPact.UpdateIsPass(Convert.ToInt32(Grid_LibraryRecord_Pact.DataKeys[selections[i]][0]), false);
                        operate.LoginName = Session["LoginName"].ToString();
                        operate.OperationTime = DateTime.Now;
                        operate.LoginIP = " ";
                        operate.OperationContent = "Pact";
                        operate.OperationType = "删除";
                        operate.OperationDataID = Convert.ToInt32(Grid_LibraryRecord_Pact.DataKeys[selections[i]][0]);
                        op.Insert(operate);
                    }
                    //刷新
                    btnDelete.Enabled = false;
                    DropDownListPact.SelectedValue = "0";
                    Grid_LibraryRecord_Pact.PageIndex = 0;
                    Grid_LibraryRecord_Pact.PageSize = 20;
                    BindData();
                    Alert.ShowInTop("操作已经提交，请等待管理员确认!");
                }
            }
            catch (Exception ex)
            {
                BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
                pm.SaveError(ex, this.Request);
            }
        }
        //编辑选中行
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                int m;
                //取整数（不是四舍五入，全舍）
                int Pages = (int)Math.Floor(Convert.ToDouble(Grid_LibraryRecord_Pact.RecordCount / this.Grid_LibraryRecord_Pact.PageSize));
                List<int> selections = new List<int>();
                if (Grid_LibraryRecord_Pact.PageIndex == Pages)
                    m = (Grid_LibraryRecord_Pact.RecordCount - this.Grid_LibraryRecord_Pact.PageSize * Grid_LibraryRecord_Pact.PageIndex);
                else
                    m = this.Grid_LibraryRecord_Pact.PageSize;
                for (int i = 0; i < m; i++)
                {
                    if (BoxSelect_PactRecord.GetCheckedState(i))
                        selections.Add(i);
                }
                if (selections.Count() != 0)
                {
                    if (selections.Count() == 1)
                    {
                        int rowID = Convert.ToInt32(Grid_LibraryRecord_Pact.DataKeys[selections[0]][0]);
                        Session["LibraryRecordID"] = rowID;
                        Alert.Show("你确定要修改该行数据吗!", "确认消息", MessageBoxIcon.Information, Window_Update.GetShowReference("Update_PactRecord.aspx", "编辑借阅记录信息"), Target.Top);
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
                BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
                pm.SaveError(ex, this.Request);
            }
        }

        //grid序号
        public int RowNumber(int dataItemIndex)
        {
            return dataItemIndex + (Grid_LibraryRecord_Pact.PageIndex) * Grid_LibraryRecord_Pact.PageSize;
        }
        //转化等级
        public string ChangeSecrecyLevel(int level)
        {
            string[] SecrecyLevels = new string[] { "四级", "三级", "二级", "一级", "管理员" };
            return SecrecyLevels[level - 1];
        }
    }
}