using FineUI;
/**编写人：张凡凡
 * 时间：2014年8月1号
 * 功能:进修学习（接受）更新界面后台
 * 修改履历：
 **/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class Update_FutherStudy : System.Web.UI.Page
    {
        BLHelper.BLLFutherStudy FutherStudy = new BLHelper.BLLFutherStudy();
        BLHelper.BLLAgency agen = new BLHelper.BLLAgency();
        Common.Entities.FutherStudy fur = new Common.Entities.FutherStudy();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitDroplist();
                InitText();
            }
        }

        //初始化下拉框
        public void InitDroplist()
        {
            BLHelper.BLLAgency agency = new BLHelper.BLLAgency();
            List<Common.Entities.Agency> list = agency.FindAllAgencyName();
            for (int i = 0; i < list.Count(); i++)
            {
                DropDownListAgency.Items.Add(list[i].AgencyName.ToString(), (i + 1).ToString());
            }

            string[] SecrecyLevels = new string[] { "四级", "三级", "二级", "一级", "管理员" };
            for (int i = 0; i < Convert.ToInt32(Session["SecrecyLevel"]); i++)
                DroSecrecyLevel.Items.Add(SecrecyLevels[i], i.ToString());
        }

        //初始化文本框
        public void InitText()
        {
            if (Session["FurID"] != null)
            {
                fur = FutherStudy.FindFurByID(Convert.ToInt32(Session["FurID"]));
                tName.Text = fur.Name;
                if (fur.Sex == true)
                    rbtnBoy.Checked = true;
                else
                    rbtnGril.Checked = true;
                tHometown.Text = fur.Hometown;
                tIDNum.Text = fur.IDNum;
                tLearnContent.Text = fur.LearnContent;
                tLearnPlace.Text = fur.LearnPlace;
                tLearnSchool.Text = fur.LearnSchool;
                tPhoneNum.Text = fur.PhoneNum;
                tPintroduce.Text = fur.Profile;
                tRemark.Text = fur.Remark;
                tEmail.Text = fur.Email;
                tDocuType.SelectedValue = fur.DocuType;
                if (fur.AgencyID != null)
                    DropDownListAgency.Text = agen.FindAgenName(Convert.ToInt32(fur.AgencyID));
                DroSecrecyLevel.SelectedValue = (fur.SecrecyLevel - 1).ToString();
                if (fur.Birthday != null)
                    dBirthday.SelectedDate = fur.Birthday;
                else
                    dBirthday.SelectedDate = DateTime.Now;
                dLearnBeginTime.SelectedDate = fur.LearnBeginTime;
                if (fur.LearnEndTime != null)
                    dLearnEndTime.SelectedDate = fur.LearnEndTime;
            }
            else
                return;
        }
        //保存
        protected void Save_Click(object sender, EventArgs e)
        {

            BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
            try
            {
                if (tName.Text.Trim() == "")
                {
                    Alert.ShowInTop("姓名不能为空！");
                    tName.Text = "";
                    return;
                }
                if (tLearnPlace.Text.Trim() == "")
                {
                    Alert.ShowInTop("进修地点不能为空！");
                    tLearnPlace.Text = "";
                    return;
                }
                if (tLearnSchool.Text.Trim() == "")
                {
                    Alert.ShowInTop("进修学校不能为空！");
                    tLearnSchool.Text = "";
                    return;
                }
                if (tLearnContent.Text.Trim() == "")
                {
                    Alert.ShowInTop("进修内容不能为空！");
                    tLearnContent.Text = "";
                    return;
                }
                if (dLearnEndTime.SelectedDate < dLearnBeginTime.SelectedDate)
                {
                    Alert.ShowInTop("结束时间不能小于开始时间！");
                    return;
                }
                BLHelper.BLLAgency agency = new BLHelper.BLLAgency();
                Common.Entities.OperationLog op = new Common.Entities.OperationLog();
                BLHelper.BLLOperationLog blop = new BLHelper.BLLOperationLog();

                fur = FutherStudy.FindFurByID(Convert.ToInt32(Session["FurID"]));
                fur.Name = tName.Text.Trim();
                if (dBirthday.SelectedDate != null)
                    fur.Birthday = Convert.ToDateTime(dBirthday.Text.Trim());
                fur.DocuType = tDocuType.SelectedValue.Trim();
                fur.IDNum = tIDNum.Text.Trim();
                fur.Email = tEmail.Text.Trim();
                fur.Hometown = tHometown.Text.Trim();
                fur.LearnBeginTime = Convert.ToDateTime(dLearnBeginTime.Text.Trim());
                fur.LearnContent = tLearnContent.Text.Trim();
                if (dLearnEndTime.SelectedDate != null)
                    fur.LearnEndTime = Convert.ToDateTime(dLearnEndTime.Text.Trim());
                fur.LearnPlace = tLearnPlace.Text.Trim();
                fur.LearnSchool = tLearnSchool.Text.Trim();
                fur.PhoneNum = tPhoneNum.Text.Trim();
                fur.Profile = tPintroduce.Text.Trim();
                fur.Remark = tRemark.Text.Trim();
                fur.SecrecyLevel = Convert.ToInt32(DroSecrecyLevel.SelectedValue.Trim()) + 1;
                if (rbtnBoy.Checked == true)
                {
                    fur.Sex = true;
                }
                else
                    fur.Sex = false;

                fur.AgencyID = agency.SelectAgencyID(DropDownListAgency.SelectedText);

                if (Convert.ToInt32(Session["SecrecyLevel"].ToString()) == 5)
                {
                    fur.FutherStudyID = Convert.ToInt32(Session["FurID"]);
                    fur.IsPass = true;
                    FutherStudy.Update(fur);
                    PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("保存成功！"));
                }
                else
                {
                    fur.IsPass = false;
                    FutherStudy.Insert(fur);
                    op.LoginIP = "";
                    op.LoginName = Session["LoginName"].ToString();
                    op.OperationContent = "FutherStudy";
                    op.OperationDataID = Convert.ToInt32(Session["FurID"]);  //
                    op.OperationType = "更新";
                    op.OperationTime = DateTime.Now;
                    op.Remark = FutherStudy.FindIdByNT(tName.Text.Trim().ToString(), dLearnBeginTime.SelectedDate.Value).ToString();
                    blop.Insert(op);
                    FutherStudy.UpdateIsPass(Convert.ToInt32(Session["FurID"]), false);
                    PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHidePostBackReference() + Alert.GetShowInTopReference("数据已经提交，请等待管理员确认！"));
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }

        //重置
        protected void Reset_Click(object sender, EventArgs e)
        {
            tName.Reset();
            Sex.Reset();
            tLearnSchool.Reset();
            tLearnPlace.Reset();
            tLearnContent.Reset();
            tIDNum.Reset();
            tHometown.Reset();
            tEmail.Reset();
            tDocuType.Reset();
            tPhoneNum.Reset();
            tPintroduce.Reset();
            tRemark.Reset();
            dBirthday.Reset();
            dLearnBeginTime.Reset();
            dLearnEndTime.Reset();
            DroSecrecyLevel.Reset();
        }
    }
}