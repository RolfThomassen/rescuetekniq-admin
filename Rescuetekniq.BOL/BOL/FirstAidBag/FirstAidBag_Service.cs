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
    
    
    public class FirstAidBag_Service : BaseObject
    {
        
#region  New
        
        public FirstAidBag_Service()
        {
            
        }
        public FirstAidBag_Service(int ID)
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
        public FirstAidBag_Service(System.Guid GUID)
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
        
        private FAB_ServiceStatusType _Status = FAB_ServiceStatusType.Aktiv;
        private int _FAB_FK;
        private int _ServiceType = 0;
        private bool _ServiceActive = false;
        private DateTime _ServiceDueDate;
        private DateTime _ServiceVisitedDate;
        private double _ServicePrice = 0;
        private string _ServiceConsultant = "";
        private int _ServiceConsultantID = 0;
        
        private RescueTekniq.BOL.FAB _FirstAidBag;
        private RescueTekniq.BOL.Vare _Vare = new RescueTekniq.BOL.Vare();
        private RescueTekniq.BOL.KundeGrp_Pris _kgp = new RescueTekniq.BOL.KundeGrp_Pris();
#endregion
        
#region  Properties
        
        public FAB_ServiceStatusType Status
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
                res = Combobox.GetTitleByValue("FirstAidBag.Service.Status.Enum", Status.ToString());
                return res;
            }
        }
        public int StatusNo
        {
            get
            {
                return Funktioner.ToInt(_Status);
            }
            set
            {
                _Status = (FAB_ServiceStatusType) value;
            }
        }
        
        public int FAB_FK
        {
            get
            {
                return _FAB_FK;
            }
            set
            {
                _FAB_FK = value;
            }
        }
        
        public int ServiceType
        {
            get
            {
                return _ServiceType;
            }
            set
            {
                _ServiceType = value;
            }
        }
        public string ServiceTypeText
        {
            get
            {
                string res = Vare.Navn;
                return res;
            }
        }
        
        public bool ServiceActive
        {
            get
            {
                return _ServiceActive;
            }
            set
            {
                _ServiceActive = value;
            }
        }
        
        public DateTime ServiceDueDate
        {
            get
            {
                return _ServiceDueDate;
            }
            set
            {
                _ServiceDueDate = value;
            }
        }
        
        public DateTime ServiceVisitedDate
        {
            get
            {
                return _ServiceVisitedDate;
            }
            set
            {
                _ServiceVisitedDate = value;
            }
        }
        
        public string ServiceConsultant
        {
            get
            {
                return _ServiceConsultant;
            }
            set
            {
                _ServiceConsultant = value;
            }
        }
        public int ServiceConsultantID
        {
            get
            {
                return _ServiceConsultantID;
            }
            set
            {
                _ServiceConsultantID = value;
            }
        }
        
        public double ServicePrice
        {
            get
            {
                return _ServicePrice;
            }
            set
            {
                _ServicePrice = value;
            }
        }
        
        public string Firmanavn
        {
            get
            {
                return FirstAidBag.Virksomhed.Firmanavn;
            }
        }
        public string FirstAidBag_model
        {
            get
            {
                return FirstAidBag.Vare.Navn;
            }
        }
        public string AEDSerialNo
        {
            get
            {
                return FirstAidBag.SerialNo;
            }
        }
        
        public string AEDaddresse
        {
            get
            {
                return FirstAidBag.LocationFuldAdresse;
            }
        }
        
        public RescueTekniq.BOL.FAB FirstAidBag
        {
            get
            {
                try
                {
                    if (_FAB_FK > 0)
                    {
                        if (ReferenceEquals(_FirstAidBag, null))
                        {
                            _FirstAidBag = new FAB();
                        }
                        if (!_FirstAidBag.loaded)
                        {
                            _FirstAidBag = RescueTekniq.BOL.FAB.GetFAB(_FAB_FK);
                        }
                        else if (_FirstAidBag.ID != _FAB_FK)
                        {
                            _FirstAidBag = RescueTekniq.BOL.FAB.GetFAB(_FAB_FK);
                        }
                    }
                }
                catch (Exception)
                {
                }
                return _FirstAidBag;
            }
        }
        
        public RescueTekniq.BOL.Vare Vare
        {
            get
            {
                try
                {
                    if (_ServiceType > 0)
                    {
                        if (!_Vare.loaded)
                        {
                            _Vare = RescueTekniq.BOL.Vare.GetVare(_ServiceType);
                        }
                        else if (_Vare.ID != _ServiceType)
                        {
                            _Vare = RescueTekniq.BOL.Vare.GetVare(_ServiceType);
                        }
                    }
                }
                catch (Exception)
                {
                }
                return _Vare;
            }
        }
        
        public RescueTekniq.BOL.KundeGrp_Pris KundeGrpPris
        {
            get
            {
                try
                {
                    //KundeGrp_Pris.GetCompany_VarePris(item.FirstAidBag.CompanyID, item.Vare.ID)
                    if (FirstAidBag.loaded && Vare.loaded)
                    {
                        if (!_kgp.loaded)
                        {
                            _kgp = RescueTekniq.BOL.KundeGrp_Pris.GetCompany_VarePris(FirstAidBag.CompanyID, Vare.ID);
                        }
                    }
                }
                catch (Exception)
                {
                }
                return _kgp;
            }
        }
        
