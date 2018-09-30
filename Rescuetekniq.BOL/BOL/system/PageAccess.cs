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
    public sealed class PageAccessModule
    {
        public static int PageAccessDelete(string PageUrl, string ApplicationName)
        {
            return PageAccessClass.PageAccessDelete(PageUrl, ApplicationName);
        }
        
        public static string get_PageAccess(string PageUrl, string ApplicationName)
        {
            return PageAccessClass.get_PageAccess(PageUrl, ApplicationName);
        }
        public static void set_PageAccess(string PageUrl, string ApplicationName, string value)
        {
            PageAccessClass.set_PageAccess(PageUrl, ApplicationName, value);
        }
        
        public static string PageAccessDef(string PageUrl, string def, string ApplicationName)
        {
            string role = AdgangsKontrol.get_AccessRole(def);
            return PageAccessClass.PageAccessDef(PageUrl, role, ApplicationName);
        }
        
    }
    
    
    public class PageAccessClass : BaseObject
    {
        
#region  Privates
        
        private string _ApplicationName;
        private string _Page;
        private string _Access;
        
#endregion
        
#region  Publics
        
        public string ApplicationName
        {
            get
            {
                return _ApplicationName;
            }
            set
            {
                _ApplicationName = value;
            }
        }
        
        public string Page
        {
            get
            {
                return _Page;
            }
            set
            {
                _Page = value;
            }
        }
        
        public string Access
        {
            get
            {
                return _Access;
            }
            set
            {
                _Access = value;
            }
        }
        
#endregion
        
#region  Stored Procedure Names
        private const string _SQLDelete = "Co2Db_PageAccess_Delete";
        private const string _SQLUpdate = "Co2Db_PageAccess_Update";
        
        private const string _SQLGet = "Co2Db_PageAccess_Get";
        private const string _SQLGetOut = "Co2Db_PageAccess_GetOut";
#endregion
        
#region  New
        
        public PageAccessClass()
        {
        }
        
#endregion
        
#region  PageAccess
        
        public static int PageAccessDelete(string PageUrl, string ApplicationName)
        {
            
            DBAccess db = new DBAccess();
            db.AddNVarChar("ApplicationName", ApplicationName, 256);
            db.AddNVarChar("Page", PageUrl, 250);
            return db.ExecuteNonQuery(_SQLDelete);
            
        }
        
        
        public static string get_PageAccess(string PageUrl, string ApplicationName)
        {
            string res = "";
            DBAccess db = new DBAccess();
            
            db.AddNVarChar("ApplicationName", ApplicationName, 256);
            db.AddNVarChar("Page", PageUrl, 250);
            
            //db.addGetString("Value")
            SqlParameter value = new SqlParameter("@Access", 0);
            value.Direction = ParameterDirection.Output;
            value.SqlDbType = SqlDbType.NVarChar;
            value.Size = 250;
            db.AddParameter(value);
            
            db.ExecuteNonQuery(_SQLGetOut);
            
            res = Funktioner.ToDbString(value.Value);
            return res.Trim();
            
        }
        public static void set_PageAccess(string PageUrl, string ApplicationName, string value)
        {
            DBAccess db = new DBAccess();
            PageAccessClass p = new PageAccessClass();
            
            db.AddNVarChar("ApplicationName", ApplicationName, 256);
            db.AddNVarChar("Page", PageUrl, 250);
            db.AddNVarChar("Access", value, 250);
            
            db.AddNVarChar("RettetAF", p.RettetAf, 50);
            db.AddNVarChar("RettetIP", p.RettetIP, 15);
            db.addGetInt("ID");
            
            db.ExecuteNonQuery(_SQLUpdate);
        }
        
        public static string PageAccessDef(string PageUrl, string def, string ApplicationName)
        {
            string res = get_PageAccess(PageUrl, ApplicationName);
            if (string.IsNullOrEmpty(res))
            {
                res = def;
                set_PageAccess(PageUrl, ApplicationName, def);
            }
            return res;
        }
        
#endregion
        
    }
}
