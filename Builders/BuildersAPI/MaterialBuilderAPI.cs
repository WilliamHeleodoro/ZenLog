using ZenLog.Models.ModelsAPI.ModelsMaterialEnvioAPI.MaterialEnvioAPI;
using ZenLog.Models.ModelsController.ModelsIntegracaoController;
using ZenLog.Models.ModelsController.ModelsMaterialController;

namespace ZenLog.Builders.BuildersAPI
{
    public class MaterialBuilderAPI
    {
       public MaterialEnvioAPI MontarMaterialEnvioAPI(CadastroIntegracoes cadastroIntegracoes, MaterialController materialController)
        {
            MaterialEnvioAPI materialEnvioAPI = new MaterialEnvioAPI
            {
                cpfCnpj = "52.890.843/0001-04",
                codigo = materialController.CD_MATERIAL.ToString(),
                cpfCnpjDepostante = cadastroIntegracoes.CD_EMPRESA_REF_SITE,
                codigoCategoria = "3",
                descricao = materialController.DS_MATERIAL ?? "",
                codigoUnidadeMedida = materialController.DS_SIGLA ?? "",
                classificacaoFiscalNCM = materialController.CD_CODFISCAL ?? "",
                qtdItemSKU = 1,
                descricaoEmbalagem = materialController.DS_MATERIAL?.Length > 45 ? materialController.DS_MATERIAL.Substring(0, 45) : materialController.DS_MATERIAL ?? "",
                pesoSku = 0,
                altura = materialController.NR_ALTURA.ToString(),
                comprimento = materialController.NR_COMPRIMENTO.ToString(),
                largura = materialController.NR_LARGURA.ToString(),
            };


            return materialEnvioAPI;
        } 
    }
}
