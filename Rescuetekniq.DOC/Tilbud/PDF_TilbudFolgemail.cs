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
    public abstract class PDF_TilbudFolgemail
    {
        
#region  Privates
        
        protected string _Path = "~/upload";
        protected string _PDFfilename = "tilbudfolgebrev";
        protected string _PDFext = ".pdf";
        protected int _TilbudID = -1;
        protected string _Logo = "logo.gif";
        
#endregion
        
#region  New
        
        public PDF_TilbudFolgemail()
        {
        }
        public PDF_TilbudFolgemail(int tilbudID)
        {
            _TilbudID = tilbudID;
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
                return _Path + _PDFfilename + "_" + TilbudID.ToString() + _PDFext;
            }
            set
            {
                _PDFfilename = value;
            }
        }
        
        public int TilbudID
        {
            get
            {
                return _TilbudID;
            }
            set
            {
                _TilbudID = value;
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
        
#endregion
        
        public abstract void MakePDFtilbudFolgemail(int tilbudID);
        
    }
    
}
