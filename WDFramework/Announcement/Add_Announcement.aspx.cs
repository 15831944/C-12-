/**编写人：李金秋
 * 时间：2014年8月12号
 * 功能：添加通知公告界面
 * 修改履历：
 **/
using Common.Entities;
using FineUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class Add_Announcement : System.Web.UI.Page
    {
        //通知公告的保密级别均为1
        BLHelper.BLLAnnouncement BLLAnnouncement = new BLHelper.BLLAnnouncement();
        Common.Entities.Announcement NewAnnouncement = new Common.Entities.Announcement();
        BLHelper.BLLAttachment BLLattachment = new BLHelper.BLLAttachment();
        BLHelper.BLLUser BLLUser = new BLHelper.BLLUser();
        BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
        BLHelper.BLLBasicCode bllBasicCode = new BLHelper.BLLBasicCode();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitDropDownListSort();
                DatePicker_Time.MaxDate = DateTime.Now;
            }
        }
        //重置通知通告信息
        protected void btnReSet_Click(object sender, EventArgs e)
        {
            txtHeadLine.Reset();
            DropDownList_Sort.SelectedValue = "1";
            DatePicker_Time.Reset();
            txtSourceAgency.Reset();
            PageContext.RegisterStartupScript("clearFile();");
            //DropDownList_SecrecyLevel.SelectedValue = "1";
            //filePath.Reset();
        }
        //保存通知公告信息
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (txtHeadLine.Text.Trim() == "")
            {
                Alert.ShowInTop("标题不能为空！");
                txtHeadLine.Reset();
                return;
            }
           
            NewAnnouncement.HeadLine = txtHeadLine.Text;
            NewAnnouncement.AnnouncementSortName =DropDownList_Sort.SelectedItem .Text;
            NewAnnouncement.Time = DatePicker_Time.SelectedDate;
            NewAnnouncement.SourceAgency = txtSourceAgency.Text;
            //NewAnnouncement.SecrecyLevel = Convert.ToInt32(DropDownList_SecrecyLevel.SelectedText);
            NewAnnouncement.SecrecyLevel = 1;
            NewAnnouncement.EntryPerson = BLLUser.FindByLoginName(Session["LoginName"].ToString()).UserName;
            NewAnnouncement.IsPass = true;
            int AttachID = pm.UpLoadFile(fileupload).Attachid;
            switch (AttachID)
            {
                case -1:
                    Alert.ShowInTop("文件类型不符，请重新选择！");
                    return;
                case 0:
                    Alert.ShowInTop("文件名已经存在！");
                    return;
                case -2:
                    Alert.ShowInTop("文件不能大于150M");
                    return;
                case -3:
                    Alert.ShowInTop("请上传附件");
                    return;
                default:
                    NewAnnouncement.AttachmentID = AttachID;
                    break;
            }
            BLLAnnouncement.Insert(NewAnnouncement);
            Alert.ShowInTop("通知公告信息添加成功！");
            PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference());        
        }
        //取消录入通知公告信息
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
        //初始化通知公告分类下拉框
        public void InitDropDownListSort()
        {
            List<BasicCode> list = bllBasicCode.FindALLName("通知公告分类名称");
            for (int i = 0; i < list.Count(); i++)
            {
                DropDownList_Sort.Items.Add(list[i].CategoryContent.ToString(), (i + 1).ToString());
            }
        }
    }
}