namespace ZenLog.Models.ModelsAPI.ModelsRetornoAPI
{
    public class RetornoPedidoExpedicao
    {
        public int totalCount { get; set; }
        public  required List<Items> items { get; set; }
    }

    public class Items 
    {
        public required string codigo { get; set; }
        public string? situacao { get; set; }
        public int situacaoIntegracao { get; set; }
        public string? data { get; set; }
        public required List<Documentos> documentos { get; set; } 
    }

    public class Documentos
    {
        public required string pedido { get; set; }
        public decimal quantidadeVolumes { get; set; }
        public decimal pesoLiquido { get; set; }
        public decimal pesoBruto { get; set; }
        public decimal m3 { get; set; }
    }
}
