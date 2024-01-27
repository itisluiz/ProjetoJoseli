namespace HotelEF;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class MTelefone
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Codigo { get; set; }

    [MaxLength(24)]
    public string? Numero { get; set; }   
}