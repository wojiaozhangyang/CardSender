using MachineBox.Core.Enums;

namespace MachineBox.Core.Models
{
    public class CardReaderResponse
    {
        public ResponseStatuses Status { get; set; }
        public string       Data   { get; set; }
    }
}