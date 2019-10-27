using PathfindingVisualizerClientSide.Algorithms;
using PathfindingVisualizerClientSide.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PathfindingVisualizerClientSide.Models
{
    public class GridState
    {
        public enum DrawState
        {
            Wall, Start, Finish, Weight
        }

        public enum AlgorithmState
        {
            Dijkstra, AStar
        }

        public enum SpeedState
        {
            VerySlow, Slow, Medium, Fast, VeryFast
        }

        public bool MouseDown { get; set; }
        public DrawState Draw { get; set; }

        public SpeedState Speed { get; set; } = SpeedState.VeryFast;
        public int SpeedInt {
            get
            {
                switch (Speed)
                {
                    case SpeedState.VerySlow:
                        return 200;
                    case SpeedState.Slow:
                        return 100;
                    case SpeedState.Medium:
                        return 50;
                    case SpeedState.Fast: 
                        return 25;
                    case SpeedState.VeryFast:
                        return 1;
                    default:
                        return 1;
                }
            }
        }

        public AlgorithmState Algorithm { get; set; }
        public string AlgorithmString 
        { 
            get
            {
                if (Algorithm == 0) return "Dijkstra";
                else return "A*";
            } 
        }

        public List<NodeRendererBase> NodeBaseRenderers { get; set; } = new List<NodeRendererBase>();
        public int StartNodeRow { get; set; } = 10;
        public int StartNodeColumn { get; set; } = 5;

        public int FinishNodeRow { get; set; } = 10;
        public int FinishNodeColumn { get; set; } = 35;

        public List<List<Node>> Grid { get; set; } = new List<List<Node>>();

        public event EventHandler RerenderEventHandler;

        public void RerenderEventInvoke(EventArgs e)
        {
            EventHandler handler = RerenderEventHandler;
            handler?.Invoke(this, e);
        }

        public void SetAlgorithmState(string algorithmState)
        {
            GridState.AlgorithmState algorithm;
            bool success = Enum.TryParse(algorithmState, out algorithm);

            if (success)
            {
                Algorithm = algorithm;
            }

            RerenderEventInvoke(new EventArgs());
        }

        public void SetSpeedState(string speed)
        {
            GridState.SpeedState speedState;
            bool success = Enum.TryParse(speed, out speedState);

            if (success)
            {
                Speed = speedState;
            }

            RerenderEventInvoke(new EventArgs());
        }

        public void ClearTheBoard()
        {
            ResetNodes();
        }

        public void FullReset()
        {
            foreach (List<Node> row in Grid)
            {
                foreach (Node node in row)
                {
                    node.IsShortestPath = false;
                    node.IsVisited = false;
                    node.DrawVisited = false;
                    node.PreviousNode = null;
                    node.Distance = 100000000;
                    node.Weight = 0;
                    node.IsWall = false;
                }
            }
            Grid[StartNodeRow][StartNodeColumn].IsStart = false;

            StartNodeRow = 10;
            StartNodeColumn = 5;
            Grid[StartNodeRow][StartNodeColumn].IsStart = true;

            Grid[FinishNodeRow][FinishNodeColumn].IsFinish = false;

            FinishNodeRow = 10;
            FinishNodeColumn = 35;
            Grid[FinishNodeRow][FinishNodeColumn].IsFinish = true;
            RerenderEventInvoke(new EventArgs());
        }

        public void SetDrawState(string drawState)
        {
            DrawState draw;
            bool success = Enum.TryParse(drawState, out draw);

            if (success)
            {
                Draw = draw;
            }
            RerenderEventInvoke(new EventArgs());
        }

        public async void RunAlgorithm()
        {
            ResetNodes();
            IAlgorithm algorithm;

            switch(Algorithm)
            {
                case AlgorithmState.Dijkstra:
                    algorithm = new Dijkstra();
                    break;
                case AlgorithmState.AStar:
                    algorithm = new AStar();
                    break;
                default:
                    algorithm = new Dijkstra();
                    break;
            }

            List<Node> nodesInOrderVisited = algorithm.Run(Grid, Grid[StartNodeRow][StartNodeColumn], Grid[FinishNodeRow][FinishNodeColumn]);
            await VisualizeAlgorithm(nodesInOrderVisited);

            List<Node> shortestPath = algorithm.GetNodesInShortestPathOrder(Grid[FinishNodeRow][FinishNodeColumn]);
            await VisualizeShortestPath(shortestPath);
        }

        public void ResetNodes()
        {
            foreach (List<Node> row in Grid)
            {
                foreach (Node node in row)
                {
                    node.IsShortestPath = false;
                    node.IsVisited = false;
                    node.DrawVisited = false;
                    node.PreviousNode = null;
                    node.Distance = 100000000;
                }
            }
            RerenderEventInvoke(new EventArgs());
        }

        public async Task VisualizeAlgorithm(List<Node> nodesInOrderVisited)
        {
            foreach (Node node in nodesInOrderVisited)
            {
                node.DrawVisited = true;
                //RerenderEventInvoke(new EventArgs());
                //NodeBaseRenderers[20 * node.Row + node.Column].CallStateHasChanged();
                // now rerenders by firing an event on the set
                await Task.Delay(SpeedInt);
            }
        }

        public async Task VisualizeShortestPath(List<Node> shortestPath)
        {
            foreach (Node node in shortestPath)
            {
                node.IsShortestPath = true;
                //RerenderEventInvoke(new EventArgs());
                //NodeBaseRenderers[19 * node.Row + node.Column].CallStateHasChanged();
                // now rerenders by firing an event on the set
                await Task.Delay(5);
            }
        }
    }
}
