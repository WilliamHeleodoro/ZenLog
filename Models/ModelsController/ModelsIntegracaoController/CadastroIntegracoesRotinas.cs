using Dapper.Contrib.Extensions;

namespace ZenLog.Models.ModelsController.ModelsIntegracaoController
{
    public class CadastroIntegracoesRotinas
    {
        [Key]
        public int CD_ID { get; set; }
        public int CD_ROTINA { get; set; }

        public string? DS_ROTINA { get; set; }
        public int CD_CADASTRO_INTEGRACAO { get; set; }

        public bool X_CADASTRAR { get; set; }
        public bool X_ATUALIZAR { get; set; }
        public bool X_DELETAR { get; set; }
        public bool X_BUSCAR { get; set; }
    }
}
