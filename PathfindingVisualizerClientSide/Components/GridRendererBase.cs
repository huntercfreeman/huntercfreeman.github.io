using Microsoft.AspNetCore.Components;
using PathfindingVisualizerClientSide.Algorithms;
using PathfindingVisualizerClientSide.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PathfindingVisualizerClientSide.Components
{
    public class GridRendererBase : ComponentBase
    {
        [Inject]
        public GridState GridState { get; set; }

        protected override void OnInitialized()
        {
            GridState.RerenderEventHandler += OnRerenderEvent;
            for (int row = 0; row < 20; row++)
            {
                List<Node> newRow = new List<Node>();
                for(int column = 0; column < 40; column++)
                {
                    newRow.Add(new Node { Class = "default", Row = row, Column = column, IsStart = (row == GridState.StartNodeRow) && (column == GridState.StartNodeColumn), IsFinish = (row == GridState.FinishNodeRow) && (column == GridState.FinishNodeColumn) });
                }
                GridState.Grid.Add(newRow);
            }
            base.OnInitialized();
        }

        public void OnRerenderEvent(object sender, EventArgs e)
        {
            StateHasChanged();
        }
    }
}
