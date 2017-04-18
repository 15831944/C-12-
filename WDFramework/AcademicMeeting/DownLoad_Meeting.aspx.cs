﻿using FineUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WDFramework.AcademicMeeting
{
    public partial class DownLoad_Meeting : System.Web.UI.Page
    {
        BLHelper.BLLAcademicMeeting BLLAcademic = new BLHelper.BLLAcademicMeeting();
        BLHelper.BLLAttachment BLLAttachment = new BLHelper.BLLAttachment();
        BLCommon.PublicMethod publicMethod = new BLCommon.PublicMethod();
        protected void Page_Load(object sender, EventArgs e)
        {
            InitData();
        }
        //初始化
        public void InitData()
        {
            try
            {
                int AcademicID = Convert.ToInt32(Request.QueryString["id"].ToString());
                Common.Entities.AcademicMeeting Academic = BLLAcademic.FindByAcademicMeetingID(AcademicID, true);
                MeetingName.Text = Academic.MeetingName;
                Organizers.Text = Academic.Organizers;
                Coorganizers.Text = Academic.Coorganizers;
                MeetingPlace.Text = Academic.MeetingPlace;
            }
            catch (Exception ex)
            {
                BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
                pm.SaveError(ex, this.Request);
            }
        }

        //下载
        protected void DownLoad_Click(object sender, EventArgs e)
        {
            try
            {
                int AcademicID = Convert.ToInt32(Request.QueryString["id"].ToString());
                Common.Entities.AcademicMeeting Academic = BLLAcademic.FindByAcademicMeetingID(AcademicID, true);
                int attachId = BLLAcademic.FindAttachmentID(AcademicID);
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
                //return;
            }
        }
        //删除
        protected void Delete_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(Request.QueryString["id"].ToString());
                int attachId = BLLAcademic.FindAttachmentID(id);
                string srcPath = BLLAttachment.FindPath(attachId);
                if (attachId == 0 || srcPath == "")
                {
                    Alert.Show("该学术会议不存在相关文档");
                }
                else
                {
                    publicMethod.DeleteFile(attachId, srcPath);
                    int MeetingID = Convert.ToInt32(Request.QueryString["id"].ToString());
                    Common.Entities.AcademicMeeting meeting = BLLAcademic.FindByAcademicMeetingID(MeetingID, true);
                    meeting.AttachmentID = null;
                    BLLAcademic.UpdateAttachment(MeetingID);
                    //Alert.Show("删除成功！");
                    PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideReference() + Alert.GetShowInTopReference("删除成功！"));
                }
            }
            catch (Exception ex)
            {
                publicMethod.SaveError(ex, this.Request);
            }
        }
    }
}