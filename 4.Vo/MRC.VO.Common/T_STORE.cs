using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRC.VO.Common
{
    public class T_STORE_COND {
        public int? COMPANY_CODE { get; set; }
        public string COMPANY_ID { get; set; }
        public string COMPANY_NAME { get; set; }
        public int? STORE_CODE { get; set; }
        public string STORE_ID { get; set; }
        public string STORE_NAME { get; set; }
        public int? STATUS { get; set; }
    }
    #region >> 회사별매장정보(T_STORE)
    /// <summary>       
    /// 회사별매장정보(T_STORE)
    /// </summary>	   
    public class T_STORE
    {
        public string COMPANY_ID { get; set; }
        /// <summary>       
        /// 매장코드(일련번호)
        /// </summary>	   
        public int STORE_CODE { get; set; }
        /// <summary>       
        /// T_COMPANY의 COMPANY_CODE(회사코드)
        /// </summary>	   
        public int COMPANY_CODE { get; set; }
        /// <summary>       
        /// 매장아이디(Unique)
        /// </summary>	   
        public string STORE_ID { get; set; }
        /// <summary>       
        /// 암호(SHA1암호화)
        /// </summary>	   
        public string PASSWORD { get; set; }
        /// <summary>       
        /// 회사명
        /// </summary>	   
        public string STORE_NAME { get; set; }
        /// <summary>       
        /// 전화번호
        /// </summary>	   
        public string PHONE { get; set; }
        /// <summary>       
        /// 핸드폰번호
        /// </summary>	   
        public string MOBILE { get; set; }
        /// <summary>       
        /// 이메일
        /// </summary>	   
        public string EMAIL { get; set; }
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
        public string ZIP_CODE { get; set; }
        /// <summary>       
        /// 대표자명
        /// </summary>	   
        public string OWNER_NAME { get; set; }
        /// <summary>       
        /// 대표자전화
        /// </summary>	   
        public string OWNER_PHONE { get; set; }
        /// <summary>       
        /// 대표자핸드폰
        /// </summary>	   
        public string OWNER_MOBILE { get; set; }
        /// <summary>       
        /// 대표자이메일
        /// </summary>	   
        public string OWNER_EMAIL { get; set; }
        /// <summary>       
        /// 상태(T_COMMON테이블의 MAIN_CODE : S001
        /// </summary>	   
        public int? STATUS { get; set; }
        /// <summary>       
        /// 문화권(언어-국가, ko-KR)
        /// </summary>	   
        public string CULTURE_NAME { get; set; }
        /// <summary>       
        /// 테마명
        /// </summary>	   
        public string THEME_NAME { get; set; }
        /// <summary>       
        /// 비고
        /// </summary>	   
        public string REMARK { get; set; }
        /// <summary>       
        /// 등록자(T_MEMBER의 MEMBER_CODE)
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
    #endregion >> 회사별매장정보(T_STORE) END 
}
