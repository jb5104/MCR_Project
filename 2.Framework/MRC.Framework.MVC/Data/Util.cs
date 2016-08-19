using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

using System.Web.Mvc;
using System.Text.RegularExpressions;

using System.Web.Security;
using Newtonsoft.Json;

namespace MRC.Framework.Mvc.Data
{
    public class Util
    {
        #region >> 쿠키 저장 및 파기
        public void SetCookie(string cookieName, string cookieValue, bool bAuto = false)
        {
            string msg = string.Empty;
            try
            {
                HttpCookie cookie = new HttpCookie(cookieName);

                if (bAuto)
                    cookie.Expires = DateTime.Now.AddYears(1);
                else
                    cookie.Expires = DateTime.Now.AddHours(20);

                cookie.Value = HttpUtility.UrlEncode(cookieValue);

                HttpContext.Current.Response.Cookies.Add(cookie);
            }
            catch (Exception ex) { msg = ex.Message; }
        }
        public string getCookie(string cookieName)
        {
            try
            {
                if (HttpContext.Current.Request.Cookies[cookieName] == null)
                    return string.Empty;
                return HttpUtility.UrlDecode(HttpContext.Current.Request.Cookies[cookieName].Value);
            }
            catch (Exception ex)
            {
                return string.Empty;
            }

        }

        public T getLoginInfo<T>(ControllerContext controller, string key)
        {
            return JsonConvert.DeserializeObject<T>(this.getCookie(key));
        }

        public void RemoveCookie(string CookieName)
        {
            if (HttpContext.Current.Request.Cookies.AllKeys.Contains(CookieName))
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies[CookieName];
                cookie.Expires = DateTime.Now.AddDays(-1);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }
        #endregion

        #region >> 단방향 암호화
        public string Encrypt_SHA(string sValue)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(sValue, "SHA1");
        }

        public string Encrypt_MD5(string sValue)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(sValue, "MD5");
        }

        public string Encrypt(string sValue, string type)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(sValue, type);
        }
        #endregion


        public  string Base64Encoding(string EncodingText, System.Text.Encoding oEncoding = null)
        {
            if (oEncoding == null)
                oEncoding = System.Text.Encoding.UTF8;

            byte[] arr = oEncoding.GetBytes(EncodingText);
            return System.Convert.ToBase64String(arr);
        }

        public  string Base64Decoding(string DecodingText, System.Text.Encoding oEncoding = null)
        {
            if (oEncoding == null)
                oEncoding = System.Text.Encoding.UTF8;

            byte[] arr = System.Convert.FromBase64String(DecodingText);
            return oEncoding.GetString(arr);
        }

        #region >> 브라우저체크 후 리다이렉트
        /// <summary>
        /// 브라우저체크 후 리다이렉트
        /// </summary>
        /// <param name="redirectUtl">리다이렉트 경로</param>
        /// <param name="ChkUrl">체크 URL</param>
        public void BrowerCheckRedirect( string redirectUtl, params string[] ChkUrl)
        {
            
            Regex regex = new Regex(@"iPhone|iPod|iPad|Android|Windows CE|BlackBerry|Symbian|Windows Phone|webOS|Opera Mini|Opera Mobi|POLARIS|IEMobile|lgtelecom|nokia|SonyEricsson|LG|SAMSUNG", RegexOptions.IgnoreCase);

            if (!regex.IsMatch(HttpContext.Current.Request.UserAgent))
            {
                foreach ( string sUrl in ChkUrl)
                {
                    if (HttpContext.Current.Request.Url.AbsoluteUri == sUrl)
                    {
                        HttpContext.Current.Response.Redirect(redirectUtl, true);
                    }
                }
            }
        }
        #endregion
    }
}

