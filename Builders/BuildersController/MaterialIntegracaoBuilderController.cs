using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenLog.Models.ModelsController.ModelsIntegracaoController;
using ZenLog.Models.ModelsController.ModelsMaterialController;

namespace ZenLog.Builders.BuildersController
{
    public class MaterialIntegracaoBuilderController
    {
        public MaterialIntegracaoController MontarMaterialIntegracao(MaterialController materialController, MaterialIntegracaoController? materialIntegracaoController = null) 
        {
            MaterialIntegracaoController materialIntegracaoControllerNovo = new MaterialIntegracaoController();

            if (materialIntegracaoController == null)
            {

                materialIntegracaoControllerNovo.CD_MATERIAL = materialController.CD_MATERIAL;
                materialIntegracaoControllerNovo.CD_MATERIAL_INTEGRACAO = materialController.CD_MATERIAL.ToString();
                materialIntegracaoControllerNovo.DS_INTEGRACAO = "ZENLOG";
                materialIntegracaoControllerNovo.DS_TIPO = "ERP";
                materialIntegracaoControllerNovo.DT_ATUALIZACAO = DateTime.Now;
                materialIntegracaoControllerNovo.DT_ENVIO = DateTime.Now;
                materialIntegracaoControllerNovo.X_ENVIADO = true;
            }
            else
            {
                materialIntegracaoControllerNovo.CD_ID = materialIntegracaoController.CD_ID;
                materialIntegracaoControllerNovo.CD_MATERIAL = materialController.CD_MATERIAL;
                materialIntegracaoControllerNovo.CD_MATERIAL_INTEGRACAO = materialController.CD_MATERIAL.ToString();
                materialIntegracaoControllerNovo.DS_INTEGRACAO = "ZENLOG";
                materialIntegracaoControllerNovo.DS_TIPO = "ERP";
                materialIntegracaoControllerNovo.DT_ATUALIZACAO = DateTime.Now;
                materialIntegracaoControllerNovo.DT_ENVIO = materialIntegracaoController.DT_ENVIO;
                materialIntegracaoControllerNovo.X_ENVIADO = true;

            }

            return materialIntegracaoControllerNovo;
        }
    }
}
