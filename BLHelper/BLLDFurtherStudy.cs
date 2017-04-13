/**编写人：李金秋
 * 时间：2014年6月20号
 * 功能：进修学习表(派遣)表的相关操作
 * 修改履历：1.时间：8月4日
 *             修改人：方淑云
 *             修改内容：修改插入方法，增加分页方法
 *                      增加判断是否存在该UserID的方法
 *           2.时间：8月5日
 *            修改人:方淑云
 *            修改内容：添加根据ID查找所有信息的方法，添加更新数据的方法
 *           3.时间：8月11日
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
    public class BLLDFurtherStudy
    {
        DataBaseContext dbcontext = new DataBaseContext();

        //更新IsPass状态
        public void UpdateIsPass(int ID, bool isPass)
        {
            try
            {
                DFurtherStudy NewDFurtherStudy = dbcontext.DFurtherStudyContext.Find(ID);
                if (NewDFurtherStudy == null)
                    return;
                NewDFurtherStudy.IsPass = isPass;
                dbcontext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        //录入进修学习(接受)信息
        public void Insert(DFurtherStudy futherStudy)
        {
            try
            {
           
                dbcontext.DFurtherStudyContext.Add(futherStudy);
                dbcontext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }
        //更新
        public bool Update(DFurtherStudy df)
        {
            try
            {
                DFurtherStudy newdf = dbcontext.DFurtherStudyContext.Find(df.DFurtherStudyID);
                newdf.UserInfoID = df.UserInfoID;
                newdf.SecrecyLevel = df.SecrecyLevel;
                newdf.StudyContent = df.StudyContent;
                newdf.StudyPlace = df.StudyPlace;
                newdf.StudySchool = df.StudySchool;
                newdf.DEndTime = df.DEndTime;
                newdf.DBegainTime = df.DBegainTime;
                newdf.EntryPerson = df.EntryPerson;
                newdf.IsPass = df.IsPass;
                dbcontext.SaveChanges();
                return true;
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                throw e;
            }
        }
       //根据人员Id和开始时间查找ID
        public int FindIDs(int? id, DateTime? starttime, int? DFurtherStudyID)
        {
            List<DFurtherStudy> df = new List<DFurtherStudy>();
            df = dbcontext.DFurtherStudyContext.Where(d => d.DBegainTime == starttime && d.UserInfoID == id && d.DFurtherStudyID != DFurtherStudyID).ToList();
            if (df.Count() != 0)
            {
                return df.FirstOrDefault().DFurtherStudyID;
            }
            else
            {
                return 0;
            }
        }
        //根据人员Id和开始时间查找ID(不考虑状态)
        public int FindID(int? id, DateTime? starttime)
        {
            List<DFurtherStudy> df = new List<DFurtherStudy>();
            df = dbcontext.DFurtherStudyContext.Where(d => d.DBegainTime == starttime && d.UserInfoID == id ).ToList();
            if (df.Count() != 0)
            {
                return df.FirstOrDefault().DFurtherStudyID;
            }
            else
            {
                return 0;
            }
        }
        //将改变表中的审核
        public bool ChangePass(int df, bool ispass)
        {
            try
            {
                DFurtherStudy newdf = dbcontext.DFurtherStudyContext.Find(df);
                newdf.IsPass = ispass;
                dbcontext.SaveChanges();
                return true;
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                throw e;
            }
        }
        //判断是否存在该UserID 
        public bool ExitUserID(int UserID)
        {
            DFurtherStudy df = dbcontext.DFurtherStudyContext.Where(d => d.UserInfoID == UserID && d.IsPass == true).FirstOrDefault();
            if (df != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //根据ID查询所有信息
        public DFurtherStudy FindByID(int DfurtherStudyID)
        {
            return dbcontext.DFurtherStudyContext.Find(DfurtherStudyID);
        }
        //根据FutherStudyID删除信息
        public bool Delete(int DfutherStudyID)
        {
            try
            {
                DFurtherStudy futherStudy = dbcontext.DFurtherStudyContext.Where(u => u.DFurtherStudyID == DfutherStudyID).FirstOrDefault();
                dbcontext.DFurtherStudyContext.Attach(futherStudy);
                dbcontext.DFurtherStudyContext.Remove(futherStudy);
                dbcontext.SaveChanges();
                return true;
            }
            catch
            {
                throw;
            }
        }
     
        //分年份查看进修学习信息(进修开始时间)
        public List<DFurtherStudy> FindByYear( int year, int? SecrecyLevel)
        {
            return dbcontext.DFurtherStudyContext.Where(d => d.DBegainTime.Value.Year == year && d.SecrecyLevel <= SecrecyLevel && d.IsPass == true).OrderBy(c => c.UserInfoID).ToList();
        }
        //分页
        public List<DFurtherStudy> FindPaged( int? SecrecyLevel)
        {
            return dbcontext.DFurtherStudyContext.Where(d => d.SecrecyLevel <= SecrecyLevel && d.IsPass == true).OrderBy(c => c.UserInfoID).ToList();
        }
        //根据人员ID找到DFurtherStudyID
        public List<int> FindByUserInfoID(int UserInfoID)
        {
            List<int> list = new List<int>();
            var newlist = dbcontext.DFurtherStudyContext.Where(u => u.UserInfoID == UserInfoID).ToList();
            for (int i = 0; i < newlist.Count(); i++)
            {
                list.Add(Convert.ToInt32(newlist[i].DFurtherStudyID));
            }
            return list;
        }

        //根据人员姓名查询
        public List<DFurtherStudy> FindByName(int userid, int level)
        {
            return dbcontext.DFurtherStudyContext.Where(u => u.UserInfoID == userid && u.SecrecyLevel <= level && u.IsPass == true).ToList();

        }

    }
}
