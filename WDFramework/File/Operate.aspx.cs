/**编写人：方淑云
 * 时间：2014年8月1号
 * 功能:文件下载界面后台
 * 修改履历：
 **/
using Common.Entities;
using FineUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WDFramework.File
{
    public partial class Operate : System.Web.UI.Page
    {
        BLHelper.BLLFiles file = new BLHelper.BLLFiles();
        BLHelper.BLLAgency agen = new BLHelper.BLLAgency();
        BLHelper.BLLAttachment at = new BLHelper.BLLAttachment();
        BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                InitData();
            }
        }
        //初始化
        public void InitData()
        {
            try
            {
                int id = Convert.ToInt32(Request.QueryString["id"].ToString());
                List<Files> list = file.FindAll(id);
                Files files = list.FirstOrDefault();
                filename.Text = files.FileName;
                //filesort.Text = file.SelectFileName(Convert.ToInt32(files.DocumentCategoryID));
                filesort.Text = files.DocumentCategoryID;
                agency.Text = agen.FindAgenName(files.AgencyID);
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }

        //下载
        protected void DownLoad_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(Request.QueryString["id"].ToString());
                int attachId = file.FindAttachmentID(id);
                string srcPath = at.FindPath(attachId);
                pm.DownloadFile(srcPath);
            }
            catch (Exception ex)
            {
                PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference() + Alert.GetShowInTopReference("下载出错，请联系管理员！"));
                pm.SaveError(ex, this.Request);
            }
        }
    }
}