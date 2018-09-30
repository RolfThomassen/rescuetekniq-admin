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
    
    //SELECT   ID, ParentID, NewsGrpNr, NewsGrpTekst
    //FROM Co2Db_NewsGrp
    
    public class NewsGrp : BaseObject
    {
        
#region  New
        
        public NewsGrp()
        {
            
            _ParentID = 0;
            _NewsGrpNr = "";
            _NewsGrpTekst = "";
        }
        
        public NewsGrp(int ID)
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
        
#region  Privates
        //SELECT   ID, ParentID, NewsGrpNr, NewsGrpTekst
        //FROM Co2Db_NewsGrp
        
        private int _ParentID = 0;
        private string _NewsGrpNr = "";
        private string _NewsGrpTekst = "";
        
        private NewsGrp _Parent; //= New NewsGrp
#endregion
        
#region  Properties
        
        public int ParentID
        {
            get
            {
                return _ParentID;
            }
            set
            {
                _ParentID = value;
                if (_ParentID > 0)
                {
                    _Parent = NewsGrp.GetNewsGrp(_ParentID);
                }
            }
        }
        
        public string NewsGrpNr
        {
            get
            {
                return _NewsGrpNr;
            }
            set
            {
                _NewsGrpNr = value;
            }
        }
        
        public string NewsGrpTekst
        {
            get
            {
                return _NewsGrpTekst;
            }
            set
            {
                _NewsGrpTekst = value;
            }
        }
        
        public RescueTekniq.BOL.NewsGrp Parent
        {
            get
            {
                return _Parent;
            }
        }
        
#endregion
        
#region  Shared Populate
        
        private static void AddParms(ref DBAccess db, NewsGrp c)
        {
            var with_1 = c;
            db.AddInt("ParentID", with_1.ParentID);
            db.AddNVarChar("NewsGrpNr", with_1.NewsGrpNr, 50);
            db.AddNVarChar("NewsGrpTekst", with_1.NewsGrpTekst, 50);
            AddParmsStandard(db, c);
        }
        
        private static void Populate(SqlDataReader dr, NewsGrp c)
        {
            var with_1 = c;
            with_1.ParentID = System.Convert.ToInt32(dr.DBtoInt("ParentID"));
            with_1.NewsGrpNr = dr.DBtoString("NewsGrpNr");
            with_1.NewsGrpTekst = dr.DBtoString("NewsGrpTekst");
            PopulateStandard(dr, c);
        }
        
#endregion
        
#region  Stored Procedure Names
        private const string _SQLDelete = "Co2Db_NewsGrp_Delete";
        private const string _SQLInsert = "Co2Db_NewsGrp_Insert";
        private const string _SQLUpdate = "Co2Db_NewsGrp_Update";
        private const string _SQLSelectAll = "Co2Db_NewsGrp_SelectAll";
        private const string _SQLSelectAllList = "Co2Db_NewsGrp_SelectAllList";
        private const string _SQLSelectByParent = "Co2Db_NewsGrp_SelectAllByParent";
        private const string _SQLSelectID = "Co2Db_NewsGrp_SelectID";
        private const string _SQLSelectOne = "Co2Db_NewsGrp_SelectOne";
        private const string _SQLSelectBySearch = "Co2Db_NewsGrp_SelectBySearch";
#endregion
        
#region  Metoder
        
        public static int Delete(int ID)
        {
            DBAccess db = new DBAccess();
            db.AddInt("ID", ID);
            int retval = db.ExecuteNonQuery(_SQLDelete);
            return retval;
        }
        public static int Delete(NewsGrp c)
        {
            return Delete(c.ID);
        }
        
        public static int Insert(NewsGrp c)
        {
            DBAccess db = new DBAccess();
            
            AddParms(ref db, c);
            
            SqlParameter objParam = new SqlParameter("@ID", 0);
            objParam.Direction = ParameterDirection.Output;
            db.Parameters.Add(objParam);
            int retval = db.ExecuteNonQuery(_SQLInsert);
            if (retval == 1)
            {
                return int.Parse(objParam.Value.ToString());
            }
            else
            {
                return -1;
            }
        }
        public static int Insert(int ParentID, string 
	NewsGrpNr, 
	string NewsGrpTekst)
        {
            NewsGrp c = new NewsGrp();
            c.ParentID = ParentID;
            c.NewsGrpNr = NewsGrpNr;
            c.NewsGrpTekst = NewsGrpTekst;
            
            return Insert(c);
        }
        
        public static int Update(NewsGrp c)
        {
            DBAccess db = new DBAccess();
            
            db.AddInt("ID", c.ID);
            AddParms(ref db, c);
            
            int retval = db.ExecuteNonQuery(_SQLUpdate);
            return retval;
        }
        public static int Update(int ID, int ParentID, string NewsGrpNr, string NewsGrpTekst)
        {
            NewsGrp c = new NewsGrp(ID);
            c.ParentID = ParentID;
            c.NewsGrpNr = NewsGrpNr;
            c.NewsGrpTekst = NewsGrpTekst;
            
            return Update(c);
        }
        
        public static DataSet GetAllNewsGrp()
        {
            DBAccess db = new DBAccess();
            DataSet ds = db.ExecuteDataSet(_SQLSelectAll);
            return ds;
        }
        public static DataSet GetAllNewsGrpList()
        {
            DBAccess db = new DBAccess();
            DataSet ds = db.ExecuteDataSet(_SQLSelectAllList);
            return ds;
        }
        
        public static DataSet GetNewsGrpByParent(int ParentID)
        {
            DBAccess db = new DBAccess();
            db.AddInt("ParentID", ParentID);
            DataSet ds = db.ExecuteDataSet(_SQLSelectByParent);
            return ds;
        }
        
        public static DataSet GetNewsGrpDS(int ID)
        {
            DBAccess db = new DBAccess();
            db.AddInt("ID", ID);
            DataSet ds = db.ExecuteDataSet(_SQLSelectID);
            return ds;
        }
        
        public static NewsGrp GetNewsGrp(int ID)
        {
            DBAccess db = new DBAccess();
            db.AddInt("ID", ID);
            SqlDataReader dr = (SqlDataReader) (db.ExecuteReader(_SQLSelectOne));
            if (dr.HasRows)
            {
                NewsGrp c = new NewsGrp();
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
        
        public static DataSet Search_NewsGrp(string Search)
        {
            DataSet ds = new DataSet();
            DataSet dsTemp = new DataSet();
            bool flag = false;
            DBAccess db = new DBAccess();
            
            string[] arr = Search.Split(' ');
            foreach (string s in arr)
            {
                db.AddNVarChar("Search", s, 50);
                
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
        
#endregion
        
        
    }
    
    
}
