using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MRC.Framework
{
    public partial class ConfigInfo
    {
        public string DefaultDBSource
        {
            
            get { return System.Configuration.ConfigurationSettings.AppSettings.Get("DefaultDBSource"); }
        }
        public string SqlXmlPath
        {
            get { return System.Configuration.ConfigurationSettings.AppSettings["SqlXmlPath"]; }
        }

        public string FtpUser
        {
            get { return System.Configuration.ConfigurationSettings.AppSettings["FtpUser"]; }
        }

        public string FtpPw
        {
            get { return System.Configuration.ConfigurationSettings.AppSettings["FtpPw"]; }
        }

        public string FtpDomain
        {
            get { return System.Configuration.ConfigurationSettings.AppSettings["FtpDomain"]; }
        }

        public string FileUser
        {
            get { return System.Configuration.ConfigurationSettings.AppSettings["FileUser"]; }
        }
        public string LoginSessionName
        {
            get { return System.Configuration.ConfigurationSettings.AppSettings["LoginSessionName"]; }
        }
        public string DefaultCultureName
        {
            get { return System.Configuration.ConfigurationSettings.AppSettings["DefaultCultureName"]; }
        }

        public string KakaoAppKey
        {
            get { return System.Configuration.ConfigurationSettings.AppSettings["KakaoAppKey"]; }
        }

        public string FacebookAppKey { get { return System.Configuration.ConfigurationSettings.AppSettings["FacebookAppKey"]; } }
        //public string RoundCalc
        //{
        //    get { return System.Configuration.ConfigurationSettings.AppSettings["RoundCalc"]; }
        //}

        public decimal dRoundCal
        {
            get { return (int)Math.Pow(10, Convert.ToDouble(System.Configuration.ConfigurationSettings.AppSettings["RoundCalc"])); }
        }


        public string FaceBookAppID_Test
        {
            get { return System.Configuration.ConfigurationSettings.AppSettings["FaceBookAppID_Test"]; }
        }
        public string FaceBookAppID_Live
        {
            get { return System.Configuration.ConfigurationSettings.AppSettings["FaceBookAppID_Live"]; }
        }
        public string FaceBookAppID { get; set; }
        
    }
}
