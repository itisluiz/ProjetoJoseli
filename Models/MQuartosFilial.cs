namespace HotelEF.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

public class MQuartosFilial
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Codigo { get; set; }
    
    public int Quantidade { get; set; }

    public MTipoQuarto? TipoQuarto { get; set; }

    private MQuartosFilial() { }

    public MQuartosFilial(MTipoQuarto tipoQuarto, int quantidade)
    {
        this.TipoQuarto = tipoQuarto;
        this.Quantidade = quantidade;
    }
}
