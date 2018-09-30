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
    
    //Namespace RescueTekniq.BOL
    
    //SELECT
    //	ID,
    //	Type,
    //	CompanyID,
    //	MedarbejderID,
    //	Status,
    //	Overskrift,
    //	Tekst,
    //	DokumentID,
    //	rettet,
    //	rettetAf,
    //	rettetIP
    //FROM vicjos1_sysadm.Co2Db_Noter AS note
    //WHERE (Deleted = 0)
    
    public class Noter : BaseObject
    {
        
#region  New
        
        public Noter()
        {
            ID = -1;
        }
        
        public Noter(int Type, int CompanyID, int MedarbejderID, int Status, string Overskrift, string Tekst) : this()
	        {
            this.Type = Type;
            this.CompanyID = CompanyID;
            this.MedarbejderID = MedarbejderID;
            this.Status = Status;
            this.Overskrift = Overskrift;
            this.Tekst = Tekst;
        }
        
#endregion
        
#region  Public Properties
        
        private int _Type = -1;
        public int Type
        {
            get
            {
                return _Type;
            }
            set
            {
                _Type = value;
            }
        }
        
        private int _CompanyID = -1;
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
        
        private int _MedarbejderID = -1;
        public int MedarbejderID
        {
            get
            {
                return _MedarbejderID;
            }
            set
            {
                _MedarbejderID = value;
            }
        }
        
        private int _Status = -1;
        public int Status
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
        
        private string _Overskrift = "";
        public string Overskrift
        {
            get
            {
                return _Overskrift;
            }
            set
            {
                _Overskrift = value;
            }
        }
        
        private string _Tekst = "";
        public string Tekst
        {
            get
            {
                return _Tekst;
            }
            set
            {
                _Tekst = value;
            }
        }
        
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
            db.Parameters.Add(new SqlParameter("@ID", ID));
            int retval = db.ExecuteNonQuery("Co2Db_Noter_Delete");
            AddLog(Status: "Noter", Logtext: string.Format("Delete Noter: ID:{0}", ID), Metode: "Delete");
            return retval;
        }
        public static int Delete(Noter n)
        {
            return Delete(n.ID);
        }
        
        
        public int Insert()
        {
            return Insert(this);
        }
        public static int Insert(Noter n)
        {
            DBAccess db = new DBAccess();
            SqlParameter ID = new SqlParameter("@ID", 0);
            ID.Direction = ParameterDirection.Output;
            
            db.Parameters.Add(new SqlParameter("@Type", Funktioner.ToInt(n.Type)));
            db.Parameters.Add(new SqlParameter("@CompanyID", Funktioner.ToInt(n.CompanyID)));
            db.Parameters.Add(new SqlParameter("@MedarbejderID", Funktioner.ToInt(n.MedarbejderID)));
            db.Parameters.Add(new SqlParameter("@Status", Funktioner.ToInt(n.Status)));
            
            db.Parameters.Add(new SqlParameter("@Overskrift", SQLfunctions.SQLstr(n.Overskrift)));
            db.Parameters.Add(new SqlParameter("@Tekst", SQLfunctions.SQLstr(n.Tekst)));
            
            db.Parameters.Add(new SqlParameter("@RettetAf", SQLfunctions.SQLstr(CurrentUserName)));
            db.Parameters.Add(new SqlParameter("@RettetIP", SQLfunctions.SQLstr(CurrentUserIP)));
            db.Parameters.Add(ID);
            int retval = db.ExecuteNonQuery("Co2Db_Noter_Insert");
            if (retval == 1)
            {
                n.ID = int.Parse(ID.Value.ToString());
                AddLog(Status: "Noter", Logtext: string.Format("Create Noter: ID:{0}", n.ID), Metode: "Insert");
                return n.ID; //Integer.Parse(ID.Value.ToString)
            }
            else
            {
                AddLog(Status: "Noter", Logtext: string.Format("Failure to Create Noter:"), logtype: LogTypeEnum.Error, Metode: "Insert");
                return -1;
            }
            
        }
        public static int Insert(int Type, int CompanyID, int MedarbejderID, int Status, string Overskrift, string Tekst)
	        {
            Noter n = new Noter(Type, CompanyID, MedarbejderID, Status, Overskrift, Tekst);
            return Insert(n);
        }
        
        //SELECT
        //	ID,
        //	Type,
        //	CompanyID,
        //	MedarbejderID,
        //	Status,
        //	Overskrift,
        //	Tekst,
        //	DokumentID,
        //	rettet,
        //	rettetAf,
        //	rettetIP
        //FROM vicjos1_sysadm.Co2Db_Noter AS note
        //WHERE (Deleted = 0)
        
        //@Type int,
        //@CompanyID int,
        //@MedarbejderID int,
        //@Status int,
        //@Overskrift nvarchar(100),
        //@Tekst nvarchar(MAX),
        //@DokumentID int = -1,
        //@RettetAF nvarchar(50),
        //@RettetIP nvarchar(15),
        //@ID int OUTPUT
        
        public int Update()
        {
            return Update(this);
        }
        public static int Update(Noter n)
        {
            DBAccess db = new DBAccess();
            db.Parameters.Add(new SqlParameter("@ID", n.ID));
            
            db.Parameters.Add(new SqlParameter("@Type", Funktioner.ToInt(n.Type)));
            db.Parameters.Add(new SqlParameter("@CompanyID", Funktioner.ToInt(n.CompanyID)));
            db.Parameters.Add(new SqlParameter("@MedarbejderID", Funktioner.ToInt(n.MedarbejderID)));
            db.Parameters.Add(new SqlParameter("@Status", Funktioner.ToInt(n.Status)));
            
            db.Parameters.Add(new SqlParameter("@Overskrift", SQLfunctions.SQLstr(n.Overskrift)));
            db.Parameters.Add(new SqlParameter("@Tekst", SQLfunctions.SQLstr(n.Tekst)));
            
            db.Parameters.Add(new SqlParameter("@RettetAf", SQLfunctions.SQLstr(CurrentUserName)));
            db.Parameters.Add(new SqlParameter("@RettetIP", SQLfunctions.SQLstr(CurrentUserIP)));
            int retval = db.ExecuteNonQuery("Co2Db_Noter_Update");
            AddLog(Status: "Noter", Logtext: string.Format("Update Noter: ID:{0}", n.ID), Metode: "Update");
            return retval;
        }
        public static int Update(int 
	Id, int Type, int CompanyID, int MedarbejderID, int Status, string Overskrift, string Tekst)
        {
            Noter n = Noter.GetNoter(Id);
            n.Type = Type;
            n.CompanyID = CompanyID;
            n.MedarbejderID = MedarbejderID;
            n.Status = Status;
            n.Overskrift = Overskrift;
            n.Tekst = Tekst;
            return Update(n);
        }
        
        public int Save()
        {
            return Save(this);
        }
        public static int Save(Noter N)
        {
            int retval = 0;
            if (N.ID > 0)
            {
                retval = Update(N);
            }
            else
            {
                retval = Insert(N);
            }
            return retval;
        }
