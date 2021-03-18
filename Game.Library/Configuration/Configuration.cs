using MonoGame.Framework.Utilities.Deflate;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GameLibrary.Config.App
{
    public class ConfigurationData
    {
        private List<JsonDocument> ConfigData;
        private JsonDocument JsonConfigs;
        public JsonSerializerOptions serializerOptions { get; }


        internal ConfigurationData(Dictionary<string, JsonDocument> configData, List<JsonConverter> converters)
        {
            ConfigData = new List<JsonDocument>(configData.Select(a=>a.Value));
            JsonConfigs = CreateJsonConfig(configData);
            //File.WriteAllText("Wilma.json",data.RootElement.GetRawText());
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

               var rawData =  Encoding.UTF8.GetString(stream.ToArray());
                stream.Position = 0;
                return JsonDocument.Parse(stream);
                
            }
        }

        public string Get(string propertyName)
        {
            if (propertyName.Contains(":"))
                return ExperimentalGet(propertyName).ToString();

            foreach (var root in this.ConfigData)
            {
                if (root.RootElement.TryGetProperty(propertyName, out var kim))
                    return kim.ToString();
            }
            return "";

        }

        public JsonElement ExperimentalGet(string Path)
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
            if (!assigned) throw new NullReferenceException("Cannot not find path propert");

            // iterate the paths list
            foreach (var path in allPaths.Where((a, idx) => idx > 0))
                if (jsonElement.TryGetProperty(path, out jsonElement))
                    break;

            return jsonElement;
        }

        public T Get<T>(string propertyName, Func<string, T> mapFunc = null) where T : class
        {
            if (propertyName.Contains(":"))
            {
                var map = mapFunc ?? new Func<string, T>((str) => JsonSerializer.Deserialize<T>(ExperimentalGet(propertyName).GetRawText(), serializerOptions));
                return map(propertyName);
            }

            foreach (var root in this.JsonConfigs.RootElement.EnumerateObject())
            {
                if (root.Value.TryGetProperty(propertyName, out var matchedObject))
                {
                    var map = mapFunc ?? new Func<string, T>((str) => JsonSerializer.Deserialize<T>(matchedObject.GetProperty(propertyName).GetRawText(), serializerOptions));
                    return map(propertyName);
                }
            }
            throw new NullReferenceException("Could not match property");
        }

        // Simple Case leaning on Json.net
        public T Get<T>(string propertyName) where T : class
        {
            JsonElement property = new JsonElement();
            if (propertyName.Contains(":"))
            {
                property = ExperimentalGet(propertyName);
            }
            else
                foreach (var root in this.JsonConfigs.RootElement.EnumerateObject())
                {
                    if (root.Value.TryGetProperty(propertyName, out property))
                        break;
                }

            if (property.Equals(default(JsonElement)))
                throw new NullReferenceException("Could not match property");
            return JsonSerializer.Deserialize<T>(property.ToString(), serializerOptions);



        }

        // Simple Case leaning on Json.net
        //With a Custom Converter
        public T Get<T, C>(string propertyName) where T : class
                                                where C : JsonConverter<T>, new()
        {
            JsonElement property = new JsonElement();
            foreach (var root in this.JsonConfigs.RootElement.EnumerateObject())
            {
                if (root.Value.TryGetProperty(propertyName, out property))
                    break;
            }

            var opts = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new C() }
            };
            if (property.Equals(default(JsonElement)))
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
        private List<JsonDocument> LoadedData = new List<JsonDocument>();

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
                foreach(var file in fileNames)
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
