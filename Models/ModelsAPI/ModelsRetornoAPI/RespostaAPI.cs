
using Newtonsoft.Json;

namespace ZenLog.Models.ModelsAPI.ModelsRetornoAPI
{
    public class RespostaAPI<T>
    {
        public required Error error { get; set; }
        public required T result { get; set; }
        public required bool success { get; set; }
    }

    public class Error 
    {
        public string? code { get; set; }
        public string? details { get; set; }
        public string? message { get; set; }
    }

}
