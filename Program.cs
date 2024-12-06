using ZenLog;
using ZenLog.Conexoes;
using ZenLog.Servicos;

public static class Globais
{
    public static LogDBIntegracoes log { get; set; } = new LogDBIntegracoes(AppDomain.CurrentDomain.BaseDirectory, "ZENLOG");
}

public class Program
{
    async static Task Main(string[] args)
    {
        try
        {
            Globais.log.Info($"Iniciando Integração");

            var conexao = new ConexaoSQL();

            if (string.IsNullOrEmpty(conexao.ObjetoConexao.Database))
            {
                Globais.log.Info($"Não achou nenhuma conexão. Verifique se possui o VSCONF", true, LogDBIntegracoes.TipoIntegracao.ERP, LogDBIntegracoes.Integracao.ADN, true);
                return;
            }

            await new ServicoIntegracao().Integrar();

        }
        catch (Exception ex)
        {
            Globais.log.Info($"{ex.Message}", true, LogDBIntegracoes.TipoIntegracao.ERP, LogDBIntegracoes.Integracao.ADN, true);
        }

    }
}