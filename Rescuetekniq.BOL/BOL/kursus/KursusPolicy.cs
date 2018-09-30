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

namespace RescueTekniq.BOL
{
    
    public class KursusPolicy : BaseObject
    {
        
#region  New
        
        public KursusPolicy()
        {
            
        }
        
        public KursusPolicy(int ID)
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
        
        public KursusPolicy(string Title)
        {
            
            if (Title.Trim() != "")
            {
                DBAccess db = new DBAccess();
                db.AddNVarChar("Title", Title, 50);
                SqlDataReader dr = (SqlDataReader) (db.ExecuteReader(_SQLSelectByTitle));
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
        //SELECT [ID]
        //      ,[companyID]
        //      ,[status]
        //      ,[FirstAidTitle]
        //      ,[FirstAidAftale]
        //      ,[FirstAidAftaleText]
        //      ,[FirstAidLeverandor]
        //      ,[FirstAidLeverandorText]
        //      ,[FirstAidKursusType]
        //      ,[FirstAidKursusTypeText]
        //      ,[FirstAidAdminAftale]
        //      ,[FirstAidAdminAftaleText]
        //      ,[FirstAidRepetition]
        //      ,[FirstAidRepetitionText]
        //      ,[FirstAidMedarbInfo]
        //  FROM [vicjos1_Heart2Start].[vicjos1_sysadm].[vw_Co2Db_KursusPolicy]
        
        private int _CompanyID = -1;
        private KursusStatusEnum _Status = KursusStatusEnum.Initialize;
        
        private string _FirstAidTitle = "";
        private int _FirstAidAftale = 0;
        private string _FirstAidAftaleText = "";
        private int _FirstAidLeverandor = 0;
        private string _FirstAidLeverandorText = "";
        private int _FirstAidKursusType = 0;
        private string _FirstAidKursusTypeText = "";
        private int _FirstAidAdminAftale = 0;
        private string _FirstAidAdminAftaleText = "";
        private int _FirstAidRepetition = 0;
        private string _FirstAidRepetitionText = "";
        private string _FirstAidMedarbInfo = "";
        
        private RescueTekniq.BOL.Virksomhed _Virksomhed = new RescueTekniq.BOL.Virksomhed();
        private List<KursusDag> _KursusDage;
        private int _KursusDageCount = -1;
        
#endregion
        
#region  Private Metode
        
        
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
            }
        }
        
        public KursusStatusEnum Status
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
        
        public string FirstAidTitle
        {
            get
            {
                return _FirstAidTitle;
            }
            set
            {
                _FirstAidTitle = value;
            }
        }
        
        public int FirstAidAftale
        {
            get
            {
                return _FirstAidAftale;
            }
            set
            {
                _FirstAidAftale = value;
            }
        }
        public string FirstAidAftaleText
        {
            get
            {
                return _FirstAidAftaleText;
            }
        }
        
        public int FirstAidLeverandor
        {
            get
            {
                return _FirstAidLeverandor;
            }
            set
            {
                _FirstAidLeverandor = value;
            }
        }
        public string FirstAidLeverandorText
        {
            get
            {
                return _FirstAidLeverandorText;
            }
        }
        
        public int FirstAidKursusType
        {
            get
            {
                return _FirstAidKursusType;
            }
            set
            {
                _FirstAidKursusType = value;
            }
        }
        public string FirstAidKursusTypeText
        {
            get
            {
                return _FirstAidKursusTypeText;
            }
        }
        
        public int FirstAidAdminAftale
        {
            get
            {
                return _FirstAidAdminAftale;
            }
            set
            {
                _FirstAidAdminAftale = value;
            }
        }
        public string FirstAidAdminAftaleText
        {
            get
            {
                return _FirstAidAdminAftaleText;
            }
        }
        
        public int FirstAidRepetition
        {
            get
            {
                return _FirstAidRepetition;
            }
            set
            {
                _FirstAidRepetition = value;
            }
        }
        public string FirstAidRepetitionText
        {
            get
            {
                return _FirstAidRepetitionText;
            }
        }
        
