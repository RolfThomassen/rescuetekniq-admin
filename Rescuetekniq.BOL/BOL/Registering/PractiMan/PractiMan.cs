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
    
    public enum PractiManStatusType
    {
        All = -3,
        Deleted = -2,
        Initialize, //-1
        Create, //0
        Aktiv, //1
        Expired
    }
    
    public class PractiMan : BaseObject
    {
        
#region  New
        
        public PractiMan()
        {
            // VBConversions Note: Non-static class variable initialization is below.  Class variables cannot be initially assigned non-static values in C#.
            _SalesDate = DateTime.Today;
            
            
        }
        public PractiMan(int ID)
        {
            // VBConversions Note: Non-static class variable initialization is below.  Class variables cannot be initially assigned non-static values in C#.
            _SalesDate = DateTime.Today;
            
            this.ID = ID;
            
            if (ID > 0)
            {
                DBAccess db = new DBAccess();
                db.AddInt("ID", ID);
                SqlDataReader dr = (SqlDataReader) (db.ExecuteReader(_SQLSelectID));
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
        
        //Når man åbner fanen Practiman – skal vi kunne registrere vore Practi-man dukker på følgende måde:
        //Opret practiman:
        //
        //Salgsdato	: DDMMÅÅÅÅ
        //Fakturanummer: (fritekstfelt tal)
        //Serienr.: (fritekstfelt tal)
        //Derefter skal man kunne taste gem
        //
        //Således at vi kan notere vore Practiman på næsten samme måde som vi kan notere aed trænere
        
        private RescueTekniq.BOL.PractiManStatusType _Status = PractiManStatusType.Aktiv;
        private int _CompanyID = -1;
        private int _ModelID = -1;
        private string _SerialNo = ""; //(50)
        private int _InvoiceID = 0;
        private DateTime _SalesDate; // VBConversions Note: Initial value cannot be assigned here since it is non-static.  Assignment has been moved to the class constructors.
        
        private RescueTekniq.BOL.Virksomhed _virksomhed = new RescueTekniq.BOL.Virksomhed();
        private RescueTekniq.BOL.Vare _vare = new RescueTekniq.BOL.Vare();
        
#endregion
        
#region  Properties
        
        public PractiManStatusType Status
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
                return PractiManStatusType.GetName(_Status.GetType(), _Status);
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
        
        public int InvoiceID
        {
            get
            {
                return _InvoiceID;
            }
            set
            {
                _InvoiceID = value;
            }
        }
        
        public DateTime SalesDate
        {
            get
            {
                return _SalesDate;
            }
            set
            {
                _SalesDate = value;
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
        
#endregion
        
#region  Shared Populate
        
        //UPDATE [vicjos1_RescueTekniq].[vicjos1_sysadm].[Co2Db_PractiMan]
        //   SET [CompanyID] = <CompanyID, int,>
        //      ,[Status] = <Status, int,>
        //      ,[ModelID] = <ModelID, int,>
        //      ,[SerialNo] = <SerialNo, nvarchar(50),>
        //      ,[InvoiceID] = <InvoiceID, int,>
        //      ,[SalesDate] = <SalesDate, datetime,>
        
        //      ,[OprettetAf] = <OprettetAf, nvarchar(50),>
        //      ,[OprettetDen] = <OprettetDen, datetime,>
        //      ,[OprettetIP] = <OprettetIP, nvarchar(15),>
        //      ,[RettetAf] = <RettetAf, nvarchar(50),>
        //      ,[RettetDen] = <RettetDen, datetime,>
        //      ,[RettetIP] = <RettetIP, nvarchar(50),>
        // WHERE <Search Conditions,,>
        //GO
        
        private static void AddParms(ref DBAccess db, PractiMan c)
        {
            var with_1 = c;
            db.AddInt("Status", (System.Int32) with_1.Status);
            db.AddInt("CompanyID", with_1.CompanyID);
            db.AddInt("ModelID", with_1.ModelID);
            db.AddInt("InvoiceID", with_1.InvoiceID);
            db.AddNVarChar("SerialNo", with_1.SerialNo, 50);
            db.AddDateTime("SalesDate", with_1.SalesDate);
            AddParmsStandard(db, c);
        }
        
        protected static void Populate(SqlDataReader dr, PractiMan c)
        {
            PopulateStandard(dr, c);
            var with_1 = c;
            with_1.Status = (RescueTekniq.BOL.PractiManStatusType) (dr.DBtoInt("Status"));
            
            with_1.Status = (RescueTekniq.BOL.PractiManStatusType) (dr.DBtoInt("Status"));
            with_1.CompanyID = System.Convert.ToInt32(dr.DBtoInt("CompanyID"));
            with_1.ModelID = System.Convert.ToInt32(dr.DBtoInt("ModelID"));
            with_1.InvoiceID = System.Convert.ToInt32(dr.DBtoInt("InvoiceID"));
            with_1.SerialNo = dr.DBtoString("SerialNo");
            with_1.SalesDate = System.Convert.ToDateTime(dr.DBtoDate("SalesDate"));
            
        }
        
#endregion
        
#region  Stored Procedure Names
        
        private const string _SQLDelete = "Co2Db_PractiMan_Delete";
        private const string _SQLPurge = "Co2Db_PractiMan_Purge";
        private const string _SQLInsert = "Co2Db_PractiMan_Insert";
        private const string _SQLUpdate = "Co2Db_PractiMan_Update";
        
        private const string _SQLSelectAll = "Co2Db_PractiMan_SelectAll";
        private const string _SQLSelectID = "Co2Db_PractiMan_SelectID";
        private const string _SQLSelectCompanyID = "Co2Db_PractiMan_SelectCompanyID";
        private const string _SQLSelectModelID = "Co2Db_PractiMan_SelectModelID";
        
#endregion
        
#region  Metoder
        
#region  Manipulate data
        
        public int Delete()
        {
            return Delete(this);
        }
        public static int Delete(PractiMan rec)
        {
            return Delete(rec.ID);
        }
        public static int Delete(int ID)
        {
            DBAccess db = new DBAccess();
            db.AddInt("ID", ID);
            int retval = db.ExecuteNonQuery(_SQLDelete);
            AddLog(Status: "PractiMan", Logtext: string.Format("Delete PractiMan: ID:{0}", ID), Metode: "Delete");
            return retval;
        }
        
        public int Insert()
        {
            return Insert(this);
        }
        public static int Insert(PractiMan rec)
        {
            DBAccess db = new DBAccess();
            AddParms(ref db, rec);
            
            SqlParameter objParam = new SqlParameter("@ID", 0);
            objParam.Direction = ParameterDirection.Output;
            db.Parameters.Add(objParam);
            
            int retval = db.ExecuteNonQuery(_SQLInsert);
            if (retval == 1)
            {
                rec.ID = Convert.ToInt32(objParam.Value);
                AddLog(Status: "PractiMan", Logtext: string.Format("Create PractiMan: ID:{0}", rec.ID), Metode: "Insert");
                return rec.ID;
            }
            else
            {
                AddLog(Status: "PractiMan", Logtext: string.Format("Failure to Create PractiMan:"), logtype: LogTypeEnum.Error, Metode: "Insert");
                return -1;
            }
        }
        
        public int Update()
        {
            return Update(this);
        }
        public static int Update(PractiMan rec)
        {
            DBAccess db = new DBAccess();
            
            db.AddInt("ID", rec.ID);
            AddParms(ref db, rec);
            
            int retval = db.ExecuteNonQuery(_SQLUpdate);
            AddLog(Status: "PractiMan", Logtext: string.Format("Update PractiMan: ID:{0}", rec.ID), Metode: "Update");
            return retval;
        }
        
        public int Save()
        {
            return Save(this);
        }
        public static int Save(PractiMan rec)
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
        
        //UPDATE [vicjos1_RescueTekniq].[vicjos1_sysadm].[Co2Db_PractiMan]
        //   SET [CompanyID] = <CompanyID, int,>
        //      ,[Status] = <Status, int,>
        //      ,[ModelID] = <ModelID, int,>
        //      ,[SerialNo] = <SerialNo, nvarchar(50),>
        //      ,[InvoiceID] = <InvoiceID, int,>
        //      ,[SalesDate] = <SalesDate, datetime,>
        
        public static PractiMan GetPractiManByID(int id)
        {
            return GetPractiManByCriteria("", "[ID]=@ID", new SqlParameter("@ID", id));
        }
        public static PractiMan GetPractiManByCriteria(string OrderBY, string criteria, params SqlParameter[] @params)
        {
            List<PractiMan> list = GetPractiMansByCriteria(OrderBY, criteria, @params);
            if (list.Count > 0)
            {
                return list[0];
            }
            else
            {
                return new PractiMan();
            }
        }
        public static List<PractiMan> GetPractiMansByCriteria(string OrderBY, string criteria, params SqlParameter[] @params)
        {
            DBAccess db = new DBAccess();
            //Dim conn As SqlConnection = DataFunctions.GetConnection()
            string query = "";
            query += "SELECT ";
            if (OrderBY != "")
            {
                query += " TOP (100) PERCENT ";
            }
            query += " * FROM vw_Co2Db_PractiMan ";
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
            SqlDataReader dr = (SqlDataReader) (db.ExecuteReader());
            
            List<PractiMan> list = new List<PractiMan>();
            while (dr.Read())
            {
                PractiMan pc = new PractiMan();
                PractiMan.Populate(dr, pc);
                list.Add(pc);
            }
            
            db.Dispose();
            
            return list;
        }
        public static DataSet GetPractiMansByCriteriaDS(string fieldnames, string criteria, params SqlParameter[] @params)
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
            query += " FROM vw_Co2Db_PractiMan ";
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
            
            DataSet ds = db.ExecuteDataSet();
            return ds;
        }
        
        public static PractiMan GetPractiMan(int ID)
        {
            PractiMan c = new PractiMan();
            DBAccess db = new DBAccess();
            db.AddInt("ID", ID);
            
            SqlDataReader dr = (SqlDataReader) (db.ExecuteReader(_SQLSelectID));
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
        
        public static DataSet GetPractiManDS(int ID)
        {
            DBAccess db = new DBAccess();
            db.AddInt("ID", ID);
            DataSet ds = db.ExecuteDataSet(_SQLSelectID);
            return ds;
        }
        
        public static System.Collections.Generic.List<PractiMan> GetPractiManList()
        {
            System.Collections.Generic.List<PractiMan> result = new System.Collections.Generic.List<PractiMan>();
            int ID = -1;
            DBAccess db = new DBAccess();
            
            db.AddBoolean("IsAgent", AdgangsKontrol.IsAgent());
            db.AddGuid("AgentID", CurrentUserID);
            
            SqlDataReader dr = default(SqlDataReader);
            try
            {
                dr = (System.Data.SqlClient.SqlDataReader) (db.ExecuteReader(_SQLSelectAll));
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        PractiMan pc = new PractiMan();
                        Populate(dr, pc);
                        result.Add(pc);
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
        
        public static DataSet GetPractiManDS()
        {
            DBAccess db = new DBAccess();
            
            db.AddBoolean("IsAgent", AdgangsKontrol.IsAgent());
            db.AddGuid("AgentID", CurrentUserID);
            
            DataSet ds = db.ExecuteDataSet(_SQLSelectAll);
            return ds;
        }
        
        public static PractiMan GetPractiManByModelID(int ModelID)
        {
            DBAccess db = new DBAccess();
            PractiMan pc = new PractiMan();
            db.AddInt("ModelID", ModelID);
            db.AddBoolean("IsAgent", AdgangsKontrol.IsAgent());
            db.AddGuid("AgentID", CurrentUserID);
            
            SqlDataReader dr = (SqlDataReader) (db.ExecuteReader(_SQLSelectModelID));
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Populate(dr, pc);
                }
            }
            if (!dr.IsClosed)
            {
                dr.Close();
            }
            return pc;
        }
        public static PractiMan GetPractiManByCompanyID(int CompanyID)
        {
            DBAccess db = new DBAccess();
            PractiMan pc = new PractiMan();
            db.AddInt("CompanyID", CompanyID);
            db.AddBoolean("IsAgent", AdgangsKontrol.IsAgent());
            db.AddGuid("AgentID", CurrentUserID);
            
            SqlDataReader dr = (SqlDataReader) (db.ExecuteReader(_SQLSelectCompanyID));
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Populate(dr, pc);
                }
            }
            if (!dr.IsClosed)
            {
                dr.Close();
            }
            return pc;
        }
        
        public static DataSet GetPractiManByCompanyDS(int CompanyID)
        {
            DBAccess db = new DBAccess();
            
            db.AddInt("CompanyID", CompanyID);
            db.AddBoolean("IsAgent", AdgangsKontrol.IsAgent());
            db.AddGuid("AgentID", CurrentUserID);
            
            DataSet ds = db.ExecuteDataSet(_SQLSelectCompanyID);
            return ds;
        }
        
#endregion
        
#endregion
        
#region  Tags
        
        public string Tags(string tekst)
        {
            return Tags(tekst, this);
        }
        public static string Tags(string tekst, RescueTekniq.BOL.PractiMan item)
        {
            string res = "";
            StringBuilder sb = new StringBuilder();
            //UPDATE [vicjos1_RescueTekniq].[vicjos1_sysadm].[Co2Db_PractiMan]
            //   SET [CompanyID] = <CompanyID, int,>
            //      ,[Status] = <Status, int,>
            //      ,[ModelID] = <ModelID, int,>
            //      ,[SerialNo] = <SerialNo, nvarchar(50),>
            //      ,[InvoiceID] = <InvoiceID, int,>
            //      ,[SalesDate] = <SalesDate, datetime,>
            
            // WHERE <Search Conditions,,>
            //GO
            
            sb.Append(tekst);
            //.Replace("[VARE.TYPE]", "PractiMan")
            //.Replace("[PractiMan.TYPE]", "[VARE.NAVN]")
            
            sb.Replace("[PractiMan.SalesDate]", item.SalesDate.ToString("dd. MMM yyyy"));
            sb.Replace("[PractiMan.Sales.DATE]", item.SalesDate.ToString("dd. MMM yyyy"));
            
            sb.Replace("[PractiMan.InvoiceID]", System.Convert.ToString(item.InvoiceID));
            sb.Replace("[PractiMan.ModelID]", System.Convert.ToString(item.ModelID));
            sb.Replace("[PractiMan.SerialNo]", item.SerialNo);
            
            sb.Replace("[PractiMan.GUID]", item.Guid.ToString());
            
            res = sb.ToString();
            
            return res;
        }
        
#endregion
        
    }
    
    
}
