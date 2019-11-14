using System;
using System.Windows;

namespace Caves
{
    class Cave
    {
        public int Index { get; set; }
        public Point Location { get; set; }
        public double DistanceToEnd { get; set; }
        public double ShortestPathFromStart { get; set; }
//        
//        int Index;
//        Point Location;
//        double DistanceToEnd;
//        double ShortestPathFromStart;
//
//
//        public Cave(int index, Point location)
//        {
//            DistanceToEnd = 0;
//            Index = index;
//            Location = location;
//            ShortestPathFromStart = double.MaxValue;
//        }
//        
//        public Cave(int index, Point location, double distanceToEnd)
//        {
//            DistanceToEnd = distanceToEnd;
//            Index = index;
//            Location = location;
//            ShortestPathFromStart = double.MaxValue;
//        }
//
//        public Cave(int index, Point location, double distanceToEnd, double shortestPathFromStart)
//        {
//            DistanceToEnd = distanceToEnd;
//            Index = index;
//            Location = location;
//            ShortestPathFromStart = shortestPathFromStart;
//        }
    }
}