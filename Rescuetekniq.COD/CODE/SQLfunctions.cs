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
using RescueTekniq.CODE;



namespace RescueTekniq.CODE
{
    public sealed class SQLfunctions
    {
        
        public static string SQLstr(string res)
        {
            return SQLstring(res);
        }
        public static string SQLstring(string res)
        {
            if (ReferenceEquals(res, null))
            {
                res = "";
            }
            if (res.Length < 1)
            {
                res = "";
            }
            res = res.Replace("'", "´");
            res = res.Replace("--", "&dash;&dash;");
            return res;
        }
        
        public static Nullable<DateTime> SQLdate(object value)
        {
            return SQLdatetime(value);
        }
        public static Nullable<DateTime> SQLdatetime(object value)
        {
            //Default instance of Nullable does not contain a value
            Nullable<DateTime> res = new Nullable<DateTime>();
            if (value != null)
            {
                if (value is DateTime)
                {
                    if (Funktioner.ToDec(value) == 0)
                    {
                        //res = Nothing
                    }
                    else if ((DateTime) value == DateTime.MinValue || (DateTime) value == DateTime.MaxValue || (DateTime) value == new DateTime(2018, 8, 6, 12, 0, 0))
                    {
                        //res = Nothing
                    }
                    else
                    {
                        res = (DateTime) value;
                    }
                }
                else if (Information.IsDate(value))
                {
                    if (Funktioner.ToDec(value) == 0)
                    {
                        //res = Nothing
                    }
                    else if ((DateTime) value == DateTime.MinValue || (DateTime) value == DateTime.MaxValue || (DateTime) value == new DateTime(2018, 8, 6, 12, 0, 0))
                    {
                        //res = Nothing
                    }
                    else
                    {
                        res = System.Convert.ToDateTime(value);
                    }
                }
            }
            return res;
        }
        
    }
    
    //Module DBdatareader
    
    //    Function DBtoInt(ByRef dr As SqlDataReader, ByVal felt As String) As Integer
    //        Return DBtoInteger(dr, felt)
    //    End Function
    //    Function DBtoInteger(ByRef dr As SqlDataReader, ByVal felt As String) As Integer
    //        Dim res As Integer
    //        Try
    //            Dim i As Integer = dr.GetOrdinal(felt)
    //            res = ToInteger(dr.GetValue(i))
    //        Catch
    //        End Try
    //        Return res
    //    End Function
    
    //    Function DBtoBit(ByRef dr As SqlDataReader, ByVal felt As String) As Boolean
    //        Return DBtoBool(dr, felt)
    //    End Function
    //    Function DBtoBool(ByRef dr As SqlDataReader, ByVal felt As String) As Boolean
    //        Return DBtoBoolean(dr, felt)
    //    End Function
    //    Function DBtoBoolean(ByRef dr As SqlDataReader, ByVal felt As String) As Boolean
    //        Dim res As Boolean
    //        Try
    //            Dim i As Integer = dr.GetOrdinal(felt)
    //            res = ToBoolean(dr.GetValue(i))
    //        Catch
    //        End Try
    //        Return res
    //    End Function
    
    //    Function DBtoLong(ByRef dr As SqlDataReader, ByVal felt As String) As Long
    //        Dim res As Long
    //        Try
    //            Dim i As Integer = dr.GetOrdinal(felt)
    //            res = ToLong(dr.GetValue(i))
    //        Catch
    //        End Try
    //        Return res
    //    End Function
    
    //    Function DBtoFloat(ByRef dr As SqlDataReader, ByVal felt As String) As Decimal
    //        Return DBtoDecimal(dr, felt)
    //    End Function
    //    Function DBtoDecimal(ByRef dr As SqlDataReader, ByVal felt As String) As Decimal
    //        Dim res As Decimal
    //        Try
    //            Dim i As Integer = dr.GetOrdinal(felt)
    //            res = ToDecimal(dr.GetValue(i))
    //        Catch
    //        End Try
    //        Return res
    //    End Function
    //    Function DBtoDouble(ByRef dr As SqlDataReader, ByVal felt As String) As Double
    //        Dim res As Double
    //        Try
    //            Dim i As Integer = dr.GetOrdinal(felt)
    //            res = ToDouble(dr.GetValue(i))
    //        Catch
    //        End Try
    //        Return res
    //    End Function
    
    //    Function DBtoDate(ByRef dr As SqlDataReader, ByVal felt As String) As Date
    //        Return DBtoDateTime(dr, felt)
    //    End Function
    //    Function DBtoDateTime(ByRef dr As SqlDataReader, ByVal felt As String) As Date
    //        Dim res As Date '= #12:00:00 PM#
    //        Try
    //            Dim i As Integer = dr.GetOrdinal(felt)
    //            res = ToDate(dr.GetValue(i))
    //        Catch
    //        End Try
    //        Return res
    //    End Function
    //    Function DBtoGuid(ByRef dr As SqlDataReader, ByVal felt As String) As Guid
    //        Dim res As Guid = Guid.Empty
    //        Try
    //            Dim i As Integer = dr.GetOrdinal(felt)
    //            res = ToGuid(dr.GetGuid(i).ToString)
    //        Catch
    //        End Try
    //        Return res
    //    End Function
    
    //    Function DBtoNVarChar(ByRef dr As SqlDataReader, ByVal felt As String) As String
    //        Return DBtoString(dr, felt)
    //    End Function
    //    Function DBtoVarChar(ByRef dr As SqlDataReader, ByVal felt As String) As String
    //        Return DBtoString(dr, felt)
    //    End Function
    //    Function DBtoString(ByRef dr As SqlDataReader, ByVal felt As String) As String
    //        Dim res As String = ""
    //        Try
    //            Dim i As Integer = dr.GetOrdinal(felt)
    //            res = ToDbString(dr.GetValue(i))
    //        Catch
    //        End Try
    //        Return res
    //    End Function
    
    //End Module
    
    
    
}
