using ExtensionMethods;
using GeneralPurpose.Navigation.Unity;
using GeneralPurpose.Utils;
using UnityEngine;

public class VectorTest : MonoBehaviour
{
    private GameObject m_OriginNode;
    private GameObject m_DestNode;
    private LineRenderer m_Edge;
    private LineRenderer m_OriginToDestLine;
    private GameObject m_ProjNode;

    [Header("Origin and Destination")]
    public Vector3 m_OriginPos;
    public Vector3 m_DestPos;

    [Header("Edge")]
    public Vector3 m_EdgeAPos;
    public Vector3 m_EdgeBPos;

    [Header("Projected Output")]
    public Vector3 m_ProjPos;

    private void Start()
    {
        // Setup Axis
        Utils.CreateSphere("AxisCenter", Vector3.zero, 0.5f, Color.grey);
        Utils.CreateSphere("AxisUp", Vector3.up * 10, 0.5f, Color.grey);
        Utils.CreateSphere("AxisDown", Vector3.down * 10, 0.5f, Color.grey);
        Utils.CreateSphere("AxisRight", Vector3.right * 10, 0.5f, Color.grey);
        Utils.CreateSphere("AxisLeft", Vector3.left * 10, 0.5f, Color.grey);

        Utils.CreateLine("AxisX", Vector3.left * 10, Vector3.right * 10, Color.black);
        Utils.CreateLine("AxisY", Vector3.down * 10, Vector3.up * 10, Color.black);

        // Setup Origin and Dest
        m_OriginNode = Utils.CreateSphere("Origin", m_OriginPos, 1, Color.blue);
        m_DestNode = Utils.CreateSphere("Dest", m_DestPos, 1, Color.green);
        m_OriginToDestLine = Utils.CreateLine("Edge", m_EdgeAPos, m_EdgeBPos, Color.yellow);

        // Setup Edge
        m_Edge = Utils.CreateLine("Edge", m_EdgeAPos, m_EdgeBPos, Color.red);

        // Projected Nodes
        m_ProjNode = Utils.CreateSphere("Proj", m_ProjPos, 1, Color.magenta);
    }

    private void Update()
    {
        // Update GOs
        m_OriginNode.transform.position = m_OriginPos;
        m_DestNode.transform.position = m_DestPos;
        m_OriginToDestLine.SetPosition(0, m_OriginPos);
        m_OriginToDestLine.SetPosition(1, m_DestPos);
        m_Edge.SetPosition(0, m_EdgeAPos);
        m_Edge.SetPosition(1, m_EdgeBPos);
        m_ProjNode.transform.position = m_ProjPos;

        // Find the nearest point on the line
        m_ProjPos = VectorMath.GetNearestPointOnSegment(m_OriginPos.ToGpVector3(), m_DestPos.ToGpVector3(), m_EdgeAPos.ToGpVector3(), m_EdgeBPos.ToGpVector3()).ToVector3();
    }

    //private void OnGUI()
    //{
    //    GUILayout.BeginArea(new Rect(10, 10, 300, 300), GUIContent.none, "box");

    //    if (GUILayout.Button("Line"))
    //    {
    //        Utils.CreateLine("Line", Vector3.zero, (Vector3.up + Vector3.right) * 10, Color.red);
    //    }

    //    GUILayout.EndArea();
    //}
}
