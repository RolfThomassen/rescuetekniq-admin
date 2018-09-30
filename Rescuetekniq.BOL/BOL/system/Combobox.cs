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
    public sealed class Combobox1
    {
        
        public static string GetTitle(string field, int idx)
        {
            return Combobox.GetTitle(field, idx);
        }
        
    }
    
    public class Combobox : BaseObject
    {
        
#region  Privates
        
        private string _Field = "";
        private int _Index = 0;
        private int _Sort = 0;
        private string _Title = "";
        private string _Value = "";
        private string _Content = "";
        private bool _Valid = true;
        private bool _Hidden = false;
        private DateTime _Dato;
        private decimal _Tal = 0;
        private int _FieldType = 0;
        
#endregion
        
#region  New
        
        public Combobox()
        {
            Valid = true;
            //IP = ""
            //	UserID =  'DBNull	' 'Profile.Virksomhed.CompanyID = -1
        }
        
        public Combobox(int ID)
        {
            this.ID = ID;
            
            if (ID > 0)
            {
                DBAccess db = new DBAccess();
                db.AddInt("ID", ID);
                SqlDataReader dr = (SqlDataReader) (db.ExecuteReader(_SQLSelectID));
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
        
#region  Public Properties
        
        public bool Valid
        {
            get
            {
                return _Valid;
            }
            set
            {
                _Valid = value;
            }
        }
        
        public bool Hidden
        {
            get
            {
                return _Hidden;
            }
            set
            {
                _Hidden = value;
            }
        }
        
        
        public string Field
        {
            get
            {
                return _Field;
            }
            set
            {
                _Field = value;
            }
        }
        
        public int Idx
        {
            get
            {
                return _Index;
            }
            set
            {
                _Index = value;
            }
        }
        
        public string Title
        {
            get
            {
                return _Title;
            }
            set
            {
                _Title = value;
            }
        }
        
        public string Value
        {
            get
            {
                return _Value;
            }
            set
            {
                _Value = value;
            }
        }
        
        public string Contents
        {
            get
            {
                return _Content;
            }
            set
            {
                _Content = value;
            }
        }
        
        public string UserID
        {
            get
            {
                return RettetAf;
            }
            set
            {
                RettetAf = value;
            }
        }
        
        public string IP
        {
            get
            {
                return RettetIP;
            }
            set
            {
                RettetIP = value;
            }
        }
        
        public Nullable<DateTime> Dato
        {
            get
            {
                return _Dato;
            }
            set
            {
                _Dato = value.Value;
            }
        }
        
        public decimal Tal
        {
            get
            {
                return _Tal;
            }
            set
            {
                _Tal = value;
            }
        }
        
        public int FieldType
        {
            get
            {
                return _FieldType;
            }
            set
            {
                _FieldType = value;
            }
        }
        
#endregion
        
#region  Shared Populate
        
        private static void AddParms(ref DBAccess db, Combobox c)
        {
            var with_1 = c;
            db.AddNVarChar("Field", with_1.Field, 100);
            db.AddNVarChar("Title", with_1.Title, 250);
            db.AddNVarChar("Value", with_1.Value, 250);
            db.AddNVarChar("Contents", with_1.Contents, -1);
            db.AddBoolean("Hidden", with_1.Hidden);
            db.AddBoolean("Valid", with_1.Valid);
            db.AddInt("Idx", with_1.Idx);
            db.AddNVarChar("ApplicationName", Roles.ApplicationName, 256);
            
            //db.AddDateTime("Dato", .Dato)
            //db.AddDecimal("Tal", .Tal)
            //db.AddInt("FieldType", .FieldType)
            
            AddParmsStandard(db, c);
        }
        
        protected static void Populate(SqlDataReader dr, Combobox c)
        {
            PopulateStandard(dr, c);
            var with_1 = c;
            
            //SELECT ID, Field, Idx, Sort, ApplicationName, Title, Value, Contents, Valid, Hidden, RettetAf, RettetDen, RettetIP, Tal, Dato, FieldType
            //FROM Co2Db_Combobox
            
            with_1.Field = dr.DBtoString("Field");
            with_1.Idx = System.Convert.ToInt32(dr.DBtoInt("Idx"));
            //.sort = dr.DBtoInt("sort")
            //.ApplicationName = dr.DBtoString("ApplicationName")
            with_1.Title = dr.DBtoString("Title");
            with_1.Value = dr.DBtoString("Value");
            with_1.Contents = dr.DBtoString("Contents");
            
            with_1.Valid = bool.Parse(dr.DBtoString("Valid"));
            with_1.Hidden = System.Convert.ToBoolean(dr.DBtoBool("Hidden"));
            
            with_1.Dato = dr.DBtoDate("Dato");
            with_1.Tal = System.Convert.ToDecimal(dr.DBtoDecimal("Tal"));
            with_1.FieldType = System.Convert.ToInt32(dr.DBtoInt("FieldType"));
            
        }
        
#endregion
        
#region  Stored Procedure Names
        private const string _SQLDelete = "Co2Db_Combobox_Delete";
        private const string _SQLInsert = "Co2Db_Combobox_Insert";
        private const string _SQLUpdate = "Co2Db_Combobox_Update";
        private const string _SQLMoveUp = "Co2Db_Combobox_MoveUp";
        private const string _SQLMoveDown = "Co2Db_Combobox_MoveDown";
        
        private const string _SQLSelectListAll = "Co2Db_Combobox_ListAll";
        
        private const string _SQLSelectAll = "Co2Db_Combobox_SelectAll";
        private const string _SQLSelectAllValue = "Co2Db_Combobox_SelectAll_AddValue";
        
        private const string _SQLSelectID = "Co2Db_Combobox_SelectByID";
        private const string _SQLSelectOne = "Co2Db_Combobox_SelectOne";
        
        private const string _SQLSelectGetTitle = "Co2Db_Combobox_GetTitle";
        private const string _SQLSelectGetValue = "Co2Db_Combobox_GetValue";
        
        private const string _SQLSelectGetTitleByValue = "Co2Db_Combobox_GetTitleByValue";
        private const string _SQLSelectGetValueByTitle = "Co2Db_Combobox_GetValueByTitle";
        
        private const string _SQLSelectGetIdxByTitle = "Co2Db_Combobox_GetIdxByTitle";
        
        private const string _SQLSelectGetContent = "Co2Db_Combobox_GetContent";
#endregion
        
#region  Public Shared Data Metodes
        
        
        public int Delete()
        {
            return Delete(System.Convert.ToInt32(this));
        }
        public static int Delete(int ID)
        {
            DBAccess db = new DBAccess();
            db.AddInt("ID", ID);
            return db.ExecuteNonQuery(_SQLDelete);
        }
        public static int Delete(Combobox c)
        {
            return Delete(c.ID);
        }
        
        public int Insert()
        {
            return Insert(this);
        }
        public static int Insert(Combobox c)
        {
            DBAccess db = new DBAccess();
            
            if (SQLfunctions.SQLstr(c.Field).Trim() == "")
            {
                return System.Convert.ToInt32(false);
            }
            
            AddParms(ref db, c);
            SqlParameter pID = new SqlParameter("@ID", 0);
            pID.Direction = ParameterDirection.Output;
            db.AddParameter(pID);
            
            //Return db.ExecuteNonQuery(_SQLInsert)
            int retval = db.ExecuteNonQuery(_SQLInsert);
            if (retval == 1)
            {
                c.ID = int.Parse(pID.Value.ToString());
                return c.ID; //Integer.Parse(pID.Value.ToString)
            }
            else
            {
                return -1;
            }
        }
        public static int Insert(string Field, string Title, string Value, string Contents, bool Hidden)
        {
            Combobox cb = new Combobox();
            cb.Field = Field;
            cb.Title = Title;
            cb.Value = Value;
            cb.Contents = Contents;
            cb.Hidden = Hidden;
            return Insert(cb);
        }
        
        public int Update()
        {
            return Update(this);
        }
        public static int Update(Combobox c)
        {
            DBAccess db = new DBAccess();
            
            db.AddInt("ID", c.ID);
            AddParms(ref db, c);
            
            //db.AddParameter("@Field", SQLstr(c.Field))
            //db.AddParameter("@Title", SQLstr(c.Title))
            //db.AddParameter("@Value", SQLstr(c.Value))
            //db.AddParameter("@Content", SQLstr(c.Contents))
            //db.AddParameter("@Hidden", ToBool(c.Hidden))
            //db.AddParameter("@Index", ToInt(c.Idx))
            //db.AddParameter("@Valid", ToBool(c.Valid))
            
            return db.ExecuteNonQuery(_SQLUpdate);
        }
        public static int Update(int ID, string Field, string Title, string Value, string Contents, bool Hidden, int Idx, bool Valid)
        {
            Combobox cb = new Combobox(ID);
            //.ID = ID
            cb.Field = Field;
            cb.Title = Title;
            cb.Value = Value;
            cb.Contents = Contents;
            cb.Hidden = Hidden;
            cb.Idx = Idx;
            cb.Valid = Valid;
            return Update(cb);
        }
        
        public int Save()
        {
            return Save(this);
        }
        public static int Save(Combobox rec)
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
        
        public int MoveUp()
        {
            return MoveUp(System.Convert.ToInt32(this));
        }
        public static int MoveUp(int ID)
        {
            //EXEC	@return_value = [vicjos1_sysadm].[Co2Db_Combobox_MoveUp]@ID = 35
            DBAccess db = new DBAccess();
            db.AddInt("ID", ID);
            return db.ExecuteNonQuery(_SQLMoveUp);
        }
        public static int MoveUp(Combobox c)
        {
            return MoveUp(c.ID);
        }
        
        public int MoveDown()
        {
            return MoveDown(System.Convert.ToInt32(this));
        }
        public static int MoveDown(int ID)
        {
            //EXEC	@return_value = [vicjos1_sysadm].[Co2Db_Combobox_MoveDown]@ID = 35
            DBAccess db = new DBAccess();
            db.AddInt("ID", ID);
            return db.ExecuteNonQuery(_SQLMoveDown);
        }
        public static int MoveDown(Combobox c)
        {
            return MoveDown(c.ID);
        }
        
#endregion
        
#region  Public Shared Combobox Metodes
        //[Co2Db_Combobox_ListAll]
        //SELECT cbField, cbHidden, COUNT(*) AS cbCount
        public static DataSet ListCombobox()
        {
            DBAccess db = new DBAccess();
            return db.ExecuteDataSet(_SQLSelectListAll);
        }
        
        //		'CREATE PROCEDURE [Co2Db_Dokumenter_SelectAll]
        //	[Aktiv], [Name], [Mime], [Size], [Width], [Height], [Dato], [UserID], [IP]
        public static DataSet SelectCombobox(string Field)
        {
            return SelectComboboxValue(Field, false);
        }
        public static DataSet SelectComboboxValue(string Field, bool ShowValue = false)
        {
            DBAccess db = new DBAccess();
            string stp = "";
            db.AddParameter("@Field", Field);
            if (ShowValue)
            {
                stp = _SQLSelectAllValue; //"Co2Db_Combobox_SelectAll_AddValue"
            }
            else
            {
                stp = _SQLSelectAll; //"Co2Db_Combobox_SelectAll"
            }
            return db.ExecuteDataSet(stp);
        }
        
        public static DataSet SelectComboboxByID(int ID)
        {
            DBAccess db = new DBAccess();
            db.AddInt("ID", ID);
            return db.ExecuteDataSet(_SQLSelectID);
        }
        public static Combobox Select(int ID)
        {
            return GetCombobox(ID);
        }
        public static Combobox GetCombobox(int ID)
        {
            DBAccess db = new DBAccess();
            db.AddInt("ID", ID);
            SqlDataReader dr = (SqlDataReader) (db.ExecuteReader(_SQLSelectID));
            if (dr.HasRows)
            {
                Combobox c = new Combobox();
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
        
        public static Combobox GetCombobox(string Field, int idx)
        {
            int ID = -1;
            DBAccess db = new DBAccess();
            db.AddParameter("@Field", Field);
            db.AddParameter("@Idx", idx);
            string res = Funktioner.ToDbString(db.ExecuteScalar(_SQLSelectOne)); //"Co2Db_Combobox_SelectOne"
            if (Information.IsNumeric(res))
            {
                ID = Funktioner.ToInt(res);
            }
            return GetCombobox(ID);
        }
        
        public static string GetTitle(string Field, int idx)
        {
            DBAccess db = new DBAccess();
            db.AddParameter("@Field", Field);
            db.AddParameter("@Idx", idx);
            string res = Funktioner.ToDbString(db.ExecuteScalar(_SQLSelectGetTitle)); //"Co2Db_Combobox_GetTitle"
            if (res.Trim().Length < 1)
            {
                res = "&nbsp;";
            }
            return res;
        }
        
        public static string GetValue(string Field, int idx)
        {
            DBAccess db = new DBAccess();
            db.AddParameter("@Field", Field);
            db.AddParameter("@Idx", idx);
            string res = Funktioner.ToDbString(db.ExecuteScalar(_SQLSelectGetValue)); //"Co2Db_Combobox_GetValue"
            if (res.Trim().Length < 1)
            {
                res = "&nbsp;";
            }
            return res;
        }
        
        public static string GetTitleByValue(string Field, string Value)
        {
            DBAccess db = new DBAccess();
            db.AddParameter("@Field", Field);
            db.AddParameter("@Value", Value);
            string res = Funktioner.ToDbString(db.ExecuteScalar(_SQLSelectGetTitleByValue)); //"Co2Db_Combobox_GetTitleByValue"
            if (res.Trim().Length < 1)
            {
                res = "&nbsp;";
            }
            return res;
        }
        
        public static string GetValueByTitle(string Field, string Title)
        {
            DBAccess db = new DBAccess();
            db.AddParameter("@Field", Field);
            db.AddParameter("@Title", Title);
            string res = Funktioner.ToDbString(db.ExecuteScalar(_SQLSelectGetValueByTitle)); //"Co2Db_Combobox_GetValueByTitle"
            if (res.Trim().Length < 1)
            {
                res = "&nbsp;";
            }
            return res;
        }
        
        public static int GetIdxByTitle(string Field, string Title)
        {
            DBAccess db = new DBAccess();
            db.AddParameter("@Field", Field);
            db.AddParameter("@Title", Title);
            int res = Funktioner.ToInt(db.ExecuteScalar(_SQLSelectGetIdxByTitle)); //"Co2Db_Combobox_GetIdxByTitle"
            return res;
        }
        
        public static int GetIndexByTitle(string Field, string Title)
        {
            DBAccess db = new DBAccess();
            db.AddParameter("@Field", Field);
            db.AddParameter("@Title", Title);
            int res = Funktioner.ToInt(db.ExecuteScalar(_SQLSelectGetIdxByTitle)); //"Co2Db_Combobox_GetIdxByTitle"
            return res;
        }
        
        public static string GetContent(string Field, int idx)
        {
            DBAccess db = new DBAccess();
            db.AddParameter("@Field", Field);
            db.AddParameter("@Idx", idx);
            string res = Funktioner.ToDbString(db.ExecuteScalar(_SQLSelectGetContent)); //"Co2Db_Combobox_GetContent"
            if (res.Trim().Length < 1)
            {
                res = "&nbsp;";
            }
            return res;
        }
        
#endregion
        
    }
    
    
}
