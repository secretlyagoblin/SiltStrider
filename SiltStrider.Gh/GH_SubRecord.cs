using Grasshopper.Kernel.Types;
using Rhino.Geometry;
using SiltStrider.Records;
using SiltStrider.Conversion;
using Grasshopper.Kernel;

namespace SiltStrider.Gh
{
    public class GH_SubRecord : GH_Goo<SubRecord>
    {
        public override bool IsValid => true;

        public override string TypeName => "SubRecord";

        public override string TypeDescription => "Record for generating Morrowind files";

        public bool Hidden { get; set; }

        public bool IsPreviewCapable => false;

        public override IGH_Goo Duplicate()
        {
            throw new System.NotImplementedException();
        }

        public override string ToString()
        {
            return Value.Type;
        }
    }
}