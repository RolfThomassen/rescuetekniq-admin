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
    
    public class Vare : BaseObject
    {
        
#region  New
        
        public Vare()
        {
            
        }
        
        public Vare(int ID)
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
        
        public Vare(string VareNr)
        {
            
            if (VareNr.Trim() != "")
            {
                DBAccess db = new DBAccess();
                db.AddNVarChar("Varenr", VareNr, 50);
                SqlDataReader dr = (SqlDataReader) (db.ExecuteReader(_SQLSelectByVarenr));
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
        
        private int _VareGrpID = 8;
        private string _VareNr = "";
        private string _Navn = "";
        private string _Beskrivelse = "";
        private int _CurrencyID = -1;
        private decimal _CurrencyRate = -1;
        private decimal _Indkobspris = 0;
        private decimal _FragtPct = 0.055M;
        private decimal _Fragt = -1;
        private decimal _KostprisCurrency = -1;
        private decimal _KostprisDKK = -1;
        private decimal _DaekningsBidrag = 0;
        private decimal _DaekningsGrad = 210;
        private decimal _SalgsPris = -1;
        private VareStatusEnum _Status = VareStatusEnum.Initialize;
        private int _FragtID = -1;
        private string _FaktaArkPath = "";
        private string _SupplierItemNo = "";
        private string _SupplierItemName = "";
        
        private RescueTekniq.BOL.VareGrp _VareGrp = new RescueTekniq.BOL.VareGrp();
        private RescueTekniq.BOL.Currency _Currency = new RescueTekniq.BOL.Currency();
#endregion
        
#region  Private Metode
        
        private enum ReCalcPos
        {
            Top = 0, // fra toppen (har DG, default)
            DækningsGrad, // når du har DB
            DækningsBidrag, // når du har DG
            Dækning // når du har Salgspris
        }
        private void ReCalcVare(ReCalcPos Pos)
        {
            _Fragt = _Indkobspris * _FragtPct / 100;
            _KostprisCurrency = _Indkobspris + _Fragt;
            _KostprisDKK = _KostprisCurrency * (_CurrencyRate / 100);
            switch (Pos)
            {
                case ReCalcPos.DækningsGrad:
                    _SalgsPris = _KostprisDKK + _DaekningsBidrag;
                    _DaekningsGrad = (_DaekningsBidrag / _KostprisDKK + 1) * 100;
                    break;
                case ReCalcPos.DækningsBidrag:
                    _SalgsPris = _KostprisDKK * (_DaekningsGrad / 100);
                    _DaekningsBidrag = _SalgsPris - _KostprisDKK;
                    break;
                case ReCalcPos.Dækning:
                    _DaekningsBidrag = _SalgsPris - _KostprisDKK;
                    if (_KostprisDKK != 0M)
                    {
                        _DaekningsGrad = ((_DaekningsBidrag / _KostprisDKK) + 1) * 100;
                    }
                    else
                    {
                        _DaekningsGrad = 1; //((_DaekningsBidrag / _KostprisDKK) + 1) * 100
                    }
                    break;
                default:
                    _SalgsPris = _KostprisDKK * (_DaekningsGrad / 100);
                    _DaekningsBidrag = _SalgsPris - _KostprisDKK;
                    break;
            }
        }
        
#endregion
        
#region  Properties
        
        public int VareGrpID
        {
            get
            {
                return _VareGrpID;
            }
            set
            {
                _VareGrpID = value;
            }
        }
        
        public string VareNr
        {
            get
            {
                return _VareNr;
            }
            set
            {
                _VareNr = value;
            }
        }
        public string Navn
        {
            get
            {
                return _Navn;
            }
            set
            {
                _Navn = value;
            }
        }
        public string Beskrivelse
        {
            get
            {
                return _Beskrivelse;
            }
            set
            {
                _Beskrivelse = value;
            }
        }
        
        public int CurrencyID
        {
            get
            {
                return _CurrencyID;
            }
            set
            {
                if (value != _CurrencyID)
                {
                    _CurrencyID = value;
                    _CurrencyRate = Currency.CurrencyRate;
                    ReCalcVare(ReCalcPos.Top);
                }
            }
        }
        public decimal CurrencyRate
        {
            get
            {
                return _CurrencyRate;
            }
            set
            {
                if (value != _CurrencyRate)
                {
                    _CurrencyRate = value;
                    ReCalcVare(ReCalcPos.Top);
                }
            }
        }
        
        public decimal Indkobspris
        {
            get
            {
                return _Indkobspris;
            }
            set
            {
                if (value != _Indkobspris)
                {
                    _Indkobspris = value;
                    ReCalcVare(ReCalcPos.Top);
                }
            }
        }
        public decimal FragtPct
        {
            get
            {
                return _FragtPct;
            }
            set
            {
                if (value != _FragtPct)
                {
                    _FragtPct = value;
                    ReCalcVare(ReCalcPos.Top);
                }
            }
        }
        
        
        public decimal Fragt
        {
            get
            {
                return _Fragt;
            }
            protected set
            {
                if (value != _Fragt)
                {
                    _Fragt = value;
                    ReCalcVare(ReCalcPos.Top);
                }
            }
        }
        public decimal KostprisCurrency
        {
            get
            {
                return _KostprisCurrency;
            }
            protected set
            {
                if (value != _KostprisCurrency)
                {
                    _KostprisCurrency = value;
                    ReCalcVare(ReCalcPos.Top);
                }
            }
        }
        public decimal KostprisDKK
        {
            get
            {
                return _KostprisDKK;
            }
            protected set
            {
                if (value != _KostprisDKK)
                {
                    _KostprisDKK = value;
                    ReCalcVare(ReCalcPos.Top);
                }
            }
        }
        public decimal DaekningsBidrag
        {
            get
            {
                return _DaekningsBidrag;
            }
            protected set
            {
                if (value != _DaekningsBidrag)
                {
                    _DaekningsBidrag = value;
                    ReCalcVare(ReCalcPos.DækningsGrad);
                }
            }
        }
        public decimal DaekningsGrad
        {
            get
            {
                return _DaekningsGrad;
            }
            set
            {
                if (value != _DaekningsGrad)
                {
                    _DaekningsGrad = value;
                    ReCalcVare(ReCalcPos.DækningsBidrag);
                }
            }
        }
        public decimal SalgsPris
        {
            get
            {
                return _SalgsPris;
            }
            set
            {
                if (value != _SalgsPris)
                {
                    _SalgsPris = value;
                    ReCalcVare(ReCalcPos.Dækning);
                }
                else
                {
                }
            }
        }
        
        
        public VareStatusEnum Status
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
        
        public int FragtID
        {
            get
            {
                return _FragtID;
            }
            set
            {
                _FragtID = value;
            }
        }
        
        public string FaktaArkPath
        {
            get
            {
                return _FaktaArkPath;
            }
            set
            {
                _FaktaArkPath = value;
            }
        }
        
        public string SupplierItemNo
        {
            get
            {
                return _SupplierItemNo;
            }
            set
            {
                _SupplierItemNo = value;
            }
        }
        public string SupplierItemName
        {
            get
            {
                return _SupplierItemName;
            }
            set
            {
                _SupplierItemName = value;
            }
        }
        
        public RescueTekniq.BOL.VareGrp VareGrp
        {
            get
            {
                //Try
                if (_VareGrpID > 0)
                {
                    if (!_VareGrp.loaded)
                    {
                        _VareGrp = VareGrp.GetVareGrp(_VareGrpID);
                    }
                    else if (_VareGrp.ID != _VareGrpID)
                    {
                        _VareGrp = VareGrp.GetVareGrp(_VareGrpID);
                    }
                }
                //'Catch ex As Exception
                //End Try
                return _VareGrp;
            }
        }
        public RescueTekniq.BOL.Currency Currency
        {
            get
            {
                //'Try
                if (_CurrencyID > 0)
                {
                    if (!_Currency.loaded)
                    {
                        _Currency = Currency.GetCurrency(_CurrencyID);
                        CurrencyRate = _Currency.CurrencyRate;
                    }
                    else if (_Currency.ID != _CurrencyID)
                    {
                        _Currency = Currency.GetCurrency(_CurrencyID);
                        CurrencyRate = _Currency.CurrencyRate;
                    }
                }
                //Catch ex As Exception
                //End Try
                return _Currency;
            }
        }
        
        
#endregion
        
#region  Shared Populate
        
        //UPDATE Co2Db_Vare SET FG = Fragt, KPcur = KostprisCurrency, KPdkk = KostprisDKK, DB = DaekningsBidrag, DG = DaekningsGrad, SP = SalgsPris
        //,	@Fragt	float	= null
        //,	@KostPrisCUR	float	= null
        //,	@KostPrisDKK	float	= null
        //,	@DaekningsBidrag	float	= null
        //,	@DaekningsGrad float
        //,	@SalgsPris		float	= null
        
        private static void AddParms(ref DBAccess db, Vare rec)
        {
            var with_1 = rec;
            db.AddInt("VareGrpID", with_1.VareGrpID);
            db.AddNVarChar("VareNr", with_1.VareNr, 50);
            db.AddNVarChar("Navn", with_1.Navn, 255);
            db.AddNVarChar("Beskrivelse", with_1.Beskrivelse, -1);
            db.AddInt("CurrencyID", with_1.CurrencyID);
            db.AddDecimal("CurrencyRate", with_1.CurrencyRate);
            
            db.AddDecimal("Indkobspris", with_1.Indkobspris);
            db.AddDecimal("FragtPct", with_1.FragtPct);
            db.AddDecimal("Fragt", with_1.Fragt);
            
            db.AddDecimal("KostPrisCUR", with_1.KostprisCurrency);
            db.AddDecimal("KostPrisDKK", with_1.KostprisDKK);
            
            db.AddDecimal("DaekningsBidrag", with_1.DaekningsBidrag);
            db.AddDecimal("DaekningsGrad", with_1.DaekningsGrad);
            db.AddDecimal("SalgsPris", with_1.SalgsPris);
            
            db.AddInt("Status", (System.Int32) with_1.Status);
            db.AddInt("FragtID", with_1.FragtID);
            
            db.AddNVarChar("FaktaArkPath", with_1.FaktaArkPath, 250);
            
            db.AddNVarChar("SupplierItemNo", with_1.SupplierItemNo, 50);
            db.AddNVarChar("SupplierItemName", with_1.SupplierItemName, 255);
            
            AddParmsStandard(db, rec);
        }
        
        private static void Populate(System.Data.SqlClient.SqlDataReader dr, Vare rec)
        {
            var with_1 = rec;
            with_1.VareGrpID = System.Convert.ToInt32(dr.DBtoInt("VareGrpID"));
            with_1.VareNr = dr.DBtoString("VareNr");
            with_1.Navn = dr.DBtoString("Navn");
            with_1.Beskrivelse = dr.DBtoString("Beskrivelse");
            with_1._CurrencyID = System.Convert.ToInt32(dr.DBtoInt("CurrencyID"));
            with_1._CurrencyRate = System.Convert.ToDecimal(dr.DBtoDecimal("CurrencyRate"));
            with_1._Indkobspris = System.Convert.ToDecimal(dr.DBtoDecimal("Indkobspris"));
            with_1._FragtPct = System.Convert.ToDecimal(dr.DBtoDecimal("FragtPct"));
            with_1._Fragt = System.Convert.ToDecimal(dr.DBtoDecimal("Fragt"));
            with_1._KostprisCurrency = System.Convert.ToDecimal(dr.DBtoDecimal("KostprisCurrency"));
            with_1._KostprisDKK = System.Convert.ToDecimal(dr.DBtoDecimal("KostprisDKK"));
            with_1._DaekningsBidrag = System.Convert.ToDecimal(dr.DBtoDecimal("DaekningsBidrag"));
            with_1._DaekningsGrad = System.Convert.ToDecimal(dr.DBtoDecimal("DaekningsGrad"));
            with_1._SalgsPris = System.Convert.ToDecimal(dr.DBtoDecimal("SalgsPris"));
            with_1.Status = (RescueTekniq.BOL.VareStatusEnum) (dr.DBtoInt("Status"));
            with_1.FragtID = System.Convert.ToInt32(dr.DBtoInt("FragtID"));
            with_1.FaktaArkPath = dr.DBtoString("FaktaArkPath");
            with_1.SupplierItemNo = dr.DBtoString("SupplierItemNo");
            with_1.SupplierItemName = dr.DBtoString("SupplierItemName");
            PopulateStandard(dr, rec);
        }
        
        private static void Populate(System.Data.DataTableReader dr, Vare rec)
        {
            var with_1 = rec;
            with_1.VareGrpID = System.Convert.ToInt32(dr.DBtoInt("VareGrpID"));
            with_1.VareNr = dr.DBtoString("VareNr");
            with_1.Navn = dr.DBtoString("Navn");
            with_1.Beskrivelse = dr.DBtoString("Beskrivelse");
            with_1._CurrencyID = System.Convert.ToInt32(dr.DBtoInt("CurrencyID"));
            with_1._CurrencyRate = System.Convert.ToDecimal(dr.DBtoDecimal("CurrencyRate"));
            with_1._Indkobspris = System.Convert.ToDecimal(dr.DBtoDecimal("Indkobspris"));
            with_1._FragtPct = System.Convert.ToDecimal(dr.DBtoDecimal("FragtPct"));
            with_1._Fragt = System.Convert.ToDecimal(dr.DBtoDecimal("Fragt"));
            with_1._KostprisCurrency = System.Convert.ToDecimal(dr.DBtoDecimal("KostprisCurrency"));
            with_1._KostprisDKK = System.Convert.ToDecimal(dr.DBtoDecimal("KostprisDKK"));
            with_1._DaekningsBidrag = System.Convert.ToDecimal(dr.DBtoDecimal("DaekningsBidrag"));
            with_1._DaekningsGrad = System.Convert.ToDecimal(dr.DBtoDecimal("DaekningsGrad"));
            with_1._SalgsPris = System.Convert.ToDecimal(dr.DBtoDecimal("SalgsPris"));
            with_1.Status = (RescueTekniq.BOL.VareStatusEnum) (dr.DBtoInt("Status"));
            with_1.FragtID = System.Convert.ToInt32(dr.DBtoInt("FragtID"));
            with_1.FaktaArkPath = dr.DBtoString("FaktaArkPath");
            with_1.SupplierItemNo = dr.DBtoString("SupplierItemNo");
            with_1.SupplierItemName = dr.DBtoString("SupplierItemName");
            PopulateStandard(dr, rec);
        }
        
#endregion
        
#region  Stored Procedure Names
        private const string _SQLDelete = "Co2Db_Vare_Delete";
        private const string _SQLInsert = "Co2Db_Vare_Insert";
        private const string _SQLUpdate = "Co2Db_Vare_Update";
        private const string _SQLSelectAll = "Co2Db_Vare_SelectAll";
        private const string _SQLSelectID = "Co2Db_Vare_SelectID";
        private const string _SQLSelectOne = "Co2Db_Vare_SelectOne";
        private const string _SQLSelectAllVareGrp = "Co2Db_Vare_SelectAllVareGrp";
        private const string _SQLSelectBySearch = "Co2Db_Vare_SelectBySearch";
        private const string _SQLSelectByVarenr = "Co2Db_Vare_SelectByVarenr";
        
        private const string _SQLSelectVareList = "Co2Db_Vare_VareList";
        
        private const string _SQLSearchVareNr = "Co2Db_Vare_SearchByVareNr";
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
            AddLog(Status: "Vare", Logtext: string.Format("Delete Vare: ID:{0}", ID), Metode: "Delete");
            return retval;
        }
        public static int Delete(Vare c)
        {
            return Delete(c.ID);
        }
        
        public int Insert()
        {
            return Insert(this);
        }
        public static int Insert(Vare c)
        {
            DBAccess db = new DBAccess();
            
            if (c.Status == VareStatusEnum.Initialize)
            {
                c.Status = VareStatusEnum.Aktiv;
            }
            AddParms(ref db, c);
            
            SqlParameter objParam = new SqlParameter("@ID", 0);
            objParam.Direction = ParameterDirection.Output;
            db.Parameters.Add(objParam);
            int retval = db.ExecuteNonQuery(_SQLInsert);
            if (retval == 1)
            {
                c.ID = int.Parse(objParam.Value.ToString());
                AddLog(Status: "Vare", Logtext: string.Format("Create Vare: ID:{0} No:{1} Name:{2}", c.ID, c.VareNr, c.Navn), Metode: "Insert");
                return c.ID; //Integer.Parse(objParam.Value.ToString)
            }
            else
            {
                AddLog(Status: "Vare", Logtext: string.Format("Failure to Create Vare: No:{0} Name:{1}", c.VareNr, c.Navn), logtype: LogTypeEnum.Error, Metode: "Insert");
                return -1;
            }
        }
        public static int Insert(int VareGrpID, string VareNr, string Navn, string Beskrivelse, int CurrencyID, decimal CurrencyRate, decimal Indkobspris, decimal FragtPct, decimal DaekningsGrad, VareStatusEnum Status, int FragtID, string FaktaArkPath)
        {
            Vare c = new Vare();
            c.VareGrpID = VareGrpID;
            c.VareNr = VareNr;
            c.Navn = Navn;
            c.Beskrivelse = Beskrivelse;
            
            c.CurrencyID = CurrencyID;
            c.CurrencyRate = CurrencyRate;
            
            c.Indkobspris = Indkobspris;
            c.FragtPct = FragtPct;
            
            c.DaekningsGrad = DaekningsGrad;
            
            c.Status = VareStatusEnum.Aktiv;
            c.FragtID = FragtID;
            
            c.FaktaArkPath = FaktaArkPath;
            
            return Insert(c);
        }
        
        public int Update()
        {
            return Update(this);
        }
        public static int Update(Vare c)
        {
            DBAccess db = new DBAccess();
            
            db.AddInt("ID", c.ID);
            AddParms(ref db, c);
            
            int retval = db.ExecuteNonQuery("Co2Db_Vare_Update");
            AddLog(Status: "Vare", Logtext: string.Format("Update Vare: ID:{0} No:{1} ", c.ID, c.VareNr), Metode: "Update");
            
            return retval;
        }
        public static int Update(int ID, int VareGrpID, string VareNr, string Navn, string Beskrivelse, int CurrencyID, decimal CurrencyRate, decimal Indkobspris, decimal FragtPct, decimal DaekningsGrad, TilbudStatusEnum Status, int FragtID, string FaktaArkPath)
        {
            Vare c = new Vare(ID);
            c.VareGrpID = VareGrpID;
            c.VareNr = VareNr;
            c.Navn = Navn;
            c.Beskrivelse = Beskrivelse;
            
            c.CurrencyID = CurrencyID;
            c.CurrencyRate = CurrencyRate;
            
            c.Indkobspris = Indkobspris;
            c.FragtPct = FragtPct;
            
            c.DaekningsGrad = DaekningsGrad;
            
            c.Status = (RescueTekniq.BOL.VareStatusEnum) Status;
            c.FragtID = FragtID;
            
            c.FaktaArkPath = FaktaArkPath;
            
            return Update(c);
        }
        
        public int Save()
        {
            return Save(this);
        }
        public static int Save(Vare rec)
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
        
        public static int UpdateSupplierItemNo(int ID, string SupplierItemNo)
        {
            int res = -1;
            Vare rec = new Vare(ID);
            if (rec.isLoaded)
            {
                rec.SupplierItemNo = SupplierItemNo;
                res = rec.Save();
            }
            return res;
        }
        
        public static int UpdateSupplierItemName(int ID, string SupplierItemName)
        {
            int res = -1;
            Vare rec = new Vare(ID);
            if (rec.isLoaded)
            {
                rec.SupplierItemName = SupplierItemName;
                res = rec.Save();
            }
            return res;
        }
        
#endregion
        
#region  Get data
        
        public static DataSet GetAllVare()
        {
            DBAccess db = new DBAccess();
            //db.Parameters.Add(New SqlParameter("@sUserName", username))
            DataSet ds = db.ExecuteDataSet(_SQLSelectAll);
            return ds;
        }
        
        public static DataSet GetAllVareVareGrp(int VareGrpID)
        {
            DBAccess db = new DBAccess();
            db.AddInt("VareGrpID", VareGrpID);
            DataSet ds = db.ExecuteDataSet(_SQLSelectAllVareGrp);
            return ds;
        }
        
        //  'SELECT [ID]
        //    ,[VareID]
        //    ,[VareNr]
        //    ,[VareNavn]
        //    ,[VareNrNavn]
        //    ,[VarePipeName]
        //    ,[Status]
        //    ,[StatusText]
        //FROM [vicjos1_rescuetekniq_test].[vicjos1_sysadm].[vw_Co2Db_VareList]
        //vw_Co2Db_VareList
        //Co2Db_Vare_VareList
        
        public static DataSet GetVareList()
        {
            DBAccess db = new DBAccess();
            DataSet ds = db.ExecuteDataSet(_SQLSelectVareList);
            return ds;
        }
        
        public static DataSet GetVareListVareGrp(int VareGrpID)
        {
            DBAccess db = new DBAccess();
            db.AddInt("VareGrpID", VareGrpID);
            DataSet ds = db.ExecuteDataSet(_SQLSelectVareList);
            return ds;
        }
        
        public static System.Collections.Generic.List<Vare> GetAllVareVareGrpList(int VareGrpID)
        {
            System.Collections.Generic.List<Vare> result = new System.Collections.Generic.List<Vare>();
            
            DBAccess db = new DBAccess();
            db.AddInt("VareGrpID", VareGrpID);
            
            SqlDataReader dr = default(SqlDataReader);
            try
            {
                dr = (System.Data.SqlClient.SqlDataReader) (db.ExecuteReader(_SQLSelectAllVareGrp));
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        result.Add(Vare.GetVare(System.Convert.ToInt32(dr.DBtoInt("ID"))));
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
        
        public static DataSet GetVareDS(int ID)
        {
            DBAccess db = new DBAccess();
            db.AddInt("ID", ID);
            DataSet ds = db.ExecuteDataSet(_SQLSelectID);
            return ds;
        }
        
        public static Vare GetVare(int ID)
        {
            DBAccess db = new DBAccess();
            db.AddInt("ID", ID);
            Vare c = new Vare();
            try
            {
                SqlDataReader dr = (SqlDataReader) (db.ExecuteReader(_SQLSelectOne));
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Populate(dr, c);
                    }
                    dr.Close();
                }
            }
            catch (Exception)
            {
            }
            return c;
        }
        public static Vare GetVare(string Varenr)
        {
            DBAccess db = new DBAccess();
            Vare c = new Vare();
            db.AddNVarChar("Varenr", Varenr, 250);
            try
            {
                SqlDataReader dr = (SqlDataReader) (db.ExecuteReader(_SQLSelectByVarenr));
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Populate(dr, c);
                    }
                }
                dr.Close();
            }
            catch (Exception)
            {
            }
            return c;
        }
        
        public static DataSet Search_Vare(string Search)
        {
            return Search_Vare(Search, VareStatusEnum.Alle, System.Convert.ToInt32(VareStatusEnum.Alle));
        }
        public static DataSet Search_Vare(string Search, VareStatusEnum Status)
        {
            return Search_Vare(Search, Status, System.Convert.ToInt32(VareStatusEnum.Alle));
        }
        public static DataSet Search_Vare(string Search, VareStatusEnum Status, int VareGrpID)
        {
            DataSet ds = new DataSet();
            DataSet dsTemp = new DataSet();
            bool flag = false;
            DBAccess db = new DBAccess();
            
            string[] arr = Search.Split(' ');
            foreach (string s in arr)
            {
                db.AddNVarChar("Search", s, 50);
                db.AddInt("Status", System.Convert.ToInt32(Status));
                db.AddInt("VareGrpID", VareGrpID);
                
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
        
        public static List<Vare> Search_Varelist(string Search, VareStatusEnum Status, int VareGrpID)
        {
            DataSet ds = new DataSet();
            DataSet dsTemp = new DataSet();
            bool flag = false;
            DBAccess db = new DBAccess();
            List<Vare> res = new List<Vare>();
            
            string[] arr = Search.Split(' ');
            foreach (string s in arr)
            {
                db.AddNVarChar("Search", s, 50);
                db.AddInt("Status", System.Convert.ToInt32(Status));
                db.AddInt("VareGrpID", VareGrpID);
                
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
            
            Vare rec = default(Vare);
            DataTableReader dr = ds.Tables[0].CreateDataReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    rec = new Vare();
                    Populate(dr, rec);
                    res.Add(rec);
                }
            }
            
            return res;
            
        }
        
        
        public static DataSet Search_VareNr(string Search)
        {
            return Search_VareNr(Search, VareStatusEnum.Alle, System.Convert.ToInt32(VareStatusEnum.Alle));
        }
        public static DataSet Search_VareNr(string Search, VareStatusEnum Status)
        {
            return Search_VareNr(Search, Status, System.Convert.ToInt32(VareStatusEnum.Alle));
        }
        public static DataSet Search_VareNr(string Search, VareStatusEnum Status, int VareGrpID)
        {
            DataSet ds = new DataSet();
            DataSet dsTemp = new DataSet();
            bool flag = false;
            DBAccess db = new DBAccess();
            
            string[] arr = Search.Split(' ');
            foreach (string s in arr)
            {
                db.AddNVarChar("Search", s, 50);
                db.AddInt("Status", System.Convert.ToInt32(Status));
                db.AddInt("VareGrpID", VareGrpID);
                
                dsTemp = db.ExecuteDataSet(_SQLSearchVareNr);
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
        public static string Tags(string tekst, RescueTekniq.BOL.Vare item)
        {
            string res = "";
            StringBuilder sb = new StringBuilder();
            sb.Append(tekst);
            sb.Replace("[VARE.VARENR]", item.VareNr);
            
            sb.Replace("[VARE.NAVN]", item.Navn);
            sb.Replace("[VARE.MODEL]", item.Navn);
            
            sb.Replace("[VARE.BESKRIVELSE]", item.Beskrivelse);
            
            sb.Replace("[VARE.KOSTPRIS]", System.Convert.ToString(item.KostprisDKK));
            sb.Replace("[VARE.VEJLPRIS]", System.Convert.ToString(item.SalgsPris));
            
            sb.Replace("[VARE.SUPPLIERITEMNO]", item.SupplierItemNo);
            sb.Replace("[VARE.LEVARANDØRVARENR]", item.SupplierItemNo);
            
            sb.Replace("[VARE.CURRENCY.CODE]", "[CURRENCY.CODE]");
            sb.Replace("[VARE.CURRENCY.DATE]", "[CURRENCY.DATE]");
            sb.Replace("[VARE.CURRENCY.DESC]", "[CURRENCY.DESC]");
            sb.Replace("[VARE.CURRENCY.RATE]", "[CURRENCY.RATE]");
            sb.Replace("[VARE.CURRENCY.SYMBOL]", "[CURRENCY.SYMBOL]");
            
            sb.Replace("[VARE.CURRENCYCODE]", "[CURRENCY.CODE]");
            sb.Replace("[VARE.CURRENCYDATE]", "[CURRENCY.DATE]");
            sb.Replace("[VARE.CURRENCYDESC]", "[CURRENCY.DESC]");
            sb.Replace("[VARE.CURRENCYRATE]", "[CURRENCY.RATE]");
            sb.Replace("[VARE.CURRENCYSYMBOL]", "[CURRENCY.SYMBOL]");
            
            
            res = sb.ToString();
            res = item.Currency.Tags(res);
            res = item.VareGrp.Tags(res);
            
            return res;
        }
        
#endregion
        
    }
    
    
}
