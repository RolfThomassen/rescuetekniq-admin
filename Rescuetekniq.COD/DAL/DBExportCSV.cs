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

using System.Text;
using RescueTekniq.CODE.DAL;
using RescueTekniq.CODE;


namespace RescueTekniq.CODE
{
    
    public class DBExportCSV
    {
        
        public static string DS_ExportCSV_Fieldlist(DataSet ds, string FieldName, string strSep = ";")
        {
            StringBuilder sb = new StringBuilder();
            string strLine = "";
            
            if (strSep == "")
            {
                strSep = ";";
            }
            
            object item = null;
            string tmp = "";
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                
                strLine = "";
                foreach (string name in FieldName.Split(strSep.ToCharArray()[0]))
                {
                    item = row[name];
                    
                    tmp = (item + " ").Trim();
                    
                    tmp = tmp.Replace(Constants.vbNewLine, " ");
                    
                    if (strSep == ",")
                    {
                        tmp = tmp.Replace(",", ".");
                    }
                    else
                    {
                        tmp = tmp.Replace(strSep, ",");
                    }
                    
                    if (!string.IsNullOrEmpty(strLine))
                    {
                        strLine += strSep;
                    }
                    strLine += tmp;
                }
                
                sb.AppendLine(strLine);
            }
            
            return sb.ToString();
        }
        
        public static string DS_ExportCSV(DataSet ds, string strSep = ";")
        {
            string FieldName = "";
            string strLine = "";
            StringBuilder sb = new StringBuilder();
            
            if (strSep == "")
            {
                strSep = ";";
            }
            
            strLine = "";
            foreach (DataColumn items in ds.Tables[0].Columns)
            {
                if (!string.IsNullOrEmpty(strLine))
                {
                    strLine += strSep;
                }
                strLine += items.ColumnName;
            }
            FieldName = strLine;
            sb.AppendLine(strLine);
            
            sb.Append(DS_ExportCSV_Fieldlist(ds, FieldName, strSep));
            
            return sb.ToString();
        }
        
        
        public static string DB_ExportCSV(string strSQL, string strSep = ";")
        {
            DBAccess db = new DBAccess();
            
            
            db.CommandType = CommandType.Text;
            DataSet ds = db.ExecuteDataSet(strSQL);
            
            return DS_ExportCSV(ds, strSep);
            
        }
        
    }
    
}
