using PathfindingVisualizerClientSide.Models;
using Priority_Queue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PathfindingVisualizerClientSide.Algorithms
{
    public class Dijkstra : IAlgorithm
    {
        public SimplePriorityQueue<Node> UnvisitedNodes { get; set; }
        public List<Node> Run(List<List<Node>> grid, Node startNode, Node finishNode)
        {
            UnvisitedNodes = GetAllNodes(grid);
            ResetDistances(UnvisitedNodes);
            List<Node> visitedNodesInOrder = new List<Node>();
            startNode.Distance = 0;
            UnvisitedNodes.UpdatePriority(startNode, 0);

            while(true)
            {
                // unvisitedNodes = SortNodesByDistance(unvisitedNodes);
                Node closestNode = UnvisitedNodes.Dequeue();
                if (closestNode.IsWall)
                {
                    closestNode.IsVisited = true;
                    continue;
                }
                if (closestNode.Distance == 100000000) return visitedNodesInOrder;
                closestNode.IsVisited = true;
                visitedNodesInOrder.Add(closestNode);
                if (closestNode.IsFinish == true) return visitedNodesInOrder;
                Console.WriteLine(closestNode.Id);
                UpdateUnvisitedNeighbors(closestNode, grid);
            }
        }

        public void UpdateUnvisitedNeighbors(Node node, List<List<Node>> grid)
        {
            List<Node> unvisitedNeighbors = GetUnvisitedNeighbors(node, grid);        
            foreach(Node neighbor in unvisitedNeighbors)
            {
                neighbor.Distance = node.Distance + 1 + neighbor.Weight;
                UnvisitedNodes.UpdatePriority(neighbor, node.Distance + 1 + neighbor.Weight);
                neighbor.PreviousNode = node;
            }
        }

        public List<Node> GetUnvisitedNeighbors(Node node, List<List<Node>> grid)
        {
            List<Node> neighbors = new List<Node>();
            int row = node.Row;
            int column = node.Column;

            if (row > 0) neighbors.Add(grid[row - 1][column]);
            if (row < grid.Count - 1) neighbors.Add(grid[row + 1][column]);
            if (column > 0) neighbors.Add(grid[row][column - 1]);
            if (column < grid[0].Count - 1) neighbors.Add(grid[row][column + 1]);
            return neighbors.Where(x => x.IsVisited != true).ToList<Node>();
        }

        public void ResetDistances(SimplePriorityQueue<Node> nodes)
        {
            foreach(Node node in nodes)
            {
                node.Distance = 100000000;
            }
        }

        public SimplePriorityQueue<Node> SortNodesByDistance(SimplePriorityQueue<Node> nodes)
        {
            return nodes;
        }

        public SimplePriorityQueue<Node> GetAllNodes(List<List<Node>> grid)
        {
            SimplePriorityQueue<Node> nodes = new SimplePriorityQueue<Node>();
            foreach(List<Node> row in grid)
            {
                foreach(Node node in row)
                {
                    nodes.Enqueue(node, 100000000);
                }
            }

            return nodes;
        }

        public List<Node> GetNodesInShortestPathOrder(Node finishNode)
        {
            List<Node> nodesInShortestPathOrder = new List<Node>();
            Node currentNode = finishNode;
            while(currentNode != null)
            {
                nodesInShortestPathOrder.Insert(0, currentNode);
                currentNode = currentNode.PreviousNode;
            }
            return nodesInShortestPathOrder;
        }
    }
}
