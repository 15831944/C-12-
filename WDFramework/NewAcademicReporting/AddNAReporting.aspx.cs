using FineUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WDFramework.NewAcademicReporting
{
    public partial class AddNAReporting : System.Web.UI.Page
    {
        BLHelper.BLLNewAcademicReporting BLLNAR = new BLHelper.BLLNewAcademicReporting();
        BLHelper.BLLOperationLog BLLOL = new BLHelper.BLLOperationLog();
        BLHelper.BLLAttachment BLLattachment = new BLHelper.BLLAttachment();
        BLHelper.BLLAgency BLLAgency = new BLHelper.BLLAgency();
        BLHelper.BLLUser BLLUser = new BLHelper.BLLUser();
        BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
        Common.Entities.NewAcademicReporting newacademicreporting = new Common.Entities.NewAcademicReporting();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //等级下拉框绑定(可选等级不大于登陆等级)
                string[] arraySecrecyLevel = new string[5] { "四级", "三级", "二级", "一级", "管理员" };
                for (int i = 0; i < Convert.ToInt32(Session["SecrecyLevel"]); i++)
                    DropDownList_SecrecyLevel.Items.Add(arraySecrecyLevel[i], (i + 1).ToString());
                DatePikerReportTime.MaxDate = DateTime.Now;
            }
        }

        //返回插入操作日志对象
        public Common.Entities.OperationLog InsertOperationLog()
        {
            //向操作日志表插入信息
            Common.Entities.OperationLog operationLog = new Common.Entities.OperationLog();
            operationLog.LoginIP = " ";
            operationLog.LoginName = Session["LoginName"].ToString();
            operationLog.OperationType = "添加";
            operationLog.OperationContent = "NewAcademicReporting";
            operationLog.OperationTime = DateTime.Now;
            return operationLog;
        }

        //保存
        protected void Save_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtReportPeople.Text.Trim() == "")
                {
                    Alert.ShowInTop("姓名不能为空！");
                    txtReportPeople.Reset();
                    return;
                }
                if (txtReportUnit.Text.Trim() == "")
                {
                    Alert.ShowInTop("报告人单位不能为空！");
                    txtReportUnit.Reset();
                    return;
                }
                if (txtReportName.Text.Trim() == "")
                {
                    Alert.ShowInTop("学术报告名称不能为空！");
                    txtReportName.Reset();
                    return;
                }
                if (txtReportPlace.Text.Trim() == "")
                {
                    Alert.ShowInTop("报告地点不能为空！");
                    txtReportPlace.Reset();
                    return;
                }
                if (DatePikerReportTime.Text == "")
                {
                    Alert.ShowInTop("报告时间不能为空！");
                    DatePikerReportTime.Reset();
                    return;
                }

                if (txtPeopleCount.Text.Trim() == "")
                {
                    Alert.ShowInTop("参与人数不能为空！");
                    txtPeopleCount.Reset();
                    return;
                }

                newacademicreporting.ReportTime = Convert.ToDateTime(DatePikerReportTime.Text);
                newacademicreporting.PeopleCount = Convert.ToInt32(txtPeopleCount.Text.Trim());
                newacademicreporting.SecrecyLevel = Convert.ToInt32(DropDownList_SecrecyLevel.SelectedValue);
                newacademicreporting.Remark = txtRemark.Text;
                newacademicreporting.ReportPeople = txtReportPeople.Text;
                newacademicreporting.JobName = txtJobName.Text;
                newacademicreporting.JobMission = txtJobMission.Text;
                newacademicreporting.ReportUnit = txtReportUnit.Text;
                newacademicreporting.Report = txtReport.Text;
                newacademicreporting.ReportTele = txtReportTele.Text;
                newacademicreporting.AcademicTitle = txtAcademicTitle.Text;
                newacademicreporting.ReportName = txtReportName.Text;
                newacademicreporting.ReportPlace = txtReportPlace.Text;
                newacademicreporting.ApplyFund = txtApplyFund.Text;
                newacademicreporting.Organizers = txtOrganizers.Text;
                newacademicreporting.Coorganizer = txtCoorganizer.Text;
                newacademicreporting.ReportType = txtReportType.Text;
                newacademicreporting.MajorPeople = txtMajorPeople.Text;
                //用户等级为5级可直接通过
                if (Convert.ToInt32(Session["SecrecyLevel"]) == 5)
                    newacademicreporting.IsPass = true;
                else
                    newacademicreporting.IsPass = false;
                newacademicreporting.EntryPerson = BLLUser.FindByLoginName(Session["LoginName"].ToString()).UserName;

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
                        newacademicreporting.AttachmentID = null;
                        break;
                    //Alert.ShowInTop("请上传附件");
                    //return;
                    default:
                        newacademicreporting.AttachmentID = AttachID;
                        break;
                }
                //向学术报告表中插入信息
                BLLNAR.Insert(newacademicreporting);
                if (Convert.ToInt32(Session["SecrecyLevel"]) != 5)
                {
                    //操作日志表对象
                    Common.Entities.OperationLog operationLog = InsertOperationLog();
                    operationLog.OperationDataID = newacademicreporting.NewAcademicReportingID;
                    BLLOL.Insert(operationLog);
                    Alert.ShowInTop("您的数据已提交，请等待确认");
                    //PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("数据已缓存，正在等待审核！"));
                    //return;
                }
                else
                    Alert.ShowInTop("保存成功");
                PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference());
            }
            catch (Exception ex)
            {
                //删除附件文件
                string path = BLLattachment.FindPath(Convert.ToInt32(newacademicreporting.AttachmentID));
                if (path != "")
                {
                    pm.DeleteFile(Convert.ToInt32(newacademicreporting.AttachmentID), path);
                    //删除附件表中的数据
                    BLLattachment.Delete(Convert.ToInt32(newacademicreporting.AttachmentID));//删除成功返回true    
                }
                //BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
                pm.SaveError(ex, this.Request);
            }
            //PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("保存成功！"));
        }

        //重置
        protected void DeleteAll_Click(object sender, EventArgs e)
        {
            txtReportPeople.Reset();
            txtJobName.Reset();
            txtJobMission.Reset();
            txtReportUnit.Reset();
            txtReport.Reset();
            DropDownList_SecrecyLevel.SelectedValue = "0";
            txtReportTele.Reset();
            txtAcademicTitle.Reset();
            txtRemark.Reset();
            txtReportName.Reset();
            Remark.Reset();
            DatePikerReportTime.Reset();
            txtReportPlace.Reset();
            txtReportType.Reset();
            txtApplyFund.Reset();
            txtPeopleCount.Reset();
            txtMajorPeople.Reset();
            txtOrganizers.Reset();
            txtCoorganizer.Reset();
        }

        //取消
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
    }
}