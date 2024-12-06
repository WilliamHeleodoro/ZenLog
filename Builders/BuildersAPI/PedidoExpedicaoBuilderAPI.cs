using ZenLog.Models.ModelsAPI.ModelsEnvioAPI.ModelsPedidoEnvioAPI;
using ZenLog.Models.ModelsController.ModelsIntegracaoController;
using ZenLog.Models.ModelsController.ModelsPedidoController;

namespace ZenLog.Builders.BuildersAPI
{
    public class PedidoExpedicaoBuilderAPI
    {
        public PedidoExpedicaoAPI MontarPedidoExpedicao(CadastroIntegracoes integracao, PedidoController pedidoController)
        {
            PedidoExpedicaoAPI pedidoExpedicaoAPI = new PedidoExpedicaoAPI
            {
                CpfCnpjOperadorLogistico = "52.890.843/0001-04",
                CpfCnpjDepositante = integracao.CD_EMPRESA_REF_SITE,
                InscricaoEstadualDepositante = pedidoController.NR_IE_EMPRESA,
                Pedido = $"PD{pedidoController.CD_PEDIDO}",
                NumeroNf = pedidoController.NR_NOTAFISCAL.ToString(),
                ChaveAcesso = pedidoController.CV_ACESSO,
                Serie = pedidoController.DS_DF_SERIE,
            };

            return pedidoExpedicaoAPI;
        }
    }
}
