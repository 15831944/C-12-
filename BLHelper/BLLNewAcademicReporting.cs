/**编写人：刘诚中
 * 时间：2014年10月31号
 * 功能：新学术报告表的相关操作
 * 修改履历:         
 **/
using System;
using DataBase;
using Common.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLHelper
{
    public class BLLNewAcademicReporting
    {
        DataBaseContext dbcontext = new DataBaseContext();

        ////增
        //
        //插入新学术报告信息
       public void Insert(NewAcademicReporting newAcademicReporting)
        {
            try
            {
                dbcontext.NewAcademicReportingContext.Add(newAcademicReporting);
                dbcontext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }


        ////删
        //
        //根据newAcademicReportingID删除报告信息
        public bool Delete(int newAcademicReportingID)
        {
            try
            {
                NewAcademicReporting newAcademicReporting = dbcontext.NewAcademicReportingContext.Find(newAcademicReportingID);
                dbcontext.NewAcademicReportingContext.Attach(newAcademicReporting);
                dbcontext.NewAcademicReportingContext.Remove(newAcademicReporting);
                dbcontext.SaveChanges();
                return true;
            }
            catch
            {
                throw;
            }
        }
        public bool Delete(int[] newAcademicReportingID)
        {
            try
            {
                int count = newAcademicReportingID.Count();
                for (int i = 0; i < count; i++)
                {
                    NewAcademicReporting newAcademicReporting = dbcontext.NewAcademicReportingContext.Find(newAcademicReportingID);
                    dbcontext.NewAcademicReportingContext.Attach(newAcademicReporting);
                    dbcontext.NewAcademicReportingContext.Remove(newAcademicReporting);
                }
                dbcontext.SaveChanges();
                return true;
            }
            catch
            {
                throw;
            }
        }
        //删除附件
        public void UpdateAttachment(int ID)
        {
            try
            {
                NewAcademicReporting NewNAReporting = dbcontext.NewAcademicReportingContext.Find(ID);
                NewNAReporting.AttachmentID = null;
                dbcontext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }


        ////改
        //
        //更新报告信息
        public void Update(NewAcademicReporting newAcademicReporting)
        {
            try
            {
                NewAcademicReporting NewNAReporting = dbcontext.NewAcademicReportingContext.Find(newAcademicReporting.NewAcademicReportingID);
                NewNAReporting.ReportPeople = newAcademicReporting.ReportPeople;
                NewNAReporting.JobName = newAcademicReporting.JobName;
                NewNAReporting.JobMission = newAcademicReporting.JobMission;
                NewNAReporting.ReportUnit = newAcademicReporting.ReportUnit;
                NewNAReporting.Report = newAcademicReporting.Report;
                NewNAReporting.ReportTele = newAcademicReporting.ReportTele;
                NewNAReporting.Remark = newAcademicReporting.Remark;
                NewNAReporting.AcademicTitle = newAcademicReporting.AcademicTitle;
                NewNAReporting.ReportName = newAcademicReporting.ReportName;
                NewNAReporting.ReportTime = newAcademicReporting.ReportTime;
                NewNAReporting.ReportPlace = newAcademicReporting.ReportPlace;
                NewNAReporting.ApplyFund = newAcademicReporting.ApplyFund;
                NewNAReporting.PeopleCount = newAcademicReporting.PeopleCount;
                NewNAReporting.Organizers = newAcademicReporting.Organizers;
                NewNAReporting.Coorganizer = newAcademicReporting.Coorganizer;
                NewNAReporting.AttachmentID = newAcademicReporting.AttachmentID;
                NewNAReporting.ReportType = newAcademicReporting.ReportType;
                NewNAReporting.MajorPeople = newAcademicReporting.MajorPeople;
                dbcontext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }


        ////查
        //
        //根据newAcademicReportingID查询附件ID
        public int FindAttachmentID(int newAcademicReportingID)
        {
            var newAcademicReporting = dbcontext.NewAcademicReportingContext.Find(newAcademicReportingID);
            if (newAcademicReporting != null)
            {
                if (newAcademicReporting.AttachmentID != null)
                    return Convert.ToInt32(newAcademicReporting.AttachmentID);
                else
                    return 0;
            }
            else
                return 0;
        }
        //根据 报告名称查询报告信息
        public List<NewAcademicReporting> FindByReportName(int SecrecyLevel, string ReportName)
        {
            return dbcontext.NewAcademicReportingContext.Where(p => p.ReportName == ReportName && p.SecrecyLevel <= SecrecyLevel && p.IsPass == true).ToList();
        }
        //根据NAReportingID查询报告
        public NewAcademicReporting FindByNAReportingID(int NAReportingID)
        {
            return dbcontext.NewAcademicReportingContext.Find(NAReportingID);
        }    
        //根据报告名称查询
        public List<NewAcademicReporting> FindByRN(string RN, int SecrecyLevel)
        {
            return dbcontext.NewAcademicReportingContext.Where(a => a.ReportName.Contains(RN) && a.SecrecyLevel <= SecrecyLevel && a.IsPass == true).ToList();
        }


        ////附加功能
        //
        //分页
        public List<NewAcademicReporting> FindPaged(int? SecrecyLevel)
        {
            return dbcontext.NewAcademicReportingContext.Where(u => u.SecrecyLevel <= SecrecyLevel && u.IsPass == true).ToList();
        }
        //更新IsPass状态
        public void UpdatePass(int ID, bool isPass)
        {
            try
            {
                NewAcademicReporting newAcademicReporting = dbcontext.NewAcademicReportingContext.Find(ID);
                if (newAcademicReporting == null)
                    return;
                newAcademicReporting.IsPass = isPass;
                dbcontext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        //根据报告时间查询
        public List<NewAcademicReporting> FindByReportTime(int year, int level)
        {
            return dbcontext.NewAcademicReportingContext.Where(u => u.ReportTime.Value.Year == year && u.SecrecyLevel <= level && u.IsPass == true).ToList();
        }

        //根据报告人查询
        public List<NewAcademicReporting> FindByReportPeople(string reporpeo, int level)
        {
            return dbcontext.NewAcademicReportingContext.Where(u => u.ReportPeople == reporpeo && u.SecrecyLevel <= level && u.IsPass == true).ToList();
        }
    }
}
