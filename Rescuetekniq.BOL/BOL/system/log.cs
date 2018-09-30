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
using System.Text;

namespace RescueTekniq.BOL
{
    
    public enum LogTypeEnum
    {
        Normal,
        @Error,
        Critical
    }
    
    public sealed class Log
    {
        public static void AddLog(string status, string logtext, string cvrnr = "", LogTypeEnum logtype = LogTypeEnum.Normal, string Metode = "")
        {
            LogLine log = new LogLine();
            log.Status = status;
            log.Cvrnr = cvrnr;
            log.LogText = logtext;
            log.LogType = logtype;
            log.Metode = Metode;
            log.Insert();
        }
    }
    
    public class LogLine : BaseObject
    {
        
#region  New
        
        public LogLine()
        {
            ClientInfo = GetIPinfo();
        }
        
#endregion
        
#region  Private
        
        private string _Status; //   50
        private string _Cvrnr; // 15
        private string _Metode; // 50
        private string _LogText; // max
        private string _ClientInfo; // 1023
        private LogTypeEnum _LogType;
        
#endregion
        
#region  Public
        
        public string Status // 50
        {
            get
            {
                return _Status;
            }
            set
            {
                _Status = value;
            }
        }
        
        public string Cvrnr // 15
        {
            get
            {
                return _Cvrnr;
            }
            set
            {
                _Cvrnr = value; //   .Substring(0, 15)
            }
        }
        
        public string Metode // 50
        {
            get
            {
                return _Metode;
            }
            set
            {
                _Metode = value; //   .Substring(0, 50)
            }
        }
        
        public string LogText // Max
        {
            get
            {
                return _LogText;
            }
            set
            {
                _LogText = value;
            }
        }
        
        public string ClientInfo // 1023
        {
            get
            {
                return _ClientInfo;
            }
            set
            {
                _ClientInfo = value;
            }
        }
        
        public LogTypeEnum LogType
        {
            get
            {
                return _LogType;
            }
            set
            {
                _LogType = value;
            }
        }
        
#endregion
        
#region  Shared Populate
        
        private static void AddParms(ref DBAccess db, LogLine c)
        {
            var with_1 = c;
            db.AddNVarChar("Status", with_1.Status, 50);
            db.AddNVarChar("Metode", with_1.Metode, 50);
            db.AddNVarChar("Cvrnr", with_1.Cvrnr, 50);
            db.AddNVarChar("LogText", with_1.LogText, -1);
            db.AddNVarChar("ClientInfo", with_1.ClientInfo, 1023);
            db.AddInt("LogType", (System.Int32) with_1.LogType);
            AddParmsStandard(db, c);
        }
        
        protected static void Populate(SqlDataReader dr, LogLine c)
        {
            PopulateStandard(dr, c);
            var with_1 = c;
            with_1.Status = dr.DBtoString("Status");
            with_1.Metode = dr.DBtoString("Metode");
            with_1.Cvrnr = dr.DBtoString("Cvrnr");
            with_1.LogText = dr.DBtoString("LogText");
            with_1.ClientInfo = dr.DBtoString("ClientInfo");
            with_1.LogType = (RescueTekniq.BOL.LogTypeEnum) (dr.DBtoInt("LogType"));
        }
        
#endregion
        
#region  Stored Procedure Names
        private const string _SQLDelete = "Co2Db_LogLine_Delete";
        private const string _SQLInsert = "Co2Db_LogLine_Insert";
        private const string _SQLUpdate = "Co2Db_LogLine_Update";
        
        private const string _SQLSelectAll = "Co2Db_LogLine_SelectAll";
        
        private const string _SQLSelectID = "Co2Db_LogLine_SelectByID";
        private const string _SQLSelectOne = "Co2Db_LogLine_SelectOne";
        
#endregion
        
#region  Public Shared Data Metodes
        
        public int Insert()
        {
            return Insert(this);
        }
        public static int Insert(LogLine c)
        {
            DBAccess db = new DBAccess();
            
            AddParms(ref db, c);
            SqlParameter pID = new SqlParameter("@ID", 0);
            pID.Direction = ParameterDirection.Output;
            db.AddParameter(pID);
            
            //Return db.ExecuteNonQuery(_SQLInsert)
            int retval = db.ExecuteNonQuery(_SQLInsert);
            if (retval == 1)
            {
                c.ID = int.Parse(pID.Value.ToString());
                return c.ID; //Integer.Parse(pID.Value.ToString)
            }
            else
            {
                return -1;
            }
        }
        public static int Insert(string Status, string Cvrnr, string LogText, LogTypeEnum LogType, string Metode = "")
        {
            LogLine cb = new LogLine();
            cb.Status = Status;
            cb.Cvrnr = Cvrnr;
            cb.LogText = LogText;
            cb.LogType = LogType;
            return Insert(cb);
        }
        