#endregion
        
#region  Shared Populate
        
        private static void AddParms(ref DBAccess db, FirstAidBag_Service c)
        {
            var with_1 = c;
            db.AddInt("Status", (System.Int32) with_1.Status);
            db.AddInt("FAB_FK", with_1.FAB_FK);
            
            db.AddInt("ServiceType", with_1.ServiceType);
            db.AddBoolean("ServiceActive", with_1.ServiceActive);
            db.AddDateTime("ServiceDueDate", with_1.ServiceDueDate);
            db.AddDateTime("ServiceVisitedDate", with_1.ServiceVisitedDate);
            db.AddInt("ServiceConsultantID", with_1.ServiceConsultantID);
            db.AddNVarChar("ServiceConsultant", with_1.ServiceConsultant, 50);
            db.AddMoney("ServicePrice", with_1.ServicePrice);
            //
            AddParmsStandard(db, c);
        }
        
        protected static void Populate(SqlDataReader dr, FirstAidBag_Service c)
        {
            PopulateStandard(dr, c);
            var with_1 = c;
            with_1.Status = (RescueTekniq.BOL.FAB_ServiceStatusType) (dr.DBtoInt("Status"));
            with_1.FAB_FK = System.Convert.ToInt32(dr.DBtoInt("FAB_FK"));
            with_1.ServiceType = System.Convert.ToInt32(dr.DBtoInt("ServiceType"));
            with_1.ServiceActive = System.Convert.ToBoolean(dr.DBtoBoolean("ServiceActive"));
            with_1.ServiceDueDate = System.Convert.ToDateTime(dr.DBtoDate("ServiceDueDate"));
            with_1.ServiceVisitedDate = System.Convert.ToDateTime(dr.DBtoDate("ServiceVisitedDate"));
            with_1.ServiceConsultantID = System.Convert.ToInt32(dr.DBtoInt("ServiceConsultantID"));
            with_1.ServiceConsultant = dr.DBtoString("ServiceConsultant");
            with_1.ServicePrice = System.Convert.ToDouble(dr.DBtoDouble("ServicePrice"));
        }
        
#endregion
        
#region  Stored Procedure Names
        
        private const string _SQLDelete = "Co2Db_FirstAidBag_Service_Delete";
        private const string _SQLInsert = "Co2Db_FirstAidBag_Service_Insert";
        private const string _SQLUpdate = "Co2Db_FirstAidBag_Service_Update";
        
        private const string _SQLSelectAll = "Co2Db_FirstAidBag_Service_SelectAll";
        private const string _SQLSelectID = "Co2Db_FirstAidBag_Service_SelectID";
        private const string _SQLSelectOne = "Co2Db_FirstAidBag_Service_SelectOne";
        private const string _SQLSelectBySearch = "Co2Db_FirstAidBag_Service_SelectBySearch";
        private const string _SQLSelectByCompany = "Co2Db_FirstAidBag_Service_SelectByCompany";
        private const string _SQLSelectByAED = "Co2Db_FirstAidBag_Service_SelectByFAB";
        private const string _SQLSelectByGuid = "Co2Db_FirstAidBag_Service_SelectByGuid";
        
        private const string _SQLSelectAllSoonExpired = "Co2Db_FirstAidBag_Service_SelectAllSoonExpired";
        private const string _SQLSelectAllExpiredEmail = "Co2Db_FirstAidBag_Service_SelectAllExpiredEmail";
        
#endregion
        
#region  Metoder
        
