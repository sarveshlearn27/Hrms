using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hrms.Core.Parsers;
using System.Configuration;
using System.Data;

namespace Hrms.Core
{
    public class ExcelParser
    {
        public CMResult GetAttendanceInformation(string fileName)
        {
            var cmResult = new CMResult();
            try
            {
                fileName = string.IsNullOrWhiteSpace(fileName) ? ConfigurationManager.AppSettings["AttendanceExcelFilePath"] : fileName;
                if (!string.IsNullOrWhiteSpace(fileName))
                {
                    var attendanceInformation = DataReader.ReadExcel(fileName);
                }
                else
                {
                    cmResult.CMResultErrorCode = ErrorCodes.Failure;
                    return cmResult;
                }
            }
            catch (Exception ex)
            {
                // Logs needs to be added
                cmResult.ErrorMessage = ex.Message;
                cmResult.CMResultErrorCode = ErrorCodes.Failure;
                return cmResult;
            }

            return cmResult;
        }
    }
}
