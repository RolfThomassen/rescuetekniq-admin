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

using System.Net.Mail;
using System.Text;




namespace RescueTekniq.BOL
{
    namespace EMAIL
    {
        
        public class SendEmail
        {
            //Implements System.IDisposable
            
            
#region  Privates
            
            private string SmtpURL = "";
            private string SmtpAcount = "";
            private string SmtpCode = "";
            
            private bool _SendToBCC = false;
            private MailAddress _BCC; // VBConversions Note: Initial value cannot be assigned here since it is non-static.  Assignment has been moved to the class constructors. //("admin@RescueTekniq.dk", "admin") '
            private MailAddress _Fra = new MailAddress("admin@RescueTekniq.dk", "admin"); //(GlobalConst.AppSettings("email.info", "admin@RescueTekniq.dk"), "admin")
            private MailAddress _Til; //= New MailAddress()
            private string _Subject = "";
            private string _Body = "";
            private List<string> _FileList = new List<string>();
            
            private MailPriority _Priority = MailPriority.Normal;
            private Encoding _BodyEncoding; // VBConversions Note: Initial value cannot be assigned here since it is non-static.  Assignment has been moved to the class constructors.
            private bool _IsBodyHtml = true;
            
            private DeliveryNotificationOptions _DeliveryNotificationOptions = (int) System.Net.Mail.DeliveryNotificationOptions.OnFailure + System.Net.Mail.DeliveryNotificationOptions.OnSuccess;
            
#endregion
            
#region  New
            
            public SendEmail()
            {
                // VBConversions Note: Non-static class variable initialization is below.  Class variables cannot be initially assigned non-static values in C#.
                _BCC = new MailAddress(GlobalConst.get_AppSettings("email.info", "admin@RescueTekniq.dk"), "admin");
                _BodyEncoding = Encoding.Default;
                
                SmtpURL = ConfigurationManager.AppSettings["smtp.server"];
                SmtpAcount = ConfigurationManager.AppSettings["smtp.acount"];
                SmtpCode = ConfigurationManager.AppSettings["smtp.code"];
                DeliveryNotificationOptions = (int) DeliveryNotificationOptions.OnFailure + DeliveryNotificationOptions.OnSuccess;
                
                //"massmail.scannet.dk"
                //ConfigurationManager.AppSettings("adminrolename")
            }
            
#endregion
            
#region  Propterties
            
            public List<string> FileList
            {
                get
                {
                    return _FileList;
                }
            }
            
            public string Subject
            {
                get
                {
                    return _Subject;
                }
                set
                {
                    _Subject = value;
                }
            }
            
            public string Body
            {
                get
                {
                    return _Body;
                }
                set
                {
                    _Body = value;
                }
            }
            
            public bool IsBodyHtml
            {
                get
                {
                    return _IsBodyHtml;
                }
                set
                {
                    _IsBodyHtml = value;
                }
            }
            
            public MailPriority Priority
            {
                get
                {
                    return _Priority;
                }
                set
                {
                    _Priority = value;
                }
            }
            
            public Encoding BodyEncoding
            {
                get
                {
                    return _BodyEncoding;
                }
                set
                {
                    _BodyEncoding = value;
                }
            }
            
            public MailAddress From
            {
                get
                {
                    return _Fra;
                }
                set
                {
                    _Fra = value;
                }
            }
            public MailAddress Fra
            {
                get
                {
                    return _Fra;
                }
                set
                {
                    _Fra = value;
                }
            }
            
            public MailAddress To
            {
                get
                {
                    return _Til;
                }
                set
                {
                    _Til = value;
                }
            }
            public MailAddress Til
            {
                get
                {
                    return _Til;
                }
                set
                {
                    _Til = value;
                }
            }
            
            public bool SendToBCC
            {
                get
                {
                    return _SendToBCC;
                }
                set
                {
                    _SendToBCC = value;
                }
            }
            
            public MailAddress BCC
            {
                get
                {
                    return _BCC;
                }
                set
                {
                    _BCC = value;
                }
            }
            
