/**编写人：方淑云
 * 时间：2014年8月24号
 * 功能:专著更新界面
 * 修改履历：    1.修改人：吕博扬
 *              修改时间：2015年9月23日
 *              修改内容：取消所有输入项的数据校验
 *              2.修改人：陈起明
 *              修改时间：2015年11月28日
 *              修改内容：1.字段名：“专著名称”改为“著作名称”
 *                       2.取消成果名称验证
 *                       3.编辑对话框中，下拉框“著作类型”新增选项：译著、教材
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
    public partial class Update_Monograph : System.Web.UI.Page
    {
        BLHelper.BLLMonograph mo = new BLHelper.BLLMonograph();
        Monograph monh = new Monograph();
        BLHelper.BLLUser user = new BLHelper.BLLUser();
        BLHelper.BLLAttachment at = new BLHelper.BLLAttachment();
        BLHelper.BLLAchievement ach = new BLHelper.BLLAchievement();
        BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
        BLHelper.BLLOperationLog op = new BLHelper.BLLOperationLog();
        OperationLog log = new OperationLog();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dPUblicationTime.MaxDate = DateTime.Now;
              
                Initddl();
                InitData();
            }
        }
        //初始化等级下拉框
        public void Initddl()
        {
            for (int i = 0; i < Convert.ToInt32(Session["SecrecyLevel"]); i++)
            {
                string[] SecrecyLevels = new string[] { "四级", "三级", "二级", "一级", "管理员" };
                dSecrecyLevel.Items.Add(SecrecyLevels[i], i.ToString());
            }
            BLHelper.BLLBasicCode bs = new BLHelper.BLLBasicCode();
            List<Common.Entities.BasicCode> bascode = bs.FindByCategoryName("著作类型");
            for (int i = 0; i < bascode.Count; i++)
                ddlMonographType.Items.Add(bascode[i].CategoryContent.Trim(), bascode[i].CategoryContent.Trim());
        }
     
      
        //成果名称验证
        /*protected void tAchievement_TextChanged(object sender, EventArgs e)
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
        }*/
    
        //著作名称验证
        protected void tMonographName_TextChanged(object sender, EventArgs e)
        {
            if (tMonographName.Text.Trim() != "")
            {
                if (mo.IsEixtName(tMonographName.Text.Trim()) != null)
                {
                    if (mo.IsEixtName(tMonographName.Text.Trim()).IsPass == false)
                    {
                        Alert.Show("该著作名称正在审核中！");
                        return;
                    }
                    else
                    {
                        Alert.Show("该著作名称已存在！");
                        return;
                    }
                }
            }
        }
        //初始化
        public void InitData()
        {
            try
            {
                Common.Entities.Monograph mon = mo.FindAll(Convert.ToInt32(Session["MonographID"]));
                /*if (mon.AchievementID != null)
                {
                    string achievement = ach.FindAchieveName(Convert.ToInt32(mon.AchievementID));
                    tAchievement.Text = achievement;
                }
                else
                {
                    tAchievement.Text = "";
                }*/
                tBookNuber.Text = mon.BookNuber;
                tMonographName.Text = mon.MonographName;
                dSecrecyLevel.SelectedIndex = Convert.ToInt32(mon.SecrecyLevel - 1);
                //tSort.Text = mon.Sort;
                tISBN.Text = mon.ISBNNum;
                tCIP.Text = mon.CIPNum;
                TFirstWriter.Text = mon.FirstWriter;
                ddlMonographType.SelectedValue = mon.MonographType;
                dPUblicationTime.SelectedDate = mon.PUblicationTime;
                tPublisher.Text = mon.Publisher;
                tRevision.Text = mon.Revision;
                MoPeople.Text = mon.MonographPeople;
                if (mon.IssueRegin != null)
                {
                    tIssueRegin.Text = mon.IssueRegin;
                }
                else
                {
                    tIssueRegin.Text = "";
                }
                if (mon.Remark != null)
                {
                    tRemark.Text = mon.Remark;
                }
                else
                {
                    tRemark.Text = "";
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
        
        //保存
        protected void Save_Click(object sender, EventArgs e)
        {
            try
            {
                //if (tMonographName.Text.Trim() == "")
                //{
                //    Alert.Show("著作名称不能为空！");
                //    return;
                //}
                //if (MoPeople.Text.Trim() == "")
                //{
                //    Alert.Show("著作人不能为空！");
                //    return;
                //}
                //if (TFirstWriter.Text.Trim() == null)
                //{
                //    Alert.Show("第一作者不能为空！");
                //    return;
                //}
                /*int AchievementID = ach.FindByAchievementName(tAchievement.Text.Trim());
                if (AchievementID != 0)
                {
                    monh.AchievementID = AchievementID;
                }
                else
                {
                    monh.AchievementID = null;
                }*/
                monh.BookNuber = tBookNuber.Text.Trim();
                // monh.EntryPerson = Session["LoginName"].ToString();
                monh.EntryPerson = mo.FindAll(Convert.ToInt32(Session["MonographID"])).EntryPerson;
                monh.MonographName = tMonographName.Text.Trim();
                monh.SecrecyLevel = Convert.ToInt32(dSecrecyLevel.SelectedIndex + 1);
                //monh.Sort = tSort.Text.Trim();
                monh.PUblicationTime = dPUblicationTime.SelectedDate;
                monh.Publisher = tPublisher.Text.Trim();
                monh.Revision = tRevision.Text.Trim();
                monh.MonographPeople = MoPeople.Text.Trim();
                //monh.MDepartment = DropDownListAgency.SelectedText.Trim();
                monh.CIPNum = tCIP.Text.Trim();
                monh.ISBNNum = tISBN.Text.Trim();
                monh.FirstWriter = TFirstWriter.Text.Trim();
                monh.MonographType = ddlMonographType.SelectedValue.Trim();
                monh.IssueRegin = tIssueRegin.Text.Trim();
                monh.Remark = tRemark.Text.Trim();
                int BAttachmentID = Convert.ToInt32(mo.IsExitAttacment(Convert.ToInt32(Session["MonographID"])).BAttachmentID);
                int FAttachmentID = Convert.ToInt32(mo.IsExitAttacment(Convert.ToInt32(Session["MonographID"])).FAttachmentID);
                string Bpath = at.FindPath(BAttachmentID);
                string Fpath = at.FindPath(FAttachmentID);
                int Battachid = pm.UpLoadFile(fileuploadB).Attachid;
                string nbpath = at.FindPath(Battachid);
                int Fattachid = pm.UpLoadFile(fileuploadF).Attachid;
                switch (Battachid)
                {
                    case -1:
                        Alert.ShowInTop("版权页文件类型不符，请重新选择！");
                        return;
                    case 0:
                        Alert.ShowInTop("版权页文件名已经存在！");
                        return;
                    case -2:
                        Alert.ShowInTop("版权页文件不能大于150M");
                        return;
                }
                switch (Fattachid)
                {
                    case -1:
                        Alert.ShowInTop("封面文件类型不符，请重新选择！");
                        pm.DeleteFile(Battachid, nbpath);
                        return;
                    case 0:
                        Alert.ShowInTop("封面文件名已经存在！");
                        pm.DeleteFile(Battachid, nbpath);
                        return;
                    case -2:
                        Alert.ShowInTop("封面文件不能大于150M");
                        pm.DeleteFile(Battachid, nbpath);
                        return;
                }
                if (Convert.ToInt32(Session["SecrecyLevel"]) == 5)
                {
                    monh.IsPass = true;
                    monh.MonographID = Convert.ToInt32(Session["MonographID"]);
                    if (Battachid != -3)
                    {
                        monh.BAttachmentID = Battachid;
                        if (BAttachmentID != 0)
                        {
                            pm.DeleteFile(BAttachmentID, Bpath);
                        }
                    }
                    else
                    {
                        if (BAttachmentID != 0)
                            monh.BAttachmentID = BAttachmentID;
                    }
                    if (Fattachid != -3)
                    {
                        monh.FAttachmentID = Fattachid;
                        if (FAttachmentID != 0)
                        {
                            pm.DeleteFile(FAttachmentID, Fpath);
                        }
                    }
                    else
                    {
                        if (FAttachmentID != 0)
                            monh.FAttachmentID = FAttachmentID;
                    }
                    mo.Update(monh);
                    PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("保存成功"));

                }
                else
                {
                    monh.IsPass = false;
                    if (Battachid != -3)
                    {
                        monh.BAttachmentID = Battachid;
                    }
                    else
                    {
                        if (BAttachmentID != 0)
                            monh.BAttachmentID = BAttachmentID;
                    }
                    if (Fattachid != -3)
                    {
                        monh.FAttachmentID = Fattachid;
                    }
                    else
                    {
                        if (FAttachmentID != 0)
                            monh.FAttachmentID = FAttachmentID;
                    }
                    mo.Insert(monh);
                    mo.UpdateIsPass(Convert.ToInt32(Session["MonographID"]), false);
                    log.LoginIP = "";
                    string username = user.FindByLoginName(Session["LoginName"].ToString()).UserName;
                    log.LoginName = username;
                    log.OperationContent = "Monograph";
                    log.OperationTime = DateTime.Now;
                    log.OperationType = "更新";
                    log.OperationDataID = Convert.ToInt32(Session["MonographID"]);
                    log.Remark = monh.MonographID.ToString();
                    op.Insert(log);
                    PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("您的数据已提交，请等待确认！"));

                }
            }
            catch (Exception ex)
            {
                int attachidb = Convert.ToInt32(monh.BAttachmentID);
                int attachidf = Convert.ToInt32(monh.FAttachmentID);
                string pathb = at.FindPath(attachidb);
                string pathf = at.FindPath(attachidf);
                pm.DeleteFile(attachidb, pathb);
                pm.DeleteFile(attachidf, pathf);
                pm.SaveError(ex, this.Request);
            }
        }
        //重置
        protected void DeleteAll_Click(object sender, EventArgs e)
        {
            try
            {
                MoPeople.Reset();
                //tAchievement.Reset();
                tBookNuber.Reset();
                tMonographName.Reset();
                dSecrecyLevel.Reset();
                //tSort.Reset();
                ddlMonographType.Reset();
                tISBN.Reset();
                tCIP.Reset();
                TFirstWriter.Reset();
                dPUblicationTime.Reset();
                tPublisher.Reset();
                tRevision.Reset();
                tIssueRegin.Reset();
                tRemark.Reset();
                PageContext.RegisterStartupScript("clearFileF();");
                PageContext.RegisterStartupScript("clearFileB();");
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }

    }
}