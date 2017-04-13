/**编写人：吕博杨
 * 时间：2015年11月29日
 * 功能：更新学术会议界面
 * 修改履历：    暂无
 **/
using DataBase;
using FineUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common.Entities;
namespace WDFramework.AcademicMeeting
{
    public partial class UpdateAcademic : System.Web.UI.Page
    {
        BLHelper.BLLAcademicMeeting BLLAM = new BLHelper.BLLAcademicMeeting();
        BLHelper.BLLOperationLog BLLOL = new BLHelper.BLLOperationLog();
        BLHelper.BLLAttachment BLLattachment = new BLHelper.BLLAttachment();
        BLHelper.BLLUser BLLUser = new BLHelper.BLLUser();
        BLCommon.PublicMethod publicMethod = new BLCommon.PublicMethod();
        BLHelper.BLLBasicCode bllBasicCode = new BLHelper.BLLBasicCode();
        OperationLog log = new OperationLog();
        Common.Entities.AcademicMeeting academicMeeting = new Common.Entities.AcademicMeeting();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
                //DatePicker_StratTime.MaxDate = DateTime.Now;
                //DatePicker_EndTime.MaxDate = DateTime.Now;
            }
        }
        //界面初始化
        public void BindData()
        {
            //会议分类下拉框绑定
            InitDropDownListSort();
            //等级下拉框绑定(可选等级不大于登陆等级)
            string[] arraySecrecyLevel = new string[5] { "四级", "三级", "二级", "一级", "管理员" };
            for (int i = 0; i < Convert.ToInt32(Session["SecrecyLevel"]); i++)
                DropDownList_SecrecyLevel.Items.Add(arraySecrecyLevel[i], (i + 1).ToString());

            //lby ↓
            try
            {
                Common.Entities.AcademicMeeting academic = BLLAM.FindAcademicMeetingByMeetingID(Convert.ToInt32(Session["AcademicMeetingID"]));

                txtMeetingName.Text = academic.MeetingName;
                txtOrganizer.Text = academic.Organizers;
                txtCoorganizer.Text = academic.Coorganizers;
                txtMeetingPlace.Text = academic.MeetingPlace;
                txtMajorPerson.Text = academic.MeetingMajorPerson;
                AttendMeetingPeople.Text = academic.AttendMeetingPeople;
                txtMeetingHost.Text = academic.MeetingHost;
                txtMajorTheme.Text = academic.MeetingMajorTheme;
                DatePicker_StratTime.SelectedDate = academic.StratTime;
                DatePicker_EndTime.SelectedDate = academic.EndTime;
                txtMeetingCount.Text = academic.MeetingCount;
                txtProceedingsofTitle.Text = academic.ProceedingsofTitle;
                DropDownList_SecrecyLevel.SelectedIndex = Convert.ToInt32(academic.SecrecyLevel - 1);
                MeetingContent.Text = academic.MeetingContent;
                DropDownList_MeetingSort.SelectedValue = DropDownList_MeetingSort.Items.FindByText(academic.MeetingSortName).Value;
            }
            catch (Exception ex)
            {
                publicMethod.SaveError(ex, this.Request);
            }

        }
        //保存增加会议信息
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["LoginName"].ToString() == "")
                {
                    Response.Redirect("login.aspx");
                    Alert.Show("登录超时！");
                }
                if (DatePicker_EndTime != null)
                {
                    if (DatePicker_StratTime.SelectedDate > DatePicker_EndTime.SelectedDate)
                    {
                        Alert.ShowInTop("结束日期应该大于开始日期！");
                        return;
                    }
                }
                if (txtMeetingName.Text.Trim() == "")
                {
                    Alert.ShowInTop("会议名称不能为空！");
                    txtMeetingName.Reset();
                    return;
                }
                if (txtOrganizer.Text.Trim() == "")
                {
                    Alert.ShowInTop("主办方不能为空！");
                    txtOrganizer.Reset();
                    return;
                }
                if (txtMeetingPlace.Text.Trim() == "")
                {
                    Alert.ShowInTop("会议地点不能为空！");
                    txtMeetingPlace.Reset();
                    return;
                }
                int AttachmentID = BLLAM.FindAttachmentID(Convert.ToInt32(Session["AcademicMeetingID"]));
                int PhotoID = BLLAM.FindPhotoID(Convert.ToInt32(Session["AcademicMeetingID"]));
                string attachmentPath = BLLattachment.FindPath(AttachmentID);
                string photoPath = BLLattachment.FindPath(PhotoID);
                if (Convert.ToInt32(Session["SecrecyLevel"]) == 5)
                {
                    UpdateValue();
                    int Attachment = publicMethod.UpLoadFile(fileupload).Attachid;
                    if (Attachment != -3)
                    {
                        switch (Attachment)
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
                        }
                        academicMeeting.AttachmentID = Attachment;
                        publicMethod.DeleteFile(AttachmentID, attachmentPath);
                    }
                    else
                    {
                        if (AttachmentID != 0)
                            academicMeeting.AttachmentID = AttachmentID;
                    }

                    Attachment = publicMethod.UpLoadPhoto(photoupload);
                    if (Attachment != -3)
                    {
                        switch (Attachment)
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
                        }
                        academicMeeting.PhotoID = Attachment;
                        publicMethod.DeleteFile(PhotoID, photoPath);
                    }
                    else
                    {
                        if (PhotoID != 0)
                            academicMeeting.PhotoID = PhotoID;
                    }
                    academicMeeting.IsPass = true;
                    academicMeeting.AcademicMeetingID = Convert.ToInt32(Session["AcademicMeetingID"]);
                    BLLAM.Update(academicMeeting);
                    PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("保存成功！"));
                }
                else
                {
                    UpdateValue();
                    int Attachment = publicMethod.UpLoadFile(fileupload).Attachid;
                    if (Attachment != -3)
                    {
                        switch (Attachment)
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
                        }
                        academicMeeting.AttachmentID = Attachment;
                    }
                    else
                    {
                        if (AttachmentID != 0)
                            academicMeeting.AttachmentID = AttachmentID;
                    }

                    Attachment = publicMethod.UpLoadPhoto(photoupload);
                    if (Attachment != -3)
                    {
                        switch (Attachment)
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
                        }
                        academicMeeting.PhotoID = Attachment;
                    }
                    else
                    {
                        if (PhotoID != 0)
                            academicMeeting.PhotoID = PhotoID;
                    }

                    academicMeeting.IsPass = false;
                    BLLAM.Insert(academicMeeting);
                    BLLAM.UpdateIsPass(Convert.ToInt32(Session["AcademicMeetingID"]), false);
                    log.LoginIP = "";
                    log.LoginName = Session["LoginName"].ToString();
                    log.OperationContent = "AcademicMeeting";
                    log.OperationTime = DateTime.Now;
                    log.OperationType = "更新";
                    log.OperationDataID = Convert.ToInt32(Session["AcademicMeetingID"]);
                    log.Remark = academicMeeting.AcademicMeetingID.ToString();
                    BLLOL.Insert(log);
                    PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("您的数据已提交，请等待确认！"));
                }
            }
            catch (Exception ex)
            {
                int attachid = Convert.ToInt32(academicMeeting.AttachmentID);
                string path = BLLattachment.FindPath(attachid);
                publicMethod.DeleteFile(attachid, path);
                publicMethod.SaveError(ex, this.Request);
            }
        }
        //更新数值
        public void UpdateValue()
        {
            academicMeeting.MeetingName = txtMeetingName.Text;//必填
            academicMeeting.Organizers = txtOrganizer.Text;//必填
            academicMeeting.Coorganizers = txtCoorganizer.Text;
            //if (txtCoorganizer.Text == "")
            //    academicMeeting.Coorganizers = null;
            //else
            //    academicMeeting.Coorganizers = txtCoorganizer.Text;
            academicMeeting.StratTime = DatePicker_StratTime.SelectedDate;
            academicMeeting.EndTime = DatePicker_EndTime.SelectedDate;
            academicMeeting.MeetingPlace = txtMeetingPlace.Text;//必填
            academicMeeting.ProceedingsofTitle = txtProceedingsofTitle.Text;
            //if (txtProceedingsofTitle.Text == "")
            //    academicMeeting.ProceedingsofTitle = null;
            //else
            //    academicMeeting.ProceedingsofTitle = txtProceedingsofTitle.Text;
            academicMeeting.SecrecyLevel = Convert.ToInt32(DropDownList_SecrecyLevel.SelectedValue);//必填
            if (DropDownList_MeetingSort.SelectedText == "请选择")
            {
                academicMeeting.MeetingSortName = null;
            }
            else
                academicMeeting.MeetingSortName = DropDownList_MeetingSort.SelectedText;
            //如果AttachmentID为null则没有添加附件

            //用户等级为5级可直接通过
            if (Convert.ToInt32(Session["SecrecyLevel"]) == 5)
                academicMeeting.IsPass = true;//必填
            else
                academicMeeting.IsPass = false;
            academicMeeting.EntryPerson = BLLUser.FindByLoginName(Session["LoginName"].ToString()).UserName;//必填

            //lby ↓
            bool IsHasLetter = false;
            foreach (char temp in txtMeetingCount.Text)
                if (char.IsLetter(temp))
                {
                    IsHasLetter = true;
                    break;
                }
            if (!IsHasLetter)
                academicMeeting.MeetingCount = txtMeetingCount.Text;

            academicMeeting.MeetingMajorPerson = txtMajorPerson.Text;
            academicMeeting.MeetingMajorTheme = txtMajorTheme.Text;
            academicMeeting.MeetingHost = txtMeetingHost.Text;
            academicMeeting.MeetingContent = MeetingContent.Text;
            academicMeeting.AttendMeetingPeople = AttendMeetingPeople.Text;
        }
        //重置会议信息事件
        protected void btnSet_Click(object sender, EventArgs e)
        {
            txtMeetingName.Reset();
            txtOrganizer.Reset();
            txtCoorganizer.Reset();
            txtMeetingPlace.Reset();
            txtProceedingsofTitle.Reset();
            DropDownList_MeetingSort.Reset();
            DropDownList_SecrecyLevel.SelectedValue = "0";
            DatePicker_StratTime.Reset();
            DatePicker_EndTime.Reset();
            txtMeetingCount.Reset();
            txtMajorPerson.Reset();
            txtMajorTheme.Reset();
            AttendMeetingPeople.Reset();
            txtMeetingCount.Reset();
            txtMeetingHost.Reset();
            MeetingContent.Reset();
            PageContext.RegisterStartupScript("clearFile();");
        }
        //初始化会议分类下拉框
        public void InitDropDownListSort()
        {
            try
            {
                List<BasicCode> list = bllBasicCode.FindALLName("会议分类名称");
                for (int i = 0; i < list.Count(); i++)
                {
                    DropDownList_MeetingSort.Items.Add(list[i].CategoryContent.ToString(), (i + 1).ToString());
                }
            }
            catch (Exception ex)
            {
                BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
                pm.SaveError(ex, this.Request);
                return;
            }
        }
    }
}