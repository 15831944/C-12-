/**编写人：未知
 * 时间：  未知
 * 功能：  学术会议基础类
 * 修改履历：       1、修改人：吕博杨
 *                    修改时间：2015年11月29日
 *                    修改内容：将27行显示内容提取的字段改为AttendMeetingPeople
 **/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WDFramework.AcademicMeeting
{
    public partial class AttendMeetingPeople : System.Web.UI.Page
    {
        BLHelper.BLLAttendMeeting BLLAttend = new BLHelper.BLLAttendMeeting();
        BLHelper.BLLUser BLLUser = new BLHelper.BLLUser();
        BLHelper.BLLAcademicMeeting BLLAcademic = new BLHelper.BLLAcademicMeeting();
        protected void Page_Load(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(Request.QueryString["id"].ToString());
            Common.Entities.AcademicMeeting Meeting = BLLAcademic.FindAcademicMeetingByMeetingID(id);
            if (Meeting != null)
                Content.Text = Meeting.AttendMeetingPeople;
            else
                Content.Text = "无";
            //List<int?> listUserID = BLLAttend.FindUserNameByMeetingID(id, Convert.ToInt32(Session["SecrecyLevel"]));
            //string UserName;
            //for (int i = 0; i < listUserID.Count(); i++)
            //{
            //    UserName = BLLUser.FindUserName(listUserID[i]);
            //    if (UserName != "")
            //        Content.Text += UserName + "  ";
                
            //}
            //if (Content.Text == "")
            //    Content.Text = "无";
        }
    }
}