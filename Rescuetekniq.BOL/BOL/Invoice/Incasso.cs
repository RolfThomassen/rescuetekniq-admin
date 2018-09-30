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
    
    [Serializable()]public class IncassoSag : BaseObject
    {
        //Implements tags
        
#region  Privates
        private RescueTekniq.BOL.Incasso_StatusEnum _Status = Incasso_StatusEnum.Initialize;
        
        private int _CompanyID = 0;
        private int _InvoiceID = 0;
        private DateTime _IncassoDate; // VBConversions Note: Initial value cannot be assigned here since it is non-static.  Assignment has been moved to the class constructors.
        private string _Notes = "";
        
        private string _ContactEmail = "";
        private string _ContactName = "";
        
        private double _InvoiceTotal = 0;
        private double _RenterOpgjortAfKreditor = 0;
        private double _GebyrOpgjortAfKreditor = 0;
        private double _RenteTilskrevetFraPaymentDatetilDD = 0;
        private double _GebyrForDenneRykkerskrivelse = 0;
        private double _IncassoTotal = 0;
        
        private double _IncassoSalaer = 1450;
        private double _Oversendelsesgebyr = 100;
        
        private bool _Paid = false;
        private DateTime _PaidDate;
        
        [NonSerialized]private Virksomhed _Virksomhed = new Virksomhed();
        [NonSerialized]private InvoiceHeader _Invoice = new InvoiceHeader();
        
#endregion
        
#region  New
        
        public IncassoSag()
        {
            // VBConversions Note: Non-static class variable initialization is below.  Class variables cannot be initially assigned non-static values in C#.
            _IncassoDate = DateTime.Now;
            
        }
        
        public IncassoSag(int ID)
        {
            // VBConversions Note: Non-static class variable initialization is below.  Class variables cannot be initially assigned non-static values in C#.
            _IncassoDate = DateTime.Now;
            
            this.ID = ID;
            if (ID > 0)
            {
                DBAccess db = new DBAccess();
                db.Parameters.Add(new SqlParameter("@ID", ID));
                SqlDataReader dr = (SqlDataReader) (db.ExecuteReader(_SQLSelectID));
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Populate(dr, this);
                    }
                }
                if (!dr.IsClosed)
                {
                    dr.Close();
                }
            }
        }
        
#endregion
        
