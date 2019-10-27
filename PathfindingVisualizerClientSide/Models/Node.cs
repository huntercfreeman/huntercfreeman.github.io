using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PathfindingVisualizerClientSide.Models
{
    public class Node
    {
        public string Id { get; } = "" + Guid.NewGuid();
        private string _class;
        public string Class 
        { 
            get => _class; 
            set 
            {
                _class = value;
                RerenderEventInvoke(new EventArgs());
            } 
        }
        public int Row { get; set; }
        public int Column { get; set; }
        private bool _isVisited;
        public bool IsVisited
        {
            get => _isVisited;
            set
            {
                _isVisited = value;
                RerenderEventInvoke(new EventArgs());
            }
        }
        private bool _isStart;
        public bool IsStart
        {
            get => _isStart;
            set
            {
                _isStart = value;
                RerenderEventInvoke(new EventArgs());
            }
        }
        private bool _isFinish;
        public bool IsFinish
        {
            get => _isFinish;
            set
            {
                _isFinish = value;
                RerenderEventInvoke(new EventArgs());
            }
        }
        private bool _isWall;
        public bool IsWall
        {
            get => _isWall;
            set
            {
                _isWall = value;
                RerenderEventInvoke(new EventArgs());
            }
        }
        public int Distance { get; set; } = 100000000;
        private bool _drawVisited;
        public bool DrawVisited
        {
            get => _drawVisited;
            set
            {
                _drawVisited = value;
                RerenderEventInvoke(new EventArgs());
            }
        }
        private bool _isShortestPath;
        public bool IsShortestPath
        {
            get => _isShortestPath;
            set
            {
                _isShortestPath = value;
                RerenderEventInvoke(new EventArgs());
            }
        }
        private int _weight;
        public int Weight
        {
            get => _weight;
            set
            {
                _weight = value;
                RerenderEventInvoke(new EventArgs());
            }
        }
        public Node PreviousNode { get; set; }
        public int HFunction { get; set; }
        public int GFunction { get; set; }

        public event EventHandler RerenderEventHandler;

        public void RerenderEventInvoke(EventArgs e)
        {
            EventHandler handler = RerenderEventHandler;
            handler?.Invoke(this, e);
        }
    }
}
