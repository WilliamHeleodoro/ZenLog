using ZenLog.DAL.ControllerDAL;
using ZenLog.EndPoints.Pedido;
using ZenLog.Models.ModelsController.ModelsIntegracaoController;
using static ZenLog.LogDBIntegracoes;
namespace ZenLog.Servicos
{
    public class BuscarPedidos
    {
        ControllerDAL controllerDAL = new ControllerDAL();
        public async Task IntegrarBuscaPedidos(CadastroIntegracoes integracao)
        {
            try
            {
                if (integracao.integracoesRotinas.FirstOrDefault(x => x.DS_ROTINA == "PEDIDO" && x.X_BUSCAR) != null)
                {
                    List<string> listaPedidosEnviados = await controllerDAL.BuscarCodigosPedidosEnviados();

                    var respostaAPI = await new EndPointPedido().BuscarPedidosExpedicao(integracao);

                    var retornoPedidosConferidos = respostaAPI.result.items.Where(x => x.situacaoIntegracao == 3 && x.codigo.Contains("PD") &&
                                                                                listaPedidosEnviados.Contains(x.codigo.Replace("PD", ""))).ToList();

                    foreach (var pedidoConferido in retornoPedidosConferidos)
                    {
                        try
                        {
                            await controllerDAL.AtualizarPedido(pedidoConferido.documentos.First());


                            Globais.log.Info($"Pedido: {pedidoConferido.codigo?.Replace("PD", "")} buscado da API e atualizado os dados de volume no controller com sucesso", true, TipoIntegracao.ERP, Integracao.ZENLOG, false,
                                  Convert.ToInt64(pedidoConferido.codigo?.Replace("PD", "")), Rotina.PEDIDO, MetodoAPI.BUSCAR, "CONFERIDO");
                        }
                        catch (Exception ex)
                        {
                            Globais.log.Info($"Falha ao integrar o pedido: {pedidoConferido.codigo}. {ex.Message}", true, TipoIntegracao.ERP, Integracao.ZENLOG, true,
                               Convert.ToInt64(pedidoConferido.codigo?.Replace("PD", "")), Rotina.PEDIDO, MetodoAPI.BUSCAR);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Globais.log.Info($"Falha ao integrar o pedido. {ex.Message}", true, TipoIntegracao.ERP, Integracao.ZENLOG, true,
                                0, Rotina.PEDIDO, MetodoAPI.BUSCAR);
            }
        }
    }
}
