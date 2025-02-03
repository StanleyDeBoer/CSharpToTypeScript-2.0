using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace CSharpToTypeScript.CLITool.Utilities
{
    public static class ConfigurationFile
    {
        private static JsonSerializerSettings JsonSerializerSettings => new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            Formatting = Formatting.Indented,
            Converters = new[] { new StringEnumConverter(new CamelCaseNamingStrategy()) }
        };

        public static Configuration Load(string fileName)
            => File.Exists(fileName)
            ? JsonConvert.DeserializeObject<Configuration>(File.ReadAllText(fileName), JsonSerializerSettings)
            : null;

        public static void Create(string fileName, Configuration configuration)
            => File.WriteAllText(fileName, JsonConvert.SerializeObject(configuration, JsonSerializerSettings));
    }
}