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
    
    public class AED : BaseObject
    {
        
#region  New
        
        public AED()
        {
            
        }
        
        public AED(int ID)
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
        public AED(System.Guid GUID)
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
        private DateTime _EmailSendt;
        
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
        private string _LocationBuildingNo; //(10)
        
        private DateTime _RegisterretCSI;
        private string _RegisterretAF;
        
        private string _Note; //(max)
        
        private RescueTekniq.BOL.Virksomhed _virksomhed = new RescueTekniq.BOL.Virksomhed();
        private RescueTekniq.BOL.Vare _vare = new RescueTekniq.BOL.Vare();
        private List<AED_Electrod> _electrodList;
        private List<AED_Battery> _batteryList;
        private List<AED_Service> _serviceList;
        
        private int _AntalAED = 0;
        
        private RescueTekniq.BOL.KundeGrp_Pris _kgp = new RescueTekniq.BOL.KundeGrp_Pris();
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
        public string StatusText
        {
            get
            {
                string res = "";
                res = Combobox.GetTitleByValue("AED.Status", Status.ToString());
                return res;
            }
        }
        public int StatusNo
        {
            get
            {
                return Convert.ToInt32(_Status);
            }
            set
            {
                _Status = (AEDStatusEnum) value;
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
        
        public DateTime EmailSendt
        {
            get
            {
                return _EmailSendt;
            }
            set
            {
                _EmailSendt = value;
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
        public string LocationBuildingNo
        {
            get
            {
                return _LocationBuildingNo;
            }
            set
            {
                _LocationBuildingNo = value;
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
        
        public string LocationFuldAdresse
        {
            get
            {
                string res = "";
                if (LocationAdresse != "")
                {
                    res += "<br />" + LocationAdresse;
                }
                if (LocationPostnrBy != "")
                {
                    res += "<br />" + LocationPostnrBy;
                }
                if (LocationLandID != 0)
                {
                    res += "<br />" + Combobox.GetTitleByValue("landekode", System.Convert.ToString(LocationLandID));
                }
                if (LocationBuildingNo != "")
                {
                    res += "<br />" + LocationBuildingNo;
                }
                if (res.StartsWith("<br />"))
                {
                    res = res.Substring(6);
                }
                
                return res;
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
        
#endregion
        
#region  Sub Properties
        
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
        
        public RescueTekniq.BOL.KundeGrp_Pris KundeGrpPris
        {
            get
            {
                try
                {
                    //KundeGrp_Pris.GetCompany_VarePris(item.AED.CompanyID, item.Vare.ID)
                    if (Virksomhed.loaded)
                    {
                        if (Vare.loaded)
                        {
                            if (!_kgp.loaded)
                            {
                                _kgp = RescueTekniq.BOL.KundeGrp_Pris.GetCompany_VarePris(CompanyID, Vare.ID);
                            }
                        }
                    }
                }
                catch (Exception)
                {
                }
                return _kgp;
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
                    if ((item.Status == AEDStatusEnum.Aktiv) || (item.Status == AEDStatusEnum.EmailSendt))
                    {
                        res.Append(item.ElectrodTypeText);
                        res.Append(item.ElectrodExpireDate.ToString("dd MMMM yyyy")); //(" MMM. yyyy"))
                        res.Append("<br />");
                        res.AppendLine();
                    }
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
                    if ((item.Status == AEDStatusEnum.Aktiv) || (item.Status == AEDStatusEnum.EmailSendt))
                    {
                        //res.Append(item.BatteryTypeText)
                        res.Append(item.BatteryExpireDate.ToString("dd MMM. yyyy"));
                        res.Append("<br />");
                        res.AppendLine();
                    }
                }
                return res.ToString();
            }
        }
        
        
        public DateTime NextServiceDate
        {
            get
            {
                return NextService.ServiceDueDate;
            }
        }
        public string NextServiceConsultant
        {
            get
            {
                return NextService.ServiceConsultant;
            }
        }
        
        public DateTime PrevServiceDate
        {
            get
            {
                return PrevService.ServiceVisitedDate;
            }
        }
        public string PrevServiceConsultant
        {
            get
            {
                return PrevService.ServiceConsultant;
            }
        }
        
        
        private AED_Service _nextService = new AED_Service();
        private bool _nextServiceLoaded = false;
        private int _nextServiceAED_FK = -1;
        public AED_Service NextService
        {
            get
            {
                if (!_nextServiceLoaded) //ServiceActive = 1 AND
                {
                    _nextService = AED_Service.GetServiceByCriteria("ServiceDueDate ASC", "Status IN (1,2) AND AED_FK=@AED_FK", new SqlParameter("@AED_FK", ID));
                    _nextServiceAED_FK = ID;
                    _nextServiceLoaded = true;
                }
                else if (_nextServiceAED_FK != ID)
                {
                    _nextService = AED_Service.GetServiceByCriteria("ServiceDueDate ASC", "Status IN (1,2) AND AED_FK=@AED_FK", new SqlParameter("@AED_FK", ID));
                    _nextServiceAED_FK = ID;
                    _nextServiceLoaded = true;
                }
                return _nextService;
            }
        }
        
        private AED_Service _prevService = new AED_Service();
        private bool _prevServiceLoaded = false;
        private int _prevServiceAED_FK = -1;
        public AED_Service PrevService
        {
            get
            {
                if (!_prevServiceLoaded)
                {
                    _prevService = AED_Service.GetServiceByCriteria("ServiceVisitedDate DESC", "Status IN (3) AND AED_FK=@AED_FK", new SqlParameter("@AED_FK", ID));
                    _prevServiceAED_FK = ID;
                    _prevServiceLoaded = true;
                }
                else if (_prevServiceAED_FK != ID)
                {
                    _nextService = AED_Service.GetServiceByCriteria("ServiceVisitedDate DESC", "Status IN (3) AND AED_FK=@AED_FK", new SqlParameter("@AED_FK", ID));
                    _prevServiceAED_FK = ID;
                    _nextServiceLoaded = true;
                }
                return _nextService;
            }
        }
        
        private bool _ServiceListLoaded = false;
        private int _ServiceListAED_FK = -1;
        public System.Collections.Generic.List<AED_Service> ServiceList
        {
            get
            {
                if (!_ServiceListLoaded)
                {
                    _serviceList = AED_Service.GetServiceList(ID);
                    _ServiceListLoaded = true;
                    _ServiceListAED_FK = ID;
                }
                else if (_ServiceListAED_FK != ID)
                {
                    _serviceList = AED_Service.GetServiceList(ID);
                    _ServiceListLoaded = true;
                    _ServiceListAED_FK = ID;
                }
                return _serviceList;
            }
        }
        public string ServiceListText
        {
            get
            {
                StringBuilder res = new StringBuilder();
                foreach (AED_Service item in ServiceList)
                {
                    if (item.Status == AED_ServiceStatusType.Aktiv | item.Status == AED_ServiceStatusType.OverDue)
                    {
                        res.Append(item.ServiceTypeText);
                        res.Append(item.ServiceDueDate.ToString("dd MMM. yyyy"));
                        res.Append("<br />");
                        res.AppendLine();
                    }
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
        
        private static object AddParms(DBAccess db, AED rec)
        {
            db.AddInt("Status", (System.Int32) rec.Status);
            db.AddInt("CompanyID", rec.CompanyID);
            
            db.AddInt("ModelID", rec.ModelID);
            db.AddNVarChar("SerialNo", rec.SerialNo, 50);
            db.AddNVarChar("FirmwareVersion", rec.FirmwareVersion, 50);
            db.AddNVarChar("FirmwareLanguage", rec._FirmwareLanguage, 50);
            
            db.AddDateTime("DeleveryDate", rec.DeleveryDate);
            db.AddDateTime("WarencyExpireDate", rec.WarencyExpireDate);
            db.AddDateTime("EmailSendt", rec.EmailSendt);
            
            db.AddDateTime("BatteryActivationDate", rec.BatteryActivationDate);
            db.AddDateTime("BatteryWarencyExpireDate", rec.BatteryWarencyExpireDate);
            
            db.AddInt("BilagStatus", (System.Int32) rec.BilagStatus);
            db.AddDateTime("BilagSendtDato", rec.BilagSendtDato);
            db.AddDateTime("BilagModtagetDato", rec.BilagModtagetDato);
            
            db.AddNVarChar("ResponsibleName", rec.ResponsibleName, 50);
            db.AddNVarChar("ResponsiblePhone", rec.ResponsiblePhone, 16);
            db.AddNVarChar("ResponsibleEmail", rec.ResponsibleEmail, 250);
            
            db.AddNVarChar("LocationAdresse", rec.LocationAdresse, 50);
            db.AddNVarChar("LocationPostnr", rec.LocationPostnr, 16);
            db.AddNVarChar("LocationBy", rec.LocationBy, 50);
            db.AddNVarChar("LocationState", rec.LocationState, 50);
            db.AddInt("LocationLandID", rec.LocationLandID);
            db.AddNVarChar("Location", rec.Location, -1);
            db.AddNVarChar("LocationBuildingNo", rec.LocationBuildingNo, 10);
            
            db.AddDateTime("RegisterretCSI", rec.RegisterretCSI);
            db.AddNVarChar("RegisterretAF", rec.RegisterretAF, 50);
            
            db.AddNVarChar("Note", rec.Note, -1);
            
            rec = (AED) (AddParmsStandard(db, rec));
            return rec;
        }
        
        private static AED Populate(System.Data.IDataReader dr, AED rec)
        {
            rec = (AED) (PopulateStandard(dr, rec));
            
            rec.Status = (RescueTekniq.BOL.AEDStatusEnum) (dr.DBtoInt("Status"));
            rec.CompanyID = System.Convert.ToInt32(dr.DBtoInt("CompanyID"));
            
            rec.ModelID = System.Convert.ToInt32(dr.DBtoInt("ModelID"));
            rec.SerialNo = dr.DBtoString("SerialNo");
            rec.FirmwareVersion = dr.DBtoString("FirmwareVersion");
            rec.FirmwareLanguage = dr.DBtoString("FirmwareLanguage");
            
            rec.DeleveryDate = System.Convert.ToDateTime(dr.DBtoDate("DeleveryDate"));
            rec.WarencyExpireDate = System.Convert.ToDateTime(dr.DBtoDate("WarencyExpireDate"));
            rec.EmailSendt = System.Convert.ToDateTime(dr.DBtoDate("EmailSendt"));
            
            rec.BatteryActivationDate = System.Convert.ToDateTime(dr.DBtoDate("BatteryActivationDate"));
            rec.BatteryWarencyExpireDate = System.Convert.ToDateTime(dr.DBtoDate("BatteryWarencyExpireDate"));
            
            rec.BilagStatus = (RescueTekniq.BOL.AEDBilagStatus) (dr.DBtoInt("BilagStatus"));
            rec.BilagSendtDato = System.Convert.ToDateTime(dr.DBtoDate("BilagSendtDato"));
            rec.BilagModtagetDato = System.Convert.ToDateTime(dr.DBtoDate("BilagModtagetDato"));
            
            rec.ResponsibleName = dr.DBtoString("ResponsibleName");
            rec.ResponsiblePhone = dr.DBtoString("ResponsiblePhone");
            rec.ResponsibleEmail = dr.DBtoString("ResponsibleEmail");
            
            rec.LocationAdresse = dr.DBtoString("LocationAdresse");
            rec.LocationPostnr = dr.DBtoString("LocationPostnr");
            rec.LocationBy = dr.DBtoString("LocationBy");
            rec.LocationState = dr.DBtoString("LocationState");
            rec.LocationLandID = System.Convert.ToInt32(dr.DBtoInt("LocationLandID"));
            rec.Location = dr.DBtoString("Location");
            rec.LocationBuildingNo = dr.DBtoString("LocationBuildingNo");
            
            rec.RegisterretCSI = System.Convert.ToDateTime(dr.DBtoDate("RegisterretCSI"));
            rec.RegisterretAF = dr.DBtoString("RegisterretAF");
            
            rec.Note = dr.DBtoString("Note");
            return rec;
        }
        
#endregion
        
#region  Stored Procedure Names
        
        private const string _SQLDelete = "Co2Db_AED_Delete";
        private const string _SQLInsert = "Co2Db_AED_Insert";
        private const string _SQLUpdate = "Co2Db_AED_Update";
        private const string _SQLSelectAll = "Co2Db_AED_SelectAll";
        private const string _SQLSelectID = "Co2Db_AED_SelectID";
        private const string _SQLSelectOne = "Co2Db_AED_SelectOne";
        private const string _SQLSelectAllVareGrp = "Co2Db_AED_SelectAllVareGrp";
        private const string _SQLSelectBySearch = "Co2Db_AED_SelectBySearch";
        private const string _SQLSelectByCompany = "Co2Db_AED_SelectByCompany";
        private const string _SQLSelectByAfventer = "Co2Db_AED_SelectByAfventer";
        private const string _SQLSelectByBilagStatus = "Co2Db_AED_SelectByBilagStatus";
        
        private const string _SQLSelectByGuid = "Co2Db_AED_SelectByGuid";
        private const string _SQLSelectBySerialNo = "Co2Db_AED_SelectBySerialNo";
        
        private const string _SQLCountByCompany = "Co2Db_AED_CountAEDByCompany";
        private const string _SQLSelectIDByCompany = "Co2Db_AED_SelectIDByCompany";
        
        private const string _SQLSelectAllSoonExpired = "Co2Db_AED_SelectAllSoonExpired";
        private const string _SQLSelectAllExpired = "Co2Db_AED_SelectAllExpired";
        
        private const string _SQLSearch = "Co2Db_AED_Search";
        
        private const string _SQLSearchConcernAED = "Co2Db_AED_SelectConcernAED";
        
#endregion
        
#region  Metoder
        
#region  Manipulate data
        
        public int Delete()
        {
            return Delete(System.Convert.ToInt32(this));
        }
        public static int Delete(int ID)
        {
            AED item = new AED(ID);
            return Delete(item);
        }
        public static int Delete(AED A)
        {
            int retval = -1;
            
            if (A.ID > 0)
            {
                foreach (AED_Electrod E in A.ElectrodList)
                {
                    E.Delete();
                }
                foreach (AED_Battery B in A.BatteryList)
                {
                    B.Delete();
                }
                
                DBAccess db = new DBAccess();
                db.AddInt("ID", A.ID);
                
                retval = db.ExecuteNonQuery(_SQLDelete);
                AddLog(Status: "AED", Logtext: string.Format("Delete AED: ID:{0} SNo:{1} ", A.ID, A.SerialNo), Metode: "Delete");
            }
            
            return retval;
            
        }
        public static int DeleteCompanyAED(int companyID)
        {
            foreach (AED A in GetAEDList(companyID))
            {
                A.Delete();
            }
            return 1;
        }
        
        public int Insert()
        {
            return Insert(this);
        }
        public static int Insert(AED c)
        {
            DBAccess db = new DBAccess();
            
            AddParms(db, c);
            
            SqlParameter objParam = new SqlParameter("@ID", 0);
            objParam.Direction = ParameterDirection.Output;
            db.Parameters.Add(objParam);
            int retval = db.ExecuteNonQuery(_SQLInsert);
            if (retval == 1)
            {
                c.ID = Funktioner.ToInt(objParam.Value, -1); //Integer.Parse(objParam.Value.ToString)
                AddLog(Status: "AED", Logtext: string.Format("Create AED: ID:{0} SNo:{1} ", c.ID, c.SerialNo), Metode: "Insert");
                return c.ID; //Integer.Parse(objParam.Value.ToString)
            }
            else
            {
                AddLog(Status: "AED", Logtext: string.Format("Failure to Create AED: SNo:{0}", c.SerialNo), logtype: LogTypeEnum.Error, Metode: "Insert");
                return -1;
            }
        }
        
        public int Update()
        {
            return Update(this);
        }
        public static int Update(AED c)
        {
            DBAccess db = new DBAccess();
            
            db.AddInt("ID", c.ID);
            AddParms(db, c);
            
            int retval = db.ExecuteNonQuery(_SQLUpdate);
            AddLog(Status: "AED", Logtext: string.Format("Update AED: ID:{0} SNo:{1} ", c.ID, c.SerialNo), Metode: "Update");
            return retval;
        }
        
        
        //            Log.AddLog("AED", String.Format("Create AED: ID:{0} SNo:{1} ", c.ID, c.SerialNo))
        public int Save()
        {
            return Save(this);
        }
        public static int Save(AED A)
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
        
        public int MoveToNewOwner(int NewOwner)
        {
            CompanyID = NewOwner;
            AddLog(Status: "AED", Logtext: string.Format("Update AED: ID:{0} SNo:{1} New owner: {2}-{3}", ID, SerialNo, CompanyID, Firmanavn), Metode: "MoveToNewOwner");
            return Save();
        }
        public static int MoveToNewOwner(int ID, int NewOwner)
        {
            AED a = new AED(ID);
            a.CompanyID = NewOwner;
            AddLog(Status: "AED", Logtext: string.Format("Update AED: ID:{0} SNo:{1} New owner: {2}-{3}", a.ID, a.SerialNo, a.CompanyID, a.Firmanavn), Metode: "MoveToNewOwner");
            return Save(a);
        }
        
#endregion
        
#region  Get data
        
        public static AED GetAEDByCriteria(string OrderBY, string criteria, params SqlParameter[] @params)
        {
            List<AED> list = GetAEDsByCriteria(OrderBY, criteria, @params);
            if (list.Count > 0)
            {
                return list[0];
            }
            else
            {
                return new AED(); //Nothing
            }
        }
        public static List<AED> GetAEDsByCriteria(string OrderBY, string criteria, params SqlParameter[] @params)
        {
            DBAccess db = new DBAccess();
            //Dim conn As SqlConnection = DataFunctions.GetConnection()
            string query = "";
            query += "SELECT ";
            if (OrderBY != "")
            {
                query += " TOP (100) PERCENT ";
            }
            query += " * FROM vw_Co2Db_AED ";
            query += " WHERE ( @IsAgent = 0 OR ( @IsAgent = 1 AND [AgentID] = @AgentID ) )";
            if (criteria != "")
            {
                query += " AND (" + criteria + ")";
            }
            if (OrderBY != "")
            {
                query += " ORDER BY " + OrderBY;
            }
            
            //Dim cmd As New SqlCommand(query, conn)
            db.CommandType = CommandType.Text;
            db.CommandText = query;
            db.AddRange(@params);
            db.AddBoolean("IsAgent", AdgangsKontrol.IsAgent());
            db.AddGuid("AgentID", CurrentUserID);
            
            db.Open();
            SqlDataReader dr = (SqlDataReader) (db.ExecuteReader()); //cmd.ExecuteReader()
            
            List<AED> list = new List<AED>();
            while (dr.Read())
            {
                AED _AED = new AED();
                AED.Populate(dr, _AED);
                list.Add(_AED);
            }
            
            db.Dispose();
            
            return list;
        }
        public static DataSet GetAEDsByCriteriaDS(string fieldnames, string criteria, params SqlParameter[] @params)
        {
            DBAccess db = new DBAccess();
            string query = "";
            query += "SELECT ";
            if (fieldnames.Trim() != "")
            {
                query += fieldnames;
            }
            else
            {
                query += " * ";
            }
            query += " FROM vw_Co2Db_AED ";
            query += " WHERE ( @IsAgent = 0 OR ( @IsAgent = 1 AND [AgentID] = @AgentID ) )";
            if (criteria != "")
            {
                query += " AND (" + criteria + ")";
            }
            
            db.CommandType = CommandType.Text;
            db.CommandText = query;
            db.AddRange(@params);
            db.AddBoolean("IsAgent", AdgangsKontrol.IsAgent());
            db.AddGuid("AgentID", CurrentUserID);
            
            DataSet ds = db.ExecuteDataSet(); //(_SQLSelectByGuid)
            return ds;
        }
        
        public static AED GetAED(int ID)
        {
            DBAccess db = new DBAccess();
            AED c = new AED();
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
            }
            dr.Close();
            return c;
        }
        public static AED GetAEDbyGuid(Guid GuID)
        {
            DBAccess db = new DBAccess();
            AED c = new AED();
            db.AddGuid("Guid", GuID);
            
            db.AddBoolean("IsAgent", AdgangsKontrol.IsAgent());
            db.AddGuid("AgentID", CurrentUserID);
            
            SqlDataReader dr = (SqlDataReader) (db.ExecuteReader(_SQLSelectByGuid));
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Populate(dr, c);
                }
            }
            dr.Close();
            return c;
        }
        
        public static AED GetAEDBySerialNo(string SerialNo)
        {
            DBAccess db = new DBAccess();
            AED c = new AED();
            db.AddNVarChar("SerialNo", SerialNo, 50);
            
            db.AddBoolean("IsAgent", AdgangsKontrol.IsAgent());
            db.AddGuid("AgentID", CurrentUserID);
            
            SqlDataReader dr = (SqlDataReader) (db.ExecuteReader(_SQLSelectBySerialNo));
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Populate(dr, c);
                }
            }
            dr.Close();
            return c;
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
        
        public static DataSet GetAllAED()
        {
            DBAccess db = new DBAccess();
            
            db.AddBoolean("IsAgent", AdgangsKontrol.IsAgent());
            db.AddGuid("AgentID", CurrentUserID);
            
            //db.Parameters.Add(New SqlParameter("@sUserName", username))
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
        public static DataSet GetAEDbyGUID_DS(Guid GuID)
        {
            DBAccess db = new DBAccess();
            db.AddGuid("Guid", GuID);
            
            db.AddBoolean("IsAgent", AdgangsKontrol.IsAgent());
            db.AddGuid("AgentID", CurrentUserID);
            
            DataSet ds = db.ExecuteDataSet(_SQLSelectByGuid);
            return ds;
        }
        
        public static DataSet GetAEDbyAfventer()
        {
            return GetAEDbyAfventer(-1);
        }
        public static DataSet GetAEDbyAfventer(int CompanyID)
        {
            return GetAEDbyBilagStatus(CompanyID, 1);
        }
        
        public static DataSet GetAEDbyModtaget()
        {
            return GetAEDbyModtaget(-1);
        }
        public static DataSet GetAEDbyModtaget(int CompanyID)
        {
            return GetAEDbyBilagStatus(CompanyID, 2);
        }
        
        public static DataSet GetAEDbyBilagStatus(int CompanyID, int BilagStatus)
        {
            DBAccess db = new DBAccess();
            db.AddInt("CompanyID", CompanyID);
            db.AddInt("BilagStatus", BilagStatus);
            
            db.AddBoolean("IsAgent", AdgangsKontrol.IsAgent());
            db.AddGuid("AgentID", CurrentUserID);
            
            DataSet ds = db.ExecuteDataSet(_SQLSelectByBilagStatus);
            return ds;
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
        
        public static int GetAEDCountByKoncern(int CompanyID)
        {
            int AEDcount = 0;
            List<int> VirkList = new List<int>();
            
            DataSet ds = Virksomhed.GetCompanyChildrenList(CompanyID);
            
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                int ID = System.Convert.ToInt32(row["CompanyID"]);
                VirkList.Add(ID);
            }
            
            AEDcount += GetAEDcountByCompany(CompanyID);
            foreach (int ID in VirkList)
            {
                AEDcount += GetAEDCountByKoncern(ID);
            }
            
            return AEDcount;
        }
        
        
        public static DataSet GetKoncernAED(int CompanyID)
        {
            DBAccess db = new DBAccess();
            db.AddInt("CompanyID", CompanyID);
            
            db.AddBoolean("IsAgent", AdgangsKontrol.IsAgent());
            db.AddGuid("AgentID", CurrentUserID);
            
            DataSet ds = db.ExecuteDataSet(_SQLSearchConcernAED);
            return ds;
        }
        //Public Shared Function GetAEDCountByCompany(ByVal CompanyID As Integer) As Integer
        //    Dim db As DBAccess = New DBAccess
        //    db.AddInt("CompanyID", CompanyID)
        //    Dim res As Integer = ToInteger(db.ExecuteScalar(_SQLCountByCompany), 0)
        
        //    Return res
        //End Function
        //[Co2Db_AED_Trainer_CountAEDByCompany]
        public static List<AED> GetKoncernAEDlist(int CompanyID)
        {
            List<AED> AEDlist = new List<AED>();
            List<int> VirkList = new List<int>();
            
            DataSet ds = Virksomhed.GetCompanyChildrenList(CompanyID);
            
            if (ds != null)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    int ID = System.Convert.ToInt32(row["CompanyID"]);
                    VirkList.Add(ID);
                }
            }
            
            AEDlist.AddRange(GetAEDList(CompanyID));
            foreach (int ID in VirkList)
            {
                AEDlist.AddRange(GetKoncernAEDlist(ID));
            }
            
            return AEDlist;
        }
        
        public static System.Collections.Generic.List<AED> GetAEDList(int CompanyID)
        {
            System.Collections.Generic.List<AED> result = new System.Collections.Generic.List<AED>();
            int ID = -1;
            DBAccess db = new DBAccess();
            db.AddInt("CompanyID", CompanyID);
            
            db.AddBoolean("IsAgent", AdgangsKontrol.IsAgent());
            db.AddGuid("AgentID", CurrentUserID);
            
            SqlDataReader dr = default(SqlDataReader);
            //Try
            dr = (System.Data.SqlClient.SqlDataReader) (db.ExecuteReader(_SQLSelectIDByCompany));
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ID = System.Convert.ToInt32(dr.DBtoInt("ID"));
                    result.Add(AED.GetAED(ID));
                }
            }
            dr.Close();
            //Catch ex As Exception
            //Throw ex
            //End Try
            
            return result;
        }
        
        //ALTER PROCEDURE [vicjos1_sysadm].[Co2Db_AED_SelectAllExpired]
        //@CompanyID int = -1,
        //@Status int = 1,
        //@ExpireTime int = -1,
        //@Date datetime = null
        public static DataSet SelectAllExpired(int CompanyID)
        {
            int Status = 1;
            int ExpireTime = -1;
            DateTime ExpireDate = DateTime.Now;
            return SelectAllExpired(CompanyID, Status, ExpireTime, ExpireDate);
        }
        public static DataSet SelectAllExpired(int CompanyID, int Status)
        {
            int ExpireTime = -1;
            DateTime ExpireDate = DateTime.Now;
            return SelectAllExpired(CompanyID, Status, ExpireTime, ExpireDate);
        }
        public static DataSet SelectAllExpired(int CompanyID, int Status, int ExpireTime)
        {
            DateTime ExpireDate = DateTime.Now;
            return SelectAllExpired(CompanyID, Status, ExpireTime, ExpireDate);
        }
        public static DataSet SelectAllExpired(int CompanyID, int Status, int ExpireTime, DateTime ExpireDate)
        {
            DBAccess db = new DBAccess();
            db.AddInt("CompanyID", CompanyID);
            db.AddInt("Status", Status);
            db.AddInt("ExpireTime", ExpireTime);
            db.AddDateTime("ExpireDate", ExpireDate);
            
            db.AddBoolean("IsAgent", AdgangsKontrol.IsAgent());
            db.AddGuid("AgentID", CurrentUserID);
            
            DataSet ds = db.ExecuteDataSet(_SQLSelectAllExpired);
            return ds;
        }
        
        public static System.Collections.Generic.List<AED> GetListSoonExpired(int CompanyID)
        {
            System.Collections.Generic.List<AED> result = new System.Collections.Generic.List<AED>();
            int ID = -1;
            DBAccess db = new DBAccess();
            db.AddInt("CompanyID", CompanyID);
            
            db.AddBoolean("IsAgent", AdgangsKontrol.IsAgent());
            db.AddGuid("AgentID", CurrentUserID);
            
            SqlDataReader dr = default(SqlDataReader);
            //Try
            dr = (System.Data.SqlClient.SqlDataReader) (db.ExecuteReader(_SQLSelectAllSoonExpired));
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ID = System.Convert.ToInt32(dr.DBtoInt("ID"));
                    result.Add(AED.GetAED(ID));
                }
            }
            dr.Close();
            //Catch ex As Exception
            //    Throw ex
            //End Try
            
            return result;
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
        
        public static DataSet GetAED_SearchAll(string Postnr, string SerialNo)
        {
            DataSet ds = new DataSet();
            DataSet dsTemp = new DataSet();
            bool flag = false;
            DBAccess db = new DBAccess();
            
            //Dim arr As String() = Search.Split(" "c)
            //For Each s As String In arr
            db.AddNVarChar("Postnr", Postnr, 50);
            db.AddNVarChar("SerialNo", SerialNo, 50);
            
            db.AddBoolean("IsAgent", AdgangsKontrol.IsAgent());
            db.AddGuid("AgentID", CurrentUserID);
            
            //	EXEC	@return_value = [vicjos1_sysadm].[Co2Db_AED_Search]
            //			@SerialNo = ''
            //		,	@Postnr = 	'3400'
            //		,	@IsAgent = 0
            //		,	@AgentID = null
            dsTemp = db.ExecuteDataSet(_SQLSearch);
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
        
#endregion
        
#endregion
        
#region  Tags
        
        public string Tags(string tekst)
        {
            return Tags(tekst, this);
        }
        public static string Tags(string tekst, RescueTekniq.BOL.AED item)
        {
            string res = "";
            StringBuilder sb = new StringBuilder();
            sb.Append(tekst);
            sb.Replace("[VARE.TYPE]", "AED/Hjertestarter");
            
            sb.Replace("[AED.ANSVARLIG]", item.ResponsibleName);
            sb.Replace("[AED.ANSVARLIG.NAVN]", item.ResponsibleName);
            sb.Replace("[AED.ANSVARLIG.EMAIL]", item.ResponsibleEmail);
            sb.Replace("[AED.ANSVARLIG.TELEFON]", item.ResponsiblePhone);
            
            sb.Replace("[AED.NOTE]", item.Note);
            
            sb.Replace("[AED.TYPE]", "[VARE.NAVN]");
            sb.Replace("[AED.SERIALNO]", item.SerialNo);
            sb.Replace("[AED.LOCATION]", item.Location);
            sb.Replace("[AED.LOCATIONFULDADR]", item.LocationFuldAdresse);
            sb.Replace("[AED.VARENR]", "[VARE.VARENR]"); //item.Vare.VareNr)
            sb.Replace("[AED.MODEL]", "[VARE.NAVN]"); //item.Vare.Navn)
            sb.Replace("[AED.EXPIREDATE]", item.WarencyExpireDate.ToString("dd MMMM yyyy"));
            res = sb.ToString();
            res = item.KundeGrpPris.Tags(res);
            res = item.Virksomhed.Tags(res);
            
            return res;
        }
        
#endregion
        
    }
    
    
}
