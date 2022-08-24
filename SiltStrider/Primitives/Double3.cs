using System;
using System.Collections.Generic;
using System.Text;

namespace SiltStrider.Primitives
{
    public struct Double3 : IEquatable<Double3>
    {
        public Double3(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public double X { get; }
        public double Y { get; }
        public double Z { get; }

        public override bool Equals(object obj)
        {
            return obj is Double3 @double && Equals(@double);
        }

        public bool Equals(Double3 other)
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

        public static bool operator ==(Double3 left, Double3 right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Double3 left, Double3 right)
        {
            return !(left == right);
        }
    }
}
