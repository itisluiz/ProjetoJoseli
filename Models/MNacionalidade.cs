namespace HotelEF.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class MNacionalidade
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Codigo { get; set; }

    [MaxLength(64)]
    public string? Pais { get; set; }   

    [MaxLength(64)]
    public string? Titulo { get; set; }   

    [StringLength(2)]
    public string? Sigla { get; set; }
}
