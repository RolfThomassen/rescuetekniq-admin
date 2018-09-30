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
    
    //	SELECT ID, CompanyID, Opretdato, Udlobdato, AnsvarligID, OprettetAf, OprettetDen, OprettetIP, RettetAf, RettetDen, RettetIP, Status, Transport
    //	FROM Co2Db_Tilbud
    
    //	ID	int
    //	Status	int
    //	CompanyID	int
    //	Opretdato	datetime
    //	Udlobdato	datetime
    //	AnsvarligID	int
    //	KontaktEmail	nvarchar(250)
    //	Noter	nvarchar(MAX)
    //   TilbudsTillaeg	nvarchar(MAX)
    //	OprettetAf	nvarchar(50)
    //	OprettetDen	datetime
    //	OprettetIP	nvarchar(50)
    //	RettetAf	nvarchar(50)
    //	RettetDen	datetime
    //	RettetIP	nvarchar(50)
    //   Transport   float
    
    public class tilbudHeader : BaseObject
    {
        
#region  Privates
        
        private int _CompanyID = 0;
        private RescueTekniq.BOL.TilbudStatusEnum _Status = TilbudStatusEnum.Initialize;
        
        private DateTime _Opretdato; // VBConversions Note: Initial value cannot be assigned here since it is non-static.  Assignment has been moved to the class constructors.
        private DateTime _Udlobdato; // VBConversions Note: Initial value cannot be assigned here since it is non-static.  Assignment has been moved to the class constructors.
        
        private int _AnsvarligID = -1;
        private Guid _AnsvarligGUID = Guid.Empty;
        
        private string _KontaktEmail = "";
        private string _KontaktPerson = "";
        private string _Noter = "";
        private string _TilbudsTillaeg = "";
        
        private decimal _TotalTilbudspris = 0;
        private decimal _Transport = 0;
        
        private Virksomhed _Virksomhed = new Virksomhed();
        
        private System.Collections.Generic.List<TilbudsLinie> _Tilbudslinier; // = TilbudsLinie.GetTilbudsLinierList(ID)
        
#endregion
        
#region  New
        
        public tilbudHeader()
        {
            // VBConversions Note: Non-static class variable initialization is below.  Class variables cannot be initially assigned non-static values in C#.
            _Opretdato = DateTime.Today;
            _Udlobdato = DateAndTime.DateAdd(DateInterval.WeekOfYear, 3, DateAndTime.DateAdd(DateInterval.Day, 1, DateTime.Today));
            
        }
        
        public tilbudHeader(int ID)
        {
            // VBConversions Note: Non-static class variable initialization is below.  Class variables cannot be initially assigned non-static values in C#.
            _Opretdato = DateTime.Today;
            _Udlobdato = DateAndTime.DateAdd(DateInterval.WeekOfYear, 3, DateAndTime.DateAdd(DateInterval.Day, 1, DateTime.Today));
            
            this.ID = ID;
            if (ID > 0)
            {
                DBAccess db = new DBAccess();
                db.Parameters.Add(new SqlParameter("@ID", ID));
                SqlDataReader dr = (SqlDataReader) (db.ExecuteReader("Co2Db_TilbudHeader_SelectOne"));
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Populate(dr, this);
                    }
                }
                dr.Close();
            }
        }
        
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
                try
                {
                    //_Virksomhed = Virksomhed.GetCompany(_CompanyID)
                }
                catch (Exception)
                {
                    
                }
            }
        }
        
        public TilbudStatusEnum Status
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
        public string StatusText
        {
            get
            {
                return TilbudStatusEnum.GetName(_Status.GetType(), _Status);
            }
        }
        
        public DateTime Opretdato
        {
            get
            {
                return _Opretdato;
            }
            set
            {
                _Opretdato = value;
            }
        }
        
        public DateTime Udlobdato
        {
            get
            {
                return _Udlobdato;
            }
            set
            {
                _Udlobdato = value;
            }
        }
        
        public int AnsvarligID
        {
            get
            {
                return _AnsvarligID;
            }
            set
            {
                _AnsvarligID = value;
            }
        }
        public Guid AnsvarligGUID
        {
            get
            {
                return _AnsvarligGUID;
            }
            set
            {
                _AnsvarligGUID = value;
            }
        }
        public string Ansvarlig
        {
            get
            {
                MembershipUser User = default(MembershipUser);
                string name = "";
                try
                {
                    User = Membership.GetUser(AnsvarligGUID);
                    name = User.UserName;
                }
                catch (Exception)
                {
                }
                return name;
            }
        }
        
        public string KontaktEmail
        {
            get
            {
                return _KontaktEmail;
            }
            set
            {
                _KontaktEmail = value;
            }
        }
        public string KontaktPerson
        {
            get
            {
                return _KontaktPerson;
            }
            set
            {
                _KontaktPerson = value;
            }
        }
        
        public string Noter
        {
            get
            {
                return _Noter;
            }
            set
            {
                _Noter = value;
            }
        }
        public string TilbudsTillaeg
        {
            get
            {
                return (_TilbudsTillaeg + "").Trim();
            }
            set
            {
                _TilbudsTillaeg = value;
            }
        }
        
        public decimal TotalTilbudspris
        {
            get
            {
                return _TotalTilbudspris;
            }
        }
        
        public decimal Transport
        {
            get
            {
                return _Transport;
            }
            set
            {
                _Transport = value;
            }
        }
        
        public RescueTekniq.BOL.Virksomhed Virksomhed
        {
            get
            {
                if (_CompanyID > 0)
                {
                    if (!_Virksomhed.loaded)
                    {
                        _Virksomhed = Virksomhed.GetCompany(_CompanyID);
                    }
                    else if (_Virksomhed.ID != _CompanyID)
                    {
                        _Virksomhed = Virksomhed.GetCompany(_CompanyID);
                    }
                }
                return _Virksomhed;
            }
        }
        
        private bool _TilbudslinierLoaded = false;
        public System.Collections.Generic.List<TilbudsLinie> Tilbudslinier
        {
            get
            {
                if (!_TilbudslinierLoaded)
                {
                    _Tilbudslinier = TilbudsLinie.GetTilbudsLinierList(ID);
                    _TilbudslinierLoaded = true;
                }
                return _Tilbudslinier;
            }
        }
        
