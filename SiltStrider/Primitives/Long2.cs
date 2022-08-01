using System;
using System.Collections.Generic;
using System.Text;

namespace SiltStrider.Primitives
{
    internal class Long2 : IEquatable<Long2>
    {
        public Long2(long x, long y)
        {
            X = x;
            Y = y;
        }

        public long X { get;set; }
        public long Y { get;set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as Long2);
        }

        public bool Equals(Long2 other)
        {
            return other != null &&
                   X == other.X &&
                   Y == other.Y;
        }

        public override int GetHashCode()
        {
            int hashCode = 1861411795;
            hashCode = hashCode * -1521134295 + X.GetHashCode();
            hashCode = hashCode * -1521134295 + Y.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(Long2 left, Long2 right)
        {
            return EqualityComparer<Long2>.Default.Equals(left, right);
        }

        public static bool operator !=(Long2 left, Long2 right)
        {
            return !(left == right);
        }
    }
}
