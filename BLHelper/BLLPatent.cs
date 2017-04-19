/**编写人：李金秋
 * 时间：2014年6月20号
 * 功能：专利情况表的相关操作
 * 修改履历：1.修改人：吕博扬
 *            修改时间：2015年9月19日
 *            修改内容：添加“成员”字段的更新语句（Update函数中）以及FindMember(int id)成员函数
 *          2.修改人：吕博杨
 *            修改时间：2015年11月28日
 *            修改内容：将附件分为申请书、专利证书进行存储
 *                     对新增字段（专利授权号、专利证书号）提供代码支持
 **/
using Common.Entities;
using DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLHelper
{
    public class BLLPatent
    {
        DataBaseContext dbcontext = new DataBaseContext();

        //更新IsPass状态
        public void UpdateIsPass(int ID, bool isPass)
        {
            try
            {
                Patent NewPatent = dbcontext.PatentContext.Find(ID);
                if (NewPatent == null)
                    return;
                NewPatent.IsPass = isPass;
                dbcontext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        //删除
        public int[] Delete(int ID)
        {
            try
            {
                Patent Patent = dbcontext.PatentContext.Where(u => u.PatentID == ID).FirstOrDefault();//new Announcement { AnnouncementID = announcementID };
                //lby ↓
                int[] attachID = new int[2];
                if (Patent.Attachment_Patent!=null)
                    attachID[0] = Patent.Attachment_Patent.Value;
                if (Patent.Attachment_Application!=null)
                    attachID[1] = Patent.Attachment_Application.Value;

                dbcontext.PatentContext.Attach(Patent);
                dbcontext.PatentContext.Remove(Patent);
                dbcontext.SaveChanges();
                return attachID;
            }
            catch
            {
                throw;
            }
        }

        //录入专利信息
        public void Insert(Patent patent)
        {
            try
            {
                dbcontext.PatentContext.Add(patent);
                dbcontext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }
       
        //修改专利信息
        public void Update(Patent aPatent)
        {
            Patent patent = dbcontext.PatentContext.Find(aPatent.PatentID);
            patent.ApplicationTime = aPatent.ApplicationTime;
            patent.CertificateNumber = aPatent.CertificateNumber;
            patent.Comment = aPatent.Comment;
            patent.PatentDepartment = aPatent.PatentDepartment;
            patent.PatentForm = aPatent.PatentForm;
            patent.PatentName = aPatent.PatentName;
            patent.PatentNumber = aPatent.PatentNumber;
            patent.AccreditTime = aPatent.AccreditTime;
            patent.AchievementID = aPatent.AchievementID;
            //lby ↓
            patent.Attachment_Application = aPatent.Attachment_Application;
            patent.Attachment_Patent = aPatent.Attachment_Patent;
            patent.PatentAuthorization = aPatent.PatentAuthorization;
            patent.PatentCertificate = aPatent.PatentCertificate;

            patent.GivenUnit = aPatent.GivenUnit;
            patent.FirstPeople = aPatent.FirstPeople;
            patent.AgencyID = aPatent.AgencyID;
            patent.Fund = aPatent.Fund;
            patent.PatentCondition = aPatent.PatentCondition;
            patent.ApplyNum = aPatent.ApplyNum;
            patent.SecrecyLevel = aPatent.SecrecyLevel;
            patent.State = aPatent.State;
            patent.PatentPeople = aPatent.PatentPeople;
            patent.EntryPerson = aPatent.EntryPerson;
            patent.IsPass = aPatent.IsPass;
            patent.Member = aPatent.Member;
            dbcontext.SaveChanges();
        }
        //分部门查看专利信息
        public List<Patent> FindByPatentDepartment( string PatentDepartment, int? SecrecyLevel)
        {
            List<Patent> res = new List<Patent>();
            List<Patent> patentlist = dbcontext.PatentContext.Where(p=>p.SecrecyLevel <= SecrecyLevel).ToList();
            for (int i = 0; i < patentlist.Count; i++)
            {
                Patent pa = patentlist[i];
                string department = pa.PatentDepartment.Replace("，", ",");
                string[] str = department.Split(',');
                foreach (string ss in str)
                {
                    if (ss.Contains(PatentDepartment))
                    {
                        res.Add(pa);
                        break;
                    }
                }
            }
            return res;
        }
        //分人员查看专利信息
        public List<Patent> FindByPatentPeople(string PatentPeople, int? SecrecyLevel)
        {
            List<Patent> res = new List<Patent>();
            List<Patent> patentlist = dbcontext.PatentContext.Where(p => p.SecrecyLevel <= SecrecyLevel).ToList();
            for (int i = 0; i < patentlist.Count; i++)
            {
                Patent pa = patentlist[i];
                string People = pa.PatentPeople.Replace("，", ",");
                string[] str = People.Split(',');
                foreach (string ss in str)
                {
                    if (ss.Contains(PatentPeople))
                    {
                        res.Add(pa);
                        break;
                    }
                }
            }
            return res;
        }
        //按成员查询专利信息
        public List<Patent> FindByMember(string member, int SecrecyLevel)
        {
            return dbcontext.PatentContext.Where(p => p.Member.Contains(member) && p.IsPass == true && p.SecrecyLevel <= SecrecyLevel).OrderBy(c => c.PatentID).ToList();
        }
        //按保密级别查看专利信息
        public List<Patent> FindBySecrecyLevel(int secrecy, int SecrecyLevel)
        {
            return dbcontext.PatentContext.Where(p => p.SecrecyLevel == secrecy && p.IsPass == true && p.SecrecyLevel <= SecrecyLevel).OrderBy(c => c.PatentID).ToList();
        }

        //按申请时间查询专利信息
        public List<Patent> FindByApplicationTime(int year , int? SecrecyLevel)
        {
            return dbcontext.PatentContext.Where(p => p.ApplicationTime.Value.Year == year && p.IsPass == true && p.SecrecyLevel <= SecrecyLevel).OrderBy(c => c.PatentID).ToList();
        }

        //按授权时间查询专利信息 在人员专利表中查询
        public List<Patent> FindByAccreditTime( int year, int? SecrecyLevel)
        {
            return dbcontext.PatentContext.Where(p => p.AccreditTime.Value.Year == year && p.IsPass == true && p.SecrecyLevel <= SecrecyLevel).OrderBy(c => c.PatentID).ToList();
        }
        
        //根据PatentID查询专利信息
        public Patent FindByPatentID(int patentID)
        {
            return dbcontext.PatentContext.Find(patentID);
        }
        //根据PatentID查询专利信息
        public List<Patent> FindAll(int id)
        {
            return dbcontext.PatentContext.Where(p => p.PatentID == id).ToList();
        }

   
        //按专利类型查询
        public List<Patent> FindByPatentForm(string form, int? SecrecyLevel)
        {
            return dbcontext.PatentContext.Where(p => p.PatentForm == form && p.IsPass == true && p.SecrecyLevel <= SecrecyLevel).OrderBy(c => c.PatentID).ToList();
        }
      
        //分页
        public List<Patent> FindPaged( int? SecrecyLevel)
        {
            return dbcontext.PatentContext.Where(u => u.SecrecyLevel <= SecrecyLevel && u.IsPass == true).OrderBy(c => c.PatentID).ToList();
        }
        //根据ID查找附件ID
        public int[] FindAttachmentID(int id)
        {
            List<Patent> list = new List<Patent>();
            list = dbcontext.PatentContext.Where(a => a.PatentID == id).ToList();
            //lby ↓
            int[] attachID = new int[2];

            if (list.Count() != 0)
            {
                if (list.FirstOrDefault().Attachment_Application != null && list.FirstOrDefault().Attachment_Patent != null)
                {
                    //lby ↓
                    attachID[0] = list.FirstOrDefault().Attachment_Patent.Value;
                    attachID[1] = list.FirstOrDefault().Attachment_Application.Value;

                    return attachID;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
        //根据ID查询发明人
        public string FindWriter(int id)
        {
            List<Patent> list = new List<Patent>();
            list = dbcontext.PatentContext.Where(p => p.PatentID == id).ToList();
            if (list.Count() != 0)
            {
                if (list.FirstOrDefault().PatentPeople != "")
                {
                    return list.FirstOrDefault().PatentPeople;
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
            List<Patent> list = new List<Patent>();
            list = dbcontext.PatentContext.Where(p => p.PatentID == id).ToList();
            if (list.Count() != 0)
            {
                if (list.FirstOrDefault().Member != "")
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
        //根据专利名称查询是否存在该专利
        public bool FindByPaperName(string name)
        {
            Patent patent = new Patent();
            patent = dbcontext.PatentContext.Where(p => p.PatentName == name).FirstOrDefault();
            if (patent!= null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //找到发明人为1个人的PatentPeople
        public List<string> FindPatentPeopleList()
        {
            List<string> list = new List<string>();
            var newlist = dbcontext.PatentContext.Where(u => u.IsPass == true).ToList();
            for (int i = 0; i < newlist.Count(); i++)
            {
                string People = newlist[i].PatentPeople .Replace("，", ",");
                string[] str = People.Split(',');
                if (str.Count() == 1)
                {
                    list.Add(People);
                }
            }
            return list;
        }
        //根据发明人PatentPeople找到PatentID
        public List<int> FindPatentIDList(string PatentPeople)
        {
            List<int> list = new List<int>();
            var newlist = dbcontext.PatentContext.Where(u => u.PatentPeople == PatentPeople).ToList();
            for (int i = 0; i < newlist.Count(); i++)
            {
                list.Add(Convert.ToInt32(newlist[i].PatentID ));
            }
            return list;
        }        
    }
}
