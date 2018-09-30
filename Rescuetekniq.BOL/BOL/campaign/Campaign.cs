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
    
    public class Campaign : BaseObject
    {
        //Implements tags
        
#region  Privates
        //CREATE TABLE [vicjos1_sysadm].[Co2DB_Campaign](
        //	[ID] [int] IDENTITY(1,1) NOT NULL,
        //	[Active] [bit] NULL DEFAULT ((1)),
        //	[Status] [int] NULL DEFAULT ((1)),
        //	[CampaignNo] [nvarchar](50) NULL,
        //	[Name] [nvarchar](255) NULL,
        //	[Description] [nvarchar](max) NULL,
        //	[StartDate] [datetime] NULL,
        //	[EndDate] [datetime] NULL,
        //	[ExpectedImediateSale] [money] NULL,
        //	[ExpectedFollowupSale] [money] NULL,
        //	[CampaignMaterial] [int] NULL,
        
        private RescueTekniq.BOL.Campaign_StatusEnum _Status = Campaign_StatusEnum.Active;
        private bool _Active = true;
        private string _CampaignNo; // VBConversions Note: Initial value cannot be assigned here since it is non-static.  Assignment has been moved to the class constructors.
        private string _CampaignNoName = "";
        private string _Name = "";
        private string _Description = "";
        private DateTime _Startdate;
        private DateTime _EndDate;
        private decimal _ExpectedImediateSale = 0;
        private decimal _ExpectedFollowUpSale = 0;
        private int _CampaignMaterial = 0;
        
#endregion
        
#region  New
        
        public Campaign()
        {
            // VBConversions Note: Non-static class variable initialization is below.  Class variables cannot be initially assigned non-static values in C#.
            _CampaignNo = string.Format("{0}-{1}", DateTime.Now.Year, 0);
            
        }
        
        public Campaign(int ID)
        {
            // VBConversions Note: Non-static class variable initialization is below.  Class variables cannot be initially assigned non-static values in C#.
            _CampaignNo = string.Format("{0}-{1}", DateTime.Now.Year, 0);
            
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
                }
                dr.Close();
            }
        }
        
#endregion
        
#region  Properties
        //CREATE TABLE [vicjos1_sysadm].[Co2DB_Campaign](
        //	[ID] [int] IDENTITY(1,1) NOT NULL,
        //	[Active] [bit] NULL DEFAULT ((1)),
        //	[Status] [int] NULL DEFAULT ((1)),
        //	[CampaignNo] [nvarchar](50) NULL,
        //	[Name] [nvarchar](255) NULL,
        //	[Description] [nvarchar](max) NULL,
        //	[StartDate] [datetime] NULL,
        //	[EndDate] [datetime] NULL,
        //	[ExpectedImediateSale] [money] NULL,
        //	[ExpectedFollowupSale] [money] NULL,
        //	[CampaignMaterial] [int] NULL,
        
        public Campaign_StatusEnum Status
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
                return Campaign_StatusEnum.GetName(_Status.GetType(), _Status);
            }
        }
        
        public bool Active
        {
            get
            {
                return _Active;
            }
            set
            {
                _Active = value;
            }
        }
        
        public string CampaignNo
        {
            get
            {
                return _CampaignNo;
            }
            set
            {
                _CampaignNo = value;
            }
        }
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
            }
        }
        
        public string CampaignNoName
        {
            get
            {
                return _CampaignNoName;
            }
        }
        
        
        public string Description
        {
            get
            {
                return _Description;
            }
            set
            {
                _Description = value;
            }
        }
        
        public DateTime StartDate
        {
            get
            {
                return _Startdate;
            }
            set
            {
                _Startdate = value;
            }
        }
        public DateTime EndDate
        {
            get
            {
                return _EndDate;
            }
            set
            {
                _EndDate = value;
            }
        }
        
        public decimal ExpectedImediateSale
        {
            get
            {
                return _ExpectedImediateSale;
            }
            set
            {
                _ExpectedImediateSale = value;
            }
        }
        
        public decimal ExpectedFollowUpSale
        {
            get
            {
                return _ExpectedFollowUpSale;
            }
            set
            {
                _ExpectedFollowUpSale = value;
            }
        }
        
        //Public ReadOnly Property Firmanavn() As String
        //    Get
        //        Dim res As String = ""
        //        Try
        //            res = Company.Firmanavn
        //        Catch
        //        End Try
        //        Return res
        //    End Get
        //End Property
        //Public ReadOnly Property Company() As RescueTekniq.BOL.Virksomhed
        //    Get
        //        If _SupplierID > 0 Then
        //            If Not _Supplier.loaded Then
        //                _Supplier = Virksomhed.GetCompany(_SupplierID)
        //            ElseIf _Supplier.ID <> _SupplierID Then
        //                _Supplier = Virksomhed.GetCompany(_SupplierID)
        //            End If
        //        End If
        //        Return _Supplier
        //    End Get
        //End Property
        
        //'CampaignItem
        //Private _CampaignItemsLoaded As Boolean = False
        //Public ReadOnly Property CampaignItems() As System.Collections.Generic.List(Of CampaignItem)
        //    Get
        //        If Not _CampaignItemsLoaded Then
        //            _CampaignItems = CampaignItem.GetCampaignItemList(ID)
        //            _CampaignItemsLoaded = True
        //        End If
        //        Return _CampaignItems
        //    End Get
        //End Property
        
