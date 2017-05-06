/**编写人：方淑云
 * 时间：2014年7月28号
 * 功能:附件表的相关操作
 * 修改履历：1.时间：8月8日
 *            修改人：方淑云
 *            修改内容：添加更新，根据ID找路径的方法
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
    public class BLLAttachment
    {
        DataBaseContext dbcontext = new DataBaseContext();

        //插入数据
        public bool Insert(Attachment at)
        {
            try
            {
                if (at != null )
                {

                    dbcontext.AttachmentContext.Add(at);
                    dbcontext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                //throw e;
                throw dbEx;
            }
        }
        //更新
        public bool Update(Attachment at)
        {
            try
            {
                Attachment newat = dbcontext.AttachmentContext.Find(at.AttachmentID);
                newat.FileName = at.FileName;
                newat.FilePath = at.FilePath;
                dbcontext.SaveChanges();
                return true;
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                throw e;
            }
        }
        //删除
        public bool Delete(int ID)
        {
            try
            {
                Attachment at = dbcontext.AttachmentContext.Where(a => a.AttachmentID == ID).FirstOrDefault();
                if (at != null)
                {
                    dbcontext.AttachmentContext.Attach(at);
                    dbcontext.AttachmentContext.Remove(at);
                    dbcontext.SaveChanges();
                    return true;
                }
                else
                    return true;
            }
            catch
            {
                throw;
            }
        }
        //根据文件名获取附件ID
        public int SelectAttachmentID(string FileName)
        {
            if (FileName != null)
            {
                var results = dbcontext.AttachmentContext.Where(a => a.FileName == FileName).Select(a => new { a.AttachmentID}).ToList();
                return results.FirstOrDefault().AttachmentID;
            }
            else
            {
                return 0;
            }

        }
        //根据ID获取文件路径
        public string FindPath(int? AttachmentID)
        {
            List<Attachment> at = new List<Attachment>();
            at = dbcontext.AttachmentContext.Where(a => a.AttachmentID == AttachmentID).ToList();
            if (at.Count() != 0)
            {
                return at.FirstOrDefault().FilePath;
            }
            else 
            {
                return "";
            }
        }
        //判断是否存在该附件名
        public bool IsAttachmentName(string AttachmentName)
        {
            Attachment attachment = dbcontext.AttachmentContext.Where(a => a.FileName == AttachmentName).FirstOrDefault();
            if (attachment != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //判断除当前ID外是否存在同名文件名
        public bool BIsAttachmentName(string AttachmentName, int AttachmentID)
        {
            Attachment attachment = dbcontext.AttachmentContext.Where(a => a.FileName == AttachmentName && a.AttachmentID != AttachmentID).FirstOrDefault();
            if (attachment != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //跟新时判断除原文件名以外是否存在
        public bool IsNullAttachmentName(string AttachmentName,string OldName)
        {
            Attachment attachment = dbcontext.AttachmentContext.Where(a => a.FileName == AttachmentName && a.FileName !=OldName ).FirstOrDefault();
            if (attachment != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //根据附件ID查找附件Name
        public string SelectAttachmentName(int AttachmentID)
        {
            if (AttachmentID != 0)
            {
                var results = dbcontext.AttachmentContext.Where(a => a.AttachmentID == AttachmentID)
                    .Select(a => new { a.FileName  }).ToList();
                if (results.Count != 0)
                {
                    return results.FirstOrDefault().FileName;
                }
                else
                    return "";
            }
            else
                return "";
        }
        //根据附件ID查找附件信息
        public List<Attachment> FindAttanchment(int attanchID)
        {
            return dbcontext.AttachmentContext.Where(u => u.AttachmentID == attanchID).ToList();
        }
    }
}
