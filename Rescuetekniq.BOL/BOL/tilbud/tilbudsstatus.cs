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


namespace RescueTekniq.BOL
{
    
    public enum TilbudStatusEnum
    {
        Alle = -3,
        Slettet = -2,
        Initialize = -1,
        Opret = 0,
        Rekvireret,
        Ubehandlede,
        Igangvaerende,
        SendtRekvirent,
        Accepteret,
        Afvist,
        Aktiv,
        Lukket,
        Udgaaet,
        Udloebet
    }
    
    
    
}
