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
    
    public class PDF_InvoiceForm_Dk : PDF_InvoiceForm
    {
        //Implements _PDF_InvoiceForm
        
        //Private Inv As BOL.InvoiceHeader
        private OrderForm_Dk OrderForm;
        //Private OrderFormDoc As Document
        
        
        public override void Make_PDF_Invoice(int InvoiceID)
        {
            this.InvoiceID = InvoiceID;
            
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
            
            // Create a renderer for PDF that uses Unicode font encoding
            PdfDocumentRenderer pdfRenderer = new PdfDocumentRenderer(true);
            
            // Create a invoice form with the sample invoice data
            InvoiceForm_Dk InvoiceObj = new InvoiceForm_Dk(this.InvoiceID, Logo, Signatur, SignaturImage, IsCopy);
            //InvoiceObj.Logo = Logo
            //InvoiceObj.Signatur = Signatur
            //InvoiceObj.SignaturImage = SignaturImage
            //InvoiceObj.IsCopy = IsCopy
            
            // Create a MigraDoc document
            Document InvDocument = InvoiceObj.CreateDocument();
            InvDocument.UseCmykColor = true;
            
            // Set the MigraDoc document
            pdfRenderer.Document = InvDocument;
            
            // Create the PDF document
            pdfRenderer.RenderDocument();
            
            // Save the PDF document...
            pdfRenderer.Save(PDFfilename);
            
            //' If Invoice is EAN then make OrderForm / Følgeseddel
            //Inv = New InvoiceHeader(Me.InvoiceID)
            //If Inv.isLoaded AndAlso Inv.IsEAN Then IsEAN = True
            //If IsEAN Then
            //    Try
            //        If File.Exists(PDFOrderfilename) Then File.Delete(PDFOrderfilename)
            //    Catch ex As Exception
            //    End Try
            
            //    Dim pdfRendererOrder As New PdfDocumentRenderer(True)
            
            //    OrderForm = New OrderForm_Dk(Me.InvoiceID, Logo, Signatur, SignaturImage, IsCopy)
            //    OrderFormDoc = OrderForm.CreateDocument
            //    pdfRendererOrder.Document = OrderFormDoc
            //    pdfRendererOrder.RenderDocument()
            //    pdfRendererOrder.Save(PDFOrderfilename)
            //End If
            
        }
        
    }
    
}
