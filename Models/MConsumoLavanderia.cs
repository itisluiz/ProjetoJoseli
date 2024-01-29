namespace HotelEF.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class MConsumoLavanderia
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Codigo { get; set; }

    public DateTime? Data { get; set; }

    public MEstadia? Estadia { get; set; }

    public MServicoLavanderia? ServicoLavanderia { get; set; }

    private MConsumoLavanderia() { }

    public MConsumoLavanderia(MEstadia estadia, MServicoLavanderia servicoLavanderia)
    {
        this.Data = DateTime.Now;
        this.Estadia = estadia;
        this.ServicoLavanderia = servicoLavanderia;
    }
}
