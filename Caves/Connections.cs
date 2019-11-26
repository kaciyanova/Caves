using System;
using System.Collections.Generic;
using System.Linq;

namespace Caves
{
    using System.Windows;

    /// <summary>
    /// Array of connections/edges
    /// </summary>
    public class Connections
    {
        //array of paths: int=destination cave index, double= path length
        List<Tuple<int, double>>[] connections;


        //constructor 
        public Connections(int numberOfCaves)
        {
            connections = new List<Tuple<int, double>>[numberOfCaves];
            for (int i = 0; i < numberOfCaves; i++)
            {
                connections[i] = new List<Tuple<int, double>>();
            }
        }

        //adds new path to cave
        public void Add(int origin, int destination, double weight)
        {
            connections[origin].Add(new Tuple<int, double>(destination, weight));
        }


        /// <summary>
        /// Returns the paths of a cave.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public List<Tuple<int, double>> ReturnPaths(int index)
        {
            return connections[index];
        }

        //returns smallest path on map
        public double SmallestPath()
        {
            return connections.OrderBy(c => c.Min(x => x.Item2)).First().OrderBy(x => x.Item2).First().Item2;
        }
    }
}