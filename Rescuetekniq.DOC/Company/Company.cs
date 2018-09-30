// VBConversions Note: VB project level imports
using System.Data;
using PdfSharp;
using System.Diagnostics;
using System.Xml.Linq;
using System.Globalization;
using System.Collections.Generic;
using RescueTekniq.CODE;
using Microsoft.VisualBasic;
using System.Configuration;
using System.Collections;
using RescueTekniq.BOL;
using MigraDoc;
using System;
using System.Linq;
// End of VB project level imports

using RescueTekniq.Doc;


namespace RescueTekniq.Doc
{
    public sealed class Company
    {
        private static string AppSet(string Key, string def = "")
        {
            string res = def;
            try
            {
                res = ConfigurationManager.AppSettings[Key];
            }
            catch (Exception)
            {
            }
            return res;
        }
        
        //<add key="company.name" value="Rescuetekniq ApS"/>
        //<add key="company.adresse" value="Gefionsvej 8"/>
        //<add key="company.zipcode" value="3400"/>
        //<add key="company.city" value="Hillerød"/>
        //<add key="company.country" value="Danmark"/>
        //<add key="company.vatno" value="32446329"/>
        //<add key="company.phone" value="48 15 28 79"/>
        //<add key="company.int.phone" value="+45 48 15 28 79"/>
        //<add key="company.link" value="RescueTekniq.dk"/>
        //<add key="company.email" value="info@Rescuetekniq.dk"/>
        
        public static string name
        {
            get
            {
                return AppSet("company.name");
            }
        }
        public static string adresse
        {
            get
            {
                return AppSet("company.adresse");
            }
        }
        public static string zipcode
        {
            get
            {
                return AppSet("company.zipcode");
            }
        }
        public static string city
        {
            get
            {
                return AppSet("company.city");
            }
        }
        public static string country
        {
            get
            {
                return AppSet("company.country");
            }
        }
        public static string vatno
        {
            get
            {
                return AppSet("company.vatno");
            }
        }
        public static string phone
        {
            get
            {
                return AppSet("company.phone");
            }
        }
        public static string int_phone
        {
            get
            {
                return AppSet("company.int.phone");
            }
        }
        public static string link
        {
            get
            {
                return AppSet("company.link");
            }
        }
        public static string email
        {
            get
            {
                return AppSet("company.email");
            }
        }
        
        public static string OrdreEmail
        {
            get
            {
                return AppSet("email.ordre");
            }
        }
        
        public static string InvoiceEmail
        {
            get
            {
                return AppSet("invoice.email");
            }
        }
        
        public static string InvoicePhone
        {
            get
            {
                return AppSet("invoice.phone");
            }
        }
        
        //Dim KontaktEmail As String = ConfigurationManager.AppSettings("invoice.email")
        //Dim KontaktPhone As String = ConfigurationManager.AppSettings("invoice.phone")
        
        
        
    }
    
}
