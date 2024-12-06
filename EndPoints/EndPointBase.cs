using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZenLog.Models.ModelsAPI.ModelsRetornoAPI;

namespace ZenLog.EndPoints
{
    public class EndPointBase
    {
        public class WebRequestParamaters
        {
            public required string URL { get; set; }
            public string Method { get; set; } = "POST";
            public string ContentType { get; set; } = "application/json";

            public Dictionary<string, string>? Headers { get; set; }

            public object? ObjetoEnviar { get; set; } = null;
            public bool IgnorarNullValues { get; set; } = false;

        }

        public static async Task<T> ExecutarRequest<T>(WebRequestParamaters parametros)
        {
            using (var client = new HttpClient())
            {
                // Configuração de Headers
                if (parametros.Headers != null)
                {
                    foreach (var header in parametros.Headers)
                    {
                        client.DefaultRequestHeaders.Add(header.Key, header.Value);
                    }
                }

                HttpResponseMessage responseMessage;
                string responseContent;

                try
                {
                    // Escolha o método HTTP
                    if (parametros.Method == "GET")
                    {
                        responseMessage = await client.GetAsync(parametros.URL);
                    }
                    else if (parametros.Method == "POST")
                    {
                        // Constrói o conteúdo JSON para a requisição POST
                        var jsonContent = JsonConvert.SerializeObject(parametros.ObjetoEnviar, Formatting.None,
                            new JsonSerializerSettings { NullValueHandling = parametros.IgnorarNullValues ? NullValueHandling.Ignore : NullValueHandling.Include });

                        var content = new StringContent(jsonContent, Encoding.UTF8, parametros.ContentType);
                        responseMessage = await client.PostAsync(parametros.URL, content);
                    }
                    else if (parametros.Method == "PUT")
                    {
                        // Constrói o conteúdo JSON para a requisição POST
                        var jsonContent = JsonConvert.SerializeObject(parametros.ObjetoEnviar, Formatting.None,
                            new JsonSerializerSettings { NullValueHandling = parametros.IgnorarNullValues ? NullValueHandling.Ignore : NullValueHandling.Include });

                        var content = new StringContent(jsonContent, Encoding.UTF8, parametros.ContentType);
                        responseMessage = await client.PutAsync(parametros.URL, content);
                    }
                    else
                    {
                        throw new NotSupportedException($"Método HTTP '{parametros.Method}' não é suportado.");
                    }

                    // Lê a resposta da requisição
                    responseContent = await responseMessage.Content.ReadAsStringAsync();

                    var settings = new JsonSerializerSettings
                    {
                        MissingMemberHandling = MissingMemberHandling.Ignore, // Ignorar membros ausentes
                        NullValueHandling = NullValueHandling.Ignore // Ignorar valores nulos
                    };

                    return JsonConvert.DeserializeObject<T>(responseContent, settings);


                }
                catch (HttpRequestException ex)
                {
                    throw new Exception($"Erro ao executar requisição: {ex.Message}", ex);
                }
            }
        }
    }
}
