using System.ComponentModel.DataAnnotations;

namespace ZenLog.Models.ModelsController.ModelsPedidoController
{
    public class PedidoController
    {

        [Required]
        public long CD_PEDIDO { get; set; }
        public long CD_EMPRESA { get; set; }
        public int CD_CLIENTE { get; set; }
        public int CD_STATUS { get; set; }
        public  string? NR_CPFCNPJ_CLIENTE { get; set; }
        public  string? DS_RAZAOSOCIAL_CLIENTE { get; set; }
        public  string? DS_UF_CLIENTE { get; set; }
        public  string? CD_MUNIBGE_CLIENTE { get; set; }
        public  string? NR_CEP_CLIENTE { get; set; }
        public  string? DS_BAIRRO_CLIENTE { get; set; }
        public  string? NR_NUMERO_CLIENTE { get; set; }
        public  string? DS_ENDERECO_CLIENTE { get; set; }
        public  string? DS_COMPLEMENTO_CLIENTE { get; set; }
        public  string? NR_CPFCNPJ_TRANSPORTADOR { get; set; }
        public  string? NR_IE_EMPRESA { get; set; }
        public long NR_NOTAFISCAL { get; set; }
        public string? DS_DF_SERIE { get; set; }
        public string? CV_ACESSO { get; set; }
        public DateTime DT_EMISSAO { get; set; }
        public decimal? VL_PEDIDO { get; set; }
        public long CD_ID_PEDIDO_INTEGRACAO { get; set; }
        public List<PedidoItensController>? servicosController { get; set; }
        public  List<PedidoItensController>? itensController { get; set; }
        public  List<PedidoParcelasController>? parcelasController { get; set; }

    }
}
