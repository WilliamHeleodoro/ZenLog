using ZenLog.Models.ModelsController.ModelsEntradaController;
using ZenLog.Models.ModelsController.ModelsIntegracaoController;

namespace ZenLog.Builders.BuildersController
{
    public class EntradaIntegracaoBuilderController
    {
        public EntradaIntegracaoController MontarEntradaIntegracaoController(EntradaController entradaController)
        {
            EntradaIntegracaoController entradaIntegracaoController = new EntradaIntegracaoController
            {
                CD_ID = entradaController.CD_ID_ENTRADA_INTEGRACAO,
                CD_ENTRADA = entradaController.CD_ENTRADA,
                NR_NOTAFISCAL = entradaController.NR_DOCUMENTO.ToString(),
                DS_INTEGRACAO = "ZENLOG",
                DS_TIPO = "ERP",
                DT_ENVIO = DateTime.Now,
                X_ENVIADO = true,
            };

            return entradaIntegracaoController;
        }
    }
}
