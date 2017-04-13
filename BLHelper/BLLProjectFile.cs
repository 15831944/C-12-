/*
 * 编写人：吕博扬
 * 时间：2015年12月5日
 * 功能：项目-项目全部信息 相关文档的相关操作
 * 修改履历：    暂无
 */
using Common.Entities;
using DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Data.Entity;

namespace BLHelper
{
    public class BLLProjectFile
    {
        DataBaseContext dbcontext = new DataBaseContext();
        //根据项目文件ID查找文件
        public ProjectFile FindByProjectFileId(int fileId)
        {
            return dbcontext.ProjectFileContext.Find(fileId);
        }

        //根据项目文件ID查找文件
        public ProjectFile FindByProjectFileId(int fileId, int secrelevel)
        {
            return dbcontext.ProjectFileContext.Where(u => u.ProjectFileID == fileId && u.SecrecyLevel <= secrelevel && u.IsPass == true).ToList().FirstOrDefault();
        }

        //按照项目ID查询
        public List<ProjectFile> FindByProjectId(int projectId, int secrelevel)
        {
            return dbcontext.ProjectFileContext.Where(u => u.ProjectID == projectId && u.SecrecyLevel <= secrelevel && u.IsPass == true).ToList();
        }

        //按照文件编号查询
        public List<ProjectFile> FindByFileCode(string fileCode, int secrelevel)
        {
            return dbcontext.ProjectFileContext.Where(u => u.FileCode == fileCode && u.SecrecyLevel <= secrelevel && u.IsPass == true).ToList();
        }

        //按照文件名称查询
        public List<ProjectFile> FindByFileName(string fileName, int secrelevel)
        {
            return dbcontext.ProjectFileContext.Where(u => u.FileName.Contains(fileName) && u.SecrecyLevel <= secrelevel && u.IsPass == true).ToList();
        }

        //按照文件类型查询
        public List<ProjectFile> FindByFileType(string fileType, int secrelevel)
        {
            return dbcontext.ProjectFileContext.Where(u => u.FileType == fileType && u.SecrecyLevel <= secrelevel && u.IsPass == true).ToList();
        }

        //按照保密等级查询
        public List<ProjectFile> FindBySecrecyLevel(int secrecyLevel)
        {
            return dbcontext.ProjectFileContext.Where(u => u.SecrecyLevel <= secrecyLevel && u.IsPass == true).ToList();
        }

        //添加项目文档信息
        public bool InsertProjectFile(ProjectFile projectFile)
        {
            try
            {
                BLLProject bllProject = new BLLProject();
                if (bllProject.FindByid(projectFile.ProjectID) == null)
                    return false;
                dbcontext.ProjectFileContext.Add(projectFile);
                dbcontext.SaveChanges();
                return true;
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                throw e;
            }
        }

        //删除项目文档信息
        public int Delete(int projectFileId)
        {
            try
            {
                ProjectFile projectFile = dbcontext.ProjectFileContext.Where(u => u.ProjectFileID == projectFileId).FirstOrDefault();
                int attachmentId = projectFile.AttachmentID;
                dbcontext.ProjectFileContext.Attach(projectFile);
                dbcontext.ProjectFileContext.Remove(projectFile);
                dbcontext.SaveChanges();
                return attachmentId;
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                throw e;
            }

        }

        //将表中的审核状态变为False
        public bool ChangePass(int projectFileId, bool ispass)
        {
            try
            {
                ProjectFile projectFile = dbcontext.ProjectFileContext.Find(projectFileId);
                if (projectFile == null)
                    return false;
                projectFile.IsPass = ispass;
                dbcontext.SaveChanges();
                return true;
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                throw e;
            }
        }

        //更新项目文档信息
        public void Update(ProjectFile newFile)
        {
            try
            {
                ProjectFile projectFile = dbcontext.ProjectFileContext.Find(newFile.ProjectFileID);
                projectFile.ProjectID = newFile.ProjectID;
                projectFile.FileCode = newFile.FileCode;
                projectFile.FileName = newFile.FileName;
                projectFile.FileType = newFile.FileType;
                projectFile.AttachmentID = newFile.AttachmentID;
                projectFile.IsPass = newFile.IsPass;
                projectFile.SecrecyLevel = newFile.SecrecyLevel;
                projectFile.EntryPerson = newFile.EntryPerson;
                dbcontext.SaveChanges();
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                throw e;
            }
        }

        //查出全部信息
        public List<ProjectFile> FindAll()
        {
            return dbcontext.ProjectFileContext.Where(u => u.IsPass == true).OrderBy(u => u.ProjectFileID).ToList();
        }

        //根据项目文件ID找附件ID
        public int FindAttachmentID(int projectFileId)
        {
            if (projectFileId != 0)
            {
                var results = dbcontext.ProjectFileContext.Where(a => a.ProjectFileID == projectFileId).Select(a => new { a.AttachmentID }).ToList();
                if (results.Count != 0)
                {
                    if (results.FirstOrDefault().AttachmentID != 0)
                    {
                        return results.FirstOrDefault().AttachmentID;
                    }
                    else
                        return 0;
                }
                else
                    return 0;
            }
            else
                return 0;
        }

        //根据项目ID找附件ID
        public List<int> FindAttachmentID(int projectId, int? level)
        {
            if (projectId != 0)
            {
                var files = dbcontext.ProjectFileContext.Where(a => a.ProjectID == projectId && a.SecrecyLevel <= level && a.IsPass == true).Select(a => new { a.AttachmentID }).ToList();
                if (files.Count != 0)
                {
                    List<int> results = new List<int>();
                    foreach (var file in files)
                        results.Add(file.AttachmentID);
                    return results;
                }
                else
                    return null;
            }
            else
                return null;
        }

        //根据项目ID找文件ID
        public List<int> FindProjectFileID(int projectId, int? level)
        {
            if (projectId != 0)
            {
                var files = dbcontext.ProjectFileContext.Where(a => a.ProjectID == projectId && a.SecrecyLevel <= level && a.IsPass == true).Select(a => new { a.ProjectFileID }).ToList();
                if (files.Count != 0)
                {
                    List<int> results = new List<int>();
                    foreach (var file in files)
                        results.Add(file.ProjectFileID);
                    return results;
                }
                else
                    return null;
            }
            else
                return null;
        }

        //分页
        public List<ProjectFile> FindPaged(int SecrecyLevel)
        {
            return dbcontext.ProjectFileContext.Where(u => u.SecrecyLevel <= SecrecyLevel && u.IsPass == true).OrderBy(u => u.ProjectFileID).ToList();
        }

    }
}