#endregion
        
#region  Stored Procedure Names
        private const string _SQLPurge = "Co2Db_Campaign_Purge";
        private const string _SQLDelete = "Co2Db_Campaign_Delete";
        private const string _SQLInsert = "Co2Db_Campaign_Insert";
        private const string _SQLUpdate = "Co2Db_Campaign_Update";
        private const string _SQLSelectAll = "Co2Db_Campaign_SelectAll";
        private const string _SQLSelectID = "Co2Db_Campaign_SelectID";
        private const string _SQLSelectOne = "Co2Db_Campaign_SelectOne";
        private const string _SQLGetTotalpris = "Co2Db_Campaign_GetTotalPrice";
        private const string _SQLSelectByStatus = "Co2Db_Campaign_SelectByStatus";
        private const string _SQLSelectByStatusSupplier = "Co2Db_Campaign_SelectByStatusSupplier";
        private const string _SQLSelectBySupplier = "Co2Db_Campaign_SelectBySupplier";
        private const string _SQLSelectBySupplierCount = "Co2Db_Campaign_GetSupplierInvoiceCount";
        private const string _SQLSelectBySupplierSearch = "Co2Db_Campaign_Search";
        
        private const string _SQLGetNextCampaignNo = "Co2Db_Campaign_GetNextCampaignNo";
        
        private const string _SQLSetStatus = "Co2Db_Campaign_SetStatus";
        
#endregion
        
