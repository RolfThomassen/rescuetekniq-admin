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
    
    public class AED_Electrod : BaseObject
    {
        
#region  New
        
        public AED_Electrod()
        {
            
        }
        
        public AED_Electrod(int ID)
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
        public AED_Electrod(System.Guid GUID)
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
        private int _ElectrodType;
        private DateTime _ElectrodDeleveryDate;
        private DateTime _ElectrodExpireDate;
        private DateTime _ElectrodEmailSendt;
        
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
        
        public int ElectrodType
        {
            get
            {
                return _ElectrodType;
            }
            set
            {
                _ElectrodType = value;
            }
        }
        public string ElectrodTypeText
        {
            get
            {
                string res = Vare.Navn;
                // Vare.GetVare(ElectrodType).Navn
                return res;
                //Combobox.GetTitle("ElectrodType", ElectrodType)
            }
        }
        
        public DateTime ElectrodDeleveryDate
        {
            get
            {
                return _ElectrodDeleveryDate;
            }
            set
            {
                _ElectrodDeleveryDate = value;
            }
        }
        
        public DateTime ElectrodExpireDate
        {
            get
            {
                return _ElectrodExpireDate;
            }
            set
            {
                _ElectrodExpireDate = value;
            }
        }
        
        public Nullable<DateTime> ElectrodEmailSendt
        {
            get
            {
                Nullable<DateTime> res = new Nullable<DateTime>();
                if (_ElectrodEmailSendt.ToBinary() != 0)
                {
                    res = _ElectrodEmailSendt;
                }
                return res; //_ElectrodEmailSendt
            }
            set
            {
                _ElectrodEmailSendt = value.Value;
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
                    if (ElectrodType > 0)
                    {
                        if (!_Vare.loaded)
                        {
                            _Vare = RescueTekniq.BOL.Vare.GetVare(ElectrodType);
                        }
                        else if (_Vare.ID != ElectrodType)
                        {
                            _Vare = RescueTekniq.BOL.Vare.GetVare(ElectrodType);
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
                    //KundeGrp_Pris.GetCompany_VarePris(item.AED.CompanyID, item.Vare.ID)
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
        
        private static void AddParms(ref DBAccess db, AED_Electrod c)
        {
            var with_1 = c;
            db.AddInt("Status", (System.Int32) with_1.Status);
            db.AddInt("AED_FK", with_1.AED_FK);
            db.AddInt("ElectrodType", with_1.ElectrodType);
            db.AddDateTime("ElectrodDeleveryDate", with_1.ElectrodDeleveryDate);
            db.AddDateTime("ElectrodExpireDate", with_1.ElectrodExpireDate);
            db.AddDateTime("ElectrodEmailSendt", with_1.ElectrodEmailSendt);
            AddParmsStandard(db, c);
        }
        
        private static void Populate(System.Data.SqlClient.SqlDataReader dr, AED_Electrod c)
        {
            PopulateStandard(dr, c);
            var with_1 = c;
            with_1.Status = (RescueTekniq.BOL.AEDStatusEnum) (dr.DBtoInt("Status"));
            with_1.AED_FK = System.Convert.ToInt32(dr.DBtoInt("AED_FK"));
            
            with_1.ElectrodType = System.Convert.ToInt32(dr.DBtoInt("ElectrodType"));
            with_1.ElectrodDeleveryDate = System.Convert.ToDateTime(dr.DBtoDate("ElectrodDeleveryDate"));
            with_1.ElectrodExpireDate = System.Convert.ToDateTime(dr.DBtoDate("ElectrodExpireDate"));
            with_1.ElectrodEmailSendt = dr.DBtoDate("ElectrodEmailSendt");
            
        }
        
#endregion
        
#region  Stored Procedure Names
        private const string _SQLDelete = "Co2Db_AED_Electrod_Delete";
        private const string _SQLInsert = "Co2Db_AED_Electrod_Insert";
        private const string _SQLUpdate = "Co2Db_AED_Electrod_Update";
        private const string _SQLSelectAll = "Co2Db_AED_Electrod_SelectAll";
        private const string _SQLSelectID = "Co2Db_AED_Electrod_SelectID";
        private const string _SQLSelectOne = "Co2Db_AED_Electrod_SelectOne";
        private const string _SQLSelectBySearch = "Co2Db_AED_Electrod_SelectBySearch";
        private const string _SQLSelectByCompany = "Co2Db_AED_Electrod_SelectByCompany";
        private const string _SQLSelectByAED = "Co2Db_AED_Electrod_SelectByAED";
        
        private const string _SQLSelectByGuid = "Co2Db_AED_Electrod_SelectByGuid";
        
        private const string _SQLSelectAllSoonExpired = "Co2Db_AED_Electrod_SelectAllSoonExpired";
        private const string _SQLSelectAllExpiredEmail = "Co2Db_AED_Electrod_SelectAllExpiredEmail";
        
#endregion
        
#region  Metoder
        
#region  Manipulate data
        
        public int Delete()
        {
            return Delete(System.Convert.ToInt32(this));
        }
        public static int Delete(int ID)
        {
            AED_Electrod E = new AED_Electrod();
            E.ID = ID;
            return Delete(E);
        }
        public static int Delete(AED_Electrod E)
        {
            DBAccess db = new DBAccess();
            db.AddInt("ID", E.ID);
            int retval = db.ExecuteNonQuery(_SQLDelete);
            AddLog(Status: "AED_Electrod", Logtext: string.Format("Delete AED_Electrod: ID:{0}", E.ID), Metode: "Delete");
            return retval;
        }
        
        public int Insert()
        {
            return Insert(this);
        }
        public static int Insert(AED_Electrod E)
        {
            DBAccess db = new DBAccess();
            
            AddParms(ref db, E);
            
            SqlParameter objParam = new SqlParameter("@ID", 0);
            objParam.Direction = ParameterDirection.Output;
            db.Parameters.Add(objParam);
            int retval = db.ExecuteNonQuery(_SQLInsert);
            if (retval == 1)
            {
                E.ID = int.Parse(objParam.Value.ToString());
                AddLog(Status: "AED_Electrod", Logtext: string.Format("Create AED_Electrod: ID:{0}", E.ID), Metode: "Insert");
                return E.ID;
            }
            else
            {
                AddLog(Status: "AED_Electrod", Logtext: string.Format("Failure to Create AED_Electrod:"), logtype: LogTypeEnum.Error, Metode: "Insert");
                return -1;
            }
        }
        public static int Insert(AEDStatusEnum Status, int AED_FK, int ElectrodType, DateTime ElectrodDeleveryDate, DateTime ElectrodExpireDate, DateTime ElectrodEmailSendt)
        {
            AED_Electrod c = new AED_Electrod();
            c.Status = Status;
            c.AED_FK = AED_FK;
            
            c.ElectrodType = ElectrodType;
            c.ElectrodDeleveryDate = ElectrodDeleveryDate;
            c.ElectrodExpireDate = ElectrodExpireDate;
            c.ElectrodEmailSendt = ElectrodEmailSendt;
            
            return Insert(c);
        }
        
        public int Update()
        {
            return Update(this);
        }
        public static int Update(AED_Electrod E)
        {
            DBAccess db = new DBAccess();
            
            db.AddInt("ID", E.ID);
            AddParms(ref db, E);
            
            int retval = db.ExecuteNonQuery(_SQLUpdate);
            AddLog(Status: "AED_Electrod", Logtext: string.Format("Update AED_Electrod: ID:{0}", E.ID), Metode: "Update");
            return retval;
        }
        public static int Update(int ID, AEDStatusEnum Status, int AED_FK, int ElectrodType, DateTime ElectrodDeleveryDate, DateTime ElectrodExpireDate, DateTime ElectrodEmailSendt)
        {
            AED_Electrod c = new AED_Electrod(ID);
            c.Status = Status;
            c.AED_FK = AED_FK;
            
            c.ElectrodType = ElectrodType;
            c.ElectrodDeleveryDate = ElectrodDeleveryDate;
            c.ElectrodExpireDate = ElectrodExpireDate;
            c.ElectrodEmailSendt = ElectrodEmailSendt;
            
            return Update(c);
        }
        
        public int Save()
        {
            return Save(this);
        }
        public static int Save(AED_Electrod E)
        {
            int retval = 0;
            if (E.ID > 0)
            {
                retval = Update(E);
            }
            else
            {
                retval = Insert(E);
            }
            return retval;
        }
        
#endregion
        
#region  Get data
        
        public static AED_Electrod GetElectrod(int ID)
        {
            AED_Electrod c = new AED_Electrod();
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
        public static AED_Electrod GetElectrod(System.Guid Guid)
        {
            AED_Electrod c = new AED_Electrod();
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
        
        public static DataSet GetElectrodDS(int ID)
        {
            DBAccess db = new DBAccess();
            db.AddInt("ID", ID);
            
            db.AddBoolean("IsAgent", AdgangsKontrol.IsAgent());
            db.AddGuid("AgentID", CurrentUserID);
            
            DataSet ds = db.ExecuteDataSet(_SQLSelectID);
            return ds;
        }
        public static DataSet GetElectrodDS(System.Guid Guid)
        {
            DBAccess db = new DBAccess();
            db.AddGuid("Guid", Guid);
            
            db.AddBoolean("IsAgent", AdgangsKontrol.IsAgent());
            db.AddGuid("AgentID", CurrentUserID);
            
            DataSet ds = db.ExecuteDataSet(_SQLSelectByGuid);
            return ds;
        }
        
        public static System.Collections.Generic.List<AED_Electrod> GetElectrodList(int AED_FK)
        {
            System.Collections.Generic.List<AED_Electrod> result = new System.Collections.Generic.List<AED_Electrod>();
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
                    result.Add(AED_Electrod.GetElectrod(ID));
                }
            }
            dr.Close();
            //Catch ex As Exception
            //Throw ex
            //End Try
            
            return result;
        }
        
        public static DataSet GetAllElectrod(int AED_FK)
        {
            DBAccess db = new DBAccess();
            
            db.AddBoolean("IsAgent", AdgangsKontrol.IsAgent());
            db.AddGuid("AgentID", CurrentUserID);
            
            DataSet ds = db.ExecuteDataSet(_SQLSelectAll);
            return ds;
        }
        
        public static DataSet GetElectrodByAED(int AED_FK)
        {
            DBAccess db = new DBAccess();
            db.AddInt("AED_FK", AED_FK);
            
            db.AddBoolean("IsAgent", AdgangsKontrol.IsAgent());
            db.AddGuid("AgentID", CurrentUserID);
            
            DataSet ds = db.ExecuteDataSet(_SQLSelectByAED);
            return ds;
        }
        
        public static DataSet GetAllElectrodByCompany(int CompanyID)
        {
            DBAccess db = new DBAccess();
            db.AddInt("CompanyID", CompanyID);
            
            db.AddBoolean("IsAgent", AdgangsKontrol.IsAgent());
            db.AddGuid("AgentID", CurrentUserID);
            
            DataSet ds = db.ExecuteDataSet(_SQLSelectByCompany);
            return ds;
        }
        
        public static DataSet GetElectrodBySoonExpired(int CompanyID)
        {
            DBAccess db = new DBAccess();
            db.AddInt("CompanyID", CompanyID);
            
            db.AddBoolean("IsAgent", AdgangsKontrol.IsAgent());
            db.AddGuid("AgentID", CurrentUserID);
            
            DataSet ds = db.ExecuteDataSet(_SQLSelectAllSoonExpired);
            return ds;
        }
        
        public static System.Collections.Generic.List<AED_Electrod> GetElectrodListSoonExpired(int CompanyID)
        {
            System.Collections.Generic.List<AED_Electrod> result = new System.Collections.Generic.List<AED_Electrod>();
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
                    result.Add(AED_Electrod.GetElectrod(ID));
                }
            }
            dr.Close();
            //Catch ex As Exception
            //Throw ex
            //End Try
            
            return result;
        }
        
        public static System.Collections.Generic.List<AED_Electrod> GetBatteryListExpiredEmail(int CompanyID)
        {
            System.Collections.Generic.List<AED_Electrod> result = new System.Collections.Generic.List<AED_Electrod>();
            int ID = -1;
            DBAccess db = new DBAccess();
            db.AddInt("CompanyID", CompanyID);
            
            db.AddBoolean("IsAgent", AdgangsKontrol.IsAgent());
            db.AddGuid("AgentID", CurrentUserID);
            
            SqlDataReader dr = default(SqlDataReader);
            //Try
            dr = (System.Data.SqlClient.SqlDataReader) (db.ExecuteReader(_SQLSelectAllExpiredEmail));
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ID = System.Convert.ToInt32(dr.DBtoInt("ID"));
                    result.Add(AED_Electrod.GetElectrod(ID));
                }
            }
            dr.Close();
            //Catch ex As Exception
            //Throw ex
            //End Try
            
            return result;
        }
        
        public static DataSet Search_Electrod(string Search)
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
        public static string Tags(string tekst, RescueTekniq.BOL.AED_Electrod item)
        {
            string res = "";
            StringBuilder sb = new StringBuilder();
            sb.Append(tekst);
            sb.Replace("[VARE.TYPE]", "elektroder");
            sb.Replace("[ELECTROD.TYPE]", "[VARE.NAVN]");
            
            sb.Replace("[EXPIREDATE]", item.ElectrodExpireDate.ToString("dd MMMM yyyy")); //("MMMM yyyy"))
            sb.Replace("[ELECTROD.EXPIREDATE]", item.ElectrodExpireDate.ToString("dd MMMM yyyy")); //("MMMM yyyy"))
            sb.Replace("[ELECTROD.UDLÃ˜BSDATO]", item.ElectrodExpireDate.ToString("dd MMMM yyyy")); //("MMMM yyyy"))
            
            sb.Replace("[ELECTROD.DELEVERYDATE]", item.ElectrodDeleveryDate.ToString("dd MMMM yyyy")); //
            sb.Replace("[ELECTROD.EMAILSENDT]", item.ElectrodEmailSendt.ToString());
            sb.Replace("[ELECTROD.GUID]", item.Guid.ToString());
            
            sb.Replace("[ELECTROD.VARENR]", "[VARE.VARENR]");
            sb.Replace("[ELECTROD.SALGSPRIS]", "[VARE.SALGSPRIS]");
            sb.Replace("[ELECTROD.RABAT]", "[VARE.RABAT]");
            sb.Replace("[ELECTROD.PRIS]", "[VARE.PRIS]");
            sb.Replace("[ELECTROD.KUNDEPRIS]", "[VARE.KUNDEPRIS]");
            sb.Replace("[ELECTROD.FRAGTGEBYR]", "[VARE.FRAGTGEBYR]");
            sb.Replace("[ELECTROD.FRAGTPRIS]", "[VARE.FRAGTPRIS]");
            sb.Replace("[ELECTROD.MOMS]", "[VARE.MOMS]");
            sb.Replace("[ELECTROD.TOTAL]", "[VARE.TOTAL]");
            
            res = sb.ToString();
            res = item.KundeGrpPris.Tags(res);
            res = item.AED.Tags(res);
            
            return res;
        }
        
#endregion
        
    }
    
    
}
