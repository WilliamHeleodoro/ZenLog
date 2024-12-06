using Dapper.Contrib.Extensions;

namespace ZenLog.Models.ModelsController.ModelsIntegracaoController
{
    public class CadastroIntegracoes
    {
        [Key]
        public int CD_ID { get; set; }
        public int CD_TIPO { get; set; }
        public required string DS_TIPO { get; set; }
        public int CD_INTEGRACAO { get; set; }
        public required string DS_INTEGRACAO { get; set; }
        public required string DS_URL { get; set; }
        public required string DS_USUARIO { get; set; }
        public required string DS_SENHA { get; set; }
        public required string DS_KEY { get; set; }
        public bool X_ATIVAR { get; set; }
        public int CD_USUARIO { get; set; }
        public int CD_USUARIOAT { get; set; }
        public required string DS_TOKEN { get; set; }
        public DateTime DT_CADASTRO { get; set; }

        public DateTime DT_ATUALIZACAO { get; set; }

        public string? DS_LOGIN { get; set; }

        public string? DS_LOGINAT { get; set; }
        public required string CD_EMPRESA_REF_SITE { get; set; }

        public required List<CadastroIntegracoesRotinas> integracoesRotinas { get; set; }
        public required List<CadastroIntegracoesEmpresas> integracoesEmpresas { get; set; }
    }
}
