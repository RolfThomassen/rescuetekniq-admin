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
    
    public enum EyeWash_StatusEnum
    {
        alle = -3,
        slettet = -2,
        Initialize, //-1
        Opret, //0
        Aktiv, //1
        Lukket, //2
        Udgaaet, //3
        Udloebet, //4
        EmailSendt, //5
        Afvist, //6
        Accepteret, //NyEnhedkobt '7
        AccepteretIntern, //NyEnhedkobt '8
        UdskiftetGaranti //9
    }
    
    public enum EyeWash_BilagStatus
    {
        Initialize = 0,
        BilagSendt,
        BilagModtagt
    }
    
    public enum EyeWash_ExpiredType
    {
        AED = 1,
        Bottle
    }
    
    public enum EyeWash_ServiceStatusType
    {
        All = -3,
        Deleted = -2,
        Initialize, //-1
        Create, //0
        Aktiv, //1
        OverDue,
        Visited
    }
    
}
