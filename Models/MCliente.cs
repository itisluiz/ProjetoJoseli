namespace HotelEF.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class MCliente
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Codigo { get; set; }

    [MaxLength(64)]
    public string? Nome { get; set; }

    [MaxLength(256)]
    public string? Endereco { get; set; }

    [MaxLength(128)]
    public string? Email { get; set; }
    public MNacionalidade? Nacionalidade { get; set; }
    
    public ISet<MTelefone> Telefones { get; set; } = new HashSet<MTelefone>();

    private MCliente() { }

    public MCliente(string nome, string endereco, string email, MNacionalidade nacionalidade)
    {
        this.Nome = nome;
        this.Endereco = endereco;
        this.Email = email;
        this.Nacionalidade = nacionalidade;
    }
}
