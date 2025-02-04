using System;
using System.Diagnostics;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SetlistNet;

public class DateTimeConverterUsingDateTimeParse : JsonConverter<DateTime>
{
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        Debug.Assert(typeToConvert == typeof(DateTime));
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