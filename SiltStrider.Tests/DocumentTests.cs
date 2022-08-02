using NUnit.Framework;

namespace SiltStrider.Tests
{
    public class DocumentTests
    {
        [Test]
        public void CreateRecord()
        {
            var instance = new Records.Instance("boyMod", 1);

            var cell = new Records.Cell(0, 0, Records.Region.BitterCoastRegion, new List<Records.Instance>() { instance });

            var landscape = new Records.Landscape(0, 0, Enumerable.Range(0, 65 * 65).Select(x => Random.Shared.Next(-120, 120)));

            var e3 = new ES3Document();
            e3.Records.Add(cell);
            e3.Records.Add(landscape);


            var yaml = e3.ToYaml();

            Assert.IsNotNull(yaml);

        }

    }
}