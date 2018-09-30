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
//using MigraDoc.DocumentObjectModel.Unit;
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
    
    namespace FirstAidBag //registering
    {
        
        /// <summary>
        /// Creates the FAB registration form.
        /// </summary>
        internal class FAB_Registering_Dk
        {
            
#region  Privates
            
            /// <summary>
            /// Class contains all info about _fabID og tilbudslinier
            /// </summary>
            private RescueTekniq.BOL.FAB _FAB = new RescueTekniq.BOL.FAB();
            
            /// <summary>
            /// FAB's ID
            /// </summary>
            /// <remarks></remarks>
            private int _fabID = -1;
            
            /// <summary>
            /// The MigraDoc document that represents the invoice.
            /// </summary>
            private Document document;
            
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
            private Table tableVirksomhed;
            private Table tableModel;
            private Table tableLokation;
            
            /// <summary>
            /// Path for logo image file
            /// </summary>
            private string _Logo = "logo.gif";
            
#endregion
            
#region  Shared Const Colors
            
            // Some pre-defined colors
            static readonly Color TableBorder = new Color((byte) 81, (byte) 125, (byte) 192);
            static readonly Color TableBlue = new Color((byte) 235, (byte) 240, (byte) 249);
            static readonly Color TableDarkGray = new Color((byte) 121, (byte) 121, (byte) 121);
            static readonly Color TableLightGray = new Color((byte) 242, (byte) 242, (byte) 242);
            static readonly Color TekstWhite = new Color((byte) 255, (byte) 255, (byte) 255);
            
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
            /// Initializes a new instance of the class TilbudFolgemail and opens the specified _fabID from _FAB.
            /// </summary>
            public FAB_Registering_Dk(int _fabID)
            {
                this.fabID = _fabID;
            }
            public FAB_Registering_Dk()
            {
                
            }
            
#endregion
            
#region  Properties
            
            public int fabID
            {
                get
                {
                    return _fabID;
                }
                set
                {
                    _fabID = value;
                    if (_FAB.loaded)
                    {
                        if (_FAB.ID != _fabID)
                        {
                            _FAB = new RescueTekniq.BOL.FAB(_fabID);
                        }
                    }
                    else
                    {
                        _FAB = new RescueTekniq.BOL.FAB(_fabID);
                    }
                }
            }
            
            public RescueTekniq.BOL.FAB FAB
            {
                get
                {
                    return _FAB;
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
            
#region  Create Document
            
            /// <summary>
            /// Creates the PDF document.
            /// </summary>
            public Document CreateDocument()
            {
                if (!_FAB.loaded)
                {
                    if (_fabID > 0)
                    {
                        _FAB = new RescueTekniq.BOL.FAB(_fabID);
                    }
                }
                
                if (!_FAB.loaded)
                {
                    throw (new Exception("Førstehjælpstaske findes ikke, ID ukendt :" + fabID.ToString()));
                }
                
                // Create a new MigraDoc document
                this.document = new Document();
                var with_1 = this.document.Info;
                with_1.Title = "Førstehjælpstaske regstering nr. " + System.Convert.ToString(_fabID);
                with_1.Subject = "Førstehjælpstaske registering";
                with_1.Author = Company.name;
                with_1.Comment = "";
                with_1.Keywords = "";
                
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
            /// Creates the static parts of the PDF document.
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
                //-------------------------------------
                // Create Header
                CreateHeader(section);
                
                //-------------------------------------
                // Create Footer
                CreateFooter(section);
                
                //-------------------------------------
                var with_3 = section.AddParagraph();
                //.Format.SpaceBefore = "1,5cm"
                with_3.AddFormattedText("Virksomhedsinfo", TextFormat.Bold);
                
                //-------------------------------------
                // Create the table for Virksomhed
                CreateTableVirksomhed(section.AddTable());
                
                var with_4 = section.AddParagraph();
                with_4.Format.SpaceBefore = "0,5cm";
                with_4.AddFormattedText(string.Format("Udfyldes af {0}", Company.name), TextFormat.Bold);
                
                //-------------------------------------
                // Create the table for Model
                CreateTableModel(section.AddTable());
                
                var with_5 = section.AddParagraph();
                with_5.Format.SpaceBefore = "0,5cm";
                with_5.AddFormattedText("Udfyldes af Virksomheden:", TextFormat.Bold);
                
                //-------------------------------------
                // Create the table for Lokation
                CreateTableLokation(section.AddTable());
                
                //-------------------------------------
                var with_6 = section.AddParagraph();
                with_6.Format.SpaceBefore = "0,2cm";
                with_6.AddText(string.Format("Blanketten bedes returneret til {0} i udfyldt stand. ", Company.name));
                with_6.AddText("Og sendes til nedennævnte adresse – eller alternativt indscan og send til ");
                with_6.AddText(Company.email);
                
            }
            
            /// <summary>
            /// Create Header for docukment
            /// </summary>
            /// <param name="section"></param>
            /// <remarks></remarks>
            private void CreateHeader(Section section)
            {
                
                // Put a logo in the header
                Image image = section.Headers.Primary.AddImage(Logo);
                image.Height = "2.5cm";
                image.LockAspectRatio = true;
                image.RelativeVertical = RelativeVertical.Line;
                image.RelativeHorizontal = RelativeHorizontal.Margin;
                image.Top = ShapePosition.Top;
                image.Left = ShapePosition.Right;
                image.WrapFormat.Style = WrapStyle.Through;
                
                // Create the text frame for the header
                this.headerFrame = section.Headers.Primary.AddTextFrame();
                //section.AddTextFrame()
                this.headerFrame.Height = "3.5cm";
                this.headerFrame.Width = "13.0cm";
                this.headerFrame.Left = ShapePosition.Left; //Right
                this.headerFrame.RelativeHorizontal = RelativeHorizontal.Page; //Margin
                //.Top = "5.0cm"
                this.headerFrame.RelativeVertical = RelativeVertical.Page;
                
                // Add the dokument header text
                var with_3 = this.headerFrame.AddParagraph();
                with_3.Style = "Reference";
                var with_4 = with_3.Format;
                with_4.Alignment = ParagraphAlignment.Center; //Right
                with_4.SpaceBefore = "2,0cm";
                with_4.Font.Size = 14;
                with_4.Font.Name = "Times New Roman";
                with_4.Font.Bold = true;
                with_3.AddText("REGISTRERING AF FØRSTEHJÆLPSTASKE");
                
            }
            
            /// <summary>
            /// Create Footer for document
            /// </summary>
            /// <param name="section"></param>
            /// <remarks></remarks>
            private void CreateFooter(Section section)
            {
                
                //Dim info As ApplicationServices.AssemblyInfo
                //info = New ApplicationServices.AssemblyInfo("RescueTekniq.Doc")
                
                var with_1 = section.Footers.Primary.AddParagraph();
                var with_2 = with_1.Format;
                with_2.Font.Size = 5;
                with_2.Alignment = ParagraphAlignment.Right;
                with_1.AddDateField();
                with_1.AddLineBreak();
                //.AddText(info.Version.ToString)
                
                var with_3 = section.Footers.Primary.AddParagraph();
                var with_4 = with_3.Format;
                with_4.Font.Size = 9;
                with_4.Alignment = ParagraphAlignment.Center;
                with_3.AddText("- ");
                with_3.AddPageField();
                with_3.AddText(" / ");
                with_3.AddNumPagesField();
                with_3.AddText(" -");
                
                var with_5 = section.Footers.Primary.AddParagraph();
                var with_6 = with_5.Format;
                with_6.Font.Size = 7;
                with_5.AddText(Company.name);
                with_5.AddLineBreak();
                with_5.AddText(Company.adresse);
                with_5.AddLineBreak();
                with_5.AddText(Company.zipcode);
                with_5.AddText(" ");
                with_5.AddText(Company.city);
                if (this.FAB.Virksomhed.LandekodeID != 45)
                {
                    with_5.AddText(", ");
                    with_5.AddText(Company.country);
                }
                with_5.AddLineBreak();
                if (this.FAB.Virksomhed.LandekodeID != 45)
                {
                    with_5.AddText("Phone ");
                    with_5.AddText(Company.int_phone);
                }
                else
                {
                    with_5.AddText("Telefon ");
                    with_5.AddText(Company.phone);
                }
                with_5.AddLineBreak();
                
                with_5.AddText("E-mail: ");
                Hyperlink _email = with_5.AddHyperlink(string.Format("mailto:{0}", Company.email), HyperlinkType.Web);
                _email.AddFormattedText(Company.email);
                with_5.AddLineBreak();
                
                with_5.AddText("Internet: ");
                Hyperlink link = with_5.AddHyperlink(string.Format("http://{0}", Company.link), HyperlinkType.Web);
                link.AddFormattedText(Company.link);
                
                
            }
            
            private void CreateTableVirksomhed(Table tabel)
            {
                // Before you can add a row, you must define the columns
                //Dim column As Column
                this.tableVirksomhed = tabel;
                this.tableVirksomhed.Style = "Table";
                this.tableVirksomhed.Borders.Color = TableBorder;
                this.tableVirksomhed.Borders.Width = 0.25;
                this.tableVirksomhed.Borders.Left.Width = 0.5;
                this.tableVirksomhed.Borders.Right.Width = 0.5;
                this.tableVirksomhed.Rows.LeftIndent = 0.5;
                
                var with_2 = this.tableVirksomhed.AddColumn("3,5cm");
                var with_3 = with_2.Format;
                with_3.Alignment = ParagraphAlignment.Left;
                with_3.Font.Color = TekstWhite;
                with_3.Font.Bold = true;
                with_3.SpaceBefore = "0,2cm";
                with_3.SpaceAfter = "0,2cm";
                
                var with_4 = this.tableVirksomhed.AddColumn("4,0cm");
                var with_5 = with_4.Format;
                with_5.Alignment = ParagraphAlignment.Left;
                with_5.Font.Color = TekstWhite;
                with_5.Font.Bold = true;
                
                var with_6 = this.tableVirksomhed.AddColumn("1,0cm");
                var with_7 = with_6.Format;
                with_7.Alignment = ParagraphAlignment.Left;
                with_7.Font.Color = TekstWhite;
                with_7.Font.Bold = true;
                
                var with_8 = this.tableVirksomhed.AddColumn("7,5cm");
                var with_9 = with_8.Format;
                with_9.Alignment = ParagraphAlignment.Left;
                with_9.Font.Color = TekstWhite;
                with_9.Font.Bold = true;
            }
            
            private void CreateTableModel(Table tabel)
            {
                // Before you can add a row, you must define the columns
                //Dim column As Column
                this.tableModel = tabel;
                this.tableModel.Style = "Table";
                this.tableModel.Borders.Color = TableBorder;
                this.tableModel.Borders.Width = 0.25;
                this.tableModel.Borders.Left.Width = 0.5;
                this.tableModel.Borders.Right.Width = 0.5;
                this.tableModel.Rows.LeftIndent = 0.5;
                
                var with_2 = this.tableModel.AddColumn("3,5cm");
                var with_3 = with_2.Format;
                with_3.Alignment = ParagraphAlignment.Left;
                with_3.Font.Color = TekstWhite;
                with_3.Font.Bold = true;
                with_3.SpaceBefore = "0,2cm";
                with_3.SpaceAfter = "0,2cm";
                var with_4 = this.tableModel.AddColumn("8,0cm");
                var with_5 = with_4.Format;
                with_5.Alignment = ParagraphAlignment.Left;
                with_5.Font.Color = TekstWhite;
                with_5.Font.Bold = true;
                var with_6 = this.tableModel.AddColumn("1,0cm");
                var with_7 = with_6.Format;
                with_7.Alignment = ParagraphAlignment.Left;
                with_7.Font.Color = TekstWhite;
                with_7.Font.Bold = true;
                var with_8 = this.tableModel.AddColumn("3,5cm");
                var with_9 = with_8.Format;
                with_9.Alignment = ParagraphAlignment.Center;
                with_9.Font.Color = TekstWhite;
                with_9.Font.Bold = true;
            }
            
            private void CreateTableLokation(Table tabel)
            {
                // Before you can add a row, you must define the columns
                //Dim column As Column
                this.tableLokation = tabel;
                this.tableLokation.Style = "Table";
                this.tableLokation.Borders.Color = TableBorder;
                this.tableLokation.Borders.Width = 0.25;
                this.tableLokation.Borders.Left.Width = 0.5;
                this.tableLokation.Borders.Right.Width = 0.5;
                this.tableLokation.Rows.LeftIndent = 0.5;
                
                var with_2 = this.tableLokation.AddColumn("3,5cm");
                var with_3 = with_2.Format;
                with_3.Alignment = ParagraphAlignment.Left;
                with_3.Font.Color = TekstWhite;
                with_3.Font.Bold = true;
                with_3.SpaceBefore = "0,2cm";
                with_3.SpaceAfter = "0,2cm";
                var with_4 = this.tableLokation.AddColumn("12,5cm");
                var with_5 = with_4.Format;
                with_5.Alignment = ParagraphAlignment.Left;
                with_5.Font.Color = TekstWhite;
                with_5.Font.Bold = true;
            }
            
#endregion
            
#region  Fill Page with Data
            
            /// <summary>
            /// Creates the dynamic parts of the invoice.
            /// </summary>
            private void FillContent()
            {
                
                FillVirksomhed();
                
                FillModel();
                
                FillLokation();
                
            }
            
            private void FillVirksomhed()
            {
                // Add the total price row
                if (FAB.Virksomhed.Cvrnr.Trim() != "")
                {
                    rowTitleTekst(this.tableVirksomhed.AddRow(), "CVR. nr.:", FAB.Virksomhed.Cvrnr);
                }
                rowTitleTekst(this.tableVirksomhed.AddRow(), "Virksomhedens navn:", FAB.Virksomhed.Firmanavn);
                rowTitleTekst(this.tableVirksomhed.AddRow(), "Adresse:", System.Convert.ToString(FAB.Virksomhed.Adresse1 + System.Convert.ToString(FAB.Virksomhed.Adresse2.Trim() != "" ? Constants.vbNewLine + FAB.Virksomhed.Adresse2 : "")));
                rowTitleTekst(this.tableVirksomhed.AddRow(), "Postnr.:", FAB.Virksomhed.PostnrBy);
                rowTitleTekst(this.tableVirksomhed.AddRow(), "Telefon nr.:", FAB.Virksomhed.Telefon, "Web:", FAB.Virksomhed.WebSiteUrl);
                
            }
            
            private void FillModel()
            {
                // Add the total price row
                rowTitleTekst(this.tableModel.AddRow(), "Model :", FAB.Vare.Navn);
                rowTitleTekst(this.tableModel.AddRow(), "S-nr.:", FAB.SerialNo);
                //For Each item As FAB_Electrod In FAB.ElectrodList
                //    rowTitleTekst(Me.tableModel.AddRow(), "Elektroder:", item.ElectrodTypeText, "Dato:", "Udløb " & item.ElectrodExpireDate.ToString("MMM. yyyy"))
                //Next
                //If FAB.ElectrodList.Count = 0 Then
                //    rowTitleTekst(Me.tableModel.AddRow(), "Elektroder:", "Ingen Elektroder")
                //End If
                //'rowTitleTekst(Me.tableModel.AddRow(), "Elektroder:", FAB.ElectrodList.Item(0).ElectrodTypeText, "Dato:", FAB.ElectrodList.Item(0).ElectrodExpireDate)
                
                //For Each item As FAB_Battery In FAB.BatteryList
                //    rowTitleTekst(Me.tableModel.AddRow(), "Batteri:", item.BatteryTypeText, "Dato:", "Udløb " & item.BatteryExpireDate.ToString("MMM. yyyy"))
                //Next
                //If FAB.BatteryList.Count = 0 Then
                //    rowTitleTekst(Me.tableModel.AddRow(), "Batteri:", "Ingen Batterier")
                //End If
                
            }
            
            private void FillLokation()
            {
                // Add the total price row
                rowTitleTekst2(this.tableLokation.AddRow(), "Navn på overordnet ansvarlig for førstehjælpstaske i virksomheden", "");
                rowTitleTekst2(this.tableLokation.AddRow(), "E-mail adresse:", "");
                rowTitleTekst2(this.tableLokation.AddRow(), "Dato for opstilling :", "");
                rowTitleTekst2(this.tableLokation.AddRow(), "Oplys nedenfor placering af ovennævnte førstehjælpstaske med angivelse af geografisk placering (adresse), og fysisk placering (eks. placeret i kantinen):");
                
                //With Me.tableLokation.AddRow()
                //    With .Cells(0)
                //        If FAB.ElectrodList.Count > 1 Then
                //            .Format.SpaceBefore = "4cm"
                //        Else
                //            .Format.SpaceBefore = "5cm"
                //        End If
                //        .MergeRight = 1
                //    End With
                //End With
            }
            
            
#endregion
            
#region  row fillers
            
            private void rowTitleTekst(Row row, string title, string tekst)
            {
                var with_1 = row;
                var with_2 = with_1.Cells[0];
                with_2.Shading.Color = TableDarkGray;
                with_2.VerticalAlignment = VerticalAlignment.Center;
                var with_3 = with_2.AddParagraph();
                with_3.AddText(title);
                with_3.Format.Font.Bold = true;
                with_3.Format.Font.Color = TekstWhite;
                //.Borders.Visible = False
                //.Format.Alignment = ParagraphAlignment.Right
                var with_4 = with_1.Cells[1];
                with_4.Shading.Color = TableLightGray;
                with_4.VerticalAlignment = VerticalAlignment.Center;
                
                with_4.MergeRight = 2;
                var with_5 = with_4.AddParagraph();
                with_5.Format.Font.Color = new Color((byte) 0, (byte) 0, (byte) 0);
                with_5.AddText(tekst);
            }
            private void rowTitleTekst(Row row, string 
	title, string tekst, string title2, 
	string tekst2)
            {
                var with_1 = row;
                var with_2 = with_1.Cells[0];
                with_2.Shading.Color = TableDarkGray;
                with_2.VerticalAlignment = VerticalAlignment.Center;
                var with_3 = with_2.AddParagraph();
                with_3.AddText(title);
                with_3.Format.Font.Bold = true;
                with_3.Format.Font.Color = TekstWhite;
                //.Borders.Visible = False
                //.Format.Font.Bold = True
                //.Format.Alignment = ParagraphAlignment.Right
                var with_4 = with_1.Cells[1];
                with_4.Shading.Color = TableLightGray;
                with_4.VerticalAlignment = VerticalAlignment.Center;
                var with_5 = with_4.AddParagraph();
                with_5.Format.Font.Color = new Color((byte) 0, (byte) 0, (byte) 0);
                with_5.AddText(tekst);
                
                var with_6 = with_1.Cells[2];
                with_6.Shading.Color = TableDarkGray;
                with_6.VerticalAlignment = VerticalAlignment.Center;
                var with_7 = with_6.AddParagraph();
                with_7.AddText(title2);
                with_7.Format.Font.Bold = true;
                with_7.Format.Font.Color = TekstWhite;
                //.Borders.Visible = False
                //.Format.Font.Bold = True
                //.Format.Alignment = ParagraphAlignment.Right
                var with_8 = with_1.Cells[3];
                with_8.Shading.Color = TableLightGray;
                with_8.VerticalAlignment = VerticalAlignment.Center;
                var with_9 = with_8.AddParagraph();
                with_9.Format.Font.Color = new Color((byte) 0, (byte) 0, (byte) 0);
                with_9.AddText(tekst2);
                
            }
            
            private void rowTitleTekst2(Row row, string title)
            {
                var with_1 = row;
                var with_2 = with_1.Cells[0];
                with_2.Shading.Color = TableDarkGray;
                with_2.VerticalAlignment = VerticalAlignment.Center;
                
                var with_3 = with_2.AddParagraph();
                with_3.AddText(title);
                with_3.Format.Font.Bold = true;
                with_3.Format.Font.Color = TekstWhite;
                
                with_2.MergeRight = 1;
            }
            private void rowTitleTekst2(Row row, string title, string tekst)
            {
                var with_1 = row;
                var with_2 = with_1.Cells[0];
                with_2.Shading.Color = TableDarkGray;
                with_2.VerticalAlignment = VerticalAlignment.Center;
                var with_3 = with_2.AddParagraph();
                with_3.AddText(title);
                with_3.Format.Font.Bold = true;
                with_3.Format.Font.Color = TekstWhite;
                //.Borders.Visible = False
                //.Format.Alignment = ParagraphAlignment.Right
                var with_4 = with_1.Cells[1];
                with_4.Shading.Color = TableLightGray;
                with_4.VerticalAlignment = VerticalAlignment.Center;
                var with_5 = with_4.AddParagraph();
                with_5.Format.Font.Color = new Color((byte) 0, (byte) 0, (byte) 0);
                with_5.AddText(tekst);
            }
            
#endregion
            
        }
        
    }
    
    
    
    
    
    
    
    
    
    
}