        //   ALTER PROCEDURE [vicjos1_sysadm].[Co2Db_Logline_Insert]
        //   	@Status nvarchar(50)
        //   ,	@Cvrnr nvarchar(15) = null
        //   ,	@LogText nvarchar(max) = null
        //   ,	@ClientInfo nvarchar(1023) = null
        //   ,	@RettetAf nvarchar(50) = 'admin'
        //   ,	@RettetIP nvarchar(15) = '127.0.0.1'
        //   ,	@LogType int = 0
        //   ,	@ID int OUTPUT
        
        public static void LogLine_Renamed(string status, string cvrnr, string logtext, LogTypeEnum logtype, string Metode = "")
        {
            Insert(status, cvrnr, logtext, logtype, Metode);
        }
        
#endregion
        
#region  Old info
        
        //##############################################################################
        //##
        //##  Add Log status
        //##
        //##############################################################################
        //	SELECT lID, lDate, lIP, lCvrnr, lLogin, lRole, lStatus, lText, lNew, lWorkstation FROM [logline]
        
        //Function LogLine(ByVal sStatus, ByVal sText)
        //    Dim oCnLog, oRsLog, strSQL, sIP
        
        //    oCnLog = Server.CreateObject("ADODB.Connection")
        //    oCnLog = DBConnexion
        //    oRsLog = Server.CreateObject("ADODB.RecordSet")
        //    With oRsLog
        //        .Open("SELECT * FROM logline WHERE 0=1", oCnLog, adOpenKeyset, adLockOptimistic)
        //        .AddNew()
        //        .Fields("lIP") = SQLEncryptLen(reformatIP(Request.ServerVariables("REMOTE_ADDR")), .Fields("lIP").DefinedSize)
        //        .Fields("lCvrnr") = SQLEncryptLen(session("usercvrnr"), .Fields("lCvrnr").DefinedSize)
        //        .Fields("lLogin") = SQLEncryptLen(session("userinitials"), .Fields("lLogin").DefinedSize)
        //        .Fields("lRole") = SQLEncryptLen(session("userstatus"), .Fields("lRole").DefinedSize)
        //        .Fields("lStatus") = SQLEncryptLen(sStatus, .Fields("lStatus").DefinedSize)
        //        .Fields("lText") = SQLEncryptLen(sText, .Fields("lText").DefinedSize)
        //        .Fields("lWorkstation") = SQLencryptLen(GetIPinfo, .Fields("lWorkstation").DefinedSize)
        //        .Update()
        //    End With
        
        //    If oRsLog.State = adStateOpen Then oRsLog.Close()
        //    oRsLog = Nothing
        
        //    If oCnLog.State = adStateOpen Then oCnLog.Close()
        //    oCnLog = Nothing
        //End Function
        
        //Function DeleteOldLoglines()
        //    Const LogOlderThan6MdrDeleted = "LogOlderThan6MdrDeleted"
        //    If Session("DeleteOldLoglines") <> LogOlderThan6MdrDeleted Then
        //        DebugPrint("DELETE FROM logline WHERE lDate<'" & MSSQLdatetime(DateAdd("M", -6, Now)) & "'")
        //        DBexecute("DELETE FROM logline WHERE lDate<'" & MSSQLdatetime(DateAdd("M", -6, Now)) & "'")
        //        Session("DeleteOldLoglines") = LogOlderThan6MdrDeleted
        //    End If
        //End Function
        
        //'##############################################################################
        //'##
        //'##  Add Log status
        //'##
        //'##############################################################################
        //Function LogLine(ByVal sStatus, ByVal sText)
        //    Dim oCnLog, oRsLog, strSQL, sIP
        
        //    If Request.ServerVariables("REMOTE_ADDR") <> "" Then
        //        '	sText = sText & " [IP:" & Request.ServerVariables("REMOTE_ADDR") & "]"
        //    End If
        //    If Request.ServerVariables("HTTP_X_FORWARDED_FOR") <> "" Then
        //        '	sText = sText & " [FORWARDED IP:" & Request.ServerVariables("HTTP_X_FORWARDED_FOR") & "]"
        //    End If
        //    If Request.ServerVariables("HTTP_CLIENT_IP") <> "" Then
        //        '	sText = sText & " [CLIENT IP:" & Request.ServerVariables("HTTP_CLIENT_IP") & "]"
        //    End If
        
        //    oCnLog = Server.CreateObject("ADODB.Connection")
        //    oCnLog = DBConnexion
        //    oRsLog = Server.CreateObject("ADODB.RecordSet")
        //    oRsLog.Open("select * from log where 0=1", oCnLog, adOpenKeyset, adLockOptimistic)
        //    With oRsLog
        //        .AddNew()
        //        .Fields("lLogin") = SQLEncrypt(session("userinitials"))
        //        .Fields("lCvrnr") = SQLEncrypt(session("usercvrnr"))
        //        .Fields("lRole") = SQLEncrypt(session("userstatus"))
        //        .Fields("lStatus") = SQLEncrypt(sStatus)
        //        .Fields("lText") = SQLEncrypt(sText)
        //        .Fields("lDate") = now
        //        .Fields("lIP") = Request.ServerVariables("REMOTE_ADDR") 'sIP
        //        .Update()
        //    End With
        