#region  Properties
        
        public string ContactName
        {
            get
            {
                return _ContactName;
            }
            set
            {
                _ContactName = value;
            }
        }
        
        public string ContactEmail
        {
            get
            {
                return _ContactEmail;
            }
            set
            {
                _ContactEmail = value;
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
        
        public Incasso_StatusEnum Status
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
                return Incasso_StatusEnum.GetName(_Status.GetType(), _Status);
            }
        }
        
        public double InvoiceTotal
        {
            get
            {
                _InvoiceTotal = (double) Invoice.InvoiceTotalInclVAT;
                return _InvoiceTotal;
            }
        }
        
        public double RenterOpgjortAfKreditor
        {
            get
            {
                return _RenterOpgjortAfKreditor;
            }
            set
            {
                _RenterOpgjortAfKreditor = value;
            }
        }
        
        public double GebyrOpgjortAfKreditor
        {
            get
            {
                return _GebyrOpgjortAfKreditor;
            }
            set
            {
                _GebyrOpgjortAfKreditor = value;
            }
        }
        
        public double RenteTilskrevetFraPaymentDatetilDD
        {
            get
            {
                return _RenteTilskrevetFraPaymentDatetilDD;
            }
            set
            {
                _RenteTilskrevetFraPaymentDatetilDD = value;
            }
        }
        
        public double GebyrForDenneRykkerskrivelse
        {
            get
            {
                return _GebyrForDenneRykkerskrivelse;
            }
            set
            {
                _GebyrForDenneRykkerskrivelse = value;
            }
        }
        
        public double IncassoTotal
        {
            get
            {
                _IncassoTotal = 0;
                _IncassoTotal += InvoiceTotal;
                _IncassoTotal += RenterOpgjortAfKreditor;
                _IncassoTotal += GebyrOpgjortAfKreditor;
                _IncassoTotal += RenteTilskrevetFraPaymentDatetilDD;
                _IncassoTotal += GebyrForDenneRykkerskrivelse;
                
                return _IncassoTotal;
            }
        }
        
        public double IncassoSalaer
        {
            get
            {
                return _IncassoSalaer;
            }
            set
            {
                _IncassoSalaer = value;
            }
        }
        
        public double Oversendelsesgebyr
        {
            get
            {
                return _Oversendelsesgebyr;
            }
            set
            {
                _Oversendelsesgebyr = value;
            }
        }
        
        public string Notes
        {
            get
            {
                return _Notes;
            }
            set
            {
                _Notes = value;
            }
        }
        
        public DateTime IncassoDate
        {
            get
            {
                return _IncassoDate;
            }
            set
            {
                _IncassoDate = value;
            }
        }
        
        public bool Paid
        {
            get
            {
                return _Paid;
            }
            set
            {
                _Paid = value;
            }
        }
        
        public DateTime PaidDate
        {
            get
            {
                return _PaidDate;
            }
            set
            {
                _PaidDate = value;
            }
        }
        
        
        public string Firmanavn
        {
            get
            {
                string res = "";
                try
                {
                    res = Virksomhed.Firmanavn;
                }
                catch
                {
                }
                return res;
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
        
        public RescueTekniq.BOL.InvoiceHeader Invoice
        {
            get
            {
                if (_InvoiceID > 0)
                {
                    if (!_Invoice.loaded)
                    {
                        _Invoice = InvoiceHeader.GetInvoice(_InvoiceID);
                    }
                    else if (_Invoice.ID != _InvoiceID)
                    {
                        _Invoice = InvoiceHeader.GetInvoice(_InvoiceID);
                    }
                }
                if (ReferenceEquals(_Invoice, null))
                {
                    _Invoice = new InvoiceHeader();
                }
                return _Invoice;
            }
        }
        
#endregion
        
#region  Stored Procedure Names
        private const string _SQLPurge = "Co2Db_Incasso_Purge";
        private const string _SQLDelete = "Co2Db_Incasso_Delete";
        private const string _SQLInsert = "Co2Db_Incasso_Insert";
        private const string _SQLUpdate = "Co2Db_Incasso_Update";
        
        private const string _SQLSelectAll = "Co2Db_Incasso_SelectAll";
        private const string _SQLSelectID = "Co2Db_Incasso_SelectID";
        
        private const string _SQLGetTotalpris = "Co2Db_Incasso_GetTotalpris";
        private const string _SQLSelectByStatus = "Co2Db_Incasso_SelectByStatus";
        private const string _SQLSelectByStatusCompany = "Co2Db_Incasso_SelectByStatusCompany";
        private const string _SQLSelectByCompany = "Co2Db_Incasso_SelectByCompany";
        private const string _SQLSelectByCompanyCount = "Co2Db_Incasso_GetCompanyInvoiceCount";
        
        private const string _SQLSetStatus = "Co2Db_Incasso_SetStatus";
        private const string _SQLSetPaid = "Co2Db_Incasso_SetPaid";
        
        private const string _SQLSelectByInvoiceID = "Co2Db_Incasso_SelectByInvoiceID";
        
#endregion
        
#region  Shared Populate
        
        private static void AddParms(ref DBAccess db, IncassoSag i)
        {
            var with_1 = i;
            db.AddNVarChar("ContactEmail", with_1.ContactEmail, 250);
            db.AddNVarChar("ContactName", with_1.ContactName, 50);
            
            db.AddInt("Status", (System.Int32) with_1.Status);
            db.AddInt("CompanyID", with_1.CompanyID);
            db.AddInt("InvoiceID", with_1.InvoiceID);
            db.AddDateTime("IncassoDate", with_1.IncassoDate);
            db.AddNVarChar("Notes", with_1.Notes, -1);
            
            db.AddFloat("InvoiceTotal", with_1.InvoiceTotal);
            db.AddFloat("RenterOpgjortAfKreditor", with_1.RenterOpgjortAfKreditor);
            db.AddFloat("GebyrOpgjortAfKreditor", with_1.GebyrOpgjortAfKreditor);
            db.AddFloat("RenteTilskrevetFraPaymentDatetilDD", with_1.RenteTilskrevetFraPaymentDatetilDD);
            db.AddFloat("GebyrForDenneRykkerskrivelse", with_1.GebyrForDenneRykkerskrivelse);
            db.AddFloat("IncassoTotal", with_1.IncassoTotal);
            
            db.AddFloat("IncassoSalaer", with_1.IncassoSalaer);
            db.AddFloat("Oversendelsesgebyr", with_1.Oversendelsesgebyr);
            
            db.AddBoolean("Paid", with_1.Paid);
            db.AddDateTime("PaidDate", with_1.PaidDate);
            
            
            AddParmsStandard(db, i);
        }
        
        private static void Populate(SqlDataReader dr, IncassoSag i)
        {
            PopulateStandard(dr, i);
            var with_1 = i;
            with_1.ContactEmail = dr.DBtoString("ContactEmail");
            with_1.ContactName = dr.DBtoString("ContactName");
            
            with_1.Status = (RescueTekniq.BOL.Incasso_StatusEnum) (dr.DBtoInt("Status"));
            with_1.CompanyID = System.Convert.ToInt32(dr.DBtoInt("CompanyID"));
            with_1.InvoiceID = System.Convert.ToInt32(dr.DBtoInt("InvoiceID"));
            with_1.IncassoDate = System.Convert.ToDateTime(dr.DBtoDateTime("IncassoDate"));
            with_1.Notes = dr.DBtoString("Notes");
            
            with_1._InvoiceTotal = System.Convert.ToDouble(dr.DBtoDouble("InvoiceTotal"));
            with_1.RenterOpgjortAfKreditor = System.Convert.ToDouble(dr.DBtoDouble("RenterOpgjortAfKreditor"));
            with_1.GebyrOpgjortAfKreditor = System.Convert.ToDouble(dr.DBtoDouble("GebyrOpgjortAfKreditor"));
            with_1.RenteTilskrevetFraPaymentDatetilDD = System.Convert.ToDouble(dr.DBtoDouble("RenteTilskrevetFraPaymentDatetilDD"));
            with_1.GebyrForDenneRykkerskrivelse = System.Convert.ToDouble(dr.DBtoDouble("GebyrForDenneRykkerskrivelse"));
            with_1._IncassoTotal = System.Convert.ToDouble(dr.DBtoDouble("IncassoTotal"));
            
            with_1.IncassoSalaer = dr.DBtoDouble("IncassoSalaer");
            with_1.Oversendelsesgebyr = System.Convert.ToDouble(dr.DBtoDouble("Oversendelsesgebyr"));
            
            with_1.Paid = System.Convert.ToBoolean(dr.DBtoBoolean("Paid"));
            with_1.PaidDate = dr.DBtoDateTime("PaidDate");
            
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
            AddLog(Status: "Incasso", Logtext: string.Format("Purge Incasso: ID:{0}", ID), Metode: "Purge");
            return retval;
        }
        public static int Purge(IncassoSag c)
        {
            return Purge(c.ID);
        }
        
        public int Delete()
        {
            return Delete(System.Convert.ToInt32(this));
        }
        public static int Delete(int ID)
        {
            IncassoSag IH = new IncassoSag();
            IH.ID = ID;
            return Delete(IH);
        }
        public static int Delete(IncassoSag IH)
        {
            DBAccess db = new DBAccess();
            db.AddInt("ID", IH.ID);
            int retval = db.ExecuteNonQuery(_SQLDelete);
            AddLog(Status: "Incasso", Logtext: string.Format("Delete Incasso: ID:{0}", IH.ID), Metode: "Delete");
            return retval;
        }
        
        public int Insert()
        {
            return Insert(this);
        }
        public static int Insert(IncassoSag c)
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
                AddLog(Status: "Incasso", Logtext: string.Format("Create Incasso: ID:{0}", c.ID), Metode: "Insert");
                return c.ID;
            }
            else
            {
                AddLog(Status: "Incasso", Logtext: string.Format("Failure to Create Incasso:"), logtype: LogTypeEnum.Error, Metode: "Insert");
                return -1;
            }
            
        }
        
        public int Update()
        {
            return Update(this);
        }
        public static int Update(IncassoSag c)
        {
            DBAccess db = new DBAccess();
            
            db.AddInt("ID", c.ID);
            AddParms(ref db, c);
            
            int retval = db.ExecuteNonQuery(_SQLUpdate);
            AddLog(Status: "Incasso", Logtext: string.Format("Update Incasso: ID:{0}", c.ID), Metode: "Update");
            return retval;
        }
        
        public int Save()
        {
            return Save(this);
        }
        public static int Save(IncassoSag i)
        {
            int retval = 0;
            if (i.ID > 0)
            {
                retval = Update(i);
            }
            else
            {
                retval = Insert(i);
            }
            return retval;
        }
        
        public int SetStatus()
        {
            return SetStatus(this);
        }
        public static int SetStatus(IncassoSag c)
        {
            return SetStatus(c.ID, (Invoice_StatusEnum) c.Status);
        }
        public static int UpdateStatus(int id, Invoice_StatusEnum status)
        {
            return SetStatus(id, status);
        }
        public static int SetStatus(int ID, Invoice_StatusEnum Status)
        {
            DBAccess db = new DBAccess();
            
            IncassoSag c = new IncassoSag(ID);
            db.AddInt("ID", ID);
            db.AddInt("Status", System.Convert.ToInt32(Status));
            AddParmsStandard(db, c);
            
            int retval = db.ExecuteNonQuery(_SQLSetStatus);
            AddLog(Status: "Incasso", Logtext: string.Format("SetStatus Incasso: ID:{0} Status:{1}", c.ID, c.StatusText), Metode: "SetStatus");
            return retval;
        }
        
