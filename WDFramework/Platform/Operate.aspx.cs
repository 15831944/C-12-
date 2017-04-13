/**编写人：吕博杨
 * 时 间 ：2015年11月28日
 * 功 能 ：下载
 * 修改履历：    暂无
 **/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common.Entities;
using FineUI;

namespace WDFramework.Platform
{
    public partial class Operate : System.Web.UI.Page
    {
        BLHelper.BLLPlatform BLLPlatform = new BLHelper.BLLPlatform();
        BLHelper.BLLAttachment BLLAttachment = new BLHelper.BLLAttachment();
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
                Common.Entities.Platform platform = BLLPlatform.FindByPlatformID(id);
                PlatformName.Text = platform.PlatformName;
                PlatformRank.Text = platform.PlatformRank;
                AgreeUnit.Text = platform.AgreeUnit;
            }
            catch (Exception ex)
            {
                publicMethod.SaveError(ex, this.Request);

            }
        }
      
        //删除
        protected void Delete_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(Request.QueryString["id"].ToString());
                int attachId = BLLPlatform.FindAttachmentID(id);
                string srcPath = BLLAttachment.FindPath(attachId);
                if (attachId == 0 || srcPath == "")
                {
                    Alert.Show("该成果应用不存在相关文档");
                }
                else
                {
                    publicMethod.DeleteFile(attachId, srcPath);
                    Common.Entities.Platform platform = BLLPlatform.FindByPlatformID(id);
                    platform.AttachmentID = null;
                    BLLPlatform.Update(platform);
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
                int attachId = BLLPlatform.FindAttachmentID(id);
                string srcPath = BLLAttachment.FindPath(attachId);
                if (attachId == 0 || srcPath == "")
                {
                    Alert.Show("该成果应用不存在相关文档");
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