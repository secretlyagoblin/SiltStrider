using Grasshopper.Kernel.Types;
using SiltStrider.Records;

namespace SiltStrider.Gh
{
    public class GH_Record : GH_Goo<Record>
    {
        public override bool IsValid => throw new System.NotImplementedException();

        public override string TypeName => throw new System.NotImplementedException();

        public override string TypeDescription => throw new System.NotImplementedException();

        public override IGH_Goo Duplicate()
        {
            throw new System.NotImplementedException();
        }

        public override string ToString()
        {
            throw new System.NotImplementedException();
        }
    }
}