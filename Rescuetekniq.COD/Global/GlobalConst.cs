// VBConversions Note: VB project level imports
using System.Collections.Generic;
using System;
using System.Linq;
using System.Configuration;
using System.Diagnostics;
using Microsoft.VisualBasic;
using System.Xml.Linq;
using System.Collections;
using System.Data;
// End of VB project level imports

using RescueTekniq.CODE;

namespace RescueTekniq.CODE
{
    public sealed class GlobalConst
    {
        
        //----------------------------------
        // URL Path for Mail folder
        private const string _MainPath = "~/";
        // URL Path for Main Temp files
        private const string _TempPath = "~/temp/";
        // URL Path for Tekst files
        private const string _TextPath = "~/texts/";
        // URL Path for Pages
        private const string _pages_path = "~/pages/";
        // URL Path for Upload folder
        private const string _UploadPath = "~/Upload/";
        
        // URL Path for Custom Error pages
        private const string _CustomErrorPagesPath = "~/CustomErrorPages/";
        //----------------------------------
        
        // URL Path for Image folder
        private const string _ImagePath = "~/images/";
        // URL Path for Logo folder
        private const string _LogoPath = _ImagePath + "logo/";
        
        private static System.Collections.Specialized.NameValueCollection _AppSettings;
        public static string AppSettings(string KeyName, string Def = "")
        {
            string value = Def;
            try
            {
                if (ReferenceEquals(_AppSettings, null))
                {
                    _AppSettings = ConfigurationManager.AppSettings;
                }
                value = _AppSettings.Get(KeyName);
                //value = ConfigurationManager.AppSettings(KeyName)
            }
            catch (ConfigurationErrorsException ex)
            {
                Debug.Print(ex.BareMessage);
                value = Def;
            }
            return value;
            //System.Configuration.ConfigurationErrorsException:
            //Could not retrieve a System.Collections.Specialized.NameValueCollection object with the application settings data.
        }
        
        public static string CurrentCurrencyCode
        {
            get
            {
                return AppSettings("Current.CurrencyCode", "Dkk");
            }
        }
        
        public static int CurrentCountryCode
        {
            get
            {
                int ccc = 45;
                string res = AppSettings("Current.CountryCode", "45");
                if (!int.TryParse(res, out ccc))
                {
                    ccc = 45;
                }
                return ccc;
            }
        }
        
        public static string ApplicationLanguage
        {
            get
            {
                return AppSettings("application.Language", "dk");
            }
        }
        
        public static string ApplicationCulture
        {
            get
            {
                return AppSettings("application.Culture", "da-DK");
            }
        }
        public static string ApplicationUICulture
        {
            get
            {
                return AppSettings("application.UICulture", "da-DK");
            }
        }
        
        public static int ApplicationLCID
        {
            get
            {
                string res = AppSettings("application.LCID", "1030");
                int LCID = 1030;
                if (!int.TryParse(res, out LCID))
                {
                    LCID = 1030;
                }
                return LCID;
            }
        }
        
        public static string ApplicationName
        {
            get
            {
                return AppSettings("applicationName");
            }
        }
        public static string ApplicationSiteUserRole
        {
            get
            {
                return ApplicationName +".user";
            }
        }
        public static string ApplicationSiteAgentRole
        {
            get
            {
                //H2Sadmin.agent
                return ApplicationName +".agent";
            }
        }
        
        public static string ApplicationNameClientSite
        {
            get
            {
                return AppSettings("applicationName.client");
            }
        }
        public static string ClientSiteUserRole
        {
            get
            {
                return ApplicationNameClientSite +".user";
            }
        }
        public static string ClientSiteHovedAdmin
        {
            get
            {
                return ApplicationNameClientSite +".admin.hoved";
            }
        }
        
        //invoice.vat
        public static decimal InvoiceVAT
        {
            get
            {
                string res = AppSettings("invoice.vat", "25");
                decimal vat = 25;
                decimal.TryParse(res, out vat);
                //If Not Integer.TryParse(res, vat) Then vat = 25
                return vat;
            }
        }
        
        
        //----------------------------------
        // URL Path for Tekst files
        public const string TextPath = _TextPath;
        public const string PdfPath = _TextPath + "pdf/";
        public const string FaktaarkPath = _TextPath + "faktaark/";
        
        // URL Path for Images files
        public const string SignaturPath = _ImagePath + "signatur/";
        public const string LogoPath = _ImagePath + "logo/";
        
