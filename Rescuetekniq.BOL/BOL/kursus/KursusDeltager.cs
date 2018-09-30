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
    
    public class KursusDeltager : BaseObject
    {
        
#region  New
        
        public KursusDeltager()
        {
        }
        
        public KursusDeltager(int ID)
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
        
        private int _CompanyID;
        private KursusKursistStatusEnum _Status = KursusKursistStatusEnum.Initialize;
        private string _Fornavn;
        private string _Efternavn;
        private DateTime _Birthday;
        private string _Cprnr;
        private string _MedArbNr;
        private string _Lokation;
        private string _Stilling;
        private string _Adresse;
        private string _Postnr;
        private string _Bynavn;
        private string _State;
        private int _LandekodeID = 45;
        private DateTime _FratradtDato;
        
        private RescueTekniq.BOL.Virksomhed _virksomhed = new RescueTekniq.BOL.Virksomhed();
        private List<KursusListe> _KursusList;
        private int _KursusListCount = -1;
        
#endregion
        
#region  Properties
        
        public int CompanyID
        {
            get
            {
                return _CompanyID;
            }
            set
            {
                _CompanyID = value;
            }
        }
        
        public KursusKursistStatusEnum Status
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
        
        public string Fornavn
        {
            get
            {
                return _Fornavn;
            }
            set
            {
                _Fornavn = value;
            }
        }
        
        public string Efternavn
        {
            get
            {
                return _Efternavn;
            }
            set
            {
                _Efternavn = value;
            }
        }
        
        public string KursistInfo
        {
            get
            {
                string res = "";
                res += Fornavn;
                if (Efternavn != "")
                {
                    res += " " + Efternavn;
                }
                if (Birthday.ToString() != "")
                {
                    if (Cprnr != "")
                    {
                        res += ", " + Birthday.ToString("ddMMyy");
                        res += "-" + Cprnr;
                    }
                    else
                    {
                        res += ", " + Birthday.ToShortDateString();
                        
                    }
                }
                if (MedArbNr != "")
                {
                    res += ", " + MedArbNr;
                }
                return res;
            }
        }
        
        
        public DateTime Birthday
        {
            get
            {
                return _Birthday;
            }
            set
            {
                _Birthday = value;
            }
        }
        
        public string Cprnr
        {
            get
            {
                return _Cprnr;
            }
            set
            {
                _Cprnr = value;
            }
        }
        
        public string MedArbNr
        {
            get
            {
                return _MedArbNr;
            }
            set
            {
                _MedArbNr = value;
            }
        }
        
        public string Lokation
        {
            get
            {
                return _Lokation;
            }
            set
            {
                _Lokation = value;
            }
        }
        
        public string Stilling
        {
            get
            {
                return _Stilling;
            }
            set
            {
                _Stilling = value;
            }
        }
        
        public string Adresse
        {
            get
            {
                return _Adresse;
            }
            set
            {
                _Adresse = value;
            }
        }
        
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
        
        public string State
        {
            get
            {
                return _State;
            }
            set
            {
                _State = value;
            }
        }
        
        public int LandekodeID
        {
            get
            {
                return _LandekodeID;
            }
            set
            {
                _LandekodeID = value;
            }
        }
        
        public string Land
        {
            get
            {
                return Combobox.GetTitleByValue("Landekode", LandekodeID.ToString()); //Combobox.GetTitle("Landekode", LandekodeID)
            }
        }
        
        public string PostnrBy
        {
            get
            {
                string res = "";
                switch (Land)
                {
                    case "45":
                    case "298":
                    case "299":
                        res = Postnr + " " + Bynavn;
                        break;
                    case "1":
                        res = Bynavn + ", " + State.ToUpper() + " " + Postnr;
                        break;
                        //Washington, DC 20546-0001
                    default:
                        res = Postnr + " " + Bynavn;
                        break;
                }
                return res;
            }
        }
        
        public string FuldAdresse
        {
            get
            {
                string res = "";
                if (Adresse != "")
                {
                    res += "<br />" + Adresse;
                }
                if (PostnrBy != "")
                {
                    res += "<br />" + PostnrBy;
                }
                if (Land != "")
                {
                    res += "<br />" + Land;
                }
                if (res.StartsWith("<br />"))
                {
                    res = res.Substring(6);
                }
                return res;
            }
        }
        
        public DateTime FratradtDato
        {
            get
            {
                return _FratradtDato;
            }
            set
            {
                _FratradtDato = value;
            }
        }
        
        public RescueTekniq.BOL.Virksomhed Virksomhed
        {
            get
            {
                try
                {
                    if (_CompanyID > 0)
                    {
                        if (!_virksomhed.loaded)
                        {
                            _virksomhed = Virksomhed.GetCompany(_CompanyID);
                        }
                        else if (_virksomhed.ID != _CompanyID)
                        {
                            _virksomhed = Virksomhed.GetCompany(_CompanyID);
                        }
                    }
                }
                catch (Exception)
                {
                }
                return _virksomhed;
            }
        }
        
        public List<KursusListe> Kurser
        {
            get
            {
                try
                {
                    if (_KursusListCount < 0)
                    {
                        _KursusList = KursusListe.GetKursusListByKursusDeltager(ID);
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
        
#endregion
        
#region  Shared Populate
        
        private static void AddParms(ref DBAccess db, KursusDeltager c)
        {
            AddParmsStandard(db, c);
            var with_1 = c;
            db.AddInt("CompanyID", with_1.CompanyID);
            db.AddInt("Status", (System.Int32) with_1.Status);
            db.AddNVarChar("Fornavn", with_1.Fornavn, 50);
            db.AddNVarChar("Efternavn", with_1.Efternavn, 50);
            db.AddDateTime("Birthday", with_1.Birthday);
            db.AddNVarChar("Cprnr", with_1.Cprnr, 4);
            db.AddNVarChar("MedArbNr", with_1.MedArbNr, 15);
            db.AddNVarChar("Lokation", with_1.Lokation, 250);
            db.AddNVarChar("Stilling", with_1.Stilling, 100);
            db.AddNVarChar("Adresse", with_1.Adresse, 250);
            db.AddNVarChar("Postnr", with_1.Postnr, 50);
            db.AddNVarChar("Bynavn", with_1.Bynavn, 50);
            db.AddNVarChar("State", with_1.State, 50);
            db.AddInt("Land", with_1.LandekodeID);
            db.AddDateTime("FratradtDato", with_1.FratradtDato);
        }
        
        private static void Populate(SqlDataReader dr, KursusDeltager c)
        {
            PopulateStandard(dr, c);
            var with_1 = c;
            with_1.CompanyID = System.Convert.ToInt32(dr.DBtoInt("CompanyID"));
            with_1.Status = (RescueTekniq.BOL.KursusKursistStatusEnum) (dr.DBtoInt("Status"));
            with_1.Fornavn = dr.DBtoString("Fornavn");
            with_1.Efternavn = dr.DBtoString("Efternavn");
            with_1.Birthday = System.Convert.ToDateTime(dr.DBtoDate("Birthday"));
            with_1.Cprnr = dr.DBtoString("Cprnr");
            with_1.MedArbNr = dr.DBtoString("MedArbNr");
            with_1.Lokation = dr.DBtoString("Lokation");
            with_1.Stilling = dr.DBtoString("Stilling");
            with_1.Adresse = dr.DBtoString("Adresse");
            with_1.Postnr = dr.DBtoString("Postnr");
            with_1.Bynavn = dr.DBtoString("Bynavn");
            with_1.State = dr.DBtoString("State");
            with_1.LandekodeID = System.Convert.ToInt32(dr.DBtoInt("Land"));
            with_1.FratradtDato = System.Convert.ToDateTime(dr.DBtoDate("FratradtDato"));
        }
        
#endregion
        
#region  Stored Procedure Names
        private const string _SQLDelete = "Co2Db_KursusDeltager_Delete";
        private const string _SQLInsert = "Co2Db_KursusDeltager_Insert";
        private const string _SQLUpdate = "Co2Db_KursusDeltager_Update";
        private const string _SQLSelectAll = "Co2Db_KursusDeltager_SelectAll";
        private const string _SQLSelectAllList = "Co2Db_KursusDeltager_SelectAllList";
        private const string _SQLSelectByCompany = "Co2Db_KursusDeltager_SelectAllByCompany";
        private const string _SQLSelectID = "Co2Db_KursusDeltager_SelectID";
        private const string _SQLSelectOne = "Co2Db_KursusDeltager_SelectOne";
        private const string _SQLSelectBySearch = "Co2Db_KursusDeltager_SelectBySearch";
        
        private const string _SQLSelectIDAllList = "Co2Db_KursusDeltager_SelectIDAllList";
        private const string _SQLSelectIDAllDagList = "Co2Db_KursusDeltager_SelectIDAllDagList";
        private const string _SQLSelectIDAllCompanyList = "Co2Db_KursusDeltager_SelectIDAllCompanyList";
        
        private const string _SQLKursusDagList_Add = "Co2Db_KursusDagListe_Add";
        private const string _SQLKursusDagList_Del = "Co2Db_KursusDagListe_Del";
        
#endregion
        
#region  Metoder
        
#region  Manipulate Data
        
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
        public static int Delete(KursusDeltager c)
        {
            return Delete(c.ID);
        }
        
        public int Insert()
        {
            return Insert(this);
        }
        public static int Insert(KursusDeltager c)
        {
            DBAccess db = new DBAccess();
            
            if (c.Status == KursusKursistStatusEnum.Initialize)
            {
                c.Status = KursusKursistStatusEnum.Aktiv;
            }
            AddParms(ref db, c);
            SqlParameter objParam = new SqlParameter("@ID", 0);
            objParam.Direction = ParameterDirection.Output;
            db.Parameters.Add(objParam);
            
            int retval = db.ExecuteNonQuery(_SQLInsert);
            if (retval == 1)
            {
                c.ID = Funktioner.ToInteger(objParam.Value.ToString(), -1);
                return c.ID;
            }
            else
            {
                return -1;
            }
        }
        public static int Insert(int CompanyID, KursusKursistStatusEnum Status, string Fornavn, string Efternavn, DateTime Birthday, string Cprnr, string MedArbNr, string Lokation, string Stilling, string Adresse, string Postnr, string Bynavn, string State, int Land, DateTime FratradtDato)
        {
            KursusDeltager c = new KursusDeltager();
            c.CompanyID = CompanyID;
            c.Status = Status;
            c.Fornavn = Fornavn;
            c.Efternavn = Efternavn;
            c.Birthday = Birthday;
            c.Cprnr = Cprnr;
            c.MedArbNr = MedArbNr;
            c.Lokation = Lokation;
            c.Stilling = Stilling;
            c.Adresse = Adresse;
            c.Postnr = Postnr;
            c.Bynavn = Bynavn;
            c.State = State;
            c.LandekodeID = Land;
            c.FratradtDato = FratradtDato;
            
            return Insert(c);
        }
        
        public int Update()
        {
            return Update(this);
        }
        public static int Update(KursusDeltager c)
        {
            DBAccess db = new DBAccess();
            
            db.AddInt("ID", c.ID);
            AddParms(ref db, c);
            
            int retval = db.ExecuteNonQuery(_SQLUpdate);
            return retval;
        }
        public static int Update(int ID, int CompanyID, KursusKursistStatusEnum Status, string Fornavn, string Efternavn, DateTime Birthday, string Cprnr, string MedArbNr, string Lokation, string Stilling, string Adresse, string Postnr, string Bynavn, string State, int Land, DateTime FratradtDato)
        {
            KursusDeltager c = new KursusDeltager(ID);
            c.CompanyID = CompanyID;
            c.Status = Status;
            c.Fornavn = Fornavn;
            c.Efternavn = Efternavn;
            c.Birthday = Birthday;
            c.Cprnr = Cprnr;
            c.MedArbNr = MedArbNr;
            c.Lokation = Lokation;
            c.Stilling = Stilling;
            c.Adresse = Adresse;
            c.Postnr = Postnr;
            c.Bynavn = Bynavn;
            c.State = State;
            c.LandekodeID = Land;
            c.FratradtDato = FratradtDato;
            
            return Update(c);
        }
        
        public int Save()
        {
            return Save(this);
        }
        public static int Save(KursusDeltager K)
        {
            int retval = 0;
            if (K.ID > 0)
            {
                retval = Update(K);
            }
            else
            {
                retval = Insert(K);
            }
            return retval;
        }
        
#endregion
        
#region  Get Data
        
        public static DataSet GetAllKursusDeltager()
        {
            DBAccess db = new DBAccess();
            DataSet ds = db.ExecuteDataSet(_SQLSelectAll);
            return ds;
        }
        //Public Shared Function GetAllKursusDeltagerList() As DataSet
        //    Dim db As DBAccess = New DBAccess
        //    Dim ds As DataSet = db.ExecuteDataSet(_SQLSelectAllList)
        //    Return ds
        //End Function
        public static System.Collections.Generic.List<KursusDeltager> GetAllKursusDeltagerDagList(int KursusDagID)
        {
            System.Collections.Generic.List<KursusDeltager> result = new System.Collections.Generic.List<KursusDeltager>();
            int ID = -1;
            DBAccess db = new DBAccess();
            db.AddInt("KursusDagID", KursusDagID);
            
            System.Data.SqlClient.SqlDataReader dr = default(System.Data.SqlClient.SqlDataReader);
            try
            {
                dr = (System.Data.SqlClient.SqlDataReader) (db.ExecuteReader(_SQLSelectIDAllDagList));
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ID = System.Convert.ToInt32(dr.DBtoInt("ID"));
                        KursusDeltager kurist = KursusDeltager.GetKursusDeltager(ID);
                        if (kurist != null && kurist.isLoaded)
                        {
                            result.Add(kurist);
                        }
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
        
        public static System.Collections.Generic.List<KursusDeltager> GetAllKursusDeltagerCompanyList(int CompanyID)
        {
            System.Collections.Generic.List<KursusDeltager> result = new System.Collections.Generic.List<KursusDeltager>();
            int ID = -1;
            DBAccess db = new DBAccess();
            db.AddInt("CompanyID", CompanyID);
            
            SqlDataReader dr = default(SqlDataReader);
            try
            {
                dr = (System.Data.SqlClient.SqlDataReader) (db.ExecuteReader(_SQLSelectIDAllCompanyList));
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ID = System.Convert.ToInt32(dr.DBtoInt("ID"));
                        result.Add(KursusDeltager.GetKursusDeltager(ID));
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
        
        public static DataSet GetKursusDeltagerByCompany(int CompanyID)
        {
            DBAccess db = new DBAccess();
            db.AddInt("CompanyID", CompanyID);
            DataSet ds = db.ExecuteDataSet(_SQLSelectByCompany);
            return ds;
        }
        
        public static DataSet GetKursusDeltagerDS(int ID)
        {
            DBAccess db = new DBAccess();
            db.AddInt("ID", ID);
            DataSet ds = db.ExecuteDataSet(_SQLSelectID);
            return ds;
        }
        
        public static KursusDeltager GetKursusDeltager(int ID)
        {
            DBAccess db = new DBAccess();
            db.AddInt("ID", ID);
            SqlDataReader dr = (SqlDataReader) (db.ExecuteReader(_SQLSelectOne));
            if (dr.HasRows)
            {
                KursusDeltager c = new KursusDeltager();
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
        
        public static DataSet Search_KursusDeltager(string Search)
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
        
#region  Kursus Dag Liste
        
        public static int KursusDagList_Add(int KursusDagID, int KursusDeltagerID, KursusKursistStatusEnum KursusListeStatus)
        {
            DBAccess db = new DBAccess();
            db.AddInt("KursusDagID", KursusDagID);
            db.AddInt("KursusDeltagerID", KursusDeltagerID);
            db.AddInt("KursusListeStatus", System.Convert.ToInt32(KursusListeStatus));
            int retval = db.ExecuteNonQuery(_SQLKursusDagList_Add);
            return retval;
        }
        
        public static int KursusDagList_Del(int KursusDagID, int KursusDeltagerID)
        {
            DBAccess db = new DBAccess();
            db.AddInt("KursusDagID", KursusDagID);
            db.AddInt("KursusDeltagerID", KursusDeltagerID);
            int retval = db.ExecuteNonQuery(_SQLKursusDagList_Del);
            return retval;
        }
        
#endregion
        
        
#endregion
        
    }
    
}
