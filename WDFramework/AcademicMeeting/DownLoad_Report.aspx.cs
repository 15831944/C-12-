using FineUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WDFramework.AcademicMeeting
{
    public partial class DownLoad_Report : System.Web.UI.Page
    {
        BLHelper.BLLScienceReport BLLScienceReprot = new BLHelper.BLLScienceReport();
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
                int ReportID = Convert.ToInt32(Request.QueryString["id"].ToString());
                Common.Entities.ScienceReport scienceReport = BLLScienceReprot.FindByReportID(ReportID);
                SReportName.Text = scienceReport.SReportName;
                SReportPeople.Text = scienceReport.SReportPeople;
                if (scienceReport.SReportTime != null)
                {
                    DateTime date = scienceReport.SReportTime.Value;
                    SReportTime.Text = date.Year + "-" + date.Month + "-" + date.Day;
                }
                SReportPlace.Text = scienceReport.SReportPlace;
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
                int Reportid = Convert.ToInt32(Request.QueryString["id"].ToString());
                int attachId = BLLScienceReprot.FindAttachmentid(Reportid);
                string srcPath = BLLAttachment.FindPath(attachId);
                if (srcPath != "")
                    publicMethod.DownloadFile(srcPath);
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
                int attachId = BLLScienceReprot.FindAttachmentid(id);
                string srcPath = BLLAttachment.FindPath(attachId);
                if (attachId == 0 || srcPath == "")
                {
                    Alert.Show("该学术报告不存在相关文档");
                }
                else
                {
                    publicMethod.DeleteFile(attachId, srcPath);
                    int ReportID = Convert.ToInt32(Request.QueryString["id"].ToString());
                    Common.Entities.ScienceReport scienceReport = BLLScienceReprot.FindByReportID(ReportID);
                    //scienceReport.AccessoryID = null;
                    BLLScienceReprot.UpdateAttachment(ReportID);
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