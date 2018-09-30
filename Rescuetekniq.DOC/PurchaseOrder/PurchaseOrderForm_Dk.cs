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

using System.Xml;
using System.Xml.XPath;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.DocumentObjectModel.Shapes;
using MigraDoc.Rendering;
using MigraDoc.RtfRendering;
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
    
    /// <summary>
    /// Creates the PurchaseOrder følgemail form.
    /// </summary>
    public class PurchaseOrderForm_Dk
    {
        
#region  Privates
        
        /// <summary>
        /// The MigraDoc document that represents the invoice.
        /// </summary>
        private Document document;
        
        /// <summary>
        /// An XML invoice based on a sample created with Microsoft InfoPath.
        /// </summary>
        readonly XmlDocument invoiceXML;
        
        /// <summary>
        /// The root navigator for the XML document.
        /// </summary>
        readonly XPathNavigator navigator;
        
        /// <summary>
        /// The text frame of the MigraDoc document that contains the address.
        /// </summary>
        private TextFrame addressFrame;
        private TextFrame deleveryFrame;
        private TextFrame vendorFrame;
        
        /// <summary>
        /// The text frame of the MigraDoc document that contains the header (top/right).
        /// </summary>
        private TextFrame headerFrame;
        
        /// <summary>
        /// The table of the MigraDoc document that contains the PurchaseOrder items.
        /// </summary>
        private Table table;
        
        /// <summary>
        /// Class contains all info about PurchaseOrder og PurchaseOrderItems
        /// </summary>
        private RescueTekniq.BOL.PurchaseOrder _PurchaseOrder = new RescueTekniq.BOL.PurchaseOrder();
        
        /// <summary>
        /// total sum Price
        /// </summary>
        private double totalExtendedPrice = 0;
        
        /// <summary>
        /// PurchaseOrder's ID
        /// </summary>
        /// <remarks></remarks>
        private int _PurchaseOrderID = -1;
        
        /// <summary>
        /// Path for logo image file
        /// </summary>
        private string _Logo = "logo.gif";
        
        /// <summary>
        /// Path for Signatur image file
        /// </summary>
        private string _SignaturImage = "Signatur.gif";
        /// <summary>
        /// Path for Signatur
        /// </summary>
        private string _Signatur = "Lennart Funch";
        
        /// <summary>
        /// Is this PurchaseOrder Copy of original
        /// </summary>
        /// <remarks></remarks>
        private bool _IsCopy = false;
        
#endregion
        
#region  Shared Const
        
        // Some pre-defined colors
#if true
        // RGB colors
        static readonly Color TableBorder = new Color((byte) 81, (byte) 125, (byte) 192);
        static readonly Color TableBlue = new Color((byte) 235, (byte) 240, (byte) 249);
        static readonly Color TableGray = new Color((byte) 242, (byte) 242, (byte) 242);
#else
        // CMYK colors
        static readonly Color tableBorder = Color.FromCmyk(100, 50, 0, 30);
        static readonly Color tableBlue = Color.FromCmyk(0, 80, 50, 30);
        static readonly Color tableGray = Color.FromCmyk(30, 0, 0, 0, 100);
#endif
        
#endregion
        
#region  New
        
        // ''' <summary>
        // ''' Initializes a new instance of the class BillFrom and opens the specified XML document.
        // ''' </summary>
        //Public Sub New(ByVal filename As String)
        //    Me.invoice = New XmlDocument()
        //    Me.invoice.Load(filename)
        //    Me.navigator = Me.invoice.CreateNavigator()
        //End Sub
        
        /// <summary>
        /// Initializes a new instance of the class InvoiceFolgemail and opens the specified Invoice from InvoiceID.
        /// </summary>
        public PurchaseOrderForm_Dk(int PurchaseOrderID)
        {
            this.PurchaseOrderID = PurchaseOrderID;
        }
        public PurchaseOrderForm_Dk()
        {
            
        }
        public PurchaseOrderForm_Dk(int PurchaseOrderID, string Logo, string Signatur, string signaturImage, bool IsCopy)
        {
            this.PurchaseOrderID = PurchaseOrderID;
            this.Logo = Logo;
            this.Signatur = Signatur;
            this.SignaturImage = signaturImage;
            this.IsCopy = IsCopy;
        }
        
#endregion
        
#region  Properties
        
        public int PurchaseOrderID
        {
            get
            {
                return _PurchaseOrderID;
            }
            set
            {
                _PurchaseOrderID = value;
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
        
        public string Signatur
        {
            get
            {
                return _Signatur;
            }
            set
            {
                _Signatur = value;
            }
        }
        
        public string SignaturImage
        {
            get
            {
                return _SignaturImage;
            }
            set
            {
                _SignaturImage = value;
            }
        }
        
        public bool IsCopy
        {
            get
            {
                return _IsCopy;
            }
            set
            {
                _IsCopy = value;
            }
        }
        
        public RescueTekniq.BOL.PurchaseOrder PurchaseOrder
        {
            get
            {
                if (_PurchaseOrderID > 0)
                {
                    if (!_PurchaseOrder.loaded)
                    {
                        _PurchaseOrder = RescueTekniq.BOL.PurchaseOrder.GetPurchaseOrder(_PurchaseOrderID);
                    }
                    else if (_PurchaseOrder.ID != _PurchaseOrderID)
                    {
                        _PurchaseOrder = RescueTekniq.BOL.PurchaseOrder.GetPurchaseOrder(_PurchaseOrderID);
                    }
                }
                return _PurchaseOrder;
            }
            protected set
            {
                _PurchaseOrder = value;
            }
        }
        
#endregion
        
#region  Metodes
        
        //Public Sub LoadInvoice(ByVal InvoiceID As Integer)
        //    Me.Invoice = RescueTekniq.BOL.InvoiceHeader.GetInvoice(InvoiceID)
        //End Sub
        
        private string EANtitle
        {
            get
            {
                string _EANtitle = "";
                //TODO EAN ?
                //If PurchaseOrder.EANInvoice Then _EANtitle = "EAN "
                return _EANtitle;
            }
        }
        
#endregion
        
#region  Create Document
        
        
        /// <summary>
        /// Creates the invoice document.
        /// </summary>
        public Document CreateDocument()
        {
            if (!PurchaseOrder.loaded)
            {
                //RaiseEvent InvoiceNotExist()
                //LoadInvoice(InvoiceID)
            }
            
            // Create a new MigraDoc document
            this.document = new Document();
            var with_1 = this.document.Info;
            with_1.Title = "PurchaseOrder nr. " + System.Convert.ToString(PurchaseOrderID);
            with_1.Subject = "PurchaseOrder from " + Config.Company.name;
            with_1.Author = Config.Company.name;
            //.Comment = ""
            //.Keywords = ""
            
            DefineStyles();
            
            CreatePage();
            
            FillContent();
            
            return this.document;
        }
        
#endregion
        
#region  Define Styles
        
        /// <summary>
        /// Defines the styles used to format the MigraDoc document.
        /// </summary>
        private void DefineStyles()
        {
            // Get the predefined style Normal.
            Style style = this.document.Styles["Normal"];
            // Because all styles are derived from Normal, the next line changes the
            // font of the whole document. Or, more exactly, it changes the font of
            // all styles and paragraphs that do not redefine the font.
            style.Font.Name = "Verdana";
            
            style = this.document.Styles[StyleNames.Header];
            style.ParagraphFormat.AddTabStop("16cm", TabAlignment.Right);
            
            style = this.document.Styles[StyleNames.Footer];
            style.ParagraphFormat.AddTabStop("8cm", TabAlignment.Center);
            
            // Create a new style called Table based on style Normal
            style = this.document.Styles.AddStyle("Table", "Normal");
            style.Font.Name = "Verdana";
            style.Font.Name = "Times New Roman";
            style.Font.Size = "9pt";
            
            // Create a new style called Reference based on style Normal
            style = this.document.Styles.AddStyle("Reference", "Normal");
            style.ParagraphFormat.SpaceBefore = "5mm";
            style.ParagraphFormat.SpaceAfter = "5mm";
            style.ParagraphFormat.TabStops.AddTabStop("16cm", TabAlignment.Right);
        }
        
#endregion
        
#region  Create Static Page
        
        /// <summary>
        /// Creates the static parts of the invoice.
        /// </summary>
        private void CreatePage()
        {
            // Each MigraDoc document needs at least one section.
            Section section = this.document.AddSection();
            var with_2 = section.PageSetup;
            //.HeaderDistance = "1cm"
            with_2.TopMargin = "4,0cm";
            with_2.BottomMargin = "3,0cm";
            with_2.FooterDistance = "0,3cm";
            with_2.Orientation = Orientation.Landscape;
            
            
            // Put a logo in the header
            Image image = section.Headers.Primary.AddImage(Logo);
            image.Height = "2.5cm";
            image.LockAspectRatio = true;
            image.RelativeVertical = RelativeVertical.Line;
            image.RelativeHorizontal = RelativeHorizontal.Margin;
            image.Top = ShapePosition.Top;
            image.Left = ShapePosition.Right;
            image.WrapFormat.Style = WrapStyle.Through;
            
            // Create the text frame for the vendor name
            this.vendorFrame = section.Headers.Primary.AddTextFrame();
            this.vendorFrame.Top = "2.0cm";
            this.vendorFrame.Left = ShapePosition.Left;
            this.vendorFrame.Width = "7.0cm";
            this.vendorFrame.RelativeHorizontal = RelativeHorizontal.Margin;
            this.vendorFrame.RelativeVertical = RelativeVertical.Page;
            
            // Create the text frame for the BILL TO
            this.addressFrame = section.Headers.Primary.AddTextFrame();
            this.addressFrame.Top = "3.0cm";
            this.addressFrame.Left = ShapePosition.Left;
            this.addressFrame.Width = "7.0cm";
            //.Height = "3.0cm"
            this.addressFrame.RelativeHorizontal = RelativeHorizontal.Margin;
            this.addressFrame.RelativeVertical = RelativeVertical.Page;
            
            // Create the text frame for the SHIP TO
            this.deleveryFrame = section.Headers.Primary.AddTextFrame();
            this.deleveryFrame.Top = "3.0cm";
            this.deleveryFrame.Left = "8.00cm"; //ShapePosition.Left
            this.deleveryFrame.Width = "6.0cm";
            //.Height = "3.0cm"
            this.deleveryFrame.RelativeHorizontal = RelativeHorizontal.Margin;
            this.deleveryFrame.RelativeVertical = RelativeVertical.Page;
            
            // Create the text frame for the header info
            this.headerFrame = section.Headers.Primary.AddTextFrame(); // section.AddTextFrame()
            this.headerFrame.Top = "2,5cm";
            this.headerFrame.Left = "16.0cm"; //ShapePosition.Right
            this.headerFrame.Width = "8.0cm";
            //.Height = "2.0cm"
            this.headerFrame.RelativeHorizontal = RelativeHorizontal.Margin;
            this.headerFrame.RelativeVertical = RelativeVertical.Page;
            
            // Create footer
            var with_8 = section.Footers.Primary.AddParagraph();
            var with_9 = with_8.Format;
            with_9.SpaceBefore = "1cm";
            with_9.Font.Size = 7;
            with_9.Borders.Color = RescueTekniq.Doc.PurchaseOrderForm_Dk.TableBorder;
            with_9.Borders.Width = 0.25;
            with_9.Borders.Left.Width = 0.5;
            with_9.Borders.Right.Width = 0.5;
            with_9.Alignment = ParagraphAlignment.Center;
            with_8.AddText(Company.name);
            with_8.AddText(" · ");
            with_8.AddText(Company.adresse);
            with_8.AddText(" · ");
            with_8.AddText(Company.zipcode);
            with_8.AddText(" ");
            with_8.AddText(Company.city);
            if (this.PurchaseOrder.Supplier.LandekodeID != 45)
            {
                with_8.AddText(" · ");
                with_8.AddText(Company.country);
            }
            with_8.AddText(" · ");
            with_8.AddText("CVR.nr. ");
            with_8.AddText(Company.vatno);
            with_8.AddText(" · ");
            if (this.PurchaseOrder.Supplier.LandekodeID != 45)
            {
                with_8.AddText("Pho. ");
                with_8.AddText(Company.int_phone);
            }
            else
            {
                with_8.AddText("Tlf. ");
                with_8.AddText(Company.phone);
            }
            with_8.AddText(" · ");
            Hyperlink link = with_8.AddHyperlink(string.Format("http://{0}", Company.link), HyperlinkType.Web);
            link.AddFormattedText(Company.link);
            
            var with_10 = section.Footers.Primary.AddParagraph();
            var with_11 = with_10.Format;
            with_11.Font.Size = 4;
            with_11.Alignment = ParagraphAlignment.Center;
            with_10.AddLineBreak();
            with_10.AddLineBreak();
            with_10.AddText(string.Format("- PurchaseOrder no. {0} - page ", PurchaseOrder.ID.ToString()));
            with_10.AddPageField();
            with_10.AddText(" of ");
            with_10.AddNumPagesField();
            with_10.AddText(" - ");
            
            var with_12 = section.AddParagraph();
            with_12.Format.SpaceBefore = "3cm";
            with_12.Style = "Reference";
            //.AddFormattedText("PurchaseOrder", TextFormat.Bold)
            with_12.AddFormattedText(string.Format("PurchaseOrder no. {0}", PurchaseOrder.ID.ToString()), TextFormat.Bold);
            with_12.AddLineBreak();
            
            var with_13 = section.AddParagraph();
            with_13.Style = "Reference";
            with_13.AddText("We hereby submit the purchase order for the following products:");
            
            // Create the item table
            this.table = section.AddTable();
            this.table.Style = "Table";
            this.table.Borders.Color = RescueTekniq.Doc.PurchaseOrderForm_Dk.TableBorder;
            this.table.Borders.Width = 0.25;
            this.table.Borders.Left.Width = 0.5;
            this.table.Borders.Right.Width = 0.5;
            this.table.Rows.LeftIndent = 0;
            this.table.Format.Font.Size = "10pt";
            
            // Before you can add a row, you must define the columns
            Column column;
            
            //cell 0
            column = this.table.AddColumn("1,00cm"); // - Line Item (Line no)
            column.Format.Alignment = ParagraphAlignment.Right;
            
            //cell 1
            column = this.table.AddColumn("12,0cm"); // - Item Name / description
            column.Format.Alignment = ParagraphAlignment.Left;
            
            //cell 2
            column = this.table.AddColumn("3,50cm"); // - Item no / P/N
            column.Format.Alignment = ParagraphAlignment.Right;
            
            //cell 3
            column = this.table.AddColumn("1,50cm"); // - Quantity
            column.Format.Alignment = ParagraphAlignment.Center;
            
            //cell 4
            column = this.table.AddColumn("2,50cm"); // - Item Price / Unit Price (CURR)
            column.Format.Alignment = ParagraphAlignment.Right;
            
            //cell 5
            column = this.table.AddColumn("2,50cm"); // - Line total
            column.Format.Alignment = ParagraphAlignment.Right;
            //
            //   Sum CM must be = 16 cm | 1+6,5+2,5+1+2,5+2,5
            //
            
            // Create the header of the table
            Row row = default(Row);
            row = table.AddRow();
            row.HeadingFormat = true;
            row.Format.Alignment = ParagraphAlignment.Center;
            row.Format.Font.Bold = true;
            row.Shading.Color = RescueTekniq.Doc.PurchaseOrderForm_Dk.TableBlue;
            
            row.Cells[0].AddParagraph("Line item"); //0
            row.Cells[0].Format.Alignment = ParagraphAlignment.Center;
            
            row.Cells[1].AddParagraph("Description"); //1
            row.Cells[1].Format.Alignment = ParagraphAlignment.Center;
            
            row.Cells[2].AddParagraph("P/N"); //2
            row.Cells[2].Format.Alignment = ParagraphAlignment.Center;
            
            row.Cells[3].AddParagraph("Qty."); //3
            row.Cells[3].Format.Alignment = ParagraphAlignment.Center;
            
            row.Cells[4].AddParagraph(string.Format("Unit price ({0})", PurchaseOrder.CurrencyCode)); //4
            row.Cells[4].Format.Alignment = ParagraphAlignment.Center;
            
            row.Cells[5].AddParagraph("Extension"); //5
            row.Cells[5].Format.Alignment = ParagraphAlignment.Center;
            
            
            this.table.SetEdge(0, 0, 6, 1, Edge.Box, BorderStyle.Single, 0.75, Color.Empty);
        }
        
#endregion
        
#region  Fill Page with Data
        
        /// <summary>
        /// Creates the dynamic parts of the invoice.
        /// </summary>
        private void FillContent()
        {
            FillContentHeader();
            FillContentHeader_Delevery();
            FillContentHeader_PageInfo();
            
            FillContentLines();
            
            FillContentBottom();
        }
        
        /// <summary>
        /// Creates the dynamic parts of the invoice.
        /// </summary>
        private void FillContentHeader()
        {
            
            // Fill address in address text frame
            var with_1 = this.vendorFrame.AddParagraph();
            with_1.Format.Font.Size = "10pt";
            with_1.AddFormattedText("Vendor name:", TextFormat.Bold);
            with_1.AddLineBreak();
            var with_2 = this.vendorFrame.AddParagraph();
            with_2.Format.LeftIndent = Unit.FromCentimeter(0.5);
            with_2.Format.Font.Size = "10pt";
            with_2.AddText(this.PurchaseOrder.Supplier.Firmanavn);
            with_2.AddLineBreak();
            
            var with_3 = this.addressFrame.AddParagraph();
            with_3.Format.Font.Size = "10pt";
            with_3.AddFormattedText("BILL TO:", TextFormat.Bold);
            with_3.AddLineBreak();
            var with_4 = this.addressFrame.AddParagraph();
            with_4.Format.Font.Size = "10pt";
            with_4.Format.LeftIndent = Unit.FromCentimeter(0.5);
            with_4.AddText(Config.Company.name);
            with_4.AddLineBreak();
            with_4.AddText(Config.Company.adresse);
            with_4.AddLineBreak();
            with_4.AddText(Config.Company.PostnrBy);
            with_4.AddLineBreak();
            with_4.AddText(Config.Company.country);
            with_4.AddLineBreak();
        }
        
        /// <summary>
        /// Creates the dynamic parts of the invoice.
        /// </summary>
        private void FillContentHeader_Delevery()
        {
            if (this.PurchaseOrder.ShipToName == "")
            {
                return;
            }
            
            var with_1 = this.deleveryFrame.AddParagraph();
            with_1.Format.Font.Size = "10pt";
            with_1.AddFormattedText("SHIP TO:", TextFormat.Bold);
            with_1.AddLineBreak();
            
            var with_2 = this.deleveryFrame.AddParagraph();
            with_2.Format.LeftIndent = Unit.FromCentimeter(0.5);
            with_2.Format.Font.Size = "10pt";
            
            with_2.AddText(this.PurchaseOrder.ShipToName);
            with_2.AddLineBreak();
            with_2.AddText(this.PurchaseOrder.ShipToAddress1);
            with_2.AddLineBreak();
            if (this.PurchaseOrder.ShipToAddress2.Trim() != "")
            {
                with_2.AddText(this.PurchaseOrder.ShipToAddress2);
                with_2.AddLineBreak();
            }
            with_2.AddText(this.PurchaseOrder.ShipToPostnrBy);
            with_2.AddLineBreak();
            if (this.PurchaseOrder.ShipToCountry != "")
            {
                with_2.AddText(this.PurchaseOrder.ShipToCountry);
                with_2.AddLineBreak();
            }
            
            if (PurchaseOrder.ShipToAtt.Trim() != "")
            {
                with_2.AddText("Att. : ");
                with_2.AddText(this.PurchaseOrder.ShipToAtt);
                with_2.AddLineBreak();
            }
        }
        
        /// <summary>
        /// Fill Header Page Info
        /// </summary>
        /// <remarks></remarks>
        private void FillContentHeader_PageInfo()
        {
            
            // Add the print date field
            var with_1 = this.headerFrame.AddParagraph();
            var with_2 = with_1.Format;
            with_2.Font.Size = "9pt";
            
            with_1.Style = "Reference";
            //.AddFormattedText(String.Format("PurchaseOrder nr. {0}", PurchaseOrder.ID), TextFormat.Bold)
            //.AddLineBreak()
            
            if (this.PurchaseOrder.Supplier.LandekodeID != 45)
            {
                with_1.AddText("Date: ");
                with_1.AddFormattedText(string.Format("{0:MMM dd, yyyy}", PurchaseOrder.PurchaseDate), TextFormat.Bold);
            }
            else
            {
                with_1.AddText("Dato: ");
                with_1.AddFormattedText(string.Format("{0:dd. MMM yyyy}", PurchaseOrder.PurchaseDate), TextFormat.Bold);
            }
            with_1.AddLineBreak();
            
            with_1.AddText("Order created by: ");
            with_1.AddFormattedText(this.PurchaseOrder.OrderCreatedBy, TextFormat.Bold);
            with_1.AddLineBreak();
            with_1.AddText("Contact Name: ");
            with_1.AddFormattedText(Config.PurchaseOrder.name, TextFormat.Bold);
            with_1.AddLineBreak();
            with_1.AddText("Phone number: ");
            with_1.AddFormattedText(Config.PurchaseOrder.phone, TextFormat.Bold);
            with_1.AddLineBreak();
            with_1.AddText("E-mail address: ");
            with_1.AddFormattedText(Config.PurchaseOrder.email, TextFormat.Bold);
            with_1.AddLineBreak();
            //.AddLineBreak()
            
            with_1.AddText("Credit Terms: ");
            with_1.AddFormattedText(PurchaseOrder.PaymentText, TextFormat.Bold);
            with_1.AddLineBreak();
            with_1.AddText("Ship via: ");
            with_1.AddLineBreak();
            with_1.AddFormattedText(PurchaseOrder.FreightText, TextFormat.Bold);
            with_1.AddLineBreak();
            
            //.AddLineBreak()
            //.AddText("Page ")
            //.AddPageField()
            //.AddText(" of ")
            //.AddNumPagesField()
            
        }
        
        /// <summary>
        /// Fill up Purchase Order Lines
        /// </summary>
        /// <remarks></remarks>
        private void FillContentLines()
        {
            //Dim paragraph As Paragraph
            
            // Iterate the invoice items
            totalExtendedPrice = 0;
            int LineItems = 0;
            foreach (PurchaseOrderItem linie in PurchaseOrder.PurchaseOrderItems)
            {
                LineItems++;
                double Quantity = (double) linie.Quantity;
                double Price = (double) linie.ItemPrice;
                double LinePrice = (double) linie.LinePrice;
                
                double extendedPrice = (double) linie.LineTotal; //Quantity * LinePrice
                totalExtendedPrice += extendedPrice;
                
                // Each item fills row
                var with_1 = this.table.AddRow();
                with_1.Cells[0].Format.Alignment = ParagraphAlignment.Center;
                with_1.Cells[0].AddParagraph(System.Convert.ToString(LineItems));
                
                with_1.Cells[1].Format.Alignment = ParagraphAlignment.Left;
                with_1.Cells[1].AddParagraph(linie.SupplierItemName);
                
                with_1.Cells[2].Format.Alignment = ParagraphAlignment.Left;
                with_1.Cells[2].AddParagraph(linie.SupplierItemID);
                
                if ((double) linie.Quantity != 0.0)
                {
                    
                    with_1.Cells[3].Format.Alignment = ParagraphAlignment.Center;
                    with_1.Cells[3].AddParagraph(System.Convert.ToString(linie.Quantity));
                    
                    with_1.Cells[4].Format.Alignment = ParagraphAlignment.Right;
                    with_1.Cells[4].AddParagraph(linie.ItemPrice.ToString("#,##0.00"));
                    
                    with_1.Cells[5].Format.Alignment = ParagraphAlignment.Right;
                    with_1.Cells[5].Shading.Color = RescueTekniq.Doc.PurchaseOrderForm_Dk.TableGray;
                    with_1.Cells[5].AddParagraph(extendedPrice.ToString("#,##0.00"));
                }
                
                
                this.table.SetEdge(0, this.table.Rows.Count - 1, 6, 1, Edge.Box, BorderStyle.Single, 0.75);
            }
        }
        
        private void FillContentBottom()
        {
            
            decimal moms = 25;
            string InvoceVat = Config.Invoice.VAT; // ConfigurationManager.AppSettings("invoice.vat")
            try
            {
                if (InvoceVat.Trim() != "")
                {
                    moms = decimal.Parse(InvoceVat);
                }
            }
            catch
            {
            }
            
            decimal momsbelob = 0;
            
            // Add an invisible row as a space line to the table
            Row row = this.table.AddRow();
            row.Borders.Visible = false;
            
            totalExtendedPrice = Math.Round(totalExtendedPrice, 2);
            
            // Add the total price row
            TotalRow(this.table.AddRow(), "Total", (decimal) totalExtendedPrice);
            
            //If PurchaseOrder.FreightPrice <> 0.0 Then
            //    ' Add the additional Freight fee row
            //    TotalRow(Me.table.AddRow(), "Freight", PurchaseOrder.FreightPrice)
            //    totalExtendedPrice += PurchaseOrder.FreightPrice
            //End If
            
            if (!PurchaseOrder.Supplier.VATFree)
            {
                momsbelob = (decimal) (Math.Round(totalExtendedPrice * (double) (moms / 100), 2));
                // Add the VAT row
                TotalRow(this.table.AddRow(), "VAT (" + (moms / 100).ToString("P") + ")", momsbelob);
            }
            
            // Add the total due row
            totalExtendedPrice += (double) momsbelob;
            //TotalRow(Me.table.AddRow(), String.Format("Total {0} freight", IIf(PurchaseOrder.FreightPrice <> 0.0, "incl.", "excl.")), totalExtendedPrice, True)
            TotalRow(this.table.AddRow(), "Total excl. freight", (decimal) totalExtendedPrice, true);
            
            // Set the borders of the specified cell range
            this.table.SetEdge(5, this.table.Rows.Count - 3, 1, 3, Edge.Box, BorderStyle.Single, 0.75);
            
            AddBesked();
            
            AddNoter();
        }
        
        private void AddBesked()
        {
            Section LastSec = this.document.LastSection;
            
            //With LastSec.AddParagraph()
            //    .Format.SpaceBefore = "5mm"
            //    '.Format.Font.Size = 7
            //    Dim RegNo As String = Config.Bank.RegNo
            //    Dim AccountNo As String = Config.Bank.AccountNo
            //    Dim BankName As String = Config.Bank.Name
            //    Dim BankAdr As String = Config.Bank.Address
            //    Dim BankSwift As String = Config.Bank.SWIFT
            //    Dim BankIBAN As String = Config.Bank.IBAN
            //    '.AddText("Betalingsbetingelser : ")
            //    '.AddFormattedText(Me.PurchaseOrder.PaymentTerms, TextFormat.Bold)
            //    '.AddLineBreak()
            //End With
            
            //With LastSec.AddParagraph
            //    .Format.SpaceBefore = "5mm"
            //    .AddText("Please send infomation about the expected delivery time.")
            //    .AddLineBreak()
            //End With
            
            var with_1 = LastSec.AddParagraph();
            with_1.Format.SpaceBefore = "5mm";
            with_1.AddText("Please send invoice to ");
            Hyperlink link = with_1.AddHyperlink(string.Format("{0}", Config.PurchaseOrder.email), HyperlinkType.Web);
            link.AddFormattedText(Config.PurchaseOrder.email);
            with_1.AddText(string.Format(", attention: {0}.", Config.PurchaseOrder.name));
            with_1.AddLineBreak();
            
            Font FontTal = new Font();
            FontTal.Color = new Color((byte) 255, (byte) 0, (byte) 0);
            FontTal.Bold = true;
            var with_3 = LastSec.AddParagraph();
            with_3.Format.SpaceBefore = "5mm";
            //.Format.Font.Size = 7
            with_3.Format.Alignment = ParagraphAlignment.Center;
            with_3.AddText("Regards");
            with_3.AddLineBreak();
            with_3.AddText(Company.name);
            with_3.AddLineBreak();
            //With LastSec.AddParagraph
            //    .Format.SpaceBefore = "0,5cm"
            //    '.Format.Font.Size = 7
            //    .Format.Alignment = ParagraphAlignment.Center
            
            //    ' Put a signatur image
            //    If SignaturImage <> "" AndAlso SignaturImage <> "signatur.gif" Then
            //        With .AddImage(SignaturImage)
            //            .LockAspectRatio = True
            //            .Width = "3,05cm"
            //            .WrapFormat.Style = WrapStyle.None
            //        End With
            //    End If
            //    .AddLineBreak()
            //    .AddText(Signatur)
            //    .AddLineBreak()
            //End With
            
        }
        
        private void AddNoter()
        {
            Section LastSec = this.document.LastSection;
            
            if (PurchaseOrder.Notes.Trim() != "")
            {
                
                // Add the notes paragraph
                var with_1 = LastSec.AddParagraph();
                var with_2 = with_1.Format;
                with_2.SpaceBefore = "5mm";
                with_2.Borders.Width = 0.75;
                with_2.Borders.Distance = 3;
                with_2.Borders.Color = RescueTekniq.Doc.PurchaseOrderForm_Dk.TableBorder;
                with_2.Shading.Color = RescueTekniq.Doc.PurchaseOrderForm_Dk.TableGray;
                with_2.KeepTogether = true;
                with_1.AddText(PurchaseOrder.Notes);
                
            }
        }
        
        private void TotalRow(Row row, string tekst, decimal value, bool Bold = false)
        {
            
            var with_1 = row;
            var with_2 = with_1.Cells[0];
            with_2.AddParagraph(tekst);
            with_2.Borders.Visible = false;
            with_2.Format.Font.Bold = true;
            with_2.Format.Alignment = ParagraphAlignment.Right;
            with_2.MergeRight = 4;
            var with_3 = with_1.Cells[5];
            var with_4 = with_3.AddParagraph();
            if (Bold)
            {
                with_4.Format.Font.Bold = true;
            }
            with_4.AddText(value.ToString("#,##0.00"));
            
        }
#endregion
        
    }
    
    
}
