﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WDFramework.Achievement.AchieveAward
{
    public partial class AchieveAwardMamber : System.Web.UI.Page
    {
        BLHelper.BLLAchieveAward achieve = new BLHelper.BLLAchieveAward();
        BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitData();
            }
        }
        public void InitData()
        {
            try
            {
                int id = Convert.ToInt32(Request.QueryString["id"].ToString());
                string str = achieve.FindMember(id);
                if (str != "" || str != null)
                {
                    Contents.Text = str;
                }
                else
                {
                    Contents.Text = " ";
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
    }
}