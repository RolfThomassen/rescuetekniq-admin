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
    
    public class KursusListe : BaseObject
    {
        
#region  New
        
        public KursusListe()
        {
            
            //_ParentID = 0
            //_KursusDagNr = ""
            //_KursusDagTekst = ""
        }
        
#endregion
        
#region  Public Properties
        
#region  KursusList Status
        
        public KursusStatusEnum Status
        {
            get
            {
                return _Status;
            }
            set
            {
                _Status = value;
            }
        }
        private KursusStatusEnum _Status = KursusStatusEnum.Initialize;
        
#endregion
        
#region  KursusDeltager
        
        public RescueTekniq.BOL.KursusDeltager KursusDeltager
        {
            get
            {
                if (_KursusDeltagerID > 0)
                {
                    if (ReferenceEquals(_kursusDeltager, null))
                    {
                        _kursusDeltager = new KursusDeltager();
                    }
                    if (!_kursusDeltager.loaded)
                    {
                        _kursusDeltager = KursusDeltager.GetKursusDeltager(_KursusDeltagerID);
                    }
                    else if (_kursusDeltager.ID != _KursusDeltagerID)
                    {
                        _kursusDeltager = KursusDeltager.GetKursusDeltager(_KursusDeltagerID);
                    }
                }
                return _kursusDeltager;
            }
        }
        private KursusDeltager _kursusDeltager;
        
        public int KursusDeltagerID
        {
            get
            {
                return _KursusDeltagerID;
            }
            set
            {
                _KursusDeltagerID = value;
            }
        }
        private int _KursusDeltagerID = -1;
        
        public string KursusDeltager_Adresse
        {
            get
            {
                return KursusDeltager.Adresse;
            }
        }
        public DateTime KursusDeltager_Birthday
        {
            get
            {
                return KursusDeltager.Birthday;
            }
        }
        public string KursusDeltager_Bynavn
        {
            get
            {
                return KursusDeltager.Bynavn;
            }
        }
        public string KursusDeltager_Cprnr
        {
            get
            {
                return KursusDeltager.Cprnr;
            }
        }
        public string KursusDeltager_Efternavn
        {
            get
            {
                return KursusDeltager.Efternavn;
            }
        }
        public string KursusDeltager_Fornavn
        {
            get
            {
                return KursusDeltager.Fornavn;
            }
        }
        public DateTime KursusDeltager_FratradtDato
        {
            get
            {
                return KursusDeltager.FratradtDato;
            }
        }
        public string KursusDeltager_FuldAdress
        {
            get
            {
                return KursusDeltager.FuldAdresse;
            }
        }
        public string KursusDeltager_Land
        {
            get
            {
                return KursusDeltager.Land;
            }
        }
        public int KursusDeltager_LandekodeID
        {
            get
            {
                return KursusDeltager.LandekodeID;
            }
        }
        public string KursusDeltager_Location
        {
            get
            {
                return KursusDeltager.Lokation;
            }
        }
        public string KursusDeltager_MedArbNr
        {
            get
            {
                return KursusDeltager.MedArbNr;
            }
        }
        public string KursusDeltager_Postnr
        {
            get
            {
                return KursusDeltager.Postnr;
            }
        }
        public string KursusDeltager_PostnrBy
        {
            get
            {
                return KursusDeltager.PostnrBy;
            }
        }
        public string KursusDeltager_State
        {
            get
            {
                return KursusDeltager.State;
            }
        }
        public KursusKursistStatusEnum KursusDeltager_Status
        {
            get
            {
                return KursusDeltager.Status;
            }
        }
        public string KursusDeltager_Stilling
        {
            get
            {
                return KursusDeltager.Stilling;
            }
        }
        
#endregion
        
#region  Kursus Dag
        
        public int KursusDagID
        {
            get
            {
                return _KursusDagID;
            }
            set
            {
                _KursusDagID = value;
            }
        }
        private int _KursusDagID = -1;
        
        public RescueTekniq.BOL.KursusDag KursusDag
        {
            get
            {
                if (_KursusDagID > 0)
                {
                    if (ReferenceEquals(_kursusDag, null))
                    {
                        _kursusDag = new KursusDag();
                    }
                    if (!_kursusDag.loaded)
                    {
                        _kursusDag = KursusDag.GetKursusDag(_KursusDagID);
                    }
                    else if (_kursusDag.ID != _KursusDagID)
                    {
                        _kursusDag = KursusDag.GetKursusDag(_KursusDagID);
                    }
                }
                return _kursusDag;
            }
        }
        private KursusDag _kursusDag;
        
        public DateTime KursusDag_KursusDato
        {
            get
            {
                return KursusDag.KursusDato;
            }
        }
        
        public bool KursusDag_Repetionskursus
        {
            get
            {
                return KursusDag.Repetionskursus;
            }
        }
        
        public KursusStatusEnum KursusDag_Status
        {
            get
            {
                return KursusDag.Status;
            }
        }
        
#endregion
        
#region  Kursus Policy
        
        public int KursusPolicy_FirstAidAdminAftale
        {
            get
            {
                return KursusDag.Kursus.FirstAidAdminAftale;
            }
        }
        public string KursusPolicy_FirstAidAdminAftaleText
        {
            get
            {
                return KursusDag.Kursus.FirstAidAdminAftaleText;
            }
        }
        public int KursusPolicy_FirstAidAftale
        {
            get
            {
                return KursusDag.Kursus.FirstAidAftale;
            }
        }
        public string KursusPolicy_FirstAidAftaleText
        {
            get
            {
                return KursusDag.Kursus.FirstAidAftaleText;
            }
        }
        public int KursusPolicy_FirstAidKursusType
        {
            get
            {
                return KursusDag.Kursus.FirstAidKursusType;
            }
        }
        public string KursusPolicy_FirstAidKursusTypeText
        {
            get
            {
                return KursusDag.Kursus.FirstAidKursusTypeText;
            }
        }
        public int KursusPolicy_FirstAidLeverandor
        {
            get
            {
                return KursusDag.Kursus.FirstAidLeverandor;
            }
        }
        public string KursusPolicy_FirstAidLeverandorText
        {
            get
            {
                return KursusDag.Kursus.FirstAidLeverandorText;
            }
        }
        public string KursusPolicy_FirstAidMedarbInfo
        {
            get
            {
                return KursusDag.Kursus.FirstAidMedarbInfo;
            }
        }
        public int KursusPolicy_FirstAidRepetition
        {
            get
            {
                return KursusDag.Kursus.FirstAidRepetition;
            }
        }
        public string KursusPolicy_FirstAidRepetitionText
        {
            get
            {
                return KursusDag.Kursus.FirstAidRepetitionText;
            }
        }
        public string KursusPolicy_FirstAidTitle
        {
            get
            {
                return KursusDag.Kursus.FirstAidTitle;
            }
        }
        public KursusStatusEnum KursusPolicy_Status
        {
            get
            {
                return KursusDag.Kursus.Status;
            }
        }
        
#endregion
        
#endregion
        
#region  Shared Populate
        
        private static void AddParms(ref DBAccess db, KursusListe c)
        {
            var with_1 = c;
            db.AddInt("KursusDagID", with_1.KursusDagID);
            db.AddInt("KursusDeltagerID", with_1.KursusDeltagerID);
            db.AddInt("Status", (System.Int32) with_1.Status);
            AddParmsStandard(db, c);
        }
        
        private static void Populate(SqlDataReader dr, KursusListe c)
        {
            var with_1 = c;
            with_1.KursusDagID = System.Convert.ToInt32(dr.DBtoInt("KursusDagID"));
            with_1.KursusDeltagerID = System.Convert.ToInt32(dr.DBtoInt("KursusDeltagerID"));
            with_1.Status = (RescueTekniq.BOL.KursusStatusEnum) (dr.DBtoInt("Status"));
            PopulateStandard(dr, c);
        }
        
#endregion
        
#region  Stored Procedure Names
        
        private const string _SQLDelete = "Co2Db_KursusListe_Delete";
        private const string _SQLInsert = "Co2Db_KursusListe_Insert";
        private const string _SQLUpdate = "Co2Db_KursusListe_Update";
        private const string _SQLSelectAll = "Co2Db_KursusListe_SelectAll";
        private const string _SQLSelectAllList = "Co2Db_KursusListe_SelectAllList";
        private const string _SQLSelectByParent = "Co2Db_KursusListe_SelectAllByParent";
        private const string _SQLSelectID = "Co2Db_KursusListe_SelectID";
        private const string _SQLSelectOne = "Co2Db_KursusListe_SelectOne";
        private const string _SQLSelectBySearch = "Co2Db_KursusListe_SelectBySearch";
        
        private const string _SQLSelectByDate = "Co2Db_KursusListe_SelectByDate";
        
        private const string _SQLSelectByKursusDag = "Co2Db_KursusList_SelectByKursusDag";
        private const string _SQLSelectByKursusDeltager = "Co2Db_KursusList_SelectByKursusDeltager";
        
#endregion
        
#region  Get Data
        
        public static System.Collections.Generic.List<KursusListe> GetKursusListByKursusDeltager(int KursusDeltagerID)
        {
            System.Collections.Generic.List<KursusListe> result = new System.Collections.Generic.List<KursusListe>();
            int ID = -1;
            DBAccess db = new DBAccess();
            db.AddInt("KursusDeltagerID", KursusDeltagerID);
            
            SqlDataReader dr = default(SqlDataReader);
            try
            {
                dr = (System.Data.SqlClient.SqlDataReader) (db.ExecuteReader(_SQLSelectByKursusDeltager));
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        KursusListe kd = new KursusListe();
                        kd.KursusDeltagerID = System.Convert.ToInt32(dr.DBtoInt("KursusDeltagerID"));
                        kd.KursusDagID = System.Convert.ToInt32(dr.DBtoInt("KursusDagID"));
                        kd.Status = (RescueTekniq.BOL.KursusStatusEnum) (dr.DBtoInt("KursusListeStatus"));
                        result.Add(kd);
                    }
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            
            return result;
        }
        
        public static System.Collections.Generic.List<KursusListe> GetKursusListByKursusDag(int KursusDagID)
        {
            System.Collections.Generic.List<KursusListe> result = new System.Collections.Generic.List<KursusListe>();
            int ID = -1;
            DBAccess db = new DBAccess();
            db.AddInt("KursusDagID", KursusDagID);
            
            SqlDataReader dr = default(SqlDataReader);
            try
            {
                dr = (System.Data.SqlClient.SqlDataReader) (db.ExecuteReader(_SQLSelectByKursusDag));
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        KursusListe kd = new KursusListe();
                        kd.KursusDeltagerID = System.Convert.ToInt32(dr.DBtoInt("KursusDeltagerID"));
                        kd.KursusDagID = System.Convert.ToInt32(dr.DBtoInt("KursusDagID"));
                        kd.Status = (RescueTekniq.BOL.KursusStatusEnum) (dr.DBtoInt("KursusListeStatus"));
                        result.Add(kd);
                    }
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            
            return result;
        }
        
#endregion
        
        //INSERT INTO [vicjos1_Heart2Start].[vicjos1_sysadm].[Co2Db_KursusListe]
        //           ([KursusDagID]
        //           ,[KursusDeltagerID]
        //           ,[KursusListeStatus])
        //VALUES
        //           (<KursusDagID, int,>
        //           ,<KursusDeltagerID, int,>
        //           ,<KursusListeStatus, int,>)
        
        //DELETE FROM [vicjos1_Heart2Start].[vicjos1_sysadm].[Co2Db_KursusListe]
        //      WHERE <Search Conditions,,>
        
        //UPDATE [vicjos1_Heart2Start].[vicjos1_sysadm].[Co2Db_KursusListe]
        //   SET [KursusDagID] = <KursusDagID, int,>
        //      ,[KursusDeltagerID] = <KursusDeltagerID, int,>
        //      ,[KursusListeStatus] = <KursusListeStatus, int,>
        // WHERE <Search Conditions,,>
        
        
        
        
        
    }
    
}
