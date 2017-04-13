using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WDFramework.AcademicMeeting
{
    public partial class MeetingContent : System.Web.UI.Page
    {
        BLHelper.BLLAcademicMeeting BLLAcademic = new BLHelper.BLLAcademicMeeting();
        protected void Page_Load(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(Request.QueryString["id"].ToString());
            Common.Entities.AcademicMeeting Meeting = BLLAcademic.FindAcademicMeetingByMeetingID(id);
            if (Meeting != null)
                Content.Text = Meeting.MeetingContent;
            else
                Content.Text = "无";
        }
    }
}