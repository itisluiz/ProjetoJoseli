namespace HotelEF.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class MNotaFiscal
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [StringLength(44)]
    public string? NFe { get; set; }

    public MTipoPagamento? TipoPagamento { get; set; }

    public MEstadia? Estadia { get; set; }
}
