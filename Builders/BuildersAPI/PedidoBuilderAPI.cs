using ZenLog.Models.ModelsAPI.ModelsEnvioAPI.ModelsPedidoEnvioAPI;
using ZenLog.Models.ModelsController.ModelsDiversasController;
using ZenLog.Models.ModelsController.ModelsIntegracaoController;
using ZenLog.Models.ModelsController.ModelsPedidoController;

namespace ZenLog.Builders.BuildersAPI
{
    public class PedidoBuilderAPI
    {
        public PedidoEnvioAPI MontarPedidoEnvioAPI(CadastroIntegracoes integracao, PedidoController pedidoController)
        {
            List<PedidoMaterialEnvioAPI> listapedidoMaterialEnvioAPI = new List<PedidoMaterialEnvioAPI>();

            if (pedidoController.itensController != null)
            {
                int numeroItem = 0;
                foreach (var material in pedidoController.itensController)
                {
                    numeroItem++;
                    PedidoMaterialEnvioAPI pedidoMaterialEnvioAPI = new PedidoMaterialEnvioAPI
                    {
                        CodigoProduto = material.CD_MATERIAL.ToString(),
                        NumeroItem = numeroItem.ToString(),
                        NumeroPedido = pedidoController.CD_PEDIDO.ToString(),
                        Quantidade = material.NR_QUANTIDADE,
                        ValorUnitario = material.VL_UNITARIO,
                    };

                    listapedidoMaterialEnvioAPI.Add(pedidoMaterialEnvioAPI);
                }

            }

            PedidoEnvioAPI pedidoEnvioAPI = new PedidoEnvioAPI
            {
                CpfCnpj = "52.890.843/0001-04",
                CpfCnpjDepositante = integracao.CD_EMPRESA_REF_SITE,
                InscricaoEstadualDepositante = pedidoController.NR_IE_EMPRESA,
                Pedido = $"PD{pedidoController.CD_PEDIDO}",
                CpfCnpjDestinatario = pedidoController.NR_CPFCNPJ_CLIENTE,
                RazaoSocialDestinatario = pedidoController.DS_RAZAOSOCIAL_CLIENTE,
                UfDestinatario = pedidoController.DS_UF_CLIENTE,
                CidadeIbgeDestinatario = pedidoController.CD_MUNIBGE_CLIENTE,
                CepDestinatario = pedidoController.NR_CEP_CLIENTE,
                BairroDestinatario = pedidoController.DS_BAIRRO_CLIENTE,
                NumeroDestinatario = pedidoController.NR_NUMERO_CLIENTE,
                RuaDestinatario = pedidoController.DS_ENDERECO_CLIENTE,
                ComplementoDestinatario = pedidoController.DS_COMPLEMENTO_CLIENTE,
                CpfCnpjTransportador = pedidoController.NR_CPFCNPJ_TRANSPORTADOR,
                DataEmissao = pedidoController.DT_EMISSAO,
                produtos = listapedidoMaterialEnvioAPI,
            };

            return pedidoEnvioAPI;
        }

        public PedidoEnvioAPI MontarDiversaEnvioAPI(CadastroIntegracoes integracao, NotaDiversasController diversaController)
        {
            List<PedidoMaterialEnvioAPI> listapedidoMaterialEnvioAPI = new List<PedidoMaterialEnvioAPI>();

            if (diversaController.itensDiversasController != null)
            {
                foreach (var material in diversaController.itensDiversasController)
                {
                    PedidoMaterialEnvioAPI pedidoMaterialEnvioAPI = new PedidoMaterialEnvioAPI
                    {
                        CodigoProduto = material.CD_MATERIAL.ToString(),
                        NumeroItem = material.CD_ITEM.ToString(),
                        NumeroPedido = diversaController.CD_LANCAMENTO.ToString(),
                        Quantidade = material.NR_QUANTIDADE,
                        ValorUnitario = material.VL_UNITARIO,
                    };

                    listapedidoMaterialEnvioAPI.Add(pedidoMaterialEnvioAPI);
                }

            }

            PedidoEnvioAPI pedidoEnvioAPI = new PedidoEnvioAPI
            {
                CpfCnpj = "52.890.843/0001-04",
                CpfCnpjDepositante = integracao.CD_EMPRESA_REF_SITE,
                InscricaoEstadualDepositante = diversaController.NR_IE_EMPRESA,
                Pedido = $"ND{diversaController.CD_LANCAMENTO}",
                NumeroNf = diversaController.NR_DOCUMENTO.ToString(),
                ChaveAcesso = diversaController.CV_ACESSO,
                Serie = diversaController.DS_DF_SERIE,
                CpfCnpjDestinatario = diversaController.NR_CPFCNPJ_ENTIDADE,
                RazaoSocialDestinatario = diversaController.DS_RAZAOSOCIAL_ENTIDADE,
                UfDestinatario = diversaController.DS_UF_ENTIDADE,
                CidadeIbgeDestinatario = diversaController.CD_MUNIBGE_ENTIDADE,
                CepDestinatario = diversaController.NR_CEP_ENTIDADE,
                BairroDestinatario = diversaController.DS_BAIRRO_ENTIDADE,
                NumeroDestinatario = diversaController.NR_NUMERO_ENTIDADE,
                RuaDestinatario = diversaController.DS_ENDERECO_ENTIDADE,
                ComplementoDestinatario = diversaController.DS_COMPLEMENTO_ENTIDADE,
                CpfCnpjTransportador = diversaController.NR_CPFCNPJ_TRANSPORTADOR,
                DataEmissao = diversaController.DT_EMISSAO,
                produtos = listapedidoMaterialEnvioAPI,
            };

            return pedidoEnvioAPI;
        }
    }
}
