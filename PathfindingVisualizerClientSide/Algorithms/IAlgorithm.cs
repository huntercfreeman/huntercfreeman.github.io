using PathfindingVisualizerClientSide.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PathfindingVisualizerClientSide.Algorithms
{
    interface IAlgorithm
    {
        List<Node> Run(List<List<Node>> grid, Node startNode, Node finishNode);
        List<Node> GetNodesInShortestPathOrder(Node finishNode);
    }
}
