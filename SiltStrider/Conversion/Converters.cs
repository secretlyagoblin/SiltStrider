using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SiltStrider.Primitives;
using SiltStrider.Records;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace SiltStrider.Conversion
{
    public static class Converters
    {
        public static string ToYaml(this ES3Document document)
        {
            var serializer = new SerializerBuilder()
                .Build();

            return serializer.Serialize(document.ToSerialisationDocument());
        }

        public static object ToSerialisationDocument(this ES3Document document)
        {
            return new
            {
                __esp_version__ = 1.3,
                __plugin_type__ = 1,
                transpiler_version = "0.15.0",
                author = "",
                desc = "",
                masters = new[]
                {
                    new {name = "Morrowind.esm", size = 79837557},
                    new {name = "Bloodmoon.esm", size = 9631798},
                    new {name = "Tribunal.esm", size = 4565686}
                },
                records = document.Records.ToDictionary(x => x.Name, x => x.Serialise())
            };
        }

        public static object Serialise(this Record record)
        {
            if (record is LandscapeRecord l) return l.ToSerialisationRecord();

            throw new NotImplementedException();
        }

        public static object ToSerialisationRecord(this LandscapeRecord landscapeRecord)
        {
            var heightmap = new byte[4232];
            landscapeRecord.VerticalDatum.ToBytes().CopyTo(heightmap, 0);
            heightmap[4] = 0x00;
            landscapeRecord.Heightmap.ToBytes().CopyTo(heightmap, 5);

            return new
            {
                type = "Landscape",
                height_map = heightmap.ToBase64(), //Nope, needs more
                normal_map = landscapeRecord.Normalmap.ToBytes().ToBase64()
            };
        }

        public static string ToBase64(this byte[] bytes)
        {
            return Convert.ToBase64String(bytes);
        }

        public static byte[] ToBytes<T>(this T[,] array)
        {
            if (array is null | array.Length == 0)
            {
                throw new ArgumentNullException(nameof(array));
            }

            var byteLength = array[0, 0].ByteLength();

            var bytes = new byte[array.Length * byteLength];

            var count = 0;

            for (int x = 0; x < array.GetLength(0); x++)
            {
                for (int y = 0; y < array.GetLength(1); y++)
                {
                    array[x, y].ToBytes().CopyTo(bytes, count);

                    count += byteLength;
                }
            }

            return bytes;
        }

        public static int ByteLength<T>(this T item)
        {
            if (item is byte) return 1;
            if (item is sbyte) return 1;
            if (item is Byte3) return 3;
            if (item is SByte3) return 3;
            throw new NotImplementedException(typeof(T).ToString());
        }

        public static byte[] ToBytes<T>(this T item)
        {
            if (item is byte b) return new byte[] { b };
            if (item is sbyte sb) return new byte[] { (byte)sb };
            if (item is Byte3 b3) return new byte[] { b3.X,b3.Y,b3.Z };
            if (item is SByte3 sb3) return new byte[] { (byte)sb3.X, (byte)sb3.Y, (byte)sb3.Z };
            if (item is float f) return BitConverter.GetBytes(f);
            throw new NotImplementedException(typeof(T).ToString());
        }


    }
}