#endregion
        
#region  Get data
        
        public static DataSet GetAllIncasso()
        {
            DBAccess db = new DBAccess();
            //db.Parameters.Add(New SqlParameter("@sUserName", username))
            DataSet ds = db.ExecuteDataSet(_SQLSelectAll);
            return ds;
        }
        public static DataSet GetAllIncasso(Invoice_StatusEnum status)
        {
            DBAccess db = new DBAccess();
            db.AddInt("status", System.Convert.ToInt32(status));
            DataSet ds = db.ExecuteDataSet(_SQLSelectByStatus);
            return ds;
        }
        
        public static DataSet GetIncassoDS(int ID)
        {
            DBAccess db = new DBAccess();
            db.AddInt("ID", ID);
            DataSet ds = db.ExecuteDataSet(_SQLSelectID);
            return ds;
        }
        
        public static IncassoSag GetIncasso(int ID)
        {
            DBAccess db = new DBAccess();
            IncassoSag c = new IncassoSag();
            db.AddInt("ID", ID);
            SqlDataReader dr = (SqlDataReader) (db.ExecuteReader(_SQLSelectID));
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Populate(dr, c);
                }
            }
            if (!dr.IsClosed)
            {
                dr.Close();
            }
            return c;
        }
        
        public static IncassoSag GetIncassoByInvoiceID(int InvoiceID)
        {
            DBAccess db = new DBAccess();
            IncassoSag c = new IncassoSag();
            db.AddInt("InvoiceID", InvoiceID);
            SqlDataReader dr = (SqlDataReader) (db.ExecuteReader(_SQLSelectByInvoiceID));
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Populate(dr, c);
                }
            }
            if (!dr.IsClosed)
            {
                dr.Close();
            }
            return c;
        }
        
        public static DataSet GetIncassoDS(int CompanyID, Invoice_StatusEnum Status)
        {
            DataSet ds = default(DataSet);
            DBAccess db = new DBAccess();
            db.AddInt("CompanyID", CompanyID);
            db.AddInt("Status", System.Convert.ToInt32(Status));
            ds = db.ExecuteDataSet(_SQLSelectByStatusCompany);
            return ds;
        }
        
        public static System.Collections.Generic.List<IncassoSag> GetIncassoList(int CompanyID, Invoice_StatusEnum Status)
        {
            System.Collections.Generic.List<IncassoSag> result = new System.Collections.Generic.List<IncassoSag>();
            DBAccess db = new DBAccess();
            db.AddInt("CompanyID", CompanyID);
            db.AddInt("Status", System.Convert.ToInt32(Status));
            
            SqlDataReader dr = default(SqlDataReader);
            dr = (System.Data.SqlClient.SqlDataReader) (db.ExecuteReader(_SQLSelectByStatusCompany));
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    result.Add(IncassoSag.GetIncasso(System.Convert.ToInt32(dr.DBtoInt("ID"))));
                }
            }
            
            if (!dr.IsClosed)
            {
                dr.Close();
            }
            return result;
        }
        
        public static System.Collections.Generic.List<IncassoSag> GetIncassoByCompany(int CompanyID)
        {
            return GetIncassoList(CompanyID, Invoice_StatusEnum.Active);
        }
        
        public static int GetCompanyIncassoCount(int CompanyID)
        {
            DBAccess db = new DBAccess();
            db.AddInt("CompanyID", CompanyID);
            int res = 0;
            res = Funktioner.ToInt(db.ExecuteScalar(_SQLSelectByCompanyCount), -1);
            return res;
        }
        
        public static DataSet GetRekvireret_Invoice(Invoice_StatusEnum type)
        {
            return Search_Incasso((System.Convert.ToInt32(type)).ToString());
        }
        public static DataSet GetIncassoCompanyStatus(int CompanyID, Invoice_StatusEnum type)
        {
            return Search_Incasso((System.Convert.ToInt32(type)).ToString(), CompanyID);
        }
        public static DataSet Search_Incasso(string skills, int CompanyID = -1)
        {
            DataSet ds = new DataSet();
            DataSet dsTemp = new DataSet();
            bool flag = false;
            DBAccess db = new DBAccess();
            
            string[] arr = skills.Split(' ');
            foreach (string s in arr)
            {
                db.AddInt("Status", Funktioner.ToInt(s));
                
                if (CompanyID != -1)
                {
                    db.AddInt("CompanyID", CompanyID);
                }
                dsTemp = db.ExecuteDataSet(_SQLSelectByStatusCompany);
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
        
        
    }
    
}
