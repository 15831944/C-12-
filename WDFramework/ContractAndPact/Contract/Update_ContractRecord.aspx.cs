using FineUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WDFramework.ContractAndPact.Contract
{
    public partial class Update_ContractRecord : System.Web.UI.Page
    {
        BLHelper.BLLLibraryRecord BLLLibraryRecord = new BLHelper.BLLLibraryRecord();
        BLHelper.BLLContract BLLContract = new BLHelper.BLLContract();
        BLHelper.BLLUser BLLUser = new BLHelper.BLLUser();
        Common.Entities.LibraryRecord libraryRecord = new Common.Entities.LibraryRecord();
        Common.Entities.LibraryRecord NewLibraryRecord = new Common.Entities.LibraryRecord();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //int ContractID = Convert.ToInt32(Request.QueryString["id"].ToString());
                //等级下拉框绑定(可选等级不大于登陆等级)
                string[] arraySecrecyLevel = new string[5] { "四级", "三级", "二级", "一级", "管理员" };
                for (int i = 0; i < Convert.ToInt32(Session["SecrecyLevel"]); i++)
                    DropDownList_SecrecyLevel.Items.Add(arraySecrecyLevel[i], (i + 1).ToString());
                BindData();
            }
        }
        public void BindData()
        {
            //int ContractID = Convert.ToInt32(Request.QueryString["id"].ToString());
            int LibraryRecordID = Convert.ToInt32(Session["LibraryRecordID"]);
            if (LibraryRecordID != 0)
            {
                libraryRecord = BLLLibraryRecord.FindByLibreryRecordID(LibraryRecordID);
                txtContractName.Text = BLLContract.FindByContractID(libraryRecord.ContractID).ContractHeadLine;
                txtBorrowPeople.Text = BLLUser.FindUserName(libraryRecord.UserInfoID);
                DatePicker_BorrowTime.SelectedDate = libraryRecord.BorrowTime;
                DatePicker_ReturnTime.SelectedDate = libraryRecord.ReturnTime;
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
                    Alert.ShowInTop("结束日期应该大于开始日期！");
                    return;
                }
                int LibraryRecordID = Convert.ToInt32(Session["LibraryRecordID"]);
                NewLibraryRecord = BLLLibraryRecord.FindByLibreryRecordID(LibraryRecordID);
                NewLibraryRecord = FindLibraryRecord();
                if (NewLibraryRecord != null)
                    BLLLibraryRecord.Update(NewLibraryRecord);
                Alert.ShowInTop("保存成功！");
                PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference());
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
            txtBorrowPeople.Reset();
            //txtContractName.Text = " ";
            DatePicker_BorrowTime.Reset();
            DatePicker_ReturnTime.Reset();
            DropDownList_SecrecyLevel.SelectedValue = "1";
        }
        //获取借阅信息
        public Common.Entities.LibraryRecord FindLibraryRecord()
        {
            int LibraryRecordID = Convert.ToInt32(Session["LibraryRecordID"]);
            NewLibraryRecord.BorrowTime = Convert.ToDateTime(DatePicker_BorrowTime.Text);
            NewLibraryRecord.EntryPerson = BLLUser.FindByLoginName(Session["LoginName"].ToString()).UserName;
            NewLibraryRecord.IsPass = true;
            //NewLibraryRecord.ContractID = BLLContract.FindByContractHeadLine(txtContractName.Text, Convert.ToInt32(Session["SecrecyLevel"])).ContractID;
            NewLibraryRecord.ContractID = BLLLibraryRecord.FindByLibreryRecordID(LibraryRecordID).ContractID;
            NewLibraryRecord.ReturnTime = DatePicker_ReturnTime.SelectedDate;
            NewLibraryRecord.SecrecyLevel = Convert.ToInt32(DropDownList_SecrecyLevel.SelectedValue);
            NewLibraryRecord.Sort = "资料";
            NewLibraryRecord.UserInfoID = BLLUser.FindByUserName(txtBorrowPeople.Text).UserInfoID;
            return NewLibraryRecord;
        }
    }
}