/*
 * 修改者：高琪
 * 修改时间:2015.11.28
 * 修改内容：对应新增信息网页，达到更改页面一致性
 */
using FineUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WDFramework.ContractAndPact.Pact
{
    public partial class Update_PactRecord : System.Web.UI.Page
    {
        BLHelper.BLLAttachment attachment = new BLHelper.BLLAttachment();
        Common.Entities.Pact achieve = new Common.Entities.Pact();
        BLHelper.BLLProject BLLProject = new BLHelper.BLLProject();
        BLHelper.BLLLibraryRecord BLLLibraryRecord = new BLHelper.BLLLibraryRecord();
        BLHelper.BLLPact BLLPact = new BLHelper.BLLPact();
        BLHelper.BLLUser BLLUser = new BLHelper.BLLUser();
        Common.Entities.LibraryRecord libraryRecord = new Common.Entities.LibraryRecord();
        Common.Entities.LibraryRecord NewLibraryRecord = new Common.Entities.LibraryRecord();
        BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
        BLHelper.BLLUser user = new BLHelper.BLLUser();
        //Common.Entities.Pact Pacts = new Common.Entities.Pact();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //int ContractID = Convert.ToInt32(Request.QueryString["id"].ToString());
                //等级下拉框绑定(可选等级不大于登陆等级)
                /*string[] arraySecrecyLevel = new string[5] { "四级", "三级", "二级", "一级", "管理员" };
                for (int i = 0; i < Convert.ToInt32(Session["SecrecyLevel"]); i++)
                    DropDownList_SecrecyLevel.Items.Add(arraySecrecyLevel[i], (i + 1).ToString());
                Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);*/               
                //BindData();
                InitSecrecyLevel();
                InitProject();
                InitData();
                //UpDateValue();
            }
        }
        //初始化等级下拉框
        public void InitSecrecyLevel()
        {
            try
            {
                for (int i = 0; i < Convert.ToInt32(Session["SecrecyLevel"]); i++)
                {
                    string[] SecrecyLevels = new string[] { "四级", "三级", "二级", "一级", "管理员" };
                    DropDownList_SecrecyLevel.Items.Add(SecrecyLevels[i], i.ToString());
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);

            }
        }
        //初始化所属项目
        public void InitProject()
        {
            try
            {
                List<Common.Entities.Project> list = BLLProject.FindBySecrecyLevel(Convert.ToInt32(Session["SecrecyLevel"]));
                for (int i = 0; i < list.Count(); i++)
                    DropDownList_Project.Items.Add(list[i].ProjectName, (i + 1).ToString());
            }
            catch
            {
 
            }
        }
        //初始化界面
        public void InitData()
        {
            Common.Entities.Pact ac = BLLPact.FindAll(Convert.ToInt32(Session["PactID"]));
            txtPactNum.Text = ac.PactNum;
            txtPactName.Text = ac.PactName;
            txtPactType.Text = ac.PactType;
            DatePicker_StartTime.SelectedDate = ac.StartTime;
            DatePicker_EndTime.SelectedDate = ac.EndTime;
            if (ac.ProjectID == 118)
                DropDownList_Project.SelectedValue = "1";
            else if (ac.ProjectID == 119)
                DropDownList_Project.SelectedValue = "2";
            DropDownList_SecrecyLevel.SelectedIndex = Convert.ToInt32(ac.SecrecyLevel - 1);
            txtChargePerson.Text = ac.ChargePerson;
            txtPactMoney.Text = ac.PactMoney;
            txtRealMoney.Text = ac.RealMoney;
            txtPactCompletion.Text = ac.PactCompletion;
            txtIsExistingFile.Text = ac.IsExistingFile;
            txtFileNum.Text = ac.FileNum;
        }
        //更新赋值
       /* public void UpDateValue()
        {
            achieve.PactNum = txtPactNum.Text;
            achieve.PactName = txtPactName.Text;
            achieve.PactType = txtPactType.Text;
            achieve.StartTime = DatePicker_StartTime.SelectedDate;
            achieve.EndTime = DatePicker_EndTime.SelectedDate;
            achieve.ProjectID = BLLProject.SelectProjectID(DropDownList_Project.SelectedText);
            achieve.SecrecyLevel = Convert.ToInt32(DropDownList_SecrecyLevel.SelectedIndex + 1);
            achieve.ChargePerson = txtChargePerson.Text;
            achieve.PactMoney = txtPactMoney.Text;
            achieve.RealMoney = txtRealMoney.Text;
            achieve.PactCompletion = txtPactCompletion.Text;
            achieve.IsExistingFile = txtIsExistingFile.Text;
            achieve.FileNum = txtFileNum.Text;
            //achieve.EntryPerson = BLLPact.FindByPactID(Convert.ToInt32(Session["PactID"])).EntryPerson;

        }*/
        //原版本
        /*public void BindData()
        {
            //int ContractID = Convert.ToInt32(Request.QueryString["id"].ToString());
            int LibraryRecordID = Convert.ToInt32(Session["LibraryRecordID"]);
            if (LibraryRecordID != 0)
            {
                libraryRecord = BLLLibraryRecord.FindByLibreryRecordID(LibraryRecordID);
                txtPactNum.Text = BLLPact.FindByPactID(Convert.ToInt32(libraryRecord.ContractID)).PactNum;
                txtBorrowPeople.Text = BLLUser.FindUserName(libraryRecord.UserInfoID);
                DatePicker_BorrowTime.SelectedDate = libraryRecord.BorrowTime;
                DatePicker_ReturnTime.SelectedDate = libraryRecord.ReturnTime;
            }
        }*/
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                achieve.PactNum = txtPactNum.Text;
                achieve.PactName = txtPactName.Text;
                achieve.PactType = txtPactType.Text;
                achieve.StartTime = DatePicker_StartTime.SelectedDate;
                achieve.EndTime = DatePicker_EndTime.SelectedDate;
                achieve.ProjectID = BLLProject.SelectProjectID(DropDownList_Project.SelectedText);
                achieve.SecrecyLevel = Convert.ToInt32(DropDownList_SecrecyLevel.SelectedIndex + 1);
                achieve.ChargePerson = txtChargePerson.Text;
                achieve.PactMoney = txtPactMoney.Text;
                achieve.RealMoney = txtRealMoney.Text;
                achieve.PactCompletion = txtPactCompletion.Text;
                achieve.IsExistingFile = txtIsExistingFile.Text;
                achieve.FileNum = txtFileNum.Text;

                string username = user.FindByLoginName(Session["LoginName"].ToString()).UserName;
                achieve.EntryPerson = username;
                achieve.SecrecyLevel = Convert.ToInt32(DropDownList_SecrecyLevel.SelectedIndex + 1);
                int AttachmentID = Convert.ToInt32(BLLPact.FindByPactID(Convert.ToInt32(Session["PactID"])).AttachmentID);
                string path = attachment.FindPath(AttachmentID);
                achieve.AttachmentID = AttachmentID;
                achieve.PactID = Convert.ToInt32(Session["PactID"]);
                achieve.IsPass = true;
                BLLPact.update(achieve);//插入文件表
                PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("保存成功"));
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
        /*protected void btnSave_Click(object sender, EventArgs e)
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
                {
                    BLLLibraryRecord.Update(NewLibraryRecord);
                    Alert.ShowInTop("保存成功！");
                }
                else
                    Alert.ShowInTop("保存失败！");
                PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference());
            }
            catch (Exception ex)
            {
                BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
                pm.SaveError(ex, this.Request);
            }
        }*/

        protected void btnReSet_Click(object sender, EventArgs e)
        {
            txtPactNum.Reset();
            txtPactName.Reset();
            txtPactType.Reset();
            DatePicker_StartTime.Reset();
            DatePicker_EndTime.Reset();
            DropDownList_Project.Reset();
            DropDownList_SecrecyLevel.Reset();
            txtChargePerson.Reset();
            txtPactMoney.Reset();
            txtRealMoney.Reset();
            txtPactCompletion.Reset();
            txtIsExistingFile.Reset();
            txtFileNum.Reset();
            //txtPactNum.Reset();
            //txtBorrowPeople.Reset();
            //DatePicker_BorrowTime.Reset();
            //DatePicker_ReturnTime.Reset();
            //DropDownList_SecrecyLevel.SelectedValue = "1";
        }
        //获取借阅信息
       /* public Common.Entities.LibraryRecord FindLibraryRecord()
        {
            NewLibraryRecord.BorrowTime =DatePicker_BorrowTime.SelectedDate;
            NewLibraryRecord.EntryPerson = BLLUser.FindByLoginName(Session["LoginName"].ToString()).UserName;
            NewLibraryRecord.IsPass = true;
            NewLibraryRecord.ContractID = BLLPact.FindByPactNum(txtPactNum.Text).PactID;
            NewLibraryRecord.ReturnTime = DatePicker_ReturnTime.SelectedDate;
            NewLibraryRecord.SecrecyLevel = Convert.ToInt32(DropDownList_SecrecyLevel.SelectedValue);
            NewLibraryRecord.Sort = "合同";
            NewLibraryRecord.UserInfoID = BLLUser.FindByUserName(txtBorrowPeople.Text).UserInfoID;
            return NewLibraryRecord;
        }*/
    }
}