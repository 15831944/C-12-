/**编写人：方淑云
 * 时间：2014年8月24号
 * 功能:专著添加界面
 * 修改履历：1.时间：2015年11月28日
 *          修改人：陈起明
 *          修改内容：1.隐藏字段：类别，所属成果名称
 *                   2.字段名“专著名称”改为“著作名称”
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
    public partial class Add_Monograph : System.Web.UI.Page
    {
        BLHelper.BLLMonograph mo = new BLHelper.BLLMonograph();
        Monograph mon = new Monograph();
        BLHelper.BLLAchievement ach = new BLHelper.BLLAchievement();
        BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
        BLHelper.BLLOperationLog op = new BLHelper.BLLOperationLog();
        OperationLog log = new OperationLog();
        BLHelper.BLLAttachment at = new BLHelper.BLLAttachment();
        BLHelper.BLLUser user = new BLHelper.BLLUser();
        BLHelper.BLLBasicCode ba = new BLHelper.BLLBasicCode();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dPUblicationTime.MaxDate = DateTime.Now;
                InitdFirstWriterSite();
                Initddl();
                InitDropListAgency();
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
        //初始化第一作者身份
        public void InitdFirstWriterSite()
        {
            try
            {
                List<BasicCode> listname = ba.FindByCategoryName("第一作者身份");
                for (int i = 0; i < listname.Count(); i++)
                {
                    dPaperIdentity.Items.Add(listname[i].CategoryContent.ToString(), listname[i].CategoryContent.ToString());
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
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
      
        //保存赋值
        public void InsertValue()
        {
           /*if(tAchievement.Text !="")
           { 
                int AchievementID = ach.FindByAchievementName(tAchievement.Text.Trim());
                mon.AchievementID = AchievementID;
            }
            else
            {
                mon.AchievementID = null;
            }*/
            mon.BookNuber = tBookNuber.Text.Trim();
          
            mon.MonographName = tMonographName.Text.Trim();
            mon.SecrecyLevel =Convert.ToInt32 (dSecrecyLevel.SelectedIndex + 1);
            //mon.Sort = tSort.Text.Trim();
            mon.PUblicationTime = dPUblicationTime.SelectedDate;
            mon.Publisher = tPublisher.Text.Trim();
            mon.Revision = tRevision.Text.Trim();
            mon.PaperUnit = DropDownListAgency.SelectedText; //所属机构
            mon.MonographPeople = MoPeople.Text.Trim();
            mon.FirstWriter = TFirstWriter.Text.Trim();
            mon.WriterIdentity = dPaperIdentity.SelectedValue;
            mon.MonographType = ddlMonographType.SelectedValue.Trim();
            if (tCIP.Text.Trim() == "")
                mon.CIPNum = null;
            else
                mon.CIPNum = tCIP.Text.Trim();
            if (tISBN.Text.Trim() == "")
                mon.ISBNNum = null;
            else
                mon.ISBNNum = tISBN.Text.Trim();
            mon.IssueRegin = tIssueRegin.Text.Trim();
            if (tRemark.Text.Trim() != "")
            {
                mon.Remark = tRemark.Text.Trim();
            }
            else
            {
                mon.Remark = null;
            }
        }
      
        //保存
        protected void Save_Click(object sender, EventArgs e)
        {
            try
            {
                if (tMonographName.Text.Trim() == "")
                {
                    Alert.Show("著作名称不能为空！");
                    return;
                }
                if (MoPeople.Text.Trim() == "")
                {
                    Alert.Show("著作人不能为空！");
                    return;
                }

                if (TFirstWriter.Text.Trim() == null)
                {
                    Alert.Show("第一作者不能为空！");
                    return;
                }
                string username = user.FindByLoginName(Session["LoginName"].ToString()).UserName;
                mon.EntryPerson = username;
                if (Convert.ToInt32(Session["SecrecyLevel"]) == 5)
                {
                    InsertValue();
                    mon.IsPass = true;
                    int Battachid = pm.UpLoadFile(fileuploadB).Attachid;
                    string path = at.FindPath(Battachid);
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
                    if (Battachid != -3)
                    {
                        mon.BAttachmentID = Battachid;
                    }
                    else
                    {
                        mon.BAttachmentID = null;
                    }
                    int Fattachid = pm.UpLoadFile(fileuploadF).Attachid;
                    switch (Fattachid)
                    { 
                        case -1:
                            Alert.ShowInTop("封面文件类型不符，请重新选择！");
                            pm.DeleteFile(Battachid, path);
                            return;
                        case 0:
                            Alert.ShowInTop("封面文件名已经存在！");
                            pm.DeleteFile(Battachid, path);
                            return;
                        case -2:
                            Alert.ShowInTop("封面文件不能大于150M");
                            pm.DeleteFile(Battachid, path);
                            return;
                    }
                    if (Fattachid != -3)
                    {
                        mon.FAttachmentID = Fattachid;
                    }
                    else
                    {
                        mon.FAttachmentID = null;
                    }
                    mo.Insert(mon);
                    PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("保存成功"));
                }
                else
                {
                    InsertValue();
                    mon.IsPass = false;
                    int Battachid = pm.UpLoadFile(fileuploadB).Attachid;
                    string path = at.FindPath(Battachid);
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
                    if (Battachid != -3)
                    {
                        mon.BAttachmentID = Battachid;
                    }
                    else
                    {
                        mon.BAttachmentID = null;
                    }
                    int Fattachid = pm.UpLoadFile(fileuploadF).Attachid;
                    switch (Fattachid)
                    {
                        case -1:
                            Alert.ShowInTop("封面文件类型不符，请重新选择！");
                            pm.DeleteFile(Battachid, path);
                            return;
                        case 0:
                            Alert.ShowInTop("封面文件名已经存在！");
                            pm.DeleteFile(Battachid, path);
                            return;
                        case -2:
                            Alert.ShowInTop("封面文件不能大于150M");
                            pm.DeleteFile(Battachid, path);
                            return;
                    }
                    if (Fattachid != -3)
                    {
                        mon.FAttachmentID = Fattachid;
                    }
                    else
                    {
                        mon.FAttachmentID = null;
                    }
                    mo.Insert(mon);
                    log.LoginName = username;
                    log.OperationTime = DateTime.Now;
                    log.LoginIP = " ";
                    log.OperationContent = "Monograph";
                    log.OperationType = "添加";
                    log.OperationDataID = mon.MonographID;
                    op.Insert(log);
                    PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("您的数据已提交，请等待确认！"));
                }
            }
            catch (Exception ex)
            {
                int attachidb = Convert.ToInt32(mon.BAttachmentID);
                int attachidf = Convert.ToInt32(mon.FAttachmentID);
                string pathb = at.FindPath(attachidb);
                string pathf = at.FindPath(attachidf);
                pm.DeleteFile(attachidb, pathb);
                pm.DeleteFile(attachidf, pathf);
                pm.SaveError(ex, this.Request);
            }
        }
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
                dPUblicationTime.Reset();
                tPublisher.Reset();
                tRevision.Reset();
                tIssueRegin.Reset();
                ddlMonographType.Reset();
                tISBN.Reset();
                tCIP.Reset();
                TFirstWriter.Reset();
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