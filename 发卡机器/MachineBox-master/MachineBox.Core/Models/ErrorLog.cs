using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineBox.Core.Models
{
    public class ErrorLog
    {
        public string ErrorMessage          { get; set; }
        public string Source                { get; set; }
        public string StackTrace            { get; set; }
        public string Target                { get; set; }
        public string InnerExceptionMessage { get; set; }
    }
}
