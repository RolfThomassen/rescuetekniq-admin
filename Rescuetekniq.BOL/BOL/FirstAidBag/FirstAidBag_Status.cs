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
    
    public enum FAB_StatusEnum
    {
        All = -3,
        Deleted = -2,
        Initialize, //-1
        Create, //0
        Active, //1
        Closed, //2
        Discontinued, //3
        Expired, //4
        EmailSend, //5
        Dismissed, //6
        Accepted, //NyEnhedkobt '7
        Accepted_Intern, //NyEnhedkobt '8
        WarrantySubstituted //9
    }
    
    public enum FAB_ServiceStatusType
    {
        All = -3,
        Deleted = -2,
        Initialize, //-1
        Create, //0
        Aktiv, //1
        OverDue,
        Visited
    }
    
    public enum FAB_BilagStatus
    {
        Initialize = 0,
        BilagSendt,
        BilagModtagt
        
    }
}