        //    If oRsLog.State = adStateOpen Then oRsLog.Close()
        //    oRsLog = Nothing
        
        //    If oCnLog.State = adStateOpen Then oCnLog.Close()
        //    oCnLog = Nothing
        //End Function
        
#endregion
        
#region  Public Metodes
        
        public static string GetIPinfo()
        {
            string res = "";
            string tmp = "";
            StringBuilder sb = new StringBuilder();
            
            
            tmp = CurrentServerVariables("REMOTE_ADDR");
            if (tmp.Trim() != "")
            {
                sb.Append("<br />IP:" + tmp);
            }
            
            tmp = CurrentServerVariables("HTTP_X_FORWARDED_FOR");
            if (tmp.Trim() != "")
            {
                sb.Append("<br />Http X Forwarded For:" + tmp);
            }
            
            tmp = CurrentServerVariables("HTTP_CLIENT_IP");
            if (tmp.Trim() != "")
            {
                sb.Append("<br />Http Client Ip:" + tmp);
            }
            
            tmp = CurrentServerVariables("REMOTE_HOST");
            if (tmp.Trim() != "")
            {
                sb.Append("<br />Remote host:" + tmp);
            }
            
            tmp = CurrentServerVariables("HTTP_REFERER");
            if (tmp.Trim() != "")
            {
                sb.Append("<br />Http Referer:" + tmp);
            }
            else
            {
                sb.Append("<br />Http Referer: Direkt to page from outsite");
            }
            
            tmp = CurrentServerVariables("SCRIPT_NAME");
            if (tmp.Trim() != "")
            {
                sb.Append("<br />Script Name:" + tmp);
                tmp = CurrentServerVariables("QUERY_STRING");
                if (tmp.Trim() != "")
                {
                    sb.Append("<br />?" + tmp);
                }
            }
            
            if (DebugModule.DebugMode)
            {
                tmp = CurrentServerVariables("ALL_RAW");
                if (tmp.Trim() != "")
                {
                    sb.Append("<br />All Raw:" + tmp);
                }
            }
            
            res = sb.ToString();
            if (res.StartsWith("<br />"))
            {
                res = res.Substring(6);
            }
            
            return res;
        }
        
#endregion
        
#region  Get Data
        
        public static LogLine GetLogLineByID(int id)
        {
            return GetLogLineByCriteria("", "[ID]=@ID", new SqlParameter("@ID", id));
        }
        public static LogLine GetLogLineByCriteria(string OrderBY, string criteria, params SqlParameter[] @params)
        {
            List<LogLine> list = GetLogLinesByCriteria(OrderBY, criteria, @params);
            if (list.Count > 0)
            {
                return list[0];
            }
            else
            {
                return new LogLine(); //Nothing
            }
        }
        public static List<LogLine> GetLogLinesByCriteria(string OrderBY, string criteria, params SqlParameter[] @params)
        {
            DBAccess db = new DBAccess();
            //Dim conn As SqlConnection = DataFunctions.GetConnection()
            string query = "";
            query += "SELECT ";
            if (OrderBY != "")
            {
                query += " TOP (100) PERCENT ";
            }
            query += " * FROM vw_Co2Db_LogLine ";
            if (criteria != "")
            {
                query += " WHERE (" + criteria + ")";
            }
            if (OrderBY != "")
            {
                query += " ORDER BY " + OrderBY;
            }
            
            db.CommandType = CommandType.Text;
            db.CommandText = query;
            db.AddRange(@params);
            
            db.Open();
            SqlDataReader dr = (SqlDataReader) (db.ExecuteReader()); //cmd.ExecuteReader()
            
            List<LogLine> list = new List<LogLine>();
            while (dr.Read())
            {
                LogLine service = new LogLine();
                Populate(dr, service);
                list.Add(service);
            }
            
            db.Dispose();
            
            return list;
        }
        public static DataSet GetLogLinesByCriteriaDS(string fieldnames, string criteria, string GroupBY, params SqlParameter[] @params)
        {
            DBAccess db = new DBAccess();
            string query = "";
            query += "SELECT ";
            if (fieldnames.Trim() != "")
            {
                query += fieldnames;
            }
            else
            {
                query += " * ";
            }
            query += " FROM vw_Co2Db_LogLine ";
            if (criteria != "")
            {
                query += " WHERE (" + criteria + ")";
            }
            if (GroupBY != "")
            {
                query += " GROUP BY " + GroupBY;
            }
            
            db.CommandType = CommandType.Text;
            db.CommandText = query;
            db.AddRange(@params);
            
            DataSet ds = db.ExecuteDataSet(); //(_SQLSelectByGuid)
            return ds;
        }
        
#endregion
        
    }
    
}
