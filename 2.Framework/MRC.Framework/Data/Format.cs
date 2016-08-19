using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.ComponentModel;
using System.Reflection;
using MRC.Framework;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Xml;
namespace MRC.Framework.Data
{
    public static class ExtendFormat
    {
        /// <summary>
        /// 디폴트 스트링 설정
        /// </summary>
        /// <param name="value"></param>
        /// <param name="defaultValue"></param>
        /// <param name="bHtml"></param>
        /// <returns></returns>
        public static string ToString(this object value, string defaultValue = "", bool bHtml = false)
        {
            if (value.isNumeric())
                return (value == null || (value.GetType().Name.ToUpper() != "STRING"
                    && value.GetType().Name.ToUpper().Contains("DATE")
                    )) ? defaultValue : value.ToString();
            else
            {
                
                if (value == null)
                {
                    return defaultValue;
                }
                else
                {
                    string[] arrData = value.ToString().Split('-');
                    if (arrData.Length == 3 && value.ToString().Length < 20) //전화번호 날짜일때 제외
                    {
                        return value.ToString();
                    }
                    else
                    {
                        if (bHtml)
                            return (value == null) ? defaultValue : Global.SecurityInfo.getGetSafeHtml(value.ToString());
                        else
                            return (value == null || Convert.ToString(value) == "") ? defaultValue : Global.SecurityInfo.getSqlInjectIon(value.ToString());
                    }
                }
            }
        }

        public static string ToInjectionString(this string val)
        {
            val = val.Replace("'", "''");

            return val;
        }

        public static decimal Step(this int val)
        {
            return Convert.ToDecimal(Convert.ToDouble(1) / System.Math.Pow(10, (double)val));
        }

        public static decimal ConvertDecimal(this object value)
        {
            if (value.isNumeric())
            {
                return Convert.ToDecimal(value);
            }
            else
                return 0;
        }

        public static int ConvertInt(this object value)
        {
            if (value.isNumeric())
            {
                return Convert.ToInt32(value);
            }
            else
                return 0;
        }
        public static decimal ToRound(this object value, int degit = 2)
        {
            if (value.isNumeric())
            {
                return Math.Round(Convert.ToDecimal(value) * (decimal)Math.Pow(10, degit)) / (decimal)Math.Pow(10, degit);
            }
            else
                return 0;
        }


        /// <summary>
        /// 특수문자 처리
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToLettreString(this string value)
        {
            return value.Replace(value, "\\" + value);
        }
        public static string RemoveDateString(this string val)
        {
            try
            {
                if (string.IsNullOrEmpty(val))
                    return string.Empty;
                else
                    return Convert.ToDateTime(val).ToString("yyyyMMdd");
            }
            catch
            {
                return string.Empty;
            }
        }
        // public 
        /// <summary>
        /// 숫자형인지 체크
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool isNumeric(this object value)
        {
            if (value == null)
                return false;
            double Num;
            return double.TryParse(value.ToString(), out Num);
        }

        public static DateTime ToDate(this string value, string CultureName = "ko-KR")
        {
            return Convert.ToDateTime(value, new System.Globalization.CultureInfo(CultureName));
        }

        public static string ToFormatDate(this string value, string format = "yyyy.mm.dd")
        {
            if (value == null || value.Count() <8) return string.Empty;
            value = value.Replace(".", "").Replace("-", "").Replace("/", "");
            if (value.Count() != 8) return string.Empty;

            DateTime dDate = Convert.ToDateTime(value.Substring(0, 4) + "-" + value.Substring(4, 2) + value.Substring(6, 2));
            return  dDate.ToString(format);

        }

    }

    public class Format
    {

        //public string ToString(object value, string defaultValue = "", bool bHtml = false)
        //{


