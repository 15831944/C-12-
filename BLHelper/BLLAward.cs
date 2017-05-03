/**编写人：李金秋
 * 时间：2014年6月20号
 * 功能：获奖情况表的相关操作
 * 修改履历：1.时间：8月11日
 *           修改人：张凡凡
 *           修改内容：添加更新IsPass状态函数UpdateIsPass(int,bool)
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
   public class BLLAward
    {
        DataBaseContext dbcontext = new DataBaseContext();

        //更新IsPass状态
        public void UpdateIsPass(int ID, bool isPass)
        {
            try
            {
                Award Award = dbcontext.AwardContext.Find(ID);
                Award.IsPass = isPass;
                dbcontext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }
        //更新获奖情况
        public bool Update(Award  award)
        {
            try
            {
                if (award != null)
                {
                    Award newaward = dbcontext.AwardContext .Find(award.AwardID);
                    newaward.AwardName = award.AwardName;
                    newaward.AwardTime = award.AwardTime;
                    newaward.AwardwSpecies = award.AwardwSpecies;
                    newaward.GivAgency = award.GivAgency;
                    newaward.Grade = award.Grade;
                    newaward.AwardNum = award.AwardNum;
                    newaward.FirstAward = award.FirstAward;
                    newaward.AwardForm = award.AwardForm;
                    newaward.Remark = award.Remark;
                    newaward.SecrecyLevel = award.SecrecyLevel;
                    newaward.Acheivement = award.Acheivement;
                    newaward.AttachmentID = award.AttachmentID;
                    newaward.AwardPeople = award.AwardPeople;
                    newaward.EntryPerson = award.EntryPerson;
                    newaward.Unit = award.Unit;
                    newaward.Sort = award.Sort;
                    newaward.Member = award.Member;
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
        //根据ID查找附件ID
        public int FindAttachmentID(int id)
        {
            List<Award> list = new List<Award>();
            list = dbcontext.AwardContext.Where(a => a.AwardID == id).ToList();
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
        //录入获奖情况信息
        public void Insert(Award award)
        {
            try
            {
                dbcontext.AwardContext.Add(award);
                dbcontext.SaveChanges();
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                throw e;
            }
        }
        //根据AwardID删除获奖情况信息
        public int Delete(int awardID)
        {
            try
            {
                Award Award = dbcontext.AwardContext.Where(u => u.AwardID == awardID).FirstOrDefault();
                if (Award == null)
                    return 0;
                int attachID = Convert.ToInt32(Award.AttachmentID);
                dbcontext.AwardContext.Attach(Award);
                dbcontext.AwardContext.Remove(Award);
                dbcontext.SaveChanges();
                return attachID;
            }
            catch
            {
                throw;
            }
        }
      
        //按成果名称查询
        public List<Award> FindByAchievementName(string achievement, int? SecrecyLevel)
        {
            List<Award> res = new List<Award>();
            List<Award> Awardlist = dbcontext.AwardContext.Where(a => a.SecrecyLevel <= SecrecyLevel).ToList();
            for (int i = 0; i < Awardlist.Count; i++)
            {
                    Award aw = Awardlist[i];
                if(aw.Acheivement != null){
                    string award = aw.Acheivement.Replace("，", ",");                   
                        string[] str = award.Split(',');
                        foreach (string ss in str)
                        {
                            if (ss.Contains(achievement))
                            {
                                res.Add(aw);
                                break;
                            }
                        }
                    }
            }
            return res;
        }
        //按级别查询
        public List<Award> FindByAchievementRank( string Rank, int? SecrecyLevel)
        {
            return dbcontext.AwardContext.Where(p => p.Grade.Contains(Rank) && p.IsPass == true && p.SecrecyLevel <= SecrecyLevel).OrderBy(c => c.AwardID).ToList();
        }
        //按时间查询
        public List<Award> FindByAchievementTime( int year, int? SecrecyLevel)
        {
            return dbcontext.AwardContext.Where(p => p.AwardTime.Value.Year == year && p.IsPass == true && p.SecrecyLevel <= SecrecyLevel).OrderBy(c => c.AwardID).ToList();
        }
       //按颁奖部门查看
        public List<Award> FindByUnit( string unit, int? SecrecyLevel)
        {
            return dbcontext.AwardContext.Where(p => p.GivAgency.Contains(unit) && p.IsPass == true && p.SecrecyLevel <= SecrecyLevel).OrderBy(c => c.AwardID).ToList();
        }
       //分奖项名称按等级查看
        public List<Award> FindByAwardName(string AwardName, int? SecrecyLevel)
        {
            return dbcontext.AwardContext.Where(p => p.AwardName.Contains(AwardName) && p.IsPass == true && p.SecrecyLevel <= SecrecyLevel).OrderBy(c => c.Grade).ToList();
        }
       //根据ID查询备注
        public string FindRemark(int id)
        {
            List<Award> list = new List<Award>();
            list = dbcontext.AwardContext.Where(a => a.AwardID == id).ToList();
            if (list != null)
            {
                if (list.FirstOrDefault().Remark != "")
                {
                    return list.FirstOrDefault().Remark;
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
       ////根据ID查成员及排序
       // public string FindMember(int id)
       // {
       //     List<Award> list = new List<Award>();
       //     list = dbcontext.AwardContext.Where(a => a.AwardID == id).ToList();
       //     if (list != null)
       //     {
       //         if (list.FirstOrDefault().Member != "")
       //         {
       //             return list.FirstOrDefault().Member;
       //         }
       //         else
       //         {
       //             return "";
       //         }
       //     }
       //     else
       //     {
       //         return "";
       //     }
       // }
        //根据ID查成员及排序
        public string FindMember(int id)
        {
            List<Award> list = new List<Award>();
            list = dbcontext.AwardContext.Where(a => a.AwardID == id).ToList();
            if (list != null)
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
       //根据id查询单位
        public string FindUnit(int id)
        {
            List<Award> list = new List<Award>();
            list = dbcontext.AwardContext.Where(a => a.AwardID == id).ToList();
            if (list != null)
            {
                if (list.FirstOrDefault().Unit != "")
                {
                    return list.FirstOrDefault().Unit;
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
       //分部门查看科研成果(在成果基本信息表中查询)
        public List<Achievement> FindByAgencyID(int agencyID)
        {
            return dbcontext.AchievementContext.Where(p => p.AgencyID == agencyID && p.IsPass == true).ToList();
        }
       //分科研成果查看获奖情况(在获奖情况表中查询)
        public List<Award> FindByAchievementID(int AchievementID)
        {
            //return dbcontext.AwardContext.Where(p => p.Acheivement == AchievementID && p.IsPass == true).ToList();
            return null;
        }
       //分科研人员查看科研成果(在人员成果表中查询成果ID,在成果基本信息表中查看成果信息)
        public List<Achievement> FindByUserInfoID(int userInfoID)
        {
            List<StaffAchieve> list = dbcontext.StaffAchieveContext.Where(p => p.UserInfoID == userInfoID && p.IsPass == true).ToList();
            List<Achievement> listachievement = new List<Achievement>();
            for (int i = 0; i < list.Count(); i++)
            {
                listachievement[i] = dbcontext.AchievementContext.Find(list[i].AchievementID);
            }
            return listachievement;

        }
       //根据ID查询
        public List<Award> FindAll(int id)
        {
            return dbcontext.AwardContext.Where(a => a.AwardID == id).ToList();
        }
       //根据ID查询model
        public Award Findmodel(int id)
        {
            return dbcontext.AwardContext.Find(id);
        }
        //分页
        public List<Award> FindPaged( int? SecrecyLevel)
        {
            return dbcontext.AwardContext.Where(u => u.SecrecyLevel <= SecrecyLevel && u.IsPass == true).OrderBy(c => c.AwardID).ToList();
        }
        //添加根据AwardID查找EntryPerson的方法
        public Award Find(int AwardID)
        {
            return dbcontext.AwardContext.Find(AwardID);
        }

        //根据ID查询获奖人
        public string FindWriter(int id)
        {
            List<Award> list = new List<Award>();
            list = dbcontext.AwardContext.Where(p => p.AwardID == id).ToList();
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
        //分人员查看获奖信息
        public List<Award> FindByAwardPeople(string AwardPeople, int? SecrecyLevel)
        {
            List<Award> res = new List<Award>();
            List<Award> Awardlist = dbcontext.AwardContext.Where(a => a.SecrecyLevel <= SecrecyLevel).ToList();
            for (int i = 0; i < Awardlist.Count; i++)
            {
                Award aw = Awardlist[i];
                if (aw.AwardPeople != null)
                {
                    string People = aw.AwardPeople.Replace("，", ",");
                    string[] str = People.Split(',');
                    foreach (string ss in str)
                    {
                        if (ss.Contains(AwardPeople))
                        {
                            res.Add(aw);
                            break;
                        }
                    }
                }
            }
            return res;
        }
        //分获奖部门查看获奖信息
        public List<Award> AwardUnit(string AwardUnit, int? SecrecyLevel)
        {
            List<Award> res = new List<Award>();
            List<Award> Awardlist = dbcontext.AwardContext.Where(p => p.SecrecyLevel <= SecrecyLevel).ToList();
            for (int i = 0; i < Awardlist.Count; i++)
            {
                    Award aw = Awardlist[i];
                    if (aw.Unit != null)
                    {
                        string unit = aw.Unit.Replace("，", ",");
                        string[] str = unit.Split(',');
                        foreach (string ss in str)
                        {
                            if (ss.Contains(AwardUnit))
                            {
                                res.Add(aw);
                                break;
                            }
                        }
                    }
            }
            return res;
        }
        //判断该获奖名称是否存在
        public Award IsExitAwardName(string Name)
        {
            return dbcontext.AwardContext.Where(a => a.AwardName == Name).FirstOrDefault();
        }
       //找到获奖人为1个人的AwardPeople
        public List<string> FindAwardPeopleList()
        {
            List<string> list = new List<string>();
            var newlist = dbcontext.AwardContext.Where (u=>u.IsPass ==true).ToList();
            for (int i = 0; i < newlist.Count(); i++)
            {
                if (newlist[i].AwardPeople != null)
                {
                    string People = newlist[i].AwardPeople.Replace("，", ",");
                    string[] str = People.Split(',');
                    if (str.Count() == 1)
                    {
                        list.Add(People);
                    }
                }
            }
            return list;
        }
        //根据获奖人AwardPeople找到AwardID
        public List<int> FindAwardIDList(string AwardPeople)
        {
            List<int> list = new List<int>();
            var newlist = dbcontext.AwardContext.Where(u => u.AwardPeople == AwardPeople).ToList();
            for (int i = 0; i < newlist.Count(); i++)
            {
                list.Add(Convert.ToInt32(newlist[i].AwardID));
            }
            return list;
        }

       //根据获奖类型查询
       public List<Award> FindByAwardForm(string awardform, int level)
       {
           return dbcontext.AwardContext.Where(u => u.AwardForm == awardform && u.SecrecyLevel <= level && u.IsPass == true).ToList();
       }
       //根据保密级别查询
       public List<Award> FindBySecrecyLevel(int SecrecyLevels, int? SecrecyLevel)
       {
           return dbcontext.AwardContext.Where(p => p.SecrecyLevel == SecrecyLevels && p.IsPass == true && p.SecrecyLevel <= SecrecyLevel).ToList();
       }
       //根据成员查询
       public List<Award> FindByMember(string Member, int level)
       {
           return dbcontext.AwardContext.Where(u => u.Member.Contains(Member) && u.SecrecyLevel <= level && u.IsPass == true).ToList();
       }
    }
}