        public string FirstAidMedarbInfo
        {
            get
            {
                return _FirstAidMedarbInfo;
            }
            set
            {
                _FirstAidMedarbInfo = value;
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
                        if (!_Virksomhed.loaded)
                        {
                            _Virksomhed = Virksomhed.GetCompany(_CompanyID);
                        }
                        else if (_Virksomhed.ID != _CompanyID)
                        {
                            _Virksomhed = Virksomhed.GetCompany(_CompanyID);
                        }
                    }
                }
                catch (Exception)
                {
                }
                return _Virksomhed;
            }
        }
        
        public List<KursusDag> KursesDage
        {
            get
            {
                try
                {
                    if (_KursusDageCount < 0)
                    {
                        _KursusDage = KursusDag.GetAllKursusDagList(ID);
                        _KursusDageCount = _KursusDage.Count;
                    }
                }
                catch (Exception)
                {
                    _KursusDageCount = 0;
                }
                return _KursusDage;
            }
        }
#endregion
        
#region  Shared Populate
        
        private static void AddParms(ref DBAccess db, KursusPolicy c)
        {
            var with_1 = c;
            db.AddInt("CompanyID", with_1.CompanyID);
            db.AddInt("Status", (System.Int32) with_1.Status);
            
            db.AddNVarChar("FirstAidTitle", with_1.FirstAidTitle, 250);
            db.AddInt("FirstAidAftale", with_1.FirstAidAftale);
            db.AddInt("FirstAidLeverandor", with_1.FirstAidLeverandor);
            db.AddInt("FirstAidKursusType", with_1.FirstAidKursusType);
            db.AddInt("FirstAidAdminAftale", with_1.FirstAidAdminAftale);
            db.AddInt("FirstAidRepetition", with_1.FirstAidRepetition);
            db.AddNVarChar("FirstAidMedarbInfo", with_1.FirstAidMedarbInfo, 50);
            
            AddParmsStandard(db, c);
        }
        
        //SELECT [ID]
        //      ,[companyID]
        //      ,[status]
        //      ,[FirstAidTitle]
        //      ,[FirstAidAftale]
        //      ,[FirstAidAftaleText]
        //      ,[FirstAidLeverandor]
        //      ,[FirstAidLeverandorText]
        //      ,[FirstAidKursusType]
        //      ,[FirstAidKursusTypeText]
        //      ,[FirstAidAdminAftale]
        //      ,[FirstAidAdminAftaleText]
        //      ,[FirstAidRepetition]
        //      ,[FirstAidRepetitionText]
        //      ,[FirstAidMedarbInfo]
        //  FROM [vicjos1_Heart2Start].[vicjos1_sysadm].[vw_Co2Db_KursusPolicy]
        
        private static void Populate(SqlDataReader dr, KursusPolicy c)
        {
            PopulateStandard(dr, c);
            var with_1 = c;
            with_1.CompanyID = System.Convert.ToInt32(dr.DBtoInt("CompanyID"));
            with_1.Status = (RescueTekniq.BOL.KursusStatusEnum) (dr.DBtoInt("Status"));
            with_1.FirstAidTitle = dr.DBtoString("FirstAidTitle");
            with_1.FirstAidAftale = System.Convert.ToInt32(dr.DBtoInt("FirstAidAftale"));
            with_1._FirstAidAftaleText = dr.DBtoString("FirstAidAftaleText");
            with_1.FirstAidLeverandor = System.Convert.ToInt32(dr.DBtoInt("FirstAidLeverandor"));
            with_1._FirstAidLeverandorText = dr.DBtoString("FirstAidLeverandorText");
            with_1.FirstAidKursusType = System.Convert.ToInt32(dr.DBtoInt("FirstAidKursusType"));
            with_1._FirstAidKursusTypeText = dr.DBtoString("FirstAidKursusTypeText");
            with_1.FirstAidAdminAftale = System.Convert.ToInt32(dr.DBtoInt("FirstAidAdminAftale"));
            with_1._FirstAidAdminAftaleText = dr.DBtoString("FirstAidAdminAftaleText");
            with_1.FirstAidRepetition = System.Convert.ToInt32(dr.DBtoInt("FirstAidRepetition"));
            with_1._FirstAidRepetitionText = dr.DBtoString("FirstAidRepetitionText");
            with_1.FirstAidMedarbInfo = dr.DBtoString("FirstAidMedarbInfo");
            
        }
        
#endregion
        
