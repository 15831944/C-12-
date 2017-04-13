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

namespace WDFramework.Achievement.Monograph
{
    public partial class OperateB : System.Web.UI.Page
    {
        BLHelper.BLLMonograph mo = new BLHelper.BLLMonograph();
        BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
        BLHelper.BLLAchievement ach = new BLHelper.BLLAchievement();
        BLHelper.BLLAttachment at = new BLHelper.BLLAttachment();
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
                Common.Entities.Monograph mon = mo.FindAll(id);
                name.Text = ach.FindAchieveName(Convert.ToInt32(mon.AchievementID));
                monograph.Text = mon.MonographName;
                //agency.Text = mon.MDepartment;
                Publisher.Text = mon.Publisher;
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
                int attachId = mo.FindBAttachmentID(id);
                string srcPath = at.FindPath(attachId);
                if (attachId == 0 || srcPath == "")
                {
                    Alert.Show("该专著不存在版权页资料");
                }
                else
                {
                    pm.DeleteFile(attachId, srcPath);
                    Common.Entities.Monograph mon = mo.FindAll(id);
                    mon.BAttachmentID = null;
                    mo.Update(mon);
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
                int attachId = mo.FindBAttachmentID(id);
                string srcPath = at.FindPath(attachId);
                if (attachId == 0 || srcPath == "")
                {
                    Alert.Show("该专著不存在版权页资料");
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