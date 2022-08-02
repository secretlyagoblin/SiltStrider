using SiltStrider.Primitives;
using System.Collections.Generic;

namespace SiltStrider.Records
{
    public abstract class OverlandRecord :  Record
    {
        public Long2 Location { get; }

        public OverlandRecord(string name, Long2 location) : base(name)
        {
            Location = location;
        }
    }
}