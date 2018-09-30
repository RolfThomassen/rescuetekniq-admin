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
using RescueTekniq.CODE;

namespace RescueTekniq.CODE
{
    
    public class DBSQLfunction
    {
        
        
        protected const int SQLDBVersion = 2005;
        protected const int MaxNVarChar = 4000;
        protected const int MaxVarChar = 8000;
        public const int NVarCharMax = -1;
        public const int VarCharMax = -1;
        
        //Private cmd As IDbCommand = New SqlCommand()
        protected SqlCommand cmd = new SqlCommand();
        protected SqlConnection SQLConnection = new SqlConnection();
        
        public void addGetInt(string field)
        {
            if (!field.StartsWith("@"))
            {
                field = "@" + field;
            }
            SqlParameter Param = new SqlParameter(); //("@ID", 0)
            Param.ParameterName = field;
            Param.Direction = ParameterDirection.Output;
            Param.SqlDbType = SqlDbType.Int;
            Param.Value = 0;
            cmd.Parameters.Add(Param);
        }
        public void addGetInteger(string field)
        {
            addGetInt(field);
        }
        public void addGetVariant(string field)
        {
            if (!field.StartsWith("@"))
            {
                field = "@" + field;
            }
            SqlParameter Param = new SqlParameter();
            Param.ParameterName = field;
            Param.Direction = ParameterDirection.Output;
            Param.SqlDbType = SqlDbType.Variant;
            Param.Value = 0;
            cmd.Parameters.Add(Param);
        }
        public void addGetStr(string field)
        {
            if (!field.StartsWith("@"))
            {
                field = "@" + field;
            }
            SqlParameter Param = new SqlParameter();
            Param.ParameterName = field;
            Param.Direction = ParameterDirection.Output;
            Param.SqlDbType = SqlDbType.NVarChar;
            Param.Value = "";
            cmd.Parameters.Add(Param);
        }
        public void addGetString(string field)
        {
            addGetStr(field);
        }
        
        public void AddBoolean(string field, bool value)
        {
            if (!field.StartsWith("@"))
            {
                field = "@" + field;
            }
            cmd.Parameters.Add(field, SqlDbType.Bit).Value = value;
        }
        public void AddBit(string field, bool value)
        {
            if (!field.StartsWith("@"))
            {
                field = "@" + field;
            }
            cmd.Parameters.Add(field, SqlDbType.Bit).Value = value;
        }
        
        public void AddInt(string field, int value)
        {
            if (!field.StartsWith("@"))
            {
                field = "@" + field;
            }
            cmd.Parameters.Add(field, SqlDbType.Int).Value = value;
        }
        public void AddBigInt(string field, long value)
        {
            if (!field.StartsWith("@"))
            {
                field = "@" + field;
            }
            cmd.Parameters.Add(field, SqlDbType.BigInt).Value = value;
        }
        public void AddLong(string field, long value)
        {
            if (!field.StartsWith("@"))
            {
                field = "@" + field;
            }
            cmd.Parameters.Add(field, SqlDbType.BigInt).Value = value;
        }
        public void AddDecimal(string field, decimal value)
        {
            if (!field.StartsWith("@"))
            {
                field = "@" + field;
            }
            cmd.Parameters.Add(field, SqlDbType.Decimal).Value = value;
        }
        public void AddFloat(string field, double value)
        {
            if (!field.StartsWith("@"))
            {
                field = "@" + field;
            }
            cmd.Parameters.Add(field, SqlDbType.Float).Value = value;
        }
        public void AddMoney(string field, double value)
        {
            if (!field.StartsWith("@"))
            {
                field = "@" + field;
            }
            cmd.Parameters.Add(field, SqlDbType.Money).Value = value;
        }
        
        public void AddGuid(string field, Guid value)
        {
            if (!field.StartsWith("@"))
            {
                field = "@" + field;
            }
            cmd.Parameters.Add(field, SqlDbType.UniqueIdentifier).Value = value;
        }
        
        public void AddDate(string field, Nullable<DateTime> value)
        {
            if (!field.StartsWith("@"))
            {
                field = "@" + field;
            }
            if (ReferenceEquals(value, null))
            {
                cmd.Parameters.Add(field, SqlDbType.Date);
            }
            else if (Information.IsDate(value) && value != new DateTime(2018, 8, 6))
            {
                cmd.Parameters.Add(field, SqlDbType.Date).Value = value;
            }
            else
            {
                cmd.Parameters.Add(field, SqlDbType.Date);
            }
        }
        public void AddDateTime(string field, Nullable<DateTime> value)
        {
            if (!field.StartsWith("@"))
            {
                field = "@" + field;
            }
            if (ReferenceEquals(value, null))
            {
                cmd.Parameters.Add(field, SqlDbType.DateTime);
            }
            else if (Information.IsDate(value) && value != new DateTime(2018, 8, 6))
            {
                cmd.Parameters.Add(field, SqlDbType.DateTime).Value = value;
            }
            else
            {
                cmd.Parameters.Add(field, SqlDbType.DateTime);
            }
        }
        
        public void AddChar(string field, string value, int maxlen = 8000)
        {
            if (!field.StartsWith("@"))
            {
                field = "@" + field;
            }
            
            value = SQLfunctions.SQLstr(value);
            if (System.Convert.ToBoolean(SQLDBVersion < 2005))
            {
                if (maxlen > MaxVarChar)
                {
                    maxlen = MaxVarChar;
                }
                value = value.Substring(0, maxlen);
                cmd.Parameters.Add(field, SqlDbType.VarChar, maxlen).Value = value;
            }
            else
            {
                if (maxlen == -1)
                {
                    cmd.Parameters.Add(field, SqlDbType.Char).Value = value;
                }
                else if (maxlen < MaxVarChar)
                {
                    value = value.Substring(0, maxlen);
                    cmd.Parameters.Add(field, SqlDbType.Char, maxlen).Value = value;
                }
                else if (maxlen > MaxVarChar)
                {
                    if (maxlen > MaxVarChar)
                    {
                        maxlen = MaxVarChar;
                    }
                    value = value.Substring(0, maxlen);
                    cmd.Parameters.Add(field, SqlDbType.Char, maxlen).Value = value;
                }
                else
                {
                    //	skulle ikke komme
                }
            }
        }
        
