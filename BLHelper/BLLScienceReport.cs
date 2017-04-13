/**编写人：李金秋
 * 时间：2014年6月20号
 * 功能：学术报告表的相关操作
 * 修改履历：1、8月1日，添加根据会议id查看学术报告函数FindByMeetingID（int, int）
 *          2.时间：8月21日
 *           修改人：张凡凡
 *           修改内容：添加根据id查找附件id函数FindAttachmentid(int)
 **/
using Common.Entities;
using DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLHelper
{
    public class BLLScienceReport
    {
        DataBaseContext dbcontext = new DataBaseContext();
        //录入学术报告信息
        public void Insert(ScienceReport scienceReport)
        {
            try
            {
                dbcontext.ScienceReportContext.Add(scienceReport);
                dbcontext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }
        //根据ScienceReportID删除学术报告信息
        public int? Delete(int scienceReportID)
        {
            try
            {
                //ScienceReport scienceReport = new ScienceReport { ScienceReportID = scienceReportID };
                ScienceReport scienceReport = dbcontext.ScienceReportContext.Find(scienceReportID);
                dbcontext.ScienceReportContext.Attach(scienceReport);
                dbcontext.ScienceReportContext.Remove(scienceReport);
                dbcontext.SaveChanges();
                return scienceReport.AccessoryID;
            }
            catch
            {
                return 0;
            }
        }
        //删除附件
        public void UpdateAttachment(int ID)
        {
            try
            {
                ScienceReport scienceReport = dbcontext.ScienceReportContext.Find(ID);
                scienceReport.AccessoryID = null;
                dbcontext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }
        public bool Delete(int[] scienceReportID)
        {
            try
            {
                int count = scienceReportID.Count();
                for (int i = 0; i < count; i++)
                {
                    ScienceReport scienceReport = new ScienceReport { ScienceReportID = scienceReportID[i] };
                    dbcontext.ScienceReportContext.Attach(scienceReport);
                    dbcontext.ScienceReportContext.Remove(scienceReport);
                }
                dbcontext.SaveChanges();
                return true;
            }
            catch
            {
                throw;
            }
        }
        //根据ScienceReportID查询学术报告信息
        public List<ScienceReport> FindByScienceReportID(int ScienceReportID, bool ispass)
        {
            return dbcontext.ScienceReportContext.Where(p => p.ScienceReportID == ScienceReportID && p.IsPass == ispass).ToList();
        }
        //根据AgencyID(AgencyID)查询学术报告信息
        public List<ScienceReport> FindByAgencyID(int agencyID)
        {
            return dbcontext.ScienceReportContext.Where(p => p.AgencyID == agencyID).ToList();
        }
        //根据ScienceReportID查询AccessoryID(附件ID)
        public List<int?> FindAcademicMeetingID(int ScienceReportID)
        {
            List<int?> list = new List<int?>();
            int count = dbcontext.ScienceReportContext.OrderBy(p => p.ScienceReportID == ScienceReportID).Count();
            //return dbcontext.AttendMeetingContext.OrderBy(p=>p.UserInfoID.UserInfoID==userInfoID)
            List<ScienceReport> objectlist = dbcontext.ScienceReportContext.OrderBy(p => p.ScienceReportID == ScienceReportID).ToList();
            for (int i = 0; i < count; i++)
            {
                list[i] = objectlist[i].AccessoryID;
            }
            return list;
        }
        //根据科研人员（报告人）查看学术报告
        public List<ScienceReport> FindBySReportPeople(string SReportPeople)
        {
            return dbcontext.ScienceReportContext.OrderBy(p => p.SReportPeople == SReportPeople).ToList();
        }
        //分页
        //public List<ScienceReport> FindPaged(int MeetingID,int pageIndex, int pageSize, int? SecrecyLevel)
        //{
        //    return dbcontext.ScienceReportContext.Where(u =>u.MeetingID ==MeetingID&& u.SecrecyLevel <= SecrecyLevel && u.IsPass == true).OrderBy(c => c.ScienceReportID).Skip(pageIndex * pageSize).Take(pageSize).ToList();
        //}
        public List<ScienceReport> FindPaged(int MeetingID, int? SecrecyLevel)
        {
            return dbcontext.ScienceReportContext.Where(u => u.MeetingID == MeetingID && u.SecrecyLevel <= SecrecyLevel && u.IsPass == true).OrderBy(c => c.ScienceReportID).ToList();
        }

        //根据会议id查看学术报告
        public List<ScienceReport> FindByMeetingID(int MeetingID, int SecrecyLevel)
        {
            return dbcontext.ScienceReportContext.Where(u => u.MeetingID == MeetingID && u.SecrecyLevel <= SecrecyLevel && u.IsPass == true).ToList();
        }
        //统计某个会议的学术报告数量
        public int ReportCount(int MeetingID, int? SecrecyLevel)
        {
            return dbcontext.ScienceReportContext.Where(p => p.MeetingID == MeetingID && p.SecrecyLevel <= SecrecyLevel).Count();
        }
        //根据报告ID获取录入人
        public string FindEntryPeople(int ReportID)
        {
            return dbcontext.ScienceReportContext.Find(ReportID).EntryPerson;
        }
        //审核状态的改变
        public void ChangePass(int ReportID, bool IsPass)
        {
            //bool? isPass = dbcontext.ScienceReportContext.Find(ReportID).IsPass;
            //isPass = IsPass;
            ScienceReport sci = dbcontext.ScienceReportContext.Find(ReportID);
            if (sci == null)
                return;
            sci.IsPass = IsPass;
            dbcontext.SaveChanges();
        }
        public ScienceReport FindByReportID(int ReportID)
        {
            return dbcontext.ScienceReportContext.Find(ReportID);
        }

        //查询附件id
        public int FindAttachmentid(int reportid)
        {
            var report = dbcontext.ScienceReportContext.Find(reportid);
            if (report != null)
            {
                if (report.AccessoryID.HasValue)
                    return report.AccessoryID.Value;
                else
                    return 0;
            }
            else
                return 0;
        }
        
    }
}