        //    if (Global.Format.IsNumeric(value))
        //        return (value == null || (value.GetType().Name.ToUpper() != "STRING" && value.ToString() == "0")) ? defaultValue : value.ToString();
        //    else
        //    {
        //        if (value == null)
        //        {
        //            return defaultValue;
        //        }
        //        else
        //        {
        //            string[] arrData = value.ToString().Split('-');
        //            if (arrData.Length == 3 && value.ToString().Length < 20) //전화번호 날짜일때 제외
        //            {
        //                return value.ToString();
        //            }
        //            else
        //            {
        //                if (bHtml)
        //                    return (value == null) ? defaultValue : Global.SecurityInfo.getGetSafeHtml(value.ToString());
        //                else
        //                    return (value == null) ? defaultValue : Global.SecurityInfo.getSqlInjectIon(value.ToString());
        //            }
        //        }

        //    }
        //}



        public string ConvertFromToFormatDate(string frDate, string toDate, string format = "yyyy.mm.dd", string gubun = "~")
        {
            return (string.IsNullOrEmpty(frDate) ? "" : frDate.ToString(format)) + gubun + (string.IsNullOrEmpty(toDate) ? "" : frDate.ToString(toDate));
        }
        /// <summary>
        /// 나이가져오기
        /// </summary>
        /// <param name="birth"></param>
        /// <returns></returns>
        public int GetAge(string birth)
        {

            try
            {
                birth = birth.Substring(0, 4);
                return Convert.ToInt32(DateTime.Now.Year - Convert.ToInt32(birth.Substring(0, 4)) + 1);
            }
            catch (Exception)
            { }
            return 0;
        }
        /// <summary>
        /// 만 나이가져오기
        /// </summary>
        /// <param name="birth"></param>
        /// <returns></returns>
        public int GetAge2(string birth)
        {

            try
            {
                if (string.IsNullOrEmpty(birth) || birth.Length < 8)
                    return 0;
                birth = this.RemoveDateFormat(birth);
                birth = birth.Substring(0, 4) + "-" + birth.Substring(4, 2) + "-" + birth.Substring(6, 2);
                TimeSpan ts = DateTime.Now - Convert.ToDateTime(birth);
                decimal dYear = Convert.ToDecimal(ts.Days) / Convert.ToDecimal(365);

                return Convert.ToInt32(Math.Floor(dYear));
            }
            catch (Exception)
            { }
            return 0;
        }

        /// <summary>
        /// 날짜포맷 없애기
        /// </summary>
        /// <param name="sDate"></param>
        /// <returns></returns>
        public string RemoveDateFormat(string sDate)
        {
            if (string.IsNullOrEmpty(sDate))
                return string.Empty;

            try
            {
                DateTime dt;
                bool chk = DateTime.TryParse(sDate, out dt);
                if (chk)
                    return dt.ToString("yyyyMMdd");
                else
                    return string.Empty;
            }
            catch (Exception) { }
            sDate = sDate.Replace("/", "");
            sDate = sDate.Replace(".", "");
            sDate = sDate.Replace("-", "");
            return sDate;
        }
        /// <summary>
        /// 날짜 포맷으로 만들기
        /// </summary>
        /// <param name="sDate"></param>
        /// <param name="dtFormat"></param>
        /// <param name="gubun"></param>
        /// <returns></returns>
        public string ConvertToDateString(string sDate, GlobalEnum.DateFormat dtFormat, string gubun)
        {

            sDate = this.RemoveDateFormat(sDate);
            try
            {
                switch (dtFormat)
                {
                    case GlobalEnum.DateFormat.yyyy:
                        break;
                    case GlobalEnum.DateFormat.yyyyMM:
                        sDate = sDate.Substring(0, 4) + gubun + sDate.Substring(4, 2);
                        break;
                    case GlobalEnum.DateFormat.yyyyMMdd:
                        sDate = sDate.Substring(0, 4) + gubun + sDate.Substring(4, 2) + gubun + sDate.Substring(6, 2);
                        break;
                    case GlobalEnum.DateFormat.yyyyMMddKor:
                        sDate = sDate.Substring(0, 4) + "년 " + Convert.ToInt32(sDate.Substring(4, 2)).ToString() + "월 " + Convert.ToInt32(sDate.Substring(6, 2)) + "일";
                        break;
                    default:
                        sDate = string.Empty;
                        break;
                }
            }
            catch { return string.Empty; }
            return sDate;
        }

