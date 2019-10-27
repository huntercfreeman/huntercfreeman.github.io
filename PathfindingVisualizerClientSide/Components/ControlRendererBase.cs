using Microsoft.AspNetCore.Components;
using PathfindingVisualizerClientSide.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PathfindingVisualizerClientSide.Components
{
    public class ControlRendererBase : ComponentBase
    {
        [Inject]
        public GridState GridState { get; set; }
        public GridState.DrawState DrawState { get; set; }
        [Parameter]
        public Action<string> OnClick { get; set; }
        [Parameter]
        public string OnClickParameter { get; set; } = "";
        [Parameter]
        public List<string> NodeRendererClasses { get; set; } = new List<string>();
        [Parameter]
        public string Title { get; set; } = "";

        protected override void OnInitialized()
        {
            base.OnInitialized();
            GridState.DrawState drawState;
            bool success = Enum.TryParse(OnClickParameter, out drawState);

            if (success)
            {
                DrawState = drawState;
            }
        }
    }
}
