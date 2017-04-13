using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WDFramework.ContractAndPact
{
    public partial class NoLibraryMessage : System.Web.UI.Page
    {
        Common.Entities.Contract Contract = new Common.Entities.Contract();
        protected void Page_Load(object sender, EventArgs e)
        {         
            //Content.Text = "不可借阅，请选择下载！";
            string sort = Request.QueryString["sort"].ToString();
            if(sort=="下载")
                Content.Text = "不可下载,请选择借阅！";
            else
                Content.Text = "不可借阅，请选择下载！";
        }
     
    }
}