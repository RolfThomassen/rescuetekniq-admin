// VBConversions Note: VB project level imports
using System.Data;
using PdfSharp;
using System.Diagnostics;
using System.Xml.Linq;
using System.Globalization;
using System.Collections.Generic;
using RescueTekniq.CODE;
using Microsoft.VisualBasic;
using System.Configuration;
using System.Collections;
using RescueTekniq.BOL;
using MigraDoc;
using System;
using System.Linq;
// End of VB project level imports

using RescueTekniq.Doc;

namespace RescueTekniq.Doc
{
    
    public abstract class PDF_InvoiceForm
    {
        
#region  Privates
        
        protected string _Path = "~/upload";
        protected string _PDFfilename = "Invoice";
        protected string _PDFOrderfilename = "Folgeseddel";
        protected string _PDFext = ".pdf";
        protected int _InvoiceID = -1;
        protected string _Logo = "logo.gif";
        protected bool _IsCopy = false;
        protected bool _IsEAN = false;
        
        protected string _SignaturImage = "signatur.gif";
        protected string _Signatur = "Lennart Funch";
        
#endregion
        
#region  New
        
        public PDF_InvoiceForm()
        {
            
        }
        public PDF_InvoiceForm(int InvoiceID)
        {
            _InvoiceID = InvoiceID;
        }
        public PDF_InvoiceForm(int InvoiceID, string Logo, string Signatur, string signaturImage, bool IsCopy)
        {
            this.Logo = Logo;
            this.Signatur = Signatur;
            this.SignaturImage = signaturImage;
            this.IsCopy = IsCopy;
        }
        
#endregion
        
#region  Properties
        
        public string Path
        {
            get
            {
                return _Path;
            }
            set
            {
                _Path = value;
                _Path = _Path.Replace("/", "\\");
                if (!_Path.EndsWith("\\"))
                {
                    _Path += "\\";
                }
            }
        }
        
        public string PDFfilename
        {
            get
            {
                return _Path + _PDFfilename + "_" + InvoiceID.ToString() + _PDFext;
            }
            set
            {
                _PDFfilename = value;
            }
        }
        
        public string PDFOrderfilename
        {
            get
            {
                return _Path + _PDFOrderfilename + "_" + InvoiceID.ToString() + _PDFext;
            }
            set
            {
                _PDFfilename = value;
            }
        }
        
        public int InvoiceID
        {
            get
            {
                return _InvoiceID;
            }
            set
            {
                _InvoiceID = value;
            }
        }
        
        public string Logo
        {
            get
            {
                return _Logo;
            }
            set
            {
                _Logo = value;
            }
        }
        
        public string Signatur
        {
            get
            {
                return _Signatur;
            }
            set
            {
                _Signatur = value;
            }
        }
        public string SignaturImage
        {
            get
            {
                return _SignaturImage;
            }
            set
            {
                _SignaturImage = value;
            }
        }
        
        public bool IsCopy
        {
            get
            {
                return _IsCopy;
            }
            set
            {
                _IsCopy = value;
            }
        }
        
        public bool IsEAN
        {
            get
            {
                return _IsEAN;
            }
            set
            {
                _IsEAN = value;
            }
        }
        
#endregion
        
        public abstract void Make_PDF_Invoice(int InvoiceID);
        
    }
    
}
