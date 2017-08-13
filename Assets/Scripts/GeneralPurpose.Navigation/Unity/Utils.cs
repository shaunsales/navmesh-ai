using UnityEngine;

namespace GeneralPurpose.Navigation.Unity
{
    public static class Utils
    {
        private const string SHADER_NAME = "Unlit/Color";

        public static LineRenderer CreateLine(string name, Vector3 start, Vector3 end, Color color)
        {
            var go = new GameObject(name);
            go.transform.position = start;

            var lineRenderer = go.AddComponent<LineRenderer>();

            AssignMaterial(go, color);

            lineRenderer.startWidth = 0.1f;
            lineRenderer.endWidth = 0.1f;

            lineRenderer.SetPosition(0, start);
            lineRenderer.SetPosition(1, end);

            return lineRenderer;
        }

        public static GameObject CreateSphere(string name, Vector3 position, float scale, Color color)
        {
            var go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            go.name = name;
            go.transform.position = position;
            go.transform.localScale = Vector3.one * scale;

            AssignMaterial(go, color);

            return go;
        }

        private static void AssignMaterial(GameObject go, Color color)
        {
            var renderer = go.GetComponent<Renderer>();
            var shader = Shader.Find("Unlit/Color");

            renderer.material = new Material(shader)
            {
                color = color
            };
        }
    }
}
