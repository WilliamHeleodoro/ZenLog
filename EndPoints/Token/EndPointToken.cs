
using ZenLog.Models.ModelsAPI;
using ZenLog.Models.ModelsAPI.ModelsRetornoAPI;
using ZenLog.Models.ModelsController.ModelsIntegracaoController;
using static ZenLog.EndPoints.EndPointBase;
using WebRequestParamaters = ZenLog.EndPoints.EndPointBase.WebRequestParamaters;

namespace ZenLog.EndPoints.Token
{
    public class EndPointToken
    {
        public async Task<string> Token(CadastroIntegracoes integracao)
        {
            try
            {
                LoginAPI login = new LoginAPI
                {
                    userNameOrEmailAddress = integracao.DS_USUARIO,
                    password = integracao.DS_SENHA,
                    personalAccessToken = integracao.DS_KEY,
                    tenantName = "horus"
                };

                WebRequestParamaters webRequestParamaters = new WebRequestParamaters()
                {
                    URL = $"{integracao.DS_URL}/api/TokenAuth/AuthenticatePAT",
                    ContentType = "application/json",
                    Method = "POST",
                    Headers = new Dictionary<string, string>(),
                    ObjetoEnviar = login,
                };
                
                var respostaAPI = await ExecutarRequest<RespostaAPI<RetornoToken>>(webRequestParamaters);

                if (respostaAPI.success == false)
                    throw new Exception();

                return !string.IsNullOrEmpty(respostaAPI.result?.accessToken) ? respostaAPI.result.accessToken : "";
            }
            catch (Exception ex)
            {
                throw new Exception($"Não foi Possível obter o token. Verifique se os dados do login nas configurações da integração estão corretos ou se a API está funcionando corretamente");
            }
        }
    }
}
