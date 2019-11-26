using System;
using System.Linq;
using System.Windows;

namespace Caves
{
    class Program
    {
        /// <summary>
        /// Reads in cav data , parses the location data from the cav file.

        /// </summary>
        /// <param name="args">Command line arguments, %1 should be cav filename.</param>
        static void Main(string[] args)
        {
            var inputFile = args[0];
//            var input = System.IO.File.ReadAllText("D:\\Uni\\AI\\Caves\\TEST\\generated2000-1.cav");
            var input = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + inputFile + ".cav");
            var outputFile = inputFile + ".csn";
//            var outputFile = "test" + ".csn";

            var caveData = Array.ConvertAll(input.Split(','), int.Parse);

            var numberOfCaves = caveData[0];

            var coordCount = numberOfCaves * 2;

            var caveCoordinates = caveData.Skip(1).Take(coordCount).ToArray();

            var caveConnections = caveData.Skip(coordCount + 1).Take(caveData.Length - (coordCount + 1)).ToArray();

            var caves = GetCaves(caveCoordinates, numberOfCaves);

            var connections = new Connections(numberOfCaves);

            connections = GetConnections(caves, connections, numberOfCaves, caveConnections);

            var shortestPathString = FindPath.Pathfinder(caves, connections);

            System.IO.File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + outputFile, shortestPathString);
        }

        //returns ordered list of all caves from coordinates list incl euclidian distance to destination cave & default max value shortest path
        /// <summary>
        /// Gets the caves.
        /// </summary>
        /// <param name="caveCoordinates">The cave coordinates.</param>
        /// <param name="numberOfCaves">The number of caves.</param>
        /// <returns></returns>
        static Cave[] GetCaves(int[] caveCoordinates, int numberOfCaves)
        {
            var caves = new Cave[numberOfCaves];

            //iterating backwards so we have the last node to calculate distance from from the beginning
            for (int i = numberOfCaves - 1; i >= 0; i--)
            {
                var coordinates = caveCoordinates.Skip(i * 2).Take(2).ToArray();
                var newCaveLocation = new Point(coordinates[0], coordinates[1]);

                //if it's the destination cave
                if (i == numberOfCaves - 1)
                {
                    caves[i] = new Cave
                    {
                        EstimatedDistanceToEnd = 0,
                        Index = i,
                        Location = newCaveLocation,
                        ShortestPathFromStartCost = Double.MaxValue
                    };
                }
                else
                {
                    caves[i] = new Cave
                    {
                        //euclidean distance heuristic 
                        //                        Index = i,
                        //                        Location = newCaveLocation,
                        //                        EstimatedDistanceToEnd =
                        //                            Point.Subtract(caves[numberOfCaves - 1].Location, newCaveLocation).Length,
                        //                        ShortestPathFromStartCost = double.MaxValue

                        //manhattan distance heuristic
                        //                        Index = i,
                        //                        Location = newCaveLocation,
                        //                        EstimatedDistanceToEnd =
                        //                            Math.Abs(caves[numberOfCaves - 1].Location.X- newCaveLocation.X ) +Math.Abs(caves[numberOfCaves - 1].Location.Y - newCaveLocation.Y),
                        //                        ShortestPathFromStartCost = double.MaxValue

                        //dijkstra (no heuristic) definitely shortest path
                        Index = i,
                        Location = newCaveLocation,
                        EstimatedDistanceToEnd =
                            0,
                        ShortestPathFromStartCost = double.MaxValue
                    };
                }
            }

            return caves;
        }

        //parses connections from matrix to connections list array
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