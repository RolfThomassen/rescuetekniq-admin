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
using System.Security.Principal;
using System.Text;


namespace RescueTekniq.BOL
{
    
    
    [Serializable()]public abstract class BaseObject
    {
        
        // ==========
        // Private variables
        // ==========
#region  Privates
        protected const int MAXROWS = int.MaxValue;
        
        private int _id = -1;
        private System.Guid _Guid = System.Guid.Empty;
        
        private string _RettetAf; // VBConversions Note: Initial value cannot be assigned here since it is non-static.  Assignment has been moved to the class constructors.
        private System.Guid _RettetAfGuid; // VBConversions Note: Initial value cannot be assigned here since it is non-static.  Assignment has been moved to the class constructors.
        private string _RettetIP; // VBConversions Note: Initial value cannot be assigned here since it is non-static.  Assignment has been moved to the class constructors.
        private DateTime _RettetDen; // VBConversions Note: Initial value cannot be assigned here since it is non-static.  Assignment has been moved to the class constructors.
        
        private string _OprettetAf; // VBConversions Note: Initial value cannot be assigned here since it is non-static.  Assignment has been moved to the class constructors.
        private System.Guid _OprettetAfGuid; // VBConversions Note: Initial value cannot be assigned here since it is non-static.  Assignment has been moved to the class constructors.
        private string _OprettetIP; // VBConversions Note: Initial value cannot be assigned here since it is non-static.  Assignment has been moved to the class constructors.
        private DateTime _OprettetDen; // VBConversions Note: Initial value cannot be assigned here since it is non-static.  Assignment has been moved to the class constructors.
        
        private bool _IsDirty = false;
        
#endregion
        
        // ==========
        // Properties
        // ==========
#region  Properties
        public int ID
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }
        
        public System.Guid Guid
        {
            get
            {
                return _Guid;
            }
            set
            {
                _Guid = value;
            }
        }
        
        public bool isLoaded
        {
            get
            {
                return _id > 0;
            }
        }
        public bool loaded
        {
            get
            {
                return _id > 0;
            }
        }
        
        public string RettetAf
        {
            get
            {
                return _RettetAf;
            }
            protected set
            {
                _RettetAf = value;
            }
        }
        public System.Guid RettetAfGuid
        {
            get
            {
                return _RettetAfGuid;
            }
            set
            {
                _RettetAfGuid = value;
            }
        }
        
        public string RettetIP
        {
            get
            {
                return _RettetIP;
            }
            protected set
            {
                _RettetIP = value;
            }
        }
        
        public DateTime RettetDen
        {
            get
            {
                return _RettetDen;
            }
            protected set
            {
                _RettetDen = value;
            }
        }
        
        
        public string OprettetAf
        {
            get
            {
                return _OprettetAf;
            }
            protected set
            {
                _OprettetAf = value;
            }
        }
        
        public string OprettetIP
        {
            get
            {
                return _OprettetIP;
            }
            protected set
            {
                _OprettetIP = value;
            }
        }
        
        public System.Guid OprettetAfGuid
        {
            get
            {
                return _OprettetAfGuid;
            }
            set
            {
                _OprettetAfGuid = value;
            }
        }
        public DateTime OprettetDen
        {
            get
            {
                return _OprettetDen;
            }
            protected set
            {
                _OprettetDen = value;
            }
        }
        
        public string CurUser
        {
            get
            {
                return CurrentUserName;
            }
        }
        
        public string CurIP
        {
            get
            {
                return CurrentUserIP;
            }
        }
        
        public void Dirty()
        {
            _IsDirty = true;
        }
        public bool isDirty
        {
            get
            {
                return _IsDirty;
            }
            set
            {
                _IsDirty = value;
            }
        }
        
#endregion
        
        // ==========
        // Methods
        // ==========
#region  New
        
        protected BaseObject()
        {
            // VBConversions Note: Non-static class variable initialization is below.  Class variables cannot be initially assigned non-static values in C#.
            _RettetAf = CurrentUserName;
            _RettetAfGuid = CurrentUserID;
            _RettetIP = CurrentUserIP;
            _RettetDen = DateTime.Now;
            _OprettetAf = CurrentUserName;
            _OprettetAfGuid = CurrentUserID;
            _OprettetIP = CurrentUserIP;
            _OprettetDen = DateTime.Now;
            
            _id = -1;
            _Guid = Guid.NewGuid();
            
            _RettetAf = CurrentUserName;
            _RettetAfGuid = CurrentUserID;
            _RettetIP = CurrentUserIP;
            _RettetDen = DateTime.Now;
            
            _OprettetAf = CurrentUserName;
            _OprettetAfGuid = CurrentUserID;
            _OprettetIP = CurrentUserIP;
            _OprettetDen = DateTime.Now;
            
            _IsDirty = false;
        }
        
#endregion
        
#region  Protected metodes
        
