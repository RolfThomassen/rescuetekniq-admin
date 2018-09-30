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

using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using RescueTekniq.Doc;


namespace RescueTekniq.Doc
{
    namespace Eyewash //registering
    {
        
        public abstract class PDF_Eyewash_Registering
        {
            
#region  Privates
            
            protected string _Path = "~/upload";
            protected string _PDFfilename = "EyewashRegistering";
            protected string _PDFext = ".pdf";
            protected int _eyeID = -1;
            protected string _Logo = "logo.gif";
            
#endregion
            
#region  New
            
            public PDF_Eyewash_Registering()
            {
            }
            public PDF_Eyewash_Registering(int eyeID)
            {
                _eyeID = eyeID;
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
                    return _Path + _PDFfilename + "_" + eyeID.ToString() + _PDFext;
                }
                set
                {
                    _PDFfilename = value;
                }
            }
            
            public int eyeID
            {
                get
                {
                    return _eyeID;
                }
                set
                {
                    _eyeID = value;
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
            
#region  MakePDF
            
            public void MakePDF()
            {
                MakePDF(eyeID);
            }
            public abstract void MakePDF(int eyeID);
            
#endregion
            
        }
        
    }
    
}
