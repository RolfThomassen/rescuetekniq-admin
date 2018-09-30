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

//Imports System.Web.ApplicationServices

namespace RescueTekniq.CODE
{
    public sealed class CurrentUserModule
    {
        
        public static IPrincipal CurrentUser
        {
            get
            {
                return HttpContext.Current.User;
            }
        }
        
        public static string CurrentUserName
        {
            get
            {
                string userName = "";
                if (AdgangsKontrol.IsLogin)
                {
                    userName = CurrentUser.Identity.Name;
                }
                return userName;
            }
        }
        
        public static Guid CurrentUserID
        {
            get
            {
                Guid UserID = Guid.Empty;
                MembershipUser User = default(MembershipUser);
                try
                {
                    User = Membership.GetUser(CurrentUserName);
                    UserID = (System.Guid) User.ProviderUserKey;
                }
                catch
                {
                }
                return UserID;
            }
        }
        
        public static string CurrentUserIP
        {
            get
            {
                return HttpContext.Current.Request.UserHostAddress;
            }
        }
        
        public static string CurrentUserHostName
        {
            get
            {
                return HttpContext.Current.Request.UserHostName;
            }
        }
        
        public static string CurrentServerVariables(string item)
        {
            return HttpContext.Current.Request.ServerVariables[item].ToString();
        }
        
    }
    
    
    
}
