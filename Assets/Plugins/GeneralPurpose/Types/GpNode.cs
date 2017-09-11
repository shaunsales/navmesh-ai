using System.Collections.Generic;

namespace GeneralPurpose.Types
{
    public class GpNode
    {
        public int Id { get; }
        public GpVector3 Position { get; }
        public List<GpNode> Neighbors { get; }

        public bool IsVisited { get; private set; }
        public GpNode ParentNode { get; private set; }

        public GpNode(int id, GpVector3 postition)
        {
            Id = id;
            Position = postition;

            Neighbors = new List<GpNode>();
        }

        public void AddNeighbor(GpNode node)
        {
            Neighbors.Add(node);
        }

        public void SetParentNode(GpNode node)
        {
            ParentNode = node;
        }

        public void SetVisited(bool isVisited)
        {
            IsVisited = isVisited;
        }
    }
}
