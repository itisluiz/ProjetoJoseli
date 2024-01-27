namespace HotelEF;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class MConsumoLavanderia
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Codigo { get; set; }

    public DateTime Data { get; set; }

    public MEstadia? Estadia { get; set; }

    public MServicoLavanderia? ServicoLavanderia { get; set; }
}
