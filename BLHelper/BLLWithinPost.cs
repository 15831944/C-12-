/**编写人：林美彤
* 时间：2014年11月2号
* 功能：文件表的相关操作
* 修改履历：
 * 
 *           
**/


using Common.Entities;
using DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace BLHelper
{
    public class BLLWithinPost
    {
        DataBaseContext dbcontext = new DataBaseContext();
        //更新IsPass状态
        public void UpdateIsPass(int ID, bool isPass)
        {
            try
            {
                WithinPost wh = dbcontext.WithinPostContext.Find(ID);
                if (wh == null)
                    return;
                wh.IsPass = isPass;
                dbcontext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        //插入文件信息
        public void Insert(WithinPost wh)
        {
            try
            {
                dbcontext.WithinPostContext.Add(wh);
                dbcontext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }
        //更新
        public bool Update(WithinPost wh)
        {
            try
            {
                WithinPost newwh = dbcontext.WithinPostContext.Find(wh.WithinPostID);
                newwh.AndUnit = wh.AndUnit;
                newwh.FileType = wh.FileType;
                newwh.recipient = wh.recipient;
                newwh.FileName = wh.FileName;
                newwh.Time = wh.Time;
                newwh.AttachmentID = wh.AttachmentID;
                newwh.SecrecyLevel = wh.SecrecyLevel;
                newwh.EntryPerson = wh.EntryPerson;
                newwh.IsPass = wh.IsPass;
                dbcontext.SaveChanges();
                return true;
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                throw e;
            }

        }
        //根据FileID查询所有信息
        public List<WithinPost> FindAll(int withinID)
        {
            return dbcontext.WithinPostContext.Where(w => w.WithinPostID == withinID).ToList();
        }
       
        //根据ID获取录入人
        public string FindPeople(int withinID)
        {
            List<WithinPost> file = new List<WithinPost>();
            file = dbcontext.WithinPostContext.Where(w => w.WithinPostID == withinID).ToList();
            if (file.Count != 0)
            {
                return file.FirstOrDefault().EntryPerson;
            }
            else
            {
                return "";
            }
        }
        //根据文件名查找文件ID
        public int SelectFileIDs(string FileName, int? withinID)
        {
            List<WithinPost> file = new List<WithinPost>();
            file = dbcontext.WithinPostContext.Where(w => w.FileName == FileName && w.WithinPostID != withinID).ToList();
            if (file.Count() != 0)
            {
                return file.FirstOrDefault().WithinPostID;
            }
            else
            {
                return 0;
            }
        }
        //根据文件名查找文件ID（不考虑状态）
        //public int SelectFileID(string FileName)
        //{
        //    List<Files> file = new List<Files>();
        //    file = dbcontext.FilesContext.Where(f => f.FileName == FileName).ToList();
        //    if (file.Count() != 0)
        //    {
        //        return file.FirstOrDefault().FilesID;
        //    }
        //    else
        //    {
        //        return 0;
        //    }
        //}
        //根据文件ID查找附件ID
        public WithinPost FindByFileID(int ID)
        {
            return dbcontext.WithinPostContext.Where(w => w.WithinPostID == ID).FirstOrDefault();
        }
        //根据FilesID删除文件信息
        public int Delete(int WithinID)
        {
            try
            {
                WithinPost file = dbcontext.WithinPostContext.Where(u => u.WithinPostID == WithinID).FirstOrDefault();
                int attachid = Convert.ToInt32(file.AttachmentID);
                dbcontext.WithinPostContext.Attach(file);
                dbcontext.WithinPostContext.Remove(file);
                dbcontext.SaveChanges();
                return attachid;
            }
            catch
            {
                throw;
            }
        }


        //根据文件分类和所属部门查询文件信息
        public List<WithinPost> FindAllInfo(string documentCategory, string unit, int? SecrecyLevel)
        {
            List<WithinPost> file = new List<WithinPost>();
            file = dbcontext.WithinPostContext.Where(p => p.FileType == documentCategory && p.AndUnit == unit && p.SecrecyLevel <= SecrecyLevel && p.IsPass == true).OrderBy(f => f.WithinPostID).ToList();

            return file;
        }
        //根据所属部门查看文件信息
        public List<WithinPost> FindByAgency(string unit, int? SecrecyLevel)
        {
            List<WithinPost> file = new List<WithinPost>();
            file = dbcontext.WithinPostContext.Where(p => p.AndUnit == unit && p.SecrecyLevel <= SecrecyLevel && p.IsPass == true).OrderBy(f => f.WithinPostID).ToList();

            return file;
        }
        //根据文件分类查询
        public List<WithinPost> FindByFileType(string type, int? SecrecyLevel)
        {
            List<WithinPost> file = new List<WithinPost>();
            file = dbcontext.WithinPostContext.Where(p => p.FileType == type && p.SecrecyLevel <= SecrecyLevel && p.IsPass == true).OrderBy(f => f.WithinPostID).ToList();

            return file;
        }
        //根据ID查询AttachmentID(附件ID)
        public int FindAttachmentID(int WithinPostID)
        {
            List<WithinPost> file = new List<WithinPost>();
            file = dbcontext.WithinPostContext.Where(f => f.WithinPostID == WithinPostID).ToList();
            return file.FirstOrDefault().AttachmentID.Value;
        }
        //分页
        public List<WithinPost> FindPaged(int? SecrecyLevel)
        {
            List<WithinPost> file = new List<WithinPost>();
            file = dbcontext.WithinPostContext.Where(f => f.SecrecyLevel <= SecrecyLevel && f.IsPass == true).OrderBy(f => f.WithinPostID).ToList();
            return file;
        }

        //判断是否存在该文件名
        public WithinPost IsFileName(string FileName)
        {
            return dbcontext.WithinPostContext.Where(f => f.FileName == FileName).FirstOrDefault();
        }
    }
}
