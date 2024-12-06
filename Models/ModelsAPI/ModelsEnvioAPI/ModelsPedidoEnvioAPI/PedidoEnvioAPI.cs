
using System.Text.Json.Serialization;

namespace ZenLog.Models.ModelsAPI.ModelsEnvioAPI.ModelsPedidoEnvioAPI
{
    public class PedidoEnvioAPI
    {
        public string? CpfCnpj { get; set; }
        public string? CpfCnpjDepositante { get; set; }
        public string? InscricaoEstadualDepositante { get; set; }
        public string? Pedido { get; set; }
        public string? NumeroNf { get; set; }
        public string? Serie { get; set; }
        public string? ChaveAcesso { get; set; }
        public string? Cfop { get; set; }
        public string? CpfCnpjDestinatario { get; set; }
        public string? RazaoSocialDestinatario { get; set; }
        public string? UfDestinatario { get; set; }
        public string? CidadeIbgeDestinatario { get; set; }
        public string? CepDestinatario { get; set; }
        public string? BairroDestinatario { get; set; }
        public string? NumeroDestinatario { get; set; }
        public string? RuaDestinatario { get; set; }
        public string? ComplementoDestinatario { get; set; }
        public string? CpfCnpjTransportador { get; set; }
        public DateTime DataEmissao { get; set; }
        public required List<PedidoMaterialEnvioAPI> produtos { get; set; }
    }                
}                    
