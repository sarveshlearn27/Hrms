using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Core
{
    public static class DataReader
    {
        public static DataSet ReadExcel(string fileName)
        {
            try
            {
                DataSet dtExcel = new DataSet();
                using (var stream = File.Open(fileName, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        do
                        {
                            while (reader.Read())
                            {
                            }
                        }
                        while (reader.NextResult());
                        dtExcel = reader.AsDataSet();
                    }
                }
                return dtExcel;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
