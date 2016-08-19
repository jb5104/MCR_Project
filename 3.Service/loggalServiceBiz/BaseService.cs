﻿using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ALT.Framework;
using System.Web;

namespace loggalServiceBiz
{
    public class BaseService
    {
        protected readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public DataContext db;
        public BaseService()
        {
            //log.Debug("디비생성");
            if (db == null)
                db = new DataContext(Global.ConfigInfo.DefaultDBSource);
        }

        public BaseService(DataContext _db)
        {
            //log.Debug("디비생성");
            db = _db;
        }

        public string sqlBasePath { get { return HttpContext.Current.Server.MapPath(Global.ConfigInfo.SqlXmlPath); } }
    }
}