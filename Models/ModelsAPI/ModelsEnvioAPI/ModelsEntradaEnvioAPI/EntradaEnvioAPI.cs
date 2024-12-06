namespace ZenLog.Models.ModelsAPI.ModelsEnvioAPI.ModelsEntradaEnvioAPI
{
    public class EntradaEnvioAPI
    {
        public string? Codigo { get; set; }
        public string? CpfCnpj { get; set; }
        public string? CpfCnpjDepositante { get; set; }
        public string? CpfCnpjFornecedor { get; set; }
        public string? InscricaoEstadualFornecedor { get; set; }
        public string? RazaoSocialFornecedor { get; set; }
        public string? UfFornecedor { get; set; }
        public string? CidadeIbgeFornecedor { get; set; }
        public string? CepFornecedor { get; set; }
        public string? BairroFornecedor { get; set; }
        public string? RuaFornecedor { get; set; }
        public string? NumeroFornecedor { get; set; }
        public string? TelefoneFornecedor { get; set; }
        public bool SituacaoFornecedor { get; set; }
        public string? LogradouroFornecedor { get; set; }
        public string? TipoFornecedor { get; set; }
        public string? Tipo { get; set; }
        public string? NumeroNf { get; set; }
        public string? Serie { get; set; }
        public string? DataEmissao { get; set; }
        public string? Cfop { get; set; }
        public string? Chave { get; set; }
        public decimal ValorTotal { get; set; }
        public string? especieNfe { get; set; }
        public required List<EntradaItensEnvioAPI> produtos { get; set; }

    }
}
