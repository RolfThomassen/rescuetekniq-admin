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
    
    public abstract class PurchaseOrderForm_PDF
    {
        
#region  Privates
        
        protected string _Path = "~/upload";
        protected string _PDFfilename = "PurchaseOrder";
        protected string _PDFext = ".pdf";
        protected int _PurchaseOrderID = -1;
        protected string _Logo = "logo.gif";
        protected bool _IsCopy = false;
        
        protected string _SignaturImage = "signatur.gif";
        protected string _Signatur = "Lennart Funch";
        
#endregion
        
#region  New
        
        public PurchaseOrderForm_PDF()
        {
            
        }
        public PurchaseOrderForm_PDF(int PurchaseOrderID)
        {
            _PurchaseOrderID = PurchaseOrderID;
        }
        public PurchaseOrderForm_PDF(int PurchaseOrderID, string Logo, string Signatur, string signaturImage, bool IsCopy)
        {
            this.Logo = Logo;
            this.Signatur = Signatur;
            this.SignaturImage = signaturImage;
            this.IsCopy = IsCopy;
            this._PurchaseOrderID = PurchaseOrderID;
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
                return _Path + _PDFfilename + "_" + PurchaseOrderID.ToString() + _PDFext;
            }
            set
            {
                _PDFfilename = value;
            }
        }
        
        public int PurchaseOrderID
        {
            get
            {
                return _PurchaseOrderID;
            }
            set
            {
                _PurchaseOrderID = value;
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
        
#endregion
        
        public abstract void Make_PDF_PurchaseOrder(int PurchaseOrderID);
        
    }
    
}
