using Hrms.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrms
{
    class Program
    {

        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Attendance Report Generation Started...");
                //Future Dependices can be injected from outside
                AttendanceExtractor attendanceExtractor = new AttendanceExtractor();

                DataSet attendanceDetails;
                List<Employee> allEmployeeDetails = new List<Employee>();
                var attendaceDetails = attendanceExtractor.GetAttendanceInformation(string.Empty, out attendanceDetails, out allEmployeeDetails);

                //Report Generator
                ReportGenerator reportGenerator = new ReportGenerator();
                if (allEmployeeDetails.Any())
                {
                    reportGenerator.WriteDataReport(allEmployeeDetails);
                }

                //Send Email
                EmailNotification emailNotify = new EmailNotification();
                emailNotify.sendNotification(string.Empty);

                Console.WriteLine("Attendance Report Generation Completed...");

            }
            catch (Exception ex)
            {
                Console.WriteLine("Excepetion ex {0} occured", ex.Message);
            }
        }
    }
}
