using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiltStrider.Gh
{
    internal class SiltStriderInfo : Grasshopper.Kernel.GH_AssemblyInfo
    {

        public override string Version => "0.1.0";

        public override string Name => "Silt Strider";

        //public override Bitmap Icon => null;

        public override string Description => "A utility library for deserializing and composing JSON.";

        public override Guid Id => new Guid("26EC1541-F909-4F6B-A93C-440186C09303");

        public override string AuthorName => "Chris Welch";

        public override string AuthorContact => "";
    }
}
