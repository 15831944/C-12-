using FineUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WDFramework.Announcement
{
    public partial class DownLoad_Announce : System.Web.UI.Page
    {
        BLHelper.BLLAnnouncement BLLAnnounce = new BLHelper.BLLAnnouncement();
        BLHelper.BLLAttachment BLLAttachment = new BLHelper.BLLAttachment();
        BLCommon.PublicMethod publicMethod = new BLCommon.PublicMethod();
        protected void Page_Load(object sender, EventArgs e)
        {
            InitData();
        }
        //初始化
        public void InitData()
        {
            int AnnouncementID = Convert.ToInt32(Request.QueryString["id"].ToString());
            Common.Entities.Announcement Announce = BLLAnnounce.Find(AnnouncementID);
            HeadLine.Text = Announce.HeadLine;
            switch (Announce.AnnouncementSortName)
            {
                //case 1:
                //    AnnouncementSortName.Text = "通知";
                //    break;
                //case 2:
                //    AnnouncementSortName.Text = "学校公告";
                //    break;
                //case 3:
                //    AnnouncementSortName.Text = "外来公告";
                //    break;
            }
            
            if (Announce.Time != null)
            {
                DateTime date = Announce.Time.Value;
                Time.Text = date.Year + "-" + date.Month + "-" + date.Day;
            }
        }

        //下载
        protected void DownLoad_Click(object sender, EventArgs e)
        {
            try
            {
                int AnnouncementID = Convert.ToInt32(Request.QueryString["id"].ToString());
                Common.Entities.Announcement Announce = BLLAnnounce.Find(AnnouncementID);
                int attachId = BLLAnnounce.FindAttachmentID(AnnouncementID);
                if (attachId != 0)
                {
                    string srcPath = BLLAttachment.FindPath(attachId);
                    if (srcPath != "")
                        publicMethod.DownloadFile(srcPath);
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
                //Alert.ShowInTop("附件下载失败!");
            }
        }
    }
}