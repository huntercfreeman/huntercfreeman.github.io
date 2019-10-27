using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PathfindingVisualizerClientSide.Models
{
    public class DropdownItem
    {
        public string Title { get; set; }
        public Action<string> Action { get; set; }
        public string ActionParameter { get; set; }
    }
}
