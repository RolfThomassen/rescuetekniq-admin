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
    
    public partial class _Default_1 : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, System.EventArgs e)
        {
            
            System.Collections.Generic.List<DateTime> dates = GetDates();
            
        }
        
        private const string ConnectionString = "server=ServerName;uid=UserName;password=Password;database=DatebaseName";
        
        private System.Collections.Generic.List<DateTime> GetDates()
        {
            
            System.Collections.Generic.List<DateTime> result = new System.Collections.Generic.List<DateTime>();
            SqlConnection connection = new SqlConnection(ConnectionString);
            SqlCommand command = new SqlCommand("select DateColumn FROM TableName WHERE Condition", connection);
            SqlDataReader reader = default(SqlDataReader);
            
            try
            {
                connection.Open();
                reader = command.ExecuteReader();
                
                while (reader.Read())
                {
                    result.Add(reader.GetDateTime(0));
                }
                
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                connection.Dispose();
            }
            
            return result;
            
        }
        
    }
    
}
