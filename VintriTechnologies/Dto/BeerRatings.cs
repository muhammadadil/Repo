using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using VintriTechnologies.Helper;

namespace VintriTechnologies.Dto
{

    public partial class BeerRatings
        {
            [JsonProperty("id")]
            [JsonConverter(typeof(ParseStringConverter))]
            public long Id { get; set; }          

            [JsonProperty("userRatings")]
            public List<UserRating> UserRatings { get; set; }
        }

        public partial class UserRating
        {
            [JsonProperty("username")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Email address is not valid.")]
        public string Username { get; set; }

        [Range(1, 5)]
        [JsonProperty("rating")]           
        public long Rating { get; set; }

            [JsonProperty("comments")]
            public string Comments { get; set; }
        }

        public partial class BeerRatings
        {
            public static List<BeerRatings> FromJson(string json) => JsonConvert.DeserializeObject<List<BeerRatings>>(json, Converter.Settings);
        }  

        internal class ParseStringConverter : JsonConverter
        {
            public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

            public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
            {
                if (reader.TokenType == JsonToken.Null) return null;
                var value = serializer.Deserialize<string>(reader);
                long l;
                if (Int64.TryParse(value, out l))
                {
                    return l;
                }
                throw new Exception("Cannot unmarshal type long");
            }

            public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
            {
                if (untypedValue == null)
                {
                    serializer.Serialize(writer, null);
                    return;
                }
                var value = (long)untypedValue;
                serializer.Serialize(writer, value.ToString());
                return;
            }

            public static readonly ParseStringConverter Singleton = new ParseStringConverter();
        }
 

}

