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
    /// Creates the Incasso følgemail form.
    /// </summary>
    public class IncassoForm_Dk
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
        /// Class contains all info about Invoice og Invoiceslinier
        /// </summary>
        private RescueTekniq.BOL.IncassoSag _Incasso = new RescueTekniq.BOL.IncassoSag();
        
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
        /// IncassoID's ID
        /// </summary>
        /// <remarks></remarks>
        private int _IncassoID = -1;
        
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
        private string _Signatur = "Regnskabsafdelingen";
        
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
        public IncassoForm_Dk(int IncassoID)
        {
            this.IncassoID = IncassoID;
        } //Incasso
        public IncassoForm_Dk()
        {
            
        }
        public IncassoForm_Dk(int IncassoID, string Logo, string Signatur, string signaturImage, bool IsCopy)
        {
            this.IncassoID = IncassoID;
            this.Logo = Logo;
            this.Signatur = Signatur;
            this.SignaturImage = signaturImage;
            this.IsCopy = IsCopy;
        }
        
#endregion
        
#region  Properties
        
        public int IncassoID
        {
            get
            {
                return _IncassoID;
            }
            set
            {
                _IncassoID = value;
            }
        }
        
        //Public Property InvoiceID() As Integer
        //    Get
        //        Return _InvoiceID
        //    End Get
        //    Set(ByVal value As Integer)
        //        _InvoiceID = value
        //        'If Invoice.loaded Then
        //        '    If Invoice.ID <> _InvoiceID Then
        //        '        LoadInvoice(_InvoiceID)
        //        '    End If
        //        'Else
        //        '    LoadInvoice(_InvoiceID)
        //        'End If
        //    End Set
        //End Property
        
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
        
        public RescueTekniq.BOL.IncassoSag Incasso
        {
            get
            {
                if (_IncassoID > 0)
                {
                    if (!_Incasso.loaded)
                    {
                        _Incasso = IncassoSag.GetIncasso(_IncassoID);
                    }
                    else if (_Incasso.ID != _IncassoID)
                    {
                        _Incasso = IncassoSag.GetIncasso(_IncassoID);
                    }
                }
                return _Incasso;
            }
            protected set
            {
                _Incasso = value;
            }
        }
        
        //Public Property Invoice() As RescueTekniq.BOL.InvoiceHeader
        //    Get
        //        If _InvoiceID > 0 Then
        //            If Not _Invoice.loaded Then
        //                _Invoice = InvoiceHeader.GetInvoice(_InvoiceID)
        //            ElseIf _Invoice.ID <> _InvoiceID Then
        //                _Invoice = InvoiceHeader.GetInvoice(_InvoiceID)
        //            End If
        //        End If
        //        Return _Invoice
        //    End Get
        //    Protected Set(ByVal value As RescueTekniq.BOL.InvoiceHeader)
        //        _Invoice = value
        //    End Set
        //End Property
        
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
                if (Incasso.Invoice.EANInvoice)
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
            if (!Incasso.loaded)
            {
                //RaiseEvent InvoiceNotExist()
                //LoadInvoice(InvoiceID)
            }
            
            // Create a new MigraDoc document
            this.document = new Document();
            var with_1 = this.document.Info;
            with_1.Title = "";
            with_1.Subject = "";
            
            with_1.Title += string.Format("Incasso varsel {1} på faktura nr. {0}", Incasso.Invoice.ID, Incasso.ID);
            with_1.Subject += EANtitle + "Faktura på hjertestarter og evt tilbehør.";
            
            with_1.Author = Config.Company.name; // "RescueTekniq"
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
            with_7.Borders.Color = RescueTekniq.Doc.IncassoForm_Dk.TableBorder;
            with_7.Borders.Width = 0.25;
            with_7.Borders.Left.Width = 0.5;
            with_7.Borders.Right.Width = 0.5;
            with_7.Alignment = ParagraphAlignment.Center;
            paragraph.AddText(Config.Company.name);
            paragraph.AddText(" · ");
            
            paragraph.AddText(Config.Company.adresse);
            
            paragraph.AddText(" · ");
            paragraph.AddText(Config.Company.zipcode);
            paragraph.AddText(" ");
            paragraph.AddText(Config.Company.city);
            if (this.Incasso.Virksomhed.LandekodeID != 45)
            {
                paragraph.AddText(" · ");
                paragraph.AddText(Config.Company.country);
            }
            paragraph.AddText(" · ");
            paragraph.AddText("CVR.nr. ");
            paragraph.AddText(Config.Company.vatno);
            paragraph.AddText(" · ");
            if (this.Incasso.Virksomhed.LandekodeID != 45)
            {
                paragraph.AddText("Pho. ");
                paragraph.AddText(Config.Company.int_phone);
            }
            else
            {
                paragraph.AddText("Tlf. ");
                paragraph.AddText(Config.Company.phone);
            }
            paragraph.AddText(" · ");
            Hyperlink link = paragraph.AddHyperlink("http://" + Config.Company.link, HyperlinkType.Web);
            link.AddFormattedText(Config.Company.link);
            
            var with_8 = section.Footers.Primary.AddParagraph();
            var with_9 = with_8.Format;
            with_9.Font.Size = 4;
            with_9.Alignment = ParagraphAlignment.Center;
            with_8.AddLineBreak();
            with_8.AddLineBreak();
            
            with_8.AddText(string.Format("- {0}Incasso varsel nr. {1}", EANtitle, Incasso.ID));
            if (IsCopy)
            {
                with_8.AddText(" kopi");
            }
            
            with_8.AddText(" - side ");
            with_8.AddPageField();
            with_8.AddText(" af ");
            with_8.AddNumPagesField();
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
            string Cvrnr = Config.Company.vatno;
            
            paragraph.Style = "Reference";
            //.Format.Alignment = ParagraphAlignment.Right
            
            //Select Case Invoice.Status
            //    Case Invoice_StatusEnum.IncassoNotis
            //        '.AddFormattedText("Incasso varsel", TextFormat.Bold)
            //    Case Invoice_StatusEnum.FirstReminder
            //        .AddFormattedText(String.Format("1. Rykker på faktura nr.{0}", Invoice.ID), TextFormat.Bold)
            //    Case Else
            //        .AddFormattedText(String.Format("{0}Faktura nr. {1}", EANtitle, Invoice.ID), TextFormat.Bold)
            //        If IsCopy Then
            //            .AddFormattedText(" - kopi", TextFormat.Bold)
            //        End If
            //End Select
            //.AddLineBreak()
            
            paragraph.AddText("Dato: ");
            paragraph.AddDateField("yyyy-MM-dd");
            paragraph.AddLineBreak();
            
            paragraph.AddText("Sagsnr.: ");
            paragraph.AddFormattedText(string.Format("{0}", Incasso.ID), TextFormat.Bold);
            paragraph.AddLineBreak();
            
            paragraph.AddText("Kreditor: ");
            paragraph.AddFormattedText(Config.Company.name, TextFormat.Bold);
            paragraph.AddLineBreak();
            
            paragraph.AddLineBreak();
            
            
            
            // Add the print date field
            paragraph = section.AddParagraph();
            paragraph.Format.SpaceBefore = "3cm";
            paragraph.Style = "Reference";
            
            paragraph.AddFormattedText(string.Format("Incasso varsel - Vedr. faktura nr. {0}", Incasso.Invoice.ID), TextFormat.Bold);
            paragraph.AddLineBreak();
            
            // Add the print date field
            paragraph = section.AddParagraph();
            paragraph.Style = "Reference";
            paragraph.AddText("Vi skal hermed rykke Dem for betaling af Deres gæld, som nu er opgjort til:");
            //Select Case Invoice.Status
            //    Case Invoice_StatusEnum.IncassoNotis
            //        .AddText(String.Format("{0} har bedt os om at rykke Dem for betaling af Deres gæld, som nu er opgjort til:", Config.Company.name))
            //    Case Invoice_StatusEnum.FirstReminder
            //        .AddText(String.Format("Hermed skal vi bringe vort tilgodehavende i henhold til ovennævnte faktura nr. {0} i erindring – da vi ikke kan se at denne er betalt", Invoice.ID))
            //        ' – og vi skal herved erindre om indbetaling af nedennævnte beløb.", Invoice.ID))
            //        .AddLineBreak()
            //        .AddText("Såfremt beløbet er indbetalt inden modtagelse af denne skrivelse – bedes De bortse fra denne.")
            //        .AddLineBreak()
            //    Case Else
            //        .AddText("I henhold til aftale fremsendes hermed faktura på:")
            //End Select
            
            
            // Create the item table
            this.table = section.AddTable();
            this.table.Style = "Table";
            //.Borders.Color = TableBorder
            this.table.Borders.Width = 0; //0.25 '0.25
            this.table.Borders.Left.Width = 0; //0.5
            this.table.Borders.Right.Width = 0; //0.5
            this.table.Rows.LeftIndent = 0;
            this.table.Format.Font.Size = "8pt";
            this.table.Format.Alignment = ParagraphAlignment.Center;
            
            // Before you can add a row, you must define the columns
            //Dim column As Column
            
            var with_15 = this.table.AddColumn("1cm");
            with_15.Format.Alignment = ParagraphAlignment.Left;
            var with_16 = this.table.AddColumn("12,5cm");
            with_16.Format.Alignment = ParagraphAlignment.Left;
            
            var with_17 = this.table.AddColumn("2,5cm");
            with_17.Format.Alignment = ParagraphAlignment.Right;
            
            
            //' Create the header of the table
            //Dim row As Row
            //row = table.AddRow()
            //With row
            //    .HeadingFormat = True
            //    .Format.Alignment = ParagraphAlignment.Center
            //    .Format.Font.Bold = True
            //    .Shading.Color = TableBlue
            
            //    .Cells(0).AddParagraph("Vare nr")
            //    .Cells(0).Format.Alignment = ParagraphAlignment.Left
            
            //    .Cells(1).AddParagraph("i alt")
            //    .Cells(1).Format.Alignment = ParagraphAlignment.Right
            
            //End With
            
            //Me.table.SetEdge(0, 0, 7, 1, Edge.Box, BorderStyle.[Single], 0.75, Color.Empty)
        }
        
