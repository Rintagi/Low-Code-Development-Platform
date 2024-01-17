using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace OpenAI.Extensions
{
    internal sealed class JsonStringEnumConverter<TEnum> : JsonConverter where TEnum : struct
    {
        private readonly NamingStrategy _namingStrategy;
        private readonly Dictionary<int, TEnum> _numberToEnum = new Dictionary<int, TEnum>();
        private readonly Dictionary<TEnum, string> _enumToString = new Dictionary<TEnum, string>();
        private readonly Dictionary<string, TEnum> _stringToEnum = new Dictionary<string, TEnum>();

        public JsonStringEnumConverter()
        {
            _namingStrategy = new SnakeCaseNamingStrategy();
            var type = typeof(TEnum);

            if (!type.IsEnum)
            {
                throw new InvalidOperationException(string.Format("Type parameter TEnum must be an enumeration."));
            }

            foreach (var value in Enum.GetValues(typeof(TEnum)).Cast<TEnum>())
            {
                var enumMember = type.GetMember(value.ToString())[0];
                var attribute = enumMember.GetCustomAttribute<EnumMemberAttribute>();
                var index = Convert.ToInt32(Enum.Parse(type, value.ToString()));

                if (attribute != null && attribute.Value != null)
                {
                    _numberToEnum.Add(index, value);
                    _enumToString.Add(value, attribute.Value);
                    _stringToEnum.Add(attribute.Value, value);
                }
                else
                {
                    var convertedName = _namingStrategy.GetPropertyName(value.ToString(), false);
                    _numberToEnum.Add(index, value);
                    _enumToString.Add(value, convertedName);
                    _stringToEnum.Add(convertedName, value);
                }
            }
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(TEnum);
        }
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.String)
            {
                var stringValue = reader.Value.ToString();

                if (stringValue != null)
                {
                    var value = _namingStrategy.GetPropertyName(stringValue, false);
                    TEnum enumValue = default(TEnum);
                    if (_stringToEnum.TryGetValue(value, out enumValue))
                    {
                        return enumValue;
                    }
                }
            }
            else if (reader.TokenType == JsonToken.Integer)
            {
                var numValue = Convert.ToInt32(reader.Value);
                TEnum enumValue = default(TEnum);
                if (_numberToEnum.TryGetValue(numValue, out enumValue))
                {
                    return enumValue;
                }
            }

            throw new JsonSerializationException(string.Format("Unable to deserialize enum value for {0}", objectType));
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            string stringValue;
            if (value is TEnum && _enumToString.TryGetValue((TEnum)value, out stringValue))
            {
                writer.WriteValue(stringValue);
            }
            else
            {
                throw new JsonSerializationException(string.Format("Unable to serialize enum value {0} of type {1}", value, typeof(TEnum)));
            }
        }

    }
}