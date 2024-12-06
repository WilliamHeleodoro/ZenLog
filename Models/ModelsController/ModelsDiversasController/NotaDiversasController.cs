namespace ZenLog.Models.ModelsController.ModelsDiversasController
{
    public class NotaDiversasController
    {
        public long CD_LANCAMENTO { get; set; }
        public long CD_EMPRESA { get; set; }
        public int CD_ENTIDADE { get; set; }
        public int CD_STATUS { get; set; }
        public string? NR_CPFCNPJ_ENTIDADE { get; set; }
        public string? DS_RAZAOSOCIAL_ENTIDADE { get; set; }
        public string? DS_UF_ENTIDADE { get; set; }
        public string? CD_MUNIBGE_ENTIDADE { get; set; }
        public string? NR_CEP_ENTIDADE { get; set; }
        public string? DS_BAIRRO_ENTIDADE { get; set; }
        public string? NR_NUMERO_ENTIDADE { get; set; }
        public string? DS_ENDERECO_ENTIDADE { get; set; }
        public string? DS_COMPLEMENTO_ENTIDADE { get; set; }
        public string? NR_CPFCNPJ_TRANSPORTADOR { get; set; }
        public string? NR_IE_EMPRESA { get; set; }
        public long NR_DOCUMENTO { get; set; }
        public string? CV_ACESSO { get; set; }
        public string? DS_DF_SERIE { get; set; }
        public DateTime DT_EMISSAO { get; set; }
        public List<NotaDiversasItensController>? itensDiversasController { get; set; }
    }
}
