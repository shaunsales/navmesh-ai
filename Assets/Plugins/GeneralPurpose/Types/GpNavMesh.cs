namespace GeneralPurpose.Types
{
    public class GpNavMesh
    {
        public int[] TriangleIndices { get; }
        public GpVector3[] Vertices { get; }
        public GpTriangle[] Triangles { get; }

        public GpNavMesh(GpVector3[] vertices, int[] triangles)
        {
            Vertices = vertices;
            TriangleIndices = triangles;
            Triangles = new GpTriangle[triangles.Length / 3];

            // Calculate triangles for all vertices
            var index = 0;
            for (var i = 0; i < TriangleIndices.Length; i += 3)
            {
                var v1 = Vertices[TriangleIndices[i]];
                var v2 = Vertices[TriangleIndices[i + 1]];
                var v3 = Vertices[TriangleIndices[i + 2]];

                Triangles[index] = new GpTriangle(v1, v2, v3);

                index++;
            }
        }

        public GpVector3[] GetVectorField(GpVector3 destination)
        {
            var vectorField = new GpVector3[Triangles.Length];

            for (int i = 0; i < Triangles.Length; i++)
            {
                var origin = Triangles[i].Centroid();
                vectorField[i] = destination - origin;
            }

            return vectorField;
        }
    }
}
