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
#region  Debug
    
    public sealed class DebugModule
    {
        
        public static bool DebugMode
        {
            get
            {
                bool res = false;
                if (Funktioner.TestBoolean(SysparmModule.SysParmDef("Debug", System.Convert.ToString(false))) == true) // "true", "1", "-1", "on"
                {
                    if (AdgangsKontrol.HaveAccess("Debug"))
                    {
                        res = true;
                    }
                }
                else
                {
                    res = false;
                }
                return res;
            }
            set
            {
                SysparmModule.set_SysParm("Debug", value.ToString());
            }
        }
        
        public static string DebugText(string text)
        {
            string res = "";
            if (DebugMode)
            {
                res = text;
                Console.WriteLine(text);
            }
            return res;
        }
        
        public static void DebugWrite(string text)
        {
            if (DebugMode)
            {
                Console.WriteLine(text);
            }
        }
        
        public static string DebugSmall(string text)
        {
            string res = "";
            if (DebugMode)
            {
                res = " <small>" + text + "</small>";
                Console.WriteLine(text);
            }
            return res;
        }
        
        public static string DebugPrint(string text)
        {
            string res = "";
            if (DebugMode)
            {
                res = " <span class='debug'>" + text + "</span>";
                Console.WriteLine(text);
            }
            return res;
        }
        
    }
    
#endregion
    
}
