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
    public sealed class Config
    {
        
        public static string AppSet(string Key, string def = "")
        {
            string res = def;
            try
            {
                res = ConfigurationManager.AppSettings[Key];
            }
            catch (Exception)
            {
            }
            if (string.IsNullOrEmpty(res))
            {
                res = def;
            }
            return res;
        }
        
        public static string GetConnectionString(string name = "connectionstring", string def = "")
        {
            string ConnectionString = "NULL";
            try
            {
                ConnectionString = ConfigurationManager.ConnectionStrings[name].ConnectionString;
            }
            catch (Exception)
            {
            }
            if (string.IsNullOrEmpty(ConnectionString))
            {
                ConnectionString = def;
            }
            return ConnectionString;
        }
        
        public static AppConfig.AppApplication Application
        {
            get
            {
                return new AppConfig.AppApplication();
            }
        }
        
        public static AppConfig.AppBank Bank
        {
            get
            {
                return new AppConfig.AppBank();
            }
        }
        
        public static AppConfig.AppCompany Company
        {
            get
            {
                return new AppConfig.AppCompany();
            }
        }
        
        public static AppConfig.AppContact Contact
        {
            get
            {
                return new AppConfig.AppContact();
            }
        }
        
        public static AppConfig.AppCurrency Currency
        {
            get
            {
                return new AppConfig.AppCurrency();
            }
        }
        
        public static AppConfig.AppEmail Email
        {
            get
            {
                return new AppConfig.AppEmail();
            }
        }
        
        public static AppConfig.AppInvoice Invoice
        {
            get
            {
                return new AppConfig.AppInvoice();
            }
        }
        
        public static AppConfig.AppPurchaseOrder PurchaseOrder
        {
            get
            {
                return new AppConfig.AppPurchaseOrder();
            }
        }
        
        public static AppConfig.AppSmtp Smtp
        {
            get
            {
                return new AppConfig.AppSmtp();
            }
        }
        
        public static AppConfig.AppHouseKeeping HouseKeeping
        {
            get
            {
                return new AppConfig.AppHouseKeeping();
            }
        }
        
        //
    }
    
