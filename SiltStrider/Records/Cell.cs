using System;
using System.Collections.Generic;
using System.Text;

namespace SiltStrider.Records
{
    public class Cell : OverlandRecord
    {
        public Cell(long cellX, long cellY, Region region, IList<Instance> references) : base($"cell::{cellX}x{cellY}",new Primitives.Long2())
        {
            Region = region;
            References = references;
        }

        public override string Type => "Cell";
        public Region Region { get; set; }
        public IList<Instance> References { get; } = new List<Instance>();

        



    }
}
