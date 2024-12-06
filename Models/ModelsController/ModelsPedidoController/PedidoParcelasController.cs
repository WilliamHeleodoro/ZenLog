using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZenLog.Models.ModelsController.ModelsPedidoController
{
    public class PedidoParcelasController
    {
        public long CD_PEDIDO { get; set; }
        public int NR_PARCELA { get; set; }
        public DateTime DT_VENCIMENTO { get; set; }
        public decimal VL_PARCELA { set; get; }
    }
}
