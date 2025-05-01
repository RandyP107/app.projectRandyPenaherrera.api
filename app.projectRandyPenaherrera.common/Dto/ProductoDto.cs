using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app.projectRandyPenaherrera.common.Dto
{
    public class ProductoDto
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public int CategoriaId { get; set; }
        public decimal PrecioUnitario { get; set; }
    }
}
