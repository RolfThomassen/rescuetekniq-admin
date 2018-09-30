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

using System.Security.Principal;
using System.Data.SqlClient;



namespace RescueTekniq.BOL
{
    
    public abstract partial class BaseAdvObject : BaseObject
    {
        
        //Private Overridable _SQLDelete As String = ""
        
        public static int DeleteSQL(int ID, string _SQLDelete)
        {
            DBAccess db = new DBAccess();
            db.Parameters.Add(new SqlParameter("@ID", ID));
            int retval = db.ExecuteNonQuery(_SQLDelete); //(_d "Co2Db_TilbudHeader_Delete")
            return retval;
        }
        
    }
    
}