#endregion
        
#region  Fill Page with Data
        
        /// <summary>
        /// Creates the dynamic parts of the invoice.
        /// </summary>
        private void FillContent()
        {
            FillContentHeader();
            //FillContentHeader_Delevery()
            
            FillContentLines();
            
            //FillContentBottom()
            
            addBesked();
            
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
            
            if (Incasso.Invoice.EANInvoice)
            {
                with_1.AddText("EAN: ");
                with_1.AddFormattedText(Incasso.Invoice.InvoiceEANno, TextFormat.Bold);
                with_1.AddLineBreak();
            }
            with_1.AddText(this.Incasso.Invoice.ContactName);
            with_1.AddLineBreak();
            with_1.AddText(this.Incasso.Invoice.InvoiceName);
            with_1.AddLineBreak();
            with_1.AddText(this.Incasso.Invoice.InvoiceAddress1);
            with_1.AddLineBreak();
            if (this.Incasso.Invoice.InvoiceAddress2.Trim() != "")
            {
                with_1.AddText(this.Incasso.Invoice.InvoiceAddress2);
                with_1.AddLineBreak();
            }
            with_1.AddText(this.Incasso.Invoice.InvoicePostnrBy);
            with_1.AddLineBreak();
            if (this.Incasso.Invoice.InvoiceCountry != "" && this.Incasso.Invoice.InvoiceCountry.ToLower() != "danmark")
            {
                with_1.AddText(this.Incasso.Invoice.InvoiceCountry);
                with_1.AddLineBreak();
            }
            if (Incasso.Invoice.InvoiceAtt.Trim() != "")
            {
                with_1.AddText("Att. : ");
                with_1.AddText(this.Incasso.Invoice.InvoiceAtt);
                with_1.AddLineBreak();
            }
        }
        
        /// <summary>
        /// Creates the dynamic parts of the invoice.
        /// </summary>
        private void FillContentHeader_Delevery()
        {
            return;
//            if (this.Incasso.Invoice.DeleveryName == "")
//            {
//                return;
//                }
                
                // Fill address in address text frame
//                var with_1 = this.deleveryFrame.AddParagraph();
//                with_1.Format.Font.Size = "9pt";
//                with_1.AddFormattedText("Leveringsadresse", TextFormat.Bold);
//                with_1.AddLineBreak();
                
//                if (Incasso.Invoice.EANInvoice && Incasso.Invoice.DeleveryEANno.Trim() != "")
//                {
//                    with_1.AddText("EAN: ");
//                    with_1.AddFormattedText(Incasso.Invoice.DeleveryEANno, TextFormat.Bold);
//                    with_1.AddLineBreak();
//                    }
                    
//                    with_1.AddText(this.Incasso.Invoice.DeleveryName); //.Virksomhed.Firmanavn
//                    with_1.AddLineBreak();
//                    with_1.AddText(this.Incasso.Invoice.DeleveryAddress1); //.Virksomhed.Adresse1
//                    with_1.AddLineBreak();
//                    if (this.Incasso.Invoice.DeleveryAddress2.Trim() != "") //.Virksomhed.Adresse2
//                    {
//                        with_1.AddText(this.Incasso.Invoice.DeleveryAddress2); //.Virksomhed.Adresse2
//                        with_1.AddLineBreak();
//                        }
//                        with_1.AddText(this.Incasso.Invoice.DeleveryPostnrBy); //.Virksomhed.PostnrBy
//                        with_1.AddLineBreak();
//                        if (this.Incasso.Invoice.DeleveryCountry != "" && this.Incasso.Invoice.DeleveryCountry.ToLower() != "danmark") //.Virksomhed.LandekodeID <> 45
//                        {
//                            with_1.AddText(this.Incasso.Invoice.DeleveryCountry);
//                            with_1.AddLineBreak();
//                            }
//                            if (Incasso.Invoice.DeleveryAtt.Trim() != "")
//                            {
//                                with_1.AddText("Att. : ");
//                                with_1.AddText(this.Incasso.Invoice.DeleveryAtt);
//                                with_1.AddLineBreak();
//                                }
                            }
                            
                            private void FillContentLines()
                            {
                                //Dim paragraph As Paragraph
                                
                                
                                // Iterate the invoice items
                                totalExtendedPrice = (double) Incasso.Invoice.InvoiceTotalInclVAT;
                                
                                double RenterOpgjortAfKreditor = Incasso.RenterOpgjortAfKreditor;
                                double GebyrOphjortAfKreditor = Incasso.GebyrOpgjortAfKreditor;
                                double RenteTilskrevetFraPaymentDatetilDD = Incasso.RenteTilskrevetFraPaymentDatetilDD;
                                double GebyrForDenneRykkerskrivelse = Incasso.GebyrForDenneRykkerskrivelse;
                                
                                addRow(this.table.AddRow(), "Skyldigt beløb", (decimal) totalExtendedPrice, false);
                                addRow(this.table.AddRow(), "Renter opgjort af kreditor", (decimal) RenterOpgjortAfKreditor, false);
                                addRow(this.table.AddRow(), "Gebyr opgjort af kreditor i henhold til aftale eller renteloven", (decimal) GebyrOphjortAfKreditor, false);
                                addRow(this.table.AddRow(), string.Format("Rente tilskrevet fra {0:yyyy-MM-dd} til dags dato", Incasso.Invoice.PaymentDate), (decimal) RenteTilskrevetFraPaymentDatetilDD, false);
                                addRow(this.table.AddRow(), "Gebyr for denne rykkerskrivelse", (decimal) GebyrForDenneRykkerskrivelse, false);
                                
                                totalExtendedPrice += RenterOpgjortAfKreditor;
                                totalExtendedPrice += GebyrOphjortAfKreditor;
                                totalExtendedPrice += RenteTilskrevetFraPaymentDatetilDD;
                                totalExtendedPrice += GebyrForDenneRykkerskrivelse;
                                
                                addRow(this.table.AddRow(), "I alt at betale", (decimal) totalExtendedPrice, true);
                                
                            }
                            
                            private void FillContentBottom()
                            {
                                
                                decimal moms = 25;
                                string InvoceVat = Config.Invoice.VAT;
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
                                
                                // Add the total price row
                                TotalRow(this.table.AddRow(), "Total pris", (decimal) totalExtendedPrice);
                                
                                // '' Add the additional transport fee row
                                //'TotalRow(Me.table.AddRow(), "Transport", Invoice.Transport)
                                //'totalExtendedPrice += Invoice.Transport
                                
                                if (!Incasso.Virksomhed.VATFree)
                                {
                                    momsbelob = (decimal) (totalExtendedPrice * (double) (moms / 100));
                                    // Add the VAT row
                                    TotalRow(this.table.AddRow(), "Moms (" + (moms / 100).ToString("P") + ")", momsbelob);
                                }
                                
                                // Add the total due row
                                totalExtendedPrice += (double) momsbelob;
                                TotalRow(this.table.AddRow(), "Totalt", (decimal) totalExtendedPrice, true);
                                
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
                                
                                with_1.AddFormattedText("Gælden vedrører:", TextFormat.Bold);
                                with_1.AddLineBreak();
                                with_1.AddText(string.Format("Krav i henhold til faktura nr. {0} udstedt den {1:yyyy-MM-dd} med forfald den {2:yyyy-MM-dd}", this.Incasso.Invoice.ID, this.Incasso.Invoice.InvoiceDate, this.Incasso.Invoice.PaymentDate));
                                with_1.AddLineBreak();
                                
                                var with_2 = LastSec.AddParagraph(); //paragraph
                                with_2.Format.SpaceBefore = "5mm";
                                //.Format.Font.Size = 7
                                
                                with_2.AddFormattedText(string.Format("Betaling af hele beløbet skal ske senest 10 dage fra dato:"), TextFormat.Bold);
                                with_2.AddLineBreak();
                                with_2.AddText(string.Format("Såfremt dette ikke er sket, vil vi automatisk starte en inkassosag imod Dem. Det vil øge Deres gæld med et salær på {0:c}. Hertil kommer {1:c} i oversendelsesgebyr samt fortsat rentetilskrivning.", Incasso.IncassoSalaer, Incasso.Oversendelsesgebyr));
                                with_2.AddLineBreak(); //
                                with_2.AddLineBreak();
                                
                                //.AddText(String.Format("Hvis De har indsigelser eller har betalt, skal De rette henvendelse til"))
                                //.AddLineBreak()
                                //.AddText(String.Format("  {0}", Config.Company.name))
                                //.AddLineBreak()
                                //.AddText(String.Format("  {0}", Config.Company.adresse))
                                //.AddLineBreak()
                                //.AddText(String.Format("  {0}  {1}", Config.Company.zipcode, Config.Company.city))
                                //.AddLineBreak()
                                //.AddText(String.Format("  Telefon: {0}", Config.Company.phone))
                                //.AddLineBreak()
                                //.AddLineBreak()
                                //.AddText(String.Format("Det er kun {0} som kan forhindre inkassosagen i at blive anlagt, hvis vi ikke har registreret Deres betaling af {1:c} senest 10 dage fra dato.", Config.Company.name, totalExtendedPrice))
                                //.AddLineBreak()
                                
                                
                                var with_3 = LastSec.AddParagraph();
                                with_3.Format.SpaceBefore = "5mm";
                                //.Format.Font.Size = 7
                                
                                with_3.AddText("Benyt venligst følgende referencer ved indbetaling");
                                with_3.AddLineBreak();
                                
                                with_3.AddText("   Reg. Nr. ");
                                with_3.AddFormattedText(Config.Bank.RegNo, TextFormat.Bold);
                                with_3.AddText(" Kontonr. ");
                                with_3.AddFormattedText(Config.Bank.AccountNo, TextFormat.Bold);
                                with_3.AddLineBreak();
                                
                                with_3.AddText("   Swift. ");
                                with_3.AddFormattedText(Config.Bank.SWIFT, TextFormat.Bold);
                                //.AddLineBreak()
                                with_3.AddText(", ");
                                with_3.AddText("IBAN. ");
                                with_3.AddFormattedText(Config.Bank.IBAN, TextFormat.Bold);
                                with_3.AddLineBreak();
                                
                                with_3.AddText(string.Format("   {0}", Config.Bank.Name));
                                //.AddText(", ")
                                //.AddText(Config.Bank.Address)
                                //.AddLineBreak()
                                with_3.AddLineBreak();
                                
                                with_3.AddText("Besked til modtager : ");
                                with_3.AddFormattedText(string.Format("Sagsnr. {0} ", Incasso.ID), TextFormat.Bold);
                                with_3.AddLineBreak();
                                
                                
                                var with_4 = LastSec.AddParagraph();
                                with_4.Format.SpaceBefore = "5mm";
                                //.Format.Font.Size = 7
                                with_4.Format.Alignment = ParagraphAlignment.Center;
                                with_4.AddText("Med venlig hilsen");
                                with_4.AddLineBreak();
                                var with_5 = LastSec.AddParagraph();
                                with_5.Format.Alignment = ParagraphAlignment.Center;
                                with_5.AddFormattedText(Config.Company.name, TextFormat.Bold);
                                with_5.AddLineBreak();
                                
                            }
                            
                            private void AddNoter()
                            {
                                Section LastSec = this.document.LastSection;
                                
                                if (Incasso.Notes.Trim() != "")
                                {
                                    
                                    // Add the notes paragraph
                                    var with_1 = LastSec.AddParagraph();
                                    var with_2 = with_1.Format;
                                    with_2.SpaceBefore = "5mm";
                                    with_2.Borders.Width = 0.75;
                                    with_2.Borders.Distance = 3;
                                    with_2.Borders.Color = RescueTekniq.Doc.IncassoForm_Dk.TableBorder;
                                    with_2.Shading.Color = RescueTekniq.Doc.IncassoForm_Dk.TableGray;
                                    with_2.KeepTogether = true;
                                    with_1.AddText(Incasso.Notes);
                                    
                                }
                            }
                            
                            
                            private void addRow(Row row, string tekst, decimal value, bool Bold = false)
                            {
                                
                                var with_1 = row;
                                var with_2 = with_1.Cells[0];
                                with_2.Borders.Visible = false;
                                var with_3 = with_1.Cells[1];
                                with_3.AddParagraph(tekst);
                                with_3.Borders.Visible = false;
                                with_3.Format.Font.Bold = Bold; //False
                                with_3.Format.Alignment = ParagraphAlignment.Left;
                                //.MergeRight = 5
                                var with_4 = with_1.Cells[2];
                                with_4.Format.Borders.ClearAll();
                                if (Bold)
                                {
                                    with_4.Format.Borders.Top.Color = new Color((byte) 0, (byte) 0, (byte) 0); //TableBorder
                                    with_4.Format.Borders.Top.Width = "0.25";
                                    with_4.Format.Borders.Bottom.Color = new Color((byte) 0, (byte) 0, (byte) 0); //TableBorder
                                    with_4.Format.Borders.Bottom.Width = "0.5";
                                }
                                //.AddParagraph(value.ToString("#,##0.00")) ' & " kr."
                                var with_5 = with_4.AddParagraph();
                                if (Bold)
                                {
                                    with_5.Format.Font.Bold = true;
                                }
                                //.Format.Font.Size = 8
                                with_5.AddText(value.ToString("#,##0.00 DKK"));
                                
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
                                with_4.AddText(value.ToString("#,##0.00"));
                                
                            }
                            
                            
#endregion
                            
                        }
                        
                        //End Namespace
                        
                    }
