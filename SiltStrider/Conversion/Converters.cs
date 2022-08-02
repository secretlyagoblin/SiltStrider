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
                records = document.Records.ToDictionary(x => $"{x.Name}", x => x.Serialise())
            };
        }

        public static object Serialise(this Record record)
        {
            if (record is Landscape l) return l.ToSerialisationRecord();
            if (record is Cell c) return c.ToSerialisationRecord();
            if (record is Instance i) return i.ToSerialisationRecord();

            throw new NotImplementedException();
        }
        public static object ToSerialisationRecord(this Cell cell)
        {
            return new
            {
                type = "Cell",
                region = cell.Region.ToFormattedText(),
                location = new { x = cell.Location.X, y = cell.Location.Y },
                traits = new[] {"HasWater"},
                references = cell.References.ToDictionary(
                    x => x.Name, 
                    x => x.Serialise())
            };
        }

        public static object ToSerialisationRecord(this Instance instance)
        {
            return new
            {
                object_id = instance.Block,
                scale = instance.Scale,
                reference_blocked = false,
                position = new { 
                    position = new[] {
                        instance.Position.X,
                        instance.Position.Y,
                        instance.Position.Z},
                    rotation = new[] {
                        instance.Rotation.X,
                        instance.Rotation.Y,
                        instance.Rotation.Z},
                }
            };
        }

        public static object ToSerialisationRecord(this Landscape landscape)
        {
            var heightmap = new byte[4232];
            landscape.VerticalDatum.ToBytes().CopyTo(heightmap, 0);
            heightmap[4] = 0x00;
            landscape.Heightmap.ToBytes().CopyTo(heightmap, 5);

            return new
            {
                type = "Landscape",
                traits = new[] { "Unknown1", "Unknown2" }, // ??
                height_map = heightmap.ToBase64(), 
                normal_map = landscape.Normalmap.ToBytes().ToBase64(),
                low_lod_height_map = landscape.LowLODHeightMap.ToBytes().ToBase64(),
                textures = landscape.Textures.ToBytes().ToBase64(),
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

        internal static int ByteLength<T>(this T item)
        {
            if (item is byte) return 1;
            if (item is sbyte) return 1;
            if (item is Byte3) return 3;
            if (item is SByte3) return 3;
            if (item is short) return 2;
            throw new NotImplementedException(typeof(T).ToString());
        }

        internal static byte[] ToBytes<T>(this T item)
        {
            if (item is byte b) return new byte[] { b };
            if (item is sbyte sb) return new byte[] { (byte)sb };
            if (item is Byte3 b3) return new byte[] { b3.X,b3.Y,b3.Z };
            if (item is SByte3 sb3) return new byte[] { (byte)sb3.X, (byte)sb3.Y, (byte)sb3.Z };
            if (item is float f) return BitConverter.GetBytes(f);
            if (item is short s) return BitConverter.GetBytes(s);
            throw new NotImplementedException(typeof(T).ToString());
        }

        internal static string ToFormattedText(this Region value)
        {
            var stringVal = value.ToString();
            var bld = new StringBuilder();

            for (var i = 0; i < stringVal.Length; i++)
            {
                if (char.IsUpper(stringVal[i]))
                {
                    bld.Append(" ");
                }

                bld.Append(stringVal[i]);
            }

            return bld.ToString().Trim();
        }


    }
}