        protected static void AddLog(string Status, string Logtext, LogTypeEnum logtype = LogTypeEnum.Normal, string Metode = "")
        {
            Log.AddLog(status: Status, logtext: Logtext, logtype: logtype, Metode: Metode);
        }
        //Protected Sub AddLog(ByVal Status As String, ByVal Logtext As String, Optional ByVal logtype As LogTypeEnum = LogTypeEnum.Normal)
        //    Log.AddLog(Status, Logtext, logtype)
        //End Sub
        
#endregion
        
#region  Standard Populate
        
        protected static object AddParmsStandard(DBAccess db, BaseObject rec)
        {
            
            db.AddNVarChar("RettetAf", rec.CurUser, 50);
            db.AddNVarChar("RettetIP", rec.CurIP, 15);
            
            return rec;
        }
        
        protected static BaseObject PopulateStandard(IDataReader dr, BaseObject rec)
        {
            rec.ID = System.Convert.ToInt32(dr.DBtoInt("ID"));
            rec.Guid = dr.DBtoGuid("Guid");
            
            rec.RettetAf = dr.DBtoString("RettetAf");
            rec.RettetDen = System.Convert.ToDateTime(dr.DBtoDate("RettetDen"));
            rec.RettetIP = dr.DBtoString("RettetIP");
            
            rec.OprettetAf = dr.DBtoString("OprettetAf");
            rec.OprettetDen = System.Convert.ToDateTime(dr.DBtoDate("OprettetDen"));
            rec.OprettetIP = dr.DBtoString("OprettetIP");
            return rec;
        }
        
        //Protected Shared Sub PopulateStandard(ByRef dr As SqlDataReader, ByRef rec As BaseObject)
        //    With rec
        //        .ID = dr.DBtoInt("ID")
        //        .Guid = dr.DBtoGuid("Guid")
        
        //        .RettetAf = dr.DBtoString("RettetAf")
        //        .RettetDen = dr.DBtoDate("RettetDen")
        //        .RettetIP = dr.DBtoString("RettetIP")
        
        //        .OprettetAf = dr.DBtoString("OprettetAf")
        //        .OprettetDen = dr.DBtoDate("OprettetDen")
        //        .OprettetIP = dr.DBtoString("OprettetIP")
        //    End With
        //End Sub
        
        //Protected Shared Sub PopulateStandard(ByRef dr As DataTableReader, ByRef rec As BaseObject)
        //    With rec
        //        .ID = dr.DBtoInt("ID")
        //        .Guid = dr.DBtoGuid("Guid")
        
        //        .RettetAf = dr.DBtoString("RettetAf")
        //        .RettetDen = dr.DBtoDate("RettetDen")
        //        .RettetIP = dr.DBtoString("RettetIP")
        
        //        .OprettetAf = dr.DBtoString("OprettetAf")
        //        .OprettetDen = dr.DBtoDate("OprettetDen")
        //        .OprettetIP = dr.DBtoString("OprettetIP")
        //    End With
        //End Sub
        
#endregion
        
#region  Shared Metodes
        
#region  Cache
        
        //Protected Shared ReadOnly Property Settings() As MedarbejderGrupperElement
        //    Get
        //        Return Globale.Settings.MedarbejderGrupper
        //    End Get
        //End Property
        
        //' Cache the input data, if caching is enabled
        //Protected Shared Sub CacheData(ByVal key As String, ByVal data As Object)
        //    If Settings.EnableCaching AndAlso Not IsNothing(data) Then
        //        BaseObject.Cache.Insert(key, data, Nothing, DateTime.Now.AddSeconds(Settings.CacheDuration), TimeSpan.Zero)
        //    End If
        //End Sub
        
        protected static Cache Cache
        {
            get
            {
                return HttpContext.Current.Cache;
            }
        }
        
        protected static void PurgeCacheItems(string prefix)
        {
            prefix = prefix.ToLower();
            List<string> itemsToRemove = new List<string>();
            
            IDictionaryEnumerator enumerator = BaseObject.Cache.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (enumerator.Key.ToString().ToLower().StartsWith(prefix))
                {
                    itemsToRemove.Add(enumerator.Key.ToString());
                }
            }
            
            foreach (string itemToRemove in itemsToRemove)
            {
                BaseObject.Cache.Remove(itemToRemove);
            }
        }
        
#endregion
        
