using ZenLog.EndPoints.Token;
using ZenLog.Models.ModelsController.ModelsIntegracaoController;

namespace ZenLog.Servicos
{
    public class BuscarToken
    {
        EndPointToken endPointToken = new EndPointToken();

        public async Task<string> Login(CadastroIntegracoes integracao)
        {
            try
            {
                string token = await endPointToken.Token(integracao);

                return token;
            }
            catch (Exception ex)
            {
                throw new Exception($"Falha no Login. {ex.Message}");
            }
        }
    }
}
