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
    
    
    
    public class Invoiceline : BaseObject
    {
        
#region  Privates
        
        private int _InvoiceID = -1;
        private int _Pos = -1;
        private Invoiceline_StatusEnum _Status = Invoiceline_StatusEnum.Active;
        
        private int _ItemID = -1;
        private string _ItemNo;
        private string _ItemName;
        
        private string _LineText;
        private string _SerialNo;
        
        private decimal _ItemPrice = 0;
        private decimal _Discount = 0;
        private decimal _SalesPrice = 0;
        private decimal _Quantity = 1;
        private decimal _LineTotal = 0;
        
        private decimal _ProvisionRate = 0;
        private decimal _Provision = 0;
        private decimal _LineProvision = 0;
        
        private bool _VAT = true;
        private decimal _Freight = 0;
        
        private decimal _TotalInvoicesSalgsPris = 0;
        
        private int _CompanyID = -1;
        private DateTime _InvoiceDate;
        private Invoice_StatusEnum _InvoiceStatus;
        private bool _InvoicePaid;
        private DateTime _InvoicePaidDate;
        
        //   Invoice Vare
        private Vare _Item = new Vare();
        private RescueTekniq.BOL.KundeGrp_Pris _kgp = new RescueTekniq.BOL.KundeGrp_Pris();
        
#endregion
        
#region  New
        
        public Invoiceline()
        {
        }
        
        public Invoiceline(int ID)
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
        public Invoiceline_StatusEnum Status
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
                    _kgp = new KundeGrp_Pris();
                    
                    _ItemNo = Item.VareNr;
                    _ItemName = Item.Navn;
                    
                    //_ItemPrice = KundeGrpPris.SalgsPris
                    //_Discount = KundeGrpPris.Rabat
                    //_Freight = KundeGrpPris.FragtGebyr
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
                    _kgp = new KundeGrp_Pris();
                    
                    _ItemID = Item.ID;
                    _ItemName = Item.Navn;
                    
                    //_ItemPrice = KundeGrpPris.SalgsPris
                    //_Discount = KundeGrpPris.Rabat
                    //_Freight = KundeGrpPris.FragtGebyr
                }
                catch
                {
                }
            }
        }
        
        public string LineText
        {
            get
            {
                return _LineText;
            }
            set
            {
                try
                {
                    _LineText = value;
                }
                catch
                {
                }
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
                try
                {
                    _SerialNo = value;
                }
                catch
                {
                }
            }
        }
        
        /// <summary>
        /// Item price with Custom group Price
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
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
        
        /// <summary>
        /// Discount on Itemprice
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public decimal Discount
        {
            get
            {
                return _Discount;
            }
            set
            {
                _Discount = value;
            }
        }
        
        /// <summary>
        /// Item Salesprice after discount
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public decimal SalesPrice
        {
            get
            {
                decimal res = 0;
                try
                {
                    res = ItemPrice - (ItemPrice * Discount / 100);
                }
                catch
                {
                }
                return res;
            }
            private set
            {
                _SalesPrice = value;
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
        
        /// <summary>
        /// Provision on SalesPrice
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public decimal ProvisionRate
        {
            get
            {
                return _ProvisionRate;
            }
            set
            {
                _ProvisionRate = value;
            }
        }
        
        /// <summary>
        /// Provision on SalesPrice
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public decimal Provision
        {
            get
            {
                decimal res = 0;
                try
                {
                    res = SalesPrice * (ProvisionRate / 100);
                }
                catch
                {
                }
                return res;
            }
            private set
            {
                _Provision = value;
            }
        }
        
        /// <summary>
        /// Total provision pr line
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public decimal LineProvision
        {
            get
            {
                decimal res = 0;
                res = Quantity * Provision;
                return res; //_LineProvision
            }
            private set
            {
                _LineProvision = value;
            }
        }
        
        public decimal LineTotal
        {
            get
            {
                decimal res = 0;
                res = Quantity * SalesPrice;
                return res; //_LineTotal
            }
            private set
            {
                _LineTotal = value;
            }
        }
        
        public bool VAT
        {
            get
            {
                return _VAT;
            }
            set
            {
                _VAT = value;
            }
        }
        
        public decimal Freight
        {
            get
            {
                return _Freight;
            }
            set
            {
                _Freight = value;
            }
        }
        
        public decimal Invoice_TotalAmount
        {
            get
            {
                _TotalInvoicesSalgsPris = 0;
                try
                {
                    _TotalInvoicesSalgsPris = GetTotalInvoicesSalgsPris(InvoiceID);
                }
                catch
                {
                }
                return _TotalInvoicesSalgsPris;
            }
        }
        
        public int CompanyID
        {
            get
            {
                //_CompanyID = 0
                //Try
                //    _CompanyID = GetCompanyID(InvoiceID)
                //Catch
                //End Try
                return _CompanyID;
            }
            protected set
            {
                _CompanyID = value;
            }
        }
        
        public DateTime InvoiceDate
        {
            get
            {
                return _InvoiceDate;
            }
            protected set
            {
                _InvoiceDate = value;
            }
        }
        public Invoice_StatusEnum InvoiceStatus
        {
            get
            {
                return _InvoiceStatus;
            }
            protected set
            {
                _InvoiceStatus = value;
            }
        }
        public bool InvoicePaid
        {
            get
            {
                return _InvoicePaid;
            }
            protected set
            {
                _InvoicePaid = value;
            }
        }
        public DateTime InvoicePaidDate
        {
            get
            {
                return _InvoicePaidDate;
            }
            protected set
            {
                _InvoicePaidDate = value;
            }
        }
        
#region  Multiple Properties
        
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
                            _kgp = new KundeGrp_Pris();
                            
                            _ItemID = _Item.ID;
                            _ItemNo = _Item.VareNr;
                            _ItemName = _Item.Navn;
                            
                            _ItemPrice = KundeGrpPris.SalgsPris;
                            _Discount = KundeGrpPris.Rabat;
                            _Freight = KundeGrpPris.FragtGebyr;
                            
                        }
                    }
                }
                catch
                {
                }
                return _Item;
            }
        }
        
        public RescueTekniq.BOL.KundeGrp_Pris KundeGrpPris
        {
            get
            {
                try
                {
                    if (Item.loaded)
                    {
                        //If Not _kgp.loaded OrElse Item.ID <> _kgp.VareID Then
                        //    _kgp = RescueTekniq.BOL.KundeGrp_Pris.GetCompany_VarePris(CompanyID, Item.ID)
                        //End If
                        
                        if (Item.ID > 0)
                        {
                            if (!_kgp.loaded)
                            {
                                _kgp = KundeGrp_Pris.GetCompany_VarePris(CompanyID, Item.ID);
                            }
                            else if (Item.ID != _kgp.VareID)
                            {
                                _kgp = KundeGrp_Pris.GetCompany_VarePris(CompanyID, Item.ID);
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
        
#endregion
        
#endregion
        
#region  Stored Procedure Names
        private const string _SQLDelete = "Co2Db_Invoice_Line_Delete";
        private const string _SQLInsert = "Co2Db_Invoice_Line_Insert";
        private const string _SQLUpdate = "Co2Db_Invoice_Line_Update";
        private const string _SQLSelectAll = "Co2Db_Invoice_Line_SelectAll";
        private const string _SQLSelectAllID = "Co2Db_Invoice_Line_SelectAllID";
        private const string _SQLSelectID = "Co2Db_Invoice_Line_SelectID";
        private const string _SQLSelectOne = "Co2Db_Invoice_Line_SelectOne";
        private const string _SQLTotalInvoicesPris = "Co2Db_Invoice_Line_TotalInvoicePrice";
        
        private const string _SQLGetCompanyID = "[Co2Db_Invoice_Line_GetCompanyID]";
#endregion
        
#region  Shared Populate
        
        private static void AddParms(ref DBAccess db, Invoiceline c)
        {
            var with_1 = c;
            db.AddInt("InvoiceID", with_1.InvoiceID);
            db.AddInt("Pos", with_1.Pos);
            db.AddInt("Status", (System.Int32) with_1.Status);
            
            db.AddInt("ItemID", with_1.ItemID);
            db.AddNVarChar("ItemNo", with_1.ItemNo, 50);
            db.AddNVarChar("ItemName", with_1.ItemName, 100);
            
            db.AddNVarChar("LineText", with_1.LineText, 250);
            db.AddNVarChar("SerialNo", with_1.SerialNo, 250);
            
            db.AddDecimal("ItemPrice", with_1.ItemPrice);
            db.AddDecimal("Discount", with_1.Discount);
            db.AddDecimal("SalesPrice", with_1.SalesPrice);
            db.AddDecimal("Quantity", with_1.Quantity);
            db.AddDecimal("LineTotal", with_1.LineTotal);
            
            db.AddBoolean("VAT", with_1.VAT);
            db.AddDecimal("Freight", with_1.Freight);
            
            db.AddDecimal("ProvisionRate", with_1.ProvisionRate);
            db.AddDecimal("Provision", with_1.Provision);
            db.AddDecimal("LineProvision", with_1.LineProvision);
            
            AddParmsStandard(db, c);
        }
        
        private static void Populate(System.Data.SqlClient.SqlDataReader dr, Invoiceline c)
        {
            var with_1 = c;
            with_1.InvoiceID = System.Convert.ToInt32(dr.DBtoInt("InvoiceID"));
            with_1.Pos = System.Convert.ToInt32(dr.DBtoInt("Pos"));
            with_1.Status = (RescueTekniq.BOL.Invoiceline_StatusEnum) (dr.DBtoInt("Status"));
            
            with_1.ItemID = System.Convert.ToInt32(dr.DBtoInt("ItemID"));
            with_1._ItemNo = dr.DBtoString("ItemNo");
            with_1._ItemName = dr.DBtoString("ItemName");
            
            with_1.LineText = dr.DBtoString("LineText");
            with_1.SerialNo = dr.DBtoString("SerialNo");
            
            with_1.ItemPrice = System.Convert.ToDecimal(dr.DBtoDecimal("ItemPrice"));
            with_1.Discount = System.Convert.ToDecimal(dr.DBtoDecimal("Discount"));
            with_1.SalesPrice = System.Convert.ToDecimal(dr.DBtoDecimal("SalesPrice"));
            with_1.Quantity = System.Convert.ToDecimal(dr.DBtoDecimal("Quantity"));
            with_1.LineTotal = System.Convert.ToDecimal(dr.DBtoDecimal("LineTotal"));
            
            with_1.VAT = System.Convert.ToBoolean(dr.DBtoBoolean("VAT"));
            with_1.Freight = System.Convert.ToDecimal(dr.DBtoDecimal("Freight"));
            
            with_1.Provision = System.Convert.ToDecimal(dr.DBtoDecimal("Provision"));
            with_1.ProvisionRate = System.Convert.ToDecimal(dr.DBtoDecimal("ProvisionRate"));
            with_1.LineProvision = System.Convert.ToDecimal(dr.DBtoDecimal("LineProvision"));
            
            with_1.CompanyID = System.Convert.ToInt32(dr.DBtoInt("CompanyID"));
            with_1.InvoiceDate = System.Convert.ToDateTime(dr.DBtoDateTime("InvoiceDate"));
            with_1.InvoiceStatus = (RescueTekniq.BOL.Invoice_StatusEnum) (dr.DBtoInt("InvoiceStatus"));
            with_1.InvoicePaid = System.Convert.ToBoolean(dr.DBtoBoolean("InvoicePaid"));
            with_1.InvoicePaidDate = System.Convert.ToDateTime(dr.DBtoDateTime("InvoicePaidDate"));
            
            PopulateStandard(dr, c);
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
            AddLog(Status: "Invoiceline", Logtext: string.Format("Delete Invoiceline: ID:{0}", ID), Metode: "Delete");
            return retval;
        }
        public static int Delete(Invoiceline c)
        {
            return Delete(c.ID);
        }
        
        public int Insert()
        {
            return Insert(this);
        }
        public static int Insert(Invoiceline c)
        {
            DBAccess db = new DBAccess();
            
            if (c.Status <= (int) Invoiceline_StatusEnum.Create)
            {
                c.Status = Invoiceline_StatusEnum.Active;
            }
            AddParms(ref db, c);
            SqlParameter objParam = new SqlParameter("@ID", 0);
            objParam.Direction = ParameterDirection.Output;
            db.Parameters.Add(objParam);
            
            int retval = db.ExecuteNonQuery(_SQLInsert);
            if (retval == 1)
            {
                int res = -1;
                res = Funktioner.ToInteger(objParam.Value);
                AddLog(Status: "Invoiceline", Logtext: string.Format("Create Invoiceline: ID:{0}", c.ID), Metode: "Insert");
                return res;
            }
            else
            {
                AddLog(Status: "Invoiceline", Logtext: string.Format("Failure to Invoiceline AED_Battery:"), logtype: LogTypeEnum.Error, Metode: "Insert");
                return -1;
            }
        }
        
        public int Update()
        {
            return Update(this);
        }
        public static int Update(Invoiceline c)
        {
            DBAccess db = new DBAccess();
            
            db.AddInt("ID", c.ID);
            AddParms(ref db, c);
            
            int retval = 0;
            //Try
            retval = db.ExecuteNonQuery(_SQLUpdate);
            //Catch
            //End Try
            AddLog(Status: "Invoiceline", Logtext: string.Format("Update Invoiceline: ID:{0}", c.ID), Metode: "Update");
            return retval;
        }
        
        public int Save()
        {
            return Save(this);
        }
        public static int Save(Invoiceline IL)
        {
            int retval = 0;
            if (IL.ID > 0)
            {
                retval = Update(IL);
            }
            else
            {
                retval = Insert(IL);
            }
            return retval;
        }
        
#endregion
        
#region  Get Data
        
        public static DataSet GetInvoicelines(int InvoiceID)
        {
            DBAccess db = new DBAccess();
            db.AddInt("InvoiceID", InvoiceID);
            DataSet ds = db.ExecuteDataSet(_SQLSelectAll);
            return ds;
        }
        
        public static System.Collections.Generic.List<Invoiceline> GetInvoiceLineList(int InvoiceID)
        {
            System.Collections.Generic.List<Invoiceline> result = new System.Collections.Generic.List<Invoiceline>();
            
            DBAccess db = new DBAccess();
            db.AddInt("InvoiceID", InvoiceID);
            
            SqlDataReader dr = default(SqlDataReader);
            try
            {
                dr = (System.Data.SqlClient.SqlDataReader) (db.ExecuteReader(_SQLSelectAll)); //CType(db.ExecuteReader(_SQLSelectAllID), SqlDataReader)
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        result.Add(Invoiceline.GetInvoiceLine(System.Convert.ToInt32(dr.DBtoInt("ID"))));
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
        
        public static DataSet GetInvoiceLineDS(int ID)
        {
            DBAccess db = new DBAccess();
            db.AddInt("ID", ID);
            DataSet ds = db.ExecuteDataSet(_SQLSelectID);
            return ds;
        }
        
        public static Invoiceline GetInvoiceLine(int ID)
        {
            DBAccess db = new DBAccess();
            db.Parameters.Add(new SqlParameter("@ID", ID));
            SqlDataReader dr = (SqlDataReader) (db.ExecuteReader(_SQLSelectOne));
            if (dr.HasRows)
            {
                Invoiceline c = new Invoiceline();
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
        
        public static decimal GetTotalInvoicesSalgsPris(int InvoiceID)
        {
            DBAccess db = new DBAccess();
            db.AddInt("InvoiceID", InvoiceID);
            decimal res = 0;
            try
            {
                object obj = db.ExecuteScalar(_SQLTotalInvoicesPris);
                res = Funktioner.ToDecimal(obj);
            }
            catch
            {
                res = 0;
            }
            return res;
        }
        
        public static int GetCompanyID(int InvoiceID)
        {
            DBAccess db = new DBAccess();
            db.AddInt("InvoiceID", InvoiceID);
            int res = 0;
            try
            {
                object obj = db.ExecuteScalar(_SQLGetCompanyID);
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
        public static string Tags(string tekst, RescueTekniq.BOL.Invoiceline item)
        {
            string res = "";
            StringBuilder sb = new StringBuilder();
            sb.Append(tekst);
            
            sb.Replace("[INVOICE.LINE.POS]", System.Convert.ToString(item.Pos));
            sb.Replace("[INVOICE.LINE.STATUS]", item.Status.ToString());
            
            sb.Replace("[INVOICE.LINE.ITEMNAME]", item.ItemName);
            sb.Replace("[INVOICE.LINE.ITEMNO]", item.ItemNo);
            
            sb.Replace("[INVOICE.LINE.QUANTITY]", System.Convert.ToString(item.Quantity));
            
            sb.Replace("[INVOICE.LINE.ITEMPRICE]", item.ItemPrice.ToString("C"));
            sb.Replace("[INVOICE.LINE.DISCOUNT]", item.Discount.ToString());
            sb.Replace("[INVOICE.LINE.SALESPRICE]", item.SalesPrice.ToString("C"));
            
            sb.Replace("[INVOICE.LINE.LINETOTAL]", item.LineTotal.ToString("C"));
            
            //.Replace("[INVOICE.LINE.VAT]", item.VAT.ToString)
            //.Replace("[INVOICE.LINE.FREIGHT]", item.Freight.ToString("C"))
            
            sb.Replace("[INVOICE.LINE.LINETEXT]", item.LineText);
            sb.Replace("[INVOICE.LINE.SERIALNO]", item.SerialNo);
            
            res = sb.ToString();
            res = item.KundeGrpPris.Tags(res);
            res = item.Item.Tags(res);
            
            return res;
        }
        
#endregion
        
        //UPDATE [vicjos1_Heart2Start].[vicjos1_sysadm].[Co2Db_Invoice_Line]
        //   SET [InvoiceID] = <InvoiceID, int,>
        //      ,[Pos] = <Pos, int,>
        //      ,[Status] = <Status, int,>
        //      ,[ItemID] = <ItemID, int,>
        //      ,[ItemNo] = <ItemNo, nvarchar(50),>
        //      ,[ItemName] = <ItemName, nvarchar(100),>
        //      ,[LineText] = <LineText, nvarchar(250),>
        //      ,[SerialNo] = <SerialNo, nvarchar(250),>
        //      ,[Quantity] = <Quantity, int,>
        //      ,[ItemPrice] = <ItemPrice, float,>
        //      ,[Discount] = <Discount, float,>
        //      ,[SalesPrice] = <SalesPrice, float,>
        //      ,[LineTotal] = <LineTotal, float,>
        //      ,[VAT] = <VAT, bit,>
        //      ,[Freight] = <Freight, float,>
        //      ,[OprettetAf] = <OprettetAf, nvarchar(50),>
        //      ,[OprettetDen] = <OprettetDen, datetime,>
        //      ,[OprettetIP] = <OprettetIP, nvarchar(15),>
        //      ,[RettetAf] = <RettetAf, nvarchar(50),>
        //      ,[RettetDen] = <RettetDen, datetime,>
        //      ,[RettetIP] = <RettetIP, nvarchar(15),>
        
    }
    
}
