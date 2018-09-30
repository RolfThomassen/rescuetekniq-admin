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
    
    public class PurchaseOrderItem : BaseObject
    {
        
#region  Privates
        
        //CREATE TABLE [vicjos1_sysadm].[Co2Db_PurchaseOrderItem](
        //	[ID] [int] IDENTITY(1,1) NOT NULL,
        //	[PurchaseOrderID] [int] NOT NULL,
        //	[Pos] [int] NULL,
        //	[Status] [int] NULL,
        //	[ItemID] [int] NULL,
        //	[ItemName] [nvarchar](100) NULL,
        //	[SupplierItemID] [nvarchar](50) NULL,
        //	[SupplierItemName] [nvarchar](250) NULL,
        //	[Quantity] [float] NULL,
        //	[ItemPrice] [money] NULL,
        //	[LinePrice] [money] NULL,
        //	[ExpectedReceiptDate] [datetime] NULL,
        //	[ReceiptDate] [datetime] NULL,
        //	[OprettetAF] [nvarchar](50) NULL,
        //	[OprettetDen] [datetime] NULL,
        //	[OprettetIP] [nvarchar](15) NULL,
        //	[RettetAF] [nvarchar](50) NULL,
        //	[RettetDen] [datetime] NULL,
        //	[RettetIP] [nvarchar](15) NULL,
        
        private int _PurchaseOrderID = -1;
        private int _Pos = -1;
        private RescueTekniq.BOL.PurchaseOrderItem_StatusEnum _Status = PurchaseOrderItem_StatusEnum.Active;
        
        private Vare _Item = new Vare();
        private int _ItemID = -1;
        private string _ItemNo;
        private string _ItemName;
        
        private Virksomhed _Supplier = new Virksomhed();
        private int _SupplierID = -1;
        private string _SupplierItemID;
        private string _SupplierItemName;
        
        private decimal _Quantity = 1;
        private decimal _ItemPrice = 0;
        private decimal _LinePrice = 0;
        
        private decimal _PurchaseOrder_TotalPrice = 0;
        
        private DateTime _ExpectedReceiptDate;
        private DateTime _ReceiptDate;
        
#endregion
        
#region  New
        
        public PurchaseOrderItem()
        {
        }
        
        public PurchaseOrderItem(int ID)
        {
            this.ID = ID;
            if (ID > 0)
            {
                DBAccess db = new DBAccess();
                db.Parameters.Add(new SqlParameter("@ID", ID));
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
        
#endregion
        
#region  Properties
        
        public int PurchaseOrderID
        {
            get
            {
                return _PurchaseOrderID;
            }
            set
            {
                _PurchaseOrderID = value;
            }
        }
        public int Pos
        {
            get
            {
                return _Pos;
            }
            set
            {
                _Pos = value;
            }
        }
        public PurchaseOrderItem_StatusEnum Status
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
                return PurchaseOrderItem_StatusEnum.GetName(_Status.GetType(), _Status);
            }
        }
        
        public int ItemID
        {
            get
            {
                return _ItemID;
            }
            set
            {
                _ItemID = value;
                try
                {
                    _Item = Vare.GetVare(value);
                    
                    _ItemNo = Item.VareNr;
                    _ItemName = Item.Navn;
                    
                }
                catch
                {
                }
            }
        }
        public string ItemName
        {
            get
            {
                return _ItemName;
            }
            set
            {
                _ItemName = value;
            }
        }
        public string ItemNo
        {
            get
            {
                return _ItemNo; //Item.VareNr
            }
            set
            {
                _ItemNo = value;
                _ItemID = -1;
                try
                {
                    _Item = Vare.GetVare(value);
                    
                    _ItemID = Item.ID;
                    _ItemName = Item.Navn;
                    
                }
                catch
                {
                }
            }
        }
        
        public decimal Quantity
        {
            get
            {
                return _Quantity;
            }
            set
            {
                _Quantity = value;
            }
        }
        public decimal ItemPrice
        {
            get
            {
                return _ItemPrice;
            }
            set
            {
                _ItemPrice = value;
            }
        }
        public decimal LinePrice
        {
            get
            {
                decimal res = 0;
                res = Quantity * ItemPrice;
                return res;
            }
        }
        public decimal LineTotal
        {
            get
            {
                decimal res = 0;
                res = Quantity * ItemPrice;
                return res;
            }
        }
        
        
        public decimal PurchaseOrder_TotalPrice
        {
            get
            {
                //_PurchaseOrder_TotalPrice = 0
                //Try
                //    _PurchaseOrder_TotalPrice = GetPurchaseOrderTotalPrice(PurchaseOrderID)
                //Catch
                //End Try
                return _PurchaseOrder_TotalPrice;
            }
        }
        
        public int SupplierID
        {
            get
            {
                return _SupplierID;
            }
            protected set
            {
                _SupplierID = value;
            }
        }
        
        public string SupplierItemID
        {
            get
            {
                return _SupplierItemID;
            }
            set
            {
                _SupplierItemID = value;
            }
        }
        public string SupplierItemName
        {
            get
            {
                return _SupplierItemName;
            }
            set
            {
                _SupplierItemName = value;
            }
        }
        
        public DateTime ExpectedReceiptDate
        {
            get
            {
                return _ExpectedReceiptDate;
            }
            set
            {
                _ExpectedReceiptDate = value;
            }
        }
        public DateTime ReceiptDate
        {
            get
            {
                return _ReceiptDate;
            }
            set
            {
                _ReceiptDate = value;
            }
        }
        
        public RescueTekniq.BOL.Vare Item
        {
            get
            {
                try
                {
                    if (_ItemID > 0)
                    {
                        if (!_Item.loaded || _Item.ID != _ItemID)
                        {
                            
                            _Item = Vare.GetVare(_ItemID);
                            
                            _ItemID = _Item.ID;
                            _ItemNo = _Item.VareNr;
                            _ItemName = _Item.Navn;
                            
                        }
                    }
                }
                catch
                {
                }
                return _Item;
            }
        }
        
#endregion
        
#region  Stored Procedure Names
        private const string _SQLDelete = "Co2Db_PurchaseOrderItem_Delete";
        private const string _SQLInsert = "Co2Db_PurchaseOrderItem_Insert";
        private const string _SQLUpdate = "Co2Db_PurchaseOrderItem_Update";
        private const string _SQLSelectAll = "Co2Db_PurchaseOrderItem_SelectAll";
        private const string _SQLSelectAllID = "Co2Db_PurchaseOrderItem_SelectAllID";
        private const string _SQLSelectID = "Co2Db_PurchaseOrderItem_SelectID";
        private const string _SQLSelectOne = "Co2Db_PurchaseOrderItem_SelectOne";
        private const string _SQLPurchaseOrderTotalPris = "Co2Db_PurchaseOrderItem_TotalPris";
        
        private const string _SQLPurchaseOrder_Search = "Co2Db_PurchaseOrderItem_Search";
        
        private const string _SQLGetSupplierID = "Co2Db_PurchaseOrderItem_GetSupplierID";
#endregion
        
#region  Shared Populate
        
        private static void AddParms(ref DBAccess db, PurchaseOrderItem rec)
        {
            var with_1 = rec;
            db.AddInt("PurchaseOrderID", with_1.PurchaseOrderID);
            db.AddInt("Pos", with_1.Pos);
            db.AddInt("Status", (System.Int32) with_1.Status);
            
            db.AddInt("ItemID", with_1.ItemID);
            db.AddNVarChar("ItemName", with_1.ItemName, 100);
            
            db.AddNVarChar("SupplierItemID", with_1.SupplierItemID, 50);
            db.AddNVarChar("SupplierItemName", with_1.SupplierItemName, 250);
            
            db.AddDecimal("Quantity", with_1.Quantity);
            db.AddMoney("ItemPrice", (double) with_1.ItemPrice);
            db.AddMoney("LinePrice", (double) with_1.LinePrice);
            
            db.AddDateTime("ExpectedReceiptDate", with_1.ExpectedReceiptDate);
            db.AddDateTime("ReceiptDate", with_1.ReceiptDate);
            
            AddParmsStandard(db, rec);
        }
        
        private static void Populate(SqlDataReader dr, PurchaseOrderItem rec)
        {
            PopulateStandard(dr, rec);
            var with_1 = rec;
            with_1.PurchaseOrderID = System.Convert.ToInt32(dr.DBtoInt("PurchaseOrderID"));
            with_1.Pos = System.Convert.ToInt32(dr.DBtoInt("Pos"));
            with_1.Status = (RescueTekniq.BOL.PurchaseOrderItem_StatusEnum) (dr.DBtoInt("Status"));
            
            with_1.ItemID = System.Convert.ToInt32(dr.DBtoInt("ItemID"));
            with_1.ItemName = dr.DBtoString("ItemName");
            with_1.SupplierItemID = dr.DBtoString("SupplierItemID");
            with_1.SupplierItemName = dr.DBtoString("SupplierItemName");
            with_1.SupplierID = System.Convert.ToInt32(dr.DBtoInt("SupplierID"));
            
            with_1.Quantity = System.Convert.ToDecimal(dr.DBtoDecimal("Quantity"));
            with_1.ItemPrice = System.Convert.ToDecimal(dr.DBtoDecimal("ItemPrice"));
            with_1._LinePrice = System.Convert.ToDecimal(dr.DBtoDecimal("LinePrice"));
            with_1._PurchaseOrder_TotalPrice = System.Convert.ToDecimal(dr.DBtoDecimal("PurchaseOrder_TotalPrice"));
            
            with_1.ExpectedReceiptDate = System.Convert.ToDateTime(dr.DBtoDateTime("ExpectedReceiptDate"));
            with_1.ReceiptDate = System.Convert.ToDateTime(dr.DBtoDateTime("ReceiptDate"));
            
        }
        
#endregion
        
#region  Metoder
        
#region  Manipulate Data
        
        public int Delete()
        {
            return Delete(System.Convert.ToInt32(this));
        }
        public static int Delete(int ID)
        {
            DBAccess db = new DBAccess();
            db.AddInt("ID", ID);
            int retval = db.ExecuteNonQuery(_SQLDelete);
            AddLog(Status: "PurchaseOrderItem", Logtext: string.Format("Delete PurchaseOrderItem: ID:{0}", ID), Metode: "Delete");
            return retval;
        }
        public static int Delete(PurchaseOrderItem rec)
        {
            return Delete(rec.ID);
        }
        
        public int Insert()
        {
            return Insert(this);
        }
        public static int Insert(PurchaseOrderItem rec)
        {
            DBAccess db = new DBAccess();
            
            if (rec.Status <= (int) PurchaseOrderItem_StatusEnum.Create)
            {
                rec.Status = PurchaseOrderItem_StatusEnum.Active;
            }
            AddParms(ref db, rec);
            SqlParameter objParam = new SqlParameter("@ID", 0);
            objParam.Direction = ParameterDirection.Output;
            db.Parameters.Add(objParam);
            
            int retval = db.ExecuteNonQuery(_SQLInsert);
            if (retval == 1)
            {
                int res = -1;
                res = Funktioner.ToInteger(objParam.Value);
                AddLog(Status: "PurchaseOrderItem", Logtext: string.Format("Create PurchaseOrderItem: ID:{0}", rec.ID), Metode: "Insert");
                return res;
            }
            else
            {
                AddLog(Status: "PurchaseOrderItem", Logtext: string.Format("Failure to create PurchaseOrderItem:"), logtype: LogTypeEnum.Error, Metode: "Insert");
                return -1;
            }
        }
        
        public int Update()
        {
            return Update(this);
        }
        public static int Update(PurchaseOrderItem rec)
        {
            DBAccess db = new DBAccess();
            
            db.AddInt("ID", rec.ID);
            AddParms(ref db, rec);
            
            int retval = 0;
            //Try
            retval = db.ExecuteNonQuery(_SQLUpdate);
            //Catch
            //End Try
            AddLog(Status: "PurchaseOrderItem", Logtext: string.Format("Update PurchaseOrderItem: ID:{0}", rec.ID), Metode: "Update");
            return retval;
        }
        
        public int Save()
        {
            return Save(this);
        }
        public static int Save(PurchaseOrderItem rec)
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
        
#endregion
        
#region  Get Data
        //CREATE TABLE [vicjos1_sysadm].[Co2Db_PurchaseOrderItem](
        //	[ID] [int] IDENTITY(1,1) NOT NULL,
        //	[PurchaseOrderID] [int] NOT NULL,
        //	[Pos] [int] NULL,
        //	[Status] [int] NULL,
        //	[ItemID] [int] NULL,
        //	[ItemName] [nvarchar](100) NULL,
        //	[SupplierItemID] [nvarchar](50) NULL,
        //	[SupplierItemName] [nvarchar](250) NULL,
        //	[Quantity] [float] NULL,
        //	[ItemPrice] [money] NULL,
        //	[LinePrice] [money] NULL,
        //   [ExpectedReceiptDate] [money] NOT NULL,
        //	[ExpectedReceiptDate] [datetime] NULL,
        //	[ReceiptDate] [datetime] NULL,
        //	[OprettetAF] [nvarchar](50) NULL,
        //	[OprettetDen] [datetime] NULL,
        //	[OprettetIP] [nvarchar](15) NULL,
        //	[RettetAF] [nvarchar](50) NULL,
        //	[RettetDen] [datetime] NULL,
        //	[RettetIP] [nvarchar](15) NULL,
        
        public static DataSet GetPurchaseOrderItems(int PurchaseOrderID)
        {
            DBAccess db = new DBAccess();
            db.AddInt("PurchaseOrderID", PurchaseOrderID);
            DataSet ds = db.ExecuteDataSet(_SQLSelectAll);
            return ds;
        }
        
        public static System.Collections.Generic.List<PurchaseOrderItem> GetPurchaseOrderItemList(int PurchaseOrderID)
        {
            System.Collections.Generic.List<PurchaseOrderItem> result = new System.Collections.Generic.List<PurchaseOrderItem>();
            PurchaseOrderItem rec = default(PurchaseOrderItem);
            
            DBAccess db = new DBAccess();
            db.AddInt("PurchaseOrderID", PurchaseOrderID);
            
            SqlDataReader dr = default(SqlDataReader);
            try
            {
                dr = (System.Data.SqlClient.SqlDataReader) (db.ExecuteReader(_SQLSelectAll)); //CType(db.ExecuteReader(_SQLSelectAllID), SqlDataReader)
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        rec = new PurchaseOrderItem();
                        Populate(dr, rec);
                        result.Add(rec); //(PurchaseOrderItem.GetPurchaseOrderItem(dr.DBtoInt("ID")))
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
        
        
        
        //EXEC	@return_value = [vicjos1_sysadm].[Co2Db_PurchaseOrderItem_Search]
        //		@PurchaseOrderID = -1,
        //		@SupplierID = -1,
        //		@Status = -1
        
        public static DataSet GetPurchaseOrderItem_SearchDS(int PurchaseOrderID, int SupplierID, int Status)
        {
            DBAccess db = new DBAccess();
            db.AddInt("PurchaseOrderID", PurchaseOrderID);
            db.AddInt("SupplierID", SupplierID);
            db.AddInt("Status", Status);
            DataSet ds = db.ExecuteDataSet(_SQLPurchaseOrder_Search);
            return ds;
        }
        
        public static DataSet GetPurchaseOrderItemDS(int ID)
        {
            DBAccess db = new DBAccess();
            db.AddInt("ID", ID);
            DataSet ds = db.ExecuteDataSet(_SQLSelectID);
            return ds;
        }
        
        public static PurchaseOrderItem GetPurchaseOrderItem(int ID)
        {
            DBAccess db = new DBAccess();
            PurchaseOrderItem rec = new PurchaseOrderItem();
            db.AddInt("ID", ID);
            SqlDataReader dr = (SqlDataReader) (db.ExecuteReader(_SQLSelectOne)); //CType(db.ExecuteReader(_SQLSelectOne), SqlDataReader)
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Populate(dr, rec);
                }
            }
            if (!dr.IsClosed)
            {
                dr.Close();
            }
            return rec;
        }
        
        public static decimal GetPurchaseOrderTotalPrice(int PurchaseOrderID)
        {
            DBAccess db = new DBAccess();
            db.AddInt("PurchaseOrderID", PurchaseOrderID);
            decimal res = 0;
            try
            {
                object obj = db.ExecuteScalar(_SQLPurchaseOrderTotalPris);
                res = Funktioner.ToDecimal(obj);
            }
            catch
            {
                res = 0;
            }
            return res;
        }
        
        public static int GetSupplierID(int PurchaseOrderID)
        {
            DBAccess db = new DBAccess();
            db.AddInt("PurchaseOrderID", PurchaseOrderID);
            int res = 0;
            try
            {
                object obj = db.ExecuteScalar(_SQLGetSupplierID);
                res = Funktioner.ToInteger(obj);
            }
            catch
            {
                res = 0;
            }
            return res;
        }
        
#endregion
        
#endregion
        
#region  Tags
        
        public string Tags(string tekst)
        {
            return Tags(tekst, this);
        }
        public static string Tags(string tekst, RescueTekniq.BOL.PurchaseOrderItem item)
        {
            string res = "";
            StringBuilder sb = new StringBuilder();
            sb.Append(tekst);
            
            sb.Replace("[PURCHASEORDER.LINE.POS]", System.Convert.ToString(item.Pos));
            sb.Replace("[PURCHASEORDER.LINE.STATUS]", item.StatusText);
            
            sb.Replace("[PURCHASEORDER.LINE.ITEMNAME]", item.ItemName);
            sb.Replace("[PURCHASEORDER.LINE.ITEMNO]", item.ItemNo);
            
            sb.Replace("[PURCHASEORDER.LINE.QUANTITY]", System.Convert.ToString(item.Quantity));
            
            sb.Replace("[PURCHASEORDER.LINE.ITEMPRICE]", item.ItemPrice.ToString("C"));
            
            sb.Replace("[PURCHASEORDER.LINE.LINETOTAL]", item.LinePrice.ToString("C"));
            
            //.Replace("[PURCHASEORDER.LINE.VAT]", item.VAT.ToString)
            //.Replace("[PURCHASEORDER.LINE.FREIGHT]", item.Freight.ToString("C"))
            
            //.Replace("[PURCHASEORDER.LINE.LINETEXT]", item.LineText)
            //.Replace("[PURCHASEORDER.LINE.SERIALNO]", item.SerialNo)
            
            res = sb.ToString();
            res = item.Item.Tags(res);
            
            return res;
        }
        
#endregion
        
        
    }
    
}
