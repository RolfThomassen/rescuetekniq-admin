// VBConversions Note: VB project level imports
using System.Collections.Generic;
using System;
using System.Linq;
using System.Configuration;
using System.Diagnostics;
using Microsoft.VisualBasic;
using System.Xml.Linq;
using System.Collections;
using System.Data;
// End of VB project level imports

using System.Data.SqlClient;
using System.IO;
using System.Runtime.CompilerServices;
using RescueTekniq.CODE;


namespace RescueTekniq.CODE
{
    namespace DAL
    {
        
        public static class DBDataReaderFunctions
        {
            
            public static int DBtoInt(this IDataReader dr, string felt)
            {
                return DBtoInteger(dr, felt);
            }
            public static int DBtoInteger(this IDataReader dr, string felt)
            {
                int res = 0;
                try
                {
                    int i = dr.GetOrdinal(felt);
                    res = RescueTekniq.CODE.Funktioner.ToInteger(dr.GetValue(i));
                }
                catch
                {
                }
                return res;
            }
            
            public static bool DBtoBit(this IDataReader dr, string felt)
            {
                return DBtoBoolean(dr, felt);
            }
            public static bool DBtoBool(this IDataReader dr, string felt)
            {
                return DBtoBoolean(dr, felt);
            }
            public static bool DBtoBoolean(this IDataReader dr, string felt)
            {
                bool res = false;
                try
                {
                    int i = dr.GetOrdinal(felt);
                    res = RescueTekniq.CODE.Funktioner.ToBoolean(dr.GetValue(i));
                }
                catch
                {
                }
                return res;
            }
            
            public static long DBtoLong(this IDataReader dr, string felt)
            {
                long res = 0;
                try
                {
                    int i = dr.GetOrdinal(felt);
                    res = RescueTekniq.CODE.Funktioner.ToLong(dr.GetValue(i));
                }
                catch
                {
                }
                return res;
            }
            
            public static decimal DBtoFloat(this IDataReader dr, string felt)
            {
                return DBtoDecimal(dr, felt);
            }
            public static decimal DBtoDecimal(this IDataReader dr, string felt)
            {
                decimal res = new decimal();
                try
                {
                    int i = dr.GetOrdinal(felt);
                    res = RescueTekniq.CODE.Funktioner.ToDecimal(dr.GetValue(i));
                }
                catch
                {
                }
                return res;
            }
            public static double DBtoDouble(this IDataReader dr, string felt)
            {
                double res = 0;
                try
                {
                    int i = dr.GetOrdinal(felt);
                    res = RescueTekniq.CODE.Funktioner.ToDouble(dr.GetValue(i));
                }
                catch
                {
                }
                return res;
            }
            
            public static DateTime DBtoDate(this IDataReader dr, string felt)
            {
                return DBtoDateTime(dr, felt);
            }
            public static DateTime DBtoDateTime(this IDataReader dr, string felt)
            {
                DateTime res = default(DateTime); //= #12:00:00 PM#
                try
                {
                    int i = dr.GetOrdinal(felt);
                    res = RescueTekniq.CODE.Funktioner.ToDate(dr.GetValue(i));
                }
                catch
                {
                }
                return res;
            }
            
            public static Guid DBtoGuid(this IDataReader dr, string felt)
            {
                Guid res = Guid.Empty;
                try
                {
                    int i = dr.GetOrdinal(felt);
                    res = RescueTekniq.CODE.Funktioner.ToGuid(dr.GetGuid(i).ToString());
                }
                catch
                {
                }
                return res;
            }
            
            public static string DBtoNVarChar(this IDataReader dr, string felt)
            {
                return DBtoString(dr, felt);
            }
            public static string DBtoVarChar(this IDataReader dr, string felt)
            {
                return DBtoString(dr, felt);
            }
            public static string DBtoString(this IDataReader dr, string felt)
            {
                string res = "";
                try
                {
                    int i = dr.GetOrdinal(felt);
                    res = RescueTekniq.CODE.Funktioner.ToDbString(dr.GetValue(i));
                }
                catch
                {
                }
                return res;
            }
            
        }
        
    }
    
}
