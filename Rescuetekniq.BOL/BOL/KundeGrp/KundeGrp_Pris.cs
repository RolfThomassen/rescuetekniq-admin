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
    
    public class KundeGrp_Pris : BaseObject
    {
        
#region  New
        
        public KundeGrp_Pris()
        {
            
        }
        
        public KundeGrp_Pris(int ID)
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
        //UPDATE [vicjos1_Heart2Start].[vicjos1_sysadm].[Co2Db_KundeGrp_Pris]
        //  SET [KundeGrpID] = <KundeGrpID, int,>
        //     ,[VareID] = <VareID, int,>
        //     ,[status] = <status, int,>
        //     ,[Rabat] = <Rabat, float,>
        //     ,[FragtGebyr] = <FragtGebyr, float,>
        //     ,[Dato] = <Dato, datetime,>
        //WHERE <Search Conditions,,>
        
        //SELECT ID, status, KundeGrpID, KundeGrpNavn, VareID, SalgsPris, Rabat, KundePris, FragtGebyr, Dato, VareNr, Navn, VareGrpID, VareGrpNr FROM vw_Co2Db_KundeGrp_Pris
        //2	1	1	DRK-Kunder	24	375	10	412,5	39	2010-01-30 12:35:04.670	9131-001	Defibrillation - voksen elektroder	3	Elektroder
        
        private KundeGrpStatusEnum _Status = KundeGrpStatusEnum.Aktiv;
        
        private int _KundeGrpID;
        private string _KundeGrpNavn;
        
        private decimal _SalgsPris;
        private decimal _Rabat = -0.000010000000000000001M;
        private decimal _KundePris;
        private decimal _FragtGebyr;
        private DateTime _Dato;
        
        private int _VareID;
        private string _VareNr;
        private string _VareNavn;
        private int _VareGrpID;
        private string _VareGrpNavn;
        
        private decimal _ProvisionRate = 0;
        
        private RescueTekniq.BOL.Vare _vare = new RescueTekniq.BOL.Vare();
        private RescueTekniq.BOL.KundeGrp _KundeGrp = new RescueTekniq.BOL.KundeGrp();
        
#endregion
        
#region  Properties
        
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
        
        /// <summary>
        /// Kundegruppe ID
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public int KundeGrpID
        {
            get
            {
                return _KundeGrpID;
            }
            set
            {
                _KundeGrpID = value;
            }
        }
        public string KundeGrpNavn
        {
            get
            {
                _KundeGrpNavn = KundeGrp.KundeGrpNavn;
                return _KundeGrpNavn;
            }
        }
        
        /// <summary>
        /// Dato for kundegruppe pris
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public DateTime Dato
        {
            get
            {
                return _Dato;
            }
            set
            {
                _Dato = value;
            }
        }
        
        /// <summary>
        /// Vare vejledende pris
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public decimal SalgsPris
        {
            get
            {
                return _SalgsPris;
            }
        }
        
        /// <summary>
        /// Kundegruppe rabat i %
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public decimal Rabat
        {
            get
            {
                return decimal.Round(_Rabat, 4);
            }
            set
            {
                if (value != _Rabat && (double) _Rabat > -0.000010000000000000001)
                {
                    _KundePris = SalgsPris * (1 + (_Rabat / 100));
                }
                _Rabat = value;
            }
        }
        
        /// <summary>
        /// Kundegruppens vare salgspris
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public decimal KundePris
        {
            get
            {
                return _KundePris;
            }
        }
        
        /// <summary>
        /// Fragtgebyr på vare
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public decimal FragtGebyr
        {
            get
            {
                return _FragtGebyr;
            }
            set
            {
                _FragtGebyr = value;
            }
        }
        
        public int VareID
        {
            get
            {
                return _VareID;
            }
            set
            {
                _VareID = value;
            }
        }
        public string VareNr
        {
            get
            {
                return _VareNr;
            }
        }
        public string VareNavn
        {
            get
            {
                return _VareNavn;
            }
        }
        public int VareGrpID
        {
            get
            {
                return _VareGrpID;
            }
        }
        public string VareGrpNavn
        {
            get
            {
                return _VareGrpNavn;
            }
        }
        
        /// <summary>
        /// ProvisionRate på vare
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public decimal ProvisionRate
        {
            get
            {
                return _ProvisionRate;
            }
            set
            {
                _ProvisionRate = value;
            }
        }
        
        public RescueTekniq.BOL.Vare Vare
        {
            get
            {
                try
                {
                    if (_VareID > 0)
                    {
                        if (!_vare.loaded)
                        {
                            _vare = Vare.GetVare(_VareID);
                        }
                        else if (_vare.ID != _VareID)
                        {
                            _vare = Vare.GetVare(_VareID);
                        }
                    }
                }
                catch (Exception)
                {
                }
                return _vare;
            }
        }
        
        public RescueTekniq.BOL.KundeGrp KundeGrp
        {
            get
            {
                try
                {
                    if (_KundeGrpID > 0)
                    {
                        if (!_KundeGrp.loaded)
                        {
                            _KundeGrp = KundeGrp.GetKundeGrp(_KundeGrpID);
                        }
                        else if (_KundeGrp.ID != _KundeGrpID)
                        {
                            _KundeGrp = KundeGrp.GetKundeGrp(_KundeGrpID);
                        }
                    }
                }
                catch (Exception)
                {
                }
                return _KundeGrp;
            }
        }
        
#endregion
        
#region  Shared Populate
        //  SET [KundeGrpID] = <KundeGrpID, int,>
        //     ,[VareID] = <VareID, int,>
        //     ,[status] = <status, int,>
        //     ,[Rabat] = <Rabat, float,>
        //     ,[FragtGebyr] = <FragtGebyr, float,>
        //     ,[Dato] = <Dato, datetime,>
        
        private static void AddParms(ref DBAccess db, KundeGrp_Pris c)
        {
            var with_1 = c;
            db.AddInt("KundeGrpID", with_1.KundeGrpID);
            db.AddInt("VareID", with_1.VareID);
            db.AddInt("Status", (System.Int32) with_1.Status);
            db.AddDecimal("Rabat", with_1.Rabat);
            db.AddDecimal("FragtGebyr", with_1.FragtGebyr);
            db.AddDateTime("Dato", with_1.Dato);
            db.AddDecimal("ProvisionRate", with_1.ProvisionRate);
            AddParmsStandard(db, c);
        }
        
        //SELECT ID, status, KundeGrpID, KundeGrpNavn, VareID, SalgsPris, Rabat, KundePris, FragtGebyr, Dato, VareNr, Navn, VareGrpID, VareGrpNr FROM vw_Co2Db_KundeGrp_Pris
        private static void Populate(SqlDataReader dr, KundeGrp_Pris c)
        {
            var with_1 = c;
            with_1.Status = (RescueTekniq.BOL.KundeGrpStatusEnum) (dr.DBtoInt("Status"));
            with_1.KundeGrpID = System.Convert.ToInt32(dr.DBtoInt("KundeGrpID"));
            with_1._KundeGrpNavn = dr.DBtoString("KundeGrpNavn");
            
            with_1._SalgsPris = System.Convert.ToDecimal(dr.DBtoDecimal("SalgsPris"));
            with_1.Rabat = System.Convert.ToDecimal(dr.DBtoDecimal("Rabat"));
            with_1._KundePris = System.Convert.ToDecimal(dr.DBtoDecimal("KundePris"));
            with_1.FragtGebyr = System.Convert.ToDecimal(dr.DBtoDecimal("FragtGebyr"));
            with_1.Dato = System.Convert.ToDateTime(dr.DBtoDate("Dato"));
            
            with_1.VareID = System.Convert.ToInt32(dr.DBtoInt("VareID"));
            with_1._VareNr = dr.DBtoString("VareNr");
            with_1._VareNavn = dr.DBtoString("Navn");
            
            with_1._VareGrpID = System.Convert.ToInt32(dr.DBtoInt("VareGrpID"));
            with_1._VareGrpNavn = dr.DBtoString("VareGrpNr");
            
            with_1._ProvisionRate = System.Convert.ToDecimal(dr.DBtoDecimal("ProvisionRate"));
            
            PopulateStandard(dr, c);
        }
        
#endregion
        
#region  Stored Procedure Names
        
        //PROCEDURE [vicjos1_sysadm].[Co2Db_KundeGrp_Pris_Delete]
        //PROCEDURE [vicjos1_sysadm].[Co2Db_KundeGrp_Pris_Insert]
        //PROCEDURE [vicjos1_sysadm].[Co2Db_KundeGrp_Pris_SelectAll]
        //PROCEDURE [vicjos1_sysadm].[Co2Db_KundeGrp_Pris_SelectByKundeGrp]
        //PROCEDURE [vicjos1_sysadm].[Co2Db_KundeGrp_Pris_SelectID]
        //PROCEDURE [vicjos1_sysadm].[Co2Db_KundeGrp_Pris_SelectIDByKundeGrp]
        //PROCEDURE [vicjos1_sysadm].[Co2Db_KundeGrp_Pris_SelectOne]
        //PROCEDURE [vicjos1_sysadm].[Co2Db_KundeGrp_Pris_Update]
        //PROCEDURE [vicjos1_sysadm].[Co2Db_KundeGrp_Pris_SelectBySearch]
        
        private const string _SQLDelete = "Co2Db_KundeGrp_Pris_Delete";
        private const string _SQLInsert = "Co2Db_KundeGrp_Pris_Insert";
        private const string _SQLUpdate = "Co2Db_KundeGrp_Pris_Update";
        private const string _SQLSelectAll = "Co2Db_KundeGrp_Pris_SelectAll";
        private const string _SQLSelectID = "Co2Db_KundeGrp_Pris_SelectID";
        private const string _SQLSelectOne = "Co2Db_KundeGrp_Pris_SelectOne";
        
        private const string _SQLSelectBySearch = "Co2Db_KundeGrp_Pris_SelectBySearch";
        
        private const string _SQLSelectByKundeGrp = "Co2Db_KundeGrp_Pris_SelectByKundeGrp";
        private const string _SQLSelectIDByKundeGrp = "Co2Db_KundeGrp_Pris_SelectIDByKundeGrp";
        
        private const string _SQLSelectByKundeGrpVareID = "Co2Db_KundeGrp_Pris_SelectByKundeGrpVareID";
        private const string _SQLSelectByKundeGrpVareNr = "Co2Db_KundeGrp_Pris_SelectByKundeGrpVareNr";
        
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
            db.AddInt("ID", ID);
            int retval = db.ExecuteNonQuery(_SQLDelete);
            return retval;
        }
        public static int Delete(KundeGrp_Pris c)
        {
            return Delete(c.ID);
        }
        
        //  SET [KundeGrpID] = <KundeGrpID, int,>
        //     ,[VareID] = <VareID, int,>
        //     ,[status] = <status, int,>
        //     ,[Rabat] = <Rabat, float,>
        //     ,[FragtGebyr] = <FragtGebyr, float,>
        //     ,[Dato] = <Dato, datetime,>
        public static int Insert(KundeGrpStatusEnum Status, int KundeGrpID, int VareID, decimal Rabat, decimal FragtGebyr, DateTime Dato)
        {
            KundeGrp_Pris c = new KundeGrp_Pris();
            c.Status = Status;
            c.KundeGrpID = KundeGrpID;
            c.VareID = VareID;
            
            c.Rabat = Rabat;
            c.FragtGebyr = FragtGebyr;
            c.Dato = Dato;
            
            return Insert(c);
        }
        public static int Insert(KundeGrp_Pris c)
        {
            DBAccess db = new DBAccess();
            
            c.Dato = DateTime.Now;
            
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
                c.ID = Funktioner.ToInt(objParam.Value, -1); //Integer.Parse(objParam.Value.ToString)
                return c.ID; //Integer.Parse(objParam.Value.ToString)
            }
            else
            {
                return -1;
            }
        }
        public int Insert()
        {
            return Insert(this);
        }
        
        public static int Update(int ID, KundeGrpStatusEnum Status, int KundeGrpID, int VareID, decimal Rabat, decimal FragtGebyr, DateTime Dato)
        {
            KundeGrp_Pris c = new KundeGrp_Pris(ID);
            if (c.Status <= (int) KundeGrpStatusEnum.Opret)
            {
                c.Status = KundeGrpStatusEnum.Aktiv;
            }
            c.Status = Status;
            c.KundeGrpID = KundeGrpID;
            c.VareID = VareID;
            
            c.Rabat = Rabat;
            c.FragtGebyr = FragtGebyr;
            c.Dato = Dato;
            
            return Update(c);
        }
        public static int Update(KundeGrp_Pris c)
        {
            DBAccess db = new DBAccess();
            
            c.Dato = DateTime.Now;
            db.AddInt("ID", c.ID);
            AddParms(ref db, c);
            
            int retval = db.ExecuteNonQuery(_SQLUpdate);
            return retval;
        }
        public int Update()
        {
            return Update(this);
        }
        
