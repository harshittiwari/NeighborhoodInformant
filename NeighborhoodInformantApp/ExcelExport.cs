using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.Xml;
using System.Windows.Forms;

namespace NeighborhoodInformantApp
{
    public class ExcelExport : IDisposable
    {
        private XmlWriter _writer;

        public enum CellStyle { General, Number, Currency, DateTime, ShortDate };

        public void WriteStartDocument()
        {
            if (_writer == null) throw new InvalidOperationException("Cannot write after closing.");

            _writer.WriteProcessingInstruction("mso-application", "progid=\"Excel.Sheet\"");
            _writer.WriteStartElement("ss", "Workbook", "urn:schemas-microsoft-com:office:spreadsheet");
            WriteExcelStyles();
        }

        public void WriteEndDocument()
        {
            if (_writer == null) throw new InvalidOperationException("Cannot write after closing.");

            _writer.WriteEndElement();
        }

        private void WriteExcelStyleElement(CellStyle style)
        {
            _writer.WriteStartElement("Style", "urn:schemas-microsoft-com:office:spreadsheet");
            _writer.WriteAttributeString("ID", "urn:schemas-microsoft-com:office:spreadsheet", style.ToString());
            _writer.WriteEndElement();
        }

        private void WriteExcelStyleElement(CellStyle style, string NumberFormat)
        {
            _writer.WriteStartElement("Style", "urn:schemas-microsoft-com:office:spreadsheet");

            _writer.WriteAttributeString("ID", "urn:schemas-microsoft-com:office:spreadsheet", style.ToString());
            _writer.WriteStartElement("NumberFormat", "urn:schemas-microsoft-com:office:spreadsheet");
            _writer.WriteAttributeString("Format", "urn:schemas-microsoft-com:office:spreadsheet", NumberFormat);
            _writer.WriteEndElement();

            _writer.WriteEndElement();

        }

        private void WriteExcelStyles()
        {
            _writer.WriteStartElement("Styles", "urn:schemas-microsoft-com:office:spreadsheet");

            WriteExcelStyleElement(CellStyle.General);
            WriteExcelStyleElement(CellStyle.Number, "General Number");
            WriteExcelStyleElement(CellStyle.DateTime, "General Date");
            WriteExcelStyleElement(CellStyle.Currency, "Currency");
            WriteExcelStyleElement(CellStyle.ShortDate, "Short Date");

            _writer.WriteEndElement();
        }

        public void WriteStartWorksheet(string name)
        {
            if (_writer == null) throw new InvalidOperationException("Cannot write after closing.");

            _writer.WriteStartElement("Worksheet", "urn:schemas-microsoft-com:office:spreadsheet");
            _writer.WriteAttributeString("Name", "urn:schemas-microsoft-com:office:spreadsheet", name);
            _writer.WriteStartElement("Table", "urn:schemas-microsoft-com:office:spreadsheet");
        }

        public void WriteEndWorksheet()
        {
            if (_writer == null) throw new InvalidOperationException("Cannot write after closing.");

            _writer.WriteEndElement();
            _writer.WriteEndElement();
        }

        public ExcelExport(string outputFileName)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            _writer = XmlWriter.Create(outputFileName, settings);
        }

        public void Close()
        {
            if (_writer == null) throw new InvalidOperationException("Already closed.");

            _writer.Close();
            _writer = null;
        }

        public void WriteExcelColumnDefinition(int columnWidth)
        {
            if (_writer == null) throw new InvalidOperationException("Cannot write after closing.");

            _writer.WriteStartElement("Column", "urn:schemas-microsoft-com:office:spreadsheet");
            _writer.WriteStartAttribute("Width", "urn:schemas-microsoft-com:office:spreadsheet");
            _writer.WriteValue(columnWidth);
            _writer.WriteEndAttribute();
            _writer.WriteEndElement();
        }

        public void WriteExcelUnstyledCell(string value)
        {
            if (_writer == null) throw new InvalidOperationException("Cannot write after closing.");

            _writer.WriteStartElement("Cell", "urn:schemas-microsoft-com:office:spreadsheet");
            _writer.WriteStartElement("Data", "urn:schemas-microsoft-com:office:spreadsheet");
            _writer.WriteAttributeString("Type", "urn:schemas-microsoft-com:office:spreadsheet", "String");
            _writer.WriteValue(value);
            _writer.WriteEndElement();
            _writer.WriteEndElement();
        }

        public void WriteStartRow()
        {
            if (_writer == null) throw new InvalidOperationException("Cannot write after closing.");

            _writer.WriteStartElement("Row", "urn:schemas-microsoft-com:office:spreadsheet");
        }

