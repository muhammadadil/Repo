using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Globalization;
using System.IO;
using VintriTechnologies.Dto;

namespace VintriTechnologies.Helper
{
    public static class JsonHelper
    {      

        public static bool Add(string json, string path)
        {
            if (File.Exists(path))
            {
                File.WriteAllText(path, json);
                return true;
            }
            return false;

        }
        public static string Get(string path)
        {
            if (File.Exists(path))
            {
               var result= File.ReadAllText(path);
                return result;
            }
            return null;
        }
    }
    public static class Serialize
    {
        public static string ToJson(this object self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                UnitConverter.Singleton,
                ContributedByConverter.Singleton,
                AddConverter.Singleton,
                AttributeConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
