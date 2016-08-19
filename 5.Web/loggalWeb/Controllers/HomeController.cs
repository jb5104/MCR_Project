using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using loggalServiceBiz;
using ALT.VO.loggal;
namespace loggalWeb.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult getKeywordAutoCompleate(KEYWORD_COND Cond)
        {
            return new JsonResult { Data = new KeywordService().GetKeywordKoreanList(Cond) };
        }

        public PartialViewResult PV_KeywordAutoList(KEYWORD_COND Cond)
        {
            ViewBag.Cond = Cond;
           ViewBag.list= new KeywordService().GetKeywordKoreanList(Cond);
            return PartialView();
        }


        public PartialViewResult PV_LocalNameList(CODE_DATA Cond)
        {
            ViewBag.Cond = Cond;
            ViewBag.list = new KeywordService().GetLocalNameList(Cond);
            return PartialView();
        }
    }
}