using System.ComponentModel;
using ZenLog.DAL.ControllerDAL;
using ZenLog.Models.ModelsController.ModelsIntegracaoController;

namespace ZenLog
{
    public class LogDBIntegracoes
    {
        private string Caminho { get; set; } = "C:\\";
        private string NomeArquivo { get; set; } = "Log_info.txt";
        private string Tipo { get; set; }

        public LogDBIntegracoes(string caminho, string tipo)
        {
            Caminho = caminho;
            Tipo = tipo;

            NomeArquivo = DateTime.Now.ToString("dd/MM/yyyy").Replace("/", "");
        }

        public enum TipoIntegracao
        {
            [Description("ERP")]
            ERP
        }

        public enum Integracao
        {
            [Description("ADN")]
            ADN,
            [Description("ZENLOG")]
            ZENLOG
        }

        public enum Rotina
        {
            [Description("CLIENTE")]
            CLIENTE,
            [Description("FORNECEDOR")]
            FORNECEDOR,
            [Description("CONTRATO")]
            CONTRATO,
            [Description("PEDIDO")]
            PEDIDO,
            [Description("ENTRADA DE MERCADORIA")]
            ENTRADA_DE_MERCADORIA,
            [Description("BAIXAS PEDIDO")]
            BAIXAS_PEDIDO,
            [Description("BAIXAS ENTRADA DE MERCADORIA")]
            BAIXAS_ENTRADA_DE_MERCADORIA,
            [Description("MATERIAL")]
            MATERIAL,
            [Description("NOTA DIVERSA")]
            NOTA_DIVERSA,
        }

        public enum MetodoAPI
        {
            [Description("CADASTRAR")]
            CADASTRAR,
            [Description("ATUALIZAR")]
            ATUALIZAR,
            [Description("DELETAR")]
            DELETAR,
            [Description("BUSCAR")]
            BUSCAR
        }

        public void Info(string log, bool gravarBranco = false, TipoIntegracao? tipo = null, Integracao? integracao = null, bool falha = false,
            long? codigoRegistro = null, Rotina? rotina = null, MetodoAPI? metodoAPI = null, string statusAPI = null, bool parar = false)
        {
            try
            {

                var DetalheErro = DateTime.Now + " - (" + tipo + ") " + log +
                    Environment.NewLine +
                    "-----------------------------------------------------------------------------------------------------------" +
                    Environment.NewLine;
                Console.Write(DetalheErro);
                if (!Directory.Exists($@"{Caminho}\logs\"))
                    Directory.CreateDirectory($@"{Caminho}\logs\");
                File.AppendAllText($@"{Caminho}\logs\{NomeArquivo}", DetalheErro);
            }
            catch
            {
            }
            finally
            {
                if (gravarBranco)
                {
                    GravarLogBanco(log, falha, tipo, integracao, rotina, metodoAPI, codigoRegistro, statusAPI);
                    if (parar)
                        throw new Exception("(" + tipo + ") " + log);
                }
            }
        }



        public void GravarLogBanco(string log, bool falha, TipoIntegracao? tipo = null, Integracao? integracao = null,
            Rotina? rotina = null, MetodoAPI? metodoAPI = null, long? codigoRegistro = null, string statusAPI = null)
        {
            try
            {
                IntegracoesLogs integacaoLog = new IntegracoesLogs
                {
                    DS_TIPO = tipo.ToString(),
                    DS_INTEGRACAO = integracao.ToString(),
                    DS_ROTINA = rotina.ToString(),
                    DS_METODO_API = metodoAPI.ToString(),
                    DS_LOG = log,
                    DT_LOG = DateTime.Now,
                    CD_REGISTRO = codigoRegistro ?? 0,
                    X_FALHA = falha,
                    DS_STATUS_API = statusAPI,

                };

                new ControllerDAL().InserirLog(integacaoLog);
            }
            catch (Exception)
            {

            }
        }
    }
}
