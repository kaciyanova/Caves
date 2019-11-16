using System;
using System.Windows;

namespace Caves
{
    public class Cave
    {
        public int Index { get; set; }
        public Point Location { get; set; }
        //hCost
        public double EstimatedDistanceToEnd { get; set; }
        //gCost
        public double ShortestPathFromStartCost { get; set; }
        //fCost
        public double TotalCost { get; set; }

        public int ParentIndex { get; set; }
//        
//        int Index;
//        Point Location;
//        double EstimatedDistanceToEnd;
//        double ShortestPathFromStartCost;
//
//
//        public Cave(int index, Point location)
//        {
//            EstimatedDistanceToEnd = 0;
//            Index = index;
//            Location = location;
//            ShortestPathFromStartCost = double.MaxValue;
//        }
//        
//        public Cave(int index, Point location, double distanceToEnd)
//        {
//            EstimatedDistanceToEnd = distanceToEnd;
//            Index = index;
//            Location = location;
//            ShortestPathFromStartCost = double.MaxValue;
//        }
//
//        public Cave(int index, Point location, double distanceToEnd, double shortestPathFromStart)
//        {
//            EstimatedDistanceToEnd = distanceToEnd;
//            Index = index;
//            Location = location;
//            ShortestPathFromStartCost = shortestPathFromStart;
//        }
    }
}