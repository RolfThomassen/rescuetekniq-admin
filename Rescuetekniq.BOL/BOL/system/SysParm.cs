// VBConversions Note: VB project level imports
using System.Data;
using System.Diagnostics;
using System.Web.Security;
using System.Collections.Generic;
using RescueTekniq.CODE;
using System.Xml.Linq;
using Microsoft.VisualBasic;
using System.Configuration;
using System.Collections;
using RescueTekniq.CODE.DAL;
using System.Web;
using RescueTekniq.BOL;
using System;
using System.Web.Caching;
using System.Linq;
// End of VB project level imports

using System.Data.SqlClient;



namespace RescueTekniq.BOL
{
#region  SysParm
    
    public sealed class SysparmModule
    {
        public static string get_SysParm(string Param)
        {
            DBAccess db = new DBAccess();
            SqlParameter value = new SqlParameter("@Value", 0);
            string res = "";
            value.Direction = ParameterDirection.Output;
            value.SqlDbType = SqlDbType.NVarChar;
            value.Size = 250;
            
            db.AddParameter("@ApplicationName", SQLfunctions.SQLstr(Roles.ApplicationName));
            db.AddParameter("@Title", SQLfunctions.SQLstr(Param));
            db.AddParameter(value);
            db.ExecuteNonQuery("Co2Db_Sysparm_GetOut");
            res = Funktioner.ToDbString(value.Value);
            return res.Trim();
            //If Not (res.Trim = "") Then
            //	Return res 'ToDbString(value.Value)
            //Else
            //	Return "" 'Nothing
            //End If
        }
        public static void set_SysParm(string Param, string value)
        {
            DBAccess db = new DBAccess();
            db.AddParameter("@ApplicationName", SQLfunctions.SQLstr(Roles.ApplicationName));
            db.AddParameter("@Title", SQLfunctions.SQLstr(Param));
            db.AddParameter("@Value", SQLfunctions.SQLstr(value));
            db.ExecuteNonQuery("Co2Db_Sysparm_Update");
        }
        
        public static string SysParmDef(string Param, string def)
        {
            string res = get_SysParm(Param);
            if (string.IsNullOrEmpty(res))
            {
                res = def;
                set_SysParm(Param, def);
            }
            return res;
        }
        
        public static int SysParmDelete(string Param)
        {
            DBAccess db = new DBAccess();
            db.AddParameter("@ApplicationName", SQLfunctions.SQLstr(Roles.ApplicationName));
            db.AddParameter("@Title", Param);
            return db.ExecuteNonQuery("Co2Db_SysParm_Delete");
        }
        
        public static bool DoneHousekeeping
        {
            get
            {
                return bool.Parse(SysParmDef("DoneHousekeeping", System.Convert.ToString(true)));
            }
            set
            {
                set_SysParm("DoneHousekeeping", value.ToString());
            }
        }
        
        public static DateTime HousekeepingTime
        {
            get
            {
                return Funktioner.ToDateDef(get_SysParm("HousekeepingTime"), DateAndTime.DateAdd(DateInterval.Day, -1, DateTime.Now));
            }
            set
            {
                set_SysParm("HousekeepingTime", value.ToString("yyyy-MM-dd HH:mm:ss"));
            }
        }
    }
    
#endregion
    
}