            public DeliveryNotificationOptions DeliveryNotificationOptions
            {
                get
                {
                    return _DeliveryNotificationOptions;
                }
                set
                {
                    _DeliveryNotificationOptions = value;
                }
            }
            
#endregion
            
#region  Metodes
            
            public void SendEmailMessage(string strFrom, string strTo, string strSubject)
	            {
                string[] _to = new string[1];
                _to[0] = strTo;
                string[] _files = new string[1];
                _files[0] = "";
                
                SendEmailMessage(strFrom, _to, strSubject, "", _files);
            }
            
            public void SendEmailMessage(string strFrom, string strTo, string strSubject, string strMessage)
	            {
                string[] _to = new string[1];
                _to[0] = strTo;
                string[] _files = new string[1];
                _files[0] = "";
                
                SendEmailMessage(strFrom, _to, strSubject, strMessage, _files);
            }
            
            public void SendEmailMessage(string strFrom, string strTo, string strSubject, string strMessage, string file)
	            {
                string[] _to = new string[1];
                _to[0] = strTo;
                string[] _files = new string[1];
                _files[0] = file;
                
                SendEmailMessage(strFrom, _to, strSubject, strMessage, _files);
            }
            
            public void SendEmailMessage(string strFrom, string[] strTo, string strSubject, string strMessage, string file)
	            {
                //Dim _to(0) As String : _to(0) = strTo
                string[] _files = new string[1];
                _files[0] = file;
                
                SendEmailMessage(strFrom, strTo, strSubject, strMessage, _files);
            }
            
            public void SendEmailMessage(string strFrom, string strTo, string strSubject, string strMessage, string[] fileList)
	            {
                string[] _to = new string[1];
                _to[0] = strTo;
                //Dim _files(0) As String : _files(0) = file
                
                SendEmailMessage(strFrom, _to, strSubject, strMessage, fileList);
            }
            
            public void SendEmailMessage(string strFrom, string[] strTo, string strSubject, string strMessage, string[] fileList)
	            {
                try
                {
                    foreach (string item in strTo)
                    {
                        //For each to address create a mail message
                        MailMessage MailMsg = new MailMessage(); //New MailAddress(strFrom.Trim()), New MailAddress(item)
                        
                        MailMsg.From = new MailAddress(strFrom.Trim());
                        MailMsg.To.Add(new MailAddress(item));
                        if (SendToBCC)
                        {
                            MailMsg.Bcc.Add(BCC);
                        }
                        
                        //MailMsg.ReplyTo = New MailAddress("admin@RescueTekniq.dk") '(GlobalConst.AppSettings("email.info", "admin@RescueTekniq.dk"))
                        
                        MailMsg.BodyEncoding = BodyEncoding; //Encoding.Default
                        MailMsg.Subject = strSubject.Trim();
                        MailMsg.Body = strMessage.Trim();
                        MailMsg.Priority = Priority;
                        MailMsg.IsBodyHtml = true;
                        MailMsg.DeliveryNotificationOptions = this.DeliveryNotificationOptions; //DeliveryNotificationOptions.OnFailure + DeliveryNotificationOptions.OnSuccess
                        if (item.IndexOf("test@") >= 0)
                        {
                            MailMsg.DeliveryNotificationOptions = System.Net.Mail.DeliveryNotificationOptions.None;
                        }
                        
                        //attach each file attachment
                        foreach (string strfile in fileList)
                        {
                            if (!(string.IsNullOrEmpty(strfile)))
                            {
                                if (System.IO.File.Exists(strfile))
                                {
                                    Attachment MsgAttach = new Attachment(strfile);
                                    MailMsg.Attachments.Add(MsgAttach);
                                }
                            }
                        }
                        
                        //Smtpclient to send the mail message
                        SmtpClient SmtpMail = new SmtpClient();
                        SmtpMail.Host = SmtpURL;
                        SmtpMail.Send(MailMsg);
                        
                        Log.AddLog(status: "SendEmail", logtext: string.Format("To: {0}, Subject: {1}", item, strSubject), Metode: "SendEmailMessage");
                    }
                    //Message Successful
                }
                catch (Exception)
                {
                    //Message Error
                }
            }
            
