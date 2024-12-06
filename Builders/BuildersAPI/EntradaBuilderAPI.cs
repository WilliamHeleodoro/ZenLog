using ZenLog.Models.ModelsAPI.ModelsEnvioAPI.ModelsEntradaEnvioAPI;
using ZenLog.Models.ModelsController.ModelsEntradaController;
using ZenLog.Models.ModelsController.ModelsIntegracaoController;

namespace ZenLog.Builders.BuildersAPI
{
    public class EntradaBuilderAPI
    {
        public EntradaEnvioAPI MontarEntradaEnvioAPI(CadastroIntegracoes integracao, EntradaController entradaController)
        {
            List<EntradaItensEnvioAPI> listaentradaItensEnvioAPI = new List<EntradaItensEnvioAPI>();
            foreach(var item in entradaController.itensEntradaController)
            {
                EntradaItensEnvioAPI entradaItensEnvioAPI = new EntradaItensEnvioAPI
                {
                    CodigoProduto = item.CD_MATERIAL.ToString(),
                    NumeroItem = item.CD_ITEM.ToString(),
                    codigoUnidadeMedida = item.DS_UNIDADE,
                    NumeroPedido = entradaController.CD_ENTRADA.ToString(),
                    Quantidade = item.NR_QUANTIDADE,
                    ValorUnitario = item.VL_UNITARIO,
                    ValorTotalItem = (item.NR_QUANTIDADE * item.VL_UNITARIO)
                };

                listaentradaItensEnvioAPI.Add(entradaItensEnvioAPI);
            }

            EntradaEnvioAPI entradaEnvioAPI = new EntradaEnvioAPI
            {
                Codigo = entradaController.CD_ENTRADA.ToString(),
                CpfCnpj = "52.890.843/0001-04",
                CpfCnpjDepositante = integracao.CD_EMPRESA_REF_SITE,
                CpfCnpjFornecedor = entradaController.NR_CPFCNPJ_FORNECEDOR,
                InscricaoEstadualFornecedor = entradaController.NR_IE_FORNECEDOR,
                RazaoSocialFornecedor = entradaController.DS_RAZAOSOCIAL_FORNECEDOR,
                UfFornecedor = entradaController.DS_UF_FORNECEDOR,
                CidadeIbgeFornecedor = entradaController.CD_MUNIBGE_FORNECEDOR,
                CepFornecedor = entradaController.NR_CEP_FORNECEDOR,
                BairroFornecedor = entradaController.DS_BAIRRO_FORNECEDOR,
                RuaFornecedor = entradaController.DS_ENDERECO_FORNECEDOR,
                NumeroFornecedor = entradaController.NR_NUMERO_FORNECEDOR,
                TelefoneFornecedor = entradaController.NR_TELEFONE_FORNECEDOR,
                SituacaoFornecedor = true,
                TipoFornecedor = entradaController.NR_CPFCNPJ_FORNECEDOR?.Where(char.IsDigit).ToArray().Length == 14 ? "J" : "F",
                Tipo = "B",
                NumeroNf = entradaController.NR_DOCUMENTO.ToString(),
                Serie = entradaController.DS_DF_SERIE,
                DataEmissao = entradaController.DT_EMISSAO.ToString(),
                Cfop = entradaController.itensEntradaController.Select(X=>X.DS_CFOP).First(),
                Chave = entradaController.CV_ACESSO,
                LogradouroFornecedor = entradaController.DS_ENDERECO_FORNECEDOR?.Length > 10 ? entradaController.DS_ENDERECO_FORNECEDOR.Substring(0,10) : entradaController.DS_ENDERECO_FORNECEDOR,
                ValorTotal = entradaController.VL_TOTAL,
                especieNfe = "NFE",
                produtos = listaentradaItensEnvioAPI
            };

            return entradaEnvioAPI;
        }
    }
}
