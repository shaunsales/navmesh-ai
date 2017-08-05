using GeneralPurpose.Types;
using UnityEngine;

namespace ExtensionMethods
{
    public static class Extenstions
    {
        public static Vector3 ToVector3(this GpVector3 gpVector3)
        {
            return new Vector3(gpVector3.X, gpVector3.Y, gpVector3.Z);
        }

        public static GpVector3 ToGpVector3(this Vector3 vector3)
        {
            return new GpVector3(vector3.x, vector3.y, vector3.z);
        }

        public static Vector3[] ToVector3Array(this GpVector3[] gpVector3)
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

        public static GpVector3[] ToGpVector3Array(this Vector3[] vector3)
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
}
