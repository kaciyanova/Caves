namespace Caves
{
    public class AStar
    {
        public static string Pathfinder(Cave[] caves, Connections connections)
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


    }
}