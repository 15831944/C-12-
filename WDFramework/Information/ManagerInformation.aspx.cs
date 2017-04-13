/**编写人：张凡凡
 * 时间：2014年8月11号
 * 功能：管理员个人信息
 * 修改履历：    1、修改人：吕博杨
 *                 修改时间：2015年11月28日
 *                 修改内容：Patent部分函数返回值更改，需要协调更改此处代码
 *              2、修改人：吕博杨
 *                 修改时间：2015年12月7日
 *                 修改内容：为项目-项目全部信息模块 项目相关文档的操作信息添加支持代码
 **/ 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common.Entities;
using BLHelper;

namespace WDFramework
{
    public partial class Manager : System.Web.UI.Page
    {
        BLLOperationLog blop = new BLHelper.BLLOperationLog();
        BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
        BLLAttachment blat = new BLLAttachment();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                List<OperationLog> oplist = blop.FindPaged(false);
                for (int i = 0; i < oplist.Count; i++)
                    blop.Delete(oplist[i]);
                InitGrid();
            }
        }

        //同意
        protected void BtnAgree_Click(object sender, EventArgs e)
        {
            DoChart(true);
            InitGrid();
        }

        //不同意
        protected void BtnDisAgree_Click(object sender, EventArgs e)
        {
            DoChart(false);
            InitGrid();
        }

        //
        protected void DoChart(bool IsAgree)
        {
            try
            {
                int[] selections = GridOpetate.SelectedRowIndexArray;
                for (int i = 0; i < selections.Count(); i++)
                {
                    int id = Convert.ToInt32(GridOpetate.DataKeys[selections[i]][0]);
                    OperationLog op = blop.FindbyId(id);
                    switch (op.OperationContent)
                    {
                        case "AcademicMeeting":
                            AcademicMetting(op.OperationType, IsAgree, Convert.ToInt32(op.OperationDataID), op.Remark);
                            break;
                        case "AchieveAward":
                            AchieveAward(op.OperationType, IsAgree, Convert.ToInt32(op.OperationDataID), op.Remark);
                            break;
                        case "Achievement":
                            Achievement(op.OperationType, IsAgree, Convert.ToInt32(op.OperationDataID), op.Remark);
                            break;
                        case "AchievementCA":
                            AchievementCA(op.OperationType, IsAgree, Convert.ToInt32(op.OperationDataID), op.Remark);
                            break;
                        case "AchivementApply":
                            AchivementApply(op.OperationType, IsAgree, Convert.ToInt32(op.OperationDataID), op.Remark);
                            break;
                        case "Agency":
                            Agency(op.OperationType, IsAgree, Convert.ToInt32(op.OperationDataID), op.Remark);
                            break;
                        case "Announcement":
                            Announcement(op.OperationType, IsAgree, Convert.ToInt32(op.OperationDataID), op.Remark);
                            break;
                        case "AttendMeeting":
                            AttendMeeting(op.OperationType, IsAgree, Convert.ToInt32(op.OperationDataID), op.Remark);
                            break;
                        case "Award":
                            Award(op.OperationType, IsAgree, Convert.ToInt32(op.OperationDataID), op.Remark);
                            break;
                        case "Contract":
                            Contract(op.OperationType, IsAgree, Convert.ToInt32(op.OperationDataID), op.Remark);
                            break;
                        case "DFurtherStudy":
                            DFurtherStudy(op.OperationType, IsAgree, Convert.ToInt32(op.OperationDataID), op.Remark);
                            break;
                        case "Education":
                            Education(op.OperationType, IsAgree, Convert.ToInt32(op.OperationDataID), op.Remark);
                            break;
                        case "EduExperience":
                            EduExperience(op.OperationType, IsAgree, Convert.ToInt32(op.OperationDataID), op.Remark);
                            break;
                        case "Files":
                            Files(op.OperationType, IsAgree, Convert.ToInt32(op.OperationDataID), op.Remark);
                            break;
                        case "FundInformation":
                            FundInformation(op.OperationType, IsAgree, Convert.ToInt32(op.OperationDataID), op.Remark);
                            break;
                        case "FutherStudy":
                            FutherStudy(op.OperationType, IsAgree, Convert.ToInt32(op.OperationDataID), op.Remark);
                            break;
                        case "Honor":
                            Honor(op.OperationType, IsAgree, Convert.ToInt32(op.OperationDataID), op.Remark);
                            break;
                        case "LibraryRecord":
                            LibraryRecord(op.OperationType, IsAgree, Convert.ToInt32(op.OperationDataID), op.Remark);
                            break;
                        case "Monograph":
                            Monograph(op.OperationType, IsAgree, Convert.ToInt32(op.OperationDataID), op.Remark);
                            break;
                        case "Pact":
                            Pact(op.OperationType, IsAgree, Convert.ToInt32(op.OperationDataID), op.Remark);
                            break;
                        case "Paper":
                            Paper(op.OperationType, IsAgree, Convert.ToInt32(op.OperationDataID), op.Remark);
                            break;
                        case "Patent":
                            Patent(op.OperationType, IsAgree, Convert.ToInt32(op.OperationDataID), op.Remark);
                            break;
                        case "Photos":
                            Photos(op.OperationType, IsAgree, Convert.ToInt32(op.OperationDataID), op.Remark);
                            break;
                        case "Project":
                            Project(op.OperationType, IsAgree, Convert.ToInt32(op.OperationDataID), op.Remark);
                            break;
                        case "ProjectImportantNode":
                            ProjectImportantNode(op.OperationType, IsAgree, Convert.ToInt32(op.OperationDataID), op.Remark);
                            break;
                        case "ScienceReport":
                            ScienceReport(op.OperationType, IsAgree, Convert.ToInt32(op.OperationDataID), op.Remark);
                            break;
                        case "SocialPartTime":
                            SocialPartTime(op.OperationType, IsAgree, Convert.ToInt32(op.OperationDataID), op.Remark);
                            break;
                        case "SpeakClass":
                            SpeakClass(op.OperationType, IsAgree, Convert.ToInt32(op.OperationDataID), op.Remark);
                            break;
                        case "StaffDevote":
                            StaffDevote(op.OperationType, IsAgree, Convert.ToInt32(op.OperationDataID), op.Remark);
                            break;
                        case "Student":
                            Student(op.OperationType, IsAgree, Convert.ToInt32(op.OperationDataID), op.Remark);
                            break;
                        case "UnitInspect":
                            UnitInspect(op.OperationType, IsAgree, Convert.ToInt32(op.OperationDataID), op.Remark);
                            break;
                        case "UnitLectures":
                            UnitLectures(op.OperationType, IsAgree, Convert.ToInt32(op.OperationDataID), op.Remark);
                            break;
                        case "UserInfo":
                            Users(op.OperationType, IsAgree, Convert.ToInt32(op.OperationDataID), op.Remark);
                            break;
                        case "WorkExperience":
                            WorkExperience(op.OperationType, IsAgree, Convert.ToInt32(op.OperationDataID), op.Remark);
                            break;
                        case "WorkPlanSummary":
                            WorkPlanSummary(op.OperationType, IsAgree, Convert.ToInt32(op.OperationDataID), op.Remark);
                            break;
                        case "WithinPost":
                            WithinPost(op.OperationType, IsAgree, Convert.ToInt32(op.OperationDataID), op.Remark);
                            break;
                        case "Platform":
                            Platform(op.OperationType, IsAgree, Convert.ToInt32(op.OperationDataID), op.Remark);
                            break;
                        case "NewAcademicReporting":
                            NewAcademicReporting(op.OperationType, IsAgree, Convert.ToInt32(op.OperationDataID), op.Remark);
                            break;
                        //lby ↓
                        case "ProjectFile":
                            ProjectFiles(op.OperationType, IsAgree, Convert.ToInt32(op.OperationDataID), op.Remark);
                            break;
                    }
                    blop.UpdateIsPass(id, false);
                }
            }
            catch (Exception ex)
            {
                BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
                pm.SaveError(ex, this.Request);
            }
        }
        

        //数据绑定
        protected void InitGrid()
        {
            List<OperationLog> oplist = blop.FindPaged(true);
            var res = oplist.Skip(GridOpetate.PageIndex * GridOpetate.PageSize).Take(GridOpetate.PageSize).ToList();
            GridOpetate.RecordCount = oplist.Count;
            GridOpetate.DataSource = res;
            GridOpetate.DataBind();
        }

        protected string GetEditUrlp(object ID)
        {
            return Detail.GetShowReference("Detail.aspx?id=" + ID, "操作详情");
        }

        //表Platform操作函数
        protected void Platform(string OpeType, bool Agree, int OpeId, string ReId)
        {
            BLLPlatform blplat = new BLLPlatform();
            switch (OpeType)
            {
                case "删除":
                    if (Agree)
                        blplat.Delete(OpeId);
                    else
                        blplat.UpdateIsPass(OpeId, true);
                    break;
                case "添加":
                    if (Agree)
                        blplat.UpdateIsPass(OpeId, true);
                    else
                        blplat.Delete(OpeId);
                    break;
                case "更新":
                    int i = Convert.ToInt32(ReId);
                    if (Agree)
                    {
                        blplat.UpdateIsPass(i, true);
                        blplat.Delete(OpeId);
                    }
                    else
                    {
                        blplat.UpdateIsPass(OpeId, true);
                        blplat.Delete(i);
                    }
                    break;
            }
        }

        //表WithinPost操作函数
        protected void WithinPost(string OpeType, bool Agree, int OpeId, string ReId)
        {
            BLLWithinPost blwin = new BLLWithinPost();
            switch (OpeType)
            {
                case "删除":
                    if (Agree)
                    {
                        int attachid = blwin.FindAttachmentID(OpeId);
                        if (attachid > 0)
                        {
                            string path = blat.FindPath(attachid);
                            pm.DeleteFile(attachid, path);
                        }
                        blwin.Delete(OpeId);
                    }
                    else
                        blwin.UpdateIsPass(OpeId, true);
                    break;
                case "添加":
                    if (Agree)
                        blwin.UpdateIsPass(OpeId, true);
                    else
                    {
                        int attachid = blwin.FindAttachmentID(OpeId);
                        if (attachid > 0)
                        {
                            string path = blat.FindPath(attachid);
                            pm.DeleteFile(attachid, path);
                        }
                        blwin.Delete(OpeId);
                    }
                    break;
                case "更新":
                    int i = Convert.ToInt32(ReId);
                    int attachidold = blwin.FindAttachmentID(OpeId);
                    int attachidnew = blwin.FindAttachmentID(i);
                    if (Agree)
                    {
                        blwin.UpdateIsPass(i, true);
                        if (attachidold > 0 && attachidold != attachidnew)
                        {
                            string path = blat.FindPath(attachidold);
                            pm.DeleteFile(attachidold, path);
                        }
                        blwin.Delete(OpeId);
                    }
                    else
                    {
                        blwin.UpdateIsPass(OpeId, true);
                        if (attachidnew > 0 && attachidold != attachidnew)
                        {
                            string path = blat.FindPath(attachidnew);
                            pm.DeleteFile(attachidnew, path);
                        }
                        blwin.Delete(i);
                    }
                    break;
            }
        }

        //表NewAcademicReporting操作函数
        protected void NewAcademicReporting(string OpeType, bool Agree, int OpeId, string ReId)
        {
            BLLNewAcademicReporting blnewaca = new BLLNewAcademicReporting();
            switch (OpeType)
            {
                //case "删除":
                //    if (Agree)
                //    {
                //        int attachid = blnewaca.FindAttachmentID(OpeId);
                //        if (attachid > 0)
                //        {
                //            string path = blat.FindPath(attachid);
                //            pm.DeleteFile(attachid, path);
                //        }
                //        blnewaca.Delete(OpeId);
                //    }
                //    else
                //       // blnewaca.UpdateIsPass(OpeId, true);
                //    break;
                //case "添加":
                //    if (Agree)
                //       // blnewaca.UpdateIsPass(OpeId, true);
                //    else
                //    {
                //        int attachid = blnewaca.FindAttachmentID(OpeId);
                //        if (attachid > 0)
                //        {
                //            string path = blat.FindPath(attachid);
                //            pm.DeleteFile(attachid, path);
                //        }
                //        blnewaca.Delete(OpeId);
                //    }
                //    break;
                //case "更新":
                //    int i = Convert.ToInt32(ReId);
                //    int attachidold = blnewaca.FindAttachmentID(OpeId);
                //    int attachidnew = blnewaca.FindAttachmentID(i);
                //    if (Agree)
                //    {
                //        blnewaca.UpdateIsPass(i, true);
                //        if (attachidold > 0 && attachidold != attachidnew)
                //        {
                //            string path = blat.FindPath(attachidold);
                //            pm.DeleteFile(attachidold, path);
                //        }
                //        blnewaca.Delete(OpeId);
                //    }
                //    else
                //    {
                //        blnewaca.UpdateIsPass(OpeId, true);
                //        if (attachidnew > 0 && attachidold != attachidnew)
                //        {
                //            string path = blat.FindPath(attachidnew);
                //            pm.DeleteFile(attachidnew, path);
                //        }
                //        blnewaca.Delete(i);
                //    }
                //    break;
            }
        }

        //表AcademicMeting操作函数
        protected void AcademicMetting(string OpeType, bool Agree, int OpeId, string ReId)
        {
            BLLAcademicMeeting blAca = new BLLAcademicMeeting();
            BLLAttendMeeting BLLAttend = new BLLAttendMeeting();
            switch (OpeType)
            {
                case "删除":
                    if (Agree)
                    {
                        int attachid = blAca.FindAttachmentID(OpeId);
                        if (attachid > 0)
                        {
                            string path = blat.FindPath(attachid);
                            pm.DeleteFile(attachid, path);
                        }
                        //删除会议参见人员表中的数据
                        BLLAttend.DeleteStaffByMeetingID(OpeId); ;
                        //删除学术会议中的学术报告信息
                        blAca.DeleteReportByMeetingID(OpeId);
                        blAca.DeleteByAcademicMeetingID(OpeId);
                    }
                    else
                        blAca.UpdateIsPass(OpeId, true);
                    break;
                case "添加":
                    if (Agree)
                        blAca.UpdateIsPass(OpeId, true);
                    else
                    {
                        int attachid = blAca.FindAttachmentID(OpeId);
                        if (attachid > 0)
                        {
                            string path = blat.FindPath(attachid);
                            pm.DeleteFile(attachid, path);
                        }
                        //删除会议参见人员表中的数据
                        BLLAttend.DeleteStaffByMeetingID(OpeId); ;
                        //删除学术会议中的学术报告信息
                        blAca.DeleteReportByMeetingID(OpeId);
                        blAca.DeleteByAcademicMeetingID(OpeId);
                    }
                    break;
            }
        }

        //AchieveAward
        protected void AchieveAward(string OpeType, bool Agree, int OpeId, string ReId)
        {
            BLLAchieveAward blAch = new BLLAchieveAward();
            switch (OpeType)
            {
                case "删除":
                    if (Agree)
                        blAch.Delete(OpeId);
                    else
                        blAch.UpdateIsPass(OpeId, true);
                    break;
                case "添加":
                    if (Agree)
                        blAch.UpdateIsPass(OpeId, true);
                    else
                        blAch.Delete(OpeId);
                    break;
                case "更新":
                    int i = Convert.ToInt32(ReId);
                    if (Agree)
                    {
                        blAch.UpdateIsPass(i, true);
                        blAch.Delete(OpeId);
                    }
                    else
                    {
                        blAch.UpdateIsPass(OpeId, true);
                        blAch.Delete(i);
                    }
                    break;
            }
        }

        //Achievement
        protected void Achievement(string OpeType, bool Agree, int OpeId, string ReId)
        {
            BLLAchievement blAchi = new BLLAchievement();
            switch (OpeType)
            {
                case "删除":
                    if (Agree)
                    {
                        int attachid = blAchi.FindAttachment(OpeId);
                        if (attachid > 0)
                        {
                            string path = blat.FindPath(attachid);
                            pm.DeleteFile(attachid, path);
                        }
                        blAchi.Delete(OpeId);
                    }
                    else
                        blAchi.UpdateIsPass(OpeId, true);
                    break;
                case "添加":
                    if (Agree)
                        blAchi.UpdateIsPass(OpeId, true);
                    else
                    {
                        int attachid = blAchi.FindAttachment(OpeId);
                        if (attachid > 0)
                        {
                            string path = blat.FindPath(attachid);
                            pm.DeleteFile(attachid, path);
                        }
                        blAchi.Delete(OpeId);
                    }
                    break;
                case "更新":
                    int i = Convert.ToInt32(ReId);
                        int attachidold = blAchi.FindAttachment(OpeId);
                        int attachidnew = blAchi.FindAttachment(i);
                    if (Agree)
                    {
                        blAchi.UpdateIsPass(i, true);
                        if (attachidold > 0 && attachidnew != attachidold)
                        {
                            string path = blat.FindPath(attachidold);
                            pm.DeleteFile(attachidold, path);
                        }
                        blAchi.Delete(OpeId);
                    }
                    else
                    {
                        blAchi.UpdateIsPass(OpeId, true);
                        if (attachidnew > 0 && attachidnew != attachidold)
                        {
                            string path = blat.FindPath(attachidnew);
                            pm.DeleteFile(attachidnew, path);
                        }
                        blAchi.Delete(i);
                    }
                    break;
            }
        }

        //AchievementCA
        protected void AchievementCA(string OpeType, bool Agree, int OpeId, string ReId)
        {
            BLLAchievementCA blAchi = new BLLAchievementCA();
            switch (OpeType)
            {
                case "删除":
                    if (Agree)
                    {
                        int attachid = blAchi.FindAttachmentID(OpeId);
                        if (attachid > 0)
                        {
                            string path = blat.FindPath(attachid);
                            pm.DeleteFile(attachid, path);
                        }
                        blAchi.Delete(OpeId);
                    }
                    else
                        blAchi.UpdateIsPass(OpeId, true);
                    break;
                case "添加":
                    if (Agree)
                        blAchi.UpdateIsPass(OpeId, true);
                    else
                    {
                        int attachid = blAchi.FindAttachmentID(OpeId);
                        if (attachid > 0)
                        {
                            string path = blat.FindPath(attachid);
                            pm.DeleteFile(attachid, path);
                        }
                        blAchi.Delete(OpeId);
                    }
                    break;
                case "更新":
                    int i = Convert.ToInt32(ReId);
                        int attachidold = blAchi.FindAttachmentID(OpeId);
                        int attachidnew = blAchi.FindAttachmentID(i);
                    if (Agree)
                    {
                        blAchi.UpdateIsPass(i, true);
                        if (attachidold > 0 && attachidnew != attachidold)
                        {
                            string path = blat.FindPath(attachidold);
                            pm.DeleteFile(attachidold, path);
                        }
                        blAchi.Delete(OpeId);
                    }
                    else
                    {
                        blAchi.UpdateIsPass(OpeId, true);
                        if (attachidnew > 0 && attachidnew != attachidold)
                        {
                            string path = blat.FindPath(attachidnew);
                            pm.DeleteFile(attachidnew, path);
                        }
                        blAchi.Delete(i);
                    }
                    break;
            }
        }

        //AchivementApply
        private void AchivementApply(string OpeType, bool Agree, int OpeId, string ReId)
        {
            BLLAchievementApply blAchi = new BLLAchievementApply();
            switch (OpeType)
            {
                case "删除":
                    if (Agree)
                    {
                        int attachid = blAchi.FindAttachmentID(OpeId);
                        if (attachid > 0)
                        {
                            string path = blat.FindPath(attachid);
                            pm.DeleteFile(attachid, path);
                        }
                        blAchi.Delete(OpeId);
                    }
                    else
                        blAchi.UpdateIsPass(OpeId, true);
                    break;
                case "添加":
                    if (Agree)
                        blAchi.UpdateIsPass(OpeId, true);
                    else
                    {
                        int attachid = blAchi.FindAttachmentID(OpeId);
                        if (attachid > 0)
                        {
                            string path = blat.FindPath(attachid);
                            pm.DeleteFile(attachid, path);
                        }
                        blAchi.Delete(OpeId);
                    }
                    break;
                case "更新":
                    int i = Convert.ToInt32(ReId);
                    int attachidold = blAchi.FindAttachmentID(OpeId);
                    int attachidnew = blAchi.FindAttachmentID(i);
                    if (Agree)
                    {
                        blAchi.UpdateIsPass(i, true);
                        if (attachidold > 0 && attachidnew != attachidold)
                        {
                            string path = blat.FindPath(attachidold);
                            pm.DeleteFile(attachidold, path);
                        }
                        blAchi.Delete(OpeId);
                    }
                    else
                    {
                        blAchi.UpdateIsPass(OpeId, true);
                        if (attachidnew > 0 && attachidnew != attachidold)
                        {
                            string path = blat.FindPath(attachidnew);
                            pm.DeleteFile(attachidnew, path);
                        }
                        blAchi.Delete(i);
                    }
                    break;
            }
        }

        //Agency
        protected void Agency(string OpeType, bool Agree, int OpeId, string ReId)
        {
            BLLAgency blAg = new BLLAgency();
            switch (OpeType)
            {
                case "删除":
                    if (Agree)
                        blAg.Delete(OpeId);
                    else
                        blAg.UpdatePass(OpeId, true);
                    break;
                case "添加":
                    if (Agree)
                        blAg.UpdatePass(OpeId, true);
                    else
                        blAg.Delete(OpeId);
                    break;
                case "更新":
                    int i = Convert.ToInt32(ReId);
                    if (Agree)
                    {
                        blAg.UpdatePass(i, true);
                        blAg.Delete(OpeId);
                    }
                    else
                    {
                        blAg.UpdatePass(OpeId, true);
                        blAg.Delete(i);
                    }
                    break;
            }
        }

        //Announcement
        protected void Announcement(string OpeType, bool Agree, int OpeId, string ReId)
        {
            BLLAnnouncement blAnn = new BLLAnnouncement();
            switch (OpeType)
            {
                case "删除":
                    if (Agree)
                    {
                        int attachid = blAnn.FindAttachmentID(OpeId);
                        if (attachid > 0)
                        {
                            string path = blat.FindPath(attachid);
                            pm.DeleteFile(attachid, path);
                        }
                        blAnn.Delete(OpeId);
                    }
                    else
                        blAnn.UpdateIsPass(OpeId, true);
                    break;
                case "添加":
                    if (Agree)
                        blAnn.UpdateIsPass(OpeId, true);
                    else
                    {
                        int attachid = blAnn.FindAttachmentID(OpeId);
                        if (attachid > 0)
                        {
                            string path = blat.FindPath(attachid);
                            pm.DeleteFile(attachid, path);
                        }
                        blAnn.Delete(OpeId);
                    }
                    break;
                case "更新":
                    int i = Convert.ToInt32(ReId);
                        int attachidold = blAnn.FindAttachmentID(OpeId);
                        int attachidnew = blAnn.FindAttachmentID(i);
                    if (Agree)
                    {
                        blAnn.UpdateIsPass(i, true);
                        if (attachidold > 0 && attachidold != attachidnew)
                        {
                            string path = blat.FindPath(attachidold);
                            pm.DeleteFile(attachidold, path);
                        }
                        blAnn.Delete(OpeId);
                    }
                    else
                    {
                        blAnn.UpdateIsPass(OpeId, true);
                        if (attachidnew > 0 && attachidold != attachidnew)
                        {
                            string path = blat.FindPath(attachidnew);
                            pm.DeleteFile(attachidnew, path);
                        }
                        blAnn.Delete(i);
                    }
                    break;
            }
        }

        //AttendMeeting
        protected void AttendMeeting(string OpeType, bool Agree, int OpeId, string ReId)
        {
            BLLAttendMeeting blAtt = new BLLAttendMeeting();
            switch (OpeType)
            {
                case "删除":
                    if (Agree)
                        blAtt.Delete(OpeId);
                    else
                        blAtt.UpdateIsPass(OpeId, true);
                    break;
                case "添加":
                    if (Agree)
                        blAtt.UpdateIsPass(OpeId, true);
                    else
                        blAtt.Delete(OpeId);
                    break;
                case "更新":
                    int i = Convert.ToInt32(ReId);
                    if (Agree)
                    {
                        blAtt.UpdateIsPass(i, true);
                        blAtt.Delete(OpeId);
                    }
                    else
                    {
                        blAtt.UpdateIsPass(OpeId, true);
                        blAtt.Delete(i);
                    }
                    break;
            }
        }

        //Award
        protected void Award(string OpeType, bool Agree, int OpeId, string ReId)
        {
            BLLAward blAw = new BLLAward();
            switch (OpeType)
            {
                case "删除":
                    if (Agree)
                    {
                        int attachid = blAw.FindAttachmentID(OpeId);
                        if (attachid > 0)
                        {
                            string path = blat.FindPath(attachid);
                            pm.DeleteFile(attachid, path);
                        }
                        blAw.Delete(OpeId);
                    }
                    else
                        blAw.UpdateIsPass(OpeId, true);
                    break;
                case "添加":
                    if (Agree)
                        blAw.UpdateIsPass(OpeId, true);
                    else
                    {
                        int attachid = blAw.FindAttachmentID(OpeId);
                        if (attachid > 0)
                        {
                            string path = blat.FindPath(attachid);
                            pm.DeleteFile(attachid, path);
                        }
                        blAw.Delete(OpeId);
                    }
                    break;
                case "更新":
                    int i = Convert.ToInt32(ReId);
                    int attachidold = blAw.FindAttachmentID(OpeId);
                    int attachidnew = blAw.FindAttachmentID(i);
                    if (Agree)
                    {
                        blAw.UpdateIsPass(i, true);
                        if (attachidold > 0 && attachidnew != attachidold)
                        {
                            string path = blat.FindPath(attachidold);
                            pm.DeleteFile(attachidold, path);
                        }
                        blAw.Delete(OpeId);
                    }
                    else
                    {
                        blAw.UpdateIsPass(OpeId, true);
                        if (attachidnew > 0 && attachidnew != attachidold)
                        {
                            string path = blat.FindPath(attachidnew);
                            pm.DeleteFile(attachidnew, path);
                        }
                        blAw.Delete(i);
                    }
                    break;
            }
        }

        //Contract
        protected void Contract(string OpeType, bool Agree, int OpeId, string ReId)
        {
            BLLContract blCon = new BLLContract();
            BLLLibraryRecord blrecord = new BLLLibraryRecord();
            switch (OpeType)
            {
                case "删除":
                    if (Agree)
                    {
                        int attachid = blCon.FindAttachmentID(OpeId);
                        if (attachid > 0)
                        {
                            string path = blat.FindPath(attachid);
                            pm.DeleteFile(attachid, path);
                        }
                        //删除资料借阅记录
                        List<int> listRecordID = blrecord.FindLibraryID(OpeId, "资料");
                        if (listRecordID != null)
                        {
                            for (int j = 0; j < listRecordID.Count(); j++)
                                blrecord.Delete(listRecordID[j]);
                        }

                        blCon.Delete(OpeId);
                    }
                    else
                        blCon.UpdateIsPass(OpeId, true);
                    break;
                case "添加":
                    if (Agree)
                        blCon.UpdateIsPass(OpeId, true);
                    else
                    {
                        int attachid = blCon.FindAttachmentID(OpeId);
                        if (attachid > 0)
                        {
                            string path = blat.FindPath(attachid);
                            pm.DeleteFile(attachid, path);
                        }
                        //删除资料借阅记录
                        List<int> listRecordID = blrecord.FindLibraryID(OpeId, "资料");
                        if (listRecordID != null)
                        {
                            for (int j = 0; j < listRecordID.Count(); j++)
                                blrecord.Delete(listRecordID[j]);
                        }

                        blCon.Delete(OpeId);
                    }
                    break;
                case "更新":
                    int i = Convert.ToInt32(ReId);
                    int attachidold = blCon.FindAttachmentID(OpeId);
                    int attachidnew = blCon.FindAttachmentID(i);
                    if (Agree)
                    {
                        blCon.UpdateIsPass(i, true);
                        if (attachidold > 0 && attachidold != attachidnew)
                        {
                            string path = blat.FindPath(attachidold);
                            pm.DeleteFile(attachidold, path);
                        }
                        //删除资料借阅记录
                        List<int> listRecordID = blrecord.FindLibraryID(OpeId, "资料");
                        if (listRecordID != null)
                        {
                            for (int j = 0; j < listRecordID.Count(); j++)
                                blrecord.Delete(listRecordID[j]);
                        }

                        blCon.Delete(OpeId);
                    }
                    else
                    {
                        blCon.UpdateIsPass(OpeId, true);
                        if (attachidnew > 0 && attachidold != attachidnew)
                        {
                            string path = blat.FindPath(attachidnew);
                            pm.DeleteFile(attachidnew, path);
                        }
                        //删除资料借阅记录
                        List<int> listRecordID = blrecord.FindLibraryID(i, "资料");
                        if (listRecordID != null)
                        {
                            for (int j = 0; j < listRecordID.Count(); j++)
                                blrecord.Delete(listRecordID[j]);
                        }

                        blCon.Delete(i);
                    }
                    break;
            }
        }

        //DFurtherStudy
        protected void DFurtherStudy(string OpeType, bool Agree, int OpeId, string ReId)
        {
            BLLDFurtherStudy blDFur = new BLLDFurtherStudy();
            switch (OpeType)
            {
                case "删除":
                    if (Agree)
                        blDFur.Delete(OpeId);
                    else
                        blDFur.UpdateIsPass(OpeId, true);
                    break;
                case "添加":
                    if (Agree)
                        blDFur.UpdateIsPass(OpeId, true);
                    else
                        blDFur.Delete(OpeId);
                    break;
                case "更新":
                    int i = Convert.ToInt32(ReId);
                    if (Agree)
                    {
                        blDFur.UpdateIsPass(i, true);
                        blDFur.Delete(OpeId);
                    }
                    else
                    {
                        blDFur.UpdateIsPass(OpeId, true);
                        blDFur.Delete(i);
                    }
                    break;
            }
        }

        //Education
        protected void Education(string OpeType, bool Agree, int OpeId, string ReId)
        {
            BLLEducation blEdu = new BLLEducation();
            switch (OpeType)
            {
                case "删除":
                    if (Agree)
                        blEdu.Delete(OpeId);
                    else
                        blEdu.UpdateIsPass(OpeId, true);
                    break;
                case "添加":
                    if (Agree)
                        blEdu.UpdateIsPass(OpeId, true);
                    else
                        blEdu.Delete(OpeId);
                    break;
                case "更新":
                    int i = Convert.ToInt32(ReId);
                    if (Agree)
                    {
                        blEdu.UpdateIsPass(i, true);
                        blEdu.Delete(OpeId);
                    }
                    else
                    {
                        blEdu.UpdateIsPass(OpeId, true);
                        blEdu.Delete(i);
                    }
                    break;
            }
        }

        //EduExperience
        protected void EduExperience(string OpeType, bool Agree, int OpeId, string ReId)
        {
            BLLEduExperience blEdu = new BLLEduExperience();
            switch (OpeType)
            {
                case "删除":
                    if (Agree)
                        blEdu.Delete(OpeId);
                    else
                        blEdu.UpdateIsPass(OpeId, true);
                    break;
                case "添加":
                    if (Agree)
                        blEdu.UpdateIsPass(OpeId, true);
                    else
                        blEdu.Delete(OpeId);
                    break;
                case "更新":
                    int i = Convert.ToInt32(ReId);
                    if (Agree)
                    {
                        blEdu.UpdateIsPass(i, true);
                        blEdu.Delete(OpeId);
                    }
                    else
                    {
                        blEdu.UpdateIsPass(OpeId, true);
                        blEdu.Delete(i);
                    }
                    break;
            }
        }

        //Files
        protected void Files(string OpeType, bool Agree, int OpeId, string ReId)
        {
            BLLFiles blFile = new BLLFiles();
            switch (OpeType)
            {
                case "删除":
                    if (Agree)
                    {
                        int attachid = blFile.FindAttachmentID(OpeId);
                        if (attachid > 0)
                        {
                            string path = blat.FindPath(attachid);
                            pm.DeleteFile(attachid, path);
                        }
                        blFile.Delete(OpeId);
                    }
                    else
                        blFile.UpdateIsPass(OpeId, true);
                    break;
                case "添加":
                    if (Agree)
                        blFile.UpdateIsPass(OpeId, true);
                    else
                    {
                        int attachid = blFile.FindAttachmentID(OpeId);
                        if (attachid > 0)
                        {
                            string path = blat.FindPath(attachid);
                            pm.DeleteFile(attachid, path);
                        }
                        blFile.Delete(OpeId);
                    }
                    break;
                case "更新":
                    int i = Convert.ToInt32(ReId);
                        int attachidold = blFile.FindAttachmentID(OpeId);
                        int attachidnew = blFile.FindAttachmentID(i);
                    if (Agree)
                    {
                        blFile.UpdateIsPass(i, true);
                        if (attachidold > 0 && attachidnew != attachidold)
                        {
                            string path = blat.FindPath(attachidold);
                            pm.DeleteFile(attachidold, path);
                        }
                        blFile.Delete(OpeId);
                    }
                    else
                    {
                        blFile.UpdateIsPass(OpeId, true);
                        if (attachidnew > 0 && attachidnew != attachidold)
                        {
                            string path = blat.FindPath(attachidnew);
                            pm.DeleteFile(attachidnew, path);
                        }
                        blFile.Delete(i);
                    }
                    break;
            }
        }

        //FundInformation
        protected void FundInformation(string OpeType, bool Agree, int OpeId, string ReId)
        {
            BLLFundInformation blFund = new BLLFundInformation();
            switch (OpeType)
            {
                case "删除":
                    if (Agree)
                        blFund.Delete(OpeId);
                    else
                        blFund.UpdateIsPass(OpeId, true);
                    break;
                case "添加":
                    if (Agree)
                        blFund.UpdateIsPass(OpeId, true);
                    else
                        blFund.Delete(OpeId);
                    break;
                case "更新":
                    int i = Convert.ToInt32(ReId);
                    if (Agree)
                    {
                        blFund.UpdateIsPass(i, true);
                        blFund.Delete(OpeId);
                    }
                    else
                    {
                        blFund.UpdateIsPass(OpeId, true);
                        blFund.Delete(i);
                    }
                    break;
            }
        }

        //FutherStudy
        protected void FutherStudy(string OpeType, bool Agree, int OpeId, string ReId)
        {
            BLLFutherStudy blFur = new BLLFutherStudy();
            switch (OpeType)
            {
                case "删除":
                    if (Agree)
                        blFur.Delete(OpeId);
                    else
                        blFur.UpdateIsPass(OpeId, true);
                    break;
                case "添加":
                    if (Agree)
                        blFur.UpdateIsPass(OpeId, true);
                    else
                        blFur.Delete(OpeId);
                    break;
                case "更新":
                    int i = Convert.ToInt32(ReId);
                    if (Agree)
                    {
                        blFur.UpdateIsPass(i, true);
                        blFur.Delete(OpeId);
                    }
                    else
                    {
                        blFur.UpdateIsPass(OpeId, true);
                        blFur.Delete(i);
                    }
                    break;
            }
        }

        //Honor
        protected void Honor(string OpeType, bool Agree, int OpeId, string ReId)
        {
            BLLHonor blHo = new BLLHonor();
            switch (OpeType)
            {
                case "删除":
                    if (Agree)
                        blHo.Delete(OpeId);
                    else
                        blHo.UpdateIsPass(OpeId, true);
                    break;
                case "添加":
                    if (Agree)
                        blHo.UpdateIsPass(OpeId, true);
                    else
                        blHo.Delete(OpeId);
                    break;
                case "更新":
                    int i = Convert.ToInt32(ReId);
                    if (Agree)
                    {
                        blHo.UpdateIsPass(i, true);
                        blHo.Delete(OpeId);
                    }
                    else
                    {
                        blHo.UpdateIsPass(OpeId, true);
                        blHo.Delete(i);
                    }
                    break;
            }
        }

        //LibraryRecord
        protected void LibraryRecord(string OpeType, bool Agree, int OpeId, string ReId)
        {
            BLLLibraryRecord blLib = new BLLLibraryRecord();
            switch (OpeType)
            {
                case "删除":
                    if (Agree)
                        blLib.Delete(OpeId);
                    else
                        blLib.UpdateIsPass(OpeId, true);
                    break;
                case "添加":
                    if (Agree)
                        blLib.UpdateIsPass(OpeId, true);
                    else
                        blLib.Delete(OpeId);
                    break;
                case "更新":
                    int i = Convert.ToInt32(ReId);
                    if (Agree)
                    {
                        blLib.UpdateIsPass(i, true);
                        blLib.Delete(OpeId);
                    }
                    else
                    {
                        blLib.UpdateIsPass(OpeId, true);
                        blLib.Delete(i);
                    }
                    break;
            }
        }

        //Monograph
        protected void Monograph(string OpeType, bool Agree, int OpeId, string ReId)
        {
            BLLMonograph blMono = new BLLMonograph();
            switch (OpeType)
            {
                case "删除":
                    if (Agree)
                    {
                        int Battachid = blMono.FindBAttachmentID(OpeId);
                        if (Battachid > 0)
                        {
                            string path = blat.FindPath(Battachid);
                            pm.DeleteFile(Battachid, path);
                        }
                        int fattachid = blMono.FindFAttachmentID(OpeId);
                        if (fattachid > 0)
                        {
                            string path = blat.FindPath(fattachid);
                            pm.DeleteFile(fattachid, path);
                        }
                        blMono.Delete(OpeId);
                    }
                    else
                        blMono.UpdateIsPass(OpeId, true);
                    break;
                case "添加":
                    if (Agree)
                        blMono.UpdateIsPass(OpeId, true);
                    else
                    {
                        int Battachid = blMono.FindBAttachmentID(OpeId);
                        if (Battachid > 0)
                        {
                            string path = blat.FindPath(Battachid);
                            pm.DeleteFile(Battachid, path);
                        }
                        int fattachid = blMono.FindFAttachmentID(OpeId);
                        if (fattachid > 0)
                        {
                            string path = blat.FindPath(fattachid);
                            pm.DeleteFile(fattachid, path);
                        }
                        blMono.Delete(OpeId);
                    }
                    break;
                case "更新":
                    int i = Convert.ToInt32(ReId);
                    int Battachidold = blMono.FindBAttachmentID(OpeId);
                    int fattachidold = blMono.FindFAttachmentID(OpeId);
                    int Battachidnew = blMono.FindBAttachmentID(i);
                    int fattachidnew = blMono.FindFAttachmentID(i);
                    if (Agree)
                    {
                        blMono.UpdateIsPass(i, true);
                        if (Battachidold > 0 && Battachidnew != Battachidold)
                        {
                            string path = blat.FindPath(Battachidold);
                            pm.DeleteFile(Battachidold, path);
                        }
                        if (fattachidold > 0 && fattachidnew != fattachidold)
                        {
                            string path = blat.FindPath(fattachidold);
                            pm.DeleteFile(fattachidold, path);
                        }
                        blMono.Delete(OpeId);
                    }
                    else
                    {
                        blMono.UpdateIsPass(OpeId, true);
                        if (Battachidnew > 0 && Battachidnew != Battachidold)
                        {
                            string path = blat.FindPath(Battachidnew);
                            pm.DeleteFile(Battachidnew, path);
                        }
                        if (fattachidnew > 0 && fattachidnew != fattachidold)
                        {
                            string path = blat.FindPath(fattachidnew);
                            pm.DeleteFile(fattachidnew, path);
                        }
                        blMono.Delete(i);
                    }
                    break;
            }
        }

        //Pact
        protected void Pact(string OpeType, bool Agree, int OpeId, string ReId)
        {
            BLLPact blPact = new BLLPact();
            BLLLibraryRecord blrecord = new BLLLibraryRecord();
            switch (OpeType)
            {
                case "删除":
                    if (Agree)
                    {
                        int attachid = blPact.FindAttachmentID(OpeId);
                        if (attachid > 0)
                        {
                            string path = blat.FindPath(attachid);
                            pm.DeleteFile(attachid, path);
                        }
                        //删除该合同的借阅记录
                        List<int> listRecordID = blrecord.FindLibraryID(OpeId, "合同");
                        if (listRecordID != null)
                        {
                            for (int j = 0; j < listRecordID.Count(); j++)
                                blrecord.Delete(listRecordID[j]);
                        }
                        blPact.Delete(OpeId);
                    }
                    else
                        blPact.UpdateIsPass(OpeId, true);
                    break;
                case "添加":
                    if (Agree)
                        blPact.UpdateIsPass(OpeId, true);
                    else
                    {
                        int attachid = blPact.FindAttachmentID(OpeId);
                        if (attachid > 0)
                        {
                            string path = blat.FindPath(attachid);
                            pm.DeleteFile(attachid, path);
                        }
                        //删除该合同的借阅记录
                        List<int> listRecordID = blrecord.FindLibraryID(OpeId, "合同");
                        if (listRecordID != null)
                        {
                            for (int j = 0; j < listRecordID.Count(); j++)
                                blrecord.Delete(listRecordID[j]);
                        }
                        blPact.Delete(OpeId);
                    }
                    break;
                case "更新":
                    int i = Convert.ToInt32(ReId);
                    int attachidold = blPact.FindAttachmentID(OpeId);
                    int attachidnew = blPact.FindAttachmentID(i);
                    if (Agree)
                    {
                        blPact.UpdateIsPass(i, true);
                        if (attachidold > 0 && attachidold != attachidnew)
                        {
                            string path = blat.FindPath(attachidold);
                            pm.DeleteFile(attachidold, path);
                        }
                        //删除该合同的借阅记录
                        List<int> listRecordID = blrecord.FindLibraryID(OpeId, "合同");
                        if (listRecordID != null)
                        {
                            for (int j = 0; j < listRecordID.Count(); j++)
                                blrecord.Delete(listRecordID[j]);
                        }
                        blPact.Delete(OpeId);
                    }
                    else
                    {
                        blPact.UpdateIsPass(OpeId, true);
                        if (attachidnew > 0 && attachidold != attachidnew)
                        {
                            string path = blat.FindPath(attachidnew);
                            pm.DeleteFile(attachidnew, path);
                        }
                        //删除该合同的借阅记录
                        List<int> listRecordID = blrecord.FindLibraryID(i, "合同");
                        if (listRecordID != null)
                        {
                            for (int j = 0; j < listRecordID.Count(); j++)
                                blrecord.Delete(listRecordID[j]);
                        }
                        blPact.Delete(i);
                    }
                    break;
            }
        }

        //Paper
        protected void Paper(string OpeType, bool Agree, int OpeId, string ReId)
        {
            BLLPaper blPaper = new BLLPaper();
            switch (OpeType)
            {
                case "删除":
                    if (Agree)
                        blPaper.Delete(OpeId);
                    else
                        blPaper.UpdateIsPass(OpeId, true);
                    break;
                case "添加":
                    if (Agree)
                        blPaper.UpdateIsPass(OpeId, true);
                    else
                        blPaper.Delete(OpeId);
                    break;
                case "更新":
                    int i = Convert.ToInt32(ReId);
                    if (Agree)
                    {
                        blPaper.UpdateIsPass(i, true);
                        blPaper.Delete(OpeId);
                    }
                    else
                    {
                        blPaper.UpdateIsPass(OpeId, true);
                        blPaper.Delete(i);
                    }
                    break;
            }
        }

        //Patent
        protected void Patent(string OpeType, bool Agree, int OpeId, string ReId)
        {
            BLLPatent blPatent = new BLLPatent();
            switch (OpeType)
            {
                case "删除":
                    if (Agree)
                    {
                        //lby ↓
                        int[] attachid = blPatent.FindAttachmentID(OpeId);
                        if (attachid[0] > 0)
                        {
                            string path = blat.FindPath(attachid[0]);
                            pm.DeleteFile(attachid[0], path);
                        }
                        if (attachid[1] > 0)
                        {
                            string path = blat.FindPath(attachid[1]);
                            pm.DeleteFile(attachid[1], path);
                        }

                        blPatent.Delete(OpeId);
                    }
                    else
                        blPatent.UpdateIsPass(OpeId, true);
                    break;
                case "添加":
                    if (Agree)
                        blPatent.UpdateIsPass(OpeId, true);
                    else
                    {
                        //lby ↓
                        int[] attachid = blPatent.FindAttachmentID(OpeId);
                        if (attachid[0] > 0)
                        {
                            string path = blat.FindPath(attachid[0]);
                            pm.DeleteFile(attachid[0], path);
                        }
                        if (attachid[1] > 0)
                        {
                            string path = blat.FindPath(attachid[1]);
                            pm.DeleteFile(attachid[1], path);
                        }

                        blPatent.Delete(OpeId);
                    }
                    break;
                case "更新":
                    int i = Convert.ToInt32(ReId);
                    int[] attachidold = blPatent.FindAttachmentID(OpeId);
                    int[] attachidnew = blPatent.FindAttachmentID(i);
                    if (Agree)
                    {
                        //lby ↓
                        blPatent.UpdateIsPass(i, true);
                        if (attachidold[0] > 0 && attachidnew != attachidold)
                        {
                            string path = blat.FindPath(attachidold[0]);
                            pm.DeleteFile(attachidold[0], path);
                        }
                        if (attachidold[1] > 0 && attachidnew != attachidold)
                        {
                            string path = blat.FindPath(attachidold[1]);
                            pm.DeleteFile(attachidold[1], path);
                        }

                        blPatent.Delete(OpeId);
                    }
                    else
                    {
                        //lby ↓
                        blPatent.UpdateIsPass(OpeId, true);
                        if (attachidnew[0] > 0 && attachidnew != attachidold)
                        {
                            string path = blat.FindPath(attachidnew[0]);
                            pm.DeleteFile(attachidnew[0], path);
                        }
                        if (attachidnew[0] > 0 && attachidnew != attachidold)
                        {
                            string path = blat.FindPath(attachidnew[0]);
                            pm.DeleteFile(attachidnew[0], path);
                        }

                        blPatent.Delete(i);
                    }
                    break;
            }
        }

        //Photos
        protected void Photos(string OpeType, bool Agree, int OpeId, string ReId)
        {
            BLLPhotos blPhotos = new BLLPhotos();
            switch (OpeType)
            {
                case "删除":
                    if (Agree)
                    {
                        int attachid = blPhotos.FindAttachmentID(OpeId);
                        if (attachid > 0)
                        {
                            string path = blat.FindPath(attachid);
                            pm.DeleteFile(attachid, path);
                        }
                        blPhotos.Delete(OpeId);
                    }
                    else
                        blPhotos.UpdateIsPass(OpeId, true);
                    break;
                case "添加":
                    if (Agree)
                        blPhotos.UpdateIsPass(OpeId, true);
                    else
                    {
                        int attachid = blPhotos.FindAttachmentID(OpeId);
                        if (attachid > 0)
                        {
                            string path = blat.FindPath(attachid);
                            pm.DeleteFile(attachid, path);
                        }
                        blPhotos.Delete(OpeId);
                    }
                    break;
                case "更新":
                    int i = Convert.ToInt32(ReId);
                    int attachidold = blPhotos.FindAttachmentID(OpeId);
                    int attachidnew = blPhotos.FindAttachmentID(i);
                    if (Agree)
                    {
                        blPhotos.UpdateIsPass(i, true);
                        if (attachidold > 0 && attachidold != attachidnew)
                        {
                            string path = blat.FindPath(attachidold);
                            pm.DeleteFile(attachidold, path);
                        }
                        blPhotos.Delete(OpeId);
                    }
                    else
                    {
                        blPhotos.UpdateIsPass(OpeId, true);
                        if (attachidnew > 0 && attachidold != attachidnew)
                        {
                            string path = blat.FindPath(attachidnew);
                            pm.DeleteFile(attachidnew, path);
                        }
                        blPhotos.Delete(i);
                    }
                    break;
            }
        }

        //Project
        protected void Project(string OpeType, bool Agree, int OpeId, string ReId)
        {
            BLLProject blProject = new BLLProject();
            BLHelper.BLLStaffDevote bllDevote = new BLHelper.BLLStaffDevote();
            BLHelper.BLLProjectImportantNode bllImportant = new BLHelper.BLLProjectImportantNode();
            BLHelper.BLLFundInformation bllFund = new BLHelper.BLLFundInformation();
            switch (OpeType)
            {
                case "删除":
                    if (Agree)
                    {
                        int benefitid = blProject.FindBenefit(OpeId);
                        if (benefitid > 0)
                        {
                            string path = blat.FindPath(benefitid);
                            pm.DeleteFile(benefitid, path);
                        }
                        int budgetId = blProject.FindBudget(OpeId);
                        if (budgetId > 0)
                        {
                            string path = blat.FindPath(budgetId);
                            pm.DeleteFile(budgetId, path);
                        }
                        //删除项目相关人员投入表的信息
                        List<int> staffslist = bllDevote.FindStaffDevoteIDList(OpeId);
                        for (int j = 0; j < staffslist.Count(); j++)
                        {
                            bllDevote.Delete(staffslist[j]);
                        }
                        //删除项目相关重大节点表的信息
                        List<int> Importantlist = bllImportant.FindImprotIDList(OpeId);
                        for (int n = 0; n < Importantlist.Count(); n++)
                        {
                            bllImportant.Delete(Importantlist[n]);
                        }
                        //删除项目相关经费基本表的信息
                        List<int> Fundlist = bllFund.FindFundIDlist(OpeId);
                        for (int m = 0; m < Fundlist.Count(); m++)
                        {
                            bllFund.Delete(Fundlist[m]);
                        }
                        //lby ↓ 删除合同
                        BLHelper.BLLPact bllPact = new BLHelper.BLLPact();
                        List<int> Pactlist = bllPact.FindPactIDList(OpeId);
                        for (int m = 0; m < Pactlist.Count(); m++)
                        {
                            bllPact.Delete(Pactlist[m]);
                        }
                        //lby ↓ 删除相关文档
                        BLHelper.BLLProjectFile bllProjectFile = new BLHelper.BLLProjectFile();
                        List<int> fileList = bllProjectFile.FindProjectFileID(OpeId, 5);
                        if (fileList != null)
                        {
                            int attachid;
                            foreach (int id in fileList)
                            {
                                attachid = bllProjectFile.Delete(id);
                                pm.DeleteFile(attachid, blat.FindPath(attachid));
                            }
                        }
                        blProject.Delete(OpeId);
                    }
                    else
                        blProject.ChangePass(OpeId, true);
                    break;
                case "添加":
                    if (Agree)
                        blProject.ChangePass(OpeId, true);
                    else
                    {
                        int benefitid = blProject.FindBenefit(OpeId);
                        if (benefitid > 0)
                        {
                            string path = blat.FindPath(benefitid);
                            pm.DeleteFile(benefitid, path);
                        }
                        int budgetId = blProject.FindBudget(OpeId);
                        if (budgetId > 0)
                        {
                            string path = blat.FindPath(budgetId);
                            pm.DeleteFile(budgetId, path);
                        }
                        blProject.Delete(OpeId);
                    }
                    break;
                case "更新":
                    int i = Convert.ToInt32(ReId);
                    int benefitidold = blProject.FindBenefit(OpeId);
                    int budgetIdold = blProject.FindBudget(OpeId);
                    int benefitidnew = blProject.FindBenefit(i);
                    int budgetIdnew = blProject.FindBudget(i);
                    if (Agree)
                    {
                        blProject.ChangePass(i, true);
                        if (benefitidold > 0 && benefitidnew != benefitidold)
                        {
                            string path = blat.FindPath(benefitidold);
                            pm.DeleteFile(benefitidold, path);
                        }
                        if (budgetIdold > 0 && budgetIdnew != budgetIdold)
                        {
                            string path = blat.FindPath(budgetIdold);
                            pm.DeleteFile(budgetIdold, path);
                        }
                        blProject.Delete(OpeId);
                    }
                    else
                    {
                        blProject.ChangePass(OpeId, true);
                        if (benefitidnew > 0 && benefitidnew != benefitidold)
                        {
                            string path = blat.FindPath(benefitidnew);
                            pm.DeleteFile(benefitidnew, path);
                        }
                        if (budgetIdnew > 0 && budgetIdnew != budgetIdold)
                        {
                            string path = blat.FindPath(budgetIdnew);
                            pm.DeleteFile(budgetIdnew, path);
                        }
                        blProject.Delete(i);
                    }
                    break;
            }
        }

        //ProjectImportantNode
        protected void ProjectImportantNode(string OpeType, bool Agree, int OpeId, string ReId)
        {
            BLLProjectImportantNode blProNode = new BLLProjectImportantNode();
            switch (OpeType)
            {
                case "删除":
                    if (Agree)
                        blProNode.Delete(OpeId);
                    else
                        blProNode.ChangePass(OpeId, true);
                    break;
                case "添加":
                    if (Agree)
                        blProNode.ChangePass(OpeId, true);
                    else
                        blProNode.Delete(OpeId);
                    break;
                case "更新":
                    int i = Convert.ToInt32(ReId);
                    if (Agree)
                    {
                        blProNode.ChangePass(i, true);
                        blProNode.Delete(OpeId);
                    }
                    else
                    {
                        blProNode.ChangePass(OpeId, true);
                        blProNode.Delete(i);
                    }
                    break;
            }
        }

        //ScienceReport
        protected void ScienceReport(string OpeType, bool Agree, int OpeId, string ReId)
        {
            BLLScienceReport blSci = new BLLScienceReport();
            switch (OpeType)
            {
                case "删除":
                    if (Agree)
                    {
                        int attachid = blSci.FindAttachmentid(OpeId);
                        if (attachid > 0)
                        {
                            string path = blat.FindPath(attachid);
                            pm.DeleteFile(attachid, path);
                        }
                        blSci.Delete(OpeId);
                    }
                    else
                        blSci.ChangePass(OpeId, true);
                    break;
                case "添加":
                    if (Agree)
                        blSci.ChangePass(OpeId, true);
                    else
                    {
                        int attachid = blSci.FindAttachmentid(OpeId);
                        if (attachid > 0)
                        {
                            string path = blat.FindPath(attachid);
                            pm.DeleteFile(attachid, path);
                        }
                        blSci.Delete(OpeId);
                    }
                    break;
                case "更新":
                    int i = Convert.ToInt32(ReId);
                    int attachidold = blSci.FindAttachmentid(OpeId);
                    int attachidnew = blSci.FindAttachmentid(i);
                    if (Agree)
                    {
                        blSci.ChangePass(i, true);
                        if (attachidold > 0 && attachidnew != attachidold)
                        {
                            string path = blat.FindPath(attachidold);
                            pm.DeleteFile(attachidold, path);
                        }
                        blSci.Delete(OpeId);
                    }
                    else
                    {
                        blSci.ChangePass(OpeId, true);
                        if (attachidnew > 0 && attachidnew != attachidold)
                        {
                            string path = blat.FindPath(attachidnew);
                            pm.DeleteFile(attachidnew, path);
                        }
                        blSci.Delete(i);
                    }
                    break;
            }
        }

        //SocialPartTime
        protected void SocialPartTime(string OpeType, bool Agree, int OpeId, string ReId)
        {
            BLLSocialPartTime blSoc = new BLLSocialPartTime();
            switch (OpeType)
            {
                case "删除":
                    if (Agree)
                        blSoc.Delete(OpeId);
                    else
                        blSoc.UpdateIsPass(OpeId, true);
                    break;
                case "添加":
                    if (Agree)
                        blSoc.UpdateIsPass(OpeId, true);
                    else
                        blSoc.Delete(OpeId);
                    break;
                case "更新":
                    int i = Convert.ToInt32(ReId);
                    if (Agree)
                    {
                        blSoc.UpdateIsPass(i, true);
                        blSoc.Delete(OpeId);
                    }
                    else
                    {
                        blSoc.UpdateIsPass(OpeId, true);
                        blSoc.Delete(i);
                    }
                    break;
            }
        }

        //SpeakClass
        protected void SpeakClass(string OpeType, bool Agree, int OpeId, string ReId)
        {
            BLLSpeakClass blSpe = new BLLSpeakClass();
            switch (OpeType)
            {
                case "删除":
                    if (Agree)
                        blSpe.Delete(OpeId);
                    else
                        blSpe.UpdateIsPass(OpeId, true);
                    break;
                case "添加":
                    if (Agree)
                        blSpe.UpdateIsPass(OpeId, true);
                    else
                        blSpe.Delete(OpeId);
                    break;
                case "更新":
                    int i = Convert.ToInt32(ReId);
                    if (Agree)
                    {
                        blSpe.UpdateIsPass(i, true);
                        blSpe.Delete(OpeId);
                    }
                    else
                    {
                        blSpe.UpdateIsPass(OpeId, true);
                        blSpe.Delete(i);
                    }
                    break;
            }
        }

        //StaffAchieve
        //protected void StaffAchieve(string OpeType, bool Agree, int OpeId, string ReId)
        //{
        //    BLLStaffAchieve blSta = new BLLStaffAchieve();
        //    switch (OpeType)
        //    {
        //        case "删除":
        //            if (Agree)
        //                blSta.Delete(OpeId);
        //            else
        //                blSta.UpdateIsPass(OpeId, true);
        //            break;
        //        case "添加":
        //            if (Agree)
        //                blSta.UpdateIsPass(OpeId, true);
        //            else
        //                blSta.Delete(OpeId);
        //            break;
        //        case "更新":
        //            int i = Convert.ToInt32(ReId);
        //            if (Agree)
        //            {
        //                blSta.UpdateIsPass(i, true);
        //                blSta.Delete(OpeId);
        //            }
        //            else
        //            {
        //                blSta.UpdateIsPass(OpeId, true);
        //                blSta.Delete(i);
        //            }
        //            break;
        //    }
        //}

        ////StaffAward
        //protected void StaffAward(string OpeType, bool Agree, int OpeId, string ReId)
        //{
        //    BLLStaffAward blSta = new BLLStaffAward();
        //    switch (OpeType)
        //    {
        //        case "删除":
        //            if (Agree)
        //                blSta.Delete(OpeId);
        //            else
        //                blSta.UpdateIsPass(OpeId, true);
        //            break;
        //        case "添加":
        //            if (Agree)
        //                blSta.UpdateIsPass(OpeId, true);
        //            else
        //                blSta.Delete(OpeId);
        //            break;
        //        case "更新":
        //            int i = Convert.ToInt32(ReId);
        //            if (Agree)
        //            {
        //                blSta.UpdateIsPass(i, true);
        //                blSta.Delete(OpeId);
        //            }
        //            else
        //            {
        //                blSta.UpdateIsPass(OpeId, true);
        //                blSta.Delete(i);
        //            }
        //            break;
        //    }
        //}

        //StaffDevote
        protected void StaffDevote(string OpeType, bool Agree, int OpeId, string ReId)
        {
            BLLStaffDevote blSta = new BLLStaffDevote();
            switch (OpeType)
            {
                case "删除":
                    if (Agree)
                        blSta.Delete(OpeId);
                    else
                        blSta.UpdateIsPass(OpeId, true);
                    break;
                case "添加":
                    if (Agree)
                        blSta.UpdateIsPass(OpeId, true);
                    else
                        blSta.Delete(OpeId);
                    break;
                case "更新":
                    int i = Convert.ToInt32(ReId);
                    if (Agree)
                    {
                        blSta.UpdateIsPass(i, true);
                        blSta.Delete(OpeId);
                    }
                    else
                    {
                        blSta.UpdateIsPass(OpeId, true);
                        blSta.Delete(i);
                    }
                    break;
            }
        }

        ////StaffMonograph
        //protected void StaffMonograph(string OpeType, bool Agree, int OpeId, string ReId)
        //{
        //    BLLStaffMonograph blSta = new BLLStaffMonograph();
        //    switch (OpeType)
        //    {
        //        case "删除":
        //            if (Agree)
        //                blSta.Delete(OpeId);
        //            else
        //                blSta.UpdateIsPass(OpeId, true);
        //            break;
        //        case "添加":
        //            if (Agree)
        //                blSta.UpdateIsPass(OpeId, true);
        //            else
        //                blSta.Delete(OpeId);
        //            break;
        //        case "更新":
        //            int i = Convert.ToInt32(ReId);
        //            if (Agree)
        //            {
        //                blSta.UpdateIsPass(i, true);
        //                blSta.Delete(OpeId);
        //            }
        //            else
        //            {
        //                blSta.UpdateIsPass(OpeId, true);
        //                blSta.Delete(i);
        //            }
        //            break;
        //    }
        //}

        ////StaffPaper
        //protected void StaffPaper(string OpeType, bool Agree, int OpeId, string ReId)
        //{
        //    BLLStaffPaper blSta = new BLLStaffPaper();
        //    switch (OpeType)
        //    {
        //        case "删除":
        //            if (Agree)
        //                blSta.Delete(OpeId);
        //            else
        //                blSta.UpdateIsPass(OpeId, true);
        //            break;
        //        case "添加":
        //            if (Agree)
        //                blSta.UpdateIsPass(OpeId, true);
        //            else
        //                blSta.Delete(OpeId);
        //            break;
        //        case "更新":
        //            int i = Convert.ToInt32(ReId);
        //            if (Agree)
        //            {
        //                blSta.UpdateIsPass(i, true);
        //                blSta.Delete(OpeId);
        //            }
        //            else
        //            {
        //                blSta.UpdateIsPass(OpeId, true);
        //                blSta.Delete(i);
        //            }
        //            break;
        //    }
        //}

        ////StaffPatent
        //protected void StaffPatent(string OpeType, bool Agree, int OpeId, string ReId)
        //{
        //    BLLStaffPatent blSta = new BLLStaffPatent();
        //    switch (OpeType)
        //    {
        //        case "删除":
        //            if (Agree)
        //                blSta.Delete(OpeId);
        //            else
        //                blSta.UpdateIsPass(OpeId, true);
        //            break;
        //        case "添加":
        //            if (Agree)
        //                blSta.UpdateIsPass(OpeId, true);
        //            else
        //                blSta.Delete(OpeId);
        //            break;
        //        case "更新":
        //            int i = Convert.ToInt32(ReId);
        //            if (Agree)
        //            {
        //                blSta.UpdateIsPass(i, true);
        //                blSta.Delete(OpeId);
        //            }
        //            else
        //            {
        //                blSta.UpdateIsPass(OpeId, true);
        //                blSta.Delete(i);
        //            }
        //            break;
        //    }
        //}

        //Student
        protected void Student(string OpeType, bool Agree, int OpeId, string ReId)
        {
            BLLStudent blStu = new BLLStudent();
            switch (OpeType)
            {
                case "删除":
                    if (Agree)
                        blStu.Delete(OpeId);
                    else
                        blStu.UpdateIsPass(OpeId, true);
                    break;
                case "添加":
                    if (Agree)
                        blStu.UpdateIsPass(OpeId, true);
                    else
                        blStu.Delete(OpeId);
                    break;
                case "更新":
                    int i = Convert.ToInt32(ReId);
                    if (Agree)
                    {
                        blStu.UpdateIsPass(i, true);
                        blStu.Delete(OpeId);
                    }
                    else
                    {
                        blStu.UpdateIsPass(OpeId, true);
                        blStu.Delete(i);
                    }
                    break;
            }
        }

        ////TrainExperience
        //protected void TrainExperience(string OpeType, bool Agree, int OpeId, string ReId)
        //{
        //    BLLTrainExperience blTra = new BLLTrainExperience();
        //    switch (OpeType)
        //    {
        //        case "删除":
        //            if (Agree)
        //                blTra.Delete(OpeId);
        //            else
        //                blTra.UpdateIsPass(OpeId, true);
        //            break;
        //        case "添加":
        //            if (Agree)
        //                blTra.UpdateIsPass(OpeId, true);
        //            else
        //                blTra.Delete(OpeId);
        //            break;
        //        case "更新":
        //            int i = Convert.ToInt32(ReId);
        //            if (Agree)
        //            {
        //                blTra.UpdateIsPass(i, true);
        //                blTra.Delete(OpeId);
        //            }
        //            else
        //            {
        //                blTra.UpdateIsPass(OpeId, true);
        //                blTra.Delete(i);
        //            }
        //            break;
        //    }
        //}

        //UnitInspect
        protected void UnitInspect(string OpeType, bool Agree, int OpeId, string ReId)
        {
            BLLUnitInspect blUnit = new BLLUnitInspect();
            switch (OpeType)
            {
                case "删除":
                    if (Agree)
                    {
                        int attachid = blUnit.FindAttachmentID(OpeId);
                        if (attachid > 0)
                        {
                            string path = blat.FindPath(attachid);
                            pm.DeleteFile(attachid, path);
                        }
                        blUnit.Delete(OpeId);
                    }
                    else
                        blUnit.ChangePass(OpeId, true);
                    break;
                case "添加":
                    if (Agree)
                        blUnit.ChangePass(OpeId, true);
                    else
                    {
                        int attachid = blUnit.FindAttachmentID(OpeId);
                        if (attachid > 0)
                        {
                            string path = blat.FindPath(attachid);
                            pm.DeleteFile(attachid, path);
                        }
                        blUnit.Delete(OpeId);
                    }
                    break;
                case "更新":
                    int i = Convert.ToInt32(ReId);
                    int attachidold = blUnit.FindAttachmentID(OpeId);
                    int attachidnew = blUnit.FindAttachmentID(i);
                    if (Agree)
                    {
                        blUnit.ChangePass(i, true);
                        if (attachidold > 0 && attachidold != attachidnew)
                        {
                            string path = blat.FindPath(attachidold);
                            pm.DeleteFile(attachidold, path);
                        }
                        blUnit.Delete(OpeId);
                    }
                    else
                    {
                        blUnit.ChangePass(OpeId, true);
                        if (attachidnew > 0 && attachidold != attachidnew)
                        {
                            string path = blat.FindPath(attachidnew);
                            pm.DeleteFile(attachidnew, path);
                        }
                        blUnit.Delete(i);
                    }
                    break;
            }
        }

        //UnitLectures
        protected void UnitLectures(string OpeType, bool Agree, int OpeId, string ReId)
        {//OpeId原数据Id ReId更新数据Id
            BLLUnitLectures blUnit = new BLLUnitLectures();
            switch (OpeType)
            {
                case "删除":
                    if (Agree)
                    {
                        int attachid = blUnit.FindAttachmentid(OpeId);
                        if (attachid > 0)
                        {
                            string path = blat.FindPath(attachid);
                            pm.DeleteFile(attachid, path);
                        }
                        blUnit.Delete(OpeId);
                    }
                    else
                        blUnit.ChangePass(OpeId, true);
                    break;
                case "添加":
                    if (Agree)
                        blUnit.ChangePass(OpeId, true);
                    else
                    {
                        int attachid = blUnit.FindAttachmentid(OpeId);
                        if (attachid > 0)
                        {
                            string path = blat.FindPath(attachid);
                            pm.DeleteFile(attachid, path);
                        }
                        blUnit.Delete(OpeId);
                    }
                    break;
                case "更新":
                    int i = Convert.ToInt32(ReId);
                    int attachidold = blUnit.FindAttachmentid(OpeId);
                    int attachidnew = blUnit.FindAttachmentid(i);
                    if (Agree)
                    {
                        blUnit.ChangePass(i, true);
                        if (attachidold > 0 && attachidold!=attachidnew)
                        {
                            string path = blat.FindPath(attachidold);
                            pm.DeleteFile(attachidold, path);
                        }
                        blUnit.Delete(OpeId);
                    }
                    else
                    {
                        blUnit.ChangePass(OpeId, true);
                        if (attachidnew > 0 && attachidnew != attachidold)
                        {
                            string path = blat.FindPath(attachidnew);
                            pm.DeleteFile(attachidnew, path);
                        }
                        blUnit.Delete(i);
                    }
                    break;
            }
        }

        //User
        protected void Users(string OpeType, bool Agree, int OpeId, string ReId)
        {
            BLLUser blUser = new BLLUser();
            switch (OpeType)
            {
                case "删除":
                    if (Agree)
                    {
                        BLHelper.BLLStudent bllStudent = new BLLStudent();
                        BLHelper.BLLSocialPartTime bllSocialPartTime = new BLLSocialPartTime();
                        BLHelper.BLLSpeakClass bllSpeakClass = new BLLSpeakClass();
                        BLHelper.BLLWorkExperience bllWork = new BLLWorkExperience();
                        BLHelper.BLLPhotos bllPhotos = new BLLPhotos();
                        BLHelper.BLLAttachment bllAttachment = new BLLAttachment();
                        BLHelper.BLLEducation bllEducation = new BLLEducation();
                        BLHelper.BLLEduExperience bllEduE = new BLLEduExperience();
                        BLHelper.BLLHonor bllHonor = new BLLHonor();
                        //删除人员相关学生信息
                        List<int> Studentlist = bllStudent.FindStudentIDList(OpeId);
                        for (int j = 0; j < Studentlist.Count(); j++)
                        {
                            bllStudent.Delete(Convert.ToInt32(Studentlist[j]));
                        }
                        //删除人员相关社会兼职信息
                        List<int> SocialPartTime = bllSocialPartTime.FindSocialPartTimeIDList(OpeId);
                        for (int j = 0; j < SocialPartTime.Count(); j++)
                        {
                            bllSocialPartTime.Delete(SocialPartTime[j]);
                        }
                        //删除人员相关主讲课程信息
                        List<int> SpeakClass = bllSpeakClass.FindSpeakClassIDList(OpeId);
                        for (int j = 0; j < SpeakClass.Count(); j++)
                        {
                            bllSpeakClass.Delete(SpeakClass[j]);
                        }
                        //删除人员相关工作经历信息
                        List<int> Work = bllWork.FindWorkExperienceIDList(OpeId);
                        for (int j = 0; j < Work.Count(); j++)
                        {
                            bllWork.Delete(Work[j]);
                        }
                        //删除人员相关照片信息
                        int PhotoID = bllPhotos.FindPhotoID(OpeId);
                        if (PhotoID != 0)
                        {
                            int AttachmentID = bllPhotos.FindAttachmentID(PhotoID);
                            string path = bllAttachment.FindPath(AttachmentID);
                            pm.DeleteFile(AttachmentID, path);
                            bllPhotos.Delete(PhotoID);
                        }
                        //删除人员相关学历信息
                        List<int> Education = bllEducation.FindEducationIDList(OpeId);
                        for (int j = 0; j < Education.Count(); j++)
                        {
                            bllEducation.Delete(Education[j]);
                        }
                        //删除人员相关教育经历信息
                        List<int> EduE = bllEduE.FindEduExperienceIDList(OpeId);
                        for (int j = 0; j < EduE.Count(); j++)
                        {
                            bllEduE.Delete(EduE[j]);
                        }
                        //删除人员相关荣誉称号信息
                        List<int> Honor = bllHonor.FindHonorIDList(OpeId);
                        for (int j = 0; j < Honor.Count(); j++)
                        {
                            bllHonor.Delete(Honor[j]);
                        }
                        //删除人员相关成果获奖信息
                        string UserName = blUser.Find(OpeId, true).UserName;
                        BLHelper.BLLAward bllAward = new BLLAward();
                        List<string> Awardlist = bllAward.FindAwardPeopleList();
                        List<int> AwardIDlists = new List<int>();
                        for (int n = 0; n < Awardlist.Count(); n++)
                        {
                            if (UserName == Awardlist[n])
                            {
                                for (int m = 0; m < bllAward.FindAwardIDList(Awardlist[n]).Count(); m++)
                                {
                                    AwardIDlists.Add((bllAward.FindAwardIDList(Awardlist[n]))[m]);
                                }
                            }
                        }
                        for (int k = 0; k < AwardIDlists.Count(); k++)
                        {
                            bllAward.Delete(AwardIDlists[k]);
                        }
                        //删除人员相关成果报奖信息
                        BLHelper.BLLAchieveAward bllAchieveAward = new BLLAchieveAward();
                        List<string> AchieveAwardlist = bllAchieveAward.FindAwardPeopleList();
                        List<int> AchieveAwardID = new List<int>();
                        for (int n = 0; n < AchieveAwardlist.Count(); n++)
                        {
                            if (UserName == AchieveAwardlist[n])
                            {
                                for (int m = 0; m < bllAchieveAward.FindAwardIDList(AchieveAwardlist[n]).Count(); m++)
                                {
                                    AchieveAwardID.Add((bllAchieveAward.FindAwardIDList(AchieveAwardlist[n]))[m]);
                                }
                            }
                        }
                        for (int k = 0; k < AchieveAwardID.Count(); k++)
                        {
                            bllAchieveAward.Delete(AchieveAwardID[k]);
                        }
                        //删除人员相关成果鉴定信息①②
                        BLHelper.BLLAchievement bllAchivement = new BLLAchievement();
                        BLHelper.BLLStaffAchieve bllStaffAchieve = new BLLStaffAchieve();
                        List<int> AchivementIDlist = bllStaffAchieve.FindByUserID(OpeId);
                        List<int> StaffAchievelist = bllStaffAchieve.FindByStaffAchiveID(OpeId);
                        //②删除只有该人员一人为完成人的人员成果表信息 
                        for (int n = 0; n < StaffAchievelist.Count(); n++)
                        {
                            bllStaffAchieve.Delete(StaffAchievelist[n]);
                        }
                        //①删除只有该人员一人为完成人的成果鉴定
                        for (int j = 0; j < AchivementIDlist.Count(); j++)
                        {
                            bllAchivement.Delete(AchivementIDlist[j]);
                        }

                        //删除人员相关专利信息
                        BLHelper.BLLPatent bllPatent = new BLLPatent();
                        List<string> Patentlist = bllPatent.FindPatentPeopleList();
                        List<int> PatentID = new List<int>();
                        for (int n = 0; n < Patentlist.Count(); n++)
                        {
                            if (UserName == Patentlist[n])
                            {
                                for (int m = 0; m < bllPatent.FindPatentIDList(Patentlist[n]).Count(); m++)
                                {
                                    PatentID.Add((bllPatent.FindPatentIDList(Patentlist[n]))[m]);
                                }
                            }
                        }
                        for (int k = 0; k < PatentID.Count(); k++)
                        {
                            bllPatent.Delete(PatentID[k]);
                        }
                        //删除人员相关专著信息
                        BLHelper.BLLMonograph bllMonograph = new BLLMonograph();
                        List<string> Monographlist = bllMonograph.FindMonographPeopleList();
                        List<int> MonographID = new List<int>();
                        for (int n = 0; n < Monographlist.Count(); n++)
                        {
                            if (UserName == Monographlist[n])
                            {
                                for (int m = 0; m < bllMonograph.FindMonographIDList(Monographlist[n]).Count(); m++)
                                {
                                    MonographID.Add((bllMonograph.FindMonographIDList(Monographlist[n]))[m]);
                                }
                            }
                        }
                        for (int k = 0; k < MonographID.Count(); k++)
                        {
                            bllMonograph.Delete(MonographID[k]);
                        }
                        //删除人员相关论文信息
                        BLHelper.BLLPaper bllPaper = new BLLPaper();
                        List<string> Paperlist = bllPaper.FindPaperPeopleList();
                        List<int> PaperID = new List<int>();
                        for (int n = 0; n < Paperlist.Count(); n++)
                        {
                            if (UserName == Paperlist[n])
                            {
                                for (int m = 0; m < bllPaper.FindPaperIDList(Paperlist[n]).Count(); m++)
                                {
                                    PaperID.Add((bllPaper.FindPaperIDList(Paperlist[n]))[m]);
                                }
                            }
                        }
                        for (int k = 0; k < PaperID.Count(); k++)
                        {
                            bllPaper.Delete(PaperID[k]);
                        }
                        //删除人员
                        //bllUser.Delete(Convert.ToInt32(People_Info.DataKeys[selections[i]][0]));
                        blUser.Delete(OpeId);
                    }
                    else
                        blUser.ChangePass(OpeId, true);
                    break;
                case "添加":
                    if (Agree)
                        blUser.ChangePass(OpeId, true);
                    else
                        blUser.Delete(OpeId);
                    break;
                case "更新":
                    int i = Convert.ToInt32(ReId);
                    if (Agree)
                    {
                        blUser.ChangePass(i, true);
                        blUser.Delete(OpeId);
                    }
                    else
                    {
                        blUser.ChangePass(OpeId, true);
                        blUser.Delete(i);
                    }
                    break;
            }
        }

        //WorkExperience
        protected void WorkExperience(string OpeType, bool Agree, int OpeId, string ReId)
        {
            BLLWorkExperience blWor = new BLLWorkExperience();
            switch (OpeType)
            {
                case "删除":
                    if (Agree)
                        blWor.Delete(OpeId);
                    else
                        blWor.UpdateIsPass(OpeId, true);
                    break;
                case "添加":
                    if (Agree)
                        blWor.UpdateIsPass(OpeId, true);
                    else
                        blWor.Delete(OpeId);
                    break;
                case "更新":
                    int i = Convert.ToInt32(ReId);
                    if (Agree)
                    {
                        blWor.UpdateIsPass(i, true);
                        blWor.Delete(OpeId);
                    }
                    else
                    {
                        blWor.UpdateIsPass(OpeId, true);
                        blWor.Delete(i);
                    }
                    break;
            }
        }

        //WorkPlanSummary
        protected void WorkPlanSummary(string OpeType, bool Agree, int OpeId, string ReId)
        {
            BLLWorkPlanSummary blWor = new BLLWorkPlanSummary();
            switch (OpeType)
            {
                case "删除":
                    if (Agree)
                    {
                        int attachid = blWor.FindAttachmentid(OpeId);
                        if (attachid > 0)
                        {
                            string path = blat.FindPath(attachid);
                            pm.DeleteFile(attachid, path);
                        }
                        blWor.Delete(OpeId);
                    }
                    else
                        blWor.UpdateIsPass(OpeId, true);
                    break;
                case "添加":
                    if (Agree)
                        blWor.UpdateIsPass(OpeId, true);
                    else
                    {
                        int attachid = blWor.FindAttachmentid(OpeId);
                        if (attachid > 0)
                        {
                            string path = blat.FindPath(attachid);
                            pm.DeleteFile(attachid, path);
                        }
                        blWor.Delete(OpeId);
                    }
                    break;
                case "更新":
                    int i = Convert.ToInt32(ReId);
                    int attachidold = blWor.FindAttachmentid(OpeId);
                    int attachidnew = blWor.FindAttachmentid(i);
                    if (Agree)
                    {
                        blWor.UpdateIsPass(i, true);
                        if (attachidold > 0 && attachidnew != attachidold)
                        {
                            string path = blat.FindPath(attachidold);
                            pm.DeleteFile(attachidold, path);
                        }
                        blWor.Delete(OpeId);
                    }
                    else
                    {
                        blWor.UpdateIsPass(OpeId, true);
                        if (attachidnew > 0 && attachidold != attachidnew)
                        {
                            string path = blat.FindPath(attachidnew);
                            pm.DeleteFile(attachidnew, path);
                        }
                        blWor.Delete(i);
                    }
                    break;
            }
        }

        //lby ↓ ProjectFile
        protected void ProjectFiles(string OpeType, bool Agree, int OpeId, string ReId)
        {
            BLLProjectFile bllProjectFile = new BLLProjectFile();
            switch (OpeType)
            {
                case "删除":
                    if (Agree)
                    {
                        int attachid = bllProjectFile.Delete(OpeId);
                        string filePath = blat.FindPath(attachid);
                        pm.DeleteFile(attachid, filePath);
                    }
                    else
                        bllProjectFile.ChangePass(OpeId, true);
                    break;
                case "添加":
                    if (Agree)
                        bllProjectFile.ChangePass(OpeId, true);
                    else
                    {
                        int attachid = bllProjectFile.Delete(OpeId);
                        pm.DeleteFile(attachid, blat.FindPath(attachid));
                    }
                    break;
                case "更新":
                    int id = Convert.ToInt32(ReId);
                    if (Agree)
                    {
                        bllProjectFile.ChangePass(id, true);
                        int attachid = bllProjectFile.Delete(OpeId);
                        pm.DeleteFile(attachid, blat.FindPath(attachid));
                    }
                    else
                    {
                        bllProjectFile.ChangePass(OpeId, true);
                        int attachid = bllProjectFile.Delete(id);
                        pm.DeleteFile(attachid, blat.FindPath(attachid));
                    }
                    break;
            }
        }

        protected void GridOpetate_PageIndexChange(object sender, FineUI.GridPageEventArgs e)
        {
            this.GridOpetate.PageIndex = e.NewPageIndex;
            InitGrid();
        }

        protected void ddlGridPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridOpetate.PageIndex = 0;
            this.GridOpetate.PageSize = Convert.ToInt32(ddlGridPageSize.SelectedValue);
            InitGrid();
        }
    }
}