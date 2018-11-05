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

        public List<string> Status { get; set; }

        public List<DateTime> InTimings { get; set; }

        public List<DateTime> OutTimings { get; set; }

        public List<string> Duration { get; set; }

        public List<string> LateBy { get; set; }
        public List<string> EarlyBy { get; set; }
        public List<string> OT { get; set; }

        public List<string> Shift { get; set; }

        public int LateMarks { get; set; }

        public int TotalHalfDays { get; set; }

        public float TotalDeductions { get; set; }

        public Employee()
        {
            if (LateMarks != 0)
            {
                TotalHalfDays = LateMarks / 4;
            }
        }
    }
}
