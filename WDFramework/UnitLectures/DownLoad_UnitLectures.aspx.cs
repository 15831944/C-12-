using FineUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WDFramework.UnitLectures
{
    public partial class DownLoad_UnitLectures : System.Web.UI.Page
    {
        BLHelper.BLLUnitLectures BLLUL = new BLHelper.BLLUnitLectures();
        Common.Entities.UnitLectures unitLectures = new Common.Entities.UnitLectures();
        BLHelper.BLLAttachment BLLAttachment = new BLHelper.BLLAttachment();
        BLHelper.BLLAgency BLLAgency = new BLHelper.BLLAgency();
        BLCommon.PublicMethod publicMethod = new BLCommon.PublicMethod();
        protected void Page_Load(object sender, EventArgs e)
        {
            BindData();
        }
        public void BindData()
        {
            int unitLecturesID = Convert.ToInt32(Request.QueryString["id"].ToString());
            unitLectures = BLLUL.FindByUnitLecturesID(unitLecturesID);
            LecturesName.Text = unitLectures.LecturesName;
            UReportName.Text = unitLectures.UReportName;
            LecturesPlace.Text = unitLectures.LecturesPlace;
            DateTime? date = unitLectures.LecturesTime;
            LecturesTime.Text = date.Value.Year+"-"+date.Value.Month+"-"+date.Value.Day;
            Agency.Text = BLLAgency.FindAgenName(unitLectures.AgencyID);   
        }
        protected void btn_DownLoadContract_Click(object sender, EventArgs e)
        {
            try
            {
                int unitLecturesID = Convert.ToInt32(Request.QueryString["id"].ToString());
                unitLectures = BLLUL.FindByUnitLecturesID(unitLecturesID);
                if (unitLectures.AttachmentID != null && unitLectures.AttachmentID != 0)
                {
                    int attachID = Convert.ToInt32(BLLUL.FindAttachmentid(unitLecturesID));
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