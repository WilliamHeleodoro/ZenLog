using ZenLog.Models.ModelsAPI.ModelsEnvioAPI.ModelsPedidoEnvioAPI;
using ZenLog.Models.ModelsAPI.ModelsRetornoAPI;
using WebRequestParamaters = ZenLog.EndPoints.EndPointBase.WebRequestParamaters;
using static ZenLog.EndPoints.EndPointBase;
using ZenLog.Models.ModelsController.ModelsIntegracaoController;

namespace ZenLog.EndPoints.Pedido
{
    public class EndPointPedido
    {
        public async Task<RespostaAPI<PedidoEnvioAPI>> CadastrarPedido(CadastroIntegracoes integracao, PedidoEnvioAPI pedidoEnvioAPI)
        {
            try
            {
                WebRequestParamaters webRequestParamaters = new WebRequestParamaters()
                {
                    URL = $"{integracao.DS_URL}/api/services/app/LayoutUnificadoPedidoExpedicao/Create",
                    ContentType = "application/json",
                    Method = "POST",
                    Headers = new Dictionary<string, string>(),
                    ObjetoEnviar = pedidoEnvioAPI
                };

                webRequestParamaters.Headers.Add("Authorization", $"Bearer {integracao.DS_TOKEN}");

                var respostaAPI = await ExecutarRequest<RespostaAPI<PedidoEnvioAPI>>(webRequestParamaters);

                if (respostaAPI.success == false)
                    throw new Exception($@"{respostaAPI.error?.message}. {respostaAPI.error?.details}");

                return respostaAPI;
            }
            catch (Exception ex)
            {
                throw new Exception($"Não foi Possível cadastrar o lancamento na API. {ex.Message}");
            }
        }

        public async Task<RespostaAPI<RetornoPedidoExpedicao>> BuscarPedidosExpedicao(CadastroIntegracoes integracao)
        {
            try
            {
                WebRequestParamaters webRequestParamaters = new WebRequestParamaters()
                {
                    URL = $"{integracao.DS_URL}/api/services/app/Expedicao/GetAll",
                    ContentType = "application/json",
                    Method = "GET",
                    Headers = new Dictionary<string, string>(),
                };

                webRequestParamaters.Headers.Add("Authorization", $"Bearer {integracao.DS_TOKEN}");

                var respostaAPI = await ExecutarRequest<RespostaAPI<RetornoPedidoExpedicao>>(webRequestParamaters);

                if (respostaAPI.success == false)
                    throw new Exception($@"{respostaAPI.error?.message}. {respostaAPI.error?.details}");

                return respostaAPI;
            }
            catch (Exception ex)
            {
                throw new Exception($"Não foi buscar os pedidos em expedição da API. {ex.Message}");
            }
        }

        public async Task<RespostaAPI<PedidoExpedicaoAPI>> AtribuiNFPedidoExpedicao(CadastroIntegracoes integracao, PedidoExpedicaoAPI pedidoExpedicaoAPI)
        {
            try
            {
                WebRequestParamaters webRequestParamaters = new WebRequestParamaters()
                {
                    URL = $"{integracao.DS_URL}/api/services/app/LayoutUnificadoPedidoExpedicao/AtribuiNfPedidoExpedicao",
                    ContentType = "application/json",
                    Method = "POST",
                    Headers = new Dictionary<string, string>(),
                    ObjetoEnviar = pedidoExpedicaoAPI
                };

                webRequestParamaters.Headers.Add("Authorization", $"Bearer {integracao.DS_TOKEN}");

                var respostaAPI = await ExecutarRequest<RespostaAPI<PedidoExpedicaoAPI>>(webRequestParamaters);

                if (respostaAPI.success == false)
                    throw new Exception($@"{respostaAPI.error?.message}. {respostaAPI.error?.details}");

                return respostaAPI;
            }
            catch (Exception ex)
            {
                throw new Exception($"Não foi possível enviar os dados da nota para API. {ex.Message}");
            }
        }
    }
}
