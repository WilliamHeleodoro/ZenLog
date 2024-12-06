using ZenLog.Models.ModelsController.ModelsIntegracaoController;
using ZenLog.Models.ModelsController.ModelsPedidoController;

namespace ZenLog.Builders.BuildersController
{
    public class PedidoIntegracaoBuilderController
    {
        public PedidoIntegracaoController MontarPedidoIntegracao(PedidoController pedidoController, string? statusAPI = null) 
        {
            PedidoIntegracaoController pedidoIntegracaoController = new PedidoIntegracaoController
            {
                CD_ID = pedidoController.CD_ID_PEDIDO_INTEGRACAO,
                CD_PEDIDO = pedidoController.CD_PEDIDO,
                CD_STATUS = pedidoController.CD_STATUS,
                DS_INTEGRACAO = "ZENLOG",
                DS_TIPO = "ERP",
                DT_ENVIO = DateTime.Now,
                X_ENVIADO = true,
                DS_STATUS_API = statusAPI,
            };

            return pedidoIntegracaoController; 
        }
    }
}
