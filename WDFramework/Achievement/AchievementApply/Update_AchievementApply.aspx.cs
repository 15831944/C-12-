using Common.Entities;
using FineUI;
/**编写人：方淑云
 * 时间：2014年8月14号
 * 功能:成果应用更新界面后台
 * 修改履历：
 **/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WDFramework.Acheievement.AchievementApply
{
    public partial class Update_AchievementApply : System.Web.UI.Page
    {
        BLHelper.BLLAchievement ach = new BLHelper.BLLAchievement();
        BLHelper.BLLAchievementApply applys = new BLHelper.BLLAchievementApply();
        BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
        BLHelper.BLLAttachment at = new BLHelper.BLLAttachment();
        BLHelper.BLLOperationLog op = new BLHelper.BLLOperationLog();
        OperationLog log = new OperationLog();
        BLHelper.BLLUser user = new BLHelper.BLLUser();
        AchivementApply apply = new AchivementApply();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dStartTime.MaxDate = DateTime.Now;
                InitSecrecyLevel();
                InitData();
            }
        }
        //初始化界面
        public void InitData()
        {
            try
            {
                Common.Entities.AchivementApply app = applys.FindAll(Convert.ToInt32(Session["AchievementApplyID"]));

                tApplyUnit.Text = app.ApplyUnit;
                if (app.EconomicBenefit != null)
                {
                    tEconomicBenefit.Text = app.EconomicBenefit;
                }
                else
                {
                    tEconomicBenefit.Text = "";
                }
                dEndTime.SelectedDate = app.EndTime;
                dStartTime.SelectedDate = app.StartTime;
                dSecrecyLevel.SelectedIndex = Convert.ToInt32(app.SecrecyLevel - 1);
                app.Use = tUse.Text = app.Use;
                tAchievement.Text = ach.FindAchieveName(Convert.ToInt32(app.AchievementID));
                TextAreaMember.Text = applys.FindName(Convert.ToInt32(app.AchivementApplyID));
                //app.AttachmentID = pm.UpLoad(filePhoto);
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
        //初始化等级下拉框
        public void InitSecrecyLevel()
        {
            for (int i = 0; i < Convert.ToInt32(Session["SecrecyLevel"]); i++)
            {
                string[] SecrecyLevels = new string[] { "四级", "三级", "二级", "一级", "管理员" };
                dSecrecyLevel.Items.Add(SecrecyLevels[i], SecrecyLevels[i]);
            }
        }

        //成果名称验证
        protected void tAchievement_TextChanged(object sender, EventArgs e)
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
        //更新赋值
        public void UpdateValue()
        {
            apply.ApplyUnit = tApplyUnit.Text.Trim();
            apply.EconomicBenefit = tEconomicBenefit.Text.Trim();
            apply.EndTime = dEndTime.SelectedDate;
            apply.StartTime = dStartTime.SelectedDate;
            apply.SecrecyLevel = Convert.ToInt32(dSecrecyLevel.SelectedIndex + 1);
            //apply.EntryPerson = Session["LoginName"].ToString();
            apply.EntryPerson = applys.FindAll(Convert.ToInt32(Session["AchievementApplyID"])).EntryPerson;
            apply.Use = tUse.Text.Trim();
            apply.AchievementID = ach.FindByAchievementName(tAchievement.Text.Trim());
            apply.Member = TextAreaMember.Text;
            //apply.AttachmentID = pm.UpLoad(filePhoto);
        }
        //保存按钮
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
                int AttachmentID = applys.FindAttachmentID(Convert.ToInt32(Session["AchievementApplyID"]));
                string path = at.FindPath(AttachmentID);
                if (Convert.ToInt32(Session["SecrecyLevel"]) == 5)
                {
                    int Attachment = pm.UpLoadFile(fileupload).Attachid;
                    if (Attachment != -3)
                    {
                        UpdateValue();
                        apply.IsPass = true;
                        switch (Attachment)
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
                        apply.AttachmentID = Attachment;
                        apply.AchivementApplyID = Convert.ToInt32(Session["AchievementApplyID"]);
                        applys.Update(apply);
                        pm.DeleteFile(AttachmentID, path);
                        PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("保存成功！"));
                    }
                    else
                    {
                        UpdateValue();
                        if (AttachmentID != 0)
                        {
                            apply.AttachmentID = AttachmentID;
                        }
                        apply.AchivementApplyID = Convert.ToInt32(Session["AchievementApplyID"]);
                        apply.IsPass = true;
                        applys.Update(apply);
                        PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("保存成功！"));
                    }
                }
                else
                {
                    int Attachment = pm.UpLoadFile(fileupload).Attachid;
                    if (Attachment != -3)
                    {
                        UpdateValue();
                        apply.IsPass = false;
                        switch (Attachment)
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
                        apply.AttachmentID = Attachment;
                        applys.Insert(apply);
                        applys.UpdateIsPass(Convert.ToInt32(Session["AchievementApplyID"]), false);
                        log.LoginIP = "";
                        string username = user.FindByLoginName(Session["LoginName"].ToString()).UserName;
                        log.LoginName = username;
                        log.OperationContent = "AchivementApply";
                        log.OperationTime = DateTime.Now;
                        log.OperationType = "更新";
                        log.OperationDataID = Convert.ToInt32(Session["AchievementApplyID"]);
                        log.Remark = apply.AchivementApplyID.ToString();
                        op.Insert(log);
                        PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("您的数据已提交，请等待确认！"));
                    }
                    else
                    {
                        UpdateValue();
                        if (AttachmentID != 0)
                        {
                            apply.AttachmentID = AttachmentID;
                        }
                        apply.IsPass = false;
                        applys.Insert(apply);
                        applys.UpdateIsPass(Convert.ToInt32(Session["AchievementApplyID"]), false);
                        log.LoginIP = "";
                        log.LoginName = Session["LoginName"].ToString();
                        log.OperationContent = "AchivementApply";
                        log.OperationTime = DateTime.Now;
                        log.OperationType = "更新";
                        log.OperationDataID = Convert.ToInt32(Session["AchievementApplyID"]);
                        log.Remark = apply.AchivementApplyID.ToString();
                        op.Insert(log);
                        PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("您的数据已提交，请等待确认！"));
                    }
                }
            }
            catch (Exception ex)
            {
                int attachid = Convert.ToInt32(apply.AttachmentID);
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