using ZenLog.Builders.BuildersAPI;
using ZenLog.Builders.BuildersController;
using ZenLog.DAL.ControllerDAL;
using ZenLog.EndPoints.Pedido;
using ZenLog.Models.ModelsController.ModelsIntegracaoController;
using ZenLog.Models.ModelsController.ModelsPedidoController;
using static ZenLog.LogDBIntegracoes;

namespace ZenLog.Servicos
{
    public class EnviarPedido
    {
        ControllerDAL controllerDAL = new ControllerDAL();

        public async Task IntegrarPedido(CadastroIntegracoes integracao, PedidoController pedidoController)
        {
            if (integracao.integracoesRotinas.FirstOrDefault(x => x.DS_ROTINA == "PEDIDO" && x.X_CADASTRAR) != null)
            {
                try
                {
                    var pedidoEnviar = new PedidoBuilderAPI().MontarPedidoEnvioAPI(integracao, pedidoController);

                    var resultadoEnvioPedido = await new EndPointPedido().CadastrarPedido(integracao, pedidoEnviar);

                    if (resultadoEnvioPedido.result != null)
                    {

                        PedidoIntegracaoController pedidoIntegracaoController = new PedidoIntegracaoBuilderController().MontarPedidoIntegracao(pedidoController);
                        
                        await controllerDAL.AtualizarPedidoIntegracaoController(pedidoIntegracaoController);

                        Globais.log.Info($"Pedido: {pedidoController.CD_PEDIDO} cadastrado na API com sucesso", true, TipoIntegracao.ERP, Integracao.ZENLOG, false,
                              pedidoController.CD_PEDIDO, Rotina.PEDIDO, MetodoAPI.CADASTRAR);
                    }

                }
                catch (Exception ex)
                {
                    Globais.log.Info($"Falha ao integrar o pedido: {pedidoController.CD_PEDIDO}. {ex.Message}", true, TipoIntegracao.ERP, Integracao.ZENLOG, true,
                                    pedidoController.CD_PEDIDO, Rotina.PEDIDO, MetodoAPI.CADASTRAR);

                    throw new Exception($"Falha ao integrar o pedido: {pedidoController.CD_PEDIDO}. {ex.Message}");
                }
            }
        }
    }
}
