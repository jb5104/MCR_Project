using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace MRC.Framework.Mvc.Helpers
{
    public static class SemanticUIHelper
    {
        /// <summary>
        /// Html Attribute string로 값 가져오기
        /// </summary>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static string GetHtmlAttributeString(object htmlAttributes)
        {
            StringBuilder sbHtml = new StringBuilder();
            RouteValueDictionary customAttributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);

            foreach (KeyValuePair<string, object> customAttribute in customAttributes)
            {
                sbHtml.Append(" ").Append(customAttribute.Key.ToString()).Append("='").Append(customAttribute.Value.ToString()).Append("'");
            }

            return sbHtml.ToString();
        }

        /// <summary>
        /// Semintic-UI Size 정의
        /// </summary>
        public enum Size
        {
            mini, tity, small, medium, large, big, huge, massive
        }

        /// <summary>
        /// 제목
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="TITLE"></param>
        /// <returns></returns>
        public static MvcHtmlString Semantic_TITLE(this HtmlHelper helper, string TITLE)
        {
            StringBuilder sbHtml = new StringBuilder();
            sbHtml.Append("<h1 class='ui header'>").Append(TITLE).Append("</h1> ");
            return new MvcHtmlString(sbHtml.ToString().Trim());
        }


        /// <summary>
        /// 제목(Content)
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="TITLE"></param>
        /// <returns></returns>
        public static MvcHtmlString Semantic_TITLE2(this HtmlHelper helper, string TITLE, bool bRequired = false)
        {
            StringBuilder sbHtml = new StringBuilder();

            sbHtml.Append("     <h4 class='ui header'>  ").Append("\n");
            ///필수일 경우 * 표시
            if (bRequired) sbHtml.Append("         <span style = 'color:red;'> * </span > ");
            sbHtml.Append(TITLE).Append("\n");
            sbHtml.Append("     </h4>  ").Append("\n");


            return new MvcHtmlString(sbHtml.ToString().Trim());
        }


        /// <summary>
        /// Semintic UI TextBox 정의
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <param name="size"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static MvcHtmlString Semantic_TextBox(this HtmlHelper helper, string id, string value = "", Size size = Size.mini, object htmlAttributes = null)
        {
            StringBuilder sbHtml = new StringBuilder();

            sbHtml.Append("<div class='ui mini input field'> ");
            sbHtml.Append("<input type='text' id='").Append(id).Append("' name='").Append(id).Append("' value='").Append(value).Append("'").Append(SemanticUIHelper.GetHtmlAttributeString(htmlAttributes));
            sbHtml.Append("> ");
            sbHtml.Append("</div>                      ");
            return new MvcHtmlString(sbHtml.ToString().Trim());
        }

        /// <summary>
        /// Semantic-UI YesOrNo 버튼정의
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="id"></param>
        /// <param name="list"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static MvcHtmlString Semantic_YesOrNo(this HtmlHelper helper, string id, List<SelectListItem> list, Size size = Size.mini)
        {
            StringBuilder sbHtml = new StringBuilder();

            if (list.Count() == 2)
            {
                string selectValue = list.Where(w => w.Selected).FirstOrDefault() == null ? list[0].Value : list.Where(w => w.Selected).FirstOrDefault().Value;
                sbHtml.Append("<div id='dv_").Append(id).Append("' class='ui ").Append(size.ToString()).Append(" buttons'>").Append("\n");
                sbHtml.Append("    <button type='button' id='btn_").Append(id).Append("_").Append(list[0].Value).Append("' class='ui ").Append((list[0].Value == selectValue) ? "active" : "").Append(" button' onclick='dv_").Append(id).Append("_Click(\"").Append(list[0].Value).Append("\");'> ").Append(list[0].Text).Append("</button>").Append("\n");
                sbHtml.Append("    <div class='or'></div>                              ").Append("\n");
                sbHtml.Append("    <button type='button' id='btn_").Append(id).Append("_").Append(list[1].Value).Append("' class='ui ").Append((list[1].Value == selectValue) ? "active" : "").Append(" button' onclick='dv_").Append(id).Append("_Click(\"").Append(list[1].Value).Append("\");'>").Append(list[1].Text).Append("</button>").Append("\n");
                sbHtml.Append("    <input id='").Append(id).Append("' type='hidden' value='").Append(selectValue).Append("'>").Append("\n");
                sbHtml.Append("</div>                                                  ").Append("\n");
                sbHtml.Append("<script type='text/javascript'>  ").Append("\n");
                sbHtml.Append(" function dv_").Append(id).Append("_Click(val){  ").Append("\n");
                sbHtml.Append("   $('#").Append(id).Append("').val(val);  ").Append("\n");
                sbHtml.Append("   $('#dv_").Append(id).Append(" button').removeClass('active');  ").Append("\n");
                sbHtml.Append("   $('#btn_").Append(id).Append("_' + val).addClass('active');  ").Append("\n");
                sbHtml.Append(" }  ").Append("\n");
                sbHtml.Append("</script>                        ").Append("\n");
            }
            return new MvcHtmlString(sbHtml.ToString().Trim());
        }

        public static MvcHtmlString Semantic_CheckBox(this HtmlHelper helper, string id, string label, string val = null, Size size = Size.mini)
        {
            StringBuilder sbHtml = new StringBuilder();

            sbHtml.Append(" <div class='ui checkbox'>                    ").Append("\n");
            sbHtml.Append("   <input type='checkbox' id='").Append(id).Append("' name ='").Append(id).Append("' value='").Append((val == null ? "1" : val)).Append("'>   ").Append("\n");
            sbHtml.Append("   <label>").Append(label).Append("</label>     ").Append("\n");
            sbHtml.Append(" </div>                                       ").Append("\n");

            return new MvcHtmlString(sbHtml.ToString().Trim());

        }


        #region >> 버튼정의
        public enum enColor
        {
            red, orange, yellow, olive, green, teal, blue, violet, purple, pink, brown, grey, black
        }

        public enum enButtonType
        {
            Normal, New, Save, Cancel, Del, Excel, ToList
        }
        public static MvcHtmlString Semantic_Button(this HtmlHelper helper, string id, string text, enButtonType btnType= enButtonType.Normal, Size size = Size.mini, object htmlAttributes = null)
        {
            StringBuilder sbHtml = new StringBuilder();

            sbHtml.Append(" <button class='ui ");
            switch (btnType)
            {
                case enButtonType.Del:
                    sbHtml.Append(" red ");
                    break;
                case enButtonType.Cancel:
                    sbHtml.Append(" orange ");
                    break;
                case enButtonType.New:
                    sbHtml.Append(" teal ");
                    break;
                case enButtonType.Save:
                    sbHtml.Append(" blue ");
                    break;
                case enButtonType.Excel:
                    sbHtml.Append(" green ");
                    break;
                case enButtonType.ToList:
                    sbHtml.Append(" olive ");
                    break;
                default:
                    break;
            }
            sbHtml.Append(size.ToString()).Append(" button' id='").Append(id).Append("' name='").Append(id).Append("'").Append(SemanticUIHelper.GetHtmlAttributeString(htmlAttributes)).Append("> ").Append("\n");
            sbHtml.Append(text).Append("\n");
            sbHtml.Append(" </button>").Append("\n");
            return new MvcHtmlString(sbHtml.ToString().Trim());
        }
        #endregion
    }
}
