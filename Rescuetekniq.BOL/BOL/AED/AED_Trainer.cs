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
    
    public class AED_Trainer : BaseObject
    {
        
#region  New
        
        public AED_Trainer()
        {
            
        }
        
        public AED_Trainer(int ID)
        {
            this.ID = ID;
            
            if (ID > 0)
            {
                DBAccess db = new DBAccess();
                db.AddInt("ID", ID);
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
        public AED_Trainer(System.Guid GUID)
        {
            
            if (GUID != System.Guid.Empty)
            {
                DBAccess db = new DBAccess();
                db.AddGuid("GUID", GUID);
                SqlDataReader dr = (SqlDataReader) (db.ExecuteReader(_SQLSelectByGuid));
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
        
        private AEDStatusEnum _Status = AEDStatusEnum.Aktiv;
        private int _CompanyID;
        private int _ModelID;
        private string _SerialNo; //(50)
        
        private DateTime _DeleveryDate;
        private DateTime _WarencyExpireDate;
        private DateTime _BatteryActivationDate;
        private DateTime _BatteryWarencyExpireDate;
        
        private string _FirmwareVersion; //(50)
        private string _FirmwareLanguage; //(50)
        
        private AEDBilagStatus _BilagStatus = AEDBilagStatus.Initialize;
        private DateTime _BilagSendtDato;
        private DateTime _BilagModtagetDato;
        
        private string _ResponsibleName; //(50)
        private string _ResponsiblePhone; //(16)
        private string _ResponsibleEmail; //(250)
        
        private string _LocationAdresse; //(10)
        private string _LocationPostnr; //(16)
        private string _LocationBy; //(50)
        private string _LocationState; //(50)
        private int _LocationLandID = 45; // Danmark
        private string _Location; //(max)
        
        private DateTime _RegisterretCSI;
        private string _RegisterretAF;
        
        private string _Note; //(max)
        
        private RescueTekniq.BOL.Virksomhed _virksomhed = new RescueTekniq.BOL.Virksomhed();
        private RescueTekniq.BOL.Vare _vare = new RescueTekniq.BOL.Vare();
        private List<AED_Electrod> _electrodList;
        private List<AED_Battery> _batteryList;
        
#endregion
        
#region  Properties
        
        public AEDStatusEnum Status
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
        public int ModelID
        {
            get
            {
                return _ModelID;
            }
            set
            {
                _ModelID = value;
            }
        }
        
        public string SerialNo
        {
            get
            {
                return _SerialNo;
            }
            set
            {
                _SerialNo = value;
            }
        }
        
        public string FirmwareVersion
        {
            get
            {
                return _FirmwareVersion;
            }
            set
            {
                _FirmwareVersion = value;
            }
        }
        public string FirmwareLanguage
        {
            get
            {
                return _FirmwareLanguage;
            }
            set
            {
                _FirmwareLanguage = value;
            }
        }
        
        public DateTime DeleveryDate
        {
            get
            {
                return _DeleveryDate;
            }
            set
            {
                _DeleveryDate = value;
            }
        }
        
        public DateTime WarencyExpireDate
        {
            get
            {
                return _WarencyExpireDate;
            }
            set
            {
                _WarencyExpireDate = value;
            }
        }
        
        public DateTime BatteryActivationDate
        {
            get
            {
                return _BatteryActivationDate;
            }
            set
            {
                _BatteryActivationDate = value;
            }
        }
        
        public DateTime BatteryWarencyExpireDate
        {
            get
            {
                return _BatteryWarencyExpireDate;
            }
            set
            {
                _BatteryWarencyExpireDate = value;
            }
        }
        
        public AEDBilagStatus BilagStatus
        {
            get
            {
                return _BilagStatus;
            }
            set
            {
                _BilagStatus = value;
            }
        }
        public DateTime BilagSendtDato
        {
            get
            {
                return _BilagSendtDato;
            }
            set
            {
                _BilagSendtDato = value;
            }
        }
        public DateTime BilagModtagetDato
        {
            get
            {
                return _BilagModtagetDato;
            }
            set
            {
                _BilagModtagetDato = value;
            }
        }
        
        
        public string ResponsibleName
        {
            get
            {
                return _ResponsibleName;
            }
            set
            {
                _ResponsibleName = value;
            }
        }
        
        public string ResponsiblePhone
        {
            get
            {
                return _ResponsiblePhone;
            }
            set
            {
                _ResponsiblePhone = value;
            }
        }
        
        public string ResponsibleEmail
        {
            get
            {
                return _ResponsibleEmail;
            }
            set
            {
                _ResponsibleEmail = value;
            }
        }
        
        public string Location
        {
            get
            {
                return _Location;
            }
            set
            {
                _Location = value;
            }
        }
        
        public string LocationAdresse
        {
            get
            {
                return _LocationAdresse;
            }
            set
            {
                _LocationAdresse = value;
            }
        }
        public string LocationPostnr
        {
            get
            {
                return _LocationPostnr;
            }
            set
            {
                _LocationPostnr = value;
            }
        }
        public string LocationBy
        {
            get
            {
                return _LocationBy;
            }
            set
            {
                _LocationBy = value;
            }
        }
        public string LocationState
        {
            get
            {
                return _LocationState;
            }
            set
            {
                _LocationState = value;
            }
        }
        public int LocationLandID
        {
            get
            {
                return _LocationLandID;
            }
            set
            {
                _LocationLandID = value;
            }
        }
        
        public string LocationPostnrBy
        {
            get
            {
                string res = "";
                switch (LocationLandID)
                {
                    case 45:
                    case 298:
                    case 299:
                        res = LocationPostnr + " " + LocationBy;
                        break;
                    case 1:
                        res = LocationBy + ", " + LocationState.ToUpper() + " " + LocationPostnr;
                        break;
                        //Washington, DC 20546-0001
                    default:
                        res = LocationPostnr + " " + LocationBy;
                        break;
                }
                return res.Trim();
            }
        }
        
        public DateTime RegisterretCSI
        {
            get
            {
                return _RegisterretCSI;
            }
            set
            {
                _RegisterretCSI = value;
            }
        }
        public string RegisterretAF
        {
            get
            {
                return _RegisterretAF;
            }
            set
            {
                _RegisterretAF = value;
            }
        }
        
        public string Note
        {
            get
            {
                return _Note;
            }
            set
            {
                _Note = value;
            }
        }
        
        public string Firmanavn
        {
            get
            {
                return Virksomhed.Firmanavn;
            }
            set
            {
                
            }
        }
        
        public string Varenr
        {
            get
            {
                return Vare.VareNr;
            }
            set
            {
                
            }
        }
        public string Navn
        {
            get
            {
                return Vare.Navn;
            }
            set
            {
                
            }
        }
        
        
        
        public RescueTekniq.BOL.Vare Vare
        {
            get
            {
                try
                {
                    if (_ModelID > 0)
                    {
                        if (!_vare.loaded)
                        {
                            _vare = Vare.GetVare(_ModelID);
                        }
                        else if (_vare.ID != _ModelID)
                        {
                            _vare = Vare.GetVare(_ModelID);
                        }
                    }
                }
                catch (Exception)
                {
                }
                return _vare;
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
        
        private bool _ElectrodListLoaded = false;
        private int _ElectrodListAED_FK = -1;
        public System.Collections.Generic.List<AED_Electrod> ElectrodList
        {
            get
            {
                if (!_ElectrodListLoaded)
                {
                    _electrodList = AED_Electrod.GetElectrodList(ID);
                    _ElectrodListLoaded = true;
                    _ElectrodListAED_FK = ID;
                }
                else if (_ElectrodListAED_FK != ID)
                {
                    _electrodList = AED_Electrod.GetElectrodList(ID);
                    _ElectrodListLoaded = true;
                    _ElectrodListAED_FK = ID;
                }
                return _electrodList;
            }
        }
        public string ElectrodListText
        {
            get
            {
                StringBuilder res = new StringBuilder();
                foreach (AED_Electrod item in ElectrodList)
                {
                    res.Append(item.ElectrodTypeText);
                    res.Append(item.ElectrodExpireDate.ToString("dd MMMM yyyy")); //(" MMM. yyyy"))
                    res.Append("<br />");
                    res.AppendLine();
                }
                return res.ToString();
            }
        }
        
        private bool _BatteryListLoaded = false;
        private int _BatteryListAED_FK = -1;
        public System.Collections.Generic.List<AED_Battery> BatteryList
        {
            get
            {
                if (!_BatteryListLoaded)
                {
                    _batteryList = AED_Battery.GetBatteryList(ID);
                    _BatteryListLoaded = true;
                    _BatteryListAED_FK = ID;
                }
                else if (_BatteryListAED_FK != ID)
                {
                    _batteryList = AED_Battery.GetBatteryList(ID);
                    _BatteryListLoaded = true;
                    _BatteryListAED_FK = ID;
                }
                return _batteryList;
            }
        }
        public string BatteryListText
        {
            get
            {
                StringBuilder res = new StringBuilder();
                foreach (AED_Battery item in BatteryList)
                {
                    //res.Append(.BatteryTypeText)
                    res.Append(item.BatteryExpireDate.ToString("dd MMM. yyyy"));
                    res.Append("<br />");
                    res.AppendLine();
                }
                return res.ToString();
            }
        }
        
        public int AEDcount
        {
            get
            {
                return GetAEDcountByCompany(CompanyID);
            }
        }
        
#endregion
        
#region  Shared Populate
        
        private static void AddParms(ref DBAccess db, AED_Trainer c)
        {
            var with_1 = c;
            db.AddInt("Status", (System.Int32) with_1.Status);
            db.AddInt("CompanyID", with_1.CompanyID);
            
            db.AddInt("ModelID", with_1.ModelID);
            db.AddNVarChar("SerialNo", with_1.SerialNo, 50);
            db.AddNVarChar("FirmwareVersion", with_1.FirmwareVersion, 50);
            db.AddNVarChar("FirmwareLanguage", with_1._FirmwareLanguage, 50);
            
            db.AddDateTime("DeleveryDate", with_1.DeleveryDate);
            db.AddDateTime("WarencyExpireDate", with_1.WarencyExpireDate);
            db.AddDateTime("BatteryActivationDate", with_1.BatteryActivationDate);
            db.AddDateTime("BatteryWarencyExpireDate", with_1.BatteryWarencyExpireDate);
            
            db.AddInt("BilagStatus", (System.Int32) with_1.BilagStatus);
            db.AddDateTime("BilagSendtDato", with_1.BilagSendtDato);
            db.AddDateTime("BilagModtagetDato", with_1.BilagModtagetDato);
            
            db.AddNVarChar("ResponsibleName", with_1.ResponsibleName, 50);
            db.AddNVarChar("ResponsiblePhone", with_1.ResponsiblePhone, 16);
            db.AddNVarChar("ResponsibleEmail", with_1.ResponsibleEmail, 250);
            
            db.AddNVarChar("LocationAdresse", with_1.LocationAdresse, 50);
            db.AddNVarChar("LocationPostnr", with_1.LocationPostnr, 16);
            db.AddNVarChar("LocationBy", with_1.LocationBy, 50);
            db.AddNVarChar("LocationState", with_1.LocationState, 50);
            db.AddInt("LocationLandID", with_1.LocationLandID);
            db.AddNVarChar("Location", with_1.Location, -1);
            
            db.AddDateTime("RegisterretCSI", with_1.RegisterretCSI);
            db.AddNVarChar("RegisterretAF", with_1.RegisterretAF, 50);
            
            db.AddNVarChar("Note", with_1.Note, -1);
            
            AddParmsStandard(db, c);
        }
        
        private static void Populate(System.Data.SqlClient.SqlDataReader dr, AED_Trainer c)
        {
            var with_1 = c;
            with_1.Status = (RescueTekniq.BOL.AEDStatusEnum) (dr.DBtoInt("Status")); //dr.DBtoInt("Status")
            with_1.CompanyID = System.Convert.ToInt32(dr.DBtoInt("CompanyID"));
            
            with_1.ModelID = System.Convert.ToInt32(dr.DBtoInt("ModelID"));
            with_1.SerialNo = dr.DBtoString("SerialNo");
            with_1.FirmwareVersion = dr.DBtoString("FirmwareVersion");
            with_1.FirmwareLanguage = dr.DBtoString("FirmwareLanguage");
            
            with_1.DeleveryDate = System.Convert.ToDateTime(dr.DBtoDate("DeleveryDate"));
            with_1.WarencyExpireDate = System.Convert.ToDateTime(dr.DBtoDate("WarencyExpireDate"));
            with_1.BatteryActivationDate = System.Convert.ToDateTime(dr.DBtoDate("BatteryActivationDate"));
            with_1.BatteryWarencyExpireDate = System.Convert.ToDateTime(dr.DBtoDate("BatteryWarencyExpireDate"));
            
            with_1.BilagStatus = (RescueTekniq.BOL.AEDBilagStatus) (dr.DBtoInt("BilagStatus"));
            with_1.BilagSendtDato = System.Convert.ToDateTime(dr.DBtoDate("BilagSendtDato"));
            with_1.BilagModtagetDato = System.Convert.ToDateTime(dr.DBtoDate("BilagModtagetDato"));
            
            with_1.ResponsibleName = dr.DBtoString("ResponsibleName");
            with_1.ResponsiblePhone = dr.DBtoString("ResponsiblePhone");
            with_1.ResponsibleEmail = dr.DBtoString("ResponsibleEmail");
            
            with_1.LocationAdresse = dr.DBtoString("LocationAdresse");
            with_1.LocationPostnr = dr.DBtoString("LocationPostnr");
            with_1.LocationBy = dr.DBtoString("LocationBy");
            with_1.LocationState = dr.DBtoString("LocationState");
            with_1.LocationLandID = System.Convert.ToInt32(dr.DBtoInt("LocationLandID"));
            with_1.Location = dr.DBtoString("Location");
            
            with_1.RegisterretCSI = System.Convert.ToDateTime(dr.DBtoDate("RegisterretCSI"));
            with_1.RegisterretAF = dr.DBtoString("RegisterretAF");
            
            with_1.Note = dr.DBtoString("Note");
            
            PopulateStandard(dr, c);
        }
        
#endregion
        
#region  Stored Procedure Names
        private const string _SQLDelete = "Co2Db_AED_Trainer_Delete";
        private const string _SQLInsert = "Co2Db_AED_Trainer_Insert";
        private const string _SQLUpdate = "Co2Db_AED_Trainer_Update";
        private const string _SQLSelectAll = "Co2Db_AED_Trainer_SelectAll";
        private const string _SQLSelectID = "Co2Db_AED_Trainer_SelectID";
        private const string _SQLSelectOne = "Co2Db_AED_Trainer_SelectOne";
        private const string _SQLSelectAllVareGrp = "Co2Db_AED_Trainer_SelectAllVareGrp";
        private const string _SQLSelectBySearch = "Co2Db_AED_Trainer_SelectBySearch";
        private const string _SQLSelectByCompany = "Co2Db_AED_Trainer_SelectByCompany";
        private const string _SQLSelectByGuid = "Co2Db_AED_Trainer_SelectByGuid";
        
        private const string _SQLCountByCompany = "Co2Db_AED_Trainer_CountAEDByCompany";
        private const string _SQLSelectIDByCompany = "Co2Db_AED_Trainer_SelectIDByCompany";
        
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
            AddLog(Status: "AED_Trainer", Logtext: string.Format("Delete AED_Battery: ID:{0}", ID), Metode: "Delete");
            return retval;
        }
        public static int Delete(AED_Trainer c)
        {
            return Delete(c.ID);
        }
        
        
        public int Insert()
        {
            return Insert(this);
        }
        public static int Insert(AED_Trainer c)
        {
            DBAccess db = new DBAccess();
            
            AddParms(ref db, c);
            
            SqlParameter objParam = new SqlParameter("@ID", 0);
            objParam.Direction = ParameterDirection.Output;
            db.Parameters.Add(objParam);
            int retval = db.ExecuteNonQuery(_SQLInsert);
            if (retval == 1)
            {
                c.ID = Funktioner.ToInt(objParam.Value, -1); //Integer.Parse(objParam.Value.ToString)
                AddLog(Status: "AED_Trainer", Logtext: string.Format("Create AED Trainer: ID:{0} SNo:{1} ", c.ID, c.SerialNo), Metode: "Insert");
                return c.ID; //Integer.Parse(objParam.Value.ToString)
            }
            else
            {
                AddLog(Status: "AED_Trainer", Logtext: string.Format("Failure to Create AED_Trainer: SNo:{0}", c.SerialNo), logtype: LogTypeEnum.Error, Metode: "Insert");
                return -1;
            }
        }
        public static int Insert(AEDStatusEnum Status, int CompanyID, int ModelID, string SerialNo, string FirmwareVersion, string FirmwareLanguage, DateTime DeleveryDate, DateTime WarencyExpireDate, DateTime BatteryActivationDate, DateTime BatteryWarencyExpireDate, string ResponsibleName, string ResponsiblePhone, string ResponsibleEmail, string LocationAdresse, string LocationPostnr, string LocationBy, string LocationState, int LocationLandID, string Location, string Note)
        {
            AED_Trainer c = new AED_Trainer();
            c.Status = Status;
            c.CompanyID = CompanyID;
            
            c.ModelID = ModelID;
            c.SerialNo = SerialNo;
            c.FirmwareVersion = FirmwareVersion;
            c.FirmwareLanguage = FirmwareLanguage;
            
            c.DeleveryDate = DeleveryDate;
            c.WarencyExpireDate = WarencyExpireDate;
            c.BatteryActivationDate = BatteryActivationDate;
            c.BatteryWarencyExpireDate = BatteryWarencyExpireDate;
            
            c.ResponsibleName = ResponsibleName;
            c.ResponsiblePhone = ResponsiblePhone;
            c.ResponsibleEmail = ResponsibleEmail;
            
            c.LocationAdresse = LocationAdresse;
            c.LocationPostnr = LocationPostnr;
            c.LocationBy = LocationBy;
            c.LocationState = LocationState;
            c.LocationLandID = LocationLandID;
            c.Location = Location;
            
            c.Note = Note;
            
            return Insert(c);
        }
        
        public int Update()
        {
            return Update(this);
        }
        public static int Update(AED_Trainer c)
        {
            DBAccess db = new DBAccess();
            
            db.AddInt("ID", c.ID);
            AddParms(ref db, c);
            
            int retval = db.ExecuteNonQuery(_SQLUpdate);
            AddLog(Status: "AED_Trainer", Logtext: string.Format("Update AED_Trainer: ID:{0} SNo:{1} ", c.ID, c.SerialNo), Metode: "Update");
            return retval;
        }
        public static int Update(int ID, AEDStatusEnum Status, int CompanyID, int ModelID, string SerialNo, string FirmwareVersion, string FirmwareLanguage, DateTime DeleveryDate, DateTime WarencyExpireDate, DateTime BatteryActivationDate, DateTime BatteryWarencyExpireDate, string ResponsibleName, string ResponsiblePhone, string ResponsibleEmail, string LocationAdresse, string LocationPostnr, string LocationBy, string LocationState, int LocationLandID, string Location, string Note)
        {
            AED_Trainer c = new AED_Trainer(ID);
            c.Status = Status;
            c.CompanyID = CompanyID;
            
            c.ModelID = ModelID;
            c.SerialNo = SerialNo;
            c.FirmwareVersion = FirmwareVersion;
            c.FirmwareLanguage = FirmwareLanguage;
            
            c.DeleveryDate = DeleveryDate;
            c.WarencyExpireDate = WarencyExpireDate;
            c.BatteryActivationDate = BatteryActivationDate;
            c.BatteryWarencyExpireDate = BatteryWarencyExpireDate;
            
            c.ResponsibleName = ResponsibleName;
            c.ResponsiblePhone = ResponsiblePhone;
            c.ResponsibleEmail = ResponsibleEmail;
            
            c.LocationAdresse = LocationAdresse;
            c.LocationPostnr = LocationPostnr;
            c.LocationBy = LocationBy;
            c.LocationState = LocationState;
            c.LocationLandID = LocationLandID;
            c.Location = Location;
            
            c.Note = Note;
            
            return Update(c);
        }
        
        public int Save()
        {
            return Save(this);
        }
        public static int Save(AED_Trainer A)
        {
            int retval = 0; //db.ExecuteNonQuery(_SQLUpdate)
            if (A.ID > 0)
            {
                retval = Update(A);
            }
            else
            {
                retval = Insert(A);
            }
            return retval;
        }
        
        public static int MoveToNewOwner(int ID, int NewOwner)
        {
            AED_Trainer a = new AED_Trainer(ID);
            a.CompanyID = NewOwner;
            AddLog(Status: "AED_trainer", Logtext: string.Format("Update AED_trainer: ID:{0} SNo:{1} New owner: {2}-{3}", a.ID, a.SerialNo, a.CompanyID, a.Firmanavn), Metode: "MoveToNewOwner");
            return Save(a);
        }
        public int MoveToNewOwner(int NewOwner)
        {
            CompanyID = NewOwner;
            AddLog(Status: "AED_trainer", Logtext: string.Format("Update AED_trainer: ID:{0} SNo:{1} New owner: {2}-{3}", ID, SerialNo, CompanyID, Firmanavn), Metode: "MoveToNewOwner");
            return Save();
        }
        
#endregion
        
#region  Get data
        
        public static AED_Trainer GetAED(int ID)
        {
            DBAccess db = new DBAccess();
            db.AddInt("ID", ID);
            
            db.AddBoolean("IsAgent", AdgangsKontrol.IsAgent());
            db.AddGuid("AgentID", CurrentUserID);
            
            SqlDataReader dr = (SqlDataReader) (db.ExecuteReader(_SQLSelectOne));
            if (dr.HasRows)
            {
                AED_Trainer c = new AED_Trainer();
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
        public static AED_Trainer GetAED(System.Guid Guid)
        {
            DBAccess db = new DBAccess();
            db.AddGuid("Guid", Guid);
            
            db.AddBoolean("IsAgent", AdgangsKontrol.IsAgent());
            db.AddGuid("AgentID", CurrentUserID);
            
            SqlDataReader dr = (SqlDataReader) (db.ExecuteReader(_SQLSelectByGuid));
            if (dr.HasRows)
            {
                AED_Trainer c = new AED_Trainer();
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
        public static DataSet GetAED_DS(int ID)
        {
            DBAccess db = new DBAccess();
            db.AddInt("ID", ID);
            
            db.AddBoolean("IsAgent", AdgangsKontrol.IsAgent());
            db.AddGuid("AgentID", CurrentUserID);
            
            DataSet ds = db.ExecuteDataSet(_SQLSelectID);
            return ds;
        }
        public static DataSet GetAED_DS(System.Guid Guid)
        {
            DBAccess db = new DBAccess();
            db.AddGuid("Guid", Guid);
            
            db.AddBoolean("IsAgent", AdgangsKontrol.IsAgent());
            db.AddGuid("AgentID", CurrentUserID);
            
            DataSet ds = db.ExecuteDataSet(_SQLSelectByGuid);
            return ds;
        }
        
        public static DataSet GetAllAED()
        {
            DBAccess db = new DBAccess();
            
            db.AddBoolean("IsAgent", AdgangsKontrol.IsAgent());
            db.AddGuid("AgentID", CurrentUserID);
            
            DataSet ds = db.ExecuteDataSet(_SQLSelectAll);
            return ds;
        }
        
        public static DataSet GetAEDbyCompany(int CompanyID)
        {
            DBAccess db = new DBAccess();
            db.AddInt("CompanyID", CompanyID);
            
            db.AddBoolean("IsAgent", AdgangsKontrol.IsAgent());
            db.AddGuid("AgentID", CurrentUserID);
            
            DataSet ds = db.ExecuteDataSet(_SQLSelectByCompany);
            return ds;
        }
        
        public static DataSet Search_AED(string Search)
        {
            DataSet ds = new DataSet();
            DataSet dsTemp = new DataSet();
            bool flag = false;
            DBAccess db = new DBAccess();
            
            string[] arr = Search.Split(' ');
            foreach (string s in arr)
            {
                db.AddNVarChar("Search", s, 50);
                
                db.AddBoolean("IsAgent", AdgangsKontrol.IsAgent());
                db.AddGuid("AgentID", CurrentUserID);
                
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
        
        
        public static DataSet GetKoncernAEDtrainer(int CompanyID)
        {
            DataSet AEDlist = new DataSet();
            List<int> VirkList = new List<int>();
            
            DataSet ds = Virksomhed.GetCompanyChildrenList(CompanyID);
            
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                int ID = System.Convert.ToInt32(row["CompanyID"]);
                VirkList.Add(ID);
            }
            
            //AEDlist =
            //AEDlist = '
            AEDlist.Merge(GetAEDtrainerbyCompany(CompanyID));
            foreach (int ID in VirkList)
            {
                // AEDlist =
                AEDlist.Merge(GetKoncernAEDtrainer(ID));
            }
            
            return AEDlist;
        }
        public static DataSet GetAEDtrainerbyCompany(int CompanyID)
        {
            DBAccess db = new DBAccess();
            db.AddInt("CompanyID", CompanyID);
            
            db.AddBoolean("IsAgent", AdgangsKontrol.IsAgent());
            db.AddGuid("AgentID", CurrentUserID);
            
            DataSet ds = db.ExecuteDataSet(_SQLSelectByCompany);
            return ds;
        }
        
        
        public static List<AED_Trainer> GetKoncernAEDtrainerlist(int CompanyID)
        {
            List<AED_Trainer> AEDlist = new List<AED_Trainer>();
            List<int> VirkList = new List<int>();
            
            DataSet ds = Virksomhed.GetCompanyChildrenList(CompanyID);
            
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                int ID = System.Convert.ToInt32(row["CompanyID"]);
                VirkList.Add(ID);
            }
            
            AEDlist.AddRange(GetAEDTrainerList(CompanyID));
            foreach (int ID in VirkList)
            {
                AEDlist.AddRange(GetKoncernAEDtrainerlist(ID));
            }
            
            return AEDlist;
        }
        
        public static System.Collections.Generic.List<AED_Trainer> GetAEDTrainerList(int CompanyID)
        {
            System.Collections.Generic.List<AED_Trainer> result = new System.Collections.Generic.List<AED_Trainer>();
            int ID = -1;
            DBAccess db = new DBAccess();
            db.AddInt("CompanyID", CompanyID);
            
            db.AddBoolean("IsAgent", AdgangsKontrol.IsAgent());
            db.AddGuid("AgentID", CurrentUserID);
            
            SqlDataReader dr = default(SqlDataReader);
            try
            {
                dr = (System.Data.SqlClient.SqlDataReader) (db.ExecuteReader(_SQLSelectIDByCompany));
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ID = System.Convert.ToInt32(dr.DBtoInt("ID"));
                        result.Add(AED_Trainer.GetAED(ID));
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
        
        
        public int GetAEDTrainerCountByKoncern()
        {
            return GetAEDTrainerCountByKoncern(this.CompanyID);
        }
        public static int GetAEDTrainerCountByKoncern(int CompanyID)
        {
            int AEDcount = 0;
            List<int> VirkList = new List<int>();
            
            DataSet ds = Virksomhed.GetCompanyChildrenList(CompanyID);
            
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                int ID = System.Convert.ToInt32(row["CompanyID"]);
                VirkList.Add(ID);
            }
            
            AEDcount += GetAEDTrainer_CountByCompany(CompanyID);
            foreach (int ID in VirkList)
            {
                AEDcount += GetAEDTrainerCountByKoncern(ID);
            }
            
            return AEDcount;
        }
        
        public int GetAEDTrainer_CountByCompany()
        {
            return GetAEDTrainer_CountByCompany(this.CompanyID);
        }
        public static int GetAEDTrainer_CountByCompany(int CompanyID)
        {
            DBAccess db = new DBAccess();
            db.AddInt("CompanyID", CompanyID);
            
            db.AddBoolean("IsAgent", AdgangsKontrol.IsAgent());
            db.AddGuid("AgentID", CurrentUserID);
            
            int res = Funktioner.ToInteger(db.ExecuteScalar(_SQLCountByCompany), 0);
            
            return res;
        }
        
        public int GetAEDcountByCompany()
        {
            return GetAEDcountByCompany(this.CompanyID);
        }
        public static int GetAEDcountByCompany(int CompanyID)
        {
            int res = 0;
            DBAccess db = new DBAccess();
            db.AddInt("CompanyID", CompanyID);
            
            db.AddBoolean("IsAgent", AdgangsKontrol.IsAgent());
            db.AddGuid("AgentID", CurrentUserID);
            
            res = Funktioner.ToInt(db.ExecuteScalar(_SQLCountByCompany));
            return res;
        }
        
        
#endregion
        
#endregion
        
#region  Tags
        
        public string Tags(string tekst)
        {
            return Tags(tekst, this);
        }
        public static string Tags(string tekst, RescueTekniq.BOL.AED_Trainer item)
        {
            string res = "";
            StringBuilder sb = new StringBuilder();
            sb.Append(tekst);
            sb.Replace("[VARE.TYPE]", "AED/Hjertestarter");
            
            sb.Replace("[AED.ANSVARLIG]", item.ResponsibleName);
            sb.Replace("[AED.ANSVARLIG.NAVN]", item.ResponsibleName);
            sb.Replace("[AED.ANSVARLIG.EMAIL]", item.ResponsibleEmail);
            sb.Replace("[AED.ANSVARLIG.TELEFON]", item.ResponsiblePhone);
            
            sb.Replace("[AED.SERIALNO]", item.SerialNo);
            sb.Replace("[AED.LOCATION]", item.Location);
            //.Replace("[AED.LOCATIONFULDADR]", item.LocationFuldAdresse)
            sb.Replace("[AED.VARENR]", "[VARE.VARENR]"); //item.Vare.VareNr)
            sb.Replace("[AED.MODEL]", "[VARE.NAVN]"); //item.Vare.Navn)
            sb.Replace("[AED.EXPIREDATE]", item.WarencyExpireDate.ToString("dd MMMM yyyy"));
            res = sb.ToString();
            //res = item.KundeGrpPris.Tags(res)
            res = item.Virksomhed.Tags(res);
            return res;
        }
        
#endregion
        
    }
    
    
}