        //----------------------------------
        // URL Path for Upload files
        public const string UploadPath = _UploadPath;
        
        //----------------------------------
        // Root urls
        public const string Logout = "~/logout.aspx";
        
        public const string BestilUrl = "bestil.aspx";
        public const string BestilUrl_EYE = "bestil_EYE.aspx";
        public const int BestilBlob = 49256;
        
        //----------------------------------
        // Email dokuments
        public const string URL_Text_CreateUser = _TextPath + "CreateUser.htm";
        public const string URL_Text_InvoiceLetter = _TextPath + "InvoiceLetter.htm";
        public const string URL_Text_InvoiceLetter_Agent = _TextPath + "InvoiceLetter.agent.htm";
        
        public const string URL_Text_FirstReminderLetter = _TextPath + "FirstReminderLetter.htm";
        public const string URL_Text_FirstReminderLetter_Agent = _TextPath + "FirstReminderLetter.agent.htm";
        
        public const string URL_Text_IncassoNotisLetter = _TextPath + "IncassoNotisLetter.htm";
        public const string URL_Text_IncassoNotisLetter_Agent = _TextPath + "IncassoNotisLetter.agent.htm";
        
        public const string URL_Text_EmailDisclamer = _TextPath + "EmailDisclamer.htm";
        public const string URL_Text_EmailSignatur = _TextPath + "EmailSignatur.htm";
        
        public const string URL_Text_PurchaseOrder_Template = _TextPath + "PurchaseOrder_Template.htm";
        
        public const string URL_Text_TilbudsFolgeBrev = _TextPath + "TilbudsFolgeBrev.htm";
        public const string URL_Text_TilbudsFolgeBrev_Agent = _TextPath + "TilbudsFolgeBrev.agent.htm";
        
        public const string URL_Text_Udlob_AED = _TextPath + "udlob_AED.htm";
        public const string URL_Text_Udlob_Batteri = _TextPath + "udlob_Batteri.htm";
        public const string URL_Text_Udlob_Elektrode = _TextPath + "udlob_Elektrode.htm";
        public const string URL_Text_Ordre_Confimation = _TextPath + "ordre_confimation.htm";
        public const string URL_Text_Udlob_FAB = _TextPath + "udlob_FAB.htm";
        
        public const string URL_Text_Udlob_AED_Agent = _TextPath + "udlob_AED.agent.htm";
        public const string URL_Text_Udlob_Batteri_Agent = _TextPath + "udlob_Batteri.agent.htm";
        public const string URL_Text_Udlob_Elektrode_Agent = _TextPath + "udlob_Elektrode.agent.htm";
        public const string URL_Text_Ordre_Confimation_Agent = _TextPath + "ordre_confimation.agent.htm";
        public const string URL_Text_Udlob_FAB_Agent = _TextPath + "udlob_FAB.agent.htm";
        
        public const string URL_Text_Service_Weber = _TextPath + "service_Weber.htm";
        public const string URL_Text_Service_Weber_Agent = _TextPath + "service_Weber.agent.htm";
        
        public const string URL_Text_Udlob_Bottle = _TextPath + "udlob_Bottle.htm";
        public const string URL_Text_Udlob_Bottle_Agent = _TextPath + "udlob_Bottle.agent.htm";
        
        
        //
        //----------------------------------
        // URL Path for Error pages
        public const string URL_Error_CompanyNotFound = _CustomErrorPagesPath + "virksomhednotfound.aspx";
        public const string URL_Error_NotAuthorized = _CustomErrorPagesPath + "NotAuthorized.aspx";
        public const string URL_Error_ProfileNotFound = _CustomErrorPagesPath + "profilenotfound.aspx";
        public const string URL_Error_UnderContruction = _CustomErrorPagesPath + "UnderConstruction.aspx";
        public const string URL_Error_UserNameNotValid = _CustomErrorPagesPath + "usernameNotValid.aspx";
        
        
        //##################################
        //##################################
        //##                              ##
        //##     Const for Site Pages     ##
        //##                              ##
        //##################################
        //##################################
        
        //----------------------------------
        // URL Path for Pages Administration
        private const string _pages_admin_path = _pages_path + "administration/";
        
        //-------------------------
        // URL Path for Pages Agent
        private const string _pages_agent_path = _pages_path + "agent/";
        
