using ZenLog.Builders.BuildersAPI;
using ZenLog.Builders.BuildersController;
using ZenLog.DAL.ControllerDAL;
using ZenLog.EndPoints.Pedido;
using ZenLog.Models.ModelsController.ModelsDiversasController;
using ZenLog.Models.ModelsController.ModelsIntegracaoController;
using static ZenLog.LogDBIntegracoes;

namespace ZenLog.Servicos
{
    public class EnviarNotaDiversa
    {
        ControllerDAL controllerDAL = new ControllerDAL();
        public async Task IntegrarDiversa(CadastroIntegracoes integracao, NotaDiversasController diversaController)
        {
            if (integracao.integracoesRotinas.FirstOrDefault(x => x.DS_ROTINA == "NOTA DIVERSA" && x.X_CADASTRAR) != null)
            {
                try
                {
                    var pedidoEnviar = new PedidoBuilderAPI().MontarDiversaEnvioAPI(integracao, diversaController);

                    var resultadoEnvioPedido = await new EndPointPedido().CadastrarPedido(integracao, pedidoEnviar);

                    if (resultadoEnvioPedido.result != null)
                    {

                        NotaDiversaIntegracaoController diversaIntegracaoController = new NotaDiversaIntegracaoBuilderController().MontarDiversaIntegracao(diversaController);

                        await controllerDAL.InserirDiversaIntegracaoController(diversaIntegracaoController);

                        Globais.log.Info($"Nota Diversa: {diversaController.CD_LANCAMENTO} cadastrado na API com sucesso", true, TipoIntegracao.ERP, Integracao.ZENLOG, false,
                              diversaController.CD_LANCAMENTO, Rotina.NOTA_DIVERSA, MetodoAPI.CADASTRAR);
                    }
                }
                catch (Exception ex)
                {
                    Globais.log.Info($"Falha ao integrar a nota diversa: {diversaController.CD_LANCAMENTO}. {ex.Message}", true, TipoIntegracao.ERP, Integracao.ZENLOG, true,
                                    diversaController.CD_LANCAMENTO, Rotina.NOTA_DIVERSA, MetodoAPI.CADASTRAR);

                    throw new Exception($"Falha ao integrar a nota diversa: {diversaController.CD_LANCAMENTO}. {ex.Message}");
                }
            }
        }
    }
}
