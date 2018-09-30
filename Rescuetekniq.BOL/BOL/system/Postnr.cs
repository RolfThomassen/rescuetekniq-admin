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
    
    //CREATE TABLE [vicjos1_sysadm].[Co2Db_Postnr](
    //Postnr nvarchar(10),
    //Bynavn nvarchar(50),
    //Gade nvarchar(50),
    //Firma nvarchar(50),
    //Provins bit,
    //Land int
    
    public class Postnummer : BaseObject
    {
        
#region  Privates
        
        private string _Postnr = "";
        private string _Bynavn = "";
        private string _Gade = "";
        private string _Firma = "";
        private int _LandID = -1;
        private bool _Provins = false;
        
#endregion
        
#region  New
        
        public Postnummer()
        {
        }
        public Postnummer(int ID)
        {
        }
        public Postnummer(string Postnr, string Bynavn, string Gade, string Firma, bool Provins, int LandID)
        {
            
            this.Postnr = Postnr;
            this.Bynavn = Bynavn;
            this.Gade = Gade;
            this.Firma = Firma;
            this.Provins = System.Convert.ToString(Provins);
            this.LandID = LandID;
        }
        
#endregion
        
#region  Properties
        
        public string Postnr
        {
            get
            {
                return _Postnr;
            }
            set
            {
                _Postnr = value;
            }
        }
        
        public string Bynavn
        {
            get
            {
                return _Bynavn;
            }
            set
            {
                _Bynavn = value;
            }
        }
        
        public string Gade
        {
            get
            {
                return _Gade;
            }
            set
            {
                _Gade = value;
            }
        }
        
        public string Firma
        {
            get
            {
                return _Firma;
            }
            set
            {
                _Firma = value;
            }
        }
        
        public int LandID
        {
            get
            {
                return _LandID;
            }
            set
            {
                _LandID = value;
            }
        }
        public string Landnavn
        {
            get
            {
                return Combobox.GetTitle("landekode", LandID);
            }
        }
        
        public string Provins
        {
            get
            {
                return System.Convert.ToString( _Provins);
            }
            set
            {
                _Provins = bool.Parse(value);
            }
        }
        
#endregion
        
#region  Shared Populate
        
        private static void AddParms(ref DBAccess db, Postnummer c)
        {
            var with_1 = c;
            db.AddNVarChar("Postnr", with_1.Postnr, 10);
            db.AddNVarChar("Bynavn", with_1.Bynavn, 50);
            db.AddNVarChar("Gade", with_1.Gade, 50);
            db.AddNVarChar("Firma", with_1.Firma, 50);
            db.AddInt("Land", with_1.LandID);
            db.AddBoolean("Provins", bool.Parse(with_1.Provins));
            AddParmsStandard(db, c);
        }
        
        private static void Populate(SqlDataReader dr, Postnummer p)
        {
            var with_1 = p;
            with_1.Postnr = dr.DBtoString("Postnr");
            with_1.Bynavn = dr.DBtoString("Bynavn");
            with_1.Gade = dr.DBtoString("Gade");
            with_1.Firma = dr.DBtoString("Firma");
            with_1.Provins = System.Convert.ToString(dr.DBtoBool("Provins"));
            with_1.LandID = System.Convert.ToInt32(dr.DBtoInt("Land"));
            
            PopulateStandard(dr, p);
        }
        
#endregion
        
#region  Metoder
        
        public static int Delete(int ID)
        {
            DBAccess db = new DBAccess();
            db.Parameters.Add(new SqlParameter("@ID", ID));
            int retval = db.ExecuteNonQuery("Co2Db_Postnr_Delete");
            return retval;
        }
        public static int Delete(Postnummer p)
        {
            return Delete(p.ID);
        }
        
        
        public static int Insert(Postnummer p)
        {
            DBAccess db = new DBAccess();
            
            AddParms(ref db, p);
            SqlParameter ID = new SqlParameter("@ID", 0);
            ID.Direction = ParameterDirection.Output;
            db.Parameters.Add(ID);
            
            int retval = db.ExecuteNonQuery("Co2Db_Postnr_Insert");
            if (retval == 1)
            {
                p.ID = int.Parse(ID.Value.ToString());
                return p.ID; //Integer.Parse(ID.Value.ToString)
            }
            else
            {
                return -1;
            }
            
        }
        public static int Insert(string Postnr, string Bynavn, string Gade, string Firma, bool Provins, int Land)
        {
            Postnummer p = new Postnummer(Postnr, Bynavn, Gade, Firma, Provins, Land);
            return Insert(p);
        }
        
        public static int Update(Postnummer p)
        {
            DBAccess db = new DBAccess();
            
            db.AddInt("ID", p.ID);
            AddParms(ref db, p);
            
            int retval = db.ExecuteNonQuery("Co2Db_Postnr_Update");
            return retval;
        }
        
        public static DataSet GetPostnrListe()
        {
            DBAccess db = new DBAccess();
            DataSet ds = db.ExecuteDataSet("Co2Db_Postnr_SelectAll");
            return ds;
        }
        
        
        public static DataSet GetPostnrSet(int ID)
        {
            DBAccess db = new DBAccess();
            db.Parameters.Add(new SqlParameter("@ID", ID));
            DataSet ds = db.ExecuteDataSet("Co2Db_Postnr_SelectOne");
            return ds;
        }
        
        public static DataSet GetPostnrSet(string Postnr, int Land = 1)
        {
            DBAccess db = new DBAccess();
            db.Parameters.Add(new SqlParameter("@Postnr", Postnr));
            db.Parameters.Add(new SqlParameter("@Land", Land));
            DataSet ds = db.ExecuteDataSet("Co2Db_Postnr_SelectOnePostnr");
            return ds;
        }
        
        public static Postnummer GetPostnr(int ID)
        {
            DBAccess db = new DBAccess();
            db.Parameters.Add(new SqlParameter("@ID", ID));
            SqlDataReader dr = (SqlDataReader) (db.ExecuteReader("Co2Db_Postnr_SelectOne"));
            if (dr.HasRows)
            {
                Postnummer p = new Postnummer();
                while (dr.Read())
                {
                    Populate(dr, p);
                }
                dr.Close();
                return p;
            }
            else
            {
                dr.Close();
                return null;
            }
        }
        
        public static string GetPostnrName(int ID)
        {
            DBAccess db = new DBAccess();
            db.AddParameter("@ID", ID);
            return System.Convert.ToString(db.ExecuteScalar("Co2Db_Postnr_SelectName"));
        }
        
        public static string GetByNavn(string Postnr, int LandID = 1)
        {
            DBAccess db = new DBAccess();
            db.AddParameter("@Postnr", Postnr);
            db.AddParameter("@Land", LandID);
            return System.Convert.ToString(db.ExecuteScalar("Co2Db_Postnr_SelectBynavn"));
        }
        
        public static int GetPostnrCount()
        {
            DBAccess db = new DBAccess();
            object res = null;
            int result = 0;
            
            res = db.ExecuteScalar("Co2Db_Postnr_GetCount");
            if ((!ReferenceEquals(res, null)) && Information.IsNumeric(res))
            {
                result = System.Convert.ToInt32(res);
            }
            return result;
        }
        
        //Postnr nvarchar(10),
        //Bynavn nvarchar(50),
        //Gade nvarchar(50),
        //Firma nvarchar(50),
        //Provins bit,
        //Land int
        public static DataSet SearchPostnrListe(string search)
        {
            string[] arr = search.Split(' ');
            DataSet ds = new DataSet();
            DataSet dsTemp = new DataSet();
            bool flag = false;
            DBAccess db = new DBAccess();
            foreach (string s in arr)
            {
                db.AddParameter("@Search", s);
                
                dsTemp = db.ExecuteDataSet("Co2Db_Postnr_Search");
                db.Parameters.Clear();
                ds.Merge(dsTemp);
                if (flag == false)
                {
                    DataColumn[] pk = new DataColumn[2];
                    pk[0] = ds.Tables[0].Columns["ID"];
                    ds.Tables[0].PrimaryKey = pk;
                    flag = true;
                }
            }
            return ds;
        }
        
#endregion
        
    }
    
    
}
