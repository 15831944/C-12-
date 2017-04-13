using FineUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WDFramework.ContractAndPact.Pact
{
    public partial class DownLoad_Pact : System.Web.UI.Page
    {
        BLHelper.BLLPact BLLPact = new BLHelper.BLLPact();
        BLHelper.BLLAttachment BLLAttachment = new BLHelper.BLLAttachment();
        BLHelper.BLLProject BLLProject = new BLHelper.BLLProject();
        BLCommon.PublicMethod publicMethod = new BLCommon.PublicMethod();
        Common.Entities.Pact Pact = new Common.Entities.Pact();
        protected void Page_Load(object sender, EventArgs e)
        {
            BindData();
        }
        public void BindData()
        {
            int PactID = Convert.ToInt32(Request.QueryString["id"].ToString());
            Pact = BLLPact.FindByPactID(PactID);
            PactNum.Text = Pact.PactNum;
            PactType.Text = Pact.PactType;
            if(Pact.StartTime!=null)
            {
                DateTime date = Pact.StartTime.Value;
                StartTime.Text = date.Year + "-" + date.Month + "-" + date.Day;
            }
            if (Pact.EndTime != null)
            {
                DateTime date = Pact.EndTime.Value;
                EndTime.Text = date.Year + "-" + date.Month + "-" + date.Day;
            }
            ProjectID.Text = BLLProject.SelectProjectName(Convert.ToInt32(Pact.ProjectID));
        }
        protected void btn_DownLoadContract_Click(object sender, EventArgs e)
        {
            try
            {
                int PactID = Convert.ToInt32(Request.QueryString["id"].ToString());
                Pact = BLLPact.FindByPactID(PactID);
                if (Pact.AttachmentID != null && Pact.AttachmentID != 0)
                {
                    int attachID = Convert.ToInt32(BLLPact.FindAttachmentID(PactID));
                    string path = BLLAttachment.FindPath(attachID);
                    if (path != "")
                        publicMethod.DownloadFile(path);
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
        //删除
        protected void Delete_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(Request.QueryString["id"].ToString());
                int attachId = BLLPact.FindAttachmentID(id);
                string srcPath = BLLAttachment.FindPath(attachId);
                if (attachId == 0 || srcPath == "")
                {
                    Alert.Show("该合同不存在相关文档");
                }
                else
                {
                    publicMethod.DeleteFile(attachId, srcPath);
                    int PactID = Convert.ToInt32(Request.QueryString["id"].ToString());
                    Common.Entities.Pact pact = BLLPact.FindByPactID(Convert.ToInt32(PactID));
                    pact.AttachmentID = null;
                    BLLPact.UpdateAttachment(PactID);
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