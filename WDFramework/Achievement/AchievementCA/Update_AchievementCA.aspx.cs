using Common.Entities;
using FineUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1.Achievement.AchievementCA
{
    public partial class Update_AchievementCA : System.Web.UI.Page
    {
        BLHelper.BLLAchievement ach = new BLHelper.BLLAchievement();
        BLHelper.BLLOperationLog op = new BLHelper.BLLOperationLog();
        BLHelper.BLLAttachment at = new BLHelper.BLLAttachment();
        BLHelper.BLLAchievementCA ca = new BLHelper.BLLAchievementCA();
        BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
        BLHelper.BLLUser user = new BLHelper.BLLUser();
        Common.Entities.AchievementCA aca = new Common.Entities.AchievementCA();
        OperationLog log = new OperationLog();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dCATime.MaxDate = DateTime.Now;
                InitSecrecyLevel();
                InitData(); 
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
        //项目验证
        protected void tAchievement_TextChanged(object sender, EventArgs e)
        {
            if (tAchievement.Text.Trim() != "")
            {
                if (ach.IsExitAchieveName(tAchievement.Text.Trim()) == null)
                {
                    Alert.Show("该项目名不存在！");
                    tAchievement.Text = "";
                    return;
                }
                else
                {
                    if (ach.IsExitAchieveName(tAchievement.Text.Trim()).IsPass == false)
                    {
                        Alert.Show("该项目名称正在审核中！");
                        tAchievement.Text = "";
                        return;
                    }
                }
            }
        }
        //初始化界面
        public void InitData()
        {
            try
            {
                Common.Entities.AchievementCA ahievementca = ca.FindAll(Convert.ToInt32(Session["AchievementCAID"]));
                tCACommnetLevel.Text = ahievementca.CACommnetLevel;
                tAchievement.Text = ach.FindAchieveName(Convert.ToInt32(ahievementca.AchievementID));
                //ahievementca.AttachmentID = pm.UpLoad(filePhoto);
                dCATime.SelectedDate = ahievementca.CATime;
                tCAUnit.Text = ahievementca.CAUnit;
                tProjectMember.Text = ahievementca.ProjectMember;
                dSecrecyLevel.SelectedIndex = Convert.ToInt32(ahievementca.SecrecyLevel - 1);
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
        //更新赋值
        public void UpdateValue()
        {
            aca.CACommnetLevel = tCACommnetLevel.Text.Trim();
            aca.AchievementID = ach.FindByAchievementName(tAchievement.Text.Trim());
            aca.CATime = dCATime.SelectedDate;
            aca.CAUnit = tCAUnit.Text.Trim();
            aca.ProjectMember = tProjectMember.Text.Trim();
            //aca.EntryPerson = Session["LoginName"].ToString();
            aca.EntryPerson = ca.FindAll(Convert.ToInt32(Session["AchievementCAID"])).EntryPerson;
            aca.SecrecyLevel = Convert.ToInt32(dSecrecyLevel.SelectedIndex + 1);
        }
       
     
            //保存
        protected void Save_Click(object sender, EventArgs e)
        {
            try
            {
                if (tAchievement.Text.Trim() == "")
                {
                    Alert.Show("项目名称不能为空！");
                    return;
                }
                if (tCAUnit.Text.Trim() == "")
                {
                    Alert.Show("验收部门不能为空！");
                    return;
                }
                if (tProjectMember.Text.Trim() == "")
                {
                    Alert.Show("成员不能为空！");
                    return;
                }
                if (tCACommnetLevel.Text.Trim() == "")
                {
                    Alert.Show("验收评语级别不能为空！");
                    return;
                }
                int AttachmentID = ca.FindAttachmentID(Convert.ToInt32(Session["AchievementCAID"]));
                string path = at.FindPath(AttachmentID);
                UpdateValue();
                if (Convert.ToInt32(Session["SecrecyLevel"]) == 5)//如果等于5级
                {
                    aca.IsPass = true;
                    aca.AchievementCAID = Convert.ToInt32(Session["AchievementCAID"]);
                    int Attachment = pm.UpLoadFile(fileupload).Attachid;
                    if (Attachment != -3)//上传控件是否有值
                    {
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
                        aca.AttachmentID = Attachment;//附件为新插入的附件ID
                        ca.Update(aca);//更新
                        pm.DeleteFile(AttachmentID, path);
                        PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("保存成功！"));
                    }
                    else //上传空间没有值
                    {
                        if (AttachmentID != 0)//原来有附件
                        {
                            aca.AttachmentID = AttachmentID;
                        }
                        ca.Update(aca); ;//更新项目表
                        PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("保存成功！"));
                    }
                }
                else//小于5级
                {
                    string username = user.FindByLoginName(Session["LoginName"].ToString()).UserName;
                    log.LoginName = username;
                    log.OperationTime = DateTime.Now;
                    log.LoginIP = " ";
                    log.OperationContent = "AchievementCA";
                    log.OperationType = "更新";
                    aca.IsPass = false;
                    int Attachment = pm.UpLoadFile(fileupload).Attachid;
                    if (Attachment != -3)//有值
                    {
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
                        aca.AttachmentID = Attachment;//附件为新插入的附件ID
                        ca.Insert(aca);//插入
                        log.OperationDataID = Convert.ToInt32(Session["AchievementCAID"]);
                        log.Remark = aca.AchievementCAID.ToString();
                        op.Insert(log);//将项目更新插入操作表                    
                        ca.UpdateIsPass(Convert.ToInt32(Session["AchievementCAID"]), false);
                        PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("你的数据已提交，请等待确认！"));

                    }
                    else//上传控件没有值
                    {
                        if (AttachmentID != 0)//原来有附件
                        {
                            aca.AttachmentID = AttachmentID;
                        }
                        ca.Insert(aca);
                        log.OperationDataID = Convert.ToInt32(Session["AchievementCAID"]);
                        log.Remark = aca.AchievementCAID.ToString();
                        op.Insert(log);
                        ca.UpdateIsPass(Convert.ToInt32(Session["AchievementCAID"]), false);
                        PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("你的数据已提交，请等待确认！"));

                    }
                }
            }
            catch (Exception ex)
            {
                int attachid = Convert.ToInt32(aca.AttachmentID);
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
                tCACommnetLevel.Reset();
                tAchievement.Reset();
                dCATime.Reset();
                tCAUnit.Reset();
                tProjectMember.Reset();
                dSecrecyLevel.Reset();
                PageContext.RegisterStartupScript("clearFile();");
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
    }
}