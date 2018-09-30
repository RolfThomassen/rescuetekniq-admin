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
#region MigraDoc - Creating Documents on the Fly
    //
    // Authors:
    //   PDFsharp Team (mailto:PDFsharpSupport@pdfsharp.de)
    //
    // Copyright (c) 2001-2009 empira Software GmbH, Cologne (Germany)
    //
    // http://www.pdfsharp.com
    // http://www.migradoc.com
    // http://sourceforge.net/projects/pdfsharp
    //
    // Permission is hereby granted, free of charge, to any person obtaining a
    // copy of this software and associated documentation files (the "Software"),
    // to deal in the Software without restriction, including without limitation
    // the rights to use, copy, modify, merge, publish, distribute, sublicense,
    // and/or sell copies of the Software, and to permit persons to whom the
    // Software is furnished to do so, subject to the following conditions:
    //
    // The above copyright notice and this permission notice shall be included
    // in all copies or substantial portions of the Software.
    //
    // THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    // IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    // FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
    // THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    // LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
    // FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
    // DEALINGS IN THE SOFTWARE.
#endregion
    
    
    namespace Tilbud
    {
        
        /// <summary>
        /// This sample shows how to create a simple invoice of a fictional book store. The invoice document
        /// is created with the MigraDoc document object model and then rendered to PDF with PDFsharp.
        /// </summary>
        public class Programm
        {
            private static void Main()
            {
                try
                {
                    // Create a invoice form with the sample invoice data
                    InvoiceForm invoice = new InvoiceForm("../../invoice.xml");
                    
                    // Create a MigraDoc document
                    Document document = invoice.CreateDocument();
                    document.UseCmykColor = true;
                    
#if DEBUG
                    // for debugging only...
                    MigraDoc.DocumentObjectModel.IO.DdlWriter.WriteToFile(document, "MigraDoc.mdddl");
                    document = MigraDoc.DocumentObjectModel.IO.DdlReader.DocumentFromFile("MigraDoc.mdddl");
#endif
                    
                    // Create a renderer for PDF that uses Unicode font encoding
                    PdfDocumentRenderer pdfRenderer = new PdfDocumentRenderer(true);
                    
                    // Set the MigraDoc document
                    pdfRenderer.Document = document;
                    
                    // Create the PDF document
                    pdfRenderer.RenderDocument();
                    
                    // Save the PDF document...
                    string filename = "Invoice.pdf";
#if DEBUG
                    // I don't want to close the document constantly...
                    filename = "Invoice-" + Guid.NewGuid().ToString("N").ToUpper() +".pdf";
#endif
                    pdfRenderer.Save(filename);
                    
                    // ...and start a viewer.
                    //Process.Start(filename)
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.ReadLine();
                }
            }
        }
    }
    
}
