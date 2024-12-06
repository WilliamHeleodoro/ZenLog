namespace ZenLog.Models.ModelsController.ModelsEntradaController
{
    public class EntradaController
    {
        public long CD_ENTRADA { get; set; }
        public long CD_EMPRESA { get; set; }
        public int CD_FORNECEDOR { get; set; }
        public int CD_STATUS { get; set; }
        public string? NR_CPFCNPJ_FORNECEDOR { get; set; }
        public string? DS_RAZAOSOCIAL_FORNECEDOR { get; set; }
        public string? DS_UF_FORNECEDOR { get; set; }
        public string? CD_MUNIBGE_FORNECEDOR { get; set; }
        public string? NR_CEP_FORNECEDOR { get; set; }
        public string? DS_BAIRRO_FORNECEDOR { get; set; }
        public string? NR_NUMERO_FORNECEDOR { get; set; }
        public string? DS_ENDERECO_FORNECEDOR { get; set; }
        public string? DS_COMPLEMENTO_FORNECEDOR { get; set; }
        public string? NR_CPFCNPJ_TRANSPORTADOR { get; set; }
        public string? NR_IE_EMPRESA { get; set; }
        public string? NR_IE_FORNECEDOR { get; set; }
        public string? NR_TELEFONE_FORNECEDOR { get; set; }
        public string? CV_ACESSO { get; set; }
        public string? DS_DF_SERIE { get; set; }
        public long NR_DOCUMENTO { get; set; }
        public decimal VL_TOTAL { get; set; }
        public DateTime DT_EMISSAO { get; set; }
        public long CD_ID_ENTRADA_INTEGRACAO { get; set; }
        public required List<EntradaItensController> itensEntradaController { get; set; }
    }
}
