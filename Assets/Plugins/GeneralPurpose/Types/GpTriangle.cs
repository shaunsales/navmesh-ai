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

        /// <summary>
        /// Test if another triangle shares any edges with this triangle by checking if the share at least two vertices.
        /// </summary>
        public bool IsConnected(GpTriangle otherTriangle)
        {
            var connectCount = 0;

            for (var i = 0; i < 3; i++)
            {
                if (this[i] == otherTriangle.V1 || this[i] == otherTriangle.V2 || this[i] == otherTriangle.V3)
                {
                    connectCount++;
                }
            }

            return connectCount > 1;
        }

        // Note, this ignores the triangles Y component and may yield odd results 
        public bool IsPointInsideXZ(GpVector3 p)
        {
            var a = 1f / 2f * (-V2.Z * V3.X + V1.Z * (-V2.X + V3.X) + V1.X * (V2.Z - V3.Z) + V2.X * V3.Z);
            var sign = a < 0f ? -1f : 1f;
            var s = (V1.Z * V3.X - V1.X * V3.Z + (V3.Z - V1.Z) * p.X + (V1.X - V3.X) * p.Z) * sign;
            var t = (V1.X * V2.Z - V1.Z * V2.X + (V1.Z - V2.Z) * p.X + (V2.X - V1.X) * p.Z) * sign;

            return s > 0 && t > 0 && (s + t) < 2 * a * sign;
        }
    }
}
