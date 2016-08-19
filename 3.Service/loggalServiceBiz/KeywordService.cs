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
    public class KeywordService : BaseService
    {

        /// <summary>
        /// 광고카테고리리스트가져오기
        /// </summary>
        /// <param name="Cond"></param>
        /// <returns></returns>
        public IList<CODE_DATA> GetKeywordKoreanList(KEYWORD_COND Cond)
        {
            Koreanunit.HANGUL_INFO data = Koreanunit.HangulJaso.DevideJaso(Cond.KEYWORD_NAME);

            int nCnt = 0;
            StringBuilder sbKoreanWansung = new StringBuilder();

            if(data.arrWansungs.Count() > 0)
            {
                sbKoreanWansung.Append("\n").Append("           AND (");
                for(int i=0; i< data.arrWansungs.Count(); i++)
                {
                    if (i > 0) sbKoreanWansung.Append("\n").Append("                   AND ");

                    sbKoreanWansung.Append("   KEYWORD_NAME LIKE N'%").Append(data.arrWansungs[i].ToString("")).Append("%'");
                }
                sbKoreanWansung.Append("\n").Append("             )");
            }


            if (data.arrChosung.Where(w => !string.IsNullOrEmpty(w)).Count() > 0)
            {
                Cond.CHOSUNG = "(";
                for (int i = 0; i < data.arrChosung.Count(); i++)
                {
                    if (!string.IsNullOrEmpty(data.arrChosung[i]))
                    {
                        if (nCnt > 0) Cond.CHOSUNG += " AND ";
                        Cond.CHOSUNG += " CHOSUNG LIKE " + ((i > 0) ? "N'%" : "N'") + data.arrChosung[i].ToString("") + "%'";
                        nCnt++;
                    }
                }
                Cond.CHOSUNG += ") ";
            }
            nCnt = 0;
            if (data.arrJungung.Where(w => !string.IsNullOrEmpty(w)).Count() > 0)
            {
                Cond.JUNGSUNG = "AND (";
                for (int i = 0; i < data.arrJungung.Count(); i++)
                {
                    if (!string.IsNullOrEmpty(data.arrJungung[i]))
                    {
                        if (nCnt > 0) Cond.JUNGSUNG += " AND ";
                        Cond.JUNGSUNG += " JUNGSUNG LIKE " + ((i > 0) ? "N'%" : "N'") + data.arrJungung[i].ToString("") + "%'";
                        nCnt++;
                    }
                }
                Cond.JUNGSUNG += ") ";
            }
            nCnt = 0;
            if (data.arrJongung.Where(w => !string.IsNullOrEmpty(w)).Count() > 0)
            {
                Cond.JONGSUNG = "AND (";
                for (int i = 0; i < data.arrJongung.Count(); i++)
                {
                    if (!string.IsNullOrEmpty(data.arrJongung[i]))
                    {
                        if (nCnt > 0) Cond.JONGSUNG += " AND ";
                        Cond.JONGSUNG += " JONGSUNG LIKE " + ((i > 0) ? "N'%" : "N'") + data.arrJongung[i].ToString("") + "%'";
                        nCnt++;
                    }
                }
                Cond.JONGSUNG += ") ";
            }
            Cond.KEYWORD_UNITS = data.splitstring;
            string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\Keyword.xml", "GetKeywordKoreanList"
                                         , Cond.KEYWORD_NAME.ToString("")
                                         , sbKoreanWansung.ToString()
                                         , Cond.CHOSUNG
                                         , Cond.JUNGSUNG
                                         , Cond.JONGSUNG
                                         , Cond.KEYWORD_UNITS.ToString("")

             );


            return db.ExecuteQuery<CODE_DATA>(sql).ToList();
        }

        /// <summary>
        /// 지역명으로 조회
        /// </summary>
        /// <param name="Cond"></param>
        /// <returns></returns>
        public IList<CODE_DATA> GetLocalNameList(CODE_DATA Cond)
        {
            StringBuilder sbCond1 = new StringBuilder();
            StringBuilder sbCond2 = new StringBuilder();
            StringBuilder sbCond3 = new StringBuilder();
            StringBuilder sbCond = new StringBuilder();
            StringBuilder sbCondTmp = new StringBuilder();
            int nCnt = 0, n = 0 ;
            string strBigCity = "서울|부산|인천|대구|울산|광주|대전";
            Koreanunit.HANGUL_INFO KoreandataOri = Koreanunit.HangulJaso.DevideJaso(Cond.NAME);

            if (!string.IsNullOrEmpty(Cond.NAME))
            {
                List<string> arrData = Cond.NAME.Split(' ').ToList();

                #region >> 카테고리 1 조회 조건




                sbCond1.Append("\n").Append("    AND");
                sbCond1.Append("\n").Append("     ( ");
                sbCond1.Append("\n").Append("     A1C.BASE_CODE IN (SELECT BASE_CODE FROM T_KEYWORD                                        ");
                sbCond1.Append("\n").Append("  	                           WHERE KEYWORD_NAME LIKE N'%").Append(Cond.NAME).Append("%' OR KEYWORD_UNITS LIKE N'%").Append(KoreandataOri.splitstring.ToString("")).Append("%'           ");


                sbCond1.Append("\n").Append("  							      OR ( ");
                List<string> arrSting = KoreandataOri.chosung.Split(' ').ToList();
                sbCond1.Append("\n").Append("  								       (");
                for (nCnt = 0; nCnt < arrSting.Count(); nCnt++)
                {
                    if (nCnt > 0) sbCond1.Append("\n").Append("  								      AND ");
                    sbCond1.Append("  CHOSUNG LIKE N'").Append(nCnt > 0 ? "%" : "").Append(arrSting[nCnt].ToString("")).Append("%'");

                }
                sbCond1.Append("\n").Append("  								      )");

                string sChosungFirst = arrSting[0].ToString("");
                arrSting = KoreandataOri.jungsung.Split(' ').ToList();

                sbCond1.Append("\n").Append("  								      AND (");
                for (nCnt = 0; nCnt < arrSting.Count(); nCnt++)
                {
                    if (nCnt > 0) sbCond1.Append("\n").Append("  								      AND ");
                    if (nCnt == 0)
                    {
                        sbCond1.Append("  SUBSTRING(JUNGSUNG, CHARINDEX('").Append(sChosungFirst).Append("', CHOSUNG),LEN(JUNGSUNG) ) LIKE N'").Append(arrSting[nCnt].ToString("")).Append("%'");
                    }
                    else
                        sbCond1.Append("  JUNGSUNG LIKE N'%").Append(arrSting[nCnt]).Append("%'");

                }
                sbCond1.Append("\n").Append("  								      )");

                arrSting = KoreandataOri.jongsung.Split(' ').ToList();
                sbCond1.Append("\n").Append("  								      AND (");
                for (nCnt = 0; nCnt < arrSting.Count(); nCnt++)
                {
                    if (nCnt > 0) sbCond1.Append("\n").Append("  								      AND ");
                    if (nCnt == 0)
                    {
                        sbCond1.Append("  SUBSTRING(JONGSUNG, CHARINDEX('").Append(sChosungFirst).Append("', CHOSUNG),LEN(JONGSUNG) ) LIKE N'").Append(arrSting[nCnt].ToString("")).Append("%'");
                    }
                    else
                        sbCond1.Append("  JONGSUNG LIKE N'%").Append(arrSting[nCnt]).Append("%'");

                }
                sbCond1.Append("\n").Append("  								      )");
                if (KoreandataOri.arrWansungs.Count() > 0)
                {
                    sbCond1.Append("\n").Append("  	                                     AND ( ");

                    for (nCnt = 0; nCnt < KoreandataOri.arrWansungs.Count(); nCnt++)
                    {
                        if (nCnt > 0) sbCond1.Append("\n").Append("  	                                 AND ");
                        sbCond1.Append("\n").Append("  	                                         A1C.KEYWORD_NAME LIKE N'%").Append(KoreandataOri.arrWansungs[nCnt].ToString("")).Append("%'");
                    }


                    sbCond1.Append("\n").Append("  	                                        ) ");
                }
                sbCond1.Append("\n").Append("  								)                                                                          ");
                sbCond1.Append("\n").Append("  						)                                                                                        ");

                sbCond1.Append("\n").Append("  		)  ");
                

                #endregion


                for (int i = 0; i < arrData.Count(); i++)
                {
                    #region >> for문 상세

                    string sName = string.Empty;
                    bool bChk = false;
                    if (i >= 1)
                    {
                        if (arrData.Where(w => !strBigCity.Contains(w) && (w.LastIndexOf("시", 0) == 0 || w.LastIndexOf("구", 0) == 0)).Count() == 2)
                        {
                            string sSi = arrData.Where(w => !strBigCity.Contains(w) && (w.LastIndexOf("시", 0) == 0)).ToArray()[0];
                            string sDo = arrData.Where(w => !strBigCity.Contains(w) && (w.LastIndexOf("구", 0) == 0)).ToArray()[0];
                            if (arrData[i] == sSi || arrData[i] == sDo)
                            {
                                i = i + 1;
                                bChk = true;
                                sName = sSi + " " + sDo;
                            }
                        }
                    }
                    if (!bChk)
                    {
                        sName = arrData[i];
                    }
                    sbCond2.Append("\n").Append("  AND (");
                    sbCond3.Append("\n").Append("  AND (");
                    sbCond.Remove(0, sbCond.Length);


                    for (int j = 1; j <= 3; j++)
                    {
                        Koreanunit.HANGUL_INFO Koreandata = Koreanunit.HangulJaso.DevideJaso(sName);
                        if (j > 1) sbCond.Append("\n").Append("  OR ");
                        sbCond.Append("\n").Append("     A").Append((j).ToString()).Append("C.BASE_CODE IN (SELECT BASE_CODE FROM T_KEYWORD                                        ");
                        sbCond.Append("\n").Append("  	                           WHERE KEYWORD_NAME LIKE N'%").Append(sName).Append("%' OR KEYWORD_UNITS LIKE N'%").Append(Koreandata.splitstring.ToString("")).Append("%'           ");
                        sbCond.Append("\n").Append("  							      OR ( ");


                        arrSting = Koreandata.chosung.Split(' ').ToList();
                        sbCond.Append("\n").Append("  								       (");
                        for (nCnt = 0; nCnt < arrSting.Count(); nCnt++)
                        {
                            if (nCnt > 0) sbCond.Append("\n").Append("  								      AND ");
                            sbCond.Append("  CHOSUNG LIKE N'").Append(nCnt > 0 ? "%" : "").Append(arrSting[nCnt].ToString("")).Append("%'");

                        }
                        sbCond.Append("\n").Append("  								      )");


                        sChosungFirst = arrSting[0].ToString("");
                        arrSting = Koreandata.jungsung.Split(' ').ToList();

                        sbCond.Append("\n").Append("  								      AND (");
                        for (nCnt = 0; nCnt < arrSting.Count(); nCnt++)
                        {
                            if (nCnt > 0) sbCond.Append("\n").Append("  								      AND ");
                            if (nCnt == 0)
                            {
                                sbCond.Append("  SUBSTRING(JUNGSUNG, CHARINDEX('").Append(sChosungFirst).Append("', CHOSUNG),LEN(JUNGSUNG) ) LIKE N'").Append(arrSting[nCnt].ToString("")).Append("%'");
                            }
                            else
                                sbCond.Append("  JUNGSUNG LIKE N'%").Append(arrSting[nCnt].ToString("")).Append("%'");

                        }
                        sbCond.Append("\n").Append("  								      )");





                        arrSting = Koreandata.jongsung.Split(' ').ToList();
                        sbCond.Append("\n").Append("  								      AND (");
                        for (nCnt = 0; nCnt < arrSting.Count(); nCnt++)
                        {
                            if (nCnt > 0) sbCond.Append("\n").Append("  								      AND ");
                            if (nCnt == 0)
                            {
                                sbCond.Append("  SUBSTRING(JONGSUNG, CHARINDEX('").Append(sChosungFirst).Append("', CHOSUNG),LEN(JONGSUNG) ) LIKE N'").Append(arrSting[nCnt].ToString("")).Append("%'");
                            }
                            else
                                sbCond.Append("  JONGSUNG LIKE N'%").Append(arrSting[nCnt].ToString("")).Append("%'");

                        }
                        //sbCond.Append("\n").Append("  									  AND SUBSTRING(JONGSUNG, CHARINDEX('").Append(Koreandata.chosung).Append("', CHOSUNG),LEN(JONGSUNG) ) LIKE '%").Append(Koreandata.jongsung).Append("%'                                                   ");
                        sbCond.Append("\n").Append("  									  )                                                                          ");
                        #region >> 완성조건
                        sbCondTmp.Remove(0, sbCondTmp.Length);
                        if (Koreandata.arrWansungs.Count() > 0)
                        {
                            sbCondTmp.Append("\n").Append("  	                             AND ( ");

                            for (nCnt = 0; nCnt < Koreandata.arrWansungs.Count(); nCnt++)
                            {
                                if (nCnt > 0) sbCondTmp.Append("\n").Append("  	                                 AND ");
                                sbCondTmp.Append("\n").Append("  	                                          A1C.KEYWORD_NAME LIKE N'%").Append(Koreandata.arrWansungs[nCnt].ToString("")).Append("%'");
                            }


                            sbCondTmp.Append("\n").Append("  	                                 ) ");
                        }
                        sbCond.Append(sbCondTmp.ToString());
                        #endregion >> 완성조건완료
                        sbCond.Append("\n").Append("  						)   ");
                        sbCond.Append("\n").Append("  			)  ");

                        if (j == 2)
                        {

                           
                            sbCond2.Append(sbCond.ToString());
                        }
                        if (j == 3)
                        {
                            sbCond3.Append(sbCond.ToString());
                        }
                    }
                    sbCond2.Append("\n").Append("  )");//.Append(sbCondTmp.ToString());
                    sbCond3.Append("\n").Append("  )");//.Append(sbCondTmp.ToString());
                    #endregion
                }
                n++;
             
            }
            int nTop = 20;
            if (Cond.CODE == null) nTop = 20;
            else if (Cond.CODE == -1) nTop = 1;
            else nTop = (int)Cond.CODE;


            string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\Keyword.xml", "GetLocalNameList"
                                              //, Cond.NAME
                                              //, KoreandataOri.splitstring
                                              //, KoreandataOri.chosung
                                              //, KoreandataOri.jungsung
                                              //, KoreandataOri.jongsung
                                              , nTop.ToString()
                                              , sbCond1.ToString()
                                              , sbCond2.ToString()
                                              , sbCond3.ToString()



                  );


            return db.ExecuteQuery<CODE_DATA>(sql).ToList();
        }


        /// <summary>
        /// 카테고리키워드 조회
        /// </summary>
        /// <param name="Cond"></param>
        /// <returns></returns>
        public IList<CODE_DATA> GetCategoryKeywordList(CATEGORY_KEYWORD_COND Cond)
        {
            string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\Keyword.xml", "GetCategoryKeywordList"
                                           , Cond.CATEGORY_CODE.ToString("")
                                            , Cond.SEARCH_CATEGORY_CODE.ToString("")
                                            , Cond.CATEGORY_TYPE.ToString("2")
                                            , Cond.LEVEL_DEPTH.ToString("1")
                                            , Cond.KEYWORD_TYPE.ToString("2")
                );


            return db.ExecuteQuery<CODE_DATA>(sql).ToList();
        }
    }
}
