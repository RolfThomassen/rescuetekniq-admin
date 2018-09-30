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
    
    public class CampaignCompany : BaseObject
    {
        
#region  Privates
        
        //CREATE TABLE [vicjos1_sysadm].[Co2Db_Campaign_Company](
        //[ID] [int] IDENTITY(1,1) NOT NULL,
        //[CampaignID] [int] NOT NULL,
        //[CompanyID] [int] NOT NULL,
        //[Status] [int] NULL,
        //[ResponsibleID] [uniqueidentifier] NULL,
        //[ResponsibleName] [nvarchar](50) NULL,
        //[ResponsibleEmail] [nvarchar](255) NULL,
        //[ContactPerson] [nvarchar](50) NULL,
        //[ContactedByPhone] [bit] NULL,
        //[MeetingHeld] [bit] NULL,
        //[CampaignSale] [bit] NULL,
        //[CampaignSaleAmount] [money] NULL,
        //[ExpectedSale] [bit] NULL,
        //[ExpectedSaleAmount] [money] NULL,
        //[Notes] [nvarchar](max) NULL,
        
        private int _CampaignID = -1;
        private int _CompanyID = -1;
        private RescueTekniq.BOL.CampaignCompany_StatusEnum _Status = CampaignCompany_StatusEnum.Active;
        
        private Virksomhed _Company = new Virksomhed();
        private Campaign _Campaign = new Campaign();
        
        private Guid _ResponsibleID;
        private string _ResponsibleName = "";
        private string _ResponsibleEmail = "";
        private string _ContactPerson = "";
        private bool _ContactedByPhone = false;
        private bool _MeetingHeld = false;
        private bool _CampaignSale = false;
        private decimal _CampaignSaleAmount = 0;
        private bool _ExpectedSale = false;
        private decimal _ExpectedSaleAmount = 0;
        
        private string _Notes = "";
        
#endregion
        
#region  New
        
        public CampaignCompany()
        {
        }
        
        public CampaignCompany(int ID)
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
        
        public int CampaignID
        {
            get
            {
                return _CampaignID;
            }
            set
            {
                _CampaignID = value;
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
        
        public CampaignCompany_StatusEnum Status
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
                return CampaignCompany_StatusEnum.GetName(_Status.GetType(), _Status);
            }
        }
        
        public Guid ResponsibleID
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
        
        public string ContactPerson
        {
            get
            {
                return _ContactPerson;
            }
            set
            {
                _ContactPerson = value;
            }
        }
        public bool ContactedByPhone
        {
            get
            {
                return _ContactedByPhone;
            }
            set
            {
                _ContactedByPhone = value;
            }
        }
        
        public bool MeetingHeld
        {
            get
            {
                return _MeetingHeld;
            }
            set
            {
                _MeetingHeld = value;
            }
        }
        
        public bool CampaignSale
        {
            get
            {
                return _CampaignSale;
            }
            set
            {
                _CampaignSale = value;
            }
        }
        public decimal CampaignSaleAmount
        {
            get
            {
                return _CampaignSaleAmount;
            }
            set
            {
                _CampaignSaleAmount = value;
            }
        }
        
        public bool ExpectedSale
        {
            get
            {
                return _ExpectedSale;
            }
            set
            {
                _ExpectedSale = value;
            }
        }
        public decimal ExpectedSaleAmount
        {
            get
            {
                return _ExpectedSaleAmount;
            }
            set
            {
                _ExpectedSaleAmount = value;
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
        
        public RescueTekniq.BOL.Campaign Campaign
        {
            get
            {
                try
                {
                    if (_CampaignID > 0)
                    {
                        if (!_Campaign.loaded || _Campaign.ID != _CampaignID)
                        {
                            _Campaign = RescueTekniq.BOL.Campaign.GetCampaign(_CampaignID);
                        }
                    }
                }
                catch
                {
                }
                return _Campaign;
            }
        }
        
        public RescueTekniq.BOL.Virksomhed Company
        {
            get
            {
                try
                {
                    if (_CompanyID > 0)
                    {
                        if (!_Company.loaded || _Company.ID != _CompanyID)
                        {
                            _Company = Virksomhed.GetCompany(_CompanyID);
                        }
                    }
                }
                catch
                {
                }
                return _Company;
            }
        }
        
#endregion
        
#region  Stored Procedure Names
        private const string _SQLDelete = "Co2Db_Campaign_Company_Delete";
        private const string _SQLInsert = "Co2Db_Campaign_Company_Insert";
        private const string _SQLUpdate = "Co2Db_Campaign_Company_Update";
        private const string _SQLSelectAll = "Co2Db_Campaign_Company_SelectAll";
        private const string _SQLSelectID = "Co2Db_Campaign_Company_SelectOne";
        private const string _SQLSelectOne = "Co2Db_Campaign_Company_SelectOne";
        private const string _SQLCampaign_Search = "Co2Db_Campaign_Company_Search";
        
        private const string _SQLGetCampaignID = "Co2DB_Campaign_Company_SelectCampaignID";
        private const string _SQLGetCampaignNo = "Co2DB_Campaign_Company_SelectCampaignNo";
        private const string _SQLGetCompanyID = "Co2DB_Campaign_Company_SelectCompanyID";
        
#endregion
        
#region  Shared Populate
        
        private static void AddParms(ref DBAccess db, CampaignCompany rec)
        {
            var with_1 = rec;
            db.AddInt("CompanyID", with_1.CompanyID);
            db.AddInt("CampaignID", with_1.CampaignID);
            db.AddInt("Status", (System.Int32) with_1.Status);
            
            db.AddGuid("ResponsibleID", with_1.ResponsibleID);
            db.AddNVarChar("ResponsibleName", with_1.ResponsibleName, 50);
            db.AddNVarChar("ResponsibleEmail", with_1.ResponsibleEmail, 255);
            
            db.AddNVarChar("ContactPerson", with_1.ContactPerson, 50);
            db.AddBoolean("ContactedByPhone", with_1.ContactedByPhone);
            
            db.AddBoolean("MeetingHeld", with_1.MeetingHeld);
            db.AddBoolean("CampaignSale", with_1.CampaignSale);
            db.AddMoney("CampaignSaleAmount", (double) with_1.CampaignSaleAmount);
            db.AddBoolean("ExpectedSale", with_1.ExpectedSale);
            db.AddMoney("ExpectedSaleAmount", (double) with_1.ExpectedSaleAmount);
            
            db.AddNVarChar("Notes", with_1.Notes, -1);
            
            AddParmsStandard(db, rec);
        }
        
        private static void Populate(SqlDataReader dr, CampaignCompany rec)
        {
            PopulateStandard(dr, rec);
            var with_1 = rec;
            with_1.CampaignID = System.Convert.ToInt32(dr.DBtoInt("CampaignID"));
            with_1.CampaignID = System.Convert.ToInt32(dr.DBtoInt("CampaignID"));
            with_1.Status = (RescueTekniq.BOL.CampaignCompany_StatusEnum) (dr.DBtoInt("Status"));
            
            with_1.ResponsibleID = dr.DBtoGuid("ResponsibleID");
            with_1.ResponsibleName = dr.DBtoString("ResponsibleName");
            with_1.ResponsibleEmail = dr.DBtoString("ResponsibleEmail");
            
            with_1.ContactPerson = dr.DBtoString("ContactPerson");
            with_1.ContactedByPhone = System.Convert.ToBoolean(dr.DBtoBoolean("ContactedByPhone"));
            
            with_1.MeetingHeld = System.Convert.ToBoolean(dr.DBtoBoolean("MeetingHeld"));
            with_1.CampaignSale = System.Convert.ToBoolean(dr.DBtoBoolean("CampaignSale"));
            with_1.CampaignSaleAmount = System.Convert.ToDecimal(dr.DBtoDecimal("CampaignSaleAmount"));
            with_1.ExpectedSale = System.Convert.ToBoolean(dr.DBtoBoolean("ExpectedSale"));
            with_1.ExpectedSaleAmount = decimal.Parse(dr.DBtoString("ExpectedSaleAmount"));
            
            with_1.Notes = dr.DBtoString("Notes");
            
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
            AddLog(Status: "CampaignCompany", Logtext: string.Format("Delete CampaignCompany: ID:{0}", ID), Metode: "Delete");
            return retval;
        }
        public static int Delete(CampaignCompany rec)
        {
            return Delete(rec.ID);
        }
        
        public int Insert()
        {
            return Insert(this);
        }
        public static int Insert(CampaignCompany rec)
        {
            DBAccess db = new DBAccess();
            
            if (rec.Status <= (int) CampaignCompany_StatusEnum.Create)
            {
                rec.Status = CampaignCompany_StatusEnum.Active;
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
                AddLog(Status: "CampaignCompany", Logtext: string.Format("Create CampaignCompany: ID:{0}", rec.ID), Metode: "Insert");
                return res;
            }
            else
            {
                AddLog(Status: "CampaignCompany", Logtext: string.Format("Failure to create CampaignCompany:"), logtype: LogTypeEnum.Error, Metode: "Insert");
                return -1;
            }
        }
        
        public int Update()
        {
            return Update(this);
        }
        public static int Update(CampaignCompany rec)
        {
            DBAccess db = new DBAccess();
            
            db.AddInt("ID", rec.ID);
            AddParms(ref db, rec);
            
            int retval = 0;
            //Try
            retval = db.ExecuteNonQuery(_SQLUpdate);
            //Catch
            //End Try
            AddLog(Status: "CampaignCompany", Logtext: string.Format("Update CampaignCompany: ID:{0}", rec.ID), Metode: "Update");
            return retval;
        }
        
        public int Save()
        {
            return Save(this);
        }
        public static int Save(CampaignCompany rec)
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
        
        //CREATE TABLE [vicjos1_sysadm].[Co2Db_Campaign_Company](
        //[ID] [int] IDENTITY(1,1) NOT NULL,
        //[CampaignID] [int] NOT NULL,
        //[CompanyID] [int] NOT NULL,
        //[Status] [int] NULL,
        //[ResponsibleID] [uniqueidentifier] NULL,
        //[ResponsibleName] [nvarchar](50) NULL,
        //[ResponsibleEmail] [nvarchar](255) NULL,
        //[ContactPerson] [nvarchar](50) NULL,
        //[ContactedByPhone] [bit] NULL,
        //[MeetingHeld] [bit] NULL,
        //[CampaignSale] [bit] NULL,
        //[CampaigneSaleAmount] [money] NULL,
        //[ExpectedSale] [bit] NULL,
        //[ExpectedSaleAmount] [money] NULL,
        //[Notes] [nvarchar](max) NULL,
        
        
        
        public static DataSet GetCampaignCompanys(int CampaignID)
        {
            DBAccess db = new DBAccess();
            db.AddInt("CampaignID", CampaignID);
            DataSet ds = db.ExecuteDataSet(_SQLSelectAll);
            return ds;
        }
        
        public static System.Collections.Generic.List<CampaignCompany> GetCampaignCompanyList(int CampaignID)
        {
            System.Collections.Generic.List<CampaignCompany> result = new System.Collections.Generic.List<CampaignCompany>();
            CampaignCompany rec = default(CampaignCompany);
            
            DBAccess db = new DBAccess();
            db.AddInt("CampaignID", CampaignID);
            
            SqlDataReader dr = default(SqlDataReader);
            try
            {
                dr = (System.Data.SqlClient.SqlDataReader) (db.ExecuteReader(_SQLSelectAll));
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        rec = new CampaignCompany();
                        Populate(dr, rec);
                        result.Add(rec);
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
        
        
        public static DataSet GetCampaignCompany_SearchDS(int CampaignID, int SupplierID, int Status)
        {
            DBAccess db = new DBAccess();
            db.AddInt("CampaignID", CampaignID);
            db.AddInt("SupplierID", SupplierID);
            db.AddInt("Status", Status);
            DataSet ds = db.ExecuteDataSet(_SQLCampaign_Search);
            return ds;
        }
        
        public static DataSet GetCampaignCompanyDS(int ID)
        {
            DBAccess db = new DBAccess();
            db.AddInt("ID", ID);
            DataSet ds = db.ExecuteDataSet(_SQLSelectID);
            return ds;
        }
        
        public static CampaignCompany GetCampaignCompany(int ID)
        {
            DBAccess db = new DBAccess();
            CampaignCompany rec = new CampaignCompany();
            db.AddInt("ID", ID);
            SqlDataReader dr = (SqlDataReader) (db.ExecuteReader(_SQLSelectOne));
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
        
        public static DataSet GetGetCampaignsByCampaignID(int CampaignID)
        {
            DBAccess db = new DBAccess();
            db.AddInt("CampaignID", CampaignID);
            DataSet ds = db.ExecuteDataSet(_SQLGetCampaignID);
            return ds;
        }
        
        public static DataSet GetGetCampaignsByCompanyID(int CompanyID)
        {
            DBAccess db = new DBAccess();
            db.AddInt("CompanyID", CompanyID);
            DataSet ds = db.ExecuteDataSet(_SQLGetCompanyID);
            return ds;
        }
        
        public static DataSet GetGetCampaignsByCampaignNo(string CampaignNo)
        {
            DBAccess db = new DBAccess();
            db.AddNVarChar("CampaignNo", CampaignNo, 50);
            DataSet ds = db.ExecuteDataSet(_SQLGetCampaignNo);
            return ds;
        }
        
        
#endregion
        
#endregion
        
#region  Tags
        
        public string Tags(string tekst)
        {
            return Tags(tekst, this);
        }
        public static string Tags(string tekst, RescueTekniq.BOL.CampaignCompany item)
        {
            string res = "";
            StringBuilder sb = new StringBuilder();
            sb.Append(tekst);
            sb.Replace("[CAMPAIGNCOMPANY.STATUS]", item.StatusText);
            
            sb.Replace("[CAMPAIGNCOMPANY.CAMPAIGNNO]", "[CAMPAIGN.CAMPAIGNNO]");
            sb.Replace("[CAMPAIGNCOMPANY.CAMPAIGNNAME]", "[CAMPAIGN.NAME]");
            sb.Replace("[CAMPAIGNCOMPANY.CAMPAIGNDESCRIPTION]", "[CAMPAIGN.DESCRIPTION]");
            sb.Replace("[CAMPAIGNCOMPANY.STARTDATE]", "[CAMPAIGN.STARTDATE]");
            sb.Replace("[CAMPAIGNCOMPANY.ENDDATE]", "[CAMPAIGN.ENDDATE]");
            sb.Replace("[CAMPAIGNCOMPANY.EXPECTEDIMEDIATESALE]", "[CAMPAIGN.EXPECTEDIMEDIATESALE]");
            sb.Replace("[CAMPAIGNCOMPANY.EXPECTEDFOLLOWUPSALE]", "[CAMPAIGN.EXPECTEDFOLLOWUPSALE]");
            
            sb.Replace("[CAMPAIGNCOMPANY.RESPONSIBLENAME]", item.ResponsibleName);
            sb.Replace("[CAMPAIGNCOMPANY.RESPONSIBLEEMAIL]", item.ResponsibleEmail);
            sb.Replace("[CAMPAIGNCOMPANY.CONTACTPERSON]", item.ContactPerson);
            sb.Replace("[CAMPAIGNCOMPANY.CONTACTEDBYPHONE]", System.Convert.ToString(item.ContactedByPhone ? "Ja" : "Nej"));
            sb.Replace("[CAMPAIGNCOMPANY.ITEMNMEETINGHELDO]", System.Convert.ToString(item.MeetingHeld ? "Ja" : "Nej"));
            
            sb.Replace("[CAMPAIGNCOMPANY.CAMPAIGNSALE]", System.Convert.ToString(item.CampaignSale ? "Ja" : "Nej"));
            sb.Replace("[CAMPAIGNCOMPANY.CAMPAIGNSALEAMOUNT]", item.CampaignSaleAmount.ToString("C"));
            
            sb.Replace("[CAMPAIGNCOMPANY.CAMPAIGNSALE]", System.Convert.ToString(item.ExpectedSale ? "JA" : "Nej"));
            sb.Replace("[CAMPAIGNCOMPANY.EXPECTEDSALEAMOUNT]", item.ExpectedSaleAmount.ToString("C"));
            
            sb.Replace("[CAMPAIGN.NOTES]", item.Notes);
            
            res = sb.ToString();
            res = item.Campaign.Tags(res);
            res = item.Company.Tags(res);
            
            return res;
        }
        
#endregion
        
        
    }
    
    //CREATE TABLE [vicjos1_sysadm].[Co2Db_Campaign_Company](
    //[ID] [int] IDENTITY(1,1) NOT NULL,
    //[CampaignID] [int] NOT NULL,
    //[CompanyID] [int] NOT NULL,
    //[Status] [int] NULL,
    //[ResponsibleID] [uniqueidentifier] NULL,
    //[ResponsibleName] [nvarchar](50) NULL,
    //[ResponsibleEmail] [nvarchar](255) NULL,
    //[ContactPerson] [nvarchar](50) NULL,
    //[ContactedByPhone] [bit] NULL,
    //[MeetingHeld] [bit] NULL,
    //[CampaignSale] [bit] NULL,
    //[CampaignSaleAmount] [money] NULL,
    //[ExpectedSale] [bit] NULL,
    //[ExpectedSaleAmount] [money] NULL,
    //[Notes] [nvarchar](max) NULL,
    
}
