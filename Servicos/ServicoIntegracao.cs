using ZenLog.DAL.ControllerDAL;
using ZenLog.Models.ModelsController.ModelsDiversasController;
using ZenLog.Models.ModelsController.ModelsEntradaController;
using ZenLog.Models.ModelsController.ModelsIntegracaoController;
using ZenLog.Models.ModelsController.ModelsMaterialController;
using ZenLog.Models.ModelsController.ModelsPedidoController;
using static ZenLog.LogDBIntegracoes;


namespace ZenLog.Servicos
{
    public class ServicoIntegracao
    {
        ControllerDAL controllerDAL = new ControllerDAL();
        public async Task Integrar()
        {
            try
            {
                List<CadastroIntegracoes> listaIntegracoes = await controllerDAL.BuscarIntegracao();
                CadastroIntegracoes integracao = listaIntegracoes.First(x => x.DS_INTEGRACAO == "ZENLOG");

                string token = await new BuscarToken().Login(integracao);
                integracao.DS_TOKEN = token;

                foreach (var empresa in integracao.integracoesEmpresas)
                {
                    Globais.log.Info($"Integração Empresa: {empresa.CD_EMPRESA}");

                    integracao.CD_EMPRESA_REF_SITE = empresa.CD_EMPRESA_INTEGRACAO;
                    
                    //Envio Pedido
                    List<PedidoController> listaPedidosEnviar = await controllerDAL.BuscarPedidos(empresa.CD_EMPRESA);

                    foreach (var pedido in listaPedidosEnviar)
                    {
                        try
                        {
                            if (integracao.integracoesRotinas.FirstOrDefault(x => x.DS_ROTINA == "MATERIAL") != null)
                            {

                                if (pedido.itensController?.Count() > 0 && pedido.itensController != null)
                                    foreach (var material in pedido.itensController)
                                    {
                                        MaterialController materialController = await controllerDAL.BuscarMaterialController(material.CD_MATERIAL);
                                        await new EnviarMaterial().IntegrarMaterial(integracao, materialController);
                                    }
                            }

                            if (integracao.integracoesRotinas.FirstOrDefault(x => x.DS_ROTINA == "PEDIDO") != null)
                            {
                                await new EnviarPedido().IntegrarPedido(integracao, pedido);
                            }
                        }
                        catch { }
                    }

                    //Buscar Pedidos Expedidos
                    await new BuscarPedidos().IntegrarBuscaPedidos(integracao);

                    //Atualizar Pedidos Emitidos
                    await new AtualizarPedido().AtualizarPedidoExpedido(integracao);


                    //Envio Diversas Saídas
                    List<NotaDiversasController> listaNotaDiversasController = await controllerDAL.BuscarDiversas(empresa.CD_EMPRESA);

                    foreach(var diversa in listaNotaDiversasController)
                    {
                        try
                        {
                            if (integracao.integracoesRotinas.FirstOrDefault(x => x.DS_ROTINA == "MATERIAL") != null)
                            {

                                if (diversa.itensDiversasController?.Count() > 0 && diversa.itensDiversasController != null)
                                    foreach (var material in diversa.itensDiversasController)
                                    {
                                        MaterialController materialController = await controllerDAL.BuscarMaterialController(material.CD_MATERIAL);
                                        await new EnviarMaterial().IntegrarMaterial(integracao, materialController);
                                    }
                            }

                            if (integracao.integracoesRotinas.FirstOrDefault(x => x.DS_ROTINA == "NOTA DIVERSA") != null)
                            {
                                await new EnviarNotaDiversa().IntegrarDiversa(integracao, diversa);
                            }
                        }
                        catch { }
                    }

                    //Envio Entradas
                    List<EntradaController> listaEntradasController = await controllerDAL.BuscarEntradas(empresa.CD_EMPRESA);

                    foreach (var entrada in listaEntradasController)
                    {
                        try
                        {
                            if (integracao.integracoesRotinas.FirstOrDefault(x => x.DS_ROTINA == "MATERIAL") != null)
                            {

                                if (entrada.itensEntradaController?.Count() > 0 && entrada.itensEntradaController != null)
                                    foreach (var material in entrada.itensEntradaController)
                                    {
                                        MaterialController materialController = await controllerDAL.BuscarMaterialController(material.CD_MATERIAL);
                                        await new EnviarMaterial().IntegrarMaterial(integracao, materialController);
                                    }
                            }

                            if (integracao.integracoesRotinas.FirstOrDefault(x => x.DS_ROTINA == "ENTRADA DE MERCADORIA") != null)
                            {
                                await new EnviarEntradaMercadoria().IntegrarEntrada(integracao, entrada);
                            }
                        }
                        catch { }
                    }

                }
            }
            catch (Exception ex)
            {
                Globais.log.Info($"Houve problemas com a integração. {ex.Message}", true, TipoIntegracao.ERP, Integracao.ZENLOG, true);
            }

        }
    }
}
