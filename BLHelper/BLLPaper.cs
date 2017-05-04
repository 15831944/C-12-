/**编写人：李金秋
 * 时间：2014年6月20号
 * 功能：论文情况表的相关操作
 * 修改履历：1.添加按作者，按检索情况查询论文
 * 1.时间：8月11日
 *           修改人：张凡凡
 *           修改内容：添加更新IsPass状态函数UpdateIsPass(int,bool)
 *           2.时间：8月12日
 *           修改人：方淑云
 *           修改内容：添加FindRemark(int? PaperID)方法 FindByPaperName(string name), IsExitName(string name) ,FindByPaperID(int PaperID)
 *           3.修改人：吕博杨
 *             修改时间：2015年11月30日
 *             修改内容：添加按ID查找附件ID方法，在更新函数中添加对附件ID的写入支持
 **/
using Common.Entities;
using DataBase;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace BLHelper
{
  public  class BLLPaper
    {
        DataBaseContext dbcontext = new DataBaseContext();

        //更新IsPass状态
        public void UpdateIsPass(int ID, bool isPass)
        {
            try
            {
                Paper NewPaper = dbcontext.PaperContext.Find(ID);
                if (NewPaper == null)
                    return;
                NewPaper.IsPass = isPass;
                dbcontext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        //录入刊物论文信息
        public void Insert(Paper paper)
        {
            dbcontext.PaperContext.Add(paper);
            dbcontext.SaveChanges();
        }

        //修改论文信息
        public void Update(Paper aPaper)
        {
           
            Paper paper = dbcontext.PaperContext.Find(aPaper.PaperID);
            paper.EndPageNum = aPaper.EndPageNum;
            paper.ImpactFactor = aPaper.ImpactFactor;
            paper.JournalNum = aPaper.JournalNum;
            paper.WriterIdentity = aPaper.WriterIdentity;
            paper.PubliseState = aPaper.PubliseState;
            //paper.PaperForm = aPaper.PaperForm;         
            paper.PaperRank = aPaper.PaperRank;
            paper.PaperUnit = aPaper.PaperUnit;
            paper.PublicDate = aPaper.PublicDate;   
            paper.PublicJournalName = aPaper.PublicJournalName;
            paper.QuoteNum = aPaper.QuoteNum;
            paper.RetrieveSituation = aPaper.RetrieveSituation;
            paper.SerialNum = aPaper.SerialNum;
            paper.StartPageNum = aPaper.StartPageNum;
            paper.Subject = aPaper.Subject;
            paper.VolumesNum = aPaper.VolumesNum;
            paper.Remark = aPaper.Remark;
            paper.AchievementID = aPaper.AchievementID;
            paper.PaperPeople = aPaper.PaperPeople;
            paper.IncludeNum = aPaper.IncludeNum;
            paper.FirstWriter = aPaper.FirstWriter;
            paper.MessageWriter = aPaper.MessageWriter;
            paper.MWAgency = aPaper.MWAgency;
            paper.SecrecyLevel = aPaper.SecrecyLevel;
            paper.HQuoteNum = aPaper.HQuoteNum;
            paper.EntryPerson = aPaper.EntryPerson;
            paper.IsPass = aPaper.IsPass;
            paper.Sort = aPaper.Sort;
            //lby ↓
            paper.AttachmentID = aPaper.AttachmentID;
            dbcontext.SaveChanges();
        }
      //删除论文信息
        public bool Delete(int paperID)
        {
            try
            {
                Paper paper = dbcontext.PaperContext.Where(u => u.PaperID == paperID).FirstOrDefault();
                
                if (paper != null)
                {
                    dbcontext.PaperContext.Attach(paper);
                    dbcontext.PaperContext.Remove(paper);
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
     
      //根据ID查找备注
        public string FindRemark(int? PaperID)
        {
            List<Paper> list = new List<Paper>();
            list = dbcontext.PaperContext.Where(p => p.PaperID == PaperID).ToList();
            if (list.Count() != 0)
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
        //根据ID查询附件ID
        public int FindAttachment(int id)
        {
            List<Paper> list = new List<Paper>();
            list = dbcontext.PaperContext.Where(a => a.PaperID == id).ToList();
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
        //根据ID查找收录号
        public string FindIncludeNum(int? PaperID)
        {
            List<Paper> list = new List<Paper>();
            list = dbcontext.PaperContext.Where(p => p.PaperID == PaperID).ToList();
            if (list.Count() != 0)
            {
                if (list.FirstOrDefault().IncludeNum != "")
                {
                    return list.FirstOrDefault().IncludeNum;
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
        //分部门查询论文
        public List<Paper> FindByAgency(string PaperUnit, int? SecrecyLevel)
        {
            return dbcontext.PaperContext.Where(p => p.PaperUnit == PaperUnit && p.SecrecyLevel <= SecrecyLevel && p.IsPass == true).OrderBy(c => c.PaperID).ToList();
        }
     
        //数量
        public int CountAgency(string PaperUnit, int? SecrecyLevel)
        {
            return dbcontext.PaperContext.Where(p => p.PaperUnit == PaperUnit && p.SecrecyLevel <= SecrecyLevel && p.IsPass == true).Count();
        }
        //分刊物等级查询论文
        public List<Paper> FindByPaperRank(string PaperRank, int? SecrecyLevel)
        {
            return dbcontext.PaperContext.Where(p => p.PaperRank == PaperRank && p.SecrecyLevel <= SecrecyLevel && p.IsPass == true).OrderBy(c => c.PaperID).ToList();
        }
      
        //分论文形式查询论文
        //public List<Paper> FindByPaperForm(string PaperForm, int? SecrecyLevel)
        //{
        //    return dbcontext.PaperContext.Where(p => p.PaperForm.Contains(PaperForm) && p.SecrecyLevel <= SecrecyLevel && p.IsPass == true).OrderBy(c => c.PaperID).ToList();
        //}
    
        //按发表时间查询论文
        public List<Paper> FindByPublicDate(int year, int? SecrecyLevel)
        {
            return dbcontext.PaperContext.Where(p => p.PublicDate.Value.Year == year && p.IsPass == true && p.SecrecyLevel <= SecrecyLevel).OrderBy(c => c.PaperID).ToList();
        }             
     
        //按检索情况查询论文信息
        public List<Paper> FindByRS(string stitu, int? SecrecyLevel)
        {
            return dbcontext.PaperContext.Where(p => p.RetrieveSituation == stitu && p.SecrecyLevel <= SecrecyLevel && p.IsPass == true).OrderBy(c => c.PaperID).ToList();
        }
      //数量
        public int CountRS(string stitu, int? SecrecyLevel)
        {
            return dbcontext.PaperContext.Where(p => p.RetrieveSituation == stitu && p.SecrecyLevel <= SecrecyLevel && p.IsPass == true).Count();
        }
      //根据ID查找所有
        public Paper FindAll(int PaperID)
        {
            return dbcontext.PaperContext.Find(PaperID);
        }
     
        //分页
        public List<Paper> FindPaged(int? SecrecyLevel)
        { 
            return dbcontext.PaperContext.Where(u => u.SecrecyLevel <= SecrecyLevel && u.IsPass == true).OrderBy(c => c.PaperID).ToList();
        }
      //数量
        public int Count(int? SecrecyLevel)
        {
            return dbcontext.PaperContext.Where(p => p.SecrecyLevel <= SecrecyLevel && p.IsPass == true).Count();
        }
     
         //根据论文名获取论文ID
        public int FindByPaperName(string name)
        {
            List<Paper> list = new List<Paper>();
            list = dbcontext.PaperContext.Where(p => p.Subject == name).ToList();
            if (list.Count() != 0)
            {
                return list.FirstOrDefault().PaperID;
            }
            else
            {
                return 0;
            }
        }

        //根据论文名和发布时间获取论文ID
        public int FindByPaperName_Time(string name,DateTime?Time)
        {
            List<Paper> list = new List<Paper>();
            list = dbcontext.PaperContext.Where(p => p.Subject == name&&p.PublicDate==Time).ToList();
            if (list.Count() != 0)
            {
                return list.FirstOrDefault().PaperID;
            }
            else
            {
                return 0;
            }
        }
    
        //根据论文名获取论文ID(不包括当前ID)
        public int FindByPaperNames(string name,int id)
        {
            List<Paper> list = new List<Paper>();
            list = dbcontext.PaperContext.Where(p => p.Subject == name && p.PaperID != id).ToList();
            if (list.Count() != 0)
            {
                return list.FirstOrDefault().PaperID;
            }
            else
            {
                return 0;
            }
        }
      //判断是否存在该论文名
        public Paper IsExitName(string name)
        {
            return dbcontext.PaperContext.Where(p=>p.Subject == name).FirstOrDefault();
         
        }
      //根据ID获取成果ID
        public int FindByPaperID(int PaperID)
        {
            List<Paper> list = new List<Paper>();
            list = dbcontext.PaperContext.Where(p => p.PaperID == PaperID).ToList();
            if (list.Count() != 0)
            {
                return Convert.ToInt32(list.FirstOrDefault().AchievementID);
            }
            else
            {
                return 0;
            }
        }
      //根据论文ID查询论文作者
        public string FindWriter(int id)
        {
            List<Paper> list = new List<Paper>();
            list = dbcontext.PaperContext.Where(p => p.PaperID == id).ToList();
            if (list.Count() != 0)
            {
                if (list.FirstOrDefault().PaperPeople != "")
                {
                    return list.FirstOrDefault().PaperPeople;
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

        //分人员查看论文信息
        public List<Paper> FindByPaperPeople(string PaperPeople, int? SecrecyLevel)
        {
            List<Paper> res = new List<Paper>();
            List<Paper> Paperlist = dbcontext.PaperContext.Where(p=>p.SecrecyLevel <= SecrecyLevel).ToList();
            for (int i = 0; i < Paperlist.Count; i++)
            {
                Paper pa = Paperlist[i];
                string People = pa.PaperPeople.Replace("，", ",");
                string[] str = People.Split(',');
                foreach (string ss in str)
                {
                    if (ss.Contains(PaperPeople))
                    {
                        res.Add(pa);
                        break;
                    }
                }
            }
            return res;
        }
        //找到作者为1个人的PaperPeople
        public List<string> FindPaperPeopleList()
        {
            List<string> list = new List<string>();
            var newlist = dbcontext.PaperContext .Where(u => u.IsPass == true).ToList();
            for (int i = 0; i < newlist.Count(); i++)
            {
                string People = newlist[i].PaperPeople .Replace("，", ",");
                string[] str = People.Split(',');
                if (str.Count() == 1)
                {
                    list.Add(People);
                }
            }
            return list;
        }
        //根据作者PaperPeople找到PaperID
        public List<int> FindPaperIDList(string PaperPeople)
        {
            List<int> list = new List<int>();
            var newlist = dbcontext.PaperContext.Where(u => u.PaperPeople == PaperPeople).ToList();
            for (int i = 0; i < newlist.Count(); i++)
            {
                list.Add(Convert.ToInt32(newlist[i].PaperID ));
            }
            return list;
        }

        //按第一作者查询
        public List<Paper> FindByFirstWriter(string firstwriter, int level)
        {
            return dbcontext.PaperContext.Where(u => u.FirstWriter == firstwriter && u.SecrecyLevel <= level && u.IsPass == true).ToList();
        }

        //按通讯作者查询
        public List<Paper> FindByMessageWriter(string messagewriter, int level)
        {
            return dbcontext.PaperContext.Where(u => u.MessageWriter == messagewriter && u.SecrecyLevel <= level && u.IsPass == true).ToList();
        }

        //分第一作者身份查询论文
        public List<Paper> FindByFirstWriterPosition(string PaperFirstWriter,int level)
        {
            return dbcontext.PaperContext.Where(p => p.WriterIdentity == PaperFirstWriter && p.SecrecyLevel<=level && p.IsPass == true).OrderBy(c => c.PaperID).ToList();
        }
        //按发表状态查询论文
        public List<Paper> FindByPublishState(string PubliseState, int level)
        {
            return dbcontext.PaperContext.Where(p => p.PubliseState == PubliseState && p.SecrecyLevel <= level && p.IsPass == true).OrderBy(c => c.PaperID).ToList();
        }
        //根据分类名称查询
        public List<Paper> FindByFirstWriterPosition(string WriterIdentity)
        {
            return dbcontext.PaperContext.Where(u => u.WriterIdentity == WriterIdentity).ToList();
        }
        //查找所有机构名称
        public List<Paper> FindByFirstWriterPosition()
        {

            List<Paper> list = (from a in dbcontext.PaperContext select new { WriterIdentity = a.WriterIdentity }).ToList().Select(a => new Paper { WriterIdentity = a.WriterIdentity }).ToList();
            return list;
        }
       
    }
}
