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
        
        public class PDF_FAB_Registering_Dk : PDF_FAB_Registering
        {
            
#region  New
            
            public PDF_FAB_Registering_Dk()
            {
            }
            public PDF_FAB_Registering_Dk(int fabID)
            {
                _fabID = fabID;
            }
            
#endregion
            
#region  MakePDF
            
            public override void MakePDF(int fabID)
            {
                this.fabID = fabID;
                
                // Create a invoice form with the sample invoice data
                FAB_Registering_Dk pdfForm = new FAB_Registering_Dk(this.fabID);
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
