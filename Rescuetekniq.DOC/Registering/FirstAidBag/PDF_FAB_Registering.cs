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
    namespace FirstAidBag //registering
    {
        
        public abstract class PDF_FAB_Registering
        {
            
#region  Privates
            
            protected string _Path = "~/upload";
            protected string _PDFfilename = "FABRegistering";
            protected string _PDFext = ".pdf";
            protected int _fabID = -1;
            protected string _Logo = "logo.gif";
            
#endregion
            
#region  New
            
            public PDF_FAB_Registering()
            {
            }
            public PDF_FAB_Registering(int fabID)
            {
                _fabID = fabID;
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
                    return _Path + _PDFfilename + "_" + fabID.ToString() + _PDFext;
                }
                set
                {
                    _PDFfilename = value;
                }
            }
            
            public int fabID
            {
                get
                {
                    return _fabID;
                }
                set
                {
                    _fabID = value;
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
                MakePDF(fabID);
            }
            public abstract void MakePDF(int fabID);
            
#endregion
            
        }
        
    }
    
}
