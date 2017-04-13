using BLHelper;
using Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WDFramework.Information
{
    public partial class Detail : System.Web.UI.Page
    {
        BLLOperationLog blop = new BLLOperationLog();
        BLLAttachment blat = new BLLAttachment();
        BLLUser bluser = new BLLUser();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                InitData();
        }

        //处理DateTime数据
        private string ThangeDate(DateTime? time)
        {
            if (!time.HasValue)
                return "";
            else
                return time.Value.ToShortDateString();
        }

        //
        private void InitData()
        {
            try
            {
                int id = Convert.ToInt32(Request.QueryString["id"].ToString());
                OperationLog op = blop.FindbyId(id);
                if (op.OperationContent == "Photos")
                {
                    Contents.Hidden = true;
                    old.Text = "原照片";
                    inew.Text = "新照片";
                }
                else
                {
                    old.Text = "";
                    inew.Text = "";
                    imgPhotonew.Hidden = true;
                    imgPhotoold.Hidden = true;
                }
                switch (op.OperationContent)
                {
                    case "AcademicMeeting":
                        Contents.Text = AcademicMetting(op.OperationType, Convert.ToInt32(op.OperationDataID), op.Remark);
                        break;
                    case "AchieveAward":
                        Contents.Text = AchieveAward(op.OperationType, Convert.ToInt32(op.OperationDataID), op.Remark);
                        break;
                    case "Achievement":
                        Contents.Text = Achievement(op.OperationType, Convert.ToInt32(op.OperationDataID), op.Remark);
                        break;
                    case "AchievementCA":
                        Contents.Text = AchievementCA(op.OperationType, Convert.ToInt32(op.OperationDataID), op.Remark);
                        break;
                    case "AchivementApply":
                        Contents.Text = AchivementApply(op.OperationType, Convert.ToInt32(op.OperationDataID), op.Remark);
                        break;
                    case "Agency":
                        Contents.Text = Agency(op.OperationType, Convert.ToInt32(op.OperationDataID), op.Remark);
                        break;
                    case "Announcement":
                        Contents.Text = Announcement(op.OperationType, Convert.ToInt32(op.OperationDataID), op.Remark);
                        break;
                    //case "AttendMeeting":
                    //    Contents.Text = AttendMeeting(op.OperationType, Convert.ToInt32(op.OperationDataID), op.Remark);
                    //    break;
                    case "Award":
                        Contents.Text = Award(op.OperationType, Convert.ToInt32(op.OperationDataID), op.Remark);
                        break;
                    case "Contract":
                        Contents.Text = Contract(op.OperationType, Convert.ToInt32(op.OperationDataID), op.Remark);
                        break;
                    case "DFurtherStudy":
                        Contents.Text = DFurtherStudy(op.OperationType, Convert.ToInt32(op.OperationDataID), op.Remark);
                        break;
                    case "Education":
                        Contents.Text = Education(op.OperationType, Convert.ToInt32(op.OperationDataID), op.Remark);
                        break;
                    case "EduExperience":
                        Contents.Text = EduExperience(op.OperationType, Convert.ToInt32(op.OperationDataID), op.Remark);
                        break;
                    case "Files":
                        Contents.Text = Files(op.OperationType, Convert.ToInt32(op.OperationDataID), op.Remark);
                        break;
                    case "FundInformation":
                        Contents.Text = FundInformation(op.OperationType, Convert.ToInt32(op.OperationDataID), op.Remark);
                        break;
                    case "FutherStudy":
                        Contents.Text = FutherStudy(op.OperationType, Convert.ToInt32(op.OperationDataID), op.Remark);
                        break;
                    case "Honor":
                        Contents.Text = Honor(op.OperationType, Convert.ToInt32(op.OperationDataID), op.Remark);
                        break;
                    case "LibraryRecord":
                        //Contents.Text = LibraryRecord(op.OperationType, Convert.ToInt32(op.OperationDataID), op.Remark);
                        break;
                    case "Monograph":
                        Contents.Text = Monograph(op.OperationType, Convert.ToInt32(op.OperationDataID), op.Remark);
                        break;
                    case "Pact":
                        Contents.Text = Pact(op.OperationType, Convert.ToInt32(op.OperationDataID), op.Remark);
                        break;
                    case "Paper":
                        Contents.Text = Paper(op.OperationType, Convert.ToInt32(op.OperationDataID), op.Remark);
                        break;
                    case "Patent":
                        Contents.Text = Patent(op.OperationType, Convert.ToInt32(op.OperationDataID), op.Remark);
                        break;
                    case "Photos":
                        Photos(op.OperationType, Convert.ToInt32(op.OperationDataID), op.Remark);
                        break;
                    case "Project":
                        Contents.Text = Project(op.OperationType, Convert.ToInt32(op.OperationDataID), op.Remark);
                        break;
                    case "ProjectImportantNode":
                        Contents.Text = ProjectImportantNode(op.OperationType, Convert.ToInt32(op.OperationDataID), op.Remark);
                        break;
                    case "ScienceReport":
                        Contents.Text = ScienceReport(op.OperationType, Convert.ToInt32(op.OperationDataID), op.Remark);
                        break;
                    case "SocialPartTime":
                        Contents.Text = SocialPartTime(op.OperationType, Convert.ToInt32(op.OperationDataID), op.Remark);
                        break;
                    case "SpeakClass":
                        Contents.Text = SpeakClass(op.OperationType, Convert.ToInt32(op.OperationDataID), op.Remark);
                        break;
                    case "Student":
                        Contents.Text = Student(op.OperationType, Convert.ToInt32(op.OperationDataID), op.Remark);
                        break;
                    case "UnitInspect":
                        Contents.Text = UnitInspect(op.OperationType, Convert.ToInt32(op.OperationDataID), op.Remark);
                        break;
                    case "UnitLectures":
                        Contents.Text = UnitLectures(op.OperationType, Convert.ToInt32(op.OperationDataID), op.Remark);
                        break;
                    case "UserInfo":
                        Contents.Text = Users(op.OperationType, Convert.ToInt32(op.OperationDataID), op.Remark);
                        break;
                    case "WorkExperience":
                        Contents.Text = WorkExperience(op.OperationType, Convert.ToInt32(op.OperationDataID), op.Remark);
                        break;
                    case "WorkPlanSummary":
                        Contents.Text = WorkPlanSummary(op.OperationType, Convert.ToInt32(op.OperationDataID), op.Remark);
                        break;
                    case "WithinPost":
                        Contents.Text = WithinPost(op.OperationType, Convert.ToInt32(op.OperationDataID), op.Remark);
                        break;
                    case "Platform":
                        Contents.Text = Platform(op.OperationType, Convert.ToInt32(op.OperationDataID), op.Remark);
                        break;
                    case "NewAcademicReporting":
                        Contents.Text = NewAcademicReporting(op.OperationType, Convert.ToInt32(op.OperationDataID), op.Remark);
                        break;
                }
            }
            catch (Exception ex)
            {
                BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
                pm.SaveError(ex, this.Request);
            }
        }

        //表WithinPost操作函数
        private string WithinPost(string OpeType, int OpeId, string ReId)
        {
            string[] arraySecrecyLevel = new string[5] { "公开", "内部", "秘密", "机密", "管理员" };
            BLLWithinPost blwith = new BLLWithinPost();
            Common.Entities.WithinPost withpost = blwith.FindByFileID(OpeId);
            string result = "文件名" + withpost.FileName +
                            "\n文件分类" + withpost.FileType +
                            "\n文件收放单位或部门" + withpost.AndUnit +
                            "\n收放接收人" + withpost.recipient +
                            "\n文件收放时间" + ThangeDate(withpost.Time) +
                            "\n保密级别" + arraySecrecyLevel[withpost.SecrecyLevel.Value - 1];
            switch (OpeType)
            {
                case "删除":
                    return result;
                case "添加":
                    return result;
                case "更新":
                    Common.Entities.WithinPost withpostnew = blwith.FindByFileID(Convert.ToInt32(ReId));
                    result = "文件名" + withpost.FileName + "\t新数据:" + withpostnew.FileName +
                            "\n文件分类" + withpost.FileType + "\t新数据:" + withpostnew.FileType +
                            "\n文件收放单位或部门" + withpost.AndUnit + "\t新数据:" + withpostnew.AndUnit +
                            "\n收放接收人" + withpost.recipient + "\t新数据:" + withpostnew.recipient +
                            "\n文件收放时间" + ThangeDate(withpost.Time) + "\t新数据:" + ThangeDate(withpostnew.Time) +
                            "\n保密级别" + arraySecrecyLevel[withpost.SecrecyLevel.Value - 1] + "\t新数据:" + arraySecrecyLevel[withpostnew.SecrecyLevel.Value - 1];
                    return result;
            }
            return "暂无";
        }

        //表Platform操作函数
        private string Platform(string OpeType, int OpeId, string ReId)
        {
            string[] arraySecrecyLevel = new string[5] { "公开", "内部", "秘密", "机密", "管理员" };
            BLLPlatform blplat = new BLLPlatform();
            Common.Entities.Platform platform = blplat.FindByPlatformID(OpeId);
            string result = "平台名称" + platform.PlatformName +
                            "\n平台级别" + platform.PlatformRank +
                            "\n批复部门" + platform.AgreeUnit +
                            "\n平台类别" + platform.PlatformType +
                            "\n批复日期" + ThangeDate(platform.AgreeTime) +
                            "\n保密级别" + arraySecrecyLevel[platform.SecrecyLevel.Value - 1];
            switch (OpeType)
            {
                case "删除":
                    return result;
                case "添加":
                    return result;
                case "更新":
                    Common.Entities.Platform platformnew = blplat.FindByPlatformID(Convert.ToInt32(ReId));
                    result = "平台名称" + platform.PlatformName +"\t新数据:" + platformnew.PlatformName +
                            "\n平台级别" + platform.PlatformRank +"\t新数据:" + platformnew.PlatformRank + 
                            "\n批复部门" + platform.AgreeUnit + "\t新数据:" + platformnew.AgreeUnit +
                            "\n平台类别" + platform.PlatformType + "\t新数据:" + platformnew.PlatformType +
                            "\n批复日期" + ThangeDate(platform.AgreeTime) + "\t新数据:" + ThangeDate(platformnew.AgreeTime) +
                            "\n保密级别" + arraySecrecyLevel[platform.SecrecyLevel.Value - 1] + "\t新数据:" + arraySecrecyLevel[platformnew.SecrecyLevel.Value - 1];
                    return result;
            }
            return "暂无";
        }

        //表NewAcademicReporting操作函数
        private string NewAcademicReporting(string OpeType, int OpeId, string ReId)
        {
            string[] arraySecrecyLevel = new string[5] { "公开", "内部", "秘密", "机密", "管理员" };
            BLLNewAcademicReporting blnewAca = new BLLNewAcademicReporting();
            Common.Entities.NewAcademicReporting newacademic = blnewAca.FindByNAReportingID(OpeId);
            string result = "报告人:" + newacademic.ReportPeople +
                            "\n职称:" + newacademic.JobName +
                            "\n职务:" + newacademic.JobMission +
                            "\n报告人单位:" + newacademic.ReportUnit +
                            "\n报告人身份证号:" + newacademic.Report +
                            "\n报告人手机号:" + newacademic.ReportTele +
                            "\n备注:" + newacademic.Remark +
                            "\n学术兼职及荣誉称号:" + newacademic.AcademicTitle +
                            "\n学术报告名称:" + newacademic.ReportName +
                            "\n报告时间:" + ThangeDate(newacademic.ReportTime) +
                            "\n报告地点:" + newacademic.ReportPlace +
                            "\n申请经费:" + newacademic.ApplyFund +
                            "\n参与人数:" + newacademic.PeopleCount +
                            "\n主办单位:" + newacademic.Organizers +
                            "\n协办单位:" + newacademic.Coorganizer +
                            "\n报告类别:" + newacademic.ReportType +
                            "\n主要参与人:" + newacademic.MajorPeople +
                            "\n等级:" + arraySecrecyLevel[newacademic.SecrecyLevel.Value - 1];
            switch (OpeType)
            {
                case "删除":
                    return result;
                case "添加":
                    return result;
                default:
                    return "暂无";
            }
        }

        //表AcademicMeting操作函数
        protected string AcademicMetting(string OpeType, int OpeId, string ReId)
        {

            string[] arraySecrecyLevel = new string[5] { "公开", "内部", "秘密", "机密", "管理员" };
            BLLAcademicMeeting blAca = new BLLAcademicMeeting();
            Common.Entities.AcademicMeeting academic = blAca.FindByAcademicMeetingID(OpeId, false);
            string result = "会议名称:" + academic.MeetingName + 
                            "\n主办方:" + academic.Organizers + 
                            "\n协办方:" + academic.Coorganizers + 
                            "\n会议开始时间:" + ThangeDate(academic.StratTime) + 
                            "\n会议结束时间:" + ThangeDate(academic.EndTime) + 
                            "\n会议地点:" + academic.MeetingPlace + 
                            "\n论文集名称:" + academic.ProceedingsofTitle + 
                            "\n会议分类名称:" + academic.MeetingSortName +
                            "\n等级:" + arraySecrecyLevel[academic.SecrecyLevel.Value - 1];
            switch (OpeType)
            {
                case "删除":
                    return result;
                case "添加":
                    return result;
                default:
                    return "暂无";
            }
        }

        //AchieveAward
        protected string AchieveAward(string OpeType, int OpeId, string ReId)
        {
            string[] arraySecrecyLevel = new string[5] { "公开", "内部", "秘密", "机密", "管理员" };
            BLLAchieveAward blAch = new BLLAchieveAward();
            Common.Entities.AchieveAward achold = blAch.FindAll(OpeId);
            string restult = "评奖单位:" + achold.AwardUnit +
                            "\n评奖名称:" + achold.AwardName +
                            "\n评奖等级:" + achold.AwardGrade +
                            "\n报奖类别:" + achold.AwardType +
                            "\n等级:" + arraySecrecyLevel[achold.SecrecyLevel.Value - 1];
            switch (OpeType)
            {
                case "删除": return restult;
                case "添加": return restult;
                case "更新":
                    int i = Convert.ToInt32(ReId);
                    AchieveAward achnew = blAch.FindAll(i);
                    restult = "评奖单位:" + achold.AwardUnit + "\t新数据:" + achnew.AwardUnit +
                            "\n评奖名称:" + achold.AwardName + "\t新数据:" + achnew.AwardName +
                            "\n评奖等级:" + achold.AwardGrade + "\t新数据:" + achnew.AwardGrade +
                            "\n报奖类别:" + achold.AwardType + "\t新数据:" + achnew.AwardType +
                            "\n等级:" + arraySecrecyLevel[achold.SecrecyLevel.Value - 1] + "\t新数据:" + arraySecrecyLevel[achnew.SecrecyLevel.Value - 1];
                    return restult;
            }
            return "暂无";
        }

        //Achievement
        protected string Achievement(string OpeType, int OpeId, string ReId)
        {
            string[] arraySecrecyLevel = new string[5] { "公开", "内部", "秘密", "机密", "管理员" };
            BLLAchievement blAchi = new BLLAchievement();
            BLLAgency blag = new BLLAgency();
            BLLProject blpro = new BLLProject();
            Common.Entities.Achievement achiold = blAchi.FindAll(OpeId);
            string restult = "成果名称:" + achiold.AchievementName +
                             //"\n级别:" + achiold.AchievementRank +
                             //"\n时间:" + ThangeDate(achiold.AchievementTime) +
                             "\n机构名称:" + blag.FindAgenName(achiold.AgencyID) +
                              "\n项目分类编号:" + achiold.ProjectInNum +
                             "\n鉴定级别:" + achiold.ProjectRank +
                             "\n鉴定水平:" + achiold.ProjectLevel +
                             "\n鉴定形式:" + achiold.ProjectForm +
                             "\n所属项目名称:" + achiold.ProjectName +
                             "\n鉴定时间:" + ThangeDate(achiold.AppraisalTime) +
                             "\n鉴定组织部门:" + achiold.AppraisalUnit +
                             "\n鉴定评语级别:" + achiold.ApRemarkRank +
                             "\n等级:" + arraySecrecyLevel[achiold.SecrecyLevel.Value - 1];
            switch (OpeType)
            {
                case "删除": return restult;
                case "添加": return restult;
                case "更新":
                    int i = Convert.ToInt32(ReId);
                    Common.Entities.Achievement achinew = blAchi.FindAll(i);
                    restult = "成果名称:" + achiold.AchievementName + "\t新数据:" + achinew.AchievementName +
                             //"\n级别:" + achiold.AchievementRank + "\t新数据:" + achinew.AchievementRank +
                             //"\n时间:" + ThangeDate(achiold.AchievementTime) + "\t新数据:" + ThangeDate(achinew.AchievementTime) +
                             "\n机构名称:" + blag.FindAgenName(achiold.AgencyID) + "\t新数据:" + blag.FindAgenName(achinew.AgencyID) +
                               "\n项目分类编号:" + achiold.ProjectInNum + "\t新数据:" + achinew.ProjectInNum +
                             "\n鉴定级别:" + achiold.ProjectRank + "\t新数据:" + achinew.ProjectRank +
                             "\n鉴定水平:" + achiold.ProjectLevel + "\t新数据:" + achinew.ProjectLevel +
                             "\n鉴定形式:" + achiold.ProjectForm + "\t新数据:" + achinew.ProjectForm +
                             "\n所属项目名称:" + achiold.ProjectName + "\t新数据:" + achinew.ProjectName +
                             "\n鉴定时间:" + ThangeDate(achiold.AppraisalTime) + "\t新数据:" + ThangeDate(achinew.AppraisalTime) +
                             "\n鉴定组织部门:" + achiold.AppraisalUnit + "\t新数据:" + achinew.AppraisalUnit +
                             "\n鉴定评语级别:" + achiold.ApRemarkRank + "\t新数据:" + achinew.ApRemarkRank +
                             "\n等级:" + arraySecrecyLevel[achiold.SecrecyLevel.Value - 1] + "\t新数据:" + arraySecrecyLevel[achinew.SecrecyLevel.Value - 1];
                    return restult;
            }
            return "暂无";
        }

        //AchievementCA
        protected string AchievementCA(string OpeType, int OpeId, string ReId)
        {
            string[] arraySecrecyLevel = new string[5] { "公开", "内部", "秘密", "机密", "管理员" };
            BLLAchievementCA blAchi = new BLLAchievementCA();
            BLLAchievement bllAchivement = new BLLAchievement();
            AchievementCA achicaold = blAchi.FindAll(OpeId);
            string restult = "成果名称：" + bllAchivement.FindAchieveName(Convert.ToInt32(achicaold.AchievementID)) +
                            "\n验收时间:" + ThangeDate(achicaold.CATime) +
                            "\n验收部门:" + achicaold.CAUnit +
                            "\n验收评语级别:" + achicaold.CACommnetLevel +
                             "\n等级:" + arraySecrecyLevel[achicaold.SecrecyLevel.Value - 1];
            switch (OpeType)
            {
                case "删除": return restult;
                case "添加": return restult;
                case "更新":
                    int i = Convert.ToInt32(ReId);
                    AchievementCA achicanew = blAchi.FindAll(i);
                    restult = "成果名称：" + bllAchivement.FindAchieveName(Convert.ToInt32(achicaold.AchievementID)) + "\t新数据:" + bllAchivement.FindAchieveName(Convert.ToInt32(achicanew.AchievementID)) +
                            "\n验收时间:" + ThangeDate(achicaold.CATime) + "\t新数据:" + ThangeDate(achicanew.CATime) +
                            "\n验收部门:" + achicaold.CAUnit + "\t新数据:" + achicanew.CAUnit +
                            "\n验收评语级别:" + achicaold.CACommnetLevel + "\t新数据:" + achicanew.CACommnetLevel +
                             "\n等级:" + arraySecrecyLevel[achicaold.SecrecyLevel.Value - 1] + "\t新数据:" + arraySecrecyLevel[achicanew.SecrecyLevel.Value - 1];
                    return restult;
            }
            return "暂无";
        }

        //AchivementApply
        private string AchivementApply(string OpeType, int OpeId, string ReId)
        {
            string[] arraySecrecyLevel = new string[5] { "公开", "内部", "秘密", "机密", "管理员" };
            BLLAchievementApply blAchi = new BLLAchievementApply();
            BLLAchievement bllAchivement = new BLLAchievement();
            AchivementApply achiold = blAchi.FindAll(OpeId);
            string restult = "应用单位:" + achiold.ApplyUnit +
                            "\n开始时间:" + ThangeDate(achiold.StartTime) +
                            "\n结束时间:" + ThangeDate(achiold.EndTime) +
                            "\n成果名称：" + bllAchivement.FindAchieveName(Convert.ToInt32(achiold.AchievementID)) +
                            "\n用途:" + achiold.Use +
                             "\n等级:" + arraySecrecyLevel[achiold.SecrecyLevel.Value - 1];
            switch (OpeType)
            {
                case "删除": return restult;
                case "添加": return restult;
                case "更新":
                    int i = Convert.ToInt32(ReId);
                    AchivementApply achinew = blAchi.FindAll(i);
                    restult = "应用单位:" + achiold.ApplyUnit + "\t新数据:" + achinew.ApplyUnit +
                            "\n开始时间:" + ThangeDate(achiold.StartTime) + "\t新数据:" + ThangeDate(achinew.StartTime) +
                            "\n结束时间:" + ThangeDate(achiold.EndTime) + "\t新数据:" + ThangeDate(achinew.EndTime) +
                            "\n成果名称：" + bllAchivement.FindAchieveName(Convert.ToInt32(achiold.AchievementID)) + "\t新数据:" + bllAchivement.FindAchieveName(Convert.ToInt32(achinew.AchievementID)) +
                            "\n用途:" + achiold.Use + "\t新数据:" + achinew.Use +
                             "\n等级:" + arraySecrecyLevel[achiold.SecrecyLevel.Value - 1] + "\t新数据:" + arraySecrecyLevel[achinew.SecrecyLevel.Value - 1];
                    return restult;
            }
            return "暂无";
        }

        //Agency
        protected string Agency(string OpeType, int OpeId, string ReId)
        {
            string[] arraySecrecyLevel = new string[5] { "公开", "内部", "秘密", "机密", "管理员" };
            BLLAgency blAg = new BLLAgency();
            Common.Entities.Agency agenold = blAg.FindByid(OpeId);
            string restult = "机构名称:" + agenold.AgencyName +
                            "\n父机构:" + blAg.FindByid(Convert.ToInt32(agenold.ParentID)) +
                            "\n机构负责人:" + agenold.AgencyHeads + 
                            "\n机构分类编号:" + agenold.AgencyNumber + 
                            "\n研究方向:" + agenold.Research + 
                            "\n专职人数:" + agenold.FullTimeNumbers + 
                            "\n兼职人数:" + agenold.PartTimeNumbers + 
                            "\n面积:" + agenold.Area +
                            "\n地点:" + agenold.Location +
                             "\n等级:" + arraySecrecyLevel[agenold.SecrecyLevel.Value - 1];
            switch (OpeType)
            {
                case "删除": return restult;
                case "添加": return restult;
                case "更新":
                    int i = Convert.ToInt32(ReId);
                    Common.Entities.Agency agennew = blAg.FindByid(i);
                    restult = "机构名称:" + agenold.AgencyName + "\t新数据:" + agennew.AgencyName +
                            "\n父机构:" + blAg.FindByid(Convert.ToInt32(agenold.ParentID)) + "\t新数据:" + blAg.FindByid(Convert.ToInt32(agennew.ParentID)) +
                            "\n机构负责人:" + agenold.AgencyHeads + "\t新数据:" + agennew.AgencyHeads +
                            "\n机构分类编号:" + agenold.AgencyNumber + "\t新数据:" + agennew.AgencyNumber +
                            "\n研究方向:" + agenold.Research + "\t新数据:" + agennew.Research +
                            "\n专职人数:" + agenold.FullTimeNumbers + "\t新数据:" + agennew.FullTimeNumbers +
                            "\n兼职人数:" + agenold.PartTimeNumbers + "\t新数据:" + agennew.PartTimeNumbers +
                            "\n面积:" + agenold.Area + "\t新数据:" + agennew.Area +
                            "\n地点:" + agenold.Location + "\t新数据:" + agennew.Location +
                             "\n等级:" + arraySecrecyLevel[agenold.SecrecyLevel.Value - 1] + "\t新数据:" + arraySecrecyLevel[agennew.SecrecyLevel.Value - 1];
                    return restult;
            }
            return "暂无";
        }

        //Announcement
        protected string Announcement(string OpeType, int OpeId, string ReId)
        {
            string[] arraySecrecyLevel = new string[5] { "公开", "内部", "秘密", "机密", "管理员" };
            BLLAnnouncement blAnn = new BLLAnnouncement();
            Common.Entities.Announcement annouold = blAnn.Find(OpeId);
            string restult = "标题:" + annouold.HeadLine + 
                            "\n公告来源单位:" + annouold.SourceAgency + 
                            "\n分类:" + annouold.AnnouncementSortName +
                            "\n时间:" + ThangeDate(annouold.Time) +
                             "\n等级:" + arraySecrecyLevel[annouold.SecrecyLevel.Value - 1]; 
            switch (OpeType)
            {
                case "删除": return restult;
                case "添加": return restult;
                case "更新":
                    int i = Convert.ToInt32(ReId);
                    Common.Entities.Announcement announew = blAnn.Find(i);
                    restult = "标题:" + annouold.HeadLine + "\t新数据:" + announew.HeadLine +
                            "\n公告来源单位:" + annouold.SourceAgency + "\t新数据:" + announew.SourceAgency +
                            "\n分类:" + annouold.AnnouncementSortName + "\t新数据:" + announew.AnnouncementSortName +
                            "\n时间:" + ThangeDate(annouold.Time) + "\t新数据:" + ThangeDate(announew.Time) +
                             "\n等级:" + arraySecrecyLevel[annouold.SecrecyLevel.Value - 1] + "\t新数据:" + arraySecrecyLevel[announew.SecrecyLevel.Value - 1];
                    return restult;
            }
            return "暂无";
        }

        //Award
        protected string Award(string OpeType, int OpeId, string ReId)
        {
            string[] arraySecrecyLevel = new string[5] { "公开", "内部", "秘密", "机密", "管理员" };
            BLLAward blAw = new BLLAward();
            BLLAchievement bllAchivement = new BLLAchievement();
            Award awardold = blAw.Find(OpeId);
            string restult = "奖励种类:" + awardold.AwardwSpecies + 
                            "\n等级:" + awardold.Grade + 
                            "\n获奖名称:" + awardold.AwardTime + 
                            "\n获奖时间:" + ThangeDate(awardold.AwardTime) + 
                            "\n赋予机构:" + awardold.GivAgency + 
                            "\n单位:" + awardold.Unit +
                            "\n成果名称：" + bllAchivement.FindAchieveName(Convert.ToInt32(awardold.Acheivement)) +
                             "\n等级:" + arraySecrecyLevel[awardold.SecrecyLevel.Value - 1]; 
            switch (OpeType)
            {
                case "删除": return restult;
                case "添加": return restult;
                case "更新":
                    int i = Convert.ToInt32(ReId);
                    Award awardnew = blAw.Find(i);
                    restult = "奖励种类:" + awardold.AwardwSpecies + "\t新数据:" + awardnew.AwardwSpecies +
                            "\n等级:" + awardold.Grade + "\t新数据:" + awardnew.Grade +
                            "\n获奖名称:" + awardold.AwardName + "\t新数据:" + awardnew.AwardName +
                            "\n获奖时间:" + ThangeDate(awardold.AwardTime) + "\t新数据:" + ThangeDate(awardnew.AwardTime) +
                            "\n赋予机构:" + awardold.GivAgency + "\t新数据:" + awardnew.GivAgency +
                            "\n单位:" + awardold.Unit + "\t新数据:" + awardnew.Unit +
                            "\n成果名称：" + bllAchivement.FindAchieveName(Convert.ToInt32(awardold.Acheivement)) + "\t新数据:" + bllAchivement.FindAchieveName(Convert.ToInt32(awardnew.Acheivement)) +
                             "\n等级:" + arraySecrecyLevel[awardold.SecrecyLevel.Value - 1] + "\t新数据:" + arraySecrecyLevel[awardnew.SecrecyLevel.Value - 1];
                    return restult;
            }
            return "暂无";
        }

        //Contract
        protected string Contract(string OpeType, int OpeId, string ReId)
        {
            string[] arraySecrecyLevel = new string[5] { "公开", "内部", "秘密", "机密", "管理员" };
            BLLContract blCon = new BLLContract();
            Contract conold = blCon.FindByContractID(OpeId);
            string restult = "资料题目:" + conold.ContractHeadLine +
                            "\n资料保存人:" + conold.ContractAuthors +
                            "\n原始文件保存人:" + conold.ContractOriginal +
                             "\n等级:" + arraySecrecyLevel[conold.SecrecyLevel.Value - 1];
            switch (OpeType)
            {
                case "删除": return restult;
                case "添加": return restult;
                case "更新":
                    int i = Convert.ToInt32(ReId);
                    Contract connew = blCon.FindByContractID(i);
                    restult = "资料题目:" + conold.ContractHeadLine + "\t新数据:" + connew.ContractHeadLine +
                            "\n资料保存人:" + conold.ContractAuthors + "\t新数据:" + connew.ContractAuthors +
                            "\n原始文件保存人:" + conold.ContractOriginal + "\t新数据:" + connew.ContractOriginal +
                             "\n等级:" + arraySecrecyLevel[conold.SecrecyLevel.Value - 1] + "\t新数据:" + arraySecrecyLevel[connew.SecrecyLevel.Value - 1];
                    return restult;
            }
            return "暂无";
        }

        //DFurtherStudy
        protected string DFurtherStudy(string OpeType, int OpeId, string ReId)
        {
            string[] arraySecrecyLevel = new string[5] { "公开", "内部", "秘密", "机密", "管理员" };
            BLLDFurtherStudy blDFur = new BLLDFurtherStudy();
            BLLUser bllUser = new BLLUser();

            DFurtherStudy dfutherstudyold = blDFur.FindByID(OpeId);
            string restult = "人员名称：" + bllUser.FindByUserID(Convert.ToInt32(dfutherstudyold.UserInfoID)) +
                            "\n进修地点:" + dfutherstudyold.StudyPlace +
                            "\n进修学校:" + dfutherstudyold.StudySchool +
                            "\n进修内容:" + dfutherstudyold.StudyContent +
                            "\n进修开始时间:" + ThangeDate(dfutherstudyold.DBegainTime) +
                            "\n进修结束时间:" + ThangeDate(dfutherstudyold.DEndTime) +
                             "\n等级:" + arraySecrecyLevel[dfutherstudyold.SecrecyLevel.Value - 1];
            switch (OpeType)
            {
                case "删除": return restult;
                case "添加": return restult;
                case "更新":
                    int i = Convert.ToInt32(ReId);
                    DFurtherStudy dfutherstudynew = blDFur.FindByID(i);
                    restult = "人员名称：" + bllUser.FindByUserID(Convert.ToInt32(dfutherstudyold.UserInfoID)) + "\t新数据:" + bllUser.FindByUserID(Convert.ToInt32(dfutherstudynew.UserInfoID)) +
                            "\n进修地点:" + dfutherstudyold.StudyPlace + "\t新数据:" + dfutherstudynew.StudyPlace +
                            "\n进修学校:" + dfutherstudyold.StudySchool + "\t新数据:" + dfutherstudynew.StudySchool +
                            "\n进修内容:" + dfutherstudyold.StudyContent + "\t新数据:" + dfutherstudynew.StudyContent +
                            "\n进修开始时间:" + ThangeDate(dfutherstudyold.DBegainTime) + "\t新数据:" + ThangeDate(dfutherstudynew.DBegainTime) +
                            "\n进修结束时间:" + ThangeDate(dfutherstudyold.DEndTime) + "\t新数据:" + ThangeDate(dfutherstudynew.DEndTime) +
                             "\n等级:" + arraySecrecyLevel[dfutherstudyold.SecrecyLevel.Value - 1] + "\t新数据:" + arraySecrecyLevel[dfutherstudynew.SecrecyLevel.Value - 1];
                    return restult;
            }
            return "暂无";
        }

        //Education
        protected string Education(string OpeType, int OpeId, string ReId)
        {
            string[] arraySecrecyLevel = new string[5] { "公开", "内部", "秘密", "机密", "管理员" };
            BLLEducation blEdu = new BLLEducation();
            BLLUser user = new BLLUser();
            Education educaold = blEdu.Find(OpeId);
            string restult = "学校名称:" + educaold.SchoolName + 
                            "\n学位:" + educaold.Degree + 
                            "\n学历取得时间:" + ThangeDate(educaold.EduTime) + 
                            "\n人员姓名:" + user.FindUserName(Convert.ToInt32(educaold.UserInfoID)) + 
                            "\n学院:" + educaold.College + 
                            "\n系:" + educaold.Series +
                            "\n专业:" + educaold.Major +
                             "\n等级:" + arraySecrecyLevel[educaold.SecrecyLevel.Value - 1];
            switch (OpeType)
            {
                case "删除": return restult;
                case "添加": return restult;
                case "更新":
                    int i = Convert.ToInt32(ReId);
                    Education educanew = blEdu.Find(i);
                    restult = "学校名称:" + educaold.SchoolName + "\t新数据:" + educanew.SchoolName +
                            "\n学位:" + educaold.Degree + "\t新数据:" + educanew.Degree +
                            "\n学历取得时间:" + ThangeDate(educaold.EduTime) + "\t新数据:" + ThangeDate(educanew.EduTime) +
                            "\n人员姓名:" + user.FindUserName(Convert.ToInt32(educaold.UserInfoID)) + "\t新数据:" + user.FindUserName(Convert.ToInt32(educanew.UserInfoID)) +
                            "\n学院:" + educaold.College + "\t新数据:" + educanew.College +
                            "\n系:" + educaold.Series + "\t新数据:" + educanew.Series +
                            "\n专业:" + educaold.Major + "\t新数据:" + educanew.Major +
                             "\n等级:" + arraySecrecyLevel[educaold.SecrecyLevel.Value - 1] + "\t新数据:" + arraySecrecyLevel[educanew.SecrecyLevel.Value - 1];
                    return restult;
            }
            return "暂无";
        }

        //EduExperience
        protected string EduExperience(string OpeType, int OpeId, string ReId)
        {
            string[] arraySecrecyLevel = new string[5] { "公开", "内部", "秘密", "机密", "管理员" };
            BLLEduExperience blEdu = new BLLEduExperience();
            BLLUser user = new BLLUser();
            EduExperience eduold = blEdu.Find(OpeId);
            string restult = "开始时间:" + ThangeDate(eduold.StartTime) + 
                            "\n结束时间:" + ThangeDate(eduold.EndTime) + 
                            "\n所学专业:" + eduold.Major + 
                            "\n人员姓名:" + user.FindUserName(Convert.ToInt32(eduold.UserInfoID)) +
                            "\n担任职位:" + eduold.EHoldOffice +
                             "\n等级:" + arraySecrecyLevel[eduold.SecrecyLevel.Value - 1];
            switch (OpeType)
            {
                case "删除": return restult;
                case "添加": return restult;
                case "更新":
                    int i = Convert.ToInt32(ReId);
                    EduExperience edunew = blEdu.Find(i);
                    restult = "开始时间:" + ThangeDate(eduold.StartTime) + "\t新数据:" + ThangeDate(edunew.StartTime) +
                            "\n结束时间:" + ThangeDate(eduold.EndTime) + "\t新数据:" + ThangeDate(edunew.EndTime) +
                            "\n所学专业:" + eduold.Major + "\t新数据:" + edunew.Major +
                            "\n人员姓名:" + user.FindUserName(Convert.ToInt32(eduold.UserInfoID)) + "\t新数据:" + user.FindUserName(Convert.ToInt32(edunew.UserInfoID)) +
                            "\n担任职位:" + eduold.EHoldOffice + "\t新数据:" + edunew.EHoldOffice +
                             "\n等级:" + arraySecrecyLevel[eduold.SecrecyLevel.Value - 1] + "\t新数据:" + arraySecrecyLevel[edunew.SecrecyLevel.Value - 1];
                    return restult;
            }
            return "暂无";
        }

        //Files
        protected string Files(string OpeType, int OpeId, string ReId)
        {
            string[] arraySecrecyLevel = new string[5] { "公开", "内部", "秘密", "机密", "管理员" };
            BLLFiles blFile = new BLLFiles();
            BLLAgency agen = new BLLAgency();
            Common.Entities.Files fileold = blFile.FindByFileID(OpeId);
            string restult = "文件名:" + fileold.FileName + 
                            "\n文件分类:" + fileold.DocumentCategoryID + 
                            "\n所属部门:" + agen.FindAgenName(Convert.ToInt32(fileold.AgencyID))+
                             "\n等级:" + arraySecrecyLevel[fileold.SecrecyLevel.Value - 1];
            switch (OpeType)
            {
                case "删除": return restult;
                case "添加": return restult;
                case "更新":
                    int i = Convert.ToInt32(ReId);
                    Common.Entities.Files filenew = blFile.FindByFileID(i);
                    restult = "文件名:" + fileold.FileName + "\t新数据:" + filenew.FileName +
                            "\n文件分类:" + fileold.DocumentCategoryID + "\t新数据:" + filenew.DocumentCategoryID +
                            "\n所属部门:" + agen.FindAgenName(Convert.ToInt32(fileold.AgencyID)) + "\t新数据:" + agen.FindAgenName(Convert.ToInt32(filenew.AgencyID))+
                             "\n等级:" + arraySecrecyLevel[fileold.SecrecyLevel.Value - 1] + "\t新数据:" + arraySecrecyLevel[filenew.SecrecyLevel.Value - 1];
                    return restult;
            }
            return "暂无";
        }

        //FundInformation
        protected string FundInformation(string OpeType, int OpeId, string ReId)
        {
            string[] arraySecrecyLevel = new string[5] { "公开", "内部", "秘密", "机密", "管理员" };
            BLLFundInformation blFund = new BLLFundInformation();
            BLLUser user = new BLLUser();
            BLLProject projects = new BLLProject();

            FundInformation fundold = blFund.FindByid(OpeId);
            string restult = "提取人姓名:" + user.FindUserName(Convert.ToInt32(fundold.UserInfoID)) + 
                            "\n时间:" + ThangeDate(fundold.Time) + 
                            "\n经费用途:" + fundold.FundInformationID + 
                            "\n所属项目名称:" + projects.SelectProjectName(Convert.ToInt32(fundold.ProjectID)) + 
                            "\n每项用途所用金额:" + fundold.EveItemUseMoney + 
                            "\n比例:" + fundold.Proportion + 
                            "\n操作类型:" + fundold.OperateType +
                            "\n经费负责人:" + fundold.BudgetDirector +
                             "\n等级:" + arraySecrecyLevel[fundold.SecrecyLevel.Value - 1];
            switch (OpeType)
            {
                case "删除": return restult;
                case "添加": return restult;
                case "更新":
                    //int i = Convert.ToInt32(ReId);
                    //FundInformation fundnew = blFund.FindByid(i);
                    //restult = "提取人姓名:" + user.FindUserName(Convert.ToInt32(fundold.UserInfoID)) + "\t新数据:" + user.FindUserName(Convert.ToInt32(fundnew.UserInfoID)) +
                    //        "\n时间:" + fundold.Time.Value.ToShortDateString() + "\t新数据:" + fundnew.Time.Value.ToShortDateString() +
                    //        "\n经费用途:" + fundold.FundingPurposeSortName + "\t新数据:" + fundnew.FundingPurposeSortName +
                    //        "\n所属项目名称:" + projects.SelectProjectName(Convert.ToInt32(fundold.ProjectID)) + "\t新数据:" + projects.SelectProjectName(Convert.ToInt32(fundnew.ProjectID)) +
                    //        "\n每项用途所用金额:" + fundold.EveItemUseMoney + "\t新数据:" + fundnew.EveItemUseMoney +
                    //        "\n比例:" + fundold.Proportion + "\t新数据:" + fundnew.Proportion +
                    //        "\n操作类型:" + fundold.OperateType + "\t新数据:" +  fundnew.OperateType +
                    //        "\n经费负责人:" + fundold.BudgetDirector + "\t新数据:" + fundnew.BudgetDirector;
                    break;
            }
            return "暂无";
        }

        //FutherStudy
        protected string FutherStudy(string OpeType, int OpeId, string ReId)
        {
            string[] arraySecrecyLevel = new string[5] { "公开", "内部", "秘密", "机密", "管理员" };
            BLLFutherStudy blFur = new BLLFutherStudy();
            BLLAgency agen = new BLLAgency();
            Common.Entities.FutherStudy furold = blFur.FindFurByID(OpeId);
            string restult = "姓名:" + furold.Name + 
                            "\n性别:" +  (Convert.ToBoolean(furold.Sex) ? "男" : "女") + 
                            "\n籍贯:" + furold.Hometown + 
                            "\n出生年月:" + ThangeDate(furold.Birthday) + 
                            "\n联系电话:" + furold.PhoneNum + 
                            "\n证件类型:" + furold.DocuType + 
                            "\n证件号码:" + furold.IDNum + 
                            "\n进修地点:" + furold.LearnPlace + 
                            "\n进修学校:" + furold.LearnSchool + 
                            "\n进修开始时间:" + ThangeDate(furold.LearnBeginTime) + 
                            "\n进修结束时间:" + ThangeDate(furold.LearnEndTime) + 
                            "\n进修内容:" + furold.LearnContent +
                            "\n所属机构:" + agen.FindAgenName(Convert.ToInt32(furold.AgencyID)) +
                             "\n等级:" + arraySecrecyLevel[furold.SecrecyLevel.Value - 1];
            switch (OpeType)
            {
                case "删除": return restult;
                case "添加": return restult;
                case "更新":
                    int i = Convert.ToInt32(ReId);
                    Common.Entities.FutherStudy furnew = blFur.FindFurByID(i);
                    restult = "姓名:" + furold.Name + "\t新数据:" + furnew.Name +
                            "\n性别:" + (Convert.ToBoolean(furold.Sex) ? "男" : "女") + "\t新数据:" + (Convert.ToBoolean(furnew.Sex) ? "男" : "女") +
                            "\n籍贯:" + furold.Hometown + "\t新数据:" + furnew.Hometown +
                            "\n出生年月:" + ThangeDate(furold.Birthday) + "\t新数据:" + ThangeDate(furnew.Birthday) +
                            "\n联系电话:" + furold.PhoneNum + "\t新数据:" + furnew.PhoneNum +
                            "\n证件类型:" + furold.DocuType + "\t新数据:" + furnew.DocuType +
                            "\n证件号码:" + furold.IDNum + "\t新数据:" + furnew.IDNum +
                            "\n进修地点:" + furold.LearnPlace + "\t新数据:" + furnew.LearnPlace +
                            "\n进修学校:" + furold.LearnSchool + "\t新数据:" + furnew.LearnSchool +
                            "\n进修开始时间:" + ThangeDate(furold.LearnBeginTime) + "\t新数据:" + ThangeDate(furnew.LearnBeginTime) +
                            "\n进修结束时间:" + ThangeDate(furold.LearnEndTime) + "\t新数据:" + ThangeDate(furnew.LearnEndTime) +
                            "\n进修内容:" + furold.LearnContent + "\t新数据:" + furnew.LearnContent +
                            "\n所属机构:" + agen.FindAgenName(Convert.ToInt32(furold.AgencyID)) + "\t新数据:" + agen.FindAgenName(Convert.ToInt32(furnew.AgencyID)) +
                             "\n等级:" + arraySecrecyLevel[furold.SecrecyLevel.Value - 1] + "\t新数据:" + arraySecrecyLevel[furnew.SecrecyLevel.Value - 1];
                    return restult;
            }
            return "暂无";
        }

        //Honor
        protected string Honor(string OpeType, int OpeId, string ReId)
        {
            string[] arraySecrecyLevel = new string[5] { "公开", "内部", "秘密", "机密", "管理员" };
            BLLHonor blHo = new BLLHonor();
            BLLUser user = new BLLUser();
            Honor honorold = blHo.FindByHonorID(OpeId, false);
            string restult = "称号名称:" + honorold.TitleName + 
                            "\n级别:" + honorold.Sort + 
                            "\n授予时间:" + ThangeDate(honorold.GiveTime) + 
                            "\n授予部门:" + honorold.GivDivision +
                            "\n人员姓名:" + user.FindUserName(Convert.ToInt32(honorold.UserInfoID)) +
                             "\n等级:" + arraySecrecyLevel[honorold.SecrecyLevel.Value - 1];
            switch (OpeType)
            {
                case "删除": return restult;
                case "添加": return restult;
                case "更新":
                    int i = Convert.ToInt32(ReId);
                    Honor honornew = blHo.FindByHonorID(i, false);
                    restult = "称号名称:" + honorold.TitleName + "\t新数据:" + honornew.TitleName +
                            "\n级别:" + honorold.Sort + "\t新数据:" + honornew.Sort +
                            "\n授予时间:" + ThangeDate(honorold.GiveTime) + "\t新数据:" + ThangeDate(honornew.GiveTime) +
                            "\n授予部门:" + honorold.GivDivision + "\t新数据:" + honornew.GivDivision +
                            "\n人员姓名:" + user.FindUserName(Convert.ToInt32(honorold.UserInfoID)) + "\t新数据:" + user.FindUserName(Convert.ToInt32(honornew.UserInfoID)) +
                             "\n等级:" + arraySecrecyLevel[honorold.SecrecyLevel.Value - 1] + "\t新数据:" + arraySecrecyLevel[honornew.SecrecyLevel.Value - 1];
                    break;
            }
            return "暂无";
        }


        //Monograph
        protected string Monograph(string OpeType, int OpeId, string ReId)
        {
            string[] arraySecrecyLevel = new string[5] { "公开", "内部", "秘密", "机密", "管理员" };
            BLLMonograph blMono = new BLLMonograph();
            BLLAchievement ach = new BLLAchievement();
            Monograph monoold = blMono.FindAll(OpeId);
            string restult = "专著名称:" + monoold.MonographName + 
                            "\n出版社:" + monoold.Publisher +
                            "\n分类:" + monoold.Sort +
                            "\n专著作者:" + monoold.MonographPeople +
                             "\n图书编号:" + monoold.BookNuber + 
                            "\n发行国家和地区:" + monoold.IssueRegin + 
                            "\n出版时间:" + ThangeDate(monoold.PUblicationTime) + 
                            "\n图书版次:" + monoold.Revision +
                            "\n所属成果:" + ach.FindAchieveName(Convert.ToInt32(monoold.AchievementID)) +
                             "\n等级:" + arraySecrecyLevel[monoold.SecrecyLevel.Value - 1];
            switch (OpeType)
            {
                case "删除": return restult;
                case "添加": return restult;
                case "更新":
                    Monograph mononew = blMono.FindAll(Convert.ToInt32(ReId));
                    restult = "专著名称:" + monoold.MonographName + "\t新数据:" + mononew.MonographName +
                            "\n出版社:" + monoold.Publisher + "\t新数据:" + mononew.Publisher +
                             "\n分类:" + monoold.Sort + "\t新数据:" + mononew.Sort +
                            "\n专著作者:" + monoold.MonographPeople + "\t新数据:" + mononew.MonographPeople +
                             "\n图书编号:" + monoold.BookNuber + "\t新数据:" + mononew.BookNuber +
                            "\n发行国家和地区:" + monoold.IssueRegin + "\t新数据:" + mononew.IssueRegin +
                            "\n出版时间:" + ThangeDate(monoold.PUblicationTime) + "\t新数据:" + ThangeDate(mononew.PUblicationTime) +
                            "\n图书版次:" + monoold.Revision + "\t新数据:" + mononew.Revision +
                            "\n所属成果:" + ach.FindAchieveName(Convert.ToInt32(monoold.AchievementID)) + "\t新数据:" + ach.FindAchieveName(Convert.ToInt32(mononew.AchievementID)) +
                             "\n等级:" + arraySecrecyLevel[monoold.SecrecyLevel.Value - 1] + "\t新数据:" + arraySecrecyLevel[mononew.SecrecyLevel.Value - 1];
                    return restult;
            }
            return "暂无";
        }

        //Pact
        protected string Pact(string OpeType, int OpeId, string ReId)
        {
            string[] arraySecrecyLevel = new string[5] { "公开", "内部", "秘密", "机密", "管理员" };
            BLLPact blPact = new BLLPact();
            BLLProject projects = new BLLProject();
            Pact pactold = blPact.FindByPactID(OpeId);
            string restult = "合同编号:" + pactold.PactNum + 
                            "\n合同开始时间:" + ThangeDate(pactold.StartTime) + 
                            "\n合同结束时间:" +ThangeDate( pactold.EndTime) + 
                            "\n合同类别:" + pactold.PactType +
                            "\n项目名称:" + projects.SelectProjectName(Convert.ToInt32(pactold.ProjectID)) +
                             "\n等级:" + arraySecrecyLevel[pactold.SecrecyLevel.Value - 1];
            switch (OpeType)
            {
                case "删除": return restult;
                case "添加": return restult;
                case "更新":
                    Pact pactnew = blPact.FindByPactID(Convert.ToInt32(ReId));
                    restult = "合同编号:" + pactold.PactNum + "\t新数据:" + pactnew.PactNum +
                            "\n合同开始时间:" + ThangeDate(pactold.StartTime) + "\t新数据:" + ThangeDate(pactnew.StartTime) +
                            "\n合同结束时间:" + ThangeDate(pactold.EndTime) + "\t新数据:" + ThangeDate(pactnew.EndTime) +
                            "\n合同类别:" + pactold.PactType + "\t新数据:" + pactnew.PactType +
                            "\n项目名称:" + projects.SelectProjectName(Convert.ToInt32(pactold.ProjectID)) + "\t新数据:" + projects.SelectProjectName(Convert.ToInt32(pactnew.ProjectID)) +
                             "\n等级:" + arraySecrecyLevel[pactold.SecrecyLevel.Value - 1] + "\t新数据:" + arraySecrecyLevel[pactnew.SecrecyLevel.Value - 1];
                    return restult;
            }
            return "暂无";
        }

        //Paper
        protected string Paper(string OpeType, int OpeId, string ReId)
        {
            string[] arraySecrecyLevel = new string[5] { "公开", "内部", "秘密", "机密", "管理员" };
            BLLPaper blPaper = new BLLPaper();
            BLLAchievement ach = new BLLAchievement();
            Paper paperold = blPaper.FindAll(OpeId);
            string restult = "题目:" + paperold.Subject +
                            "\n论文发表刊物名称:" + paperold.PublicJournalName +
                            "\n影响因子:" + paperold.ImpactFactor +
                            //"\n是否收录:" + (Convert.ToBoolean(paperold.Record) ? "是" : "否") +
                            "\n他引次数:" + paperold.HQuoteNum +
                            "\n论文所属单位:" + paperold.PaperUnit +
                            "\n发表日期:" + ThangeDate(paperold.PublicDate) +
                            "\n卷号:" + paperold.VolumesNum +
                            "\n期号:" + paperold.JournalNum +
                            "\n刊号:" + paperold.SerialNum +
                            "\n起始页码:" + paperold.StartPageNum.ToString() +
                            "\n结束页码:" + paperold.EndPageNum.ToString() +
                            "\n收录情况:" + paperold.RetrieveSituation +
                            "\n刊物级别:" + paperold.PaperRank +
                            //"\n论文形式:" + paperold.PaperForm +
                            "\n收录号:" + paperold.IncludeNum +
                            "\n引用次数:" + paperold.QuoteNum.ToString() +
                            "\n成果:" + ach.FindAchieveName(Convert.ToInt32(paperold.AchievementID)) +
                            "\n论文作者:" + paperold.PaperPeople +
                             "\n等级:" + arraySecrecyLevel[paperold.SecrecyLevel.Value - 1];
            switch (OpeType)
            {
                case "删除": return restult;
                case "添加": return restult;
                case "更新":
                    Paper papernew = blPaper.FindAll(Convert.ToInt32(ReId));
                    restult = "题目:" + paperold.Subject + "\t新数据:" + papernew.Subject +
                            "\n论文发表刊物名称:" + paperold.PublicJournalName + "\t新数据:" + papernew.PublicJournalName +
                            "\n影响因子:" + paperold.ImpactFactor + "\t新数据:" + papernew.ImpactFactor +
                            //"\n是否收录:" + (Convert.ToBoolean(paperold.Record) ? "是" : "否") + "\t新数据:" + (Convert.ToBoolean(papernew.Record) ? "是" : "否") +
                            "\n他引次数:" + paperold.HQuoteNum + "\t新数据:" + papernew.HQuoteNum +
                            "\n论文所属单位:" + paperold.PaperUnit + "\t新数据:" + papernew.PaperUnit +
                            "\n发表日期:" + ThangeDate(paperold.PublicDate) + "\t新数据:" + ThangeDate(papernew.PublicDate) +
                            "\n卷号:" + paperold.VolumesNum + "\t新数据:" + papernew.VolumesNum +
                            "\n期号:" + paperold.JournalNum + "\t新数据:" + papernew.JournalNum +
                            "\n刊号:" + paperold.SerialNum + "\t新数据:" + papernew.SerialNum +
                            "\n起始页码:" + paperold.StartPageNum.ToString() + "\t新数据:" + papernew.StartPageNum.ToString() +
                            "\n结束页码:" + paperold.EndPageNum.ToString() + "\t新数据:" + papernew.EndPageNum.ToString() +
                            "\n收录情况:" + paperold.RetrieveSituation + "\t新数据:" + papernew.RetrieveSituation +
                            "\n刊物级别:" + paperold.PaperRank + "\t新数据:" + papernew.PaperRank +
                           // "\n论文形式:" + paperold.PaperForm + "\t新数据:" + papernew.PaperForm +
                            "\n收录号:" + paperold.IncludeNum + "\t新数据:" + papernew.IncludeNum +
                            "\n引用次数:" + paperold.QuoteNum.ToString() + "\t新数据:" + papernew.QuoteNum.ToString() +
                            "\n成果:" + ach.FindAchieveName(Convert.ToInt32(paperold.AchievementID)) + "\t新数据:" + ach.FindAchieveName(Convert.ToInt32(papernew.AchievementID)) +
                            "\n论文作者:" + paperold.PaperPeople + "\t新数据:" + papernew.PaperPeople +
                             "\n等级:" + arraySecrecyLevel[paperold.SecrecyLevel.Value - 1] + "\t新数据:" + arraySecrecyLevel[papernew.SecrecyLevel.Value - 1];
                    return restult;
            }
            return "暂无";
        }

        //Patent
        protected string Patent(string OpeType, int OpeId, string ReId)
        {
            string[] arraySecrecyLevel = new string[5] { "公开", "内部", "秘密", "机密", "管理员" };
            BLLPatent blPatent = new BLLPatent();
            BLLAchievement ach = new BLLAchievement();
            Patent patentold = blPatent.FindByPatentID(OpeId);
            string restult = "专利名称:" + patentold.PatentName + 
                            "\n专利号:" + patentold.PatentNumber + 
                            //"\n类别:" + patentold.PatentType + 
                            "\n申请时间:" + ThangeDate(patentold.ApplicationTime) + 
                            "\n证书号:" + patentold.CertificateNumber + 
                            "\n授权时间:" + ThangeDate(patentold.AccreditTime) + 
                            "\n成果名称:" + ach.FindAchieveName(Convert.ToInt32(patentold.AchievementID)) + 
                            "\n授予机构:" + patentold.GivenUnit + 
                            "\n状态:" + patentold.State +
                            "\n单位:" + patentold.PatentDepartment +
                             "\n等级:" + arraySecrecyLevel[patentold.SecrecyLevel.Value - 1];
            switch (OpeType)
            {
                case "删除": return restult;
                case "添加": return restult;
                case "更新":
                    Patent patentnew = blPatent.FindByPatentID(Convert.ToInt32(ReId));
                    restult = "专利名称:" + patentold.PatentName + "\t新数据:" + patentnew.PatentName +
                            "\n专利号:" + patentold.PatentNumber + "\t新数据:" + patentnew.PatentNumber +
                           // "\n类别:" + patentold.PatentType + "\t新数据:" + patentnew.PatentType +
                            "\n申请时间:" + ThangeDate(patentold.ApplicationTime) + "\t新数据:" + ThangeDate(patentnew.ApplicationTime) +
                            "\n证书号:" + patentold.CertificateNumber + "\t新数据:" + patentnew.CertificateNumber +
                            "\n授权时间:" + ThangeDate(patentold.AccreditTime) + "\t新数据:" + ThangeDate(patentnew.AccreditTime) +
                            "\n成果名称:" + ach.FindAchieveName(Convert.ToInt32(patentold.AchievementID)) + "\t新数据:" + ach.FindAchieveName(Convert.ToInt32(patentnew.AchievementID)) +
                            "\n授予机构:" + patentold.GivenUnit + "\t新数据:" + patentnew.GivenUnit +
                            "\n状态:" + patentold.State + "\t新数据:" + patentnew.State +
                            "\n单位:" + patentold.PatentDepartment + "\t新数据:" + patentnew.PatentDepartment +
                             "\n等级:" + arraySecrecyLevel[patentold.SecrecyLevel.Value - 1] + "\t新数据:" + arraySecrecyLevel[patentnew.SecrecyLevel.Value - 1];
                    return restult;
            }
            return "暂无";
        }

        //Photos
        protected void Photos(string OpeType, int OpeId, string ReId)
        {
            BLLPhotos blPhotos = new BLLPhotos();
            BLLAttachment bllAttachment = new BLLAttachment();
            switch (OpeType)
            {
                case "添加":
                    string FindPath = bllAttachment.FindPath(blPhotos.FindAttachmentID(OpeId)); 
                    imgPhotonew.ImageUrl = FindPath;
                    old.Text = "";
                    imgPhotoold.Hidden = true;
                    break;
                case "更新":
                    imgPhotoold.ImageUrl = bllAttachment.FindPath(blPhotos.FindAttachmentID(OpeId));
                    imgPhotonew.ImageUrl = bllAttachment.FindPath(blPhotos.FindAttachmentID(Convert.ToInt32(ReId)));
                    break;
            }
        }

        //Project
        protected string Project(string OpeType, int OpeId, string ReId)
        {
            string[] arraySecrecyLevel = new string[5] { "公开", "内部", "秘密", "机密", "管理员" };
            BLLProject blProject = new BLLProject();
            BLLAgency blag = new BLLAgency();
            Common.Entities.Project proold = blProject.FindByid(OpeId);
            string restult = "项目名称:" + proold.ProjectName + 
                        "\n项目所属机构:" + blag.FindAgenName(Convert.ToInt32(proold.AgencyID)) + 
                        "\n承接单位:" + proold.AcceptUnit + 
                        "\n来源单位:" + proold.SourceUnit + 
                        "\n项目分类:" + proold.ProjectSortName + 
                        "\n项目状态:" + proold.ProjectState + 
                        "\n项目经费:" + proold.ApprovedMoney + 
                        "\n到账金额:" + proold.GetMoney + 
                        "\n管理费比例"+proold.ManageMoney+
                        "\n合作形式:" + proold.CooperationForms + 
                        "\n项目级别:" + proold.ProjectLevel + 
                        "\n实际负责人:" + proold.ProjectHeads + 
                        "\n开始时间:" + ThangeDate(proold.StartTime) + 
                        "\n结束时间:" + ThangeDate(proold.EndTime) + 
                        "\n预期成果:" + proold.ExpecteResults + 
                        "\n来款单位:" + proold.GivenMoneyUnits + 
                        "\n项目性质:" + proold.ProjectNature + 
                        "\n预期完成时间:" + proold.ExpectEndTime + 
                        "\n合同编号:" + proold.PactNum +
                        "\n课题编号:" + proold.TaskNum +
                        "\n等级:" + arraySecrecyLevel[proold.SecrecyLevel.Value - 1];
            switch (OpeType)
            {
                case "删除": return restult;
                case "添加": return restult;
                case "更新":
                    Common.Entities.Project pronew = blProject.FindByid(Convert.ToInt32(ReId));

                    restult = "项目名称:" + proold.ProjectName + "\t新数据:" + pronew.ProjectName +
                        "\n项目所属机构:" + blag.FindAgenName(Convert.ToInt32(proold.AgencyID)) + "\t新数据:" + blag.FindAgenName(Convert.ToInt32(pronew.AgencyID)) +
                        "\n承接单位:" + proold.AcceptUnit + "\t新数据:" + pronew.AcceptUnit +
                        "\n来源单位:" + proold.SourceUnit + "\t新数据:" + pronew.SourceUnit +
                        "\n项目分类:" + proold.ProjectSortName + "\t新数据:" + pronew.ProjectSortName +
                        "\n项目状态:" + proold.ProjectState + "\t新数据:" + pronew.ProjectNature +
                        "\n项目经费:" + proold.ApprovedMoney + "\t新数据:" + pronew.ApprovedMoney +
                        "\n到账金额:" + proold.GetMoney + "\t新数据:" + pronew.GetMoney +
                        "\n合作形式:" + proold.CooperationForms + "\t新数据:" + pronew.CooperationForms +
                        "\n项目级别:" + proold.ProjectLevel + "\t新数据:" + pronew.ProjectLevel +
                        "\n实际负责人:" + proold.ProjectHeads + "\t新数据:" + pronew.ProjectHeads +
                        "\n开始时间:" + ThangeDate(proold.StartTime) + "\t新数据:" + ThangeDate(pronew.StartTime) +
                        "\n结束时间:" + ThangeDate(proold.EndTime) + "\t新数据:" + ThangeDate(pronew.EndTime) +
                        "\n预期成果:" + proold.ExpecteResults + "\t新数据:" + pronew.ExpecteResults +
                        "\n来款单位:" + proold.GivenMoneyUnits + "\t新数据:" + pronew.GivenMoneyUnits +
                        "\n项目性质:" + proold.ProjectNature + "\t新数据:" + pronew.ProjectNature +
                        "\n预期完成时间:" + ThangeDate(proold.ExpectEndTime) + "\t新数据:" + ThangeDate(pronew.ExpectEndTime) +
                        "\n合同编号:" + proold.PactNum + "\t新数据:" + pronew.PactNum +
                        "\n课题编号:" + proold.TaskNum + "\t新数据:" + pronew.TaskNum +
                         "\n等级:" + arraySecrecyLevel[proold.SecrecyLevel.Value - 1] + "\t新数据:" + arraySecrecyLevel[pronew.SecrecyLevel.Value - 1];
                    return restult;
            }
            return "暂无";
        }

        //ProjectImportantNode
        protected string ProjectImportantNode(string OpeType, int OpeId, string ReId)
        {
            string[] arraySecrecyLevel = new string[5] { "四级", "三级", "二级", "一级", "管理员" };
            BLLProjectImportantNode blProNode = new BLLProjectImportantNode();
            BLLProject blpro = new BLLProject();
            ProjectImportantNode pronodeold = blProNode.FindProjectImportant(OpeId, false);//+ "\n时间:" + ThangeDate(pronodeold.Time)
            string restult = "节点名称:" + pronodeold.MissionName  + "\n项目:" + blpro.SelectProjectName(Convert.ToInt32(pronodeold.ProjectID)) +
                        "\n等级:" + arraySecrecyLevel[pronodeold.SecrecyLevel.Value - 1];

            switch (OpeType)
            {
                case "删除":
                    return restult;
                case "添加":
                    return restult;
                case "更新":
                    ProjectImportantNode pronodenew = blProNode.FindProjectImportant(Convert.ToInt32(ReId), false);//+ "\n时间:" + ThangeDate(pronodeold.Time)+ "\t新数据:" + ThangeDate(pronodenew.Time)
                    restult = "节点名称:" + pronodeold.MissionName + "\t新数据:" + pronodenew.MissionName   + "\n项目:" + blpro.SelectProjectName(Convert.ToInt32(pronodeold.ProjectID)) + "\t新数据:" + blpro.SelectProjectName(Convert.ToInt32(pronodenew.ProjectID)) +
                         "\n等级:" + arraySecrecyLevel[pronodeold.SecrecyLevel.Value - 1] + "\t新数据:" + arraySecrecyLevel[pronodenew.SecrecyLevel.Value - 1];
                    return restult;
                default:
                    return "";
            }
        }

        //ScienceReport
        protected string ScienceReport(string OpeType, int OpeId, string ReId)
        {
            string[] arraySecrecyLevel = new string[5] { "四级", "三级", "二级", "一级", "管理员" };
            BLLScienceReport blSci = new BLLScienceReport();
            BLLAcademicMeeting blaca = new BLLAcademicMeeting();
            BLLAgency blag = new BLLAgency();
            ScienceReport sciencenew = blSci.FindByScienceReportID(OpeId, false).FirstOrDefault();
            string result = "报告名称:" + sciencenew.SReportName + 
                            "\n报告人:" + sciencenew.SReportPeople + 
                            "\n报告时间:" + ThangeDate(sciencenew.SReportTime) + 
                            "\n报告地点:" + sciencenew.SReportPlace + 
                            "\n所属会议:" + blaca.FindByAcademicMeetingID(Convert.ToInt32(sciencenew.MeetingID), true) + 
                            "\n机构:" + blag.FindAgenName(Convert.ToInt32(sciencenew.AgencyID)) +
                            "\n等级:" + arraySecrecyLevel[sciencenew.SecrecyLevel.Value - 1];
            switch (OpeType)
            {
                case "删除":
                    return result;
                case "添加":
                    return result;
                case "更新":
                    ScienceReport scienceold = blSci.FindByScienceReportID(Convert.ToInt32(ReId), false).FirstOrDefault();
                    result = "报告名称:" + sciencenew.SReportName + "\t新数据:" + scienceold.SReportName + 
                        "\n报告人:" + sciencenew.SReportPeople + "\t新数据:" + scienceold.SReportPeople + 
                        "\n报告时间:" + ThangeDate(sciencenew.SReportTime) + "\t新数据:" + ThangeDate(scienceold.SReportTime) + 
                        "\n报告地点:" + sciencenew.SReportPlace + "\t新数据:" + scienceold.SReportPlace + 
                        "\n所属会议:" + blaca.FindByAcademicMeetingID(Convert.ToInt32(sciencenew.MeetingID), true) + "\t新数据:" + blaca.FindByAcademicMeetingID(Convert.ToInt32(scienceold.MeetingID), true) +
                        "\n机构:" + blag.FindAgenName(Convert.ToInt32(sciencenew.AgencyID)) + "\t新数据:" + blag.FindAgenName(Convert.ToInt32(scienceold.AgencyID)) +
                            "\n等级:" + arraySecrecyLevel[sciencenew.SecrecyLevel.Value - 1] + "\t新数据:" + arraySecrecyLevel[scienceold.SecrecyLevel.Value - 1];
                    return result;
                default:
                    return null;
            }
        }

        //SocialPartTime
        protected string SocialPartTime(string OpeType, int OpeId, string ReId)
        {
            string[] arraySecrecyLevel = new string[5] { "四级", "三级", "二级", "一级", "管理员" };
            BLLSocialPartTime blSoc = new BLLSocialPartTime();
            SocialPartTime socianew = blSoc.FindBySocialID(OpeId, 5, false);
            string restult = "级别名称:" + socianew.LevelName +
                            "\n兼职职位名称:" + socianew.PartTimeName +
                            "\n授予部门:" + socianew.PartUnitName +
                            "\n任期:" + socianew.Terms +
                            "\n批准时间:" + ThangeDate(socianew.ApproveTime) +
                            "\n人员:" + bluser.FindUserName(socianew.UserInfoID) +
                            "\n等级:" + arraySecrecyLevel[socianew.SecrecyLevel.Value - 1];

            switch (OpeType)
            {
                case "删除":
                    return restult;
                case "添加":
                    return restult;
                case "更新":
                    SocialPartTime sociaold = blSoc.FindBySocialID(Convert.ToInt32(ReId), 5, false);
                    restult = "级别名称:" + socianew.LevelName + "\t新数据:" + sociaold.LevelName +
                            "\n兼职职位名称:" + socianew.PartTimeName + "\t新数据:" + sociaold.PartTimeName +
                            "\n授予部门:" + socianew.PartUnitName + "\t新数据:" + sociaold.PartUnitName + 
                            "\n任期:" + socianew.Terms + "\t新数据:" + sociaold.Terms +
                            "\n批准时间:" + ThangeDate(socianew.ApproveTime) + "\t新数据:" + ThangeDate(sociaold.ApproveTime) +
                            "\n人员:" + bluser.FindUserName(socianew.UserInfoID) + "\t新数据:" + bluser.FindUserName(sociaold.UserInfoID) +
                            "\n等级:" + arraySecrecyLevel[socianew.SecrecyLevel.Value - 1] + "\t新数据:" + arraySecrecyLevel[sociaold.SecrecyLevel.Value - 1];
                    return restult;
            }
            return "暂无";
        }

        //SpeakClass
        protected string SpeakClass(string OpeType, int OpeId, string ReId)
        {
            string[] arraySecrecyLevel = new string[5] { "四级", "三级", "二级", "一级", "管理员" };
            BLLSpeakClass blSpe = new BLLSpeakClass();
            SpeakClass speold = blSpe.Find(OpeId);
            string result = "人员:" + bluser.FindUserName(speold.UserInfoID) + 
                            "\n课程名称:" + speold.ClassName +
                            "\n专业:" + speold.Specialty +
                            "\n教学对象:" + speold.TeachingDegree +
                            "\n教学时间:" + ThangeDate(speold.TeachingTime) +
                            "\n年级:" + speold.Grade +
                            "\n等级:" + arraySecrecyLevel[speold.SecrecyLevel.Value - 1];
            switch (OpeType)
            {
                case "删除": return result;
                case "添加": return result;
                case "更新":
                    SpeakClass spenew = blSpe.Find(Convert.ToInt32(ReId));
                    result = "人员:" + bluser.FindUserName(speold.UserInfoID) + "\t新数据:" + bluser.FindUserName(spenew.UserInfoID) +
                            "\n课程名称:" + speold.ClassName + "\t新数据:" + spenew.ClassName +
                            "\n专业:" + speold.Specialty + "\t新数据:" + spenew.Specialty +
                            "\n教学对象:" + speold.TeachingDegree + "\t新数据:" + spenew.TeachingDegree +
                            "\n教学时间:" + ThangeDate(speold.TeachingTime) + "\t新数据:" + ThangeDate(spenew.TeachingTime) +
                            "\n年级:" + speold.Grade + "\t新数据:" + spenew.Grade +
                            "\n等级:" + arraySecrecyLevel[speold.SecrecyLevel.Value - 1] + "\t新数据:" + arraySecrecyLevel[spenew.SecrecyLevel.Value - 1];
                    return result;
            }
            return "暂无";
        }

        //Student
        protected string Student(string OpeType, int OpeId, string ReId)
        {
            string[] arraySecrecyLevel = new string[5] { "四级", "三级", "二级", "一级", "管理员" };
            BLLStudent blStu = new BLLStudent();
            Student stuold = blStu.FindStudents(OpeId);
            string result = "学号:" + stuold.Sno +
                            "\n姓名:" + stuold.Sname +
                            "\n性别:" + (Convert.ToBoolean(stuold.Sex) ? "男" : "女") +
                            "\n证件类型:" + stuold.DocumentType +
                            "\n证件号码:" + stuold.DocumentNumber +
                            "\n联系方式:" + stuold.Contact +
                            "\n是否毕业:" + (Convert.ToBoolean(stuold.IsGraduation) ? "已毕业" : "未毕业") +
                            "\n专业:" + stuold.Specialty +
                            "\n研究方向:" + stuold.SResearch +
                            "\n毕业去向:" + stuold.SGraduationDirection +
                            "\n类型:" + stuold.Type +
                            "\n入学时间:" + ThangeDate(stuold.EnterTime) +
                            "\n毕业时间:" + ThangeDate(stuold.GraduationTime) +
                            "\n授课老师:" + bluser.FindUserName(stuold.UserInfoID) +
                            "\n等级:" + arraySecrecyLevel[stuold.SecrecyLevel.Value - 1];
            switch (OpeType)
            {
                case "删除": return result;
                case "添加": return result;
                case "更新":
                    int i = Convert.ToInt32(ReId);
                    Student stunew = blStu.FindStudents(i);
                    result = "学号:" + stuold.Sno + "\t新数据:" + stunew.Sno +
                            "\n姓名:" + stuold.Sname + "\t新数据:" + stunew.Sname +
                            "\n性别:" + (Convert.ToBoolean(stuold.Sex) ? "男" : "女") + "\t新数据:" + (Convert.ToBoolean(stunew.Sex) ? "男" : "女") +
                            "\n证件类型:" + stuold.DocumentType + "\t新数据:" + stunew.DocumentType +
                            "\n证件号码:" + stuold.DocumentNumber + "\t新数据:" + stunew.DocumentNumber + 
                            "\n联系方式:" + stuold.Contact + "\t新数据:" + stunew.Contact +
                            "\n是否毕业:" + (Convert.ToBoolean(stuold.IsGraduation) ? "已毕业" : "未毕业") + "\t新数据:" + (Convert.ToBoolean(stunew.IsGraduation) ? "已毕业" : "未毕业") +
                            "\n专业:" + stuold.Specialty + "\t新数据:" + stunew.Specialty +
                            "\n研究方向:" + stuold.SResearch + "\t新数据:" + stunew.SResearch +
                            "\n毕业去向:" + stuold.SGraduationDirection + "\t新数据:" + stunew.SGraduationDirection +
                            "\n类型:" + stuold.Type + "\t新数据:" + stunew.Type +
                            "\n入学时间:" + ThangeDate(stuold.EnterTime) + "\t新数据:" + ThangeDate(stunew.EnterTime) + 
                            "\n毕业时间:" + ThangeDate(stuold.GraduationTime) + "\t新数据:" +  ThangeDate(stunew.GraduationTime) +
                            "\n授课老师:" + bluser.FindUserName(stuold.UserInfoID) + "\t新数据:" + bluser.FindUserName(stunew.UserInfoID) +
                            "\n等级:" + arraySecrecyLevel[stuold.SecrecyLevel.Value - 1] + "\t新数据:" + arraySecrecyLevel[stunew.SecrecyLevel.Value - 1];
                    return result;
            }
            return "暂无";
        }

        //UnitInspect
        protected string UnitInspect(string OpeType, int OpeId, string ReId)
        {
            string[] arraySecrecyLevel = new string[5] { "四级", "三级", "二级", "一级", "管理员" };
            BLLUnitInspect blUnit = new BLLUnitInspect();
            BLLAgency blag = new BLLAgency();
            Common.Entities.UnitInspect unitold = blUnit.FindInspectInfo(OpeId, false);
            string restult = "姓名:" + unitold.InspectName +
                             "\n工作单位:" + unitold.WorkPlace +
                             "\n职称/职务:" + unitold.Duty +
                             "\n时间:" + ThangeDate(unitold.InspectTime) +
                             "\n参观内容:" + unitold.VisitContent +
                            "\n等级:" + arraySecrecyLevel[unitold.SecrecyLevel.Value - 1];
            switch (OpeType)
            {
                case "删除": return restult;
                case "添加": return restult;
                case "更新":
                    int i = Convert.ToInt32(ReId);
                    Common.Entities.UnitInspect unitnew = blUnit.FindInspectInfo(i, false);
                    restult = "姓名:" + unitold.InspectName + "\t新数据:" + unitnew.InspectName +
                             "\n工作单位:" + unitold.WorkPlace + "\t新数据:" + unitnew.WorkPlace +
                             "\n职称/职务:" + unitold.Duty + "\t新数据:" + unitnew.Duty +
                             "\n时间:" + ThangeDate(unitold.InspectTime) + "\t新数据:" + ThangeDate(unitnew.InspectTime) +
                             "\n参观内容:" + unitold.VisitContent + "\t新数据:" + unitnew.VisitContent +
                            "\n等级:" + arraySecrecyLevel[unitold.SecrecyLevel.Value - 1] + "\t新数据:" + arraySecrecyLevel[unitnew.SecrecyLevel.Value - 1];
                    return restult;
            }
            return "暂无";
        }

        //UnitLectures
        protected string UnitLectures(string OpeType, int OpeId, string ReId)
        {
            string[] arraySecrecyLevel = new string[5] { "四级", "三级", "二级", "一级", "管理员" };
            BLLUnitLectures blUnit = new BLLUnitLectures();
            Common.Entities.UnitLectures unitlecold = blUnit.FindByUnitLecturesID(OpeId);
            string restult = "姓名:" + unitlecold.LecturesName +
                            "\n工作单位:" + unitlecold.WorkUnit +
                            "\n报告名称:" + unitlecold.UReportName +
                            "\n时间:" + ThangeDate(unitlecold.LecturesTime) +
                            "\n地点:" + unitlecold.LecturesPlace +
                            "\n听众人数:" + unitlecold.listenerNumber.ToString() +
                            "\n等级:" + arraySecrecyLevel[unitlecold.SecrecyLevel.Value - 1];
            switch (OpeType)
            {
                case "删除": return restult;
                case "添加": return restult;
                case "更新":
                    int i = Convert.ToInt32(ReId);
                    Common.Entities.UnitLectures unitlecnew = blUnit.FindByUnitLecturesID(OpeId);
                    restult = "姓名:" + unitlecold.LecturesName + "\t新数据:" + unitlecnew.LecturesName +
                            "\n工作单位:" + unitlecold.WorkUnit + "\t新数据:" + unitlecnew.WorkUnit +
                            "\n报告名称:" + unitlecold.UReportName + "\t新数据:" + unitlecnew.UReportName +
                            "\n时间:" + ThangeDate(unitlecold.LecturesTime) + "\t新数据:" + ThangeDate(unitlecnew.LecturesTime) +
                            "\n地点:" + unitlecold.LecturesPlace + "\t新数据:" + unitlecnew.LecturesPlace +
                            "\n听众人数:" + unitlecold.listenerNumber.ToString() + "\t新数据:" + unitlecnew.listenerNumber.ToString() +
                            "\n等级:" + arraySecrecyLevel[unitlecold.SecrecyLevel.Value - 1] + "\t新数据:" + arraySecrecyLevel[unitlecnew.SecrecyLevel.Value - 1];
                    return restult;
            }
            return "暂无";
        }

        //User
        protected string Users(string OpeType, int OpeId, string ReId)
        {
            string[] arraySecrecyLevel = new string[5] { "四级", "三级", "二级", "一级", "管理员" };
            BLLAgency blag = new BLLAgency();
            UserInfo userold = bluser.Find(OpeId, false);
            string restult =// "用户编号:" + userold.UserInfoBH.ToString() +
                            "登录名:" + userold.LoginName.ToString() +
                            "\n用户名:" + userold.UserName.ToString() +
                            "\n邮箱地址:" + userold.Email.ToString() +
                            "\n电话号:" + userold.TeleNum.ToString() +
                            "\n性别:" + (Convert.ToBoolean(userold.Sex) ? "男" : "女") +
                            "\n民族:" + userold.Nation.ToString() +
                            "\n籍贯:" + userold.Hometown.ToString() +
                            "\n出生年月:" + ThangeDate(userold.Birth) +
                            "\n项目所属单位:" + blag.FindAgenName(userold.AgencyID) +
                            "\n职称:" + userold.JobTitle.ToString() +
                            "\n家庭号码:" + userold.HomeNum.ToString() +
                            "\n办公电话:" + userold.OfficeNum.ToString() +
                            "\n证件类型:" + userold.DocumentsType.ToString() +
                            "\n证件号码:" + userold.DocumentsNum.ToString() +
                            "\n政治面貌:" + userold.PoliticalStatus.ToString() +
                            "\n学历:" + userold.Education.ToString() +
                            "\n学位:" + userold.Degree.ToString() +
                            "\n研究方向:" + userold.ResearchDirection.ToString() +
                            "\n专长:" + userold.Specialty.ToString() +
                            "\n婚姻状况:" + (Convert.ToBoolean(userold.Marriage) ? "已婚" : "未婚") +
                            "\n传真:" + userold.Fax.ToString() +
                            "\n家庭住址:" + userold.HomeAddress.ToString() +
                            "\n邮政编码:" + userold.PostalCode.ToString() +
                            "\nqq号码:" + userold.qqNum.ToString() +
                            "\n单位名称:" + userold.UnitName.ToString() +
                             //"\n员工类型:" + userold.StaffType.ToString() +
                            "\n户籍地:" + userold.Domicile.ToString() +
                            "\n职称获得时间:" + ThangeDate(userold.JobTitleTime) +
                            "\n政治面貌获得时间:" + ThangeDate(userold.PoliticalStatusTime) +
                            "\n是否为博士生导师:" + (Convert.ToBoolean(userold.IsDocdorTeacher) ? "是" : "否") +
                            "\n是否为硕士生导师:" + (Convert.ToBoolean(userold.IsMasteTeacher) ? "是" : "否") +
                            "\n硕士生导师取得时间:" + ThangeDate(userold.MasterTeacherTime) +
                            "\n博士生导师取得时间:" + ThangeDate(userold.DoctorTeacherTime) +
                            "\n学科分类:" + userold.SubjectSortName.ToString() +
                            "\n行政级别:" + userold.AdministrativeLevelName.ToString() +
                            "\n等级:" + arraySecrecyLevel[userold.SecrecyLevel.Value - 1];
            switch (OpeType)
            {
                case "删除": return restult;
                case "添加": return restult;
                case "更新":
                    UserInfo usernew = bluser.Find(Convert.ToInt32(ReId), false);
                    restult = //"用户编号:" + userold.UserInfoBH + "\t新数据:" + usernew.UserInfoBH +
                            "登录名:" + userold.LoginName + "\t新数据:" + usernew.LoginName +
                            "\n用户名:" + userold.UserName + "\t新数据:" + usernew.UserName +
                            "\n邮箱地址:" + userold.Email + "\t新数据:" + usernew.Email +
                            "\n电话号:" + userold.TeleNum + "\t新数据:" + usernew.TeleNum +
                            "\n性别:" + (Convert.ToBoolean(userold.Sex) ? "男" : "女") + "\t新数据:" + (Convert.ToBoolean(usernew.Sex) ? "男" : "女") +
                            "\n民族:" + userold.Nation + "\t新数据:" + usernew.Nation +
                            "\n籍贯:" + userold.Hometown + "\t新数据:" + usernew.Hometown +
                            "\n出生年月:" + ThangeDate(userold.Birth) + "\t新数据:" + ThangeDate(usernew.Birth) +
                            "\n项目所属单位:" + blag.FindAgenName(userold.AgencyID) + "\t新数据:" + blag.FindAgenName(usernew.AgencyID) +
                            "\n职称:" + userold.JobTitle + "\t新数据:" + usernew.JobTitle +
                            "\n家庭号码:" + userold.HomeNum + "\t新数据:" + usernew.HomeNum +
                            "\n办公电话:" + userold.OfficeNum + "\t新数据:" + usernew.OfficeNum +
                            "\n证件类型:" + userold.DocumentsType + "\t新数据:" + usernew.DocumentsType +
                            "\n证件号码:" + userold.DocumentsNum + "\t新数据:" + usernew.DocumentsNum + 
                            "\n政治面貌:" + userold.PoliticalStatus + "\t新数据:" + usernew.PoliticalStatus +
                            "\n学历:" + userold.Education + "\t新数据:" + usernew.Education +
                            "\n学位:" + userold.Degree + "\t新数据:" + usernew.Degree +
                            "\n研究方向:" + userold.ResearchDirection + "\t新数据:" + usernew.ResearchDirection +
                            "\n专长:" + userold.Specialty + "\t新数据:" + usernew.Specialty +
                            "\n婚姻状况:" + (Convert.ToBoolean(userold.Marriage) ? "已婚" : "未婚") + "\t新数据:" + (Convert.ToBoolean(usernew.Marriage) ? "已婚" : "未婚") +
                            "\n传真:" + userold.Fax + "\t新数据:" + usernew.Fax +
                            "\n家庭住址:" + userold.HomeAddress + "\t新数据:" + usernew.HomeAddress +
                            "\n邮政编码:" + userold.PostalCode + "\t新数据:" + usernew.PostalCode +
                            "\nqq号码:" + userold.qqNum + "\t新数据:" + usernew.qqNum +
                            "\n单位名称:" + userold.UnitName + "\t新数据:" + usernew.UnitName +
                             //"\n员工类型:" + userold.StaffType + "\t新数据:" + usernew.StaffType +
                            "\n户籍地:" + userold.Domicile + "\t新数据:" + usernew.Domicile +
                            "\n职称获得时间:" + ThangeDate(userold.JobTitleTime) + "\t新数据:" + ThangeDate(usernew.JobTitleTime) +
                            "\n政治面貌获得时间:" + ThangeDate(userold.PoliticalStatusTime) + "\t新数据:" + ThangeDate(usernew.PoliticalStatusTime) +
                            "\n是否为博士生导师:" + (Convert.ToBoolean(userold.IsDocdorTeacher) ? "是" : "否") + "\t新数据:" + (Convert.ToBoolean(usernew.IsDocdorTeacher) ? "是" : "否") +
                            "\n是否为硕士生导师:" + (Convert.ToBoolean(userold.IsMasteTeacher) ? "是" : "否") + "\t新数据:" + (Convert.ToBoolean(usernew.IsMasteTeacher) ? "是" : "否") +
                            "\n硕士生导师取得时间:" + ThangeDate(userold.MasterTeacherTime) + "\t新数据:" + ThangeDate(usernew.MasterTeacherTime) +
                            "\n博士生导师取得时间:" + ThangeDate(userold.DoctorTeacherTime) + "\t新数据:" + ThangeDate(usernew.DoctorTeacherTime) +
                            "\n学科分类:" + userold.SubjectSortName + "\t新数据:" + usernew.SubjectSortName +
                            "\n行政级别:" + userold.AdministrativeLevelName + "\t新数据:" + usernew.AdministrativeLevelName +
                            "\n等级:" + arraySecrecyLevel[userold.SecrecyLevel.Value - 1] + "\t新数据:" + arraySecrecyLevel[usernew.SecrecyLevel.Value - 1];
                    return restult;
            }
            return "暂无";
        }

        //WorkExperience
        protected string WorkExperience(string OpeType, int OpeId, string ReId)
        {
            string[] arraySecrecyLevel = new string[5] { "四级", "三级", "二级", "一级", "管理员" };
            BLLWorkExperience blWor = new BLLWorkExperience();
            WorkExperience worold = blWor.Find(OpeId);
            string restult = "开始时间:" + ThangeDate(worold.StartTime) +
                            "\n结束时间:" + ThangeDate(worold.EndTime) +
                            "\n职务:" + worold.Post +
                            "\n职称:" + worold.JobTitle +
                            "\n工作单位:" + worold.WorkUnit +
                            "\n人员:" + bluser.FindUserName(worold.UserInfoID) +
                            "\n等级:" + arraySecrecyLevel[worold.SecrecyLevel.Value - 1];
            switch (OpeType)
            {
                case "删除": return restult;
                case "添加": return restult;
                case "更新":
                    int i = Convert.ToInt32(ReId);
                    WorkExperience wornew = blWor.Find(i);
                    restult = "开始时间:" + ThangeDate(worold.StartTime) + "\t新数据:" + ThangeDate(wornew.StartTime) +
                            "\n结束时间:" + ThangeDate(worold.EndTime) + "\t新数据:" + ThangeDate(wornew.EndTime) +
                            "\n职务:" + worold.Post + "\t新数据:" + wornew.Post +
                            "\n职称:" + worold.JobTitle + "\t新数据:" + wornew.JobTitle +
                            "\n工作单位:" + worold.WorkUnit + "\t新数据:" + wornew.WorkUnit +
                            "\n人员:" + bluser.FindUserName(worold.UserInfoID) + "\t新数据:" + bluser.FindUserName(wornew.UserInfoID) +
                            "\n等级:" + arraySecrecyLevel[worold.SecrecyLevel.Value - 1] + "\t新数据:" + arraySecrecyLevel[wornew.SecrecyLevel.Value - 1];
                    return restult;
            }
            return "暂无";
        }

        //WorkPlanSummary
        protected string WorkPlanSummary(string OpeType, int OpeId, string ReId)
        {
            string[] arraySecrecyLevel = new string[5] { "四级", "三级", "二级", "一级", "管理员" };
            BLLWorkPlanSummary blWor = new BLLWorkPlanSummary();
            BLLAgency blag = new BLLAgency();
            Common.Entities.WorkPlanSummary worpl = blWor.FindByWorkPlanSummaryID(OpeId);
            string result = "分类:" + worpl.Sort +
                            "\n机构:" + blag.FindAgenName(worpl.AgencyID) +
                            "\n人员:" + bluser.FindUserName(worpl.UserInfoID) +
                            "\n计划名称:" + worpl.PlanWork +
                            "\n时间:" + ThangeDate(worpl.Time) +
                            "\n等级:" + arraySecrecyLevel[worpl.SecrecyLevel.Value - 1];
            switch (OpeType)
            {
                case "删除": return result;
                case "添加": return result;
                case "更新":
                    break;
            }
            return "暂无";
        }

    }
}