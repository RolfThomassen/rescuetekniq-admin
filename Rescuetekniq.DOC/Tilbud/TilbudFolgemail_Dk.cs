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
    
    //Namespace Tilbud
    
    /// <summary>
    /// Creates the Tilbuds følgemail form.
    /// </summary>
    public class TilbudFolgemail_Dk
    {
        
#region  Privates
        
        /// <summary>
        /// The MigraDoc document that represents the invoice.
        /// </summary>
        private Document document;
        
        /// <summary>
        /// An XML invoice based on a sample created with Microsoft InfoPath.
        /// </summary>
        readonly XmlDocument invoice;
        
        /// <summary>
        /// The root navigator for the XML document.
        /// </summary>
        readonly XPathNavigator navigator;
        
        /// <summary>
        /// The text frame of the MigraDoc document that contains the address.
        /// </summary>
        private TextFrame addressFrame;
        
        /// <summary>
        /// The text frame of the MigraDoc document that contains the header (top/right).
        /// </summary>
        private TextFrame headerFrame;
        
        /// <summary>
        /// The table of the MigraDoc document that contains the invoice items.
        /// </summary>
        private Table table;
        
        /// <summary>
        /// Class contains all info about Tilbud og tilbudslinier
        /// </summary>
        private tilbudHeader tilbud = new tilbudHeader();
        
        /// <summary>
        /// total sum Price
        /// </summary>
        private double totalExtendedPrice = 0;
        
        /// <summary>
        /// Tilbud's ID
        /// </summary>
        /// <remarks></remarks>
        private int _TilbudID = -1;
        
        /// <summary>
        /// Path for logo image file
        /// </summary>
        private string _Logo = "logo.gif";
        
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
        /// Initializes a new instance of the class TilbudFolgemail and opens the specified Tilbud from TilbudID.
        /// </summary>
        public TilbudFolgemail_Dk(int TilbudID)
        {
            this.TilbudID = TilbudID;
        }
        public TilbudFolgemail_Dk()
        {
            
        }
        
#endregion
        
#region  Properties
        
        public int TilbudID
        {
            get
            {
                return _TilbudID;
            }
            set
            {
                _TilbudID = value;
                if (tilbud.loaded)
                {
                    if (tilbud.ID != _TilbudID)
                    {
                        LoadTilbud(_TilbudID);
                    }
                }
                else
                {
                    LoadTilbud(_TilbudID);
                }
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
        
#endregion
        
#region  Metodes
        
        public void LoadTilbud(int TilbudID)
        {
            this.tilbud = RescueTekniq.BOL.tilbudHeader.GetTilbud(TilbudID);
        }
        
#endregion
        
#region  Create Document
        
        
        /// <summary>
        /// Creates the invoice document.
        /// </summary>
        public Document CreateDocument()
        {
            if (!tilbud.loaded)
            {
                LoadTilbud(TilbudID);
            }
            
            // Create a new MigraDoc document
            this.document = new Document();
            var with_1 = this.document.Info;
            with_1.Title = "Tilbud nr. " + System.Convert.ToString(TilbudID);
            with_1.Subject = "Tilbud på hjertestarter og evt tilbehør.";
            with_1.Author = "Snowball Service";
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
            style.Font.Size = 9;
            
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
            //.Height = "3.0cm"
            this.addressFrame.Width = "8.0cm";
            this.addressFrame.Left = ShapePosition.Left;
            this.addressFrame.RelativeHorizontal = RelativeHorizontal.Margin;
            this.addressFrame.Top = "2.0cm";
            this.addressFrame.RelativeVertical = RelativeVertical.Page;
            
            
            // Create footer
            Paragraph paragraph = section.Footers.Primary.AddParagraph();
            var with_6 = paragraph.Format;
            with_6.SpaceBefore = "1cm";
            with_6.Font.Size = 7;
            with_6.Borders.Color = RescueTekniq.Doc.TilbudFolgemail_Dk.TableBorder;
            with_6.Borders.Width = 0.25;
            with_6.Borders.Left.Width = 0.5;
            with_6.Borders.Right.Width = 0.5;
            with_6.Alignment = ParagraphAlignment.Center;
            paragraph.AddText(Company.name);
            paragraph.AddText(" · ");
            paragraph.AddText(Company.adresse);
            paragraph.AddText(" · ");
            paragraph.AddText(Company.zipcode);
            paragraph.AddText(" ");
            paragraph.AddText(Company.city);
            if (this.tilbud.Virksomhed.LandekodeID != 45)
            {
                paragraph.AddText(" · ");
                paragraph.AddText(Company.country);
            }
            paragraph.AddText(" · ");
            paragraph.AddText("CVR.nr. ");
            paragraph.AddText(Company.vatno);
            paragraph.AddText(" · ");
            if (this.tilbud.Virksomhed.LandekodeID != 45)
            {
                paragraph.AddText("Pho. ");
                paragraph.AddText(Company.int_phone);
            }
            else
            {
                paragraph.AddText("Tlf. ");
                paragraph.AddText(Company.phone);
            }
            paragraph.AddText(" · ");
            Hyperlink link = paragraph.AddHyperlink(string.Format("http://{0}", Company.link), HyperlinkType.Web);
            link.AddFormattedText(Company.link);
            
            
            var with_7 = section.Footers.Primary.AddParagraph();
            var with_8 = with_7.Format;
            with_8.Font.Size = 4;
            with_8.Alignment = ParagraphAlignment.Center;
            with_7.AddLineBreak();
            with_7.AddLineBreak();
            with_7.AddText("- TILBUD nr. " + tilbud.ID.ToString());
            with_7.AddText(" - side ");
            with_7.AddPageField();
            with_7.AddText(" af ");
            with_7.AddNumPagesField();
            with_7.AddText(" - ");
            
            // Create the text frame for the address
            this.headerFrame = section.AddTextFrame();
            this.headerFrame.Height = "2.0cm";
            this.headerFrame.Width = "4.0cm";
            this.headerFrame.Left = ShapePosition.Right;
            this.headerFrame.RelativeHorizontal = RelativeHorizontal.Margin;
            this.headerFrame.Top = "4.5cm";
            this.headerFrame.RelativeVertical = RelativeVertical.Page;
            // Add the print date field
            paragraph = this.headerFrame.AddParagraph();
            paragraph.Style = "Reference";
            //.Format.Alignment = ParagraphAlignment.Right
            paragraph.AddFormattedText("TILBUD nr. " + tilbud.ID.ToString(), TextFormat.Bold);
            paragraph.AddLineBreak();
            paragraph.AddText(Company.city); //("Hillerød, ")
            paragraph.AddText(", ");
            paragraph.AddDateField("dd.MM.yyyy");
            paragraph.AddLineBreak();
            paragraph.AddText(System.Convert.ToString("/" + System.Convert.ToString(this.tilbud.Ansvarlig != "" ? this.tilbud.Ansvarlig : "LFU")));
            
            paragraph.AddLineBreak();
            paragraph.AddLineBreak();
            paragraph.AddText("Side ");
            paragraph.AddPageField();
            paragraph.AddText(" af ");
            paragraph.AddNumPagesField();
            
            
            //' Put sender in address frame
            //paragraph = Me.addressFrame.AddParagraph()
            //With paragraph
            //    .AddText("Heart2Start Aps · Gefionsvej 8 · 3400 Hillerød")
            //    .Format.Font.Name = "Times New Roman"
            //    .Format.Font.Size = 7
            //    .Format.SpaceAfter = 3
            //End With
            
            // Add the print date field
            //paragraph = section.AddParagraph()
            //With paragraph
            //    .Format.SpaceBefore = "5cm"
            //    .Style = "Reference"
            //    .AddFormattedText("TILBUD nr. " & tilbud.ID.ToString, TextFormat.Bold)
            //    .AddTab()
            //    .AddText("Hillerød, ")
            //    .AddDateField("dd.MM.yyyy")
            //    .Format.Font.Color = RGB(255, 0, 0)
            //End With
            
            // Add the print date field
            paragraph = section.AddParagraph();
            paragraph.Format.SpaceBefore = "3cm";
            paragraph.Style = "Reference";
            paragraph.AddFormattedText("Fremsendelse af tilbud", TextFormat.Bold);
            paragraph.AddLineBreak();
            // Add the print date field
            paragraph = section.AddParagraph();
            paragraph.Style = "Reference";
            paragraph.AddText("I henhold til aftale fremsendes hermed tilbud på:");
            
            // Create the item table
            this.table = section.AddTable();
            this.table.Style = "Table";
            this.table.Borders.Color = RescueTekniq.Doc.TilbudFolgemail_Dk.TableBorder;
            this.table.Borders.Width = 0.25;
            this.table.Borders.Left.Width = 0.5;
            this.table.Borders.Right.Width = 0.5;
            this.table.Rows.LeftIndent = 0;
            
            // Before you can add a row, you must define the columns
            Column column;
            column = this.table.AddColumn("1cm"); //1cm - Antal
            column.Format.Alignment = ParagraphAlignment.Center;
            
            column = this.table.AddColumn("7,75cm"); //3cm - Beskrivelse
            column.Format.Alignment = ParagraphAlignment.Right;
            
            column = this.table.AddColumn("1,75cm"); //3cm - Stk. pris DKK
            column.Format.Alignment = ParagraphAlignment.Right;
            
            column = this.table.AddColumn("1,75cm"); //3cm - Reduktion
            column.Format.Alignment = ParagraphAlignment.Right;
            
            column = this.table.AddColumn("1,75cm"); //3cm - á pris
            column.Format.Alignment = ParagraphAlignment.Right;
            
            column = this.table.AddColumn("2cm"); //3cm - i alt
            column.Format.Alignment = ParagraphAlignment.Right;
            //   1+(5*3) = 16 cm | 1+7,5+2+1+2+2,5
            
            // Create the header of the table
            Row row = default(Row);
            //row = table.AddRow()
            //With row
            //    .HeadingFormat = True
            //    .Format.Alignment = ParagraphAlignment.Center
            //    .Format.Font.Bold = True
            //    .Shading.Color = TableBlue
            //    .Cells(0).AddParagraph("Antal")
            //    .Cells(0).Format.Font.Bold = False
            //    .Cells(0).Format.Alignment = ParagraphAlignment.Left
            //    .Cells(0).VerticalAlignment = VerticalAlignment.Bottom
            //    .Cells(0).MergeDown = 1
            
            //    .Cells(1).AddParagraph("Beskrivelse")
            //    .Cells(1).Format.Alignment = ParagraphAlignment.Left
            //    .Cells(1).MergeRight = 3
            
            //    .Cells(5).AddParagraph("i alt")
            //    .Cells(5).Format.Alignment = ParagraphAlignment.Right 'Left
            //    .Cells(5).VerticalAlignment = VerticalAlignment.Bottom
            //    .Cells(5).MergeDown = 1
            //End With
            
            row = table.AddRow();
            row.HeadingFormat = true;
            row.Format.Alignment = ParagraphAlignment.Center;
            row.Format.Font.Bold = true;
            row.Shading.Color = RescueTekniq.Doc.TilbudFolgemail_Dk.TableBlue;
            
            row.Cells[0].AddParagraph("Antal");
            row.Cells[0].Format.Font.Bold = false;
            row.Cells[0].Format.Alignment = ParagraphAlignment.Left;
            row.Cells[0].VerticalAlignment = VerticalAlignment.Bottom;
            //.Cells(0).MergeDown = 1
            
            row.Cells[1].AddParagraph("Beskrivelse");
            row.Cells[1].Format.Alignment = ParagraphAlignment.Left;
            
            row.Cells[2].AddParagraph("Stk. pris "); //DKK
            row.Cells[2].Format.Alignment = ParagraphAlignment.Left;
            
            row.Cells[3].AddParagraph("Reduktion"); // (%)Ambassadør - Reduktion
            row.Cells[3].Format.Alignment = ParagraphAlignment.Left;
            
            row.Cells[4].AddParagraph("á pris");
            row.Cells[4].Format.Alignment = ParagraphAlignment.Left;
            
            row.Cells[5].AddParagraph("i alt");
            row.Cells[5].Format.Alignment = ParagraphAlignment.Right; //Left
            row.Cells[5].VerticalAlignment = VerticalAlignment.Bottom;
            //.Cells(5).MergeDown = 1
            
            
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
            with_1.AddText(this.tilbud.Virksomhed.Firmanavn);
            with_1.AddLineBreak();
            with_1.AddText(this.tilbud.Virksomhed.Adresse1);
            with_1.AddLineBreak();
            if (this.tilbud.Virksomhed.Adresse2.Trim() != "")
            {
                with_1.AddText(this.tilbud.Virksomhed.Adresse2);
                with_1.AddLineBreak();
            }
            with_1.AddText(this.tilbud.Virksomhed.PostnrBy);
            with_1.AddLineBreak();
            if (this.tilbud.Virksomhed.LandekodeID != 45)
            {
                with_1.AddText(this.tilbud.Virksomhed.Land);
                with_1.AddLineBreak();
            }
            if (tilbud.KontaktPerson.Trim() != "")
            {
                with_1.AddText("Att. : ");
                //.AddText(Me.tilbud.Virksomhed.BeslutNavn)
                with_1.AddText(this.tilbud.KontaktPerson);
                with_1.AddLineBreak();
            }
        }
        
        private void FillContentLines()
        {
            Paragraph paragraph = default(Paragraph);
            
            // Iterate the invoice items
            totalExtendedPrice = 0;
            
            foreach (TilbudsLinie linie in tilbud.Tilbudslinier)
            {
                double quantity = linie.Antal;
                double price = (double) linie.Salgspris; //Tilbudspris
                double discount = (double) linie.Rabat;
                double tilbudspris = (double) linie.Tilbudspris;
                
                double extendedPrice = quantity * tilbudspris;
                //extendedPrice = extendedPrice * (100 - discount) / 100
                totalExtendedPrice += extendedPrice;
                
                // Each item fills two rows
                //Dim row1 As Row = Me.table.AddRow()
                Row row2 = this.table.AddRow();
                
                //With row1
                //    .TopPadding = 1.5
                
                //    '.Cells(0).Shading.Color = TableGray
                //    '.Cells(0).VerticalAlignment = VerticalAlignment.Center
                //    '.Cells(0).MergeDown = 1
                //    '.Cells(0).AddParagraph(linie.Pos)
                
                //    '.Cells(1).Format.Alignment = ParagraphAlignment.Left
                //    '.Cells(1).MergeRight = 3
                //    'paragraph = .Cells(1).AddParagraph()
                //    'paragraph.AddFormattedText(linie.VareNr, TextFormat.Bold)
                //    'paragraph.AddText(" : ")
                //    ''paragraph.AddFormattedText(" : ", TextFormat.Italic)
                //    'paragraph.AddText(linie.VareNavn)
                
                //    '.Cells(5).Shading.Color = TableGray
                //    '.Cells(5).MergeDown = 1
                //    '.Cells(5).AddParagraph(extendedPrice.ToString("#,##0.00") & " kr.")
                //    '.Cells(5).VerticalAlignment = VerticalAlignment.Bottom
                //End With
                
                row2.Cells[0].Shading.Color = RescueTekniq.Doc.TilbudFolgemail_Dk.TableGray;
                row2.Cells[0].VerticalAlignment = VerticalAlignment.Center;
                row2.Cells[0].AddParagraph(System.Convert.ToString(linie.Antal));
                
                row2.Cells[1].Format.Alignment = ParagraphAlignment.Left;
                //.Cells(1).MergeRight = 3
                paragraph = row2.Cells[1].AddParagraph();
                paragraph.Format.Font.Size = 8;
                paragraph.AddFormattedText(linie.VareNr, TextFormat.Bold);
                paragraph.AddText(" : ");
                paragraph.AddText(linie.VareNavn);
                
                row2.Cells[2].AddParagraph(price.ToString("#,##0.00")); //& " kr."
                if (discount != 0)
                {
                    row2.Cells[3].AddParagraph(discount.ToString("#,##0.00") + "%");
                }
                else
                {
                    row2.Cells[3].AddParagraph("");
                }
                
                //(price - (price * (discount / 100))).ToString("#,##0.00")
                row2.Cells[4].AddParagraph(tilbudspris.ToString("#,##0.00")); // & " kr."
                
                row2.Cells[5].Shading.Color = RescueTekniq.Doc.TilbudFolgemail_Dk.TableGray;
                //.Cells(5).MergeDown = 1
                row2.Cells[5].AddParagraph(extendedPrice.ToString("#,##0.00")); // & " kr."
                //.Cells(5).VerticalAlignment = VerticalAlignment.Bottom
                
                this.table.SetEdge(0, this.table.Rows.Count - 2, 6, 1, Edge.Box, BorderStyle.Single, 0.75);
            }
        }
        
        private void FillContentBottom()
        {
            decimal moms = 25;
            decimal momsbelob = 0;
            
            // Add an invisible row as a space line to the table
            Row row = this.table.AddRow();
            row.Borders.Visible = false;
            
            // Add the total price row
            TotalRow(this.table.AddRow(), "Total pris", (decimal) totalExtendedPrice);
            
            // Add the additional transport fee row
            TotalRow(this.table.AddRow(), "Transport", tilbud.Transport);
            totalExtendedPrice += (double) tilbud.Transport;
            
            if (!tilbud.Virksomhed.VATFree)
            {
                momsbelob = (decimal) (totalExtendedPrice * (double) (moms / 100));
                // Add the VAT row
                TotalRow(this.table.AddRow(), "Moms (" + (moms / 100).ToString("P") + ")", momsbelob);
            }
            
            // Add the total due row
            totalExtendedPrice += (double) momsbelob;
            TotalRow(this.table.AddRow(), "Totalt", (decimal) totalExtendedPrice);
            
            // Set the borders of the specified cell range
            this.table.SetEdge(5, this.table.Rows.Count - 4, 1, 4, Edge.Box, BorderStyle.Single, 0.75);
            
            addBesked();
            
            AddNoter();
        }
        
        private void TotalRow(Row row, string tekst, decimal value)
        {
            var with_1 = row;
            var with_2 = with_1.Cells[0];
            with_2.AddParagraph(tekst);
            with_2.Borders.Visible = false;
            with_2.Format.Font.Bold = true;
            with_2.Format.Alignment = ParagraphAlignment.Right;
            with_2.MergeRight = 4;
            var with_3 = with_1.Cells[5];
            with_3.AddParagraph(value.ToString("#,##0.00")); // & " kr."
        }
        
        private void addBesked()
        {
            Paragraph paragraph = default(Paragraph);
            
            // Add the notes paragraph
            paragraph = this.document.LastSection.AddParagraph();
            //.Format.Font.Size = 7
            //With .Format
            //    .Font.Size = 8
            paragraph.Format.SpaceBefore = "5mm";
            //    '.Borders.Width = 0.75
            //    '.Borders.Distance = 3
            //    '.Borders.Color = TableBorder
            //    '.Shading.Color = TableGray
            //End With
            paragraph.AddText("Dette tilbud er gældende i 3 uger fra d.d.");
            paragraph.AddLineBreak();
            
            paragraph = this.document.LastSection.AddParagraph();
            paragraph.Format.SpaceBefore = "5mm";
            //.Format.Font.Size = 7
            
            paragraph.AddText("Accept af tilbud kan foretages on-line ved fremsendelse af accept på mailadressen ");
            Hyperlink hyp = paragraph.AddHyperlink(string.Format("mailto:{0}?subject=Tilbudnr.{1}", Company.OrdreEmail, TilbudID.ToString()), HyperlinkType.Web);
            //hyp.Font.Size = 7
            hyp.AddText(Company.OrdreEmail);
            paragraph.AddText(" og samtidig oplyse tilbudsnr. ");
            paragraph.AddText(TilbudID.ToString());
            paragraph.AddLineBreak();
            
            paragraph = this.document.LastSection.AddParagraph();
            paragraph.Format.SpaceBefore = "5mm";
            //.Format.Font.Size = 7
            paragraph.AddText("Såfremt der er spørgsmål");
            paragraph.AddText(" – eller korrektioner");
            paragraph.AddText(" – til det fremsendte tilbud, er du meget velkommen til at kontakte os på telefon nr. ");
            paragraph.AddText(Company.phone);
            paragraph.AddLineBreak();
            
            paragraph = this.document.LastSection.AddParagraph();
            paragraph.Format.SpaceBefore = "5mm";
            //.Format.Font.Size = 7
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            paragraph.AddText("Med venlig hilsen");
            paragraph.AddLineBreak();
            
            Font FontTal = new Font();
            FontTal.Color = new Color((byte) 255, (byte) 0, (byte) 0);
            FontTal.Bold = true;
            paragraph = this.document.LastSection.AddParagraph();
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            paragraph.AddText(Company.name);
            paragraph.AddLineBreak();
            
            //paragraph = Me.document.LastSection.AddParagraph()
            //With paragraph
            //    .Format.SpaceBefore = "1,5cm"
            //    '.Format.Font.Size = 7
            //    .Format.Alignment = ParagraphAlignment.Center
            //    .AddText("Kamilla Rye	/	Lennart Funch")
            //    .AddLineBreak()
            //End With
            
            paragraph = this.document.LastSection.AddParagraph();
            paragraph.Format.SpaceBefore = "1cm";
            //.Format.Font.Size = 7
            paragraph.AddText("Bilag:");
            paragraph.AddLineBreak();
            paragraph.AddText(" · ");
            paragraph.AddText("Salgs- og leveringsbetingelser");
            paragraph.AddLineBreak();
            paragraph.AddText(" · ");
            paragraph.AddText("Produktark");
            //.AddLineBreak()
        }
        
        private void AddNoter()
        {
            Paragraph paragraph = default(Paragraph);
            
            if (tilbud.Noter.Trim() != "")
            {
                // Add the notes paragraph
                paragraph = this.document.LastSection.AddParagraph();
                var with_2 = paragraph.Format;
                with_2.SpaceBefore = "5mm";
                with_2.Borders.Width = 0.75;
                with_2.Borders.Distance = 3;
                with_2.Borders.Color = RescueTekniq.Doc.TilbudFolgemail_Dk.TableBorder;
                with_2.Shading.Color = RescueTekniq.Doc.TilbudFolgemail_Dk.TableGray;
                with_2.KeepTogether = true;
                paragraph.AddText(tilbud.Noter);
            }
        }
        
#endregion
        
    }
    
    //End Namespace
    
}
