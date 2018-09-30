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
    
    //SELECT ID, KundeGrpNavn, status, sort, OprettetAf, OprettetDen, OprettetIP, RettetAf, RettetDen, RettetIP
    //FROM Co2Db_KundeGrp
    
    public class KundeGrp : BaseObject
    {
        
#region  New
        
        public KundeGrp()
        {
            
            _Status = (KundeGrpStatusEnum) 0;
            _KundeGrpNavn = "";
            _Sort = 0;
        }
        
        public KundeGrp(int ID)
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
        //SELECT ID, KundeGrpNavn, status, sort, OprettetAf, OprettetDen, OprettetIP, RettetAf, RettetDen, RettetIP
        //FROM Co2Db_KundeGrp
        
        private KundeGrpStatusEnum _Status = KundeGrpStatusEnum.Aktiv;
        private int _Sort = 0;
        private string _KundeGrpNavn = "";
        
        //Private _KundeGrp_Priser As List(Of KundeGrp_Priser)
        
#endregion
        
#region  Properties
        //SELECT ID, KundeGrpNavn, status, sort, OprettetAf, OprettetDen, OprettetIP, RettetAf, RettetDen, RettetIP
        //FROM Co2Db_KundeGrp
        
        public KundeGrpStatusEnum Status
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
        
        public string KundeGrpNavn
        {
            get
            {
                return _KundeGrpNavn;
            }
            set
            {
                _KundeGrpNavn = value;
            }
        }
        
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
        
#endregion
        
#region  Shared Populate
        //SELECT ID, KundeGrpNavn, status, sort, OprettetAf, OprettetDen, OprettetIP, RettetAf, RettetDen, RettetIP
        //FROM Co2Db_KundeGrp
        
        private static void AddParms(ref DBAccess db, KundeGrp c)
        {
            var with_1 = c;
            db.AddInt("Status", (System.Int32) with_1.Status);
            db.AddNVarChar("KundeGrpNavn", with_1.KundeGrpNavn, 50);
            db.AddInt("Sort", with_1.Sort);
            AddParmsStandard(db, c);
        }
        
        private static void Populate(SqlDataReader dr, KundeGrp c)
        {
            var with_1 = c;
            with_1.Status = (RescueTekniq.BOL.KundeGrpStatusEnum) (dr.DBtoInt("Status"));
            with_1.KundeGrpNavn = dr.DBtoString("KundeGrpNavn");
            with_1.Sort = System.Convert.ToInt32(dr.DBtoInt("Sort"));
            PopulateStandard(dr, c);
        }
        
#endregion
        
#region  Stored Procedure Names
        private const string _SQLDelete = "Co2Db_KundeGrp_Delete";
        private const string _SQLInsert = "Co2Db_KundeGrp_Insert";
        private const string _SQLUpdate = "Co2Db_KundeGrp_Update";
        private const string _SQLSelectAll = "Co2Db_KundeGrp_SelectAll";
        private const string _SQLSelectID = "Co2Db_KundeGrp_SelectID";
        private const string _SQLSelectOne = "Co2Db_KundeGrp_SelectOne";
        
        private const string _SQLSelectBySearch = "Co2Db_KundeGrp_SelectBySearch";
        private const string _SQLSelectByCompany = "Co2Db_KundeGrp_SelectByCompany";
        
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
            return retval;
        }
        public static int Delete(KundeGrp c)
        {
            return Delete(c.ID);
        }
        
        public int Insert()
        {
            return Insert(this);
        }
        public static int Insert(KundeGrp c)
        {
            DBAccess db = new DBAccess();
            
            if (c.Status <= (int) KundeGrpStatusEnum.Opret)
            {
                c.Status = KundeGrpStatusEnum.Aktiv;
            }
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
        public static int Insert(string KundeGrpNavn, int Status, int Sort)
        {
            KundeGrp c = new KundeGrp();
            c.KundeGrpNavn = KundeGrpNavn;
            c.Status = (KundeGrpStatusEnum) Status;
            c.Sort = Sort;
            
            return Insert(c);
        }
        
        public int Update()
        {
            return Update(this);
        }
        public static int Update(KundeGrp c)
        {
            DBAccess db = new DBAccess();
            
            if (c.Status == KundeGrpStatusEnum.Opret)
            {
                c.Status = KundeGrpStatusEnum.Aktiv;
            }
            db.AddInt("ID", c.ID);
            AddParms(ref db, c);
            
            int retval = db.ExecuteNonQuery(_SQLUpdate);
            return retval;
        }
        public static int Update(int ID, string KundeGrpNavn, int Status, int Sort)
        {
            KundeGrp c = new KundeGrp(ID);
            c.KundeGrpNavn = KundeGrpNavn;
            c.Status = (KundeGrpStatusEnum) Status;
            c.Sort = Sort;
            
            return Update(c);
        }
        
#endregion
        
#region  Get Data
        
        public static DataSet GetAllKundeGrp()
        {
            DBAccess db = new DBAccess();
            DataSet ds = db.ExecuteDataSet(_SQLSelectAll);
            return ds;
        }
        
        public static DataSet GetKundeGrpDS(int ID)
        {
            DBAccess db = new DBAccess();
            db.AddInt("ID", ID);
            DataSet ds = db.ExecuteDataSet(_SQLSelectID);
            return ds;
        }
        
        public static System.Collections.Generic.List<KundeGrp> GetKundeGrpList()
        {
            System.Collections.Generic.List<KundeGrp> result = new System.Collections.Generic.List<KundeGrp>();
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
                        result.Add(KundeGrp.GetKundeGrp(ID));
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
        
        public static KundeGrp GetKundeGrp(int ID)
        {
            DBAccess db = new DBAccess();
            db.AddInt("ID", ID);
            SqlDataReader dr = (SqlDataReader) (db.ExecuteReader(_SQLSelectOne));
            if (dr.HasRows)
            {
                KundeGrp c = new KundeGrp();
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
        
        public static KundeGrp GetKundeGrp_Company(int CompanyID)
        {
            KundeGrp c = new KundeGrp();
            DBAccess db = new DBAccess();
            db.AddInt("CompanyID", CompanyID);
            SqlDataReader dr = (SqlDataReader) (db.ExecuteReader(_SQLSelectByCompany));
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
        
        public static DataSet Search_KundeGrp(string Search)
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
        public static string Tags(string tekst, RescueTekniq.BOL.KundeGrp item)
        {
            string res = "";
            StringBuilder sb = new StringBuilder();
            sb.Append(tekst);
            
            sb.Replace("[VARE.KUNDEGRP]", item.KundeGrpNavn);
            
            res = sb.ToString();
            return res;
        }
        
#endregion
        
    }
    
    
}
