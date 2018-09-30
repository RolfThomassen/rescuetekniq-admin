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
    
    public class News : BaseObject
    {
        
#region  New
        
        public News()
        {
            
        }
        
        public News(int ID)
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
        
        public News(string Title)
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
        
        private int _NewsGrpID = 1;
        private NewsStatusEnum _Status = NewsStatusEnum.Initialize;
        private string _Title = "";
        private string _Teaser = "";
        private string _Body = "";
        private string _LinkUrl = "";
        private int _LinkTarget = 0;
        private DateTime _PublishDate;
        private DateTime _ExpiredDate;
        
        private RescueTekniq.BOL.NewsGrp _NewsGrp = new RescueTekniq.BOL.NewsGrp();
#endregion
        
#region  Private Metode
        
        
#endregion
        
#region  Properties
        
        public int NewsGrpID
        {
            get
            {
                return _NewsGrpID;
            }
            set
            {
                _NewsGrpID = value;
            }
        }
        
        public NewsStatusEnum Status
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
        
        public string Title
        {
            get
            {
                return _Title;
            }
            set
            {
                _Title = value;
            }
        }
        public string Teaser
        {
            get
            {
                return _Teaser;
            }
            set
            {
                _Teaser = value;
            }
        }
        public string Body
        {
            get
            {
                return _Body;
            }
            set
            {
                _Body = value;
            }
        }
        
        public string LinkUrl
        {
            get
            {
                return _LinkUrl;
            }
            set
            {
                _LinkUrl = value;
            }
        }
        
        public int LinkTarget
        {
            get
            {
                return _LinkTarget;
            }
            set
            {
                _LinkTarget = value;
            }
        }
        
        public DateTime PublishDate
        {
            
            get
            {
                return _PublishDate;
            }
            set
            {
                _PublishDate = value;
            }
        }
        
        public DateTime ExpiredDate
        {
            
            get
            {
                return _ExpiredDate;
            }
            set
            {
                _ExpiredDate = value;
            }
        }
        
        public RescueTekniq.BOL.NewsGrp NewsGrp
        {
            get
            {
                //Try
                if (_NewsGrpID > 0)
                {
                    if (!_NewsGrp.loaded)
                    {
                        _NewsGrp = NewsGrp.GetNewsGrp(_NewsGrpID);
                    }
                    else if (_NewsGrp.ID != _NewsGrpID)
                    {
                        _NewsGrp = NewsGrp.GetNewsGrp(_NewsGrpID);
                    }
                }
                //'Catch ex As Exception
                //End Try
                return _NewsGrp;
            }
        }
        
#endregion
        
#region  Shared Populate
        
        private static void AddParms(ref DBAccess db, News c)
        {
            var with_1 = c;
            db.AddInt("NewsGrpID", with_1.NewsGrpID);
            db.AddInt("Status", (System.Int32) with_1.Status);
            db.AddNVarChar("Title", with_1.Title, 255);
            db.AddNVarChar("Teaser", with_1.Teaser, 1023);
            db.AddNVarChar("Body", with_1.Body, -1);
            db.AddNVarChar("LinkUrl", with_1.Title, 250);
            db.AddInt("LinkTarget", with_1.LinkTarget);
            db.AddDateTime("PublishDate", with_1.PublishDate);
            db.AddDateTime("ExpiredDate", with_1.ExpiredDate);
            AddParmsStandard(db, c);
        }
        
        private static void Populate(SqlDataReader dr, News c)
        {
            PopulateStandard(dr, c);
            var with_1 = c;
            with_1.NewsGrpID = System.Convert.ToInt32(dr.DBtoInt("NewsGrpID"));
            with_1.Status = (RescueTekniq.BOL.NewsStatusEnum) (dr.DBtoInt("Status"));
            with_1.Title = dr.DBtoString("Title");
            with_1.Teaser = dr.DBtoString("Teaser");
            with_1.Body = dr.DBtoString("Body");
            with_1.LinkTarget = System.Convert.ToInt32(dr.DBtoInt("LinkTarget"));
            with_1.LinkUrl = dr.DBtoString("LinkUrl");
            with_1.PublishDate = System.Convert.ToDateTime(dr.DBtoDate("PublishDate"));
            with_1.ExpiredDate = System.Convert.ToDateTime(dr.DBtoDate("ExpiredDate"));
        }
        
#endregion
        
#region  Stored Procedure Names
        private const string _SQLDelete = "Co2Db_News_Delete";
        private const string _SQLInsert = "Co2Db_News_Insert";
        private const string _SQLUpdate = "Co2Db_News_Update";
        private const string _SQLSelectAll = "Co2Db_News_SelectAll";
        private const string _SQLSelectID = "Co2Db_News_SelectID";
        private const string _SQLSelectOne = "Co2Db_News_SelectOne";
        private const string _SQLSelectAllNewsGrp = "Co2Db_News_SelectAllNewsGrp";
        private const string _SQLSelectBySearch = "Co2Db_News_SelectBySearch";
        private const string _SQLSelectByTitle = "Co2Db_News_SelectByTitle";
        
        private const string _SQLSelectIDByNewsGrp = "Co2Db_News_SelectIDByNewsGrp";
        private const string _SQLSelectIDBySearch = "Co2Db_News_SelectIDBySearch";
        
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
        public static int Delete(News c)
        {
            return Delete(c.ID);
        }
        
        public int Insert()
        {
            return Insert(this);
        }
        public static int Insert(News c)
        {
            DBAccess db = new DBAccess();
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
        public static int Insert(int NewsGrpID, NewsStatusEnum Status, string Title, string Teaser, string Body, string LinkUrl, int LinkTarget, DateTime PublishDate, DateTime ExpiredDate)
        {
            News c = new News();
            c.NewsGrpID = NewsGrpID;
            c.Status = NewsStatusEnum.Aktiv;
            c.Title = Title;
            c.Teaser = Teaser;
            c.Body = Body;
            c.LinkUrl = LinkUrl;
            c.LinkTarget = LinkTarget;
            c.PublishDate = PublishDate;
            c.ExpiredDate = ExpiredDate;
            
            return Insert(c);
        }
        
        public int Update()
        {
            return Update(this);
        }
        public static int Update(News c)
        {
            DBAccess db = new DBAccess();
            db.AddInt("ID", c.ID);
            AddParms(ref db, c);
            int retval = db.ExecuteNonQuery("Co2Db_News_Update");
            return retval;
        }
        public static int Update(int ID, int NewsGrpID, NewsStatusEnum Status, string Title, string Teaser, string Body, string LinkUrl, int LinkTarget, DateTime PublishDate, DateTime ExpiredDate)
        {
            News c = new News(ID);
            c.NewsGrpID = NewsGrpID;
            c.Status = NewsStatusEnum.Aktiv;
            c.Title = Title;
            c.Teaser = Teaser;
            c.Body = Body;
            c.LinkUrl = LinkUrl;
            c.LinkTarget = LinkTarget;
            c.PublishDate = PublishDate;
            c.ExpiredDate = ExpiredDate;
            
            return Update(c);
        }
        
#endregion
        
#region  Get data
        
        public static DataSet GetAllNews()
        {
            DBAccess db = new DBAccess();
            DataSet ds = db.ExecuteDataSet(_SQLSelectAll);
            return ds;
        }
        
        public static DataSet GetAllNewsNewsGrp(int NewsGrpID)
        {
            DBAccess db = new DBAccess();
            db.AddInt("NewsGrpID", NewsGrpID);
            DataSet ds = db.ExecuteDataSet(_SQLSelectAllNewsGrp);
            return ds;
        }
        public static System.Collections.Generic.List<News> GetNewsList(int NewsGrpID)
        {
            System.Collections.Generic.List<News> result = new System.Collections.Generic.List<News>();
            int ID = -1;
            DBAccess db = new DBAccess();
            db.AddInt("NewsGrpID", NewsGrpID);
            
            SqlDataReader dr = default(SqlDataReader);
            try
            {
                dr = (System.Data.SqlClient.SqlDataReader) (db.ExecuteReader(_SQLSelectIDByNewsGrp));
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ID = System.Convert.ToInt32(dr.DBtoInt("ID"));
                        result.Add(News.GetNews(ID));
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
        
        public static DataSet GetNewsDS(int ID)
        {
            DBAccess db = new DBAccess();
            db.AddInt("ID", ID);
            DataSet ds = db.ExecuteDataSet(_SQLSelectID);
            return ds;
        }
        
        public static News GetNews(int ID)
        {
            DBAccess db = new DBAccess();
            db.AddInt("ID", ID);
            News c = new News();
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
        public static News GetNews(string Title)
        {
            DBAccess db = new DBAccess();
            News c = new News();
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
        
        public static DataSet Search_News(string Search)
        {
            return Search_News(Search, NewsStatusEnum.Alle, System.Convert.ToInt32(NewsStatusEnum.Alle));
        }
        public static DataSet Search_News(string Search, NewsStatusEnum Status)
        {
            return Search_News(Search, Status, System.Convert.ToInt32(NewsStatusEnum.Alle));
        }
        public static DataSet Search_News(string Search, NewsStatusEnum Status, int NewsGrpID)
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
                db.AddInt("NewsGrpID", NewsGrpID);
                
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
        
        public static System.Collections.Generic.List<News> Search_NewsList(string search, NewsStatusEnum status, int NewsGrpID)
        {
            System.Collections.Generic.List<News> result = new System.Collections.Generic.List<News>();
            int ID = -1;
            DBAccess db = new DBAccess();
            DataSet ds = new DataSet();
            DataSet dsTemp = new DataSet();
            bool flag = false;
            
            db.AddInt("NewsGrpID", NewsGrpID);
            
            string[] arr = search.Split(' ');
            foreach (string s in arr)
            {
                db.AddNVarChar("Search", s, 250);
                db.AddInt("Status", System.Convert.ToInt32(status));
                db.AddInt("NewsGrpID", NewsGrpID);
                
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
                result.Add(News.GetNews(ID));
            }
            
            return result;
        }
        
        
        
#endregion
        
        //[vicjos1_sysadm].[Co2Db_News](
        //	[ID] [int] IDENTITY(1,1) NOT NULL,
        //	[NewsGrpID] [int] NOT NULL,
        //	[Status] [int] NULL,
        //	[Title] [nvarchar](255) NULL,
        //	[Teaser] [nvarchar](1023) NULL,
        //	[Body] [nvarchar](max) NULL,
        //	[LinkUrl] [nvarchar](250) NULL,
        //	[LinkTarget] [int] NULL,
        //	[PublishDate] [datetime] NULL,
        //	[ExpiredDate] [datetime] NULL,
        //	[OprettetAf] [nvarchar](50) NULL,
        //	[OprettetDen] [datetime] NULL,
        //	[OprettetIP] [nvarchar](15) NULL,
        //	[RettetAf] [nvarchar](50) NULL,
        //	[RettetDen] [datetime] NULL,
        //	[RettetIP] [nvarchar](15) NULL,
#endregion
        
    }
    
    
}
