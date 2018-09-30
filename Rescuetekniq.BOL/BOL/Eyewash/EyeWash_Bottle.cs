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
    
    public class EyeWash_Bottle : BaseObject
    {
        
#region  New
        
        public EyeWash_Bottle()
        {
            
        }
        public EyeWash_Bottle(int ID)
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
        public EyeWash_Bottle(System.Guid GUID)
        {
            
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
#endregion
        
#region  Privates
        
        private EyeWash_StatusEnum _Status = EyeWash_StatusEnum.Aktiv;
        private int _EYE_FK;
        private int _BottleType;
        private DateTime _BottleDeleveryDate;
        private DateTime _BottleExpireDate;
        private DateTime _BottleEmailSendt;
        
        private RescueTekniq.BOL.EyeWash _EYE = new RescueTekniq.BOL.EyeWash();
        private RescueTekniq.BOL.Vare _Vare = new RescueTekniq.BOL.Vare();
        private RescueTekniq.BOL.KundeGrp_Pris _kgp = new RescueTekniq.BOL.KundeGrp_Pris();
#endregion
        
#region  Properties
        public EyeWash_StatusEnum Status
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
                string res = "";
                res = Combobox.GetTitleByValue("EyeWash.Status.Enum", Status.ToString());
                return res;
            }
        }
        public int StatusNo
        {
            get
            {
                return Funktioner.ToInt(_Status);
            }
            set
            {
                _Status = (EyeWash_StatusEnum) value;
            }
        }
        
        public int EYE_FK
        {
            get
            {
                return _EYE_FK;
            }
            set
            {
                _EYE_FK = value;
            }
        }
        
        public int BottleType
        {
            get
            {
                return _BottleType;
            }
            set
            {
                _BottleType = value;
            }
        }
        public string BottleTypeText
        {
            get
            {
                string res = Vare.Navn;
                //Vare.GetVare(BottleType).Navn
                return res;
                //Combobox.GetTitle("BottleType", BottleType)
            }
        }
        
        public DateTime BottleDeleveryDate
        {
            get
            {
                return _BottleDeleveryDate;
            }
            set
            {
                _BottleDeleveryDate = value;
            }
        }
        
        public DateTime BottleExpireDate
        {
            get
            {
                return _BottleExpireDate;
            }
            set
            {
                _BottleExpireDate = value;
            }
        }
        
        public Nullable<DateTime> BottleEmailSendt
        {
            get
            {
                Nullable<DateTime> res = new Nullable<DateTime>();
                if (_BottleEmailSendt.ToBinary() != 0)
                {
                    res = _BottleEmailSendt;
                }
                return res; //_BottleEmailSendt
            }
            set
            {
                _BottleEmailSendt = value.Value;
            }
        }
        
        public string Firmanavn
        {
            get
            {
                return EYE.Virksomhed.Firmanavn;
            }
        }
        public string EYEmodel
        {
            get
            {
                return EYE.Vare.Navn;
            }
        }
        public string EYESerialNo
        {
            get
            {
                return EYE.SerialNo;
            }
        }
        
        public RescueTekniq.BOL.EyeWash EYE
        {
            get
            {
                try
                {
                    if (_EYE_FK > 0)
                    {
                        if (!_EYE.loaded)
                        {
                            _EYE = RescueTekniq.BOL.EyeWash.GetEyeWash(_EYE_FK);
                        }
                        else if (_EYE.ID != _EYE_FK)
                        {
                            _EYE = RescueTekniq.BOL.EyeWash.GetEyeWash(_EYE_FK);
                        }
                    }
                }
                catch (Exception)
                {
                }
                return _EYE;
            }
        }
        
        public RescueTekniq.BOL.Vare Vare
        {
            get
            {
                try
                {
                    if (BottleType > 0)
                    {
                        if (!_Vare.loaded)
                        {
                            _Vare = RescueTekniq.BOL.Vare.GetVare(BottleType);
                        }
                        else if (_Vare.ID != BottleType)
                        {
                            _Vare = RescueTekniq.BOL.Vare.GetVare(BottleType);
                        }
                    }
                }
                catch (Exception)
                {
                }
                return _Vare;
            }
        }
        
        
        public RescueTekniq.BOL.KundeGrp_Pris KundeGrpPris
        {
            get
            {
                try
                {
                    if (EYE.loaded && Vare.loaded)
                    {
                        if (!_kgp.loaded)
                        {
                            _kgp = RescueTekniq.BOL.KundeGrp_Pris.GetCompany_VarePris(EYE.CompanyID, Vare.ID);
                        }
                    }
                }
                catch (Exception)
                {
                }
                return _kgp;
            }
        }
#endregion
        
