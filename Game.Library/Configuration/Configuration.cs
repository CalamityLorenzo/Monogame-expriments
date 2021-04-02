using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GameLibrary.Config.App
{
    public class ConfigurationData
    {
        private JsonDocument JsonConfigs;
        public JsonSerializerOptions serializerOptions { get; }


        internal ConfigurationData(Dictionary<string, JsonDocument> configData, List<JsonConverter> converters)
        {
            JsonConfigs = CreateJsonConfig(configData);
            this.serializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            converters.ForEach(a => serializerOptions.Converters.Add(a));
        }

        private JsonDocument CreateJsonConfig(Dictionary<string, JsonDocument> configs)
        {
            using (var stream = new MemoryStream())
            {
                var utf8Memwriter = new Utf8JsonWriter(stream);

                utf8Memwriter.WriteStartObject();

                foreach (var config in configs)
                {
                    utf8Memwriter.WritePropertyName(config.Key);
                    config.Value.RootElement.WriteTo(utf8Memwriter);
                }

                utf8Memwriter.WriteEndObject();
                utf8Memwriter.Flush();

                stream.Position = 0;
                return JsonDocument.Parse(stream);

            }
        }

        private string QueryJsonConfig(string propertyName)
        {
            foreach (var rootEle in this.JsonConfigs.RootElement.EnumerateObject())
            {
                var assigned = rootEle.Value.TryGetProperty(propertyName, out var jsonElement);
                if (assigned) return jsonElement.ToString();

            }

            throw new NullReferenceException("Cannot not find path propert");

            //            return default(JsonElement);
        }

        public string Get(string propertyName)
        {
            if (propertyName.Contains(":"))
                return RichPathQueryJsonConfig(propertyName).ToString();

            return QueryJsonConfig(propertyName);

        }

        public JsonElement RichPathQueryJsonConfig(string Path)
        {
            var allPaths = Path.Split(":", StringSplitOptions.RemoveEmptyEntries);

            // Get the first value, cos that get us the document

            JsonElement jsonElement = new JsonElement();
            bool assigned = false;
            foreach (var rootEle in this.JsonConfigs.RootElement.EnumerateObject())
            {
                assigned = rootEle.Value.TryGetProperty(allPaths.First(), out jsonElement);
                if (assigned)
                    break;
            }
            if (!assigned) throw new NullReferenceException("Cannot not find path property");

            // iterate the paths list
            foreach (var path in allPaths.Where((a, idx) => idx > 0))
                if (jsonElement.TryGetProperty(path, out jsonElement))
                    break;

            return jsonElement;
        }

        public T Get<T>(string propertyName, Func<string, T> mapFunc) 
        {
            var rawData = "";
            var map = mapFunc ?? new Func<string, T>((rawStr) => JsonSerializer.Deserialize<T>(rawStr, serializerOptions));

            if (propertyName.Contains(":"))
            {
                rawData = RichPathQueryJsonConfig(propertyName).GetRawText();
            }
            else
                rawData = QueryJsonConfig(propertyName);
            if (String.IsNullOrEmpty(rawData)) throw new NullReferenceException("Could not match property");
            return map(rawData);

        }

        // Simple Case leaning on Json.net
        public T Get<T>(string propertyName)
        {
            string property = "";
            if (propertyName.Contains(":"))
            {
                property = RichPathQueryJsonConfig(propertyName).GetRawText() ?? null;
            }
            else
                property = QueryJsonConfig(propertyName);

            if(string.IsNullOrEmpty(property))
                throw new NullReferenceException("Could not match property"); ;
            return JsonSerializer.Deserialize<T>(property, serializerOptions);

        }

        // Simple Case leaning on Json.net
        //With a Custom Converter
        public T Get<T, C>(string propertyName) where T : class
                                                where C : JsonConverter<T>, new()
        {
            string property = "";
            if (propertyName.Contains(":"))
            {
                property = RichPathQueryJsonConfig(propertyName).GetRawText() ?? null;
            }
            else
                property = QueryJsonConfig(propertyName);

            var opts = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new C() }
            };
            if (string.IsNullOrEmpty(property))
                throw new NullReferenceException("Could not match property");

            return JsonSerializer.Deserialize<T>(property.ToString(), opts);
        }
    }

    public class ConfigurationBuilder
    {
        private static Lazy<ConfigurationBuilder> configInstance = new Lazy<ConfigurationBuilder>(() => new ConfigurationBuilder());
        private ConfigurationBuilder() { }
        private bool BuildComplete = false;
        public static ConfigurationBuilder Manager => configInstance.Value;
        private List<JsonConverter> Converters = new List<JsonConverter>();
        private HashSet<string> fileNames = new HashSet<string>();

        public ConfigurationBuilder LoadJsonFile(string jsonfilePath)
        {
            fileNames.Add(jsonfilePath);
            return this;
        }

        public ConfigurationBuilder AddJsonConverter<T>(T converter) where T : JsonConverter
        {
            this.Converters.Add(converter);
            return this;
        }

        public ConfigurationData Build()
        {
            if (!BuildComplete)
            {

                var jsonDict = new Dictionary<string, JsonDocument>();
                foreach (var file in fileNames)
                {
                    var fileName = Path.GetFileNameWithoutExtension(file);
                    var doc = JsonDocument.Parse(File.ReadAllText(file));
                    var isNotUnique = jsonDict.ContainsKey(fileName);
                    var counter = 1;
                    while (isNotUnique == true)
                    {
                        var fileNameTest = $"{fileName}_{counter}";
                        isNotUnique = jsonDict.ContainsKey(fileNameTest);
                        if (isNotUnique == false) fileName = fileNameTest;
                    }
                    jsonDict.Add(fileName, doc);

                }

                BuildComplete = true;
                return new ConfigurationData(jsonDict, Converters);
            }
            throw new ArgumentOutOfRangeException("Build has already been completed.");
        }

    }
}