#region  Shared Populate
        
        private static void AddParms(ref DBAccess db, Campaign rec)
        {
            
            db.AddInt("Status", (System.Int32) rec.Status);
            db.AddBit("Active", rec.Active);
            
            db.AddNVarChar("CampaignNo", rec.CampaignNo, 50);
            db.AddNVarChar("Name", rec.Name, 255);
            db.AddNVarChar("Description", rec.Description);
            
            db.AddDateTime("StartDate", rec.StartDate);
            db.AddDateTime("EndDate", rec.EndDate);
            
            db.AddMoney("ExpectedImediateSale", (double) rec.ExpectedImediateSale);
            db.AddMoney("ExpectedFollowUpSale", (double) rec.ExpectedFollowUpSale);
            
            AddParmsStandard(db, rec);
        }
        
        private static object Populate(System.Data.IDataReader dr, Campaign rec)
        {
            rec = (RescueTekniq.BOL.Campaign) (PopulateStandard(dr, rec));
            
            rec.Status = (RescueTekniq.BOL.Campaign_StatusEnum) (dr.DBtoInt("Status"));
            rec.Active = System.Convert.ToBoolean(dr.DBtoBoolean("Active"));
            
            rec.CampaignNo = dr.DBtoString("CampaignNo");
            rec.Name = dr.DBtoString("Name");
            rec._CampaignNoName = dr.DBtoString("CampaignNoName");
            rec.Description = dr.DBtoString("Description");
            
            rec.StartDate = System.Convert.ToDateTime(dr.DBtoDate("StartDate"));
            rec.EndDate = System.Convert.ToDateTime(dr.DBtoDate("EndDate"));
            
            rec.ExpectedImediateSale = System.Convert.ToDecimal(dr.DBtoDecimal("ExpectedImediateSale"));
            rec.ExpectedFollowUpSale = System.Convert.ToDecimal(dr.DBtoDecimal("ExpectedFollowUpSale"));
            
            //.CampaignMaterial = dr.DBtoInt("CampaignMaterial")
            return rec;
        }
        
        //CREATE TABLE [vicjos1_sysadm].[Co2DB_Campaign](
        //	[ID] [int] IDENTITY(1,1) NOT NULL,
        //	[Active] [bit] NULL DEFAULT ((1)),
        //	[Status] [int] NULL DEFAULT ((1)),
        //	[CampaignNo] [nvarchar](50) NULL,
        //	[Name] [nvarchar](255) NULL,
        //	[Description] [nvarchar](max) NULL,
        //	[StartDate] [datetime] NULL,
        //	[EndDate] [datetime] NULL,
        //	[ExpectedImediateSale] [money] NULL,
        //	[ExpectedFollowupSale] [money] NULL,
        //	[CampaignMaterial] [int] NULL,
        
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
            AddLog(Status: "Campaign", Logtext: string.Format("Purge Campaign: ID:{0}", ID), Metode: "Purge");
            return retval;
        }
        public static int Purge(Campaign rec)
        {
            return Purge(rec.ID);
        }
        
        public int Delete()
        {
            return Delete(System.Convert.ToInt32(this));
        }
        public static int Delete(int ID)
        {
            Campaign IH = new Campaign();
            IH.ID = ID;
            return Delete(IH);
        }
        public static int Delete(Campaign rec)
        {
            //For Each POI As CampaignItem In rec.CampaignItems
            //    POI.Delete()
            //Next
            DBAccess db = new DBAccess();
            db.AddInt("ID", rec.ID);
            int retval = db.ExecuteNonQuery(_SQLDelete);
            AddLog(Status: "Campaign", Logtext: string.Format("Delete Campaign: ID:{0}", rec.ID), Metode: "Delete");
            return retval;
        }
        
        public int Insert()
        {
            return Insert(this);
        }
        public static int Insert(Campaign rec)
        {
            DBAccess db = new DBAccess();
            
            if (System.Convert.ToInt32(rec.Status) < (int) Campaign_StatusEnum.Active )
            {
                rec.Status = Campaign_StatusEnum.Active;
            }
            
            AddParms(ref db, rec);
            
            SqlParameter objParam = new SqlParameter("@ID", 0);
            objParam.Direction = ParameterDirection.Output;
            db.Parameters.Add(objParam);
            int retval = db.ExecuteNonQuery(_SQLInsert);
            if (retval == 1)
            {
                rec.ID = Funktioner.ToInt(objParam.Value, -1);
                AddLog(Status: "Campaign", Logtext: string.Format("Create Campaign: ID:{0}", rec.ID), Metode: "Insert");
                return rec.ID;
            }
            else
            {
                AddLog(Status: "Campaign", Logtext: string.Format("Failure to Create Campaign:"), logtype: LogTypeEnum.Error, Metode: "Insert");
                return -1;
            }
        }
        
        public int Update()
        {
            return Update(this);
        }
        public static int Update(Campaign rec)
        {
            DBAccess db = new DBAccess();
            
            db.AddInt("ID", rec.ID);
            AddParms(ref db, rec);
            
            int retval = db.ExecuteNonQuery(_SQLUpdate);
            AddLog(Status: "Campaign", Logtext: string.Format("Update Campaign: ID:{0}", rec.ID), Metode: "Update");
            return retval;
        }
        
        public int Save()
        {
            return Save(this);
        }
        public static int Save(Campaign rec)
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
        public static int SetStatus(Campaign rec)
        {
            return SetStatus(rec.ID, rec.Status);
        }
        public static int SetStatus(int ID, Campaign_StatusEnum Status)
        {
            DBAccess db = new DBAccess();
            
            Campaign rec = new Campaign(ID);
            db.AddInt("ID", ID);
            db.AddInt("Status", Convert.ToInt32(Status));
            AddParmsStandard(db, rec);
            
            int retval = db.ExecuteNonQuery(_SQLSetStatus);
            AddLog(Status: "Campaign", Logtext: string.Format("SetStatus Campaign: ID:{0} Status:{1}", rec.ID, rec.StatusText), Metode: "SetStatus");
            return retval;
        }
        
