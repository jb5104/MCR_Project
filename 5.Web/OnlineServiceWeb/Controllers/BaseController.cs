using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ALT.Framework.Mvc.Contoller;
using ALT.Framework.Mvc.Helpers;
using OnlineServiceBiz;
using ALT.VO.Common;

namespace OnlineServiceWeb.Controllers
{
    public class BaseController : MVCBaseController
    { /**
        * 이벤트 순서
        * 1) Initialize
        * 2) OnAuthorization
        * 3) OnActionExecuting
        * 4) 실제 Contoller
        * 5) OnActionExecuted
        */
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            
            //GlobalMvc.Util.BrowerCheckRedirect("http://m.nave.com/", "http://naver.com");
            base.Initialize(requestContext);

            string language = this.RouteData.Values["lang"].ToString();
            string compID = this.RouteData.Values["comp"].ToString();
            string controllerName = this.RouteData.Values["controller"].ToString().ToLower();
            string actionName = this.RouteData.Values["action"].ToString().ToLower();

            SessionHelper.LoginInfo.COMPANY_ID = compID;
            SessionHelper.LoginInfo.LANGUAGE = language;
            int? storeCode;
            if (Request["storeCode"] != null)
            {
                storeCode = Convert.ToInt32(Request["storeCode"]);
            }
            else if (controllerName == "home" && actionName == "index" && (Request["id"] != null || SessionHelper.LoginInfo.WebMemu.Count == 0))
            {
                storeCode = (int?)Convert.ToInt32(Request["id"] == null ? "1" : Request["id"]);
                SessionHelper.LoginInfo.WebMemu = new HomePageService().GetStoreWebMenuList(new T_STORE_WEBMENU { STORE_CODE = (int)storeCode, HIDE = false });
            }
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);

            //if (filterContext.ExceptionHandled)
            //{
            //    return;
            //}
            //filterContext.Result = new ViewResult
            //{

            //    ViewName = "~/Views/Shared/Error.htm"
            //};
            //filterContext.ExceptionHandled = true;

        }

        public ActionResult ReturnView()
        {
            string controllerName = this.RouteData.Values["controller"].ToString().ToLower();
            string actionName = this.RouteData.Values["action"].ToString().ToLower();
            string rtnPage = "~/Views/Theme/" + SessionHelper.LoginInfo.STORE.THEME_NAME + "/" +  controllerName + "/" + actionName + ".cshtml";
            return View(rtnPage);
        }

        public PartialViewResult ReturnPatianView()
        {
            string controllerName = this.RouteData.Values["controller"].ToString().ToLower();
            string actionName = this.RouteData.Values["action"].ToString().ToLower();
            string rtnPage = "~/Views/Theme/PatialView/" + SessionHelper.LoginInfo.STORE.THEME_NAME + "/" + controllerName + "/" + actionName + ".cshtml";
            return PartialView(rtnPage);
        }

    }
}