using ZenLog.Models.ModelsController.ModelsDiversasController;
using ZenLog.Models.ModelsController.ModelsIntegracaoController;

namespace ZenLog.Builders.BuildersController
{
    public class NotaDiversaIntegracaoBuilderController
    {
        public NotaDiversaIntegracaoController MontarDiversaIntegracao(NotaDiversasController diversaController)
        {
            NotaDiversaIntegracaoController diversaIntegracaoController = new NotaDiversaIntegracaoController
            {
                CD_LANCAMENTO = diversaController.CD_LANCAMENTO,
                CD_STATUS = diversaController.CD_STATUS,
                NR_NOTAFISCAL = diversaController.NR_DOCUMENTO.ToString(),
                DS_INTEGRACAO = "ZENLOG",
                DS_TIPO = "ERP",
                DT_ENVIO = DateTime.Now,
                X_ENVIADO = true,
            };

            return diversaIntegracaoController;
        }
    }
}
