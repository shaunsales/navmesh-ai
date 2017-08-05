using UnityEngine;
using UnityEngine.AI;
using GeneralPurpose.Navigation;
using GeneralPurpose.Navigation.NavMesh;

public class MeshTester : MonoBehaviour
{
    [SerializeField]
    private Material m_PolyMaterial;

    [SerializeField]
    private GameObject m_NodePrefab;

    [SerializeField]
    private Material m_NodeMaterial;

    private Transform m_NavMeshGo;
    private Mesh m_NavMesh;

    private GpNavMesh m_GpNavMesh;

    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 200, 300), GUIContent.none, "box");

        if (GUILayout.Button("Export to GpNavMesh"))
        {
            // Convert the scene NavMesh to a GpNavMesh
            m_GpNavMesh = GenerateGpNavMesh();

            // Visualize the GpNavMesh as a GameObject
            CreateVisibleMesh(m_GpNavMesh);
        }

        if (GUILayout.Button("Show Centroids"))
        {
            // Create objects to show the center of each NavMesh triangle
            var triangleIndex = 0;
            foreach (var centroid in m_GpNavMesh.Centroids)
            {
                CreateNode($"Node:{triangleIndex}",new Vector3(centroid.X, centroid.Y, centroid.Z));
                triangleIndex++;
            }
        }

        GUILayout.EndArea();
    }

    private void CreateNode(string nodeName, Vector3 position)
    {
        var go = Instantiate(m_NodePrefab, m_NavMeshGo);

        go.name = nodeName;

        go.transform.SetParent(m_NavMeshGo);
        go.transform.position = position;

        var meshRenderer = go.GetComponent<MeshRenderer>();
        meshRenderer.material = m_NodeMaterial;
    }

    private void CreateVisibleMesh(GpNavMesh gpNavMesh)
    {
        var navMeshName = "GpNavMesh";

        m_NavMesh = new Mesh
        {
            name = navMeshName,
            vertices = ToVector3(gpNavMesh.Vertices),
            triangles = gpNavMesh.Triangles
        };

        var go = new GameObject(navMeshName);
        var meshFilter = go.AddComponent<MeshFilter>();
        var meshRenderer = go.AddComponent<MeshRenderer>();

        m_NavMeshGo = go.transform;

        meshRenderer.material = m_PolyMaterial;
        meshFilter.mesh = m_NavMesh;
    }

    private static GpNavMesh GenerateGpNavMesh()
    {
        var navMeshTriangulation = NavMesh.CalculateTriangulation();

        if (navMeshTriangulation.indices.Length == 0)
        {
            Debug.LogError("Scene does not contain any navigation data.");

            return null;
        }

        return new GpNavMesh(ToGpVector3(navMeshTriangulation.vertices), navMeshTriangulation.indices);
    }

    private static Vector3[] ToVector3(GpVector3[] gpVector3)
    {
        var vector3Array = new Vector3[gpVector3.Length];

        for (var i = 0; i < vector3Array.Length; i++)
        {
            var x = gpVector3[i].X;
            var y = gpVector3[i].Y;
            var z = gpVector3[i].Z;

            vector3Array[i] = new Vector3(x, y, z);
        }

        return vector3Array;
    }

    private static GpVector3[] ToGpVector3(Vector3[] vector3)
    {
        var gpVector3Array = new GpVector3[vector3.Length];

        for (var i = 0; i < gpVector3Array.Length; i++)
        {
            var x = vector3[i].x;
            var y = vector3[i].y;
            var z = vector3[i].z;

            gpVector3Array[i] = new GpVector3(x, y, z);
        }

        return gpVector3Array;
    }
}
