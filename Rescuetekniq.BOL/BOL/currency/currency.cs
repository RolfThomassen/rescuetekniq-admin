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
    
    //SELECT ID, CurrencyCode, CurrencyDesc, CurrencyRate, CurrencyDate, CurrencySymbol
    //FROM Co2Db_Valuta
    
    public class Currency : BaseObject
    {
        
#region  New
        
        public Currency()
        {
            // VBConversions Note: Non-static class variable initialization is below.  Class variables cannot be initially assigned non-static values in C#.
            _CurrencyDate = DateTime.Today;
            
            
            _CurrencyCode = "";
            _CurrencyDesc = "";
            _CurrencyRate = 0;
            _CurrencyDate = DateTime.Today;
            _CurrencySymbol = "";
        }
        
        public Currency(int ID)
        {
            // VBConversions Note: Non-static class variable initialization is below.  Class variables cannot be initially assigned non-static values in C#.
            _CurrencyDate = DateTime.Today;
            
            this.ID = ID;
            
            if (ID > 0)
            {
                DBAccess db = new DBAccess();
                db.Parameters.Add(new SqlParameter("@ID", ID));
                SqlDataReader dr = (SqlDataReader) (db.ExecuteReader(_SQLSelectOne));
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Populate(dr, this);
                    }
                    dr.Close();
                }
                else
                {
                    dr.Close();
                }
            }
        }
        
#endregion
        
#region  Privates
        
        private string _CurrencyCode = "";
        private string _CurrencyDesc = "";
        private decimal _CurrencyRate = 0;
        private DateTime _CurrencyDate; // VBConversions Note: Initial value cannot be assigned here since it is non-static.  Assignment has been moved to the class constructors.
        private string _CurrencySymbol = "";
        
#endregion
        
#region  Properties
        
        public string CurrencyCode
        {
            get
            {
                return _CurrencyCode;
            }
            set
            {
                _CurrencyCode = value;
            }
        }
        
        public string CurrencyDesc
        {
            get
            {
                return _CurrencyDesc;
            }
            set
            {
                _CurrencyDesc = value;
            }
        }
        
        public decimal CurrencyRate
        {
            get
            {
                return _CurrencyRate;
            }
            set
            {
                _CurrencyRate = value;
            }
        }
        
        public DateTime CurrencyDate
        {
            get
            {
                return _CurrencyDate;
            }
            set
            {
                _CurrencyDate = value;
            }
        }
        
        public string CurrencySymbol
        {
            get
            {
                return _CurrencySymbol;
            }
            set
            {
                _CurrencySymbol = value;
            }
        }
        
#endregion
        
#region  Shared Populate
        
        private static void AddParms(ref DBAccess db, Currency c)
        {
            var with_1 = c;
            db.AddVarChar("CurrencyCode", with_1.CurrencyCode, 10);
            db.AddNVarChar("CurrencyDesc", with_1.CurrencyDesc, 50);
            db.AddDecimal("CurrencyRate", with_1.CurrencyRate);
            db.AddDateTime("CurrencyDate", with_1.CurrencyDate);
            db.AddNVarChar("CurrencySymbol", with_1.CurrencySymbol, 50);
            AddParmsStandard(db, c);
        }
        
        private static void Populate(SqlDataReader dr, Currency c)
        {
            var with_1 = c;
            with_1.CurrencyCode = dr.DBtoString("CurrencyCode");
            with_1.CurrencyDesc = dr.DBtoString("CurrencyDesc");
            with_1.CurrencyRate = System.Convert.ToDecimal(dr.DBtoDecimal("CurrencyRate"));
            with_1.CurrencyDate = System.Convert.ToDateTime(dr.DBtoDate("CurrencyDate"));
            with_1.CurrencySymbol = dr.DBtoString("CurrencySymbol");
            PopulateStandard(dr, c);
        }
        
#endregion
        
#region  Stored Procedure Names
        private const string _SQLDelete = "Co2Db_Currency_Delete";
        private const string _SQLInsert = "Co2Db_Currency_Insert";
        private const string _SQLUpdate = "Co2Db_Currency_Update";
        private const string _SQLSelectAll = "Co2Db_Currency_SelectAll";
        
        private const string _SQLSelectID = "Co2Db_Currency_SelectID";
        private const string _SQLSelectOne = "Co2Db_Currency_SelectOne";
        private const string _SQLSelectByCode = "Co2Db_Currency_SelectByCode";
        private const string _SQLSelectBySearch = "Co2Db_Currency_SelectBySearch";
#endregion
        
#region  Metoder
        
