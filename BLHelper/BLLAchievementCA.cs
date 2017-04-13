/**编写人：李金秋
 * 时间：2014年6月20号
 * 功能：验收成果表的相关操作
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
    public class BLLAchievementCA
    {
        DataBaseContext dbcontext = new DataBaseContext();

        //更新IsPass状态
        public void UpdateIsPass(int id, bool Ispass)
        {
            AchievementCA newachi = dbcontext.AchievementCAContext.Find(id);
            newachi.IsPass = Ispass;
            dbcontext.SaveChanges();
        }

        //删除验收信息
        public int Delete(int ID)
        {
            try
            {
                AchievementCA AchievementCA = dbcontext.AchievementCAContext.Where(u => u.AchievementCAID == ID).FirstOrDefault();//new Announcement { AnnouncementID = announcementID };
                int attachID = Convert.ToInt32(AchievementCA.AttachmentID);
                dbcontext.AchievementCAContext.Attach(AchievementCA);
                dbcontext.AchievementCAContext.Remove(AchievementCA);
                dbcontext.SaveChanges();
                return attachID;
            }
            catch
            {
                throw;
            }
        }

        //录入验收信息
        public void Insert(AchievementCA achievementCA)
        {
            try
            {
                dbcontext.AchievementCAContext.Add(achievementCA);
                dbcontext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }
        //根据ID查找附件ID
        public int FindAttachmentID(int id)
        {
            List<AchievementCA> list = new List<AchievementCA>();
            list = dbcontext.AchievementCAContext.Where(a => a.AchievementCAID == id).ToList();
            if (list.Count() != 0)
            {
                if (list.FirstOrDefault().AttachmentID != null)
                {
                    return list.FirstOrDefault().AttachmentID.Value;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }

        //根据ID查询成员
        public string FindMember(int id)
        {
            List<AchievementCA> list = new List<AchievementCA>();
            list = dbcontext.AchievementCAContext.Where(p => p.AchievementCAID == id).ToList();
            if (list.Count() != 0)
            {
                if (list.FirstOrDefault().ProjectMember != "")
                {
                    return list.FirstOrDefault().ProjectMember;
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }

        //修改验收信息
        public void Update(AchievementCA achievementCA)
        {
            try
            {
                AchievementCA AchievementCA = dbcontext.AchievementCAContext.Find(achievementCA.AchievementCAID);
                AchievementCA.AchievementID = achievementCA.AchievementID;
                AchievementCA.CACommnetLevel = achievementCA.CACommnetLevel;
                AchievementCA.CATime = achievementCA.CATime;
                AchievementCA.CAUnit = achievementCA.CAUnit;
                AchievementCA.SecrecyLevel = achievementCA.SecrecyLevel;
                AchievementCA.EntryPerson = achievementCA.EntryPerson;
                AchievementCA.IsPass = achievementCA.IsPass;
                AchievementCA.AttachmentID = achievementCA.AttachmentID;
                AchievementCA.ProjectMember = achievementCA.ProjectMember;
                dbcontext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }
        //查看验收信息
        public AchievementCA FindAll(int id)
        {
            return dbcontext.AchievementCAContext.Find(id);
        }
        //按id查询验收信息
        public List<AchievementCA> FindByAchievementName( List<int> AchievementID, int? SecrecyLevel)
        {
            List<AchievementCA> res = new List<AchievementCA>();
            for (int i = 0; i < AchievementID.Count; i++)
            {
                int id = AchievementID[i];
                res.AddRange(dbcontext.AchievementCAContext.Where(u => u.AchievementID == id && u.SecrecyLevel <= SecrecyLevel && u.IsPass == true));
            }
            return res.ToList(); 
        }
        //根据成果ID(单个)
        public List<AchievementCA> FindOneName( int AchievementID, int? SecrecyLevel)
        {
            return dbcontext.AchievementCAContext.Where(p => p.AchievementID == AchievementID && p.IsPass == true && p.SecrecyLevel <= SecrecyLevel).OrderBy(c => c.AchievementID).ToList();
        }
        //按验收时间查询验收信息
        public List<AchievementCA> FindByCATime(int year, int? SecrecyLevel)
        {
            return dbcontext.AchievementCAContext.Where(p => p.CATime.Value.Year == year && p.IsPass == true && p.SecrecyLevel <= SecrecyLevel).OrderBy(c => c.AchievementID).ToList();
        }
        //按验收部门查询验收信息
        public List<AchievementCA> FindByCAUnit( string CAUnit, int? SecrecyLevel)
        {
            return dbcontext.AchievementCAContext.Where(p => p.CAUnit.Contains(CAUnit) && p.IsPass == true && p.SecrecyLevel <= SecrecyLevel).OrderBy(c => c.AchievementID).ToList();
        }
        //按验收评语级别查询验收信息
        public List<AchievementCA> FindByCACommnetLevel( string CACommnetLevel, int? SecrecyLevel)
        {
            return dbcontext.AchievementCAContext.Where(p => p.CACommnetLevel.Contains(CACommnetLevel) && p.IsPass == true && p.SecrecyLevel <= SecrecyLevel).OrderBy(c => c.AchievementID).ToList();
        }
        //按成员模糊查询
        public List<AchievementCA> FindByMember(string Name, int? SecrecyLevel)
        {
            return dbcontext.AchievementCAContext.Where(p => p.ProjectMember.Contains(Name) && p.IsPass == true && p.SecrecyLevel <= SecrecyLevel).ToList();
        }
        //分页
        public List<AchievementCA> FindPaged( int? SecrecyLevel)
        {
            return dbcontext.AchievementCAContext.Where(u => u.SecrecyLevel <= SecrecyLevel && u.IsPass == true).OrderBy(c => c.AchievementID).ToList();
        }
        //根据成果ID找到AchievementCAID
        public List<int> FindByAchievement(int AchievementID)
        {
            List<int> list = new List<int>();
            var newlist = dbcontext.AchievementCAContext.Where(u => u.AchievementID == AchievementID).ToList();
            for (int i = 0; i < newlist.Count(); i++)
            {
                list.Add(Convert.ToInt32(newlist[i].AchievementCAID));
            }
            return list;
        }
    }
}
