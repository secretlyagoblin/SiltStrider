using NUnit.Framework;

namespace SiltStrider.Tests
{
    public class LandscapeRecordTests
    {
        [Test]
        public void CreateRecord()
        {
            var record = new Records.LandscapeRecord("testCell",0,0,Enumerable.Range(0,65*65).Select(x => Random.Shared.Next(-120,120)));


            Assert.IsNotNull(record);

            var e3 = new ES3Document();
            e3.Records.Add(record);

            var yaml = e3.ToYaml();

            Assert.IsNotNull(yaml);

        }

    }
}