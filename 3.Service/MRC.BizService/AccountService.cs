using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MRC.Framework;
using MRC.Framework.Data;
using MRC.VO.Common;

namespace MRC.BizService
{
    public class AccountService : BaseService
    {
        public AccountService() { }
        public AccountService(System.Data.Linq.DataContext _db) : base(_db) { }

        public string SaveMember(T_MEMBER Param)
        {

            string msg = string.Empty;
            try
            {
                string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Account\\Account.xml", "MemberSave"
                                                , Param.MEMBER_CODE
                                                , Param.USER_TYPE.ToString("2")
                                                , Param.USER_ID
                                                , Param.PASSWORD
                                                , Param.USER_NAME
                                                , Param.EMAIL
                                                , Param.PHONE
                                                , Param.MOBILE
                                                , Param.ADDRESS1
                                                , Param.ADDRESS2
                                                , Param.ZIPCODE
                                                , Param.REMARK
                                                , Param.HIDE == true ? "1" : "0"
                                                , "0" /*Admin*/

                  );
                db.ExecuteCommand(sql);
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return msg;
        }
    }
}
