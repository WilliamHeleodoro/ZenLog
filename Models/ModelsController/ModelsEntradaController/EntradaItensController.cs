namespace ZenLog.Models.ModelsController.ModelsEntradaController
{
    public class EntradaItensController
    {
        public long CD_ENTRADA { get; set; }
        public int CD_ITEM { get; set; }
        public long CD_MATERIAL { get; set; }
        public string? DS_MATERIAL { get; set; }
        public string? DS_UNIDADE { get; set; }
        public string? DS_CFOP { get; set; }
        public decimal NR_QUANTIDADE { get; set; }
        public decimal VL_UNITARIO { get; set; }
    }
}