#region  Currentuser
        
        protected static bool IsInRole(string rolelist)
        {
            string[] roles = rolelist.Split(',');
            bool inRole = false;
            try
            {
                foreach (string role in roles)
                {
                    if (role.Trim() != "")
                    {
                        if (HttpContext.Current.User.IsInRole(role.Trim()))
                        {
                            inRole = true;
                        }
                    }
                }
            }
            catch
            {
            }
            return inRole;
        }
        
        protected static IPrincipal CurrentUser
        {
            get
            {
                return HttpContext.Current.User;
            }
        }
        
        protected static Guid CurrentUserID
        {
            get
            {
                Guid UserID = Guid.Empty;
                MembershipUser User = default(MembershipUser);
                try
                {
                    User = Membership.GetUser(CurrentUserName);
                    UserID = (System.Guid) User.ProviderUserKey;
                }
                catch
                {
                }
                return UserID;
            }
        }
        
        protected static string CurrentUserName
        {
            get
            {
                string userName = "";
                
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    userName = HttpContext.Current.User.Identity.Name;
                }
                return userName;
            }
        }
        
        protected static string CurrentUserIP
        {
            get
            {
                return HttpContext.Current.Request.UserHostAddress;
            }
        }
        
        protected static string CurrentUserHostName
        {
            get
            {
                return HttpContext.Current.Request.UserHostName;
            }
        }
        
        protected static string CurrentServerVariables(string item)
        {
            string res = "";
            try
            {
                res = HttpContext.Current.Request.ServerVariables[item].ToString();
            }
            catch (Exception)
            {
            }
            return res;
        }
        
#endregion
        
#region  Metodes
        
        protected static int GetPageIndex(int startRowIndex, int maximumRows)
        {
            if (maximumRows <= 0)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(Math.Floor(Convert.ToDouble(startRowIndex) / Convert.ToDouble(maximumRows)));
            }
        }
        
        protected static string EncodeText(string content)
        {
            content = HttpUtility.HtmlEncode(content);
            content = content.Replace("  ", "&nbsp;&nbsp;");
            content = content.Replace("\\n", "<br />");
            content = content.Replace(Constants.vbNewLine, "<br />");
            content = content.Replace(System.Convert.ToString("\t"), "&nbsp;&nbsp;&nbsp;");
            return content;
        }
        
        protected static string ConvertNullToEmptyString(string input)
        {
            if (input == null)
            {
                return "";
            }
            else
            {
                return input;
            }
        }
        
#endregion
        
#region  DB Export
        
        public static string DB_ExportCSV(DataSet ds, string strFileName = "export.csv", string strSep = ";", string strNull = "(NULL)")
        {
            
            string strLine = "";
            string strValue = "";
            object objValue = null;
            StringBuilder sb = new StringBuilder();
            
            if (strFileName == "")
            {
                strFileName = "export.csv";
            }
            if (strSep == "")
            {
                strSep = ";";
            }
            
            //Response.ContentType = "text/plain"
            //Response.AddHeader("content-disposition", "attachment;filename=" & strFileName)
            
            foreach (DataTable table in ds.Tables)
            {
                strLine = "";
                foreach (DataColumn item in table.Columns)
                {
                    if (!item.ColumnName.StartsWith("-"))
                    {
                        if (!string.IsNullOrEmpty(strLine))
                        {
                            strLine += strSep;
                        }
                        strLine += item.ColumnName;
                    }
                }
                sb.AppendLine(strLine);
                
                foreach (DataRow row in table.Rows)
                {
                    strLine = "";
                    
                    foreach (DataColumn dc in table.Columns)
                    {
                        
                        if (!dc.ColumnName.StartsWith("-"))
                        {
                            objValue = row[dc.ColumnName];
                            
                            if (DBNull.Value.Equals(row[dc.ColumnName]))
                            {
                                strValue = strNull;
                            }
                            else if (ReferenceEquals(objValue, null))
                            {
                                strValue = strNull;
                            }
                            else if (ReferenceEquals(dc.DataType, System.Type.GetType("System.Guid")))
                            {
                                strValue = objValue.ToString();
                            }
                            else if (string.IsNullOrEmpty(System.Convert.ToString(objValue)))
                            {
                                strValue = strNull;
                            }
                            else if (Information.IsDate(objValue))
                            {
                                strValue = Funktioner.ToSqlDateTime(objValue);
                                //ElseIf IsNumeric(objValue) Then
                                //   strValue = ToDecimal(objValue, 0).ToString
                            }
                            else
                            {
                                strValue = ((objValue + "").Trim()).ToString();
                                strValue = strValue.Replace(strSep, "|");
                                strValue = strValue.Replace("'", "Â´");
                                strValue = strValue.Replace(Constants.vbNewLine, " ");
                                strValue = strValue.Replace("\r\n", " ");
                                strValue = strValue.Replace(Constants.vbLf, " ");
                                strValue = strValue.Replace(Constants.vbCr, " ");
                            }
                            
                            if (!string.IsNullOrEmpty(strLine))
                            {
                                strLine += strSep;
                            }
                            strLine += strValue;
                        }
                    }
                    sb.AppendLine(strLine);
                }
            }
            
            return sb.ToString();
        }
        
#endregion
        
#endregion
        
    }
    
    //End Namespace
    
}
