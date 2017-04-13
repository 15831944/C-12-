/**编写人：方淑云
 * 时间：2014年8月24号
 * 功能:下载
 * 修改履历：
 **/
using FineUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WDFramework.Achievement.Patent
{
    public partial class Operate_Application : System.Web.UI.Page
    {
        BLHelper.BLLPatent pa = new BLHelper.BLLPatent();
        BLHelper.BLLAttachment at = new BLHelper.BLLAttachment();
        BLHelper.BLLAchievement ach = new BLHelper.BLLAchievement();
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
                List<Common.Entities.Patent> list = pa.FindAll(id);
                Common.Entities.Patent pat = list.FirstOrDefault();
                name.Text = pat.PatentNumber;
                patent.Text = pat.PatentName;
                agency.Text = pat.GivenUnit;
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }

        //删除
        protected void Delete_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(Request.QueryString["id"].ToString());
                int[] attachId = pa.FindAttachmentID(id);
                string srcPath = at.FindPath(attachId[1]);
                if (attachId[1] == 0 || srcPath == "")
                {
                    Alert.Show("该专利不存在相关文档");
                }
                else
                {
                    pm.DeleteFile(attachId[1], srcPath);
                    Common.Entities.Patent pat = pa.FindAll(id).FirstOrDefault();
                    pat.Attachment_Patent = null;
                    pa.Update(pat);
                    //Alert.Show("删除成功！");
                    PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideReference() + Alert.GetShowInTopReference("删除成功！"));
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
        //下载
        protected void DownFile_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(Request.QueryString["id"].ToString());
                int[] attachId = pa.FindAttachmentID(id);
                string srcPath = at.FindPath(attachId[1]);
                if (attachId[1] == 0 || srcPath == "")
                {
                    Alert.Show("该专利不存在相关文档");
                }
                else
                {
                    pm.DownloadFile(srcPath);
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
    }
}