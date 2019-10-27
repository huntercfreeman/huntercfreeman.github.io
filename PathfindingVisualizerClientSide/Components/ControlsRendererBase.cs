using Microsoft.AspNetCore.Components;
using PathfindingVisualizerClientSide.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PathfindingVisualizerClientSide.Components
{
    public class ControlsRendererBase : ComponentBase
    {
        [Inject]
        public GridState GridState { get; set; }
        public List<string> NodeRendererClassesForStart { get; set; }
        public List<string> NodeRendererClassesForFinish { get; set; }
        public List<string> NodeRendererClassesForWeighted { get; set; }
        public List<string> NodeRendererClassesForBomb { get; set; }
        public List<string> NodeRendererClassesForWall { get; set; }
        public List<string> NodeRendererClassesForUnvisited { get; set; }
        public List<string> NodeRendererClassesForVisited { get; set; }
        public List<string> NodeRendererClassesForShortest { get; set; }

        protected override void OnInitialized()
        {
            string classesForStart = "isStart";
            NodeRendererClassesForStart = new List<string> { classesForStart };

            string classesForFinish = "isFinish";
            NodeRendererClassesForFinish = new List<string> { classesForFinish };

            string classesForWeighted = "isWeighted";
            NodeRendererClassesForWeighted = new List<string> { classesForWeighted };

            string classesForBomb = "isBomb";
            NodeRendererClassesForBomb = new List<string> { classesForBomb };

            string classesForWall = "isWall";
            NodeRendererClassesForWall = new List<string> { classesForWall };

            string classesForUnvisited = "";
            NodeRendererClassesForUnvisited = new List<string> { classesForUnvisited };

            string classesForVisited = "drawVisited";
            NodeRendererClassesForVisited = new List<string> { classesForVisited };

            string classesForShortest = "drawVisited isShortest";
            NodeRendererClassesForShortest = new List<string> { classesForShortest };
            
            base.OnInitialized();
            GridState.RerenderEventHandler += OnRerenderEvent;
        }

        public void OnRerenderEvent(object sender, EventArgs e)
        {
            StateHasChanged();
        }
    }
}
