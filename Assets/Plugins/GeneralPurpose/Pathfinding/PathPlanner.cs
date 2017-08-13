using GeneralPurpose.Types;
using GeneralPurpose.Utils;

namespace GeneralPurpose.Pathfinding
{
    public class PathPlanner
    {
        private GpGraph m_GpGraph;

        public PathPlanner(GpGraph gpGraph)
        {
            m_GpGraph = gpGraph;
        }

        private void DepthFirst(GpVector3 origin, GpVector3 destination)
        {
            var gpNodeStart = GetClosestNode(origin);
            var gpNodeEnd = GetClosestNode(destination);

            if (gpNodeStart == null)
            {
                return;
            }

            if (gpNodeEnd == null)
            {
                return;
            }

            // Need to test if the origin and destination fall within the triangle (if not we can plan the closest path, or something)

            // Get the start node
            // Push it to the stack
            // Mark it as seen
            // For every edge
            //   Is it the target?
            //   Is it seen?
            //   Push to stack if neither
            //   Go to Next Node
            // Else pop this node
        }

        private GpNode GetClosestNode(GpVector3 position)
        {
            var closestDist = float.MaxValue;
            GpNode closestNode = null;

            foreach (var gpNode in m_GpGraph.GetNodes())
            {
                var sqrDist = GpVector3.SqrDistance(position, gpNode.Position);

                if (sqrDist < closestDist)
                {
                    closestDist = sqrDist;
                    closestNode = gpNode;
                }
            }

            return closestNode;
        }
    }
}