#region  Stored Procedure Names
        private const string _SQLDelete = "Co2Db_KursusPolicy_Delete";
        private const string _SQLInsert = "Co2Db_KursusPolicy_Insert";
        private const string _SQLUpdate = "Co2Db_KursusPolicy_Update";
        private const string _SQLSelectAll = "Co2Db_KursusPolicy_SelectAll";
        private const string _SQLSelectID = "Co2Db_KursusPolicy_SelectID";
        private const string _SQLSelectOne = "Co2Db_KursusPolicy_SelectOne";
        private const string _SQLSelectAllCompanyID = "Co2Db_KursusPolicy_SelectAllCompanyID";
        private const string _SQLSelectBySearch = "Co2Db_KursusPolicy_SelectBySearch";
        private const string _SQLSelectByTitle = "Co2Db_KursusPolicy_SelectByTitle";
        
        private const string _SQLSelectIDByCompanyID = "Co2Db_KursusPolicy_SelectIDByCompanyID";
        private const string _SQLSelectIDBySearch = "Co2Db_KursusPolicy_SelectIDBySearch";
        
        private const string _SQLCountByCompany = "Co2Db_KursusPolicy_CountByCompany";
        
#endregion
        
#region  Metoder
        
#region  Manipulate data
        
        public int Delete()
        {
            return Delete(System.Convert.ToInt32(this));
        }
        public static int Delete(int ID)
        {
            DBAccess db = new DBAccess();
            db.AddInt("ID", ID);
            int retval = db.ExecuteNonQuery(_SQLDelete);
            return retval;
        }
        public static int Delete(KursusPolicy c)
        {
            return Delete(c.ID);
        }
        
        public int Insert()
        {
            return Insert(this);
        }
        public static int Insert(KursusPolicy c)
        {
            DBAccess db = new DBAccess();
            if (c.Status == KursusStatusEnum.Initialize)
            {
                c.Status = KursusStatusEnum.Aktiv;
            }
            AddParms(ref db, c);
            SqlParameter objParam = new SqlParameter("@ID", 0);
            objParam.Direction = ParameterDirection.Output;
            db.Parameters.Add(objParam);
            
            int retval = db.ExecuteNonQuery(_SQLInsert);
            if (retval == 1)
            {
                c.ID = int.Parse(objParam.Value.ToString());
                return int.Parse(objParam.Value.ToString());
            }
            else
            {
                return -1;
            }
        }
        public static int Insert(int CompanyID, KursusStatusEnum Status, string FirstAidTitle, int FirstAidAftale, int FirstAidLeverandor, int FirstAidKursusType, int FirstAidAdminAftale, int FirstAidRepetition, int FirstAidMedarbInfo)
        {
            KursusPolicy c = new KursusPolicy();
            c.CompanyID = CompanyID;
            c.Status = Status;
            c.FirstAidTitle = FirstAidTitle;
            c.FirstAidAftale = FirstAidAftale;
            c.FirstAidLeverandor = FirstAidLeverandor;
            c.FirstAidKursusType = FirstAidKursusType;
            c.FirstAidAdminAftale = FirstAidAdminAftale;
            c.FirstAidRepetition = FirstAidRepetition;
            c.FirstAidMedarbInfo = System.Convert.ToString(FirstAidMedarbInfo);
            
            return Insert(c);
        }
        
        public int Update()
        {
            return Update(this);
        }
        public static int Update(KursusPolicy c)
        {
            DBAccess db = new DBAccess();
            db.AddInt("ID", c.ID);
            AddParms(ref db, c);
            int retval = db.ExecuteNonQuery("Co2Db_KursusPolicy_Update");
            return retval;
        }
        public static int Update(int ID, int CompanyID, KursusStatusEnum Status, string FirstAidTitle, int FirstAidAftale, int FirstAidLeverandor, int FirstAidKursusType, int FirstAidAdminAftale, int FirstAidRepetition, int FirstAidMedarbInfo)
        {
            KursusPolicy c = new KursusPolicy(ID);
            c.CompanyID = CompanyID;
            if (Status == KursusStatusEnum.Initialize)
            {
                Status = KursusStatusEnum.Aktiv;
            }
            c.Status = Status;
            c.FirstAidTitle = FirstAidTitle;
            c.FirstAidAftale = FirstAidAftale;
            c.FirstAidLeverandor = FirstAidLeverandor;
            c.FirstAidKursusType = FirstAidKursusType;
            c.FirstAidAdminAftale = FirstAidAdminAftale;
            c.FirstAidRepetition = FirstAidRepetition;
            c.FirstAidMedarbInfo = System.Convert.ToString(FirstAidMedarbInfo);
            
            return Update(c);
        }
        
#endregion
        
