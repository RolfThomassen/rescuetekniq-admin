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
    
    public class KursusDag : BaseObject
    {
        
#region  New
        
        public KursusDag()
        {
            
            //_ParentID = 0
            //_KursusDagNr = ""
            //_KursusDagTekst = ""
        }
        
        public KursusDag(int ID)
        {
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
        private int _KursusPolicyID;
        private DateTime _KursusDato;
        private bool _Repetionskursus;
        private KursusStatusEnum _Status = KursusStatusEnum.Initialize;
        
        private KursusPolicy _Kursus;
        
        //Private _Kursister As List(Of KursusDeltager)
        private List<KursusListe> _KursusList;
        private int _KursusListCount = -1;
        
#endregion
        
#region  Properties
        
        public int KursusPolicyID
        {
            get
            {
                return _KursusPolicyID;
            }
            set
            {
                _KursusPolicyID = value;
            }
        }
        
        public DateTime KursusDato
        {
            get
            {
                return _KursusDato;
            }
            set
            {
                _KursusDato = value;
            }
        }
        
        public bool Repetionskursus
        {
            get
            {
                return _Repetionskursus;
            }
            set
            {
                _Repetionskursus = value;
            }
        }
        
        public KursusStatusEnum Status
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
        
        public RescueTekniq.BOL.KursusPolicy Kursus
        {
            get
            {
                if (_KursusPolicyID > 0)
                {
                    if (ReferenceEquals(_Kursus, null))
                    {
                        _Kursus = new KursusPolicy();
                    }
                    if (!_Kursus.loaded)
                    {
                        _Kursus = KursusPolicy.GetKursusPolicy(_KursusPolicyID);
                    }
                    else if (_Kursus.ID != _KursusPolicyID)
                    {
                        _Kursus = KursusPolicy.GetKursusPolicy(_KursusPolicyID);
                    }
                }
                return _Kursus;
            }
        }
        
        public List<KursusListe> Kursister
        {
            get
            {
                try
                {
                    if (_KursusListCount < 0)
                    {
                        _KursusList = KursusListe.GetKursusListByKursusDag(ID);
                        _KursusListCount = _KursusList.Count;
                    }
                }
                catch (Exception)
                {
                    _KursusListCount = 0;
                }
                return _KursusList;
            }
        }
        public int KursistCount
        {
            get
            {
                if (_KursusListCount < 0)
                {
                    List<KursusListe> x = Kursister;
                }
                return _KursusListCount;
            }
        }
        
#endregion
        
#region  Shared Populate
        
        private static void AddParms(ref DBAccess db, KursusDag c)
        {
            var with_1 = c;
            db.AddInt("KursusPolicyID", with_1.KursusPolicyID);
            db.AddDateTime("KursusDato", with_1.KursusDato);
            db.AddBoolean("Repetionskursus", with_1.Repetionskursus);
            db.AddInt("Status", (System.Int32) with_1.Status);
            AddParmsStandard(db, c);
        }
        
        private static void Populate(System.Data.SqlClient.SqlDataReader dr, KursusDag c)
        {
            var with_1 = c;
            with_1.KursusPolicyID = System.Convert.ToInt32(dr.DBtoInt("KursusPolicyID"));
            with_1.KursusDato = System.Convert.ToDateTime(dr.DBtoDate("KursusDato"));
            with_1.Repetionskursus = System.Convert.ToBoolean(dr.DBtoBool("Repetionskursus"));
            with_1.Status = (RescueTekniq.BOL.KursusStatusEnum) (dr.DBtoInt("Status"));
            PopulateStandard(dr, c);
        }
        
#endregion
        
#region  Stored Procedure Names
        private const string _SQLDelete = "Co2Db_KursusDag_Delete";
        private const string _SQLInsert = "Co2Db_KursusDag_Insert";
        private const string _SQLUpdate = "Co2Db_KursusDag_Update";
        private const string _SQLSelectAll = "Co2Db_KursusDag_SelectAll";
        private const string _SQLSelectAllList = "Co2Db_KursusDag_SelectAllList";
        private const string _SQLSelectByParent = "Co2Db_KursusDag_SelectAllByParent";
        private const string _SQLSelectID = "Co2Db_KursusDag_SelectID";
        private const string _SQLSelectOne = "Co2Db_KursusDag_SelectOne";
        private const string _SQLSelectBySearch = "Co2Db_KursusDag_SelectBySearch";
        
        private const string _SQLSelectByDate = "Co2Db_KursusDag_SelectByDate";
#endregion
        
#region  Metoder
        
#region  Manipulate Data
        
        public int Delete()
        {
            return Delete(this);
        }
        public static int Delete(KursusDag c)
        {
            return Delete(c.ID);
        }
        public static int Delete(int ID)
        {
            DBAccess db = new DBAccess();
            db.AddInt("ID", ID);
            int retval = db.ExecuteNonQuery(_SQLDelete);
            return retval;
        }
        
        public int Insert()
        {
            return Insert(this);
        }
        public static int Insert(KursusDag c)
        {
            DBAccess db = new DBAccess();
            
            if (c.Status == KursusStatusEnum.Initialize)
            {
                c.Status = KursusStatusEnum.Aktiv;
            }
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
        public static int Insert(int KursusPolicyID, DateTime KursusDato, bool Repetionskursus, KursusStatusEnum Status)
        {
            KursusDag c = new KursusDag();
            c.KursusPolicyID = KursusPolicyID;
            c.KursusDato = KursusDato;
            c.Repetionskursus = Repetionskursus;
            c.Status = Status;
            
            return Insert(c);
        }
        
        public int Update()
        {
            return Update(this);
        }
        public static int Update(KursusDag c)
        {
            DBAccess db = new DBAccess();
            
            db.AddInt("ID", c.ID);
            AddParms(ref db, c);
            
            int retval = db.ExecuteNonQuery(_SQLUpdate);
            return retval;
        }
        public static int Update(int ID, int KursusPolicyID, DateTime KursusDato, bool Repetionskursus, KursusStatusEnum Status)
        {
            KursusDag c = new KursusDag(ID);
            c.KursusPolicyID = KursusPolicyID;
            c.KursusDato = KursusDato;
            c.Repetionskursus = Repetionskursus;
            c.Status = Status;
            
            return Update(c);
        }
        
#endregion
        
#region  Get Data
        
        public static DataSet GetAllKursusDag(int KursusPolicyID)
        {
            DBAccess db = new DBAccess();
            db.AddInt("KursusPolicyID", KursusPolicyID);
            DataSet ds = db.ExecuteDataSet(_SQLSelectAll);
            return ds;
        }
        public static DataSet GetAllKursusDagListDS(int KursusPolicyID)
        {
            DBAccess db = new DBAccess();
            db.AddInt("KursusPolicyID", KursusPolicyID);
            DataSet ds = db.ExecuteDataSet(_SQLSelectAllList);
            return ds;
        }
        
        public static DataSet GetKursusDagByParent(int KursusPolicyID)
        {
            DBAccess db = new DBAccess();
            db.AddInt("KursusPolicyID", KursusPolicyID);
            DataSet ds = db.ExecuteDataSet(_SQLSelectByParent);
            return ds;
        }
        
        public static DataSet GetKursusDagDS(int ID)
        {
            DBAccess db = new DBAccess();
            db.AddInt("ID", ID);
            DataSet ds = db.ExecuteDataSet(_SQLSelectID);
            return ds;
        }
        
        public static KursusDag GetKursusDag(int ID)
        {
            DBAccess db = new DBAccess();
            db.AddInt("ID", ID);
            KursusDag c = new KursusDag();
            SqlDataReader dr = (SqlDataReader) (db.ExecuteReader(_SQLSelectOne));
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Populate(dr, c);
                }
                dr.Close();
            }
            return c;
        }
        
        public static System.Collections.Generic.List<KursusDag> GetAllKursusDagList(int KursusPolicyID)
        {
            System.Collections.Generic.List<KursusDag> result = new System.Collections.Generic.List<KursusDag>();
            int ID = -1;
            DBAccess db = new DBAccess();
            db.AddInt("KursusPolicyID", KursusPolicyID);
            
            SqlDataReader dr = default(SqlDataReader);
            try
            {
                dr = (System.Data.SqlClient.SqlDataReader) (db.ExecuteReader(_SQLSelectAllList));
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        KursusDag kd = new KursusDag();
                        Populate(dr, kd);
                        result.Add(kd);
                    }
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            
            return result;
            
        }
        
        
        public static KursusDag GetKursusDag(int KursusPolicyID, DateTime KursusDato)
        {
            KursusDag c = new KursusDag();
            DBAccess db = new DBAccess();
            
            db.AddDateTime("KursusDato", KursusDato);
            db.AddInt("KursusPolicyID", KursusPolicyID);
            
            SqlDataReader dr = (SqlDataReader) (db.ExecuteReader(_SQLSelectByDate));
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Populate(dr, c);
                }
                dr.Close();
            }
            
            return c;
        }
        
        public static DataSet Search_KursusDag(int KursusPolicyID, DateTime KursusDato)
        {
            DataSet ds = new DataSet();
            DataSet dsTemp = new DataSet();
            bool flag = false;
            DBAccess db = new DBAccess();
            
            db.AddDateTime("KursusDato", KursusDato);
            db.AddInt("KursusPolicyID", KursusPolicyID);
            
            dsTemp = db.ExecuteDataSet(_SQLSelectBySearch);
            db.Parameters.Clear();
            
            ds.Merge(dsTemp);
            if (flag == false)
            {
                DataColumn[] pk = new DataColumn[2];
                pk[0] = ds.Tables[0].Columns["ID"];
                ds.Tables[0].PrimaryKey = pk;
                flag = true;
            }
            
            return ds;
        }
        
        
        public static DataSet Search_KursusDag1(string Search)
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
        
    }
    
    
}
