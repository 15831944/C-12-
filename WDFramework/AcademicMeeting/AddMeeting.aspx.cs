/**编写人：李金秋
 * 时间：2014年6月20号
 * 功能：新增学术会议界面
 * 修改履历：    8月5日：保存 重置 取消按钮事件的添加
 *              2、修改人：吕博杨
 *                 修改时间：2015年11月29日
 *                 修改内容：根据AcademicMeeting部分字段的更改协调更改该页面后台代码
 *                          添加会议照片上传功能
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
    public partial class AddMeeting : System.Web.UI.Page
    {
        BLHelper.BLLAcademicMeeting BLLAM = new BLHelper.BLLAcademicMeeting();
        BLHelper.BLLOperationLog BLLOL = new BLHelper.BLLOperationLog();
        BLHelper.BLLAttachment BLLattachment = new BLHelper.BLLAttachment();
        BLHelper.BLLUser BLLUser = new BLHelper.BLLUser();
        BLCommon.PublicMethod publicMethod = new BLCommon.PublicMethod();
        BLHelper.BLLBasicCode bllBasicCode = new BLHelper.BLLBasicCode();
        Common.Entities.AcademicMeeting NewcademicMeeting = new Common.Entities.AcademicMeeting();
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

        }
        //保存增加会议信息
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                NewcademicMeeting = ObjectAcademicMeeting();
                if (Session["LoginName"].ToString() == "")
                {
                    Response.Redirect("login.aspx");
                    Alert.Show("登录超时！");
                }
                string MeetingName = txtMeetingName.Text;
                int MeetingID = BLLAM.FindMeetingID(MeetingName);
                if (MeetingID != 0)
                {
                    //txtMeetingName.Text = "";
                    txtMeetingName.Reset();
                    Alert.ShowInTop("该学术会议名称已存在，请重新输入！");
                    return;
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
                //int attachId = publicMethod.UpLoad(filePath);
                //学术会议表对象
                //Common.Entities.AcademicMeeting NewcademicMeeting = ObjectAcademicMeeting();
                int AttachID = publicMethod.UpLoadFile(fileupload).Attachid;
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
                        NewcademicMeeting.AttachmentID = null;
                        break;
                        //Alert.ShowInTop("请上传附件");
                        //return;
                    default:
                        NewcademicMeeting.AttachmentID = AttachID;
                        break;
                }

                AttachID = publicMethod.UpLoadPhoto(photoupload);
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
                        NewcademicMeeting.PhotoID = null;
                        break;
                    //Alert.ShowInTop("请上传附件");
                    //return;
                    default:
                        NewcademicMeeting.PhotoID = AttachID;
                        break;
                }
                //向学术会议表插入信息
                BLLAM.Insert(NewcademicMeeting);
                //非管理员登陆需要向操作日志表中插入信息，等待管理员审核
                if (Convert.ToInt32(Session["SecrecyLevel"]) != 5)
                {
                    //操作日志表对象
                    Common.Entities.OperationLog operationLog = InsertOperationLog();
                    operationLog.OperationDataID = NewcademicMeeting.AcademicMeetingID;
                    //向操作日志表中插入信息
                    BLLOL.Insert(operationLog);
                    Alert.ShowInTop("您的数据已提交，请等待确认");
                }
                else
                    Alert.ShowInTop("保存成功");
                PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference());
            }
            catch (Exception ex)
            {
                int AttactID = BLLAM.FindAttachmentID(NewcademicMeeting.AcademicMeetingID);
                string strPath;
                if (AttactID != 0)
                {
                    strPath = BLLattachment.FindPath(AttactID);
                    if (strPath != "")
                    {
                        //删除附件文件
                        publicMethod.DeleteFile(AttactID, strPath);
                        //在附件表中删除附件数据
                        BLLattachment.Delete(AttactID);
                    }
                }
                BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
                pm.SaveError(ex, this.Request);
                Alert.ShowInTop("保存失败！");
               // return;
            }
        }
        //返回插入学术会议对象
        public Common.Entities.AcademicMeeting ObjectAcademicMeeting()
        {

            //向学术会议表中插入会议信息
            Common.Entities.AcademicMeeting academicMeeting = new Common.Entities.AcademicMeeting();
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
            //lby ↓
            academicMeeting.AttendMeetingPeople = AttendMeetingPeople.Text;
            return academicMeeting;

        }
        //返回插入操作日志对象
        public Common.Entities.OperationLog InsertOperationLog()
        {
            //向操作日志表插入信息
            Common.Entities.OperationLog operationLog = new Common.Entities.OperationLog();
            operationLog.LoginIP = " ";
            operationLog.LoginName = Session["LoginName"].ToString();
            operationLog.OperationType = "添加";
            operationLog.OperationContent = "AcademicMeeting";
            operationLog.OperationTime = DateTime.Now;
            return operationLog;
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
            //lby ↓
            AttendMeetingPeople.Reset();
            txtMeetingCount.Reset();
            txtMeetingHost.Reset();
            MeetingContent.Reset();
            PageContext.RegisterStartupScript("clearFile();");
            //fileupload.Reset();
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