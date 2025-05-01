using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app.projectRandyPenaherrera.common.Dto
{
    public class VentaDetalleDto
    {
        public int Id { get; set; }
        public int VentaId { get; set; }

        public int NumeroItem { get; set; }

        public int ProductoId { get; set; }

        public decimal PrecioUnitario { get; set; }

        public decimal Cantidad { get; set; }

        public decimal Total { get; set; }

    }
}