#endregion
        
#region  Get data
        
        public static DataSet GetNoterListe(int CompanyID = -1, int MedarbejderID = -1)
        {
            DBAccess db = new DBAccess();
            db.Parameters.Add(new SqlParameter("@CompanyID", Funktioner.ToInt(CompanyID)));
            db.Parameters.Add(new SqlParameter("@MedarbejderID", Funktioner.ToInt(MedarbejderID)));
            DataSet ds = db.ExecuteDataSet("Co2Db_Noter_SelectAll");
            return ds;
        }
        
        public static DataSet GetNote(int ID)
        {
            DBAccess db = new DBAccess();
            db.Parameters.Add(new SqlParameter("@ID", ID));
            DataSet ds = db.ExecuteDataSet("Co2Db_Noter_SelectOne");
            return ds;
        }
        
        public static DataSet GetNoterSet(int CompanyID = -1, int MedarbejderID = -1)
        {
            DBAccess db = new DBAccess();
            db.Parameters.Add(new SqlParameter("@CompanyID", Funktioner.ToInt(CompanyID)));
            db.Parameters.Add(new SqlParameter("@MedarbejderID", Funktioner.ToInt(MedarbejderID)));
            DataSet ds = db.ExecuteDataSet("Co2Db_Noter_SelectAll");
            return ds;
        }
        
        public static Noter GetNoter(int ID)
        {
            DBAccess db = new DBAccess();
            db.Parameters.Add(new SqlParameter("@ID", ID));
            SqlDataReader dr = (SqlDataReader) (db.ExecuteReader("Co2Db_Noter_SelectOne"));
            if (dr.HasRows)
            {
                Noter n = new Noter();
                while (dr.Read())
                {
                    PopulateNoter(dr, n);
                }
                dr.Close();
                return n;
            }
            else
            {
                dr.Close();
                return null;
            }
        }
        
        private static void PopulateNoter(SqlDataReader dr, Noter n)
        {
            var with_1 = n;
            with_1.ID = System.Convert.ToInt32(dr.DBtoInt("ID"));
            with_1.Type = System.Convert.ToInt32(dr.DBtoInt("Type"));
            with_1.CompanyID = System.Convert.ToInt32(dr.DBtoInt("CompanyID"));
            with_1.MedarbejderID = System.Convert.ToInt32(dr.DBtoInt("MedarbejderID"));
            with_1.Status = System.Convert.ToInt32(dr.DBtoInt("Status"));
            with_1.Overskrift = dr.DBtoString("Overskrift");
            with_1.Tekst = dr.DBtoString("Tekst");
            with_1.RettetAf = dr.DBtoString("RettetAF");
            with_1.RettetDen = System.Convert.ToDateTime(dr.DBtoDate("RettetDen"));
            with_1.RettetIP = dr.DBtoString("RettetIP");
        }
        
        public static int GetNoterCount(int CompanyID = -1, int MedarbejderID = -1)
        {
            DBAccess db = new DBAccess();
            object res = null;
            int result = 0;
            db.Parameters.Add(new SqlParameter("@CompanyID", Funktioner.ToInt(CompanyID)));
            db.Parameters.Add(new SqlParameter("@MedarbejderID", Funktioner.ToInt(MedarbejderID)));
            
            res = db.ExecuteScalar("Co2Db_Noter_GetCount");
            if ((!ReferenceEquals(res, null)) && Information.IsNumeric(res))
            {
                result = System.Convert.ToInt32(res);
            }
            return result;
        }
        
#endregion
        
#endregion
        
#region  Tags
        
        public string Tags(string tekst)
        {
            return Tags(tekst, this);
        }
        public static string Tags(string tekst, RescueTekniq.BOL.Noter item)
        {
            string res = "";
            StringBuilder sb = new StringBuilder();
            sb.Append(tekst);
            
            sb.Replace("[NOTE.OVERSKRIFT]", item.Overskrift);
            sb.Replace("[NOTE.TEKST]", item.Tekst);
            
            res = sb.ToString();
            //res = item.KundeGrpPris.Tags(res)
            //res = item.AED.Tags(res)
            
            return res;
        }
        
#endregion
        
        
        
    }
    
    //End Namespace
}
