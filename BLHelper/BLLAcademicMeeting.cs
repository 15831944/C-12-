/**编写人：李金秋
 * 时间：2014年6月20号
 * 功能：学术会议表的相关操作
 * 修改履历：1.7月24日，添加分科研人员查看学术会议FindByUser
 *           8月4日：对方法添加保密等级参数
 *           8月4日：添加 FindMeetingName方法( 根据人员级别显示学术会议名称) 修改人：李金秋
 *           8月5日：添加查询会议分类名称方法 修改人：李金秋
 *                   添加根据会议分类名称返回会议分类编号 修改人：李金秋
 *                   添加根据年份和参加人员查询学术会议名称 修改人：李金秋
 *           2.时间：8月11日
 *           修改人：张凡凡
 *           修改内容：添加更新IsPass状态函数UpdateIsPass(int,bool)
 *           3.修改人：吕博杨
 *             修改时间：2015年11月29日
 *             修改内容：添加更新功能（Update方法）
 *                      添加根据AcademicMeetingID查询照片ID的功能
 *           
 **/

using Common.Entities;
using DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLHelper
{
    public class BLLAcademicMeeting
    {
        DataBaseContext dbcontext = new DataBaseContext();

        //更新IsPass状态
        public void UpdateIsPass(int Id, bool Ispass)
        {
            AcademicMeeting newaca = dbcontext.AcademicMeetingContext.Find(Id);
            if (newaca == null)
                return;
            newaca.IsPass = Ispass;
            dbcontext.SaveChanges();
        }
        //删除附件
        public void UpdateAttachment(int ID)
        {
            try
            {
                AcademicMeeting Meeting = dbcontext.AcademicMeetingContext.Find(ID);
                Meeting.AttachmentID = null;
                dbcontext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }
        //更新学术会议信息
        public void Update(AcademicMeeting academicMeeting)
        {
            AcademicMeeting target = dbcontext.AcademicMeetingContext.Find(academicMeeting.AcademicMeetingID);
            target.AttachmentID = academicMeeting.AttachmentID;
            target.PhotoID = academicMeeting.PhotoID;
            target.AttendMeetingPeople = academicMeeting.AttendMeetingPeople;
            target.Coorganizers = academicMeeting.Coorganizers;
            target.EndTime = academicMeeting.EndTime;
            target.EntryPerson = academicMeeting.EntryPerson;
            target.IsPass = academicMeeting.IsPass;
            target.MeetingContent = academicMeeting.MeetingContent;
            target.MeetingCount = academicMeeting.MeetingCount;
            target.MeetingHost = academicMeeting.MeetingHost;
            target.MeetingMajorPerson = academicMeeting.MeetingMajorPerson;
            target.MeetingMajorTheme = academicMeeting.MeetingMajorTheme;
            target.MeetingName = academicMeeting.MeetingName;
            target.MeetingPlace = academicMeeting.MeetingPlace;
            target.MeetingSortName = academicMeeting.MeetingSortName;
            target.Organizers = academicMeeting.Organizers;
            target.ProceedingsofTitle = academicMeeting.ProceedingsofTitle;
            target.SecrecyLevel = academicMeeting.SecrecyLevel;
            target.StratTime = academicMeeting.StratTime;
            dbcontext.SaveChanges();
        }
        //插入学术会议信息
        public void Insert(AcademicMeeting academicMeeting)
        {
            try
            {
                dbcontext.AcademicMeetingContext.Add(academicMeeting);
                dbcontext.SaveChanges();
            }
            catch
            {
                throw;
            }

        }
        //根据AcademicMeetingID删除学术会议信息
        public int? DeleteByAcademicMeetingID(int academicMeetingID)
        {
            try
            {
                //AcademicMeeting academicMeeting = new AcademicMeeting { AcademicMeetingID = academicMeetingID };
                AcademicMeeting academicMeeting = dbcontext.AcademicMeetingContext.Find(academicMeetingID);
                dbcontext.AcademicMeetingContext.Attach(academicMeeting);
                dbcontext.AcademicMeetingContext.Remove(academicMeeting);
                dbcontext.SaveChanges();
                //return true;
                return academicMeeting.AttachmentID;
            }
            catch
            {
                return 0;
            }

        }
        public bool Delete(int academicMeetingID)
        {
            try
            {

                AcademicMeeting academicMeeting = dbcontext.AcademicMeetingContext.Find(academicMeetingID);
                if (academicMeeting == null)
                    return false;
                dbcontext.AcademicMeetingContext.Attach(academicMeeting);
                dbcontext.AcademicMeetingContext.Remove(academicMeeting);
                dbcontext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        //根据AcademicMeetingID查询附件ID
        public int FindAttachmentID(int academicMeetingID)
        {
            var Academin = dbcontext.AcademicMeetingContext.Find(academicMeetingID);
            if (Academin == null)
                return 0;
            else
            {
                if (Academin.AttachmentID == null)
                    return 0;
                else
                    return Convert.ToInt32(Academin.AttachmentID);
            }
        }

        //根据AcademicMeetingID查询照片ID
        public int FindPhotoID(int academicMeetingID)
        {
            var Academic = dbcontext.AcademicMeetingContext.Find(academicMeetingID);
            if (Academic == null)
                return 0;
            else
            {
                if (Academic.PhotoID == null)
                    return 0;
                else
                    return Convert.ToInt32(Academic.PhotoID);
            }
        }

        //根据StratTime(会议开始时间)查看学术会议信息
        public List<AcademicMeeting> FindByStartTime(DateTime stratTime)
        {
            return dbcontext.AcademicMeetingContext.OrderBy(p => p.StratTime == stratTime && p.IsPass == true).ToList();
        }
        //根据会议ID和保密级别查询会议信息
        public List<AttendMeeting> FindByUser(int MeetingID, int SecrecyLevel)
        {
            return dbcontext.AttendMeetingContext.Where(p => p.AcademicMeetingID == MeetingID && p.SecrecyLevel <= SecrecyLevel && p.IsPass == true).ToList();
        }

        //根据AcademicMeetingID(会议ID)查看学术会议信息
        public AcademicMeeting FindByAcademicMeetingID(int academicMeetingID, bool ispass)
        {
            return dbcontext.AcademicMeetingContext.Where(p => p.AcademicMeetingID == academicMeetingID && p.IsPass == ispass).FirstOrDefault();
        }
        //根据AcademicMeetingID(会议ID)查看学术会议信息
        public List<AcademicMeeting> FindAcademicMeetingByMeetingID(List<int?> academicMeetingID, int SecrecyLevel)
        {
            List<AcademicMeeting> res = new List<AcademicMeeting>();
            for (int i = 0; i < academicMeetingID.Count; i++)
            {
                AcademicMeeting list = dbcontext.AcademicMeetingContext.Find(academicMeetingID[i]);
                res.Add(list);
            }
            return res;
        }

        //根据MeetingCategoryID(会议分类编号)查询学术会议信息(被改了)
        public List<AcademicMeeting> FindByMeetingCategoryID(int meetingCategoryID)
        {
            return dbcontext.AcademicMeetingContext.Where(p => p.MeetingSortName == meetingCategoryID.ToString() && p.IsPass == true).ToList();
        }
        //查找所有学术会议信息
        public List<AcademicMeeting> FindAll(int SecrecyLevel)
        {
            return dbcontext.AcademicMeetingContext.Where(p => p.SecrecyLevel <= SecrecyLevel && p.IsPass == true).ToList();
        }
        //查找所有学术会议名称
        public List<string> GetMeetingName(int SecrecyLevel)
        {
            List<string> listName = new List<string>();
            List<AcademicMeeting> listMeeting = FindAll(SecrecyLevel);
            for (int i = 0; i < listMeeting.Count; i++)
                listName[i] = listMeeting[i].MeetingName;
            return listName;
        }
        //分页
        public List<AcademicMeeting> FindPaged(int pageIndex, int pageSize, int? SecrecyLevel)
        {
            return dbcontext.AcademicMeetingContext.Where(u => u.SecrecyLevel <= SecrecyLevel && u.IsPass == true).OrderBy(c => c.AcademicMeetingID).Skip(pageIndex * pageSize).Take(pageSize).ToList();
        }
        //根据人员级别显示学术会议名称
        public List<AcademicMeeting> FindMeetingName(int SecrecyLevel)
        {
            return dbcontext.AcademicMeetingContext.Where(p => p.SecrecyLevel <= SecrecyLevel && p.IsPass == true).ToList();
        }
        //根据人员等级查询会议名称
        public List<string> FindMeetingNameBySecrecyLevel(int SecrecyLevel)
        {
            return dbcontext.AcademicMeetingContext.Where(p => p.SecrecyLevel <= SecrecyLevel && p.IsPass == true).Select(p => p.MeetingName).ToList();
        }
        //根据年份查询学术会议信息
        public List<AcademicMeeting> FindByYear(int year, int SecrecyLevel)
        {
            return dbcontext.AcademicMeetingContext.Where(p => p.SecrecyLevel <= SecrecyLevel && p.StratTime.Value.Year == year && p.IsPass == true).ToList();
        }
        public string FindByMeetingID(int meetingID)
        {
            return dbcontext.AcademicMeetingContext.Find(meetingID).MeetingName.FirstOrDefault().ToString();
        }
        //根据学术会议名称返回会议ID
        public int FindMeetingID(string MeetingName)
        {
            if (MeetingName != "")
            {
                AcademicMeeting academicMeeting = dbcontext.AcademicMeetingContext.Where(p => p.MeetingName == MeetingName).FirstOrDefault();
                if (academicMeeting != null)
                    return academicMeeting.AcademicMeetingID;
                else
                    return 0;
            }
            else
                return 0;
        }
        //根据会议名称判断是否为登录人上传
        public bool IsUpload(string MeetingName, string EntryPerson)
        {
            int MeetingID = dbcontext.AcademicMeetingContext.Where(p => p.MeetingName == MeetingName).FirstOrDefault().AcademicMeetingID;
            AcademicMeeting academicMeeting = dbcontext.AcademicMeetingContext.Find(MeetingID);
            if (academicMeeting.EntryPerson == EntryPerson)
                return true;
            else
                return false;
        }
        //根据会议ID查询会议对象
        public AcademicMeeting FindAcademicMeetingByMeetingID(int meetingID)
        {
            return dbcontext.AcademicMeetingContext.Find(meetingID);
        }
        //按报告名称查询报告信息
        public List<ScienceReport> FindByAgencyName(int MeetingID, int pageIndex, int pageSize, int? SecrecyLevel)
        {
            return (from a in dbcontext.ScienceReportContext
                    where a.MeetingID == MeetingID && a.SecrecyLevel <= SecrecyLevel && a.IsPass == true
                    select new
                    {
                        SReportName = a.SReportName,
                        SReportPeople = a.SReportPeople,
                        SReportTime = a.SReportTime,
                        SReportPlace = a.SReportPlace,
                        AgencyID = a.AgencyID
                    }).ToList().Select(a => new ScienceReport
                    {
                        SReportName = a.SReportName,
                        SReportPeople = a.SReportPeople,
                        SReportTime = a.SReportTime,
                        SReportPlace = a.SReportPlace,
                        AgencyID = a.AgencyID
                    }).Skip(pageIndex * pageSize).Take(pageSize).ToList();
        }
        //根据人员年份查询会议信息
        public List<AcademicMeeting> FindByUserAndYear(List<AcademicMeeting> list, int year)
        {
            return list.Where(p => p.StratTime != null && p.StratTime.Value.Year == year).ToList();
        }
        //根据学术会议名称返回会议ID
        public int FindByMeetingID(string MeetingName)
        {
            return dbcontext.AcademicMeetingContext.Where(p => p.MeetingName == MeetingName).FirstOrDefault().AcademicMeetingID;
        }
        //删除学术会议的所有学术报告
        public bool DeleteReportByMeetingID(int MeetingID)
        {
            try
            {
                List<int> listReportID = dbcontext.ScienceReportContext.Where(p => p.MeetingID == MeetingID).Select(p => p.ScienceReportID).ToList();
                for (int i = 0; i < listReportID.Count(); i++)
                {
                    ScienceReport sc = dbcontext.ScienceReportContext.Find(listReportID[i]);
                    dbcontext.ScienceReportContext.Attach(sc);
                    dbcontext.ScienceReportContext.Remove(sc);
                }
                dbcontext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
