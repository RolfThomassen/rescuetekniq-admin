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
    
    //Namespace RescueTekniq.BOL
    
    //---------------------------------------------------------------------------
    //	SELECT
    //		ID,
    //		Aktiv,
    //		IsDeleted,
    //		Title,
    //		Description,
    //		TemplateID,
    //		OpenDate,
    //		ExpireDate,
    //		SplashPageID,
    //		LoginPageID,
    //		FrontPageID,
    //		RettetAF,
    //		RettetDen,
    //		RettetIP
    //	FROM	vw_Co2Db_InfositeContents / Co2Db_DynamicSite
    //---------------------------------------------------------------------------
    //	SELECT
    //	ID	int
    //	Aktiv	bit
    //	IsDeleted	bit
    //	Title	nvarchar(50)
    //	Description	nvarchar(MAX)
    //	TemplateID	int
    //	OpenDate	datetime
    //	ExpireDate	datetime
    //	SplashPageID	int
    //	LoginPageID	int
    //	FrontPageID	int
    //	RettetAF	nvarchar(50)
    //	RettetDen	datetime
    //	RettetIP	nvarchar(15)
    //	FROM Co2Db_DynamicSite
    //---------------------------------------------------------------------------
    
    
    
    
    public class InfositeContents : BaseObject
    {
        
#region  --== New ==--
        
        public InfositeContents()
        {
            ID = -1;
        }
        
        public InfositeContents(int ID, bool Aktiv, bool IncludeInMenu, bool IsDeleted, bool IsDisabled, string PageName, string PageTitle, string Description, string Keywords, int Template_ID, int Category_ID, int LinkType_ID, string LinkUrl, int LinkPage_ID, string LinkTarget, DateTime OpenDate, DateTime ExpireDate) : this()
        {
            var with_1 = this;
            //	ID	int
            //	Aktiv	bit
            //	IsDeleted	bit
            //	Title	nvarchar(50)
            //	Description	nvarchar(MAX)
            //	TemplateID	int
            //	OpenDate	datetime
            //	ExpireDate	datetime
            //	SplashPageID	int
            //	LoginPageID	int
            //	FrontPageID	int
            //	RettetAF	nvarchar(50)
            //	RettetDen	datetime
            //	RettetIP	nvarchar(15)
            with_1.Aktiv = Aktiv;
            //.IsDeleted = IsDeleted
            
            //	.Title = Title
            with_1.Description = Description;
            
            with_1.Template_ID = Template_ID;
            
            with_1.OpenDate = OpenDate;
            with_1.ExpireDate = ExpireDate;
            
            //	.SplashPageID = SplashPageID
            //	.LoginPageID = LoginPageID
            //	.FrontPageID = FrontPageID
            
            
        }
        
#endregion
        
#region  --== Properties ==--
        
        private bool _Aktiv;
        public bool Aktiv
        {
            get
            {
                return _Aktiv;
            }
            set
            {
                _Aktiv = value;
            }
        }
        
        private bool _IsDeleted;
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
        
        private bool _IsDisabled;
        public bool IsDisabled
        {
            get
            {
                return _IsDisabled;
            }
            set
            {
                _IsDisabled = value;
            }
        }
        
        private int _Site_FK = -1;
        public int Site_FK
        {
            get
            {
                return _Site_FK;
            }
            set
            {
                _Site_FK = value;
            }
        }
        
        private int _Parent = 0;
        public int Parent_ID
        {
            get
            {
                return _Parent;
            }
            set
            {
                _Parent = value;
            }
        }
        
        private int _Sort = -1;
        public int Sort
        {
            get
            {
                return _Sort;
            }
            set
            {
                _Sort = value;
            }
        }
        
        private bool _IncludeInMenu = true;
        public bool IncludeInMenu
        {
            get
            {
                return _IncludeInMenu;
            }
            set
            {
                _IncludeInMenu = value;
            }
        }
        
        private bool _Forside = false;
        public bool Forside
        {
            get
            {
                return _Forside;
            }
            set
            {
                _Forside = value;
            }
        }
        
        private string _PageName = "";
        public string PageName
        {
            get
            {
                return _PageName;
            }
            set
            {
                _PageName = value;
            }
        }
        
        private string _PageTitle = "";
        public string PageTitle
        {
            get
            {
                return _PageTitle;
            }
            set
            {
                _PageTitle = value;
            }
        }
        
        private string _Description = "";
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
        
        private string _Keywords = "";
        public string Keywords
        {
            get
            {
                return _Keywords;
            }
            set
            {
                _Keywords = value;
            }
        }
        
        private int _Template = -1;
        public int Template_ID
        {
            get
            {
                return _Template;
            }
            set
            {
                _Template = value;
            }
        }
        
        private int _Category = -1;
        public int Category_ID
        {
            get
            {
                return _Category;
            }
            set
            {
                _Category = value;
            }
        }
        
        private int _LinkType = -1;
        public int LinkType_ID
        {
            get
            {
                return _LinkType;
            }
            set
            {
                _LinkType = value;
            }
        }
        
        private int _LinkPage_ID = -1;
        public int LinkPage_ID
        {
            get
            {
                return _LinkPage_ID;
            }
            set
            {
                _LinkPage_ID = value;
            }
        }
        
        private string _LinkUrl = "";
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
        
        private string _LinkTarget = "";
        public string LinkTarget
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
        
        private DateTime _OpenDate;
        public DateTime OpenDate
        {
            get
            {
                return _OpenDate;
            }
            set
            {
                _OpenDate = value;
            }
        }
        
        private DateTime _ExpireDate;
        public DateTime ExpireDate
        {
            get
            {
                return _ExpireDate;
            }
            set
            {
                _ExpireDate = value;
            }
        }
        
#endregion
        
#region  --== Public Metodes ==--
        
        public static int Delete(int ID)
        {
            DBAccess db = new DBAccess();
            db.Parameters.Add(new SqlParameter("@ID", ID));
            int retval = db.ExecuteNonQuery("Co2Db_InfositePages_Delete");
            return retval;
        }
        public static int Delete(InfositeContents isp)
        {
            return Delete(isp.ID);
        }
        
        
        private static void PopulateDBwithInfositePages(DBAccess db, InfositeContents po)
        {
            var with_1 = po;
            db.AddBoolean("@Aktiv", with_1.Aktiv);
            db.AddInt("Site_FK", with_1.Site_FK);
            db.AddInt("Parent_ID", with_1.Parent_ID);
            //	db.AddInt("Sort", .Sort)
            db.AddBoolean("@IncludeInMenu", with_1.IncludeInMenu);
            db.AddBoolean("@Forside", with_1.Forside);
            
            db.AddBoolean("@IsDisabled", with_1.IsDisabled);
            db.AddBoolean("@IsDeleted", with_1.IsDeleted);
            
            db.AddNVarChar("PageName", with_1.PageName, 50);
            db.AddNVarChar("PageTitle", with_1.PageTitle, 50);
            db.AddNVarChar("Description", with_1.Description, 250);
            db.AddNVarChar("Keywords", with_1.Keywords, 250);
            db.AddInt("Template_ID", with_1.Template_ID);
            db.AddInt("Category_ID", with_1.Category_ID);
            db.AddInt("LinkType_ID", with_1.LinkType_ID);
            db.AddNVarChar("LinkUrl", with_1.LinkUrl, 250);
            db.AddInt("LinkPage_ID", with_1.LinkPage_ID);
            db.AddNVarChar("LinkTarget", with_1.LinkTarget, 50);
            db.AddDateTime("OpenDate", with_1.OpenDate);
            db.AddDateTime("ExpireDate", with_1.ExpireDate);
            db.AddNVarChar("RettetAf", with_1.CurUser);
            db.AddNVarChar("RettetIP", with_1.CurIP);
        }
        
        public static int Insert(InfositeContents isp)
        {
            DBAccess db = new DBAccess();
            
            PopulateDBwithInfositePages(db, isp);
            
            SqlParameter objParam = new SqlParameter("@ID", 0);
            objParam.Direction = ParameterDirection.Output;
            db.Parameters.Add(objParam);
            
            int retval = db.ExecuteNonQuery("Co2Db_InfositePages_Insert");
            if (retval == 1)
            {
                isp.ID = int.Parse(objParam.Value.ToString());
                return isp.ID;
            }
            else
            {
                return -1;
            }
        }
        
        public static int Insert(bool Aktiv, int Site_FK, int Parent_ID, int Sort, bool IncludeInMenu, bool IsDeleted, bool IsDisabled, string PageName, string PageTitle, string Description, string Keywords, int Template_ID, int Category_ID, int LinkType_ID, string LinkUrl, int LinkPage_ID, string LinkTarget, DateTime OpenDate, DateTime ExpireDate)
        {
            InfositeContents isp = new InfositeContents();
            isp.Aktiv = Aktiv;
            isp.Site_FK = Site_FK;
            isp.Parent_ID = Parent_ID;
            isp.Sort = Sort;
            isp.IncludeInMenu = IncludeInMenu;
            isp.IsDisabled = IsDisabled;
            isp.IsDeleted = IsDeleted;
            isp.Forside = false; //Forside
            isp.PageName = PageName;
            isp.PageTitle = PageTitle;
            isp.Description = Description;
            isp.Keywords = Keywords;
            isp.Template_ID = Template_ID;
            isp.Category_ID = Category_ID;
            isp.LinkType_ID = LinkType_ID;
            isp.LinkUrl = LinkUrl;
            isp.LinkPage_ID = LinkPage_ID;
            isp.LinkTarget = LinkTarget;
            isp.OpenDate = OpenDate;
            isp.ExpireDate = ExpireDate;
            return Insert(isp);
        }
        
        public static int Update(InfositeContents isp)
        {
            DBAccess db = new DBAccess();
            var with_1 = isp;
            PopulateDBwithInfositePages(db, isp);
            db.AddInt("ID", with_1.ID);
            int retval = db.ExecuteNonQuery("Co2Db_InfositePages_Update");
            return retval;
        }
        
        public static int Update(int ID, bool Aktiv, int Site_FK, int Parent_ID, int Sort, bool IncludeInMenu, bool IsDeleted, bool IsDisabled, string PageName, string PageTitle, string Description, string Keywords, int Template_ID, int Category_ID, int LinkType_ID, string LinkUrl, int LinkPage_ID, string LinkTarget, DateTime OpenDate, DateTime ExpireDate)
        {
            InfositeContents isp = InfositeContents.GetInfositePages(ID);
            isp.Aktiv = Aktiv;
            isp.Site_FK = Site_FK;
            isp.Parent_ID = Parent_ID;
            isp.Sort = Sort;
            isp.IncludeInMenu = IncludeInMenu;
            isp.IsDeleted = IsDeleted;
            isp.IsDisabled = IsDisabled;
            isp.Forside = false; // Forside
            isp.PageName = PageName;
            isp.PageTitle = PageTitle;
            isp.Description = Description;
            isp.Keywords = Keywords;
            isp.Template_ID = Template_ID;
            isp.Category_ID = Category_ID;
            isp.LinkType_ID = LinkType_ID;
            isp.LinkUrl = LinkUrl;
            isp.LinkPage_ID = LinkPage_ID;
            isp.LinkTarget = LinkTarget;
            isp.OpenDate = OpenDate;
            isp.ExpireDate = ExpireDate;
            return Update(isp);
        }
        
        
        //Public Shared Function GetInfositePagesListe(Optional ByVal CompanyID As Integer = -1, Optional ByVal MedarbejderID As Integer = -1) As DataSet
        //	Dim db As DBAccess = New DBAccess
        //	db.AddInt("CompanyID", CompanyID)
        //	db.AddInt("MedarbejderID", MedarbejderID)
        //	'db.Parameters.Add(New SqlParameter("@CompanyID", ToInt(CompanyID)))
        //	'db.Parameters.Add(New SqlParameter("@MedarbejderID", ToInt(MedarbejderID)))
        //	Dim ds As DataSet = db.ExecuteDataSet("Co2Db_InfositePages_SelectAll")
        //	Return ds
        //End Function
        
        
        //Public Shared Function GetInfositePagesSet(Optional ByVal CompanyID As Integer = -1, Optional ByVal MedarbejderID As Integer = -1) As DataSet
        //	Dim db As DBAccess = New DBAccess
        //	db.Parameters.Add(New SqlParameter("@CompanyID", ToInt(CompanyID)))
        //	db.Parameters.Add(New SqlParameter("@MedarbejderID", ToInt(MedarbejderID)))
        //	Dim ds As DataSet = db.ExecuteDataSet("Co2Db_InfositePages_SelectAll")
        //	Return ds
        //End Function
        
        //	EXEC	@return_value = [vicjos1_sysadm].[Co2Db_InfositePages_ListPages]
        //		@Site_FK = 1,
        //		@Parent_ID = 2
        //	EXEC	@return_value = [vicjos1_sysadm].[Co2Db_InfositePages_ListPages]
        //		@Site_FK = 1,
        //		@Parent_ID = 0
        
        public static DataSet GetInfositePagesSet(int Site_FK, int Parent_ID, int SelfID, bool TopPage)
        {
            DBAccess db = new DBAccess();
            if (Site_FK != 1)
            {
                db.AddInt("Site_FK", Site_FK);
            }
            if (Parent_ID != 0)
            {
                db.AddInt("Parent_ID", Parent_ID);
            }
            if (SelfID > 0)
            {
                db.AddInt("SelfID", SelfID);
            }
            if (TopPage)
            {
                db.AddInt("TopPage", 0);
            }
            DataSet ds = db.ExecuteDataSet("Co2Db_InfositePages_ListPages");
            return ds;
        }
        public static DataSet GetInfositePagesSet(int Site_FK)
        {
            return GetInfositePagesSet(Site_FK, 0);
        }
        public static DataSet GetInfositePagesSet(int Site_FK, int Parent_ID)
        {
            return GetInfositePagesSet(Site_FK, Parent_ID, -1);
        }
        public static DataSet GetInfositePagesSet(int Site_FK, int Parent_ID, int SelfID)
        {
            return GetInfositePagesSet(Site_FK, Parent_ID, SelfID, false);
        }
        
        public static DataSet GetInfositePagesDS(int ID)
        {
            DBAccess db = new DBAccess();
            db.AddInt("ID", ID);
            DataSet ds = db.ExecuteDataSet("Co2Db_InfositePages_SelectOne");
            return ds;
        }
        
        public static InfositeContents GetInfositePages(int ID)
        {
            DBAccess db = new DBAccess();
            //db.Parameters.Add(New SqlParameter("@ID", ID))
            db.AddInt("ID", ID);
            SqlDataReader dr = (SqlDataReader) (db.ExecuteReader("Co2Db_InfositePages_SelectOne"));
            if (dr.HasRows)
            {
                InfositeContents isp = new InfositeContents();
                while (dr.Read())
                {
                    PopulateInfositePages(dr, isp);
                }
                dr.Close();
                return isp;
            }
            else
            {
                dr.Close();
                return null;
            }
        }
        
        private static void PopulateInfositePages(SqlDataReader dr, InfositeContents isp)
        {
            var with_1 = isp;
            with_1.ID = System.Convert.ToInt32(dr.DBtoInt("ID"));
            with_1.Aktiv = System.Convert.ToBoolean(dr.DBtoInt("Aktiv"));
            with_1.Site_FK = System.Convert.ToInt32(dr.DBtoInt("Site_FK"));
            with_1.Parent_ID = System.Convert.ToInt32(dr.DBtoInt("Parent_ID"));
            with_1.Sort = System.Convert.ToInt32(dr.DBtoInt("Sort"));
            with_1.IncludeInMenu = System.Convert.ToBoolean(dr.DBtoBool("IncludeInMenu"));
            with_1.IsDeleted = System.Convert.ToBoolean(dr.DBtoBool("IsDeleted"));
            with_1.IsDisabled = System.Convert.ToBoolean(dr.DBtoBool("IsDisabled"));
            with_1.Forside = System.Convert.ToBoolean(dr.DBtoBool("Forside"));
            with_1.PageName = dr.DBtoString("PageName");
            with_1.PageTitle = dr.DBtoString("PageTitle");
            with_1.Description = dr.DBtoString("Description");
            with_1.Keywords = dr.DBtoString("Keywords");
            with_1.Template_ID = System.Convert.ToInt32(dr.DBtoInt("Template_ID"));
            with_1.Category_ID = System.Convert.ToInt32(dr.DBtoInt("Category_ID"));
            with_1.LinkType_ID = System.Convert.ToInt32(dr.DBtoInt("LinkType_ID"));
            with_1.LinkUrl = dr.DBtoString("LinkUrl");
            with_1.LinkPage_ID = System.Convert.ToInt32(dr.DBtoInt("LinkPage_ID"));
            with_1.LinkTarget = dr.DBtoString("LinkTarget");
            with_1.OpenDate = System.Convert.ToDateTime(dr.DBtoDate("OpenDate"));
            with_1.ExpireDate = System.Convert.ToDateTime(dr.DBtoDate("ExpireDate"));
        }
        
        public static int MoveUp(int ID)
        {
            DBAccess db = new DBAccess();
            //db.AddParameter("@ID", ID)
            db.AddInt("ID", ID);
            return db.ExecuteNonQuery("Co2Db_InfositePages_MoveUp");
        }
        public static int MoveUp(InfositeContents c)
        {
            return MoveUp(c.ID);
        }
        
        public static int MoveLeft(int ID)
        {
            DBAccess db = new DBAccess();
            //db.AddParameter("@ID", ID)
            db.AddInt("ID", ID);
            return db.ExecuteNonQuery("Co2Db_InfositePages_MoveLeft");
        }
        public static int MoveLeft(InfositeContents c)
        {
            return MoveDown(c.ID);
        }
        
        public static int MoveRight(int ID)
        {
            DBAccess db = new DBAccess();
            //db.AddParameter("@ID", ID)
            db.AddInt("ID", ID);
            return db.ExecuteNonQuery("Co2Db_InfositePages_MoveRight");
        }
        public static int MoveRight(InfositeContents c)
        {
            return MoveDown(c.ID);
        }
        
        public static int MoveDown(int ID)
        {
            DBAccess db = new DBAccess();
            //db.AddParameter("@ID", ID)
            db.AddInt("ID", ID);
            return db.ExecuteNonQuery("Co2Db_InfositePages_MoveDown");
        }
        public static int MoveDown(InfositeContents c)
        {
            return MoveDown(c.ID);
        }
        
#endregion
        
        
    }
    
    //End Namespace
    
}
