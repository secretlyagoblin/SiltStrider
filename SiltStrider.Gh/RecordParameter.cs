using Grasshopper.Kernel;
using System;

namespace SiltStrider.Gh
{
    public class RecordParameter : GH_Param<GH_Record>
    {
        public RecordParameter() : base("Record", "R", "", category, subcategory, access)
        {
        }

        public override Guid ComponentGuid => new Guid("2CFD4DC1-0AFF-4EB1-BE13-6D4CC4AA22FD");
    }
}