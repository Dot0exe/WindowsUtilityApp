using Excel = Microsoft.Office.Interop.Excel;
using System.Collections.Generic;
using WindowsUtilityApp;

public class ExcelReader
{
    public List<ApplicationInfo> ReadExcel(string filePath)
    {
        var appList = new List<ApplicationInfo>();
        Excel.Application xlApp = new Excel.Application();
        Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(filePath);
        Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
        Excel.Range xlRange = xlWorksheet.UsedRange;

        for (int i = 1; i <= xlRange.Rows.Count; i++)
        {
            string appName = xlRange.Cells[i, 1].Value2.ToString();
            string appId = xlRange.Cells[i, 2].Value2.ToString();
            appList.Add(new ApplicationInfo { Name = appName, Id = appId });
        }

        xlWorkbook.Close();
        xlApp.Quit();

        return appList;
    }
}