#region  Application Config Keys
    
    namespace AppConfig
    {
        
        public class AppPurchaseOrder
        {
            //  <add key="purchase.logo" value="~/images/logo/rescuetekniq_logo_big.png"/>
            //  <add key="purchase.email" value="admin@rescuetekniq.dk"/>
            //  <add key="purchase.phone" value="+45 48152879"/>
            //  <add key="purchase.name" value="Vinnie Baun Haldbo"/>
            
            public string name
            {
                get
                {
                    return Config.AppSet("purchase.name");
                }
            }
            public string email
            {
                get
                {
                    return Config.AppSet("purchase.email");
                }
            }
            public string phone
            {
                get
                {
                    return Config.AppSet("purchase.phone");
                }
            }
            public string logo
            {
                get
                {
                    return Config.AppSet("purchase.logo");
                }
            }
            
        }
        
        public class AppInvoice
        {
            //<add key="invoice.email" value="regnskab@heart2start.dk"/>
            //  <add key="invoice.phone" value="+45 28 69 62 00"/>
            //  <add key="invoice.signatur" value="Regnskabsafdelingen"/>
            //  <add key="invoice.signaturimage" value=""/><!--		~/images/signatur/Julia_Gudbjerg.png	-->
            //<add key="invoice.logo" value="~/images/logo/Heart2Start_logo_big.jpg"/>
            //  <add key="invoice.vat" value="25"/>
            
            public string email
            {
                get
                {
                    return Config.AppSet("invoice.email");
                }
            }
            public string phone
            {
                get
                {
                    return Config.AppSet("invoice.phone");
                }
            }
            public string signatur
            {
                get
                {
                    return Config.AppSet("invoice.signatur");
                }
            }
            public string signaturimage
            {
                get
                {
                    return Config.AppSet("invoice.signaturimage");
                }
            }
            public string logo
            {
                get
                {
                    return Config.AppSet("invoice.logo");
                }
            }
            public string VAT
            {
                get
                {
                    return Config.AppSet("invoice.vat");
                }
            }
            public double VATpct
            {
                get
                {
                    double dbl = Funktioner.ToDouble(Config.Invoice.VAT, 25);
                    if (dbl > 1)
                    {
                        dbl = dbl / 100.0;
                    }
                    return dbl;
                }
            }
            
        }
        
        public class AppCompany
        {
            //<add key="company.name" value="Heart2start ApS"/>
            //  <add key="company.adresse" value="Gefionsvej 8"/>
            //  <add key="company.zipcode" value="3400"/>
            //  <add key="company.city" value="Hillerød"/>
            //  <add key="company.country" value="Danmark"/>
            //  <add key="company.vatno" value="32446329"/>
            //  <add key="company.phone" value="4821 2879"/>
            //  <add key="company.int.phone" value="+45 4821 2879"/>
            //  <add key="company.link" value="heart2start.dk"/>
            //  <add key="company.email" value="admin@heart2start.dk"/>
            
            public string name
            {
                get
                {
                    return Config.AppSet("company.name");
                }
            }
            public string adresse
            {
                get
                {
                    return Config.AppSet("company.adresse");
                }
            }
            public string zipcode
            {
                get
                {
                    return Config.AppSet("company.zipcode");
                }
            }
            public string city
            {
                get
                {
                    return Config.AppSet("company.city");
                }
            }
            public string state
            {
                get
                {
                    return Config.AppSet("company.state");
                }
            }
            public string country
            {
                get
                {
                    return Config.AppSet("company.country");
                }
            }
            public string vatno
            {
                get
                {
                    return Config.AppSet("company.vatno");
                }
            }
            public string phone
            {
                get
                {
                    return Config.AppSet("company.phone");
                }
            }
            public string int_phone
            {
                get
                {
                    return Config.AppSet("company.int.phone");
                }
            }
            public string link
            {
                get
                {
                    return Config.AppSet("company.link");
                }
            }
            public string email
            {
                get
                {
                    return Config.AppSet("company.email");
                }
            }
            
            public string PostnrBy
            {
                get
                {
                    string res = "";
                    switch (country) // LandekodeID
                    {
                        case "":
                        case "Danmark":
                        case "45":
                        case "298":
                        case "299":
                            res = zipcode + " " + city;
                            break;
                        case "USA":
                        case "United Stats Of America":
                        case "1":
                            res = city + ", " + state.ToUpper() + " " + zipcode;
                            break;
                            //Washington, DC 20546-0001
                        default:
                            res = zipcode + " " + city;
                            break;
                    }
                    return res;
                }
            }
            
        }
        
        public class AppContact
        {
            //<add key="kontakt.email" value="admin@heart2start.dk"/>
            //  <add key="kontakt.phone" value="+45 48 21 28 79"/>
            //  <add key="kontakt.name" value="Julia Gudbjerg"/>
            
            public string email
            {
                get
                {
                    return Config.AppSet("kontakt.email");
                }
            }
            public string phone
            {
                get
                {
                    return Config.AppSet("kontakt.phone");
                }
            }
            public string name
            {
                get
                {
                    return Config.AppSet("kontakt.name");
                }
            }
        }
        
        public class AppBank
        {
            //<add key="bank.Name" value="Bank Nordik"/>
            //  <add key="bank.Address" value="Amagerbrogade 25, 2300 København S"/>
            //  <add key="bank.RegNo" value="5190"/>
            //  <add key="bank.AccountNo" value="4004499"/>
            //  <add key="bank.SWIFT" value="AMBKDKKK"/>
            //  <add key="bank.IBAN" value="DK4951900004004499"/>
            
            public string Name
            {
                get
                {
                    return Config.AppSet("bank.Name");
                }
            }
            public string Address
            {
                get
                {
                    return Config.AppSet("bank.Address");
                }
            }
            public string RegNo
            {
                get
                {
                    return Config.AppSet("bank.RegNo");
                }
            }
            public string AccountNo
            {
                get
                {
                    return Config.AppSet("bank.AccountNo");
                }
            }
            public string SWIFT
            {
                get
                {
                    return Config.AppSet("bank.SWIFT");
                }
            }
            public string IBAN
            {
                get
                {
                    return Config.AppSet("bank.IBAN");
                }
            }
            
        }
        
        public class AppEmail
        {
            //<add key="email.webmaster" value="webmaster@heart2start.dk"/>
            //  <add key="email.advertise" value="admin@heart2start.dk"/>
            //  <add key="email.info" value="admin@heart2start.dk"/>
            //  <add key="email.test" value="test@heart2start.dk"/>
            //  <add key="email.ordre" value="admin@heart2start.dk"/>
            
            public string webmaster
            {
                get
                {
                    return Config.AppSet("email.webmaster");
                }
            }
            public string advertise
            {
                get
                {
                    return Config.AppSet("email.advertise");
                }
            }
            public string info
            {
                get
                {
                    return Config.AppSet("email.info");
                }
            }
            public string test
            {
                get
                {
                    return Config.AppSet("email.test");
                }
            }
            public string ordre
            {
                get
                {
                    return Config.AppSet("email.ordre");
                }
            }
            
        }
        
        public class AppSmtp
        {
            //<add key="smtp.server" value="mailout1.surf-town.net"/>
            //  <add key="smtp.acount" value="admin@heart2start.dk"/>
            //  <add key="smtp.code" value="vicjos11"/>
            
            public string server
            {
                get
                {
                    return Config.AppSet("smtp.server");
                }
            }
            public string acount
            {
                get
                {
                    return Config.AppSet("smtp.acount");
                }
            }
            public string code
            {
                get
                {
                    return Config.AppSet("smtp.code");
                }
            }
        }
        
        public class AppCurrency
        {
            //<add key="Current.CurrencyCode" value="DKK"/>
            //  <add key="Current.CountryCode" value="45"/>
            
            public string CurrencyCode
            {
                get
                {
                    return Config.AppSet("Current.CurrencyCode", "DKK");
                }
            }
            public string CountryCode
            {
                get
                {
                    return Config.AppSet("Current.CountryCode", "45");
                }
            }
        }
        
        public class AppApplication
        {
            //  <add key="applicationName" value="H2Sadmin"/>
            //  <add key="applicationName.client" value="AEDinfo"/>
            //  <add key="sitename" value="Heart2Start TEST Administration"/>
            //  <add key="pagetitle" value="Test Heart2Start"/>
            //  <add key="sitetitle" value="Test Administration"/>
            //  <add key="sitelogo" value="logo/Heart2Start_logo.gif"/>
            //  <add key="agentrole" value="H2Sadmin.agent"/>
            
            public string Name
            {
                get
                {
                    return Config.AppSet("applicationName");
                }
            }
            
            public string Name_Client
            {
                get
                {
                    return Config.AppSet("applicationName.client");
                }
            }
            
            public string SiteName
            {
                get
                {
                    return Config.AppSet("sitename");
                }
            }
            
            public string PageTitle
            {
                get
                {
                    return Config.AppSet("pagetitle");
                }
            }
            
            public string SiteTitle
            {
                get
                {
                    return Config.AppSet("sitetitle");
                }
            }
            
            public string SiteLogo
            {
                get
                {
                    return Config.AppSet("sitelogo");
                }
            }
            
            public string AgentRole
            {
                get
                {
                    return Config.AppSet("agentrole");
                }
            }
            
            //  <add key="application.version" value="20120806 1707"/>
            //   <add key="application.Test" value="true"/>
            //  <add key="application.Language" value="dk"/>
            //  <add key="application.Culture" value="da-DK"/>      '  <!--  Currency, Date, Time Defaults  -->
            //  <add key="application.UICulture" value="da-DK"/>    '  <!--  User Inteface Language  -->
            //  <add key="application.LCID" value="1030"/>          '  <!--  after initial installation its a good idea to set this to true. If you sign in as admin before doing upgrades you can leave this as false.-->
            //  <add key="DisableSetup" value="false"/>             '  <!--  do not change this unless you understand the workings of the code and have a reason to change it. Default: [dbo]. -->
            //  <add key="MSSQLOwnerPrefix" value="[dbo]."/>        '  <!--  Application info  -->
            
            public string version
            {
                get
                {
                    return Config.AppSet("application.version");
                }
            }
            
            public string Language
            {
                get
                {
                    return Config.AppSet("application.Language");
                }
            }
            
            public string Culture
            {
                get
                {
                    return Config.AppSet("application.Culture");
                }
            }
            
            public string UICulture
            {
                get
                {
                    return Config.AppSet("application.UICulture");
                }
            }
            
            public string LCID
            {
                get
                {
                    return Config.AppSet("application.LCID");
                }
            }
            
            public string DisableSetup
            {
                get
                {
                    return Config.AppSet("DisableSetup");
                }
            }
            
            public string MSSQLOwnerPrefix
            {
                get
                {
                    return Config.AppSet("MSSQLOwnerPrefix");
                }
            }
            
            public bool Test
            {
                get
                {
                    return Funktioner.ToBoolean(Config.AppSet("application.Test", "false"));
                }
            }
            
            public bool UseNewMenu
            {
                get
                {
                    return Funktioner.ToBoolean(Config.AppSet("application.UseNewMenu", "false"));
                }
            }
            
        }
        
        public class AppHouseKeeping
        {
            
            public bool Disable
            {
                get
                {
                    bool res = false;
                    res = Funktioner.ToBoolean(Config.AppSet("HouseKeeping.Disable", "false"));
                    return res;
                }
            }
            
            
            public int Interval
            {
                get
                {
                    int res = 0;
                    res = Funktioner.ToInt(Config.AppSet("HouseKeeping.Interval", "8"), 8);
                    return res;
                }
            }
            
        }
        
        
        
    }
    
#endregion
    
}