#region  Manipulate data
        
        public int Delete()
        {
            return Delete(System.Convert.ToInt32(this));
        }
        public static int Delete(int ID)
        {
            FirstAidBag_Service S = new FirstAidBag_Service();
            S.ID = ID;
            return Delete(S);
        }
        public static int Delete(FirstAidBag_Service rec)
        {
            DBAccess db = new DBAccess();
            db.AddInt("ID", rec.ID);
            int retval = db.ExecuteNonQuery(_SQLDelete);
            AddLog(Status: "FirstAidBag_Service", Logtext: string.Format("Delete FirstAidBag_Service: ID:{0}", rec.ID), Metode: "Delete");
            return retval;
        }
        
        public int Insert()
        {
            return Insert(this);
        }
        public static int Insert(FirstAidBag_Service rec)
        {
            DBAccess db = new DBAccess();
            AddParms(ref db, rec);
            
            SqlParameter objParam = new SqlParameter("@ID", 0);
            objParam.Direction = ParameterDirection.Output;
            db.Parameters.Add(objParam);
            
            int retval = db.ExecuteNonQuery(_SQLInsert);
            if (retval == 1)
            {
                rec.ID = int.Parse(objParam.Value.ToString());
                AddLog(Status: "FirstAidBag_Service", Logtext: string.Format("Create FirstAidBag_Service: ID:{0}", rec.ID), Metode: "Insert");
                return rec.ID;
            }
            else
            {
                AddLog(Status: "FirstAidBag_Service", Logtext: string.Format("Failure to Create FirstAidBag_Service:"), logtype: LogTypeEnum.Error, Metode: "Insert");
                return -1;
            }
        }
        
        public int Update()
        {
            return Update(this);
        }
        public static int Update(FirstAidBag_Service rec)
        {
            DBAccess db = new DBAccess();
            
            db.AddInt("ID", rec.ID);
            AddParms(ref db, rec);
            
            int retval = db.ExecuteNonQuery(_SQLUpdate);
            AddLog(Status: "FirstAidBag_Service", Logtext: string.Format("Update FirstAidBag_Service: ID:{0}", rec.ID), Metode: "Update");
            return retval;
        }
        
        public int Save()
        {
            return Save(this);
        }
        public static int Save(FirstAidBag_Service rec)
        {
            int retval = 0;
            
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
        
#endregion
        
#region  Get data
        
        public static FirstAidBag_Service GetServiceByID(int id)
        {
            return GetServiceByCriteria("", "[ID]=@ID", new SqlParameter("@ID", id));
        }
        public static FirstAidBag_Service GetServiceByCriteria(string OrderBY, string criteria, params SqlParameter[] @params)
        {
            List<FirstAidBag_Service> list = GetServicesByCriteria(OrderBY, criteria, @params);
            if (list.Count > 0)
            {
                return list[0];
            }
            else
            {
                return new FirstAidBag_Service(); //Nothing
            }
        }
        public static List<FirstAidBag_Service> GetServicesByCriteria(string OrderBY, string criteria, params SqlParameter[] @params)
        {
            DBAccess db = new DBAccess();
            //Dim conn As SqlConnection = DataFunctions.GetConnection()
            string query = "";
            query += "SELECT ";
            if (OrderBY != "")
            {
                query += " TOP (100) PERCENT ";
            }
            query += " * FROM vw_Co2Db_FirstAidBag_Service ";
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
            
            List<FirstAidBag_Service> list = new List<FirstAidBag_Service>();
            while (dr.Read())
            {
                FirstAidBag_Service service = new FirstAidBag_Service();
                FirstAidBag_Service.Populate(dr, service);
                list.Add(service);
            }
            
            db.Dispose();
            
            return list;
        }
        public static DataSet GetServicesByCriteriaDS(string fieldnames, string criteria, params SqlParameter[] @params)
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
            query += " FROM vw_Co2Db_FirstAidBag_Service ";
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
        
        public static FirstAidBag_Service GetService(int ID)
        {
            FirstAidBag_Service c = new FirstAidBag_Service();
            DBAccess db = new DBAccess();
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
        public static FirstAidBag_Service GetService(System.Guid Guid)
        {
            FirstAidBag_Service c = new FirstAidBag_Service();
            DBAccess db = new DBAccess();
            db.AddGuid("Guid", Guid);
            
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
        
        public static DataSet GetServiceDS(int ID)
        {
            DBAccess db = new DBAccess();
            db.AddInt("ID", ID);
            
            db.AddBoolean("IsAgent", AdgangsKontrol.IsAgent());
            db.AddGuid("AgentID", CurrentUserID);
            
            DataSet ds = db.ExecuteDataSet(_SQLSelectID);
            return ds;
        }
        public static DataSet GetServiceDS(System.Guid Guid)
        {
            DBAccess db = new DBAccess();
            db.AddGuid("Guid", Guid);
            
            db.AddBoolean("IsAgent", AdgangsKontrol.IsAgent());
            db.AddGuid("AgentID", CurrentUserID);
            
            DataSet ds = db.ExecuteDataSet(_SQLSelectByGuid);
            return ds;
        }
        
        public static System.Collections.Generic.List<FirstAidBag_Service> GetServiceList(int FAB_FK)
        {
            System.Collections.Generic.List<FirstAidBag_Service> result = new System.Collections.Generic.List<FirstAidBag_Service>();
            int ID = -1;
            DBAccess db = new DBAccess();
            db.AddInt("FAB_FK", FAB_FK);
            
            db.AddBoolean("IsAgent", AdgangsKontrol.IsAgent());
            db.AddGuid("AgentID", CurrentUserID);
            
            SqlDataReader dr = default(SqlDataReader);
            try
            {
                dr = (System.Data.SqlClient.SqlDataReader) (db.ExecuteReader(_SQLSelectByAED));
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ID = System.Convert.ToInt32(dr.DBtoInt("ID"));
                        result.Add(FirstAidBag_Service.GetService(ID));
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
        
        public static DataSet GetAllService(int FAB_FK)
        {
            DBAccess db = new DBAccess();
            
            db.AddBoolean("IsAgent", AdgangsKontrol.IsAgent());
            db.AddGuid("AgentID", CurrentUserID);
            
            DataSet ds = db.ExecuteDataSet(_SQLSelectAll);
            return ds;
        }
        
        public static DataSet GetServiceByAED(int FAB_FK)
        {
            DBAccess db = new DBAccess();
            db.AddInt("FAB_FK", FAB_FK);
            
            db.AddBoolean("IsAgent", AdgangsKontrol.IsAgent());
            db.AddGuid("AgentID", CurrentUserID);
            
            DataSet ds = db.ExecuteDataSet(_SQLSelectByAED);
            return ds;
        }
        public static List<FirstAidBag_Service> GetServicesByFAB(int FAB_FK)
        {
            List<FirstAidBag_Service> result = new List<FirstAidBag_Service>();
            int ID = -1;
            DBAccess db = new DBAccess();
            db.AddInt("FAB_FK", FAB_FK);
            
            db.AddBoolean("IsAgent", AdgangsKontrol.IsAgent());
            db.AddGuid("AgentID", CurrentUserID);
            
            SqlDataReader dr = default(SqlDataReader);
            try
            {
                dr = (System.Data.SqlClient.SqlDataReader) (db.ExecuteReader(_SQLSelectByAED));
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ID = System.Convert.ToInt32(dr.DBtoInt("ID"));
                        result.Add(FirstAidBag_Service.GetService(ID));
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
        
        public static int GetAllServiceByCompanyCount(int CompanyID)
        {
            int res = 0;
            DataSet ds = default(DataSet);
            ds = GetAllServiceByCompany(CompanyID);
            try
            {
                res = ds.Tables[0].Rows.Count;
            }
            catch (Exception)
            {
            }
            
            return res;
        }
        public static DataSet GetAllServiceByCompany(int CompanyID)
        {
            DBAccess db = new DBAccess();
            db.AddInt("CompanyID", CompanyID);
            
            db.AddBoolean("IsAgent", AdgangsKontrol.IsAgent());
            db.AddGuid("AgentID", CurrentUserID);
            
            DataSet ds = db.ExecuteDataSet(_SQLSelectByCompany);
            return ds;
        }
        public static List<FirstAidBag_Service> GetAllServicesByCompany(int CompanyID)
        {
            List<FirstAidBag_Service> result = new List<FirstAidBag_Service>();
            int ID = -1;
            DBAccess db = new DBAccess();
            db.AddInt("CompanyID", CompanyID);
            
            db.AddBoolean("IsAgent", AdgangsKontrol.IsAgent());
            db.AddGuid("AgentID", CurrentUserID);
            
            SqlDataReader dr = default(SqlDataReader);
            try
            {
                dr = (System.Data.SqlClient.SqlDataReader) (db.ExecuteReader(_SQLSelectByCompany));
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ID = System.Convert.ToInt32(dr.DBtoInt("ID"));
                        result.Add(FirstAidBag_Service.GetService(ID));
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
        
        public static DataSet GetServiceBySoonExpired(int CompanyID)
        {
            DBAccess db = new DBAccess();
            db.AddInt("CompanyID", CompanyID);
            
            db.AddBoolean("IsAgent", AdgangsKontrol.IsAgent());
            db.AddGuid("AgentID", CurrentUserID);
            
            DataSet ds = db.ExecuteDataSet(_SQLSelectAllSoonExpired);
            return ds;
        }
        
        public static System.Collections.Generic.List<FirstAidBag_Service> GetServiceListSoonExpired(int CompanyID)
        {
            System.Collections.Generic.List<FirstAidBag_Service> result = new System.Collections.Generic.List<FirstAidBag_Service>();
            int ID = -1;
            DBAccess db = new DBAccess();
            db.AddInt("CompanyID", CompanyID);
            
            db.AddBoolean("IsAgent", AdgangsKontrol.IsAgent());
            db.AddGuid("AgentID", CurrentUserID);
            
            SqlDataReader dr = default(SqlDataReader);
            try
            {
                dr = (System.Data.SqlClient.SqlDataReader) (db.ExecuteReader(_SQLSelectAllSoonExpired));
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ID = System.Convert.ToInt32(dr.DBtoInt("ID"));
                        result.Add(FirstAidBag_Service.GetService(ID));
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
        
        public static System.Collections.Generic.List<FirstAidBag_Service> GetBatteryListExpiredEmail(int CompanyID)
        {
            System.Collections.Generic.List<FirstAidBag_Service> result = new System.Collections.Generic.List<FirstAidBag_Service>();
            int ID = -1;
            DBAccess db = new DBAccess();
            db.AddInt("CompanyID", CompanyID);
            
            db.AddBoolean("IsAgent", AdgangsKontrol.IsAgent());
            db.AddGuid("AgentID", CurrentUserID);
            
            SqlDataReader dr = default(SqlDataReader);
            try
            {
                dr = (System.Data.SqlClient.SqlDataReader) (db.ExecuteReader(_SQLSelectAllExpiredEmail));
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ID = System.Convert.ToInt32(dr.DBtoInt("ID"));
                        result.Add(FirstAidBag_Service.GetService(ID));
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
        
        public static DataSet Search_Service(string Search)
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
        
#endregion
        
#endregion
        
#region  Tags
        
        public string Tags(string tekst)
        {
            return Tags(tekst, this);
        }
        public static string Tags(string tekst, RescueTekniq.BOL.FirstAidBag_Service item)
        {
            string res = "";
            StringBuilder sb = new StringBuilder();
            sb.Append(tekst);
            sb.Replace("[VARE.TYPE]", "Service");
            sb.Replace("[SERVICE.TYPE]", "[VARE.NAVN]");
            
            sb.Replace("[DUEDATE]", item.ServiceDueDate.ToString("MMMM yyyy"));
            sb.Replace("[SERVICE.DUEDATE]", item.ServiceDueDate.ToString("MMMM yyyy"));
            sb.Replace("[SERVICE.BESØGSDATO]", item.ServiceDueDate.ToString("MMMM yyyy"));
            
            sb.Replace("[SERVICE.VISITEDDATE]", item.ServiceVisitedDate.ToString("dd MMMM yyyy"));
            sb.Replace("[SERVICE.BESØGTDATO]", item.ServiceVisitedDate.ToString("dd MMMM yyyy"));
            
            //.Replace("[SERVICE.EMAILSENDT]", item.ServiceEmailSendt.ToString())
            sb.Replace("[SERVICE.GUID]", item.Guid.ToString());
            
            sb.Replace("[SERVICE.VARENR]", "[VARE.VARENR]");
            sb.Replace("[SERVICE.SALGSPRIS]", "[VARE.SALGSPRIS]");
            sb.Replace("[SERVICE.RABAT]", "[VARE.RABAT]");
            sb.Replace("[SERVICE.PRIS]", "[VARE.PRIS]");
            sb.Replace("[SERVICE.KUNDEPRIS]", "[VARE.KUNDEPRIS]");
            sb.Replace("[SERVICE.FRAGTGEBYR]", "[VARE.FRAGTGEBYR]");
            sb.Replace("[SERVICE.FRAGTPRIS]", "[VARE.FRAGTPRIS]");
            sb.Replace("[SERVICE.MOMS]", "[VARE.MOMS]");
            sb.Replace("[SERVICE.TOTAL]", "[VARE.TOTAL]");
            
            res = sb.ToString();
            res = item.KundeGrpPris.Tags(res);
            res = item.FirstAidBag.Tags(res);
            
            return res;
        }
        
#endregion
        
    }
    
    
}
