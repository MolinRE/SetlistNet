using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SetlistNet.JsonConverters;

public class DateTimeConverter : JsonConverter<DateTime>
{
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var token = reader.GetString();
        if (string.IsNullOrEmpty(token))
        {
            return DateTime.MinValue;
        }

        if (token.Length == 10)
        {
            return DateTime.ParseExact(token, "dd-MM-yyyy", DateTimeFormatInfo.InvariantInfo);
        }
        
        return DateTime.Parse(token);
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}