#region  Shared Populate
        
        private static void AddParms(ref DBAccess db, EyeWash_Bottle rec)
        {
            var with_1 = rec;
            db.AddInt("Status", (System.Int32) with_1.Status);
            db.AddInt("EYE_FK", with_1.EYE_FK);
            db.AddInt("BottleType", with_1.BottleType);
            db.AddDateTime("BottleDeleveryDate", with_1.BottleDeleveryDate);
            db.AddDateTime("BottleExpireDate", with_1.BottleExpireDate);
            db.AddDateTime("BottleEmailSendt", with_1.BottleEmailSendt);
            AddParmsStandard(db, rec);
        }
        
        private static void Populate(SqlDataReader dr, EyeWash_Bottle rec)
        {
            PopulateStandard(dr, rec);
            var with_1 = rec;
            with_1.Status = (RescueTekniq.BOL.EyeWash_StatusEnum) (dr.DBtoInt("Status"));
            with_1.EYE_FK = System.Convert.ToInt32(dr.DBtoInt("EYE_FK"));
            
            with_1.BottleType = System.Convert.ToInt32(dr.DBtoInt("BottleType"));
            with_1.BottleDeleveryDate = System.Convert.ToDateTime(dr.DBtoDate("BottleDeleveryDate"));
            with_1.BottleExpireDate = System.Convert.ToDateTime(dr.DBtoDate("BottleExpireDate"));
            with_1.BottleEmailSendt = dr.DBtoDate("BottleEmailSendt");
            
        }
        
#endregion
        
#region  Stored Procedure Names
        private const string _SQLDelete = "Co2Db_EYE_Bottle_Delete";
        private const string _SQLInsert = "Co2Db_EYE_Bottle_Insert";
        private const string _SQLUpdate = "Co2Db_EYE_Bottle_Update";
        private const string _SQLSelectAll = "Co2Db_EYE_Bottle_SelectAll";
        private const string _SQLSelectID = "Co2Db_EYE_Bottle_SelectID";
        private const string _SQLSelectOne = "Co2Db_EYE_Bottle_SelectOne";
        private const string _SQLSelectBySearch = "Co2Db_EYE_Bottle_SelectBySearch";
        private const string _SQLSelectByCompany = "Co2Db_EYE_Bottle_SelectByCompany";
        private const string _SQLSelectByEYE = "Co2Db_EYE_Bottle_SelectByEYE";
        private const string _SQLSelectByGuid = "Co2Db_EYE_Bottle_SelectByGUID";
        
        private const string _SQLSelectAllSoonExpired = "Co2Db_EYE_Bottle_SelectAllSoonExpired";
        private const string _SQLSelectAllExpiredEmail = "Co2Db_EYE_Bottle_SelectAllExpiredEmail";
        
#endregion
        
#region  Metoder
        
#region  Manipulate data
        
        public int Delete()
        {
            return Delete(System.Convert.ToInt32(this));
        }
        public static int Delete(int ID)
        {
            EyeWash_Bottle B = new EyeWash_Bottle() {
	                ID = ID
	            };
            return Delete(B);
        }
        public static int Delete(EyeWash_Bottle rec)
        {
            DBAccess db = new DBAccess();
            db.AddInt("ID", rec.ID);
            int retval = db.ExecuteNonQuery(_SQLDelete);
            AddLog(Status: "EyeWash_Bottle", Logtext: string.Format("Delete EyeWash_Bottle: ID:{0}", rec.ID), Metode: "Delete");
            return retval;
        }
        
        public int Insert()
        {
            return Insert(this);
        }
        public static int Insert(EyeWash_Bottle rec)
        {
            DBAccess db = new DBAccess();
            
            AddParms(ref db, rec);
            
            SqlParameter objParam = new SqlParameter("@ID", 0) {
	                Direction = ParameterDirection.Output
	            };
            db.Parameters.Add(objParam);
            int retval = db.ExecuteNonQuery(_SQLInsert);
            if (retval == 1)
            {
                rec.ID = int.Parse(objParam.Value.ToString());
                AddLog(Status: "EyeWash_Bottle", Logtext: string.Format("Create EyeWash_Bottle: ID:{0}", rec.ID), Metode: "Insert");
                return rec.ID;
            }
            else
            {
                AddLog(Status: "EyeWash_Bottle", Logtext: string.Format("Failure to Create EyeWash_Bottle:"), logtype: LogTypeEnum.Error, Metode: "Insert");
                return -1;
            }
        }
        
        
        public int Update()
        {
            return Update(this);
        }
        public static int Update(EyeWash_Bottle rec)
        {
            DBAccess db = new DBAccess();
            
            db.AddInt("ID", rec.ID);
            AddParms(ref db, rec);
            
            int retval = db.ExecuteNonQuery(_SQLUpdate);
            AddLog(Status: "EyeWash_Bottle", Logtext: string.Format("Update EyeWash_Bottle: ID:{0}", rec.ID), Metode: "Update");
            return retval;
        }
        
        
        //            Log.AddLog("EYE", String.Format("Create EYE: ID:{0} SNo:{1} ", c.ID, c.SerialNo))
        public int Save()
        {
            return Save(this);
        }
        public static int Save(EyeWash_Bottle rec)
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
        
