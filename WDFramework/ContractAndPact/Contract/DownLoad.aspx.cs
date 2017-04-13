using FineUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WDFramework.ContractAndPact
{
    public partial class DownLoad : System.Web.UI.Page
    {
        BLHelper.BLLContract BLLContract = new BLHelper.BLLContract();
        BLHelper.BLLAttachment BLLAttachment = new BLHelper.BLLAttachment();
        BLCommon.PublicMethod publicMethod = new BLCommon.PublicMethod();
        Common.Entities.Contract Contract = new Common.Entities.Contract();
        protected void Page_Load(object sender, EventArgs e)
        {
            BindData();
        }
        public void BindData()
        {
            try
            {
                int ContractID = Convert.ToInt32(Request.QueryString["id"].ToString());
                Contract = BLLContract.FindByContractID(Convert.ToInt32(ContractID));
                ContractHeadLine.Text = Contract.ContractHeadLine;
                ContractAuthors.Text = Contract.ContractAuthors;
                ContractOriginal.Text = Contract.ContractOriginal;
            }
            catch (Exception ex)
            {
                BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
                pm.SaveError(ex, this.Request);
            }
        }
        protected void btn_DownLoadContract_Click(object sender, EventArgs e)
        {
            try
            {
                int ContractID = Convert.ToInt32(Request.QueryString["id"].ToString());
                Contract = BLLContract.FindByContractID(Convert.ToInt32(ContractID));
                if (Contract.AttachmentID != null && Contract.AttachmentID != 0)
                {
                    int attachID = Convert.ToInt32(BLLContract.FindAttachmentID(ContractID));
                    string path = BLLAttachment.FindPath(attachID);
                    if (path != "")
                        publicMethod.DownloadFile(path);
                    else
                        Alert.ShowInTop("无附件可下载!");
                }
                else
                    Alert.ShowInTop("无附件下载!");
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
        //protected void Delete_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        int id = Convert.ToInt32(Request.QueryString["id"].ToString());
        //        int attachId = BLLContract.FindAttachmentID(id);
        //        string srcPath = BLLAttachment.FindPath(attachId);
        //        if (attachId == 0 || srcPath == "")
        //        {
        //            Alert.Show("该资料不存在相关文档");
        //        }
        //        else
        //        {
        //            publicMethod.DeleteFile(attachId, srcPath);
        //            int ContractID = Convert.ToInt32(Request.QueryString["id"].ToString());
        //            Common.Entities.Contract contract = BLLContract.FindByContractID(Convert.ToInt32(ContractID));
        //            contract.AttachmentID = null;
        //            BLLContract.UpdateAttachment(ContractID);
        //            //Alert.Show("删除成功！");
        //            PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideReference() + Alert.GetShowInTopReference("删除成功！"));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        publicMethod.SaveError(ex, this.Request);
        //    }
        //}
    }
}