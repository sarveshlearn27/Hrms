using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Core
{
    public static class DataReader
    {
        public static DataTable ReadExcel(string fileName)
        {
            string conn = string.Empty;
            List<string> excelSheets = new List<string>();
            DataTable dt = new DataTable();

            DataTable dtExcel = new DataTable();

            conn = @"provider=Microsoft.Jet.OLEDB.4.0;
            Data Source=" + fileName + ";Extended Properties='Excel 8.0;HRD=Yes;IMEX=1';";
            using (OleDbConnection con = new OleDbConnection(conn))
            {
                try
                {
                    // Get the data table containg the schema guid.
                    dt = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    if (dt == null)
                    {
                        return null;
                    }

                    // Add the sheet name to the string array.
                    foreach (DataRow row in dt.Rows)
                    {
                        excelSheets.Add(row["TABLE_NAME"].ToString());
                    }

                    //Loop through all of the sheets...
                    if (excelSheets.Any())
                    {
                        foreach (var sheet in excelSheets)
                        {
                            OleDbDataAdapter oleAdpt = new OleDbDataAdapter("select * from [sheet]", con); //here we read data from sheet1  
                            oleAdpt.Fill(dtExcel); //fill excel data into dataTable  
                        }
                    }

                }
                catch { }
            }
            return dtExcel;
        }
    }
}