#endregion
        
#region  Stored Procedure Names
        private const string _SQLPurge = "Co2Db_TilbudHeader_Purge";
        private const string _SQLDelete = "Co2Db_TilbudHeader_Delete";
        private const string _SQLInsert = "Co2Db_TilbudHeader_Insert";
        private const string _SQLUpdate = "Co2Db_TilbudHeader_Update";
        private const string _SQLSelectAll = "Co2Db_TilbudHeader_SelectAll";
        private const string _SQLSelectID = "Co2Db_TilbudHeader_SelectID";
        private const string _SQLSelectOne = "Co2Db_TilbudHeader_SelectOne";
        private const string _SQLGetTotalpris = "Co2Db_TilbudHeader_GetTotalpris";
        private const string _SQLSelectByStatus = "Co2Db_TilbudHeader_SelectByStatus";
        private const string _SQLSelectByStatusCompany = "Co2Db_TilbudHeader_SelectByStatusCompany";
        private const string _SQLSelectByCompany = "Co2Db_TilbudHeader_SelectByCompany";
        private const string _SQLSelectByCompanyCount = "Co2Db_TilbudHeader_GetCompanyTilbudCount";
        
#endregion
        
#region  Shared Populate
        
        private static void AddParms(ref DBAccess db, tilbudHeader c)
        {
            db.AddInt("Status", (System.Int32) c.Status);
            db.AddInt("CompanyID", c.CompanyID);
            db.AddDateTime("Opretdato", c.Opretdato);
            db.AddDateTime("Udlobdato", c.Udlobdato);
            db.AddInt("AnsvarligID", c.AnsvarligID);
            db.AddGuid("AnsvarligGUID", c.AnsvarligGUID);
            db.AddNVarChar("KontaktEmail", c.KontaktEmail, 250);
            db.AddNVarChar("KontaktPerson", c.KontaktPerson, 50);
            db.AddNVarChar("Noter", c.Noter, -1);
            db.AddNVarChar("TilbudsTillaeg", c.TilbudsTillaeg, -1);
            db.AddDecimal("Transport", c.Transport);
            AddParmsStandard(db, c);
        }
        
        private static void Populate(SqlDataReader dr, tilbudHeader c)
        {
            PopulateStandard(dr, c);
            var with_1 = c;
            with_1.Status = (RescueTekniq.BOL.TilbudStatusEnum) (dr.DBtoInt("Status"));
            with_1.CompanyID = System.Convert.ToInt32(dr.DBtoInt("CompanyID"));
            with_1.Opretdato = System.Convert.ToDateTime(dr.DBtoDate("Opretdato"));
            with_1.Udlobdato = System.Convert.ToDateTime(dr.DBtoDate("Udlobdato"));
            with_1.AnsvarligID = System.Convert.ToInt32(dr.DBtoInt("AnsvarligID"));
            with_1.AnsvarligGUID = dr.DBtoGuid("AnsvarligGUID");
            with_1.KontaktEmail = dr.DBtoString("KontaktEmail");
            with_1.KontaktPerson = dr.DBtoString("KontaktPerson");
            with_1.Noter = dr.DBtoString("Noter");
            with_1.TilbudsTillaeg = dr.DBtoString("TilbudsTillaeg");
            with_1.Transport = System.Convert.ToDecimal(dr.DBtoDecimal("Transport"));
        }
        
