
/**编写人：方淑云
 * 时间：2014年8月14号
 * 功能:成果应用表的相关操作
 * 修改履历：
 **/
using Common.Entities;
using DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLHelper
{
   public class BLLAchievementApply
    {
        DataBaseContext dbcontext = new DataBaseContext();

        //更新IsPass状态
        public void UpdateIsPass(int ID, bool isPass)
        {
            try
            {
                AchivementApply apply = dbcontext.AchivementApplyContext.Find(ID);
                if (apply == null)
                    return;
               apply.IsPass = isPass;
                dbcontext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }
        //添加
        public bool Insert(AchivementApply apply)
        {
            try
            {
                if (apply != null)
                {
                    dbcontext.AchivementApplyContext.Add(apply);
                    dbcontext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                throw e;
            }
        }
       
     
        //根据ID查找
        public AchivementApply FindAll(int ID)
        {        
            return dbcontext.AchivementApplyContext.Find(ID);
        }
       //根据ID查找附件ID
        public int FindAttachmentID(int id)
        {
            List<AchivementApply> list = new List<AchivementApply>();
            list = dbcontext.AchivementApplyContext.Where(a => a.AchivementApplyID == id).ToList();
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
      
        //更新用户学生情况
        public bool Update(AchivementApply apply)
        {
            try
            {
                if (apply != null)
                {
                    AchivementApply newapply = dbcontext.AchivementApplyContext.Find(apply.AchivementApplyID);
                    newapply.ApplyUnit = apply.ApplyUnit;
                    newapply.EconomicBenefit = apply.EconomicBenefit;
                    newapply.EndTime = apply.EndTime;
                    newapply.EntryPerson = apply.EntryPerson;
                    newapply.SecrecyLevel = apply.SecrecyLevel;
                    newapply.StartTime = apply.StartTime;
                    newapply.Use = apply.Use;
                    newapply.EntryPerson = apply.EntryPerson;
                    newapply.AttachmentID = apply.AttachmentID;
                    newapply.AchievementID = apply.AchievementID;
                    newapply.Member = apply.Member;
                    dbcontext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                throw e;
            }
        }
        //删除
        public int Delete(int id)
        {
            try
            {
                AchivementApply AchivementApply = dbcontext.AchivementApplyContext.Where(u => u.AchivementApplyID == id).FirstOrDefault();//new Announcement { AnnouncementID = announcementID };
                int attachID = Convert.ToInt32(AchivementApply.AttachmentID);
                dbcontext.AchivementApplyContext.Attach(AchivementApply);
                dbcontext.AchivementApplyContext.Remove(AchivementApply);
                dbcontext.SaveChanges();
                return attachID;
            }
            catch
            {
                throw;
            }
        }
        //根据成果ID查找成果应用(单个)
        public List<AchivementApply> FindByAchievementID( int AchievementID, int? SecrecyLevel)
        {
            List<AchivementApply> list = new List<AchivementApply>();
            list = dbcontext.AchivementApplyContext.Where(a => a.AchievementID == AchievementID && a.SecrecyLevel <= SecrecyLevel && a.IsPass == true).OrderBy(c => c.AchievementID).ToList();
            if (list != null)
            {
                return list;
            }
            else
            {
                return null;
            }
        }
       //根据成果ID查找成果应用（多个）
        public List<AchivementApply> FindByAchievementName( List<int> AchievementID, int? SecrecyLevel)
        {
            List<AchivementApply> res = new List<AchivementApply>();
            for (int i = 0; i < AchievementID.Count; i++)
            {
                int id = AchievementID[i];
                res.AddRange(dbcontext.AchivementApplyContext.Where(u => u.AchievementID == id && u.SecrecyLevel <= SecrecyLevel && u.IsPass == true));
            }
            return res.ToList(); 
        }
       //根据应用单位查找
        public List<AchivementApply> FindByUnit(string unit, int? SecrecyLevel)
        {
            List<AchivementApply> list = new List<AchivementApply>();
            list = dbcontext.AchivementApplyContext.Where(a => a.ApplyUnit.Contains(unit) && a.SecrecyLevel <= SecrecyLevel && a.IsPass == true).OrderBy(c => c.AchievementID).ToList();
            if (list != null)
            {
                return list;
            }
            else
            {
                return null;
            }
        }
       //根据开始时间查找
        public List<AchivementApply> FindByTime( int year, int? SecrecyLevel)
        {
            List<AchivementApply> list = new List<AchivementApply>();
            list = dbcontext.AchivementApplyContext.Where(a => a.StartTime.Value.Year == year && a.SecrecyLevel <= SecrecyLevel && a.IsPass == true).OrderBy(c => c.AchievementID).ToList();
            if (list != null)
            {
                return list;
            }
            else
            {
                return null;
            }
        }
        //分页
        public List<AchivementApply> FindPage( int? SecrecyLevel)
        {
            return dbcontext.AchivementApplyContext.Where(u => u.SecrecyLevel <= SecrecyLevel && u.IsPass == true).OrderBy(c => c.AchievementID).ToList();
        }
       //根据成果ID找到AchievementApplyID
        public List<int> FindByAchievement(int AchievementID)
        {
            List<int> list = new List<int>();
            var newlist = dbcontext.AchivementApplyContext .Where(u => u.AchievementID == AchievementID).ToList();
            for (int i = 0; i < newlist.Count(); i++)
            {
                list.Add(Convert.ToInt32(newlist[i].AchivementApplyID));
            }
            return list;
        }
       //根据保密级别查找
       public List<AchivementApply> FindBySecrecyLevel(int secrecylevel,int ? SecrecyLevel)
        {
            List<AchivementApply> list = new List<AchivementApply>();
            list = dbcontext.AchivementApplyContext.Where(a => a.SecrecyLevel == secrecylevel && a.SecrecyLevel <= SecrecyLevel && a.IsPass == true).OrderBy(c => c.AchievementID).ToList();
            if(list!=null)
            {
                return list;
            }
            else
            {
                return null;
            }
        }
       //根据成员查找
       public List<AchivementApply> FindByMember(string member,int ? SecrecyLevel)
       {
           List<AchivementApply> list = new List<AchivementApply>();
           list = dbcontext.AchivementApplyContext.Where(a => a.Member.Contains(member) && a.SecrecyLevel <= SecrecyLevel && a.IsPass == true).OrderBy(c => c.AchievementID).ToList();
           if (list != null)
           {
               return list;
           }
           else
           {
               return null;
           }
       }

       //根据成果ID找到成员
       public string FindName(int? userid)
       {
           List<AchivementApply> ag = new List<AchivementApply>();
           ag = dbcontext.AchivementApplyContext.Where(u => u.AchivementApplyID == userid && u.IsPass == true).ToList();
           if (ag.Count != 0)
               return ag.FirstOrDefault().Member;
           else
               return "";
       } 
    }
}
