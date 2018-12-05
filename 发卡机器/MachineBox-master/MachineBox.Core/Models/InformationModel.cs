using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineBox.Core.Models
{
    public class InformationModel
    {
        public string IpAddress   { get; set; }
        public string MacAddress  { get; set; }
        public string MachineName { get; set; }
        public string UserName    { get; set; }
        public string DomainName  { get; set; }
    }
}
