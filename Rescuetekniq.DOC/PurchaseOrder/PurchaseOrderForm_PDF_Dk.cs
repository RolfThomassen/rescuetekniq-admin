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
    
    public class PurchaseOrderForm_PDF_Dk : PurchaseOrderForm_PDF
    {
        
        public override void Make_PDF_PurchaseOrder(int InvoiceID)
        {
            this.PurchaseOrderID = PurchaseOrderID;
            
            //Try
            if (File.Exists(PDFfilename))
            {
                File.Delete(PDFfilename);
            }
            //Catch ex As Exception
            //End Try
            
            //Try
            // Create a PurchaseOrder form with the sample PurchaseOrder data
            PurchaseOrderForm_Dk PDF_Obj = new PurchaseOrderForm_Dk(this.PurchaseOrderID, Logo, Signatur, SignaturImage, IsCopy);
            //InvoiceObj.Logo = Logo
            //InvoiceObj.Signatur = Signatur
            //InvoiceObj.SignaturImage = SignaturImage
            //InvoiceObj.IsCopy = IsCopy
            
            // Create a MigraDoc document
            Document document = PDF_Obj.CreateDocument();
            document.UseCmykColor = true;
            
            // Create a renderer for PDF that uses Unicode font encoding
            PdfDocumentRenderer pdfRenderer = new PdfDocumentRenderer(true);
            
            // Set the MigraDoc document
            pdfRenderer.Document = document;
            
            // Create the PDF document
            pdfRenderer.RenderDocument();
            
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
