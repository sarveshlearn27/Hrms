using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Core.DAL
{
    interface IReportGenerator
    {
        void WriteDataToRepository(List<Employee> allEmployeeInformation);
    }
}
