
namespace ZenLog.Models.ModelsController.ModelsPedidoController
{
    public class PedidoItensController
    {
        public long CD_PEDIDO { get; set; }
        public int CD_ITEM { get; set; }
        public long CD_MATERIAL { get; set; }
        public string? DS_MATERIAL { get; set; }
        public string? DS_UNIDADE { get; set; }
        public decimal NR_QUANTIDADE { get; set; }
        public decimal VL_UNITARIO { get; set; }
        public string? DS_CFOP { get; set; }
        public decimal VL_BASE_ISS { get; set; }
        public decimal AL_ISS { get; set; }
        public decimal VL_ISS { get; set; }
        public bool X_ISS_RETIDO { get; set; }
    }
}
