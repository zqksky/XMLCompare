using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.IO;
using Microsoft.Office.Interop.Excel;

namespace xmlCompare
{
    class ExcelOperator
    {
        /// <summary>
        /// Takes a CSV file and sucks it into the specified worksheet of this workbook at the specified range
        /// </summary>
        /// <param name="importFileName">Specifies the full path to the .CSV file to import</param>
        /// <param name="destinationSheet">Excel.Worksheet object corresponding to the destination worksheet.</param>
        /// <param name="destinationRange">Excel.Range object specifying the destination cell(s)</param>
        /// <param name="columnDataTypes">Column data type specifier array. For the QueryTable.TextFileColumnDataTypes property.</param>
        /// <param name="autoFitColumns">Specifies whether to do an AutoFit on all imported columns.</param>
        public void ImportCSV(string importFileName, Microsoft.Office.Interop.Excel.Worksheet destinationSheet,
          Microsoft.Office.Interop.Excel.Range destinationRange, int[] columnDataTypes, bool autoFitColumns)
        {
            destinationSheet.QueryTables.Add("TEXT;" + Path.GetFullPath(importFileName), destinationRange, Type.Missing);
            destinationSheet.QueryTables[1].Name = Path.GetFileNameWithoutExtension(importFileName);
            destinationSheet.QueryTables[1].FieldNames = true;
            destinationSheet.QueryTables[1].RowNumbers = false;
            destinationSheet.QueryTables[1].FillAdjacentFormulas = false;
            destinationSheet.QueryTables[1].PreserveFormatting = true;
            destinationSheet.QueryTables[1].RefreshOnFileOpen = false;
            destinationSheet.QueryTables[1].RefreshStyle = XlCellInsertionMode.xlInsertDeleteCells;
            destinationSheet.QueryTables[1].SavePassword = false;
            destinationSheet.QueryTables[1].SaveData = true;
            destinationSheet.QueryTables[1].AdjustColumnWidth = true;
            destinationSheet.QueryTables[1].RefreshPeriod = 0;
            destinationSheet.QueryTables[1].TextFilePromptOnRefresh = false;
            destinationSheet.QueryTables[1].TextFilePlatform = 437;
            destinationSheet.QueryTables[1].TextFileStartRow = 1;
            destinationSheet.QueryTables[1].TextFileParseType = XlTextParsingType.xlDelimited;
            destinationSheet.QueryTables[1].TextFileTextQualifier = XlTextQualifier.xlTextQualifierDoubleQuote;
            destinationSheet.QueryTables[1].TextFileConsecutiveDelimiter = false;
            destinationSheet.QueryTables[1].TextFileTabDelimiter = false;
            destinationSheet.QueryTables[1].TextFileSemicolonDelimiter = false;
            destinationSheet.QueryTables[1].TextFileCommaDelimiter = true;
            destinationSheet.QueryTables[1].TextFileSpaceDelimiter = false;
            destinationSheet.QueryTables[1].TextFileColumnDataTypes = columnDataTypes;
            //Logger.GetInstance().WriteLog("Importing data...");
            destinationSheet.QueryTables[1].Refresh(false);
            if (autoFitColumns == true)
            {
                destinationSheet.QueryTables[1].Destination.EntireColumn.AutoFit();
            }

            // cleanup
            //this.ActiveSheet.QueryTables[1].Delete();
            destinationSheet.QueryTables[1].Delete();
        }
        //ImportCSV(@"C:\MyStuff\MyFile.CSV",(Microsoft.Office.Interop.Excel.Worksheet)(MyWorkbook.Worksheets[1]),
        //    (Microsoft.Office.Interop.Excel.Range)(((Microsoft.Office.Interop.Excel.Worksheet)MyWorkbook.Worksheets[1]).get_Range("$A$7")),
        //    new int[] { 2, 2, 2, 2, 2 }, true);

