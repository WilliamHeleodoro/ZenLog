namespace ZenLog.Models.ModelsAPI.ModelsEnvioAPI.ModelsEntradaEnvioAPI
{
    public class EntradaItensEnvioAPI
    {
        public string? CodigoProduto { get; set; }
        public string? NumeroItem { get; set; }
        public string? codigoUnidadeMedida { get; set; }
        public string? NumeroPedido { get; set; }
        public decimal Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal ValorTotalItem { get; set; }

    }
}
