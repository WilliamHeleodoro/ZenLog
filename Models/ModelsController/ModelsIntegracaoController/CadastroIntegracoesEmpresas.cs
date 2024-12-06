using Dapper.Contrib.Extensions;

namespace ZenLog.Models.ModelsController.ModelsIntegracaoController
{
    public class CadastroIntegracoesEmpresas
    {
        [Key]
        public int CD_ID { get; set; }
        public int CD_EMPRESA { get; set; }

        public string? DS_EMPRESA { get; set; }
        public int CD_CADASTRO_INTEGRACAO { get; set; }
        public required string CD_EMPRESA_INTEGRACAO { get; set; }
    }
}
