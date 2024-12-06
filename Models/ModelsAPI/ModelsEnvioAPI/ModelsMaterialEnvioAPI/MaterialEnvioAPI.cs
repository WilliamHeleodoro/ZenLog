
namespace ZenLog.Models.ModelsAPI.ModelsMaterialEnvioAPI.MaterialEnvioAPI
{
    public class MaterialEnvioAPI
    {
        public required string cpfCnpj { get; set; }
        public required string codigo { get; set; }
        public required string cpfCnpjDepostante { get; set; }
        public required string codigoCategoria { get; set; }
        public required string descricao { get; set; }
        public required string codigoUnidadeMedida { get; set; }
        public required string classificacaoFiscalNCM { get; set; }
        public required int qtdItemSKU { get; set; }
        public required string descricaoEmbalagem { get; set; }
        public required double pesoSku { get; set; }
        public string? altura { get; set; }
        public string? largura { get; set; }
        public string? comprimento { get; set; }
    }
}
