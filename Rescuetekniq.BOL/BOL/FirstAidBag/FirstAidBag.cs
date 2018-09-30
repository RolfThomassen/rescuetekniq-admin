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
    
    public class FAB : BaseObject
    {
        
#region  New
        
        public FAB()
        {
            
        }
        public FAB(int ID)
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
        public FAB(System.Guid GUID)
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
        
        private FAB_StatusEnum _Status = FAB_StatusEnum.Active;
        private int _CompanyID;
        private int _ModelID;
        private string _SerialNo; //(50)
        
        private DateTime _DeleveryDate;
        private DateTime _WarencyExpireDate;
        private DateTime _EmailSendt;
        private DateTime _DecommissionDate;
        
        private FAB_BilagStatus _BilagStatus = FAB_BilagStatus.Initialize;
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
        
        private int _ServiceDate;
        private DateTime _ServiceDateEmail;
        private string _RegisterretAF;
        
        private string _PDF;
        private bool _VisibleRescueinfo;
        
        private string _Note; //(max)
        
        private RescueTekniq.BOL.Virksomhed _virksomhed = new RescueTekniq.BOL.Virksomhed();
        private RescueTekniq.BOL.Vare _vare = new RescueTekniq.BOL.Vare();
        private List<FirstAidBag_Service> _serviceList;
        
        private int _AntalFAB = 0;
        
        private RescueTekniq.BOL.KundeGrp_Pris _kgp = new RescueTekniq.BOL.KundeGrp_Pris();
#endregion
        
#region  Properties
        
        public FAB_StatusEnum Status
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
        //
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
        
        
        public DateTime ServiceDateEmail
        {
            get
            {
                return _ServiceDateEmail;
            }
            set
            {
                _ServiceDateEmail = value;
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
        
        public DateTime DecommissionDate
        {
            get
            {
                return _DecommissionDate;
            }
            set
            {
                _DecommissionDate = value;
            }
        }
        
        public FAB_BilagStatus BilagStatus
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
        
        public int ServiceDate
        {
            get
            {
                return _ServiceDate;
            }
            set
            {
                _ServiceDate = value;
            }
        }
        public string ServiceMonth
        {
            get
            {
                return Combobox.GetTitleByValue("Combobox.Måneder", System.Convert.ToString(ServiceDate));
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
        
        public string PDF
        {
            get
            {
                return Vare.FaktaArkPath;
            }
            set
            {
                
            }
        }
        
        public bool VisibleRescueinfo
        {
            get
            {
                return _VisibleRescueinfo;
            }
            set
            {
                _VisibleRescueinfo = value;
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
                    //KundeGrp_Pris.GetCompany_VarePris(item.FAB.CompanyID, item.Vare.ID)
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
        
        
        private FirstAidBag_Service _nextService = new FirstAidBag_Service();
        private bool _nextServiceLoaded = false;
        private int _nextServiceFAB_FK = -1;
        public FirstAidBag_Service NextService
        {
            get
            {
                if (!_nextServiceLoaded) //ServiceActive = 1 AND
                {
                    _nextService = FirstAidBag_Service.GetServiceByCriteria("ServiceDueDate ASC", "Status IN (1,2) AND FAB_FK=@FAB_FK", new SqlParameter("@FAB_FK", ID));
                    _nextServiceFAB_FK = ID;
                    _nextServiceLoaded = true;
                }
                else if (_nextServiceFAB_FK != ID)
                {
                    _nextService = FirstAidBag_Service.GetServiceByCriteria("ServiceDueDate ASC", "Status IN (1,2) AND FAB_FK=@FAB_FK", new SqlParameter("@FAB_FK", ID));
                    _nextServiceFAB_FK = ID;
                    _nextServiceLoaded = true;
                }
                return _nextService;
            }
        }
        
        private FirstAidBag_Service _prevService = new FirstAidBag_Service();
        private bool _prevServiceLoaded = false;
        private int _prevServiceFAB_FK = -1;
        public FirstAidBag_Service PrevService
        {
            get
            {
                if (!_prevServiceLoaded)
                {
                    _prevService = FirstAidBag_Service.GetServiceByCriteria("ServiceVisitedDate DESC", "Status IN (3) AND FAB_FK=@FAB_FK", new SqlParameter("@FAB_FK", ID));
                    _prevServiceFAB_FK = ID;
                    _prevServiceLoaded = true;
                }
                else if (_prevServiceFAB_FK != ID)
                {
                    _nextService = FirstAidBag_Service.GetServiceByCriteria("ServiceVisitedDate DESC", "Status IN (3) AND FAB_FK=@FAB_FK", new SqlParameter("@FAB_FK", ID));
                    _prevServiceFAB_FK = ID;
                    _nextServiceLoaded = true;
                }
                return _nextService;
            }
        }
        
        private bool _ServiceListLoaded = false;
        private int _ServiceListFAB_FK = -1;
        public System.Collections.Generic.List<FirstAidBag_Service> ServiceList
        {
            get
            {
                if (!_ServiceListLoaded)
                {
                    _serviceList = FirstAidBag_Service.GetServiceList(ID);
                    _ServiceListLoaded = true;
                    _ServiceListFAB_FK = ID;
                }
                else if (_ServiceListFAB_FK != ID)
                {
                    _serviceList = FirstAidBag_Service.GetServiceList(ID);
                    _ServiceListLoaded = true;
                    _ServiceListFAB_FK = ID;
                }
                return _serviceList;
            }
        }
        public string ServiceListText
        {
            get
            {
                StringBuilder res = new StringBuilder();
                foreach (FirstAidBag_Service item in ServiceList)
                {
                    if (item.Status == FAB_ServiceStatusType.Aktiv | item.Status == FAB_ServiceStatusType.OverDue)
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
        
        public int FABcount
        {
            get
            {
                return GetFABCountByCompany(CompanyID);
            }
        }
        
#endregion
        
#region  Shared Populate
        
        private static void AddParms(ref DBAccess db, FAB c)
        {
            var with_1 = c;
            db.AddInt("Status", (System.Int32) with_1.Status);
            db.AddInt("CompanyID", with_1.CompanyID);
            
            db.AddInt("ModelID", with_1.ModelID);
            db.AddNVarChar("SerialNo", with_1.SerialNo, 50);
            
            db.AddDateTime("DeleveryDate", with_1.DeleveryDate);
            db.AddDateTime("WarencyExpireDate", with_1.WarencyExpireDate);
            db.AddDateTime("DecommissionDate", with_1.DecommissionDate);
            db.AddDateTime("EmailSendt", with_1.EmailSendt);
            
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
            
            db.AddInt("ServiceDate", with_1.ServiceDate);
            db.AddDateTime("ServiceDateEmail", with_1.ServiceDateEmail);
            db.AddBoolean("VisibleRescueinfo", with_1.VisibleRescueinfo);
            //
            db.AddNVarChar("RegisterretAF", with_1.RegisterretAF, 50);
            
            db.AddNVarChar("Note", with_1.Note, -1);
            
            AddParmsStandard(db, c);
        }
        
        private static void Populate(System.Data.SqlClient.SqlDataReader dr, FAB c)
        {
            var with_1 = c;
            with_1.Status = (RescueTekniq.BOL.FAB_StatusEnum) (dr.DBtoInt("Status"));
            with_1.CompanyID = System.Convert.ToInt32(dr.DBtoInt("CompanyID"));
            
            with_1.ModelID = System.Convert.ToInt32(dr.DBtoInt("ModelID"));
            with_1.SerialNo = dr.DBtoString("SerialNo");
            
            with_1.DeleveryDate = System.Convert.ToDateTime(dr.DBtoDate("DeleveryDate"));
            with_1.WarencyExpireDate = System.Convert.ToDateTime(dr.DBtoDate("WarencyExpireDate"));
            with_1.DecommissionDate = System.Convert.ToDateTime(dr.DBtoDate("DecommissionDate"));
            with_1.EmailSendt = System.Convert.ToDateTime(dr.DBtoDate("EmailSendt"));
            
            with_1.BilagStatus = (RescueTekniq.BOL.FAB_BilagStatus) (dr.DBtoInt("BilagStatus"));
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
            
            with_1.ServiceDate = System.Convert.ToInt32(dr.DBtoInt("ServiceDate"));
            with_1.ServiceDateEmail = System.Convert.ToDateTime(dr.DBtoDate("ServiceDateEmail"));
            with_1.VisibleRescueinfo = System.Convert.ToBoolean(dr.DBtoBoolean("VisibleRescueinfo"));
            with_1.RegisterretAF = dr.DBtoString("RegisterretAF");
            
            with_1.Note = dr.DBtoString("Note");
            
            PopulateStandard(dr, c);
        }
        
#endregion
        
#region  Stored Procedure Names
        
        private const string _SQLDelete = "Co2Db_FirstAidBag_Delete";
        private const string _SQLInsert = "Co2Db_FirstAidBag_Insert";
        private const string _SQLUpdate = "Co2Db_FirstAidBag_Update";
        private const string _SQLSelectAll = "Co2Db_FirstAidBag_SelectAll";
        private const string _SQLSelectID = "Co2Db_FirstAidBag_SelectID";
        private const string _SQLSelectOne = "Co2Db_FirstAidBag_SelectOne";
        private const string _SQLSelectAllVareGrp = "Co2Db_FirstAidBag_SelectAllVareGrp";
        private const string _SQLSelectBySearch = "Co2Db_FirstAidBag_SelectBySearch";
        private const string _SQLSelectByCompany = "Co2Db_FirstAidBag_SelectByCompany";
        private const string _SQLSelectByAfventer = "Co2Db_FirstAidBag_SelectByAfventer";
        private const string _SQLSelectByBilagStatus = "Co2Db_FirstAidBag_SelectByBilagStatus";
        
        private const string _SQLSelectByGuid = "Co2Db_FirstAidBag_SelectByGuid";
        private const string _SQLSelectBySerialNo = "Co2Db_FirstAidBag_SelectBySerialNo";
        
        private const string _SQLCountByCompany = "Co2Db_FirstAidBag_CountFABByCompany";
        private const string _SQLSelectIDByCompany = "Co2Db_FirstAidBag_SelectIDByCompany";
        
        private const string _SQLSelectAllSoonExpired = "Co2Db_FirstAidBag_SelectAllSoonExpired";
        private const string _SQLSelectAllExpired = "Co2Db_FirstAidBag_SelectAllExpired";
        
        private const string _SQLSearch = "Co2Db_FirstAidBag_Search";
        
        private const string _SQLSearchConcernFAB = "Co2Db_FirstAidBag_SelectConcernFAB";
        
#endregion
        
#region  Metoder
        
#region  Manipulate data
        
        public int Delete()
        {
            return Delete(System.Convert.ToInt32(this));
        }
        public static int Delete(int ID)
        {
            FAB item = new FAB(ID);
            return Delete(item);
        }
        public static int Delete(FAB rec)
        {
            int retval = -1;
            
            if (rec.ID > 0)
            {
                foreach (FirstAidBag_Service S in rec.ServiceList)
                {
                    S.Delete();
                }
                
                DBAccess db = new DBAccess();
                db.AddInt("ID", rec.ID);
                
                retval = db.ExecuteNonQuery(_SQLDelete);
                AddLog(Status: "FAB", Logtext: string.Format("Delete FAB: ID:{0} SNo:{1} ", rec.ID, rec.SerialNo), Metode: "Delete");
            }
            
            return retval;
            
        }
        public static int DeleteCompanyFAB(int companyID)
        {
            foreach (FAB rec in GetFABList(companyID))
            {
                rec.Delete();
            }
            return 1;
        }
        
        public int Insert()
        {
            return Insert(this);
        }
        public static int Insert(FAB rec)
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
                AddLog(Status: "FAB", Logtext: string.Format("Create FAB: ID:{0} SNo:{1} ", rec.ID, rec.SerialNo), Metode: "Insert");
                return rec.ID;
            }
            else
            {
                AddLog(Status: "FAB", Logtext: string.Format("Failure to Create FAB: SNo:{0}", rec.SerialNo), logtype: LogTypeEnum.Error, Metode: "Insert");
                return -1;
            }
        }
        
        
        public int Update()
        {
            return Update(this);
        }
        public static int Update(FAB c)
        {
            DBAccess db = new DBAccess();
            
            db.AddInt("ID", c.ID);
            AddParms(ref db, c);
            
            int retval = db.ExecuteNonQuery(_SQLUpdate);
            AddLog(Status: "FAB", Logtext: string.Format("Update FAB: ID:{0} SNo:{1} ", c.ID, c.SerialNo), Metode: "Update");
            return retval;
        }
        
        //            Log.AddLog("FAB", String.Format("Create FAB: ID:{0} SNo:{1} ", c.ID, c.SerialNo))
        public int Save()
        {
            return Save(this);
        }
        public static int Save(FAB rec)
        {
            int retval = 0; //db.ExecuteNonQuery(_SQLUpdate)
            if (rec.ID > 0)
            {
                retval = rec.Update();
            }
            else
            {
                retval = rec.Insert();
            }
            return retval;
        }
        
        public int MoveToNewOwner(int NewOwner)
        {
            CompanyID = NewOwner;
            AddLog(Status: "FAB", Logtext: string.Format("Update FAB: ID:{0} SNo:{1} New owner: {2}-{3}", ID, SerialNo, CompanyID, Firmanavn), Metode: "MoveToNewOwner");
            return Save();
        }
        public static int MoveToNewOwner(int ID, int NewOwner)
        {
            FAB rec = new FAB(ID);
            rec.CompanyID = NewOwner;
            AddLog(Status: "FAB", Logtext: string.Format("Update FAB: ID:{0} SNo:{1} New owner: {2}-{3}", rec.ID, rec.SerialNo, rec.CompanyID, rec.Firmanavn), Metode: "MoveToNewOwner");
            return rec.Save();
        }
        
#endregion
        
#region  Get data
        
        public static FAB GetFABByCriteria(string OrderBY, string criteria, params SqlParameter[] @params)
        {
            List<FAB> list = GetFABsByCriteria(OrderBY, criteria, @params);
            if (list.Count > 0)
            {
                return list[0];
            }
            else
            {
                return new FAB(); //Nothing
            }
        }
        public static List<FAB> GetFABsByCriteria(string OrderBY, string criteria, params SqlParameter[] @params)
        {
            DBAccess db = new DBAccess();
            //Dim conn As SqlConnection = DataFunctions.GetConnection()
            string query = "";
            query += "SELECT ";
            if (OrderBY != "")
            {
                query += " TOP (100) PERCENT ";
            }
            query += " * FROM vw_Co2Db_FirstAidBag ";
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
            
            List<FAB> list = new List<FAB>();
            while (dr.Read())
            {
                FAB _fab = new FAB();
                FAB.Populate(dr, _fab);
                list.Add(_fab);
            }
            
            db.Dispose();
            
            return list;
        }
        public static DataSet GetFABsByCriteriaDS(string fieldnames, string criteria, params SqlParameter[] @params)
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
            query += " FROM vw_Co2Db_FirstAidBag ";
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
        
        public static FAB GetFAB(int ID)
        {
            DBAccess db = new DBAccess();
            FAB c = new FAB();
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
        public static FAB GetFABbyGuid(Guid GuID)
        {
            DBAccess db = new DBAccess();
            FAB c = new FAB();
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
        
        public static FAB GetFABBySerialNo(string SerialNo)
        {
            DBAccess db = new DBAccess();
            FAB c = new FAB();
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
        
        
        public static DataSet GetFAB_DS(int ID)
        {
            DBAccess db = new DBAccess();
            db.AddInt("ID", ID);
            
            db.AddBoolean("IsAgent", AdgangsKontrol.IsAgent());
            db.AddGuid("AgentID", CurrentUserID);
            
            DataSet ds = db.ExecuteDataSet(_SQLSelectID);
            return ds;
        }
        
        public static DataSet GetAllFAB()
        {
            DBAccess db = new DBAccess();
            
            db.AddBoolean("IsAgent", AdgangsKontrol.IsAgent());
            db.AddGuid("AgentID", CurrentUserID);
            
            //db.Parameters.Add(New SqlParameter("@sUserName", username))
            DataSet ds = db.ExecuteDataSet(_SQLSelectAll);
            return ds;
        }
        
        public static DataSet GetFABbyCompany(int CompanyID)
        {
            DBAccess db = new DBAccess();
            db.AddInt("CompanyID", CompanyID);
            
            db.AddBoolean("IsAgent", AdgangsKontrol.IsAgent());
            db.AddGuid("AgentID", CurrentUserID);
            
            DataSet ds = db.ExecuteDataSet(_SQLSelectByCompany);
            return ds;
        }
        public static DataSet GetFABbyGUID_DS(Guid GuID)
        {
            DBAccess db = new DBAccess();
            db.AddGuid("Guid", GuID);
            
            db.AddBoolean("IsAgent", AdgangsKontrol.IsAgent());
            db.AddGuid("AgentID", CurrentUserID);
            
            DataSet ds = db.ExecuteDataSet(_SQLSelectByGuid);
            return ds;
        }
        
        public static DataSet GetFABbyAfventer()
        {
            return GetFABbyAfventer(-1);
        }
        public static DataSet GetFABbyAfventer(int CompanyID)
        {
            return GetFABbyBilagStatus(CompanyID, 1);
        }
        
        public static DataSet GetFABbyModtaget()
        {
            return GetFABbyModtaget(-1);
        }
        public static DataSet GetFABbyModtaget(int CompanyID)
        {
            return GetFABbyBilagStatus(CompanyID, 2);
        }
        
        public static DataSet GetFABbyBilagStatus(int CompanyID, int BilagStatus)
        {
            DBAccess db = new DBAccess();
            db.AddInt("CompanyID", CompanyID);
            db.AddInt("BilagStatus", BilagStatus);
            
            db.AddBoolean("IsAgent", AdgangsKontrol.IsAgent());
            db.AddGuid("AgentID", CurrentUserID);
            
            DataSet ds = db.ExecuteDataSet(_SQLSelectByBilagStatus);
            return ds;
        }
        
        public static int GetFABCountByCompany(int CompanyID)
        {
            int res = 0;
            DBAccess db = new DBAccess();
            db.AddInt("CompanyID", CompanyID);
            
            db.AddBoolean("IsAgent", AdgangsKontrol.IsAgent());
            db.AddGuid("AgentID", CurrentUserID);
            
            res = Funktioner.ToInt(db.ExecuteScalar(_SQLCountByCompany));
            return res;
        }
        
        public static int GetFABCountByKoncern(int CompanyID)
        {
            int FABcount = 0;
            List<int> VirkList = new List<int>();
            
            DataSet ds = Virksomhed.GetCompanyChildrenList(CompanyID);
            
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                int ID = System.Convert.ToInt32(row["CompanyID"]);
                VirkList.Add(ID);
            }
            
            FABcount += GetFABCountByCompany(CompanyID);
            foreach (int ID in VirkList)
            {
                FABcount += GetFABCountByKoncern(ID);
            }
            
            return FABcount;
        }
        
        
        public static DataSet GetKoncernFAB(int CompanyID)
        {
            DBAccess db = new DBAccess();
            db.AddInt("CompanyID", CompanyID);
            
            db.AddBoolean("IsAgent", AdgangsKontrol.IsAgent());
            db.AddGuid("AgentID", CurrentUserID);
            
            DataSet ds = db.ExecuteDataSet(_SQLSearchConcernFAB);
            return ds;
        }
        //Public Shared Function GetFABCountByCompany(ByVal CompanyID As Integer) As Integer
        //    Dim db As DBAccess = New DBAccess
        //    db.AddInt("CompanyID", CompanyID)
        //    Dim res As Integer = ToInteger(db.ExecuteScalar(_SQLCountByCompany), 0)
        
        //    Return res
        //End Function
        //[Co2Db_FirstAidBag_Trainer_CountFABByCompany]
        public static List<FAB> GetKoncernFABlist(int CompanyID)
        {
            List<FAB> FABlist = new List<FAB>();
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
            
            FABlist.AddRange(GetFABList(CompanyID));
            foreach (int ID in VirkList)
            {
                FABlist.AddRange(GetKoncernFABlist(ID));
            }
            
            return FABlist;
        }
        
        public static System.Collections.Generic.List<FAB> GetFABList(int CompanyID)
        {
            System.Collections.Generic.List<FAB> result = new System.Collections.Generic.List<FAB>();
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
                    result.Add(FAB.GetFAB(ID));
                }
            }
            dr.Close();
            //Catch ex As Exception
            //Throw ex
            //End Try
            
            return result;
        }
        
        //ALTER PROCEDURE [vicjos1_sysadm].[Co2Db_FirstAidBag_SelectAllExpired]
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
        
        public static System.Collections.Generic.List<FAB> GetListSoonExpired(int CompanyID)
        {
            System.Collections.Generic.List<FAB> result = new System.Collections.Generic.List<FAB>();
            int ID = -1;
            DBAccess db = new DBAccess();
            db.AddInt("CompanyID", CompanyID);
            
            db.AddBoolean("IsAgent", AdgangsKontrol.IsAgent());
            db.AddGuid("AgentID", CurrentUserID);
            
            System.Data.SqlClient.SqlDataReader dr = default(System.Data.SqlClient.SqlDataReader);
            //Try
            dr = (System.Data.SqlClient.SqlDataReader) (db.ExecuteReader(_SQLSelectAllSoonExpired));
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ID = System.Convert.ToInt32(dr.DBtoInt("ID"));
                    result.Add(FAB.GetFAB(ID));
                }
            }
            dr.Close();
            //Catch ex As Exception
            //    Throw ex
            //End Try
            
            return result;
        }
        
        public static DataSet Search_FAB(string Search)
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
        
        public static DataSet GetFAB_SearchAll(string Postnr, string SerialNo)
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
            
            //	EXEC	@return_value = [vicjos1_sysadm].[Co2Db_FirstAidBag_Search]
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
        public static string Tags(string tekst, RescueTekniq.BOL.FAB item)
        {
            string res = "";
            StringBuilder sb = new StringBuilder();
            sb.Append(tekst);
            sb.Replace("[VARE.TYPE]", "FAB/FirstAidBag");
            
            sb.Replace("[FAB.ANSVARLIG]", item.ResponsibleName);
            sb.Replace("[FAB.ANSVARLIG.NAVN]", item.ResponsibleName);
            sb.Replace("[FAB.ANSVARLIG.EMAIL]", item.ResponsibleEmail);
            sb.Replace("[FAB.ANSVARLIG.TELEFON]", item.ResponsiblePhone);
            sb.Replace("[FAB.NOTE]", item.Note);
            
            sb.Replace("[FAB.PDF.LINK]", "<a href='[FAB.PDF]' target='_Blank'>LINK</a>");
            sb.Replace("[FAB.PDF]", item.PDF);
            
            sb.Replace("[FAB.SERVICEDATE]", Combobox.GetTitleByValue("Combobox.Måneder", System.Convert.ToString(item.ServiceDate)));
            
            sb.Replace("[FAB.TYPE]", "[VARE.NAVN]");
            sb.Replace("[FAB.SERIALNO]", item.SerialNo);
            sb.Replace("[FAB.LOCATION]", item.Location);
            sb.Replace("[FAB.LOCATIONFULDADR]", item.LocationFuldAdresse);
            sb.Replace("[FAB.VARENR]", "[VARE.VARENR]");
            sb.Replace("[FAB.NAVN]", "[VARE.NAVN]");
            sb.Replace("[FAB.MODEL]", "[VARE.NAVN]");
            sb.Replace("[FAB.EXPIREDATE]", item.WarencyExpireDate.ToString("dd MMMM yyyy"));
            res = sb.ToString();
            res = item.KundeGrpPris.Tags(res);
            res = item.Virksomhed.Tags(res);
            
            return res;
        }
        
#endregion
        
    }
    
    
}