#endregion
        
#region  Metoder
        
#region  Manipulate data
        
        public int Purge()
        {
            return Purge(System.Convert.ToInt32(this));
        }
        public static int Purge(int ID)
        {
            DBAccess db = new DBAccess();
            db.AddInt("ID", ID);
            int retval = db.ExecuteNonQuery(_SQLPurge);
            AddLog(Status: "tilbudHeader", Logtext: string.Format("Purge tilbudHeader: ID:{0} ", ID), Metode: "Purge");
            return retval;
        }
        public static int Purge(tilbudHeader c)
        {
            return Purge(c.ID);
        }
        
        public int Delete()
        {
            return Delete(this);
        }
        public static int Delete(tilbudHeader c)
        {
            int retval = -1;
            if (c.loaded)
            {
                foreach (TilbudsLinie item in c.Tilbudslinier)
                {
                    item.Delete();
                }
                
                DBAccess db = new DBAccess();
                db.AddInt("ID", c.ID);
                retval = db.ExecuteNonQuery(_SQLDelete);
                AddLog(Status: "tilbudHeader", Logtext: string.Format("Delete tilbudHeader: ID:{0}", c.ID), Metode: "Delete");
            }
            return retval;
        }
        public static int Delete(int ID)
        {
            tilbudHeader item = new tilbudHeader(ID);
            return item.Delete();
        }
        
        public int Insert()
        {
            return Insert(this);
        }
        public static int Insert(tilbudHeader c)
        {
            DBAccess db = new DBAccess();
            
            AddParms(ref db, c);
            
            SqlParameter objParam = new SqlParameter("@ID", 0);
            objParam.Direction = ParameterDirection.Output;
            db.Parameters.Add(objParam);
            int retval = db.ExecuteNonQuery(_SQLInsert);
            if (retval == 1)
            {
                c.ID = Funktioner.ToInt(objParam.Value);
                AddLog(Status: "tilbudHeader", Logtext: string.Format("Create tilbudHeader: ID:{0} KontaktPerson:{1} ", c.ID, c.KontaktPerson), Metode: "Insert");
                return c.ID; //Integer.Parse(objParam.Value.ToString)
            }
            else
            {
                AddLog(Status: "tilbudHeader", Logtext: string.Format("Failure to Create tilbudHeader: KontaktPerson:{0}", c.KontaktPerson), logtype: LogTypeEnum.Error, Metode: "Insert");
                return -1;
            }
        }
        public static int Insert(int CompanyID, int AnsvarligID, Guid AnsvarligGUID, string KontaktEmail, string KontaktPerson, string Noter, string TilbudsTillaeg, DateTime OpretDato, DateTime UdlobDato, TilbudStatusEnum Status)
        {
            tilbudHeader c = new tilbudHeader();
            c.CompanyID = CompanyID;
            c.AnsvarligID = AnsvarligID;
            c.AnsvarligGUID = AnsvarligGUID;
            c.KontaktEmail = KontaktEmail;
            c.KontaktPerson = KontaktPerson;
            c.Noter = Noter;
            c.TilbudsTillaeg = TilbudsTillaeg;
            c.Opretdato = OpretDato;
            c.Udlobdato = UdlobDato;
            c.Status = Status;
            return Insert(c);
        }
        
        public int Update()
        {
            return Update(this);
        }
        public static int Update(tilbudHeader c)
        {
            DBAccess db = new DBAccess();
            
            db.AddInt("ID", c.ID);
            AddParms(ref db, c);
            
            int retval = db.ExecuteNonQuery(_SQLUpdate);
            AddLog(Status: "tilbudHeader", Logtext: string.Format("Update tilbudHeader: ID:{0} KontaktPerson:{1} ", c.ID, c.KontaktPerson), Metode: "Update");
            return retval;
        }
        public static int Update(int ID, int CompanyID, int AnsvarligID, Guid AnsvarligGUID, string KontaktEmail, string KontaktPerson, string Noter, string TilbudsTillaeg, DateTime OpretDato, DateTime UdlobDato, TilbudStatusEnum Status)
        {
            tilbudHeader c = new tilbudHeader();
            c.ID = ID;
            c.CompanyID = CompanyID;
            c.AnsvarligID = AnsvarligID;
            c.AnsvarligGUID = AnsvarligGUID;
            c.KontaktEmail = KontaktEmail;
            c.KontaktPerson = KontaktPerson;
            c.Noter = Noter;
            c.TilbudsTillaeg = TilbudsTillaeg;
            c.Opretdato = OpretDato;
            c.Udlobdato = UdlobDato;
            c.Status = Status;
            return Update(c);
        }
        
        public static object UpdateStatus(int id, TilbudStatusEnum status)
        {
            tilbudHeader t = new tilbudHeader(id);
            t.Status = status;
            return Update(t);
        }
        