        public void WriteEndRow()
        {
            if (_writer == null) throw new InvalidOperationException("Cannot write after closing.");

            _writer.WriteEndElement();
        }

        public void WriteExcelStyledCell(object value, CellStyle style)
        {
            if (_writer == null) throw new InvalidOperationException("Cannot write after closing.");

            _writer.WriteStartElement("Cell", "urn:schemas-microsoft-com:office:spreadsheet");
            _writer.WriteAttributeString("StyleID", "urn:schemas-microsoft-com:office:spreadsheet", style.ToString());
            _writer.WriteStartElement("Data", "urn:schemas-microsoft-com:office:spreadsheet");
            switch (style)
            {
                case CellStyle.General:
                    _writer.WriteAttributeString("Type", "urn:schemas-microsoft-com:office:spreadsheet", "String");
                    break;
                case CellStyle.Number:
                case CellStyle.Currency:
                    _writer.WriteAttributeString("Type", "urn:schemas-microsoft-com:office:spreadsheet", "Number");
                    break;
                case CellStyle.ShortDate:
                case CellStyle.DateTime:
                    _writer.WriteAttributeString("Type", "urn:schemas-microsoft-com:office:spreadsheet", "DateTime");
                    break;
            }
            _writer.WriteValue(value);
            //  tag += String.Format("{1}\"><ss:Data ss:Type=\"DateTime\">{0:yyyy\\-MM\\-dd\\THH\\:mm\\:ss\\.fff}</ss:Data>", value,

            _writer.WriteEndElement();
            _writer.WriteEndElement();
        }

        public void WriteExcelAutoStyledCell(object value)
        {
            if (_writer == null) throw new InvalidOperationException("Cannot write after closing.");

            //write the <ss:Cell> and <ss:Data> tags for something
            if (value is Int16 || value is Int32 || value is Int64 || value is SByte ||
                value is UInt16 || value is UInt32 || value is UInt64 || value is Byte)
            {
                WriteExcelStyledCell(value, CellStyle.Number);
            }
            else if (value is Single || value is Double || value is Decimal) //we'll assume it's a currency
            {
                WriteExcelStyledCell(value, CellStyle.Currency);
            }
            else if (value is DateTime)
            {
                //check if there's no time information and use the appropriate style
                WriteExcelStyledCell(value, ((DateTime)value).TimeOfDay.CompareTo(new TimeSpan(0, 0, 0, 0, 0)) == 0 ? CellStyle.ShortDate : CellStyle.DateTime);
            }
            else
            {
                WriteExcelStyledCell(value, CellStyle.General);
            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            if (_writer == null)
                return;

            _writer.Close();
            _writer = null;
        }

        #endregion
    }

    #region tryouts- commented code
    //class ExcelExport
    //{

    //private static Cell InsertCellInWorksheet(string columnName, uint rowIndex, WorksheetPart worksheetPart)
    //{
    //    Worksheet worksheet = worksheetPart.Worksheet;
    //    SheetData sheetData = worksheet.GetFirstChild<SheetData>();
    //    string cellReference = columnName + rowIndex;

    //    // If the worksheet does not contain a row with the specified row index, insert one.
    //    Row row;
    //    if (sheetData.Elements<Row>().Where(r => r.RowIndex == rowIndex).Count() != 0)
    //    {
    //        row = sheetData.Elements<Row>().Where(r => r.RowIndex == rowIndex).First();
    //    }
    //    else
    //    {
    //        row = new Row() { RowIndex = rowIndex };
    //        sheetData.Append(row);
    //    }

    //    // If there is not a cell with the specified column name, insert one.  
    //    if (row.Elements<Cell>().Where(c => c.CellReference.Value == columnName + rowIndex).Count() > 0)
    //    {
    //        return row.Elements<Cell>().Where(c => c.CellReference.Value == cellReference).First();
    //    }
    //    else
    //    {
    //        // Cells must be in sequential order according to CellReference. Determine where to insert the new cell.
    //        Cell refCell = null;
    //        foreach (Cell cell in row.Elements<Cell>())
    //        {
    //            if (string.Compare(cell.CellReference.Value, cellReference, true) > 0)
    //            {
    //                refCell = cell;
    //                break;
    //            }
    //        }

    //        Cell newCell = new Cell() { CellReference = cellReference };
    //        row.InsertBefore(newCell, refCell);

    //        worksheet.Save();
    //        return newCell;
    //    }
    //}
    //private static int InsertSharedStringItem(string text, SharedStringTablePart shareStringPart)
    //{
    //    // If the part does not contain a SharedStringTable, create one.
    //    if (shareStringPart.SharedStringTable == null)
    //    {
    //        shareStringPart.SharedStringTable = new SharedStringTable();
    //    }

