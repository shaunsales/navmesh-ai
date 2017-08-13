using GeneralPurpose.Types;
using System;

namespace GeneralPurpose.Utils
{
    public struct Mathf
    {
        public const float PI = 3.14159274f;
        public const float DEG_TO_RAD = 0.0174532924f;
        public const float RAD_TO_DEG = 57.29578f;

        public static float Sin(float f) => (float)Math.Sin(f);

        public static float Cos(float f) => (float)Math.Cos(f);

        public static float Tan(float f) => (float)Math.Tan(f);

        public static float Asin(float f) => (float)Math.Asin(f);

        public static float Acos(float f) => (float)Math.Acos(f);

        public static float Atan(float f) => (float)Math.Atan(f);

        public static float Atan2(float y, float x) => (float)Math.Atan2(y, x);

        public static float Sqrt(float f) => (float)Math.Sqrt(f);

        public static float Abs(float f) => Math.Abs(f);

        public static int Abs(int value) => Math.Abs(value);

        public static float Min(float a, float b) => (a >= b) ? b : a;

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

        public static int Min(int a, int b) => (a >= b) ? b : a;

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

        public static float Max(float a, float b) => (a <= b) ? b : a;

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

        public static int Max(int a, int b) => (a <= b) ? b : a;

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

        public static float Pow(float f, float p) => (float)Math.Pow(f, p);

        public static float Exp(float power) => (float)Math.Exp(power);

        public static float Log(float f, float p) => (float)Math.Log(f, p);

        public static float Log(float f) => (float)Math.Log(f);

        public static float Log10(float f) => (float)Math.Log10(f);

        public static float Ceil(float f) => (float)Math.Ceiling(f);

        public static float Floor(float f) => (float)Math.Floor(f);

        public static float Round(float f) => (float)Math.Round(f);

        public static int CeilToInt(float f) => (int)Math.Ceiling(f);

        public static int FloorToInt(float f) => (int)Math.Floor(f);

        public static int RoundToInt(float f) => (int)Math.Round(f);

        public static float Sign(float f) => (f < 0f) ? -1f : 1f;

        public static bool Clamp(float value, float min, float max, out float clampedValue)
        {
            // Correct the min and max to account for negative to positive ranges
            var newMin = Min(min, max);
            var newMax = Max(min, max);

            if (value < newMin)
            {
                clampedValue = newMin;
                return true;
            }

            if (value > newMax)
            {
                clampedValue = newMax;
                return true;
            }

            clampedValue = value;
            return false;
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

        public static float Lerp(float a, float b, float t) => a + (b - a) * Clamp01(t);

        public static float LerpUnclamped(float a, float b, float t) => a + (b - a) * t;

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

        public static bool Approximately(float a, float b) => Abs(b - a) < Max(1E-06f * Max(Abs(a), Abs(b)), float.Epsilon * 8f);

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

        public static float Repeat(float t, float length) => t - Floor(t / length) * length;

        public static float PingPong(float t, float length) => length - Abs(Repeat(t, length * 2f) - length);

        public static float InverseLerp(float a, float b, float value) => Approximately(a, b) ? 0f : Clamp01((value - a) / (b - a));

        public static float DeltaAngle(float current, float target)
        {
            var num = Repeat(target - current, 360f);

            if (num > 180f)
            {
                num -= 360f;
            }

            return num;
        }

        public static long RandomToLong(Random r)
        {
            var array = new byte[8];
            r.NextBytes(array);

            return (long)(BitConverter.ToUInt64(array, 0) & 9223372036854775807uL);
        }
    }
}