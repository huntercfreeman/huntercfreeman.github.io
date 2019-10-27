using Microsoft.AspNetCore.Components;
using PathfindingVisualizerClientSide.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PathfindingVisualizerClientSide.Shared
{
    public class TopRowButtonBase : ComponentBase
    {
        [Parameter]
        public Action OnClick { get; set; } = (() => Console.WriteLine("No onclick was provided"));
        [Parameter]
        public string Title { get; set; }
        [Parameter]
        public List<DropdownItem> DropdownItems { get; set; }
        [Parameter]
        public string GetStringForDisplay { get; set; } = "";
        [Parameter]
        public string CSSClasses { get; set; } = "";

        public bool DisplayDropdownItem { get; set; }

        public void FlipDisplayDropdownItems()
        {
            DisplayDropdownItem = !DisplayDropdownItem;
        }
    }
}