#endregion
        
#region  Get data
        
        public static KundeGrp_Pris GetCompany_VarePris(int CompanyID, int VareID)
        {
            KundeGrp kg = KundeGrp.GetKundeGrp_Company(CompanyID);
            KundeGrp_Pris kgp = KundeGrp_Pris.GetKundeGrp_VarePris(kg.ID, VareID);
            if (!kgp.loaded)
            {
                kgp = new KundeGrp_Pris();
                kgp.KundeGrpID = kg.ID;
                kgp.VareID = VareID;
                kgp.Rabat = 0;
                kgp.FragtGebyr = 99.989999999999995M;
                kgp.Insert();
            }
            return kgp;
        }
        
        public static KundeGrp_Pris GetKundeGrp_VarePris(int KundeGrpID, int VareID)
        {
            KundeGrp_Pris c = new KundeGrp_Pris();
            
            DBAccess db = new DBAccess();
            db.AddInt("KundeGrpID", KundeGrpID);
            db.AddInt("VareID", VareID);
            SqlDataReader dr = (SqlDataReader) (db.ExecuteReader(_SQLSelectByKundeGrpVareID));
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
        public static KundeGrp_Pris GetKundeGrp_VarePris(int KundeGrpID, string VareNr)
        {
            KundeGrp_Pris c = new KundeGrp_Pris();
            
            DBAccess db = new DBAccess();
            db.AddInt("KundeGrpID", KundeGrpID);
            db.AddNVarChar("VareNr", VareNr, 50);
            SqlDataReader dr = (SqlDataReader) (db.ExecuteReader(_SQLSelectByKundeGrpVareNr));
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
        
        public static KundeGrp_Pris GetKundeGrp_Pris(int ID)
        {
            KundeGrp_Pris c = new KundeGrp_Pris();
            
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
        public static DataSet GetKundeGrp_Pris_DS(int ID)
        {
            DBAccess db = new DBAccess();
            db.AddInt("ID", ID);
            DataSet ds = db.ExecuteDataSet(_SQLSelectID);
            return ds;
        }
        
        public static DataSet GetAllKundeGrp_Pris()
        {
            DBAccess db = new DBAccess();
            //db.Parameters.Add(New SqlParameter("@sUserName", username))
            DataSet ds = db.ExecuteDataSet(_SQLSelectAll);
            return ds;
        }
        
        public static DataSet GetKundeGrp_PrisbyKundeGrp(int KundeGrpID)
        {
            DBAccess db = new DBAccess();
            db.AddInt("KundeGrpID", KundeGrpID);
            DataSet ds = db.ExecuteDataSet(_SQLSelectByKundeGrp);
            return ds;
        }
        
        public static System.Collections.Generic.List<KundeGrp_Pris> GetKundeGrp_PrisList(int KundeGrpID)
        {
            return GetKundeGrp_PrisList(KundeGrpID, "VareNr", 1);
        }
        public static System.Collections.Generic.List<KundeGrp_Pris> GetKundeGrp_PrisList(int KundeGrpID, string Sort)
        {
            return GetKundeGrp_PrisList(KundeGrpID, Sort, 1);
        }
        public static System.Collections.Generic.List<KundeGrp_Pris> GetKundeGrp_PrisList(int KundeGrpID, string Sort, int SortDir)
        {
            System.Collections.Generic.List<KundeGrp_Pris> result = new System.Collections.Generic.List<KundeGrp_Pris>();
            int ID = -1;
            
            // @Sort='VareNr'
            // @Sort='VareNavn'
            // @Sort='ID'
            // @Sort='Dato'
            if (Sort == "")
            {
                Sort = "VareNr";
            }
            
            DBAccess db = new DBAccess();
            db.AddInt("KundeGrpID", KundeGrpID);
            db.AddNVarChar("Sort", Sort, 50);
            db.AddInt("SortDir", SortDir);
            
            SqlDataReader dr = default(SqlDataReader);
            try
            {
                dr = (System.Data.SqlClient.SqlDataReader) (db.ExecuteReader(_SQLSelectIDByKundeGrp));
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ID = System.Convert.ToInt32(dr.DBtoInt("ID"));
                        result.Add(KundeGrp_Pris.GetKundeGrp_Pris(ID));
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
        
        
        public static DataSet Search_KundeGrp_Pris(string Search)
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
        public static string Tags(string tekst, RescueTekniq.BOL.KundeGrp_Pris item)
        {
            string InvoceVat = ConfigurationManager.AppSettings["invoice.vat"];
            decimal vat = 25;
            try
            {
                vat = decimal.Parse(InvoceVat);
            }
            catch
            {
            }
            
            string res = "";
            StringBuilder sb = new StringBuilder(); // = New StringBuilder
            sb.Append(tekst);
            
            sb.Replace("[VARE.SALGSPRIS]", item.SalgsPris.ToString("C"));
            sb.Replace("[VARE.RABAT]", item.Rabat.ToString("0.0000 %"));
            
            sb.Replace("[VARE.PRIS]", item.KundePris.ToString("C"));
            sb.Replace("[VARE.KUNDEPRIS]", item.KundePris.ToString("C"));
            
            sb.Replace("[VARE.FRAGTPRIS]", item.FragtGebyr.ToString("C"));
            sb.Replace("[VARE.FRAGTGEBYR]", item.FragtGebyr.ToString("C"));
            
            decimal moms = 0;
            decimal total = 0;
            
            moms = (item.KundePris + item.FragtGebyr) * (vat / 100); //0.25
            total = item.KundePris + item.FragtGebyr + moms;
            
            sb.Replace("[VARE.MOMS]", moms.ToString("C"));
            sb.Replace("[VARE.TOTAL]", total.ToString("C"));
            
            res = sb.ToString();
            res = item.Vare.Tags(res);
            res = item.KundeGrp.Tags(res);
            return res;
        }
        
#endregion
        
    }
    
    
}
