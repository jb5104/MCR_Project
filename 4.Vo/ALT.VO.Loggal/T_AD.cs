using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALT.VO.loggal
{
    #region >> 광고 검색 조회 조건 - AD_SEARCH_COND
    /// <summary>
    /// 광고 검색 조회 조건 - AD_SEARCH_COND
    /// </summary>
    public class AD_SEARCH_COND
    {
        /// <summary>
        /// 카테고리 멀티 선택시 사용, 검색요청시 5자리로 만듦, 코드가 240 일경우 요청코드 : 00240
        /// </summary>
        public string[] SEARCH_CATEGORY_CODE { get; set; }
        /// <summary>
        /// 경도(클라이언트단말기 위치정보) >> 값이 있으면 조건에추가 dbo.FN_TO_DISTANCE(@LATITUDE, @LONGITUDE, C.LATITUDE, C.LONGTITUDE,'KM') <= @RADIUS
        /// </summary>
        public decimal? LATITUDE { get; set; }
        /// <summary>
        /// 위도(클라이언트단말기 위치정보) >> 값이 있으면 조건에추가 dbo.FN_TO_DISTANCE(@LATITUDE, @LONGITUDE, C.LATITUDE, C.LONGTITUDE,'KM') <= @RADIUS
        /// </summary>
        public decimal? LONGITUDE { get; set; }
        /// <summary>
        /// 반경(km) : 클라이언트 단말기로 부터 검색 반경
        /// </summary>
        public int? RADIUS { get; set; }
        /// <summary>
        /// 키워드명 >> 값이 있으면 조건에추가 IN (5,10)
        /// </summary>
        public string[] SEARCH_KEYWORD_CODE { get; set; }
        /// <summary>
        /// 키워드명 >> 값이 있으면 조건에추가 LIKE '%검색조건%'
        /// </summary>
        public string KEYWORD_NAME { get; set; }
        /// <summary>
        /// 광고코드 >> 값이 잇으면 조건에추가
        /// </summary>
        public Int64? AD_CODE { get; set; }
        /// <summary>
        /// 카테고리와 키워드 연결 테이블의 기본키
        /// </summary>
        public int? CK_CODE { get; set; }
        /// <summary>
        /// 광고숨김여부 (0:보이기:default, 1:숨김)
        /// </summary>
        public bool? HIDE { get; set; }
        /// <summary>
        /// 페이지당 건수 (기본 20건)
        /// </summary>
        public int? PageCount { get; set; }
        /// <summary>
        /// 선택된 페이지 기본 1
        /// </summary>
        public int? Page { get; set; }
    }
    #endregion

    #region >> 광고 조회 클래스
    /// <summary>
    /// 광고 조회결과 클래스
    /// </summary>
    public class AD_LIST
    {
        /// <summary>
        /// 광고코드
        /// </summary>
        public Int64 AD_CODE { get; set; }
        /// <summary>
        /// 광고제목
        /// </summary>
        public string TITLE { get; set; }
        /// <summary>
        /// 광고부제목
        /// </summary>
        public string SUB_TITLE { get; set; }
        /// <summary>
        /// 광고로고
        /// </summary>
        public string LOGO_URL { get; set; }
        /// <summary>
        /// 광고 클릭 횟수
        /// </summary>
        public int CLICK_CNT { get; set; }
        /// <summary>
        /// 평가점수
        /// </summary>
        public decimal GRADE_POINT { get; set; }
        /// <summary>
        /// 광고요청한 회사 코드 T_COMPANY 테이블의 COMPANY_CODE
        /// </summary>
        public int? REQUEST_COMPANY_CODE { get; set; }
        /// <summary>
        /// 요청한매장코드들 T_SOTRE 테이블의 STORE_CODE , 구분자 => | 값이 없으면 업체 전체 광고
        /// </summary>
        public string REQUEST_STORE_CODES { get; set; }
        /// <summary>
        /// 광고요청한 회사명 T_COMPANY테이블의 COMPANY_NAME
        /// </summary>
        public string REQUEST_COMPANY_NAME { get; set; }
        /// <summary>
        /// 요청자 코드 T_MEMBER테이블의 MEMBER_CODE
        /// </summary>
        public int? REQUEST_USER_CODE { get; set; }
        /// <summary>
        /// 요청자명 T_MEMBER테이블의 MEMBER_CODE
        /// </summary>
        public string REQUEST_USER_NAME { get; set; }
    }
    #endregion

    #region >> 광고테이블(T_AD)
    /// <summary>       
    /// 광고테이블(T_AD)
    /// </summary>	   
    public class T_AD
    {
        /// <summary>
        /// 조회순번
        /// </summary>
        public int SEQ { get; set; }
        /// <summary>       
        /// 일련번호(기본키)
        /// </summary>	   
        public Int64 AD_CODE { get; set; }
        /// <summary>       
        /// 로고 URL
        /// </summary>	   
        public string LOGO_URL { get; set; }
        /// <summary>       
        /// 제목
        /// </summary>	   
        public string TITLE { get; set; }
        /// <summary>       
        /// 부제목
        /// </summary>	   
        public string SUB_TITLE { get; set; }
        /// <summary>       
        /// 내용
        /// </summary>	   
        public string CONTENT { get; set; }
        /// <summary>       
        /// 광고시작일(yyyyMMdd)
        /// </summary>	   
        public string FR_DATE { get; set; }
        /// <summary>       
        /// 광고종료일(yyyyMMdd)
        /// </summary>	   
        public string TO_DATE { get; set; }
        /// <summary>       
        /// 광고시작시간(HH:mm)
        /// </summary>	   
        public string FR_TIME { get; set; }
        /// <summary>       
        /// 광고시작시간(HH:mm)
        /// </summary>	   
        public string TO_TIME { get; set; }
        /// <summary>       
        /// 광고클릭수
        /// </summary>	   
        public int CLICK_CNT { get; set; }
        /// <summary>       
        /// 별포인트 5점 만점 평점으로 계산
        /// </summary>	   
        public decimal GRADE_POINT { get; set; }
        /// <summary>       
        /// T_COMPANY 테이블의 COMPANY_CODE
        /// </summary>	   
        public int? REQUEST_COMPANY_CODE { get; set; }
        /// <summary>       
        /// 요청한매장코드들 T_SOTRE 테이블의 STORE_CODE , 구분자 => | 값이 없으면 업체 전체 광고
        /// </summary>	   
        public string REQUEST_STORE_CODES { get; set; }
        /// <summary>       
        /// 요청한사용자코드 T_MEMBER 테이블의 MEMBER_CODE
        /// </summary>	   
        public int? REQUEST_USER_CODE { get; set; }
        /// <summary>       
        /// 비고
        /// </summary>	   
        public string REMARK { get; set; }
        /// <summary>       
        /// 숨김여부(1:숨김)
        /// </summary>	   
        public bool HIDE { get; set; }
        /// <summary>       
        /// 등록자
        /// </summary>	   
        public int INSERT_CODE { get; set; }
        /// <summary>       
        /// 등록일
        /// </summary>	   
        public DateTime INSERT_DATE { get; set; }
        /// <summary>       
        /// 광고테이블
        /// </summary>	   
        public int UPDATE_CODE { get; set; }
        /// <summary>       
        /// 광고테이블
        /// </summary>	   
        public DateTime UPDATE_DATE { get; set; }
    }
    #endregion >> 광고테이블(T_AD) END 

}