#endregion
        
#region  Get data
        
        public static DataSet GetAllCampaignsDS()
        {
            DBAccess db = new DBAccess();
            //db.Parameters.Add(New SqlParameter("@sUserName", username))
            DataSet ds = db.ExecuteDataSet(_SQLSelectAll);
            return ds;
        }
        public static DataSet GetAllCampaignsDS(Campaign_StatusEnum status)
        {
            DBAccess db = new DBAccess();
            db.AddInt("status", Convert.ToInt32(status));
            DataSet ds = db.ExecuteDataSet(_SQLSelectByStatus);
            return ds;
        }
        
        public static DataSet GetCampaignDS(int ID)
        {
            DBAccess db = new DBAccess();
            db.AddInt("ID", ID);
            DataSet ds = db.ExecuteDataSet(_SQLSelectID);
            return ds;
        }
        
        public static Campaign GetCampaign(int ID)
        {
            DBAccess db = new DBAccess();
            Campaign rec = new Campaign();
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
        
        public static DataSet GetCampaignsDS(int SupplierID, Campaign_StatusEnum Status)
        {
            DBAccess db = new DBAccess();
            db.AddInt("SupplierID", SupplierID);
            db.AddInt("Status", Convert.ToInt32(Status));
            DataSet ds = db.ExecuteDataSet(_SQLSelectByStatusSupplier);
            return ds;
        }
        
        public static System.Collections.Generic.List<Campaign> GetCampaignBySupplier(int SupplierID)
        {
            return GetCampaignsList(SupplierID, Campaign_StatusEnum.Active);
        }
        public static System.Collections.Generic.List<Campaign> GetCampaignsList(int SupplierID, Campaign_StatusEnum Status)
        {
            System.Collections.Generic.List<Campaign> result = new System.Collections.Generic.List<Campaign>();
            DBAccess db = new DBAccess();
            Campaign rec = default(Campaign);
            db.AddInt("SupplierID", SupplierID);
            db.AddInt("Status", Convert.ToInt32(Status));
            
            SqlDataReader dr = default(SqlDataReader);
            //Try
            dr = (System.Data.SqlClient.SqlDataReader) (db.ExecuteReader(_SQLSelectByStatusSupplier)); //_SQLSelectAll 'CType(db.ExecuteReader(_SQLSelectAllID), SqlDataReader)
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    rec = new Campaign();
                    Populate(dr, rec);
                    result.Add(rec); //(Campaign.GetCampaign(dr.DBtoInt("ID")))
                }
            }
            dr.Close();
            //Catch ex As Exception
            //Throw ex
            //End Try
            
            return result;
        }
        
        public static DataSet GetRekvireret_Campaign(Campaign_StatusEnum type)
        {
            return Search_Campaign((System.Convert.ToInt32(type)).ToString());
        }
        public static DataSet GetCampaignSupplierStatus(int SupplierID, Campaign_StatusEnum type)
        {
            return Search_Campaign((System.Convert.ToInt32(type)).ToString(), SupplierID);
        }
        public static DataSet Search_Campaign(string skills, int SupplierID = -1)
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
                dsTemp = db.ExecuteDataSet(_SQLSelectByStatusSupplier); //"Co2Db_Campaign_SelectByStatusCompany"
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
        
        //EXEC	@return_value = [vicjos1_sysadm].[Co2Db_Campaign_Search]
        //		@SupplierID = -1,
        //		@CampaignID = -1,
        //		@Status = -1,
        //		@StartDate = NULL,
        //		@EndDate = NULL
        public static DataSet GetCampaignsSupplierDS(int SupplierID, Campaign_StatusEnum Status, int CampaignID, string StartDate, string EndDate)
        {
            DBAccess db = new DBAccess();
            db.AddInt("SupplierID", SupplierID);
            db.AddInt("CampaignID", CampaignID);
            db.AddInt("Status", Convert.ToInt32(Status));
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
        
        public static decimal GetCampaignTotalpris(int CampaignID)
        {
            DBAccess db = new DBAccess();
            db.AddInt("ID", CampaignID);
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
        
        public static string GetNextCampaignNo()
        {
            DBAccess db = new DBAccess();
            string res = "";
            string CampaignNo = "";
            db.addGetStr("CampaignNo");
            
            res = System.Convert.ToString(db.ExecuteScalar(_SQLGetNextCampaignNo));
            
            //res = db.Parameters("@CampaignNo").value.ToString
            
            return res;
        }
        
        
        
#endregion
        
#endregion
        
#region  Tags
        
        public string Tags(string tekst)
        {
            return Tags(tekst, this);
        }
        public static string Tags(string tekst, RescueTekniq.BOL.Campaign item)
        {
            string res = "";
            StringBuilder sb = new StringBuilder();
            sb.Append(tekst);
            
            //CREATE TABLE [vicjos1_sysadm].[Co2DB_Campaign](
            //	[ID] [int] IDENTITY(1,1) NOT NULL,
            //	[Active] [bit] NULL DEFAULT ((1)),
            //	[Status] [int] NULL DEFAULT ((1)),
            //	[CampaignNo] [nvarchar](50) NULL,
            //	[Name] [nvarchar](255) NULL,
            //	[Description] [nvarchar](max) NULL,
            //	[StartDate] [datetime] NULL,
            //	[EndDate] [datetime] NULL,
            
            //	[ExpectedImediateSale] [money] NULL,
            //	[ExpectedFollowupSale] [money] NULL,
            //	[CampaignMaterial] [int] NULL,
            
            sb.Replace("[CAMPAIGN.STATUS]", item.StatusText);
            sb.Replace("[CAMPAIGN.ACTIVE]", System.Convert.ToString(item.Active ? "Ja" : "Nej"));
            sb.Replace("[CAMPAIGN.CAMPAIGNNO]", item.CampaignNo);
            
            sb.Replace("[CAMPAIGN.NAME]", item.Name);
            sb.Replace("[CAMPAIGN.DESCRIPTION]", item.Description);
            
            sb.Replace("[CAMPAIGN.STARTDATE]", item.StartDate.ToString("dd. MMM yyyy"));
            sb.Replace("[CAMPAIGN.STARTDATE.LONGDATE]", item.StartDate.ToLongDateString());
            sb.Replace("[CAMPAIGN.ENDDATE]", item.EndDate.ToString("dd. MMM yyyy"));
            sb.Replace("[CAMPAIGN.ENDDATE.LONGDATE]", item.EndDate.ToLongDateString());
            
            sb.Replace("[CAMPAIGN.EXPECTEDIMEDIATESALE]", item.ExpectedImediateSale.ToString("C"));
            sb.Replace("[CAMPAIGN.EXPECTEDFOLLOWUPSALE]", item.ExpectedFollowUpSale.ToString("C"));
            
            res = sb.ToString();
            //res = item.Item.Tags(res)
            
            return res;
        }
        
#endregion
        
    }
    
}
