using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System;
using System.Linq;
using ClosedXML.Excel;
using System.Configuration;

namespace Hrms.Core
{
    public class ReportGenerator : IReportGenerator
    {
        public static List<string> colsForReport = new List<string>() { "Name", "LateMarks", "TotalHalfDays", "TotalDeductions" };


        /// <summary>
        /// Write Attendance to Excel Report
        /// </summary>
        /// <param name="allEmployeeInformation"></param>
        public void WriteDataReport(List<Employee> allEmployeeInformation)
        {
            var dataTableTobeWritten = ToDataTable<Employee>(allEmployeeInformation);

            XLWorkbook workbook = new XLWorkbook();
            DataTable table = dataTableTobeWritten;
            workbook.Worksheets.Add(table);

            //Write Data To File
            var fileName = ConfigurationManager.AppSettings["GeneratedReport"];
            workbook.SaveAs(fileName);
        }

        private static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => colsForReport.Contains(p.Name)).ToArray();
            //Get Specific properties
            foreach (PropertyInfo prop in Props)
            {
                //Defining type of data column gives proper data table 
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }
    }
}
