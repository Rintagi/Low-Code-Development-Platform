using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace OpenAI.Extensions
{
    internal sealed class SnakeCaseNamingPolicy : NamingStrategy 
    {
        protected override string ResolvePropertyName(string name)
        { return StringExtensions.ToSnakeCase(name); }
    }
}