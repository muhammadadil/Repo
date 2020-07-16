using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace VintriTechnologies.Dto
{


    public partial class Beer
        {
            [JsonProperty("id")]
            public long Id { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("tagline")]
            public string Tagline { get; set; }

            [JsonProperty("first_brewed")]
            public string FirstBrewed { get; set; }

            [JsonProperty("description")]
            public string Description { get; set; }

            [JsonProperty("image_url")]
            public Uri ImageUrl { get; set; }

            [JsonProperty("abv")]
            public double Abv { get; set; }

            [JsonProperty("ibu")]
            public double? Ibu { get; set; }

            [JsonProperty("target_fg")]
            public long TargetFg { get; set; }

            [JsonProperty("target_og")]
            public double TargetOg { get; set; }

            [JsonProperty("ebc")]
            public long? Ebc { get; set; }

            [JsonProperty("srm")]
            public double? Srm { get; set; }

            [JsonProperty("ph")]
            public double? Ph { get; set; }

            [JsonProperty("attenuation_level")]
            public double AttenuationLevel { get; set; }

            [JsonProperty("volume")]
            public BoilVolume Volume { get; set; }

            [JsonProperty("boil_volume")]
            public BoilVolume BoilVolume { get; set; }

            [JsonProperty("method")]
            public Method Method { get; set; }

            [JsonProperty("ingredients")]
            public Ingredients Ingredients { get; set; }

            [JsonProperty("food_pairing")]
            public List<string> FoodPairing { get; set; }

            [JsonProperty("brewers_tips")]
            public string BrewersTips { get; set; }

            [JsonProperty("contributed_by")]
            public ContributedBy ContributedBy { get; set; }
        }

        public partial class BoilVolume
        {
            [JsonProperty("value")]
            public double Value { get; set; }

            [JsonProperty("unit")]
            public Unit Unit { get; set; }
        }

        public partial class Ingredients
        {
            [JsonProperty("malt")]
            public List<Malt> Malt { get; set; }

            [JsonProperty("hops")]
            public List<Hop> Hops { get; set; }

            [JsonProperty("yeast")]
            public string Yeast { get; set; }
        }

        public partial class Hop
        {
            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("amount")]
            public BoilVolume Amount { get; set; }

            [JsonProperty("add")]
            public Add Add { get; set; }

            [JsonProperty("attribute")]
            public Attribute Attribute { get; set; }
        }

        public partial class Malt
        {
            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("amount")]
            public BoilVolume Amount { get; set; }
        }

        public partial class Method
        {
            [JsonProperty("mash_temp")]
            public List<MashTemp> MashTemp { get; set; }

            [JsonProperty("fermentation")]
            public Fermentation Fermentation { get; set; }

            [JsonProperty("twist")]
            public string Twist { get; set; }
        }

        public partial class Fermentation
        {
            [JsonProperty("temp")]
            public BoilVolume Temp { get; set; }
        }

        public partial class MashTemp
        {
            [JsonProperty("temp")]
            public BoilVolume Temp { get; set; }

            [JsonProperty("duration")]
            public long? Duration { get; set; }
        }

        public enum Unit { Celsius, Grams, Kilograms, Litres };

        public enum ContributedBy { AliSkinnerAliSkinner, SamMasonSamjbmason };

        public enum Add { DryHop, End, Middle, Start };

        public enum Attribute { Aroma, AttributeFlavour, Bitter, Flavour };

        public partial class Beer
        {
            public static List<Beer> FromJson(string json) => JsonConvert.DeserializeObject<List<Beer>>(json, Helper.Converter.Settings);
        }
       

        internal class UnitConverter : JsonConverter
        {
            public override bool CanConvert(Type t) => t == typeof(Unit) || t == typeof(Unit?);

            public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
            {
                if (reader.TokenType == JsonToken.Null) return null;
                var value = serializer.Deserialize<string>(reader);
                switch (value)
                {
                    case "celsius":
                        return Unit.Celsius;
                    case "grams":
                        return Unit.Grams;
                    case "kilograms":
                        return Unit.Kilograms;
                    case "litres":
                        return Unit.Litres;
                }
                throw new Exception("Cannot unmarshal type Unit");
            }

            public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
            {
                if (untypedValue == null)
                {
                    serializer.Serialize(writer, null);
                    return;
                }
                var value = (Unit)untypedValue;
                switch (value)
                {
                    case Unit.Celsius:
                        serializer.Serialize(writer, "celsius");
                        return;
                    case Unit.Grams:
                        serializer.Serialize(writer, "grams");
                        return;
                    case Unit.Kilograms:
                        serializer.Serialize(writer, "kilograms");
                        return;
                    case Unit.Litres:
                        serializer.Serialize(writer, "litres");
                        return;
                }
                throw new Exception("Cannot marshal type Unit");
            }

            public static readonly UnitConverter Singleton = new UnitConverter();
        }

        internal class ContributedByConverter : JsonConverter
        {
            public override bool CanConvert(Type t) => t == typeof(ContributedBy) || t == typeof(ContributedBy?);

            public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
            {
                if (reader.TokenType == JsonToken.Null) return null;
                var value = serializer.Deserialize<string>(reader);
                switch (value)
                {
                    case "Ali Skinner <AliSkinner>":
                        return ContributedBy.AliSkinnerAliSkinner;
                    case "Sam Mason <samjbmason>":
                        return ContributedBy.SamMasonSamjbmason;
                }
                throw new Exception("Cannot unmarshal type ContributedBy");
            }

            public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
            {
                if (untypedValue == null)
                {
                    serializer.Serialize(writer, null);
                    return;
                }
                var value = (ContributedBy)untypedValue;
                switch (value)
                {
                    case ContributedBy.AliSkinnerAliSkinner:
                        serializer.Serialize(writer, "Ali Skinner <AliSkinner>");
                        return;
                    case ContributedBy.SamMasonSamjbmason:
                        serializer.Serialize(writer, "Sam Mason <samjbmason>");
                        return;
                }
                throw new Exception("Cannot marshal type ContributedBy");
            }

            public static readonly ContributedByConverter Singleton = new ContributedByConverter();
        }

        internal class AddConverter : JsonConverter
        {
            public override bool CanConvert(Type t) => t == typeof(Add) || t == typeof(Add?);

            public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
            {
                if (reader.TokenType == JsonToken.Null) return null;
                var value = serializer.Deserialize<string>(reader);
                switch (value)
                {
                    case "dry hop":
                        return Add.DryHop;
                    case "end":
                        return Add.End;
                    case "middle":
                        return Add.Middle;
                    case "start":
                        return Add.Start;
                }
                throw new Exception("Cannot unmarshal type Add");
            }

            public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
            {
                if (untypedValue == null)
                {
                    serializer.Serialize(writer, null);
                    return;
                }
                var value = (Add)untypedValue;
                switch (value)
                {
                    case Add.DryHop:
                        serializer.Serialize(writer, "dry hop");
                        return;
                    case Add.End:
                        serializer.Serialize(writer, "end");
                        return;
                    case Add.Middle:
                        serializer.Serialize(writer, "middle");
                        return;
                    case Add.Start:
                        serializer.Serialize(writer, "start");
                        return;
                }
                throw new Exception("Cannot marshal type Add");
            }

            public static readonly AddConverter Singleton = new AddConverter();
        }

        internal class AttributeConverter : JsonConverter
        {
            public override bool CanConvert(Type t) => t == typeof(Attribute) || t == typeof(Attribute?);

            public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
            {
                if (reader.TokenType == JsonToken.Null) return null;
                var value = serializer.Deserialize<string>(reader);
                switch (value)
                {
                    case "Flavour":
                        return Attribute.AttributeFlavour;
                    case "aroma":
                        return Attribute.Aroma;
                    case "bitter":
                        return Attribute.Bitter;
                    case "flavour":
                        return Attribute.Flavour;
                }
                throw new Exception("Cannot unmarshal type Attribute");
            }

            public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
            {
                if (untypedValue == null)
                {
                    serializer.Serialize(writer, null);
                    return;
                }
                var value = (Attribute)untypedValue;
                switch (value)
                {
                    case Attribute.AttributeFlavour:
                        serializer.Serialize(writer, "Flavour");
                        return;
                    case Attribute.Aroma:
                        serializer.Serialize(writer, "aroma");
                        return;
                    case Attribute.Bitter:
                        serializer.Serialize(writer, "bitter");
                        return;
                    case Attribute.Flavour:
                        serializer.Serialize(writer, "flavour");
                        return;
                }
                throw new Exception("Cannot marshal type Attribute");
            }

            public static readonly AttributeConverter Singleton = new AttributeConverter();
        }
    }


