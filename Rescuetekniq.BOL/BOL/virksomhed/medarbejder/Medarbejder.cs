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
    
    public enum MedarbejderStatus
    {
        init = -1,
        slettet,
        opret,
        historik
    }
    
    //CREATE TABLE [vicjos1_sysadm].[Co2Db_Medarbejder](
    //	[ID] [int] IDENTITY(1,1) NOT NULL,
    //	[mgID] [int] NULL,
    //	[CompanyID] [int] NULL,
    //	[status] [int] NULL CONSTRAINT [DF_Co2Db_Medarbejder_status]  DEFAULT ((1)),
    //	[Cprnr] [nvarchar](11) NULL,
    //	[Birthday] [datetime] NULL,
    //	[Navn] [nvarchar](100) NULL,
    //	[Adresse1] [nvarchar](100) NULL,
    //	[Adresse2] [nvarchar](100) NULL,
    //	[Postnr] [nvarchar](10) NULL,
    //	[Bynavn] [nvarchar](50) NULL,
    //	[LandID] [int] NULL,
    //	[Telefon] [nvarchar](16) NULL,
    //	[Mobil] [nvarchar](16) NULL,
    //	[Fax] [nvarchar](16) NULL,
    //	[Email] [nvarchar](250) NULL,
    //	[HospitalsforsikringIndmeldsDato] [datetime] NULL,
    //	[AfkaldserklaringsDato] [datetime] NULL,
    //	[FratradelsesDato] [datetime] NULL,
    //	[OrlovStart] [datetime] NULL,
    //	[OrlovSlut] [datetime] NULL,
    //	[RettetAf] [nvarchar](50) NULL,
    //	[RettetDen] [datetime] NULL CONSTRAINT [DF_Co2Db_Medarbejder_RettetDen]  DEFAULT (getdate()),
    //	[RettetIP] [nvarchar](15) NULL
    
    public class Medarbejder : BaseObject
    {
        
        public Medarbejder()
        {
            
            ID = -1;
            Status = MedarbejderStatus.init;
            LandID = System.Convert.ToInt32(Combobox.GetValueByTitle("Landekode", "Danmark"));
            RettetDen = DateTime.Now;
        }
        public Medarbejder(int CompanyID)
        {
            
            this.CompanyID = CompanyID;
        }
        
        private MedarbejderStatus _Status = MedarbejderStatus.init;
        public MedarbejderStatus Status
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
        
        private int _mgID;
        public int mgID
        {
            get
            {
                return _mgID;
            }
            set
            {
                _mgID = value;
            }
        }
        
        private int _CompanyID;
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
        
        private string _Cprnr = "";
        public string Cprnr
        {
            get
            {
                return _Cprnr;
            }
            set
            {
                _Cprnr = value;
                Birthday = CprnrToDate(_Cprnr);
            }
        }
        private DateTime _Birthday = DateTime.MinValue;
        public DateTime Birthday
        {
            get
            {
                if (_Birthday == DateTime.MinValue)
                {
                    return default(DateTime);
                }
                else
                {
                    return _Birthday;
                }
            }
            set
            {
                _Birthday = value;
            }
        }
        
        public DateTime CprnrToDate(string cprnr)
        {
            DateTime res = default(DateTime);
            int dd = 1;
            int mm = 1;
            int yy = 0;
            int year = 1900;
            int lbnr = 0;
            if (cprnr.Length == 11)
            {
                dd = Funktioner.ToInt(cprnr.Substring(0, 2));
                mm = Funktioner.ToInt(cprnr.Substring(2, 2));
                yy = Funktioner.ToInt(cprnr.Substring(4, 2));
                lbnr = Funktioner.ToInt(cprnr.Substring(7, 4));
                if (lbnr <= 3999)
                {
                    year = 1900;
                }
                else if (lbnr >= 5000 & lbnr <= 8999)
                {
                    if (yy <= 57)
                    {
                        year = 2000;
                    }
                    else
                    {
                        year = 1800;
                    }
                }
                else if ((lbnr >= 4000 & lbnr <= 4999) || (lbnr >= 9000 & lbnr <= 9999))
                {
                    if (yy <= 36)
                    {
                        year = 2000;
                    }
                    else
                    {
                        year = 1900;
                    }
                }
            }
            res = DateAndTime.DateSerial(yy + year, mm, dd);
            
            return res;
        }
        
        
        private string _Navn = "";
        public string Navn
        {
            get
            {
                return _Navn;
            }
            set
            {
                _Navn = value;
            }
        }
        
        private string _Adresse1 = "";
        public string Adresse1
        {
            get
            {
                return _Adresse1;
            }
            set
            {
                _Adresse1 = value;
            }
        }
        
        private string _Adresse2 = "";
        public string Adresse2
        {
            get
            {
                return _Adresse2;
            }
            set
            {
                _Adresse2 = value;
            }
        }
        
        private string _Postnr = "";
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
        
        private string _Bynavn = "";
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
        
        private int _LandID = -1;
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
                return Combobox.GetTitleByValue("landekode", System.Convert.ToString(LandID));
            }
        }
        
        private string _Telefon = "";
        public string Telefon
        {
            get
            {
                return _Telefon;
            }
            set
            {
                _Telefon = value;
            }
        }
        
        private string _Mobil = "";
        public string Mobil
        {
            get
            {
                return _Mobil;
            }
            set
            {
                _Mobil = value;
            }
        }
        
        private string _Fax = "";
        public string Fax
        {
            get
            {
                return _Fax;
            }
            set
            {
                _Fax = value;
            }
        }
        
        private string _Email = "";
        public string Email
        {
            get
            {
                return _Email;
            }
            set
            {
                _Email = value;
            }
        }
        
        private DateTime _HospitalsforsikringIndmeldsDato = DateTime.MinValue;
        public DateTime HospitalsforsikringIndmeldsDato
        {
            get
            {
                if (_HospitalsforsikringIndmeldsDato == DateTime.MinValue)
                {
                    return default(DateTime);
                }
                else
                {
                    return _HospitalsforsikringIndmeldsDato;
                }
            }
            set
            {
                _HospitalsforsikringIndmeldsDato = value;
            }
        }
        
        private DateTime _AfkaldserklaringsDato = DateTime.MinValue;
        public DateTime AfkaldserklaringsDato
        {
            get
            {
                if (_AfkaldserklaringsDato == DateTime.MinValue)
                {
                    return default(DateTime);
                }
                else
                {
                    return _AfkaldserklaringsDato;
                }
            }
            set
            {
                _AfkaldserklaringsDato = value;
            }
        }
        
        private DateTime _FratradelsesDato = DateTime.MaxValue;
        public DateTime FratradelsesDato
        {
            get
            {
                if (_FratradelsesDato == DateTime.MaxValue)
                {
                    return default(DateTime);
                }
                else
                {
                    return _FratradelsesDato;
                }
            }
            set
            {
                _FratradelsesDato = value;
            }
        }
        
        private DateTime _OrlovStart = DateTime.MinValue;
        public DateTime OrlovStart
        {
            get
            {
                if (_OrlovStart == DateTime.MinValue)
                {
                    return default(DateTime);
                }
                else
                {
                    return _OrlovStart;
                }
            }
            set
            {
                _OrlovStart = value;
            }
        }
        
        private DateTime _OrlovSlut = DateTime.MinValue;
        public DateTime OrlovSlut
        {
            get
            {
                if (_OrlovSlut == DateTime.MinValue)
                {
                    return default(DateTime);
                }
                else
                {
                    return _OrlovSlut;
                }
            }
            set
            {
                _OrlovSlut = value;
            }
        }
        
        public static int Delete(int ID)
        {
            DBAccess db = new DBAccess();
            db.Parameters.Add(new SqlParameter("@ID", ID));
            int retval = db.ExecuteNonQuery("Co2Db_Medarbejder_Delete");
            return retval;
        }
        public static int Delete(Medarbejder mg)
        {
            return Delete(mg.ID);
        }
        
        public static int Insert(Medarbejder mg)
        {
            DBAccess db = new DBAccess();
            SqlParameter ID = new SqlParameter("@ID", 0);
            ID.Direction = ParameterDirection.Output;
            
            db.Parameters.Add(new SqlParameter("@mgID", mg.mgID));
            db.Parameters.Add(new SqlParameter("@CompanyID", mg.CompanyID));
            //db.Parameters.Add(New SqlParameter("@status", ToInt(mg.Status)))
            mg.Status = MedarbejderStatus.opret;
            
            db.Parameters.Add(new SqlParameter("@Cprnr", SQLfunctions.SQLstr(mg.Cprnr)));
            db.Parameters.Add(new SqlParameter("@Birthday", SQLfunctions.SQLdate(mg.Birthday)));
            
            //cmd.Parameters.Add("@Description", SqlDbType.NVarChar).Value = category.Description
            
            db.Parameters.Add(new SqlParameter("@Navn", SQLfunctions.SQLstr(mg.Navn)));
            db.Parameters.Add(new SqlParameter("@Adresse1", SQLfunctions.SQLstr(mg.Adresse1)));
            db.Parameters.Add(new SqlParameter("@Adresse2", SQLfunctions.SQLstr(mg.Adresse2)));
            db.Parameters.Add(new SqlParameter("@Postnr", SQLfunctions.SQLstr(mg.Postnr)));
            db.Parameters.Add(new SqlParameter("@Bynavn", SQLfunctions.SQLstr(mg.Bynavn)));
            db.Parameters.Add(new SqlParameter("@LandID", Funktioner.ToInt(mg.LandID)));
            
            db.Parameters.Add(new SqlParameter("@Telefon", SQLfunctions.SQLstr(mg.Telefon)));
            db.Parameters.Add(new SqlParameter("@Mobil", SQLfunctions.SQLstr(mg.Mobil)));
            db.Parameters.Add(new SqlParameter("@Fax", SQLfunctions.SQLstr(mg.Fax)));
            db.Parameters.Add(new SqlParameter("@Email", SQLfunctions.SQLstr(mg.Email)));
            
            db.Parameters.Add(new SqlParameter("@HospitalsforsikringIndmeldsDato", SQLfunctions.SQLdate(mg.HospitalsforsikringIndmeldsDato)));
            db.Parameters.Add(new SqlParameter("@AfkaldserklaringsDato", SQLfunctions.SQLdate(mg.AfkaldserklaringsDato)));
            db.Parameters.Add(new SqlParameter("@FratradelsesDato", SQLfunctions.SQLdate(mg.FratradelsesDato)));
            db.Parameters.Add(new SqlParameter("@OrlovStart", SQLfunctions.SQLdate(mg.OrlovStart)));
            db.Parameters.Add(new SqlParameter("@OrlovSlut", SQLfunctions.SQLdate(mg.OrlovSlut)));
            
            db.Parameters.Add(new SqlParameter("@RettetAf", SQLfunctions.SQLstr(CurrentUserName)));
            db.Parameters.Add(new SqlParameter("@RettetIP", SQLfunctions.SQLstr(CurrentUserIP)));
            
            db.Parameters.Add(ID);
            int retval = db.ExecuteNonQuery("Co2Db_Medarbejder_Insert");
            if (retval == 1)
            {
                mg.ID = int.Parse(ID.Value.ToString());
                return mg.ID; //Integer.Parse(ID.Value.ToString)
            }
            else
            {
                return -1;
            }
            
        }
        //Public Shared Function Insert(ByVal CompanyID As Integer, ByVal Navn As String, ByVal Brugernavn As String, ByVal Kodeord As String, ByVal IP As String, ByVal Dato As Date) As Integer
        //	Dim mg As Medarbejder = New Medarbejder(CompanyID, Navn, Brugernavn, Kodeord, IP, Dato)
        //	Return Insert(mg)
        //End Function
        
        public static int Update(Medarbejder mg)
        {
            DBAccess db = new DBAccess();
            db.Parameters.Add(new SqlParameter("@ID", mg.ID));
            
            db.Parameters.Add(new SqlParameter("@mgID", mg.mgID));
            db.Parameters.Add(new SqlParameter("@CompanyID", mg.CompanyID));
            //db.Parameters.Add(New SqlParameter("@status", ToInt(mg.Status)))
            
            db.Parameters.Add(new SqlParameter("@Cprnr", SQLfunctions.SQLstr(mg.Cprnr)));
            db.Parameters.Add(new SqlParameter("@Birthday", SQLfunctions.SQLdate(mg.Birthday)));
            
            db.Parameters.Add(new SqlParameter("@Navn", SQLfunctions.SQLstr(mg.Navn)));
            db.Parameters.Add(new SqlParameter("@Adresse1", SQLfunctions.SQLstr(mg.Adresse1)));
            db.Parameters.Add(new SqlParameter("@Adresse2", SQLfunctions.SQLstr(mg.Adresse2)));
            db.Parameters.Add(new SqlParameter("@Postnr", SQLfunctions.SQLstr(mg.Postnr)));
            db.Parameters.Add(new SqlParameter("@Bynavn", SQLfunctions.SQLstr(mg.Bynavn)));
            db.Parameters.Add(new SqlParameter("@LandID", Funktioner.ToInt(mg.LandID)));
            
            db.Parameters.Add(new SqlParameter("@Telefon", SQLfunctions.SQLstr(mg.Telefon)));
            db.Parameters.Add(new SqlParameter("@Mobil", SQLfunctions.SQLstr(mg.Mobil)));
            db.Parameters.Add(new SqlParameter("@Fax", SQLfunctions.SQLstr(mg.Fax)));
            db.Parameters.Add(new SqlParameter("@Email", SQLfunctions.SQLstr(mg.Email)));
            
            db.Parameters.Add(new SqlParameter("@HospitalsforsikringIndmeldsDato", SQLfunctions.SQLdate(mg.HospitalsforsikringIndmeldsDato)));
            db.Parameters.Add(new SqlParameter("@AfkaldserklaringsDato", SQLfunctions.SQLdate(mg.AfkaldserklaringsDato)));
            db.Parameters.Add(new SqlParameter("@FratradelsesDato", SQLfunctions.SQLdate(mg.FratradelsesDato)));
            db.Parameters.Add(new SqlParameter("@OrlovStart", SQLfunctions.SQLdate(mg.OrlovStart)));
            db.Parameters.Add(new SqlParameter("@OrlovSlut", SQLfunctions.SQLdate(mg.OrlovSlut)));
            
            db.Parameters.Add(new SqlParameter("@RettetAf", SQLfunctions.SQLstr(CurrentUserName)));
            db.Parameters.Add(new SqlParameter("@RettetIP", SQLfunctions.SQLstr(CurrentUserIP)));
            
            int retval = db.ExecuteNonQuery("Co2Db_Medarbejder_Update");
            return retval;
        }
        
        public static DataSet GetMedarbejderListe(int mgID)
        {
            DBAccess db = new DBAccess();
            db.Parameters.Add(new SqlParameter("@mgID", mgID));
            DataSet ds = db.ExecuteDataSet("Co2Db_Medarbejder_SelectAll");
            return ds;
        }
        
        public static DataSet GetMedarbejderListe_MedarbejderGruppe(int mgCompanyID, string search = "")
        {
            
            //[vicjos1_sysadm].[Co2Db_Medarbejder_SelectMedArbGrp]()
            //@mgCompanyID int,
            //@Search varchar(250) = ''
            
            DBAccess db = new DBAccess();
            db.Parameters.Add(new SqlParameter("@mgCompanyID", mgCompanyID));
            db.Parameters.Add(new SqlParameter("@Search", search));
            DataSet ds = db.ExecuteDataSet("Co2Db_Medarbejder_SelectMedArbGrp");
            return ds;
        }
        
        
        public static DataSet GetMedarbejderSet(int ID)
        {
            DBAccess db = new DBAccess();
            db.Parameters.Add(new SqlParameter("@ID", ID));
            DataSet ds = db.ExecuteDataSet("Co2Db_Medarbejder_SelectOne");
            return ds;
        }
        
        public static Medarbejder GetMedarbejder(int ID)
        {
            DBAccess db = new DBAccess();
            db.Parameters.Add(new SqlParameter("@ID", ID));
            SqlDataReader dr = (SqlDataReader) (db.ExecuteReader("Co2Db_Medarbejder_SelectOne"));
            if (dr.HasRows)
            {
                Medarbejder mg = new Medarbejder();
                while (dr.Read())
                {
                    PopulateMedarbejder(dr, mg);
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
        
        private static void PopulateMedarbejder(SqlDataReader dr, Medarbejder mg)
        {
            var with_1 = mg;
            with_1.ID = System.Convert.ToInt32(dr.DBtoInt("ID"));
            with_1.mgID = System.Convert.ToInt32(dr.DBtoInt("mgID"));
            with_1.CompanyID = System.Convert.ToInt32(dr.DBtoInt("CompanyID"));
            with_1.Status = (RescueTekniq.BOL.MedarbejderStatus) (dr.DBtoInt("status"));
            
            with_1.Cprnr = dr.DBtoString("Cprnr");
            with_1.Birthday = System.Convert.ToDateTime(dr.DBtoDate("Birthday"));
            
            with_1.Navn = dr.DBtoString("Navn");
            with_1.Adresse1 = dr.DBtoString("Adresse1");
            with_1.Adresse2 = dr.DBtoString("Adresse2");
            with_1.Postnr = dr.DBtoString("Postnr");
            with_1.Bynavn = dr.DBtoString("Bynavn");
            with_1.LandID = System.Convert.ToInt32(dr.DBtoInt("LandID"));
            
            with_1.Telefon = dr.DBtoString("Telefon");
            with_1.Mobil = dr.DBtoString("Mobil");
            with_1.Fax = dr.DBtoString("Fax");
            with_1.Email = dr.DBtoString("Email");
            
            with_1.HospitalsforsikringIndmeldsDato = System.Convert.ToDateTime(dr.DBtoDate("HospitalsforsikringIndmeldsDato"));
            with_1.AfkaldserklaringsDato = System.Convert.ToDateTime(dr.DBtoDate("AfkaldserklaringsDato"));
            with_1.FratradelsesDato = System.Convert.ToDateTime(dr.DBtoDate("FratradelsesDato"));
            with_1.OrlovStart = System.Convert.ToDateTime(dr.DBtoDate("OrlovStart"));
            with_1.OrlovSlut = dr.DBtoDate("OrlovSlut");
            
            with_1.RettetAf = dr.DBtoString("RettetAf");
            with_1.RettetIP = dr.DBtoString("RettetIP");
            with_1.RettetDen = System.Convert.ToDateTime(dr.DBtoDate("RettetDen"));
            
        }
        
        public static string GetMedarbejderName(int ID)
        {
            DBAccess db = new DBAccess();
            db.AddParameter("@ID", ID);
            return System.Convert.ToString(db.ExecuteScalar("Co2Db_Medarbejder_SelectName"));
        }
        
        public static int GetMedarbejderCount(int mgID)
        {
            DBAccess db = new DBAccess();
            object res = null;
            int result = 0;
            db.Parameters.Add(new SqlParameter("@mgID", mgID));
            res = db.ExecuteScalar("Co2Db_Medarbejder_GetCount");
            if ((!ReferenceEquals(res, null)) && Information.IsNumeric(res))
            {
                result = System.Convert.ToInt32(res);
            }
            return result;
            //Return CType(db.ExecuteScalar("Co2Db_TilbudHospital_GetCount"), Integer)
        }
        
        //	SELECT ID, mgNavn, mgCompanyID, Cvrnr, Firmanavn,
        //		medID, medCompanyID, Cprnr, Navn, Adresse1, Adresse2, Postnr, Bynavn, Land, Telefon, Mobil, Fax,
        //		Email, HospitalsforsikringIndmeldsDato, AfkaldserklaringsDato, FratradelsesDato, OrlovStart, OrlovSlut
        //		FROM vw_Co2Db_MedarbejderGruppe_Medarbejder
        public static DataSet SearchMedarbejderListe(int mgCompanyID, string search = "")
        {
            string[] arr = search.Split(' ');
            DataSet ds = new DataSet();
            DataSet dsTemp = new DataSet();
            bool flag = false;
            DBAccess db = new DBAccess();
            foreach (string s in arr)
            {
                db.AddParameter("@mgCompanyID", mgCompanyID);
                db.AddParameter("@Search", s);
                
                dsTemp = db.ExecuteDataSet("Co2Db_Medarbejder_SelectMedArbGrp");
                db.Parameters.Clear();
                ds.Merge(dsTemp);
                if (flag == false)
                {
                    DataColumn[] pk = new DataColumn[2];
                    pk[0] = ds.Tables[0].Columns["medID"];
                    ds.Tables[0].PrimaryKey = pk;
                    flag = true;
                }
            }
            return ds;
        }
        
        //	SELECT ID, mgNavn, mgCompanyID, Cvrnr, Firmanavn,
        //		medID, medCompanyID, Cprnr, Navn, Adresse1, Adresse2, Postnr, Bynavn, Land, Telefon, Mobil, Fax,
        //		Email, HospitalsforsikringIndmeldsDato, AfkaldserklaringsDato, FratradelsesDato, OrlovStart, OrlovSlut
        //		FROM vw_Co2Db_MedarbejderGruppe_Medarbejder
        public static DataSet SearchMedarbejderFliter(int mgCompanyID, int mgID, int Forsikring, int Status, DateTime dato)
        {
            //Dim arr As String() = search.Split(" "c)
            DataSet ds = new DataSet();
            DataSet dsTemp = new DataSet();
            bool flag = false;
            DBAccess db = new DBAccess();
            //For Each s As String In arr
            
            db.AddParameter("@mgCompanyID", mgCompanyID);
            db.AddParameter("@mgID", mgID);
            db.AddParameter("@forsikring", Forsikring);
            db.AddParameter("@status", Status);
            if (Information.IsDate(dato))
            {
                if (dato != new DateTime(2018, 8, 6))
                {
                    db.AddParameter("@dato", dato);
                }
            }
            
            dsTemp = db.ExecuteDataSet("Co2Db_Medarbejder_SelectFilter");
            db.Parameters.Clear();
            ds.Merge(dsTemp);
            if (flag == false)
            {
                DataColumn[] pk = new DataColumn[2];
                pk[0] = ds.Tables[0].Columns["medID"];
                ds.Tables[0].PrimaryKey = pk;
                flag = true;
            }
            //Next
            return ds;
        }
        
        //	Navn Vej Postnr fødselsdato
        //ALTER PROCEDURE [vicjos1_sysadm].[Co2Db_Medarbejder_SelectMedarbejderGlobal]
        //	@mgCompanyID int = -1,
        //	@navn nvarchar(50) = '' ,
        //	@vej nvarchar(50) = '' ,
        //	@postnr nvarchar(50) = '' ,
        //	@birthday datetime = null ,
        //	@Search nvarchar(250) = ''
        
        public static DataSet SearchMedarbejderGlobal(int mgCompanyID, string search, string navn, string vej, string postnr, string birthday)
        {
            string[] arr = search.Split(' ');
            DataSet ds = new DataSet();
            DataSet dsTemp = new DataSet();
            bool flag = false;
            DBAccess db = new DBAccess();
            foreach (string s in arr)
            {
                db.AddParameter("@mgCompanyID", mgCompanyID);
                db.AddParameter("@navn", SQLfunctions.SQLstr(navn));
                db.AddParameter("@vej", SQLfunctions.SQLstr(vej));
                db.AddParameter("@postnr", SQLfunctions.SQLstr(postnr));
                db.AddParameter("@birthday", SQLfunctions.SQLdate(birthday));
                db.AddParameter("@Search", SQLfunctions.SQLstr(s));
                
                dsTemp = db.ExecuteDataSet("Co2Db_Medarbejder_SelectMedarbejderGlobal");
                db.Parameters.Clear();
                ds.Merge(dsTemp);
                if (flag == false)
                {
                    DataColumn[] pk = new DataColumn[2];
                    pk[0] = ds.Tables[0].Columns["medID"];
                    ds.Tables[0].PrimaryKey = pk;
                    flag = true;
                }
            }
            return ds;
        }
        
        //CREATE TABLE [vicjos1_sysadm].[Co2Db_Medarbejder](
        //	[ID] [int] IDENTITY(1,1) NOT NULL,
        //	[mgID] [int] NULL,
        //	[CompanyID] [int] NULL,
        //	[status] [int] NULL CONSTRAINT [DF_Co2Db_Medarbejder_status]  DEFAULT ((1)),
        //	[Cprnr] [nvarchr](10) NULL,
        //	[Navn] [nvarchar](100) NULL,
        //	[Adresse1] [nvarchar](100) NULL,
        //	[Adresse2] [nvarchar](100) NULL,
        //	[Postnr] [nvarchar](10) NULL,
        //	[Bynavn] [nvarchar](50) NULL,
        //	[LandID] [int] NULL,
        //	[Telefon] [nvarchar](16) NULL,
        //	[Mobil] [nvarchar](16) NULL,
        //	[Fax] [nvarchar](16) NULL,
        //	[Email] [nvarchar](250) NULL,
        //	[HospitalsforsikringIndmeldsDato] [datetime] NULL,
        //	[AfkaldserklaringsDato] [datetime] NULL,
        //	[FratradelsesDato] [datetime] NULL,
        //	[OrlovStart] [datetime] NULL,
        //	[OrlovSlut] [datetime] NULL,
        //	[RettetAf] [nvarchar](50) NULL,
        //	[RettetDen] [datetime] NULL CONSTRAINT [DF_Co2Db_Medarbejder_RettetDen]  DEFAULT (getdate()),
        //	[RettetIP] [nvarchar](15) NULL
        
        public static DataSet SearchMedarbejder(int mgCompanyID, string navn)
        {
            string[] arr = navn.Split(' ');
            DataSet ds = new DataSet();
            DataSet dsTemp = new DataSet();
            bool flag = false;
            DBAccess db = new DBAccess();
            foreach (string s in arr)
            {
                db.AddParameter("@mgCompanyID", mgCompanyID);
                db.AddParameter("@Search", s);
                
                dsTemp = db.ExecuteDataSet("Co2Db_Medarbejder_SelectBySearch");
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
        
    }
    
    //End Namespace
}
