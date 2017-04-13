/**编写人：李金秋
 * 时间：2014年6月20号
 * 功能：单位讲学表的相关操作
 * 修改履历：1.时间：8月21日
 *           修改人：张凡凡
 *           修改内容：添加根据id查找附件id函数FindAttachmentid(int)
 **/

using Common.Entities;
using DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLHelper
{
    public class BLLUnitLectures
    {
        DataBaseContext dbcontext = new DataBaseContext();

        //录入单位讲学信息
        public void Insert(UnitLectures unitLectures)
        {
            try
            {
                dbcontext.UnitLecturesContext.Add(unitLectures);
                dbcontext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public bool Delete(int UnitLecturesID)
        {
            try
            {
                //UnitLectures unitLectures = new UnitLectures { UnitLecturesID = UnitLecturesID };
                UnitLectures unitLectures = dbcontext.UnitLecturesContext.Find(UnitLecturesID);
                dbcontext.UnitLecturesContext.Attach(unitLectures);
                dbcontext.UnitLecturesContext.Remove(unitLectures);
                dbcontext.SaveChanges();
                return true;
            }
            catch
            {
                throw;
            }
        }
        //更新AgencyID = agencyID的机构信息为agency
        public void Update(UnitLectures unitLectures)
        {
            try
            {
                UnitLectures NewUnitLectures = dbcontext.UnitLecturesContext.Find(unitLectures.UnitLecturesID);
                NewUnitLectures.LecturesName = unitLectures.LecturesName;
                NewUnitLectures.AgencyID = unitLectures.AgencyID;
                NewUnitLectures.UReportName = unitLectures.UReportName;
                NewUnitLectures.LecturesTime = unitLectures.LecturesTime;
                NewUnitLectures.LecturesPlace = unitLectures.LecturesPlace;
                NewUnitLectures.listenerNumber = unitLectures.listenerNumber;
                NewUnitLectures.WorkUnit = unitLectures.WorkUnit;
                NewUnitLectures.AttachmentID = unitLectures.AttachmentID;
                NewUnitLectures.SecrecyLevel = unitLectures.SecrecyLevel;
                NewUnitLectures.EntryPerson = unitLectures.EntryPerson;
                NewUnitLectures.IsPass = unitLectures.IsPass;
                NewUnitLectures.WorkTitle = unitLectures.WorkTitle;
                NewUnitLectures.Identity = unitLectures.Identity;
                //NewUnitLectures.Identity = "123";
                NewUnitLectures.Telephone = unitLectures.Telephone;
                NewUnitLectures.Remark = unitLectures.Remark;
                dbcontext.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
       



        //查询所有单位讲学信息
        public List<UnitLectures> FindAll()
        {
            return dbcontext.UnitLecturesContext.ToList();
        }
        //根据AgencyID(所属机构ID)查询单位讲学情况
        public List<UnitLectures> FindByAgrncyID(int agencyID)
        {
            return dbcontext.UnitLecturesContext.Where(p => p.AgencyID == agencyID && p.IsPass == true).ToList();
        }
        //根据AgrncyID查询AttachmentID(附件表ID)
        public List<int> FindAcademicMeetingID(int unitLecturesID)
        {
            List<int> list = new List<int>();
            int count = dbcontext.UnitLecturesContext.Where(p => p.UnitLecturesID == unitLecturesID && p.IsPass == true).Count();
            //return dbcontext.AttendMeetingContext.OrderBy(p=>p.UserInfoID.UserInfoID==userInfoID)
            List<UnitLectures> objectlist = dbcontext.UnitLecturesContext.Where(p => p.UnitLecturesID == unitLecturesID && p.IsPass == true).ToList();
            for (int i = 0; i < count; i++)
            {
                list[i] = Convert.ToInt32( objectlist[i].AttachmentID);
            }
            return list;
        }
        //查询讲学信息
        public List<UnitLectures> FindLecturesName(int pageIndex, int pageSize, int? SecrecyLevel)
        {
            return (from a in dbcontext.UnitLecturesContext
                    where  a.SecrecyLevel <= SecrecyLevel && a.IsPass == true
                    select new
                    {
                        UnitLecturesID = a.UnitLecturesID,
                        LecturesName = a.LecturesName,
                        AgencyID = a.AgencyID,
                        UReportName = a.UReportName,
                        LecturesTime = a.LecturesTime,
                        LecturesPlace = a.LecturesPlace,
                        listenerNumber = a.listenerNumber,
                        SecrecyLevel = a.SecrecyLevel,
                        WorkUnit = a.WorkUnit
                    }).ToList().Select(a => new UnitLectures
                    {
                        UnitLecturesID = a.UnitLecturesID,
                        LecturesName = a.LecturesName,
                        AgencyID = a.AgencyID,
                        UReportName = a.UReportName,
                        LecturesTime = a.LecturesTime,
                        LecturesPlace = a.LecturesPlace,
                        listenerNumber = a.listenerNumber,
                        SecrecyLevel = a.SecrecyLevel,
                        WorkUnit = a.WorkUnit
                    }).Skip(pageIndex * pageSize).Take(pageSize).ToList();
        }
        //返回单位讲学数量
        public int LecturesCount(int SecrecyLevel)
        {
            return dbcontext.UnitLecturesContext.Where(p => p.SecrecyLevel <= SecrecyLevel && p.IsPass == true).Count();
        }
        //根据所属机构查询讲学数量
        public int LecturesCountByAgency(int SecrecyLevel, int AgencyID)
        {
            return dbcontext.UnitLecturesContext.Where(p => p.SecrecyLevel <= SecrecyLevel && p.AgencyID == AgencyID && p.IsPass == true).Count();
        }
        //根据所属部门查询讲学信息
        public List<UnitLectures> FindLectures(int agencyID, int SecrecyLevel)
        {
            return dbcontext.UnitLecturesContext.Where(p => p.AgencyID == agencyID && p.SecrecyLevel <= SecrecyLevel && p.IsPass == true).ToList();
        }
        //将改变表中的审核
        public bool ChangePass(int UnitLecturesID, bool ispass)
        {
            try
            {
                UnitLectures unitLectures = dbcontext.UnitLecturesContext.Find(UnitLecturesID);
                if (unitLectures == null)
                    return false;
                unitLectures.IsPass = ispass;
                dbcontext.SaveChanges();
                return true;
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                throw e;
            }
        }
        //根据UnitLecturesID查询讲学对象
        public UnitLectures FindByUnitLecturesID(int UnitLecturesID)
        {
            return dbcontext.UnitLecturesContext.Find(UnitLecturesID);
        }

        


        //查询附件id
        public int FindAttachmentid(int unitlectureid)
        {
            var unitlecture = dbcontext.UnitLecturesContext.Find(unitlectureid);
            if (unitlecture != null)
            {
                if (unitlecture.AttachmentID.HasValue)
                    return unitlecture.AttachmentID.Value;
                else
                    return 0;
            }
            else
                return 0;
        }

        //分页
        public List<UnitLectures> FindPaged(int? SecrecyLevel)
        {
            return dbcontext.UnitLecturesContext.Where(u => u.SecrecyLevel <= SecrecyLevel && u.IsPass == true).OrderBy(c => c.UnitLecturesID).ToList();
        }
    }
}
