using System;
using System.IO;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Async;
using System.Web.Profile;
using System.Web.Routing;

using Newtonsoft.Json;
using log4net;
using log4net.Config;

using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Net;
using MRC.Framework.Mvc.Helpers;
using MRC.VO.Common;



namespace MRC.Framework.Mvc.Contoller
{

  
    public class MVCBaseController : System.Web.Mvc.Controller
    {
        /**
        * 이벤트 순서
        * 1) Initialize
        * 2) OnAuthorization
        * 3) OnActionExecuting
        * 4) 실제 Contoller
        * 5) OnActionExecuted
        */
       
        protected readonly ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static bool IgnoreCertificateErrorHandler(object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            var certificate = (X509Certificate2)cert;
            
            return true;
        }

        public MVCBaseController()
        {
            ServicePointManager.ServerCertificateValidationCallback += IgnoreCertificateErrorHandler;
            //ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(IgnoreCertificateErrorHandler); 
        }
  


    }
}
