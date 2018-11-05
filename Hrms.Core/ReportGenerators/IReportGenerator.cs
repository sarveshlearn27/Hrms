using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Core
{
    interface IReportGenerator
    {
        void WriteDataReport(List<Employee> allEmployeeInformation);
    }
}
