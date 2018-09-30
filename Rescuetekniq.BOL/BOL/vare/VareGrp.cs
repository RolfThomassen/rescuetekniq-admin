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
    
    //SELECT   ID, ParentID, VareGrpNr, VareGrpTekst
    //FROM Co2Db_VareGrp
    
    public class VareGrp : BaseObject
    {
        
#region  New
        
        public VareGrp()
        {
            
            _ParentID = 0;
            _VareGrpNr = "";
            _VareGrpTekst = "";
        }
        
        public VareGrp(int ID)
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
        //SELECT   ID, ParentID, VareGrpNr, VareGrpTekst
        //FROM Co2Db_VareGrp
        
        private int _ParentID = 0;
        private string _VareGrpNr = "";
        private string _VareGrpTekst = "";
        private string _Alias = "";
        
        private VareGrp _Parent; //= New VareGrp
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
                    _Parent = VareGrp.GetVareGrp(_ParentID);
                }
            }
        }
        
        public string VareGrpNr
        {
            get
            {
                return _VareGrpNr;
            }
            set
            {
                _VareGrpNr = value;
            }
        }
        
        public string VareGrpTekst
        {
            get
            {
                return _VareGrpTekst;
            }
            set
            {
                _VareGrpTekst = value;
            }
        }
        
        public string Alias
        {
            get
            {
                return _Alias;
            }
            set
            {
                _Alias = value;
            }
        }
        
        
        public RescueTekniq.BOL.VareGrp Parent
        {
            get
            {
                return _Parent;
            }
        }
        
#endregion
        
#region  Shared Populate
        
        private static void AddParms(ref DBAccess db, VareGrp c)
        {
            var with_1 = c;
            db.AddInt("ParentID", with_1.ParentID);
            db.AddNVarChar("VareGrpNr", with_1.VareGrpNr, 50);
            db.AddNVarChar("VareGrpTekst", with_1.VareGrpTekst, 50);
            db.AddNVarChar("Alias", with_1.Alias, 50);
            AddParmsStandard(db, c);
        }
        
        private static void Populate(SqlDataReader dr, VareGrp c)
        {
            var with_1 = c;
            with_1.ParentID = System.Convert.ToInt32(dr.DBtoInt("ParentID"));
            with_1.VareGrpNr = dr.DBtoString("VareGrpNr");
            with_1.VareGrpTekst = dr.DBtoString("VareGrpTekst");
            with_1.Alias = dr.DBtoString("Alias");
            PopulateStandard(dr, c);
        }
        
#endregion
        
#region  Stored Procedure Names
        private const string _SQLDelete = "Co2Db_VareGrp_Delete";
        private const string _SQLInsert = "Co2Db_VareGrp_Insert";
        private const string _SQLUpdate = "Co2Db_VareGrp_Update";
        private const string _SQLSelectAll = "Co2Db_VareGrp_SelectAll";
        private const string _SQLSelectAllList = "Co2Db_VareGrp_SelectAllList";
        private const string _SQLSelectByParent = "Co2Db_VareGrp_SelectAllByParent";
        private const string _SQLSelectID = "Co2Db_VareGrp_SelectID";
        private const string _SQLSelectOne = "Co2Db_VareGrp_SelectOne";
        private const string _SQLSelectBySearch = "Co2Db_VareGrp_SelectBySearch";
        private const string _SQLSelectByAlias = "Co2Db_VareGrp_SelectByAlias";
#endregion
        
#region  Metoder
        
#region  Manipulate Data
        
        public static int Delete(int ID)
        {
            DBAccess db = new DBAccess();
            db.AddInt("ID", ID);
            int retval = db.ExecuteNonQuery(_SQLDelete);
            return retval;
        }
        public static int Delete(VareGrp c)
        {
            return Delete(c.ID);
        }
        public int Delete()
        {
            return Delete(System.Convert.ToInt32(this));
        }
        
        public int Insert()
        {
            return Insert(this);
        }
        public static int Insert(VareGrp c)
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
	VareGrpNr, 
	string VareGrpTekst)
        {
            VareGrp c = new VareGrp();
            c.ParentID = ParentID;
            c.VareGrpNr = VareGrpNr;
            c.VareGrpTekst = VareGrpTekst;
            
            return Insert(c);
        }
        
        public int Update()
        {
            return Update(this);
        }
        public static int Update(VareGrp c)
        {
            DBAccess db = new DBAccess();
            
            db.AddInt("ID", c.ID);
            AddParms(ref db, c);
            
            int retval = db.ExecuteNonQuery(_SQLUpdate);
            return retval;
        }
        public static int Update(int ID, int ParentID, string VareGrpNr, string VareGrpTekst)
        {
            VareGrp c = new VareGrp(ID);
            c.ParentID = ParentID;
            c.VareGrpNr = VareGrpNr;
            c.VareGrpTekst = VareGrpTekst;
            
            return Update(c);
        }
        
        public int Save()
        {
            return Save(this);
        }
        public static int Save(VareGrp c)
        {
            int retval = 0;
            if (c.ID > 0)
            {
                retval = Update(c);
            }
            else
            {
                retval = Insert(c);
            }
            return retval;
        }
        
#endregion
        
#region  Get Data
        
        public static DataSet GetAllVareGrp()
        {
            DBAccess db = new DBAccess();
            DataSet ds = db.ExecuteDataSet(_SQLSelectAll);
            return ds;
        }
        public static DataSet GetAllVareGrpList()
        {
            DBAccess db = new DBAccess();
            DataSet ds = db.ExecuteDataSet(_SQLSelectAllList);
            return ds;
        }
        
        public static System.Collections.Generic.List<VareGrp> GetVareGrpList()
        {
            System.Collections.Generic.List<VareGrp> result = new System.Collections.Generic.List<VareGrp>();
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
                        ID = System.Convert.ToInt32(dr.DBtoInt("ID"));
                        result.Add(VareGrp.GetVareGrp(ID));
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
        
        
        public static DataSet GetVareGrpByParent(int ParentID)
        {
            DBAccess db = new DBAccess();
            db.AddInt("ParentID", ParentID);
            DataSet ds = db.ExecuteDataSet(_SQLSelectByParent);
            return ds;
        }
        
        public static DataSet GetVareGrpDS(int ID)
        {
            DBAccess db = new DBAccess();
            db.AddInt("ID", ID);
            DataSet ds = db.ExecuteDataSet(_SQLSelectID);
            return ds;
        }
        
        public static VareGrp GetVareGrp(int ID)
        {
            DBAccess db = new DBAccess();
            db.AddInt("ID", ID);
            SqlDataReader dr = (SqlDataReader) (db.ExecuteReader(_SQLSelectOne));
            if (dr.HasRows)
            {
                VareGrp c = new VareGrp();
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
        
        public static VareGrp GetVareGrpByAlias(string Alias)
        {
            DBAccess db = new DBAccess();
            db.AddNVarChar("Alias", Alias, 50);
            VareGrp c = new VareGrp();
            SqlDataReader dr = (SqlDataReader) (db.ExecuteReader(_SQLSelectByAlias));
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
        
        public static DataSet Search_VareGrp(string Search)
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
        
#endregion
        
#region  Tags
        
        public string Tags(string tekst)
        {
            return Tags(tekst, this);
        }
        public static string Tags(string tekst, RescueTekniq.BOL.VareGrp item)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(tekst);
            sb.Replace("[VARE.VAREGRP]", item.VareGrpNr);
            sb.Replace("[VARE.VAREGRPTEKST]", item.VareGrpTekst);
            return sb.ToString();
        }
        
#endregion
        
        
    }
    
    
}