        // pages in Agent
        public const string URL_Agent_List = _pages_agent_path + "AgentListe.aspx";
        public const string URL_Agent_Edit = _pages_agent_path + "AgentEdit.aspx?mode=edit";
        
        //--------------------------------------
        // URL Path for Pages Administration AED
        private const string _pages_admin_aed_path = _pages_admin_path + "AED/";
        
        // pages in AED
        public const string URL_Admin_AED_List = _pages_admin_aed_path + "AEDlist.aspx";
        public const string URL_Admin_AED_Show = _pages_admin_aed_path + "showAED.aspx?DL=1&ID=";
        public const string URL_Admin_AED_Edit = _pages_admin_aed_path + "AEDedit.aspx?mode=edit&ID=";
        public const string URL_Admin_AED_View = _pages_admin_aed_path + "AEDedit.aspx?mode=view&ID=";
        public const string URL_Admin_AED_New = _pages_admin_aed_path + "AEDedit.aspx?mode=new";
        public const string URL_Admin_AED_Afventer = _pages_admin_aed_path + "AEDafventer.aspx";
        public const string URL_Admin_AED_NewOwner = _pages_admin_aed_path + "AEDnewowner.aspx?ID=";
        
        //---------------------------------------------
        // URL Path for Pages Administration AED_Trainer
        private const string _pages_admin_aedtrainer_path = _pages_admin_path + "AEDtrainer/";
        
        // pagrs in AED_Trainer
        public const string URL_Admin_AEDtrainer_List = _pages_admin_aedtrainer_path + "AEDlist.aspx";
        public const string URL_Admin_AEDtrainer_Show = _pages_admin_aedtrainer_path + "showAED.aspx?DL=1&ID=";
        public const string URL_Admin_AEDtrainer_Edit = _pages_admin_aedtrainer_path + "AEDedit.aspx?mode=edit&ID=";
        public const string URL_Admin_AEDtrainer_New = _pages_admin_aedtrainer_path + "AEDedit.aspx?mode=new";
        
        // URL Path for Pages Practiman
        private const string _pages_admin_PractiMan_path = _pages_admin_path + "PractiMan/";
        // pagrs in AED_Trainer
        public const string URL_Admin_PractiMan_List = _pages_admin_PractiMan_path + "olist.aspx";
        public const string URL_Admin_PractiMan_Show = _pages_admin_PractiMan_path + "show.aspx?DL=1&ID={0}";
        public const string URL_Admin_PractiMan_Edit = _pages_admin_PractiMan_path + "edit.aspx?mode=edit&ID={0}";
        public const string URL_Admin_PractiMan_New = _pages_admin_PractiMan_path + "edit.aspx?mode=new";
        
        // URL Path for Pages Pax tasker
        private const string _pages_admin_Pax_path = _pages_admin_path + "Pax/";
        // pagrs in AED_Trainer
        public const string URL_Admin_Pax_List = _pages_admin_Pax_path + "olist.aspx";
        public const string URL_Admin_Pax_Show = _pages_admin_Pax_path + "show.aspx?DL=1&ID={0}";
        public const string URL_Admin_Pax_Edit = _pages_admin_Pax_path + "edit.aspx?mode=edit&ID={0}";
        public const string URL_Admin_Pax_New = _pages_admin_Pax_path + "edit.aspx?mode=new";
        
        // URL Path for Pages Pax tasker
        private const string _pages_admin_Aiva_path = _pages_admin_path + "Aiva/";
        // pagrs in AED_Trainer
        public const string URL_Admin_Aiva_List = _pages_admin_Aiva_path + "olist.aspx";
        public const string URL_Admin_Aiva_Show = _pages_admin_Aiva_path + "show.aspx?DL=1&ID={0}";
        public const string URL_Admin_Aiva_Edit = _pages_admin_Aiva_path + "edit.aspx?mode=edit&ID={0}";
        public const string URL_Admin_Aiva_New = _pages_admin_Aiva_path + "edit.aspx?mode=new";
        
        //--------------------------------------
        // URL Path for Pages Administration EyeWash
        private const string _pages_admin_eye_path = _pages_admin_path + "eyewash/";
        
