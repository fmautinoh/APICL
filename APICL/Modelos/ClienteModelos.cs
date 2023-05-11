using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APICL.Modelos
{
    public class ClienteModelos
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_cli { get; set; }
        [Required]
        public int dni_cli { get; set; }
        public int ruc_cli { get; set; }
        [Required]
        public string? nom_cli { get; set; }
        [Required]
        public string? apat_cli { get; set; }
        [Required]
        public string? amat_cli { get; set; }
        [Required]
        public string? tel_cli { get; set; }
        [Required]
        public string? cor_cli { get; set; }
        [Required]
        public string? dir_cli { get; set; }
    }
}
