using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace OpenAI.Extensions
{
    /// <summary>
    /// https://github.com/dotnet/runtime/issues/74385#issuecomment-1456725149
    /// </summary>
    internal sealed class JsonStringEnumConverterFactory
    {
        public bool CanConvert(Type typeToConvert)
        { return typeToConvert.IsEnum; }

        public JsonConverter CreateConverter(Type typeToConvert, JsonSerializerSettings options)
        { return (JsonConverter)Activator.CreateInstance(typeof(JsonStringEnumConverter<>).MakeGenericType(typeToConvert)); }
    }
}