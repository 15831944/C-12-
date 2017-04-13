using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Web.Configuration;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace WDFramework
{
    public partial class ConnectionEncrypt : System.Web.UI.Page
    {
        string provider = "RSAProtectedConfigurationProvider";
        string section = "connectionStrings";
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        //加密
        protected void btnEncrypt_Click(object sender, EventArgs e)
        {
            Configuration confg = WebConfigurationManager.OpenWebConfiguration(Request.ApplicationPath);
            ConfigurationSection configSect = confg.GetSection(section);
            if (configSect != null)
            {
                configSect.SectionInformation.ProtectSection(provider);
                confg.Save();
            }
        }
        //解密
        protected void btnDecrypt_Click(object sender, EventArgs e)
        {
            Configuration config = WebConfigurationManager.OpenWebConfiguration(Request.ApplicationPath);
            ConfigurationSection configSect = config.GetSection(section);
            if (configSect.SectionInformation.IsProtected)
            {
                configSect.SectionInformation.UnprotectSection();
                config.Save();
            }
        }




        ////加密按钮  
        //protected void btnEncrypt_Click(object sender, EventArgs e)
        //{
        //    //①需要加密的节点：   
        //    string name = @"connectionStrings";
        //    //②当前路径；   
        //    string appPath = "/loginContral";
        //    Configuration config = WebConfigurationManager.OpenWebConfiguration(appPath);
        //    //③提供加密的方式：(这里使用自定义的加密方式)   
        //    // string provider = "RsaProtectConfigurationProvider";   
        //    string provider = "ConnectionStringsKeyProvider";
        //    config.GetSection(name).SectionInformation.ProtectSection(provider);

        //    //⑤保存web.config文件   
        //    try
        //    {
        //        config.Save();
        //    }
        //    catch (Exception ex)
        //    {
        //        Response.Write(ex.Message);
        //    }
        //    if (config.GetSection(name).SectionInformation.IsProtected)
        //    {
        //        Button1.Enabled = false;
        //        Response.Write("加密成功！");
        //    }
        //    else
        //    {
        //        Response.Write("加密失败！");
        //    }
        //}

        ////解密按钮：  

        //protected void btnDecrypt_Click(object sender, EventArgs e)
        //{
        //    //①需要节密的节点：   
        //    string name = @"connectionStrings";

        //    //②当前路径；   
        //    string appPath = "/loginContral";
        //    Configuration config = WebConfigurationManager.OpenWebConfiguration(appPath);

        //    //③使用UnprotectSection方法进行解密；        

        //    config.GetSection(name).SectionInformation.UnprotectSection();

        //    //④保存web.config文件        
        //    config.Save();

        //    if (config.GetSection(name).SectionInformation.IsProtected == false)
        //    {
        //        Button2.Enabled = false;
        //        Response.Write("解密成功！");
        //    }
        //    else
        //    {
        //        Response.Write("解密失败！");
        //    }
        //}  
    }
}