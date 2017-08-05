using ExtensionMethods;
using GeneralPurpose.Types;
using UnityEngine;
using UnityEngine.AI;

public class MeshTester : MonoBehaviour
{
    [SerializeField] private GameObject m_NavMeshPrefab = null;
    [SerializeField] private GameObject m_NodePrefab = null;
    [SerializeField] private GameObject m_DirectionPrefab = null;
    [SerializeField] private GameObject m_DestinationPrefab = null;

    [SerializeField] private Vector3 m_Destination = Vector3.up;

    private Transform m_NavMeshGo;
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
            for (var i = 0; i < m_GpNavMesh.Triangles.Length; i++)
            {
                var centroid = m_GpNavMesh.Triangles[i].Centroid();

                CreateNode($"Node:{i}", centroid.ToVector3());
            }
        }

        if (GUILayout.Button("Show Directions"))
        {
            // Create objects to show the center of each NavMesh triangle
            var vectorField = m_GpNavMesh.GetVectorField(m_Destination.ToGpVector3());

            CreateDestination(m_Destination);

            for (var i = 0; i < vectorField.Length; i++)
            {
                var postition = m_GpNavMesh.Triangles[i].Centroid().ToVector3();
                var direction = vectorField[i].ToVector3();
                var rotation = Quaternion.LookRotation(direction);

                CreateDirection($"Dir:{i}", postition, rotation);
            }
        }

        GUILayout.EndArea();
    }

    private void CreateDestination(Vector3 position)
    {
        var go = Instantiate(m_DestinationPrefab, m_NavMeshGo);

        go.name = "Destination";
        go.transform.position = position;
    }

    private void CreateDirection(string directionName, Vector3 position, Quaternion rotation)
    {
        var go = Instantiate(m_DirectionPrefab, m_NavMeshGo);

        go.name = directionName;
        go.transform.position = position;
        go.transform.rotation = rotation;
    }

    private void CreateNode(string nodeName, Vector3 position)
    {
        var go = Instantiate(m_NodePrefab, m_NavMeshGo);

        go.name = nodeName;
        go.transform.position = position;
    }

    private void CreateVisibleMesh(GpNavMesh gpNavMesh)
    {
        var navMeshName = "GpNavMesh";

        var mesh = new Mesh
        {
            name = navMeshName,
            vertices = gpNavMesh.Vertices.ToVector3Array(),
            triangles = gpNavMesh.TriangleIndices
        };

        var go = Instantiate(m_NavMeshPrefab);
        m_NavMeshGo = go.transform;
        go.transform.position = Vector3.zero;
        go.transform.localRotation = Quaternion.identity;

        var meshFilter = go.GetComponent<MeshFilter>();
        meshFilter.mesh = mesh;
    }

    private static GpNavMesh GenerateGpNavMesh()
    {
        var navMeshTriangulation = NavMesh.CalculateTriangulation();

        if (navMeshTriangulation.indices.Length == 0)
        {
            Debug.LogError("Scene does not contain any navigation data.");

            return null;
        }

        return new GpNavMesh(navMeshTriangulation.vertices.ToGpVector3Array(), navMeshTriangulation.indices);
    }
}
