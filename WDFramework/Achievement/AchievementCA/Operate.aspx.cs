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

namespace WDFramework.Achievement.AchievementCA
{
    public partial class Operate : System.Web.UI.Page
    {
        BLHelper.BLLAttachment at = new BLHelper.BLLAttachment();
        BLHelper.BLLAchievement ach = new BLHelper.BLLAchievement();
        BLHelper.BLLAchievementCA ca = new BLHelper.BLLAchievementCA();
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
            int id = Convert.ToInt32(Request.QueryString["id"].ToString());
            Common.Entities.AchievementCA caa = ca.FindAll(id);
            name.Text = ach.FindAchieveName(Convert.ToInt32(caa.AchievementID));
            unit.Text = caa.CAUnit;
            level.Text = caa.CACommnetLevel;
        }

        //删除
        protected void Delete_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(Request.QueryString["id"].ToString());
                int attachId = ca.FindAttachmentID(id);
                string srcPath = at.FindPath(attachId);
                if (attachId == 0 || srcPath == "")
                {
                    Alert.Show("该成果验收不存在相关文档");
                }
                else
                {
                    pm.DeleteFile(attachId, srcPath);
                    Common.Entities.AchievementCA caa = ca.FindAll(id);
                    caa.AttachmentID = null;
                    ca.Update(caa);
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
                int attachId = ca.FindAttachmentID(id);
                string srcPath = at.FindPath(attachId);
                if (attachId == 0 || srcPath == "")
                {
                    Alert.Show("该成果验收不存在相关文档");
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