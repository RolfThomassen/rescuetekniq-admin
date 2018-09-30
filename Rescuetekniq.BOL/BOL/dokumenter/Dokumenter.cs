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
using System.IO;


namespace RescueTekniq.BOL
{
    
    //ID [int],
    //Aktiv [bit],
    //Dato [datetime],
    //ExpireDate [datetime],
    //Name [nvarchar](250),
    //Mime [nvarchar](100),
    //Beskrivelse [nvarchar](MAX),
    //Type [int],
    //Category [int],
    //Owner [int],
    //MedArbGrpID [int],
    //Filename [nvarchar](250),
    //Ext [nvarchar](50),
    //Size [int],
    //Width [int],
    //Height [int],
    //UserID [nvarchar](50),
    //IP [nvarchar](15),
    //RettetAF [nvarchar](50),
    //RettetDEN [datetime],
    //RettetIP [nvarchar](15)
    
    public class Dokumenter : BaseObject
    {
        
#region  New
        
        public Dokumenter()
        {
            Dato = DateTime.Now;
            Aktiv = true;
            UserID = ""; // 'Profile.Virksomhed.CompanyID = -1
        }
        
        public Dokumenter(int ID)
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
        
        private bool _Aktiv = true;
        private bool _Hidden = false;
        private DateTime _Dato = DateTime.Now;
        private string _ExpireDate;
        private string _Name = ""; // nvarchar(250),
        private string _Filename = ""; // nvarchar(256),
        private string _Ext = "";
        private string _Beskrivelse = ""; // nvarchar(max),
        private string _Mime = ""; // varchar(100),
        private int _Size = -1;
        private int _Width = -1;
        private int _Height = -1;
        private string _UserID; // nvarchar(50),
        private string _Clob; // nvarchar(max),
        private object _Blob; // image,
        private int _Type = -1;
        private int _Category = -1;
        private int _Owner = -1;
        private int _MedArbGrpID = -1;
        private string _IP; // varchar(15),
        
#endregion
        
#region  Public Properties
        
        public bool Aktiv
        {
            get
            {
                return _Aktiv;
            }
            set
            {
                _Aktiv = value;
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
        
        public DateTime ExpireDate
        {
            get
            {
                return DateTime.Parse( _ExpireDate);
            }
            set
            {
                _ExpireDate = value.ToString();
            }
        }
        
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
            }
        }
        
