using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hrms.Core.Parsers
{
    public enum ErrorCodes:int
    {
        Success,
        Failure
    }
    public class CMResult
    {
        public ErrorCodes CMResultErrorCode { get; set; }

        public string ErrorMessage { get; set; }

    }
}
