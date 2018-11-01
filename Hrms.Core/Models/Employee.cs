using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Core
{
    public class Employee
    {
        public string Name { get; set; }

        public List<DateTime> InTimings { get; set; }

        public List<DateTime> OutTimings { get; set; }

        public int LateMarks { get; set; }

        public int TotalDeductions { get; set; }

        public Employee()
        {
            if (LateMarks != 0)
            {
                TotalDeductions = LateMarks / 4;
            }
        }
    }
}
