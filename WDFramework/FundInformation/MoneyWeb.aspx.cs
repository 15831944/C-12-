/**编写人：张凡凡
 * 时间：2014年8月13号
 * 功能：比例设置后台的相关操作
 * 修改履历：
 * 
 */
using FineUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WDFramework
{
    public partial class MoneyWeb : System.Web.UI.Page
    {
        BLHelper.BLLFundingSet blfundset = new BLHelper.BLLFundingSet();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitData();
            }
        }

        //初始化
        private void InitData()
        {
            try
            {
                tb_HorGProportion2.Text = blfundset.FindProportion("横向", "管理费").ToString();
                tb_VerGProportion2.Text = blfundset.FindProportion("纵向", "管理费").ToString();
                tb_school.Text = blfundset.FindProportion("校内", "管理费").ToString();
            }
            catch (Exception ex)
            {
                BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
                pm.SaveError(ex, this.Request);
                return;
            }
        }

        //重置
        public void Delete_Onclick(object sender, EventArgs e)
        {
            tb_HorGProportion2.Reset();
            tb_VerGProportion2.Reset();
            tb_school.Reset();
        }

        protected void btn_Save_Click(object sender, EventArgs e)
        {
            blfundset.Update("横向", "管理费", tb_HorGProportion2.Text.Trim());
            blfundset.Update("纵向", "管理费", tb_VerGProportion2.Text.Trim());
            blfundset.Update("校内", "管理费", tb_school.Text.Trim());
            PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("保存成功！"));
        }
    }
}