using Dapper.Contrib.Extensions;

namespace ZenLog.Models.ModelsController.ModelsIntegracaoController
{
    [Table("TBL_PEDIDOS_INTEGRACAO_ENVIO")]
    public class PedidoIntegracaoController
    {
        [Key]
        public long CD_ID { get; set; }
        public long CD_PEDIDO { get; set; }
        public string? NR_NOTAFISCAL { get; set; }
        public string? DS_TIPO { get; set; }
        public string? DS_INTEGRACAO { get; set; }
        public DateTime DT_ENVIO { get; set; }
        public bool X_ENVIADO { get; set; }
        public int CD_STATUS { get; set; }
        public string? DS_STATUS_API { get; set; }
    }
}
