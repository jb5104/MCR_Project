using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ALT.VO.loggal;
using ALT.Framework;
using ALT.Framework.Data;
namespace loggalServiceBiz
{
    public class CategoryService : BaseService
    {
        public CategoryService() { }

        /// <summary>
        /// 광고카테고리리스트가져오기
        /// </summary>
        /// <param name="Cond"></param>
        /// <returns></returns>
        public IList<CATEGORY_LIST> GetCategoryList(CATEGORY_COND Cond)
        {
            string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\Category.xml", "GetCategoryList"
                                         , Cond.CATEGORY_CODE.ToString("")
                                         , Cond.CATEGORY_TYPE.ToString("")
                                         , Cond.PARENT_CATEGORY_CODE.ToString("")
                                         , Cond.LEVEL_DEPTH.ToString("")
                                         , Cond.SEARCH_CATEGORY_CODE.ToString("")
                                         , Cond.CATEGORY_NAME.ToString("")
                                         , (Cond.HIDE == null || Cond.HIDE == false) ? "0" : "1"
             );


            return db.ExecuteQuery<CATEGORY_LIST>(sql).ToList();
        }


        /// <summary>
        /// 광고카테고리리스트가져오기
        /// </summary>
        /// <param name="Cond"></param>
        /// <returns></returns>
        public IList<AD_LIST> GetAdList(AD_SEARCH_COND Cond)
        {
            //Cond = new AD_SEARCH_COND
            //{
            //    AD_CODE = 1,
            //    HIDE = false,
            //    KEYWORD_NAME = "삼",
            //    LATITUDE = 0,
            //    LONGITUDE = 0,
            //    Page = 1,
            //    PageCount = 10,
            //    RADIUS = 100000
            //    ,CK_CODE = 6092
            //    /* SEARCH_CATEGORY_CODE = new string[] { "1|2|3", "1|2|3" }
            //     ,
            //     SEARCH_KEYWORD_CODE = new string[] { "33", "44" }*/
            //};

            StringBuilder sbSearchCategory = new StringBuilder();
            if (Cond.SEARCH_CATEGORY_CODE != null && Cond.SEARCH_CATEGORY_CODE.Count() > 0)
            {
                sbSearchCategory.Append("AND (");
                for (int i = 0; i < Cond.SEARCH_CATEGORY_CODE.Count(); i++)
                {
                    if (i > 0) sbSearchCategory.Append(" OR ");
                    sbSearchCategory.Append("G.SEARCH_CATEGORY_CODE LIKE '" + Cond.SEARCH_CATEGORY_CODE[i] + "%'");
                }
                sbSearchCategory.Append(")");
            }
            string searchKeyCode = string.Empty;
            if (Cond.SEARCH_KEYWORD_CODE != null)
            {
                for (int i = 0; i < Cond.SEARCH_KEYWORD_CODE.Count(); i++)
                {
                    if (i > 0) searchKeyCode += ",";
                    searchKeyCode += Cond.SEARCH_KEYWORD_CODE[i];
                }
            }
            ////// 위치에 따른 반경의 광고 데이터 보여 주기
            //if (Cond.LATITUDE != null && Cond.LONGITUDE != null && Cond.RADIUS !=null)
            //{
            //    sbSqlCond.Append("\n").Append("AND dbo.FN_TO_DISTANCE(").Append(Cond.LATITUDE).Append(",").Append(Cond.LONGITUDE).Append(", C.LATITUDE, C.LONGTITUDE, 'KM') <= ").Append(Cond.RADIUS).Append(")");
            //}



            string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\Ad.xml", "GetAdList"
                                         , Cond.PageCount.ToString("20")
                                         , Cond.Page.ToString("1")

                                         , (Cond.Page.ToString("1").ConvertInt() * Cond.PageCount.ToString("20").ConvertInt()).ToString()
                                         , Cond.LATITUDE.ToString("0")
                                         , Cond.LONGITUDE.ToString("0")
                                         , Cond.RADIUS.ToString("1000")
                                         , Cond.AD_CODE.ToString("")
                                         , sbSearchCategory.ToString()
                                         , searchKeyCode
                                         , Cond.KEYWORD_NAME.ToString("")
                                         , Cond.CK_CODE.ToString("")
                                         , (Cond.HIDE == null || Cond.HIDE == false) ? "0" : "1"
             );


            return db.ExecuteQuery<AD_LIST>(sql).ToList();
        }
    }
}