        /// <summary>
        /// 유일한 문자 만들기
        /// </summary>
        /// <param name="maxSize"></param>
        /// <returns></returns>
        public string MakeUniqueString(int maxSize)
        {
            char[] chars = new char[45];
            chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            byte[] data = new byte[1];

            RNGCryptoServiceProvider identity = new RNGCryptoServiceProvider();
            identity.GetNonZeroBytes(data);
            data = new byte[maxSize];
            identity.GetNonZeroBytes(data);
            StringBuilder result = new StringBuilder(maxSize);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }
            return result.ToString();
        }

        #region >> 숫자를 체크하는 함수
        public bool IsNumeric(object value)
        {
            if (value == null)
                return false;
            int nCnt = 0;
            foreach (char _char in value.ToString())
            {
                if ((nCnt > 0 && _char == '-') && !Char.IsNumber(_char) && _char != '.' && _char != ',')
                    return false;
                nCnt++;
            }
            return true;
        }
        #endregion

        #region >> 숫자만 가져오는 함수
        public string GetOnlyNumber(string value)
        {
            // System.Text.RegularExpressions
            return Regex.Replace(value, @"\D", "");
        }

        #endregion

        #region >> 리스트를 데이터 테이블로 변환
        public DataTable ConvertToDataTable<T>(IList<T> list)
        {
            DataTable table = CreateTable<T>();
            Type entityType = typeof(T);
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);

            foreach (T item in list)
            {
                DataRow row = table.NewRow();

                foreach (PropertyDescriptor prop in properties)
                {
                    row[prop.Name] = prop.GetValue(item);
                }

                table.Rows.Add(row);
            }
            return table;
        }

        public IList<T> ConvertTo<T>(IList<DataRow> rows)
        {
            IList<T> list = null;

            if (rows != null)
            {
                list = new List<T>();

                foreach (DataRow row in rows)
                {
                    T item = CreateItem<T>(row);
                    list.Add(item);
                }
            }

            return list;
        }

        public IList<T> ConvertToList<T>(DataTable table)
        {
            if (table == null)
            {
                return null;
            }

            List<DataRow> rows = new List<DataRow>();

            foreach (DataRow row in table.Rows)
            {
                rows.Add(row);
            }

            return ConvertTo<T>(rows);
        }

        public T CreateItem<T>(DataRow row)
        {
            T obj = default(T);
            if (row != null)
            {
                obj = Activator.CreateInstance<T>();

                foreach (DataColumn column in row.Table.Columns)
                {
                    PropertyInfo prop = obj.GetType().GetProperty(column.ColumnName);
                    try
                    {
                        object value = row[column.ColumnName];
                        prop.SetValue(obj, value, null);
                    }
                    catch
                    {
                        // You can log something here
                        throw;
                    }
                }
            }

            return obj;
        }

        public DataTable CreateTable<T>()
        {
            Type entityType = typeof(T);
            DataTable table = new DataTable(entityType.Name);
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);

            foreach (PropertyDescriptor prop in properties)
            {
                table.Columns.Add(prop.Name, prop.PropertyType);
            }

            return table;
        }
        #endregion

        #region >>특정월에 요일에 해당 하는 일자 가져오기
        /// <summary>
        /// 특정월에 요일에 해당 하는 일자 가져오기
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="weeks"></param>
        /// <returns></returns>
        public  List<DateTime> GetDates(int year, int month, params int[] weeks)
        {
            var list = Enumerable.Range(1, DateTime.DaysInMonth(year, month))
                             .Select(day => new DateTime(year, month, day))
                             ;
            foreach(int nweek in weeks)
            {
                list = list.Where(w => w.DayOfWeek == (DayOfWeek)nweek);
            }
            return list.ToList();
        }
        public List<DateTime> GetDates(string year, string month, params int[] weeks)
        {
            var list = Enumerable.Range(1, DateTime.DaysInMonth(Convert.ToInt32(year), Convert.ToInt32(month)))
                             .Select(day => new DateTime(Convert.ToInt32(year), Convert.ToInt32(month), day))
                             ;
            foreach (int nweek in weeks)
            {
                list = list.Where(w => w.DayOfWeek == (DayOfWeek)nweek);
            }
            return list.ToList();
        }
        #endregion
    }
}
