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
    
    //TABLE [Co2Db_Agents]
    //SET [AgentID] = <AgentID, uniqueidentifier,>
    //   ,[CompanyName] = <CompanyName, nvarchar(50),>
    //   ,[Title] = <Title, nvarchar(50),>
    //   ,[Name] = <Name, nvarchar(50),>
    //   ,[FirstName] = <FirstName, nvarchar(50),>
    //   ,[LastName] = <LastName, nvarchar(50),>
    //   ,[Address1] = <Address1, nvarchar(50),>
    //   ,[Address2] = <Address2, nvarchar(50),>
    //   ,[City] = <City, nvarchar(50),>
    //   ,[ZipCode] = <ZipCode, nvarchar(50),>
    //   ,[State] = <State, nvarchar(50),>
    //   ,[CountryID] = <CountryID, int,>
    //   ,[Email] = <Email, nvarchar(250),>
    //   ,[OrdreEmail] = <OrdreEmail, nvarchar(250),>
    //   ,[Phone] = <Phone, nvarchar(16),>
    //   ,[CellPhone] = <CellPhone, nvarchar(16),>
    //   ,[Fax] = <Fax, nvarchar(16),>
    //   ,[Status] = <Status, int,>
    //   ,[UserID] = <UserID, nvarchar(50),>
    //   ,[ServiceAgent] = <ServiceAgent, bit,>
    //	,	@AgentSignaturImg	nvarchar(255) = ''
    //   ,[OprettetAF] = <OprettetAF, nvarchar(50),>
    //   ,[OprettetDen] = <OprettetDen, datetime,>
    //   ,[OprettetIP] = <OprettetIP, nvarchar(15),>
    //   ,[RettetAF] = <RettetAF, nvarchar(50),>
    //   ,[RettetDen] = <RettetDen, datetime,>
    //   ,[RettetIP] = <RettetIP, nvarchar(15),>
    
    public class Agents : BaseObject
    {
        
#region  Privates
        
        private Guid _AgentID = Guid.Empty;
        private string _CompanyName;
        private string _Title;
        private string _Name;
        private string _FirstName;
        private string _LastName;
        private string _Address1;
        private string _Address2;
        private string _City;
        private string _ZipCode;
        private string _State;
        private int _CountryID;
        private string _Country;
        private string _Email;
        private string _OrdreEmail;
        private string _Phone;
        private string _CellPhone;
        private string _Fax;
        private AgentsStatusEnum _Status = AgentsStatusEnum.Active;
        private string _UserID;
        private bool _ServiceAgent = false;
        private string _AgentSignaturImg; //	nvarchar(255) = ''
        
#endregion
        
#region  New
        
        public Agents()
        {
        }
        
        public Agents(int ID)
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
        
        public Guid AgentID
        {
            get
            {
                return _AgentID;
            }
            set
            {
                _AgentID = value;
            }
        }
        public string CompanyName
        {
            get
            {
                return _CompanyName;
            }
            set
            {
                _CompanyName = value;
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
        public string Name
        {
            get
            {
                return FirstName + " " + LastName;
            }
            set
            {
                _Name = value;
                _LastName = _Name.Substring(_Name.LastIndexOf(" ")).Trim();
                _FirstName = _Name.Substring(0, _Name.LastIndexOf(" ")).Trim();
            }
        }
        public string FirstName
        {
            get
            {
                return _FirstName;
            }
            set
            {
                _FirstName = value;
            }
        }
        public string LastName
        {
            get
            {
                return _LastName;
            }
            set
            {
                _LastName = value;
            }
        }
        public string Address1
        {
            get
            {
                return _Address1;
            }
            set
            {
                _Address1 = value;
            }
        }
        public string Address2
        {
            get
            {
                return _Address2;
            }
            set
            {
                _Address2 = value;
            }
        }
        public string City
        {
            get
            {
                return _City;
            }
            set
            {
                _City = value;
            }
        }
        public string ZipCode
        {
            get
            {
                return _ZipCode;
            }
            set
            {
                _ZipCode = value;
            }
        }
        public string State
        {
            get
            {
                return _State;
            }
            set
            {
                _State = value;
            }
        }
        public int CountryID
        {
            get
            {
                return _CountryID;
            }
            set
            {
                _CountryID = value;
            }
        }
        public string Country
        {
            get
            {
                return _Country;
            }
            protected set
            {
                _Country = value;
            }
        }
        
        public string CityZipCodeState
        {
            get
            {
                string res = "";
                switch (CountryID)
                {
                    case 45:
                    case 298:
                    case 299:
                        res = ZipCode + " " + City;
                        break;
                    case 1:
                        res = City + ", " + State.ToUpper() + " " + ZipCode;
                        res = res.Replace("  ", " ").Trim();
                        break;
                        //Washington, DC 20546-0001
                    default:
                        res = ZipCode + " " + City;
                        break;
                }
                return res;
            }
        }
        
        
        public string Email
        {
            get
            {
                return _Email;
            }
            set
            {
                _Email = value;
            }
        }
        public string OrdreEmail
        {
            get
            {
                return _OrdreEmail;
            }
            set
            {
                _OrdreEmail = value;
            }
        }
        public string Phone
        {
            get
            {
                return _Phone;
            }
            set
            {
                _Phone = value;
            }
        }
        public string CellPhone
        {
            get
            {
                return _CellPhone;
            }
            set
            {
                _CellPhone = value;
            }
        }
        public string Fax
        {
            get
            {
                return _Fax;
            }
            set
            {
                _Fax = value;
            }
        }
        public AgentsStatusEnum Status
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
        
        public bool ServiceAgent
        {
            get
            {
                return _ServiceAgent;
            }
            set
            {
                _ServiceAgent = value;
            }
        }
        
        public string AgentSignaturImg
        {
            get
            {
                return _AgentSignaturImg;
            }
            set
            {
                _AgentSignaturImg = value;
            }
        }
        
#endregion
        
#region  Shared Populate
        
        private static void AddParms(ref DBAccess db, Agents c)
        {
            var with_1 = c;
            db.AddGuid("AgentID", with_1.AgentID);
            db.AddNVarChar("CompanyName", with_1.CompanyName, 50);
            db.AddNVarChar("Title", with_1.Title, 50);
            
            db.AddNVarChar("Name", with_1.Name, 50);
            db.AddNVarChar("FirstName", with_1.FirstName, 50);
            db.AddNVarChar("LastName", with_1.LastName, 50);
            
            db.AddNVarChar("Address1", with_1.Address1, 50);
            db.AddNVarChar("Address2", with_1.Address2, 50);
            db.AddNVarChar("City", with_1.City, 50);
            db.AddNVarChar("ZipCode", with_1.ZipCode, 50);
            db.AddNVarChar("State", with_1.State, 50);
            db.AddInt("CountryID", with_1.CountryID);
            
            db.AddNVarChar("Email", with_1.Email, 250);
            db.AddNVarChar("OrdreEmail", with_1.OrdreEmail, 250);
            
            db.AddNVarChar("Phone", with_1.Phone, 16);
            db.AddNVarChar("CellPhone", with_1.CellPhone, 16);
            db.AddNVarChar("Fax", with_1.Fax, 16);
            
            db.AddInt("Status", (System.Int32) with_1.Status);
            
            db.AddNVarChar("UserID", with_1.UserID, 50);
            db.AddBoolean("ServiceAgent", with_1.ServiceAgent);
            
            db.AddNVarChar("AgentSignaturImg", with_1.AgentSignaturImg, 255);
            
            AddParmsStandard(db, c);
        }
        
        private static void Populate(SqlDataReader dr, Agents c)
        {
            PopulateStandard(dr, c);
            var with_1 = c;
            with_1.AgentID = dr.DBtoGuid("AgentID");
            with_1.CompanyName = dr.DBtoString("CompanyName");
            with_1.Title = dr.DBtoString("Title");
            with_1._Name = dr.DBtoString("Name");
            with_1.FirstName = dr.DBtoString("FirstName");
            with_1.LastName = dr.DBtoString("LastName");
            with_1.Address1 = dr.DBtoString("Address1");
            with_1.Address2 = dr.DBtoString("Address2");
            with_1.City = dr.DBtoString("City");
            with_1.ZipCode = dr.DBtoString("ZipCode");
            with_1.State = dr.DBtoString("State");
            
            with_1.CountryID = System.Convert.ToInt32(dr.DBtoInteger("CountryID"));
            with_1._Country = dr.DBtoString("Country");
            
            with_1.Email = dr.DBtoString("Email");
            with_1.OrdreEmail = dr.DBtoString("OrdreEmail");
            
            with_1.Phone = dr.DBtoString("Phone");
            with_1.CellPhone = dr.DBtoString("CellPhone");
            with_1.Fax = dr.DBtoString("Fax");
            
            with_1.Status = (RescueTekniq.BOL.AgentsStatusEnum) (dr.DBtoInteger("Status"));
            
            with_1.UserID = dr.DBtoString("UserID");
            with_1.ServiceAgent = System.Convert.ToBoolean(dr.DBtoBoolean("ServiceAgent"));
            
            with_1.AgentSignaturImg = dr.DBtoString("AgentSignaturImg");
        }
        
#endregion
        
#region  Stored Procedure Names
        
        private const string _SQLDelete = "Co2Db_Agents_Delete";
        private const string _SQLInsert = "Co2Db_Agents_Insert";
        private const string _SQLUpdate = "Co2Db_Agents_Update";
        private const string _SQLSelectAll = "Co2Db_Agents_SelectAll";
        
        private const string _SQLSelectID = "Co2Db_Agents_SelectID";
        private const string _SQLSelectOne = "Co2Db_Agents_SelectOne";
        private const string _SQLSelectByAgentGUID = "Co2Db_Agents_SelectByAgentGUID";
        //    Private Const _SQLSelectBySearch As String = "Co2Db_Agents_SelectBySearch"
        
#endregion
        
#region  Metoder
        
#region  Manipulate data
        
        public int Delete()
        {
            return Delete(System.Convert.ToInt32(this));
        }
        public static int Delete(int ID)
        {
            Agents item = new Agents(ID);
            return Delete(item);
        }
        public static int Delete(Agents A)
        {
            DBAccess db = new DBAccess();
            db.AddInt("ID", A.ID);
            int retval = db.ExecuteNonQuery(_SQLDelete);
            AddLog(Status: "Agents", Logtext: string.Format("Delete Agent: ID:{0} Name:{1} ", A.ID, A.Name), Metode: "Delete");
            return retval;
        }
        
        public int Insert()
        {
            return Insert(this);
        }
        public static int Insert(Agents A)
        {
            DBAccess db = new DBAccess();
            
            AddParms(ref db, A);
            
            SqlParameter objParam = new SqlParameter("@ID", 0);
            objParam.Direction = ParameterDirection.Output;
            db.Parameters.Add(objParam);
            int retval = db.ExecuteNonQuery(_SQLInsert);
            if (retval == 1)
            {
                A.ID = Funktioner.ToInt(objParam.Value, -1);
                AddLog(Status: "Agents", Logtext: string.Format("Create Agent: ID:{0} Name:{1} ", A.ID, A.Name), Metode: "Insert");
                return A.ID;
            }
            else
            {
                AddLog(Status: "Agents", Logtext: string.Format("Failure to Create Agent: Name:{0}", A.Name), logtype: LogTypeEnum.Error, Metode: "Insert");
                return -1;
            }
        }
        
        public int Update()
        {
            return Update(this);
        }
        public static int Update(Agents A)
        {
            DBAccess db = new DBAccess();
            
            db.AddInt("ID", A.ID);
            AddParms(ref db, A);
            
            int retval = db.ExecuteNonQuery(_SQLUpdate);
            AddLog(Status: "AED", Logtext: string.Format("Update AED: ID:{0} Name:{1} ", A.ID, A.Name), Metode: "Update");
            return retval;
        }
        
#endregion
        
#region  Get Data
        
        public static DataSet GetAllAgents()
        {
            DBAccess db = new DBAccess();
            DataSet ds = db.ExecuteDataSet(_SQLSelectAll);
            return ds;
        }
        
        public static DataSet GetAgentsDS(int ID)
        {
            DBAccess db = new DBAccess();
            db.AddInt("ID", ID);
            DataSet ds = db.ExecuteDataSet(_SQLSelectID);
            return ds;
        }
        
        public static Agents GetAgents(int ID)
        {
            DBAccess db = new DBAccess();
            db.AddInt("ID", ID);
            SqlDataReader dr = (SqlDataReader) (db.ExecuteReader(_SQLSelectOne));
            if (dr.HasRows)
            {
                Agents c = new Agents();
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
        
        public static Agents GetAgentsByCode(Guid AgentID)
        {
            DBAccess db = new DBAccess();
            db.AddGuid("AgentID", AgentID);
            SqlDataReader dr = (SqlDataReader) (db.ExecuteReader(_SQLSelectByAgentGUID));
            if (dr.HasRows) //SqlDataReader
            {
                
                Agents c = new Agents();
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
        
        //Public Shared Function Search_Agents(ByVal Search As String) As DataSet
        //    Dim ds As DataSet = New DataSet
        //    Dim dsTemp As DataSet = New DataSet
        //    Dim flag As Boolean = False
        //    Dim db As DBAccess = New DBAccess
        
        //    Dim arr As String() = Search.Split(" "c)
        //    For Each s As String In arr
        //        db.AddNVarChar("Search", s, 50)
        
        //        dsTemp = db.ExecuteDataSet(_SQLSelectBySearch)
        //        db.Parameters.Clear()
        //        'If dsTemp.Tables.Count > 0 Then
        //        ds.Merge(dsTemp)
        //        If flag = False Then
        //            Dim pk(1) As DataColumn
        //            pk(0) = ds.Tables(0).Columns("ID")
        //            ds.Tables(0).PrimaryKey = pk
        //            flag = True
        //        End If
        //        'End If
        //    Next
        //    Return ds
        //End Function
        
#endregion
        
#endregion
        
#region  Tags
        
        public string Tags(string tekst)
        {
            return Tags(tekst, this);
        }
        public static string Tags(string tekst, RescueTekniq.BOL.Agents item)
        {
            StringBuilder sb = new StringBuilder();
            tekst = (tekst + " ").Trim();
            if (item.loaded)
            {
                sb.Append(tekst);
                sb.Replace("[AGENT.CODE]", item.AgentID.ToString());
                sb.Replace("[AGENT.GUID]", item.AgentID.ToString());
                
                sb.Replace("[AGENT.COMPANY]", item.CompanyName);
                sb.Replace("[AGENT.COMPANYNAME]", item.CompanyName);
                sb.Replace("[AGENT.TITLE]", item.Title);
                sb.Replace("[AGENT.NAME]", item.Name);
                sb.Replace("[AGENT.FIRSTNAME]", item.FirstName);
                sb.Replace("[AGENT.LASTNAME]", item.LastName);
                
                sb.Replace("[AGENT.ADDRESS]", System.Convert.ToString(item.Address1 + "  " + System.Convert.ToString(item.Address2.Trim() != "" ? "<br />" + Constants.vbNewLine + item.Address2 : "")));
                sb.Replace("[AGENT.ADDRESS1]", item.Address1);
                sb.Replace("[AGENT.ADDRESS2]", item.Address2);
                sb.Replace("[AGENT.ZIPCODECITY]", item.CityZipCodeState);
                sb.Replace("[AGENT.CITY]", item.City);
                sb.Replace("[AGENT.ZIPCODE]", item.ZipCode);
                sb.Replace("[AGENT.STATE]", item.State);
                sb.Replace("[AGENT.COUNTRY]", item.Country);
                
                sb.Replace("[AGENT.EMAIL]", item.Email);
                sb.Replace("[AGENT.ORDREEMAIL]", item.OrdreEmail);
                
                sb.Replace("[AGENT.PHONE]", item.Phone);
                sb.Replace("[AGENT.CELLPHONE]", item.CellPhone);
                sb.Replace("[AGENT.FAX]", item.Fax);
                tekst = sb.ToString();
            }
            return tekst; //sb.ToString
            
        }
        
#endregion
        
    }
    
    
}
