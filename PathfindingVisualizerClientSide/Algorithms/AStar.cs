using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PathfindingVisualizerClientSide.Models;
using Priority_Queue;

namespace PathfindingVisualizerClientSide.Algorithms
{
    public class AStar : IAlgorithm
    {
        public SimplePriorityQueue<Node> OpenSet { get; set; } = new SimplePriorityQueue<Node>();
        public List<Node> ClosedSet { get; set; } = new List<Node>();

        public List<Node> Run(List<List<Node>> grid, Node startNode, Node finishNode)
        {
            startNode.Distance = 0;
            List<Node> visitedNodesInOrder = new List<Node>();
            ClosedSet.Add(startNode);
            startNode.IsVisited = true;
            UpdateUnvisitedNeighbors(startNode, finishNode, grid);

            while (true)
            {
                Node bestNode;

                try
                {
                    bestNode = OpenSet.Dequeue();
                }
                catch(Exception e)
                {
                    return visitedNodesInOrder; // Set is empty
                }

                bestNode.IsVisited = true;
                if (bestNode.IsWall)
                {
                    continue;
                }
                visitedNodesInOrder.Add(bestNode);
                if (bestNode.IsFinish == true) return visitedNodesInOrder;
                UpdateUnvisitedNeighbors(bestNode, finishNode, grid);
            }
        }

        public void UpdateUnvisitedNeighbors(Node node, Node finishNode, List<List<Node>> grid)
        {
            SimplePriorityQueue<Node> unvisitedNeighbors = GetUnvisitedNeighbors(node, grid);
            foreach (Node neighbor in unvisitedNeighbors)
            {
                neighbor.GFunction = node.GFunction + 1 + neighbor.Weight;

                neighbor.PreviousNode = node;

                neighbor.HFunction = CalculateHFunction(neighbor, finishNode);

                int fFunction = neighbor.GFunction + neighbor.HFunction;
                
                if(OpenSet.Contains(neighbor))
                {
                    if (fFunction < OpenSet.GetPriority(neighbor))
                        OpenSet.UpdatePriority(neighbor, fFunction);
                }
                else
                {
                    OpenSet.Enqueue(neighbor, fFunction);
                }
            }
        }

        public int CalculateHFunction(Node node, Node finishNode)
        {
            int xDistance = node.Column - finishNode.Column;
            if (xDistance < 0) xDistance *= -1;

            int yDistance = node.Row - finishNode.Row;
            if (yDistance < 0) yDistance *= -1;

            return xDistance + yDistance;
        }

        public SimplePriorityQueue<Node> GetUnvisitedNeighbors(Node node, List<List<Node>> grid)
        {
            SimplePriorityQueue<Node> neighbors = new SimplePriorityQueue<Node>();
            int row = node.Row;
            int column = node.Column;

            if (row > 0) neighbors.Enqueue(grid[row - 1][column], 1000000);
            if (row < grid.Count - 1) neighbors.Enqueue(grid[row + 1][column], 1000000);
            if (column > 0) neighbors.Enqueue(grid[row][column - 1], 0);
            if (column < grid[0].Count - 1) neighbors.Enqueue(grid[row][column + 1], 1000000);
            foreach (Node node1 in neighbors)
            {
                if (node1.IsVisited)
                {
                    neighbors.Remove(node1);
                }
            }
            return neighbors;
        }

        public void ResetDistances(List<Node> nodes)
        {
            foreach (Node node in nodes)
            {
                node.Distance = 100000000;
            }
        }

        public List<Node> SortNodesByDistance(List<Node> nodes)
        {
            return nodes.OrderBy(x => x.Distance).ToList<Node>();
        }


        public List<Node> GetNodesInShortestPathOrder(Node finishNode)
        {
            List<Node> nodesInShortestPathOrder = new List<Node>();
            Node currentNode = finishNode;
            while (currentNode != null)
            {
                nodesInShortestPathOrder.Insert(0, currentNode);
                currentNode = currentNode.PreviousNode;
            }
            return nodesInShortestPathOrder;
        }
    }
}
