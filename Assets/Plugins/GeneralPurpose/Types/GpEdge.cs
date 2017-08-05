using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralPurpose.Types
{
    public class GpEdge
    {
        public GpNode NodeA { get; }
        public GpNode NodeB { get; }

        public GpEdge(GpNode nodeA, GpNode nodeB)
        {
            NodeA = nodeA;
            NodeB = nodeB;
        }
    }
}
