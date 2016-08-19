using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ALT.Framework.Data;
namespace loggalApi.Controllers
{

    public class BaseController : ApiController
    {
        protected readonly ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public BaseController() {
        }
    }
}