#region  Manipulate data
        
        public int Delete()
        {
            return Delete(System.Convert.ToInt32(this));
        }
        public static int Delete(int ID)
        {
            DBAccess db = new DBAccess();
            db.AddInt("ID", ID);
            int retval = db.ExecuteNonQuery(_SQLDelete);
            return retval;
        }
        public static int Delete(Currency c)
        {
            return Delete(c.ID);
        }
        
        public int Insert()
        {
            return Insert(this);
        }
        public static int Insert(Currency c)
        {
            DBAccess db = new DBAccess();
            
            AddParms(ref db, c);
            
            SqlParameter objParam = new SqlParameter("@ID", 0);
            objParam.Direction = ParameterDirection.Output;
            db.Parameters.Add(objParam);
            int retval = db.ExecuteNonQuery(_SQLInsert);
            if (retval == 1)
            {
                return int.Parse(objParam.Value.ToString());
            }
            else
            {
                return -1;
            }
        }
        public static int Insert(string CurrencyCode, string CurrencyDesc, decimal CurrencyRate, DateTime CurrencyDate, string CurrencySymbol)
        {
            Currency c = new Currency();
            c.CurrencyCode = CurrencyCode;
            c.CurrencyDesc = CurrencyDesc;
            c.CurrencyRate = CurrencyRate;
            c.CurrencyDate = CurrencyDate;
            c.CurrencySymbol = CurrencySymbol;
            
            return Insert(c);
        }
        
        public int Update()
        {
            return Update(this);
        }
        public static int Update(Currency c)
        {
            DBAccess db = new DBAccess();
            
            db.AddInt("ID", c.ID);
            AddParms(ref db, c);
            
            int retval = db.ExecuteNonQuery(_SQLUpdate);
            return retval;
        }
        public static int Update(int ID, string CurrencyCode, string CurrencyDesc, decimal CurrencyRate, DateTime CurrencyDate, string CurrencySymbol)
        {
            Currency c = new Currency(ID);
            c.CurrencyCode = CurrencyCode;
            c.CurrencyDesc = CurrencyDesc;
            c.CurrencyRate = CurrencyRate;
            if (c.CurrencyRate != CurrencyRate)
            {
                c.CurrencyDate = DateTime.Now;
            }
            else
            {
                c.CurrencyDate = CurrencyDate;
            }
            c.CurrencySymbol = CurrencySymbol;
            
            return Update(c);
        }
        
#endregion
        
#region  Get Data
        
        public static DataSet GetAllCurrency()
        {
            DBAccess db = new DBAccess();
            DataSet ds = db.ExecuteDataSet(_SQLSelectAll);
            return ds;
        }
        
        public static DataSet GetCurrencyDS(int ID)
        {
            DBAccess db = new DBAccess();
            db.AddInt("ID", ID);
            DataSet ds = db.ExecuteDataSet(_SQLSelectID);
            return ds;
        }
        
        public static Currency GetCurrency(int ID)
        {
            DBAccess db = new DBAccess();
            db.AddInt("ID", ID);
            SqlDataReader dr = (SqlDataReader) (db.ExecuteReader(_SQLSelectOne));
            if (dr.HasRows)
            {
                Currency c = new Currency();
                while (dr.Read())
                {
                    Populate(dr, c);
                }
                dr.Close();
                return c;
            }
            else
            {
                dr.Close();
                return null;
            }
        }
        
        public static Currency GetCurrencyByCode(string Code)
        {
            DBAccess db = new DBAccess();
            Currency c = new Currency();
            db.AddNVarChar("CurrencyCode", Code, 4);
            SqlDataReader dr = (SqlDataReader) (db.ExecuteReader(_SQLSelectByCode));
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Populate(dr, c);
                }
            }
            try
            {
                dr.Close();
            }
            catch
            {
            }
            return c;
        }
        
        public static DataSet Search_Currency(string Search)
        {
            DataSet ds = new DataSet();
            DataSet dsTemp = new DataSet();
            bool flag = false;
            DBAccess db = new DBAccess();
            
            string[] arr = Search.Split(' ');
            foreach (string s in arr)
            {
                db.AddNVarChar("Search", s, 50);
                
                dsTemp = db.ExecuteDataSet(_SQLSelectBySearch);
                db.Parameters.Clear();
                //If dsTemp.Tables.Count > 0 Then
                ds.Merge(dsTemp);
                if (flag == false)
                {
                    DataColumn[] pk = new DataColumn[2];
                    pk[0] = ds.Tables[0].Columns["ID"];
                    ds.Tables[0].PrimaryKey = pk;
                    flag = true;
                }
                //End If
            }
            return ds;
        }
        
#endregion
        
#endregion
        
#region  Tags
        
        public string Tags(string tekst)
        {
            return Tags(tekst, this);
        }
        public static string Tags(string tekst, RescueTekniq.BOL.Currency item)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(tekst);
            
            sb.Replace("[CURRENCY.CODE]", item.CurrencyCode);
            sb.Replace("[CURRENCY.DATE]", item.CurrencyDate.ToString());
            sb.Replace("[CURRENCY.DESC]", item.CurrencyDesc);
            sb.Replace("[CURRENCY.RATE]", System.Convert.ToString(item.CurrencyRate));
            sb.Replace("[CURRENCY.SYMBOL]", item.CurrencySymbol);
            
            
            return sb.ToString();
        }
        
#endregion
        
    }
    
    
}
