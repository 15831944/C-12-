/**编写人：王会会
 * 时间：2014年8月25号
 * 功能：获奖情况表的相关操作
 * 修改履历：
 **/
using Common.Entities;
using DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLHelper
{
    public class BLLBasicCode
    {
        DataBaseContext dbcontext = new DataBaseContext();
        //录入基本代码表
        public void Insert(BasicCode  basiccode)
        {
            try
            {
                dbcontext.BasicCodeContext.Add(basiccode);
                dbcontext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }
        //删除
        public void   Delete(int BasicCodeID)
        {
            try
            {
                BasicCode basiccode = dbcontext.BasicCodeContext.Where(u => u.BasicCodeID == BasicCodeID).FirstOrDefault();
                dbcontext.BasicCodeContext.Attach(basiccode);
                dbcontext.BasicCodeContext.Remove(basiccode);
                dbcontext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }
        //更新
        public bool Update(BasicCode basiccode)
        {
            try
            {
                if (basiccode != null)
                {
                    BasicCode newbasiccode = dbcontext.BasicCodeContext.Find(basiccode.BasicCodeID );
                   // newbasiccode.CategoryID = basiccode.CategoryID;
                    newbasiccode.CategoryName = basiccode.CategoryName;
                    newbasiccode.CategoryContent = basiccode.CategoryContent;
                    dbcontext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                throw e;
            }
        }
        //查出全部信息
        public List<BasicCode > FindAll()
        {
            return dbcontext.BasicCodeContext.OrderBy (u=>u.CategoryID).ToList();
        }
        //根据分类名称查询
        public List<BasicCode> FindByCategoryName(string CategoryName)
        {
            return dbcontext.BasicCodeContext.Where (u=>u.CategoryName ==CategoryName ).ToList();
        }
        //根据BasicCodeID查询
        public List<BasicCode> FindByBasicCodeID(int BasicCodeID)
        {
            return dbcontext.BasicCodeContext.Where(u => u.BasicCodeID  == BasicCodeID).ToList();
        }
        //判断编号，名称，内容是否对应
        public bool IsIDandName(int CategoryID, string CategoryName)
        {
            List <BasicCode > resultlist = dbcontext.BasicCodeContext.Where(u => u.CategoryID == CategoryID).ToList();
            if (resultlist .FirstOrDefault ().CategoryName ==CategoryName )
            {
                return true;
            }
            else
                return false;
        }
        //判断编号，名称，内容是否存在
        public bool IsTrue(int CategoryID, string CategoryName, string CategoryContent)
        {
            var result = dbcontext.BasicCodeContext.Where(u => u.CategoryID == CategoryID && u.CategoryName == CategoryName && u.CategoryContent == CategoryContent).ToList();
            if (result.Count() == 0)
            {
                return true;
            }
            else
                return false;
        }
        //查找
        public List<BasicCode> FindALLName(string  Name)
        {
            var result = dbcontext.BasicCodeContext.Where(u => u.CategoryName == Name).ToList();
            return result.ToList();
        }
    }
}