    //    int i = 0;

    //    // Iterate through all the items in the SharedStringTable. If the text already exists, return its index.
    //    foreach (SharedStringItem item in shareStringPart.SharedStringTable.Elements<SharedStringItem>())
    //    {
    //        if (item.InnerText == text)
    //        {
    //            return i;
    //        }

    //        i++;
    //    }

    //    // The text does not exist in the part. Create the SharedStringItem and return its index.
    //    shareStringPart.SharedStringTable.AppendChild(new SharedStringItem(new DocumentFormat.OpenXml.Spreadsheet.Text(text)));
    //    shareStringPart.SharedStringTable.Save();

    //    return i;
    //}
    //internal static void InsertDT2(string doc, DataTable dt)
    //{
    //    int tblborder = 2;
    //    StreamWriter SW;
    //    SW = File.CreateText(doc);
    //    StringBuilder objSB = new StringBuilder();
    //    objSB.Append("<Table border=" + tblborder + "  width=100%>");

    //    objSB.Append("<tr>");

    //    for (int i = 0; i < dt.Columns.Count; i++)
    //    {
    //        objSB.Append("<th valign=middle>" + dt.Columns[i].ColumnName + "</th>");
    //    }

    //    objSB.Append("</tr>");
    //    objSB.Append("<tr>");

    //    for (int i = 0; i < dt.Rows.Count; i++)
    //    {
    //        for (int j = 0; j < dt.Columns.Count; j++)
    //        {
    //            objSB.Append("<tr>");
    //            objSB.Append("<td align=center>" + dt.Rows[i][j].ToString() + "</td>");
    //            objSB.Append("</tr>");
    //        }
    //    }

    //    objSB.Append("</Table>");
    //    SW.Write(objSB.ToString());
    //    SW.Close();
    //}
    //internal static void InsertDT(string docName, DataTable dt)
    //{

    //    // Create a spreadsheet document by supplying the filepath.
    //    // By default, AutoSave = true, Editable = true, and Type = xlsx.
    //    SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.
    //        Create(docName, SpreadsheetDocumentType.Workbook);

    //    // Add a WorkbookPart to the document.
    //    WorkbookPart workbookpart = spreadsheetDocument.AddWorkbookPart();
    //    workbookpart.Workbook = new Workbook();

    //    // Add a WorksheetPart to the WorkbookPart.
    //    WorksheetPart worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
    //    worksheetPart.Worksheet = new Worksheet(new SheetData());

    //    //// Add Sheets to the Workbook.
    //    //Sheets sheets = spreadsheetDocument.WorkbookPart.Workbook.
    //    //    AppendChild<Sheets>(new Sheets());

    //    //// Append a new worksheet and associate it with the workbook.
    //    //Sheet sheet = new Sheet()
    //    //{
    //    //    Id = spreadsheetDocument.WorkbookPart.
    //    //    GetIdOfPart(worksheetPart),
    //    //    SheetId = 1,
    //    //    Name = "mySheet"
    //    //};

    //    int rowIndex = 1;
    //    foreach (DataRow dr in dt.Rows)
    //    {
    //        int colIndex = 0;
    //        foreach (object tcell in dr.ItemArray)
    //        {
    //            string text = tcell.ToString();

    //            // Insert cell into the new worksheet.
    //            // Get the SharedStringTablePart. If it does not exist, create a new one.
    //            SharedStringTablePart shareStringPart;
    //            if (spreadsheetDocument.WorkbookPart.GetPartsOfType<SharedStringTablePart>().Count() > 0)
    //            {
    //                shareStringPart = spreadsheetDocument.WorkbookPart.GetPartsOfType<SharedStringTablePart>().First();
    //            }
    //            else
    //            {
    //                shareStringPart = spreadsheetDocument.WorkbookPart.AddNewPart<SharedStringTablePart>();
    //            }

    //            // Insert the text into the SharedStringTablePart.
    //            int index = InsertSharedStringItem(text, shareStringPart);
    //            char c = (char)('A' + colIndex);
    //            Cell cell = InsertCellInWorksheet(c.ToString(), (uint)rowIndex, worksheetPart);

    //            // Set the value of cell A1.
    //            cell.CellValue = new CellValue(text);

    //            colIndex++;
    //        }
    //        rowIndex++;
    //    }
    //    //sheets.Append(sheet);

    //    workbookpart.Workbook.Save();

    //    // Close the document.
    //    spreadsheetDocument.Close();

    //}



//}
#endregion
}
