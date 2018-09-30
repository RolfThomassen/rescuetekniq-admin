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
    
    //Namespace RescueTekniq.BOL
    
    public enum MedarbejderGruppeStatusEnum
    {
        slettet = 0,
        opret
    }
    
    //[Co2Db_MedarbejderGruppe_Update]
    //@ID int ,
    //@Aktiv bit ,
    //@CompanyID int ,
    
    //@Navn nvarchar(50) ,
    //@Brugernavn nvarchar(50) ,
    //@Kodeord nvarchar(50) ,
    //@Password nvarchar(36) ,
    //@IP nvarchar(250) ,
    //@Dato datetime ,
    //@RettetAf nvarchar(50) ,
    //@RettetIP nvarchar(15)
    
    public class MedarbejderGruppe : BaseObject
    {
        
        private bool _Aktiv = true;
        
        private int _CompanyID;
        private string _Navn = "";
        private string _BrugerNavn = "";
        private string _KodeOrd = "";
        private string _Password = "";
        private string _IP = "";
        private DateTime _Dato; // VBConversions Note: Initial value cannot be assigned here since it is non-static.  Assignment has been moved to the class constructors.
        
        private bool _Informationssite; // Der skal ikke etableres informationssite
        private bool _Administrationssite; // Der skal ikke etableres administrationssite
        
        private string _RettetAf = "";
        private string _RettetIP = "";
        private DateTime _RettetDen; // VBConversions Note: Initial value cannot be assigned here since it is non-static.  Assignment has been moved to the class constructors.
        
        public MedarbejderGruppe()
        {
            // VBConversions Note: Non-static class variable initialization is below.  Class variables cannot be initially assigned non-static values in C#.
            _Dato = DateTime.Now;
            _RettetDen = DateTime.Now;
            
            ID = -1;
            Aktiv = true;
            RettetDen = DateTime.Now;
        }
        public MedarbejderGruppe(int CompanyID)
        {
            // VBConversions Note: Non-static class variable initialization is below.  Class variables cannot be initially assigned non-static values in C#.
            _Dato = DateTime.Now;
            _RettetDen = DateTime.Now;
            
            ID = -1;
            Aktiv = true;
            RettetDen = DateTime.Now;
            this.CompanyID = CompanyID;
        }
        public MedarbejderGruppe(int CompanyID, string Navn, string Brugernavn, string Kodeord, string IP, DateTime Dato)
        {
            // VBConversions Note: Non-static class variable initialization is below.  Class variables cannot be initially assigned non-static values in C#.
            _Dato = DateTime.Now;
            _RettetDen = DateTime.Now;
            
            
            this.CompanyID = CompanyID;
            this.Navn = Navn;
            this.Brugernavn = Brugernavn;
            this.Kodeord = Kodeord;
            this.IP = IP;
            this.Dato = Dato.ToString();
            
        }
        
        public MedarbejderGruppe(int CompanyID, string Navn, string Brugernavn, string Kodeord, string IP, DateTime Dato, bool Informationssite, bool Administrationssite)
        {
            // VBConversions Note: Non-static class variable initialization is below.  Class variables cannot be initially assigned non-static values in C#.
            _Dato = DateTime.Now;
            _RettetDen = DateTime.Now;
            
            
            this.CompanyID = CompanyID;
            this.Navn = Navn;
            this.Brugernavn = Brugernavn;
            this.Kodeord = Kodeord;
            this.IP = IP;
            this.Dato = Dato.ToString();
            this.Informationssite = Informationssite;
            this.Administrationssite = Administrationssite;
            
        }
        
        public bool Aktiv
        {
            get
            {
                return _Aktiv;
            }
            set
            {
                _Aktiv = value;
            }
        }
        
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
        private string _CompanyName;
        public string CompanyName
        {
            get
            {
                if (ReferenceEquals(_CompanyName, null))
                {
                    _CompanyName = Virksomhed.GetCompanyName(CompanyID); //& "&nbsp;"
                }
                return _CompanyName;
            }
        }
        
        
        public string Navn
        {
            get
            {
                if (ReferenceEquals(_Navn, null))
                {
                    return "";
                }
                else
                {
                    return _Navn;
                }
            }
            set
            {
                _Navn = value;
            }
        }
        
        public string Brugernavn
        {
            get
            {
                if (ReferenceEquals(_BrugerNavn, null))
                {
                    return "";
                }
                else
                {
                    return _BrugerNavn;
                }
            }
            set
            {
                _BrugerNavn = value;
            }
        }
        
        public string Kodeord
        {
            get
            {
                if (ReferenceEquals(_KodeOrd, null))
                {
                    return "";
                }
                else
                {
                    return _KodeOrd;
                }
            }
            set
            {
                _KodeOrd = value;
            }
        }
        public int Password
        {
            get
            {
                return Kodeord.GetHashCode();
            }
        }
        
        public string IP
        {
            get
            {
                if (ReferenceEquals(_IP, null))
                {
                    return "";
                }
                else
                {
                    return _IP;
                }
            }
            set
            {
                _IP = value;
            }
        }
        
        public string Dato
        {
            get
            {
                if (_Dato == DateTime.MinValue)
                {
                    return "";
                }
                else
                {
                    return System.Convert.ToString( _Dato);
                }
            }
            set
            {
                if (Information.IsDate(value))
                {
                    _Dato = DateTime.Parse(value);
                }
                else
                {
                    _Dato = DateTime.MinValue;
                }
            }
        }
        
        public bool Informationssite
        {
            get
            {
                return _Informationssite;
            }
            set
            {
                _Informationssite = value;
            }
        }
        
        public bool Administrationssite
        {
            get
            {
                return _Administrationssite;
            }
            set
            {
                _Administrationssite = value;
            }
        }
        
        public static int Delete(int ID)
        {
            DBAccess db = new DBAccess();
            db.Parameters.Add(new SqlParameter("@ID", ID));
            int retval = db.ExecuteNonQuery("Co2Db_MedarbejderGruppe_Delete");
            return retval;
        }
        public static int Delete(MedarbejderGruppe mg)
        {
            return Delete(mg.ID);
        }
        
        
        public static int Insert(MedarbejderGruppe mg)
        {
            DBAccess db = new DBAccess();
            SqlParameter objParam = new SqlParameter("@ID", 0);
            objParam.Direction = ParameterDirection.Output;
            db.Parameters.Add(new SqlParameter("@CompanyID", mg.CompanyID));
            
            db.Parameters.Add(new SqlParameter("@Navn", SQLfunctions.SQLstr(mg.Navn)));
            db.Parameters.Add(new SqlParameter("@Brugernavn", SQLfunctions.SQLstr(mg.Brugernavn)));
            db.Parameters.Add(new SqlParameter("@Kodeord", SQLfunctions.SQLstr(mg.Kodeord)));
            db.Parameters.Add(new SqlParameter("@Password", mg.Password));
            db.Parameters.Add(new SqlParameter("@IP", SQLfunctions.SQLstr(mg.IP)));
            db.Parameters.Add(new SqlParameter("@Dato", SQLfunctions.SQLdate(mg.Dato)));
            
            db.Parameters.Add(new SqlParameter("@Informationssite", Funktioner.ToBool(mg.Informationssite)));
            db.Parameters.Add(new SqlParameter("@Administrationssite", Funktioner.ToBool(mg.Administrationssite)));
            
            db.Parameters.Add(new SqlParameter("@RettetAf", SQLfunctions.SQLstr(mg.CurUser)));
            db.Parameters.Add(new SqlParameter("@RettetIP", SQLfunctions.SQLstr(mg.CurIP)));
            
            db.Parameters.Add(objParam);
            int retval = db.ExecuteNonQuery("Co2Db_MedarbejderGruppe_Insert");
            if (retval == 1)
            {
                return int.Parse(objParam.Value.ToString());
            }
            else
            {
                return -1;
            }
        }
        public static int Insert(int CompanyID, string Navn, string Brugernavn, string Kodeord, string IP, DateTime Dato)
        {
            MedarbejderGruppe mg = new MedarbejderGruppe(CompanyID, Navn, Brugernavn, Kodeord, IP, Dato);
            return Insert(mg);
        }
        public static int Insert(int CompanyID, string Navn, string Brugernavn, string Kodeord, string IP, DateTime Dato, bool Informationssite, bool Administrationssite)
        {
            MedarbejderGruppe mg = new MedarbejderGruppe(CompanyID, Navn, Brugernavn, Kodeord, IP, Dato, Informationssite, Administrationssite);
            return Insert(mg);
        }
        
        public static int Update(MedarbejderGruppe mg)
        {
            DBAccess db = new DBAccess();
            db.Parameters.Add(new SqlParameter("@ID", mg.ID));
            db.Parameters.Add(new SqlParameter("@Aktiv", Funktioner.ToBool(mg.Aktiv)));
            db.Parameters.Add(new SqlParameter("@CompanyID", mg.CompanyID));
            db.Parameters.Add(new SqlParameter("@Navn", SQLfunctions.SQLstr(mg.Navn)));
            db.Parameters.Add(new SqlParameter("@Brugernavn", SQLfunctions.SQLstr(mg.Brugernavn)));
            db.Parameters.Add(new SqlParameter("@Kodeord", SQLfunctions.SQLstr(mg.Kodeord)));
            db.Parameters.Add(new SqlParameter("@Password", mg.Password));
            db.Parameters.Add(new SqlParameter("@IP", SQLfunctions.SQLstr(mg.IP)));
            db.Parameters.Add(new SqlParameter("@Dato", SQLfunctions.SQLdate(mg.Dato)));
            
            db.Parameters.Add(new SqlParameter("@Informationssite", Funktioner.ToBool(mg.Informationssite)));
            db.Parameters.Add(new SqlParameter("@Administrationssite", Funktioner.ToBool(mg.Administrationssite)));
            
            db.Parameters.Add(new SqlParameter("@RettetAf", SQLfunctions.SQLstr(mg.CurUser)));
            db.Parameters.Add(new SqlParameter("@RettetIP", SQLfunctions.SQLstr(mg.CurIP)));
            int retval = db.ExecuteNonQuery("Co2Db_MedarbejderGruppe_Update");
            return retval;
        }
        
        public static DataSet GetMedarbejderGruppeListe(int CompanyID)
        {
            DBAccess db = new DBAccess();
            db.Parameters.Add(new SqlParameter("@CompanyID", CompanyID));
            DataSet ds = db.ExecuteDataSet("Co2Db_MedarbejderGruppe_SelectAll");
            return ds;
        }
        
        public static DataSet GetMedarbejderGruppeSet(int ID)
        {
            DBAccess db = new DBAccess();
            db.Parameters.Add(new SqlParameter("@ID", ID));
            DataSet ds = db.ExecuteDataSet("Co2Db_MedarbejderGruppe_SelectOne");
            return ds;
        }
        
        public static MedarbejderGruppe GetMedarbejderGruppe(int ID)
        {
            DBAccess db = new DBAccess();
            db.Parameters.Add(new SqlParameter("@ID", ID));
            SqlDataReader dr = (SqlDataReader) (db.ExecuteReader("Co2Db_MedarbejderGruppe_SelectOne"));
            if (dr.HasRows)
            {
                MedarbejderGruppe mg = new MedarbejderGruppe();
                while (dr.Read())
                {
                    PopulateMedarbejderGruppe(dr, mg);
                }
                dr.Close();
                return mg;
            }
            else
            {
                dr.Close();
                return null;
            }
        }
        
        private static void PopulateMedarbejderGruppe(SqlDataReader dr, MedarbejderGruppe mg)
        {
            var with_1 = mg;
            //[Co2Db_MedarbejderGruppe_Update]
            //@ID int ,
            //@Aktiv bit ,
            //@CompanyID int ,
            
            //@Navn nvarchar(50) ,
            //@Brugernavn nvarchar(50) ,
            //@Kodeord nvarchar(50) ,
            //@Password nvarchar(36) ,
            //@IP nvarchar(250) ,
            //@Dato datetime ,
            //@RettetAf nvarchar(50) ,
            //@RettetIP nvarchar(15)
            
            with_1.ID = System.Convert.ToInt32(dr.DBtoInt("ID"));
            with_1.Aktiv = System.Convert.ToBoolean(dr.DBtoBool("Aktiv"));
            with_1.CompanyID = System.Convert.ToInt32(dr.DBtoInt("CompanyID"));
            with_1.Navn = dr.DBtoString("Navn");
            
            with_1.Dato = System.Convert.ToString(dr.DBtoDate("Dato"));
            with_1.Brugernavn = dr.DBtoString("Brugernavn");
            with_1.Kodeord = dr.DBtoString("Kodeord");
            with_1.IP = dr.DBtoString("IP");
            
            with_1.Informationssite = System.Convert.ToBoolean(dr.DBtoBool("Informationssite"));
            with_1.Administrationssite = System.Convert.ToBoolean(dr.DBtoBool("Administrationssite"));
            
            with_1.RettetAf = dr.DBtoString("RettetAf");
            with_1.RettetIP = dr.DBtoString("RettetIP");
            with_1.RettetDen = System.Convert.ToDateTime(dr.DBtoDate("RettetDen"));
            
        }
        
        public static string GetMedarbejderGruppeName(int ID)
        {
            DBAccess db = new DBAccess();
            db.AddParameter("@ID", ID);
            return System.Convert.ToString(db.ExecuteScalar("Co2Db_MedarbejderGruppe_SelectName"));
        }
        
        public static int GetMedarbejderGruppeCount(int CompanyID)
        {
            DBAccess db = new DBAccess();
            object res = null;
            int result = 0;
            db.Parameters.Add(new SqlParameter("@CompanyID", CompanyID));
            res = db.ExecuteScalar("Co2Db_MedarbejderGruppe_GetCount");
            if ((!ReferenceEquals(res, null)) && Information.IsNumeric(res))
            {
                result = System.Convert.ToInt32(res);
            }
            return result;
            //Return CType(db.ExecuteScalar("Co2Db_TilbudHospital_GetCount"), Integer)
        }
        
        public static DataSet GetMedarbejderGruppe_MemberCvrnr(int ID)
        {
            DBAccess db = new DBAccess();
            db.Parameters.Add(new SqlParameter("@ID", ID));
            DataSet ds = db.ExecuteDataSet("Co2Db_MedarbejderGruppe_MemberCvrnr");
            return ds;
        }
        
        
        public static int MedarbejderGruppe_MemberCvrnr_Insert(int ID, int CompanyID)
        {
            DBAccess db = new DBAccess();
            db.Parameters.Add(new SqlParameter("@ID", ID));
            db.Parameters.Add(new SqlParameter("@CompanyID", CompanyID));
            int retval = db.ExecuteNonQuery("Co2Db_MedarbejderGruppe_Insert_MemberCvrnr");
            return retval;
        }
        public static int MedarbejderGruppe_MemberCvrnr_Delete(int ID, int CompanyID)
        {
            DBAccess db = new DBAccess();
            db.Parameters.Add(new SqlParameter("@ID", ID));
            db.Parameters.Add(new SqlParameter("@CompanyID", CompanyID));
            int retval = db.ExecuteNonQuery("Co2Db_MedarbejderGruppe_Delete_MemberCvrnr");
            return retval;
        }
        
    }
    
    //End Namespace
}
