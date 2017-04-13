/**编写人：李金秋
 * 时间：2014年6月20号
 * 功能：进修学习(接受)表的相关操作
 * 修改履历：1.时间：8月5日
 *            修改人：方淑云
 *            修改内容：修改插入方法，增加更新方法
 *          2.时间：8月8日
 *            修改人：张凡凡  
 *            修改内容：添加根据ID查找相应对象方法FindFurByID(int)
 *                     添加更新IsPass状态函数UpdateIsPass(bool)
 *                     添加根据名字和开始时间查找ID方法FindIdByNT(string, Datetime)
 *           
 **/

using Common.Entities;
using DataBase;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;

namespace BLHelper
{
    public class BLLFutherStudy
    {
        DataBaseContext dbcontext = new DataBaseContext();

        //录入进修学习(接受)信息
        public void Insert(FutherStudy futherStudy)
        {
            try
            {
                dbcontext.FutherStudyContext.Add(futherStudy);
                dbcontext.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                throw dbEx;
            }
        }
        //根据FutherStudyID删除信息
        public bool Delete(int futherStudyID)
        {
            try
            {
                FutherStudy futherStudy = new FutherStudy { FutherStudyID = futherStudyID };
                dbcontext.FutherStudyContext.Attach(futherStudy);
                dbcontext.FutherStudyContext.Remove(futherStudy);
                dbcontext.SaveChanges();
                return true;
            }
            catch
            {
                throw;
            }
        }
    
        //分年份查看进修学习信息(进修开始时间)
        public List<FutherStudy> FindByYear(int year, int? level)
        {
            return dbcontext.FutherStudyContext.Where(p => p.LearnBeginTime.Value.Year == year && p.SecrecyLevel <= level && p.IsPass == true).ToList();
        }
        //分页
        public List<FutherStudy> FindPaged(int? SecrecyLevel)
        {
            return dbcontext.FutherStudyContext.Where(u => u.SecrecyLevel <= SecrecyLevel && u.IsPass == true).OrderBy(c => c.FutherStudyID).ToList();
        }
        ////统计数量
        //public int Count(int? level)
        //{
        //    return dbcontext.FutherStudyContext.Where(f => f.SecrecyLevel <= level).Count();
        //}
        //public int CountByYear(int? level, int year)
        //{
        //    return dbcontext.FutherStudyContext.Where(f => f.SecrecyLevel <= level && f.LearnBeginTime.Value.Year == year && f.IsPass == true).Count();
        //}

        //根据ID查找相应对象FindFurByID(int)
        public FutherStudy FindFurByID(int ID)
        {
            var res = dbcontext.FutherStudyContext.Where(u => u.FutherStudyID == ID).FirstOrDefault();
            return res;
        }

        //更新IsPass状态UpdateIsPass(bool)
        public void UpdateIsPass(int furID, bool Ispass)
        {
            try
            {
                FutherStudy futherStudy = dbcontext.FutherStudyContext.Find(furID);
                if(futherStudy == null)
                    return;
                futherStudy.IsPass = Ispass;
                dbcontext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        //更新
        public void Update(FutherStudy fur)
        {
            try
            {
                FutherStudy newFur = dbcontext.FutherStudyContext.Find(fur.FutherStudyID);
                newFur.AgencyID = fur.AgencyID;
                newFur.Birthday = fur.Birthday;
                newFur.DocuType = fur.DocuType;
                newFur.Email = fur.Email;
                newFur.EntryPerson = fur.EntryPerson;
                newFur.Hometown = fur.Hometown;
                newFur.IDNum = fur.IDNum;
                newFur.IsPass = fur.IsPass;
                newFur.LearnBeginTime = fur.LearnBeginTime;
                newFur.LearnContent = fur.LearnContent;
                newFur.LearnEndTime = fur.LearnEndTime;
                newFur.LearnPlace = fur.LearnPlace;
                newFur.LearnSchool = fur.LearnSchool;
                newFur.Name = fur.Name;
                newFur.Profile = fur.Profile;
                newFur.Remark = fur.Remark;
                newFur.SecrecyLevel = fur.SecrecyLevel;
                newFur.Sex = fur.Sex;
                dbcontext.SaveChanges();
                
            }
            catch
            {
                throw;
            }
        }
        //根据ID查找model
        public FutherStudy FindModel(int id)
        {
            return dbcontext.FutherStudyContext.Find(id);
        }
        //根据名字和开始时间查找ID,FindIdByNT(string, Datetime)
        public int FindIdByNT(string name, DateTime starttime)
        {
            var result = dbcontext.FutherStudyContext.Where(u => u.Name == name && u.LearnBeginTime == starttime && u.IsPass == false).FirstOrDefault();
            if (result != null)
                return result.FutherStudyID;
            else
                return 0;
        }

        //根据人员姓名查询
        public List<FutherStudy> FindByName(string peoname, int level)
        {
            return dbcontext.FutherStudyContext.Where(u => u.Name == peoname && u.SecrecyLevel <= level && u.IsPass == true).ToList();

        }
    }
}
