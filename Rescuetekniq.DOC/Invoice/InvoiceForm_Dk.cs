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
    
    
    //Namespace Invoice
    
    /// <summary>
    /// Creates the Invoices følgemail form.
    /// </summary>
    public class InvoiceForm_Dk
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
        
        /// <summary>
        /// The text frame of the MigraDoc document that contains the header (top/right).
        /// </summary>
        private TextFrame headerFrame;
        
        /// <summary>
        /// The table of the MigraDoc document that contains the invoice items.
        /// </summary>
        private Table table;
        
        /// <summary>
        /// Class contains all info about Invoice og Invoiceslinier
        /// </summary>
        private RescueTekniq.BOL.InvoiceHeader _Invoice = new RescueTekniq.BOL.InvoiceHeader();
        
        /// <summary>
        /// total sum Price
        /// </summary>
        private double totalExtendedPrice = 0;
        
        /// <summary>
        /// Invoice's ID
        /// </summary>
        /// <remarks></remarks>
        private int _InvoiceID = -1;
        
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
        /// Is this Invoice Copy of original
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
        public InvoiceForm_Dk(int InvoiceID)
        {
            this.InvoiceID = InvoiceID;
        }
        public InvoiceForm_Dk()
        {
            
        }
        public InvoiceForm_Dk(int InvoiceID, string Logo, string Signatur, string signaturImage, bool IsCopy)
        {
            this.InvoiceID = InvoiceID;
            this.Logo = Logo;
            this.Signatur = Signatur;
            this.SignaturImage = signaturImage;
            this.IsCopy = IsCopy;
        }
        
#endregion
        