#endregion
        
#region  Get data
        
        public static DataSet GetAllTilbud()
        {
            DBAccess db = new DBAccess();
            //db.Parameters.Add(New SqlParameter("@sUserName", username))
            
            db.AddBoolean("IsAgent", AdgangsKontrol.IsAgent());
            db.AddGuid("AgentID", CurrentUserID);
            
            DataSet ds = db.ExecuteDataSet(_SQLSelectAll);
            return ds;
        }
        public static DataSet GetAllTilbud(TilbudStatusEnum status)
        {
            DBAccess db = new DBAccess();
            db.AddInt("status", System.Convert.ToInt32(status));
            
            db.AddBoolean("IsAgent", AdgangsKontrol.IsAgent());
            db.AddGuid("AgentID", CurrentUserID);
            
            DataSet ds = db.ExecuteDataSet(_SQLSelectByStatus);
            return ds;
        }
        
        public static DataSet GetTilbudDS(int ID)
        {
            DBAccess db = new DBAccess();
            db.AddInt("ID", ID);
            
            db.AddBoolean("IsAgent", AdgangsKontrol.IsAgent());
            db.AddGuid("AgentID", CurrentUserID);
            
            DataSet ds = db.ExecuteDataSet(_SQLSelectID);
            return ds;
        }
        
        public static tilbudHeader GetTilbud(int ID)
        {
            DBAccess db = new DBAccess();
            tilbudHeader c = new tilbudHeader();
            db.AddInt("ID", ID);
            
            db.AddBoolean("IsAgent", AdgangsKontrol.IsAgent());
            db.AddGuid("AgentID", CurrentUserID);
            
            SqlDataReader dr = (SqlDataReader) (db.ExecuteReader(_SQLSelectOne));
            if (dr.HasRows)
            {
                
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
                return c; //Nothing
            }
        }
        
        public static tilbudHeader GetTilbudByCompany(int CompanyID)
        {
            DBAccess db = new DBAccess();
            tilbudHeader c = new tilbudHeader();
            db.AddInt("CompanyID", CompanyID);
            
            db.AddBoolean("IsAgent", AdgangsKontrol.IsAgent());
            db.AddGuid("AgentID", CurrentUserID);
            
            SqlDataReader dr = (SqlDataReader) (db.ExecuteReader(_SQLSelectByCompany));
            if (dr.HasRows)
            {
                
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
                return c; //Nothing
            }
        }
        
        public static int GetCompanyTilbudCount(int CompanyID)
        {
            DBAccess db = new DBAccess();
            db.AddInt("CompanyID", CompanyID);
            
            db.AddBoolean("IsAgent", AdgangsKontrol.IsAgent());
            db.AddGuid("AgentID", CurrentUserID);
            
            int res = 0;
            try
            {
                res = Funktioner.ToInt(db.ExecuteScalar(_SQLSelectByCompanyCount));
            }
            catch (Exception)
            {
                res = 0;
            }
            return res;
        }
        
        public static DataSet GetRekvireret_Tilbud(TilbudStatusEnum type)
        {
            return Search_Tilbud((System.Convert.ToInt32(type)).ToString());
        }
        public static DataSet GetTilbudCompanyStatus(int CompanyID, TilbudStatusEnum type)
        {
            return Search_Tilbud((System.Convert.ToInt32(type)).ToString(), CompanyID);
        }
        public static DataSet Search_Tilbud(string skills, int CompanyID = -1)
        {
            DataSet ds = new DataSet();
            DataSet dsTemp = new DataSet();
            bool flag = false;
            DBAccess db = new DBAccess();
            
            string[] arr = skills.Split(' ');
            foreach (string s in arr)
            {
                db.AddInt("Status", Funktioner.ToInt(s));
                db.AddBoolean("IsAgent", AdgangsKontrol.IsAgent());
                db.AddGuid("AgentID", CurrentUserID);
                
                if (CompanyID == -1)
                {
                    //db.AddParameter("@CompanyID", DBNull.Value)
                }
                else
                {
                    db.AddInt("CompanyID", CompanyID);
                }
                dsTemp = db.ExecuteDataSet("Co2Db_TilbudHeader_SelectByStatusCompany");
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
        
        public static decimal GetTilbudTotalpris(int TilbudID)
        {
            DBAccess db = new DBAccess();
            db.AddInt("TilbudID", TilbudID);
            
            decimal res = 0;
            try
            {
                res = Funktioner.ToDecimal(db.ExecuteScalar(_SQLGetTotalpris));
            }
            catch (Exception)
            {
                res = 0;
            }
            return res;
        }
        
#endregion
        
#endregion
        
    }
    
}
