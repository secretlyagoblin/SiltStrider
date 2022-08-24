using SiltStrider.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiltStrider
{
    public class ES3Document
    {
        public string Name { get; set; } = "MyPlugin";

        public IList<Record> Records { get; } = new List<Record>();

        public string ToYaml() => Conversion.Converters.ToYaml(this);
    }
}
