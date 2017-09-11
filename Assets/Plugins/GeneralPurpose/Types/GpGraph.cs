using System.Collections.Generic;

namespace GeneralPurpose.Types
{
    public class GpGraph
    {
        public List<GpNode> Nodes { get; }

        public GpGraph(GpNavMesh gpNavMesh)
        {
            Nodes = new List<GpNode>();

            var tris = gpNavMesh.GetConnectedTriangles();

            // Create our initial list of nodes
            for (var i = 0; i < tris.Count; i++)
            {
                var node = new GpNode(i, gpNavMesh.Triangles[i].Centroid());

                Nodes.Add(node);
            }

            // Now go add all neighbors
            for (var i = 0; i < tris.Count; i++)
            {
                foreach (var nodeId in tris[i])
                {
                    var neighborNode = Nodes[nodeId];
                    Nodes[i].AddNeighbor(neighborNode);
                }
            }
        }
    }
}