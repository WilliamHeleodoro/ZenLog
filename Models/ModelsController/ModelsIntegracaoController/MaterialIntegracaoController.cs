using Dapper.Contrib.Extensions;

namespace ZenLog.Models.ModelsController.ModelsIntegracaoController
{
    [Table("TBL_MATERIAIS_INTEGRACAO_ENVIO")]
    public class MaterialIntegracaoController
    {
        [Key]
        public long CD_ID { get; set; }
        public long CD_MATERIAL { get; set; }
        public string? CD_MATERIAL_INTEGRACAO { get; set; }
        public string? DS_TIPO { get; set; }
        public string? DS_INTEGRACAO { get; set; }
        public DateTime DT_ENVIO { get; set; }
        public DateTime DT_ATUALIZACAO { get; set; }
        public bool X_ENVIADO { get; set; }

    }
}
