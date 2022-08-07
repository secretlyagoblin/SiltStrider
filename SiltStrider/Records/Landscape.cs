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

            // Below from MorrowGen - needs to be implemented 

            //  // Seems like for (i, j) j = 64 we have an differential offset vector that applies to a row at a time.
            //  // SO height(i, j) = float offset + sum( height(n, 64), n = 0..i) ) + sum( height(i, n), n = 0..j) )
            //  // So we have a 65x65 height map where row (i, 0) = float offset + sum( height(n, 64), n = 0..i )
            //  //
            //  // North is along increasing i, and so East is along increasing j.
            //  int LandRecord::convertHeightMapToDiff()
            //  {
            //      signed char temp[65][65];
            //      memset(temp, 0, 65 * 65 * sizeof(char));
            //  
            //      // point (0, 0) = float offset, soooo....
            //      this->Unknown1 = round(this->AbsHeightMap[0][0]);
            //      std::cout << "offset: " << this->Unknown1 << std::endl;
            //  
            //      // Now set the first column using the differential row offset vector...
            //      for (unsigned int i = 1; i < 65; i++)
            //      {
            //          temp[i - 1][64] = this->AbsHeightMap[i][0] - this->AbsHeightMap[i - 1][0];
            //      }
            //  
            //      // Now set all the other 64 columns using the differential row offset vector AND
            //      // each points differential summation vector.
            //      for (unsigned int i = 0; i < 65; i++)
            //      {
            //          for (unsigned int j = 0; j < 64; j++)
            //          {
            //              temp[i][j] = this->AbsHeightMap[i][j + 1] - this->AbsHeightMap[i][j];
            //          }
            //      }
            //  
            //      memcpy(&(this->DiffHeightMap), temp, 65 * 65);
            //  
            //      return 1;
            //  }



        }
    }
}