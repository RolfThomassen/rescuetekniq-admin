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
    
    public enum PageContentStatusType
    {
        All = -3,
        Deleted = -2,
        Initialize, //-1
        Create, //0
        Aktiv, //1
        Expired
    }
    
    public class PageContent : BaseObject
    {
        
#region  New
        
        /// <summary>
        /// Init New Content record
        /// </summary>
        /// <remarks></remarks>
        public PageContent()
        {
            // VBConversions Note: Non-static class variable initialization is below.  Class variables cannot be initially assigned non-static values in C#.
            _PublishDate = DateTime.Today;
            
        }
        /// <summary>
        /// Get Content by ID
        /// </summary>
        /// <param name="ID"></param>
        /// <remarks></remarks>
        public PageContent(int ID)
        {
            // VBConversions Note: Non-static class variable initialization is below.  Class variables cannot be initially assigned non-static values in C#.
            _PublishDate = DateTime.Today;
            
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
        /// <summary>
        /// Get New Content by GUID
        /// </summary>
        /// <param name="GUID"></param>
        /// <remarks></remarks>
        public PageContent(System.Guid GUID)
        {
            // VBConversions Note: Non-static class variable initialization is below.  Class variables cannot be initially assigned non-static values in C#.
            _PublishDate = DateTime.Today;
            
            
            if (GUID != System.Guid.Empty)
            {
                DBAccess db = new DBAccess();
                db.AddGuid("GUID", GUID);
                SqlDataReader dr = (SqlDataReader) (db.ExecuteReader(_SQLSelectByGuid));
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
        /// <summary>
        /// Get new Content by Title
        /// </summary>
        /// <param name="Title">Title of Content</param>
        /// <remarks></remarks>
        public PageContent(string Title)
        {
            // VBConversions Note: Non-static class variable initialization is below.  Class variables cannot be initially assigned non-static values in C#.
            _PublishDate = DateTime.Today;
            
            
            if (Guid != System.Guid.Empty)
            {
                DBAccess db = new DBAccess();
                
                db.AddInt("Title", System.Convert.ToInt32(Title));
                SqlDataReader dr = (SqlDataReader) (db.ExecuteReader(_SQLSelectByTitle));
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
        
#region  Privates
        
        private RescueTekniq.BOL.PageContentStatusType _Status = PageContentStatusType.Aktiv;
        private bool _IsActive = true;
        private bool _IsDeleted = false;
        private int _PageType;
        private int _LocalCultureID;
        private string _Alias;
        private string _Title;
        private bool _ShowTitle = false;
        private string _SubTitle;
        private string _Description;
        private string _KeyWords;
        private string _Content;
        private DateTime _PublishDate; // VBConversions Note: Initial value cannot be assigned here since it is non-static.  Assignment has been moved to the class constructors.
        private DateTime _ExpiredDate = DateTime.MaxValue;
        
#endregion
        
#region  Properties
        
        public PageContentStatusType Status
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
                return PageContentStatusType.GetName(_Status.GetType(), _Status);
            }
        }
        
        public bool IsActive
        {
            get
            {
                return _IsActive;
            }
            set
            {
                _IsActive = value;
            }
        }
        
        public bool IsDeleted
        {
            get
            {
                return _IsDeleted;
            }
            set
            {
                _IsDeleted = value;
            }
        }
        
        public int PageType
        {
            get
            {
                return _PageType;
            }
            set
            {
                _PageType = value;
            }
        }
        
        public int LocalCultureID
        {
            get
            {
                return _LocalCultureID;
            }
            set
            {
                _LocalCultureID = value;
            }
        }
        
        public string Alias
        {
            get
            {
                return _Alias.Replace(" ", "_").ToLower();
            }
            set
            {
                _Alias = value.Replace(" ", "_").ToLower();
            }
        }
        
        public bool ShowTitle
        {
            get
            {
                return _ShowTitle;
            }
            set
            {
                _ShowTitle = value;
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
        
        public string SubTitle
        {
            get
            {
                return _SubTitle;
            }
            set
            {
                _SubTitle = value;
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
        
        public string KeyWords
        {
            get
            {
                return _KeyWords;
            }
            set
            {
                _KeyWords = value;
            }
        }
        
        public string Content
        {
            get
            {
                return _Content;
            }
            set
            {
                _Content = value;
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
        
#endregion
        
#region  Shared Populate
        
        
        //UPDATE [vicjos1_Heart2Start].[vicjos1_sysadm].[Co2Db_HtmlPageContent]
        //   SET [IsAktiv] = <IsAktiv, bit,>
        //      ,[IsDeleted] = <IsDeleted, bit,>
        //      ,[PageType] = <PageType, int,>
        //      ,[LocalCultureID] = <LocalCultureID, int,>
        //      ,[Alias] = <Alias, nvarchar(100),>
        //      ,[Title] = <Title, nvarchar(100),>
        //      ,[ShowTitle] = <ShowTitle, bit,>
        //      ,[SubTitle] = <SubTitle, nvarchar(100),>
        //      ,[Description] = <Description, nvarchar(250),>
        //      ,[KeyWords] = <KeyWords, nvarchar(250),>
        //      ,[Content] = <Content, nvarchar(max),>
        //      ,[PublishDate] = <PublishDate, datetime,>
        //      ,[ExpireDate] = <ExpireDate, datetime,>
        //      ,[OprettetAF] = <OprettetAF, nvarchar(50),>
        //      ,[OprettetDen] = <OprettetDen, datetime,>
        //      ,[OprettetIP] = <OprettetIP, nvarchar(15),>
        //      ,[RettetAF] = <RettetAF, nvarchar(50),>
        //      ,[RettetDen] = <RettetDen, datetime,>
        //      ,[RettetIP] = <RettetIP, nvarchar(15),>
        // WHERE <Search Conditions,,>
        
        private static void AddParms(ref DBAccess db, PageContent c)
        {
            var with_1 = c;
            db.AddInt("Status", (System.Int32) with_1.Status);
            
            db.AddBoolean("IsActiv", with_1.IsActive);
            
            db.AddInt("PageType", with_1.PageType);
            db.AddInt("LocalCultureID", with_1.LocalCultureID);
            
            db.AddNVarChar("Title", with_1.Title, 100);
            db.AddNVarChar("Alias", with_1.Alias, 100);
            db.AddBoolean("ShowTitle", with_1.ShowTitle);
            
            db.AddNVarChar("SubTitle", with_1.SubTitle, 100);
            db.AddNVarChar("Description", with_1.Description, 250);
            db.AddNVarChar("KeyWords", with_1.KeyWords, 250);
            
            db.AddNVarChar("Content", with_1.Content, -1);
            db.AddDateTime("PublishDate", with_1.PublishDate);
            db.AddDateTime("ExpiredDate", with_1.ExpiredDate);
            
            //
            AddParmsStandard(db, c);
        }
        
        protected static void Populate(System.Data.SqlClient.SqlDataReader dr, PageContent c)
        {
            PopulateStandard(dr, c);
            var with_1 = c;
            with_1.Status = (RescueTekniq.BOL.PageContentStatusType) (dr.DBtoInt("Status"));
            
            with_1.IsActive = System.Convert.ToBoolean(dr.DBtoBoolean("IsActive"));
            
            with_1.PageType = System.Convert.ToInt32(dr.DBtoInt("PageType"));
            with_1.LocalCultureID = System.Convert.ToInt32(dr.DBtoInt("LocalCultureID"));
            
            with_1.Title = dr.DBtoString("Title");
            with_1.Alias = dr.DBtoString("Alias");
            with_1.ShowTitle = System.Convert.ToBoolean(dr.DBtoBoolean("ShowTitle"));
            
            with_1.SubTitle = dr.DBtoString("SubTitle");
            with_1.Description = dr.DBtoString("Description");
            with_1.KeyWords = dr.DBtoString("KeyWords");
            
            with_1.Content = dr.DBtoString("Content");
            with_1.PublishDate = System.Convert.ToDateTime(dr.DBtoDate("PublishDate"));
            with_1.ExpiredDate = System.Convert.ToDateTime(dr.DBtoDate("ExpiredDate"));
            
        }
        
#endregion
        
#region  Stored Procedure Names
        
        private const string _SQLDelete = "Co2Db_HtmlPageContent_Delete";
        private const string _SQLInsert = "Co2Db_HtmlPageContent_Insert";
        private const string _SQLUpdate = "Co2Db_HtmlPageContent_Update";
        
        private const string _SQLSelectAll = "Co2Db_HtmlPageContent_SelectAll";
        private const string _SQLSelectID = "Co2Db_HtmlPageContent_SelectID";
        
        private const string _SQLSelectByAlias = "Co2Db_HtmlPageContent_SelectByAlias";
        private const string _SQLSelectByTitle = "Co2Db_HtmlPageContent_SelectByTitle";
        
        
        private const string _SQLSelectBySearch = "Co2Db_HtmlPageContent_SelectBySearch";
        private const string _SQLSelectByGuid = "Co2Db_HtmlPageContent_SelectByGuid";
        
        private const string _SQLSelectAllSoonExpired = "Co2Db_HtmlPageContent_SelectAllSoonExpired";
        private const string _SQLSelectAllExpiredEmail = "Co2Db_HtmlPageContent_SelectAllExpiredEmail";
        
#endregion
        
#region  Metoder
        
#region  Manipulate data
        
        public int Delete()
        {
            return Delete(this);
        }
        public static int Delete(PageContent rec)
        {
            return Delete(rec.ID);
        }
        public static int Delete(int ID)
        {
            DBAccess db = new DBAccess();
            db.AddInt("ID", ID);
            int retval = db.ExecuteNonQuery(_SQLDelete);
            AddLog(Status: "HtmlPageContent", Logtext: string.Format("Delete HtmlPageContent: ID:{0}", ID), Metode: "Delete");
            return retval;
        }
        
        public int Insert()
        {
            return Insert(this);
        }
        public static int Insert(PageContent rec)
        {
            DBAccess db = new DBAccess();
            AddParms(ref db, rec);
            
            SqlParameter objParam = new SqlParameter("@ID", 0);
            objParam.Direction = ParameterDirection.Output;
            db.Parameters.Add(objParam);
            
            int retval = db.ExecuteNonQuery(_SQLInsert);
            if (retval == 1)
            {
                rec.ID = int.Parse(objParam.Value.ToString());
                AddLog(Status: "HtmlPageContent", Logtext: string.Format("Create HtmlPageContent: ID:{0}", rec.ID), Metode: "Insert");
                return rec.ID;
            }
            else
            {
                AddLog(Status: "HtmlPageContent", Logtext: string.Format("Failure to Create HtmlPageContent:"), logtype: LogTypeEnum.Error, Metode: "Insert");
                return -1;
            }
        }
        
        public int Update()
        {
            return Update(this);
        }
        public static int Update(PageContent rec)
        {
            DBAccess db = new DBAccess();
            
            db.AddInt("ID", rec.ID);
            AddParms(ref db, rec);
            
            int retval = db.ExecuteNonQuery(_SQLUpdate);
            AddLog(Status: "HtmlPageContent", Logtext: string.Format("Update HtmlPageContent: ID:{0}", rec.ID), Metode: "Update");
            return retval;
        }
        
        public int Save()
        {
            return Save(this);
        }
        public static int Save(PageContent rec)
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
        
        //UPDATE [vicjos1_Heart2Start].[vicjos1_sysadm].[Co2Db_HtmlPageContent]
        //   SET [IsAktiv] = <IsAktiv, bit,>
        //      ,[IsDeleted] = <IsDeleted, bit,>
        //      ,[PageType] = <PageType, int,>
        //      ,[LocalCultureID] = <LocalCultureID, int,>
        //      ,[Alias] = <Alias, nvarchar(100),>
        //      ,[Title] = <Title, nvarchar(100),>
        //      ,[ShowTitle] = <ShowTitle, bit,>
        //      ,[SubTitle] = <SubTitle, nvarchar(100),>
        //      ,[Description] = <Description, nvarchar(250),>
        //      ,[KeyWords] = <KeyWords, nvarchar(250),>
        //      ,[Content] = <Content, nvarchar(max),>
        //      ,[PublishDate] = <PublishDate, datetime,>
        //      ,[ExpireDate] = <ExpireDate, datetime,>
        //      ,[OprettetAF] = <OprettetAF, nvarchar(50),>
        //      ,[OprettetDen] = <OprettetDen, datetime,>
        //      ,[OprettetIP] = <OprettetIP, nvarchar(15),>
        //      ,[RettetAF] = <RettetAF, nvarchar(50),>
        //      ,[RettetDen] = <RettetDen, datetime,>
        //      ,[RettetIP] = <RettetIP, nvarchar(15),>
        // WHERE <Search Conditions,,>
        
        public static PageContent GetPageContentByID(int id)
        {
            return GetPageContentByCriteria("", "[ID]=@ID", new SqlParameter("@ID", id));
        }
        public static PageContent GetPageContentByCriteria(string OrderBY, string criteria, params SqlParameter[] @params)
        {
            List<PageContent> list = GetPageContentsByCriteria(OrderBY, criteria, @params);
            if (list.Count > 0)
            {
                return list[0];
            }
            else
            {
                return new PageContent();
            }
        }
        public static List<PageContent> GetPageContentsByCriteria(string OrderBY, string criteria, params SqlParameter[] @params)
        {
            DBAccess db = new DBAccess();
            //Dim conn As SqlConnection = DataFunctions.GetConnection()
            string query = "";
            query += "SELECT ";
            if (OrderBY != "")
            {
                query += " TOP (100) PERCENT ";
            }
            query += " * FROM vw_Co2Db_HtmlPageContent ";
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
            
            db.Open();
            SqlDataReader dr = (SqlDataReader) (db.ExecuteReader());
            
            List<PageContent> list = new List<PageContent>();
            while (dr.Read())
            {
                PageContent pc = new PageContent();
                PageContent.Populate(dr, pc);
                list.Add(pc);
            }
            
            db.Dispose();
            
            return list;
        }
        public static DataSet GetPageContentsByCriteriaDS(string fieldnames, string criteria, params SqlParameter[] @params)
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
            query += " FROM vw_Co2Db_HtmlPageContent ";
            query += " WHERE ( @IsAgent = 0 OR ( @IsAgent = 1 AND [AgentID] = @AgentID ) )";
            if (criteria != "")
            {
                query += " AND (" + criteria + ")";
            }
            
            db.CommandType = CommandType.Text;
            db.CommandText = query;
            db.AddRange(@params);
            
            DataSet ds = db.ExecuteDataSet();
            return ds;
        }
        
        public static PageContent GetPageContent(int ID)
        {
            PageContent c = new PageContent();
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
        
        public static DataSet GetPageContentDS(int ID)
        {
            DBAccess db = new DBAccess();
            db.AddInt("ID", ID);
            DataSet ds = db.ExecuteDataSet(_SQLSelectID);
            return ds;
        }
        
        public static System.Collections.Generic.List<PageContent> GetPageContentList()
        {
            System.Collections.Generic.List<PageContent> result = new System.Collections.Generic.List<PageContent>();
            int ID = -1;
            DBAccess db = new DBAccess();
            
            SqlDataReader dr = default(SqlDataReader);
            try
            {
                dr = (System.Data.SqlClient.SqlDataReader) (db.ExecuteReader(_SQLSelectAll));
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        PageContent pc = new PageContent();
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
        
        public static DataSet GetPageContentDS()
        {
            DBAccess db = new DBAccess();
            DataSet ds = db.ExecuteDataSet(_SQLSelectAll);
            return ds;
        }
        
        public static DataSet GetPageContentByAliasDS(string Alias)
        {
            DBAccess db = new DBAccess();
            db.AddInt("Alias", System.Convert.ToInt32(Alias));
            DataSet ds = db.ExecuteDataSet(_SQLSelectByAlias);
            return ds;
        }
        public static DataSet GetPageContentByTitleDS(string Title)
        {
            DBAccess db = new DBAccess();
            db.AddInt("Title", System.Convert.ToInt32(Title));
            DataSet ds = db.ExecuteDataSet(_SQLSelectByTitle);
            return ds;
        }
        
        public static PageContent GetPageContentByAlias(string Alias)
        {
            DBAccess db = new DBAccess();
            PageContent pc = new PageContent();
            db.AddInt("Alias", System.Convert.ToInt32(Alias));
            
            SqlDataReader dr = (SqlDataReader) (db.ExecuteReader(_SQLSelectByAlias));
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
        public static PageContent GetPageContentByTitle(string Title)
        {
            DBAccess db = new DBAccess();
            PageContent pc = new PageContent();
            db.AddInt("Title", System.Convert.ToInt32(Title));
            
            SqlDataReader dr = (SqlDataReader) (db.ExecuteReader(_SQLSelectByTitle));
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
        
        
#endregion
        
#endregion
        
#region  Tags
        
        public string Tags(string tekst)
        {
            return Tags(tekst, this);
        }
        public static string Tags(string tekst, RescueTekniq.BOL.PageContent item)
        {
            string res = "";
            StringBuilder sb = new StringBuilder();
            //UPDATE [vicjos1_Heart2Start].[vicjos1_sysadm].[Co2Db_HtmlPageContent]
            //   SET [IsAktiv] = <IsAktiv, bit,>
            //      ,[IsDeleted] = <IsDeleted, bit,>
            //      ,[PageType] = <PageType, int,>
            //      ,[LocalCultureID] = <LocalCultureID, int,>
            //      ,[Alias] = <Alias, nvarchar(100),>
            //      ,[Title] = <Title, nvarchar(100),>
            //      ,[ShowTitle] = <ShowTitle, bit,>
            //      ,[SubTitle] = <SubTitle, nvarchar(100),>
            //      ,[Description] = <Description, nvarchar(250),>
            //      ,[KeyWords] = <KeyWords, nvarchar(250),>
            //      ,[Content] = <Content, nvarchar(max),>
            //      ,[PublishDate] = <PublishDate, datetime,>
            //      ,[ExpireDate] = <ExpireDate, datetime,>
            //      ,[OprettetAF] = <OprettetAF, nvarchar(50),>
            //      ,[OprettetDen] = <OprettetDen, datetime,>
            //      ,[OprettetIP] = <OprettetIP, nvarchar(15),>
            //      ,[RettetAF] = <RettetAF, nvarchar(50),>
            //      ,[RettetDen] = <RettetDen, datetime,>
            //      ,[RettetIP] = <RettetIP, nvarchar(15),>
            // WHERE <Search Conditions,,>
            
            sb.Append(tekst);
            //.Replace("[VARE.TYPE]", "PageContent")
            //.Replace("[PAGECONTENT.TYPE]", "[VARE.NAVN]")
            
            sb.Replace("[PAGECONTENT.PUBLISHDATE]", item.PublishDate.ToString("dd. MMM yyyy"));
            sb.Replace("[PAGECONTENT.PUBLISH.DATE]", item.PublishDate.ToString("dd. MMM yyyy"));
            
            sb.Replace("[PAGECONTENT.EXPIREDDATE]", item.ExpiredDate.ToString("dd MMMM yyyy"));
            sb.Replace("[PAGECONTENT.EXPIRED.DATE]", item.ExpiredDate.ToString("dd MMMM yyyy"));
            
            sb.Replace("[PAGECONTENT.LOCALCULTUREID]", item.LocalCultureID.ToString());
            sb.Replace("[PAGECONTENT.ALIAS]", item.Alias.ToString());
            sb.Replace("[PAGECONTENT.SHOWTITLE]", item.ShowTitle.ToString());
            sb.Replace("[PAGECONTENT.TITLE]", item.Title.ToString());
            sb.Replace("[PAGECONTENT.SUBTITLE]", item.SubTitle.ToString());
            sb.Replace("[PAGECONTENT.DESCRIPTION]", item.Description.ToString());
            sb.Replace("[PAGECONTENT.KEYWORDS]", item.KeyWords.ToString());
            sb.Replace("[PAGECONTENT.CONTENT]", item.Content.ToString());
            
            sb.Replace("[PAGECONTENT.GUID]", item.Guid.ToString());
            
            res = sb.ToString();
            
            return res;
        }
        
#endregion
        
    }
    
    
}
