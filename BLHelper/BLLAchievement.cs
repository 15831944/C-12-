/**编写人：李金秋
 * 时间：2014年6月20号
 * 功能：成果基本信息表的相关操作
 * 修改履历：1.时间：8月11日
 *           修改人：张凡凡
 *           修改内容：添加更新IsPass状态函数UpdateIsPass(int,bool)
 *           2.时间：8月12号
 *           修改人：方淑云
 *           修改内容：添加 FindByAchievementName(string name),FindAchieveName(int id)
 *           3.时间：2015年12月2日
 *           修改人：桌面
 *           修改内容：为新增的上传文件添加代码支持
 **/
using Common.Entities;
using DataBase;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLHelper
{
    public class BLLAchievement
    {
        DataBaseContext dbcontext = new DataBaseContext();

        //更新IsPass状态
        public void UpdateIsPass(int id, bool Ispass)
        {
            Achievement newachi = dbcontext.AchievementContext.Find(id);
            newachi.IsPass = Ispass;
            dbcontext.SaveChanges();
        }
     
        //插入成果基本信息
        public void Insert(Achievement achievement)
        {
            try
            {
                dbcontext.AchievementContext.Add(achievement);
                dbcontext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        //根据成果名称获取成果ID
        public int FindByAchievementName(string name)
        {
            List<Achievement> list = new List<Achievement>();
            list = dbcontext.AchievementContext.Where(a => a.AchievementName == name).ToList();
            if (list.Count() != 0)
            {
                if (list.FirstOrDefault().IsPass == true)
                {
                    return list.FirstOrDefault().AchievementID;
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
        //成员
        public string FindName(int? userid)
        {
            List<Achievement> ag = new List<Achievement>();
            ag = dbcontext.AchievementContext.Where(u => u.AchievementID == userid && u.IsPass == true).ToList();
            if (ag.Count != 0)
                return ag.FirstOrDefault().ProjectPeople;
            else
                return "";
        }
        //根据成果名称获取成果ID
        public List<int> FindByAchievementNamelist(string name)
        {
            List<int> list = new List<int>();
            var newlist = dbcontext.AchievementContext.Where(a => a.AchievementName.Contains(name)).ToList();
            for (int i = 0; i < newlist.Count(); i++)
            {
                list.Add(Convert.ToInt32(newlist[i].AchievementID));
            }
            return list;
        }
        //判断该成果名称是否存在
        public Achievement IsExitAchieveName(string AchieveName)
        {
            return dbcontext.AchievementContext.Where(a=>a.AchievementName == AchieveName).FirstOrDefault();
        }
        //根据成果ID获取成果名称
        public string FindAchieveName(int id)
        {
            List<Achievement> list = new List<Achievement>();
            list = dbcontext.AchievementContext.Where(a => a.AchievementID == id).ToList();
            if (list.Count() != 0)
            {
                return list.FirstOrDefault().AchievementName;
            }
            else
            {
                return "";
            }
        }
        //删除成果基本信息
        public int Delete(int achievementID)
        {
            try
            {
                Achievement Achievement = dbcontext.AchievementContext.Where(u => u.AchievementID == achievementID).FirstOrDefault();
                int attachID = Convert.ToInt32(Achievement.AttachmentID);
                dbcontext.AchievementContext.Attach(Achievement);
                dbcontext.AchievementContext.Remove(Achievement);
                dbcontext.SaveChanges();
                return attachID;
            }
            catch
            {
                throw;
            }
        }
   
        //更新成果信息
        public void Update(Achievement achievement)
        {
            try
            {
                Achievement Achievement = dbcontext.AchievementContext.Find(achievement.AchievementID);
                Achievement.AchievementName = achievement.AchievementName;
                Achievement.AgencyID = achievement.AgencyID;
                Achievement.AppraisalTime = achievement.AppraisalTime;
                Achievement.AppraisalUnit = achievement.AppraisalUnit;
                Achievement.ApprovalNum = achievement.ApprovalNum;
                Achievement.FirstFinishedPeople = achievement.FirstFinishedPeople;
                Achievement.ApRemarkRank = achievement.ApRemarkRank;
                Achievement.AttachmentID = achievement.AttachmentID;
                Achievement.MemberPage = achievement.MemberPage;
                Achievement.OpinionPage = achievement.OpinionPage;
                Achievement.SealPage = achievement.SealPage;
                Achievement.ProjectName = achievement.ProjectName;
                //Achievement.AchievementRank = achievement.AchievementRank;
                //Achievement.AchievementTime = achievement.AchievementTime;
                Achievement.ProjectInNum = achievement.ProjectInNum;//项目内部编号
                Achievement.ProjectForm = achievement.ProjectForm;//鉴定形式
                Achievement.ProjectLevel = achievement.ProjectLevel;//鉴定水平
                Achievement.ProjectRank = achievement.ProjectRank;//鉴定级别
                Achievement.SecrecyLevel = achievement.SecrecyLevel;
                Achievement.EntryPerson = achievement.EntryPerson;
                Achievement.ProjectPeople = achievement.ProjectPeople;
                Achievement.IsPass = achievement.IsPass;
                dbcontext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }
      
        //查看部门相关成果
        public List<Achievement> FindByAgency(int AgencyID, int? SecrecyLevel)
        {
            return dbcontext.AchievementContext.Where(p => p.AgencyID == AgencyID && p.IsPass == true && p.SecrecyLevel <= SecrecyLevel).OrderBy(c => c.AchievementID).ToList();
        }
        //查看项目相关成果
        public List<Achievement> FindByProject( string projectname, int? SecrecyLevel)
        {
            return dbcontext.AchievementContext.Where(p => p.ProjectName.Contains(projectname) && p.IsPass == true && p.SecrecyLevel <= SecrecyLevel).OrderBy(c => c.AchievementID).ToList();     
        }
        //根据人员ID查询成果ID
        //public List<int> SelectID(int id)
        //{
        //    List<int> list = new List<int>();
        //    var newlist = dbcontext.StaffAchieveContext.Where(p => p.UserInfoID == id).ToList();
        //    for (int i = 0; i < newlist.Count(); i++)
        //    {
        //        list.Add(Convert.ToInt32(newlist[i].AchievementID));
        //    }
        //    return list;
        //}
        //根据ID查询全部信息
        public Achievement Findmodel(int id)
        {
            return dbcontext.AchievementContext.Find(id);
        }
        //根据成果ID查询成果信息
        public List<Achievement> SelectAll( List<int> id, int? SecrecyLevel)
        {
            List<Achievement> res = new List<Achievement>();
            for (int i = 0; i < id.Count; i++)
            {
                int idd = id[i];
                res.AddRange(dbcontext.AchievementContext.Where(u => u.AchievementID == idd && u.SecrecyLevel <= SecrecyLevel && u.IsPass == true));
            }
            return res.ToList(); 
        }
        //根据成果名称查询
        public List<Achievement> FindByName(string name, int? SecrecyLevel)
        {
            return dbcontext.AchievementContext.Where(p => p.AchievementName.Contains(name)&& p.IsPass == true && p.SecrecyLevel <= SecrecyLevel).OrderBy(c => c.AchievementID).ToList();
        }
        //根据鉴定时间查询
        public List<Achievement> FindByTime( int year, int? SecrecyLevel)
        {
            return dbcontext.AchievementContext.Where(p => p.AppraisalTime.Value.Year == year && p.IsPass == true && p.SecrecyLevel <= SecrecyLevel).OrderBy(c => c.AchievementID).ToList();
        }
        //根据鉴定组织部门查询
        public List<Achievement> FindByUnit( string unit, int? SecrecyLevel)
        {
            return dbcontext.AchievementContext.Where(p => p.AppraisalUnit.Contains(unit) && p.IsPass == true && p.SecrecyLevel <= SecrecyLevel).OrderBy(c => c.AchievementID).ToList();
        }
        //根据鉴定评语级别查询
        public List<Achievement> FindByLevel( string level, int? SecrecyLevel)
        {
            return dbcontext.AchievementContext.Where(p => p.ApRemarkRank.Contains(level) && p.IsPass == true && p.SecrecyLevel <= SecrecyLevel).OrderBy(c => c.AchievementID).ToList();
        }
        //根据成果id查询成果基本信息
        public Achievement FindAll(int id)
        {
            return dbcontext.AchievementContext.Find(id);
        }
        //根据成果ID查询附件ID
        public int  FindAttachment(int id)
        {
            List<Achievement> list = new List<Achievement>();
            list = dbcontext.AchievementContext.Where(a => a.AchievementID == id).ToList();
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
        //分页
        public List<Achievement> FindPaged( int? SecrecyLevel)
        {
            return dbcontext.AchievementContext.Where(u => u.SecrecyLevel <= SecrecyLevel && u.IsPass == true).OrderBy(c => c.AchievementID).ToList();
        }

        //根据鉴定级别查询
        public List<Achievement> FindByProjectRank(string prorank, int level)
        {
            return dbcontext.AchievementContext.Where(u => u.ProjectRank == prorank && u.SecrecyLevel <= level && u.IsPass == true).ToList();
        }
        //根据保密级别查询
        public List<Achievement> FindBySecrecyLevel(int selevel, int level)
        {
            return dbcontext.AchievementContext.Where(u => u.SecrecyLevel == selevel && u.SecrecyLevel <= level && u.IsPass == true).ToList();
        }
       //根据成员模糊查询
        public List<Achievement> FindByMember(string smember, int level)
        {
            return dbcontext.AchievementContext.Where(u => u.ProjectPeople.Contains( smember) && u.SecrecyLevel <= level && u.IsPass == true).ToList();
        }

        //根据所属机构名称查询
        public List<Achievement> FindByAgencyName(int agencyid, int level)
        {
            return dbcontext.AchievementContext.Where(u => u.AgencyID == agencyid && u.SecrecyLevel <= level && u.IsPass == true).ToList();
        }


    }
}
