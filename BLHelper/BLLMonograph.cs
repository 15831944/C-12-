/**编写人：李金秋
 * 时间：2014年6月20号
 * 功能：专著情况表的相关操作
 * 修改履历：7月23，添加按作者和按类别查询
 *          2.时间：8月11日
 *           修改人：张凡凡
 *           修改内容：添加更新IsPass状态函数UpdateIsPass(int,bool)
 *                      修改了FindAll()
 *           3.时间：8月20日
 *            修改人：方淑云
 *           修改内容：添加FindByUser(List<int> ID, int? SecrecyLevel)，FindFAttachmentID(int id)，FindRemark(int ID)，FindBAttachmentID(int id)
 **/
using Common.Entities;
using DataBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BLHelper
{
   public class BLLMonograph
    {
        DataBaseContext dbcontext = new DataBaseContext();

        //更新IsPass状态
        public void UpdateIsPass(int ID, bool isPass)
        {
            try
            {
                Monograph NewMonograph = dbcontext.MonographContext.Find(ID);
                if (NewMonograph == null)
                    return;
                NewMonograph.IsPass = isPass;
                dbcontext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        //录入著作信息
        public void Insert(Monograph monograph)
        {
            try
            {
                dbcontext.MonographContext.Add(monograph);
                dbcontext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }
       //删除著作信息
        public bool Delete(int monographID)
        {
            try
            {
                Monograph monograph = dbcontext.MonographContext.Where(u => u.MonographID == monographID).FirstOrDefault();
                if (monograph != null)
                {
                    dbcontext.MonographContext.Attach(monograph);
                    dbcontext.MonographContext.Remove(monograph);
                    dbcontext.SaveChanges();
                    return true;
                }
                else
                    return true;
            }
            catch
            {
                throw;
            }
        }
      
        //根据MonographID修改著作信息
        public void Update(Monograph aMonograph)
        {
            Monograph monograph = dbcontext.MonographContext.Find(aMonograph.MonographID);
            monograph.BookNuber = aMonograph.BookNuber;          
            monograph.IssueRegin = aMonograph.IssueRegin;
            monograph.MonographName = aMonograph.MonographName;
            monograph.PUblicationTime = aMonograph.PUblicationTime;
            monograph.WriterIdentity = aMonograph.WriterIdentity;
            monograph.Publisher = aMonograph.Publisher;
            monograph.Remark = aMonograph.Remark;
            monograph.Revision = aMonograph.Revision;    
            monograph.AchievementID = aMonograph.AchievementID;         
            monograph.BAttachmentID = aMonograph.BAttachmentID;
            monograph.FAttachmentID = aMonograph.FAttachmentID;
            monograph.SecrecyLevel = aMonograph.SecrecyLevel;
            monograph.Sort = aMonograph.Sort;
            monograph.MonographPeople = aMonograph.MonographPeople;
            monograph.MonographType = aMonograph.MonographType;
            monograph.FirstWriter = aMonograph.FirstWriter;
            monograph.EntryPerson = aMonograph.EntryPerson;
            monograph.IsPass = aMonograph.IsPass;
            dbcontext.SaveChanges();
        }

        //查询著作信息
        public Monograph FindAll(int ID)
        {
            return dbcontext.MonographContext.Find(ID);

        }
        public Monograph IsExitAttacment(int ID)
        {
            return dbcontext.MonographContext.Where(m => m.MonographID == ID).FirstOrDefault();
        }
        //按时间查询著作信息
        public List<Monograph> FindByPublicTime(int year, int? SecrecyLevel)
        {
            return dbcontext.MonographContext.Where(p => p.PUblicationTime.Value.Year == year && p.IsPass == true && p.SecrecyLevel <= SecrecyLevel).OrderBy(c => c.MonographID).ToList();
        }

        //根据专著ID(多个)查询
        public List<Monograph> FindByUser(List<int> ID, int? SecrecyLevel)
        {
            List<Monograph> res = new List<Monograph>();
            for (int i = 0; i < ID.Count; i++)
            {
                int id = ID[i];
                res.AddRange(dbcontext.MonographContext.Where(u => u.MonographID == id && u.SecrecyLevel <= SecrecyLevel && u.IsPass == true));
            }
            return res.ToList(); 
        }

        //按类别查询
        public List<Monograph> FindBySort( string  sort, int? SecrecyLevel)
        {
            return dbcontext.MonographContext.Where(u => u.Sort.Contains(sort) && u.IsPass == true && u.SecrecyLevel <= SecrecyLevel).OrderBy(c => c.MonographID).ToList();
        }

        //分页
        public List<Monograph> FindPaged(int? SecrecyLevel)
        {
            return dbcontext.MonographContext.Where(u => u.SecrecyLevel <= SecrecyLevel && u.IsPass == true).OrderBy(c => c.Sort).ToList();
        }
        //根据ID查询备注
        public string FindRemark(int ID)
        {
            List<Monograph> list = new List<Monograph>();
            list = dbcontext.MonographContext.Where(p => p.MonographID == ID).ToList();
            if (list.Count() != 0)
            {
                return list.FirstOrDefault().Remark;
            }
            else
            {
                return "";
            }
        }
        //根据ID查找封面附件ID
        public int FindFAttachmentID(int id)
        {
            List<Monograph> list = new List<Monograph>();
            list = dbcontext.MonographContext.Where(a => a.MonographID == id).ToList();
            if (list.Count() != 0)
            {
                if (list.FirstOrDefault().FAttachmentID != null)
                {
                    return list.FirstOrDefault().FAttachmentID.Value;
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
        //根据ID查找版权页附件ID
        public int FindBAttachmentID(int id)
        {
            List<Monograph> list = new List<Monograph>();
            list = dbcontext.MonographContext.Where(a => a.MonographID == id).ToList();
            if (list.Count() != 0)
            {
                if (list.FirstOrDefault().BAttachmentID != null)
                {
                    return list.FirstOrDefault().BAttachmentID.Value;
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
       //判断是否存在该专著名称
       public Monograph IsEixtName(string name)
       {
           return dbcontext.MonographContext.Where(m=>m.MonographName == name).FirstOrDefault();
       }

       //根据ID查询著作人
       public string FindWriter(int id)
       {
           List<Monograph> list = new List<Monograph>();
           list = dbcontext.MonographContext.Where(p => p.MonographID == id).ToList();
           if (list.Count() != 0)
           {
               if (list.FirstOrDefault().MonographPeople != "")
               {
                   return list.FirstOrDefault().MonographPeople;
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
       //分人员查看专著信息
       public List<Monograph> FindByMonographPeople(string MonographPeople, int? SecrecyLevel)
       {
           List<Monograph> res = new List<Monograph>();
           List<Monograph> Monographlist = dbcontext.MonographContext.Where(m=>m.SecrecyLevel <= SecrecyLevel).ToList();
           for (int i = 0; i < Monographlist.Count; i++)
           {
               Monograph mo = Monographlist[i];
               string People = mo.MonographPeople.Replace("，", ",");
               string[] str = People.Split(',');
               foreach (string ss in str)
               {
                   if (ss.Contains(MonographPeople))
                   {
                       res.Add(mo);
                       break;
                   }
               }
           }
           return res;
       }
       //找到专著作者为1个人的MonographPeople
       public List<string> FindMonographPeopleList()
       {
           List<string> list = new List<string>();
           var newlist = dbcontext.MonographContext.Where(u => u.IsPass == true).ToList();
           for (int i = 0; i < newlist.Count(); i++)
           {
               string People = newlist[i].MonographPeople .Replace("，", ",");
               string[] str = People.Split(',');
               if (str.Count() == 1)
               {
                   list.Add(People);
               }
           }
           return list;
       }
       //根据专著作者MonographPeople找到MonographID
       public List<int> FindMonographIDList(string MonographPeople)
       {
           List<int> list = new List<int>();
           var newlist = dbcontext.MonographContext.Where(u => u.MonographPeople == MonographPeople).ToList();
           for (int i = 0; i < newlist.Count(); i++)
           {
               list.Add(Convert.ToInt32(newlist[i].MonographID ));
           }
           return list;
       }

       //按专著名称查询
       public List<Monograph> FindByMonographName(string mononame, int level)
       {
           return dbcontext.MonographContext.Where(u => u.MonographName == mononame && u.SecrecyLevel <= level && u.IsPass == true).ToList();
       }

       //按第一作者查询
       public List<Monograph> FindByFirstWriter(string firstwriter, int level)
       {
           return dbcontext.MonographContext.Where(u => u.FirstWriter == firstwriter && u.SecrecyLevel <= level && u.IsPass == true).ToList();
       }
       //按第一作者身份查询
       public List<Monograph> FindByFirstWriterPosition(string WriterIdentity, int level)
       {
           return dbcontext.MonographContext.Where(u => u.WriterIdentity == WriterIdentity && u.SecrecyLevel <= level && u.IsPass == true).ToList();
       }
    }
}
