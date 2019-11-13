using System;
using System.Collections.Generic;
using System.Linq;

namespace Caves
{
    using System.Windows;

    class Connections
    {
        //array of paths: int, double =destination cave index, path length
        List<Cave>[] connections;


        //constructor 
        public Connections(int numberOfCaves)
        {
            connections = new List<Cave>[numberOfCaves];
            for (int i = 0; i < numberOfCaves; i++)
            {
                connections[i] = new List<Cave>();
            }
        }

        //binary heap priority queue- adds new path by weight
        public void Add(int origin, int destination, double weight)
        {
            //right so since i think the connections will stay quite small i think i'll just sort the list at the end

            connections[origin].Add(new Cave{destination, weight})};

//            var childIndex = connections[origin].Count - 1;
//
//            if (childIndex==0)
//            {
//                return;
//            }
//            
//            var parentIndex = childIndex / 2;
//
//            var child = connections[origin][childIndex];
//            var parent = connections[origin][parentIndex];
//
//          
//            while (child.Item2 >= parent.Item2&&childIndex!=0)
//            {
//                //switches new node with old node if the weight is lower than old
//                var tempPathHolder = connections[origin][childIndex];
//                connections[origin][childIndex] = connections[origin][parentIndex];
//                connections[origin][parentIndex] = tempPathHolder;
//                childIndex = parentIndex;
//                childIndex /= 2;
//            }

            //binary heap one that doesn't quite work
//            while (childIndex > 0)
//            {
//                var parentIndex = (childIndex - 1) / 2;
//                if (newPath.Item2 >= connections[origin][parentIndex].Item2)
//                {
//                    break;
//                }
//
//                //switches new node with old node if the weight is lower than old
//                var tempPathHolder = connections[origin][childIndex];
//                connections[origin][childIndex] = connections[origin][parentIndex];
//                connections[origin][parentIndex] = tempPathHolder;
//                childIndex = parentIndex;
//            }
        }

        public void Sort(int numberOfCaves,Cave[] caves)
        {
            for (int i = 0; i < numberOfCaves; i++)
            {
                connections[i] = connections[i].OrderBy(path => path.Item2+caves[path.Item1].Item2).ToList();
                
//                checks
//                Console.WriteLine($"Node: {i}");
//
//                foreach (var edge in connections[i])
//                {
//                    var combinedweight = edge.Item2 + caves[edge.Item1].Item2;
//                    Console.WriteLine($"Weight (Edge): {combinedweight} ({edge.Item1}) ({caves[edge.Item1].Item2})");
//                }
            }
        }

        public int Count(int index)
        {
            return connections[index].Count;
        }

        public List<Tuple<int, double>> GetPaths(int index)
        {
            return connections[index];
        }

        public Tuple<int, double> ReturnPath(int caveIndex,int pathIndex)
        {
            return connections[caveIndex][pathIndex];
        }
        
        
    }

    //go first cave in adjacency list,  sort the edges by weight, when all edges visited mark as Visited go to next

   
}