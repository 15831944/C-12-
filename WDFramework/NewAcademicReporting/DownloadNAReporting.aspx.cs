using FineUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WDFramework.NewAcademicReporting
{
    public partial class DownloadNAReporting : System.Web.UI.Page
    {
        BLHelper.BLLNewAcademicReporting BLLNAR = new BLHelper.BLLNewAcademicReporting();
        Common.Entities.NewAcademicReporting newacademicreporting = new Common.Entities.NewAcademicReporting();
        BLHelper.BLLAttachment BLLAttachment = new BLHelper.BLLAttachment();
        BLCommon.PublicMethod publicMethod = new BLCommon.PublicMethod();
        protected void Page_Load(object sender, EventArgs e)
        {
            BindData();
        }

        public void BindData()
        {
            int NAReportingID = Convert.ToInt32(Request.QueryString["id"].ToString());
            newacademicreporting = BLLNAR.FindByNAReportingID(NAReportingID);
            ReportPeople.Text = newacademicreporting.ReportPeople;
            ReportUnit.Text = newacademicreporting.ReportUnit;
            ReportName.Text = newacademicreporting.ReportName;
            DateTime? date = newacademicreporting.ReportTime;
            ReportTime.Text = date.Value.Year + "-" + date.Value.Month + "-" + date.Value.Day;
            ReportPlace.Text = newacademicreporting.ReportPlace;
            PeopleCount.Text = Convert.ToString(newacademicreporting.PeopleCount);
        }

        protected void DownFile_Click(object sender, EventArgs e)
        {
            try
            {
                int NAReportingID = Convert.ToInt32(Request.QueryString["id"].ToString());
                newacademicreporting = BLLNAR.FindByNAReportingID(NAReportingID);
                if (newacademicreporting.AttachmentID != null && newacademicreporting.AttachmentID != 0)
                {
                    int attachID = Convert.ToInt32(BLLNAR.FindAttachmentID(NAReportingID));
                    string path = BLLAttachment.FindPath(attachID);
                    if (path != "")
                    {
                        publicMethod.DownloadFile(path);
                    }
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
            }
        }
    }
}