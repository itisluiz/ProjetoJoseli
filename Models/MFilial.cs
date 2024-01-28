namespace HotelEF.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class MFilial
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Codigo { get; set; }

    [MaxLength(64)]
    public string? Nome { get; set; }

    [MaxLength(256)]
    public string? Endereco { get; set; }

    public float? Estrelas { get; set; }

    public ICollection<MQuartosFilial> Quartos { get; set; } = new List<MQuartosFilial>();

    private MFilial() { }

    public MFilial(string nome, string endereco, float estrelas)
    {
        this.Nome = nome;
        this.Endereco = endereco;
        this.Estrelas = estrelas;
    }
}
