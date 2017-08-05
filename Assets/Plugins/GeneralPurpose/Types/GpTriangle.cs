using System;

namespace GeneralPurpose.Types
{
    public struct GpTriangle
    {
        public GpVector3 V1;
        public GpVector3 V2;
        public GpVector3 V3;

        public GpVector3 this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return V1;
                    case 1: return V2;
                    case 2: return V3;
                    default: throw new IndexOutOfRangeException("Invalid index for GpTriangle. Should be less than 3.");
                }
            }
        }

        public GpTriangle(GpVector3 v1, GpVector3 v2, GpVector3 v3)
        {
            V1 = v1;
            V2 = v2;
            V3 = v3;
        }

        public GpVector3 Centroid()
        {
            return (V1 + V2 + V3) / 3.0f;
        }
    }
}
