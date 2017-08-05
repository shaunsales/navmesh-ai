using System.Collections.Generic;

namespace GeneralPurpose.Types
{
    public class GpGraph
    {
        private List<GpNode> m_Nodes = new List<GpNode>();
        private List<GpEdge> m_Edges = new List<GpEdge>();

        public GpGraph(GpNavMesh gpNavMesh)
        {
            var maxNodes = gpNavMesh.Triangles.Length;
            var connectedTris = gpNavMesh.GetConnectedTriangles();

            // Convert all triangles to nodes
            for (var i = 0; i < maxNodes; i++)
            {
                AddNode(gpNavMesh.Triangles[i].Centroid());
            }

            // Go through all nodes and 
            for (var i = 0; i < maxNodes; i++)
            {
                var nodeA = m_Nodes[i];

                List<int> tris;
                if (connectedTris.TryGetValue(i, out tris))
                {
                    for (var j = 0; j < tris.Count; j++)
                    {
                        var nodeB = m_Nodes[tris[j]];
                        
                        // Ensure we don't add an indentical edge
                        if (!ContainsEdge(nodeA, nodeB))
                        {
                            AddEdge(nodeA, nodeB);
                        }
                    }
                }
            }
        }

        public GpNode[] GetNodes()
        {
            return m_Nodes.ToArray();
        }

        public GpEdge[] GetEdges()
        {
            return m_Edges.ToArray();
        }

        private void AddNode(GpVector3 position)
        {
            var node = new GpNode(position);
            m_Nodes.Add(node);
        }

        private void AddEdge(GpNode nodeA, GpNode nodeB)
        {
            var edge = new GpEdge(nodeA, nodeB);
            m_Edges.Add(edge);
        }

        private bool ContainsEdge(GpNode nodeA, GpNode nodeB)
        {
            for (var i = 0; i < m_Edges.Count; i++)
            {
                var edgeNodeA = m_Edges[i].NodeA;
                var edgeNodeB = m_Edges[i].NodeB;

                // Check the edge in both directions
                var isNodeAABB = edgeNodeA == nodeA && edgeNodeB == nodeB;
                var isNodeABBA = edgeNodeA == nodeB && edgeNodeB == nodeA;

                if (isNodeAABB || isNodeABBA)
                {
                    return true;
                }
            }

            return false;
        }
    }
}