        public string mFilename;
        public Microsoft.Office.Interop.Excel.Application app;
        public Microsoft.Office.Interop.Excel.Workbooks wbs;
        public Microsoft.Office.Interop.Excel.Workbook wb;
        public Microsoft.Office.Interop.Excel.Worksheets wss;
        public Microsoft.Office.Interop.Excel.Worksheet ws;
        public ExcelOperator()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        public void Create()//创建一个Microsoft.Office.Interop.Excel对象
        {
            app = new Microsoft.Office.Interop.Excel.Application();
            wbs = app.Workbooks;
            wb = wbs.Add(true);
        }
        public void Open(string FileName)//打开一个Microsoft.Office.Interop.Excel文件
        {
            app = new Microsoft.Office.Interop.Excel.Application();
            wbs = app.Workbooks;
            wb = wbs.Add(FileName);
            //wb = wbs.Open(FileName, 0, true, 5,"", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "t", false, false, 0, true,Type.Missing,Type.Missing);
            //wb = wbs.Open(FileName,Type.Missing,Type.Missing,Type.Missing,Type.Missing,Type.Missing,Type.Missing,Microsoft.Office.Interop.Excel.XlPlatform.xlWindows,Type.Missing,Type.Missing,Type.Missing,Type.Missing,Type.Missing,Type.Missing,Type.Missing);
            mFilename = FileName;
        }
        //获取一个工作表
        public Microsoft.Office.Interop.Excel.Worksheet GetSheet(string SheetName)
        {
            Microsoft.Office.Interop.Excel.Worksheet s = (Microsoft.Office.Interop.Excel.Worksheet)wb.Worksheets[SheetName];
            return s;
        }
        //添加一个工作表
        public Microsoft.Office.Interop.Excel.Worksheet AddSheet(string SheetName)
        {
            for (int i = 0; i < app.Worksheets.Count; i++)
            {
                if (SheetName == ((Worksheet)app.Worksheets[i + 1]).Name)
                {
                    app.DisplayAlerts = false; //注意一定要加上这句
                    ((Worksheet)wb.Worksheets[i + 1]).Delete();
                    app.DisplayAlerts = true;//注意一定要加上这句
                    break;
                }
                else
                {

                }
            }
            Microsoft.Office.Interop.Excel.Worksheet s = (Microsoft.Office.Interop.Excel.Worksheet)wb.Worksheets.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            s.Name = SheetName;
            return s;
        }

        //删除一个工作表
        public void DelSheet(string SheetName)
        {
            ((Microsoft.Office.Interop.Excel.Worksheet)wb.Worksheets[SheetName]).Delete();
        }

        public Microsoft.Office.Interop.Excel.Worksheet ReNameSheet(string OldSheetName, string NewSheetName)//重命名一个工作表一
        {
            Microsoft.Office.Interop.Excel.Worksheet s = (Microsoft.Office.Interop.Excel.Worksheet)wb.Worksheets[OldSheetName];
            s.Name = NewSheetName;
            return s;
        }

        public Microsoft.Office.Interop.Excel.Worksheet ReNameSheet(Microsoft.Office.Interop.Excel.Worksheet Sheet, string NewSheetName)//重命名一个工作表二
        {

            Sheet.Name = NewSheetName;

            return Sheet;
        }

        public bool Save()
        //保存文档
        {
            if (mFilename == "")
            {
                return false;
            }
            else
            {
                try
                {
                    wb.Save();
                    return true;
                }

                catch (Exception ex)
                {
                    return false;
                }
            }
        }
        public bool SaveAs(object FileName)
        //文档另存为
        {
            try
            {
                wb.SaveAs(FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                return true;

            }

            catch (Exception ex)
            {
                return false;

            }
        }
        public void Close()
        //关闭一个Microsoft.Office.Interop.Excel对象，销毁对象
        {
            //wb.Save();
            wb.Close(Type.Missing, Type.Missing, Type.Missing);
            wbs.Close();
            app.Quit();
            wb = null;
            wbs = null;
            app = null;
            GC.Collect();
        }
    }
}
