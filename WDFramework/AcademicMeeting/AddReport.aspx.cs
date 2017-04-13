/**编写人：李金秋
 * 时间：2014年6月20号
 * 功能：新增学术报告界面
 * 修改履历：
 **/
using FineUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WDFramework.AcademicMeeting
{
    public partial class AddReport : System.Web.UI.Page
    {
        BLHelper.BLLAgency BLLAgency = new BLHelper.BLLAgency();
        BLHelper.BLLAcademicMeeting BLLAM = new BLHelper.BLLAcademicMeeting();
        BLHelper.BLLScienceReport BLLScience = new BLHelper.BLLScienceReport();
        BLHelper.BLLAttachment BLLattachment = new BLHelper.BLLAttachment();
        BLHelper.BLLOperationLog operationLog = new BLHelper.BLLOperationLog();
        BLHelper.BLLUser BLLUser = new BLHelper.BLLUser();
        BLCommon.PublicMethod publicMethod = new BLCommon.PublicMethod();
        Common.Entities.ScienceReport scienceReport = new Common.Entities.ScienceReport();
        protected void Page_Load(object sender, EventArgs e)
        {
            BindData();
            DatePicker_ReportTime.MaxDate = DateTime.Now;
        }
        public void BindData()
        {
            //对所属部门下拉框的绑定
            //DropDownList_AgencyName.DataSource = BLLAgency.FindAgencyBySecrecyLevel(Convert.ToInt32(Session["SecrecyLevel"]));
            List<string> list = BLLAgency.FindAgencyBySecrecyLevel(Convert.ToInt32(Session["SecrecyLevel"]));
            int count = list.Count();
            for (int i = 0; i < count; i++)
                DropDownList_AgencyName.Items.Add(list[i], (i + 1).ToString());
            DropDownList_AgencyName.DataBind();
            //对所属会议下拉框的绑定
            //DropDownListMeetingName.DataSource = BLLAM.FindMeetingNameBySecrecyLevel(Convert.ToInt32(Session["SecrecyLevel"]));
            List<string> listAgency = BLLAM.FindMeetingNameBySecrecyLevel(Convert.ToInt32(Session["SecrecyLevel"]));
            count = listAgency.Count();
            for (int i = 0; i < count; i++)
                DropDownListMeetingName.Items.Add(listAgency[i], (i + 1).ToString());
            //等级下拉框绑定(可选等级不大于登陆等级)
            string[] arraySecrecyLevel = new string[5] { "四级", "三级", "二级", "一级", "管理员" };
            for (int i = 0; i < Convert.ToInt32(Session["SecrecyLevel"]); i++)
                DropDownList_SecrecyLevel.Items.Add(arraySecrecyLevel[i], (i + 1).ToString());

        }
        //返回插入学术报告对象
        public Common.Entities.ScienceReport InserScienceReport()
        {
            Common.Entities.ScienceReport scienceReport = new Common.Entities.ScienceReport();
            scienceReport.AgencyID = BLLAgency.SelectAgencyID(DropDownList_AgencyName.SelectedText);
            scienceReport.EntryPerson = BLLUser.FindByLoginName(Session["LoginName"].ToString()).UserName;//必填
            scienceReport.SReportTime = DatePicker_ReportTime.SelectedDate;
            //if (txtReportPlace.Text != "")
            //    scienceReport.SReportPlace = txtReportPlace.Text;
            //else
            //    scienceReport.SReportPlace = null;
            scienceReport.SReportPlace = txtReportPlace.Text;
            scienceReport.SReportPeople = txtReportPeople.Text;//必填
            scienceReport.SReportName = txtReportName.Text;//必填
            scienceReport.SecrecyLevel = Convert.ToInt32(DropDownList_SecrecyLevel.SelectedValue);//必填
            scienceReport.MeetingID = BLLAM.FindMeetingID(DropDownListMeetingName.SelectedText);//必填
            //如果AttachmentID为null则没有添加附件
            //scienceReport.AccessoryID = AttachmentID;
            if (Convert.ToInt32(Session["SecrecyLevel"]) == 5)//必填
                scienceReport.IsPass = true;
            else
                scienceReport.IsPass = false;
            return scienceReport;
        }
        //返回插入操作日志对象
        public Common.Entities.OperationLog InsertOperationLog()
        {
            //向操作日志表插入信息
            Common.Entities.OperationLog operationLog = new Common.Entities.OperationLog();
            operationLog.LoginIP = " ";
            operationLog.LoginName = BLLUser.FindByLoginName(Session["LoginName"].ToString()).UserName; 
            operationLog.OperationType = "添加";
            operationLog.OperationContent = "ScienceReport";
            operationLog.OperationTime = DateTime.Now;
            return operationLog;
        }
        //保存
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                scienceReport = InserScienceReport();
                if (Session["LoginName"].ToString() == "")
                {
                    Response.Redirect("login.aspx");
                    Alert.Show("登录超时！");
                }
                if (txtReportName.Text.Trim() == "")
                {
                    Alert.ShowInTop("报告名称不能为空！");
                    txtReportName.Reset();
                    return;
                }
                if (txtReportName.Text.Trim() == "")
                {
                    Alert.ShowInTop("报告名称不能为空！");
                    txtReportName.Reset();
                    return;
                }
                if (txtReportPeople.Text.Trim() == "")
                {
                    Alert.ShowInTop("报告人不能为空！");
                    txtReportPeople.Reset();
                    return;
                }
                //int attachId = publicMethod.UpLoad(fileupload);
                //Common.Entities.ScienceReport scienceReport = InserScienceReport();
                //上传文件并向附件表中插入数据
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
                        scienceReport.AccessoryID = null;
                        break;
                        //Alert.ShowInTop("请上传附件");
                        //return;
                    default:
                        scienceReport.AccessoryID = AttachID;
                        break;
                }
                //向学术报告表中插入数据
                BLLScience.Insert(scienceReport);
                Common.Entities.OperationLog OL = InsertOperationLog();
                if (Convert.ToInt32(Session["SecrecyLevel"]) != 5)
                {
                    //向操作日志表中插入数据
                    OL.OperationDataID = scienceReport.ScienceReportID;
                    operationLog.Insert(OL);
                    Alert.ShowInTop("您的数据已提交，请等待确认");
                }
                else
                    Alert.ShowInTop("保存成功");
                PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference());
                //Reset();
            }
            catch (Exception ex)
            {
                int? attachid = scienceReport.AccessoryID;
                if (attachid != 0 && attachid != null)
                {//删除附件
                    //在附件表中删除学术报告附件
                    BLLattachment.Delete(Convert.ToInt32(attachid));
                    //删除附件文件
                    string path = BLLattachment.FindPath(Convert.ToInt32(attachid));
                    if (path != "")
                        publicMethod.DeleteFile(Convert.ToInt32(attachid), path);
                }
                BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
                pm.SaveError(ex, this.Request);
                return;
            }
        }
        //重置
        protected void btnSet_Click(object sender, EventArgs e)
        {
            Reset();
        }
        //取消
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
        public void Reset()
        {
            txtReportName.Reset();
            txtReportPeople.Reset();
            txtReportPlace.Reset();
            DatePicker_ReportTime.Reset();
            DropDownList_SecrecyLevel.SelectedValue = "0";
            DropDownList_AgencyName.SelectedValue = "0";
            DropDownListMeetingName.SelectedValue = "0";
            PageContext.RegisterStartupScript("clearFile();");
            //filePath.Reset();
        }
    }
}