namespace HotelEF;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class MEstadia
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Codigo { get; set; }

    public DateTime? CheckIn { get; set; }

    public DateTime? CheckOut { get; set; }
    
    public MReserva? Reserva { get; set; }
}
