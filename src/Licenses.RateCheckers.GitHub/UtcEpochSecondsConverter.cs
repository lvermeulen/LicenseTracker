using System;
using Newtonsoft.Json;

namespace Licenses.RateCheckers.GitHub
{
    public class UtcEpochSecondsConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DateTime);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            long t;

            if (reader.Value?.GetType() != typeof(long))
            {
                // ReSharper disable once AssignNullToNotNullAttribute
                t = long.Parse((string)reader.Value);
            }
            else
            {
                t = (long)reader.Value;
            }

            return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(t).ToLocalTime();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
