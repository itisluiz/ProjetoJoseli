namespace HotelEF;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class MQuarto
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Codigo { get; set; }

    public int Numero { get; set; }

    public bool AdaptadoEspecial { get; set; }

    public bool Reservavel { get; set; }

    public int Capacidade { get; set; }
    
    public MTipoQuarto? Tipo { get; set; }
}
