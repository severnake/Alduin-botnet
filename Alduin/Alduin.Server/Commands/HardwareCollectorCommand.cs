using System.Collections.Generic;

namespace Alduin.Commands
{
    public class HardwareDetails
    {
        public string HardwareName { get; set; }
        public string HardwareType { get; set; }
        public string Performance { get; set; }
        public string OtherInformation { get; set; }
    }

    public class HardwareCollectorCommand
    {
        public HardwareDetails Cpu { get; set; }
        public HardwareDetails Gpu { get; set; }
        public HardwareDetails Ram { get; set; }
        public HardwareDetails Os { get; set; }
        public List<List<HardwareDetails>> OtherHarwares { get; set; }
    }
}
