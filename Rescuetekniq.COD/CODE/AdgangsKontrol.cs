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

using System.Security.Principal;
using System.Web;
using System.Web.Security;
using RescueTekniq.CODE;


namespace RescueTekniq.CODE
{
    public sealed class AdgangsKontrol
    {
        
        public static string AccessRole(string AccessRights)
        {
            string res = "-";
            try
            {
                string ApplicationName = System.Convert.ToString((ConfigurationManager.AppSettings["applicationName"] +".").Replace("..", "."));
                
                if (AccessRights == "-")
                {
                    res = "-";
                }
                else if (AccessRights == "*")
                {
                    res = "*";
                }
                else if (AccessRights.StartsWith(ApplicationName))
                {
                    res = AccessRights;
                }
                else
                {
                    res = ApplicationName + AccessRights;
                }
            }
            catch
            {
            }
            return res.Replace("..", ".");
        }
        
        public static bool HaveAccess(string level)
        {
            bool res = false;
            
            foreach (string item in level.Split(','))
            {
                if (item.Trim() != "")
                {
                    switch (item.Trim())
                    {
                        case "-":
                            res = false;
                            goto endOfForLoop;
                        case "*":
                            res = true;
                            goto endOfForLoop;
                        default:
                            if (!Roles.RoleExists(item))
                            {
                                Roles.CreateRole(item);
                            }
                            if (Roles.IsUserInRole(item.Trim()))
                            {
                                res = true;
                                //Exit For
                            }
                            break;
                    }
                }
            }
endOfForLoop:
            //If Roles.IsUserInRole("DebugMaster") Then res = True
            if (Roles.IsUserInRole("SiteMaster"))
            {
                res = true;
            }
            
            return res;
        }
        
        public static bool HaveAccessRoles(string level)
        {
            bool res = false;
            
            try
            {
                foreach (string role in level.Split(",".ToCharArray()))
                {
                    if (HaveAccess(AccessRole(role.Trim())))
                    {
                        res = true;
                        break;
                    }
                }
            }
            catch
            {
            }
            
            return res;
        }
        
        public static bool HaveAccessText(string level)
        {
            System.Boolean res = false;
            if (HaveAccess(level) == false)
            {
                res = false;
            }
            else if (HaveAccess(level) == true)
            {
                res = true;
            }
            return res;
        }
        
        public static bool IsAgent()
        {
            bool res = false;
            string agentRole = ConfigurationManager.AppSettings["agentrole"];
            if (!Roles.RoleExists(agentRole))
            {
                Roles.CreateRole(agentRole);
            }
            try
            {
                if (Roles.IsUserInRole(agentRole))
                {
                    res = true;
                }
            }
            catch
            {
            }
            return res;
        }
        public static bool IsAgent(string username)
        {
            bool res = false;
            string agentRole = ConfigurationManager.AppSettings["agentrole"];
            if (!Roles.RoleExists(agentRole))
            {
                Roles.CreateRole(agentRole);
            }
            try
            {
                if (Roles.IsUserInRole(username, agentRole))
                {
                    res = true;
                }
            }
            catch
            {
            }
            return res;
        }
        
        public static bool IsLogin
        {
            get
            {
                bool res = false;
                if (CurrentUserModule.CurrentUser.Identity.IsAuthenticated)
                {
                    res = true;
                }
                return res;
            }
        }
        
        public static bool IsInRole(string rolelist)
        {
            string[] roles = rolelist.Split(',');
            bool inRole = false;
            try
            {
                foreach (string role in roles)
                {
                    if (role.Trim() != "")
                    {
                        if (CurrentUserModule.CurrentUser.IsInRole(role.Trim()))
                        {
                            inRole = true;
                        }
                    }
                }
            }
            catch
            {
            }
            return inRole;
        }
        
        
        
    }
    
    
    
    
}
