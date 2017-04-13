/**编写人：方淑云
 * 时间：2014年8月26号
 * 功能：加密类
 * 修改履历：
 **/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace BLCommon
{
    public class Encrypt
    {
        public string MD5(string strpwd)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] md5data = md5.ComputeHash(System.Text.Encoding.Default.GetBytes(strpwd));//计算将strpwd转化为字节数组后的hash值
            md5.Clear();
            string str = "";
            for (int i = 0; i < md5data.Length; i++)
            {
                str +=md5data[i].ToString("X").PadLeft(2, '0');
            }           
            return str;
        }
    }
}