        // pages in EyeWash
        public const string URL_Admin_EYE_List = _pages_admin_eye_path + "EYElist.aspx";
        public const string URL_Admin_EYE_Show = _pages_admin_eye_path + "showEYE.aspx?DL=1&ID=";
        public const string URL_Admin_EYE_Edit = _pages_admin_eye_path + "EYEedit.aspx?mode=edit&ID=";
        public const string URL_Admin_EYE_View = _pages_admin_eye_path + "EYEedit.aspx?mode=view&ID=";
        public const string URL_Admin_EYE_New = _pages_admin_eye_path + "EYEedit.aspx?mode=new";
        public const string URL_Admin_EYE_Afventer = _pages_admin_eye_path + "EYEafventer.aspx";
        public const string URL_Admin_EYE_NewOwner = _pages_admin_eye_path + "EYEnewowner.aspx?ID=";
        
        //--------------------------------------
        // URL Path for Pages Administration FAB
        private const string _pages_admin_FAB_path = _pages_admin_path + "FirstAidBag/";
        private const string _pages_admin_firstaidbag_path = _pages_admin_path + "FirstAidBag/";
        
        // pages in FAB
        public const string URL_Admin_FAB_List = _pages_admin_FAB_path + "List.aspx";
        public const string URL_Admin_FAB_Show = _pages_admin_FAB_path + "showFirstAidBag.aspx?DL=0&ID=";
        public const string URL_Admin_FAB_PDFdl = _pages_admin_FAB_path + "showFirstAidBag.aspx?DL=1&ID=";
        public const string URL_Admin_FAB_Edit = _pages_admin_FAB_path + "Edit.aspx?mode=edit&ID=";
        public const string URL_Admin_FAB_View = _pages_admin_FAB_path + "Edit.aspx?mode=view&ID=";
        public const string URL_Admin_FAB_New = _pages_admin_FAB_path + "Edit.aspx?mode=new";
        public const string URL_Admin_FAB_Afventer = _pages_admin_FAB_path + "Afventer.aspx";
        public const string URL_Admin_FAB_NewOwner = _pages_admin_FAB_path + "NewOwner.aspx?ID=";
        
        // pages in firstaidbag
        public const string URL_Admin_FirstAidBag_List = _pages_admin_firstaidbag_path + "List.aspx";
        public const string URL_Admin_FirstAidBag_Show = _pages_admin_firstaidbag_path + "showFirstAidBag.aspx?DL=0&ID=";
        public const string URL_Admin_FirstAidBag_PDFdl = _pages_admin_firstaidbag_path + "showFirstAidBag.aspx?DL=1&ID=";
        public const string URL_Admin_FirstAidBag_Edit = _pages_admin_firstaidbag_path + "Edit.aspx?mode=edit&ID=";
        public const string URL_Admin_FirstAidBag_View = _pages_admin_firstaidbag_path + "Edit.aspx?mode=view&ID=";
        public const string URL_Admin_FirstAidBag_New = _pages_admin_firstaidbag_path + "Edit.aspx?mode=new";
        public const string URL_Admin_FirstAidBag_Afventer = _pages_admin_firstaidbag_path + "Afventer.aspx";
        public const string URL_Admin_FirstAidBag_NewOwner = _pages_admin_firstaidbag_path + "NewOwner.aspx?ID=";
        
        
        //---------------------------
        // URL Path for Pages Purchase Order
        private const string _pages_purchase_path = _pages_admin_path + "purchase/";
        
        public const string URL_purchase_PO = _pages_purchase_path + "PurchaseOrder.aspx";
        public const string URL_purchase_PO_new = URL_purchase_PO + "?mode=new";
        public const string URL_purchase_PO_edit = URL_purchase_PO + "?mode=edit&id=";
        public const string URL_purchase_PO_view = URL_purchase_PO + "?mode=view&id=";
        public const string URL_purchase_PO_list = _pages_purchase_path + "List.aspx";
        
        public const string URL_purchase_PO_Suppler_List = _pages_purchase_path + "SupplierSearch.aspx";
        public const string URL_purchase_PO_Suppler_Freight = _pages_purchase_path + "SupplierFreight.aspx";
        public const string URL_purchase_PO_Suppler_Freight_Edit = URL_purchase_PO_Suppler_Freight + "?mode=edit&ID=";
        
        //---------------------------
        // URL Path for Pages Invoice
        private const string _pages_invoice_path = _pages_path + "invoice/";
        
