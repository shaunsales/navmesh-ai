using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralPurpose.Types
{
    public class GpNode
    {
        public GpVector3 Position { get; }

        public GpNode(GpVector3 postition)
        {
            Position = postition;
        }
    }
}
