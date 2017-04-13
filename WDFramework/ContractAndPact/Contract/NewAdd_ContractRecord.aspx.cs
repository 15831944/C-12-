using FineUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WDFramework.ContractAndPact.Contract
{
    public partial class NewAdd_ContractRecord : System.Web.UI.Page
    {
        BLHelper.BLLLibraryRecord BLLLibraryRecord = new BLHelper.BLLLibraryRecord();
        BLHelper.BLLContract BLLContract = new BLHelper.BLLContract();
        BLHelper.BLLUser BLLUser = new BLHelper.BLLUser();
        Common.Entities.LibraryRecord LibraryRecord = new Common.Entities.LibraryRecord();
        Common.Entities.LibraryRecord NewLibraryRecord = new Common.Entities.LibraryRecord();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int ContractID = Convert.ToInt32(Request.QueryString["id"].ToString());
                //资料名绑定FindByContractID
                txtContractName.Text = BLLContract.FindByContractID(ContractID).ContractHeadLine;
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
                if (BLLUser.FindByUserName(txtBorrowPeople.Text.Trim()) == null)
                {
                    Alert.ShowInTop("借阅人不存在，请重新输入！");
                    return;
                }
                if (DatePicker_BorrowTime.SelectedDate > DatePicker_ReturnTime.SelectedDate)
                {
                    Alert.ShowInTop("借阅日期应该大于归还日期！");
                    return;
                }
               
                NewLibraryRecord = FindLibraryRecord();
                if (NewLibraryRecord != null)
                    BLLLibraryRecord.Insert(NewLibraryRecord);
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
        protected void btnSet_Click(object sender, EventArgs e)
        {
            txtBorrowPeople.Reset();
            //txtContractName.Text = " ";
            DatePicker_BorrowTime.Reset();
            DatePicker_ReturnTime.Reset();
            DropDownList_SecrecyLevel.SelectedValue = "1";
        }
        //取消
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
        //获取借阅信息
        public Common.Entities.LibraryRecord FindLibraryRecord()
        {
            LibraryRecord.BorrowTime = Convert.ToDateTime(DatePicker_BorrowTime.Text);
            //LibraryRecord.EntryPerson = Session["LoginName"].ToString();
            LibraryRecord.EntryPerson = BLLUser.FindByLoginName(Session["LoginName"].ToString()).UserName;
            LibraryRecord.IsPass = true;
            //LibraryRecord.ContractID = BLLContract.FindByContractHeadLine(txtContractName.Text, Convert.ToInt32(Session["SecrecyLevel"])).ContractID;
            LibraryRecord.ContractID = Convert.ToInt32(Request.QueryString["id"].ToString());
            LibraryRecord.ReturnTime = DatePicker_ReturnTime.SelectedDate;
            LibraryRecord.SecrecyLevel = Convert.ToInt32(DropDownList_SecrecyLevel.SelectedValue);
            LibraryRecord.Sort = "资料";
            LibraryRecord.UserInfoID = BLLUser.FindByUserName(txtBorrowPeople.Text).UserInfoID;
            return LibraryRecord;
        }
        ////归还时间验证
        //protected void DatePicker_ReturnTime_TextChanged(object sender, EventArgs e)
        //{
        //    if (DatePicker_BorrowTime.SelectedDate > DatePicker_ReturnTime.SelectedDate)
        //    {
        //        //btnSave.Enabled = false;
        //        Alert.ShowInTop("结束日期应该大于开始日期！");
        //    }
        //    //else
        //    //    btnSave.Enabled = true;
        //}
        ////借阅人员验证
        //protected void txtBorrowPeople_TextChanged(object sender, EventArgs e)
        //{
        //    if (BLLUser.FindByUserName(txtBorrowPeople.Text) == null)
        //    {
        //        //btnSave.Enabled = false;
        //        Alert.ShowInTop("借阅人不存在，请重新输入！");
        //        //txtBorrowPeople.Reset();
        //        return;
        //    }
        //    //else
        //    //    btnSave.Enabled = true;
        //}
    }
}