#region  Properties
        
        public int InvoiceID
        {
            get
            {
                return _InvoiceID;
            }
            set
            {
                _InvoiceID = value;
                //If Invoice.loaded Then
                //    If Invoice.ID <> _InvoiceID Then
                //        LoadInvoice(_InvoiceID)
                //    End If
                //Else
                //    LoadInvoice(_InvoiceID)
                //End If
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
        
        public RescueTekniq.BOL.InvoiceHeader Invoice
        {
            get
            {
                if (_InvoiceID > 0)
                {
                    if (!_Invoice.loaded)
                    {
                        _Invoice = InvoiceHeader.GetInvoice(_InvoiceID);
                    }
                    else if (_Invoice.ID != _InvoiceID)
                    {
                        _Invoice = InvoiceHeader.GetInvoice(_InvoiceID);
                    }
                }
                return _Invoice;
            }
            protected set
            {
                _Invoice = value;
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
                if (Invoice.EANInvoice)
                {
                    _EANtitle = "EAN ";
                }
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
            if (!Invoice.loaded)
            {
                //RaiseEvent InvoiceNotExist()
                //LoadInvoice(InvoiceID)
            }
            
            //IsCopy = Not Invoice.Status = Invoice_StatusEnum.SentToCustomer
            
            // Create a new MigraDoc document
            this.document = new Document();
            var with_1 = this.document.Info;
            with_1.Title = "";
            with_1.Subject = "";
            switch (Invoice.Status)
            {
                case Invoice_StatusEnum.FirstReminder:
                    with_1.Title += string.Format("1. Rykker på faktura nr. {0}", InvoiceID);
                    
                    with_1.Subject += "1. Rykker ";
                    break;
                default:
                    with_1.Title += EANtitle;
                    with_1.Title += "Faktura nr. " + System.Convert.ToString(InvoiceID);
                    
                    with_1.Subject += EANtitle + "Faktura på hjertestarter og evt tilbehør.";
                    break;
            }
            with_1.Title += System.Convert.ToString(IsCopy ? " - Kopi" : "");
            
            with_1.Author = Config.Company.name; // "Heart2Start"
            //.Comment = ""
            //.Keywords = ""
            
            DefineStyles();
            
            CreatePage();
            
            FillContent();
            
            if (this.Invoice.IsEAN)
            {
                switch (Invoice.Status)
                {
                    case Invoice_StatusEnum.Active:
                    case Invoice_StatusEnum.SentToCustomer:
                    case Invoice_StatusEnum.WaitingPayment:
                        OrderForm_CreatePage();
                        OrderForm_FillContent();
                        break;
                }
            }
            
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
        
#region  Design InvoiceForm
        
#region  Create Static Page for InvoiceForm
        
        /// <summary>
        /// Creates the static parts of the invoice.
        /// </summary>
        private void CreatePage()
        {
            // Each MigraDoc document needs at least one section.
            Section section = this.document.AddSection();
            var with_2 = section.PageSetup;
            //.HeaderDistance = "1cm"
            with_2.TopMargin = "4,5cm";
            
            with_2.BottomMargin = "3,0cm";
            with_2.FooterDistance = "0,3cm";
            // Create footer
            
            
            // Put a logo in the header
            Image image = section.Headers.Primary.AddImage(Logo);
            image.Height = "2.5cm";
            image.LockAspectRatio = true;
            image.RelativeVertical = RelativeVertical.Line;
            image.RelativeHorizontal = RelativeHorizontal.Margin;
            image.Top = ShapePosition.Top;
            image.Left = ShapePosition.Right;
            image.WrapFormat.Style = WrapStyle.Through;
            
            // Create the text frame for the address
            //section.Headers.Primary.AddParagraph()
            this.addressFrame = section.Headers.Primary.AddTextFrame(); //AddParagraph() 'section.AddTextFrame()
            this.addressFrame.Top = "3.0cm";
            this.addressFrame.Left = ShapePosition.Left;
            //.Height = "3.0cm"
            this.addressFrame.Width = "7.0cm";
            this.addressFrame.RelativeHorizontal = RelativeHorizontal.Margin;
            this.addressFrame.RelativeVertical = RelativeVertical.Page;
            
            // Create the text frame for the address
            //section.Headers.Primary.AddParagraph()
            this.deleveryFrame = section.Headers.Primary.AddTextFrame(); //AddParagraph() 'section.AddTextFrame()
            this.deleveryFrame.Top = "2.0cm";
            this.deleveryFrame.Left = "7.00cm"; //ShapePosition.Left
            this.deleveryFrame.Width = "5.0cm";
            //.Height = "3.0cm"
            this.deleveryFrame.RelativeHorizontal = RelativeHorizontal.Margin;
            this.deleveryFrame.RelativeVertical = RelativeVertical.Page;
            
            
            // Create footer
            Paragraph paragraph = section.Footers.Primary.AddParagraph();
            var with_7 = paragraph.Format;
            with_7.SpaceBefore = "1cm";
            with_7.Font.Size = 7;
            with_7.Borders.Color = RescueTekniq.Doc.InvoiceForm_Dk.TableBorder;
            with_7.Borders.Width = 0.25;
            with_7.Borders.Left.Width = 0.5;
            with_7.Borders.Right.Width = 0.5;
            with_7.Alignment = ParagraphAlignment.Center;
            paragraph.AddText(Company.name); //("Heart2Start ApS")
            paragraph.AddText(" · ");
            
            paragraph.AddText(Company.adresse); //("Gefionsvej 8")
            
            paragraph.AddText(" · ");
            paragraph.AddText(Company.zipcode); //("3400")
            paragraph.AddText(" ");
            paragraph.AddText(Company.city); //("Hillerød")
            if (this.Invoice.Virksomhed.LandekodeID != 45)
            {
                paragraph.AddText(" · ");
                paragraph.AddText(Company.country); //("Denmark")
            }
            paragraph.AddText(" · ");
            paragraph.AddText("CVR.nr. ");
            paragraph.AddText(Company.vatno); //("32446329")
            paragraph.AddText(" · ");
            if (this.Invoice.Virksomhed.LandekodeID != 45)
            {
                paragraph.AddText("Pho. ");
                paragraph.AddText(Company.int_phone); //("+45 48212879")
            }
            else
            {
                paragraph.AddText("Tlf. ");
                paragraph.AddText(Company.phone); //("48212879")
            }
            paragraph.AddText(" · ");
            //.AddText("www.RescueTekniq.dk")
            Hyperlink link = paragraph.AddHyperlink(string.Format("http://{0}", Company.link), HyperlinkType.Web);
            link.AddFormattedText(Company.link); //("www.RescueTekniq.dk")
            //.AddHyperlink("www.RescueTekniq.dk", HyperlinkType.Web)
            
            var with_8 = section.Footers.Primary.AddParagraph();
            var with_9 = with_8.Format;
            with_9.Font.Size = 4;
            with_9.Alignment = ParagraphAlignment.Center;
            with_8.AddLineBreak();
            with_8.AddLineBreak();
            
            switch (Invoice.Status)
            {
                case Invoice_StatusEnum.FirstReminder:
                    with_8.AddText("- 1. Rykker");
                    break;
                default:
                    with_8.AddText(string.Format("- {0}Faktura nr. {1}", EANtitle, Invoice.ID));
                    break;
            }
            if (IsCopy)
            {
                with_8.AddText(" - kopi");
            }
            
            with_8.AddText(" - side ");
            with_8.AddPageField();
            //.AddText(" af ")
            //.AddNumPagesField()
            with_8.AddText(" - ");
            
            // Create the text frame for the address
            this.headerFrame = section.AddTextFrame();
            this.headerFrame.Height = "2.0cm";
            this.headerFrame.Width = "5.5cm";
            this.headerFrame.Left = ShapePosition.Right;
            this.headerFrame.RelativeHorizontal = RelativeHorizontal.Margin;
            this.headerFrame.Top = "4.5cm";
            this.headerFrame.RelativeVertical = RelativeVertical.Page;
            // Add the print date field
            paragraph = this.headerFrame.AddParagraph();
            string Cvrnr = Company.vatno; //ConfigurationManager.AppSettings("company.vatno")
            // <add key="company.vatno" value="32446329" />
            
            paragraph.Style = "Reference";
            //.Format.Alignment = ParagraphAlignment.Right
            switch (Invoice.Status)
            {
                case Invoice_StatusEnum.FirstReminder:
                    paragraph.AddFormattedText(string.Format("1. Rykker på faktura nr.{0}", Invoice.ID), TextFormat.Bold);
                    break;
                default:
                    paragraph.AddFormattedText(string.Format("{0}Faktura nr. {1}", EANtitle, Invoice.ID), TextFormat.Bold);
                    break;
            }
            if (IsCopy)
            {
                paragraph.AddFormattedText(" - kopi", TextFormat.Bold);
            }
            paragraph.AddLineBreak();
            
            paragraph.AddText(Company.city); //("Hillerød, ")
            paragraph.AddLineBreak();
            paragraph.AddText("Fakturadato: ");
            paragraph.AddText(Invoice.InvoiceDate.ToString("dd-MM-yyyy"));
            //.AddDateField("dd. MMM yyyy")
            paragraph.AddLineBreak();
            
            //.AddText("Betalingsdato: ")
            paragraph.AddText("Forfaldsdato: ");
            paragraph.AddFormattedText(Invoice.PaymentDate.ToString("dd-MM-yyyy"), TextFormat.Bold);
            paragraph.AddLineBreak();
            
            paragraph.AddText("CVR: ");
            paragraph.AddFormattedText(Cvrnr, TextFormat.Bold);
            paragraph.AddLineBreak();
            
            paragraph.AddText(this.Invoice.RettetAf.ToUpper());
            paragraph.AddText("/");
            if (this.Invoice.ResponsibleName.Trim() != "")
            {
                paragraph.AddText(this.Invoice.ResponsibleName.ToUpper());
            }
            paragraph.AddLineBreak();
            
            paragraph.AddLineBreak();
            
            paragraph.AddText("Side ");
            paragraph.AddPageField();
            //.AddText(" af ")
            //.AddNumPagesField()
            
            
            // Add the print date field
            paragraph = section.AddParagraph();
            paragraph.Format.SpaceBefore = "3cm";
            paragraph.Style = "Reference";
            switch (Invoice.Status)
            {
                case Invoice_StatusEnum.FirstReminder:
                    paragraph.AddFormattedText("1. Rykker", TextFormat.Bold);
                    break;
                default:
                    paragraph.AddFormattedText(EANtitle + "Faktura", TextFormat.Bold);
                    if (IsCopy)
                    {
                        paragraph.AddFormattedText("kopi", TextFormat.Bold);
                    }
                    break;
            }
            paragraph.AddLineBreak();
            
            // Add the print date field
            paragraph = section.AddParagraph();
            paragraph.Style = "Reference";
            switch (Invoice.Status)
            {
                case Invoice_StatusEnum.FirstReminder:
                    paragraph.AddText(string.Format("Hermed skal vi bringe vort tilgodehavende i henhold til ovennævnte faktura nr. {0} i erindring – da vi ikke kan se at denne er betalt", Invoice.ID));
                    // – og vi skal herved erindre om indbetaling af nedennævnte beløb.", Invoice.ID))
                    paragraph.AddLineBreak();
                    paragraph.AddText("Såfremt beløbet er indbetalt inden modtagelse af denne skrivelse – bedes De bortse fra denne.");
                    paragraph.AddLineBreak();
                    break;
                default:
                    paragraph.AddText("I henhold til aftale fremsendes hermed faktura på:");
                    break;
            }
            
            // Create the item table
            this.table = section.AddTable();
            this.table.Style = "Table";
            this.table.Borders.Color = RescueTekniq.Doc.InvoiceForm_Dk.TableBorder;
            this.table.Borders.Width = 0.25; //0.25
            this.table.Borders.Left.Width = 0.5;
            this.table.Borders.Right.Width = 0.5;
            this.table.Rows.LeftIndent = 0;
            this.table.Format.Font.Size = "8pt";
            
            // Before you can add a row, you must define the columns
            Column column;
            
            column = this.table.AddColumn("2,25cm"); // - Item no
            column.Format.Alignment = ParagraphAlignment.Right;
            
            column = this.table.AddColumn("6,50cm"); // - Item Name / Serial No
            column.Format.Alignment = ParagraphAlignment.Right;
            
            column = this.table.AddColumn("1,00cm"); // - Quantity
            column.Format.Alignment = ParagraphAlignment.Center;
            
            column = this.table.AddColumn("1,50cm"); // - Item Price
            column.Format.Alignment = ParagraphAlignment.Right;
            
            column = this.table.AddColumn("1,25cm"); // - Discount
            column.Format.Alignment = ParagraphAlignment.Right;
            
            column = this.table.AddColumn("1,50cm"); // - Sales price
            column.Format.Alignment = ParagraphAlignment.Right;
            
            column = this.table.AddColumn("2,00cm"); // - Line total
            column.Format.Alignment = ParagraphAlignment.Right;
            //   Sum CM must be = 16 cm | 1+7,5+2+1+2+2,5
            
            // Create the header of the table
            Row row = default(Row);
            row = table.AddRow();
            row.HeadingFormat = true;
            row.Format.Alignment = ParagraphAlignment.Center;
            row.Format.Font.Bold = true;
            row.Shading.Color = RescueTekniq.Doc.InvoiceForm_Dk.TableBlue;
            
            row.Cells[0].AddParagraph("Vare nr");
            row.Cells[0].Format.Alignment = ParagraphAlignment.Left;
            
            row.Cells[1].AddParagraph("Tekst");
            row.Cells[1].Format.Alignment = ParagraphAlignment.Left;
            
            row.Cells[2].AddParagraph("Antal");
            //.Cells(2).Format.Font.Bold = False
            row.Cells[2].Format.Alignment = ParagraphAlignment.Left;
            //.Cells(2).VerticalAlignment = VerticalAlignment.Bottom
            
            row.Cells[3].AddParagraph("Stk. pris "); //DKK
            row.Cells[3].Format.Alignment = ParagraphAlignment.Left;
            
            row.Cells[4].AddParagraph("Rabat"); // (%)Ambassadør - Reduktion
            row.Cells[4].Format.Alignment = ParagraphAlignment.Left;
            
            row.Cells[5].AddParagraph("á pris");
            row.Cells[5].Format.Alignment = ParagraphAlignment.Left;
            
            row.Cells[6].AddParagraph("i alt DKK");
            row.Cells[6].Format.Alignment = ParagraphAlignment.Right;
            //.Cells(6).VerticalAlignment = VerticalAlignment.Bottom
            //.Cells(6).MergeDown = 1
            
            
            this.table.SetEdge(0, 0, 7, 1, Edge.Box, BorderStyle.Single, 0.75, Color.Empty);
        }
        
#endregion
        
#region  Fill InvoiceForm Page with Data
        
        /// <summary>
        /// Creates the dynamic parts of the invoice.
        /// </summary>
        private void FillContent()
        {
            FillContentHeader();
            FillContentHeader_Delevery();
            
            FillContentLines();
            
            FillContentBottom();
        }
        
        /// <summary>
        /// Creates the dynamic parts of the invoice.
        /// </summary>
        private void FillContentHeader()
        {
            
            // Fill address in address text frame
            var with_1 = this.addressFrame.AddParagraph();
            //.AddText("Personligt")
            //.AddLineBreak()
            
            if (Invoice.EANInvoice)
            {
                with_1.AddText("EAN: ");
                with_1.AddFormattedText(Invoice.InvoiceEANno, TextFormat.Bold);
                with_1.AddLineBreak();
            }
            with_1.AddText(this.Invoice.ContactName);
            with_1.AddLineBreak();
            with_1.AddText(this.Invoice.InvoiceName);
            with_1.AddLineBreak();
            with_1.AddText(this.Invoice.InvoiceAddress1);
            with_1.AddLineBreak();
            if (this.Invoice.InvoiceAddress2.Trim() != "")
            {
                with_1.AddText(this.Invoice.InvoiceAddress2);
                with_1.AddLineBreak();
            }
            with_1.AddText(this.Invoice.InvoicePostnrBy);
            with_1.AddLineBreak();
            if (this.Invoice.InvoiceCountry != "" && this.Invoice.InvoiceCountry.ToLower() != "danmark")
            {
                with_1.AddText(this.Invoice.InvoiceCountry);
                with_1.AddLineBreak();
            }
            if (Invoice.InvoiceAtt.Trim() != "")
            {
                with_1.AddText("Att. : ");
                with_1.AddText(this.Invoice.InvoiceAtt);
                with_1.AddLineBreak();
            }
        }
        
        /// <summary>
        /// Creates the dynamic parts of the invoice.
        /// </summary>
        private void FillContentHeader_Delevery()
        {
            
            if (this.Invoice.DeleveryName == "")
            {
                return;
            }
            
            // Fill address in address text frame
            var with_1 = this.deleveryFrame.AddParagraph();
            with_1.Format.Font.Size = "9pt";
            with_1.AddFormattedText("Leveringsadresse", TextFormat.Bold);
            with_1.AddLineBreak();
            
            if (Invoice.EANInvoice && Invoice.DeleveryEANno.Trim() != "")
            {
                with_1.AddText("EAN: ");
                with_1.AddFormattedText(Invoice.DeleveryEANno, TextFormat.Bold);
                with_1.AddLineBreak();
            }
            
            with_1.AddText(this.Invoice.DeleveryName); //.Virksomhed.Firmanavn
            with_1.AddLineBreak();
            with_1.AddText(this.Invoice.DeleveryAddress1); //.Virksomhed.Adresse1
            with_1.AddLineBreak();
            if (this.Invoice.DeleveryAddress2.Trim() != "") //.Virksomhed.Adresse2
            {
                with_1.AddText(this.Invoice.DeleveryAddress2); //.Virksomhed.Adresse2
                with_1.AddLineBreak();
            }
            with_1.AddText(this.Invoice.DeleveryPostnrBy); //.Virksomhed.PostnrBy
            with_1.AddLineBreak();
            if (this.Invoice.DeleveryCountry != "" && this.Invoice.DeleveryCountry.ToLower() != "danmark") //.Virksomhed.LandekodeID <> 45
            {
                with_1.AddText(this.Invoice.DeleveryCountry);
                with_1.AddLineBreak();
            }
            if (Invoice.DeleveryAtt.Trim() != "")
            {
                with_1.AddText("Att. : ");
                with_1.AddText(this.Invoice.DeleveryAtt);
                with_1.AddLineBreak();
            }
        }
        
        private void FillContentLines()
        {
            Paragraph paragraph = default(Paragraph);
            
            // Iterate the invoice items
            totalExtendedPrice = 0;
            
            foreach (Invoiceline linie in Invoice.Invoiceslinier)
            {
                double Quantity = (double) linie.Quantity;
                double Price = (double) linie.ItemPrice;
                double Discount = (double) linie.Discount;
                double InvoicesPrice = (double) linie.SalesPrice;
                
                double extendedPrice = Quantity * InvoicesPrice;
                //extendedPrice = extendedPrice * (100 - discount) / 100
                totalExtendedPrice += extendedPrice;
                
                // Each item fills row
                var with_1 = this.table.AddRow();
                with_1.Cells[0].Format.Alignment = ParagraphAlignment.Left;
                with_1.Cells[0].AddParagraph(linie.ItemNo); //Item no
                
                with_1.Cells[1].Format.Alignment = ParagraphAlignment.Left;
                paragraph = with_1.Cells[1].AddParagraph();
                paragraph.AddText(linie.ItemName);
                if (linie.SerialNo != "")
                {
                    paragraph.AddLineBreak();
                    paragraph.AddText("Serial Nr.: " + linie.SerialNo);
                }
                
                if ((double) linie.Quantity != 0.0)
                {
                    
                    with_1.Cells[2].AddParagraph(System.Convert.ToString(linie.Quantity));
                    
                    with_1.Cells[3].AddParagraph(Price.ToString("#,##0.00")); //& " kr."
                    
                    if (Discount != 0.0)
                    {
                        with_1.Cells[4].AddParagraph(Discount.ToString("#,##0.00") + "%");
                    }
                    
                    with_1.Cells[5].AddParagraph(InvoicesPrice.ToString("#,##0.00")); // & " kr."
                    
                    with_1.Cells[6].Shading.Color = RescueTekniq.Doc.InvoiceForm_Dk.TableGray;
                    with_1.Cells[6].AddParagraph(extendedPrice.ToString("#,##0.00")); // & " kr."
                }
                
                
                this.table.SetEdge(0, this.table.Rows.Count - 1, 7, 1, Edge.Box, BorderStyle.Single, 0.75);
            }
        }
        
        private void FillContentBottom()
        {
            
            decimal moms = 25;
            string InvoceVat = ConfigurationManager.AppSettings["invoice.vat"];
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
            TotalRow(this.table.AddRow(), "Total pris", (decimal) totalExtendedPrice);
            
            // '' Add the additional transport fee row
            //'TotalRow(Me.table.AddRow(), "Transport", Invoice.Transport)
            //'totalExtendedPrice += Invoice.Transport
            
            if (!Invoice.Virksomhed.VATFree)
            {
                momsbelob = (decimal) (Math.Round(totalExtendedPrice * (double) (moms / 100), 2));
                // Add the VAT row
                TotalRow(this.table.AddRow(), "Moms (" + (moms / 100).ToString("P") + ")", momsbelob);
            }
            
            // Add the total due row
            totalExtendedPrice += (double) momsbelob;
            TotalRow(this.table.AddRow(), "Totalt DKK", (decimal) totalExtendedPrice, true);
            
            // Set the borders of the specified cell range
            this.table.SetEdge(6, this.table.Rows.Count - 3, 1, 3, Edge.Box, BorderStyle.Single, 0.75);
            
            addBesked();
            
            AddNoter();
        }
        
        
        private void addBesked()
        {
            //Dim paragraph As Paragraph
            Section LastSec = this.document.LastSection;
            
            var with_1 = LastSec.AddParagraph(); //paragraph
            with_1.Format.SpaceBefore = "5mm";
            //.Format.Font.Size = 7
            
            string RegNo = ConfigurationManager.AppSettings["bank.regno"];
            string AccountNo = ConfigurationManager.AppSettings["bank.accountno"];
            string BankName = ConfigurationManager.AppSettings["bank.name"];
            string BankAdr = ConfigurationManager.AppSettings["bank.address"];
            string BankSwift = ConfigurationManager.AppSettings["bank.swift"];
            string BankIBAN = ConfigurationManager.AppSettings["bank.iban"];
            
            with_1.AddText("Betalingsbetingelser : ");
            switch (Invoice.Status)
            {
                case Invoice_StatusEnum.FirstReminder:
                    with_1.AddFormattedText("Forfalden", TextFormat.Bold);
                    break;
                default:
                    with_1.AddFormattedText(this.Invoice.PaymentTerms, TextFormat.Bold);
                    break;
            }
            with_1.AddLineBreak();
            
            with_1.AddText("Saldo i vor favør, ");
            with_1.AddFormattedText(totalExtendedPrice.ToString("C"), TextFormat.Bold);
            
            //.AddText(" er forfalden til betaling og bedes venligst indbetalt ")
            switch (Invoice.Status)
            {
                case Invoice_StatusEnum.FirstReminder:
                    with_1.AddText(" er forfalden til betaling og bedes venligst indbetalt ");
                    break;
                    //.AddFormattedText(Date.Today.ToString("d. MMMM yyyy"), TextFormat.Bold)
                default:
                    with_1.AddText(" bedes venligst indbetalt senest den ");
                    with_1.AddFormattedText(Invoice.PaymentDate.ToString("d. MMMM yyyy"), TextFormat.Bold);
                    break;
            }
            with_1.AddText(" på");
            with_1.AddLineBreak();
            
            with_1.AddText(" Reg. Nr. ");
            with_1.AddFormattedText(RegNo, TextFormat.Bold);
            with_1.AddText(" Kontonr. ");
            with_1.AddFormattedText(AccountNo, TextFormat.Bold);
            with_1.AddLineBreak();
            
            with_1.AddText(" Swift. ");
            with_1.AddFormattedText(BankSwift, TextFormat.Bold);
            //.AddLineBreak()
            with_1.AddText(" IBAN. ");
            with_1.AddFormattedText(BankIBAN, TextFormat.Bold);
            with_1.AddLineBreak();
            
            with_1.AddText(BankName);
            //.AddLineBreak()
            with_1.AddText(", ");
            with_1.AddText(BankAdr);
            with_1.AddLineBreak();
            
            with_1.AddText("Ved betaling bedes vort fakturanummer venligst angivet som betalingsreference.");
            //.AddLineBreak()
            
            var with_2 = LastSec.AddParagraph(); //paragraph
            with_2.Format.SpaceBefore = "5mm";
            //.Format.Font.Size = 7
            
            switch (Invoice.Status)
            {
                case Invoice_StatusEnum.FirstReminder:
                    with_2.AddText("Såfremt der er spørgsmål til denne rykkerskrivelse er I meget velkomne til at kontakte os på ");
                    break;
                default:
                    with_2.AddText("Såfremt der er spørgsmål til denne faktura er I meget velkommen til at kontakte os på ");
                    break;
            }
            
            string KontaktEmail = ConfigurationManager.AppSettings["invoice.email"];
            string KontaktPhone = ConfigurationManager.AppSettings["invoice.phone"];
            
            Hyperlink hyp = with_2.AddHyperlink("mailto:" + KontaktEmail, HyperlinkType.Web);
            hyp.AddText(KontaktEmail);
            with_2.AddText(" eller på telefon nr. ");
            with_2.AddText(KontaktPhone);
            with_2.AddLineBreak();
            
            Font FontTal = new Font();
            FontTal.Color = new Color((byte) 255, (byte) 0, (byte) 0);
            FontTal.Bold = true;
            var with_4 = LastSec.AddParagraph(); //paragraph
            with_4.Format.SpaceBefore = "5mm";
            //.Format.Font.Size = 7
            with_4.Format.Alignment = ParagraphAlignment.Center;
            with_4.AddText("Med venlig hilsen");
            with_4.AddLineBreak();
            var with_5 = LastSec.AddParagraph(); //paragraph
            with_5.Format.Alignment = ParagraphAlignment.Center;
            //If Invoice.Virksomhed.IsServicedByAgent Then
            //    .AddText(Invoice.Virksomhed.Agent.CompanyName)
            //Else
            //    .AddText("Heart")
            //    .AddFormattedText("2", FontTal)
            //    .AddText("Start")
            //End If
            with_5.AddText(Company.name);
            //.AddText("Heart")
            //.AddFormattedText("2", FontTal)
            //.AddText("Start")
            with_5.AddLineBreak();
            var with_6 = LastSec.AddParagraph(); //paragraph
            with_6.Format.SpaceBefore = "0,5cm";
            //.Format.Font.Size = 7
            with_6.Format.Alignment = ParagraphAlignment.Center;
            
            // Put a signatur image
            if (SignaturImage != "" && SignaturImage != "signatur.gif")
            {
                var with_7 = with_6.AddImage(SignaturImage);
                with_7.LockAspectRatio = true;
                with_7.Width = "3,05cm";
                with_7.WrapFormat.Style = WrapStyle.None;
            }
            with_6.AddLineBreak();
            with_6.AddText(Signatur);
            with_6.AddLineBreak();
            
        }
        
        private void AddNoter()
        {
            Section LastSec = this.document.LastSection;
            
            if (Invoice.Notes.Trim() != "")
            {
                
                // Add the notes paragraph
                var with_1 = LastSec.AddParagraph();
                var with_2 = with_1.Format;
                with_2.SpaceBefore = "5mm";
                with_2.Borders.Width = 0.75;
                with_2.Borders.Distance = 3;
                with_2.Borders.Color = RescueTekniq.Doc.InvoiceForm_Dk.TableBorder;
                with_2.Shading.Color = RescueTekniq.Doc.InvoiceForm_Dk.TableGray;
                with_2.KeepTogether = true;
                with_1.AddText(Invoice.Notes);
                
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
            with_2.MergeRight = 5;
            var with_3 = with_1.Cells[6];
            //.AddParagraph(value.ToString("#,##0.00")) ' & " kr."
            var with_4 = with_3.AddParagraph();
            if (Bold)
            {
                with_4.Format.Font.Bold = true;
            }
            //.Format.Font.Size = 8
            with_4.AddText(value.ToString("#,##0.00 "));
            
        }
        
#endregion
        
#endregion
        
#region  Design Orderform
        
#region  Create Static Page for Orderform
        
        /// <summary>
        /// Creates the static parts of the Orderform.
        /// </summary>
        private void OrderForm_CreatePage()
        {
            // Each MigraDoc document needs at least one section.
            Section section = this.document.AddSection();
            var with_2 = section.PageSetup;
            //.HeaderDistance = "1cm"
            with_2.TopMargin = "4,5cm";
            with_2.BottomMargin = "3,0cm";
            with_2.FooterDistance = "0,3cm";
            // Create footer
            
            
            // Put a logo in the header
            Image image = section.Headers.Primary.AddImage(Logo);
            image.Height = "2.5cm";
            image.LockAspectRatio = true;
            image.RelativeVertical = RelativeVertical.Line;
            image.RelativeHorizontal = RelativeHorizontal.Margin;
            image.Top = ShapePosition.Top;
            image.Left = ShapePosition.Right;
            image.WrapFormat.Style = WrapStyle.Through;
            
            // Create the text frame for the address
            //section.Headers.Primary.AddParagraph()
            this.addressFrame = section.Headers.Primary.AddTextFrame(); //AddParagraph() 'section.AddTextFrame()
            this.addressFrame.Top = "3.0cm";
            this.addressFrame.Left = ShapePosition.Left;
            //.Height = "3.0cm"
            this.addressFrame.Width = "7.0cm";
            this.addressFrame.RelativeHorizontal = RelativeHorizontal.Margin;
            this.addressFrame.RelativeVertical = RelativeVertical.Page;
            
            // Create the text frame for the address
            //section.Headers.Primary.AddParagraph()
            this.deleveryFrame = section.Headers.Primary.AddTextFrame(); //AddParagraph() 'section.AddTextFrame()
            this.deleveryFrame.Top = "2.0cm";
            this.deleveryFrame.Left = "7.00cm"; //ShapePosition.Left
            this.deleveryFrame.Width = "5.0cm";
            //.Height = "3.0cm"
            this.deleveryFrame.RelativeHorizontal = RelativeHorizontal.Margin;
            this.deleveryFrame.RelativeVertical = RelativeVertical.Page;
            
            
            // Create footer
            Paragraph paragraph = section.Footers.Primary.AddParagraph();
            var with_7 = paragraph.Format;
            with_7.SpaceBefore = "1cm";
            with_7.Font.Size = 7;
            with_7.Borders.Color = RescueTekniq.Doc.InvoiceForm_Dk.TableBorder;
            with_7.Borders.Width = 0.25;
            with_7.Borders.Left.Width = 0.5;
            with_7.Borders.Right.Width = 0.5;
            with_7.Alignment = ParagraphAlignment.Center;
            paragraph.AddText(Company.name); //("Heart2Start ApS")
            paragraph.AddText(" · ");
            
            paragraph.AddText(Company.adresse); //("Gefionsvej 8")
            
            paragraph.AddText(" · ");
            paragraph.AddText(Company.zipcode); //("3400")
            paragraph.AddText(" ");
            paragraph.AddText(Company.city); //("Hillerød")
            if (this.Invoice.Virksomhed.LandekodeID != 45)
            {
                paragraph.AddText(" · ");
                paragraph.AddText(Company.country); //("Denmark")
            }
            paragraph.AddText(" · ");
            paragraph.AddText("CVR.nr. ");
            paragraph.AddText(Company.vatno); //("32446329")
            paragraph.AddText(" · ");
            if (this.Invoice.Virksomhed.LandekodeID != 45)
            {
                paragraph.AddText("Pho. ");
                paragraph.AddText(Company.int_phone); //("+45 48212879")
            }
            else
            {
                paragraph.AddText("Tlf. ");
                paragraph.AddText(Company.phone); //("48212879")
            }
            paragraph.AddText(" · ");
            //.AddText("www.RescueTekniq.dk")
            Hyperlink link = paragraph.AddHyperlink(string.Format("http://{0}", Company.link), HyperlinkType.Web);
            link.AddFormattedText(Company.link); //("www.RescueTekniq.dk")
            //.AddHyperlink("www.RescueTekniq.dk", HyperlinkType.Web)
            
            var with_8 = section.Footers.Primary.AddParagraph();
            var with_9 = with_8.Format;
            with_9.Font.Size = 4;
            with_9.Alignment = ParagraphAlignment.Center;
            with_8.AddLineBreak();
            with_8.AddLineBreak();
            with_8.AddText("- Følgebrev nr. " + Invoice.ID.ToString());
            if (IsCopy)
            {
                with_8.AddText(" kopi");
            }
            //.AddText(" - side ")
            //.AddPageField()
            //.AddText(" af ")
            //.AddNumPagesField()
            //.AddText(" - ")
            
            // Create the text frame for the address
            this.headerFrame = section.AddTextFrame();
            this.headerFrame.Height = "2.0cm";
            this.headerFrame.Width = "5.5cm";
            this.headerFrame.Left = ShapePosition.Right;
            this.headerFrame.RelativeHorizontal = RelativeHorizontal.Margin;
            this.headerFrame.Top = "4.5cm";
            this.headerFrame.RelativeVertical = RelativeVertical.Page;
            // Add the print date field
            paragraph = this.headerFrame.AddParagraph();
            string Cvrnr = Company.vatno; //ConfigurationManager.AppSettings("company.vatno")
            // <add key="company.vatno" value="32446329" />
            
            paragraph.Style = "Reference";
            //.Format.Alignment = ParagraphAlignment.Right
            paragraph.AddFormattedText("Følgebrev nr. " + Invoice.ID.ToString(), TextFormat.Bold);
            paragraph.AddLineBreak();
            
            paragraph.AddText(Company.city); //("Hillerød, ")
            paragraph.AddLineBreak();
            paragraph.AddText("dato: ");
            paragraph.AddText(Invoice.InvoiceDate.ToString("dd-MM-yyyy"));
            //.AddDateField("dd. MMM yyyy")
            paragraph.AddLineBreak();
            
            paragraph.AddText("CVR: ");
            paragraph.AddFormattedText(Cvrnr, TextFormat.Bold);
            paragraph.AddLineBreak();
            
            paragraph.AddText(this.Invoice.RettetAf.ToUpper());
            paragraph.AddText("/");
            if (this.Invoice.ResponsibleName.Trim() != "")
            {
                paragraph.AddText(this.Invoice.ResponsibleName.ToUpper());
            }
            paragraph.AddLineBreak();
            
            paragraph.AddLineBreak();
            
            //.AddText("Side ")
            //.AddPageField()
            //.AddText(" af ")
            //.AddNumPagesField()
            
            
            // Add the print date field
            paragraph = section.AddParagraph();
            paragraph.Format.SpaceBefore = "3cm";
            paragraph.Style = "Reference";
            paragraph.AddFormattedText("Følgebrev", TextFormat.Bold);
            paragraph.AddLineBreak();
            
            // Add the print date field
            paragraph = section.AddParagraph();
            paragraph.Style = "Reference";
            paragraph.AddText("I henhold til aftale fremsendes hermed:");
            
            // Create the item table
            this.table = section.AddTable();
            this.table.Style = "Table";
            this.table.Borders.Color = RescueTekniq.Doc.InvoiceForm_Dk.TableBorder;
            this.table.Borders.Width = 0.25; //0.25
            this.table.Borders.Left.Width = 0.5;
            this.table.Borders.Right.Width = 0.5;
            this.table.Rows.LeftIndent = 0;
            this.table.Format.Font.Size = "8pt";
            
            // Before you can add a row, you must define the columns
            Column column;
            
            column = this.table.AddColumn("4,00cm"); // - Item no
            column.Format.Alignment = ParagraphAlignment.Right;
            
            column = this.table.AddColumn("10,00cm"); // - Item Name / Serial No
            column.Format.Alignment = ParagraphAlignment.Right;
            
            column = this.table.AddColumn("1,00cm"); // - Quantity
            column.Format.Alignment = ParagraphAlignment.Center;
            
            //column = Me.table.AddColumn("1,50cm") ' - Item Price
            //column.Format.Alignment = ParagraphAlignment.Right
            
            //column = Me.table.AddColumn("1,25cm") ' - Discount
            //column.Format.Alignment = ParagraphAlignment.Right
            
            //column = Me.table.AddColumn("1,50cm") ' - Sales price
            //column.Format.Alignment = ParagraphAlignment.Right
            
            //column = Me.table.AddColumn("2,00cm") ' - Line total
            //column.Format.Alignment = ParagraphAlignment.Right
            //'   Sum CM must be = 16 cm | 1+7,5+2+1+2+2,5
            //'
            
            // Create the header of the table
            Row row = default(Row);
            row = table.AddRow();
            row.HeadingFormat = true;
            row.Format.Alignment = ParagraphAlignment.Center;
            row.Format.Font.Bold = true;
            row.Shading.Color = RescueTekniq.Doc.InvoiceForm_Dk.TableBlue;
            
            row.Cells[0].AddParagraph("Vare nr");
            row.Cells[0].Format.Alignment = ParagraphAlignment.Left;
            
            row.Cells[1].AddParagraph("Tekst");
            row.Cells[1].Format.Alignment = ParagraphAlignment.Left;
            
            row.Cells[2].AddParagraph("Antal");
            //.Cells(2).Format.Font.Bold = False
            row.Cells[2].Format.Alignment = ParagraphAlignment.Left;
            //.Cells(2).VerticalAlignment = VerticalAlignment.Bottom
            
            //.Cells(3).AddParagraph("Stk. pris ") 'DKK
            //.Cells(3).Format.Alignment = ParagraphAlignment.Left
            
            //.Cells(4).AddParagraph("Rabat") ' (%)Ambassadør - Reduktion
            //.Cells(4).Format.Alignment = ParagraphAlignment.Left
            
            //.Cells(5).AddParagraph("á pris")
            //.Cells(5).Format.Alignment = ParagraphAlignment.Left
            
            //.Cells(6).AddParagraph("i alt DKK")
            //.Cells(6).Format.Alignment = ParagraphAlignment.Right
            //'.Cells(6).VerticalAlignment = VerticalAlignment.Bottom
            //'.Cells(6).MergeDown = 1
            
            
            this.table.SetEdge(0, 0, 3, 1, Edge.Box, BorderStyle.Single, 0.75, Color.Empty);
        }
        
#endregion
        
#region  Fill Orderform Page with Data
        
        /// <summary>
        /// Creates the dynamic parts of the Orderform.
        /// </summary>
        private void OrderForm_FillContent()
        {
            OrderForm_FillContentHeader();
            OrderForm_FillContentHeader_Delevery();
            
            OrderForm_FillContentLines();
            
            OrderForm_FillContentBottom();
        }
        
        /// <summary>
        /// Creates the dynamic parts of the Orderform.
        /// </summary>
        private void OrderForm_FillContentHeader()
        {
            
            // Fill address in address text frame
            var with_1 = this.addressFrame.AddParagraph();
            //.AddText("Personligt")
            //.AddLineBreak()
            
            if (Invoice.EANInvoice)
            {
                with_1.AddText("EAN: ");
                with_1.AddFormattedText(Invoice.InvoiceEANno, TextFormat.Bold);
                with_1.AddLineBreak();
            }
            with_1.AddText(this.Invoice.ContactName);
            with_1.AddLineBreak();
            with_1.AddText(this.Invoice.InvoiceName);
            with_1.AddLineBreak();
            with_1.AddText(this.Invoice.InvoiceAddress1);
            with_1.AddLineBreak();
            if (this.Invoice.InvoiceAddress2.Trim() != "")
            {
                with_1.AddText(this.Invoice.InvoiceAddress2);
                with_1.AddLineBreak();
            }
            with_1.AddText(this.Invoice.InvoicePostnrBy);
            with_1.AddLineBreak();
            if (this.Invoice.InvoiceCountry != "" && this.Invoice.InvoiceCountry.ToLower() != "danmark")
            {
                with_1.AddText(this.Invoice.InvoiceCountry);
                with_1.AddLineBreak();
            }
            if (Invoice.InvoiceAtt.Trim() != "")
            {
                with_1.AddText("Att. : ");
                with_1.AddText(this.Invoice.InvoiceAtt);
                with_1.AddLineBreak();
            }
        }
        
        /// <summary>
        /// Creates the dynamic parts of the Orderform.
        /// </summary>
        private void OrderForm_FillContentHeader_Delevery()
        {
            
            if (this.Invoice.DeleveryName == "")
            {
                return;
            }
            
            // Fill address in address text frame
            var with_1 = this.deleveryFrame.AddParagraph();
            with_1.Format.Font.Size = "9pt";
            with_1.AddFormattedText("Leveringsadresse", TextFormat.Bold);
            with_1.AddLineBreak();
            
            if (Invoice.EANInvoice && Invoice.DeleveryEANno.Trim() != "")
            {
                with_1.AddText("EAN: ");
                with_1.AddFormattedText(Invoice.DeleveryEANno, TextFormat.Bold);
                with_1.AddLineBreak();
            }
            
            with_1.AddText(this.Invoice.DeleveryName); //.Virksomhed.Firmanavn
            with_1.AddLineBreak();
            with_1.AddText(this.Invoice.DeleveryAddress1); //.Virksomhed.Adresse1
            with_1.AddLineBreak();
            if (this.Invoice.DeleveryAddress2.Trim() != "") //.Virksomhed.Adresse2
            {
                with_1.AddText(this.Invoice.DeleveryAddress2); //.Virksomhed.Adresse2
                with_1.AddLineBreak();
            }
            with_1.AddText(this.Invoice.DeleveryPostnrBy); //.Virksomhed.PostnrBy
            with_1.AddLineBreak();
            if (this.Invoice.DeleveryCountry != "" && this.Invoice.DeleveryCountry.ToLower() != "danmark") //.Virksomhed.LandekodeID <> 45
            {
                with_1.AddText(this.Invoice.DeleveryCountry);
                with_1.AddLineBreak();
            }
            if (Invoice.DeleveryAtt.Trim() != "")
            {
                with_1.AddText("Att. : ");
                with_1.AddText(this.Invoice.DeleveryAtt);
                with_1.AddLineBreak();
            }
        }
        
        private void OrderForm_FillContentLines()
        {
            Paragraph paragraph = default(Paragraph);
            
            // Iterate the invoice items
            totalExtendedPrice = 0;
            
            foreach (Invoiceline linie in Invoice.Invoiceslinier)
            {
                
                if (linie.ItemNo.ToLower() == "fragt")
                {
                    continue;
                }
                
                double Quantity = (double) linie.Quantity;
                double Price = (double) linie.ItemPrice;
                double Discount = (double) linie.Discount;
                double InvoicesPrice = (double) linie.SalesPrice;
                
                double extendedPrice = Quantity * InvoicesPrice;
                //extendedPrice = extendedPrice * (100 - discount) / 100
                totalExtendedPrice += extendedPrice;
                
                // Each item fills row
                var with_1 = this.table.AddRow();
                with_1.Cells[0].Format.Alignment = ParagraphAlignment.Left;
                with_1.Cells[0].AddParagraph(linie.ItemNo); //Item no
                
                with_1.Cells[1].Format.Alignment = ParagraphAlignment.Left;
                paragraph = with_1.Cells[1].AddParagraph();
                paragraph.AddText(linie.ItemName);
                if (linie.SerialNo != "")
                {
                    paragraph.AddLineBreak();
                    paragraph.AddText("Serial Nr.: " + linie.SerialNo);
                }
                
                if ((double) linie.Quantity != 0.0)
                {
                    
                    with_1.Cells[2].AddParagraph(System.Convert.ToString(linie.Quantity));
                    
                    //.Cells(3).AddParagraph(Price.ToString("#,##0.00")) '& " kr."
                    
                    //If Discount <> 0.0 Then
                    //    .Cells(4).AddParagraph(Discount.ToString("#,##0.00") & "%")
                    //End If
                    
                    //.Cells(5).AddParagraph(InvoicesPrice.ToString("#,##0.00")) ' & " kr."
                    
                    //.Cells(6).Shading.Color = TableGray
                    //.Cells(6).AddParagraph(extendedPrice.ToString("#,##0.00")) ' & " kr."
                }
                
                
                this.table.SetEdge(0, this.table.Rows.Count - 1, 3, 1, Edge.Box, BorderStyle.Single, 0.75);
            }
        }
        
        private void OrderForm_FillContentBottom()
        {
            
            OrderForm_addBesked();
            
            OrderForm_AddNoter();
        }
        
        private void OrderForm_addBesked()
        {
            //Dim paragraph As Paragraph
            Section LastSec = this.document.LastSection;
            
            var with_1 = LastSec.AddParagraph(); //paragraph
            with_1.Format.SpaceBefore = "5mm";
            //.Format.Font.Size = 7
            with_1.AddText("Såfremt der er spørgsmål til denne følgeseddel er I meget velkommen til at kontakte os på ");
            
            
            string KontaktEmail = ConfigurationManager.AppSettings["invoice.email"];
            string KontaktPhone = ConfigurationManager.AppSettings["invoice.phone"];
            
            Hyperlink hyp = with_1.AddHyperlink("mailto:" + KontaktEmail, HyperlinkType.Web);
            hyp.AddText(KontaktEmail);
            with_1.AddText(" eller på telefon nr. ");
            with_1.AddText(KontaktPhone);
            with_1.AddLineBreak();
            
            Font FontTal = new Font();
            FontTal.Color = new Color((byte) 255, (byte) 0, (byte) 0);
            FontTal.Bold = true;
            var with_3 = LastSec.AddParagraph(); //paragraph
            with_3.Format.SpaceBefore = "5mm";
            //.Format.Font.Size = 7
            with_3.Format.Alignment = ParagraphAlignment.Center;
            with_3.AddText("Med venlig hilsen");
            with_3.AddLineBreak();
            var with_4 = LastSec.AddParagraph(); //paragraph
            with_4.Format.Alignment = ParagraphAlignment.Center;
            with_4.AddText(Company.name);
            with_4.AddLineBreak();
            
            //With LastSec.AddParagraph  'paragraph
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
        
        private void OrderForm_AddNoter()
        {
            Section LastSec = this.document.LastSection;
            
            if (Invoice.Notes.Trim() != "")
            {
                
                // Add the notes paragraph
                var with_1 = LastSec.AddParagraph();
                var with_2 = with_1.Format;
                with_2.SpaceBefore = "5mm";
                with_2.Borders.Width = 0.75;
                with_2.Borders.Distance = 3;
                with_2.Borders.Color = RescueTekniq.Doc.InvoiceForm_Dk.TableBorder;
                with_2.Shading.Color = RescueTekniq.Doc.InvoiceForm_Dk.TableGray;
                with_2.KeepTogether = true;
                with_1.AddText(Invoice.Notes);
                
            }
        }
        
#endregion
        
#endregion
        
    }
    
    //End Namespace
    
}
