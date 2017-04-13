/**编写人：张凡凡
 * 时间：2014年8月1号
 * 功能:进修学习（接受）添加界面后台
 * 修改履历：
 **/
using Common.Entities;
using FineUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class 新增进修学习_接受_ : System.Web.UI.Page
    {
        BLHelper.BLLFutherStudy futherstudy = new BLHelper.BLLFutherStudy();

        FutherStudy fu = new FutherStudy();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                InitDropList();
        }
        //保存
        protected void Save_Click(object sender, EventArgs e)
        {
            BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
            BLHelper.BLLUser user = new BLHelper.BLLUser();
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
                OperationLog op = new OperationLog();
                BLHelper.BLLOperationLog blop = new BLHelper.BLLOperationLog();

                fu.Name = tName.Text.Trim();
                if (dBirthday.SelectedDate != null)
                    fu.Birthday = Convert.ToDateTime(dBirthday.Text.Trim());
                fu.DocuType = tDocuType.SelectedText.Trim();
                fu.IDNum = tIDNum.Text.Trim();
                fu.Email = tEmail.Text.Trim();
                fu.Hometown = tHometown.Text.Trim();
                fu.LearnBeginTime = dLearnBeginTime.SelectedDate;
                fu.LearnContent = tLearnContent.Text.Trim();
                if (dLearnEndTime.SelectedDate != null)
                    fu.LearnEndTime = dLearnEndTime.SelectedDate;
                fu.LearnPlace = tLearnPlace.Text.Trim();
                fu.LearnSchool = tLearnSchool.Text.Trim();
                fu.PhoneNum = tPhoneNum.Text.Trim();
                fu.Profile = tPintroduce.Text.Trim();
                fu.Remark = tRemark.Text.Trim();

                fu.SecrecyLevel = Convert.ToInt32(DroSecrecyLevel.SelectedValue.Trim()) + 1;

                if (rbtnBoy.Checked == true)
                {
                    fu.Sex = true;
                }
                else
                    fu.Sex = false;

                fu.AgencyID = agency.SelectAgencyID(DropDownListAgency.SelectedText);
                string username = user.FindByLoginName(Session["LoginName"].ToString()).UserName;
                fu.EntryPerson = username;

                if (Convert.ToInt32(Session["SecrecyLevel"].ToString()) == 5)
                {
                    fu.IsPass = true;
                    futherstudy.Insert(fu);
                    PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("保存成功！"));
                }
                else
                {
                    fu.IsPass = false;
                    futherstudy.Insert(fu);
                    op.LoginIP = "";
                    op.LoginName = Session["LoginName"].ToString();
                    op.OperationContent = "FutherStudy";
                    op.OperationDataID = futherstudy.FindIdByNT(tName.Text.Trim().ToString(), dLearnBeginTime.SelectedDate.Value);
                    op.OperationType = "添加";
                    op.OperationTime = DateTime.Now;
                    blop.Insert(op);
                    PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHidePostBackReference() + Alert.GetShowInTopReference("数据已经提交，请等待管理员确认！"));
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
        //初始化下拉框
        public void InitDropList()
        {
            BLHelper.BLLAgency agency = new BLHelper.BLLAgency();
            List<Agency> list = agency.FindAllAgencyName();
            for (int i = 0; i < list.Count(); i++)
            {
                DropDownListAgency.Items.Add(list[i].AgencyName.ToString(), (i + 1).ToString());
            }

            string[] SecrecyLevels = new string[] { "四级", "三级", "二级", "一级", "管理员" };
            for (int i = 0; i < Convert.ToInt32(Session["SecrecyLevel"]); i++)
                DroSecrecyLevel.Items.Add(SecrecyLevels[i], i.ToString());
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