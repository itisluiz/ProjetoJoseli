namespace HotelEF;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

[PrimaryKey(nameof(CodigoFilial), nameof(CodigoTipoQuarto))]
public class MQuartosFilial
{
    public int CodigoFilial { get; set; }

    public int CodigoTipoQuarto { get; set; }

    public int Quantidade { get; set; }

    [ForeignKey("CodigoFilial")]
    public MFilial? Filial { get; set; }

    [ForeignKey("CodigoTipoQuarto")]
    public MTipoQuarto? TipoQuarto { get; set; }
}
