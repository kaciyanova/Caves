using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Caves
{
    public class AStar
    {
        public static string Pathfinder(Cave[] caves, Connections connections)
        {
            var pathTakenString = "1 ";

            double pathLength = 0;
            //start at first node
            var currentExploringCaveIndex = 0;
            bool noPathPossible = false;

            var open = new PriorityQueue<Cave>();
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
                currentExploringCaveIndex = currentExploringCave.Index;

                Console.WriteLine($"Exploring cave: {currentExploringCaveIndex + 1}");
//                Console.WriteLine($"Total path length: {pathLength}");

                if (currentExploringCaveIndex == caves.Length - 1)
                {
                    var currentReconstructingCaveIndex = caves.Length - 1;
                    var reversePath = new List<int>();

                    while (currentReconstructingCaveIndex != 0)
                    {
                        reversePath.Add(currentReconstructingCaveIndex);
                        currentReconstructingCaveIndex = caves[currentReconstructingCaveIndex].ParentIndex;
                    }

                    reversePath.Reverse();

                    pathTakenString= String.Concat(pathTakenString,reversePath.Select(x=>(x+1).ToString()).Aggregate((a,b)=>a+' '+b));

                    Console.WriteLine(pathTakenString);

                    
                    Console.WriteLine($"Done!");

                    return pathTakenString;
//                    return pathTakenString + ", " + currentExploringCaveIndex;
                }

                closed.Add(currentExploringCaveIndex);

                var pathsFromCurrentCave = connections.ReturnPaths(currentExploringCaveIndex);

                //expanding paths from cave
                for (int i = 0; i < pathsFromCurrentCave.Count; i++)
                {
                    var node = pathsFromCurrentCave[i];
                    Console.WriteLine($"Evaluating path to: {node.Item1 + 1}");

                    if (closed.Contains(node.Item1))
                    {
                        Console.WriteLine($"Cave {node.Item1 + 1} already explored ");

                        continue;
                    }

                    //calculates path costs for node 
                    var pathCost = currentExploringCave.ShortestPathFromStartCost + node.Item2;
                    var totalCost = pathCost + caves[node.Item1].EstimatedDistanceToEnd;

                    Console.WriteLine($"Path Cost: {pathCost}");

                    if (open.Contains(caves[node.Item1]))
                    {
                        if (pathCost < caves[node.Item1].ShortestPathFromStartCost)
                        {
                            Console.WriteLine(
                                $"Shorter Path found, previous parent & path cost: {caves[node.Item1].ParentIndex + 1},{caves[node.Item1].ShortestPathFromStartCost}");

                            //better path than before, changes parent to current cave
                            caves[node.Item1].ParentIndex = currentExploringCaveIndex;
                            Console.WriteLine($"New parent : {caves[node.Item1].ParentIndex + 1}");

                            //new path costs
                            caves[node.Item1].ShortestPathFromStartCost = pathCost;
                            caves[node.Item1].TotalCost = pathCost + caves[node.Item1].EstimatedDistanceToEnd;
                        }
                    }
                    else if (closed.Contains(node.Item1))
                    {
                        if (caves[node.Item1].TotalCost < totalCost)
                        {
                            continue;
                        }
                        else
                        {
                            open.Enqueue(caves[node.Item1]);
                        }
                    }
                    else
                    {
                        caves[node.Item1].ParentIndex = currentExploringCaveIndex;
                        caves[node.Item1].ShortestPathFromStartCost = pathCost;
                        caves[node.Item1].TotalCost = totalCost;
                        open.Enqueue(caves[node.Item1]);
                    }
                }

//
//                for (int i = 0; i < pathsFromCurrentCave.Count; i++)
//                {
//                    var newcost = cost +
//                    if (!closed.Contains(currentExploringCaveIndex))
//                    {
//                    }
//
//                    if (open.Contains(pathsFromCurrentCave[i].Item1) &&)
//                    {
//                    }
//                }
//            }
//
//
//            do
//            {
//                open.Enqueue(currentExploringCaveIndex);
//
//                var pathsFromCurrentCave = connections.Count(currentExploringCaveIndex);
//
//                for (int i = 0; i < pathsFromCurrentCave; i++)
//                {
//                    var neighbourToExplore = connections.ReturnPaths(currentExploringCaveIndex)[i];
//                    if (closed.Contains(neighbourToExplore.Item1))
//                    {
//                        continue;
//                    }
//
//                    if (!open.Contains(caves[neighbourToExplore.Item1]))
//                    {
//                    }
//
//                    var closestCave = connections.ReturnPath(currentExploringCaveIndex, i);
//
//                    //checks if lowest cost cave is a dead end
//                    if (connections.Count(closestCave.Item1) > 0)
//                    {
//                        break;
//                    }
//
//                    if (i == pathsFromCurrentCave - 1 && connections.Count(closestCave.Item1) == 0)
//                    {
//                        noPathPossible = true;
//                        break;
//                    }
//                }
//            } while (currentExploringCaveIndex != caves.Length)
//
//
//            //set cave to explore to lowest cost one
//            while (currentExploringCaveIndex != caves.Length - 1)
//            {
//                var nextCave = connections.ReturnPath(currentExploringCaveIndex, 0);
////                Tuple<int, double> nextCave;
//
//                var pathsFromCurrentCave = connections.Count(currentExploringCaveIndex);
//
//                for (int i = 0; i < pathsFromCurrentCave; i++)
//                {
//                    nextCave = connections.ReturnPath(currentExploringCaveIndex, i);
//                    //checks if lowest cost cave is a dead end
//                    if (connections.Count(nextCave.Item1) > 0)
//                    {
//                        break;
//                    }
//
//                    if (i == pathsFromCurrentCave - 1 && connections.Count(nextCave.Item1) == 0)
//                    {
//                        noPathPossible = true;
//                        break;
//                    }
//                }
//
//                if (noPathPossible)
//                {
//                    shortestPath = "0";
//                    break;
//                }
//
//                pathLength += nextCave.Item2;

//                Console.WriteLine($"Exploring cave: {nextCave.Item1 + 1}");

//                shortestPath = shortestPath + (nextCave.Item1 + 1) + " ";
            }

            Console.WriteLine($"No path found!");
            return "0";
        }
    }
}