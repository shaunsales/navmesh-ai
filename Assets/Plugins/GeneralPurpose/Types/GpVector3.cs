using System;
using GeneralPurpose.Utils;

namespace GeneralPurpose.Types
{
    public struct GpVector3
    {
        public const float EPSILON = 1E-05f;

        public float X;
        public float Y;
        public float Z;

        public float this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return X;
                    case 1: return Y;
                    case 2: return Z;
                    default: throw new IndexOutOfRangeException("Invalid index for GpVector3. Should be less than 3.");
                }
            }
            set
            {
                switch (index)
                {
                    case 0:
                        X = value;
                        break;
                    case 1:
                        Y = value;
                        break;
                    case 2:
                        Z = value;
                        break;
                    default:
                        throw new IndexOutOfRangeException("Invalid index for GpVector3. Should be less than 3.");
                }
            }
        }

        public static GpVector3 Zero => new GpVector3();

        public static GpVector3 One => new GpVector3(1f, 1f, 1f);

        public static GpVector3 Forward => new GpVector3(z: 1f);

        public static GpVector3 Back => new GpVector3(z: -1f);

        public static GpVector3 Up => new GpVector3(y: 1f);

        public static GpVector3 Down => new GpVector3(y: -1f);

        public static GpVector3 Left => new GpVector3(x: -1f);

        public static GpVector3 Right => new GpVector3(x: 1f);

        public GpVector3(float x = 0, float y = 0, float z = 0)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public void Set(float newX, float newY, float newZ)
        {
            X = newX;
            Y = newY;
            Z = newZ;
        }

        public void Scale(GpVector3 scale)
        {
            X *= scale.X;
            Y *= scale.Y;
            Z *= scale.Z;
        }

        public void Normalize()
        {
            var num = Magnitude(this);

            if (num > EPSILON)
            {
                this /= num;
            }
            else
            {
                this = Zero;
            }
        }

        #region Static Helpers

        public static GpVector3 Lerp(GpVector3 a, GpVector3 b, float t)
        {
            t = Mathf.Clamp01(t);
            return new GpVector3(a.X + (b.X - a.X) * t, a.Y + (b.Y - a.Y) * t, a.Z + (b.Z - a.Z) * t);
        }

        public static GpVector3 LerpUnclamped(GpVector3 a, GpVector3 b, float t)
        {
            return new GpVector3(a.X + (b.X - a.X) * t, a.Y + (b.Y - a.Y) * t, a.Z + (b.Z - a.Z) * t);
        }

        public static GpVector3 MoveTowards(GpVector3 current, GpVector3 target, float maxDistanceDelta)
        {
            var a = target - current;
            var magnitude = Magnitude(a);

            GpVector3 result;
            if (magnitude <= maxDistanceDelta || magnitude == 0f)
            {
                result = target;
            }
            else
            {
                result = current + a / magnitude * maxDistanceDelta;
            }

            return result;
        }

        public static GpVector3 SmoothDamp(GpVector3 current, GpVector3 target, ref GpVector3 currentVelocity, float smoothTime, float maxSpeed, float deltaTime)
        {
            smoothTime = Mathf.Max(0.0001f, smoothTime);

            var num = 2f / smoothTime;
            var num2 = num * deltaTime;
            var d = 1f / (1f + num2 + 0.48f * num2 * num2 + 0.235f * num2 * num2 * num2);

            var vector = current - target;
            var vector2 = target;

            var maxLength = maxSpeed * smoothTime;
            vector = ClampMagnitude(vector, maxLength);
            target = current - vector;

            var vector3 = (currentVelocity + num * vector) * deltaTime;
            currentVelocity = (currentVelocity - num * vector3) * d;
            var vector4 = target + (vector + vector3) * d;

            if (Dot(vector2 - current, vector4 - vector2) > 0f)
            {
                vector4 = vector2;
                currentVelocity = (vector4 - vector2) / deltaTime;
            }

            return vector4;
        }

        public static GpVector3 Scale(GpVector3 a, GpVector3 b)
        {
            return new GpVector3(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        }

        public static GpVector3 Cross(GpVector3 lhs, GpVector3 rhs)
        {
            return new GpVector3(lhs.Y * rhs.Z - lhs.Z * rhs.Y, lhs.Z * rhs.X - lhs.X * rhs.Z, lhs.X * rhs.Y - lhs.Y * rhs.X);
        }

        public static GpVector3 Reflect(GpVector3 inDirection, GpVector3 inNormal)
        {
            return -2f * Dot(inNormal, inDirection) * inNormal + inDirection;
        }

        public static GpVector3 Normalize(GpVector3 value)
        {
            float num = Magnitude(value);
            GpVector3 result;

            if (num > EPSILON)
            {
                result = value / num;
            }
            else
            {
                result = Zero;
            }

            return result;
        }

        public static float Dot(GpVector3 lhs, GpVector3 rhs)
        {
            return lhs.X * rhs.X + lhs.Y * rhs.Y + lhs.Z * rhs.Z;
        }

        public static GpVector3 Project(GpVector3 vector, GpVector3 onNormal)
        {
            var num = Dot(onNormal, onNormal);
            GpVector3 result;

            if (num < float.Epsilon)
            {
                result = Zero;
            }
            else
            {
                result = onNormal * Dot(vector, onNormal) / num;
            }

            return result;
        }

        public static GpVector3 ProjectOnPlane(GpVector3 vector, GpVector3 planeNormal)
        {
            return vector - Project(vector, planeNormal);
        }

        public static float Angle(GpVector3 from, GpVector3 to)
        {
            return Mathf.Acos(Mathf.Clamp(Dot(Normalize(from), Normalize(to)), -1f, 1f)) * 57.29578f;
        }

        public static float Distance(GpVector3 a, GpVector3 b)
        {
            var vector = new GpVector3(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
            return Mathf.Sqrt(vector.X * vector.X + vector.Y * vector.Y + vector.Z * vector.Z);
        }

        public static GpVector3 ClampMagnitude(GpVector3 vector, float maxLength)
        {
            GpVector3 result;
            if (SqrMagnitude(vector) > maxLength * maxLength)
            {
                result = Normalize(vector) * maxLength;
            }
            else
            {
                result = vector;
            }

            return result;
        }

        public static float Magnitude(GpVector3 a)
        {
            return Mathf.Sqrt(a.X * a.X + a.Y * a.Y + a.Z * a.Z);
        }

        public static float SqrMagnitude(GpVector3 a)
        {
            return a.X * a.X + a.Y * a.Y + a.Z * a.Z;
        }

        public static GpVector3 Min(GpVector3 lhs, GpVector3 rhs)
        {
            return new GpVector3(Mathf.Min(lhs.X, rhs.X), Mathf.Min(lhs.Y, rhs.Y), Mathf.Min(lhs.Z, rhs.Z));
        }

        public static GpVector3 Max(GpVector3 lhs, GpVector3 rhs)
        {
            return new GpVector3(Mathf.Max(lhs.X, rhs.X), Mathf.Max(lhs.Y, rhs.Y), Mathf.Max(lhs.Z, rhs.Z));
        }

        #endregion

        #region Operator Overrides

        public static GpVector3 operator +(GpVector3 a, GpVector3 b)
        {
            return new GpVector3(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }

        public static GpVector3 operator -(GpVector3 a, GpVector3 b)
        {
            return new GpVector3(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }

        public static GpVector3 operator -(GpVector3 a)
        {
            return new GpVector3(-a.X, -a.Y, -a.Z);
        }

        public static GpVector3 operator *(GpVector3 a, float d)
        {
            return new GpVector3(a.X * d, a.Y * d, a.Z * d);
        }

        public static GpVector3 operator *(float d, GpVector3 a)
        {
            return new GpVector3(a.X * d, a.Y * d, a.Z * d);
        }

        public static GpVector3 operator /(GpVector3 a, float d)
        {
            return new GpVector3(a.X / d, a.Y / d, a.Z / d);
        }

        public static bool operator ==(GpVector3 lhs, GpVector3 rhs)
        {
            return SqrMagnitude(lhs - rhs) < 9.99999944E-11f;
        }

        public static bool operator !=(GpVector3 lhs, GpVector3 rhs)
        {
            return !(lhs == rhs);
        }

        #endregion

        #region Base Object Overrides

        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode() << 2 ^ Z.GetHashCode() >> 2;
        }

        public override bool Equals(object other)
        {
            bool result;

            if (!(other is GpVector3))
            {
                result = false;
            }
            else
            {
                var vector = (GpVector3)other;
                result = X.Equals(vector.X) && Y.Equals(vector.Y) && Z.Equals(vector.Z);
            }

            return result;
        }

        public override string ToString()
        {
            return $"({X:F1}, {Y:F1}, {Z:F1})";
        }

        #endregion
    }
}
