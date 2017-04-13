/**编写人：李金秋
 * 时间：2014年6月20号
 * 功能：成果报奖表的相关操作
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
    public class BLLAchieveAward
    {
        DataBaseContext dbcontext = new DataBaseContext();

        //更新IsPass状态
        public void UpdateIsPass(int id, bool Ispass)
        {
            AchieveAward newach = dbcontext.AchieveAwardContext.Find(id);
            if (newach == null)
                return;
            newach.IsPass = Ispass;
            dbcontext.SaveChanges();
        }

        //录入成果报奖信息
        public void Insert(AchieveAward achieveAward)
        {
            try
            {
                dbcontext.AchieveAwardContext.Add(achieveAward);
                dbcontext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }
        //修改成果报奖信息
        public void Update(AchieveAward achieveAward)
        {
            try
            {
                //AchieveAward AchieveAward = new AchieveAward { AchieveAwardID = AchieveAwardID };
                AchieveAward AchieveAward = dbcontext.AchieveAwardContext.Find(achieveAward.AchieveAwardID);
                AchieveAward.AchievementID = achieveAward.AchievementID;
                AchieveAward.AwardType = achieveAward.AwardType;
                AchieveAward.AwardGrade = achieveAward.AwardGrade;
                AchieveAward.AwardName = achieveAward.AwardName;
                AchieveAward.AwardPeople = achieveAward.AwardPeople;
                AchieveAward.AwardUnit = achieveAward.AwardUnit;
                AchieveAward.SecrecyLevel = achieveAward.SecrecyLevel;
                AchieveAward.EntryPerson = achieveAward.EntryPerson;
                AchieveAward.IsPass = achieveAward.IsPass;
                AchieveAward.Member = achieveAward.Member;
                dbcontext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }
        //删除成果报奖信息
        public bool Delete(int achieveAwardID)
        {
            try
            {
                AchieveAward AchieveAward = dbcontext.AchieveAwardContext.Where(u => u.AchieveAwardID == achieveAwardID).FirstOrDefault();
                if (AchieveAward == null)
                    return true;
                dbcontext.AchieveAwardContext.Attach(AchieveAward);
                dbcontext.AchieveAwardContext.Remove(AchieveAward);
                dbcontext.SaveChanges();
                return true;
            }
            catch
            {
                throw;
            }
        }
        public bool Delete(int[] achieveAwardID)
        {
            try
            {
                int count = achieveAwardID.Count();
                for (int i = 0; i < count; i++)
                {
                    AchieveAward AchieveAward = new AchieveAward { AchieveAwardID = achieveAwardID[i] };
                    dbcontext.AchieveAwardContext.Attach(AchieveAward);
                    dbcontext.AchieveAwardContext.Remove(AchieveAward);
                }
                dbcontext.SaveChanges();
                return true;
            }
            catch
            {
                throw;
            }
        }
        //根据ID查询获奖人
        public string FindAchieveAwardPeople(int id)
        {
            List<AchieveAward> list = new List<AchieveAward>();
            list = dbcontext.AchieveAwardContext.Where(p => p.AchieveAwardID == id).ToList();
            if (list.Count() != 0)
            {
                if (list.FirstOrDefault().AwardPeople != "")
                {
                    return list.FirstOrDefault().AwardPeople;
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

        //根据ID查询成员
        public string FindMember(int id)
        {
            List<AchieveAward> list = new List<AchieveAward>();
            list = dbcontext.AchieveAwardContext.Where(p => p.AchieveAwardID == id).ToList();
            if (list.Count() != 0)
            {
                if (list.FirstOrDefault().AwardPeople != "")
                {
                    return list.FirstOrDefault().Member;
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

        //查看成果报奖信息
        public AchieveAward FindAll(int id)
        {
            return dbcontext.AchieveAwardContext.Find(id);
        }
        //分奖项按等级查看成果报奖信息
        public List<AchieveAward> FindByNameAndGrade( string AwardName, int? SecrecyLevel)
        {          
            return dbcontext.AchieveAwardContext.Where(p => p.AwardName.Contains(AwardName) && p.IsPass == true && p.SecrecyLevel <= SecrecyLevel).OrderBy(c => c.AwardGrade).ToList();                   
        }
        //分页
        public List<AchieveAward> FindPaged(int? SecrecyLevel)
        {
            return dbcontext.AchieveAwardContext.Where(u => u.SecrecyLevel <= SecrecyLevel && u.IsPass == true).OrderBy(c => c.AchievementID).ToList();
        }
        //找到获奖人为1个人的AwardPeople
        public List<string> FindAwardPeopleList()
        {
            List<string> list = new List<string>();
            var newlist = dbcontext.AchieveAwardContext .Where(u => u.IsPass == true).ToList();
            for (int i = 0; i < newlist.Count(); i++)
            {
                string People = newlist[i].AwardPeople.Replace("，", ",");
                string[] str = People.Split(',');
                if (str.Count() == 1)
                {
                    list.Add(People);
                }
            }
            return list;
        }
        //根据获奖人AwardPeople找到AwardID
        public List<int> FindAwardIDList(string AwardPeople)
        {
            List<int> list = new List<int>();
            var newlist = dbcontext.AchieveAwardContext.Where(u => u.AwardPeople == AwardPeople).ToList();
            for (int i = 0; i < newlist.Count(); i++)
            {
                list.Add(Convert.ToInt32(newlist[i].AchieveAwardID));
            }
            return list;
        }
        //根据成果ID找到成果报奖ID
        public List<int> FindByAchievement(int AchievementID)
        {
            List<int> list = new List<int>();
            var newlist = dbcontext.AchieveAwardContext.Where(u => u.AchievementID == AchievementID).ToList();
            for (int i = 0; i < newlist.Count(); i++)
            {
                list.Add(Convert.ToInt32(newlist[i].AchieveAwardID));
            }
            return list;
        }

        //按照报奖单位查找报奖信息
        public List<AchieveAward> FindByAwardUnit(string unit, int level)
        {
            return dbcontext.AchieveAwardContext.Where(u => u.AwardUnit == unit && u.SecrecyLevel <= level && u.IsPass == true).ToList();
        }

        //按照成员查找报奖信息
        public List<AchieveAward> FindByMember(string member, int level)
        {
            return dbcontext.AchieveAwardContext.Where(u => u.Member.Contains(member) && u.SecrecyLevel <= level && u.IsPass == true).ToList();
        }

        //按照保密级别查找报奖信息
        public List<AchieveAward> FindBySecrecyLevel(int secrecyLevel, int level)
        {
            return dbcontext.AchieveAwardContext.Where(u => u.SecrecyLevel == secrecyLevel && u.SecrecyLevel <= level && u.IsPass == true).ToList();
        }
    }
}