        public const string URL_invoice_CreateInvoice = _pages_invoice_path + "createInvoice.aspx";
        public const string URL_invoice_Invoice = _pages_invoice_path + "invoice.aspx";
        public const string URL_invoice_Invoice_edit = URL_invoice_Invoice + "?mode=edit&id=";
        public const string URL_invoice_Invoice_view = URL_invoice_Invoice + "?mode=view&id=";
        public const string URL_invoice_InvoiceList = _pages_invoice_path + "invoiceList.aspx";
        
        // URL Path for Pages Incasso
        public const string URL_invoice_Incasso = _pages_invoice_path + "incasso.aspx";
        public const string URL_invoice_Incasso_new = URL_invoice_Incasso + "?mode=new&invoiceid=";
        public const string URL_invoice_Incasso_edit = URL_invoice_Incasso + "?mode=edit&id=";
        public const string URL_invoice_Incasso_view = URL_invoice_Incasso + "?mode=view&id=";
        public const string URL_invoice_Incasso_List = _pages_invoice_path + "incassoList.aspx";
        public const string URL_invoice_Incasso_ListPaid = _pages_invoice_path + "incassoPaid.aspx";
        
        //--------------------------
        // URL Path for Pages kunder
        private const string _pages_kunder_path = _pages_path + "kunder/";
        
        // URL Path for Pages kunder AED
        private const string _pages_kunder_aed_path = _pages_kunder_path + "AED/";
        
        public const string URL_kunder_AED_List = _pages_kunder_aed_path + "AEDlistCompany.aspx";
        public const string URL_kunder_AED_Afventer = _pages_kunder_aed_path + "AEDafventerCompany.aspx";
        public const string URL_kunder_AED_New = _pages_kunder_aed_path + "AEDedit.aspx?mode=new";
        public const string URL_kunder_AED_Edit = _pages_kunder_aed_path + "AEDedit.aspx?mode=edit&ID=";
        public const string URL_kunder_AED_View = _pages_kunder_aed_path + "AEDedit.aspx?mode=view&ID=";
        public const string URL_kunder_AED_NewOwner = _pages_kunder_aed_path + "AEDnewowner.aspx?ID=";
        
        // URL Path for Pages kunder PractiMan
        private const string _pages_kunder_PractiMan_path = _pages_kunder_path + "PractiMan/";
        // pagrs in AED_Trainer
        public const string URL_kunder_PractiMan_List = _pages_kunder_PractiMan_path + "list.aspx";
        public const string URL_kunder_PractiMan_Show = _pages_kunder_PractiMan_path + "show.aspx?DL=1&ID={0}";
        public const string URL_kunder_PractiMan_Edit = _pages_kunder_PractiMan_path + "edit.aspx?mode=edit&ID={0}";
        public const string URL_kunder_PractiMan_New = _pages_kunder_PractiMan_path + "edit.aspx?mode=new";
        
        // URL Path for Pages kunder Pax tasker
        private const string _pages_kunder_Pax_path = _pages_kunder_path + "Pax/";
        // pagrs in AED_Trainer
        public const string URL_kunder_Pax_List = _pages_kunder_Pax_path + "list.aspx";
        public const string URL_kunder_Pax_Show = _pages_kunder_Pax_path + "show.aspx?DL=1&ID={0}";
        public const string URL_kunder_Pax_Edit = _pages_kunder_Pax_path + "edit.aspx?mode=edit&ID={0}";
        public const string URL_kunder_Pax_New = _pages_kunder_Pax_path + "edit.aspx?mode=new";
        
        // URL Path for Pages kunder Aiva skabe
        private const string _pages_kunder_Aiva_path = _pages_kunder_path + "Aiva/";
        // pagrs in AED_Trainer
        public const string URL_kunder_Aiva_List = _pages_kunder_Aiva_path + "list.aspx";
        public const string URL_kunder_Aiva_Show = _pages_kunder_Aiva_path + "show.aspx?DL=1&ID={0}";
        public const string URL_kunder_Aiva_Edit = _pages_kunder_Aiva_path + "edit.aspx?mode=edit&ID={0}";
        public const string URL_kunder_Aiva_New = _pages_kunder_Aiva_path + "edit.aspx?mode=new";
        
        // URL Path for Pages kunder Eyewash
        private const string _pages_kunder_eye_path = _pages_kunder_path + "eyewash/";
        
