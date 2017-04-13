/**编写人：李金秋
 * 时间：2014年6月20号
 * 功能：单位考察表的相关操作
 * 修改履历：1.时间：8月9号
 *            修改人：方淑云
 *            修改内容：添加ChangePass(int inspect, bool ispass)，FindContent(int id)方法,添加更新方法
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
   public class BLLUnitInspect
    {
        DataBaseContext dbcontext = new DataBaseContext();

        //录入单位考察信息
        public void Insert(UnitInspect unitInspect)
        {
            try
            {
                dbcontext.UnitInspectContext.Add(unitInspect);
                dbcontext.SaveChanges();
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                throw;
            }
        }
       //更新
        public void Update(UnitInspect unitInspect)
        {
            try
            {
                UnitInspect ins = dbcontext.UnitInspectContext.Find(unitInspect.UnitInspectID);
                ins.VisitContent = unitInspect.VisitContent;
                ins.WorkPlace = unitInspect.WorkPlace;
                ins.SecrecyLevel = unitInspect.SecrecyLevel;
                ins.IsPass = unitInspect.IsPass;
                ins.InspectTime = unitInspect.InspectTime;
                ins.InspectName = unitInspect.InspectName;
                ins.EntryPerson = unitInspect.EntryPerson;
                ins.Duty = unitInspect.Duty;
                ins.AgencyID = unitInspect.AgencyID;
                ins.AccessoryID = unitInspect.AccessoryID;
                dbcontext.SaveChanges();
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                throw e;
            }
        }
       //根据姓名和时间获取ID
        public int FindIDs(string name, DateTime? time,int UnitInspectID)
        {
            List<UnitInspect> list = new List<UnitInspect>();
            list = dbcontext.UnitInspectContext.Where(u => u.InspectName == name && u.InspectTime == time && u.UnitInspectID != UnitInspectID).ToList();
            if (list.Count != 0)
            {
                return list.FirstOrDefault().UnitInspectID;
            }
            else
            {
                return 0;
            }
        }
        //根据姓名和时间获取ID（不考虑状态）
        public int FindID(string name, DateTime? time)
        {
            List<UnitInspect> list = new List<UnitInspect>();
            list = dbcontext.UnitInspectContext.Where(u => u.InspectName == name && u.InspectTime == time).ToList();
            if (list.Count != 0)
            {
                return list.FirstOrDefault().UnitInspectID;
            }
            else
            {
                return 0;
            }
        }
       //根据ID查找附件ID
        public int FindAttachmentID(int ID)
        {
            List<UnitInspect> list = new List<UnitInspect>();
            list = dbcontext.UnitInspectContext.Where(u => u.UnitInspectID == ID).ToList();
            if (list.Count() != 0)
            {
                if (list.FirstOrDefault().AccessoryID != null)
                {
                    return list.FirstOrDefault().AccessoryID.Value;
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
        public List<UnitInspect> FindAll()
        {
            return dbcontext.UnitInspectContext.ToList();
        }
       //根据ID查询信息
        public UnitInspect Findmodel(int id)
        {
            return dbcontext.UnitInspectContext.Find(id);
        }
        //根据UnitInspectID删除单位考察信息
        public int Delete(int unitInspectID)
        {
            try
            {
               // Files file = dbcontext.FilesContext.Where(u => u.FilesID == filesID).FirstOrDefault();
                UnitInspect unitInspect = dbcontext.UnitInspectContext.Where(u => u.UnitInspectID == unitInspectID).FirstOrDefault();
                int attachid = Convert.ToInt32(unitInspect.AccessoryID);
                dbcontext.UnitInspectContext.Attach(unitInspect);
                dbcontext.UnitInspectContext.Remove(unitInspect);
                return attachid;
            }
            catch
            {
                throw;
            }
        }
       //改变审核状态
        public bool ChangePass(int inspect, bool ispass)
        {
            try
            {
                UnitInspect ins = dbcontext.UnitInspectContext.Find(inspect);
                if (ins == null)
                    return false;
                ins.IsPass = ispass;
                dbcontext.SaveChanges();
                return true;
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                throw e;
            }
        }
      //根据ID查询参观内容
        public string FindContent(int id)
        {
            List<UnitInspect> list = new List<UnitInspect>();
            list = dbcontext.UnitInspectContext.Where(u => u.UnitInspectID == id).ToList();
            if (list.Count != 0)
            {
                return list.FirstOrDefault().VisitContent;
            }
            else
            {
                return "";
            }
        }
        //分年份查看访问考察信息
        public List<UnitInspect> FindByInspectTime( int year, int? SecrecyLevel)
        {
            return dbcontext.UnitInspectContext.Where(p => p.InspectTime.Value.Year == year && p.SecrecyLevel <= SecrecyLevel && p.IsPass == true).OrderBy(c => c.IsPass).ToList();
        }
        //根据AgencyID查询考察信息
        public List<UnitInspect> FindByAgencyID(int agencyID)
        {
            return dbcontext.UnitInspectContext.Where(p => p.AgencyID == agencyID && p.IsPass == true).ToList();
        }
        //根据UnitInspectID查询来访人员信息
        public UnitInspect FindInspectInfo(int unitInspectID, bool ispass)
        {
            return dbcontext.UnitInspectContext.Where(p => p.UnitInspectID == unitInspectID && p.IsPass == ispass).FirstOrDefault();
        }
        //分页
        public List<UnitInspect> FindPaged( int? SecrecyLevel)
        {
            return dbcontext.UnitInspectContext.Where(u => u.SecrecyLevel <= SecrecyLevel && u.IsPass == true).OrderBy(c => c.IsPass).ToList();
        }    

       //根据工作单位查询
        public List<UnitInspect> FindByWorkPlace(string workplace, int level)
        {
            return dbcontext.UnitInspectContext.Where(u =>u.WorkPlace == workplace && u.SecrecyLevel <= level && u.IsPass == true).OrderBy(c => c.IsPass).ToList();
        }


    }
}
