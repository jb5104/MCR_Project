using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MRC.VO.Common;
using MRC.BizService;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace MRC.Management.CommonCS
{
    public static class SemanticUI_Helper2
    {
        public static MvcHtmlString CommonCombo(this HtmlHelper helper, string id, T_COMMON Param, string selectedValue = null, string optionLabel = "-선택-", object htmlAttributes = null)
        {
            Param.HIDE = (Param.HIDE == null) ? false : Param.HIDE;
            IList<T_COMMON> list = new BaseService().GetCommon(Param);

            int nRow = 0;
            if(htmlAttributes == null)
            {
                htmlAttributes = new { @class = "mini" };
            }
            IList<SelectListItem> combo = new List<SelectListItem>();
            
            foreach (T_COMMON data1 in list)
            {
                combo.Add(new SelectListItem()
                {
                    Value = data1.SUB_CODE.ToString(),
                    Text = data1.NAME,
                    Selected = (nRow == 0 && string.IsNullOrEmpty(selectedValue)) ? false : ((selectedValue == data1.SUB_CODE.ToString()) ? true : false)
                });
                nRow++;
            }

            string shtml = helper.DropDownList(id, combo, optionLabel, htmlAttributes).ToHtmlString();
            //shtml += "<script >$('document').ready(function () { try{ $('#" + id + "').prev().html($('#" + id + "  option:selected').text()); } catch(e){} }); </script>";

            return new MvcHtmlString(shtml);
        }
}
}