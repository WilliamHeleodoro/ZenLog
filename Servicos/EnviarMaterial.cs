using ZenLog.Builders.BuildersAPI;
using ZenLog.Builders.BuildersController;
using ZenLog.DAL.ControllerDAL;
using ZenLog.EndPoints.Material;
using ZenLog.Models.ModelsAPI.ModelsMaterialEnvioAPI.MaterialEnvioAPI;
using ZenLog.Models.ModelsController.ModelsIntegracaoController;
using ZenLog.Models.ModelsController.ModelsMaterialController;
using static ZenLog.LogDBIntegracoes;

namespace ZenLog.Servicos
{
    public class EnviarMaterial
    {
        ControllerDAL controllerDAL = new ControllerDAL();

        public async Task IntegrarMaterial(CadastroIntegracoes integracao, MaterialController materialController)
        {
            MaterialIntegracaoController? materialIntegracaoController = await controllerDAL.BuscarCodigoMaterialIntegracaoController(materialController.CD_MATERIAL);

            if (materialIntegracaoController != null && materialController.DT_ATUALIZACAO > materialIntegracaoController.DT_ATUALIZACAO)
            {
                try
                {
                    if (integracao.integracoesRotinas.FirstOrDefault(x => x.DS_ROTINA == "MATERIAL" && x.X_ATUALIZAR) != null)
                    {
                        MaterialEnvioAPI materialEnvioAPI = new MaterialBuilderAPI().MontarMaterialEnvioAPI(integracao, materialController);

                        var materialRetornoAPI = await new EndPointMaterial().AtualizarMaterial(integracao, materialEnvioAPI);

                        if (materialRetornoAPI.result != null)
                        {
                            MaterialIntegracaoController materialIntegracaoControllerAtualizar = new MaterialIntegracaoBuilderController().MontarMaterialIntegracao(materialController, materialIntegracaoController);

                            await controllerDAL.AtualizarMaterialIntegracaoController(materialIntegracaoControllerAtualizar);

                            Globais.log.Info($"Material: {materialController.CD_MATERIAL} atualizado na API com sucesso", true, TipoIntegracao.ERP, Integracao.ZENLOG, false,
                               materialController.CD_MATERIAL, Rotina.MATERIAL, MetodoAPI.ATUALIZAR);
                        }
                    }

                }
                catch (Exception ex)
                {
                    Globais.log.Info($"Falha ao integrar o material: {materialController.CD_MATERIAL}. {ex.Message}", true, TipoIntegracao.ERP, Integracao.ZENLOG, true,
                            materialController.CD_MATERIAL, Rotina.MATERIAL, MetodoAPI.ATUALIZAR);

                    throw new Exception($"Falha ao integrar o material: {materialController.CD_MATERIAL}. {ex.Message}");
                }
            }
            else if(materialIntegracaoController == null)
            {
                try
                {
                    if (integracao.integracoesRotinas.FirstOrDefault(x => x.DS_ROTINA == "MATERIAL" && x.X_CADASTRAR) != null)
                    {
                        MaterialEnvioAPI materialEnvioAPI = new MaterialBuilderAPI().MontarMaterialEnvioAPI(integracao, materialController);

                        var materialRetornoAPI = await new EndPointMaterial().CadastrarMaterial(integracao, materialEnvioAPI);

                        if (materialRetornoAPI.result != null)
                        {
                            MaterialIntegracaoController materialIntegracaoControllerCadastrar = new MaterialIntegracaoBuilderController().MontarMaterialIntegracao(materialController);

                            await controllerDAL.InserirMaterialIntegracaoController(materialIntegracaoControllerCadastrar);

                            Globais.log.Info($"Material: {materialController.CD_MATERIAL} cadastrado na API com sucesso", true, TipoIntegracao.ERP, Integracao.ZENLOG, false,
                               materialController.CD_MATERIAL, Rotina.MATERIAL, MetodoAPI.CADASTRAR);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Globais.log.Info($"Falha ao integrar o material: {materialController.CD_MATERIAL}. {ex.Message}", true, TipoIntegracao.ERP, Integracao.ZENLOG, true,
                            materialController.CD_MATERIAL, Rotina.MATERIAL, MetodoAPI.CADASTRAR);

                    throw new Exception($"Falha ao integrar o material: {materialController.CD_MATERIAL}. {ex.Message}");
                }
            }

        }
    }
}
