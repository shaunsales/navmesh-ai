using System;

namespace GeneralPurpose.Navigation.NavMesh
{
    public struct Mathf
    {
        public const float PI = 3.14159274f;

        public const float DEG_TO_RAD = 0.0174532924f;

        public const float RAD_TO_DEG = 57.29578f;

        public static float Sin(float f)
        {
            return (float)Math.Sin(f);
        }

        public static float Cos(float f)
        {
            return (float)Math.Cos(f);
        }

        public static float Tan(float f)
        {
            return (float)Math.Tan(f);
        }

        public static float Asin(float f)
        {
            return (float)Math.Asin(f);
        }

        public static float Acos(float f)
        {
            return (float)Math.Acos(f);
        }

        public static float Atan(float f)
        {
            return (float)Math.Atan(f);
        }

        public static float Atan2(float y, float x)
        {
            return (float)Math.Atan2(y, x);
        }

        public static float Sqrt(float f)
        {
            return (float)Math.Sqrt(f);
        }

        public static float Abs(float f)
        {
            return Math.Abs(f);
        }

        public static int Abs(int value)
        {
            return Math.Abs(value);
        }

        public static float Min(float a, float b)
        {
            return (a >= b) ? b : a;
        }

        public static float Min(params float[] values)
        {
            int num = values.Length;
            float result;
            if (num == 0)
            {
                result = 0f;
            }
            else
            {
                float num2 = values[0];
                for (int i = 1; i < num; i++)
                {
                    if (values[i] < num2)
                    {
                        num2 = values[i];
                    }
                }
                result = num2;
            }
            return result;
        }

        public static int Min(int a, int b)
        {
            return (a >= b) ? b : a;
        }

        public static int Min(params int[] values)
        {
            int num = values.Length;
            int result;
            if (num == 0)
            {
                result = 0;
            }
            else
            {
                int num2 = values[0];
                for (int i = 1; i < num; i++)
                {
                    if (values[i] < num2)
                    {
                        num2 = values[i];
                    }
                }
                result = num2;
            }
            return result;
        }

        public static float Max(float a, float b)
        {
            return (a <= b) ? b : a;
        }

        public static float Max(params float[] values)
        {
            int num = values.Length;
            float result;
            if (num == 0)
            {
                result = 0f;
            }
            else
            {
                float num2 = values[0];
                for (int i = 1; i < num; i++)
                {
                    if (values[i] > num2)
                    {
                        num2 = values[i];
                    }
                }
                result = num2;
            }
            return result;
        }

        public static int Max(int a, int b)
        {
            return (a <= b) ? b : a;
        }

        public static int Max(params int[] values)
        {
            int num = values.Length;
            int result;
            if (num == 0)
            {
                result = 0;
            }
            else
            {
                int num2 = values[0];
                for (int i = 1; i < num; i++)
                {
                    if (values[i] > num2)
                    {
                        num2 = values[i];
                    }
                }
                result = num2;
            }
            return result;
        }

        public static float Pow(float f, float p)
        {
            return (float)Math.Pow(f, p);
        }

        public static float Exp(float power)
        {
            return (float)Math.Exp(power);
        }

        public static float Log(float f, float p)
        {
            return (float)Math.Log(f, p);
        }

        public static float Log(float f)
        {
            return (float)Math.Log(f);
        }

        public static float Log10(float f)
        {
            return (float)Math.Log10(f);
        }

        public static float Ceil(float f)
        {
            return (float)Math.Ceiling(f);
        }

        public static float Floor(float f)
        {
            return (float)Math.Floor(f);
        }

        public static float Round(float f)
        {
            return (float)Math.Round(f);
        }

        public static int CeilToInt(float f)
        {
            return (int)Math.Ceiling(f);
        }

        public static int FloorToInt(float f)
        {
            return (int)Math.Floor(f);
        }

        public static int RoundToInt(float f)
        {
            return (int)Math.Round(f);
        }

        public static float Sign(float f)
        {
            return (f < 0f) ? -1f : 1f;
        }

        public static float Clamp(float value, float min, float max)
        {
            if (value < min)
            {
                value = min;
            }
            else if (value > max)
            {
                value = max;
            }
            return value;
        }

        public static int Clamp(int value, int min, int max)
        {
            if (value < min)
            {
                value = min;
            }
            else if (value > max)
            {
                value = max;
            }
            return value;
        }

        public static float Clamp01(float value)
        {
            float result;
            if (value < 0f)
            {
                result = 0f;
            }
            else if (value > 1f)
            {
                result = 1f;
            }
            else
            {
                result = value;
            }
            return result;
        }

