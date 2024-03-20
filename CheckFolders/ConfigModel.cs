using System.Text.Json.Serialization;


namespace CheckFolders
{
    public class ConfigModel
    {
        [JsonPropertyName("path")]
        public string Path { get; set; }

        public ConfigModel(string path)
        {
            Path = path;
        }

        public ConfigModel()
        {
        }
    }
}
