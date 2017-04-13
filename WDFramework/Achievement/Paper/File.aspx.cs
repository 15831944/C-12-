/**编写人：吕博杨
 * 时间：2015年11月30日
 * 功能: 下载界面
 * 修改履历：    暂无
 **/
using FineUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WDFramework.Achievement.Paper
{
    public partial class File : System.Web.UI.Page
    {
        BLHelper.BLLPaper BllPaper = new BLHelper.BLLPaper();
        BLHelper.BLLAttachment BllAttachment = new BLHelper.BLLAttachment();
        BLHelper.BLLAchievement BllAchievement = new BLHelper.BLLAchievement();
        BLCommon.PublicMethod publicMethod = new BLCommon.PublicMethod();
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
                Common.Entities.Paper paper = BllPaper.FindAll(id);
                title.Text = paper.Subject;
                firstWriter.Text = paper.FirstWriter;
                writerIdentity.Text = paper.WriterIdentity;
            }
            catch (Exception ex)
            {
                publicMethod.SaveError(ex, this.Request);
            }
        }

        //删除
        protected void Delete_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(Request.QueryString["id"].ToString());
                int attachId = BllPaper.FindAttachment(id);
                string srcPath = BllAttachment.FindPath(attachId);
                if (attachId == 0 || srcPath == "")
                {
                    Alert.Show("不存在相关文档");
                }
                else
                {
                    publicMethod.DeleteFile(attachId, srcPath);
                    Common.Entities.Paper paper = BllPaper.FindAll(id);
                    paper.AttachmentID = null;
                    BllPaper.Update(paper);
                    //Alert.Show("删除成功！");
                    PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideReference() + Alert.GetShowInTopReference("删除成功！"));
                }
            }
            catch (Exception ex)
            {
                publicMethod.SaveError(ex, this.Request);
            }
        }
        //下载
        protected void DownFile_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(Request.QueryString["id"].ToString());
                int attachId = BllPaper.FindAttachment(id);
                string srcPath = BllAttachment.FindPath(attachId);
                if (attachId == 0 || srcPath == "")
                {
                    Alert.Show("不存在相关文档");
                }
                else
                {
                    publicMethod.DownloadFile(srcPath);
                }
            }
            catch (Exception ex)
            {
                publicMethod.SaveError(ex, this.Request);
            }
        }
    }
}