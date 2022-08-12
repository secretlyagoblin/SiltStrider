using SiltStrider.Primitives;
using System.Collections.Generic;
using System.Linq;

namespace SiltStrider.Records
{
    public class Landscape : OverlandRecord
    {
        public override string Type => "Landscape";
        public float VerticalDatum { get; set; }
        public sbyte[,] Heightmap { get; } = new sbyte[65, 65];
        public SByte3[,] Normalmap { get; } = new SByte3[65, 65];
        public Byte3[,] ColourMap { get; } = new Byte3[65, 65];
        public byte[,] LowLODHeightMap { get; } = new byte[9, 9];
        public short[,] Textures { get; } = new short[16, 16];

        //https://en.uesp.net/morrow/tech/mw_esm.txt

        //A few more records missing

        public Landscape(long cellX, long cellY, IEnumerable<int> heights)
            : base($"landscape::{cellX}x{cellY}",new Long2(cellX,cellY))
        {
            if (heights.Count() != 65 * 65) throw new System.Exception($"A Cell must contain exactly {65 * 65} cells.");

            int verticalDatum = heights.First();
           VerticalDatum = verticalDatum;

            var count = 0;

            for (int x = 0; x < 65; x++)
            {
                for (int y = 0; y < 65; y++)
                {
                    var potentialSbyte = heights.ElementAt(count++);

                    // https://en.uesp.net/morrow/tech/mw_esm.txt

                    Heightmap[x, y] = (sbyte)potentialSbyte;
                    Normalmap[x, y] = new SByte3(0, 0, 1);
                    ColourMap[x, y] = new Byte3(0, 0, 0);
                }
            }

            var tempDiff = new sbyte[65, 65];

            //Solved with lots of help from https://bitbucket.org/auralgeek/morrogen LandRecord.cpp

            for (int x = 1; x < 65; x++)
            {
                tempDiff[x - 1,64] = (sbyte)(Heightmap[x,0] - Heightmap[x - 1,0]);
            }

            for (int x = 0; x < 65; x++)
            {
                for (int y = 0; y < 64; y++)
                {
                    tempDiff[x,y] = (sbyte)(Heightmap[x,y + 1] - Heightmap[x,y]);
                }
            }

            for (int x = 0; x < 65; x++)
            {
                for (int y = 0; y < 65; y++)
                {
                    Heightmap[x, y] = tempDiff[x, y];
                }
            }
        }
    }
}