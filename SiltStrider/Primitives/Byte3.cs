using System;

namespace SiltStrider.Primitives
{
    public struct Byte3 : IEquatable<Byte3>
    {
        public byte X { get; }
        public byte Y { get; }
        public byte Z { get; }

        public Byte3(byte x, byte y, byte z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public override bool Equals(object obj)
        {
            return obj is Byte3 vector && Equals(vector);
        }

        public bool Equals(Byte3 other)
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

        public static bool operator ==(Byte3 left, Byte3 right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Byte3 left, Byte3 right)
        {
            return !(left == right);
        }
    }
}