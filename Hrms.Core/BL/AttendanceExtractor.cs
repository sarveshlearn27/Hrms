using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hrms.Core.Parsers;
using System.Configuration;
using System.Data;
using System.Globalization;

namespace Hrms.Core
{
    public class AttendanceExtractor
    {
        /// <summary>
        /// Get Attendance Information from Excel
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="attendanceInformation"></param>
        /// <param name="employeeDetails"></param>
        /// <returns></returns>
        public CMResult GetAttendanceInformation(string fileName, out DataSet attendanceInformation, out List<Employee> employeeDetails)
        {
            var cmResult = new CMResult();
            attendanceInformation = new DataSet();
            employeeDetails = new List<Employee>();

            try
            {
                fileName = string.IsNullOrWhiteSpace(fileName) ? ConfigurationManager.AppSettings["AttendanceExcelFilePath"] : fileName;
                if (!string.IsNullOrWhiteSpace(fileName))
                {
                    attendanceInformation = DataReader.ReadExcel(fileName);
                    employeeDetails = MappAttendanceToEmployee(attendanceInformation);
                }
                else
                {
                    cmResult.CMResultErrorCode = ErrorCodes.Failure;
                    return cmResult;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return cmResult;
        }

        private List<Employee> MappAttendanceToEmployee(DataSet attendanceInformation)
        {
            var attendanceDetailsOfAllEmployees = new List<Employee>();

            var tables = attendanceInformation.Tables;

            foreach (DataTable table in tables)
            {
                DataRowCollection allRows = table.Rows;

                for (int i = 0; i < allRows.Count; i++)
                {
                    for (int j = 0; j < table.Columns.Count; j++)
                    {
                        var excelEntry = allRows[i][j].ToString();

                        if (!string.IsNullOrWhiteSpace(excelEntry) && excelEntry.Contains("Employee:"))
                        {
                            Employee emp = new Employee();
                            emp.Name = allRows[i][j + 1].ToString();

                            //Get Status
                            i++;
                            emp.Status = GetRowContents(allRows[i]);

                            //Get InTime
                            i++;
                            emp.InTimings = GetRowContents(allRows[i]).Select(date => DateTime.Parse(date)).ToList();

                            //Get OutTime
                            i++;
                            emp.OutTimings = GetRowContents(allRows[i]).Select(date => DateTime.Parse(date)).ToList();

                            //Get Duration	
                            i++;
                            emp.Duration = GetRowContents(allRows[i]);

                            //Get LateBy
                            i++;
                            emp.LateBy = GetRowContents(allRows[i]);

                            //Get EarlyBy
                            i++;
                            emp.EarlyBy = GetRowContents(allRows[i]);

                            //Get OT
                            i++;
                            emp.OT = GetRowContents(allRows[i]);

                            //Get Shifts
                            i++;
                            emp.Shift = GetRowContents(allRows[i]);

                            //Get Late Marks
                            GetLateMarksAndDeductions(ref emp);

                            //Add to list
                            attendanceDetailsOfAllEmployees.Add(emp);

                            break;
                        }
                    }
                }
            }
            return attendanceDetailsOfAllEmployees;
        }

        private List<string> GetRowContents(DataRow rowCollection)
        {
            var rowInfromation = new List<string>();

            for (var i = 1; i < rowCollection.ItemArray.Length; i++)
            {
                if (!string.IsNullOrWhiteSpace(rowCollection[i].ToString()))
                {
                    rowInfromation.Add(rowCollection[i].ToString());
                }
            }
            return rowInfromation;
        }

        private void GetLateMarksAndDeductions(ref Employee emp)
        {
            var lateArrivalAllowed = DateTime.ParseExact(Common.Constants.MaxAllowedLateArrivalLimit, "HH:mm", CultureInfo.InvariantCulture);

            if (emp.InTimings.Any())
            {
                foreach (var inTime in emp.InTimings)
                {
                    if (inTime > lateArrivalAllowed)
                    {
                        emp.LateMarks++;
                    }
                }

                if (emp.LateMarks > 0)
                {
                    emp.TotalHalfDays = (emp.LateMarks / 4);
                    emp.TotalDeductions = (float)emp.TotalHalfDays / 2;
                }
            }
        }
    }
}
