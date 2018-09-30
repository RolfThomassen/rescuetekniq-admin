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
    
    public class PurchaseOrder : BaseObject
    {
        //Implements tags
        
#region  Privates
        //CREATE TABLE [vicjos1_sysadm].[Co2Db_PurchaseOrder](
        //	[ID] [int] IDENTITY(1,1) NOT NULL,
        //	[Status] [int] NULL,
        //	[OrderCreatedBy] [nvarchar](50) NULL,
        //	[PurchaseDate] [datetime] NULL,
        //	[SupplierID] [int] NULL,
        //	[SupplierEmail] [nvarchar](250) NULL,
        //	[PurchaseOrderEmailCopy] [nvarchar](250) NULL,
        //	[PaymentID] [int] NULL,
        //	[FreightID] [int] NULL,
        //	[CurrencyID] [int] NULL,
        //	[FreightPrice] [money] NULL,
        //	[TotalPurchaseOrder] [money] NULL,
        
        private RescueTekniq.BOL.PurchaseOrder_StatusEnum _Status = PurchaseOrder_StatusEnum.Active;
        
        private string _OrderCreatedBy; // VBConversions Note: Initial value cannot be assigned here since it is non-static.  Assignment has been moved to the class constructors.
        private DateTime _PurchaseDate; // VBConversions Note: Initial value cannot be assigned here since it is non-static.  Assignment has been moved to the class constructors.
        private int _SupplierID = -1;
        private string _SupplierEmail = "";
        private string _PurchaseOrderEmailCopy = "";
        
        private int _PaymentID = -1;
        private string _PaymentTerms = "";
        private DateTime _PaymentDate;
        
        private int _FreightID = -1;
        private decimal _FreightPrice = 0;
        private int _CurrencyID = -1;
        
        private decimal _TotalPurchaseOrder = 0;
        
        private string _Notes = "";
        
        private Virksomhed _Supplier = new Virksomhed();
        private System.Collections.Generic.List<PurchaseOrderItem> _PurchaseOrderItems;
        
        private string _ShipToEANno = "";
        private string _ShipToName = "";
        private string _ShipToAddress1 = "";
        private string _ShipToAddress2 = "";
        private string _ShipToZipCode = "";
        private string _ShipToCity = "";
        private string _ShipToState = "";
        private string _ShipToCountry = "";
        private string _ShipToAtt = "";
        private string _ShipToRef = "";
        
        
#endregion
        
#region  New
        
        public PurchaseOrder()
        {
            // VBConversions Note: Non-static class variable initialization is below.  Class variables cannot be initially assigned non-static values in C#.
            _OrderCreatedBy = CurrentUserName;
            _PurchaseDate = DateTime.Now;
            
        }
        
        public PurchaseOrder(int ID)
        {
            // VBConversions Note: Non-static class variable initialization is below.  Class variables cannot be initially assigned non-static values in C#.
            _OrderCreatedBy = CurrentUserName;
            _PurchaseDate = DateTime.Now;
            
            this.ID = ID;
            if (ID > 0)
            {
                DBAccess db = new DBAccess();
                db.Parameters.Add(new SqlParameter("@ID", ID));
                SqlDataReader dr = (SqlDataReader) (db.ExecuteReader(_SQLSelectOne)); //"Co2Db_PurchaseOrder_SelectOne"
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
        //CREATE TABLE [vicjos1_sysadm].[Co2Db_PurchaseOrder](
        //	[ID] [int] IDENTITY(1,1) NOT NULL,
        //	[Status] [int] NULL,
        //	[OrderCreatedBy] [nvarchar](50) NULL,
        //	[PurchaseDate] [datetime] NULL,
        //	[SupplierID] [int] NULL,
        //	[SupplierEmail] [nvarchar](250) NULL,
        //	[PurchaseOrderEmailCopy] [nvarchar](250) NULL,
        //	[PaymentID] [int] NULL,
        //	[FreightID] [int] NULL,
        //	[CurrencyID] [int] NULL,
        //	[FreightPrice] [money] NULL,
        //	[TotalPurchaseOrder] [money] NULL,
        //	[OprettetAF] [nvarchar](50) NULL,
        //	[OprettetDen] [datetime] NULL,
        //	[OprettetIP] [nvarchar](15) NULL,
        //	[RettetAF] [nvarchar](50) NULL,
        //	[RettetDen] [datetime] NULL,
        //	[RettetIP] [nvarchar](15) NULL,
        
        
        public PurchaseOrder_StatusEnum Status
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
                return PurchaseOrder_StatusEnum.GetName(_Status.GetType(), _Status);
            }
        }
        
        public int SupplierID
        {
            get
            {
                return _SupplierID;
            }
            set
            {
                _SupplierID = value;
            }
        }
        
        public string OrderCreatedBy
        {
            get
            {
                return _OrderCreatedBy;
            }
            set
            {
                _OrderCreatedBy = value;
            }
        }
        
        public DateTime PurchaseDate
        {
            get
            {
                return _PurchaseDate;
            }
            set
            {
                _PurchaseDate = value;
            }
        }
        
        public string SupplierEmail
        {
            get
            {
                return _SupplierEmail;
            }
            set
            {
                _SupplierEmail = value;
            }
        }
        public string PurchaseOrderEmailCopy
        {
            get
            {
                return _PurchaseOrderEmailCopy;
            }
            set
            {
                _PurchaseOrderEmailCopy = value;
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
        
        public int PaymentID
        {
            get
            {
                return _PaymentID;
            }
            set
            {
                _PaymentID = value;
            }
        }
        public string PaymentTerms
        {
            get
            {
                Combobox rec = default(Combobox);
                rec = Combobox.GetCombobox("PurchaseOrder.PaymentTerms", _PaymentID);
                string res = "";
                res = rec.Value;
                return res;
                //Return _PaymentTerms
            }
            set
            {
                _PaymentTerms = value;
            }
        }
        public string PaymentText
        {
            get
            {
                Combobox rec = default(Combobox);
                rec = Combobox.GetCombobox("PurchaseOrder.PaymentTerms", _PaymentID);
                string res = "";
                res = rec.Contents;
                return res;
            }
        }
        public DateTime PaymentDate
        {
            get
            {
                return _PaymentDate;
            }
            set
            {
                _PaymentDate = value;
            }
        }
        
        public int FreightID
        {
            get
            {
                return _FreightID;
            }
            set
            {
                _FreightID = value;
            }
        }
        public string FreightTitle
        {
            get
            {
                string res = "";
                Combobox rec = default(Combobox);
                rec = Combobox.GetCombobox("PurchaseOrder.Freight", _FreightID);
                res = rec.Title;
                return res;
            }
        }
        public string FreightText
        {
            get
            {
                string res = "";
                Combobox rec = default(Combobox);
                rec = Combobox.GetCombobox("PurchaseOrder.Freight", _FreightID);
                res = rec.Contents;
                return res;
            }
        }
        
        public int CurrencyID
        {
            get
            {
                return _CurrencyID;
            }
            set
            {
                _CurrencyID = value;
            }
        }
        private Currency _Currency = new Currency();
        public string CurrencyCode
        {
            get
            {
                string cc = "";
                if (_Currency.ID != CurrencyID)
                {
                    _Currency = new Currency(CurrencyID);
                }
                if (_Currency.isLoaded)
                {
                    cc = _Currency.CurrencyCode.ToUpper();
                }
                return cc;
            }
        }
        
        public decimal FreightPrice
        {
            get
            {
                return _FreightPrice;
            }
            set
            {
                _FreightPrice = value;
            }
        }
        
        public decimal TotalPurchaseOrder
        {
            get
            {
                return _TotalPurchaseOrder;
            }
        }
        
        public string Firmanavn
        {
            get
            {
                string res = "";
                try
                {
                    res = Supplier.Firmanavn;
                }
                catch
                {
                }
                return res;
            }
        }
        public RescueTekniq.BOL.Virksomhed Supplier
        {
            get
            {
                if (_SupplierID > 0)
                {
                    if (!_Supplier.loaded)
                    {
                        _Supplier = Virksomhed.GetCompany(_SupplierID);
                    }
                    else if (_Supplier.ID != _SupplierID)
                    {
                        _Supplier = Virksomhed.GetCompany(_SupplierID);
                    }
                }
                return _Supplier;
            }
        }
        
        //PurchaseOrderItem
        private bool _PurchaseOrderItemsLoaded = false;
        public System.Collections.Generic.List<PurchaseOrderItem> PurchaseOrderItems
        {
            get
            {
                if (!_PurchaseOrderItemsLoaded)
                {
                    _PurchaseOrderItems = PurchaseOrderItem.GetPurchaseOrderItemList(ID);
                    _PurchaseOrderItemsLoaded = true;
                }
                return _PurchaseOrderItems;
            }
        }
        
#region  Delvery Address
        
        public string ShipToEANno
        {
            get
            {
                return _ShipToEANno;
            }
            set
            {
                _ShipToEANno = value;
            }
        }
        public string ShipToName
        {
            get
            {
                return _ShipToName;
            }
            set
            {
                _ShipToName = value;
            }
        }
        public string ShipToAddress1
        {
            get
            {
                return _ShipToAddress1;
            }
            set
            {
                _ShipToAddress1 = value;
            }
        }
        public string ShipToAddress2
        {
            get
            {
                return _ShipToAddress2;
            }
            set
            {
                _ShipToAddress2 = value;
            }
        }
        public string ShipToZipCode
        {
            get
            {
                return _ShipToZipCode;
            }
            set
            {
                _ShipToZipCode = value;
            }
        }
        public string ShipToCity
        {
            get
            {
                return _ShipToCity;
            }
            set
            {
                _ShipToCity = value;
            }
        }
        public string ShipToState
        {
            get
            {
                return _ShipToState;
            }
            set
            {
                _ShipToState = value;
            }
        }
        public string ShipToCountry
        {
            get
            {
                return _ShipToCountry;
            }
            set
            {
                _ShipToCountry = value;
            }
        }
        public string ShipToAtt
        {
            get
            {
                return _ShipToAtt;
            }
            set
            {
                _ShipToAtt = value;
            }
        }
        public string ShipToRef
        {
            get
            {
                return _ShipToRef;
            }
            set
            {
                _ShipToRef = value;
            }
        }
        public string ShipToPostnrBy
        {
            get
            {
                string res = "";
                switch (ShipToCountry) // LandekodeID
                {
                    case "":
                    case "Danmark":
                    case "45":
                    case "298":
                    case "299":
                        res = ShipToZipCode + " " + ShipToCity;
                        break;
                    case "USA":
                    case "United Stats Of America":
                    case "1":
                        res = ShipToCity + ", " + ShipToState.ToUpper() + " " + ShipToZipCode;
                        break;
                        //Washington, DC 20546-0001
                    default:
                        res = ShipToZipCode + " " + ShipToCity;
                        break;
                }
                return res;
            }
        }
        
#endregion
        
#endregion
        
#region  Stored Procedure Names
        private const string _SQLPurge = "Co2Db_PurchaseOrder_Purge";
        private const string _SQLDelete = "Co2Db_PurchaseOrder_Delete";
        private const string _SQLInsert = "Co2Db_PurchaseOrder_Insert";
        private const string _SQLUpdate = "Co2Db_PurchaseOrder_Update";
        private const string _SQLSelectAll = "Co2Db_PurchaseOrder_SelectAll";
        private const string _SQLSelectID = "Co2Db_PurchaseOrder_SelectID";
        private const string _SQLSelectOne = "Co2Db_PurchaseOrder_SelectOne";
        private const string _SQLGetTotalpris = "Co2Db_PurchaseOrder_GetTotalPrice"; //[]
        private const string _SQLSelectByStatus = "Co2Db_PurchaseOrder_SelectByStatus";
        private const string _SQLSelectByStatusSupplier = "Co2Db_PurchaseOrder_SelectByStatusSupplier";
        private const string _SQLSelectBySupplier = "Co2Db_PurchaseOrder_SelectBySupplier";
        private const string _SQLSelectBySupplierCount = "Co2Db_PurchaseOrder_GetSupplierInvoiceCount";
        private const string _SQLSelectBySupplierSearch = "Co2Db_PurchaseOrder_Search";
        
        private const string _SQLSetStatus = "Co2Db_PurchaseOrder_SetStatus";
        
#endregion
        
#region  Shared Populate
        
        private static void AddParms(ref DBAccess db, PurchaseOrder rec)
        {
            
            var with_1 = rec;
            db.AddInt("Status", (System.Int32) with_1.Status);
            db.AddNVarChar("OrderCreatedBy", with_1.OrderCreatedBy, 50);
            db.AddDateTime("PurchaseDate", with_1.PurchaseDate);
            
            db.AddInt("SupplierID", with_1.SupplierID);
            db.AddNVarChar("SupplierEmail", with_1.SupplierEmail, 250);
            db.AddNVarChar("PurchaseOrderEmailCopy", with_1.PurchaseOrderEmailCopy, 250);
            
            db.AddInt("PaymentID", with_1.PaymentID);
            db.AddInt("FreightID", with_1.FreightID);
            db.AddInt("CurrencyID", with_1.CurrencyID);
            
            db.AddMoney("FreightPrice", (double) with_1.FreightPrice);
            db.AddMoney("TotalPurchaseOrder", (double) with_1.TotalPurchaseOrder);
            
            db.AddNVarChar("ShipToEANno", with_1.ShipToEANno, 15);
            db.AddNVarChar("ShipToName", with_1.ShipToName, 100);
            db.AddNVarChar("ShipToAddress1", with_1.ShipToAddress1, 100);
            db.AddNVarChar("ShipToAddress2", with_1.ShipToAddress2, 100);
            db.AddNVarChar("ShipToZipCode", with_1.ShipToZipCode, 50);
            db.AddNVarChar("ShipToCity", with_1.ShipToCity, 50);
            db.AddNVarChar("ShipToState", with_1.ShipToState, 50);
            db.AddNVarChar("ShipToCountry", with_1.ShipToCountry, 50);
            db.AddNVarChar("ShipToAtt", with_1.ShipToAtt, 50);
            db.AddNVarChar("ShipToRef", with_1.ShipToRef, 50);
            
            
            AddParmsStandard(db, rec);
        }
        
        private static void Populate(SqlDataReader dr, PurchaseOrder rec)
        {
            PopulateStandard(dr, rec);
            var with_1 = rec;
            
            with_1.Status = (RescueTekniq.BOL.PurchaseOrder_StatusEnum) (dr.DBtoInt("Status")); //	int '
            with_1.OrderCreatedBy = dr.DBtoString("OrderCreatedBy"); //	nvarchar(50) '
            with_1.PurchaseDate = System.Convert.ToDateTime(dr.DBtoDate("PurchaseDate")); //	datetime '
            
            with_1.SupplierID = System.Convert.ToInt32(dr.DBtoInt("SupplierID")); //	int '
            
            with_1.SupplierEmail = dr.DBtoString("SupplierEmail");
            with_1.PurchaseOrderEmailCopy = dr.DBtoString("PurchaseOrderEmailCopy");
            
            with_1.PaymentID = System.Convert.ToInt32(dr.DBtoInt("PaymentID"));
            with_1.FreightID = System.Convert.ToInt32(dr.DBtoInt("FreightID"));
            with_1.CurrencyID = System.Convert.ToInt32(dr.DBtoInt("CurrencyID"));
            
            with_1.FreightPrice = System.Convert.ToDecimal(dr.DBtoDecimal("FreightPrice"));
            with_1._TotalPurchaseOrder = System.Convert.ToDecimal(dr.DBtoDecimal("TotalPurchaseOrder"));
            
            with_1.PaymentID = System.Convert.ToInt32(dr.DBtoInt("PaymentID")); //	int '
            with_1.PaymentTerms = dr.DBtoString("PaymentTerms"); //	nvarchar(50) '
            with_1.PaymentDate = System.Convert.ToDateTime(dr.DBtoDate("PaymentDate")); //	datetime '
            
            with_1.ShipToEANno = dr.DBtoString("ShipToEANno"); //	nvarchar(15) '
            with_1.ShipToName = dr.DBtoString("ShipToName"); //	nvarchar(100) '
            with_1.ShipToAddress1 = dr.DBtoString("ShipToAddress1"); //	nvarchar(100) '
            with_1.ShipToAddress2 = dr.DBtoString("ShipToAddress2"); //	nvarchar(100) '
            with_1.ShipToZipCode = dr.DBtoString("ShipToZipCode"); //	nvarchar(50) '
            with_1.ShipToCity = dr.DBtoString("ShipToCity"); //	nvarchar(50) '
            with_1.ShipToState = dr.DBtoString("ShipToState"); //	nvarchar(50) '
            with_1.ShipToCountry = dr.DBtoString("ShipToCountry"); //	nvarchar(50) '
            with_1.ShipToAtt = dr.DBtoString("ShipToAtt"); //	nvarchar(50) '
            with_1.ShipToRef = dr.DBtoString("ShipToRef"); //	nvarchar(50) '
            
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
            AddLog(Status: "PurchaseOrder", Logtext: string.Format("Purge PurchaseOrder: ID:{0}", ID), Metode: "Purge");
            return retval;
        }
        public static int Purge(PurchaseOrder rec)
        {
            return Purge(rec.ID);
        }
        
        public int Delete()
        {
            return Delete(System.Convert.ToInt32(this));
        }
        public static int Delete(int ID)
        {
            PurchaseOrder IH = new PurchaseOrder();
            IH.ID = ID;
            return Delete(IH);
        }
        public static int Delete(PurchaseOrder PO)
        {
            foreach (PurchaseOrderItem POI in PO.PurchaseOrderItems)
            {
                POI.Delete();
            }
            DBAccess db = new DBAccess();
            db.AddInt("ID", PO.ID);
            int retval = db.ExecuteNonQuery(_SQLDelete);
            AddLog(Status: "PurchaseOrder", Logtext: string.Format("Delete PurchaseOrder: ID:{0}", PO.ID), Metode: "Delete");
            return retval;
        }
        
        public int Insert()
        {
            return Insert(this);
        }
        public static int Insert(PurchaseOrder rec)
        {
            DBAccess db = new DBAccess();
            
            if (System.Convert.ToInt32(rec.Status) < (int) PurchaseOrder_StatusEnum.Active )
            {
                rec.Status = PurchaseOrder_StatusEnum.Active;
            }
            
            AddParms(ref db, rec);
            
            SqlParameter objParam = new SqlParameter("@ID", 0);
            objParam.Direction = ParameterDirection.Output;
            db.Parameters.Add(objParam);
            int retval = db.ExecuteNonQuery(_SQLInsert);
            if (retval == 1)
            {
                rec.ID = Funktioner.ToInt(objParam.Value, -1);
                AddLog(Status: "PurchaseOrder", Logtext: string.Format("Create PurchaseOrder: ID:{0}", rec.ID), Metode: "Insert");
                return rec.ID;
            }
            else
            {
                AddLog(Status: "PurchaseOrder", Logtext: string.Format("Failure to Create PurchaseOrder:"), logtype: LogTypeEnum.Error, Metode: "Insert");
                return -1;
            }
        }
        
        public int Update()
        {
            return Update(this);
        }
        public static int Update(PurchaseOrder c)
        {
            DBAccess db = new DBAccess();
            
            db.AddInt("ID", c.ID);
            AddParms(ref db, c);
            
            int retval = db.ExecuteNonQuery(_SQLUpdate);
            AddLog(Status: "PurchaseOrder", Logtext: string.Format("Update PurchaseOrder: ID:{0}", c.ID), Metode: "Update");
            return retval;
        }
        
        public int Save()
        {
            return Save(this);
        }
        public static int Save(PurchaseOrder rec)
        {
            int retval = 0;
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
        
        public int SetStatus()
        {
            return SetStatus(this);
        }
        public static int SetStatus(PurchaseOrder rec)
        {
            return SetStatus(rec.ID, rec.Status);
        }
        public static int SetStatus(int ID, PurchaseOrder_StatusEnum Status)
        {
            DBAccess db = new DBAccess();
            
            PurchaseOrder c = new PurchaseOrder(ID);
            db.AddInt("ID", ID);
            db.AddInt("Status", System.Convert.ToInt32(Status));
            AddParmsStandard(db, c);
            
            int retval = db.ExecuteNonQuery(_SQLSetStatus);
            AddLog(Status: "PurchaseOrder", Logtext: string.Format("SetStatus PurchaseOrder: ID:{0} Status:{1}", c.ID, c.StatusText), Metode: "SetStatus");
            return retval;
        }
        
#endregion
        
#region  Get data
        
        public static DataSet GetAllPurchaseOrder()
        {
            DBAccess db = new DBAccess();
            //db.Parameters.Add(New SqlParameter("@sUserName", username))
            DataSet ds = db.ExecuteDataSet(_SQLSelectAll);
            return ds;
        }
        public static DataSet GetAllPurchaseOrder(PurchaseOrder_StatusEnum status)
        {
            DBAccess db = new DBAccess();
            db.AddInt("status", System.Convert.ToInt32(status));
            DataSet ds = db.ExecuteDataSet(_SQLSelectByStatus);
            return ds;
        }
        
        public static DataSet GetPurchaseOrderDS(int ID)
        {
            DBAccess db = new DBAccess();
            db.AddInt("ID", ID);
            DataSet ds = db.ExecuteDataSet(_SQLSelectID);
            return ds;
        }
        
        public static PurchaseOrder GetPurchaseOrder(int ID)
        {
            DBAccess db = new DBAccess();
            PurchaseOrder rec = new PurchaseOrder();
            db.AddInt("ID", ID);
            SqlDataReader dr = (SqlDataReader) (db.ExecuteReader(_SQLSelectOne));
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Populate(dr, rec);
                }
                dr.Close();
            }
            if (!dr.IsClosed)
            {
                dr.Close();
            }
            return rec;
        }
        
        public static DataSet GetPurchaseOrdersDS(int SupplierID, PurchaseOrder_StatusEnum Status)
        {
            DBAccess db = new DBAccess();
            db.AddInt("SupplierID", SupplierID);
            db.AddInt("Status", System.Convert.ToInt32(Status));
            DataSet ds = db.ExecuteDataSet(_SQLSelectByStatusSupplier);
            return ds;
        }
        
        public static System.Collections.Generic.List<PurchaseOrder> GetPurchaseOrderBySupplier(int SupplierID)
        {
            return GetPurchaseOrdersList(SupplierID, PurchaseOrder_StatusEnum.Active);
        }
        public static System.Collections.Generic.List<PurchaseOrder> GetPurchaseOrdersList(int SupplierID, PurchaseOrder_StatusEnum Status)
        {
            System.Collections.Generic.List<PurchaseOrder> result = new System.Collections.Generic.List<PurchaseOrder>();
            DBAccess db = new DBAccess();
            PurchaseOrder rec = default(PurchaseOrder);
            db.AddInt("SupplierID", SupplierID);
            db.AddInt("Status", System.Convert.ToInt32(Status));
            
            SqlDataReader dr = default(SqlDataReader);
            try
            {
                dr = (System.Data.SqlClient.SqlDataReader) (db.ExecuteReader(_SQLSelectByStatusSupplier)); //_SQLSelectAll 'CType(db.ExecuteReader(_SQLSelectAllID), SqlDataReader)
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        rec = new PurchaseOrder();
                        Populate(dr, rec);
                        result.Add(rec); //(PurchaseOrder.GetPurchaseOrder(dr.DBtoInt("ID")))
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
        
        public static DataSet GetRekvireret_PurchaseOrder(PurchaseOrder_StatusEnum type)
        {
            return Search_PurchaseOrder((System.Convert.ToInt32(type)).ToString());
        }
        public static DataSet GetPurchaseOrderSupplierStatus(int SupplierID, PurchaseOrder_StatusEnum type)
        {
            return Search_PurchaseOrder((System.Convert.ToInt32(type)).ToString(), SupplierID);
        }
        public static DataSet Search_PurchaseOrder(string skills, int SupplierID = -1)
        {
            DataSet ds = new DataSet();
            DataSet dsTemp = new DataSet();
            bool flag = false;
            DBAccess db = new DBAccess();
            
            string[] arr = skills.Split(' ');
            foreach (string s in arr)
            {
                db.AddInt("Status", Funktioner.ToInt(s));
                
                if (SupplierID == -1)
                {
                    //db.AddParameter("@CompanyID", DBNull.Value)
                }
                else
                {
                    db.AddInt("SupplierID", SupplierID);
                }
                dsTemp = db.ExecuteDataSet(_SQLSelectByStatusSupplier); //"Co2Db_PurchaseOrder_SelectByStatusCompany"
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
        
        //EXEC	@return_value = [vicjos1_sysadm].[Co2Db_PurchaseOrder_Search]
        //		@SupplierID = -1,
        //		@PurchaseOrderID = -1,
        //		@Status = -1,
        //		@StartDate = NULL,
        //		@EndDate = NULL
        public static DataSet GetPurchaseOrdersSupplierDS(int SupplierID, PurchaseOrder_StatusEnum Status, int PurchaseOrderID, string StartDate, string EndDate)
        {
            DBAccess db = new DBAccess();
            db.AddInt("SupplierID", SupplierID);
            db.AddInt("PurchaseOrderID", PurchaseOrderID);
            db.AddInt("Status", System.Convert.ToInt32(Status));
            if (Information.IsDate(StartDate))
            {
                db.AddDateTime("StartDate", (DateTime?) ((object) StartDate));
            }
            if (Information.IsDate(EndDate))
            {
                db.AddDateTime("EndDate", (DateTime?) ((object) EndDate));
            }
            DataSet ds = db.ExecuteDataSet(_SQLSelectBySupplierSearch);
            return ds;
        }
        
        public static int GetSupplierPurchaseOrderCount(int SupplierID)
        {
            DBAccess db = new DBAccess();
            db.AddInt("SupplierID", SupplierID);
            int res = 0;
            try
            {
                res = Funktioner.ToInt(db.ExecuteScalar(_SQLSelectBySupplierCount));
            }
            catch (Exception)
            {
                res = 0;
            }
            return res;
        }
        
        public static decimal GetPurchaseOrderTotalpris(int PurchaseOrderID)
        {
            DBAccess db = new DBAccess();
            db.AddInt("ID", PurchaseOrderID);
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
