using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using static Caves.ExtensionMethods;

namespace Caves
{
    class Program
    {
        static void Main(string[] args)
        {
            var watch = new System.Diagnostics.Stopwatch();
            var input = System.IO.File.ReadAllText(@"/Users/Kaci/Documents/Uni/AI/Coursework/1k/generated1000-1.cav");
            //input = "7,2,8,3,2,14,5,7,6,11,2,11,6,14,1,0,0,0,1,0,0,0,0,0,0,1,1,0,0,0,0,0,0,1,1,1,1,0,0,0,1,1,0,0,1,1,1,0,0,0,0,0,1,1,0,0,0,0,0,1,0,0,0,0";

            watch.Start();


            ParseLocations(input);

            watch.Stop();
            Console.WriteLine($"Time taken= {watch.ElapsedMilliseconds}");
        }

        static void ParseLocations(string input)
        {
            var caveData = Array.ConvertAll(input.Split(','), int.Parse);

            var numberOfCaves = caveData[0];

            var coordCount = numberOfCaves * 2;

            var caveCoordinates = caveData.Skip(1).Take(coordCount).ToArray();

            var caveConnections = caveData.Skip(coordCount + 1).Take(caveData.Length - (coordCount + 1)).ToArray();

            var connections = new Connections(numberOfCaves);

            var caves = GetCaves(caveCoordinates, numberOfCaves);

            connections = GetConnections(caves, connections, numberOfCaves, caveConnections);

            AStar(caves, connections);
        }

        static string AStar(Tuple<Point, double>[] caves, Connections connections)
        {
            var pathTakenString = "1 ";

            double pathLength = 0;
            double gScore = 0;
            //start at first node
            var currentExploringCaveIndex = 0;

            bool noPathPossible=false;

            var open=new Queue<int>();
            var closed=new List<int>();
            
            open.Enqueue(currentExploringCaveIndex);
            
            var cameFrom
            while (open.Count>0)
            {
                var pathIndex = 0;

                var currentIndex = open.First();

                if (currentIndex==caves.Length)
                {
                    return pathTakenString+", "+currentIndex;
                }

                open.Dequeue();

                var pathsFromCurrentCave = connections.GetPaths(currentExploringCaveIndex);

                for (int i = 0; i < pathsFromCurrentCave.Count; i++)
                {
                    var node = pathsFromCurrentCave[i];
                    
                    var tentativeCost = pathLength + node.Item2;

                    if (tentativeCost<gScore)
                    {
                        
                    }
                }
                currentExploringCaveIndex = priorqueue[0];
                closed.Add(currentExploringCaveIndex);
                priorqueue.RemoveAt(0);

                

                for (int i = 0; i < pathsFromCurrentCave.Count; i++)
                {
                    var newcost=cost+
                    if (!closed.Contains(currentExploringCaveIndex))
                    {
                        
                    }
                    if (open.Contains(pathsFromCurrentCave[i].Item1)&&)
                    {
                        
                    }
                }
            }
            
            
            do
            {
                open.Enqueue(currentExploringCaveIndex);
                
                var pathsFromCurrentCave = connections.Count(currentExploringCaveIndex);
                
                for (int i = 0; i < pathsFromCurrentCave; i++)
                {
                    var neighbourToExplore = connections.GetPaths(currentExploringCaveIndex)[i];
                    if (closed.Contains(neighbourToExplore.Item1))
                    {
                        continue;
                    }

                    if (!open.Contains(caves[neighbourToExplore.Item1]))
                    {
                        
                    }
                   var closestCave = connections.ReturnPath(currentExploringCaveIndex, i);
                  
                   //checks if lowest cost cave is a dead end
                    if (connections.Count(closestCave.Item1) > 0)
                    {
                        break;
                    }

                    if (i==pathsFromCurrentCave-1&&connections.Count(closestCave.Item1) ==0)
                    {
                        noPathPossible = true;
                        break;
                    }
                }
                
            }
            while (currentExploringCaveIndex!=caves.Length)

            
            //set cave to explore to lowest cost one
            while (currentExploringCaveIndex != caves.Length - 1)
            {
                var nextCave = connections.ReturnPath(currentExploringCaveIndex, 0);
//                Tuple<int, double> nextCave;

                var pathsFromCurrentCave = connections.Count(currentExploringCaveIndex);
                
                for (int i = 0; i < pathsFromCurrentCave; i++)
                {
                    nextCave = connections.ReturnPath(currentExploringCaveIndex, i);
                    //checks if lowest cost cave is a dead end
                    if (connections.Count(nextCave.Item1) > 0)
                    {
                        break;
                    }

                    if (i==pathsFromCurrentCave-1&&connections.Count(nextCave.Item1) ==0)
                    {
                        noPathPossible = true;
                        break;
                    }
                }

                if (noPathPossible)
                {
                    shortestPath = "0";
                    break;
                }
                
                pathLength += nextCave.Item2;

//                Console.WriteLine($"Exploring cave: {nextCave.Item1 + 1}");
                Console.WriteLine($"Exploring cave: {nextCave.Item1 }");
                Console.WriteLine($"Total path length: {pathLength}");

                shortestPath = shortestPath + (nextCave.Item1 + 1) + " ";
                currentExploringCaveIndex = nextCave.Item1;
            }

            Console.WriteLine($"Path: {shortestPath}");
        }


        //tuple has location and distance to final node
        static Tuple<Point, double>[] GetCaves(int[] caveCoordinates, int numberOfCaves)
        {
            var caves = new Tuple<Point, double>[numberOfCaves];

//iterating backwards so we have the last node to calculate distance from from the beginning
            for (int i = numberOfCaves - 1; i >= 0; i--)
            {
                var coordinates = caveCoordinates.Skip(i * 2).Take(2).ToArray();
                var newCaveLocation = new Point(coordinates[0], coordinates[1]);

                if (i == numberOfCaves - 1)
                {
                    caves[i] = new Tuple<Point, double>(newCaveLocation, 0);
                }

                caves[i] = new Tuple<Point, double>(newCaveLocation,
                    Point.Subtract(caves[numberOfCaves - 1].Item1, newCaveLocation).Length);
            }

            return caves;
        }

        static Connections GetConnections(Tuple<Point, double>[] caves, Connections connections, int numberOfCaves,
            int[] rawConnections)
        {
            for (int i = 0; i < numberOfCaves; i++)
            {
                for (int j = 0; j < numberOfCaves; j++)
                {
                    if (rawConnections[i * numberOfCaves + j] == 1)
                    {
                        connections.Add(j, i, Point.Subtract(caves[j].Item1, caves[i].Item1).Length);
                    }
                }
            }

            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();

            connections.Sort(numberOfCaves, caves);
            watch.Stop();
            Console.WriteLine($"Time taken to sort= {watch.ElapsedMilliseconds}");
            return connections;
        }
    }
}