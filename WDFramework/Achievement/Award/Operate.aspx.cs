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

namespace WDFramework.Achievement.Award
{
    public partial class Operate : System.Web.UI.Page
    {
        BLHelper.BLLAward aw = new BLHelper.BLLAward();
        BLHelper.BLLAttachment at = new BLHelper.BLLAttachment();
        BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
        BLHelper.BLLAchievement ach = new BLHelper.BLLAchievement();
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
                List<Common.Entities.Award> list = aw.FindAll(id);
                Common.Entities.Award awa = list.FirstOrDefault();
                name.Text = awa.Acheivement;
                award.Text = awa.AwardName;
                form.Text = awa.AwardwSpecies;
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
                int attachId = aw.FindAttachmentID(id);
                string srcPath = at.FindPath(attachId);
                if (attachId == 0 || srcPath == "")
                {
                    Alert.Show("该成果获奖不存在相关文档");
                }
                else
                {
                    pm.DeleteFile(attachId, srcPath);
                    Common.Entities.Award caa = aw.FindAll(id).FirstOrDefault();
                    caa.AttachmentID = null;
                    aw.Update(caa);
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
                int attachId = aw.FindAttachmentID(id);
                string srcPath = at.FindPath(attachId);
                if (attachId == 0 || srcPath == "")
                {
                    Alert.Show("该成果获奖不存在相关文档");
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