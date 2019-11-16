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
            var input = System.IO.File.ReadAllText(@"/Users/Kaci/Documents/Uni/AI/Coursework/1k/generated1000-2.cav");
//            input = "7,2,8,3,2,14,5,7,6,11,2,11,6,14,1,0,0,0,1,0,0,0,0,0,0,1,1,0,0,0,0,0,0,1,1,1,1,0,0,0,1,1,0,0,1,1,1,0,0,0,0,0,1,1,0,0,0,0,0,1,0,0,0,0";

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

            AStar.Pathfinder(caves, connections);
        }

//returns ordered list of all caves from coordinates list incl euclidian distance to destination cave & default max value shortest path
        static Cave[] GetCaves(int[] caveCoordinates, int numberOfCaves)
        {
            var caves = new Cave[numberOfCaves];

//iterating backwards so we have the last node to calculate distance from from the beginning
            for (int i = numberOfCaves - 1; i >= 0; i--)
            {
                var coordinates = caveCoordinates.Skip(i * 2).Take(2).ToArray();
                var newCaveLocation = new Point(coordinates[0], coordinates[1]);

                //destination cave
                if (i == numberOfCaves - 1)
                {
                    caves[i] = new Cave
                    {
                        EstimatedDistanceToEnd = 0, Index = i, Location = newCaveLocation,
                        ShortestPathFromStartCost = Double.MaxValue
                    };
                }
                else
                {
                    caves[i] = new Cave
                    {
                        Index = i, Location = newCaveLocation, EstimatedDistanceToEnd =
                            Point.Subtract(caves[numberOfCaves - 1].Location, newCaveLocation).Length,
                        ShortestPathFromStartCost = double.MaxValue
                    };
                }
            }
            return caves;
        }


        static Connections GetConnections(Cave[] caves, Connections connections, int numberOfCaves,
            int[] rawConnections)
        {
            for (int i = 0;
                i < numberOfCaves;
                i++)
            {
                for (int j = 0; j < numberOfCaves; j++)
                {
                    if (rawConnections[i * numberOfCaves + j] == 1)
                    {
                        connections.Add(j, i, Point.Subtract(caves[j].Location, caves[i].Location).Length);
                    }
                }
            }
            return connections;
        }
    }
}