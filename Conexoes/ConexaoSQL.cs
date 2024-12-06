using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace ZenLog.Conexoes
{
    public class ConexaoSQL : IDisposable
    {
        private string _stringConexao;
        public SqlConnection _conexao;

        public ConexaoSQL()
        {
            _stringConexao = GetStringConexaoBanco();
            _conexao = new SqlConnection(_stringConexao);
        }

        public string StringConexao
        {
            get { return _stringConexao; }
            set { _stringConexao = value; }
        }

        public SqlConnection ObjetoConexao
        {
            get { return _conexao; }
            set { _conexao = value; }
        }
        public void Conectar()
        {
            if (_conexao.State != ConnectionState.Open)
            {
                _conexao.Open();
            }
        }

        public void Desconectar()
        {
            if (_conexao.State == ConnectionState.Open)
            {
                _conexao.Close();
            }
        }

        public void Dispose()
        {
            // Certifique-se de que a conexão esteja fechada antes de descartá-la
            if (_conexao != null)
            {
                if (_conexao.State == ConnectionState.Open)
                {
                    Desconectar();
                }
                _conexao.Dispose();
            }
        }

        private string GetStringConexaoBanco()
        {
            // Configura o caminho do arquivo `.ini` e o provedor de configuração
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddIniFile("VSCONF.VIS") // Aqui, você pode usar o arquivo com extensão `.vis`
                .Build();

            // Acessa as configurações dentro da seção "PARAMETROS"
            var ip = configuration["PARAMETROS:IPSERVIDOR"];
            var database = configuration["PARAMETROS:DATABASE"];
            var senha = configuration["PARAMETROS:SENHA"];

            // Monta a string de conexão
            return $"Data Source={ip};Initial Catalog={database};User ID=sa;Password={senha}";
        }
    }
}
