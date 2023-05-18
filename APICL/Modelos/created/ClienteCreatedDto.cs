using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APICL.Modelos.created
{
    public class ClienteCreatedDto
    {
        [Required]
        [MaxLength(8)]
        public string? dni_cli { get; set; }
        [MaxLength(11)]
        public string? ruc_cli { get; set; }
        [Required]
        [MaxLength(50)]
        public string? nom_cli { get; set; }
        [Required]
        [MaxLength(60)]
        public string? apat_cli { get; set; }
        [Required]
        [MaxLength(60)]
        public string? amat_cli { get; set; }
        [Required]
        [MaxLength(9)]
        public string? tel_cli { get; set; }
        [Required]
        [MaxLength(70)]
        public string? cor_cli { get; set; }
        [Required]
        [MaxLength(80)]
        public string? dir_cli { get; set; }
    }
}
