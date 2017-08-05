using System.Collections;
using System.Collections.Generic;
using GeneralPurpose.Navigation.NavMesh;

namespace GeneralPurpose.Navigation
{
    public class GpNavMesh
    {
        public int[] Triangles { get; }
        public GpVector3[] Vertices { get; }
        public GpVector3[] Centroids { get; }

        public GpNavMesh(GpVector3[] vertices, int[] triangles)
        {
            Vertices = vertices;
            Triangles = triangles;
            Centroids = new GpVector3[Triangles.Length / 3];

            // Calculate centroids for all triangles
            var index = 0;
            for (var i = 0; i < Triangles.Length; i += 3)
            {
                var v1 = Vertices[Triangles[i]];
                var v2 = Vertices[Triangles[i + 1]];
                var v3 = Vertices[Triangles[i + 2]];

                var centroid = (v1 + v2 + v3) / 3.0f;
                Centroids[index] = centroid;
                index++;
            }
        }
    }
}
