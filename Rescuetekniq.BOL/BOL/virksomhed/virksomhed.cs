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
    
    public enum KundeStatusEnum
    {
        Kundemne = 1,
        Kunde,
        Ophørt
    }
    
    
    public enum ServiceStatusEnum
    {
        alle = -3,
        slettet = -2,
        Initialize,
        Opret,
        Aktiv,
        Lukket,
        EmailSendt,
        Afvist,
        Accepteret
    }
    
    public class Virksomhed : BaseObject
    {
        
#region  Private
        
        private int intKundeGrpID = 0;
        private int intParentID = 0;
        private bool bitHistorik = false;
        private int intProfileType = 0;
        private string strCvrnr = "";
        private string strEANnr = "";
        
        private string strFirmanavn = "";
        private string strAdresse1 = "";
        private string strAdresse2 = "";
        private string strPostnr = "";
        private string strBynavn = "";
        private string strState = "";
        private int intLandekodeID = 0; //
        private int intFirmastatusID = 0;
        private int intAndetID = 0;
        
        private bool bitSamArbejdspartner = false;
        private bool bitMedlemDanskErhverv = false;
        private bool bitMedlemGLSA = false;
        private int intBrancheforeningID = 0;
        
        private string strTelefon = "";
        private string strFax = "";
        private string strEmail = "";
        private string strWebSiteUrl = "";
        private string strNoter = "";
        
        private string strBeslutNavn = "";
        private string strBeslutStilling = "";
        private string strBeslutTelefon = "";
        private string strBeslutMobil = "";
        private string strBeslutEmail = "";
        
        private string strAdminNavn = "";
        private string strAdminStilling = "";
        private string strAdminTelefon = "";
        private string strAdminMobil = "";
        private string strAdminEmail = "";
        
        private KundeStatusEnum intKundeStatusID = KundeStatusEnum.Kundemne;
        private DateTime dtgOphortdato = DateTime.Parse("1-1-1900");
        private int intBetjeningsansvarligID = 1;
        private int intAfdelingAPID = 0;
        private int intDagligBetjenerAPID = 0;
        private int intKontaktansvarligAPID = 0;
        private int intSelskabID = 0;
        private int intDagligBetjenerEksternID = 0;
        private int intKontaktansvarligEksternID = 0;
        
        private Guid _AgentID = System.Guid.Empty;
        private DateTime _AgentStartDate;
        private DateTime _AgentExpiredDate;
        private RescueTekniq.BOL.Agents _Agent = new RescueTekniq.BOL.Agents();
        
        private int _PaymentID; // VBConversions Note: Initial value cannot be assigned here since it is non-static.  Assignment has been moved to the class constructors.
        private bool _FreightFree = false;
        private bool _VATFree = false;
        
        private bool _SendInvoiceViaEmail = false; //bit DEFAULT 0 NULL,
        private string _InvoiceEmail = ""; //nvarchar(100) NULL
        private int _AEDsupplierID = 0; // Ingen
        
        private string _SupplierName = "";
        private string _SupplierTitle = "";
        private string _SupplierPhone = "";
        private string _SupplierMobil = "";
        private string _SupplierEmail = "";
        
        private bool _RescueInfoEdit = false;
        private bool _RescueInfoEditUdvidet = false; //   Udvidet rapporteringsadgang på Rescuetekniq
        
        private Virksomhed _Parent;
#endregion
        
#region  New
        
        public Virksomhed()
        {
            // VBConversions Note: Non-static class variable initialization is below.  Class variables cannot be initially assigned non-static values in C#.
            _PaymentID = Funktioner.ToInt(Combobox.GetIndexByTitle("Invoice.PaymentTerms", "8 dage netto"), 2);
            
            CompanyID = -1;
            LandekodeID = Funktioner.ToInteger(Combobox.GetValueByTitle("Landekode", "Danmark"));
            //LandekodeID = Combobox.GetIdxByTitle("Landekode", "Danmark")
            //FirmastatusID = Combobox.GetIdxByTitle("Firmastatus", "Moderselskab")
        }
        
        public Virksomhed(int CompanyID)
        {
            // VBConversions Note: Non-static class variable initialization is below.  Class variables cannot be initially assigned non-static values in C#.
            _PaymentID = Funktioner.ToInt(Combobox.GetIndexByTitle("Invoice.PaymentTerms", "8 dage netto"), 2);
            
            this.ID = CompanyID;
            
            if (CompanyID > 0)
            {
                DBAccess db = new DBAccess();
                db.AddInt("iCompanyID", CompanyID);
                SqlDataReader dr = (SqlDataReader) (db.ExecuteReader("Co2Db_Virksomheder_SelectOne"));
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Populatete(dr, this);
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
        
        public int CompanyID
        {
            get
            {
                return ID; //intCompanyID
            }
            set
            {
                ID = value;
                //intCompanyID = value
            }
        }
        
        public int ParentID
        {
            get
            {
                return intParentID;
            }
            set
            {
                intParentID = value;
            }
        }
        public string ParentText
        {
            get
            {
                string res = "";
                if (ParentID > 0)
                {
                    res = GetCompanyName(ParentID);
                }
                return res;
            }
        }
        
        private int _AED_Count = -1;
        public int AED_Count
        {
            get
            {
                if (_AED_Count == -1)
                {
                    _AED_Count = AED.GetAEDCountByKoncern(CompanyID);
                }
                return _AED_Count;
            }
        }
        private int _AED_Trainer_count = -1;
        public int AED_Trainer_Count
        {
            get
            {
                if (_AED_Trainer_count == -1)
                {
                    _AED_Trainer_count = AED_Trainer.GetAEDTrainerCountByKoncern(CompanyID);
                }
                return _AED_Trainer_count;
            }
        }
        public bool IsDeleteble
        {
            get
            {
                bool res = false;
                if (AED_Count == 0 & AED_Trainer_Count == 0)
                {
                    res = true;
                }
                return res;
            }
        }
        
        
        public int KundeGrpID
        {
            get
            {
                return intKundeGrpID;
            }
            set
            {
                intKundeGrpID = value;
            }
        }
        public string KundeGrpText
        {
            get
            {
                string res = "";
                try
                {
                    res = KundeGrp.GetKundeGrp(KundeGrpID).KundeGrpNavn;
                }
                catch (Exception)
                {
                    
                }
                return res; // intKundeGrpID
            }
        }
        
        public bool Historik
        {
            get
            {
                return bitHistorik;
            }
            set
            {
                bitHistorik = value;
            }
        }
        
        public int ProfileType
        {
            get
            {
                return intProfileType;
            }
            set
            {
                intProfileType = value;
            }
        }
        public string ProfileTypeText
        {
            get
            {
                string res = "";
                switch (ProfileType)
                {
                    case 1:
                        res = "Virksomhed";
                        break;
                    case 2:
                        res = "Andet";
                        break;
                }
                return res;
            }
        }
        
        public string Cvrnr
        {
            get
            {
                return strCvrnr;
            }
            set
            {
                strCvrnr = value;
            }
        }
        public string EANnr
        {
            get
            {
                return strEANnr;
            }
            set
            {
                strEANnr = value;
            }
        }
        
        public string Firmanavn
        {
            get
            {
                return strFirmanavn;
            }
            set
            {
                strFirmanavn = value;
            }
        }
        
        public object CvrnrFirmanavn
        {
            get
            {
                string res = "";
                if (Cvrnr.Trim() != "")
                {
                    res += Cvrnr;
                    res += " | ";
                }
                if (EANnr.Trim() != "")
                {
                    res += EANnr;
                    res += " | ";
                }
                res += Firmanavn;
                return res;
            }
        }
        
        public string Noter
        {
            get
            {
                return strNoter;
            }
            set
            {
                strNoter = value;
            }
        }
        
        public string Adresse1
        {
            get
            {
                return strAdresse1;
            }
            set
            {
                strAdresse1 = value;
            }
        }
        
        public string Adresse2
        {
            get
            {
                return strAdresse2;
            }
            set
            {
                strAdresse2 = value;
            }
        }
        
        public string Postnr
        {
            get
            {
                return strPostnr;
            }
            set
            {
                strPostnr = value;
            }
        }
        
        public string Bynavn
        {
            get
            {
                return strBynavn;
            }
            set
            {
                strBynavn = value;
            }
        }
        
        public string PostnrBy
        {
            get
            {
                string res = "";
                switch (LandekodeID)
                {
                    case 45:
                    case 298:
                    case 299:
                        res = Postnr + " " + Bynavn;
                        break;
                    case 1:
                        res = Bynavn + ", " + State.ToUpper() + " " + Postnr;
                        break;
                        //Washington, DC 20546-0001
                    default:
                        res = Postnr + " " + Bynavn;
                        break;
                }
                return res;
            }
        }
        
        public string State
        {
            get
            {
                return strState;
            }
            set
            {
                strState = value;
            }
        }
        
        public int LandekodeID
        {
            get
            {
                return intLandekodeID;
            }
            set
            {
                intLandekodeID = value;
            }
        }
        
        public string Land
        {
            get
            {
                return Combobox.GetTitleByValue("Landekode", LandekodeID.ToString()); //Combobox.GetTitle("Landekode", LandekodeID)
            }
        }
        
        public string FuldAdresse
        {
            get
            {
                string res = "";
                if (Adresse1 != "")
                {
                    res += "<br />" + Adresse1;
                }
                if (Adresse2 != "")
                {
                    res += "<br />" + Adresse2;
                }
                if (PostnrBy != "")
                {
                    res += "<br />" + PostnrBy;
                }
                if (Land != "")
                {
                    res += "<br />" + Land;
                }
                if (res.StartsWith("<br />"))
                {
                    res = res.Substring(6);
                }
                return res;
            }
        }
        public string FuldAdresseOneline
        {
            get
            {
                string res = "";
                if (Adresse1 != "")
                {
                    res += ", " + Adresse1;
                }
                if (Adresse2 != "")
                {
                    res += ", " + Adresse2;
                }
                if (PostnrBy != "")
                {
                    res += ", " + PostnrBy;
                }
                if (Land != "")
                {
                    res += ", " + Land;
                }
                if (res.StartsWith(", "))
                {
                    res = res.Substring(2);
                }
                return res;
            }
        }
        
        
        public int FirmastatusID
        {
            get
            {
                return intFirmastatusID;
            }
            set
            {
                intFirmastatusID = value;
            }
        }
        public string FirmaStatusText
        {
            get
            {
                return Combobox.GetTitle("FirmaStatus", FirmastatusID);
            }
        }
        
        public int AndetID
        {
            get
            {
                return intAndetID;
            }
            set
            {
                intAndetID = value;
            }
        }
        public string AndetText
        {
            get
            {
                //Dim res As String = ""
                //Select Case AndetID 'ddlAndet" Field="Virksomhed.Andet
                //	Case 1
                //		res = "Virksomhed"
                //	Case 2
                //		res = "Andet"
                //End Select
                //Return res
                return Combobox.GetTitle("Virksomhed.Andet", AndetID);
            }
        }
        
        public bool SamArbejdspartner
        {
            get
            {
                return bitSamArbejdspartner;
            }
            set
            {
                bitSamArbejdspartner = value;
            }
        }
        public string SamArbejdspartnerText
        {
            get
            {
                return System.Convert.ToString( (SamArbejdspartner ? "Ja" : "Nej"));
            }
        }
        
        
        public bool MedlemDanskErhverv
        {
            get
            {
                return bitMedlemDanskErhverv;
            }
            set
            {
                bitMedlemDanskErhverv = value;
            }
        }
        public string MedlemDanskErhvervText
        {
            get
            {
                return System.Convert.ToString( (MedlemDanskErhverv ? "Ja" : "Nej"));
            }
        }
        
        
        public int BrancheForeningID
        {
            get
            {
                return intBrancheforeningID;
            }
            set
            {
                intBrancheforeningID = value;
            }
        }
        public string BrancheForeningText
        {
            get
            {
                return Combobox.GetTitle("Branceforening", BrancheForeningID);
            }
        }
        
        public bool MedlemGLSA
        {
            get
            {
                return bitMedlemGLSA;
            }
            set
            {
                bitMedlemGLSA = value;
            }
        }
        public string MedlemGLSAText
        {
            get
            {
                return System.Convert.ToString( (MedlemGLSA ? "Ja" : "Nej"));
            }
        }
        
        public string Telefon
        {
            get
            {
                return strTelefon;
            }
            set
            {
                strTelefon = value;
            }
        }
        
        public string Fax
        {
            get
            {
                return strFax;
            }
            set
            {
                strFax = value;
            }
        }
        
        public string Email
        {
            get
            {
                return strEmail;
            }
            set
            {
                strEmail = value;
            }
        }
        
        public string WebSiteUrl
        {
            get
            {
                return strWebSiteUrl;
            }
            set
            {
                strWebSiteUrl = value;
            }
        }
        
        public string BeslutNavn
        {
            get
            {
                return strBeslutNavn;
            }
            set
            {
                strBeslutNavn = value;
            }
        }
        public string BeslutStilling
        {
            get
            {
                return strBeslutStilling;
            }
            set
            {
                strBeslutStilling = value;
            }
        }
        public string BeslutTelefon
        {
            get
            {
                return strBeslutTelefon;
            }
            set
            {
                strBeslutTelefon = value;
            }
        }
        public string BeslutMobil
        {
            get
            {
                return strBeslutMobil;
            }
            set
            {
                strBeslutMobil = value;
            }
        }
        public string BeslutEmail
        {
            get
            {
                return strBeslutEmail;
            }
            set
            {
                strBeslutEmail = value;
            }
        }
        
        public string AdminNavn
        {
            get
            {
                return strAdminNavn;
            }
            set
            {
                strAdminNavn = value;
            }
        }
        public string AdminStilling
        {
            get
            {
                return strAdminStilling;
            }
            set
            {
                strAdminStilling = value;
            }
        }
        public string AdminTelefon
        {
            get
            {
                return strAdminTelefon;
            }
            set
            {
                strAdminTelefon = value;
            }
        }
        public string AdminMobil
        {
            get
            {
                return strAdminMobil;
            }
            set
            {
                strAdminMobil = value;
            }
        }
        public string AdminEmail
        {
            get
            {
                return strAdminEmail;
            }
            set
            {
                strAdminEmail = value;
            }
        }
        
        public string SupplierName
        {
            get
            {
                return _SupplierName;
            }
            set
            {
                SupplierPhone = value;
            }
        }
        public string SupplierTitle
        {
            get
            {
                return _SupplierTitle;
            }
            set
            {
                _SupplierTitle = value;
            }
        }
        public string SupplierPhone
        {
            get
            {
                return _SupplierPhone;
            }
            set
            {
                _SupplierPhone = value;
            }
        }
        public string SupplierMobil
        {
            get
            {
                return _SupplierMobil;
            }
            set
            {
                _SupplierMobil = value;
            }
        }
        public string SupplierEmail
        {
            get
            {
                return _SupplierEmail;
            }
            set
            {
                _SupplierEmail = value;
            }
        }
        
        public KundeStatusEnum KundeStatusID //Integer
        {
            get
            {
                return intKundeStatusID;
            }
            set
            {
                intKundeStatusID = value;
            }
        }
        public string KundeStatusText
        {
            get
            {
                string res = "";
                if (KundeStatusID == ((KundeStatusEnum) 1))
                {
                    res = "Kundemne";
                }
                else if (KundeStatusID == ((KundeStatusEnum) 2))
                {
                    res = "Kunde";
                }
                else if (KundeStatusID == ((KundeStatusEnum) 3))
                {
                    res = "Ophørt pr. " + Ophortdato.ToLongDateString();
                }
                return res;
            }
        }
        
        private bool _Supplier = false;
        public bool Supplier
        {
            get
            {
                return _Supplier;
            }
            set
            {
                _Supplier = value;
            }
        }
        public string SupplierText
        {
            get
            {
                string res = "";
                if (Supplier == true)
                {
                    res = "Leverandør";
                }
                else if (Supplier == false)
                {
                    res = "Kunde";
                }
                else
                {
                    res = "";
                }
                return res;
            }
        }
        
        //cbRescueInfoEdit
        public bool RescueInfoEdit
        {
            get
            {
                return _RescueInfoEdit;
            }
            set
            {
                _RescueInfoEdit = value;
            }
        }
        public string RescueInfoEditText
        {
            get
            {
                string res = "";
                if (RescueInfoEdit == true)
                {
                    res = "Ja"; //"Edit access"
                }
                else
                {
                    res = "Nej"; //"No Access"
                }
                return res;
            }
        }
        
        public bool RescueInfoEditUdvidet
        {
            get
            {
                return _RescueInfoEditUdvidet;
            }
            set
            {
                _RescueInfoEditUdvidet = value;
            }
        }
        public string RescueInfoEditUdvidetText
        {
            get
            {
                string res = "";
                if (RescueInfoEditUdvidet == true)
                {
                    res = "Ja"; //"Edit access"
                }
                else
                {
                    res = "Nej"; //"No Access"
                }
                return res;
            }
        }
        
        public DateTime Ophortdato
        {
            get
            {
                return dtgOphortdato;
            }
            set
            {
                dtgOphortdato = value;
            }
        }
        public int BetjeningsansvarligID
        {
            get
            {
                return intBetjeningsansvarligID;
            }
            set
            {
                intBetjeningsansvarligID = value;
            }
        }
        public string BetjeningsansvarligText
        {
            get
            {
                string res = "";
                switch (BetjeningsansvarligID)
                {
                    case 1:
                        res = "Heart2start";
                        break;
                    case 2:
                        res = "Andet";
                        break;
                }
                res = Combobox.GetTitle("Betjeningsansvarlig", BetjeningsansvarligID);
                return res; //Combobox.GetTitle("Betjeningsansvarlig", BetjeningsansvarligID)
                
            }
        }
        
        public int AfdelingAPID
        {
            get
            {
                return intAfdelingAPID;
            }
            set
            {
                intAfdelingAPID = value;
            }
        }
        public string AfdelingAPText
        {
            get
            {
                return Combobox.GetTitle("AfdelingH2S", AfdelingAPID);
            }
        }
        
        public int DagligBetjenerAPID
        {
            get
            {
                return intDagligBetjenerAPID;
            }
            set
            {
                intDagligBetjenerAPID = value;
            }
        }
        public string DagligBetjenerAPText
        {
            get
            {
                return Combobox.GetTitle("DagligBetjenerH2S", DagligBetjenerAPID);
            }
        }
        
        public int KontaktansvarligAPID
        {
            get
            {
                return intKontaktansvarligAPID;
            }
            set
            {
                intKontaktansvarligAPID = value;
            }
        }
        public string KontaktansvarligAPText
        {
            get
            {
                return Combobox.GetTitle("KontaktansvarligH2S", KontaktansvarligAPID);
            }
        }
        
        public int SelskabID
        {
            get
            {
                return intSelskabID;
            }
            set
            {
                intSelskabID = value;
            }
        }
        public string SelskabText
        {
            get
            {
                return Combobox.GetTitle("EksternSamarbejdspartner", SelskabID); //Selskab
            }
        }
        
        public int DagligBetjenerEksternID
        {
            get
            {
                return intDagligBetjenerEksternID;
            }
            set
            {
                intDagligBetjenerEksternID = value;
            }
        }
        public string DagligBetjenerEksternText
        {
            get
            {
                return Combobox.GetTitle("DagligBetjenerEkstern", DagligBetjenerEksternID);
            }
        }
        
        public int KontaktansvarligEksternID
        {
            get
            {
                return intKontaktansvarligEksternID;
            }
            set
            {
                intKontaktansvarligEksternID = value;
            }
        }
        public string KontaktansvarligEksternText
        {
            get
            {
                return Combobox.GetTitle("KontaktansvarligEkstern", KontaktansvarligEksternID);
            }
        }
        
        public bool IsServicedByAgent
        {
            get
            {
                bool res = false;
                try
                {
                    if (Agent.ServiceAgent)
                    {
                        res = true;
                    }
                }
                catch
                {
                }
                return res;
            }
        }
        public System.Guid AgentID
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
        public string AgentUID
        {
            get
            {
                string name = "";
                try
                {
                    name = Membership.GetUser(AgentID).UserName;
                }
                catch
                {
                }
                return name;
            }
        }
        public string AgentName
        {
            get
            {
                string name = "";
                try
                {
                    name = Agent.Name;
                }
                catch
                {
                }
                return name;
            }
        }
        public DateTime AgentStartDate
        {
            get
            {
                return _AgentStartDate;
            }
            set
            {
                _AgentStartDate = value;
            }
        }
        public DateTime AgentExpiredDate
        {
            get
            {
                return _AgentExpiredDate;
            }
            set
            {
                _AgentExpiredDate = value;
            }
        }
        public RescueTekniq.BOL.Agents Agent
        {
            get
            {
                try
                {
                    if (!(_AgentID == Guid.Empty))
                    {
                        if (!_Agent.loaded)
                        {
                            _Agent = Agents.GetAgentsByCode(_AgentID);
                        }
                        else if (_Agent.AgentID != _AgentID)
                        {
                            _Agent = Agents.GetAgentsByCode(_AgentID);
                        }
                    }
                }
                catch (Exception)
                {
                }
                return _Agent;
            }
        }
        
        public int PaymentID
        {
            get
            {
                return _PaymentID;
            }
            set
            {
                _PaymentID = value;
            }
        }
        public string PaymentText
        {
            get
            {
                return Combobox.GetTitle("Invoice.PaymentTerms", PaymentID);
            }
        }
        
        public bool FreightFree
        {
            get
            {
                return _FreightFree;
            }
            set
            {
                _FreightFree = value;
            }
        }
        
        public bool VATFree
        {
            get
            {
                return _VATFree;
            }
            set
            {
                _VATFree = value;
            }
        }
        
        public bool SendInvoiceViaEmail
        {
            get
            {
                return _SendInvoiceViaEmail;
            }
            set
            {
                _SendInvoiceViaEmail = value;
            }
        }
        
        public string InvoiceEmail
        {
            get
            {
                return _InvoiceEmail;
            }
            set
            {
                _InvoiceEmail = value;
            }
        }
        
        //
        public int AEDsupplierID
        {
            get
            {
                return _AEDsupplierID;
            }
            set
            {
                _AEDsupplierID = value;
            }
        }
        public string AEDsupplierText
        {
            get
            {
                return Combobox.GetTitle("Virksomhed.AED.Supplier", AEDsupplierID);
            }
        }
        
        public string ParentName
        {
            get
            {
                string res = "";
                try
                {
                    if (Parent.isLoaded)
                    {
                        res = Parent.Firmanavn;
                    }
                }
                catch (Exception)
                {
                }
                
                return res;
            }
        }
        
        public RescueTekniq.BOL.Virksomhed Parent
        {
            get
            {
                try
                {
                    if (intParentID > 0)
                    {
                        if (!_Parent.loaded)
                        {
                            _Parent = Virksomhed.GetCompany(intParentID);
                        }
                        else if (_Parent.ID != intParentID)
                        {
                            _Parent = Virksomhed.GetCompany(intParentID);
                        }
                    }
                }
                catch (Exception)
                {
                }
                return _Parent;
            }
        }
        
#endregion
        
#region  Weber Service
        //Private _AEDservice As Boolean = False
        //Private _WeberService As Boolean = False
        
        public bool AEDservice {get; set;}
        public bool WeberService {get; set;}
        
        // Weber Service Privates
        public DateTime WS_AftaleDato {get; set;}
        public string WS_KontaktPerson {get; set;}
        public string WS_KontaktEmail {get; set;}
        public DateTime WS_LastServiceVisitDate {get; set;}
        public DateTime WS_NextServiceVisitDate {get; set;}
        public DateTime WS_ServiceVisitIn {get; set;}
        public int WS_ServicePeriods {get; set;}
        public DateTime WS_ServiceEmailSendt {get; set;}
        public ServiceStatusEnum WS_ServiceStatus {get; set;}
        
        //[X] AED service
        //[X] Weber Service
        //--
        //Weber Service
        
        //Dato for aftale: [DTG]
        //Kontakt Person : [String]
        //Email kontakt  : [string]
        //Dato for sidste service besøg : [DTG]
        //Dato for næste service besøg : [DTG] (altid 1 day i mdr)
        //Service aftalt afholdt [mdr-år]
        //Service intervalt: [1,2,3 år]
        
#endregion
        
#region  Kundekort
        //	Distributionsaftaler:
        
        //	Aftale om distribution af AED og tilbehør
        //	Aftale om indkøb/distribution af PAX
        //	Aftale om distribution af førstehjælpstasker
        //	Aftale om indkøb/distribution af øjenskyl
        //	aftale om indkøb/distribution af snøgg plasterdispenser
        //	Aftale om indkøb/distribution af 112 brandslukker
        //	Aftale om distribution af Kimovi
        public bool Distrib_AED_tilbehør {get; set;}
        public bool Distrib_PAX {get; set;}
        public bool Distrib_FAB {get; set;}
        public bool Distrib_EyeWash {get; set;}
        public bool Distrib_SnøggPlasterDispenser {get; set;}
        public bool Distrib_112FireExtinguisher {get; set;}
        public bool Distrib_Kimovi {get; set;}
        
        
        //Overskrift: Kunderelationsaftaler
        //	Indkøb af LifeKeys
        //	Indkøb af Kimovi cremer
        //	Indkøb af 1. hjælp kits til undervisningsbrug
        //	Adgang etableret på shop2rescue.dk
        public bool Purchase_LifeKeys {get; set;}
        public bool Purchase_KimoviCremer {get; set;}
        public bool Purchase_FAK {get; set;}
        public bool Purchase_Shop2rescue {get; set;}
        
        
        
#endregion
        
#region  Shared Populate
        
        private static void AddParms(ref DBAccess db, Virksomhed rec)
        {
            
            db.Parameters.Add(new SqlParameter("@iParentID", rec.ParentID));
            db.Parameters.Add(new SqlParameter("@iProfileType", rec.ProfileType));
            db.Parameters.Add(new SqlParameter("@sCvrnr", rec.Cvrnr));
            db.Parameters.Add(new SqlParameter("@sEANnr", rec.EANnr));
            db.Parameters.Add(new SqlParameter("@sFirmanavn", rec.Firmanavn));
            db.Parameters.Add(new SqlParameter("@sAdresse1", rec.Adresse1));
            db.Parameters.Add(new SqlParameter("@sAdresse2", rec.Adresse2));
            db.Parameters.Add(new SqlParameter("@sPostnr", rec.Postnr));
            db.Parameters.Add(new SqlParameter("@sBynavn", rec.Bynavn));
            db.Parameters.Add(new SqlParameter("@State", rec.State));
            db.Parameters.Add(new SqlParameter("@iLandekodeID", rec.LandekodeID));
            db.Parameters.Add(new SqlParameter("@iFirmastatusID", rec.FirmastatusID));
            db.Parameters.Add(new SqlParameter("@iAndetID", rec.AndetID));
            
            db.Parameters.Add(new SqlParameter("@bSamArbejdspartner", rec.SamArbejdspartner));
            db.Parameters.Add(new SqlParameter("@bMedlemDanskErhverv", rec.MedlemDanskErhverv));
            db.Parameters.Add(new SqlParameter("@iBrancheForeningID", rec.BrancheForeningID));
            db.Parameters.Add(new SqlParameter("@bMedlemGLSA", rec.MedlemGLSA));
            
            db.Parameters.Add(new SqlParameter("@sTelefon", rec.Telefon));
            db.Parameters.Add(new SqlParameter("@sFax", rec.Fax));
            db.Parameters.Add(new SqlParameter("@sEmail", rec.Email));
            db.Parameters.Add(new SqlParameter("@sWebSiteUrl", rec.WebSiteUrl));
            db.Parameters.Add(new SqlParameter("@sNoter", rec.Noter));
            
            db.Parameters.Add(new SqlParameter("@sBeslutNavn", rec.BeslutNavn));
            db.Parameters.Add(new SqlParameter("@sBeslutStilling", rec.BeslutStilling));
            db.Parameters.Add(new SqlParameter("@sBeslutTelefon", rec.BeslutTelefon));
            db.Parameters.Add(new SqlParameter("@sBeslutMobil", rec.BeslutMobil));
            db.Parameters.Add(new SqlParameter("@sBeslutEmail", rec.BeslutEmail));
            
            db.Parameters.Add(new SqlParameter("@sAdminNavn", rec.AdminNavn));
            db.Parameters.Add(new SqlParameter("@sAdminStilling", rec.AdminStilling));
            db.Parameters.Add(new SqlParameter("@sAdminTelefon", rec.AdminTelefon));
            db.Parameters.Add(new SqlParameter("@sAdminMobil", rec.AdminMobil));
            db.Parameters.Add(new SqlParameter("@sAdminEmail", rec.AdminEmail));
            
            db.Parameters.Add(new SqlParameter("@iKundeStatusID", rec.KundeStatusID));
            db.Parameters.Add(new SqlParameter("@dOphortdato", rec.Ophortdato));
            
            db.Parameters.Add(new SqlParameter("@iBetjeningsansvarligID", rec.BetjeningsansvarligID));
            db.Parameters.Add(new SqlParameter("@iAfdelingAPID", rec.AfdelingAPID));
            db.Parameters.Add(new SqlParameter("@iDagligBetjenerAPID", rec.DagligBetjenerAPID));
            db.Parameters.Add(new SqlParameter("@iKontaktansvarligAPID", rec.KontaktansvarligAPID));
            db.Parameters.Add(new SqlParameter("@iSelskabID", rec.SelskabID));
            db.Parameters.Add(new SqlParameter("@iDagligBetjenerEksternID", rec.DagligBetjenerEksternID));
            db.Parameters.Add(new SqlParameter("@iKontaktansvarligEksternID", rec.KontaktansvarligEksternID));
            
            db.Parameters.Add(new SqlParameter("@iKundeGrpID", rec.KundeGrpID));
            
            db.AddGuid("AgentID", rec.AgentID);
            db.AddDateTime("AgentStartDate", rec.AgentStartDate);
            db.AddDateTime("AgentExpiredDate", rec.AgentExpiredDate);
            db.AddInt("PaymentID", rec.PaymentID);
            db.AddBit("FreightFree", rec.FreightFree);
            db.AddBit("VATFree", rec.VATFree);
            
            db.AddBit("SendInvoiceViaEmail", rec.SendInvoiceViaEmail);
            db.AddNVarChar("InvoiceEmail", rec.InvoiceEmail, 100);
            //Private _SendInvoiceViaEmail As Boolean = False  'bit DEFAULT 0 NULL,
            //Private _InvoiceEmail As String = "" 'nvarchar(100) NULL
            
            db.AddInt("AEDsupplierID", rec.AEDsupplierID);
            db.AddBit("Supplier", rec.Supplier);
            
            db.AddNVarChar("@SupplierName", rec.SupplierName, 100);
            db.AddNVarChar("@SupplierTitle", rec.SupplierTitle, 100);
            db.AddNVarChar("@SupplierPhone", rec.SupplierPhone, 16);
            db.AddNVarChar("@SupplierMobil", rec.SupplierMobil, 16);
            db.AddNVarChar("@SupplierEmail", rec.SupplierEmail, 100);
            
            db.AddBit("RescueInfoEdit", rec.RescueInfoEdit); //@RescueInfoEdit
            db.AddBit("RescueInfoEditUdvidet", rec.RescueInfoEditUdvidet); //@RescueInfoEditUdvidet
            
            // Weber Service Privates
            db.AddBit("AEDservice", rec.AEDservice);
            db.AddBit("WeberService", rec.WeberService);
            db.AddDateTime("WS_AftaleDato", rec.WS_AftaleDato);
            db.AddNVarChar("WS_KontaktPerson", rec.WS_KontaktPerson, 100);
            db.AddNVarChar("WS_KontaktEmail", rec.WS_KontaktEmail, 100);
            db.AddDateTime("WS_LastServiceVisitDate", rec.WS_LastServiceVisitDate);
            db.AddDateTime("WS_NextServiceVisitDate", rec.WS_NextServiceVisitDate);
            db.AddDateTime("WS_ServiceVisitIn", rec.WS_ServiceVisitIn);
            db.AddInt("WS_ServicePeriods", rec.WS_ServicePeriods);
            
            db.AddDateTime("WS_ServiceEmailSendt", rec.WS_NextServiceVisitDate);
            db.AddInt("WS_ServiceStatus", (System.Int32) rec.WS_ServiceStatus);
            
            
            //Distributionsaftaler
            db.AddBit("Distrib_AED_tilbehør", rec.Distrib_AED_tilbehør);
            db.AddBit("Distrib_PAX", rec.Distrib_PAX);
            db.AddBit("Distrib_FAB", rec.Distrib_FAB);
            db.AddBit("Distrib_EyeWash", rec.Distrib_EyeWash);
            db.AddBit("Distrib_SnøggPlasterDispenser", rec.Distrib_SnøggPlasterDispenser);
            db.AddBit("Distrib_112FireExtinguisher", rec.Distrib_112FireExtinguisher);
            db.AddBit("Distrib_Kimovi", rec.Distrib_Kimovi);
            //Kunderelationsaftaler
            db.AddBit("Purchase_LifeKeys", rec.Purchase_LifeKeys);
            db.AddBit("Purchase_KimoviCremer", rec.Purchase_KimoviCremer);
            db.AddBit("Purchase_FAK", rec.Purchase_FAK);
            db.AddBit("Purchase_Shop2rescue", rec.Purchase_Shop2rescue);
            
            
            AddParmsStandard(db, rec);
        }
        
        private static Virksomhed Populatete(System.Data.SqlClient.SqlDataReader dr, Virksomhed rec)
        {
            //Dim c As Virksomhed = New Virksomhed
            PopulateStandard(dr, rec);
            var with_1 = rec;
            with_1.ParentID = System.Convert.ToInt32(dr.DBtoInt("ParentID"));
            with_1.CompanyID = System.Convert.ToInt32(dr.DBtoInt("CompanyID"));
            
            with_1.KundeGrpID = System.Convert.ToInt32(dr.DBtoInt("KundeGrpID"));
            
            with_1.ProfileType = System.Convert.ToInt32(dr.DBtoInt("ProfileType"));
            with_1.Cvrnr = dr.DBtoString("Cvrnr");
            with_1.EANnr = dr.DBtoString("EANnr");
            
            with_1.Firmanavn = dr.DBtoString("Firmanavn");
            with_1.Adresse1 = dr.DBtoString("Adresse1");
            with_1.Adresse2 = dr.DBtoString("Adresse2");
            with_1.Postnr = dr.DBtoString("Postnr");
            with_1.Bynavn = dr.DBtoString("Bynavn");
            with_1.State = dr.DBtoString("State");
            with_1.LandekodeID = System.Convert.ToInt32(dr.DBtoInt("LandekodeID"));
            with_1.FirmastatusID = System.Convert.ToInt32(dr.DBtoInt("FirmastatusID"));
            with_1.AndetID = System.Convert.ToInt32(dr.DBtoInt("AndetID"));
            
            with_1.SamArbejdspartner = System.Convert.ToBoolean(dr.DBtoBool("SamArbejdspartner"));
            with_1.MedlemDanskErhverv = System.Convert.ToBoolean(dr.DBtoBool("MedlemDanskErhverv"));
            with_1.MedlemGLSA = System.Convert.ToBoolean(dr.DBtoBool("MedlemGLSA"));
            with_1.BrancheForeningID = System.Convert.ToInt32(dr.DBtoInt("BrancheForeningID"));
            
            with_1.Telefon = dr.DBtoString("Telefon");
            with_1.Fax = dr.DBtoString("Fax");
            with_1.Email = dr.DBtoString("Email");
            with_1.WebSiteUrl = dr.DBtoString("WebSiteUrl");
            with_1.Noter = dr.DBtoString("Noter");
            
            with_1.BeslutNavn = dr.DBtoString("BeslutNavn");
            with_1.BeslutStilling = dr.DBtoString("BeslutStilling");
            with_1.BeslutTelefon = dr.DBtoString("BeslutTelefon");
            with_1.BeslutMobil = dr.DBtoString("BeslutMobil");
            with_1.BeslutEmail = dr.DBtoString("BeslutEmail");
            
            with_1.AdminNavn = dr.DBtoString("AdminNavn");
            with_1.AdminStilling = dr.DBtoString("AdminStilling");
            with_1.AdminTelefon = dr.DBtoString("AdminTelefon");
            with_1.AdminMobil = dr.DBtoString("AdminMobil");
            with_1.AdminEmail = dr.DBtoString("AdminEmail");
            
            with_1.KundeStatusID = (RescueTekniq.BOL.KundeStatusEnum) (dr.DBtoInt("KundeStatusID"));
            with_1.Ophortdato = System.Convert.ToDateTime(dr.DBtoDate("Ophortdato"));
            
            with_1.BetjeningsansvarligID = System.Convert.ToInt32(dr.DBtoInt("BetjeningsansvarligID"));
            with_1.AfdelingAPID = System.Convert.ToInt32(dr.DBtoInt("AfdelingAPID"));
            with_1.DagligBetjenerAPID = System.Convert.ToInt32(dr.DBtoInt("DagligBetjenerAPID"));
            with_1.KontaktansvarligAPID = System.Convert.ToInt32(dr.DBtoInt("KontaktansvarligAPID"));
            with_1.SelskabID = System.Convert.ToInt32(dr.DBtoInt("SelskabID"));
            with_1.DagligBetjenerEksternID = System.Convert.ToInt32(dr.DBtoInt("DagligBetjenerEksternID"));
            with_1.KontaktansvarligEksternID = System.Convert.ToInt32(dr.DBtoInt("KontaktansvarligEksternID"));
            
            with_1.AgentID = dr.DBtoGuid("AgentID");
            with_1.AgentStartDate = System.Convert.ToDateTime(dr.DBtoDate("AgentStartDate"));
            with_1.AgentExpiredDate = System.Convert.ToDateTime(dr.DBtoDate("AgentExpiredDate"));
            
            with_1.PaymentID = System.Convert.ToInt32(dr.DBtoInt("PaymentID"));
            with_1.FreightFree = System.Convert.ToBoolean(dr.DBtoBool("FreightFree"));
            with_1.VATFree = System.Convert.ToBoolean(dr.DBtoBool("VATfree"));
            
            with_1.SendInvoiceViaEmail = dr.DBtoBool("SendInvoiceViaEmail");
            with_1.InvoiceEmail = dr.DBtoString("InvoiceEmail");
            
            with_1.AEDsupplierID = System.Convert.ToInt32(dr.DBtoInt("AEDsupplierID"));
            with_1.Supplier = System.Convert.ToBoolean(dr.DBtoBool("Supplier"));
            
            with_1.SupplierName = dr.DBtoString("SupplierName");
            with_1.SupplierTitle = dr.DBtoString("SupplierTitle");
            with_1.SupplierPhone = dr.DBtoString("SupplierPhone");
            with_1.SupplierMobil = dr.DBtoString("SupplierMobil");
            with_1.SupplierEmail = dr.DBtoString("SupplierEmail");
            
            with_1.RescueInfoEdit = System.Convert.ToBoolean(dr.DBtoBool("RescueInfoEdit")); //@RescueInfoEdit
            with_1.RescueInfoEditUdvidet = System.Convert.ToBoolean(dr.DBtoBool("RescueInfoEditUdvidet")); //@RescueInfoEditUdvidet
            
            // Weber Service Privates
            with_1.AEDservice = System.Convert.ToBoolean(dr.DBtoInt("AEDservice"));
            with_1.WeberService = System.Convert.ToBoolean(dr.DBtoInt("WeberService"));
            with_1.WS_AftaleDato = System.Convert.ToDateTime(dr.DBtoDate("WS_AftaleDato"));
            with_1.WS_KontaktPerson = dr.DBtoString("WS_KontaktPerson");
            with_1.WS_KontaktEmail = dr.DBtoString("WS_KontaktEmail");
            with_1.WS_LastServiceVisitDate = System.Convert.ToDateTime(dr.DBtoDate("WS_LastServiceVisitDate"));
            with_1.WS_NextServiceVisitDate = System.Convert.ToDateTime(dr.DBtoDate("WS_NextServiceVisitDate"));
            with_1.WS_ServiceVisitIn = System.Convert.ToDateTime(dr.DBtoDate("WS_ServiceVisitIn"));
            with_1.WS_ServicePeriods = dr.DBtoInt("WS_ServicePeriods");
            with_1.WS_ServiceEmailSendt = System.Convert.ToDateTime(dr.DBtoDate("WS_ServiceEmailSendt"));
            with_1.WS_ServiceStatus = (RescueTekniq.BOL.ServiceStatusEnum) (dr.DBtoInt("WS_ServiceStatus"));
            
            
            //Distributionsaftaler
            rec.Distrib_AED_tilbehør = System.Convert.ToBoolean(dr.DBtoBool("Distrib_AED_tilbehør"));
            rec.Distrib_PAX = System.Convert.ToBoolean(dr.DBtoBool("Distrib_PAX"));
            rec.Distrib_FAB = System.Convert.ToBoolean(dr.DBtoBool("Distrib_FAB"));
            rec.Distrib_EyeWash = System.Convert.ToBoolean(dr.DBtoBool("Distrib_EyeWash"));
            rec.Distrib_SnøggPlasterDispenser = System.Convert.ToBoolean(dr.DBtoBool("Distrib_SnøggPlasterDispenser"));
            rec.Distrib_112FireExtinguisher = System.Convert.ToBoolean(dr.DBtoBool("Distrib_112FireExtinguisher"));
            rec.Distrib_Kimovi = System.Convert.ToBoolean(dr.DBtoBool("Distrib_Kimovi"));
            //Kunderelationsaftaler
            rec.Purchase_LifeKeys = System.Convert.ToBoolean(dr.DBtoBool("Purchase_LifeKeys"));
            rec.Purchase_KimoviCremer = System.Convert.ToBoolean(dr.DBtoBool("Purchase_KimoviCremer"));
            rec.Purchase_FAK = System.Convert.ToBoolean(dr.DBtoBool("Purchase_FAK"));
            rec.Purchase_Shop2rescue = System.Convert.ToBoolean(dr.DBtoBool("Purchase_Shop2rescue"));
            
            
            return rec;
        }
        
#endregion
        
#region  Metodes
        
#region  Manipulate data
        
        public int Delete()
        {
            return Delete(this);
        }
        public static int Delete(Virksomhed rec)
        {
            int retval = 0;
            if (rec.IsDeleteble)
            {
                DBAccess db = new DBAccess();
                db.Parameters.Add(new SqlParameter("@iCompanyID", rec.CompanyID));
                retval = db.ExecuteNonQuery("Co2Db_Virksomheder_Delete");
                AddLog(Status: "Virksomhed", Logtext: string.Format("Delete Virksomhed: ID:{0}", rec.CompanyID), Metode: "Delete");
            }
            else
            {
                AddLog(Status: "Virksomhed", Logtext: string.Format("Try to delete Virksomhed: ID:{0} with AED's: {1}, or AED trainer's: {2}", rec.CompanyID, rec.AED_Count, rec.AED_Trainer_Count), Metode: "Delete");
                retval = -1;
            }
            return retval;
        }
        public static int Delete(int iCompanyID)
        {
            //Dim db As DBAccess = New DBAccess
            //db.Parameters.Add(New SqlParameter("@iCompanyID", iCompanyID))
            //Dim retval As Integer = db.ExecuteNonQuery("Co2Db_Virksomheder_Delete")
            //AddLog("Virksomhed", String.Format("Delete Virksomhed: ID:{0}", iCompanyID))
            //Return retval
            Virksomhed v = new Virksomhed(iCompanyID);
            return Delete(v);
        }
        
        public int Insert()
        {
            return Insert(this);
        }
        public static int Insert(Virksomhed rec)
        {
            DBAccess db = new DBAccess();
            
            AddParms(ref db, rec);
            SqlParameter objParam = new SqlParameter("@iCompanyID", 0);
            objParam.Direction = ParameterDirection.Output;
            db.Parameters.Add(objParam);
            
            int retval = db.ExecuteNonQuery("Co2Db_Virksomheder_Insert");
            if (retval == 1)
            {
                rec.ID = int.Parse(objParam.Value.ToString());
                AddLog(Status: "Virksomhed", Logtext: string.Format("Create Virksomhed: ID:{0}", rec.ID), Metode: "Insert");
                return rec.ID;
            }
            else
            {
                AddLog(Status: "Virksomhed", Logtext: string.Format("Failure to Create Virksomhed:"), logtype: LogTypeEnum.Error, Metode: "Insert");
                return -1;
            }
        }
        
        public int Update()
        {
            return Update(this);
        }
        public static int Update(Virksomhed rec)
        {
            DBAccess db = new DBAccess();
            
            //db.Parameters.Add(New SqlParameter("@iCompanyID", c.CompanyID))
            db.AddInt("iCompanyID", rec.CompanyID);
            AddParms(ref db, rec);
            
            int retval = db.ExecuteNonQuery("Co2Db_Virksomheder_Update");
            AddLog(Status: "Virksomhed", Logtext: string.Format("Update Virksomhed: ID:{0}", rec.ID), Metode: "Update");
            return retval;
        }
        
        public int Save()
        {
            return Save(this);
        }
        public static int Save(Virksomhed rec)
        {
            int retval = 0;
            if (rec.ID > 0)
            {
                retval = rec.Update();
            }
            else
            {
                retval = rec.Insert();
            }
            return retval;
        }
        
#endregion
        
#region  Get Data
        
        public static DataSet GetCompany()
        {
            DBAccess db = new DBAccess();
            //db.Parameters.Add(New SqlParameter("@sUserName", username))
            
            db.AddBoolean("IsAgent", AdgangsKontrol.IsAgent());
            db.AddGuid("AgentID", CurrentUserID);
            
            DataSet ds = db.ExecuteDataSet("Co2Db_Virksomheder_SelectAll");
            
            return ds;
        }
        
        
        //Public Shared Function GetCompany(ByVal username As String) As Virksomhed
        //	Dim db As DBAccess = New DBAccess
        //	db.Parameters.Add(New SqlParameter("@sUserName", UserName))
        //	Dim dr As SqlDataReader = CType(db.ExecuteReader("Co2Db_Virksomheder_SelectByUserName"), SqlDataReader)
        //	If dr.HasRows Then
        //		Dim c As Virksomhed = New Virksomhed
        //		While dr.Read
        //			c = PopulateteVirksomhed(dr)
        //		End While
        //		dr.Close()
        //		Return c
        //	Else
        //		dr.Close()
        //		Return Nothing
        //	End If
        //End Function
        
        public enum SearchVirksomhederField : int
        {
            Full = -1,
            All = 0,
            Firmanavn = 1,
            Cvrnr = 2,
            Postnr = 4,
            Telefon = 8
        }
        
        public static DataSet SearchVirksomheder(string navn, SearchVirksomhederField SearchField)
        {
            return SearchVirksomheder(navn, -1, SearchField);
        }
        public static DataSet SearchVirksomheder(string navn)
        {
            return SearchVirksomheder(navn, SearchVirksomhederField.Full);
        }
        public static DataSet SearchVirksomheder(string navn, int status, SearchVirksomhederField SearchField, bool OnlyID = false)
        {
            string[] arr = (navn + " ").Trim().Split(' ');
            DataSet ds = new DataSet();
            DataSet dsTemp = new DataSet();
            bool flag = false;
            DBAccess db = new DBAccess();
            foreach (string s in arr)
            {
                db.AddParameter("@Search", s);
                db.AddInt("Status", status);
                
                db.AddBoolean("IsAgent", AdgangsKontrol.IsAgent());
                db.AddGuid("AgentID", CurrentUserID);
                
                db.AddInt("SearchField", System.Convert.ToInt32(SearchField));
                
                if (OnlyID)
                {
                    dsTemp = db.ExecuteDataSet("Co2Db_Virksomheder_SelectBySearchStatus_ID");
                }
                else
                {
                    dsTemp = db.ExecuteDataSet("Co2Db_Virksomheder_SelectBySearchStatus");
                }
                
                db.Parameters.Clear();
                ds.Merge(dsTemp);
                if (flag == false)
                {
                    DataColumn[] pk = new DataColumn[2];
                    pk[0] = ds.Tables[0].Columns["CompanyID"];
                    ds.Tables[0].PrimaryKey = pk;
                    flag = true;
                }
            }
            return ds;
        }
        public static System.Collections.Generic.List<Virksomhed> Search_VirksomhedList(string search, int status)
        {
            System.Collections.Generic.List<Virksomhed> result = new System.Collections.Generic.List<Virksomhed>();
            int ID = -1;
            DataSet ds = SearchVirksomheder(search, status, SearchVirksomhederField.All, true);
            
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                ID = System.Convert.ToInt32(row["ID"]);
                result.Add(Virksomhed.GetCompany(ID));
            }
            
            return result;
        }
        
        public static DataSet SearchVirksomhederFilter(string Firmanavn, string Cvrnr, string Postnr, string Telefon, int Status = -1, bool OnlyID = false)
        {
            
            DataSet ds = new DataSet();
            DataSet dsTemp = new DataSet();
            bool flag = false;
            DBAccess db = new DBAccess();
            
            db.AddNVarChar("@Firmanavn", Firmanavn, 100);
            db.AddNVarChar("@Cvrnr", Cvrnr, 10);
            db.AddNVarChar("@Postnr", Postnr, 10);
            db.AddNVarChar("@Telefon", Telefon, 16);
            
            db.AddInt("Status", Status);
            
            db.AddBoolean("IsAgent", AdgangsKontrol.IsAgent());
            db.AddGuid("AgentID", CurrentUserID);
            
            if (OnlyID)
            {
                dsTemp = db.ExecuteDataSet("Co2Db_Virksomheder_SearchFilter_ID");
            }
            else
            {
                dsTemp = db.ExecuteDataSet("Co2Db_Virksomheder_SearchFilter");
            }
            
            db.Parameters.Clear();
            ds.Merge(dsTemp);
            if (flag == false)
            {
                DataColumn[] pk = new DataColumn[2];
                pk[0] = ds.Tables[0].Columns["CompanyID"];
                ds.Tables[0].PrimaryKey = pk;
                flag = true;
            }
            
            return ds;
        }
        public static System.Collections.Generic.List<Virksomhed> SearchVirksomhederFilter_List(string Firmanavn, string Cvrnr, string Postnr, string Telefon, int Status = -1)
        {
            System.Collections.Generic.List<Virksomhed> result = new System.Collections.Generic.List<Virksomhed>();
            int ID = -1;
            DataSet ds = SearchVirksomhederFilter(Firmanavn, Cvrnr, Postnr, Telefon, Status, true);
            
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                ID = System.Convert.ToInt32(row["ID"]);
                result.Add(Virksomhed.GetCompany(ID));
            }
            
            return result;
        }
        
        public static Virksomhed GetCompany(int companyid)
        {
            DBAccess db = new DBAccess();
            Virksomhed rec = new Virksomhed();
            db.Parameters.Add(new SqlParameter("@iCompanyID", companyid));
            
            db.AddBoolean("IsAgent", AdgangsKontrol.IsAgent());
            db.AddGuid("AgentID", CurrentUserID);
            
            SqlDataReader dr = (SqlDataReader) (db.ExecuteReader("Co2Db_Virksomheder_SelectOne"));
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Populatete(dr, rec);
                }
            }
            if (!dr.IsClosed)
            {
                dr.Close();
            }
            return rec;
        }
        
        public static string GetCompanyName(int companyID)
        {
            DBAccess db = new DBAccess();
            db.AddParameter("@iCompanyID", companyID);
            
            db.AddBoolean("IsAgent", AdgangsKontrol.IsAgent());
            db.AddGuid("AgentID", CurrentUserID);
            
            return System.Convert.ToString(db.ExecuteScalar("Co2Db_Virksomheder_SelectName"));
        }
        
        public static int GetCompanyCount()
        {
            DBAccess db = new DBAccess();
            
            db.AddBoolean("IsAgent", AdgangsKontrol.IsAgent());
            db.AddGuid("AgentID", CurrentUserID);
            
            return System.Convert.ToInt32(db.ExecuteScalar("Co2Db_Virksomheder_GetCount"));
        }
        
        public static DataSet GetCvrnrList(int CompanyID)
        {
            DBAccess db = new DBAccess();
            db.Parameters.Add(new SqlParameter("@CompanyID", CompanyID));
            
            db.AddBoolean("IsAgent", AdgangsKontrol.IsAgent());
            db.AddGuid("AgentID", CurrentUserID);
            
            DataSet ds = db.ExecuteDataSet("Co2Db_Virksomheder_SelectAllCvrnr");
            return ds;
        }
        
        public static DataSet GetCompanyChildrenList(int CompanyID, bool ShowFree = false, int MedarbGrpID = 0)
        {
            DBAccess db = new DBAccess();
            DataSet ds = new DataSet();
            if (CompanyID != 0)
            {
                db.Parameters.Add(new SqlParameter("@ID", CompanyID));
                db.Parameters.Add(new SqlParameter("@ShowFree", ShowFree));
                db.Parameters.Add(new SqlParameter("@MedarbGrpID", MedarbGrpID));
                
                db.AddBoolean("IsAgent", AdgangsKontrol.IsAgent());
                db.AddGuid("AgentID", CurrentUserID);
                
                ds = db.ExecuteDataSet("Co2Db_Virksomheder_SelectChildren");
                return ds;
            }
            else
            {
                return null;
            }
        }
        
        //Public Shared Function GetMedarbejder__GruppeSet(ByVal ID As Integer) As DataSet
        //	Dim db As DBAccess = New DBAccess
        //	db.Parameters.Add(New SqlParameter("@ID", ID))
        //	Dim ds As DataSet = db.ExecuteDataSet("Co2Db_Virksomheder_SelectChildren")
        //	Return ds
        //End Function
        
        public static DataSet GetCompanyChildrenList(Virksomhed c)
        {
            return GetCompanyChildrenList(c.CompanyID);
        }
        public static int GetCompanyChildrenCount(int CompanyID)
        {
            DBAccess db = new DBAccess();
            db.Parameters.Add(new SqlParameter("@CompanyID", CompanyID));
            
            db.AddBoolean("IsAgent", AdgangsKontrol.IsAgent());
            db.AddGuid("AgentID", CurrentUserID);
            
            return System.Convert.ToInt32(db.ExecuteScalar("Co2Db_Virksomheder_GetChildrenCount"));
        }
        public static int GetCompanyChildrenCount(Virksomhed c)
        {
            return GetCompanyChildrenCount(c.CompanyID);
        }
        
        public static bool SearchCompanyCvrnr(string Cvrnr, int Firmastatus)
        {
            bool res = false;
            DBAccess db = new DBAccess();
            db.AddParameter("@Cvrnr", Cvrnr);
            db.AddParameter("@FirmastatusID", Firmastatus);
            
            db.AddBoolean("IsAgent", AdgangsKontrol.IsAgent());
            db.AddGuid("AgentID", CurrentUserID);
            
            SqlDataReader dr = (SqlDataReader) (db.ExecuteReader("Co2Db_Virksomheder_SelectCvrnrStatus"));
            if (dr.HasRows)
            {
                Virksomhed c = new Virksomhed();
                while (dr.Read())
                {
                    Populatete(dr, c);
                }
                res = c.loaded;
            }
            dr.Close();
            return res;
            
            //Return CType(db.ExecuteScalar("Co2Db_Virksomheder_SelectCvrnrStatus"), String)
            //	[Co2Db_Virksomheder_SearchByCvrnr]()
        }
        
        //Public Shared Function SearchCompanyCvrnr(ByVal Cvrnr As String) As String
        //	Dim db As DBAccess = New DBAccess
        //	db.AddParameter("@Cvrnr", Cvrnr)
        //	db.AddParameter("@FirmastatusID", Firmastatus)
        //	Return CType(db.ExecuteScalar("Co2Db_Virksomheder_SearchByCvrnr"), String)
        //	'	[]()
        //End Function
        
        public static bool SearchMotherCompanyCvrnr(string Cvrnr, int CompanyID)
        {
            DBAccess db = new DBAccess();
            db.AddParameter("@Cvrnr", Cvrnr);
            db.AddParameter("@CompanyID", CompanyID);
            
            db.AddBoolean("IsAgent", AdgangsKontrol.IsAgent());
            db.AddGuid("AgentID", CurrentUserID);
            
            bool res = false;
            res = System.Convert.ToInt32(db.ExecuteScalar("Co2Db_Virksomheder_SearchMotherCompany")) != 0;
            return res;
            
        }
        
        public static Virksomhed SearchCompanyCvrnr(string Cvrnr)
        {
            DBAccess db = new DBAccess();
            db.Parameters.Add(new SqlParameter("@Cvrnr", Cvrnr));
            
            db.AddBoolean("IsAgent", AdgangsKontrol.IsAgent());
            db.AddGuid("AgentID", CurrentUserID);
            
            SqlDataReader dr = (SqlDataReader) (db.ExecuteReader("Co2Db_Virksomheder_SearchByCvrnr"));
            if (dr.HasRows)
            {
                Virksomhed c = new Virksomhed();
                while (dr.Read())
                {
                    Populatete(dr, c);
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
        
        /// <summary>
        /// Get List over Client that are Suppliers
        /// </summary>
        /// <returns>DataSet with Company Suppilers</returns>
        /// <remarks></remarks>
        public static DataSet GetCompanySupplierSet()
        {
            DBAccess db = new DBAccess();
            db.AddBoolean("IsAgent", AdgangsKontrol.IsAgent());
            db.AddGuid("AgentID", CurrentUserID);
            DataSet ds = db.ExecuteDataSet("Co2Db_Virksomheder_SelectAllSupplier");
            return ds;
        }
        
        /// <summary>
        /// Get List over Weber serivce Year Month
        /// </summary>
        /// <returns>DataSet with Company Suppilers</returns>
        /// <remarks></remarks>
        public static DataSet GetCompanyWeberServiceYearMonth(int year, int month)
        {
            DBAccess db = new DBAccess();
            
            DateTime ServiceDate = DateAndTime.DateSerial(year, month, 1);
            
            db.AddInt("year", year);
            db.AddInt("month", month);
            db.AddDateTime("Date", ServiceDate);
            db.AddBoolean("IsAgent", AdgangsKontrol.IsAgent());
            db.AddGuid("AgentID", CurrentUserID);
            DataSet ds = db.ExecuteDataSet("Co2Db_Virksomheder_WeberService_YearMonth");
            return ds;
        }
        
#endregion
        
#endregion
        
#region  Tags
        
        public string Tags(string tekst)
        {
            return Tags(tekst, this);
        }
        public static string Tags(string tekst, RescueTekniq.BOL.Virksomhed item)
        {
            StringBuilder sb = new StringBuilder();
            string res = "";
            sb.Append(tekst);
            sb.Replace("[SUPPLIER.", "[VIRKSOMHED.");
            sb.Replace("[COMPANY.", "[VIRKSOMHED.");
            
            sb.Replace("[BESLUTNINGSTAGER.NAVN]", item.BeslutNavn);
            sb.Replace("[BESLUTNINGSTAGER.EMAIL]", item.BeslutEmail);
            sb.Replace("[BESLUTNINGSTAGER.MOBIL]", item.BeslutMobil);
            sb.Replace("[BESLUTNINGSTAGER.STILLING]", item.BeslutStilling);
            sb.Replace("[BESLUTNINGSTAGER.TELEFON]", item.BeslutTelefon);
            
            sb.Replace("[ADMINISTRATOR.NAVN]", item.AdminNavn);
            sb.Replace("[ADMINISTRATOR.EMAIL]", item.AdminEmail);
            sb.Replace("[ADMINISTRATOR.MOBIL]", item.AdminMobil);
            sb.Replace("[ADMINISTRATOR.STILLING]", item.AdminStilling);
            sb.Replace("[ADMINISTRATOR.TELEFON]", item.AdminTelefon);
            
            sb.Replace("[VIRKSOMHED.SUPPLIER.NAVN]", item.SupplierName);
            sb.Replace("[VIRKSOMHED.SUPPLIER.NAME]", item.SupplierName);
            sb.Replace("[VIRKSOMHED.SUPPLIER.EMAIL]", item.SupplierEmail);
            sb.Replace("[VIRKSOMHED.SUPPLIER.MOBIL]", item.SupplierMobil);
            sb.Replace("[VIRKSOMHED.SUPPLIER.STILLING]", item.SupplierTitle);
            sb.Replace("[VIRKSOMHED.SUPPLIER.TITLE]", item.SupplierTitle);
            sb.Replace("[VIRKSOMHED.SUPPLIER.TELEFON]", item.SupplierPhone);
            sb.Replace("[VIRKSOMHED.SUPPLIER.PHONE]", item.SupplierPhone);
            sb.Replace("[VIRKSOMHED.SUPPLIER.TEXT]", item.SupplierText);
            
            sb.Replace("[VIRKSOMHED.FIRMANAVN]", item.Firmanavn);
            sb.Replace("[VIRKSOMHED.ADRESSE1]", item.Adresse1);
            sb.Replace("[VIRKSOMHED.ADRESSE2]", item.Adresse2);
            sb.Replace("[VIRKSOMHED.BYNAVN]", item.Bynavn);
            sb.Replace("[VIRKSOMHED.POSTNR]", item.Postnr);
            sb.Replace("[VIRKSOMHED.POSTNRBY]", item.PostnrBy);
            sb.Replace("[VIRKSOMHED.STATE]", item.State);
            sb.Replace("[VIRKSOMHED.LAND]", item.Land);
            sb.Replace("[VIRKSOMHED.FULDADRESSE]", item.FuldAdresse);
            
            sb.Replace("[VIRKSOMHED.EMAIL]", item.Email);
            sb.Replace("[VIRKSOMHED.TELEFON]", item.Telefon);
            sb.Replace("[VIRKSOMHED.FAX]", item.Fax);
            
            sb.Replace("[VIRKSOMHED.EANNR]", item.EANnr);
            sb.Replace("[VIRKSOMHED.CVRNR]", item.Cvrnr);
            
            sb.Replace("[VIRKSOMHED.KUNDEGRP]", item.KundeGrpText);
            sb.Replace("[VIRKSOMHED.KUNDESTATUS]", item.KundeStatusText);
            
            sb.Replace("[VIRKSOMHED.PROFILETYPE]", item.ProfileTypeText);
            sb.Replace("[VIRKSOMHED.SAMARBEJDSPARTNER]", item.SamArbejdspartnerText);
            sb.Replace("[VIRKSOMHED.SELSKAB]", item.SelskabText);
            
            sb.Replace("[VIRKSOMHED.WEBSITEURL]", item.WebSiteUrl);
            
            sb.Replace("[VIRKSOMHED.AFDELINGAP]", item.AfdelingAPText);
            sb.Replace("[VIRKSOMHED.ANDET]", item.AndetText);
            sb.Replace("[VIRKSOMHED.BETJENINGSANSVARLIG]", item.BetjeningsansvarligText);
            sb.Replace("[VIRKSOMHED.BRANCHEFORENING]", item.BrancheForeningText);
            sb.Replace("[VIRKSOMHED.DAGLIGBETJENERAP]", item.DagligBetjenerAPText);
            sb.Replace("[VIRKSOMHED.DAGLIGBETJENEREKSTERN]", item.DagligBetjenerEksternText);
            sb.Replace("[VIRKSOMHED.FIRMASTATUS]", item.FirmaStatusText);
            sb.Replace("[VIRKSOMHED.KONTAKTANSVARLIGAP]", item.KontaktansvarligAPText);
            sb.Replace("[VIRKSOMHED.KONTAKTANSVARLIGEKSTERN]", item.KontaktansvarligEksternText);
            sb.Replace("[VIRKSOMHED.MEDLEMDANSKERHVERV]", item.MedlemDanskErhvervText);
            sb.Replace("[VIRKSOMHED.MEDLEMGLSA]", item.MedlemGLSAText);
            
            sb.Replace("[VIRKSOMHED.MODERSELSKAB]", item.ParentName);
            sb.Replace("[VIRKSOMHED.NOTER]", item.Noter);
            
            sb.Replace("[VIRKSOMHED.AGENT]", item.AgentUID);
            sb.Replace("[VIRKSOMHED.AGENT.NAVN]", item.AgentName);
            sb.Replace("[VIRKSOMHED.AGENT.NAME]", item.AgentName);
            sb.Replace("[VIRKSOMHED.AGENT.STARTDATO]", item.AgentStartDate.ToLongDateString());
            sb.Replace("[VIRKSOMHED.AGENT.STARTDATE]", item.AgentStartDate.ToLongDateString());
            sb.Replace("[VIRKSOMHED.AGENT.UDLØBSDATO]", item.AgentExpiredDate.ToLongDateString());
            sb.Replace("[VIRKSOMHED.AGENT.EXPIREDDATE]", item.AgentExpiredDate.ToLongDateString());
            
            sb.Replace("[VIRKSOMHED.FREIGHTFREE]", System.Convert.ToString(item.FreightFree));
            sb.Replace("[VIRKSOMHED.VATFREE]", System.Convert.ToString(item.VATFree));
            
            sb.Replace("[VIRKSOMHED.RESCUEINFOEDIT.TEXT]", item.RescueInfoEditText);
            sb.Replace("[VIRKSOMHED.RESCUEINFOEDITUDVIDET.TEXT]", item.RescueInfoEditUdvidetText);
            
            //' Weber Service Privates
            sb.Replace("[SERVICE.WEBER.", "[VIRKSOMHED.SERVICE.WEBER.");
            sb.Replace("[WEBER.SERVICE.", "[VIRKSOMHED.SERVICE.WEBER.");
            sb.Replace("[VIRKSOMHED.WEBER.SERVICE.", "[VIRKSOMHED.SERVICE.WEBER.");
            sb.Replace("[VIRKSOMHED.SERVICE.WEBER.KONTAKTPERSON]", item.WS_KontaktPerson);
            sb.Replace("[VIRKSOMHED.SERVICE.WEBER.KONTAKTEMAIL]", item.WS_KontaktEmail);
            sb.Replace("[VIRKSOMHED.SERVICE.WEBER.AFTALEDATO]", item.WS_AftaleDato.ToString("dd. MMM yyyy"));
            sb.Replace("[VIRKSOMHED.SERVICE.WEBER.LASTSERVICEVISITDATE]", item.WS_LastServiceVisitDate.ToString("dd. MMM yyyy"));
            sb.Replace("[VIRKSOMHED.SERVICE.WEBER.NEXTSERVICEVISITDATE]", item.WS_NextServiceVisitDate.ToString("dd. MMM yyyy"));
            sb.Replace("[VIRKSOMHED.SERVICE.WEBER.SERVICEVISITIN]", item.WS_ServiceVisitIn.ToString("MMM yyyy"));
            sb.Replace("[VIRKSOMHED.SERVICE.WEBER.SERVICEEMAILSENDT]", item.WS_ServiceEmailSendt.ToString("dd. MMM yyyy"));
            sb.Replace("[VIRKSOMHED.SERVICE.WEBER.SERVICEPERIODS]", item.WS_ServicePeriods.ToString());
            
            
            //Distributionsaftaler
            sb.Replace("[VIRKSOMHED.DISTRIBUTIONSAFTALER.AED_TILBEHØR]", System.Convert.ToString(item.Distrib_AED_tilbehør));
            sb.Replace("[VIRKSOMHED.DISTRIBUTIONSAFTALER.PAX]", System.Convert.ToString(item.Distrib_PAX));
            sb.Replace("[VIRKSOMHED.DISTRIBUTIONSAFTALER.FAB]", System.Convert.ToString(item.Distrib_FAB));
            sb.Replace("[VIRKSOMHED.DISTRIBUTIONSAFTALER.EYEWASH]", System.Convert.ToString(item.Distrib_EyeWash));
            sb.Replace("[VIRKSOMHED.DISTRIBUTIONSAFTALER.SNØGGPLASTERDISPENSER]", System.Convert.ToString(item.Distrib_SnøggPlasterDispenser));
            sb.Replace("[VIRKSOMHED.DISTRIBUTIONSAFTALER.112FIREEXTINGUISHER]", System.Convert.ToString(item.Distrib_112FireExtinguisher));
            sb.Replace("[VIRKSOMHED.DISTRIBUTIONSAFTALER.KIMOVI]", System.Convert.ToString(item.Distrib_Kimovi));
            
            //Kunderelationsaftaler
            sb.Replace("[VIRKSOMHED.KUNDERELATIONSAFTALER.LIFEKEYS]", System.Convert.ToString(item.Purchase_LifeKeys));
            sb.Replace("[VIRKSOMHED.KUNDERELATIONSAFTALER.KIMOVICREMER]", System.Convert.ToString(item.Purchase_KimoviCremer));
            sb.Replace("[VIRKSOMHED.KUNDERELATIONSAFTALER.FAK]", System.Convert.ToString(item.Purchase_FAK));
            sb.Replace("[VIRKSOMHED.KUNDERELATIONSAFTALER.SHOP2RESCUE]", System.Convert.ToString(item.Purchase_Shop2rescue));
            
            res = sb.ToString();
            //If item.Agent.loaded Then res = item.Agent.Tags(res)
            return res;
        }
        
#endregion
        
        
        
    }
    
}
