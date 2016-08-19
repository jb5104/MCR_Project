using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using MRC.VO.Common;

using MRC.Framework;
using MRC.Framework.Data;
using System.Globalization;


namespace MRC.Framework.Mvc.Helpers
{
    /// <summary>
    /// 세션 클래스
    /// </summary>
    public static class SessionHelper
    {
        public static string SessionName = Global.ConfigInfo.LoginSessionName;
        #region >> 쿠키 기준
        /*public static LOGIN_INFO LoginInfo
        {
            get
            {

                LOGIN_INFO rtnData;

                if (HttpContext.Current.Session[SessionName + "_LOGININFO"] != null)
                {

                    rtnData = (LOGIN_INFO)HttpContext.Current.Session[SessionName + "_LOGININFO"];
                    if (rtnData.LOGIN_ID != null)
                    {
                        GlobalMvc.Util.SetCookie(SessionName + "_LOGININFO", JsonConvert.SerializeObject(rtnData));
                        return rtnData;
                    }
                }
                rtnData = JsonConvert.DeserializeObject<LOGIN_INFO>(GlobalMvc.Util.getCookie(SessionName + "_LOGININFO"));

                if (rtnData == null && HttpContext.Current.Session[SessionName + "_LOGININFO"] != null)
                {
                    rtnData = (LOGIN_INFO)HttpContext.Current.Session[SessionName + "_LOGININFO"];
                }
                else if (rtnData != null)
                {
                    HttpContext.Current.Session[SessionName + "_LOGININFO"] = rtnData;
                }

                if (rtnData == null) rtnData = new LOGIN_INFO { LANGUAGE = "en" };

                return rtnData;
            }
            set
            {
                {
                    HttpContext.Current.Session[SessionName + "_LOGININFO"] = value;
                    GlobalMvc.Util.SetCookie(SessionName + "_LOGININFO", JsonConvert.SerializeObject((LOGIN_INFO)HttpContext.Current.Session[SessionName + "_LOGININFO"]));
                }
            }
        }*/
        #endregion

        #region >> 세션 기준
        /// <summary>
        /// 로그인 관련 세션
        /// </summary>
        public static LOGIN_INFO LoginInfo
        {
            get
            {
                LOGIN_INFO rtnData;

                if (HttpContext.Current.Session[SessionName + "_LOGININFO"] != null)
                {
                    rtnData = (LOGIN_INFO)HttpContext.Current.Session[SessionName + "_LOGININFO"];
                }
                else
                    rtnData = new LOGIN_INFO { LANGUAGE = "en" };
                HttpContext.Current.Session[SessionName + "_LOGININFO"] = rtnData;
                return rtnData;
            }
            set
            {
                {
                    HttpContext.Current.Session[SessionName + "_LOGININFO"] = value;
                    LOGIN_INFO rtnData = (LOGIN_INFO)HttpContext.Current.Session[SessionName + "_LOGININFO"];

                }
            }
        }




        #endregion

        /// <summary>
        /// decimal 형을 String 포맷으로 변경
        /// </summary>
        /// <param name="val"></param>
        /// <param name="format">디폴트는 통화형</param>
        /// <returns></returns>
        public static string ToFormatAmt(this decimal val, string format = null)
        {

            format = "n" + SessionHelper.LoginInfo.CultureInfo.NumberFormat.CurrencyDecimalDigits;
            return val.ToString(format) ;
        }
       
    }
}
