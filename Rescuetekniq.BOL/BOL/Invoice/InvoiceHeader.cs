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
    
    public class InvoiceHeader : BaseObject
    {
        //Implements tags
        
#region  Privates
        private RescueTekniq.BOL.Invoice_StatusEnum _Status = Invoice_StatusEnum.Initialize;
        private int _CompanyID = 0;
        private DateTime _InvoiceDate; // VBConversions Note: Initial value cannot be assigned here since it is non-static.  Assignment has been moved to the class constructors.
        private int _PaymentID = -1;
        private string _PaymentTerms = "";
        private DateTime _PaymentDate;
        
        private int _ResponsibleID = -1;
        private string _ResponsibleName = "";
        private Guid _ResponsibleGUID = Guid.Empty;
        
        private string _ContactName = "";
        private string _ContactEmail = "";
        
        private string _Notes = "";
        
        private string _InvoiceEANno = "";
        private string _InvoiceName = "";
        private string _InvoiceAddress1 = "";
        private string _InvoiceAddress2 = "";
        private string _InvoiceZipCode = "";
        private string _InvoiceCity = "";
        private string _InvoiceState = "";
        private string _InvoiceCountry = "";
        private string _InvoiceAtt = "";
        private string _InvoiceRef = "";
        
        private string _DeleveryEANno = "";
        private string _DeleveryName = "";
        private string _DeleveryAddress1 = "";
        private string _DeleveryAddress2 = "";
        private string _DeleveryZipCode = "";
        private string _DeleveryCity = "";
        private string _DeleveryState = "";
        private string _DeleveryCountry = "";
        private string _DeleveryAtt = "";
        private string _DeleveryRef = "";
        
        private int _InvoiceLines;
        private int _InvoiceLineItems;
        private decimal _InvoiceLinesTotal;
        private decimal _InvoiceTransport;
        private decimal _InvoiceTotal;
        private decimal _InvoiceVAT;
        private decimal _InvoiceTotalInclVAT;
        
        private bool _Paid;
        private DateTime _PaidDate;
        private string _OrderNo;
        private string _YourRef;
        private int _CreditnoteID = -1;
        
        private bool _VATfree = false;
        private bool _EANInvoice = false;
        
        private int _IncassoID = -1;
        
        private Virksomhed _Virksomhed = new Virksomhed();
        private System.Collections.Generic.List<Invoiceline> _Invoiceslines; // = InvoicesLinie.GetInvoicesLinierList(ID)
        
#endregion
        
#region  New
        
        public InvoiceHeader()
        {
            // VBConversions Note: Non-static class variable initialization is below.  Class variables cannot be initially assigned non-static values in C#.
            _InvoiceDate = DateTime.Now;
            
        }
        
        public InvoiceHeader(int ID)
        {
            // VBConversions Note: Non-static class variable initialization is below.  Class variables cannot be initially assigned non-static values in C#.
            _InvoiceDate = DateTime.Now;
            
            this.ID = ID;
            if (ID > 0)
            {
                DBAccess db = new DBAccess();
                db.Parameters.Add(new SqlParameter("@ID", ID));
                SqlDataReader dr = (SqlDataReader) (db.ExecuteReader(_SQLSelectOne)); //"Co2Db_Invoice_Head_SelectOne"
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
        
        public int CompanyID
        {
            get
            {
                return _CompanyID;
            }
            set
            {
                _CompanyID = value;
                try
                {
                    //_Virksomhed = Virksomhed.GetCompany(_CompanyID)
                }
                catch (Exception)
                {
                    
                }
            }
        }
        
        public Invoice_StatusEnum Status
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
                return Invoice_StatusEnum.GetName(_Status.GetType(), _Status);
            }
        }
        
        public int ResponsibleID
        {
            get
            {
                return _ResponsibleID;
            }
            set
            {
                _ResponsibleID = value;
            }
        }
        public Guid ResponsibleGUID
        {
            get
            {
                return _ResponsibleGUID;
            }
            set
            {
                _ResponsibleGUID = value;
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
        public string Responsible
        {
            get
            {
                MembershipUser User = default(MembershipUser);
                string name = "";
                try
                {
                    User = Membership.GetUser(ResponsibleGUID);
                    name = User.UserName;
                }
                catch
                {
                }
                return name;
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
        
        public DateTime InvoiceDate
        {
            get
            {
                return _InvoiceDate;
            }
            set
            {
                _InvoiceDate = value;
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
                return _PaymentTerms;
            }
            set
            {
                _PaymentTerms = value;
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
        
#region  Invoice Address
        
        public string InvoiceEANno
        {
            get
            {
                return _InvoiceEANno;
            }
            set
            {
                _InvoiceEANno = value;
            }
        }
        public string InvoiceName
        {
            get
            {
                return _InvoiceName;
            }
            set
            {
                _InvoiceName = value;
            }
        }
        public string InvoiceAddress1
        {
            get
            {
                return _InvoiceAddress1;
            }
            set
            {
                _InvoiceAddress1 = value;
            }
        }
        public string InvoiceAddress2
        {
            get
            {
                return _InvoiceAddress2;
            }
            set
            {
                _InvoiceAddress2 = value;
            }
        }
        public string InvoiceZipCode
        {
            get
            {
                return _InvoiceZipCode;
            }
            set
            {
                _InvoiceZipCode = value;
            }
        }
        public string InvoiceCity
        {
            get
            {
                return _InvoiceCity;
            }
            set
            {
                _InvoiceCity = value;
            }
        }
        public string InvoiceState
        {
            get
            {
                return _InvoiceState;
            }
            set
            {
                _InvoiceState = value;
            }
        }
        public string InvoiceCountry
        {
            get
            {
                return _InvoiceCountry;
            }
            set
            {
                _InvoiceCountry = value;
            }
        }
        public string InvoiceAtt
        {
            get
            {
                return _InvoiceAtt;
            }
            set
            {
                _InvoiceAtt = value;
            }
        }
        public string InvoiceRef
        {
            get
            {
                return _InvoiceRef;
            }
            set
            {
                _InvoiceRef = value;
            }
        }
        public string InvoicePostnrBy
        {
            get
            {
                string res = "";
                switch (InvoiceCountry) // LandekodeID
                {
                    case "":
                    case "Danmark":
                    case "45":
                    case "298":
                    case "299":
                        res = InvoiceZipCode + " " + InvoiceCity;
                        break;
                    case "USA":
                    case "United Stats Of America":
                    case "1":
                        res = InvoiceCity + ", " + InvoiceState.ToUpper() + " " + InvoiceZipCode;
                        break;
                        //Washington, DC 20546-0001
                    default:
                        res = InvoiceZipCode + " " + InvoiceCity;
                        break;
                }
                return res;
            }
        }
        
        public string InvoiceFuldAdresse
        {
            get
            {
                string res = "";
                if (InvoiceAddress1 != "")
                {
                    res += "<br />" + InvoiceAddress1;
                }
                if (InvoiceAddress2 != "")
                {
                    res += "<br />" + InvoiceAddress2;
                }
                if (InvoicePostnrBy != "")
                {
                    res += "<br />" + InvoicePostnrBy;
                }
                if (InvoiceCountry != "")
                {
                    res += "<br />" + InvoiceCountry;
                }
                if (res.StartsWith("<br />"))
                {
                    res = res.Substring(6);
                }
                return res;
            }
        }
        public string InvoiceFuldAdresseOneline
        {
            get
            {
                string res = "";
                if (InvoiceAddress1 != "")
                {
                    res += ", " + InvoiceAddress1;
                }
                if (InvoiceAddress2 != "")
                {
                    res += ", " + InvoiceAddress2;
                }
                if (InvoicePostnrBy != "")
                {
                    res += ", " + InvoicePostnrBy;
                }
                if (InvoiceCountry != "")
                {
                    res += ", " + InvoiceCountry;
                }
                if (res.StartsWith(", "))
                {
                    res = res.Substring(2);
                }
                return res;
            }
        }
        
#endregion
#region  Delvery Address
        
        public string DeleveryEANno
        {
            get
            {
                return _DeleveryEANno;
            }
            set
            {
                _DeleveryEANno = value;
            }
        }
        public string DeleveryName
        {
            get
            {
                return _DeleveryName;
            }
            set
            {
                _DeleveryName = value;
            }
        }
        public string DeleveryAddress1
        {
            get
            {
                return _DeleveryAddress1;
            }
            set
            {
                _DeleveryAddress1 = value;
            }
        }
        public string DeleveryAddress2
        {
            get
            {
                return _DeleveryAddress2;
            }
            set
            {
                _DeleveryAddress2 = value;
            }
        }
        public string DeleveryZipCode
        {
            get
            {
                return _DeleveryZipCode;
            }
            set
            {
                _DeleveryZipCode = value;
            }
        }
        public string DeleveryCity
        {
            get
            {
                return _DeleveryCity;
            }
            set
            {
                _DeleveryCity = value;
            }
        }
        public string DeleveryState
        {
            get
            {
                return _DeleveryState;
            }
            set
            {
                _DeleveryState = value;
            }
        }
        public string DeleveryCountry
        {
            get
            {
                return _DeleveryCountry;
            }
            set
            {
                _DeleveryCountry = value;
            }
        }
        public string DeleveryAtt
        {
            get
            {
                return _DeleveryAtt;
            }
            set
            {
                _DeleveryAtt = value;
            }
        }
        public string DeleveryRef
        {
            get
            {
                return _DeleveryRef;
            }
            set
            {
                _DeleveryRef = value;
            }
        }
        public string DeleveryPostnrBy
        {
            get
            {
                string res = "";
                switch (DeleveryCountry) // LandekodeID
                {
                    case "":
                    case "Danmark":
                    case "45":
                    case "298":
                    case "299":
                        res = DeleveryZipCode + " " + DeleveryCity;
                        break;
                    case "USA":
                    case "United Stats Of America":
                    case "1":
                        res = DeleveryCity + ", " + DeleveryState.ToUpper() + " " + DeleveryZipCode;
                        break;
                        //Washington, DC 20546-0001
                    default:
                        res = DeleveryZipCode + " " + DeleveryCity;
                        break;
                }
                return res;
            }
        }
        
#endregion
        
        public int InvoiceLines
        {
            get
            {
                return _InvoiceLines;
            }
            set
            {
                _InvoiceLines = value;
            }
        }
        public int InvoiceLineItems
        {
            get
            {
                return _InvoiceLineItems;
            }
            set
            {
                _InvoiceLineItems = value;
            }
        }
        public decimal InvoiceLinesTotal
        {
            get
            {
                return _InvoiceLinesTotal;
            }
            set
            {
                _InvoiceLinesTotal = value;
            }
        }
        public decimal InvoiceTransport
        {
            get
            {
                return _InvoiceTransport;
            }
            set
            {
                _InvoiceTransport = value;
            }
        }
        public decimal InvoiceTotal
        {
            get
            {
                return _InvoiceTotal;
            }
            set
            {
                _InvoiceTotal = value;
            }
        }
        public decimal InvoiceVAT
        {
            get
            {
                return _InvoiceVAT;
            }
            set
            {
                _InvoiceVAT = value;
            }
        }
        public decimal InvoiceTotalInclVAT
        {
            get
            {
                return _InvoiceTotalInclVAT;
            }
            set
            {
                _InvoiceTotalInclVAT = value;
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
        public string OrderNo
        {
            get
            {
                return _OrderNo;
            }
            set
            {
                _OrderNo = value;
            }
        }
        public string YourRef
        {
            get
            {
                return _YourRef;
            }
            set
            {
                _YourRef = value;
            }
        }
        public int CreditnoteID
        {
            get
            {
                return _CreditnoteID;
            }
            set
            {
                _CreditnoteID = value;
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
        
        public bool VATfree
        {
            get
            {
                return _VATfree;
            }
            set
            {
                _VATfree = value;
            }
        }
        
        public bool EANInvoice
        {
            get
            {
                return _EANInvoice;
            }
            set
            {
                _EANInvoice = value;
            }
        }
        public bool IsEAN
        {
            get
            {
                return EANInvoice;
            }
        }
        
        public int IncassoID
        {
            get
            {
                return _IncassoID;
            }
            set
            {
                _IncassoID = value;
            }
        }
        
        private bool _InvoiceslinierLoaded = false;
        public System.Collections.Generic.List<Invoiceline> Invoiceslinier
        {
            get
            {
                if (!_InvoiceslinierLoaded)
                {
                    _Invoiceslines = Invoiceline.GetInvoiceLineList(ID);
                    _InvoiceslinierLoaded = true;
                }
                return _Invoiceslines;
            }
        }
        
#endregion
        
#region  Stored Procedure Names
        private const string _SQLPurge = "Co2Db_Invoice_Head_Purge";
        private const string _SQLDelete = "Co2Db_Invoice_Head_Delete";
        private const string _SQLInsert = "Co2Db_Invoice_Head_Insert";
        private const string _SQLUpdate = "Co2Db_Invoice_Head_Update";
        private const string _SQLSelectAll = "Co2Db_Invoice_Head_SelectAll";
        private const string _SQLSelectID = "Co2Db_Invoice_Head_SelectID";
        private const string _SQLSelectOne = "Co2Db_Invoice_Head_SelectOne";
        private const string _SQLGetTotalpris = "Co2Db_Invoice_Head_GetTotalpris";
        private const string _SQLSelectByStatus = "Co2Db_Invoice_Head_SelectByStatus";
        private const string _SQLSelectByStatusCompany = "Co2Db_Invoice_Head_SelectByStatusCompany";
        private const string _SQLSelectByCompany = "Co2Db_Invoice_Head_SelectByCompany";
        private const string _SQLSelectByCompanyCount = "Co2Db_Invoice_Head_GetCompanyInvoiceCount";
        
        private const string _SQLSetStatus = "Co2Db_Invoice_Head_SetStatus";
        private const string _SQLSetPaid = "Co2Db_Invoice_Head_SetPaid";
        
        private const string _SQLInvoiceStats = "Co2Db_Invoice_Stats";
        
#endregion
        
#region  Shared Populate
        
        private static void AddParms(ref DBAccess db, InvoiceHeader c)
        {
            db.AddInt("Status", (System.Int32) c.Status);
            db.AddInt("CompanyID", c.CompanyID);
            db.AddDateTime("InvoiceDate", c.InvoiceDate);
            
            db.AddInt("PaymentID", c.PaymentID);
            db.AddNVarChar("PaymentTerms", c.PaymentTerms, 50);
            db.AddDateTime("PaymentDate", c.PaymentDate);
            
            db.AddInt("ResponsibleID", c.ResponsibleID);
            db.AddNVarChar("ResponsibleName", c.ResponsibleName, 50);
            db.AddGuid("ResponsibleGUID", c.ResponsibleGUID);
            
            db.AddNVarChar("ContactName", c.ContactName, 50);
            db.AddNVarChar("ContactEmail", c.ContactEmail, 250);
            
            db.AddNVarChar("Notes", c.Notes, -1);
            
            db.AddNVarChar("InvoiceEANno", c.InvoiceEANno, 15);
            db.AddNVarChar("InvoiceName", c.InvoiceName, 100);
            db.AddNVarChar("InvoiceAddress1", c.InvoiceAddress1, 100);
            db.AddNVarChar("InvoiceAddress2", c.InvoiceAddress2, 100);
            db.AddNVarChar("InvoiceZipCode", c.InvoiceZipCode, 50);
            db.AddNVarChar("InvoiceCity", c.InvoiceCity, 50);
            db.AddNVarChar("InvoiceState", c.InvoiceState, 50);
            db.AddNVarChar("InvoiceCountry", c.InvoiceCountry, 50);
            db.AddNVarChar("InvoiceAtt", c.InvoiceAtt, 50);
            db.AddNVarChar("InvoiceRef", c.InvoiceRef, 50);
            
            db.AddNVarChar("DeleveryEANno", c.DeleveryEANno, 15);
            db.AddNVarChar("DeleveryName", c.DeleveryName, 100);
            db.AddNVarChar("DeleveryAddress1", c.DeleveryAddress1, 100);
            db.AddNVarChar("DeleveryAddress2", c.DeleveryAddress2, 100);
            db.AddNVarChar("DeleveryZipCode", c.DeleveryZipCode, 50);
            db.AddNVarChar("DeleveryCity", c.DeleveryCity, 50);
            db.AddNVarChar("DeleveryState", c.DeleveryState, 50);
            db.AddNVarChar("DeleveryCountry", c.DeleveryCountry, 50);
            db.AddNVarChar("DeleveryAtt", c.DeleveryAtt, 50);
            db.AddNVarChar("DeleveryRef", c.DeleveryRef, 50);
            
            db.AddInt("InvoiceLines", c.InvoiceLines);
            db.AddInt("InvoiceLineItems", c.InvoiceLineItems);
            
            db.AddDecimal("InvoiceLinesTotal", c.InvoiceLinesTotal);
            db.AddDecimal("InvoiceTransport", c.InvoiceTransport);
            db.AddDecimal("InvoiceTotal", c.InvoiceTotal);
            db.AddDecimal("InvoiceVAT", c.InvoiceVAT);
            db.AddDecimal("InvoiceTotalInclVAT", c.InvoiceTotalInclVAT);
            
            db.AddBit("Paid", c.Paid);
            db.AddDateTime("PaidDate", c.PaidDate);
            
            db.AddNVarChar("OrderNo", c.OrderNo, 50);
            db.AddNVarChar("YourRef", c.YourRef, 50);
            db.AddInt("CreditnoteID", c.CreditnoteID);
            
            db.AddBoolean("VATfree", c.VATfree);
            db.AddBoolean("EANInvoice", c.EANInvoice);
            
            db.AddInt("IncassoID", c.IncassoID);
            
            AddParmsStandard(db, c);
        }
        
        private static void Populate(SqlDataReader dr, InvoiceHeader c)
        {
            PopulateStandard(dr, c);
            var with_1 = c;
            with_1.Status = (RescueTekniq.BOL.Invoice_StatusEnum) (dr.DBtoInt("Status")); //	int '
            with_1.CompanyID = System.Convert.ToInt32(dr.DBtoInt("CompanyID")); //	int '
            
            with_1.InvoiceDate = System.Convert.ToDateTime(dr.DBtoDate("InvoiceDate")); //	datetime '
            with_1.PaymentID = System.Convert.ToInt32(dr.DBtoInt("PaymentID")); //	int '
            with_1.PaymentTerms = dr.DBtoString("PaymentTerms"); //	nvarchar(50) '
            with_1.PaymentDate = System.Convert.ToDateTime(dr.DBtoDate("PaymentDate")); //	datetime '
            
            with_1.ResponsibleID = System.Convert.ToInt32(dr.DBtoInt("ResponsibleID")); //	int '
            with_1.ResponsibleName = dr.DBtoString("ResponsibleName"); //	nvarchar(50) '
            with_1.ResponsibleGUID = dr.DBtoGuid("ResponsibleGUID"); //	uniqueidentifier '
            
            with_1.ContactName = dr.DBtoString("ContactName"); //	nvarchar(50) '
            with_1.ContactEmail = dr.DBtoString("ContactEmail"); //	nvarchar(250) '
            with_1.Notes = dr.DBtoString("Notes"); //	nvarchar(max) '
            
            with_1.InvoiceEANno = dr.DBtoString("InvoiceEANno"); //	nvarchar(15) '
            with_1.InvoiceName = dr.DBtoString("InvoiceName"); //	nvarchar(100) '
            with_1.InvoiceAddress1 = dr.DBtoString("InvoiceAddress1"); //	nvarchar(100) '
            with_1.InvoiceAddress2 = dr.DBtoString("InvoiceAddress2"); //	nvarchar(100) '
            with_1.InvoiceZipCode = dr.DBtoString("InvoiceZipCode"); //	nvarchar(50) '
            with_1.InvoiceCity = dr.DBtoString("InvoiceCity"); //	nvarchar(50) '
            with_1.InvoiceState = dr.DBtoString("InvoiceState"); //	nvarchar(50) '
            with_1.InvoiceCountry = dr.DBtoString("InvoiceCountry"); //	nvarchar(50) '
            with_1.InvoiceAtt = dr.DBtoString("InvoiceAtt"); //	nvarchar(50) '
            with_1.InvoiceRef = dr.DBtoString("InvoiceRef"); //	nvarchar(50) '
            
            with_1.DeleveryEANno = dr.DBtoString("DeleveryEANno"); //	nvarchar(15) '
            with_1.DeleveryName = dr.DBtoString("DeleveryName"); //	nvarchar(100) '
            with_1.DeleveryAddress1 = dr.DBtoString("DeleveryAddress1"); //	nvarchar(100) '
            with_1.DeleveryAddress2 = dr.DBtoString("DeleveryAddress2"); //	nvarchar(100) '
            with_1.DeleveryZipCode = dr.DBtoString("DeleveryZipCode"); //	nvarchar(50) '
            with_1.DeleveryCity = dr.DBtoString("DeleveryCity"); //	nvarchar(50) '
            with_1.DeleveryState = dr.DBtoString("DeleveryState"); //	nvarchar(50) '
            with_1.DeleveryCountry = dr.DBtoString("DeleveryCountry"); //	nvarchar(50) '
            with_1.DeleveryAtt = dr.DBtoString("DeleveryAtt"); //	nvarchar(50) '
            with_1.DeleveryRef = dr.DBtoString("DeleveryRef"); //	nvarchar(50) '
            
            with_1.InvoiceLines = System.Convert.ToInt32(dr.DBtoInt("InvoiceLines")); //	int '
            with_1.InvoiceLineItems = System.Convert.ToInt32(dr.DBtoInt("InvoiceLineItems")); //	int '
            with_1.InvoiceLinesTotal = System.Convert.ToDecimal(dr.DBtoDecimal("InvoiceLinesTotal")); //	float '
            with_1.InvoiceTransport = System.Convert.ToDecimal(dr.DBtoDecimal("InvoiceTransport")); //	float '
            with_1.InvoiceTotal = System.Convert.ToDecimal(dr.DBtoDecimal("InvoiceTotal")); //	float '
            with_1.InvoiceVAT = System.Convert.ToDecimal(dr.DBtoDecimal("InvoiceVAT")); //	float '
            with_1.InvoiceTotalInclVAT = System.Convert.ToDecimal(dr.DBtoDecimal("InvoiceTotalInclVAT")); //	float '
            
            with_1.Paid = dr.DBtoBit("Paid"); //	bit '
            with_1.PaidDate = System.Convert.ToDateTime(dr.DBtoDate("PaidDate")); //	datetime '
            
            with_1.OrderNo = dr.DBtoString("OrderNo"); //	nvarchar(50) '
            with_1.YourRef = dr.DBtoString("YourRef"); //	nvarchar(50) '
            with_1.CreditnoteID = dr.DBtoInteger("CreditnoteID");
            
            with_1.VATfree = dr.DBtoBoolean("VATfree");
            with_1.EANInvoice = System.Convert.ToBoolean(dr.DBtoBoolean("EANInvoice"));
            
            with_1.IncassoID = System.Convert.ToInt32(dr.DBtoInt("IncassoID"));
            if (with_1.IncassoID < 1)
            {
                with_1.IncassoID = -1;
            }
            
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
            AddLog(Status: "InvoiceHeader", Logtext: string.Format("Purge InvoiceHeader: ID:{0}", ID), Metode: "Purge");
            return retval;
        }
        public static int Purge(InvoiceHeader c)
        {
            return Purge(c.ID);
        }
        
        public int Delete()
        {
            return Delete(System.Convert.ToInt32(this));
        }
        public static int Delete(int ID)
        {
            InvoiceHeader IH = new InvoiceHeader();
            IH.ID = ID;
            return Delete(IH);
        }
        public static int Delete(InvoiceHeader IH)
        {
            foreach (Invoiceline IL in IH.Invoiceslinier)
            {
                IL.Delete();
            }
            DBAccess db = new DBAccess();
            db.AddInt("ID", IH.ID);
            int retval = db.ExecuteNonQuery(_SQLDelete);
            AddLog(Status: "InvoiceHeader", Logtext: string.Format("Delete InvoiceHeader: ID:{0}", IH.ID), Metode: "Delete");
            return retval;
        }
        
        public int Insert()
        {
            return Insert(this);
        }
        public static int Insert(InvoiceHeader c)
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
                AddLog(Status: "InvoiceHeader", Logtext: string.Format("Create InvoiceHeader: ID:{0}", c.ID), Metode: "Insert");
                return c.ID; //Integer.Parse(objParam.Value.ToString)
            }
            else
            {
                AddLog(Status: "InvoiceHeader", Logtext: string.Format("Failure to Create InvoiceHeader:"), logtype: LogTypeEnum.Error, Metode: "Insert");
                return -1;
            }
        }
        
        public int Update()
        {
            return Update(this);
        }
        public static int Update(InvoiceHeader c)
        {
            DBAccess db = new DBAccess();
            
            db.AddInt("ID", c.ID);
            AddParms(ref db, c);
            
            int retval = db.ExecuteNonQuery(_SQLUpdate);
            AddLog(Status: "InvoiceHeader", Logtext: string.Format("Update InvoiceHeader: ID:{0}", c.ID), Metode: "Update");
            return retval;
        }
        
        public static int UpdateStatus(int id, Invoice_StatusEnum status, int IncassoID = -1)
        {
            //Dim t As New InvoiceHeader(id)
            //t.Status = status
            //t.SetStatus()
            return SetStatus(id, status, IncassoID);
        }
        
        public int SetStatus()
        {
            return SetStatus(this);
        }
        public static int SetStatus(InvoiceHeader ih)
        {
            return SetStatus(ih.ID, ih.Status, ih.IncassoID);
        }
        public static int SetStatus(int ID, Invoice_StatusEnum Status, int IncassoID = -1)
        {
            DBAccess db = new DBAccess();
            
            InvoiceHeader c = new InvoiceHeader(ID);
            db.AddInt("ID", ID);
            db.AddInt("Status", System.Convert.ToInt32(Status));
            if (IncassoID > 0)
            {
                db.AddInt("IncassoID", IncassoID);
            }
            AddParmsStandard(db, c);
            
            int retval = db.ExecuteNonQuery(_SQLSetStatus);
            AddLog(Status: "InvoiceHeader", Logtext: string.Format("SetStatus InvoiceHeader: ID:{0} Status:{1}", c.ID, c.StatusText), Metode: "SetStatus");
            return retval;
        }
        
        public int SetPaid()
        {
            return SetPaid(this);
        }
        public static int SetPaid(InvoiceHeader ih)
        {
            return SetPaid(ih.ID, ih.PaidDate);
        }
        public static int SetPaid(int ID, DateTime PaidDate)
        {
            DBAccess db = new DBAccess();
            
            InvoiceHeader c = new InvoiceHeader(ID);
            db.AddInt("ID", ID);
            db.AddInt("Status", System.Convert.ToInt32(Invoiceline_StatusEnum.Payed));
            db.AddDateTime("PaidDate", PaidDate);
            AddParmsStandard(db, c);
            
            int retval = db.ExecuteNonQuery(_SQLSetPaid);
            AddLog(Status: "InvoiceHeader", Logtext: string.Format("SetPaid InvoiceHeader: ID:{0} Paid:{1} Status:{2}", c.ID, c.PaidDate, c.StatusText), Metode: "SetPaid");
            return retval;
        }
        
#endregion
        
#region  Get data
        
        public static DataSet GetAllInvoice()
        {
            DBAccess db = new DBAccess();
            //db.Parameters.Add(New SqlParameter("@sUserName", username))
            DataSet ds = db.ExecuteDataSet(_SQLSelectAll);
            return ds;
        }
        public static DataSet GetAllInvoice(Invoice_StatusEnum status)
        {
            DBAccess db = new DBAccess();
            db.AddInt("status", System.Convert.ToInt32(status));
            DataSet ds = db.ExecuteDataSet(_SQLSelectByStatus);
            return ds;
        }
        
        public static DataSet GetInvoiceDS(int ID)
        {
            DBAccess db = new DBAccess();
            db.AddInt("ID", ID);
            DataSet ds = db.ExecuteDataSet(_SQLSelectID);
            return ds;
        }
        
        public static InvoiceHeader GetInvoice(int ID)
        {
            DBAccess db = new DBAccess();
            InvoiceHeader c = new InvoiceHeader();
            db.AddInt("ID", ID);
            SqlDataReader dr = (SqlDataReader) (db.ExecuteReader(_SQLSelectOne));
            if (dr.HasRows)
            {
                
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
                return c; //Nothing
            }
        }
        
        
        public static System.Collections.Generic.List<InvoiceHeader> GetInvoiceList(int CompanyID, Invoice_StatusEnum Status, int EANInvoice)
        {
            System.Collections.Generic.List<InvoiceHeader> result = new System.Collections.Generic.List<InvoiceHeader>();
            DBAccess db = new DBAccess();
            db.AddInt("CompanyID", CompanyID);
            db.AddInt("Status", System.Convert.ToInt32(Status));
            db.AddInt("EANInvoice", EANInvoice);
            
            System.Data.SqlClient.SqlDataReader dr = default(System.Data.SqlClient.SqlDataReader);
            try
            {
                dr = (System.Data.SqlClient.SqlDataReader) (db.ExecuteReader(_SQLSelectByStatusCompany)); //_SQLSelectAll 'CType(db.ExecuteReader(_SQLSelectAllID), SqlDataReader)
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        result.Add(InvoiceHeader.GetInvoice(System.Convert.ToInt32(dr.DBtoInt("ID"))));
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
        
        public static System.Collections.Generic.List<InvoiceHeader> GetInvoiceByCompany(int CompanyID) //As InvoiceHeader
        {
            return GetInvoiceList(CompanyID, Invoice_StatusEnum.Active, -1);
            
            //Dim db As DBAccess = New DBAccess
            //Dim c As InvoiceHeader = New InvoiceHeader
            //db.AddInt("CompanyID", CompanyID)
            
            //Dim dr As SqlDataReader = CType(db.ExecuteReader(_SQLSelectByCompany), SqlDataReader)
            //If dr.HasRows Then
            
            //    While dr.Read
            //        Populate(dr, c)
            //    End While
            //    dr.Close()
            //    Return c
            //Else
            //    dr.Close()
            //    Return c 'Nothing
            //End If
        }
        
        public static int GetCompanyInvoiceCount(int CompanyID)
        {
            DBAccess db = new DBAccess();
            db.AddInt("CompanyID", CompanyID);
            int res = 0;
            try
            {
                res = Funktioner.ToInt(db.ExecuteScalar(_SQLSelectByCompanyCount));
            }
            catch (Exception)
            {
                res = 0;
            }
            return res;
        }
        
        public static DataSet GetRekvireret_Invoice(Invoice_StatusEnum type)
        {
            return Search_Invoice((System.Convert.ToInt32(type)).ToString());
        }
        public static DataSet GetInvoiceCompanyStatus(int CompanyID, Invoice_StatusEnum type)
        {
            return Search_Invoice((System.Convert.ToInt32(type)).ToString(), CompanyID);
        }
        public static DataSet Search_Invoice(string skills, int CompanyID = -1)
        {
            DataSet ds = new DataSet();
            DataSet dsTemp = new DataSet();
            bool flag = false;
            DBAccess db = new DBAccess();
            
            string[] arr = skills.Split(' ');
            foreach (string s in arr)
            {
                db.AddInt("Status", Funktioner.ToInt(s));
                
                if (CompanyID == -1)
                {
                    //db.AddParameter("@CompanyID", DBNull.Value)
                }
                else
                {
                    db.AddInt("CompanyID", CompanyID);
                }
                dsTemp = db.ExecuteDataSet(_SQLSelectByStatusCompany); //"Co2Db_Invoice_Head_SelectByStatusCompany"
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
        
        public static decimal GetInvoiceTotalpris(int InvoiceID)
        {
            DBAccess db = new DBAccess();
            db.AddInt("InvoiceID", InvoiceID);
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
        
        
        
        //EXEC	[vicjos1_sysadm].[Co2Db_Invoice_Stats]
        //		@PeriodStart = '2000-01-01',
        //		@PeriodEnd = '2011-01-01',
        //		@CompanyID = null,
        //		@ItemID = null,
        //		@InvoicePaid = null,
        //		@VareVareNr = null
        
        public static DataSet GetInvoiceStats(DateTime PeriodStart, DateTime PeriodEnd, int CompanyID = -2, int ItemID = -2, int InvoicePaid = -2, string VareVareNr = "-2")
        {
            DBAccess db = new DBAccess();
            
            db.AddDateTime("PeriodStart", PeriodStart);
            db.AddDateTime("PeriodEnd", PeriodEnd);
            
            if (CompanyID != -2)
            {
                db.AddInt("CompanyID", CompanyID);
            }
            if (ItemID != -2)
            {
                db.AddInt("ItemID", ItemID);
            }
            if (InvoicePaid != -2)
            {
                db.AddInt("InvoicePaid", InvoicePaid);
            }
            if (VareVareNr != "-2")
            {
                db.AddNVarChar("VareVareNr", VareVareNr, 50);
            }
            
            DataSet ds = db.ExecuteDataSet(_SQLInvoiceStats);
            return ds;
        }
        
        
#endregion
        
#endregion
        
        //EXEC	[vicjos1_sysadm].[Co2Db_Invoice_Stats]
        //		@PeriodStart = '2000-01-01',
        //		@PeriodEnd = '2011-01-01',
        //		@CompanyID = null,
        //		@ItemID = null,
        //		@InvoicePaid = null,
        //		@VareVareNr = null
        
        
        //UPDATE [vicjos1_Heart2Start].[vicjos1_sysadm].[Co2Db_Invoice_Head]
        //   SET [Status] = <Status, int,>
        //      ,[CompanyID] = <CompanyID, int,>
        //      ,[InvoiceDate] = <InvoiceDate, datetime,>
        //      ,[PaymentID] = <PaymentID, int,>
        //      ,[PaymentTerms] = <PaymentTerms, nvarchar(50),>
        //      ,[PaymentDate] = <PaymentDate, datetime,>
        //      ,[ResponsibleID] = <ResponsibleID, int,>
        //      ,[ResponsibleName] = <ResponsibleName, nvarchar(50),>
        //      ,[ResponsibleGUID] = <ResponsibleGUID, uniqueidentifier,>
        //      ,[ContactName] = <ContactName, nvarchar(50),>
        //      ,[ContactEmail] = <ContactEmail, nvarchar(250),>
        //      ,[Notes] = <Notes, nvarchar(max),>
        //      ,[InvoiceEANno] = <InvoiceEANno, nvarchar(15),>
        //      ,[InvoiceName] = <InvoiceName, nvarchar(100),>
        //      ,[InvoiceAddress1] = <InvoiceAddress1, nvarchar(100),>
        //      ,[InvoiceAddress2] = <InvoiceAddress2, nvarchar(100),>
        //      ,[InvoiceZipCode] = <InvoiceZipCode, nvarchar(50),>
        //      ,[InvoiceCity] = <InvoiceCity, nvarchar(50),>
        //      ,[InvoiceState] = <InvoiceState, nvarchar(50),>
        //      ,[InvoiceCountry] = <InvoiceCountry, nvarchar(50),>
        //      ,[InvoiceAtt] = <InvoiceAtt, nvarchar(50),>
        //      ,[InvoiceRef] = <InvoiceRef, nvarchar(50),>
        //      ,[DeleveryEANno] = <DeleveryEANno, nvarchar(15),>
        //      ,[DeleveryName] = <DeleveryName, nvarchar(100),>
        //      ,[DeleveryAddress1] = <DeleveryAddress1, nvarchar(100),>
        //      ,[DeleveryAddress2] = <DeleveryAddress2, nvarchar(100),>
        //      ,[DeleveryZipCode] = <DeleveryZipCode, nvarchar(50),>
        //      ,[DeleveryCity] = <DeleveryCity, nvarchar(50),>
        //      ,[DeleveryState] = <DeleveryState, nvarchar(50),>
        //      ,[DeleveryCountry] = <DeleveryCountry, nvarchar(50),>
        //      ,[DeleveryAtt] = <DeleveryAtt, nvarchar(50),>
        //      ,[DeleveryRef] = <DeleveryRef, nvarchar(50),>
        //      ,[InvoiceLines] = <InvoiceLines, int,>
        //      ,[InvoiceLineItems] = <InvoiceLineItems, int,>
        //      ,[InvoiceLinesTotal] = <InvoiceLinesTotal, float,>
        //      ,[InvoiceTransport] = <InvoiceTransport, float,>
        //      ,[InvoiceTotal] = <InvoiceTotal, float,>
        //      ,[InvoiceVAT] = <InvoiceVAT, float,>
        //      ,[InvoiceTotalInclVAT] = <InvoiceTotalInclVAT, float,>
        //      ,[Paid] = <Paid, bit,>
        //      ,[PaidDate] = <PaidDate, datetime,>
        //      ,[OrderNo] = <OrderNo, nvarchar(50),>
        //      ,[YourRef] = <YourRef, nvarchar(50),>
        //      ,[CreditnoteID] = <CreditnoteID, int,>
        //      ,[OprettetAf] = <OprettetAf, nvarchar(50),>
        //      ,[OprettetDen] = <OprettetDen, datetime,>
        //      ,[OprettetIP] = <OprettetIP, nvarchar(15),>
        //      ,[RettetAf] = <RettetAf, nvarchar(50),>
        //      ,[RettetDen] = <RettetDen, datetime,>
        //      ,[RettetIP] = <RettetIP, nvarchar(15),>
        //      ,[VATfree]
        //      ,EANInvoice
        
        
    }
    
}