            public void SendEmailMessageAsOne(string strFrom, string[] strTo, string strSubject, string strMessage, string[] fileList)
	            {
                try
                {
                    MailMessage MailMsg = new MailMessage();
                    MailMsg.From = new MailAddress(strFrom.Trim());
                    if (SendToBCC)
                    {
                        MailMsg.Bcc.Add(BCC);
                    }
                    
                    MailMsg.DeliveryNotificationOptions = this.DeliveryNotificationOptions; //DeliveryNotificationOptions.OnFailure + DeliveryNotificationOptions.OnSuccess
                    //create ONE email message with all TO-address
                    foreach (string item in strTo)
                    {
                        MailMsg.To.Add(new MailAddress(item));
                        if (System.Convert.ToBoolean(item.IndexOf("test@")))
                        {
                            MailMsg.DeliveryNotificationOptions = System.Net.Mail.DeliveryNotificationOptions.None;
                        }
                    }
                    
                    MailMsg.BodyEncoding = this.BodyEncoding;
                    MailMsg.Subject = strSubject.Trim();
                    MailMsg.Body = strMessage.Trim();
                    MailMsg.Priority = this.Priority;
                    MailMsg.IsBodyHtml = true;
                    
                    //attach each file attachment
                    foreach (string strfile in fileList)
                    {
                        if (!(strfile.Trim() == ""))
                        {
                            if (System.IO.File.Exists(strfile))
                            {
                                Attachment MsgAttach = new Attachment(strfile.Trim());
                                MailMsg.Attachments.Add(MsgAttach);
                            }
                        }
                    }
                    
                    //Smtpclient to send the mail message
                    SmtpClient SmtpMail = new SmtpClient();
                    SmtpMail.Host = SmtpURL;
                    SmtpMail.Send(MailMsg);
                    
                    Log.AddLog(status: "SendEmail", logtext: string.Format("Multible To: {0}, Subject: {1}", strTo.ToString(), strSubject), Metode: "SendEmailMessageAsOne");
                    
                    //Message Successful
                }
                catch (Exception)
                {
                    //Message Error
                }
            }
            
            public void SendEmail_Renamed()
            {
                //Try
                
                MailMessage MailMsg = new MailMessage();
                MailMsg.From = this.Fra;
                MailMsg.To.Add(this.Til);
                if (SendToBCC)
                {
                    MailMsg.Bcc.Add(this.BCC);
                }
                
                MailMsg.BodyEncoding = this.BodyEncoding;
                MailMsg.Subject = this.Subject;
                MailMsg.Body = this.Body;
                MailMsg.Priority = this.Priority;
                MailMsg.IsBodyHtml = this.IsBodyHtml; //True
                MailMsg.DeliveryNotificationOptions = this.DeliveryNotificationOptions;
                if (this.Til.Address.ToString().IndexOf("test@") > 0)
                {
                    MailMsg.DeliveryNotificationOptions = System.Net.Mail.DeliveryNotificationOptions.None;
                }
                
                // attach each file attachment
                foreach (string strfile in this.FileList)
                {
                    if (!(strfile.Trim() == ""))
                    {
                        if (System.IO.File.Exists(strfile))
                        {
                            Attachment MsgAttach = new Attachment(strfile.Trim());
                            MailMsg.Attachments.Add(MsgAttach);
                        }
                    }
                }
                
                
                
                
                
                
                
                
                
                
                
                //Smtpclient to send the mail message
                SmtpClient SmtpMail = new SmtpClient();
                //SmtpMail.Host = SmtpURL
                SmtpMail.Send(MailMsg);
                
                Log.AddLog(status: "SendEmail", logtext: string.Format("Send - To: {0}, Subject: {1}", Til.ToString(), Subject), Metode: "SendEmail");
                
                //Message Successful
                //Catch ex As Exception
                //Message Error
                //End Try
                
            }
            
#endregion
            
        }
        
    }
    
}
