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
    
    public class PDF_IncassoForm_Dk : PDF_InvoiceForm
    {
        //Implements _PDF_InvoiceForm
        
        public override void Make_PDF_Invoice(int InvoiceID)
        {
            this.InvoiceID = InvoiceID;
            this._PDFfilename = "Incasso";
            //Me.PDFfilename = Me.PDFfilename.Replace("Invoice", "Incasso")
            
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
            
            // Create a invoice form with the sample invoice data
            IncassoForm_Dk IncassoObj = new IncassoForm_Dk(this.InvoiceID, Logo, Signatur, SignaturImage, IsCopy);
            //InvoiceObj.Logo = Logo
            //InvoiceObj.Signatur = Signatur
            //InvoiceObj.SignaturImage = SignaturImage
            //InvoiceObj.IsCopy = IsCopy
            
            // Create a MigraDoc document
            Document document = IncassoObj.CreateDocument();
            document.UseCmykColor = true;
            
            // Create a renderer for PDF that uses Unicode font encoding
            PdfDocumentRenderer pdfRenderer = new PdfDocumentRenderer(true);
            
            // Set the MigraDoc document
            pdfRenderer.Document = document;
            
            // Create the PDF document
            pdfRenderer.RenderDocument();
            
            // Save the PDF document...
            pdfRenderer.Save(PDFfilename);
            
        }
        
        
    }
    
}
