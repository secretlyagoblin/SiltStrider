using SiltStrider.Primitives;
using System.Collections.Generic;
using System.Linq;

namespace SiltStrider.Records
{
    public class LandscapeRecord : Record
    {
        public override string Type => "Landscape";
        public long CellX { get;set; }
        public long CellY { get;set; } 
        public float VerticalDatum { get; set; }
        public sbyte[,] Heightmap { get; } = new sbyte[65, 65];
        public SByte3[,] Normalmap { get; } = new SByte3[65, 65];
        public Byte3[,] ColourMap { get; } = new Byte3[65, 65];

        //https://en.uesp.net/morrow/tech/mw_esm.txt

        //A few more records missing

        public LandscapeRecord(string name, int cellX, int cellY, IEnumerable<int> heights) : base(name)
        {
            CellX = cellX;
            CellY = cellY;

            int verticalDatum = heights.Min();
            VerticalDatum = verticalDatum;

            var count = 0;

            for (int x = 0; x < 65; x++)
            {
                for (int y = 0; y < 65; y++)
                {
                    var potentialSbyte = heights.ElementAt(count++) - verticalDatum;

                    //here I need to really understand how the offsetting works -
                    // https://en.uesp.net/morrow/tech/mw_esm.txt is kind of vague

                    Heightmap[x, y] = (sbyte)potentialSbyte;
                    Normalmap[x, y] = new SByte3(0, 0, 1);
                    ColourMap[x, y] = new Byte3(0, 0, 0);
                }
            }
  
        }
    }
}