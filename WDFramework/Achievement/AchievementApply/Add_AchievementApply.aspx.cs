/**编写人：方淑云
 * 时间：2014年8月14号
 * 功能:成果应用添加界面后台
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

namespace WDFramework.Acheievement.AchievementApply
{
    public partial class Add_AchievementApply : System.Web.UI.Page
    {
        BLHelper.BLLAchievementApply apply = new BLHelper.BLLAchievementApply();
        BLHelper.BLLAchievement ach = new BLHelper.BLLAchievement();
        BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
        BLHelper.BLLUser user = new BLHelper.BLLUser();
        AchivementApply app = new AchivementApply();
        BLHelper.BLLOperationLog op = new BLHelper.BLLOperationLog();
        BLHelper.BLLAttachment at = new BLHelper.BLLAttachment();
        OperationLog log = new OperationLog();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dStartTime.MaxDate = DateTime.Now;
                InitSecrecyLevel();
            }
        }
        //初始化等级下拉框
        public void InitSecrecyLevel()
        {
            for (int i = 0; i < Convert.ToInt32(Session["SecrecyLevel"]); i++)
            {
                string[] SecrecyLevels = new string[] { "四级", "三级", "二级", "一级", "管理员" };
                dSecrecyLevel.Items.Add(SecrecyLevels[i], i.ToString());
            }
        }

        //成果名称验证
        protected void tAchievement_TextChanged1(object sender, EventArgs e)
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
        //保存赋值
        public void InsertValue()
        {
            app.ApplyUnit = tApplyUnit.Text.Trim();
            app.EconomicBenefit = tEconomicBenefit.Text.Trim();
            app.EndTime = dEndTime.SelectedDate;
            app.StartTime = dStartTime.SelectedDate;
            app.SecrecyLevel = Convert.ToInt32(dSecrecyLevel.SelectedIndex +1);       
            app.Use = tUse.Text.Trim();
            app.AchievementID = ach.FindByAchievementName(tAchievement.Text.Trim());
            app.Member = TextAreaMember.Text.Trim();
        }
        //保存
        protected void Save_Click(object sender, EventArgs e)
        {
            try
            {
                if (tAchievement.Text.Trim() == "")
                {
                    Alert.Show("成果名称不能为空!");
                    return;
                }
                if (tApplyUnit.Text.Trim() == "")
                {
                    Alert.Show("应用单位不能为空!");
                    return;
                }            
                if (tUse.Text.Trim() == "")
                {
                    Alert.Show("用途不能为空!");
                    return;
                }
                if (dEndTime.SelectedDate < dStartTime.SelectedDate)
                {
                    Alert.ShowInTop("结束时间不能小于开始时间！");
                    return;
                }
                string username = user.FindByLoginName(Session["LoginName"].ToString()).UserName;
                app.EntryPerson = username;
                int attachid = pm.UpLoadFile(fileupload).Attachid;           
                if (Convert.ToInt32(Session["SecrecyLevel"]) == 5)
                {
                    InsertValue();
                    
                    switch (attachid)
                    {
                        case -1:
                            Alert.ShowInTop("文件类型不符，请重新选择！");
                            return;
                        case 0:
                            Alert.ShowInTop("文件名已经存在！");
                            return; 
                        case -2:
                            Alert.ShowInTop("文件不能大于150M");
                            return;
                    }
                    if (attachid != -3)
                    {
                        app.AttachmentID = attachid;
                    }
                    else
                    {
                        app.AttachmentID = null;
                    }
                    app.IsPass = true;
                    apply.Insert(app);
                    PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("保存成功！"));
                }
                else
                {
                    InsertValue();               
                    switch (attachid)
                    {
                        case -1:
                            Alert.ShowInTop("文件类型不符，请重新选择！");
                            return;
                        case 0:
                            Alert.ShowInTop("文件名已经存在！");
                            return;
                        case -2:
                            Alert.ShowInTop("文件不能大于150M");
                            return;
                    }
                    if (attachid != -3)
                    {
                        app.AttachmentID = attachid;
                    }
                    else
                    {
                        app.AttachmentID = null;
                    }
                    app.IsPass = false;
                    apply.Insert(app);
                    log.LoginName = username;
                    log.OperationTime = DateTime.Now;
                    log.LoginIP = " ";
                    log.OperationContent = "AchivementApply";
                    log.OperationType = "添加";
                    log.OperationDataID = app.AchivementApplyID;
                    op.Insert(log);
                    PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("您的数据已提交，请等待确认！"));
                }
            }
            catch (Exception ex)
            {
                int attachid = Convert.ToInt32(app.AttachmentID);
                string path = at.FindPath(attachid);
                pm.DeleteFile(attachid, path);
                pm.SaveError(ex, this.Request);

            }
        }
        //重置
        protected void DeleteAll_Click(object sender, EventArgs e)
        {
            try
            {
                tApplyUnit.Reset();
                tEconomicBenefit.Reset();
                dEndTime.Reset();
                dStartTime.Reset();
                dSecrecyLevel.Reset();
                tUse.Reset();
                tAchievement.Reset();
                TextAreaMember.Reset();
                PageContext.RegisterStartupScript("clearFile();");
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);

            }
        }

      
    }
}