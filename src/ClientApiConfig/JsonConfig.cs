using System.Text.Json;

namespace ClientApiConfig
{
    public static class JsonConfig
    {
        public static JsonSerializerOptions DefaultSerializerOptions { get; private set; }

        static JsonConfig()
        {
            DefaultSerializerOptions = new JsonSerializerOptions();
            DefaultSerializerOptions.PropertyNameCaseInsensitive = true;
        }
    }
}
