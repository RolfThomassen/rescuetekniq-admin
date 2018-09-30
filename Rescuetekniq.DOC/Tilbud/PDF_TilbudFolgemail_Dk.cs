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
using System.IO;
using RescueTekniq.Doc;

namespace RescueTekniq.Doc
{
    
    public class PDF_TilbudFolgemail_Dk : PDF_TilbudFolgemail
    {
        
        public override void MakePDFtilbudFolgemail(int tilbudID)
        {
            this.TilbudID = tilbudID;
            //Try
            // Create a invoice form with the sample invoice data
            TilbudFolgemail_Dk folgebrev = new TilbudFolgemail_Dk(this.TilbudID);
            folgebrev.Logo = Logo;
            
            // Create a MigraDoc document
            Document document = folgebrev.CreateDocument();
            document.UseCmykColor = true;
            
            // Create a renderer for PDF that uses Unicode font encoding
            PdfDocumentRenderer pdfRenderer = new PdfDocumentRenderer(true);
            
            // Set the MigraDoc document
            pdfRenderer.Document = document;
            
            // Create the PDF document
            pdfRenderer.RenderDocument();
            
            try
            {
                if (File.Exists(PDFfilename))
                {
                    File.Delete(PDFfilename);
                }
            }
            catch (Exception)
            {
            }
            
            // Save the PDF document...
            pdfRenderer.Save(PDFfilename);
            
            // ...and start a viewer.
            //Process.Start(filename)
            //Catch ex As Exception
            //Console.WriteLine(ex.Message)
            //Console.ReadLine()
            // End Try
            
        }
        
        
    }
    
}
