/**编写人：方淑云
 * 时间：2014年6月20号
 * 功能:照片表的相关操作
 * 修改履历：1.时间：8月11日
 *           修改人：张凡凡
 *           修改内容：添加更新IsPass状态函数UpdateIsPass(int,bool)
 *                  查找附件id函数FindAttachmentID(int)
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
    public class BLLPhotos
    {
        DataBaseContext dbcontext = new DataBaseContext();

        //更新IsPass状态
        public void UpdateIsPass(int ID, bool isPass)
        {
            try
            {
                Photos NewPhotos = dbcontext.PhotosContext.Find(ID);
                if (NewPhotos == null)
                    return;
                NewPhotos.IsPass = isPass;
                dbcontext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        //为用户添加照片
        public bool Insert(Photos photo)
        {
            try
            {
                if (photo != null)
                {
                    dbcontext.PhotosContext.Add(photo);
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
        //更新用户照片
        public bool Update(Photos photo)
        {
            try
            {
                if (photo != null )
                {
                    Photos newphoto = dbcontext.PhotosContext.Find(photo.PhotosID);
                    newphoto.AttachmentID = photo.AttachmentID;
                    newphoto.SecrecyLevel = photo.SecrecyLevel;
                    newphoto.EntryPerson = photo.EntryPerson;
                    newphoto.IsPass = photo.IsPass;
                    newphoto.UserInfoID = photo.UserInfoID;
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
        //删除用户照片
        public bool Delete(int photoid)
        {
            try
            {
                Photos pho = dbcontext.PhotosContext.Where(u => u.PhotosID == photoid).FirstOrDefault();
                    dbcontext.PhotosContext.Attach(pho);
                    dbcontext.PhotosContext.Remove(pho);
                    dbcontext.SaveChanges();
                    return true;
             
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                throw e;
            }
        }
        //根据Userid查找PhotoID
        public int FindPhotoID(int UserID)
        {
            List<Photos> list = new List<Photos>();
            list = dbcontext.PhotosContext.Where(a => a.UserInfoID  == UserID && a.IsPass ==true).ToList();
            if (list.Count() != 0)
            {
                if (list.FirstOrDefault().PhotosID  != null)
                {
                    return list.FirstOrDefault().PhotosID;
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

        //查找附件id
        public int FindAttachmentID(int photoid)
        {
            List<Photos> list = new List<Photos>();
            list = dbcontext.PhotosContext.Where(a => a.PhotosID == photoid ).ToList();
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
        //
        public Photos find(int photoID)
        {
            return dbcontext.PhotosContext.Find(photoID);
        }
    }
}
