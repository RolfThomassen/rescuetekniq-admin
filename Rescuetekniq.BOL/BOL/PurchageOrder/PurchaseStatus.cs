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
    // Purchase
    public enum PurchaseOrder_StatusEnum
    {
        All = -3,
        Deleted, //-2
        Initialize, //-1
        Create, //0
        Active, //1
        SentToSupplier, //2
        WaitingReceiptDate, //3
        Received, //4
        Canceled //5
    }
    
    public enum PurchaseOrderItem_StatusEnum
    {
        All = -3,
        Deleted, //-2
        Initialize, //-1
        Create, //0
        Active, //1   -       NormalOrder '2
        ItemExpected, //2
        ItemReceived, //3
        ItemCanceled, //4
        ItemDiscontinued //5
    }
    //-	Alle
    //-	Modtaget vare
    //-	Afventer vare
    //-	Afventer oplysninger om forventet afsendelsesdato
    //-	Slettet
    
    //-	normal ordre
    //-	vare afbestilt
    //-	vare udgået
    
    
}
