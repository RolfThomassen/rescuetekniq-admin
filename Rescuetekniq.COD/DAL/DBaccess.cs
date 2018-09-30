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
    namespace DAL
    {
        
        public class DBAccess : DBSQLfunction
        {
            
#region  New
            
            public DBAccess()
            {
                cmd.Connection = GetConnection();
                cmd.CommandType = CommandType.StoredProcedure;
            }
            public DBAccess(string strConnectionString)
            {
                SQLConnection.ConnectionString = strConnectionString;
                cmd.Connection = SQLConnection;
                //cmd.Connection = GetConnection(strConnectionString)
                CommandType = CommandType.StoredProcedure;
            }
            
#endregion
            
            private bool handleErrors = false;
            private string strLastError = "";
            
            public SqlConnection GetConnection(string name = "connectionstring")
            {
                SQLConnection.ConnectionString = ConfigurationManager.ConnectionStrings[name].ConnectionString;
                return SQLConnection;
            }
            
            public string ConnectionString
            {
                get
                {
                    return Connection.ConnectionString;
                }
                set
                {
                    Connection.ConnectionString = value;
                }
            }
            
            public string CommandText
            {
                get
                {
                    return cmd.CommandText;
                }
                set
                {
                    cmd.CommandText = value;
                    cmd.Parameters.Clear();
                }
            }
            
            public SqlConnection Connection // IDataParameterCollection ' SqlParameterCollection
            {
                get
                {
                    return cmd.Connection;
                }
            }
            
            public System.Data.CommandType CommandType
            {
                get
                {
                    return cmd.CommandType;
                }
                set
                {
                    cmd.CommandType = value;
                }
            }
            
            public IDataParameterCollection Parameters // SqlParameterCollection
            {
                get
                {
                    return cmd.Parameters;
                }
            }
            
            public void AddParameter(string paramname, object paramvalue)
            {
                SqlParameter param = new SqlParameter(paramname, paramvalue);
                Parameters.Add(param);
            }
            public void AddParameter(IDataParameter param)
            {
                Parameters.Add(param);
            }
            
            public void AddParameterRange(params SqlParameter[] @params)
            {
                AddRange(@params);
            }
            public void AddRange(params SqlParameter[] @params)
            {
                cmd.Parameters.AddRange(@params);
            }
            
            public void Open()
            {
                if (Connection.State != ConnectionState.Open)
                {
                    Connection.Open();
                }
            }
            
            public void Close()
            {
                if (Connection.State == ConnectionState.Open)
                {
                    Connection.Close();
                }
            }
            
            public IDataReader ExecuteReader()
            {
                IDataReader reader = null;
                try
                {
                    this.Open();
                    reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                }
                catch (SqlException ex)
                {
                    throw (new Exception(ex.Message, ex));
                }
                catch (DataException ex)
                {
                    throw (new Exception(ex.InnerException.Message, ex));
                }
                catch (Exception ex)
                {
                    if (handleErrors)
                    {
                        strLastError = ex.Message;
                    }
                    else
                    {
                        throw (new DataException("ExecuteReader SQL: " + this.CommandText, ex));
                    }
                }
                return reader;
            }
            public IDataReader ExecuteReader(string commandtext)
            {
                IDataReader reader = null;
                cmd.CommandText = commandtext;
                reader = this.ExecuteReader();
                return reader;
            }
            
            public object ExecuteScalar()
            {
                object obj = null;
                try
                {
                    this.Open();
                    obj = cmd.ExecuteScalar();
                    this.Close();
                }
                catch (SqlException ex)
                {
                    throw (new Exception(ex.InnerException.Message, ex));
                }
                catch (DataException ex)
                {
                    throw (new Exception(ex.InnerException.Message, ex));
                }
                catch (Exception ex)
                {
                    if (handleErrors)
                    {
                        strLastError = ex.Message;
                    }
                    else
                    {
                        throw;
                    }
                }
                return obj;
            }
            public object ExecuteScalar(string commandtext)
            {
                object obj = null;
                cmd.CommandText = commandtext;
                obj = this.ExecuteScalar();
                return obj;
            }
            
            public int ExecuteNonQuery()
            {
                int i = -1;
                try
                {
                    this.Open();
                    i = cmd.ExecuteNonQuery();
                    this.Close();
                }
                catch (SqlException ex)
                {
                    throw (new Exception(ex.Message, ex));
                }
                catch (DataException ex)
                {
                    throw (new Exception(ex.Message, ex));
                }
                catch (Exception ex)
                {
                    if (handleErrors)
                    {
                        strLastError = ex.Message;
                    }
                    else
                    {
                        throw;
                    }
                }
                return i;
            }
            public int ExecuteNonQuery(string commandtext)
            {
                int i = -1;
                cmd.CommandText = commandtext;
                i = this.ExecuteNonQuery();
                return i;
            }
            
            public DataSet ExecuteDataSet()
            {
                SqlDataAdapter da = null;
                DataSet ds = null;
                try
                {
                    da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    ds = new DataSet();
                    da.Fill(ds);
                }
                catch (SqlException ex)
                {
                    throw (new Exception(ex.Message, ex));
                }
                catch (DataException ex)
                {
                    throw (new Exception(ex.InnerException.Message, ex));
                }
                catch (Exception ex)
                {
                    if (handleErrors)
                    {
                        strLastError = ex.Message;
                    }
                    else
                    {
                        throw;
                    }
                }
                return ds;
            }
            public DataSet ExecuteDataSet(string commandtext)
            {
                DataSet ds = null;
                cmd.CommandText = commandtext;
                ds = this.ExecuteDataSet();
                return ds;
            }
            
            public bool HandleExceptions
            {
                get
                {
                    return handleErrors;
                }
                set
                {
                    handleErrors = value;
                }
            }
            
            public string LastError
            {
                get
                {
                    return strLastError;
                }
            }
            
            public void Dispose()
            {
                cmd.Dispose();
            }
            //    Public Sub Dispose()
            //       Connection.Dispose()
            //  End Sub
            
        }
        
    }
    
}
