using ZenLog.Models.ModelsAPI.ModelsMaterialEnvioAPI.MaterialEnvioAPI;
using ZenLog.Models.ModelsAPI.ModelsRetornoAPI;
using ZenLog.Models.ModelsController.ModelsIntegracaoController;
using static ZenLog.EndPoints.EndPointBase;
using WebRequestParamaters = ZenLog.EndPoints.EndPointBase.WebRequestParamaters;

namespace ZenLog.EndPoints.Material
{
    public class EndPointMaterial
    {
        public async Task<RespostaAPI<MaterialEnvioAPI>> CadastrarMaterial(CadastroIntegracoes integracao, MaterialEnvioAPI materialEnvioAPI)
        {
            try
            {
                WebRequestParamaters webRequestParamaters = new WebRequestParamaters()
                {
                    URL = $"{integracao.DS_URL}/api/services/app/LayoutUnificadoCadastroItens/Create",
                    ContentType = "application/json",
                    Method = "POST",
                    Headers = new Dictionary<string, string>(),
                    ObjetoEnviar = materialEnvioAPI,
                };

                webRequestParamaters.Headers.Add("Authorization", $"Bearer {integracao.DS_TOKEN}");

                var respostaAPI = await ExecutarRequest<RespostaAPI<MaterialEnvioAPI>>(webRequestParamaters);

                if (respostaAPI.success == false)
                    throw new Exception($@"{respostaAPI.error?.message}. {respostaAPI.error?.details}");

                return respostaAPI;
            }
            catch (Exception ex)
            {
                throw new Exception($"Não foi possível cadastrar o material na API. {ex.Message}");
            }
        }

        public async Task<RespostaAPI<MaterialEnvioAPI>> AtualizarMaterial(CadastroIntegracoes integracao, MaterialEnvioAPI materialEnvioAPI)
        {
            try
            {
                WebRequestParamaters webRequestParamaters = new WebRequestParamaters()
                {
                    URL = $"{integracao.DS_URL}/api/services/app/LayoutUnificadoCadastroItens/Update",
                    ContentType = "application/json",
                    Method = "PUT",
                    Headers = new Dictionary<string, string>(),
                    ObjetoEnviar = materialEnvioAPI,
                };

                webRequestParamaters.Headers.Add("Authorization", $"Bearer {integracao.DS_TOKEN}");

                var repostaAPI = await ExecutarRequest<RespostaAPI<MaterialEnvioAPI>>(webRequestParamaters);

                if (repostaAPI.success == false)
                    throw new Exception($@"{repostaAPI.error?.message}. {repostaAPI.error?.details}");

                return repostaAPI;
            }
            catch (Exception ex)
            {
                throw new Exception($"Não foi possível atualizar o material na API. {ex.Message}");
            }
        }
    }
}
