using FineUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WDFramework.UnitInspect
{
    public partial class Operate : System.Web.UI.Page
    {
        BLHelper.BLLUnitInspect un = new BLHelper.BLLUnitInspect();
        BLHelper.BLLAttachment at = new BLHelper.BLLAttachment();
        BLHelper.BLLAgency agen = new BLHelper.BLLAgency();
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
                Common.Entities.UnitInspect uni = un.FindInspectInfo(id, true);
                name.Text = uni.InspectName;
                unit.Text = uni.WorkPlace;
                agency.Text = agen.FindAgenName(uni.AgencyID);
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
                int attachId = un.FindAttachmentID(id);
                string srcPath = at.FindPath(attachId);
                if (attachId == 0 || srcPath == "")
                {
                    Alert.Show("该考察信息不存在相关文档");
                }
                else
                {
                    pm.DeleteFile(attachId, srcPath);
                    Common.Entities.UnitInspect uni = un.FindInspectInfo(id, true);
                    uni.AccessoryID = null;
                    un.Update(uni);
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
                int attachId = un.FindAttachmentID(id);
                string srcPath = at.FindPath(attachId);
                if (attachId == 0 || srcPath == "")
                {
                    Alert.Show("该考察信息不存在相关文档");
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