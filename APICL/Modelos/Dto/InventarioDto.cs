using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace APICL.Modelos.Dto
{
    public class InventarioDto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdInv { get; set; }

        public int IdProd { get; set; }

        public int StokPro { get; set; }

        public int IdPresent { get; set; }

        public int EstadoInv { get; set; }
    }
}
