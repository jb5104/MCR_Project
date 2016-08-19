using MRC.Framework.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRC.VO.Common
{
    /// <summary>
    /// 로그인정보
    /// </summary>
    public class LOGIN_INFO
    {
        public T_MEMBER MEMBER { get; set; }
        public T_COMPANY COMPANY { get; set; }
        public T_STORE STORE { get; set; }
        List<T_STORE_WEBMENU> _webMenu;
        public List<T_STORE_WEBMENU> WebMemu { get { return _webMenu == null ? new List<T_STORE_WEBMENU>() : _webMenu; } set { _webMenu = value; } }
        public string LANGUAGE { get; set; }
        public string COMPANY_ID { get; set; }
        public int? STORE_CODE { get; set; }
        public List<T_STORE_IMAGE> StoreImageList { get; set; }

        public CULTURE_INFO CultureInfo { get; set; }

        private string _baseUrl = string.Empty;
        public string BASE_URL { set { _baseUrl = value; } get { return ("/" + COMPANY_ID + "/" + LANGUAGE).ToLower(); } }

        SHOPPING_CART _cart;
        public SHOPPING_CART SHOPPING_CART { get { return ((_cart == null) ? new SHOPPING_CART() : _cart); } set { _cart = value; } }
    }

    /// <summary>
    /// 장바구니 정보
    /// </summary>
    public class SHOPPING_CART
    {
        public decimal BEFORE_AMT { get; set; }
        public int ITEM_CNT { get; set; }
        public decimal TOTAL_AMT { get; set; }
        public int ORDER_DISCOUNT_TYPE { get; set; }
        public decimal ORDER_DISCOUNT_AMT { get; set; }
        public decimal ITEM_DISCOUNT_AMT { get; set; }
        public decimal DELIVERY_FEE { get; set; }
        public decimal ETC_ADD_AMT { get; set; }
        public decimal TOT_TAX { get; set; }
        public decimal TAX1 { get; set; }
        public decimal TAX2 { get; set; }
        public decimal TAX3 { get; set; }
        public string TAX1_NAME { get; set; }
        public string TAX2_NAME { get; set; }
        public string TAX3_NAME { get; set; }
     
        public List<SHOPPING_ITEM> ITEM_LIST { get; set; }
        public List<CART_COUPON_USE> COUPON_LIST { get; set; }
        public List<T_SALE_DISCOUNT> DISCOUNT_LIST { get; set; }
        public List<T_SALE_TIP> TIP_LIST { get; set; }
  
    }


    /// <summary>
    /// 주문시 쿠폰사용
    /// </summary>
    public class CART_COUPON_USE
    {
        /// <summary>       
        /// 쿠폰코드(자동순번)
        /// </summary>	   
        public Int64 COUPON_CODE { get; set; }
        /// <summary>       
        /// 쿠폰사용일
        /// </summary>	   
        public string USE_DATE { get; set; }
        /// <summary>       
        /// 쿠폰사용 유무
        /// </summary>	   
        public bool USE_YN { get; set; }
        /// <summary>       
        /// 쿠폰사용회원, 값이 null 일경우 회원 상관없이 모두 할인
        /// </summary>	   
        public int? MEMBER_CODE { get; set; }
        /// <summary>       
        /// 쿠폰사용업체
        /// </summary>	   
        public int? COMPANY_CODE { get; set; }
        /// <summary>       
        /// 사용가능품목그룹, 값이 null 일경우 폼목그룹에 상관없이 모두 할인
        /// </summary>	
        public Int64? ITEM_GROUP_CODE { get; set; }
        /// <summary>       
        /// 사용가능품목그룹명, 값이 null 일경우 폼목그룹에 상관없이 모두 할인
        /// </summary>	
        public string ITEM_GROUP_NAME{ get; set; }
        /// <summary>       
        /// 사용가능품목, 값이 null 일경우 폼목에 상관없이 모두 할인
        /// </summary>	
        public Int64? ITEM_CODE { get; set; }
        /// <summary>       
        /// 쿠폰사용매장
        /// </summary>	   
        public int? STORE_CODE { get; set; }
        /// <summary>       
        /// T_SALE테이블 참조
        /// </summary>	   
        public Int64? SALE_CODE { get; set; }
        /// <summary>       
        /// T_SALE_ITEM테이블 참조, 값이 없을 경우 주문할인 있을 경우 해당 아이템 할인
        /// </summary>	   
        public int? SALE_ITEM_SEQ { get; set; }
        /// <summary>
        /// 비고
        /// </summary>
        public string REMARK { get; set; }
    }


    /// <summary>
    /// 장바구니 아이템 정보
    /// </summary>
    public class SHOPPING_ITEM
    {
        public string ITEM_GROUP_NAME { get; set; }
        public string ITEM_IMAGEURL { get; set; }
        public int ITEM_SEQ { get; set; }
        public Int64 ITEM_CODE { get; set; }
        public string ITEM_NAME { get; set; }
        /// <summary>       
        /// 아이템유형T_ITEM 테이블의 ITEM_TYPE그대로저장 T_COMMON : I002
        /// </summary>	   
        public int ITEM_TYPE { get; set; }
        public decimal COST { get; set; }
        public int CNT { get; set; }
        /// <summary>       
        /// 총세금(T_ITEM TOT_TAX * CNT)
        /// </summary>	   
        public decimal TOT_TAX { get; set; }
        /// <summary>       
        /// 세금1(T_ITEM TAX1 * CNT)
        /// </summary>	   
        public decimal TAX1 { get; set; }
        /// <summary>       
        /// 세금2(T_ITEM TAX2 * CNT)
        /// </summary>	   
        public decimal TAX2 { get; set; }
        /// <summary>       
        /// 세금3(T_ITEM TAX3 * CNT)
        /// </summary>	   
        public decimal TAX3 { get; set; }
        /// <summary>       
        /// 할인유형
        /// </summary>	   
        public int DISCOUNT_TYPE { get; set; }
        public decimal DISCOUNT_AMT { get; set; }
         /// <summary>       
        /// 토핑템플릿코드(토핑테이브은아직만들어지지않음)
        /// </summary>	   
        public int? TOPPING_CODE { get; set; }
        public decimal PRICE { get; set; }
        public decimal SALES_AMT { get; set; }

        public string MEMO { get; set; }

        public List<T_SALE_ITEM_ADD> ADD_ITEM_LIST { get; set; }
    
    }

    //public abstract class VO_BASE
    //{
    //    public virtual string LANGUAGE { get; set; }
    //    public virtual int INSERT_CODE { get; set; }
    //    public virtual DateTime INSERT_DATE { get; set; }
    //    public virtual int UPDATE_CODE { get; set; }
    //    public virtual DateTime UPDATE_DATE { get; set; }

    //}
    #region >> 사용자정보(T_MEMBER)
    /// <summary>       
    /// 사용자정보(T_MEMBER)
    /// </summary>	   
    public class T_MEMBER
    {
        /// <summary>       
        /// 순번(일련번호)
        /// </summary>	   
        public int MEMBER_CODE { get; set; }
        /// <summary>       
        /// T_COMMON 테이블의 MAIN_CODE:U001 사용
        /// </summary>	   
        public int? USER_TYPE { get; set; }
        /// <summary>       
        /// 사용자아이디(E-Mail)
        /// </summary>	   
        public string USER_ID { get; set; }
        /// <summary>       
        /// 암호(SHA1으로 암호화)
        /// </summary>	   
        public string PASSWORD { get; set; }
        /// <summary>       
        /// 사용자명
        /// </summary>	   
        public string USER_NAME { get; set; }
        /// <summary>       
        /// 이메일
        /// </summary>	   
        public string EMAIL { get; set; }
        /// <summary>       
        /// 일반전화
        /// </summary>	   
        public string PHONE { get; set; }
        /// <summary>       
        /// 모바일번호
        /// </summary>	   
        public string MOBILE { get; set; }
        /// <summary>       
        /// 기본주소
        /// </summary>	   
        public string ADDRESS1 { get; set; }
        /// <summary>       
        /// 상세주소
        /// </summary>	   
        public string ADDRESS2 { get; set; }
        /// <summary>       
        /// 우편번호
        /// </summary>	   
        public string ZIPCODE { get; set; }
        /// <summary>       
        /// 비고
        /// </summary>	   
        public string REMARK { get; set; }
        /// <summary>       
        /// 숨김여부(1:숨김 0:보임)
        /// </summary>	   
        public bool HIDE { get; set; }
        /// <summary>       
        /// 등록자(T_MEMBER의 MEMBER_CODE)
        /// </summary>	   
        public int INSERT_CODE { get; set; }
        /// <summary>       
        /// 등록일
        /// </summary>	   
        public DateTime INSERT_DATE { get; set; }
        /// <summary>       
        /// 수정자(T_MEMBER의 MEMBER_CODE)
        /// </summary>	   
        public int UPDATE_CODE { get; set; }
        /// <summary>       
        /// 수정일
        /// </summary>	   
        public DateTime UPDATE_DATE { get; set; }
    }
    #endregion >> 사용자정보(T_MEMBER) END 


    #region >> 공통코드 테이블(항상 첫번째 데이터는 SUB_CODE가 *이고 필드 설명이 들어감)(T_COMMON)
    /// <summary>       
    /// 공통코드 테이블(항상 첫번째 데이터는 SUB_CODE가 *이고 필드 설명이 들어감)(T_COMMON)
    /// </summary>	   
    public class T_COMMON
    {
        /// <summary>       
        /// 일련번호(자동순번)
        /// </summary>	   
        public int? IDX { get; set; }
        /// <summary>       
        /// 메인코드
        /// </summary>	   
        public string MAIN_CODE { get; set; }
        /// <summary>       
        /// 서브코드
        /// </summary>	   
        public int? SUB_CODE { get; set; }
        /// <summary>       
        /// 이름
        /// </summary>	   
        public string NAME { get; set; }
        /// <summary>       
        /// 언어코드 T_LANGUAGE테이블의 LANGUAGE_CODE
        /// </summary>	   
        public Int64? LANGUAGE_CODE { get; set; }
        /// <summary>       
        /// 정렬순번
        /// </summary>	   
        public int? ORDER_SEQ { get; set; }
        /// <summary>       
        /// 참조코드1
        /// </summary>	   
        public string REF_DATA1 { get; set; }
        /// <summary>       
        /// 참조코드2
        /// </summary>	   
        public string REF_DATA2 { get; set; }
        /// <summary>       
        /// 참조코드3
        /// </summary>	   
        public string REF_DATA3 { get; set; }
        /// <summary>       
        /// 참조코드4
        /// </summary>	   
        public string REF_DATA4 { get; set; }
        /// <summary>       
        /// 숨김여부
        /// </summary>	   
        public bool? HIDE { get; set; }
        /// <summary>       
        /// 등록자(T_MEMBER의 MEMBER_COE)
        /// </summary>	   
        public int? INSERT_CODE { get; set; }
        /// <summary>       
        /// 등록일
        /// </summary>	   
        public DateTime? INSERT_DATE { get; set; }
        /// <summary>       
        /// 수정자(T_MEMBER의 MEMBER_CODE)
        /// </summary>	   
        public int? UPDATE_CODE { get; set; }
        /// <summary>       
        /// 수정일
        /// </summary>	   
        public DateTime? UPDATE_DATE { get; set; }
    }
    #endregion >> 공통코드 테이블(항상 첫번째 데이터는 SUB_CODE가 *이고 필드 설명이 들어감)(T_COMMON) END 

}
