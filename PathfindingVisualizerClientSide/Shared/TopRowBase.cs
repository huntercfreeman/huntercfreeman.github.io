using Microsoft.AspNetCore.Components;
using PathfindingVisualizerClientSide.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PathfindingVisualizerClientSide.Shared
{
    public class TopRowBase : ComponentBase
    {
        [Inject]
        public GridState GridState { get; set; }
        public List<DropdownItem> AlgorithmsDropdownItems { get; set; } = new List<DropdownItem>();
        public List<DropdownItem> MazesAndPatternsDropdownItems { get; set; } = new List<DropdownItem>();
        public List<DropdownItem> SpeedDropdownItems { get; set; } = new List<DropdownItem>();
        public string StringForDisplayForSpeed { get; set; }
        
        protected override void OnInitialized()
        {
            StringForDisplayForSpeed = GridState.SpeedInt.ToString() + "ms";
            GridState.RerenderEventHandler += OnRerenderEvent;
            AlgorithmsDropdownItems.AddRange(
            new List<DropdownItem> {
                new DropdownItem
                {
                    Action = GridState.SetAlgorithmState,
                    Title = "Dijkstra",
                    ActionParameter = "Dijkstra"
                },
                new DropdownItem
                {
                    Action = GridState.SetAlgorithmState,
                    Title = "A*",
                    ActionParameter = "AStar"
                }
            });
            SpeedDropdownItems.AddRange(
            new List<DropdownItem> {
                new DropdownItem
                {
                    Action = GridState.SetSpeedState,
                    Title = "Very Slow",
                    ActionParameter = "VerySlow"
                },
                new DropdownItem
                {
                    Action = GridState.SetSpeedState,
                    Title = "Slow",
                    ActionParameter = "Slow"
                },
                new DropdownItem
                {
                    Action = GridState.SetSpeedState,
                    Title = "Medium",
                    ActionParameter = "Medium"
                },
                new DropdownItem
                {
                    Action = GridState.SetSpeedState,
                    Title = "Fast",
                    ActionParameter = "Fast"
                },
                new DropdownItem
                {
                    Action = GridState.SetSpeedState,
                    Title = "Very Fast",
                    ActionParameter = "VeryFast"
                },
            });
            base.OnInitialized();
        }

        public void OnRerenderEvent(object sender, EventArgs e)
        {
            StringForDisplayForSpeed = GridState.SpeedInt.ToString() + "ms";
            StateHasChanged();
        }
    }
}
