using ZenLog.Builders.BuildersAPI;
using ZenLog.Builders.BuildersController;
using ZenLog.DAL.ControllerDAL;
using ZenLog.EndPoints.Pedido;
using ZenLog.Models.ModelsController.ModelsIntegracaoController;
using ZenLog.Models.ModelsController.ModelsPedidoController;
using static ZenLog.LogDBIntegracoes;

namespace ZenLog.Servicos
{
    public class AtualizarPedido
    {
        ControllerDAL controllerDAL = new ControllerDAL();

        public async Task AtualizarPedidoExpedido(CadastroIntegracoes integracao)
        {
            if (integracao.integracoesRotinas.FirstOrDefault(x => x.DS_ROTINA == "PEDIDO" && x.X_ATUALIZAR) != null)
            {
                List<PedidoController> listaPedidoControllerEmitidos = await controllerDAL.BuscarPedidosControllerExpedidos();

                foreach (var pedido in listaPedidoControllerEmitidos)
                {
                    try
                    {
                        var pedidoAtualizar = new PedidoExpedicaoBuilderAPI().MontarPedidoExpedicao(integracao, pedido);

                        var repostaAPI = await new EndPointPedido().AtribuiNFPedidoExpedicao(integracao, pedidoAtualizar);

                        if (repostaAPI.result != null)
                        {

                            PedidoIntegracaoController pedidoIntegracaoController = new PedidoIntegracaoBuilderController().MontarPedidoIntegracao(pedido, "EXPEDIDO");

                            await controllerDAL.AtualizarPedidoIntegracaoController(pedidoIntegracaoController);

                            Globais.log.Info($"Pedido: {pedido.CD_PEDIDO} enviado os dados da nota para API com sucesso", true, TipoIntegracao.ERP, Integracao.ZENLOG, false,
                                  pedido.CD_PEDIDO, Rotina.PEDIDO, MetodoAPI.ATUALIZAR, "EXPEDIDO");
                        }
                    }
                    catch (Exception ex)
                    {
                        Globais.log.Info($"Falha ao integrar o pedido: {pedido.CD_PEDIDO}. {ex.Message}", true, TipoIntegracao.ERP, Integracao.ZENLOG, true,
                           pedido.CD_PEDIDO, Rotina.PEDIDO, MetodoAPI.ATUALIZAR);
                    }
                }
            }

        }
    }
}
