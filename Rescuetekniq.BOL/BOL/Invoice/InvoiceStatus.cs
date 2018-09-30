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
    
    public enum Invoice_StatusEnum
    {
        All = -3,
        Deleted, //-2
        Initialize, //-1
        Create, //0
        Active, //1
        SentToCustomer, //2
        WaitingPayment, //3
        Payed, //4
        Canceled, //5
        FirstReminder, //6
        IncassoNotis //7
    }
    
    public enum Invoiceline_StatusEnum
    {
        All = -3,
        Deleted, //-2
        Initialize, //-1
        Create, //0
        Active, //1
        SentToCustomer, //2
        WaitingPayment, //3
        Payed, //4
        Canceled, //5
        FirstReminder, //6
        IncassoNotis //7
    }
    
    public enum Incasso_StatusEnum
    {
        All = -3,
        Deleted, //-2
        Initialize, //-1
        Create, //0
        Active, //1
        SentToCustomer, //2
        Payed, //3
        Canceled //4
    }
    
}
