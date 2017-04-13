/**编写人：李金秋
* 时间：2014年6月20号
* 功能：文件表的相关操作
* 修改履历：1.时间：8月7日
 *           修改人：方淑云
 *           修改内容:添加ChangePass（）, FindPeople(),SelectFileID()
 *         2.时间：8月11日
 *           修改人：张凡凡
 *           修改内容：添加更新IsPass状态函数UpdateIsPass(int,bool)
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
    public class BLLFiles
    {
        DataBaseContext dbcontext = new DataBaseContext();

        //更新IsPass状态
        public void UpdateIsPass(int ID, bool isPass)
        {
            try
            {
                Files NewFiles = dbcontext.FilesContext.Find(ID);
                if (NewFiles == null)
                    return;
                NewFiles.IsPass = isPass;
                dbcontext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        //插入文件信息
        public void Insert(Files file)
        {
            try
            {
                dbcontext.FilesContext.Add(file);
                dbcontext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }
        //更新
        public bool Update(Files file)
        {
            try
            {
                Files newfile = dbcontext.FilesContext.Find(file.FilesID);
                newfile.AgencyID = file.AgencyID; 
                newfile.DocumentCategoryID = file.DocumentCategoryID;
                newfile.AttachmentID = file.AttachmentID;
                newfile.FileName = file.FileName;
                newfile.LevinTime = file.LevinTime;
                newfile.LevinUnit = file.LevinUnit;
                newfile.FileRecipient = file.FileRecipient;
                newfile.SecrecyLevel = file.SecrecyLevel;
                newfile.EntryPerson = file.EntryPerson;
                newfile.IsPass = file.IsPass;
                dbcontext.SaveChanges();
                return true;
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                throw e;
            }

        }
        //根据FileID查询所有信息
        public List<Files> FindAll(int fileID)
        {
            return dbcontext.FilesContext.Where(f => f.FilesID == fileID).ToList();
        }
        //将改变表中的审核
        public bool ChangePass(int file,bool ispass)
        {
            try
            {
                Files newfile = dbcontext.FilesContext.Find(file);
                newfile.IsPass = ispass;
                dbcontext.SaveChanges();
                return true;
            } 
            catch (System.Data.SqlClient.SqlException e)
            {
                throw e;
            }
        }
        //根据ID获取录入人
        public string FindPeople(int FileID)
        {
            List<Files> file = new List<Files>();
               file = dbcontext.FilesContext.Where(f => f.FilesID == FileID).ToList();
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
        public int SelectFileIDs( string FileName,int? FileID)
        {
            List<Files> file = new List<Files>();
            file = dbcontext.FilesContext.Where(f => f.FileName == FileName && f.FilesID != FileID).ToList();
            if (file.Count() != 0)
            {
                return file.FirstOrDefault().FilesID;
            }
            else
            {
                return 0;
            }
        }
        //根据文件名查找文件ID（不考虑状态）
        public int SelectFileID(string FileName)
        {
            List<Files> file = new List<Files>();
            file = dbcontext.FilesContext.Where(f => f.FileName == FileName).ToList();
            if (file.Count() != 0)
            {
                return file.FirstOrDefault().FilesID;
            }
            else
            {
                return 0;
            }
        }
        //根据文件ID查找附件ID
        public Files FindByFileID(int ID)
        {
            return dbcontext.FilesContext.Where(f => f.FilesID == ID).FirstOrDefault();
        }
        //根据FilesID删除文件信息
        public int Delete(int filesID)
        {
            try
            {
                Files file = dbcontext.FilesContext.Where(u => u.FilesID == filesID).FirstOrDefault();
                int attachid = Convert.ToInt32(file.AttachmentID);
                dbcontext.FilesContext.Attach(file);
                dbcontext.FilesContext.Remove(file);
                dbcontext.SaveChanges();
                return attachid;
            }
            catch
            {
                throw;
            }
        }
       
       
        //根据DocumentCategoryID(文件分类)和所属部门查询文件信息
        public List<Files> FindAllInfo(string  documentCategoryID, int agencyID, int? SecrecyLevel)
        {
            List<Files> file = new List<Files>();
           file = dbcontext.FilesContext.Where(p => p.DocumentCategoryID == documentCategoryID && p.AgencyID == agencyID && p.SecrecyLevel <= SecrecyLevel && p.IsPass == true).OrderBy(f => f.AgencyID).ToList();
        
           return file;
        }
        //根据AgencyID(所属部门)查看文件信息
        public List<Files> FindByAgencyID(int agencyID, int? SecrecyLevel)
        {
            List<Files> file = new List<Files>();
            file = dbcontext.FilesContext.Where(p => p.AgencyID == agencyID && p.SecrecyLevel <= SecrecyLevel && p.IsPass == true).OrderBy(f => f.FilesID).ToList();
           
            return file;
        }
        //根据文件分类编号查询
        public List<Files> FindByDocumentCategoryID( string   DocumentCategoryID, int? SecrecyLevel)
        {
            List<Files> file = new List<Files>();
            file = dbcontext.FilesContext.Where(p => p.DocumentCategoryID == DocumentCategoryID && p.SecrecyLevel <= SecrecyLevel && p.IsPass == true).OrderBy(f => f.AgencyID).ToList();
           
            return file;
        }
        //根据FilesID查询AttachmentID(附件ID)
        public int FindAttachmentID(int filesID)
        {
            List<Files> file = new List<Files>();
            file = dbcontext.FilesContext.Where(f=>f.FilesID == filesID).ToList();
            return file.FirstOrDefault().AttachmentID.Value;
        }
        //分页
        public List<Files> FindPaged( int? SecrecyLevel)
        {
            List<Files> file = new List<Files>();
            file = dbcontext.FilesContext.Where(f => f.SecrecyLevel <= SecrecyLevel && f.IsPass == true).OrderBy(f => f.AgencyID).ToList();
           
            return file;
        }
      
        //判断是否存在该文件名
        public Files IsFileName(string FileName)
        {
            return dbcontext.FilesContext.Where(f => f.FileName == FileName).FirstOrDefault();
        }
    }
}
