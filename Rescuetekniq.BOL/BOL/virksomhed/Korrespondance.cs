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
    //Namespace RescueTekniq.BOL
    
    //Co2Db_Korrespondance
    //ID				int
    //Type			int
    //CompanyID		int
    //MedarbejderID	int
    //Status			int
    //Overskrift		nvarchar(100)
    //Deleted		bit
    //Mime			nvarchar(250)
    //Filename		nvarchar(250)
    //Ext			nvarchar(20)
    //Size			int
    //Blob			image
    //RettetAf		nvarchar(50)
    //RettetDen		datetime
    //RettetIP		nvarchar(15)
    
    //FROM	vw_Co2Db_Korrespondance
    
    public class Korrespondance : BaseObject
    {
        
#region  New
        
        public Korrespondance()
        {
            ID = -1;
        }
        
        public Korrespondance(int 
	Type, int CompanyID, int MedarbejderID, int Status, string Overskrift, bool Ekstern, string Mime, string Filename, string Ext, int Size, object Blob) : this()
        {
            this.Type = Type;
            this.CompanyID = CompanyID;
            this.MedarbejderID = MedarbejderID;
            this.Status = Status;
            this.Overskrift = Overskrift;
            this.Ekstern = Ekstern;
            this.Mime = Mime;
            this.Filename = Filename;
            this.Ext = Ext;
            this.Size = Size;
            this.Blob = Blob;
        }
        
#endregion
        
#region  Private And Publics
        
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
        
        private bool _Ekstern = false;
        public bool Ekstern
        {
            get
            {
                return _Ekstern;
            }
            set
            {
                _Ekstern = value;
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
        
        private string _Name = ""; // nvarchar(250),
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
        
        private string _Filename = ""; // nvarchar(256),
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
        private string _Ext = "";
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
        
        private string _Mime = ""; // varchar(100),
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
        
        private int _Size = -1;
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
        
        private object _Blob; // image,
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
        
#endregion
        
#region  Populate Date
        
        private static void AddParms(ref DBAccess db, Korrespondance k)
        {
            var with_1 = k;
            db.Parameters.Add(new SqlParameter("@Type", Funktioner.ToInt(k.Type)));
            db.Parameters.Add(new SqlParameter("@CompanyID", Funktioner.ToInt(k.CompanyID)));
            db.Parameters.Add(new SqlParameter("@MedarbejderID", Funktioner.ToInt(k.MedarbejderID)));
            db.Parameters.Add(new SqlParameter("@Status", Funktioner.ToInt(k.Status)));
            
            db.Parameters.Add(new SqlParameter("@Overskrift", SQLfunctions.SQLstr(k.Overskrift)));
            db.Parameters.Add(new SqlParameter("@Ekstern", Funktioner.ToBool(k.Ekstern)));
            
            db.Parameters.Add(new SqlParameter("@Mime", SQLfunctions.SQLstr(k.Mime)));
            db.Parameters.Add(new SqlParameter("@Filename", SQLfunctions.SQLstr(k.Filename)));
            db.Parameters.Add(new SqlParameter("@Ext", SQLfunctions.SQLstr(k.Ext)));
            db.Parameters.Add(new SqlParameter("@Size", Funktioner.ToInt(k.Size)));
            
            SqlParameter pictureParameter = new SqlParameter("@Blob", SqlDbType.Image);
            pictureParameter.Value = k.Blob;
            db.Parameters.Add(pictureParameter);
            //	db.AddParameter("@Blob", Blob)
            
            AddParmsStandard(db, k);
        }
        
        //Private Shared Sub PopulateKorrespondance(ByRef dr As SqlDataReader, ByRef n As Korrespondance)
        private static void Populate(SqlDataReader dr, Korrespondance n)
        {
            PopulateStandard(dr, n);
            var with_1 = n;
            with_1.Type = System.Convert.ToInt32(dr.DBtoInt("Type"));
            with_1.CompanyID = System.Convert.ToInt32(dr.DBtoInt("CompanyID"));
            with_1.MedarbejderID = System.Convert.ToInt32(dr.DBtoInt("MedarbejderID"));
            with_1.Status = System.Convert.ToInt32(dr.DBtoInt("Status"));
            with_1.Overskrift = dr.DBtoString("Overskrift");
            with_1.Ekstern = System.Convert.ToBoolean(dr.DBtoBool("Ekstern"));
            with_1.Mime = dr.DBtoString("Mime");
            with_1.Filename = dr.DBtoString("Filename");
            with_1.Ext = dr.DBtoString("Ext");
            with_1.Size = System.Convert.ToInt32(dr.DBtoString("Size"));
            //.Blob = dr.DBtoString("Blob")
            
            
        }
        
#endregion
        
#region  Metodes
        
#region  Manipulate data
        
        public int Delete()
        {
            return Delete(System.Convert.ToInt32(this));
        }
        public static int Delete(int ID)
        {
            DBAccess db = new DBAccess();
            db.Parameters.Add(new SqlParameter("@ID", ID));
            int retval = db.ExecuteNonQuery("Co2Db_Korrespondance_Delete");
            AddLog(Status: "Korrespondance", Logtext: string.Format("Delete Korrespondance: ID:{0}", ID), Metode: "Delete");
            return retval;
        }
        public static int Delete(Korrespondance n)
        {
            return Delete(n.ID);
        }
        
        public int Insert()
        {
            return Insert(this);
        }
        public static int Insert(Korrespondance k)
        {
            DBAccess db = new DBAccess();
            SqlParameter ID = new SqlParameter("@ID", 0);
            ID.Direction = ParameterDirection.Output;
            
            db.Parameters.Add(new SqlParameter("@Type", Funktioner.ToInt(k.Type)));
            db.Parameters.Add(new SqlParameter("@CompanyID", Funktioner.ToInt(k.CompanyID)));
            db.Parameters.Add(new SqlParameter("@MedarbejderID", Funktioner.ToInt(k.MedarbejderID)));
            db.Parameters.Add(new SqlParameter("@Status", Funktioner.ToInt(k.Status)));
            
            db.Parameters.Add(new SqlParameter("@Overskrift", SQLfunctions.SQLstr(k.Overskrift)));
            db.Parameters.Add(new SqlParameter("@Ekstern", Funktioner.ToBool(k.Ekstern)));
            
            db.Parameters.Add(new SqlParameter("@Mime", SQLfunctions.SQLstr(k.Mime)));
            db.Parameters.Add(new SqlParameter("@Filename", SQLfunctions.SQLstr(k.Filename)));
            db.Parameters.Add(new SqlParameter("@Ext", SQLfunctions.SQLstr(k.Ext)));
            db.Parameters.Add(new SqlParameter("@Size", Funktioner.ToInt(k.Size)));
            SqlParameter pictureParameter = new SqlParameter("@Blob", SqlDbType.Image);
            pictureParameter.Value = k.Blob;
            db.Parameters.Add(pictureParameter);
            //	db.AddParameter("@Blob", Blob)
            
            db.Parameters.Add(new SqlParameter("@RettetAf", SQLfunctions.SQLstr(CurrentUserName)));
            db.Parameters.Add(new SqlParameter("@RettetIP", SQLfunctions.SQLstr(CurrentUserIP)));
            db.Parameters.Add(ID);
            int retval = db.ExecuteNonQuery("Co2Db_Korrespondance_Insert");
            if (retval == 1)
            {
                k.ID = int.Parse(ID.Value.ToString());
                AddLog(Status: "Korrespondance", Logtext: string.Format("Create Korrespondance: ID:{0}", k.ID), Metode: "Insert");
                return k.ID; //Integer.Parse(ID.Value.ToString)
            }
            else
            {
                AddLog(Status: "Korrespondance", Logtext: string.Format("Failure to Create Korrespondance:"), logtype: LogTypeEnum.Error, Metode: "Insert");
                return -1;
            }
            
        }
        public static int Insert(int 
	Type, int CompanyID, int MedarbejderID, int Status, string Overskrift, bool Ekstern, string Mime, string Filename, string Ext, int Size, object Blob)
        {
            Korrespondance n = new Korrespondance(Type, CompanyID, MedarbejderID, Status, Overskrift, Ekstern, Mime, Filename, Ext, Size, Blob);
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
        //FROM vicjos1_sysadm.Co2Db_Korrespondance AS note
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
        
        //Mime			nvarchar(250)
        //Filename		nvarchar(250)
        //Ext			nvarchar(20)
        //Size			int
        //Blob			image
        
        public int Update()
        {
            return Update(this);
        }
        public static int Update(Korrespondance n)
        {
            DBAccess db = new DBAccess();
            db.Parameters.Add(new SqlParameter("@ID", n.ID));
            
            db.Parameters.Add(new SqlParameter("@Type", Funktioner.ToInt(n.Type)));
            db.Parameters.Add(new SqlParameter("@CompanyID", Funktioner.ToInt(n.CompanyID)));
            db.Parameters.Add(new SqlParameter("@MedarbejderID", Funktioner.ToInt(n.MedarbejderID)));
            db.Parameters.Add(new SqlParameter("@Status", Funktioner.ToInt(n.Status)));
            
            db.Parameters.Add(new SqlParameter("@Overskrift", SQLfunctions.SQLstr(n.Overskrift)));
            db.Parameters.Add(new SqlParameter("@Extern", Funktioner.ToBool(n.Ekstern)));
            
            
            db.Parameters.Add(new SqlParameter("@Mime", SQLfunctions.SQLstr(n.Mime)));
            db.Parameters.Add(new SqlParameter("@Filename", SQLfunctions.SQLstr(n.Filename)));
            db.Parameters.Add(new SqlParameter("@Ext", SQLfunctions.SQLstr(n.Ext)));
            db.Parameters.Add(new SqlParameter("@Size", Funktioner.ToInt(n.Size)));
            //Dim pictureParameter As SqlParameter = New SqlParameter("@Blob", SqlDbType.Image)
            //pictureParameter.Value = n.Blob
            //db.Parameters.Add(pictureParameter)
            //	db.AddParameter("@Blob", Blob)
            
            db.Parameters.Add(new SqlParameter("@RettetAf", SQLfunctions.SQLstr(CurrentUserName)));
            db.Parameters.Add(new SqlParameter("@RettetIP", SQLfunctions.SQLstr(CurrentUserIP)));
            int retval = db.ExecuteNonQuery("Co2Db_Korrespondance_Update");
            AddLog(Status: "Noter", Logtext: string.Format("Update Noter: ID:{0}", n.ID), Metode: "Update");
            return retval;
        }
        public static int Update(int 
	Id, int Type, int CompanyID, int MedarbejderID, int Status, string Overskrift, bool Ekstern, string Mime, string Filename, string Ext, int Size)
        {
            Korrespondance n = Korrespondance.GetKorrespondance(Id);
            n.Type = Type;
            n.CompanyID = CompanyID;
            n.MedarbejderID = MedarbejderID;
            n.Status = Status;
            n.Overskrift = Overskrift;
            n.Ekstern = Ekstern;
            n.Mime = Mime;
            n.Filename = Filename;
            n.Ext = Ext;
            n.Size = Size;
            return Update(n);
        }
        
        public int Save()
        {
            return Save(this);
        }
        public static int Save(Korrespondance B)
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
        
#region  add Korrespondance from disk / text
        
        public static int InsertFromDisk(int CompanyID, int MedarbejderID, int TypeID, string Overskrift, string Filename, bool DeleteFile = false, bool Ekstern = false)
        {
            
            if (!File.Exists(Filename))
            {
                throw (new FileNotFoundException("Cannot create attachment because the file was not found : " + Filename, Filename));
            }
            
            Korrespondance Korr = new Korrespondance();
            Korr.CompanyID = CompanyID;
            Korr.MedarbejderID = MedarbejderID;
            Korr.Type = TypeID;
            Korr.Overskrift = Overskrift;
            Korr.Status = 1;
            Korr.Ekstern = Ekstern;
            
            Korr.Mime = FileIO.getMimeFromFile(Filename); // System.Net.Mime.MediaTypeNames.Application.Pdf ' Text.Plain
            
            // Load file from harddrive and load into database
            FileInfo fi = new FileInfo(Filename);
            Korr.Filename = fi.Name;
            Korr.Ext = fi.Extension.Replace(".", "");
            Korr.Size = (int) fi.Length;
            fi = null;
            
            using (FileStream reader = new FileStream(Filename, FileMode.Open))
            {
                byte[] blob = new byte[System.Convert.ToInt32(reader.Length - 1)+ 1];
                reader.Read(blob, 0, (int) reader.Length);
                reader.Close();
                //reader.Dispose()
                Korr.Blob = blob;
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
            
            return Korrespondance.Insert(Korr);
        }
        
        public static int InsertFromText(int CompanyID, int MedarbejderID, int TypeID, string Overskrift, string Filename, string Tekst, bool Ekstern = false)
        {
            
            Korrespondance Korr = new Korrespondance();
            Korr.CompanyID = CompanyID;
            Korr.MedarbejderID = MedarbejderID;
            Korr.Type = TypeID;
            Korr.Overskrift = Overskrift;
            Korr.Status = 1;
            Korr.Ekstern = Ekstern;
            
            Korr.Mime = "text/html"; //text/plain '   getMimeFromFile(Filename) '
            if (!Filename.Contains("."))
            {
                Filename += ".txt";
            }
            Korr.Filename = Filename;
            Korr.Ext = Filename.Substring(Filename.LastIndexOf(".") + 1);
            Korr.Size = Tekst.Length;
            
            byte[] bytes = System.Text.Encoding.ASCII.GetBytes(Tekst);
            Korr.Blob = bytes;
            
            
            return Korrespondance.Insert(Korr);
        }
        
#endregion
        
#endregion
        
#region  Get Data
        
#region  Get Data by Criteria
        
        public static Korrespondance GetKorrespondanceByCriteria(string OrderBY, string criteria, params SqlParameter[] @params)
        {
            List<Korrespondance> list = GetKorrespondancesByCriteria(OrderBY, criteria, @params);
            if (list.Count > 0)
            {
                return list[0];
            }
            else
            {
                return new Korrespondance(); //Nothing
            }
        }
        public static List<Korrespondance> GetKorrespondancesByCriteria(string OrderBY, string criteria, params SqlParameter[] @params)
        {
            DBAccess db = new DBAccess();
            //Dim conn As SqlConnection = DataFunctions.GetConnection()
            string query = "";
            query += "SELECT ";
            if (OrderBY != "")
            {
                query += " TOP (100) PERCENT ";
            }
            query += " *";
            query += " FROM vw_Co2Db_Korrespondance ";
            if (criteria != "")
            {
                query += " WHERE (" + criteria + ")";
            }
            if (OrderBY != "")
            {
                query += " ORDER BY " + OrderBY;
            }
            
            //Dim cmd As New SqlCommand(query, conn)
            db.CommandType = CommandType.Text;
            db.CommandText = query;
            db.AddRange(@params);
            
            db.Open();
            SqlDataReader dr = (SqlDataReader) (db.ExecuteReader()); //cmd.ExecuteReader()
            
            List<Korrespondance> list = new List<Korrespondance>();
            while (dr.Read())
            {
                Korrespondance Korr = new Korrespondance();
                Populate(dr, Korr);
                list.Add(Korr);
            }
            
            db.Dispose();
            
            return list;
        }
        //vw_Co2Db_Korrespondance_all
        
        public static DataSet GetKorrespondancesByCriteriaDS(string fieldnames, string criteria, params SqlParameter[] @params)
        {
            DBAccess db = new DBAccess();
            string query = "";
            query += "SELECT ";
            if (fieldnames.Trim() != "")
            {
                query += fieldnames;
            }
            else
            {
                query += " * ";
            }
            query += " FROM vw_Co2Db_Korrespondance ";
            if (criteria != "")
            {
                query += " WHERE (" + criteria + ")";
            }
            
            db.CommandType = CommandType.Text;
            db.CommandText = query;
            db.AddRange(@params);
            
            DataSet ds = db.ExecuteDataSet(); //(_SQLSelectByGuid)
            return ds;
        }
        
#endregion
        
        //Co2Db_Korrespondance_SearchAll
        
        public static DataSet GetKorrespondanceListe(int CompanyID = -1, int MedarbejderID = -1)
        {
            DBAccess db = new DBAccess();
            db.Parameters.Add(new SqlParameter("@CompanyID", Funktioner.ToInt(CompanyID)));
            db.Parameters.Add(new SqlParameter("@MedarbejderID", Funktioner.ToInt(MedarbejderID)));
            DataSet ds = db.ExecuteDataSet("Co2Db_Korrespondance_SelectAll");
            return ds;
        }
        
        public static DataSet GetNote(int ID)
        {
            DBAccess db = new DBAccess();
            db.Parameters.Add(new SqlParameter("@ID", ID));
            DataSet ds = db.ExecuteDataSet("Co2Db_Korrespondance_SelectOne");
            return ds;
        }
        
        public static DataSet GetKorrespondanceSet(int CompanyID, int MedarbejderID, bool Ekstern)
        {
            DBAccess db = new DBAccess();
            //db.Parameters.Add(New SqlParameter("@CompanyID", ToInt(CompanyID)))
            //db.Parameters.Add(New SqlParameter("@MedarbejderID", ToInt(MedarbejderID)))
            db.AddInt("CompanyID", CompanyID);
            db.AddInt("MedarbejderID", MedarbejderID);
            db.AddBoolean("Ekstern", Ekstern);
            DataSet ds = db.ExecuteDataSet("Co2Db_Korrespondance_SelectAll");
            return ds;
        }
        public static DataSet GetKorrespondanceSet(int CompanyID, int MedarbejderID)
        {
            return GetKorrespondanceSet(CompanyID, MedarbejderID, false);
        }
        public static DataSet GetKorrespondanceSet(int CompanyID)
        {
            return GetKorrespondanceSet(CompanyID, -1, false);
        }
        public static DataSet GetKorrespondanceSet()
        {
            return GetKorrespondanceSet(-1, -1, false);
        }
        public static DataSet GetKorrespondanceSet(int CompanyID, bool Ekstern)
        {
            return GetKorrespondanceSet(CompanyID, -1, Ekstern);
        }
        public static DataSet GetKorrespondanceSetEkstern(int CompanyID)
        {
            return GetKorrespondanceSet(CompanyID, -1, true);
        }
        
        public static Korrespondance GetKorrespondance(int ID)
        {
            DBAccess db = new DBAccess();
            Korrespondance n = new Korrespondance();
            db.Parameters.Add(new SqlParameter("@ID", ID));
            SqlDataReader dr = (SqlDataReader) (db.ExecuteReader("Co2Db_Korrespondance_SelectOne"));
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Populate(dr, n);
                }
            }
            if (!dr.IsClosed)
            {
                dr.Close();
            }
            return n;
        }
        
        
        public static Korrespondance GetKorrespondanceSearch(int CompanyID, int MedarbejderID = -1, bool Ekstern = false, string Search = "")
        {
            List<Korrespondance> list = GetKorrespondanceSearchList(CompanyID, MedarbejderID, Ekstern, Search);
            if (list.Count > 0)
            {
                return list[0];
            }
            else
            {
                return new Korrespondance(); //Nothing
            }
        }
        public static List<Korrespondance> GetKorrespondanceSearchList(int CompanyID, int MedarbejderID = -1, bool Ekstern = false, string Search = "")
        {
            DBAccess db = new DBAccess();
            db.AddInt("CompanyID", CompanyID);
            db.AddInt("MedarbejderID", MedarbejderID);
            db.AddBoolean("Ekstern", Ekstern);
            db.AddNVarChar("Search", Search, 255);
            SqlDataReader dr = (SqlDataReader) (db.ExecuteReader("Co2Db_Korrespondance_SearchAll")); //cmd.ExecuteReader()
            List<Korrespondance> list = new List<Korrespondance>();
            while (dr.Read())
            {
                Korrespondance Korr = new Korrespondance();
                Populate(dr, Korr);
                list.Add(Korr);
            }
            return list;
        }
        
        public static int GetKorrespondanceCount(int CompanyID = -1, int MedarbejderID = -1)
        {
            DBAccess db = new DBAccess();
            object res = null;
            int result = 0;
            db.Parameters.Add(new SqlParameter("@CompanyID", Funktioner.ToInt(CompanyID)));
            db.Parameters.Add(new SqlParameter("@MedarbejderID", Funktioner.ToInt(MedarbejderID)));
            
            res = db.ExecuteScalar("Co2Db_Korrespondance_GetCount");
            if ((!ReferenceEquals(res, null)) && Information.IsNumeric(res))
            {
                result = System.Convert.ToInt32(res);
            }
            return result;
        }
        
        public static object GetKorrespondanceBlob(int ID)
        {
            DBAccess db = new DBAccess();
            db.AddParameter("@ID", ID);
            return db.ExecuteScalar("Co2Db_Korrespondance_SelectOneBlob");
        }
        
#endregion
        
#endregion
        
    }
    
    //End Namespace
}
