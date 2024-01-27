namespace HotelEF;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class MConsumoRefeicao
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Codigo { get; set; }

    public bool EntregaQuarto { get; set; }

    [Column(TypeName = "decimal(12,2)")]
    public decimal Custo { get; set; }

    [MaxLength(64)]
    public string? Descricao { get; set; }

    public DateTime Data { get; set; }

    public MEstadia? Estadia { get; set; }
}
