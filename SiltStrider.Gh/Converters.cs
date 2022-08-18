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
        public static Float3 ToMorrow(this Rhino.Geometry.Point3d p)
        {
            return new Float3((float)p.X, (float)p.Y, (float)p.Z);
        }
    }
}
