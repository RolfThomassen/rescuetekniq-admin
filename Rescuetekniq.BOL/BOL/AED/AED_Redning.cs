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
    
    public class AED_Redning : BaseObject
    {
        
#region  New
        
        public AED_Redning()
        {
            
        }
        
        public AED_Redning(int ID)
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
        
#endregion
        
#region  Privates
        
        private AEDStatusEnum _Status = AEDStatusEnum.Aktiv;
        private int _CompanyID;
        private int _AED_FK;
        private int _RedningType;
        private DateTime _RedningDate;
        private string _RedningNote;
        
        private AED _AED = new AED();
        
#endregion
        
#region  Properties
        public AEDStatusEnum Status
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
        
        public int AED_FK
        {
            get
            {
                return _AED_FK;
            }
            set
            {
                _AED_FK = value;
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
        
        public int RedningType
        {
            get
            {
                return _RedningType;
            }
            set
            {
                _RedningType = value;
            }
        }
        public string RedningTypeText
        {
            get
            {
                return Combobox.GetTitle("RedningType", RedningType);
            }
        }
        
        public DateTime RedningDate
        {
            get
            {
                return _RedningDate;
            }
            set
            {
                _RedningDate = value;
            }
        }
        
        public string RedningNote
        {
            get
            {
                return _RedningNote;
            }
            set
            {
                _RedningNote = value;
            }
        }
        
        public string AEDmodel
        {
            get
            {
                return AED.SerialNo;
            }
        }
        
        public RescueTekniq.BOL.AED AED
        {
            get
            {
                try
                {
                    if (_AED_FK > 0)
                    {
                        if (!_AED.loaded)
                        {
                            _AED = RescueTekniq.BOL.AED.GetAED(_AED_FK);
                        }
                        else if (_AED.ID != _AED_FK)
                        {
                            _AED = RescueTekniq.BOL.AED.GetAED(_AED_FK);
                        }
                    }
                }
                catch (Exception)
                {
                }
                return _AED;
            }
        }
        
#endregion
        
#region  Shared Populate
        
        private static void AddParms(ref DBAccess db, AED_Redning c)
        {
            var with_1 = c;
            db.AddInt("Status", (System.Int32) with_1.Status);
            db.AddInt("AED_FK", with_1.AED_FK);
            db.AddInt("CompanyID", with_1.CompanyID);
            db.AddInt("RedningType", with_1.RedningType);
            db.AddDateTime("RedningDate", with_1.RedningDate);
            db.AddNVarChar("RedningNote", with_1.RedningNote, -1);
            AddParmsStandard(db, c);
        }
        
        private static void Populate(System.Data.SqlClient.SqlDataReader dr, AED_Redning c)
        {
            var with_1 = c;
            with_1.Status = (RescueTekniq.BOL.AEDStatusEnum) (dr.DBtoInt("Status"));
            with_1.AED_FK = System.Convert.ToInt32(dr.DBtoInt("AED_FK"));
            with_1.CompanyID = System.Convert.ToInt32(dr.DBtoInt("CompanyID"));
            
            with_1.RedningType = System.Convert.ToInt32(dr.DBtoInt("RedningType"));
            with_1.RedningDate = System.Convert.ToDateTime(dr.DBtoDate("RedningDate"));
            with_1.RedningNote = dr.DBtoString("RedningNote");
            PopulateStandard(dr, c);
        }
        
#endregion
        
#region  Stored Procedure Names
        
        private const string _SQLDelete = "Co2Db_AED_Redning_Delete";
        private const string _SQLInsert = "Co2Db_AED_Redning_Insert";
        private const string _SQLUpdate = "Co2Db_AED_Redning_Update";
        private const string _SQLSelectAll = "Co2Db_AED_Redning_SelectAll";
        private const string _SQLSelectID = "Co2Db_AED_Redning_SelectID";
        private const string _SQLSelectOne = "Co2Db_AED_Redning_SelectOne";
        private const string _SQLSelectBySearch = "Co2Db_AED_Redning_SelectBySearch";
        private const string _SQLSelectByCompany = "Co2Db_AED_Redning_SelectByCompany";
        private const string _SQLSelectByAED = "Co2Db_AED_Redning_SelectByAED";
        
#endregion
        
#region  Metoder
        
#region  Manipulate data
        
        public int Delete()
        {
            return Delete(System.Convert.ToInt32(this));
        }
        public static int Delete(int ID)
        {
            AED_Redning R = new AED_Redning();
            R.ID = ID;
            return Delete(R);
        }
        public static int Delete(AED_Redning R)
        {
            DBAccess db = new DBAccess();
            db.AddInt("ID", R.ID);
            int retval = db.ExecuteNonQuery(_SQLDelete);
            AddLog(Status: "AED_Redning", Logtext: string.Format("Delete AED_Redning: ID:{0}", R.ID), Metode: "Delete");
            return retval;
        }
        
        public int Insert()
        {
            return Insert(this);
        }
        public static int Insert(AED_Redning R)
        {
            DBAccess db = new DBAccess();
            
            AddParms(ref db, R);
            
            SqlParameter objParam = new SqlParameter("@ID", 0);
            objParam.Direction = ParameterDirection.Output;
            db.Parameters.Add(objParam);
            int retval = db.ExecuteNonQuery(_SQLInsert);
            if (retval == 1)
            {
                int res = Funktioner.ToInt(objParam.Value.ToString());
                R.ID = res;
                AddLog(Status: "AED_Redning", Logtext: string.Format("Create AED_Redning: ID:{0}", R.ID), Metode: "Insert");
                return res;
            }
            else
            {
                AddLog(Status: "AED_Redning", Logtext: string.Format("Failure to Create AED_Redning:"), logtype: LogTypeEnum.Error, Metode: "Insert");
                return -1;
            }
        }
        public static int Insert(AEDStatusEnum Status, int AED_FK, int CompanyID, int RedningType, DateTime RedningDate, string RedningNote)
        {
            AED_Redning c = new AED_Redning();
            c.Status = Status;
            c.AED_FK = AED_FK;
            c.CompanyID = CompanyID;
            
            c.RedningType = RedningType;
            c.RedningDate = RedningDate;
            c.RedningNote = RedningNote;
            
            return Insert(c);
        }
        
        public int Update()
        {
            return Update(this);
        }
        public static int Update(AED_Redning R)
        {
            DBAccess db = new DBAccess();
            
            db.AddInt("ID", R.ID);
            AddParms(ref db, R);
            
            int retval = db.ExecuteNonQuery(_SQLUpdate);
            AddLog(Status: "AED_Redning", Logtext: string.Format("Update AED_Redning: ID:{0}", R.ID), Metode: "Update");
            return retval;
        }
        public static int Update(int ID, AEDStatusEnum Status, int AED_FK, int CompanyID, int RedningType, DateTime RedningDate, string RedningNote)
        {
            AED_Redning c = new AED_Redning(ID);
            c.Status = Status;
            c.AED_FK = AED_FK;
            c.CompanyID = CompanyID;
            
            c.RedningType = RedningType;
            c.RedningDate = RedningDate;
            c.RedningNote = RedningNote;
            
            return Update(c);
        }
        
        public int Save()
        {
            return Save(this);
        }
        public static int Save(AED_Redning R)
        {
            int retval = 0;
            if (R.ID > 0)
            {
                retval = Update(R);
            }
            else
            {
                retval = Insert(R);
            }
            return retval;
        }
        
#endregion
        
#region  Get data
        
        public static AED_Redning GetRedning(int ID)
        {
            AED_Redning c = new AED_Redning();
            DBAccess db = new DBAccess();
            db.AddInt("ID", ID);
            SqlDataReader dr = (SqlDataReader) (db.ExecuteReader(_SQLSelectOne));
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
        public static DataSet GetRedningDS(int ID)
        {
            DBAccess db = new DBAccess();
            db.AddInt("ID", ID);
            DataSet ds = db.ExecuteDataSet(_SQLSelectID);
            return ds;
        }
        
        public static System.Collections.Generic.List<AED_Redning> GetRedningList(int AED_FK)
        {
            System.Collections.Generic.List<AED_Redning> result = new System.Collections.Generic.List<AED_Redning>();
            int ID = -1;
            DBAccess db = new DBAccess();
            db.AddInt("AED_FK", AED_FK);
            
            SqlDataReader dr = default(SqlDataReader);
            //Try
            dr = (System.Data.SqlClient.SqlDataReader) (db.ExecuteReader(_SQLSelectByAED));
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ID = System.Convert.ToInt32(dr.DBtoInt("ID"));
                    result.Add(AED_Redning.GetRedning(ID));
                }
            }
            dr.Close();
            //Catch ex As Exception
            //Throw ex
            //End Try
            
            return result;
        }
        public static System.Collections.Generic.List<AED_Redning> GetRedningListCompany(int CompanyID)
        {
            System.Collections.Generic.List<AED_Redning> result = new System.Collections.Generic.List<AED_Redning>();
            int ID = -1;
            DBAccess db = new DBAccess();
            db.AddInt("CompanyID", CompanyID);
            
            SqlDataReader dr = default(SqlDataReader);
            try
            {
                dr = (System.Data.SqlClient.SqlDataReader) (db.ExecuteReader(_SQLSelectByCompany));
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ID = System.Convert.ToInt32(dr.DBtoInt("ID"));
                        result.Add(AED_Redning.GetRedning(ID));
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
        
        public static DataSet GetAllRedning()
        {
            DBAccess db = new DBAccess();
            //db.AddInt("CompanyID", CompanyID)
            DataSet ds = db.ExecuteDataSet(_SQLSelectAll);
            return ds;
        }
        
        public static DataSet GetAllRedningByAED(int AED_FK)
        {
            DBAccess db = new DBAccess();
            db.AddInt("AED_FK", AED_FK);
            DataSet ds = db.ExecuteDataSet(_SQLSelectByAED);
            return ds;
        }
        
        public static DataSet GetAllRedningByCompany(int CompanyID)
        {
            DBAccess db = new DBAccess();
            db.AddInt("CompanyID", CompanyID);
            DataSet ds = db.ExecuteDataSet(_SQLSelectByCompany);
            return ds;
        }
        
        public static DataSet Search_Redning(string Search)
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
        
    }
    
    //Registrer redning.
    //
    //Dato for redning: ddmmåååå
    //Anvendt hjertestarter:
    //	Liste over kundens hjertestartere
    //Notefelt
    //	Beskrivelse af redningeng
    //
    //	|Register|		|Fortryd|
    //
    //(når der bliver trykket |registrer|
    //skal der komme "en side op hvor der står
    //	- tak for din registrering
    //	- der er sendt en mail til admin@RescueTekniq.dk om anvendelsen af hjertestarteren
    //	- du vil blive kontaktet med henblik på udskiftning af batteri og elektroder
    //	- for at fremme ekspeditionen
    //
    //(når der bliver trykket |registrer|
    //	- skal der sendes en mail til admin@RescueTekniq.dk
    //	- denne mail skal indeholde følgende informationer)
    //
    //		Overskrift:
    //			Anmeldelse af redning: "firmanavn"
    //
    //		Tekst:
    //			Der er foretaget en redning ved
    //				- maskine nr. "nr. angives"
    //				- administrator "navn"
    //				- "telefon nr."
    //				- "mail"
    //				- skal kontaktes med henblik på fremsendelse af nye elektroder og nyt batteri.
    //
}
