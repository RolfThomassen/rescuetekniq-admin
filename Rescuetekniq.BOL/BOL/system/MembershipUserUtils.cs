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
using System.Web.Configuration;

namespace RescueTekniq.BOL
{
    
    public class MembershipUserUtils
    {
        
        
        public static string MembershipConnectionString
        {
            get
            {
                MembershipSection membershipSection = (MembershipSection) (WebConfigurationManager.GetSection("system.web/membership"));
                string defaultProvider = membershipSection.DefaultProvider;
                ProviderSettings providerSettings = membershipSection.Providers[defaultProvider];
                string MembershipConnectionStringName = providerSettings.Parameters["connectionStringName"];
                //Dim MembershipConnectionUsername As String = providerSettings.Parameters("connectionUsername")
                //Dim MembershipConnectionPassword As String = providerSettings.Parameters("connectionPassword")
                string ConnectionString = WebConfigurationManager.ConnectionStrings[MembershipConnectionStringName].ConnectionString;
                return ConnectionString;
            }
        }
        
        public static void LogUserActivity(Guid UserID, string action)
        {
            //Call the sproc_UpdateUsersCurrentActivity sproc
            DBAccess db = new DBAccess(MembershipConnectionString);
            
            db.AddGuid("UserID", UserID);
            db.AddNVarChar("action", action, 256);
            db.AddDateTime("ApplicationName", DateTime.UtcNow);
            
            try
            {
                int retval = db.ExecuteNonQuery("dbo.sproc_UpdateUsersCurrentActivity");
            }
            catch (Exception)
            {
            }
            
            //Using myConnection As New SqlConnection(ConfigurationManager.ConnectionStrings("MembershipConnectionString").ConnectionString)
            //    Dim myCommand As New SqlCommand("dbo.sproc_UpdateUsersCurrentActivity", myConnection)
            //    myCommand.CommandType = CommandType.StoredProcedure
            
            //    myCommand.Parameters.AddWithValue("@UserId", UserId)
            //    myCommand.Parameters.AddWithValue("@Action", action)
            //    myCommand.Parameters.AddWithValue("@CurrentTimeUtc", DateTime.UtcNow)
            
            //    'Execute the sproc
            //    myConnection.Open()
            //    myCommand.ExecuteNonQuery()
            //    myConnection.Close()
            //End Using
        }
        
        public static bool ChangeUserName(string ApplicationName, string oldUserName, string newUserName)
        {
            bool IsSuccsessful = false;
            
            if (IsUserNameValid(newUserName))
            {
                DBAccess db = new DBAccess(MembershipConnectionString);
                // db.Connection.Close()
                //db.ConnectionString = MembershipConnectionString 'ConfigurationManager.ConnectionStrings("MembershipConnectionString").ConnectionString
                //db.Connection.Open()
                
                db.AddNVarChar("ApplicationName", ApplicationName, 256);
                db.AddNVarChar("OldUserName", oldUserName, 256);
                db.AddNVarChar("NewUserName", newUserName, 256);
                
                try
                {
                    int retval = db.ExecuteNonQuery("dbo.sproc_ChangeUserName");
                    IsSuccsessful = true;
                }
                catch (Exception)
                {
                    IsSuccsessful = false;
                }
            }
            else
            {
                IsSuccsessful = false;
            }
            
            return IsSuccsessful;
        }
        
        private static bool IsUserNameValid(string username)
        {
            // Add whatever username requirement validation you want here, doesn't
            // the membership provider have some build in functionality for this
            if (username.Length > 4)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        public static bool ChangeUsernameZZ(string oldUsername, string newUsername)
        {
            using (SqlConnection myConnection = new SqlConnection())
            {
                myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["MembershipConnectionString"].ConnectionString;
                
                SqlCommand myCommand = new SqlCommand();
                myCommand.Connection = myConnection;
                myCommand.CommandText = "dbo.sproc_ChangeUserName";
                myCommand.CommandType = CommandType.StoredProcedure;
                
                //myCommand.Parameters.Add(CreateInputParam("@ApplicationName", SqlDbType.NVarChar, Membership.ApplicationName))
                //myCommand.Parameters.Add(CreateInputParam("@OldUserName", SqlDbType.NVarChar, oldUsername))
                //myCommand.Parameters.Add(CreateInputParam("@NewUserName", SqlDbType.NVarChar, newUsername))
                
                SqlParameter retValParam = new SqlParameter("@ReturnValue", SqlDbType.Int);
                retValParam.Direction = ParameterDirection.ReturnValue;
                myCommand.Parameters.Add(retValParam);
                
                myConnection.Open();
                myCommand.ExecuteNonQuery();
                //myConnection.Close()
                
                int returnValue = -1;
                if (retValParam.Value != null)
                {
                    returnValue = Convert.ToInt32(retValParam.Value);
                }
                
                if (returnValue != 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            
        }
        
        
    }
}
