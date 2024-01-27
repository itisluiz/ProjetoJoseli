namespace HotelEF;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class MServicoLavanderia
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Codigo { get; set; }

    [MaxLength(64)]
    public string? Descricao { get; set; }

    [Column(TypeName = "decimal(12,2)")]
    public decimal Custo { get; set; }
}
