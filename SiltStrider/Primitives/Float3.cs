using System;
using System.Collections.Generic;
using System.Text;

namespace SiltStrider.Primitives
{
    public struct Float3 : IEquatable<Float3>
    {
        public Float3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public float X { get; }
        public float Y { get; }
        public float Z { get; }

        public override bool Equals(object obj)
        {
            return obj is Float3 @float && Equals(@float);
        }

        public bool Equals(Float3 other)
        {
            return X == other.X &&
                   Y == other.Y &&
                   Z == other.Z;
        }

        public override int GetHashCode()
        {
            int hashCode = -307843816;
            hashCode = hashCode * -1521134295 + X.GetHashCode();
            hashCode = hashCode * -1521134295 + Y.GetHashCode();
            hashCode = hashCode * -1521134295 + Z.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(Float3 left, Float3 right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Float3 left, Float3 right)
        {
            return !(left == right);
        }
    }
}
