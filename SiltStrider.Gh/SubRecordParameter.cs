using Grasshopper.Kernel;
using System;

namespace SiltStrider.Gh
{
    public class SubRecordParameter : GH_Param<GH_SubRecord>
    {
        public SubRecordParameter() : base("SubRecord", "SR", "", "Silt Strider", "Types", GH_ParamAccess.item)
        {
        }

        public override Guid ComponentGuid => new Guid("32B5C5D0-232D-4768-B2FF-E3E87216A708");
    }
}