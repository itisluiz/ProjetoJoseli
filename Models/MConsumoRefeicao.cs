namespace HotelEF.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class MConsumoRefeicao
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Codigo { get; set; }

    public bool? EntregaQuarto { get; set; }

    [Column(TypeName = "decimal(12,2)")]
    public decimal? Custo { get; set; }

    [MaxLength(64)]
    public string? Descricao { get; set; }

    public DateTime? Data { get; set; }

    public MEstadia? Estadia { get; set; }

    private MConsumoRefeicao() { }

    public MConsumoRefeicao(bool entregaQuarto, decimal custo, string descricao, MEstadia estadia)
    {
        this.EntregaQuarto = entregaQuarto;
        this.Custo = custo;
        this.Descricao = descricao;
        this.Data = DateTime.Now;
        this.Estadia = estadia;
    }
}
