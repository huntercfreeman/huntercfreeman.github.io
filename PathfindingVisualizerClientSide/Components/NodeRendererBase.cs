using Microsoft.AspNetCore.Components;
using PathfindingVisualizerClientSide.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PathfindingVisualizerClientSide.Components
{
    public class NodeRendererBase : ComponentBase
    {
        [Inject]
        public GridState GridState { get; set; }

        [Parameter]
        public Node MyNode { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            MyNode.RerenderEventHandler += OnRerenderEvent;
        }

        public void OnRerenderEvent(object sender, EventArgs e)
        {
            StateHasChanged();
        }

        public void CallStateHasChanged()
        {
            StateHasChanged();
        }

        public void OnMouseDown()
        {
            GridState.MouseDown = true;
            if (GridState.Draw == GridState.DrawState.Wall)
            {
                MyNode.IsWall = !MyNode.IsWall;

                StateHasChanged();
            }
            else if(GridState.Draw == GridState.DrawState.Start)
            {
                GridState.Grid[GridState.StartNodeRow][GridState.StartNodeColumn].IsStart = false;

                MyNode.IsStart = true;
                GridState.StartNodeRow = MyNode.Row;
                GridState.StartNodeColumn = MyNode.Column;

                GridState.RerenderEventInvoke(new EventArgs());
            }
            else if(GridState.Draw == GridState.DrawState.Finish)
            {
                GridState.Grid[GridState.FinishNodeRow][GridState.FinishNodeColumn].IsFinish = false;

                MyNode.IsFinish = true;
                GridState.FinishNodeRow = MyNode.Row;
                GridState.FinishNodeColumn = MyNode.Column;

                GridState.RerenderEventInvoke(new EventArgs());
            }
            else if (GridState.Draw == GridState.DrawState.Weight)
            {
                if (MyNode.Weight != 2)
                {
                    MyNode.Weight = 2;
                }
                else
                {
                    MyNode.Weight = 0;
                }

                StateHasChanged();
            }
        }

        public void OnMouseEnter()
        {
            if (GridState.MouseDown == true)
            {
                if (GridState.Draw == GridState.DrawState.Wall)
                {
                    MyNode.IsWall = !MyNode.IsWall;

                    StateHasChanged();
                }
                else if (GridState.Draw == GridState.DrawState.Start)
                {
                    GridState.Grid[GridState.StartNodeRow][GridState.StartNodeColumn].IsStart = false;

                    MyNode.IsStart = true;
                    GridState.StartNodeRow = MyNode.Row;
                    GridState.StartNodeColumn = MyNode.Column;

                    GridState.RerenderEventInvoke(new EventArgs());
                }
                else if (GridState.Draw == GridState.DrawState.Finish)
                {
                    GridState.Grid[GridState.FinishNodeRow][GridState.FinishNodeColumn].IsFinish = false;

                    MyNode.IsFinish = true;
                    GridState.FinishNodeRow = MyNode.Row;
                    GridState.FinishNodeColumn = MyNode.Column;

                    GridState.RerenderEventInvoke(new EventArgs());
                }
                else if (GridState.Draw == GridState.DrawState.Weight)
                {
                    if(MyNode.Weight != 2)
                    {
                        MyNode.Weight = 2;
                    }
                    else
                    {
                        MyNode.Weight = 0;
                    }
                    
                    StateHasChanged();
                }
            }
        }

        public void OnMouseUp()
        {
            GridState.MouseDown = false;
        }
    }
}
