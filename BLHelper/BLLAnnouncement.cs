/**编写人：李金秋
 * 时间：2014年6月20号
 * 功能：通知公告表的相关操作
 * 修改履历：2014.7.22 删除多条通知公告信息功能
 *          2.时间：8月11日
 *           修改人：张凡凡
 *           修改内容：添加更新IsPass状态函数UpdateIsPass(int,bool)
 *           3.时间：8月13日
 *             修改人：李金秋
 *             修改内容：添加查询通知公告数量Count
 *           4.时间：8月13日
 *             修改人：李金秋
 *             修改内容：添加查询通知公告HeadLine+Datetime(HeadLineAndDateTime())
 *           5.时间：8月13日
 *             修改人：李金秋
 *             修改内容：添加查询所有通知公告信息
 **/

using System;
using DataBase;
using Common.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLHelper
{
    public class BLLAnnouncement
    {
        DataBaseContext dbcontext = new DataBaseContext();

        //更新IsPass状态
        public void UpdateIsPass(int announceID, bool isPass)
        {
            try
            {
                Announcement NewAnnounce = dbcontext.AnnouncementContext.Find(announceID);
                if (NewAnnounce == null)
                    return;
                NewAnnounce.IsPass = isPass;
                dbcontext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        //插入通知公告信息
        public void Insert(Announcement announcement)
        {
            try
            {
                dbcontext.AnnouncementContext.Add(announcement);
                dbcontext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }
        //根据AnnouncementID(通知公告ID)删除公告信息
        public int Delete(int announcementID)
        {
            try
            {
                Announcement announcement = dbcontext.AnnouncementContext.Where(u => u.AnnouncementID == announcementID).FirstOrDefault();//new Announcement { AnnouncementID = announcementID };
                int attachID = Convert.ToInt32(announcement.AttachmentID);
                dbcontext.AnnouncementContext.Attach(announcement);
                dbcontext.AnnouncementContext.Remove(announcement);
                dbcontext.SaveChanges();
                return attachID;
            }
            catch
            {
                throw;
            }
        }
        
        //根据AnnouncementID(通知公告ID)获取通知公告的信息
        public Announcement Find(int announcementID)
        {
            return dbcontext.AnnouncementContext.Find(announcementID);
        }

        //根据AnnouncementSortID(分类ID)查看不同类别的通知公告信息
        public List<Announcement> FindBySortID(int SortID)
        {
            //return dbcontext.AnnouncementContext.OrderBy(p => p.AnnouncementSortName == SortID && p.IsPass == true).ToList();
            var res = from u in dbcontext.AnnouncementContext
                      orderby u.Time descending
                      select u;
            return res.ToList();
        }
        //根据AnnouncementID获取AttachmentID(附件表ID)
        public int FindAttachmentID(int announcementID)
        {
            var announcement = dbcontext.AnnouncementContext.Where(u => u.AnnouncementID == announcementID).FirstOrDefault();
            if (announcement != null)
            {
                if (announcement.AttachmentID != null)
                    return Convert.ToInt32(announcement.AttachmentID);
                else
                    return 0;
            }
            else
                return 0;
        }
        //分页(1:通知 2：学校公告 3:外来公告)(时间按降序排列)
        public List<Announcement> FindPaged(string sortName)
        {
            var res = from u in dbcontext.AnnouncementContext
                      where u.AnnouncementSortName == sortName && u.IsPass == true
                      orderby u.Time descending
                      select u;
            return res.ToList();
        }

        //根据用户级别查询通知信息
        public List<Announcement> FindBySecrecyLevel(int SecrecyLevel)
        {
            return dbcontext.AnnouncementContext.Where(p => p.SecrecyLevel <= SecrecyLevel).ToList();
        }
        //通知公告数量
        public int Count(int SecrecyLevel)
        {
            return dbcontext.AnnouncementContext.Where(p => p.SecrecyLevel <= SecrecyLevel && p.IsPass == true).Count();
        }
        //查询通知公告HeadLine+Datetime
        public string HeadLineAndDatetime(int AnnouncementID)
        {
            Announcement listAnnoucement = dbcontext.AnnouncementContext.Where(p => p.AnnouncementID == AnnouncementID).FirstOrDefault();
            return listAnnoucement.HeadLine + "   " + listAnnoucement.SourceAgency + "   " + listAnnoucement.Time.Value.Year + "/" + listAnnoucement.Time.Value.Month + "/" + listAnnoucement.Time.Value.Day;
        }
        //查询所有通知公告信息(时间按降序排列)
        public List<Announcement> FindAll()
        {
            var res = from u in dbcontext.AnnouncementContext
                      where (u.IsPass == true)
                      orderby u.Time descending
                      select u;
            return res.ToList();
        }
    }
}