        public static float Lerp(float a, float b, float t)
        {
            return a + (b - a) * Clamp01(t);
        }

        public static float LerpUnclamped(float a, float b, float t)
        {
            return a + (b - a) * t;
        }

        public static float LerpAngle(float a, float b, float t)
        {
            float num = Repeat(b - a, 360f);
            if (num > 180f)
            {
                num -= 360f;
            }
            return a + num * Clamp01(t);
        }

        public static float MoveTowards(float current, float target, float maxDelta)
        {
            float result;
            if (Abs(target - current) <= maxDelta)
            {
                result = target;
            }
            else
            {
                result = current + Sign(target - current) * maxDelta;
            }
            return result;
        }

        public static float MoveTowardsAngle(float current, float target, float maxDelta)
        {
            float num = DeltaAngle(current, target);
            float result;
            if (-maxDelta < num && num < maxDelta)
            {
                result = target;
            }
            else
            {
                target = current + num;
                result = MoveTowards(current, target, maxDelta);
            }
            return result;
        }

        public static float SmoothStep(float from, float to, float t)
        {
            t = Clamp01(t);
            t = -2f * t * t * t + 3f * t * t;
            return to * t + from * (1f - t);
        }

        public static float Gamma(float value, float absmax, float gamma)
        {
            var flag = value < 0f;
            var num = Abs(value);

            float result;
            if (num > absmax)
            {
                result = !flag ? num : -num;
            }
            else
            {
                var num2 = Pow(num / absmax, gamma) * absmax;
                result = !flag ? num2 : -num2;
            }
            return result;
        }

        public static bool Approximately(float a, float b)
        {
            return Abs(b - a) < Max(1E-06f * Max(Abs(a), Abs(b)), float.Epsilon * 8f);
        }

        public static float SmoothDamp(float current, float target, ref float currentVelocity, float smoothTime, float maxSpeed, float deltaTime)
        {
            smoothTime = Max(0.0001f, smoothTime);

            var num = 2f / smoothTime;
            var num2 = num * deltaTime;
            var num3 = 1f / (1f + num2 + 0.48f * num2 * num2 + 0.235f * num2 * num2 * num2);
            var num4 = current - target;
            var num5 = target;
            var num6 = maxSpeed * smoothTime;

            num4 = Clamp(num4, -num6, num6);
            target = current - num4;

            var num7 = (currentVelocity + num * num4) * deltaTime;

            currentVelocity = (currentVelocity - num * num7) * num3;

            var num8 = target + (num4 + num7) * num3;

            if (num5 - current > 0f == num8 > num5)
            {
                num8 = num5;
                currentVelocity = (num8 - num5) / deltaTime;
            }

            return num8;
        }

        public static float SmoothDampAngle(float current, float target, ref float currentVelocity, float smoothTime, float maxSpeed, float deltaTime)
        {
            target = current + DeltaAngle(current, target);
            return SmoothDamp(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
        }

        public static float Repeat(float t, float length)
        {
            return t - Floor(t / length) * length;
        }

        public static float PingPong(float t, float length)
        {
            t = Repeat(t, length * 2f);
            return length - Abs(t - length);
        }

        public static float InverseLerp(float a, float b, float value)
        {
            float result;

            result = a != b ? Clamp01((value - a) / (b - a)) : 0f;

            return result;
        }

        public static float DeltaAngle(float current, float target)
        {
            var num = Repeat(target - current, 360f);

            if (num > 180f)
            {
                num -= 360f;
            }

            return num;
        }

        internal static bool LineIntersection(GpVector3 p1, GpVector3 p2, GpVector3 p3, GpVector3 p4, ref GpVector3 result)
        {
            var num = p2.X - p1.X;
            var num2 = p2.Y - p1.Y;
            var num3 = p4.X - p3.X;
            var num4 = p4.Y - p3.Y;
            var num5 = num * num4 - num2 * num3;

            bool result2;
            if (num5 == 0f)
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

        internal static bool LineSegmentIntersection(GpVector3 p1, GpVector3 p2, GpVector3 p3, GpVector3 p4, ref GpVector3 result)
        {
            var num = p2.X - p1.X;
            var num2 = p2.Y - p1.Y;
            var num3 = p4.X - p3.X;
            var num4 = p4.Y - p3.Y;
            var num5 = num * num4 - num2 * num3;
            bool result2;

            if (num5 == 0f)
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

        internal static long RandomToLong(System.Random r)
        {
            var array = new byte[8];
            r.NextBytes(array);

            return (long)(BitConverter.ToUInt64(array, 0) & 9223372036854775807uL);
        }
    }
}