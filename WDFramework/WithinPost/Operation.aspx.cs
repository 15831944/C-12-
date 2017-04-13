/**编写人：方淑云
 * 时间：2014年11月14号
 * 功能:下载界面后台
 * 修改履历：
 **/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common.Entities;
using FineUI;

namespace WDFramework.WithinPost
{
    public partial class Operation : System.Web.UI.Page
    {
        BLHelper.BLLWithinPost wh = new BLHelper.BLLWithinPost();
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
                List<Common.Entities.WithinPost> list = wh.FindAll(id);
                Common.Entities.WithinPost files = list.FirstOrDefault();
                filename.Text = files.FileName;
                filesort.Text = files.FileType;
                agency.Text = files.AndUnit;
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
                int attachId = wh.FindAttachmentID(id);
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