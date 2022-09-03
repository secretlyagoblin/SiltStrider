using SiltStrider.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiltStrider.Gh
{
    internal static class Converters
    {
        public static Float3 ToMorrowFloat(this Rhino.Geometry.Point3d p)
        {
            return new Float3((float)p.X, (float)p.Y, (float)p.Z);
        }

        public static Double3 ToMorrow(this Rhino.Geometry.Point3d p)
        {
            return new Double3(p.X, p.Y, p.Z);
        }

        public static Double3 ToMorrow(this Rhino.Geometry.Vector3d v)
        {
            return new Double3(v.X, v.Y, v.Z);
        }
    }
}
