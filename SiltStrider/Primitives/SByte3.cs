using System;

namespace SiltStrider.Primitives
{
    public struct SByte3 : IEquatable<SByte3>
    {
        public sbyte X { get; }
        public sbyte Y { get; }
        public sbyte Z { get; }

        public SByte3(sbyte x, sbyte y, sbyte z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public override bool Equals(object obj)
        {
            return obj is SByte3 vector && Equals(vector);
        }

        public bool Equals(SByte3 other)
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

        public static bool operator ==(SByte3 left, SByte3 right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(SByte3 left, SByte3 right)
        {
            return !(left == right);
        }
    }
}