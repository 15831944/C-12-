using FineUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WDFramework.ContractAndPact.Pact
{
    public partial class NewAdd_PactRecord : System.Web.UI.Page
    {
        BLHelper.BLLPact BLLPact = new BLHelper.BLLPact();
        BLHelper.BLLUser BLLUser = new BLHelper.BLLUser();
        BLHelper.BLLLibraryRecord BLLLibraryRecord = new BLHelper.BLLLibraryRecord();
        Common.Entities.LibraryRecord LibraryRecord = new Common.Entities.LibraryRecord();
        Common.Entities.LibraryRecord NewLibraryRecord = new Common.Entities.LibraryRecord();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int ContractID = Convert.ToInt32(Request.QueryString["id"].ToString());
                //资料名绑定FindByContractID
                txtPactNum.Text = BLLPact.FindByPactID(ContractID).PactNum;
                //等级下拉框绑定(可选等级不大于登陆等级)
                string[] arraySecrecyLevel = new string[5] { "四级", "三级", "二级", "一级", "管理员" };
                for (int i = 0; i < Convert.ToInt32(Session["SecrecyLevel"]); i++)
                    DropDownList_SecrecyLevel.Items.Add(arraySecrecyLevel[i], (i + 1).ToString());
                DatePicker_BorrowTime.MaxDate = DateTime.Now;
                //DatePicker_ReturnTime.MaxDate = DateTime.Now;
            }
        }
        //保存
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (BLLUser.FindByUserName(txtBorrowPeople.Text) == null)
                {
                    Alert.ShowInTop("借阅人不存在，请重新输入！");
                    return;
                }
                if (DatePicker_BorrowTime.SelectedDate > DatePicker_ReturnTime.SelectedDate)
                {
                    Alert.ShowInTop("归还日期应该大于借阅日期！");
                    return;
                }
                NewLibraryRecord = FindLibraryRecord();
                if (NewLibraryRecord != null)
                {
                    BLLLibraryRecord.Insert(NewLibraryRecord);
                    Alert.ShowInTop("保存成功！");
                    PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference());
                }
            }
            catch (Exception ex)
            {
                BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
                pm.SaveError(ex, this.Request);
            }
        }
        //重置
        protected void btnReSet_Click(object sender, EventArgs e)
        {
            txtBorrowPeople.Text = " ";
            //txtContractName.Text = " ";
            DatePicker_BorrowTime.Text = " ";
            DatePicker_ReturnTime.Text = " ";
            DropDownList_SecrecyLevel.SelectedValue = "1";
        }
        //取消
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
        //获取合同借阅信息
        public Common.Entities.LibraryRecord FindLibraryRecord()
        {
            LibraryRecord.BorrowTime = DatePicker_BorrowTime.SelectedDate;
            //LibraryRecord.EntryPerson = Session["LoginName"].ToString();
            LibraryRecord.EntryPerson = BLLUser.FindByLoginName(Session["LoginName"].ToString()).UserName;
            LibraryRecord.IsPass = true;
            LibraryRecord.ContractID = BLLPact.FindByPactNum(txtPactNum.Text).PactID;
            LibraryRecord.ReturnTime = DatePicker_ReturnTime.SelectedDate;
            LibraryRecord.SecrecyLevel = Convert.ToInt32(DropDownList_SecrecyLevel.SelectedValue);
            LibraryRecord.Sort = "合同";
            LibraryRecord.UserInfoID = BLLUser.FindByUserName(txtBorrowPeople.Text).UserInfoID;
            return LibraryRecord;

        }
    }
}