        public string Filename
        {
            get
            {
                return _Filename;
            }
            set
            {
                _Filename = value;
            }
        }
        public string Ext
        {
            get
            {
                return _Ext;
            }
            set
            {
                _Ext = value;
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
        
        public string Mime
        {
            get
            {
                return _Mime;
            }
            set
            {
                _Mime = value;
            }
        }
        
        public int Size
        {
            get
            {
                return _Size;
            }
            set
            {
                _Size = value;
            }
        }
        
        public int Width
        {
            get
            {
                return _Width;
            }
            set
            {
                _Width = value;
            }
        }
        
        public int Height
        {
            get
            {
                return _Height;
            }
            set
            {
                _Height = value;
            }
        }
        
        public string UserID
        {
            get
            {
                return _UserID;
            }
            set
            {
                _UserID = value;
            }
        }
        
        public string Clob
        {
            get
            {
                return _Clob;
            }
            set
            {
                _Clob = value;
            }
        }
        
        public object Blob
        {
            get
            {
                return _Blob;
            }
            set
            {
                _Blob = value;
            }
        }
        
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
        public string TypeText
        {
            get
            {
                return Combobox.GetTitle("Dokument.Type", Type);
            }
        }
        
        public int Category
        {
            get
            {
                return _Category;
            }
            set
            {
                _Category = value;
            }
        }
        public string CategoryText
        {
            get
            {
                return Combobox.GetTitle("Dokument.Category", Category);
            }
        }
        
        public int Owner
        {
            get
            {
                return _Owner;
            }
            set
            {
                _Owner = value;
            }
        }
        public string OwnerText
        {
            get
            {
                string res = "";
                if (Owner < 1)
                {
                    res = "Alle";
                }
                else
                {
                    res = Virksomhed.GetCompanyName(Owner); //Combobox.GetTitle("Dokument.Category", Category)
                }
                return res;
            }
        }
        
        public int MedArbGrpID
        {
            get
            {
                return _MedArbGrpID;
            }
            set
            {
                _MedArbGrpID = value;
            }
        }
        public string MedArbGrpText
        {
            get
            {
                string res = "";
                if (MedArbGrpID < 1)
                {
                    res = "Alle";
                }
                else
                {
                    res = MedarbejderGruppe.GetMedarbejderGruppeName(MedArbGrpID);
                }
                return res;
            }
        }
        
        public string IP
        {
            get
            {
                return _IP;
            }
            set
            {
                _IP = value;
            }
        }
        
#endregion
        
#region  Shared Populate
        
        private static void AddParms(ref DBAccess db, Dokumenter d)
        {
            var with_1 = d;
            db.AddBit("Aktiv", with_1.Aktiv);
            db.AddDateTime("Dato", with_1.Dato);
            db.AddDateTime("ExpireDate", with_1.ExpireDate);
            db.AddNVarChar("Name", with_1.Name, 250);
            db.AddNVarChar("Mime", with_1.Mime, 100);
            db.AddNVarChar("Beskrivelse", with_1.Beskrivelse, -1);
            db.AddInt("Type", with_1.Type);
            db.AddInt("Category", with_1.Category);
            db.AddInt("Owner", with_1.Owner);
            db.AddInt("MedArbGrpID", with_1.MedArbGrpID);
            db.AddNVarChar("Filename", with_1.Filename, 250);
            db.AddNVarChar("Ext", with_1.Ext, 50);
            db.AddInt("Size", with_1.Size);
            db.AddInt("Width", with_1.Width);
            db.AddInt("Height", with_1.Height);
            db.AddNVarChar("UserID", with_1.UserID, 50);
            
            db.AddBlob("Blob", d.Blob);
            db.AddNVarChar("Clob", with_1.Clob, -1);
            AddParmsStandard(db, d);
        }
        
        private static void Populate(SqlDataReader dr, Dokumenter d)
        {
            PopulateStandard(dr, d);
            var with_1 = d;
            with_1.Aktiv = System.Convert.ToBoolean(dr.DBtoBool("Aktiv"));
            with_1.Dato = System.Convert.ToDateTime(dr.DBtoDate("Dato"));
            with_1.ExpireDate = System.Convert.ToDateTime(dr.DBtoDate("ExpireDate"));
            with_1.Name = dr.DBtoString("Name");
            with_1.Mime = dr.DBtoString("Mime");
            with_1.Beskrivelse = dr.DBtoString("Beskrivelse");
            with_1.Type = System.Convert.ToInt32(dr.DBtoInt("Type"));
            with_1.Category = System.Convert.ToInt32(dr.DBtoInt("Category"));
            with_1.Owner = System.Convert.ToInt32(dr.DBtoInt("Owner"));
            with_1.MedArbGrpID = System.Convert.ToInt32(dr.DBtoInt("MedArbGrpID"));
            with_1.Filename = dr.DBtoString("Filename");
            with_1.Ext = dr.DBtoString("Ext");
            with_1.Size = System.Convert.ToInt32(dr.DBtoInt("Size"));
            with_1.Width = System.Convert.ToInt32(dr.DBtoInt("Width"));
            with_1.Height = System.Convert.ToInt32(dr.DBtoInt("Height"));
            with_1.UserID = dr.DBtoString("UserID");
            
            //.Blob = dbto
            with_1.Clob = dr.DBtoString("Clob");
        }
        
        //Private Shared Sub PopulateDokument(ByRef dr As SqlDataReader, ByRef c As Dokumenter)
        //    'ID,
        //    'Aktiv, Dato, ExpireDate, Name, Mime, Beskrivelse, Type, Category, Owner, MedArbGrpID,
        //    'Filename, Ext, Size, Width, Height, UserID, IP,
        //    'RettetAF, RettetDen, RettetIP
        //    With c
        //        .ID = dr.DBtoInt("id")
        
        //        .Aktiv = DBtoBool(dr, "Aktiv")
        //        .Hidden = DBtoBool(dr, "Hidden")
        //        .Dato = dr.DBtoDate("Dato")
        //        .ExpireDate = dr.DBtoDate("ExpireDate")
        //        .Name = dr.DBtoString("Name")
        //        .Mime = dr.DBtoString("Mime")
        //        .Beskrivelse = dr.DBtoString("Beskrivelse")
        //        .Type = dr.DBtoInt("Type")
        //        .Category = dr.DBtoInt("Category")
        //        .Owner = dr.DBtoInt("Owner")
        //        .MedArbGrpID = dr.DBtoInt("MedArbGrpID")
        //        .Filename = dr.DBtoString("Filename")
        //        .Ext = dr.DBtoString("Ext")
        //        .Size = dr.DBtoInt("Size")
        //        .Width = dr.DBtoInt("Width")
        //        .Height = dr.DBtoInt("Height")
        //        .UserID = dr.DBtoString("UserID")
        //        .IP = dr.DBtoString("IP")
        
        //        .RettetAf = dr.DBtoString("RettetAf")
        //        .RettetIP = dr.DBtoString("RettetIP")
        //        .RettetDen = dr.DBtoDate("RettetDen")
        //    End With
        //End Sub
        
        
#endregion
        
#region  Stored Procedure Names
        
        private const string _SQLDelete = "Co2Db_Dokumenter_Delete";
        private const string _SQLInsert = "Co2Db_Dokumenter_Insert";
        private const string _SQLPurge = "Co2Db_Dokumenter_Purge";
        private const string _SQLUpdate = "Co2Db_Dokumenter_Update";
        private const string _SQLUpdateBlob = "Co2Db_Dokumenter_UpdateBlob";
        private const string _SQLUpdateClob = "Co2Db_Dokumenter_UpdateClob";
        
        private const string _SQLSelectAll = "Co2Db_Dokumenter_SelectAll";
        private const string _SQLSelectID = "Co2Db_Dokumenter_SelectID";
        private const string _SQLSelectByCode = "Co2Db_Dokumenter_SelectByCode";
        private const string _SQLSelectBySearch = "Co2Db_Dokumenter_SelectBySearch";
        
        private const string _SQLSelectOne = "Co2Db_Dokumenter_SelectOne";
        private const string _SQLSelectOneBlob = "Co2Db_Dokumenter_SelectOneBlob";
        private const string _SQLSelectOneClob = "Co2Db_Dokumenter_SelectOneClob";
        
        //Co2Db_Dokumenter_Forsikring_SelectBySearch
        //Co2Db_Dokumenter_GetDokumentName
        //
        //Co2Db_Dokumenter_SelectByTypeCategory
        
#endregion
        
#region  Manipulate data
        
        public int Insert()
        {
            return Insert(this);
        }
        public static int Insert(Dokumenter d)
        {
            
            DBAccess db = new DBAccess();
            
            AddParms(ref db, d);
            
            SqlParameter p = new SqlParameter("@ID", 0);
            p.Direction = ParameterDirection.Output;
            db.AddParameter(p);
            
            int retval = db.ExecuteNonQuery(_SQLInsert); //"Co2Db_Dokumenter_Insert")
            if (retval == 1)
            {
                int ID = int.Parse(p.Value.ToString());
                return ID; //Integer.Parse(ID.Value.ToString)
            }
            else
            {
                return -1;
            }
        }
        public static int Insert(string Name, string Beskrivelse, string Filename, string Ext, string Mime, int Type, int Category, int Owner, int MedArbGrpID, int Size, int Width, int Height, DateTime Dato, DateTime ExpireDate, string UserId, string IP, bool Hidden, string Clob, object Blob, string RettetAF, string RettetIP)
        {
            Dokumenter d = new Dokumenter();
            d.Aktiv = true;
            d.Hidden = Hidden;
            d.Dato = Dato;
            d.ExpireDate = ExpireDate;
            d.Name = Name;
            d.Mime = Mime;
            d.Beskrivelse = Beskrivelse;
            d.Type = Type;
            d.Category = Category;
            d.Owner = Owner;
            d.MedArbGrpID = MedArbGrpID;
            d.Filename = Filename;
            d.Ext = Ext;
            d.Size = Size;
            d.Width = Width;
            d.Height = Height;
            d.UserID = UserId;
            d.IP = IP;
            d.Clob = Clob;
            d.Blob = Blob;
            return Insert(d);
        }
        
        public int Update()
        {
            return Update(this);
        }
        public static int Update(Dokumenter d)
        {
            DBAccess db = new DBAccess();
            
            db.AddInt("ID", d.ID);
            AddParms(ref db, d);
            
            int retval = db.ExecuteNonQuery(_SQLUpdate);
            return retval;
        }
        public static int Update(int ID, bool Aktiv, string Name, string Beskrivelse, string Filename, string Ext, string Mime, int Type, int Category, int Owner, int MedArbGrpID, int Size, int Width, int Height, DateTime Dato, DateTime ExpireDate, string UserId, string IP, bool Hidden, string Clob, string RettetAF, string RettetIP)
        {
            Dokumenter d = new Dokumenter(ID);
            d.Aktiv = Aktiv;
            d.Hidden = Hidden;
            d.Dato = Dato;
            d.ExpireDate = ExpireDate;
            d.Name = Name;
            d.Mime = Mime;
            d.Beskrivelse = Beskrivelse;
            d.Type = Type;
            d.Category = Category;
            d.Owner = Owner;
            d.MedArbGrpID = MedArbGrpID;
            d.Filename = Filename;
            d.Ext = Ext;
            d.Size = Size;
            d.Width = Width;
            d.Height = Height;
            d.UserID = UserId;
            d.IP = IP;
            d.Clob = Clob;
            //.Blob = Blob
            return Update(d); //db.ExecuteNonQuery("Co2Db_Dokumenter_Update")
        }
        
        public static int UpdateBlob(Dokumenter d)
        {
            DBAccess db = new DBAccess();
            db.AddInt("ID", d.ID);
            db.AddBlob("Blob", d.Blob);
            return db.ExecuteNonQuery(_SQLUpdateBlob);
        }
        public static string UpdateClob(Dokumenter d)
        {
            DBAccess db = new DBAccess();
            db.AddInt("ID", d.ID);
            db.AddNVarChar("Clob", d.Clob, -1);
            return System.Convert.ToString( db.ExecuteNonQuery(_SQLUpdateClob));
        }
        
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
        public static int Delete(Dokumenter d)
        {
            return Delete(d.ID);
        }
        
#endregion
        
#region  Get Data
        
        public static DataSet SelectDokumenter()
        {
            DBAccess db = new DBAccess();
            return db.ExecuteDataSet(_SQLSelectAll); // '"Co2Db_Dokumenter_SelectAll")
        }
        
        public static DataSet GetDokumenter()
        {
            DBAccess db = new DBAccess();
            return db.ExecuteDataSet(_SQLSelectAll); //  "Co2Db_Dokumenter_SelectAll")
        }
        
        public static object GetDokumenterBlob(int ID)
        {
            DBAccess db = new DBAccess();
            db.AddInt("ID", ID);
            return db.ExecuteScalar(_SQLSelectOneBlob); //"Co2Db_Dokumenter_SelectOneBlob")
        }
        
        public static Dokumenter GetDokument(int ID)
        {
            DBAccess db = new DBAccess();
            Dokumenter d = new Dokumenter();
            db.AddInt("ID", ID);
            
            SqlDataReader dr = (SqlDataReader) (db.ExecuteReader(_SQLSelectOne)); //"Co2Db_Dokumenter_SelectOne")
            if (dr.HasRows)
            {
                //	While dr.Read
                dr.Read();
                Populate(dr, d);
                //	End While
            }
            dr.Close();
            return d;
        }
        
        public static string GetCountryName(int ID)
        {
            DBAccess db = new DBAccess();
            db.AddParameter("@iCountryID", ID); //
            return db.ExecuteScalar("Co2Db_Dokumenter_GetCountryName").ToString();
        }
        
        public static DataSet GetCompanyDocuments(int CompanyID)
        {
            return SearchDokumenter("", 1, -1, CompanyID, -1);
        }
        
        public static DataSet GetDocumentType(int Type)
        {
            return SearchDokumenter("", Type, -1, -1, -1);
        }
        
        public static DataSet SearchDokumenter(string navn, int Type, int Category = -1, int Owner = -1, int MedArbGrpID = -1)
        {
            string[] arr = navn.Split(' ');
            DataSet ds = new DataSet();
            DataSet dsTemp = new DataSet();
            bool flag = false;
            DBAccess db = new DBAccess();
            foreach (string s in arr)
            {
                db.AddParameter("@Type", Type);
                db.AddParameter("@Category", Category);
                db.AddParameter("@Owner", Owner);
                db.AddParameter("@MedArbGrpID", MedArbGrpID);
                db.AddParameter("@Search", s);
                
                dsTemp = db.ExecuteDataSet("Co2Db_Dokumenter_SelectBySearch");
                db.Parameters.Clear();
                ds.Merge(dsTemp);
                if (flag == false)
                {
                    DataColumn[] pk = new DataColumn[2];
                    pk[0] = ds.Tables[0].Columns["ID"];
                    ds.Tables[0].PrimaryKey = pk;
                    flag = true;
                }
            }
            return ds;
            //	EXEC @return_value = [vicjos1_sysadm].[Co2Db_Virksomheder_SelectBySearch] @sSearch = N'5'
        }
        
        public static DataSet Search_Dokumenter_Forsikringer(string navn, int Type, int Category = -1, int Owner = -1, int MedArbGrpID = -1)
        {
            string[] arr = navn.Split(' ');
            DataSet ds = new DataSet();
            DataSet dsTemp = new DataSet();
            bool flag = false;
            DBAccess db = new DBAccess();
            foreach (string s in arr)
            {
                db.AddParameter("@Type", Type);
                db.AddParameter("@Category", Category);
                db.AddParameter("@Owner", Owner);
                db.AddParameter("@MedArbGrpID", MedArbGrpID);
                db.AddParameter("@Search", s);
                //vw_Co2Db_Dokumenter_Forsikringer
                dsTemp = db.ExecuteDataSet("Co2Db_Dokumenter_Forsikring_SelectBySearch");
                db.Parameters.Clear();
                ds.Merge(dsTemp);
                if (flag == false)
                {
                    DataColumn[] pk = new DataColumn[2];
                    pk[0] = ds.Tables[0].Columns["ID"];
                    ds.Tables[0].PrimaryKey = pk;
                    flag = true;
                }
            }
            return ds;
        }
        
#endregion
        
#region  Insert from disk
        
        public static int InsertFromDisk(int OwnerID, int TypeID, int CategoryID, string Beskrivelse, string DokumentNavn, string Filename, bool Hidden, bool DeleteFile = false)
        {
            
            if (!File.Exists(Filename))
            {
                throw (new FileNotFoundException("Cannot create attachment because the file was not found", Filename));
            }
            
            Dokumenter Dok = new Dokumenter();
            Dok.Owner = OwnerID;
            Dok.Type = TypeID;
            Dok.Category = CategoryID;
            Dok.Beskrivelse = Beskrivelse;
            Dok.Name = DokumentNavn;
            
            Dok.Aktiv = true;
            Dok.Hidden = Hidden;
            Dok.Dato = DateTime.Today;
            Dok.ExpireDate = DateTime.Today.AddYears(99);
            
            Dok.Mime = FileIO.getMimeFromFile(Filename); // System.Net.Mime.MediaTypeNames.Application.Pdf ' Text.Plain
            
            // Load file from harddrive and load into database
            FileInfo fi = new FileInfo(Filename);
            Dok.Filename = fi.Name;
            Dok.Ext = fi.Extension.Replace(".", "");
            Dok.Size = (int) fi.Length;
            fi = null;
            
            using (FileStream reader = new FileStream(Filename, FileMode.Open))
            {
                byte[] blob = new byte[System.Convert.ToInt32(reader.Length - 1)+ 1];
                reader.Read(blob, 0, (int) reader.Length);
                //reader.Close()
                Dok.Blob = blob;
            }
            
            
            if (DeleteFile == true)
            {
                try
                {
                    File.Delete(Filename);
                }
                catch (Exception)
                {
                }
            }
            
            return Dokumenter.Insert(Dok);
        }
        
#endregion
        
#region  Metodes -
#endregion
        
    }
    
}
