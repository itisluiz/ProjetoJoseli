namespace HotelEF.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class MTelefone
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Codigo { get; set; }

    [MaxLength(24)]
    public string? Numero { get; set; }

    private MTelefone() { }

    public MTelefone(string numero)
    {
        this.Numero = numero;
    }
}
