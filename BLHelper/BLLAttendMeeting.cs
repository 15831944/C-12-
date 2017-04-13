/**编写人：李金秋
 * 时间：2014年6月20号
 * 功能：会议参加人员表的相关操作
 * 修改履历：1.时间：8月11日
 *           修改人：张凡凡
 *           修改内容：添加更新IsPass状态函数UpdateIsPass(int,bool)
 **/

using Common.Entities;
using DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace BLHelper
{
    public class BLLAttendMeeting
    {
        DataBaseContext dbcontext = new DataBaseContext();

        //更新IsPass状态
        public void UpdateIsPass(int ID, bool isPass)
        {
            try
            {
                AttendMeeting NewAttendMeeting = dbcontext.AttendMeetingContext.Find(ID);
                if (NewAttendMeeting == null)
                    return;
                NewAttendMeeting.IsPass = isPass;
                dbcontext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        //插入会议参加人员信息
        public int Insert(AttendMeeting attendMeeting)
        {
            try
            {
                dbcontext.AttendMeetingContext.Add(attendMeeting);
                dbcontext.SaveChanges();
                return attendMeeting.AttendMeetingID;
            }
            catch
            {
                //throw;
                return 0;
            }
        }
        //根据AttendMeetingID删除会议参加人员信息
        public bool Delete(int attendMeetingID)
        {
            try
            {
                AttendMeeting attendMeeting = dbcontext.AttendMeetingContext.Where(u => u.AttendMeetingID == attendMeetingID).FirstOrDefault();
                dbcontext.AttendMeetingContext.Attach(attendMeeting);
                dbcontext.AttendMeetingContext.Remove(attendMeeting);
                dbcontext.SaveChanges();
                return true;
            }
            catch
            {
                //throw;
                return false;
            }
        }
        
        //更新会议参加人员情况
        public void Update(AttendMeeting attendMeeting)
        {
            try
            {
                AttendMeeting AttendMeeting = dbcontext.AttendMeetingContext.Find(attendMeeting.AcademicMeetingID);
                AttendMeeting.UserInfoID = attendMeeting.UserInfoID;
                AttendMeeting.AcademicMeetingID = attendMeeting.AcademicMeetingID;
                AttendMeeting.SecrecyLevel = attendMeeting.SecrecyLevel;
                AttendMeeting.EntryPerson = attendMeeting.EntryPerson;
                AttendMeeting.IsPass = attendMeeting.IsPass;
                dbcontext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }
        //根据UserInfoID(人员ID)查看AcademicMeetingID(会议ID)
        public List<int?> FindAcademicMeetingID(int userInfoID)
        {
            List<int?> list = new List<int?>();
            int count = dbcontext.AttendMeetingContext.OrderBy(p => p.UserInfoID == userInfoID && p.IsPass == true).Count();
            List<AttendMeeting> objectlist = dbcontext.AttendMeetingContext.Where(p => p.UserInfoID == userInfoID).ToList();
            for (int i = 0; i < count; i++)
            {
                list[i] = objectlist[i].AcademicMeetingID;
            }
            return list;
        }
        //分页
        public List<AttendMeeting> FindPaged(int pageIndex, int pageSize, int? SecrecyLevel)
        {
            return dbcontext.AttendMeetingContext.Where(u => u.SecrecyLevel <= SecrecyLevel && u.IsPass == true).OrderBy(c => c.AttendMeetingID).Skip(pageIndex * pageSize).Take(pageSize).ToList();
        }
        //根据人员ID查询会议ID
        public List<int?> FindMeetingIDByUserID(int UserInfoID, int SecrecyLevel)
        {
             var res = from c in dbcontext.AttendMeetingContext
                      where c.UserInfoID == UserInfoID && c.IsPass == true && c.SecrecyLevel <= SecrecyLevel
                      select c.AcademicMeetingID;
            return res.ToList(); ;
        }
        //根据会议ID查找人员姓名
        public List<int?> FindUserNameByMeetingID(int MeetingID, int SecrecyLevel)
        {
            var res = from c in dbcontext.AttendMeetingContext
                      where c.AcademicMeetingID == MeetingID && c.IsPass == true && c.SecrecyLevel <= SecrecyLevel
                      select c.UserInfoID;
            return res.ToList();
        }
        //根据会议参加ID查询信息
        public AttendMeeting FindByID(int AttendMeetingID)
        {
            return dbcontext.AttendMeetingContext.Find(AttendMeetingID);
        }
        //根据会议ID删除会议参加人员信息
        public bool DeleteStaffByMeetingID(int MeetingID)
        {
            try
            {
                List<int> list = dbcontext.AttendMeetingContext.Where(p => p.AcademicMeetingID == MeetingID).Select(p => p.AttendMeetingID).ToList();
                for (int i = 0; i<list.Count(); i++)
                {
                    AttendMeeting attendMeeting = dbcontext.AttendMeetingContext.Find(list[i]);
                    dbcontext.AttendMeetingContext.Attach(attendMeeting);
                    dbcontext.AttendMeetingContext.Remove(attendMeeting);           
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