        public const string URL_kunder_EYE_List = _pages_kunder_eye_path + "EYElistCompany.aspx";
        public const string URL_kunder_EYE_Afventer = _pages_kunder_eye_path + "EYEafventerCompany.aspx";
        public const string URL_kunder_EYE_New = _pages_kunder_eye_path + "EYEedit.aspx?mode=new";
        public const string URL_kunder_EYE_Edit = _pages_kunder_eye_path + "EYEedit.aspx?mode=edit&ID=";
        public const string URL_kunder_EYE_View = _pages_kunder_eye_path + "EYEedit.aspx?mode=view&ID=";
        public const string URL_kunder_EYE_NewOwner = _pages_kunder_eye_path + "EYEnewowner.aspx?ID=";
        
        // URL Path for Pages kunder AED
        private const string _pages_kunder_firstaidbag_path = _pages_kunder_path + "FirstAidBag/";
        
        public const string URL_kunder_FirstAidBag_List = _pages_kunder_firstaidbag_path + "ListCompany.aspx";
        public const string URL_kunder_FirstAidBag_Afventer = _pages_kunder_firstaidbag_path + "AfventerCompany.aspx";
        public const string URL_kunder_FirstAidBag_New = _pages_kunder_firstaidbag_path + "Edit.aspx?mode=new";
        public const string URL_kunder_FirstAidBag_Edit = _pages_kunder_firstaidbag_path + "Edit.aspx?mode=edit&ID=";
        public const string URL_kunder_FirstAidBag_View = _pages_kunder_firstaidbag_path + "Edit.aspx?mode=view&ID=";
        public const string URL_kunder_FirstAidBag_NewOwner = _pages_kunder_firstaidbag_path + "NewOwner.aspx?ID=";
        
        //------------------------------
        // URL Path for Pages virksomhed
        private const string _pages_virksomhed_path = _pages_path + "virksomhed/";
        
        // URL for Pages virksomhed kursus
        private const string _pages_virksomhed_kursus_path = _pages_virksomhed_path + "kursus/";
        private const string _pages_virksomhed_kursus_List_path = _pages_virksomhed_kursus_path + "kursuslister/";
        public const string URL_virksomhed_Kursus_List = _pages_virksomhed_kursus_List_path + "kursuslist.aspx";
        public const string URL_virksomhed_Kursus_Edit = _pages_virksomhed_kursus_List_path + "kursusedit.aspx?mode=edit&ID=";
        public const string URL_virksomhed_Kursus_New = _pages_virksomhed_kursus_List_path + "kursusedit.aspx?mode=new&ID=0";
        public const string URL_virksomhed_Kursus_View = _pages_virksomhed_kursus_List_path + "kursusedit.aspx?mode=view&ID=";
        
        // URL for Pages virksomhed tilbud
        private const string _pages_virksomhed_tilbud_path = _pages_virksomhed_path + "tilbud/";
        
        public const string URL_virksomhed_tilbud_Edit = _pages_virksomhed_tilbud_path + "tilbudedit.aspx?mode=edit&TilbudID=";
        public const string URL_virksomhed_tilbud_View = _pages_virksomhed_tilbud_path + "tilbudedit.aspx?mode=view&TilbudID=";
        
        // URL for Pages virksomhed tilbud
        private const string _pages_virksomhed_user_path = _pages_virksomhed_path + "users/";
        
        // URL for Pages virksomhed user - User administation
        public const string URL_virksomhed_user_Edit = _pages_virksomhed_user_path + "editUser.aspx?mode=edit&username=";
        public const string URL_virksomhed_user_New = _pages_virksomhed_user_path + "CreateUser.aspx?mode=new";
        public const string URL_virksomhed_user_View = _pages_virksomhed_user_path + "editUser.aspx?mode=edit&username=";
        public const string URL_virksomhed_user_List = _pages_virksomhed_user_path + "listUsers.aspx";
        
        //--------------------------
        // URL Path for Pages system
        private const string _pages_system_path = _pages_path + "system/";
        
        // URL for Pages system tilbud
        private const string _pages_system_user_path = _pages_system_path + "users/";
        
        // URL for Pages system user - User administation
        public const string URL_system_user_Edit = _pages_system_user_path + "editUser.aspx?mode=edit&username=";
        public const string URL_system_user_New = _pages_system_user_path + "CreateUser.aspx?mode=new";
        public const string URL_system_user_View = _pages_system_user_path + "editUser.aspx?mode=edit&username=";
        public const string URL_system_user_List = _pages_system_user_path + "listUsers.aspx";
        
    }
    
}
