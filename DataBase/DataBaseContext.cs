/*
 * 编写人：未知
 * 时间：未知
 * 修改履历：    1、修改人：吕博扬
 *                 修改时间：2015年12月5日
 *                 修改内容：添加对表ProjectFiles的支持
 */
using Common.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext()
            : base("WDFramework")
        {
            Database.SetInitializer<DataBaseContext>(null);
            //是否启用延迟加载:  
            //  true:   延迟加载（Lazy Loading）：获取实体时不会加载其导航属性，一旦用到导航属性就会自动加载  
            //  false:  直接加载（Eager loading）：通过 Include 之类的方法显示加载导航属性，获取实体时会即时加载通过 Include 指定的导航属性  
            this.Configuration.LazyLoadingEnabled = true;
            this.Configuration.AutoDetectChangesEnabled = true;  //自动监测变化，默认值为 true  
        }
        public DbSet<TreeNav> TreeNavContext { get; set; }
        public DbSet<ToolBar> ToolBarContext { get; set; }      
        public DbSet<UserInfo> UserInfoContext { get; set; }

        public DbSet<Agency> AgencyContext { get; set; }
        public DbSet<Education> EducationContext { get; set; }
        public DbSet<Honor> HonorContext { get; set; }
        public DbSet<Award> AwardContext { get; set; }
        public DbSet<WorkExperience> WorkExperienceContext { get; set; }
        public DbSet<Paper> PaperContext { get; set; }
        public DbSet<Patent> PatentContext { get; set; }
        public DbSet<Monograph> MonographContext { get; set; }
        public DbSet<SocialPartTime> SocialPartTimeContext { get; set; }
        public DbSet<Photos> PhotosContext { get; set; }
        public DbSet<SpeakClass> SpeakClassContext { get; set; }
       
        public DbSet<EduExperience> EduExperienceContext { get; set; }
        //public DbSet<TrainExperience> TrainExperienceContext { get; set; }
        public DbSet<Student> StudentContext { get; set; }
        public DbSet<Project> ProjectContext { get; set; }
        //public DbSet<VerticalProject> VerticalProjectContext { get; set; }
        
        //lby ↓
        public DbSet<ProjectFile> ProjectFileContext { get; set; }

        public DbSet<ProjectImportantNode> ProjectImportantNodeContext { get; set; }
        public DbSet<Announcement> AnnouncementContext { get; set; }
        public DbSet<Attachment> AttachmentContext { get; set; }
        public DbSet<Contract> ContractContext { get; set; }
        public DbSet<WorkPlanSummary> WorkPlanSummaryContext { get; set; }

        public DbSet<OperationLog> OperationLogContext { get; set; }
        public DbSet<Files> FilesContext { get; set; }
        public DbSet<AcademicMeeting> AcademicMeetingContext { get; set; }
        //public DbSet<MeetingReport> MeetingReportContext { get; set; }
        public DbSet<ScienceReport> ScienceReportContext { get; set; }
        public DbSet<UnitLectures> UnitLecturesContext { get; set; }
        public DbSet<UnitInspect> UnitInspectContext { get; set; }
        public DbSet<FutherStudy> FutherStudyContext { get; set; }
        public DbSet<DFurtherStudy> DFurtherStudyContext { get; set; }
        public DbSet<Achievement> AchievementContext { get; set; }
       //public DbSet<AchAppraisal> AchAppraisalContext { get; set; }
        public DbSet<AchievementCA> AchievementCAContext { get; set; }
        public DbSet<AchieveAward> AchieveAwardContext { get; set; }
        public DbSet<FundInformation> FundInformationContext { get; set; }
       // public DbSet<StaffPaper> StaffPaperContext { get; set; }
       // public DbSet<StaffMonograph> StaffMonographContext { get; set; }
        public DbSet<StaffAchieve> StaffAchieveContext { get; set; }
       // public DbSet<StaffAward> StaffAwardContext { get; set; }
       // public DbSet<StaffPatent> StaffPatentContext { get; set; }
        public DbSet<StaffDevote> StaffDevoteContext { get; set; }
        public DbSet<AttendMeeting> AttendMeetingContext { get; set; }
        public DbSet<LibraryRecord> LibraryRecordContext { get; set; }
        //public DbSet<DicAgencySort> DicAgencySortContext { get; set; }
        //public DbSet<AnnouncementSort> AnnouncementSortContext { get; set; }
       // public DbSet<ProjectSort> ProjectSortContext { get; set; }
       // public DbSet<DocumentSort> DocumentSortContext { get; set; }
       // public DbSet<FundingPurposeSort> FundingPurposeSortContext { get; set; }
        // public DbSet<MeetingSort> MeetingSortContext { get; set; }
        //public DbSet<SubjectSort> SubjectSortContext { get; set; }
       // public DbSet<AdministrativeLevel> AdministrativeLevelContext { get; set; }
        //public DbSet<PTPost> PTPostContext { get; set; }
        public DbSet<Pact> PactContext { get; set; }
        public DbSet<AchivementApply> AchivementApplyContext { get; set; }
        public DbSet<FundProportionSet> FundProportionSetContext { get; set; }
        public DbSet<BasicCode> BasicCodeContext { get; set; }//基本代码表  
        public DbSet<NewAcademicReporting> NewAcademicReportingContext { get; set; } //新增学术表
        public DbSet<WithinPost> WithinPostContext { get; set; }  //所内发文
        public DbSet<Platform> PlatformContext { get; set; }  //平台
        public DbSet<Furniture> FurnitureContext { get; set; }  //家具
        public DbSet<Equipment> EquipmentContext { get; set; }  //设备
    }
}
