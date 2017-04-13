/**编写人：方淑云
 * 时间：2014年8月24号
 * 功能:成果鉴定更新界面
 * 修改履历：
 **/
using Common.Entities;
using FineUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1.Achievement.Achievement
{
    public partial class Update_Achievement : System.Web.UI.Page
    {
        BLHelper.BLLAchievement achieve = new BLHelper.BLLAchievement();
        Common.Entities.Achievement achs = new Common.Entities.Achievement();
        BLHelper.BLLOperationLog op = new BLHelper.BLLOperationLog();
        BLHelper.BLLUser user = new BLHelper.BLLUser();
        BLHelper.BLLProject project = new BLHelper.BLLProject();
        BLHelper.BLLAttachment at = new BLHelper.BLLAttachment();
        BLHelper.BLLAgency agency = new BLHelper.BLLAgency();
        BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
        OperationLog log = new OperationLog();
        BLHelper.BLLBasicCode bllBasicCode = new BLHelper.BLLBasicCode();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dAppraisalTime.MaxDate = DateTime.Now;            
                InitSecrecyLevel();
                InitDropListAgency();
                //初始化鉴定级别下拉框
                InitDropDownListProjectRank();
                //初始化鉴定形式下拉框
                InitDropDownListProjectForm();
                //初始化鉴定水平下拉框
                InitDropDownListProjectLevel();
                InitData();
            }
        }
        //初始化机构下拉框
        public void InitDropListAgency()
        {
            BLHelper.BLLAgency agency = new BLHelper.BLLAgency();
            List<Common.Entities.Agency> list = agency.FindAllAgencyName();
            for (int i = 0; i < list.Count(); i++)
            {
                DropDownListAgency.Items.Add(list[i].AgencyName.ToString(), list[i].AgencyName.ToString());
            }
        }
        //初始化
        public void InitData()
        {
            try
            {
                Common.Entities.Achievement ach = achieve.FindAll(Convert.ToInt32(Session["AchievementID"]));
                tAchievementName.Text = ach.AchievementName;
                DropDownListAgency.SelectedValue = agency.FindAgenName(ach.AgencyID);
                if (ach.ProjectName != null)
                {
                    tProjectID.Text = (ach.ProjectName);
                }
                else
                {
                    tProjectID.Text = "";
                }
                dAppraisalTime.SelectedDate = ach.AppraisalTime;
                tAppraisalUnit.Text = ach.AppraisalUnit;
                tApRemarkRank.Text = ach.ApRemarkRank;
                tApprovalNum.Text = ach.ApprovalNum;
                dSecrecyLevel.SelectedIndex = Convert.ToInt32(ach.SecrecyLevel - 1);
                ProjectInNum.Text = ach.ProjectInNum;//项目分类编号
                ProjectPeople.Text = ach.ProjectPeople;
                FirstFinishedPeople.Text = ach.FirstFinishedPeople;
                DropDownListProjectForm.SelectedValue = ach.ProjectForm;//鉴定形式
                DropDownListProjectLevel.SelectedValue = ach.ProjectLevel;//鉴定水平
                //DropDownListProjectRank.SelectedValue = ach.ProjectRank;//鉴定级别
                //ach.EntryPerson = Session["LoginName"].ToString();
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
        //成果验证
        protected void tAchievementName_TextChanged(object sender, EventArgs e)
        {
            if (tAchievementName.Text.Trim() != "")
            {
                if (achieve.IsExitAchieveName(tAchievementName.Text.Trim()) != null)
                {
                    if (achieve.IsExitAchieveName(tAchievementName.Text.Trim()).IsPass == false)
                    {
                        Alert.Show("该成果名称正在审核中！");
                        tAchievementName.Text = "";
                        return;
                    }
                    else
                    {
                        Alert.Show("该成果名称已存在！");
                        tAchievementName.Text = "";
                        return;
                    }
                }
            }
        }
        //保存
        protected void Save_Click(object sender, EventArgs e)
        {
            try
            {
                if (tAchievementName.Text.Trim() == "")
                {
                    Alert.Show("成果名称不能为空！");
                    return; 
                }
                if (tAppraisalUnit.Text.Trim() == "")
                {
                    Alert.Show("鉴定组织部门不能为空！");
                    return;
                }
                if (tApRemarkRank.Text.Trim() == "")
                {
                    Alert.Show("鉴定评语级别不能为空！");
                    return;
                }
                if (tApprovalNum.Text.Trim() == "")
                {
                    Alert.Show("鉴定批文号不能为空！");
                    return;
                }
                if (ProjectInNum.Text.Trim() == "")
                {
                    Alert.Show("项目内部编号不能为空！");
                    return;
                }
                if (ProjectPeople.Text.Trim() == "")
                {
                    Alert.Show("成员不能为空！");
                    return;
                }
                if (FirstFinishedPeople.Text.Trim() == "")
                {
                    Alert.Show("成果第一完成人不能为空！");
                    return;
                }
                BLHelper.BLLAgency agency = new BLHelper.BLLAgency();
                achs.AchievementName = tAchievementName.Text.Trim();
                achs.AgencyID = agency.SelectAgencyID(DropDownListAgency.SelectedText.Trim());
                achs.ProjectName = tProjectID.Text.Trim();
                achs.AppraisalTime = dAppraisalTime.SelectedDate;
                achs.AppraisalUnit = tAppraisalUnit.Text.Trim();
                achs.ApRemarkRank = tApRemarkRank.Text.Trim();
                achs.ApprovalNum = tApprovalNum.Text.Trim();
                achs.ProjectInNum = ProjectInNum.Text.Trim();//项目分类编号
                achs.ProjectPeople = ProjectPeople.Text.Trim();//成员
                achs.FirstFinishedPeople = FirstFinishedPeople.Text.Trim();//成果第一完成人
               // achs.ProjectRank = DropDownListProjectRank.SelectedText;//鉴定级别
                achs.ProjectLevel = DropDownListProjectLevel.SelectedText;//鉴定水平
                achs.ProjectForm = DropDownListProjectForm.SelectedText;//鉴定形式
                achs.SecrecyLevel = Convert.ToInt32(dSecrecyLevel.SelectedIndex + 1);
                achs.EntryPerson = achieve.Findmodel(Convert.ToInt32(Session["AchievementID"])).EntryPerson;
                int AttachmentID = achieve.FindAttachment(Convert.ToInt32(Session["AchievementID"]));
                int OpinionPage = achieve.FindAttachment(Convert.ToInt32(Session["AchievementID"]));
                int MemberPage = achieve.FindAttachment(Convert.ToInt32(Session["AchievementID"]));
                int SealPage = achieve.FindAttachment(Convert.ToInt32(Session["AchievementID"]));
                if (Convert.ToInt32(Session["SecrecyLevel"]) == 5)//如果等于5级
                {
                    achs.IsPass = true;
                    achs.AchievementID = Convert.ToInt32(Session["AchievementID"]);
                    string path = at.FindPath(AttachmentID);
                    int attachid = pm.UpLoadFile(fileupload).Attachid;
                    if (attachid != -3)//上传控件是否有值
                    {
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
                        achs.AttachmentID = attachid;//成果表的附件为新插入的附件ID
                        achieve.Update(achs);//更新
                        pm.DeleteFile(AttachmentID, path);
                        PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("保存成功！"));
                    }
                    else //上传空间没有值
                    {
                        if (AttachmentID != 0)
                        {
                            achs.AttachmentID = AttachmentID;
                        }
                        achieve.Update(achs);//更新成果表
                        PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("保存成功！"));
                    }                  
                    string path1 = at.FindPath(OpinionPage);
                    int opinion = pm.UpLoadFile(OpinionPage1).Attachid;
                    if (opinion != -3)//上传控件是否有值
                    {
                        switch (opinion)
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
                        achs.OpinionPage = opinion;
                        achieve.Update(achs);//更新
                        pm.DeleteFile(OpinionPage, path1);
                        PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("保存成功！"));
                    }
                    else //上传空间没有值
                    {
                        if (OpinionPage != 0)
                        {
                            achs.OpinionPage = OpinionPage;
                        }
                        achieve.Update(achs);//更新成果表
                        PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("保存成功！"));
                    }
                    string path2 = at.FindPath(MemberPage);
                    int member = pm.UpLoadFile(MemberPage1).Attachid;
                    if (member != -3)//上传控件是否有值
                    {
                        switch (member)
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
                        achs.MemberPage = member;//成果表的附件为新插入的附件ID
                        achieve.Update(achs);//更新
                        pm.DeleteFile(MemberPage, path2);
                        PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("保存成功！"));
                    }
                    else //上传空间没有值
                    {
                        if (MemberPage != 0)
                        {
                            achs.MemberPage = MemberPage;
                        }
                        achieve.Update(achs);//更新成果表
                        PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("保存成功！"));
                    }
                    string path3 = at.FindPath(SealPage);
                    int seal = pm.UpLoadFile(SealPage1).Attachid;
                    if (seal != -3)//上传控件是否有值
                    {
                        switch (seal)
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
                        achs.SealPage = seal;//成果表的附件为新插入的附件ID
                        achieve.Update(achs);//更新
                        pm.DeleteFile(SealPage, path3);
                        PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("保存成功！"));
                    }
                    else //上传空间没有值
                    {
                        if (SealPage != 0)
                        {
                            achs.SealPage = SealPage;
                        }
                        achieve.Update(achs);//更新成果表
                        PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("保存成功！"));
                    }
                }
                else//小于5级
                {
                    string username = user.FindByLoginName(Session["LoginName"].ToString()).UserName;
                    log.LoginName = username;
                    log.OperationTime = DateTime.Now;
                    log.LoginIP = " ";
                    log.OperationContent = "Achievement";
                    log.OperationType = "更新";
                    achs.IsPass = false;
                    int attachid = pm.UpLoadFile(fileupload).Attachid;
                    if (attachid != -3)//上传控件是否有值
                    {
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
                        achs.AttachmentID = attachid;//成果表的附件为新插入的附件ID
                        achieve.Insert(achs);//插入成果表
                        log.OperationDataID = Convert.ToInt32(Session["AchievementID"]);
                        log.Remark = achs.AchievementID.ToString();
                        op.Insert(log);//将成果更新插入操作表                    
                        achieve.UpdateIsPass(Convert.ToInt32(Session["AchievementID"]), false);
                        PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("您的数据已提交，请等待确认！"));
                    }
                    else//上传控件没有值
                    {
                        if (AttachmentID != 0)//原来有附件
                        {
                            achs.AttachmentID = AttachmentID;
                        }
                        achieve.Insert(achs);
                        log.OperationDataID = Convert.ToInt32(Session["AchievementID"]);
                        log.Remark = achs.AchievementID.ToString();
                        op.Insert(log);
                        achieve.UpdateIsPass(Convert.ToInt32(Session["AchievementID"]), false);
                        PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("您的数据已提交，请等待确认！"));
                    }
                    int opinion = pm.UpLoadFile(OpinionPage1).Attachid;
                    if (opinion != -3)//上传控件是否有值
                    {
                        switch (opinion)
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
                        achs.OpinionPage = opinion;
                        achieve.Insert(achs);//插入成果表
                        log.OperationDataID = Convert.ToInt32(Session["AchievementID"]);
                        log.Remark = achs.AchievementID.ToString();
                        op.Insert(log);//将成果更新插入操作表                    
                        achieve.UpdateIsPass(Convert.ToInt32(Session["AchievementID"]), false);
                        PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("您的数据已提交，请等待确认！"));
                    }
                    else//上传控件没有值
                    {
                        if (OpinionPage != 0)//原来有附件
                        {
                            achs.OpinionPage = OpinionPage;
                        }
                        achieve.Insert(achs);
                        log.OperationDataID = Convert.ToInt32(Session["AchievementID"]);
                        log.Remark = achs.AchievementID.ToString();
                        op.Insert(log);
                        achieve.UpdateIsPass(Convert.ToInt32(Session["AchievementID"]), false);
                        PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("您的数据已提交，请等待确认！"));
                    }
                    int member = pm.UpLoadFile(MemberPage1).Attachid;
                    if (member != -3)//上传控件是否有值
                    {
                        switch (member)
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
                        achs.MemberPage = member;
                        achieve.Insert(achs);//插入成果表
                        log.OperationDataID = Convert.ToInt32(Session["AchievementID"]);
                        log.Remark = achs.AchievementID.ToString();
                        op.Insert(log);//将成果更新插入操作表                    
                        achieve.UpdateIsPass(Convert.ToInt32(Session["AchievementID"]), false);
                        PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("您的数据已提交，请等待确认！"));
                    }
                    else//上传控件没有值
                    {
                        if (MemberPage != 0)//原来有附件
                        {
                            achs.MemberPage = MemberPage;
                        }
                        achieve.Insert(achs);
                        log.OperationDataID = Convert.ToInt32(Session["AchievementID"]);
                        log.Remark = achs.AchievementID.ToString();
                        op.Insert(log);
                        achieve.UpdateIsPass(Convert.ToInt32(Session["AchievementID"]), false);
                        PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("您的数据已提交，请等待确认！"));
                    }
                    int seal = pm.UpLoadFile(SealPage1).Attachid;
                    if (seal != -3)//上传控件是否有值
                    {
                        switch (seal)
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
                        achs.SealPage = seal;
                        achieve.Insert(achs);//插入成果表
                        log.OperationDataID = Convert.ToInt32(Session["AchievementID"]);
                        log.Remark = achs.AchievementID.ToString();
                        op.Insert(log);//将成果更新插入操作表                    
                        achieve.UpdateIsPass(Convert.ToInt32(Session["AchievementID"]), false);
                        PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("您的数据已提交，请等待确认！"));
                    }
                    else//上传控件没有值
                    {
                        if (SealPage != 0)//原来有附件
                        {
                            achs.SealPage = SealPage;
                        }
                        achieve.Insert(achs);
                        log.OperationDataID = Convert.ToInt32(Session["AchievementID"]);
                        log.Remark = achs.AchievementID.ToString();
                        op.Insert(log);
                        achieve.UpdateIsPass(Convert.ToInt32(Session["AchievementID"]), false);
                        PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("您的数据已提交，请等待确认！"));
                    }
                }
            }
            catch (Exception ex)
            {
                int attachid = Convert.ToInt32(achs.AttachmentID);
                string path = at.FindPath(attachid);
                pm.DeleteFile(attachid, path);
                pm.SaveError(ex, this.Request);
            }

        }
        ////项目名称验证
        //protected void tProjectID_TextChanged(object sender, EventArgs e)
        //{
        //    if (tProjectID.Text.Trim() != "")
        //    {
        //        if (project.IsNullProject(tProjectID.Text.Trim()) == null)
        //        {
        //            Alert.Show("该项目名称不存在！");
        //            tProjectID.Text = "";
        //            return;
        //        }
        //        else
        //        {
        //            if (project.IsNullProject(tProjectID.Text.Trim()).IsPass == false)
        //            {
        //                Alert.Show("该项目名称正在审核中！");
        //                tProjectID.Text = "";
        //                return;
        //            }
        //        }
        //    }
        //}
        //重置
        protected void DeleteAll_Click(object sender, EventArgs e)
        {
            try
            {
                tAchievementName.Reset();
                DropDownListAgency.Reset();
                tProjectID.Reset();
                dAppraisalTime.Reset();
                tAppraisalUnit.Reset();
                tApRemarkRank.Reset();
                tApprovalNum.Reset();
                dSecrecyLevel.Reset();
                ProjectInNum.Reset(); //项目分类编号
                ProjectPeople.Reset(); //成员
                FirstFinishedPeople.Reset();//成果第一完成人
                DropDownListProjectForm.Reset();//鉴定形式
                DropDownListProjectLevel.Reset();//鉴定水平
                //DropDownListProjectRank.Reset();//鉴定级别

                PageContext.RegisterStartupScript("clearFile();");
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
        //初始化鉴定级别下拉框
        public void InitDropDownListProjectRank()
        {
            try
            {
                List<BasicCode> list = bllBasicCode.FindALLName("鉴定级别");
                for (int i = 0; i < list.Count(); i++)
                {
                    //DropDownListProjectRank.Items.Add(list[i].CategoryContent.ToString(), list[i].CategoryContent.ToString());
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
        //初始化鉴定形式下拉框
        public void InitDropDownListProjectForm()
        {
            try
            {
                List<BasicCode> list = bllBasicCode.FindALLName("鉴定形式");
                for (int i = 0; i < list.Count(); i++)
                {
                    DropDownListProjectForm.Items.Add(list[i].CategoryContent.ToString(), list[i].CategoryContent.ToString());
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
        //初始化鉴定水平下拉框
        public void InitDropDownListProjectLevel()
        {
            try
            {
                List<BasicCode> list = bllBasicCode.FindALLName("鉴定水平");
                for (int i = 0; i < list.Count(); i++)
                {
                    DropDownListProjectLevel.Items.Add(list[i].CategoryContent.ToString(), list[i].CategoryContent.ToString());
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }

    }
}