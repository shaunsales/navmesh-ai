using GeneralPurpose.Types;

namespace GeneralPurpose.Utils
{
    public static class VectorMath
    {
        /// <summary>
        /// Get the nearest intersecting point of a line and segment, clamped to the nearest segment point if there is no intersection 
        /// </summary>
        public static GpVector3 GetNearestPointOnSegment(GpVector3 p1, GpVector3 p2, GpVector3 p3, GpVector3 p4)
        {
            var result = GpVector3.Zero;

            if (LineSegmentIntersection(p1, p2, p3, p4, ref result))
            {
                return result;
            }

            return GpVector3.Distance(p2, p3) < GpVector3.Distance(p2, p4) ? p3 : p4;
        }
        
        /// <summary>
        /// Find the squared distance to the nearest point on a line
        /// </summary>
        public static float DistanceToLineSqr(GpVector3 point, GpVector3 lineStart, GpVector3 lineEnd)
        {
            var lineDist = GpVector3.DistanceSqr(lineStart, lineEnd);

            // If the line is zero length, get the distance to the start point
            if (Mathf.Approximately(lineDist, 0f))
            {
                return GpVector3.DistanceSqr(point, lineStart);
            }

            var t = ((point.X - lineStart.X) * (lineEnd.X - lineStart.X) + (point.Y - lineStart.Y) * (lineEnd.Y - lineStart.Y) + (point.Z - lineStart.Z) * (lineEnd.Z - lineStart.Z)) / lineDist;
            t = Mathf.Clamp(t, 0, 1);

            var nearest = new GpVector3(lineStart.X + t * (lineEnd.X - lineStart.X), lineStart.Y + t * (lineEnd.Y - lineStart.Y), lineStart.Z + t * (lineEnd.Z - lineStart.Z));

            return GpVector3.DistanceSqr(point, nearest);
        }

        /// <summary>
        /// Find the interecting point between two infinte lines
        /// </summary>
        public static bool LineIntersection(GpVector3 p1, GpVector3 p2, GpVector3 p3, GpVector3 p4, ref GpVector3 result)
        {
            var num = p2.X - p1.X;
            var num2 = p2.Y - p1.Y;
            var num3 = p4.X - p3.X;
            var num4 = p4.Y - p3.Y;
            var num5 = num * num4 - num2 * num3;

            bool result2;
            if (Mathf.Approximately(num5, 0f))
            {
                result2 = false;
            }
            else
            {
                var num6 = p3.X - p1.X;
                var num7 = p3.Y - p1.Y;
                var num8 = (num6 * num4 - num7 * num3) / num5;

                result = new GpVector3(p1.X + num8 * num, p1.Y + num8 * num2);
                result2 = true;
            }
            return result2;
        }

        /// <summary>
        /// Find the intersecting point between an infinite line and a segment
        /// </summary>
        public static bool LineSegmentIntersection(GpVector3 p1, GpVector3 p2, GpVector3 p3, GpVector3 p4, ref GpVector3 result)
        {
            var num = p2.X - p1.X;
            var num2 = p2.Y - p1.Y;
            var num3 = p4.X - p3.X;
            var num4 = p4.Y - p3.Y;
            var num5 = num * num4 - num2 * num3;
            bool result2;

            if (Mathf.Approximately(num5, 0f))
            {
                result2 = false;
            }
            else
            {
                var num6 = p3.X - p1.X;
                var num7 = p3.Y - p1.Y;
                var num8 = (num6 * num4 - num7 * num3) / num5;
                if (num8 < 0f || num8 > 1f)
                {
                    result2 = false;
                }
                else
                {
                    var num9 = (num6 * num2 - num7 * num) / num5;
                    if (num9 < 0f || num9 > 1f)
                    {
                        result2 = false;
                    }
                    else
                    {
                        result = new GpVector3(p1.X + num8 * num, p1.Y + num8 * num2);
                        result2 = true;
                    }
                }
            }
            return result2;
        }
    }
}
