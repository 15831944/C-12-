using FineUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WDFramework.WorkPlanSummary
{
    public partial class DownLoadWPS : System.Web.UI.Page
    {
        BLHelper.BLLWorkPlanSummary BLLWorkPlanSummary = new BLHelper.BLLWorkPlanSummary();
        BLHelper.BLLAttachment BLLAttachment = new BLHelper.BLLAttachment();
        BLCommon.PublicMethod publicMethod = new BLCommon.PublicMethod();
        Common.Entities.WorkPlanSummary workPlanSummary = new Common.Entities.WorkPlanSummary();
        protected void Page_Load(object sender, EventArgs e)
        {
            BindData();
        }
        public void BindData()
        {
            int WorkPlanSummaryID = Convert.ToInt32(Request.QueryString["id"].ToString());
            workPlanSummary = BLLWorkPlanSummary.FindByWorkPlanSummaryID(WorkPlanSummaryID);
            PlanWork.Text = workPlanSummary.PlanWork;
            Time.Text = workPlanSummary.Time.Value.ToShortDateString() ;
        }
        protected void btn_DownLoad_Click(object sender, EventArgs e)
        {
            try
            {
                int WorkPlanSummaryID = Convert.ToInt32(Request.QueryString["id"].ToString());
                workPlanSummary = BLLWorkPlanSummary.FindByWorkPlanSummaryID(WorkPlanSummaryID);
                if (workPlanSummary.Attachment != null && workPlanSummary.Attachment != 0)
                {
                    int attachID = Convert.ToInt32(BLLWorkPlanSummary.FindByWorkPlanSummaryID(WorkPlanSummaryID).Attachment);
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