#region  Get data
        
        public static DataSet GetAllKursusPolicy()
        {
            DBAccess db = new DBAccess();
            DataSet ds = db.ExecuteDataSet(_SQLSelectAll);
            return ds;
        }
        
        public static DataSet GetAllKursusPolicy(int CompanyID)
        {
            DBAccess db = new DBAccess();
            db.AddInt("CompanyID", CompanyID);
            DataSet ds = db.ExecuteDataSet(_SQLSelectAllCompanyID);
            return ds;
        }
        public static System.Collections.Generic.List<KursusPolicy> GetKursusPolicyList(int CompanyID)
        {
            System.Collections.Generic.List<KursusPolicy> result = new System.Collections.Generic.List<KursusPolicy>();
            int ID = -1;
            DBAccess db = new DBAccess();
            db.AddInt("CompanyID", CompanyID);
            
            SqlDataReader dr = default(SqlDataReader);
            try
            {
                dr = (System.Data.SqlClient.SqlDataReader) (db.ExecuteReader(_SQLSelectIDByCompanyID));
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ID = System.Convert.ToInt32(dr.DBtoInt("ID"));
                        result.Add(KursusPolicy.GetKursusPolicy(ID));
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
        
        public static int GetKursusPolicyCountByCompany(int CompanyID)
        {
            int res = 0;
            DBAccess db = new DBAccess();
            db.AddInt("CompanyID", CompanyID);
            res = Funktioner.ToInt(db.ExecuteScalar(_SQLCountByCompany));
            return res;
        }
        
        
        public static DataSet GetKursusPolicyDS(int ID)
        {
            DBAccess db = new DBAccess();
            db.AddInt("ID", ID);
            DataSet ds = db.ExecuteDataSet(_SQLSelectID);
            return ds;
        }
        
        public static KursusPolicy GetKursusPolicy(int ID)
        {
            DBAccess db = new DBAccess();
            db.AddInt("ID", ID);
            KursusPolicy c = new KursusPolicy();
            try
            {
                SqlDataReader dr = (SqlDataReader) (db.ExecuteReader(_SQLSelectOne));
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Populate(dr, c);
                    }
                    dr.Close();
                }
            }
            catch (Exception)
            {
            }
            return c;
        }
        public static KursusPolicy GetKursusPolicy(string Title)
        {
            DBAccess db = new DBAccess();
            KursusPolicy c = new KursusPolicy();
            db.AddInt("Title", System.Convert.ToInt32(Title));
            try
            {
                SqlDataReader dr = (SqlDataReader) (db.ExecuteReader(_SQLSelectByTitle));
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Populate(dr, c);
                    }
                }
                dr.Close();
            }
            catch (Exception)
            {
            }
            return c;
        }
        
        public static DataSet Search_KursusPolicy(string Search)
        {
            return Search_KursusPolicy(Search, KursusStatusEnum.Alle, System.Convert.ToInt32(KursusStatusEnum.Alle));
        }
        public static DataSet Search_KursusPolicy(string Search, KursusStatusEnum Status)
        {
            return Search_KursusPolicy(Search, Status, System.Convert.ToInt32(KursusStatusEnum.Alle));
        }
        public static DataSet Search_KursusPolicy(string Search, KursusStatusEnum Status, int CompanyID)
        {
            DataSet ds = new DataSet();
            DataSet dsTemp = new DataSet();
            bool flag = false;
            DBAccess db = new DBAccess();
            
            string[] arr = Search.Split(' ');
            foreach (string s in arr)
            {
                db.AddNVarChar("Search", s, 250);
                db.AddInt("Status", System.Convert.ToInt32(Status));
                db.AddInt("CompanyID", CompanyID);
                
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
        
        public static System.Collections.Generic.List<KursusPolicy> Search_KursusPolicyList(string search, KursusStatusEnum status, int CompanyID)
        {
            System.Collections.Generic.List<KursusPolicy> result = new System.Collections.Generic.List<KursusPolicy>();
            int ID = -1;
            DBAccess db = new DBAccess();
            DataSet ds = new DataSet();
            DataSet dsTemp = new DataSet();
            bool flag = false;
            
            db.AddInt("CompanyID", CompanyID);
            
            string[] arr = search.Split(' ');
            foreach (string s in arr)
            {
                db.AddNVarChar("Search", s, 250);
                db.AddInt("Status", System.Convert.ToInt32(status));
                db.AddInt("CompanyID", CompanyID);
                
                dsTemp = db.ExecuteDataSet(_SQLSelectIDBySearch);
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
            
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                ID = System.Convert.ToInt32(row["ID"]);
                result.Add(KursusPolicy.GetKursusPolicy(ID));
            }
            
            return result;
        }
        
#endregion
        
#endregion
        
    }
    
    
}