#region  Get data
        
        public static EyeWash_Bottle GetBottle(int ID)
        {
            EyeWash_Bottle c = new EyeWash_Bottle();
            DBAccess db = new DBAccess();
            db.AddInt("ID", ID);
            
            db.AddBoolean("IsAgent", AdgangsKontrol.IsAgent());
            db.AddGuid("AgentID", CurrentUserID);
            
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
        public static EyeWash_Bottle GetBottle(System.Guid Guid)
        {
            EyeWash_Bottle c = new EyeWash_Bottle();
            DBAccess db = new DBAccess();
            db.AddGuid("Guid", Guid);
            
            db.AddBoolean("IsAgent", AdgangsKontrol.IsAgent());
            db.AddGuid("AgentID", CurrentUserID);
            
            SqlDataReader dr = (SqlDataReader) (db.ExecuteReader(_SQLSelectByGuid));
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
        public static DataSet GetBottleDS(int ID)
        {
            DBAccess db = new DBAccess();
            db.AddInt("ID", ID);
            
            db.AddBoolean("IsAgent", AdgangsKontrol.IsAgent());
            db.AddGuid("AgentID", CurrentUserID);
            
            DataSet ds = db.ExecuteDataSet(_SQLSelectID);
            return ds;
        }
        public static DataSet GetBottleDS(System.Guid Guid)
        {
            DBAccess db = new DBAccess();
            db.AddGuid("Guid", Guid);
            
            db.AddBoolean("IsAgent", AdgangsKontrol.IsAgent());
            db.AddGuid("AgentID", CurrentUserID);
            
            DataSet ds = db.ExecuteDataSet(_SQLSelectByGuid);
            return ds;
        }
        
        public static System.Collections.Generic.List<EyeWash_Bottle> GetBottleList(int EYE_FK)
        {
            System.Collections.Generic.List<EyeWash_Bottle> result = new System.Collections.Generic.List<EyeWash_Bottle>();
            int ID = -1;
            DBAccess db = new DBAccess();
            db.AddInt("EYE_FK", EYE_FK);
            
            db.AddBoolean("IsAgent", AdgangsKontrol.IsAgent());
            db.AddGuid("AgentID", CurrentUserID);
            
            SqlDataReader dr = default(SqlDataReader);
            //Try
            dr = (System.Data.SqlClient.SqlDataReader) (db.ExecuteReader(_SQLSelectByEYE));
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ID = System.Convert.ToInt32(dr.DBtoInt("ID"));
                    result.Add(EyeWash_Bottle.GetBottle(ID));
                }
            }
            dr.Close();
            //Catch ex As Exception
            //Throw ex
            //End Try
            
            return result;
        }
        
        
        public static DataSet GetAllBottle(int EYE_FK)
        {
            DBAccess db = new DBAccess();
            
            db.AddBoolean("IsAgent", AdgangsKontrol.IsAgent());
            db.AddGuid("AgentID", CurrentUserID);
            
            DataSet ds = db.ExecuteDataSet(_SQLSelectAll);
            return ds;
        }
        
        public static DataSet GetBottleByEYE(int EYE_FK)
        {
            DBAccess db = new DBAccess();
            db.AddInt("EYE_FK", EYE_FK);
            
            db.AddBoolean("IsAgent", AdgangsKontrol.IsAgent());
            db.AddGuid("AgentID", CurrentUserID);
            
            DataSet ds = db.ExecuteDataSet(_SQLSelectByEYE);
            return ds;
        }
        
        public static DataSet GetAllBottleByCompany(int CompanyID)
        {
            DBAccess db = new DBAccess();
            db.AddInt("CompanyID", CompanyID);
            
            db.AddBoolean("IsAgent", AdgangsKontrol.IsAgent());
            db.AddGuid("AgentID", CurrentUserID);
            
            DataSet ds = db.ExecuteDataSet(_SQLSelectByCompany);
            return ds;
        }
        
        public static DataSet GetBottleBySoonExpired(int CompanyID)
        {
            DBAccess db = new DBAccess();
            db.AddInt("CompanyID", CompanyID);
            
            db.AddBoolean("IsAgent", AdgangsKontrol.IsAgent());
            db.AddGuid("AgentID", CurrentUserID);
            
            DataSet ds = db.ExecuteDataSet(_SQLSelectAllSoonExpired);
            return ds;
        }
        public static System.Collections.Generic.List<EyeWash_Bottle> GetBottleListSoonExpired(int CompanyID)
        {
            System.Collections.Generic.List<EyeWash_Bottle> result = new System.Collections.Generic.List<EyeWash_Bottle>();
            int ID = -1;
            DBAccess db = new DBAccess();
            db.AddInt("CompanyID", CompanyID);
            
            db.AddBoolean("IsAgent", AdgangsKontrol.IsAgent());
            db.AddGuid("AgentID", CurrentUserID);
            
            SqlDataReader dr = default(SqlDataReader);
            //Try
            dr = (System.Data.SqlClient.SqlDataReader) (db.ExecuteReader(_SQLSelectAllSoonExpired));
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ID = System.Convert.ToInt32(dr.DBtoInt("ID"));
                    result.Add(EyeWash_Bottle.GetBottle(ID));
                }
            }
            dr.Close();
            //Catch ex As Exception
            //Throw ex
            //End Try
            
            return result;
        }
        
        public static System.Collections.Generic.List<EyeWash_Bottle> GetBottleListExpiredEmail(int CompanyID)
        {
            System.Collections.Generic.List<EyeWash_Bottle> result = new System.Collections.Generic.List<EyeWash_Bottle>();
            int ID = -1;
            DBAccess db = new DBAccess();
            db.AddInt("CompanyID", CompanyID);
            
            db.AddBoolean("IsAgent", AdgangsKontrol.IsAgent());
            db.AddGuid("AgentID", CurrentUserID);
            
            SqlDataReader dr = default(SqlDataReader);
            try
            {
                dr = (System.Data.SqlClient.SqlDataReader) (db.ExecuteReader(_SQLSelectAllExpiredEmail));
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ID = System.Convert.ToInt32(dr.DBtoInt("ID"));
                        result.Add(EyeWash_Bottle.GetBottle(ID));
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
        
        public static DataSet Search_Bottle(string Search)
        {
            DataSet ds = new DataSet();
            DataSet dsTemp = new DataSet();
            bool flag = false;
            DBAccess db = new DBAccess();
            
            string[] arr = Search.Split(' ');
            foreach (string s in arr)
            {
                db.AddNVarChar("Search", s, 50);
                
                db.AddBoolean("IsAgent", AdgangsKontrol.IsAgent());
                db.AddGuid("AgentID", CurrentUserID);
                
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
        public static string Tags(string tekst, RescueTekniq.BOL.EyeWash_Bottle item)
        {
            string res = "";
            StringBuilder sb = new StringBuilder();
            sb.Append(tekst);
            sb.Replace("[VARE.TYPE]", "Bottle");
            sb.Replace("[BOTTLE.MODEL]", "[VARE.NAVN]");
            sb.Replace("[BOTTLE.TYPE]", "[VARE.NAVN]");
            
            sb.Replace("[EXPIREDATE]", item.BottleExpireDate.ToString("d. MMMM yyyy"));
            sb.Replace("[BOTTLE.EXPIREDATE]", item.BottleExpireDate.ToString("d. MMMM yyyy"));
            sb.Replace("[BOTTLE.UDLØBSDATO]", item.BottleExpireDate.ToString("d. MMMM yyyy"));
            
            sb.Replace("[BOTTLE.DELEVERYDATE]", item.BottleDeleveryDate.ToString("d. MMMM yyyy"));
            if (Information.IsDate(item.BottleEmailSendt))
            {
                DateTime dtgBES = (DateTime) item.BottleEmailSendt;
                sb.Replace("[BOTTLE.EMAILSENDT]", dtgBES.ToString("d. MMMM yyyy"));
            }
            sb.Replace("[BOTTLE.GUID]", item.Guid.ToString());
            
            sb.Replace("[BOTTLE.VARENR]", "[VARE.VARENR]");
            sb.Replace("[BOTTLE.SALGSPRIS]", "[VARE.SALGSPRIS]");
            sb.Replace("[BOTTLE.RABAT]", "[VARE.RABAT]");
            sb.Replace("[BOTTLE.PRIS]", "[VARE.PRIS]");
            sb.Replace("[BOTTLE.KUNDEPRIS]", "[VARE.KUNDEPRIS]");
            sb.Replace("[BOTTLE.FRAGTGEBYR]", "[VARE.FRAGTGEBYR]");
            sb.Replace("[BOTTLE.FRAGTPRIS]", "[VARE.FRAGTPRIS]");
            sb.Replace("[BOTTLE.MOMS]", "[VARE.MOMS]");
            sb.Replace("[BOTTLE.TOTAL]", "[VARE.TOTAL]");
            
            res = sb.ToString();
            res = item.KundeGrpPris.Tags(res);
            res = item.EYE.Tags(res);
            
            return res;
        }
        
#endregion
        
    }
    
    
}
