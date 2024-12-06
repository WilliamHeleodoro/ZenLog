using WebRequestParamaters = ZenLog.EndPoints.EndPointBase.WebRequestParamaters;
using static ZenLog.EndPoints.EndPointBase;
using ZenLog.Models.ModelsAPI.ModelsRetornoAPI;
using ZenLog.Models.ModelsAPI.ModelsEnvioAPI.ModelsEntradaEnvioAPI;
using ZenLog.Models.ModelsController.ModelsIntegracaoController;

namespace ZenLog.EndPoints.Entrada
{
    public class EndPointEntrada
    {
        public async Task<RespostaAPI<EntradaEnvioAPI>> CadastrarEntrada(CadastroIntegracoes integracao, EntradaEnvioAPI entradaEnvioAPI)
        {
            try
            {
                WebRequestParamaters webRequestParamaters = new WebRequestParamaters()
                {
                    URL = $"{integracao.DS_URL}/api/services/app/LayoutUnificadoNotaArmazenagem/Create",
                    ContentType = "application/json",
                    Method = "POST",
                    Headers = new Dictionary<string, string>(),
                    ObjetoEnviar = entradaEnvioAPI
                };

                webRequestParamaters.Headers.Add("Authorization", $"Bearer {integracao.DS_TOKEN}");

                var respostaAPI = await ExecutarRequest<RespostaAPI<EntradaEnvioAPI>>(webRequestParamaters);

                if (respostaAPI.success == false)
                    throw new Exception($@"{respostaAPI.error?.message}. {respostaAPI.error?.details}");

                return respostaAPI;
            }
            catch (Exception ex)
            {
                throw new Exception($"Não foi Possível cadastrar a entrada na API. {ex.Message}");
            }
        }
    }
}
