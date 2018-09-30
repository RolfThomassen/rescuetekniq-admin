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
    
    public class EyeWash : BaseObject
    {
        
#region  New
        
        public EyeWash()
        {
            
        }
        
        public EyeWash(int ID)
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
        public EyeWash(System.Guid GUID)
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
        
        private EyeWash_StatusEnum _Status = EyeWash_StatusEnum.Aktiv;
        private int _CompanyID;
        private int _ModelID;
        private string _SerialNo; //(50)
        
        private DateTime _DeleveryDate;
        private DateTime _WarencyExpireDate;
        private DateTime _EmailSendt;
        
        private DateTime _BottleActivationDate;
        private DateTime _BottleWarencyExpireDate;
        
        private string _FirmwareVersion; //(50)
        private string _FirmwareLanguage; //(50)
        
        private EyeWash_BilagStatus _BilagStatus = EyeWash_BilagStatus.Initialize;
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
        private List<EyeWash_Bottle> _bottleList;
        private List<EyeWash_Service> _serviceList;
        
        private int _AntalEyeWash = 0;
        
        private RescueTekniq.BOL.KundeGrp_Pris _kgp = new RescueTekniq.BOL.KundeGrp_Pris();
#endregion
        
#region  Properties
        
        public EyeWash_StatusEnum Status
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
        
        
        public DateTime BottleActivationDate
        {
            get
            {
                return _BottleActivationDate;
            }
            set
            {
                _BottleActivationDate = value;
            }
        }
        
        public DateTime BottleWarencyExpireDate
        {
            get
            {
                return _BottleWarencyExpireDate;
            }
            set
            {
                _BottleWarencyExpireDate = value;
            }
        }
        
        public EyeWash_BilagStatus BilagStatus
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
                    //KundeGrp_Pris.GetCompany_VarePris(item.EyeWash.CompanyID, item.Vare.ID)
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
        
        
        private bool _BottleListLoaded = false;
        private int _BottleListEyeWash_FK = -1;
        public System.Collections.Generic.List<EyeWash_Bottle> BottleList
        {
            get
            {
                if (!_BottleListLoaded)
                {
                    _bottleList = EyeWash_Bottle.GetBottleList(ID);
                    _BottleListLoaded = true;
                    _BottleListEyeWash_FK = ID;
                }
                else if (_BottleListEyeWash_FK != ID)
                {
                    _bottleList = EyeWash_Bottle.GetBottleList(ID);
                    _BottleListLoaded = true;
                    _BottleListEyeWash_FK = ID;
                }
                return _bottleList;
            }
        }
        public string BottleListText
        {
            get
            {
                StringBuilder res = new StringBuilder();
                foreach (EyeWash_Bottle item in BottleList)
                {
                    if ((item.Status == EyeWash_StatusEnum.Aktiv) || (item.Status == EyeWash_StatusEnum.EmailSendt))
                    {
                        //res.Append(item.BottleTypeText)
                        res.Append(item.BottleExpireDate.ToString("dd MMM. yyyy"));
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
        
        
        private EyeWash_Service _nextService = new EyeWash_Service();
        private bool _nextServiceLoaded = false;
        private int _nextServiceEyeWash_FK = -1;
        public EyeWash_Service NextService
        {
            get
            {
                if (!_nextServiceLoaded) //ServiceActive = 1 AND
                {
                    _nextService = EyeWash_Service.GetServiceByCriteria("ServiceDueDate ASC", "Status IN (1,2) AND Eye_FK=@Eye_FK", new SqlParameter("@Eye_FK", ID));
                    _nextServiceEyeWash_FK = ID;
                    _nextServiceLoaded = true;
                }
                else if (_nextServiceEyeWash_FK != ID)
                {
                    _nextService = EyeWash_Service.GetServiceByCriteria("ServiceDueDate ASC", "Status IN (1,2) AND Eye_FK=@Eye_FK", new SqlParameter("@Eye_FK", ID));
                    _nextServiceEyeWash_FK = ID;
                    _nextServiceLoaded = true;
                }
                return _nextService;
            }
        }
        
        private EyeWash_Service _prevService = new EyeWash_Service();
        private bool _prevServiceLoaded = false;
        private int _prevServiceEyeWash_FK = -1;
        public EyeWash_Service PrevService
        {
            get
            {
                if (!_prevServiceLoaded)
                {
                    _prevService = EyeWash_Service.GetServiceByCriteria("ServiceVisitedDate DESC", "Status IN (3) AND Eye_FK=@Eye_FK", new SqlParameter("@Eye_FK", ID));
                    _prevServiceEyeWash_FK = ID;
                    _prevServiceLoaded = true;
                }
                else if (_prevServiceEyeWash_FK != ID)
                {
                    _nextService = EyeWash_Service.GetServiceByCriteria("ServiceVisitedDate DESC", "Status IN (3) AND Eye_FK=@Eye_FK", new SqlParameter("@Eye_FK", ID));
                    _prevServiceEyeWash_FK = ID;
                    _nextServiceLoaded = true;
                }
                return _nextService;
            }
        }
        
        private bool _ServiceListLoaded = false;
        private int _ServiceListEyeWash_FK = -1;
        public System.Collections.Generic.List<EyeWash_Service> ServiceList
        {
            get
            {
                if (!_ServiceListLoaded)
                {
                    _serviceList = EyeWash_Service.GetServiceList(ID);
                    _ServiceListLoaded = true;
                    _ServiceListEyeWash_FK = ID;
                }
                else if (_ServiceListEyeWash_FK != ID)
                {
                    _serviceList = EyeWash_Service.GetServiceList(ID);
                    _ServiceListLoaded = true;
                    _ServiceListEyeWash_FK = ID;
                }
                return _serviceList;
            }
        }
        public string ServiceListText
        {
            get
            {
                StringBuilder res = new StringBuilder();
                foreach (EyeWash_Service item in ServiceList)
                {
                    if (item.Status == EyeWash_ServiceStatusType.Aktiv | item.Status == EyeWash_ServiceStatusType.OverDue)
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
        
        public int EyeWashcount
        {
            get
            {
                return GetEyeWashcountByCompany(CompanyID);
            }
        }
        
#endregion
        
#region  Shared Populate
        
        private static void AddParms(ref DBAccess db, EyeWash rec)
        {
            var with_1 = rec;
            db.AddInt("Status", (System.Int32) with_1.Status);
            db.AddInt("CompanyID", with_1.CompanyID);
            
            db.AddInt("ModelID", with_1.ModelID);
            db.AddNVarChar("SerialNo", with_1.SerialNo, 50);
            db.AddNVarChar("FirmwareVersion", with_1.FirmwareVersion, 50);
            db.AddNVarChar("FirmwareLanguage", with_1._FirmwareLanguage, 50);
            
            db.AddDateTime("DeleveryDate", with_1.DeleveryDate);
            db.AddDateTime("WarencyExpireDate", with_1.WarencyExpireDate);
            db.AddDateTime("EmailSendt", with_1.EmailSendt);
            
            db.AddDateTime("BottleActivationDate", with_1.BottleActivationDate);
            db.AddDateTime("BottleWarencyExpireDate", with_1.BottleWarencyExpireDate);
            
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
            db.AddNVarChar("LocationBuildingNo", with_1.LocationBuildingNo, 10);
            
            db.AddDateTime("RegisterretCSI", with_1.RegisterretCSI);
            db.AddNVarChar("RegisterretAF", with_1.RegisterretAF, 50);
            
            db.AddNVarChar("Note", with_1.Note, -1);
            
            AddParmsStandard(db, rec);
        }
        
        private static void Populate(SqlDataReader dr, EyeWash rec)
        {
            var with_1 = rec;
            with_1.Status = (RescueTekniq.BOL.EyeWash_StatusEnum) (dr.DBtoInt("Status"));
            with_1.CompanyID = System.Convert.ToInt32(dr.DBtoInt("CompanyID"));
            
            with_1.ModelID = System.Convert.ToInt32(dr.DBtoInt("ModelID"));
            with_1.SerialNo = dr.DBtoString("SerialNo");
            with_1.FirmwareVersion = dr.DBtoString("FirmwareVersion");
            with_1.FirmwareLanguage = dr.DBtoString("FirmwareLanguage");
            
            with_1.DeleveryDate = System.Convert.ToDateTime(dr.DBtoDate("DeleveryDate"));
            with_1.WarencyExpireDate = System.Convert.ToDateTime(dr.DBtoDate("WarencyExpireDate"));
            with_1.EmailSendt = System.Convert.ToDateTime(dr.DBtoDate("EmailSendt"));
            
            with_1.BottleActivationDate = System.Convert.ToDateTime(dr.DBtoDate("BottleActivationDate"));
            with_1.BottleWarencyExpireDate = System.Convert.ToDateTime(dr.DBtoDate("BottleWarencyExpireDate"));
            
            with_1.BilagStatus = (RescueTekniq.BOL.EyeWash_BilagStatus) (dr.DBtoInt("BilagStatus"));
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
            with_1.LocationBuildingNo = dr.DBtoString("LocationBuildingNo");
            
            with_1.RegisterretCSI = System.Convert.ToDateTime(dr.DBtoDate("RegisterretCSI"));
            with_1.RegisterretAF = dr.DBtoString("RegisterretAF");
            
            with_1.Note = dr.DBtoString("Note");
            
            PopulateStandard(dr, rec);
        }
        
#endregion
        
#region  Stored Procedure Names
        
        private const string _SQLDelete = "Co2Db_EYE_Delete";
        private const string _SQLInsert = "Co2Db_EYE_Insert";
        private const string _SQLUpdate = "Co2Db_EYE_Update";
        private const string _SQLSelectAll = "Co2Db_EYE_SelectAll";
        private const string _SQLSelectID = "Co2Db_EYE_SelectID";
        private const string _SQLSelectOne = "Co2Db_EYE_SelectOne";
        private const string _SQLSelectAllVareGrp = "Co2Db_EYE_SelectAllVareGrp";
        private const string _SQLSelectBySearch = "Co2Db_EYE_SelectBySearch";
        private const string _SQLSelectByCompany = "Co2Db_EYE_SelectByCompany";
        private const string _SQLSelectByAfventer = "Co2Db_EYE_SelectByAfventer";
        private const string _SQLSelectByBilagStatus = "Co2Db_EYE_SelectByBilagStatus";
        
        private const string _SQLSelectByGuid = "Co2Db_EYE_SelectByGuid";
        private const string _SQLSelectBySerialNo = "Co2Db_EYE_SelectBySerialNo";
        
        private const string _SQLCountByCompany = "Co2Db_EYE_CountEYEByCompany";
        private const string _SQLSelectIDByCompany = "Co2Db_EYE_SelectIDByCompany";
        
        private const string _SQLSelectAllSoonExpired = "Co2Db_EYE_SelectAllSoonExpired";
        private const string _SQLSelectAllExpired = "Co2Db_EYE_SelectAllExpired";
        
        private const string _SQLSearch = "Co2Db_EYE_Search";
        
        private const string _SQLSearchConcernEyeWash = "Co2Db_EYE_SelectConcernEyeWash";
        
#endregion
        
#region  Metoder
        
#region  Manipulate data
        
        public int Delete()
        {
            return Delete(System.Convert.ToInt32(this));
        }
        public static int Delete(int ID)
        {
            EyeWash item = new EyeWash(ID);
            return Delete(item);
        }
        public static int Delete(EyeWash A)
        {
            int retval = -1;
            
            if (A.ID > 0)
            {
                foreach (EyeWash_Bottle B in A.BottleList)
                {
                    B.Delete();
                }
                
                DBAccess db = new DBAccess();
                db.AddInt("ID", A.ID);
                
                retval = db.ExecuteNonQuery(_SQLDelete);
                AddLog(Status: "EyeWash", Logtext: string.Format("Delete EyeWash: ID:{0} SNo:{1} ", A.ID, A.SerialNo), Metode: "Delete");
            }
            
            return retval;
            
        }
        public static int DeleteCompanyEyeWash(int companyID)
        {
            foreach (EyeWash A in GetEyeWashList(companyID))
            {
                A.Delete();
            }
            return 1;
        }
        
        public int Insert()
        {
            return Insert(this);
        }
        public static int Insert(EyeWash rec)
        {
            DBAccess db = new DBAccess();
            
            AddParms(ref db, rec);
            
            SqlParameter objParam = new SqlParameter("@ID", 0);
            objParam.Direction = ParameterDirection.Output;
            db.Parameters.Add(objParam);
            int retval = db.ExecuteNonQuery(_SQLInsert);
            if (retval == 1)
            {
                rec.ID = Funktioner.ToInt(objParam.Value, -1); //Integer.Parse(objParam.Value.ToString)
                AddLog(Status: "EyeWash", Logtext: string.Format("Create EyeWash: ID:{0} SNo:{1} ", rec.ID, rec.SerialNo), Metode: "Insert");
                return rec.ID; //Integer.Parse(objParam.Value.ToString)
            }
            else
            {
                AddLog(Status: "EyeWash", Logtext: string.Format("Failure to Create EyeWash: SNo:{0}", rec.SerialNo), logtype: LogTypeEnum.Error, Metode: "Insert");
                return -1;
            }
        }
        
        
        public int Update()
        {
            return Update(this);
        }
        public static int Update(EyeWash rec)
        {
            DBAccess db = new DBAccess();
            
            db.AddInt("ID", rec.ID);
            AddParms(ref db, rec);
            
            int retval = db.ExecuteNonQuery(_SQLUpdate);
            AddLog(Status: "EyeWash", Logtext: string.Format("Update EyeWash: ID:{0} SNo:{1} ", rec.ID, rec.SerialNo), Metode: "Update");
            return retval;
        }
        
        
        //            Log.AddLog("EyeWash", String.Format("Create EyeWash: ID:{0} SNo:{1} ", c.ID, c.SerialNo))
        public int Save()
        {
            return Save(this);
        }
        public static int Save(EyeWash rec)
        {
            int retval = 0; //db.ExecuteNonQuery(_SQLUpdate)
            if (rec.ID > 0)
            {
                retval = Update(rec);
            }
            else
            {
                retval = Insert(rec);
            }
            return retval;
        }
        
        public int MoveToNewOwner(int NewOwner)
        {
            CompanyID = NewOwner;
            AddLog(Status: "EyeWash", Logtext: string.Format("Update EyeWash: ID:{0} SNo:{1} New owner: {2}-{3}", ID, SerialNo, CompanyID, Firmanavn), Metode: "MoveToNewOwner");
            return Save();
        }
        public static int MoveToNewOwner(int ID, int NewOwner)
        {
            EyeWash a = new EyeWash(ID);
            a.CompanyID = NewOwner;
            AddLog(Status: "EyeWash", Logtext: string.Format("Update EyeWash: ID:{0} SNo:{1} New owner: {2}-{3}", a.ID, a.SerialNo, a.CompanyID, a.Firmanavn), Metode: "MoveToNewOwner");
            return Save(a);
        }
        
#endregion
        
#region  Get data
        
        public static EyeWash GetEyeWashByCriteria(string OrderBY, string criteria, params SqlParameter[] @params)
        {
            List<EyeWash> list = GetEyeWashsByCriteria(OrderBY, criteria, @params);
            if (list.Count > 0)
            {
                return list[0];
            }
            else
            {
                return new EyeWash(); //Nothing
            }
        }
        public static List<EyeWash> GetEyeWashsByCriteria(string OrderBY, string criteria, params SqlParameter[] @params)
        {
            DBAccess db = new DBAccess();
            //Dim conn As SqlConnection = DataFunctions.GetConnection()
            string query = "";
            query += "SELECT ";
            if (OrderBY != "")
            {
                query += " TOP (100) PERCENT ";
            }
            query += " * FROM vw_Co2Db_EyeWash ";
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
            
            List<EyeWash> list = new List<EyeWash>();
            while (dr.Read())
            {
                EyeWash _EyeWash = new EyeWash();
                EyeWash.Populate(dr, _EyeWash);
                list.Add(_EyeWash);
            }
            
            db.Dispose();
            
            return list;
        }
        public static DataSet GetEyeWashsByCriteriaDS(string fieldnames, string criteria, params SqlParameter[] @params)
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
            query += " FROM vw_Co2Db_EyeWash ";
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
        
        public static EyeWash GetEyeWash(int ID)
        {
            DBAccess db = new DBAccess();
            EyeWash c = new EyeWash();
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
        public static EyeWash GetEyeWashbyGuid(Guid GuID)
        {
            DBAccess db = new DBAccess();
            EyeWash c = new EyeWash();
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
        
        public static EyeWash GetEyeWashBySerialNo(string SerialNo)
        {
            DBAccess db = new DBAccess();
            EyeWash c = new EyeWash();
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
        
        
        public static DataSet GetEyeWash_DS(int ID)
        {
            DBAccess db = new DBAccess();
            db.AddInt("ID", ID);
            
            db.AddBoolean("IsAgent", AdgangsKontrol.IsAgent());
            db.AddGuid("AgentID", CurrentUserID);
            
            DataSet ds = db.ExecuteDataSet(_SQLSelectID);
            return ds;
        }
        
        public static DataSet GetAllEyeWash()
        {
            DBAccess db = new DBAccess();
            
            db.AddBoolean("IsAgent", AdgangsKontrol.IsAgent());
            db.AddGuid("AgentID", CurrentUserID);
            
            //db.Parameters.Add(New SqlParameter("@sUserName", username))
            DataSet ds = db.ExecuteDataSet(_SQLSelectAll);
            return ds;
        }
        
        public static DataSet GetEyeWashbyCompany(int CompanyID)
        {
            DBAccess db = new DBAccess();
            db.AddInt("CompanyID", CompanyID);
            
            db.AddBoolean("IsAgent", AdgangsKontrol.IsAgent());
            db.AddGuid("AgentID", CurrentUserID);
            
            DataSet ds = db.ExecuteDataSet(_SQLSelectByCompany);
            return ds;
        }
        public static DataSet GetEyeWashbyGUID_DS(Guid GuID)
        {
            DBAccess db = new DBAccess();
            db.AddGuid("Guid", GuID);
            
            db.AddBoolean("IsAgent", AdgangsKontrol.IsAgent());
            db.AddGuid("AgentID", CurrentUserID);
            
            DataSet ds = db.ExecuteDataSet(_SQLSelectByGuid);
            return ds;
        }
        
        //    Public Shared Function GetEyeWashByAfventer() As DataSet
        //        Return GetEyeWashbyAfventer(-1)
        //    End Function
        public static DataSet GetEyeWashByAfventer(int CompanyID)
        {
            return GetEyeWashbyBilagStatus(CompanyID, 1);
        }
        
        //Public Shared Function GetEyeWashbyModtaget() As DataSet
        //    Return GetEyeWashbyModtaget(-1)
        //End Function
        public static DataSet GetEyeWashByModtaget(int CompanyID)
        {
            return GetEyeWashbyBilagStatus(CompanyID, 2);
        }
        
        public static DataSet GetEyeWashbyBilagStatus(int CompanyID, int BilagStatus)
        {
            DBAccess db = new DBAccess();
            db.AddInt("CompanyID", CompanyID);
            db.AddInt("BilagStatus", BilagStatus);
            
            db.AddBoolean("IsAgent", AdgangsKontrol.IsAgent());
            db.AddGuid("AgentID", CurrentUserID);
            
            DataSet ds = db.ExecuteDataSet(_SQLSelectByBilagStatus);
            return ds;
        }
        
        public static int GetEyeWashcountByCompany(int CompanyID)
        {
            int res = 0;
            DBAccess db = new DBAccess();
            db.AddInt("CompanyID", CompanyID);
            
            db.AddBoolean("IsAgent", AdgangsKontrol.IsAgent());
            db.AddGuid("AgentID", CurrentUserID);
            
            res = Funktioner.ToInt(db.ExecuteScalar(_SQLCountByCompany));
            return res;
        }
        
        public static int GetEyeWashCountByKoncern(int CompanyID)
        {
            int EyeWashcount = 0;
            List<int> VirkList = new List<int>();
            
            DataSet ds = Virksomhed.GetCompanyChildrenList(CompanyID);
            
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                int ID = System.Convert.ToInt32(row["CompanyID"]);
                VirkList.Add(ID);
            }
            
            EyeWashcount += GetEyeWashcountByCompany(CompanyID);
            foreach (int ID in VirkList)
            {
                EyeWashcount += GetEyeWashCountByKoncern(ID);
            }
            
            return EyeWashcount;
        }
        
        
        public static DataSet GetKoncernEyeWash(int CompanyID)
        {
            DBAccess db = new DBAccess();
            db.AddInt("CompanyID", CompanyID);
            
            db.AddBoolean("IsAgent", AdgangsKontrol.IsAgent());
            db.AddGuid("AgentID", CurrentUserID);
            
            DataSet ds = db.ExecuteDataSet(_SQLSearchConcernEyeWash);
            return ds;
        }
        //Public Shared Function GetEyeWashCountByCompany(ByVal CompanyID As Integer) As Integer
        //    Dim db As DBAccess = New DBAccess
        //    db.AddInt("CompanyID", CompanyID)
        //    Dim res As Integer = ToInteger(db.ExecuteScalar(_SQLCountByCompany), 0)
        
        //    Return res
        //End Function
        //[Co2Db_EyeWash_Trainer_CountEyeWashByCompany]
        public static List<EyeWash> GetKoncernEyeWashlist(int CompanyID)
        {
            List<EyeWash> EyeWashlist = new List<EyeWash>();
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
            
            EyeWashlist.AddRange(GetEyeWashList(CompanyID));
            foreach (int ID in VirkList)
            {
                EyeWashlist.AddRange(GetKoncernEyeWashlist(ID));
            }
            
            return EyeWashlist;
        }
        
        public static System.Collections.Generic.List<EyeWash> GetEyeWashList(int CompanyID)
        {
            System.Collections.Generic.List<EyeWash> result = new System.Collections.Generic.List<EyeWash>();
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
                    result.Add(EyeWash.GetEyeWash(ID));
                }
            }
            dr.Close();
            //Catch ex As Exception
            //Throw ex
            //End Try
            
            return result;
        }
        
        //ALTER PROCEDURE [vicjos1_sysadm].[Co2Db_EyeWash_SelectAllExpired]
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
        
        public static System.Collections.Generic.List<EyeWash> GetListSoonExpired(int CompanyID)
        {
            System.Collections.Generic.List<EyeWash> result = new System.Collections.Generic.List<EyeWash>();
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
                    result.Add(EyeWash.GetEyeWash(ID));
                }
            }
            dr.Close();
            //Catch ex As Exception
            //    Throw ex
            //End Try
            
            return result;
        }
        
        public static DataSet Search_EyeWash(string Search)
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
        
        public static DataSet GetEyeWash_SearchAll(string Postnr, string SerialNo)
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
            
            //	EXEC	@return_value = [vicjos1_sysadm].[Co2Db_EyeWash_Search]
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
        public static string Tags(string tekst, RescueTekniq.BOL.EyeWash item)
        {
            string res = "";
            StringBuilder sb = new StringBuilder();
            sb.Append(tekst);
            sb.Replace("[VARE.TYPE]", "EyeWash/Hjertestarter");
            
            sb.Replace("[EyeWash.ANSVARLIG]", item.ResponsibleName);
            sb.Replace("[EyeWash.ANSVARLIG.NAVN]", item.ResponsibleName);
            sb.Replace("[EyeWash.ANSVARLIG.EMAIL]", item.ResponsibleEmail);
            sb.Replace("[EyeWash.ANSVARLIG.TELEFON]", item.ResponsiblePhone);
            
            sb.Replace("[EyeWash.NOTE]", item.Note);
            
            sb.Replace("[EyeWash.TYPE]", "[VARE.NAVN]");
            sb.Replace("[EyeWash.SERIALNO]", item.SerialNo);
            sb.Replace("[EyeWash.LOCATION]", item.Location);
            sb.Replace("[EyeWash.LOCATIONFULDADR]", item.LocationFuldAdresse);
            sb.Replace("[EyeWash.VARENR]", "[VARE.VARENR]"); //item.Vare.VareNr)
            sb.Replace("[EyeWash.MODEL]", "[VARE.NAVN]"); //item.Vare.Navn)
            sb.Replace("[EyeWash.EXPIREDATE]", item.WarencyExpireDate.ToString("dd MMMM yyyy"));
            res = sb.ToString();
            res = item.KundeGrpPris.Tags(res);
            res = item.Virksomhed.Tags(res);
            
            return res;
        }
        
#endregion
        
    }
    
    
}
