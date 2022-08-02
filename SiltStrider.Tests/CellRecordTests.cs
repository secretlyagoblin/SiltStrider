using NUnit.Framework;

namespace SiltStrider.Tests
{
    public class CellRecordTests
    {
        [Test]
        public void CreateRecord()
        {
            var instance = new Records.Instance("boyMod", 1);

            var record = new Records.Cell(0,0, Records.Region.BitterCoastRegion, new List<Records.Instance>() { instance });


            Assert.IsNotNull(record);

            var e3 = new ES3Document();
            e3.Records.Add(record);

            var yaml = e3.ToYaml();

            Assert.IsNotNull(yaml);

        }

    }
}