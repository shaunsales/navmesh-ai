using GeneralPurpose.Navigation.Unity;
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

        m_ProjPos = GetNearestPointOnEdge(m_OriginPos, m_DestPos, m_EdgeAPos, m_EdgeBPos);
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

    private Vector3 GetNearestPointOnEdge(Vector3 line1Start, Vector3 line1End, Vector3 line2Start, Vector3 line2End)
    {
        Vector3 nearestPoint;
        var line1Dir = line1End - line1Start;
        var line2Dir = line2End - line2Start;

        var isIntersecting = LineLineIntersection(line1Start, line1Dir, line2Start, line2Dir, out nearestPoint);

        float x;
        float y;
        var isXClamped = TryClamp(nearestPoint.x, line2Start.x, line2End.x, out x);
        var isYClamped = TryClamp(nearestPoint.y, line2Start.y, line2End.y, out y);

        var line1DirNrm = line1Dir.normalized;
        var line12StartDot = Vector3.Dot(line1DirNrm, (line2Start - line1Start).normalized);
        var line12EndDot = Vector3.Dot(line1DirNrm, (line2End - line1Start).normalized);

        if (isXClamped || isYClamped || line12StartDot < 0 || line12EndDot < 0)
        {
            var aDist = Vector3.Distance(line2Start, line1End);
            var bDist = Vector3.Distance(line2End, line1End);

            x = aDist < bDist ? line2Start.x : line2End.x;
            y = aDist < bDist ? line2Start.y : line2End.y;
        }

        return new Vector3(x, y, 0);
    }

    private bool TryClamp(float value, float min, float max, out float result)
    {
        // Correct the min and max to account for negative to positive ranges
        var newMin = Mathf.Min(min, max);
        var newMax = Mathf.Max(min, max);

        if (value < newMin)
        {
            result = newMin;
            return true;
        }

        if (value > newMax)
        {
            result = newMax;
            return true;
        }

        result = value;
        return false;
    }

    private bool LineLineIntersection(Vector3 line1Start, Vector3 line1Dir, Vector3 line2Start, Vector3 line2Dir, out Vector3 intersection)
    {
        var lineVec3 = line2Start - line1Start;
        var crossVec1and2 = Vector3.Cross(line1Dir, line2Dir);
        var crossVec3and2 = Vector3.Cross(lineVec3, line2Dir);

        var planarFactor = Vector3.Dot(lineVec3, crossVec1and2);

        // Lines are coplanar and not parallel
        if (Mathf.Abs(planarFactor) < 0.0001f && crossVec1and2.sqrMagnitude > 0.0001f)
        {
            var sqrMag = crossVec1and2.sqrMagnitude;
            var amount = Vector3.Dot(crossVec3and2, crossVec1and2) / sqrMag;

            intersection = line1Start + (line1Dir * amount);

            return true;
        }

        intersection = Vector3.zero;
        return false;
    }
}
