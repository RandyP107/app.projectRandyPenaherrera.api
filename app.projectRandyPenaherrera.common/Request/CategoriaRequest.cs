using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app.projectRandyPenaherrera.common.Request
{
    public class CategoriaRequest
    {
        [Required(ErrorMessage = "El campo Name es obligatorio")]
        public string? Nombre { get; set; }

        [StringLength(20, ErrorMessage = "El ancho del campo es muy largo")]
        public string? Descripcion { get; set; }
    }
}
