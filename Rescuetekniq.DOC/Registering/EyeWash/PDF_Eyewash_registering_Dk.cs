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
        
        public class PDF_Eyewash_Registering_Dk : PDF_Eyewash_Registering
        {
            
#region  New
            
            public PDF_Eyewash_Registering_Dk()
            {
            }
            public PDF_Eyewash_Registering_Dk(int eyeID)
            {
                _eyeID = eyeID;
            }
            
#endregion
            
#region  MakePDF
            
            public override void MakePDF(int eyeID)
            {
                this.eyeID = eyeID;
                
                // Create a invoice form with the sample invoice data
                Eyewash_Registering_Dk pdfForm = new Eyewash_Registering_Dk(this.eyeID);
                pdfForm.Logo = Logo;
                
                // Create a MigraDoc document
                Document document = pdfForm.CreateDocument();
                //document.UseCmykColor = True
                
                // Create a renderer for PDF that uses Unicode font encoding
                PdfDocumentRenderer pdfRenderer = new PdfDocumentRenderer(true);
                
                // Set the MigraDoc document
                pdfRenderer.Document = document;
                
                // Create the PDF document
                pdfRenderer.RenderDocument();
                
                // Save the PDF document...
                pdfRenderer.Save(PDFfilename);
                
            }
            
#endregion
            
        }
        
    }
    
}
