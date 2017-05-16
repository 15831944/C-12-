using Common.Entities;
using FineUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WDFramework.Achievement.Monograph
{
    public partial class Down_Photo : System.Web.UI.Page
    {
        BLHelper.BLLMonograph BLLMonograph = new BLHelper.BLLMonograph();
        BLHelper.BLLAttachment BLLAttachments = new BLHelper.BLLAttachment();
        BLCommon.PublicMethod publicMethods = new BLCommon.PublicMethod();
        protected void Page_Load(object sender, EventArgs e)
        {
            InitData();
        }
        public void InitData()
        {
            int MonographID = Convert.ToInt32(Request.QueryString["id"].ToString());
            //int photoId = BLLMonograph.FindFAttachmentID(MonographID);
            if (MonographID != 0)
            {
                string srcPaths = BLLAttachments.FindPath(MonographID);
                if (srcPaths != "")
                    Image_show.ImageUrl = srcPaths;
                else
                    Image_show.ImageUrl="../../images/blank.png";
            }
            else
                Image_show.ImageUrl = "../../images/blank.png";
        }
        //下载
        protected void DownLoad_Click(object sender, EventArgs e)
        {
            try
            {
                int MonographID = Convert.ToInt32(Request.QueryString["id"].ToString());
                int photoId = BLLMonograph.FindFAttachmentID(MonographID);
                if (photoId != 0)
                {
                    string srcPaths = BLLAttachments.FindPath(photoId);
                    if (srcPaths != "")
                        publicMethods.DownloadPhoto(srcPaths);
                    else
                        Alert.ShowInTop("无附件可下载!");
                }
                else
                    Alert.ShowInTop("无附件可下载!");
            }
            catch (Exception ex)
            {
                BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
                pm.SaveError(ex, this.Request);
                PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHidePostBackReference() + Alert.GetShowInTopReference("附件下载失败，请与管理员联系！"));
            }
        }
        //删除
        protected void Delete_Click(object sender, EventArgs e)
        {
            try
            {
                int MonographID = Convert.ToInt32(Request.QueryString["id"].ToString());
                int photoId = BLLMonograph.FindFAttachmentID(MonographID);
                string srcPath = BLLAttachments.FindPath(photoId);
                if (photoId == 0 || srcPath == "")
                {
                    Alert.Show("该人员信息不存在人员照片");
                }
                else
                {
                    publicMethods.DeleteFile(photoId, srcPath);
                    PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideReference() + Alert.GetShowInTopReference("删除成功！"));
                }
            }
            catch (Exception ex)
            {
                publicMethods.SaveError(ex, this.Request);
            }
        }
    }
}