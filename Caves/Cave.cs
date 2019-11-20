using System;
using System.Windows;

namespace Caves
{
    /// <summary>
    /// Cave (node) class
    /// </summary>
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
    }
}