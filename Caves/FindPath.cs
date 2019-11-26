using System;
using System.Collections.Generic;
using System.Linq;
using static System.String;

namespace Caves
{
    /// <summary>
    /// Fsinds path from start cave to end cave
    /// </summary>
    public class FindPath
    {
        public static string Pathfinder(Cave[] caves, Connections connections)
        {
            var pathTakenString = "1 ";

            //caves to explore
            var open = new PriorityQueue<Cave>();
            //caves already explored
            var closed = new List<int>();

            caves[0].ShortestPathFromStartCost = 0;
            caves[0].TotalCost = 0;

            //initialise with start
            open.Enqueue(caves[0]);

            while (open.Count > 0)
            {
                var pathIndex = 0;

                //pops lowest cost cave in open list
                var currentExploringCave = open.Dequeue();
                var currentExploringCaveIndex = currentExploringCave.Index;

                //if we're at destination cave
                if (currentExploringCaveIndex == caves.Length - 1)
                {
                    var currentReconstructingCaveIndex = currentExploringCaveIndex;
                    var reversePath = new List<int>();

                    //uncomment to print total pathlength to console
                    //Console.WriteLine("Pathlength: "+caves[currentReconstructingCaveIndex].ShortestPathFromStartCost);

                    //starting at destination gets parent of node and goes all the way back up to first cave
                    while (currentReconstructingCaveIndex != 0)
                    {
                        reversePath.Add(currentReconstructingCaveIndex);
                        currentReconstructingCaveIndex = caves[currentReconstructingCaveIndex].ParentIndex;
                    }

                    reversePath.Reverse();

                    pathTakenString = Concat(pathTakenString, reversePath.Select(x => (x + 1).ToString()).Aggregate((a, b) => a + ' ' + b));

                    return pathTakenString;
                }

                closed.Add(currentExploringCaveIndex);

                var pathsFromCurrentCave = connections.ReturnPaths(currentExploringCaveIndex);

                //expanding paths from cave
                for (int i = 0; i < pathsFromCurrentCave.Count; i++)
                {
                    //lowest cost path from cave
                    var node = pathsFromCurrentCave[i];

                    //if the node was already explored
                    if (closed.Contains(node.Item1))
                    {
                        continue;
                    }

                    //calculates path costs for node 
                    var pathCost = currentExploringCave.ShortestPathFromStartCost + node.Item2;
                    var totalCost = pathCost + caves[node.Item1].EstimatedDistanceToEnd;


                    if (open.Contains(caves[node.Item1]))
                    {
                        if (pathCost < caves[node.Item1].ShortestPathFromStartCost)
                        {
                           //found better path than existed before, changes parent to current cave
                            caves[node.Item1].ParentIndex = currentExploringCaveIndex;

                            //new path costs
                            caves[node.Item1].ShortestPathFromStartCost = pathCost;
                            caves[node.Item1].TotalCost = pathCost + caves[node.Item1].EstimatedDistanceToEnd;
                        }
                    }
                    else if (closed.Contains(node.Item1))
                    {
                        //if the node was on the closed list but we've found a cheaper way to get to it we put it back on the open list
                        if (caves[node.Item1].TotalCost > totalCost)
                        {
                            open.Enqueue(caves[node.Item1]);
                        }
                    }
                    else
                    {
                        //if we haven't visited the node yet populate scores and push to open list
                        caves[node.Item1].ParentIndex = currentExploringCaveIndex;
                        caves[node.Item1].ShortestPathFromStartCost = pathCost;
                        caves[node.Item1].TotalCost = totalCost;
                        open.Enqueue(caves[node.Item1]);
                    }
                }
            }
            //no path found
            return "0";
        }
    }
}