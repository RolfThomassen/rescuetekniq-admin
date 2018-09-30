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
    
    //   SELECT ID, TilbudID, VareID, Salgspris, Tilbudspris, Rabat, Status, Pos, Antal
    //   FROM Co2Db_Tilbudslinie
    //	ID	int
    //	TilbudID	int
    //	VareID	int
    //	Salgspris	float
    //	Tilbudspris	float
    //	Rabat	float
    //	Status	int
    //	Pos	int
    //	Antal	int
    
    //Public Class TilbudsLinier
    //    Inherits List(Of TilbudsLinie)
    
    //End Class
    
    public class TilbudsLinie : BaseObject
    {
        
#region  Privates
        
        private int _TilbudID = -1;
        private int _VareID = -1;
        
        private decimal _Salgspris = 0;
        private decimal _Tilbudspris = 0;
        private decimal _Rabat = 0;
        private TilbudStatusEnum _Status = TilbudStatusEnum.Rekvireret; //Aktiv
        private int _Pos = -1;
        private int _Antal = 1;
        
        private decimal _TotalTilbudsSalgsPris = 0;
        
        //   tilbud Vare
        private Vare _Vare = new Vare();
        
#endregion
        
#region  New
        
        public TilbudsLinie()
        {
            
            _TilbudID = -1;
            _VareID = -1;
            
            _Salgspris = 0;
            _Tilbudspris = 0;
            _Rabat = 0;
            _Status = TilbudStatusEnum.Rekvireret; //Aktiv
            _Pos = -1;
            _Antal = 1;
            _TotalTilbudsSalgsPris = 0;
            
        }
        
        public TilbudsLinie(int ID)
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
        
#region  Properties
        
        public int TilbudID
        {
            get
            {
                return _TilbudID;
            }
            set
            {
                _TilbudID = value;
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
                try
                {
                    if (_VareID > 0)
                    {
                        if (Vare.loaded)
                        {
                            if (Vare.ID != _VareID)
                            {
                                _Vare = Vare.GetVare(_VareID);
                            }
                            Salgspris = Vare.SalgsPris;
                        }
                        else
                        {
                            _Vare = Vare.GetVare(_VareID);
                            Salgspris = Vare.SalgsPris;
                        }
                    }
                }
                catch (Exception)
                {
                }
            }
        }
        public string VareNavn
        {
            get
            {
                return Vare.Navn;
            }
            set
            {
                
            }
        }
        public string VareNr
        {
            get
            {
                return Vare.VareNr;
            }
            set
            {
                _Vare = Vare.GetVare(value);
                Salgspris = Vare.SalgsPris;
            }
        }
        
        
        public decimal Salgspris
        {
            get
            {
                return _Salgspris;
            }
            set
            {
                _Salgspris = value;
            }
        }
        
        public decimal Tilbudspris
        {
            get
            {
                return _Tilbudspris;
            }
            set
            {
                _Tilbudspris = value;
            }
        }
        
        public decimal Rabat
        {
            get
            {
                return _Rabat;
            }
            set
            {
                _Rabat = value;
            }
        }
        
        public TilbudStatusEnum Status
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
        
        public int Pos
        {
            get
            {
                return _Pos;
            }
            set
            {
                _Pos = value;
            }
        }
        
        public int Antal
        {
            get
            {
                return _Antal;
            }
            set
            {
                _Antal = value;
            }
        }
        
        public decimal TotalTilbudsSalgsPris
        {
            get
            {
                _TotalTilbudsSalgsPris = 0;
                try
                {
                    _TotalTilbudsSalgsPris = GetTotalTilbudsSalgsPris(TilbudID);
                }
                catch (Exception)
                {
                }
                return _TotalTilbudsSalgsPris;
            }
        }
        
        public RescueTekniq.BOL.Vare Vare
        {
            get
            {
                return _Vare;
            }
        }
        
#endregion
        
#region  Stored Procedure Names
        private const string _SQLDelete = "Co2Db_TilbudsLinie_Delete";
        private const string _SQLInsert = "Co2Db_TilbudsLinie_Insert";
        private const string _SQLUpdate = "Co2Db_TilbudsLinie_Update";
        private const string _SQLSelectAll = "Co2Db_TilbudsLinie_SelectAll";
        private const string _SQLSelectAllID = "Co2Db_TilbudsLinie_SelectAllID";
        private const string _SQLSelectID = "Co2Db_TilbudsLinie_SelectID";
        private const string _SQLSelectOne = "Co2Db_TilbudsLinie_SelectOne";
        private const string _SQLTotalTilbudsPris = "Co2Db_TilbudsLinie_TotalTilbudsPris";
#endregion
        
#region  Shared Populate
        
        private static void AddParms(ref DBAccess db, TilbudsLinie c)
        {
            var with_1 = c;
            db.AddInt("TilbudID", with_1.TilbudID);
            db.AddInt("VareID", with_1.VareID);
            
            db.AddDecimal("Salgspris", with_1.Salgspris);
            db.AddDecimal("Rabat", with_1.Rabat);
            db.AddDecimal("Tilbudspris", with_1.Tilbudspris);
            
            db.AddInt("Status", (System.Int32) with_1.Status);
            db.AddInt("Pos", with_1.Pos);
            db.AddInt("Antal", with_1.Antal);
            AddParmsStandard(db, c);
        }
        
        private static void Populate(SqlDataReader dr, TilbudsLinie c)
        {
            var with_1 = c;
            with_1.TilbudID = System.Convert.ToInt32(dr.DBtoInt("TilbudID"));
            with_1.VareID = System.Convert.ToInt32(dr.DBtoInt("VareID"));
            
            with_1.Salgspris = dr.DBtoDecimal("Salgspris");
            with_1.Rabat = System.Convert.ToDecimal(dr.DBtoDecimal("Rabat"));
            with_1.Tilbudspris = System.Convert.ToDecimal(dr.DBtoDecimal("Tilbudspris"));
            
            with_1.Status = (RescueTekniq.BOL.TilbudStatusEnum) (dr.DBtoInt("Status"));
            with_1.Pos = System.Convert.ToInt32(dr.DBtoInt("Pos"));
            with_1.Antal = System.Convert.ToInt32(dr.DBtoInt("Antal"));
            PopulateStandard(dr, c);
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
            TilbudsLinie item = new TilbudsLinie(ID);
            return item.Delete();
        }
        public static int Delete(TilbudsLinie tl)
        {
            DBAccess db = new DBAccess();
            db.AddInt("ID", tl.ID);
            int retval = db.ExecuteNonQuery(_SQLDelete);
            AddLog(Status: "TilbudsLinie", Logtext: string.Format("Delete TilbudsLinie: ID:{0} Tilbud.ID:{1} ", tl.ID, tl.TilbudID), Metode: "Delete");
            return retval;
        }
        
        public int Insert()
        {
            return Insert(this);
        }
        public static int Insert(TilbudsLinie c)
        {
            DBAccess db = new DBAccess();
            
            AddParms(ref db, c);
            SqlParameter objParam = new SqlParameter("@ID", 0);
            objParam.Direction = ParameterDirection.Output;
            db.Parameters.Add(objParam);
            
            int retval = db.ExecuteNonQuery(_SQLInsert);
            if (retval == 1)
            {
                c.ID = Funktioner.ToInt(objParam.Value, -1); //Integer.Parse(objParam.Value.ToString)
                AddLog(Status: "TilbudsLinie", Logtext: string.Format("Create TilbudsLinie: ID:{0} Tilbud.ID:{1} ", c.ID, c.TilbudID), Metode: "Insert");
                return c.ID; //Integer.Parse(objParam.Value.ToString)
            }
            else
            {
                AddLog(Status: "TilbudsLinie", Logtext: string.Format("Failure to Create TilbudsLinie: Tilbud.ID:{0}", c.TilbudID), logtype: LogTypeEnum.Error, Metode: "Insert");
                return -1;
            }
        }
        public static int Insert(int TilbudID, int VareID, decimal Salgspris, decimal Tilbudspris, decimal Rabat, TilbudStatusEnum Status, int Pos, int Antal)
        {
            TilbudsLinie c = new TilbudsLinie();
            c.TilbudID = TilbudID;
            c.VareID = VareID;
            c.Salgspris = Salgspris;
            c.Rabat = Rabat;
            c.Tilbudspris = Tilbudspris;
            c.Status = Status;
            c.Pos = Pos;
            c.Antal = Antal;
            return Insert(c);
        }
        
        public int Update()
        {
            return Update(this);
        }
        public static int Update(TilbudsLinie c)
        {
            DBAccess db = new DBAccess();
            
            db.AddInt("ID", c.ID);
            AddParms(ref db, c);
            
            int retval = db.ExecuteNonQuery(_SQLUpdate);
            AddLog(Status: "TilbudsLinie", Logtext: string.Format("Update TilbudsLinie: ID:{0} Tilbud.ID:{1} ", c.ID, c.TilbudID), Metode: "Update");
            return retval;
        }
        public static int Update(int ID, int TilbudID, int VareID, decimal Salgspris, decimal Tilbudspris, decimal Rabat, TilbudStatusEnum Status, int Pos, int Antal)
        {
            TilbudsLinie c = new TilbudsLinie(ID);
            c.TilbudID = TilbudID;
            c.VareID = VareID;
            c.Salgspris = Salgspris;
            c.Rabat = Rabat;
            c.Tilbudspris = Tilbudspris;
            c.Status = Status;
            c.Pos = Pos;
            c.Antal = Antal;
            
            return Update(c);
        }
        
#endregion
        
#region  Get Data
        
        public static DataSet GetTilbudlinier(int TilbudID)
        {
            DBAccess db = new DBAccess();
            db.AddInt("TilbudID", TilbudID);
            DataSet ds = db.ExecuteDataSet(_SQLSelectAll);
            return ds;
        }
        
        public static System.Collections.Generic.List<TilbudsLinie> GetTilbudsLinierList(int TilbudID)
        {
            System.Collections.Generic.List<TilbudsLinie> result = new System.Collections.Generic.List<TilbudsLinie>();
            
            DBAccess db = new DBAccess();
            db.AddInt("TilbudID", TilbudID);
            
            SqlDataReader dr = default(SqlDataReader);
            try
            {
                dr = (System.Data.SqlClient.SqlDataReader) (db.ExecuteReader(_SQLSelectAll)); //CType(db.ExecuteReader(_SQLSelectAllID), SqlDataReader)
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        result.Add(TilbudsLinie.GetTilbudsLinie(System.Convert.ToInt32(dr.DBtoInt("ID"))));
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
        
        public static DataSet GetTilbudsLinieDS(int ID)
        {
            DBAccess db = new DBAccess();
            db.AddInt("ID", ID);
            DataSet ds = db.ExecuteDataSet(_SQLSelectID);
            return ds;
        }
        
        public static TilbudsLinie GetTilbudsLinie(int ID)
        {
            DBAccess db = new DBAccess();
            db.Parameters.Add(new SqlParameter("@ID", ID));
            SqlDataReader dr = (SqlDataReader) (db.ExecuteReader(_SQLSelectOne));
            if (dr.HasRows)
            {
                TilbudsLinie c = new TilbudsLinie();
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
        
        public static decimal GetTotalTilbudsSalgsPris(int TilbudID)
        {
            DBAccess db = new DBAccess();
            db.AddInt("TilbudID", TilbudID);
            decimal res = 0;
            try
            {
                res = Funktioner.ToDecimal(db.ExecuteScalar(_SQLTotalTilbudsPris));
            }
            catch (Exception)
            {
                res = 0;
            }
            return res;
        }
        
#endregion
        
#endregion
        
    }
    
    
}
