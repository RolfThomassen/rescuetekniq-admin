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
        /// Creates the invoice form.
        /// </summary>
        public class InvoiceForm
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
            /// The table of the MigraDoc document that contains the invoice items.
            /// </summary>
            private Table table;
            
#endregion
            
            
#region  New
            
            /// <summary>
            /// Initializes a new instance of the class BillFrom and opens the specified XML document.
            /// </summary>
            public InvoiceForm(string filename)
            {
                this.invoice = new XmlDocument();
                this.invoice.Load(filename);
                this.navigator = this.invoice.CreateNavigator();
            }
            
#endregion
            
            /// <summary>
            /// Creates the invoice document.
            /// </summary>
            public Document CreateDocument()
            {
                // Create a new MigraDoc document
                this.document = new Document();
                this.document.Info.Title = "Tilbud";
                this.document.Info.Subject = "Tilbud på hjertestarter og evt tilbehør.";
                this.document.Info.Author = "Lennart Funch";
                
                DefineStyles();
                
                CreatePage();
                
                FillContent();
                
                return this.document;
            }
            
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
            
            /// <summary>
            /// Creates the static parts of the invoice.
            /// </summary>
            private void CreatePage()
            {
                // Each MigraDoc document needs at least one section.
                Section section = this.document.AddSection();
                
                // Put a logo in the header
                Image image = section.Headers.Primary.AddImage("Heart2Start-logo.jpg");
                image.Height = "2.5cm";
                image.LockAspectRatio = true;
                image.RelativeVertical = RelativeVertical.Line;
                image.RelativeHorizontal = RelativeHorizontal.Margin;
                image.Top = ShapePosition.Top;
                image.Left = ShapePosition.Right;
                image.WrapFormat.Style = WrapStyle.Through;
                
                // Create footer
                Paragraph paragraph = section.Footers.Primary.AddParagraph();
                paragraph.AddText("Heart2Start Aps · Gefionsvej 8 · 3400 Hillerød · Danmark");
                paragraph.Format.Font.Size = 9;
                paragraph.Format.Alignment = ParagraphAlignment.Center;
                
                // Create the text frame for the address
                this.addressFrame = section.AddTextFrame();
                this.addressFrame.Height = "3.0cm";
                this.addressFrame.Width = "7.0cm";
                this.addressFrame.Left = ShapePosition.Left;
                this.addressFrame.RelativeHorizontal = RelativeHorizontal.Margin;
                this.addressFrame.Top = "5.0cm";
                this.addressFrame.RelativeVertical = RelativeVertical.Page;
                
                // Put sender in address frame
                paragraph = this.addressFrame.AddParagraph("Heart2Start Aps · Gefionsvej 8 · 3400 Hillerød");
                paragraph.Format.Font.Name = "Times New Roman";
                paragraph.Format.Font.Size = 7;
                paragraph.Format.SpaceAfter = 3;
                
                // Add the print date field
                paragraph = section.AddParagraph();
                paragraph.Format.SpaceBefore = "8cm";
                paragraph.Style = "Reference";
                paragraph.AddFormattedText("TILBUD", TextFormat.Bold);
                paragraph.AddTab();
                paragraph.AddText("Hillerød, ");
                paragraph.AddDateField("dd.MM.yyyy");
                
                // Create the item table
                this.table = section.AddTable();
                this.table.Style = "Table";
                this.table.Borders.Color = TableBorder;
                this.table.Borders.Width = 0.25;
                this.table.Borders.Left.Width = 0.5;
                this.table.Borders.Right.Width = 0.5;
                this.table.Rows.LeftIndent = 0;
                
                // Before you can add a row, you must define the columns
                Column column = this.table.AddColumn("1cm");
                column.Format.Alignment = ParagraphAlignment.Center;
                
                column = this.table.AddColumn("2.5cm");
                column.Format.Alignment = ParagraphAlignment.Right;
                
                column = this.table.AddColumn("3cm");
                column.Format.Alignment = ParagraphAlignment.Right;
                
                column = this.table.AddColumn("3.5cm");
                column.Format.Alignment = ParagraphAlignment.Right;
                
                column = this.table.AddColumn("2cm");
                column.Format.Alignment = ParagraphAlignment.Center;
                
                column = this.table.AddColumn("4cm");
                column.Format.Alignment = ParagraphAlignment.Right;
                
                // Create the header of the table
                Row row = table.AddRow();
                row.HeadingFormat = true;
                row.Format.Alignment = ParagraphAlignment.Center;
                row.Format.Font.Bold = true;
                row.Shading.Color = TableBlue;
                row.Cells[0].AddParagraph("Enhed");
                row.Cells[0].Format.Font.Bold = false;
                row.Cells[0].Format.Alignment = ParagraphAlignment.Left;
                row.Cells[0].VerticalAlignment = VerticalAlignment.Bottom;
                row.Cells[0].MergeDown = 1;
                row.Cells[1].AddParagraph("Beskrivelse");
                row.Cells[1].Format.Alignment = ParagraphAlignment.Left;
                row.Cells[1].MergeRight = 3;
                row.Cells[5].AddParagraph("i alt");
                row.Cells[5].Format.Alignment = ParagraphAlignment.Left;
                row.Cells[5].VerticalAlignment = VerticalAlignment.Bottom;
                row.Cells[5].MergeDown = 1;
                
                row = table.AddRow();
                row.HeadingFormat = true;
                row.Format.Alignment = ParagraphAlignment.Center;
                row.Format.Font.Bold = true;
                row.Shading.Color = TableBlue;
                row.Cells[1].AddParagraph("Antal");
                row.Cells[1].Format.Alignment = ParagraphAlignment.Left;
                row.Cells[2].AddParagraph("Enhedspris");
                row.Cells[2].Format.Alignment = ParagraphAlignment.Left;
                row.Cells[3].AddParagraph("Rabat (%)");
                row.Cells[3].Format.Alignment = ParagraphAlignment.Left;
                row.Cells[4].AddParagraph("eksl. moms");
                row.Cells[4].Format.Alignment = ParagraphAlignment.Left;
                
                this.table.SetEdge(0, 0, 6, 2, Edge.Box, BorderStyle.Single,
	                0.75, Color.Empty);
            }
            
            /// <summary>
            /// Creates the dynamic parts of the invoice.
            /// </summary>
            private void FillContent()
            {
                // Fill address in address text frame
                XPathNavigator item = SelectItem("/invoice/to");
                Paragraph paragraph = this.addressFrame.AddParagraph();
                paragraph.AddText(GetValue(item, "name/singleName"));
                paragraph.AddLineBreak();
                paragraph.AddText(GetValue(item, "address/line1"));
                paragraph.AddLineBreak();
                paragraph.AddText(System.Convert.ToString((GetValue(item, "address/postalCode") + " ") + GetValue(item, "address/city")));
                
                // Iterate the invoice items
                double totalExtendedPrice = 0;
                XPathNodeIterator iter = this.navigator.Select("/invoice/items/*");
                while (iter.MoveNext())
                {
                    item = iter.Current;
                    double quantity = GetValueAsDouble(item, "quantity");
                    double price = GetValueAsDouble(item, "price");
                    double discount = GetValueAsDouble(item, "discount");
                    
                    // Each item fills two rows
                    Row row1 = this.table.AddRow();
                    Row row2 = this.table.AddRow();
                    row1.TopPadding = 1.5;
                    row1.Cells[0].Shading.Color = TableGray;
                    row1.Cells[0].VerticalAlignment = VerticalAlignment.Center;
                    row1.Cells[0].MergeDown = 1;
                    row1.Cells[1].Format.Alignment = ParagraphAlignment.Left;
                    row1.Cells[1].MergeRight = 3;
                    row1.Cells[5].Shading.Color = TableGray;
                    row1.Cells[5].MergeDown = 1;
                    
                    row1.Cells[0].AddParagraph(GetValue(item, "itemNumber"));
                    paragraph = row1.Cells[1].AddParagraph();
                    paragraph.AddFormattedText(GetValue(item, "title"), TextFormat.Bold);
                    paragraph.AddFormattedText(" by ", TextFormat.Italic);
                    paragraph.AddText(GetValue(item, "author"));
                    row2.Cells[1].AddParagraph(GetValue(item, "quantity"));
                    row2.Cells[2].AddParagraph(price.ToString("0.00") + " kr.");
                    row2.Cells[3].AddParagraph(discount.ToString("0.0"));
                    row2.Cells[4].AddParagraph();
                    row2.Cells[5].AddParagraph(price.ToString("0.00"));
                    double extendedPrice = quantity * price;
                    extendedPrice = extendedPrice * (100 - discount) / 100;
                    row1.Cells[5].AddParagraph(extendedPrice.ToString("0.00") + " kr.");
                    row1.Cells[5].VerticalAlignment = VerticalAlignment.Bottom;
                    totalExtendedPrice += extendedPrice;
                    
                    this.table.SetEdge(0, this.table.Rows.Count - 2, 6, 2, Edge.Box, BorderStyle.Single, 0.75);
                }
                
                // Add an invisible row as a space line to the table
                Row row = this.table.AddRow();
                row.Borders.Visible = false;
                
                // Add the total price row
                row = this.table.AddRow();
                row.Cells[0].Borders.Visible = false;
                row.Cells[0].AddParagraph("Total pris");
                row.Cells[0].Format.Font.Bold = true;
                row.Cells[0].Format.Alignment = ParagraphAlignment.Right;
                row.Cells[0].MergeRight = 4;
                row.Cells[5].AddParagraph(totalExtendedPrice.ToString("0.00") + " kr.");
                
                // Add the VAT row
                row = this.table.AddRow();
                row.Cells[0].Borders.Visible = false;
                row.Cells[0].AddParagraph("Moms (25%)");
                row.Cells[0].Format.Font.Bold = true;
                row.Cells[0].Format.Alignment = ParagraphAlignment.Right;
                row.Cells[0].MergeRight = 4;
                row.Cells[5].AddParagraph((0.25 * totalExtendedPrice).ToString("0.00") + " kr.");
                
                // Add the additional fee row
                row = this.table.AddRow();
                row.Cells[0].Borders.Visible = false;
                row.Cells[0].AddParagraph("Transport");
                row.Cells[5].AddParagraph(0.ToString("0.00") + " kr.");
                row.Cells[0].Format.Font.Bold = true;
                row.Cells[0].Format.Alignment = ParagraphAlignment.Right;
                row.Cells[0].MergeRight = 4;
                
                // Add the total due row
                row = this.table.AddRow();
                row.Cells[0].AddParagraph("Totalt");
                row.Cells[0].Borders.Visible = false;
                row.Cells[0].Format.Font.Bold = true;
                row.Cells[0].Format.Alignment = ParagraphAlignment.Right;
                row.Cells[0].MergeRight = 4;
                totalExtendedPrice += 0.25 * totalExtendedPrice;
                row.Cells[5].AddParagraph(totalExtendedPrice.ToString("0.00") + " kr.");
                
                // Set the borders of the specified cell range
                this.table.SetEdge(5, this.table.Rows.Count - 4, 1, 4, Edge.Box, BorderStyle.Single, 0.75);
                
                // Add the notes paragraph
                paragraph = this.document.LastSection.AddParagraph();
                paragraph.Format.SpaceBefore = "1cm";
                paragraph.Format.Borders.Width = 0.75;
                paragraph.Format.Borders.Distance = 3;
                paragraph.Format.Borders.Color = TableBorder;
                paragraph.Format.Shading.Color = TableGray;
                item = SelectItem("/invoice");
                paragraph.AddText(GetValue(item, "notes"));
            }
            
            
#region  Diverse
            
            /// <summary>
            /// Selects a subtree in the XML data.
            /// </summary>
            private XPathNavigator SelectItem(string path)
            {
                XPathNodeIterator iter = this.navigator.Select(path);
                iter.MoveNext();
                return iter.Current;
            }
            
            /// <summary>
            /// Gets an element value from the XML data.
            /// </summary>
            private static string GetValue(XPathNavigator nav, string name)
            {
                //nav = nav.Clone();
                XPathNodeIterator iter = nav.Select(name);
                iter.MoveNext();
                return iter.Current.Value;
            }
            
            /// <summary>
            /// Gets an element value as double from the XML data.
            /// </summary>
            private static double GetValueAsDouble(XPathNavigator nav, string name)
            {
                try
                {
                    string value = GetValue(nav, name);
                    if (value.Length == 0)
                    {
                        return 0;
                    }
                    return double.Parse(value, CultureInfo.InvariantCulture);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
                return 0;
            }
            
#endregion
            
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
            
        }
    }
    
}
