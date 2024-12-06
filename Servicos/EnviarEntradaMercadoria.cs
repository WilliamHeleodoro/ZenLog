using ZenLog.Builders.BuildersAPI;
using ZenLog.Builders.BuildersController;
using ZenLog.DAL.ControllerDAL;
using ZenLog.EndPoints.Entrada;
using ZenLog.Models.ModelsController.ModelsEntradaController;
using ZenLog.Models.ModelsController.ModelsIntegracaoController;
using static ZenLog.LogDBIntegracoes;

namespace ZenLog.Servicos
{
    public class EnviarEntradaMercadoria
    {
        ControllerDAL controllerDAL = new ControllerDAL();
        public async Task IntegrarEntrada(CadastroIntegracoes integracao, EntradaController entradaController)
        {
            if (integracao.integracoesRotinas.FirstOrDefault(x => x.DS_ROTINA == "ENTRADA DE MERCADORIA" && x.X_CADASTRAR) != null)
            {
                try
                {
                    var entradaEnviar = new EntradaBuilderAPI().MontarEntradaEnvioAPI(integracao, entradaController);

                    var resultadoEnvioPedido = await new EndPointEntrada().CadastrarEntrada(integracao, entradaEnviar);

                    if (resultadoEnvioPedido.result != null)
                    {

                        EntradaIntegracaoController entradaIntegracaoController = new EntradaIntegracaoBuilderController().MontarEntradaIntegracaoController(entradaController);

                        await controllerDAL.AtualizarEntradaIntegracaoController(entradaIntegracaoController);

                        Globais.log.Info($"Entrada: {entradaController.CD_ENTRADA} cadastrado na API com sucesso", true, TipoIntegracao.ERP, Integracao.ZENLOG, false,
                              entradaController.CD_ENTRADA, Rotina.ENTRADA_DE_MERCADORIA, MetodoAPI.CADASTRAR);
                    }
                }
                catch (Exception ex)
                {
                    Globais.log.Info($"Falha ao integrar a entrada de mercadoria: {entradaController.CD_ENTRADA}. {ex.Message}", true, TipoIntegracao.ERP, Integracao.ZENLOG, true,
                                    entradaController.CD_ENTRADA, Rotina.ENTRADA_DE_MERCADORIA, MetodoAPI.CADASTRAR);

                    throw new Exception($"Falha ao integrar a entrada de mercadoria: {entradaController.CD_ENTRADA}. {ex.Message}");
                }
            }
        }
    }
}
