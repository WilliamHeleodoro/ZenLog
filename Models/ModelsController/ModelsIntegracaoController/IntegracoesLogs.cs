using Dapper.Contrib.Extensions;

namespace ZenLog.Models.ModelsController.ModelsIntegracaoController
{
    [Table("TBL_INTEGRACOES_LOGS")]
    public class IntegracoesLogs
    {

        [Key]
        public int CD_ID { get; set; }
        public string? DS_TIPO { get; set; }
        public string? DS_INTEGRACAO { get; set; }
        public string? DS_ROTINA { get; set; }
        public string? DS_METODO_API { get; set; }
        public string? DS_LOG { get; set; }
        public DateTime DT_LOG { get; set; }
        public long CD_REGISTRO { get; set; }
        public bool X_FALHA { get; set; }
        public string? DS_STATUS_API { get; set; }
    }
}
