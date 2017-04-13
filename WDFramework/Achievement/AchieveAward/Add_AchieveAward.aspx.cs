 /**编写人：方淑云
 * 时间：2014年8月12号
 * 功能:成果获奖添加界面后台
 * 修改履历：
 **/
using FineUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common.Entities;

namespace WebApplication1
{
    public partial class Add_AchieveAward : System.Web.UI.Page
    {
        BLHelper.BLLAchievement ach = new BLHelper.BLLAchievement();
        BLHelper.BLLAchieveAward award = new BLHelper.BLLAchieveAward();
        Common.Entities.AchieveAward ac = new Common.Entities.AchieveAward();
        BLHelper.BLLOperationLog op = new BLHelper.BLLOperationLog();
        OperationLog log = new OperationLog();
        BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
        BLHelper.BLLUser user = new BLHelper.BLLUser();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitSecrecyLevel();
                InitDropListAgency();
               
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
        //成果名称验证
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
        //插入赋值
        public void InsertValue()
        {
            ac.AchievementID = ach.FindByAchievementName(tAchievement.Text.Trim());
            ac.AwardGrade = tAwardGrade.Text.Trim();
            ac.AwardName = tAwardName.Text.Trim();
            ac.AwardType = tAwardType.Text.Trim();
            ac.AwardPeople = tAwardPeople.Text.Trim();
            ac.AwardUnit = DropDownListAgency.SelectedText.Trim();
        
            ac.SecrecyLevel = Convert.ToInt32(dSecrecyLevel.SelectedIndex + 1);

            ac.Member = tMember.Text.Trim();

        }

        //保存
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
                if(tAwardType.Text.Trim() == "")
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
                ac.EntryPerson = username;
                if (Convert.ToInt32(Session["SecrecyLevel"]) == 5)
                {
                    InsertValue();
                    ac.IsPass = true;
                    award.Insert(ac);
                    PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("保存成功！"));
                }
                else
                {
                    InsertValue();
                    ac.IsPass = false;
                    award.Insert(ac);
                    log.LoginName = username;
                    log.OperationTime = DateTime.Now;
                    log.LoginIP = " ";
                    log.OperationContent = "AchieveAward";
                    log.OperationType = "添加";
                    log.OperationDataID = ac.AchieveAwardID;
                    op.Insert(log);
                    PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("您的数据已提交，请等待确认！"));
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