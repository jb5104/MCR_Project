using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using Microsoft.Security.Application;


namespace MRC.Framework.Data
{
    #region >> 보안 관련 클래스
    /// <summary>
    /// 보안 관련 클래스
    /// </summary>

    public class SecurityInfo
    {
        public string getGetSafeHtml(string val)
        {
            return Sanitizer.GetSafeHtml(val);
        }
        /// <summary>
        /// SQL Injection
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string getSqlInjectIon(string str)
        {
            if (str == null) return string.Empty;
            str = str.Replace("'", "''");
            /*str = str.Replace(";", "");
            str = str.Replace("--", "");
            str = str.Replace("#", "");
            str = str.Replace("\\", "");
            str = str.Replace("&", "");
            str = str.Replace("<", "");
            str = str.Replace(">", "");
            str = str.Replace("(", "");
            str = str.Replace(")", "");
            str = str.Replace("=", "");
            str = str.Replace("+", "");*/
            return str;
        }
    }
   #endregion
}
