/**编写人：王会会
 * 时间：2014年11月13号
 * 功能：平台的相关操作
 * 修改履历：    1、修改人：吕博杨
 *                 修改时间：2015年11月27日
 *                 修改内容：为platform类中新增的六个字段（批复文号、平台负责人、平台成员、批复经费、平台管理（时间、人员、业务、经费）、上传文件）加上后台代码
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
    public class BLLPlatform
    {
        DataBaseContext dbcontext = new DataBaseContext();
        /// <summary>
        /// 添加平台
        /// </summary>
        /// <param name="important"></param>
        public void insert(Platform platform)
        {
            try
            {
                dbcontext.PlatformContext.Add(platform);
                dbcontext.SaveChanges();
            }
            catch (System.Data.SqlClient.SqlException e)
            { 
                throw e; 
            }
        }
        //删除平台
        public void Delete(int PlatformID)
        {
            try
            {
                Platform platform = dbcontext.PlatformContext.Where(u => u.PlatformID == PlatformID).FirstOrDefault ();
                dbcontext.PlatformContext.Attach(platform);
                dbcontext.PlatformContext.Remove(platform);
                dbcontext.SaveChanges();
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                throw e;
            }
        }
        //更新
        public void Update(Platform plat)
        {
            try
            {
                Platform newplat = dbcontext.PlatformContext.Find(plat.PlatformID);
                newplat.AgreeUnit = plat.AgreeUnit;
                newplat.AgreeTime = plat.AgreeTime;               
                newplat.PlatformName = plat.PlatformName;
                newplat.PlatformRank = plat.PlatformRank;
                newplat.PlatformType = plat.PlatformType;
                newplat.SecrecyLevel = plat.SecrecyLevel;
                newplat.IsPass = plat.IsPass;
                newplat.EntryPerson = plat.EntryPerson;
                //lby↓
                newplat.AgreeNumber = plat.AgreeNumber;
                newplat.AgreeExpenditure = plat.AgreeExpenditure;
                newplat.AttachmentID = plat.AttachmentID;
                newplat.PlatformManagement = plat.PlatformManagement;
                newplat.PlatformMember = plat.PlatformMember;
                newplat.PlatformPrincipal = plat.PlatformPrincipal;

                dbcontext.SaveChanges();
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                throw e;
            }
        }
        //根据ID查找附件ID(lby)
        public int FindAttachmentID(int id)
        {
            List<Platform> list = new List<Platform>();
            list = dbcontext.PlatformContext.Where(a => a.PlatformID == id).ToList();
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
        //更新IsPass状态
        public void UpdateIsPass(int Id, bool Ispass)
        {
            Platform platform = dbcontext.PlatformContext.Find(Id);
            if (platform == null)
                return;
            platform.IsPass = Ispass;
            dbcontext.SaveChanges();
        }
        //按登陆级别查找平台信息
        public List<Platform> FindPaged(int? SecrecyLevel)
        {
            return dbcontext.PlatformContext.Where(u => u.SecrecyLevel <= SecrecyLevel && u.IsPass == true).OrderBy(c => c.PlatformID).ToList();
        }
        //模糊查询(根据平台名称)
        public List<Platform> FindPlatformList(string PlatformName, int level)
        {
            return dbcontext.PlatformContext.Where(a => a.PlatformName.Contains(PlatformName) && a.SecrecyLevel <= level && a.IsPass == true).ToList();
        }
        //根据PlatformID查找Platform信息
        public Platform FindByPlatformID(int PlatformID)
        {
            return dbcontext.PlatformContext.Find(PlatformID);
        }
        //根据平台级别查询
        public List<Platform> FindPlatformRank(string PlatformRank, int SecrecyLevel)
        {
            return dbcontext.PlatformContext.Where(u => u.PlatformRank == PlatformRank && u.SecrecyLevel <= SecrecyLevel && u.IsPass == true).ToList();
        }
        //判断平台名称是否存在
        public Platform FindByPlatformName(string PlatformName)
        {
            return dbcontext.PlatformContext.Where(u => u.PlatformName == PlatformName).FirstOrDefault();
        }
    }
}