        public void AddVarChar(string field, string value, int maxlen = 8000)
        {
            if (!field.StartsWith("@"))
            {
                field = "@" + field;
            }
            
            value = SQLfunctions.SQLstr(value);
            if (System.Convert.ToBoolean(SQLDBVersion < 2005))
            {
                if (maxlen > MaxVarChar)
                {
                    maxlen = MaxVarChar;
                }
                value = value.Substring(0, maxlen);
                cmd.Parameters.Add(field, SqlDbType.VarChar, maxlen).Value = value;
            }
            else
            {
                if (maxlen == -1)
                {
                    cmd.Parameters.Add(field, SqlDbType.VarChar).Value = value;
                }
                else if (maxlen < MaxVarChar)
                {
                    value = value.Substring(0, maxlen);
                    cmd.Parameters.Add(field, SqlDbType.VarChar, maxlen).Value = value;
                }
                else if (maxlen > MaxVarChar)
                {
                    if (maxlen > MaxVarChar)
                    {
                        maxlen = MaxVarChar;
                    }
                    value = value.Substring(0, maxlen);
                    cmd.Parameters.Add(field, SqlDbType.VarChar, maxlen).Value = value;
                }
                else
                {
                    //	skulle ikke komme
                }
            }
        }
        
        public void AddNChar(string field, string value, int maxlen = 4000)
        {
            if (!field.StartsWith("@"))
            {
                field = "@" + field;
            }
            
            value = SQLfunctions.SQLstr(value);
            if (System.Convert.ToBoolean(SQLDBVersion < 2005))
            {
                if (maxlen > MaxNVarChar)
                {
                    maxlen = MaxNVarChar;
                }
                value = value.Substring(0, maxlen);
                cmd.Parameters.Add(field, SqlDbType.NChar, maxlen).Value = value;
            }
            else
            {
                if (maxlen == -1)
                {
                    cmd.Parameters.Add(field, SqlDbType.NChar).Value = value;
                }
                else if (maxlen < MaxVarChar)
                {
                    value = value.Substring(0, maxlen);
                    cmd.Parameters.Add(field, SqlDbType.NChar, maxlen).Value = value;
                }
                else if (maxlen > MaxVarChar)
                {
                    maxlen = MaxNVarChar;
                    value = value.Substring(0, maxlen);
                    cmd.Parameters.Add(field, SqlDbType.NChar, maxlen).Value = value;
                }
                else
                {
                    //	cmd.Parameters.Add(field, SqlDbType.NVarChar).Value = value
                }
            }
        }
        
        public void AddNVarChar(string field, string value, int maxlen = 4000)
        {
            if (!field.StartsWith("@"))
            {
                field = "@" + field;
            }
            
            value = SQLfunctions.SQLstr(value);
            if (System.Convert.ToBoolean(SQLDBVersion < 2005))
            {
                if (maxlen > MaxNVarChar)
                {
                    maxlen = MaxNVarChar;
                }
                value = value.Substring(0, maxlen);
                cmd.Parameters.Add(field, SqlDbType.NVarChar, maxlen).Value = value;
            }
            else
            {
                if (maxlen == -1)
                {
                    cmd.Parameters.Add(field, SqlDbType.NVarChar).Value = value;
                }
                else if (maxlen < MaxVarChar)
                {
                    value = value.Substring(0, maxlen);
                    cmd.Parameters.Add(field, SqlDbType.NVarChar, maxlen).Value = value;
                }
                else if (maxlen > MaxVarChar)
                {
                    maxlen = MaxNVarChar;
                    value = value.Substring(0, maxlen);
                    cmd.Parameters.Add(field, SqlDbType.NVarChar, maxlen).Value = value;
                }
                else
                {
                    //	cmd.Parameters.Add(field, SqlDbType.NVarChar).Value = value
                }
            }
        }
        
        public void AddNText(string field, string value, bool FullHTML = false)
        {
            if (!field.StartsWith("@"))
            {
                field = "@" + field;
            }
            if (!FullHTML)
            {
                value = SQLfunctions.SQLstr(value);
            }
            cmd.Parameters.Add(field, SqlDbType.NText).Value = value;
        }
        
        public void AddText(string field, string value)
        {
            if (!field.StartsWith("@"))
            {
                field = "@" + field;
            }
            value = SQLfunctions.SQLstr(value);
            cmd.Parameters.Add(field, SqlDbType.Text).Value = value;
        }
        
        public void AddBinary(string field, object value)
        {
            if (!field.StartsWith("@"))
            {
                field = "@" + field;
            }
            cmd.Parameters.Add(field, SqlDbType.Binary).Value = value;
        }
        public void AddImage(string field, object value)
        {
            if (!field.StartsWith("@"))
            {
                field = "@" + field;
            }
            cmd.Parameters.Add(field, SqlDbType.Image).Value = value;
        }
        public void AddBlob(string field, object value)
        {
            if (!field.StartsWith("@"))
            {
                field = "@" + field;
            }
            cmd.Parameters.Add(field, SqlDbType.Image).Value = value;
        }
        
    }
    
}
