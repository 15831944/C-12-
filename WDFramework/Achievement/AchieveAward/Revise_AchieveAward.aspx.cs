/**编写人：方淑云
 * 时间：2014年8月14号
 * 功能:成果报奖更新界面后台
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
    public partial class Revise_AchieveAward : System.Web.UI.Page
    {
         BLHelper.BLLAchievement ach = new BLHelper.BLLAchievement();
        BLHelper.BLLAchieveAward award = new BLHelper.BLLAchieveAward();
        Common.Entities.AchieveAward achieve = new Common.Entities.AchieveAward();
        BLHelper.BLLOperationLog op = new BLHelper.BLLOperationLog();
        OperationLog log = new OperationLog();
        BLHelper.BLLUser user = new BLHelper.BLLUser();
        BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
                InitDropListAgency();
                InitSecrecyLevel();
                InitData();
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
                    dSecrecyLevel.Items.Add(SecrecyLevels[i], i.ToString());
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
             
            }
        }
        //初始化机构下拉框
        public void InitDropListAgency()
        {
            try
            {
                BLHelper.BLLAgency agency = new BLHelper.BLLAgency();
                List<Common.Entities.Agency> list = agency.FindAllAgencyName();
                for (int i = 0; i < list.Count(); i++)
                {
                    DropDownListAgency.Items.Add(list[i].AgencyName.ToString(), list[i].AgencyName.ToString());
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
               
            }
        }
        //初始化界面
        public void InitData()
        {
            try
            {
                Common.Entities.AchieveAward ac = award.FindAll(Convert.ToInt32(Session["AchieveAwardID"]));
                tAchievement.Text = ach.FindAchieveName(Convert.ToInt32(ac.AchievementID));
                tAwardGrade.Text = ac.AwardGrade;
                tAwardName.Text = ac.AwardName;
                tAwardPeople.Text = ac.AwardPeople;
                tAwardType.Text = ac.AwardType;
                DropDownListAgency.SelectedValue = ac.AwardUnit;
                dSecrecyLevel.SelectedIndex = Convert.ToInt32(ac.SecrecyLevel - 1);

                tAwardMember.Text = ac.Member;
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
               
            }
        }
        //成果名称的验证
        protected void tAchievement_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (tAchievement.Text.Trim() != "")
                {
                    if (ach.IsExitAchieveName(tAchievement.Text.Trim()) == null)
                    {
                        Alert.Show("该成果名不存在！");
                        tAchievement.Text = "";
                        return;
                    }
                    else
                    {
                        if (ach.IsExitAchieveName(tAchievement.Text.Trim()).IsPass == false)
                        {
                            Alert.Show("该成果名称正在审核中！");
                            tAchievement.Text = "";
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            
            }
        }
        //更新赋值
        public void UpDateValue()
        {
            achieve.AchievementID = ach.FindByAchievementName(tAchievement.Text.Trim());
            achieve.AwardGrade = tAwardGrade.Text.Trim();
            achieve.AwardName = tAwardName.Text.Trim();
            achieve.AwardType = tAwardType.Text.Trim();
            achieve.AwardPeople = tAwardPeople.Text.Trim();
            achieve.AwardUnit = DropDownListAgency.SelectedText;
            achieve.EntryPerson = award.FindAll(Convert.ToInt32(Session["AchieveAwardID"])).EntryPerson;
            achieve.SecrecyLevel = Convert.ToInt32(dSecrecyLevel.SelectedIndex + 1);

            achieve.Member = tAwardMember.Text.Trim();

        }
        //保存更改
        protected void Save_Click(object sender, EventArgs e)
        {
            try
            {
                if (tAchievement.Text.Trim() == "")
                {
                    Alert.Show("成果名称不能为空！");
                    return;
                }
                if (tAwardName.Text.Trim() == "")
                {
                    Alert.Show("报奖名称不能为空！");
                    return;
                }
                if (tAwardGrade.Text.Trim() == " ")
                {
                    Alert.Show("报奖等级不能为空！");
                    return;
                }
                if (tAwardType.Text.Trim() == "")
                {
                    Alert.Show("报奖类型不能为空！");
                    return;
                }
                if (AwardPeople.Text.Trim() == "")
                {
                    Alert.Show("报奖人不能为空！");
                    return;
                }
                string username = user.FindByLoginName(Session["LoginName"].ToString()).UserName;
                if (Convert.ToInt32(Session["SecrecyLevel"]) == 5)
                {
                    UpDateValue();
                    achieve.AchieveAwardID = Convert.ToInt32(Session["AchieveAwardID"]);
                    achieve.IsPass = true;
                    award.Update(achieve);
                    PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("保存成功！"));
                }
                else
                {
                    UpDateValue();
                    achieve.IsPass = false;
                    award.Insert(achieve);
                    award.UpdateIsPass(Convert.ToInt32(Session["AchieveAwardID"]), false);
                    log.LoginName = username;
                    log.OperationTime = DateTime.Now;
                    log.LoginIP = " ";
                    log.OperationContent = "AchieveAward";
                    log.OperationType = "更新";
                    log.OperationDataID = Convert.ToInt32(Session["AchieveAwardID"]);
                    log.Remark = achieve.AchieveAwardID.ToString();
                    op.Insert(log);
                    PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("您的数据已提交，请等待确认!"));
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
               
            }
        }
        //重置
        protected void DeleteAll_Click(object sender, EventArgs e)
        {
            try
            {
                tAwardPeople.Reset();
                tAchievement.Reset();
                tAwardGrade.Reset();
                tAwardName.Reset();
                tAwardType.Reset();
                DropDownListAgency.Reset();
                dSecrecyLevel.Reset();
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
              
            }
        }
    }
}