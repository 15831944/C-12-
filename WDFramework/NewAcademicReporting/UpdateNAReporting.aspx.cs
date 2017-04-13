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
    public partial class UpdateNAReporting : System.Web.UI.Page
    {
        BLHelper.BLLNewAcademicReporting BLLNAR = new BLHelper.BLLNewAcademicReporting();
        BLHelper.BLLOperationLog BLLOL = new BLHelper.BLLOperationLog();
        BLHelper.BLLAttachment BLLattachment = new BLHelper.BLLAttachment();
        BLHelper.BLLUser BLLUser = new BLHelper.BLLUser();
        BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
        Common.Entities.NewAcademicReporting newacademicreporting = new Common.Entities.NewAcademicReporting();
        Common.Entities.NewAcademicReporting Newnareporting = new Common.Entities.NewAcademicReporting();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitData();
                //等级下拉框绑定(可选等级不大于登陆等级)
                string[] arraySecrecyLevel = new string[5] { "四级", "三级", "二级", "一级", "管理员" };
                for (int i = 0; i < Convert.ToInt32(Session["SecrecyLevel"]); i++)
                    DropDownList_SecrecyLevel.Items.Add(arraySecrecyLevel[i], (i + 1).ToString());
                DatePikerReportTime.MaxDate = DateTime.Now;
            }
        }

        //初始化
        public void InitData()
        {
            try
            {
                if (Session["NewAcademicReportingID"].ToString() != "")
                {
                    // Oldnewacademicreporting = BLLUL.FindBynewacademicreportingID(Convert.ToInt32(Session["newacademicreportingID"]));
                    newacademicreporting = BLLNAR.FindByNAReportingID(Convert.ToInt32(Session["newacademicreportingID"]));
                    DatePikerReportTime.Text = newacademicreporting.ReportTime.Value.Year + "-" + newacademicreporting.ReportTime.Value.Month + "-" + newacademicreporting.ReportTime.Value.Day;
                    DropDownList_SecrecyLevel.SelectedValue = newacademicreporting.SecrecyLevel.ToString();
                    txtPeopleCount.Text = Convert.ToString(newacademicreporting.PeopleCount);

                    txtRemark.Text = newacademicreporting.Remark;
                    txtReportPeople.Text = newacademicreporting.ReportPeople;
                    txtJobName.Text = newacademicreporting.JobName;
                    txtJobMission.Text = newacademicreporting.JobMission;
                    txtReportUnit.Text = newacademicreporting.ReportUnit;
                    txtReport.Text = newacademicreporting.Report;
                    txtReportTele.Text = newacademicreporting.ReportTele;
                    txtAcademicTitle.Text = newacademicreporting.AcademicTitle;
                    txtReportName.Text = newacademicreporting.ReportName;
                    txtReportPlace.Text = newacademicreporting.ReportPlace;
                    txtApplyFund.Text = newacademicreporting.ApplyFund;
                    txtOrganizers.Text = newacademicreporting.Organizers;
                    txtCoorganizer.Text = newacademicreporting.Coorganizer;
                    txtReportType.Text = newacademicreporting.ReportType;
                    txtMajorPeople.Text = newacademicreporting.MajorPeople;
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
                pm.SaveError(ex, this.Request);
            }
        }

        //返回插入操作日志对象
        public Common.Entities.OperationLog InsertOperationLog()
        {
            //向操作日志表插入信息
            Common.Entities.OperationLog operationLog = new Common.Entities.OperationLog();
            operationLog.LoginIP = " ";
            operationLog.LoginName = BLLUser.FindByLoginName(Session["LoginName"].ToString()).UserName;
            operationLog.OperationType = "更新";
            //operationLog.LoginName = Session["LoginName"].ToString();
            operationLog.OperationContent = "NewAcademicReporting";
            operationLog.OperationTime = DateTime.Now;
            operationLog.OperationLogID = newacademicreporting.NewAcademicReportingID;
            operationLog.Remark = Newnareporting.NewAcademicReportingID.ToString();
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

                newacademicreporting = BLLNAR.FindByNAReportingID(Convert.ToInt32(Session["NewAcademicReporting"]));
                Newnareporting.ReportTime = Convert.ToDateTime(DatePikerReportTime.Text);
                Newnareporting.PeopleCount = Convert.ToInt32(txtPeopleCount.Text.Trim());
                Newnareporting.SecrecyLevel = Convert.ToInt32(DropDownList_SecrecyLevel.SelectedValue);
                Newnareporting.Remark = Remark.Text;
                Newnareporting.ReportPeople = txtReportPeople.Text;
                Newnareporting.JobName = txtJobName.Text;
                Newnareporting.JobMission = txtJobMission.Text;
                Newnareporting.ReportUnit = txtReportUnit.Text;
                Newnareporting.Report = txtReport.Text;
                Newnareporting.ReportTele = txtReportTele.Text;
                Newnareporting.AcademicTitle = txtAcademicTitle.Text;
                Newnareporting.ReportName = txtReportName.Text;
                Newnareporting.ReportPlace = txtReportPlace.Text;
                Newnareporting.ApplyFund = txtApplyFund.Text;
                Newnareporting.Organizers = txtOrganizers.Text;
                Newnareporting.Coorganizer = txtCoorganizer.Text;
                Newnareporting.ReportType = txtReportType.Text;
                Newnareporting.MajorPeople = txtMajorPeople.Text;
                Newnareporting.EntryPerson = BLLUser.FindByLoginName(Session["LoginName"].ToString()).UserName;
                //用户等级为5级可直接通过
                if (Convert.ToInt32(Session["SecrecyLevel"]) == 5)
                    Newnareporting.IsPass = true;
                else
                    Newnareporting.IsPass = false;      

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
                        Newnareporting.AttachmentID = null;
                        break;
                    //Alert.ShowInTop("请上传附件");
                    //return;
                    default:
                        Newnareporting.AttachmentID = AttachID;
                        break;
                }

                if (Convert.ToInt32(Session["SecrecyLevel"]) != 5)
                {
                    //操作日志表对象
                    Common.Entities.OperationLog operationLog = InsertOperationLog();
                    operationLog.OperationDataID = Convert.ToInt32(Session["NewAcademicReportingID"]);
                    Newnareporting.EntryPerson = BLLUser.FindByLoginName(Session["LoginName"].ToString()).UserName;
                    Newnareporting.IsPass = false;
                    newacademicreporting.IsPass = false;
                    BLLNAR.Insert(Newnareporting);
                    operationLog.Remark = Newnareporting.NewAcademicReportingID.ToString();
                    BLLOL.Insert(operationLog);
                    Alert.ShowInTop("您的数据已提交，请等待确认");
                }
                else
                {
                    Newnareporting.NewAcademicReportingID = Convert.ToInt32(Session["NewAcademicReportingID"]);
                    BLLNAR.Update(Newnareporting);
                    Alert.ShowInTop("保存成功");
                }
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
            InitData();
        }
        //取消
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
    }
}