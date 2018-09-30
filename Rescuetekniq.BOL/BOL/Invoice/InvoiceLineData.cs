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


namespace RescueTekniq.BOL
{
    public class InvoiceLineData
    {
        
        public int ID {get; set;}
        public int InvoiceID {get; set;}
        public int Pos {get; set;}
        public Invoiceline_StatusEnum Status {get; set;}
        
        public int ItemID {get; set;}
        public string ItemNo {get; set;}
        public string ItemName {get; set;}
        
        public string LineText {get; set;}
        public string SerialNo {get; set;}
        
        public decimal ItemPrice {get; set;}
        public decimal Discount {get; set;}
        public decimal SalesPrice {get; set;}
        public decimal Quantity {get; set;}
        public decimal LineTotal {get; set;}
        
        public decimal ProvisionRate {get; set;}
        public decimal Provision {get; set;}
        public decimal LineProvision {get; set;}
        
        public bool VAT {get; set;}
        public decimal Freight {get; set;}
        
        public decimal TotalInvoicesSalgsPris {get; set;}
        
        public int CompanyID {get; set;}
        public DateTime InvoiceDate {get; set;}
        public Invoice_StatusEnum InvoiceStatus {get; set;}
        public bool InvoicePaid {get; set;}
        public DateTime InvoicePaidDate {get; set;}
        
    }
    
    
}
