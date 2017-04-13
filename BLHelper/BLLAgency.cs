/**编写人：李金秋
 * 时间：2014年6月20号
 * 功能：机构表的相关操作
 * 修改履历：  1.时间：8月4日
 *              修改人：张凡凡
 *              操作：添加插入机构信息函数Insert(Agency, int);
 *                   添加根据机构名查找机构函数FindByName(string)
 *            2.时间：8月7日
 *              修改人：张凡凡
 *              操作：添加更新IsPass状态函数UpdatePass(bool)
 *                   更改函数FindAgenName(int)
 *                   添加根据id查找机构函数FindByid(int)
 **/
using System;
using DataBase;
using Common.Entities;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLHelper
{
    public class BLLAgency
    {

        DataBaseContext dbcontext = new DataBaseContext();
        //插入机构信息
        public void Insert(Agency agency)
        {
            try
            {
                dbcontext.AgencyContext.Add(agency);
                dbcontext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        //根据是否是内部机构，等级查询机构名
        public List<string> FindByIsGlo(string isGlobal, int secrelevel)
        {
            List<string> restult = new List<string>();
            var list = dbcontext.AgencyContext.Where(u => u.IsGlobal == isGlobal && u.SecrecyLevel <= secrelevel && u.IsPass == true).ToList();
            for (int i = 0; i < list.Count; i++)
                restult.Add(list[i].AgencyName);
            return restult;
        }

        //更新IsPass状态
        public void UpdatePass(int AgenID, bool isPass)
        {
            try
            {
                Agency NewAgency = dbcontext.AgencyContext.Find(AgenID);
                if (NewAgency == null)
                    return;
                NewAgency.IsPass = isPass;
                dbcontext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        //删除AgencyID = agencyID的机构信息
        public bool Delete(int agencyID)
        {
            try
            {
                Agency agency = new Agency { AgencyID = agencyID };
                dbcontext.AgencyContext.Attach(agency);
                dbcontext.AgencyContext.Remove(agency);
                dbcontext.SaveChanges();
                return true;
            }
            catch
            {
                throw;
            }
        }
        public bool Delete(int[] agencyID)
        {
            try
            {
                int count = agencyID.Count();
                for (int i = 0; i < count; i++)
                {
                    Agency agency = new Agency { AgencyID = agencyID[i] };
                    dbcontext.AgencyContext.Attach(agency);
                    dbcontext.AgencyContext.Remove(agency);
                }
                dbcontext.SaveChanges();
                return true;
            }
            catch
            {
                throw;
            }
        }

        //更新AgencyID = agencyID的机构信息为agency
        public void Update(Agency agency)
        {
            try
            {
                Agency NewAgency = dbcontext.AgencyContext.Find(agency.AgencyID);
                NewAgency.AgencyName = agency.AgencyName;
                NewAgency.ParentID = agency.ParentID;
                NewAgency.AgencyHeads = agency.AgencyHeads;
                NewAgency.AgencyNumber = agency.AgencyNumber;
                NewAgency.Research = agency.Research;
                NewAgency.FullTimeNumbers = agency.FullTimeNumbers;
                NewAgency.PartTimeNumbers = agency.PartTimeNumbers;
                NewAgency.Area = agency.Area;
                NewAgency.Location = agency.Location;
                NewAgency.SecrecyLevel = agency.SecrecyLevel;
                NewAgency.EntryPerson = agency.EntryPerson;
                NewAgency.IsPass = agency.IsPass;
                dbcontext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }
        //查找所有机构
        public List<Agency> FindAll(int level)
        {
            return dbcontext.AgencyContext.Where(u => u.SecrecyLevel <= level && u.IsPass == true).ToList();
        }

        //按照分类查找机构
        public List<Agency> FindByAg(string AgencyNum)
        {
            return dbcontext.AgencyContext.Where(u => u.AgencyNumber == AgencyNum && u.IsPass == true).ToList();
        }
        //根据机构名查找机构ID
        public int SelectAgencyID(string AgencyName)
        {
            if (AgencyName != null&&AgencyName!="请选择")
            {
                var results = dbcontext.AgencyContext.Where(a => a.AgencyName == AgencyName && a.IsPass == true).Select(a => new { a.AgencyID }).ToList();
                if (results.Count != 0)
                    return results.FirstOrDefault().AgencyID;
                else
                    return 0;
            }
            else
            {
                return 0;
            }

        }
        //查找机构名称
        public List<Agency> FindAgencyName(int? level)
        {
            List<Agency> list = (from a in dbcontext.AgencyContext
                                 where a.SecrecyLevel <= level && a.IsPass == true
                                 select new
                                 {
                                     AgencyID = a.AgencyID,
                                     AgencyName = a.AgencyName
                                 }).ToList().Select(a => new Agency
                    {
                        AgencyID = a.AgencyID,
                        AgencyName = a.AgencyName
                    }).ToList();

            list.GroupBy(u => u.ParentID).ToList();
            return list;
        }
        //查找所有机构名称
        public List<Agency> FindAllAgencyName()
        {

            List<Agency> list = (from a in dbcontext.AgencyContext
                                 select new
                                 {
                                     AgencyName = a.AgencyName
                                 }).ToList().Select(a => new Agency
                                 {
                                     AgencyName = a.AgencyName
                                 }).ToList();

            return list;
        }
        //分页
        public List<Agency> FindPaged(int pageIndex, int pageSize, int? SecrecyLevel)
        {
            return dbcontext.AgencyContext.Where(u => u.SecrecyLevel <= SecrecyLevel).OrderBy(c => c.AgencyID).Skip(pageIndex * pageSize).Take(pageSize).ToList();
        }

        //根据ID查找机构名字
        public string FindAgenName(int? id)
        {   
            List<Agency> ag = new List<Agency>();
            ag = dbcontext.AgencyContext.Where(u => u.AgencyID == id && u.IsPass == true).ToList();
            if (ag.Count != 0)
                return ag.FirstOrDefault().AgencyName;
            else
                return "";
        }
        //判断是否存在该机构名
        public bool IsAgencyName(string AgencyName)
        {
            Agency agency = dbcontext.AgencyContext.Where(a => a.AgencyName == AgencyName && a.IsPass == true).FirstOrDefault();
            if (agency != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //根据机构名称查找机构
        public Agency FindByName(string name)
        {
            return dbcontext.AgencyContext.Where(u => u.AgencyName == name && u.IsPass == true).FirstOrDefault();
        }
        //根据登录人员等级查看机构名称
        public List<string> FindAgencyBySecrecyLevel(int SecrecyLevel)
        {
            var q = from c in dbcontext.AgencyContext
                    where c.SecrecyLevel <= SecrecyLevel && c.IsPass == true
                    select new
                    {
                        c.AgencyName
                    }.AgencyName;
            return q.ToList();
        }

        //根据id查找机构
        public Agency FindByid(int agenid)
        {
            return dbcontext.AgencyContext.Find(agenid);
        }
    }
}
