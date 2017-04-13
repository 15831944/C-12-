/*
 * 编写人：吕博杨
 * 时间：2015年12月7日
 * 功能：项目相关文件下载界面
 * 修改履历：    暂无
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FineUI;

namespace WDFramework.Projects.Projects
{
    public partial class DownloadFile : System.Web.UI.Page
    {
        Common.Entities.ProjectFile projectFile = new Common.Entities.ProjectFile();
        BLHelper.BLLProjectFile bllProjectFile = new BLHelper.BLLProjectFile();
        BLHelper.BLLAttachment bllAttachment = new BLHelper.BLLAttachment();
        BLCommon.PublicMethod publicMethod = new BLCommon.PublicMethod();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitData();
            }
        }
        //初始化
        public void InitData()
        {
            try
            {
                int id = Convert.ToInt32(Request.QueryString["id"].ToString());
                projectFile = bllProjectFile.FindByProjectFileId(id);
                FileCode.Text = projectFile.FileCode;
                FileName.Text = projectFile.FileName;
                FileType.Text = projectFile.FileType;
            }
            catch (Exception ex)
            {
                publicMethod.SaveError(ex, this.Request); ;
            }
        }

        //删除
        protected void Delete_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(Request.QueryString["id"].ToString());
                projectFile = bllProjectFile.FindByProjectFileId(id);
                int attachId = projectFile.AttachmentID;
                string srcPath = bllAttachment.FindPath(attachId);
                if (attachId == 0 || srcPath == "")
                {
                    Alert.Show("该项目不存在相关文档");
                }
                else
                {
                    publicMethod.DeleteFile(attachId, srcPath);
                    projectFile.AttachmentID = 0;
                    bllProjectFile.Update(projectFile);
                    PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideReference() + Alert.GetShowInTopReference("删除成功！"));
                }
            }
            catch (Exception ex)
            {
                publicMethod.SaveError(ex, this.Request);
            }
        }
        //下载
        protected void DownFile_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(Request.QueryString["id"].ToString());
                projectFile = bllProjectFile.FindByProjectFileId(id);
                int attachId = projectFile.AttachmentID;
                string srcPath = bllAttachment.FindPath(attachId);
                if (attachId == 0 || srcPath == "")
                {
                    Alert.Show("该项目不存在相关文档");
                }
                else
                {
                    publicMethod.DownloadFile(srcPath);
                }
            }
            catch (Exception ex)
            {
                publicMethod.SaveError(ex, this.Request);
            }
        }
    }
}