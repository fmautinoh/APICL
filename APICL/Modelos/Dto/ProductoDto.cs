namespace APICL.Modelos.Dto
{
    public class ProductoDto
    {
        public int IdProd { get; set; }

        public string NomProd { get; set; } = null!;

        public int IdTipPro { get; set; }

        public decimal PrecioProd { get; set; }

        public int DurProd { get; set; }
    }
}
