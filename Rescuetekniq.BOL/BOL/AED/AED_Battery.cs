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
    
    public class AED_Battery : BaseObject
    {
        
#region  New
        
        public AED_Battery()
        {
            
        }
        public AED_Battery(int ID)
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
        public AED_Battery(System.Guid GUID)
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
        
        private AEDStatusEnum _Status = AEDStatusEnum.Aktiv;
        private int _AED_FK;
        private int _BatteryType;
        private DateTime _BatteryDeleveryDate;
        private DateTime _BatteryExpireDate;
        private DateTime _BatteryEmailSendt;
        
        private RescueTekniq.BOL.AED _AED = new RescueTekniq.BOL.AED();
        private RescueTekniq.BOL.Vare _Vare = new RescueTekniq.BOL.Vare();
        private RescueTekniq.BOL.KundeGrp_Pris _kgp = new RescueTekniq.BOL.KundeGrp_Pris();
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
        public string StatusText
        {
            get
            {
                string res = "";
                res = Combobox.GetTitleByValue("AED.Status.Enum", Status.ToString());
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
                _Status = (AEDStatusEnum) value;
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
        
        public int BatteryType
        {
            get
            {
                return _BatteryType;
            }
            set
            {
                _BatteryType = value;
            }
        }
        public string BatteryTypeText
        {
            get
            {
                string res = Vare.Navn;
                //Vare.GetVare(BatteryType).Navn
                return res;
                //Combobox.GetTitle("BatteryType", BatteryType)
            }
        }
        
        public DateTime BatteryDeleveryDate
        {
            get
            {
                return _BatteryDeleveryDate;
            }
            set
            {
                _BatteryDeleveryDate = value;
            }
        }
        
        public DateTime BatteryExpireDate
        {
            get
            {
                return _BatteryExpireDate;
            }
            set
            {
                _BatteryExpireDate = value;
            }
        }
        
        public Nullable<DateTime> BatteryEmailSendt
        {
            get
            {
                Nullable<DateTime> res = new Nullable<DateTime>();
                if (_BatteryEmailSendt.ToBinary() != 0)
                {
                    res = _BatteryEmailSendt;
                }
                return res; //_BatteryEmailSendt
            }
            set
            {
                _BatteryEmailSendt = value.Value;
            }
        }
        
        public string Firmanavn
        {
            get
            {
                return AED.Virksomhed.Firmanavn;
            }
        }
        public string AEDmodel
        {
            get
            {
                return AED.Vare.Navn;
            }
        }
        public string AEDSerialNo
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
        
        public RescueTekniq.BOL.Vare Vare
        {
            get
            {
                try
                {
                    if (BatteryType > 0)
                    {
                        if (!_Vare.loaded)
                        {
                            _Vare = RescueTekniq.BOL.Vare.GetVare(BatteryType);
                        }
                        else if (_Vare.ID != BatteryType)
                        {
                            _Vare = RescueTekniq.BOL.Vare.GetVare(BatteryType);
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
                    if (AED.loaded && Vare.loaded)
                    {
                        if (!_kgp.loaded)
                        {
                            _kgp = RescueTekniq.BOL.KundeGrp_Pris.GetCompany_VarePris(AED.CompanyID, Vare.ID);
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
        
        private static void AddParms(ref DBAccess db, AED_Battery c)
        {
            var with_1 = c;
            db.AddInt("Status", (System.Int32) with_1.Status);
            db.AddInt("AED_FK", with_1.AED_FK);
            db.AddInt("BatteryType", with_1.BatteryType);
            db.AddDateTime("BatteryDeleveryDate", with_1.BatteryDeleveryDate);
            db.AddDateTime("BatteryExpireDate", with_1.BatteryExpireDate);
            db.AddDateTime("BatteryEmailSendt", with_1.BatteryEmailSendt);
            AddParmsStandard(db, c);
        }
        
        private static void Populate(SqlDataReader dr, AED_Battery c)
        {
            PopulateStandard(dr, c);
            var with_1 = c;
            with_1.Status = (RescueTekniq.BOL.AEDStatusEnum) (dr.DBtoInt("Status"));
            with_1.AED_FK = System.Convert.ToInt32(dr.DBtoInt("AED_FK"));
            
            with_1.BatteryType = System.Convert.ToInt32(dr.DBtoInt("BatteryType"));
            with_1.BatteryDeleveryDate = System.Convert.ToDateTime(dr.DBtoDate("BatteryDeleveryDate"));
            with_1.BatteryExpireDate = System.Convert.ToDateTime(dr.DBtoDate("BatteryExpireDate"));
            with_1.BatteryEmailSendt = dr.DBtoDate("BatteryEmailSendt");
            
        }
        
#endregion
        
#region  Stored Procedure Names
        private const string _SQLDelete = "Co2Db_AED_Battery_Delete";
        private const string _SQLInsert = "Co2Db_AED_Battery_Insert";
        private const string _SQLUpdate = "Co2Db_AED_Battery_Update";
        private const string _SQLSelectAll = "Co2Db_AED_Battery_SelectAll";
        private const string _SQLSelectID = "Co2Db_AED_Battery_SelectID";
        private const string _SQLSelectOne = "Co2Db_AED_Battery_SelectOne";
        private const string _SQLSelectBySearch = "Co2Db_AED_Battery_SelectBySearch";
        private const string _SQLSelectByCompany = "Co2Db_AED_Battery_SelectByCompany";
        private const string _SQLSelectByAED = "Co2Db_AED_Battery_SelectByAED";
        private const string _SQLSelectByGuid = "Co2Db_AED_Battery_SelectByGUID";
        
        private const string _SQLSelectAllSoonExpired = "Co2Db_AED_Battery_SelectAllSoonExpired";
        private const string _SQLSelectAllExpiredEmail = "Co2Db_AED_Battery_SelectAllExpiredEmail";
        
#endregion
        
#region  Metoder
        
#region  Manipulate data
        
        public int Delete()
        {
            return Delete(System.Convert.ToInt32(this));
        }
        public static int Delete(int ID)
        {
            AED_Battery B = new AED_Battery();
            B.ID = ID;
            return Delete(B);
        }
        public static int Delete(AED_Battery B)
        {
            DBAccess db = new DBAccess();
            db.AddInt("ID", B.ID);
            int retval = db.ExecuteNonQuery(_SQLDelete);
            AddLog(Status: "AED_Battery", Logtext: string.Format("Delete AED_Battery: ID:{0}", B.ID), Metode: "Delete");
            return retval;
        }
        
        public int Insert()
        {
            return Insert(this);
        }
        public static int Insert(AED_Battery B)
        {
            DBAccess db = new DBAccess();
            
            AddParms(ref db, B);
            
            SqlParameter objParam = new SqlParameter("@ID", 0);
            objParam.Direction = ParameterDirection.Output;
            db.Parameters.Add(objParam);
            int retval = db.ExecuteNonQuery(_SQLInsert);
            if (retval == 1)
            {
                B.ID = int.Parse(objParam.Value.ToString());
                AddLog(Status: "AED_Battery", Logtext: string.Format("Create AED_Battery: ID:{0}", B.ID), Metode: "Insert");
                return B.ID;
            }
            else
            {
                AddLog(Status: "AED_Battery", Logtext: string.Format("Failure to Create AED_Battery:"), logtype: LogTypeEnum.Error, Metode: "Insert");
                return -1;
            }
        }
        public static int Insert(AEDStatusEnum Status, int AED_FK, int BatteryType, DateTime BatteryDeleveryDate, DateTime BatteryExpireDate, DateTime BatteryEmailSendt)
        {
            AED_Battery c = new AED_Battery();
            c.Status = Status;
            c.AED_FK = AED_FK;
            
            c.BatteryType = BatteryType;
            c.BatteryDeleveryDate = BatteryDeleveryDate;
            c.BatteryExpireDate = BatteryExpireDate;
            c.BatteryEmailSendt = BatteryEmailSendt;
            
            return Insert(c);
        }
        
        public int Update()
        {
            return Update(this);
        }
        public static int Update(AED_Battery B)
        {
            DBAccess db = new DBAccess();
            
            db.AddInt("ID", B.ID);
            AddParms(ref db, B);
            
            int retval = db.ExecuteNonQuery(_SQLUpdate);
            AddLog(Status: "AED_Battery", Logtext: string.Format("Update AED_Battery: ID:{0}", B.ID), Metode: "Update");
            return retval;
        }
        public static int Update(int ID, AEDStatusEnum Status, int AED_FK, int BatteryType, DateTime BatteryDeleveryDate, DateTime BatteryExpireDate, DateTime BatteryEmailSendt)
        {
            AED_Battery c = new AED_Battery(ID);
            c.Status = Status;
            c.AED_FK = AED_FK;
            
            c.BatteryType = BatteryType;
            c.BatteryDeleveryDate = BatteryDeleveryDate;
            c.BatteryExpireDate = BatteryExpireDate;
            c.BatteryEmailSendt = BatteryEmailSendt;
            
            return Update(c);
        }
        
        //            Log.AddLog("AED", String.Format("Create AED: ID:{0} SNo:{1} ", c.ID, c.SerialNo))
        public int Save()
        {
            return Save(this);
        }
        public static int Save(AED_Battery B)
        {
            int retval = 0;
            if (B.ID > 0)
            {
                retval = Update(B);
            }
            else
            {
                retval = Insert(B);
            }
            return retval;
        }
        
#endregion
        
#region  Get data
        
        public static AED_Battery GetBattery(int ID)
        {
            AED_Battery c = new AED_Battery();
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
        public static AED_Battery GetBattery(System.Guid Guid)
        {
            AED_Battery c = new AED_Battery();
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
        public static DataSet GetBatteryDS(int ID)
        {
            DBAccess db = new DBAccess();
            db.AddInt("ID", ID);
            
            db.AddBoolean("IsAgent", AdgangsKontrol.IsAgent());
            db.AddGuid("AgentID", CurrentUserID);
            
            DataSet ds = db.ExecuteDataSet(_SQLSelectID);
            return ds;
        }
        public static DataSet GetBatteryDS(System.Guid Guid)
        {
            DBAccess db = new DBAccess();
            db.AddGuid("Guid", Guid);
            
            db.AddBoolean("IsAgent", AdgangsKontrol.IsAgent());
            db.AddGuid("AgentID", CurrentUserID);
            
            DataSet ds = db.ExecuteDataSet(_SQLSelectByGuid);
            return ds;
        }
        
        public static System.Collections.Generic.List<AED_Battery> GetBatteryList(int AED_FK)
        {
            System.Collections.Generic.List<AED_Battery> result = new System.Collections.Generic.List<AED_Battery>();
            int ID = -1;
            DBAccess db = new DBAccess();
            db.AddInt("AED_FK", AED_FK);
            
            db.AddBoolean("IsAgent", AdgangsKontrol.IsAgent());
            db.AddGuid("AgentID", CurrentUserID);
            
            SqlDataReader dr = default(SqlDataReader);
            //Try
            dr = (System.Data.SqlClient.SqlDataReader) (db.ExecuteReader(_SQLSelectByAED));
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ID = System.Convert.ToInt32(dr.DBtoInt("ID"));
                    result.Add(AED_Battery.GetBattery(ID));
                }
            }
            dr.Close();
            //Catch ex As Exception
            //Throw ex
            //End Try
            
            return result;
        }
        
        
        public static DataSet GetAllBattery(int AED_FK)
        {
            DBAccess db = new DBAccess();
            
            db.AddBoolean("IsAgent", AdgangsKontrol.IsAgent());
            db.AddGuid("AgentID", CurrentUserID);
            
            DataSet ds = db.ExecuteDataSet(_SQLSelectAll);
            return ds;
        }
        
        public static DataSet GetBatteryByAED(int AED_FK)
        {
            DBAccess db = new DBAccess();
            db.AddInt("AED_FK", AED_FK);
            
            db.AddBoolean("IsAgent", AdgangsKontrol.IsAgent());
            db.AddGuid("AgentID", CurrentUserID);
            
            DataSet ds = db.ExecuteDataSet(_SQLSelectByAED);
            return ds;
        }
        
        public static DataSet GetAllBatteryByCompany(int CompanyID)
        {
            DBAccess db = new DBAccess();
            db.AddInt("CompanyID", CompanyID);
            
            db.AddBoolean("IsAgent", AdgangsKontrol.IsAgent());
            db.AddGuid("AgentID", CurrentUserID);
            
            DataSet ds = db.ExecuteDataSet(_SQLSelectByCompany);
            return ds;
        }
        
        public static DataSet GetBatteryBySoonExpired(int CompanyID)
        {
            DBAccess db = new DBAccess();
            db.AddInt("CompanyID", CompanyID);
            
            db.AddBoolean("IsAgent", AdgangsKontrol.IsAgent());
            db.AddGuid("AgentID", CurrentUserID);
            
            DataSet ds = db.ExecuteDataSet(_SQLSelectAllSoonExpired);
            return ds;
        }
        public static System.Collections.Generic.List<AED_Battery> GetBatteryListSoonExpired(int CompanyID)
        {
            System.Collections.Generic.List<AED_Battery> result = new System.Collections.Generic.List<AED_Battery>();
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
                    result.Add(AED_Battery.GetBattery(ID));
                }
            }
            dr.Close();
            //Catch ex As Exception
            //Throw ex
            //End Try
            
            return result;
        }
        
        public static System.Collections.Generic.List<AED_Battery> GetBatteryListExpiredEmail(int CompanyID)
        {
            System.Collections.Generic.List<AED_Battery> result = new System.Collections.Generic.List<AED_Battery>();
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
                        result.Add(AED_Battery.GetBattery(ID));
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
        
        public static DataSet Search_Battery(string Search)
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
        public static string Tags(string tekst, RescueTekniq.BOL.AED_Battery item)
        {
            string res = "";
            StringBuilder sb = new StringBuilder();
            sb.Append(tekst);
            sb.Replace("[VARE.TYPE]", "Batteri");
            sb.Replace("[BATTERI.TYPE]", "[VARE.NAVN]");
            
            sb.Replace("[EXPIREDATE]", item.BatteryExpireDate.ToString("d. MMMM yyyy"));
            sb.Replace("[BATTERI.EXPIREDATE]", item.BatteryExpireDate.ToString("d. MMMM yyyy"));
            sb.Replace("[BATTERI.UDLÃ˜BSDATO]", item.BatteryExpireDate.ToString("d. MMMM yyyy"));
            
            sb.Replace("[BATTERI.DELEVERYDATE]", item.BatteryDeleveryDate.ToString("d. MMMM yyyy"));
            sb.Replace("[BATTERI.EMAILSENDT]", item.BatteryEmailSendt.ToString());
            sb.Replace("[BATTERI.GUID]", item.Guid.ToString());
            
            sb.Replace("[BATTERI.VARENR]", "[VARE.VARENR]");
            sb.Replace("[BATTERI.SALGSPRIS]", "[VARE.SALGSPRIS]");
            sb.Replace("[BATTERI.RABAT]", "[VARE.RABAT]");
            sb.Replace("[BATTERI.PRIS]", "[VARE.PRIS]");
            sb.Replace("[BATTERI.KUNDEPRIS]", "[VARE.KUNDEPRIS]");
            sb.Replace("[BATTERI.FRAGTGEBYR]", "[VARE.FRAGTGEBYR]");
            sb.Replace("[BATTERI.FRAGTPRIS]", "[VARE.FRAGTPRIS]");
            sb.Replace("[BATTERI.MOMS]", "[VARE.MOMS]");
            sb.Replace("[BATTERI.TOTAL]", "[VARE.TOTAL]");
            
            res = sb.ToString();
            res = item.KundeGrpPris.Tags(res);
            res = item.AED.Tags(res);
            
            return res;
        }
        
#endregion
        
    }
    
    
}
