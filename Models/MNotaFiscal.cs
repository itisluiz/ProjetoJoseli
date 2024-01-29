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

    private MNotaFiscal() { }

    public MNotaFiscal(string NFe, MTipoPagamento tipoPagamento, MEstadia estadia)
    {
        this.NFe = NFe;
        this.TipoPagamento = tipoPagamento;
        this.Estadia = estadia;
    }
}
