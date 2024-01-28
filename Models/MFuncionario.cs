namespace HotelEF.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class MFuncionario
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Codigo { get; set; }

    [MaxLength(64)]
    public string? Nome { get; set; }

    [MaxLength(256)]
    public string? Endereco { get; set; }

    [MaxLength(128)]
    public string? Email { get; set; }

    public MTipoFuncionario? Tipo { get; set; }

    public ISet<MTelefone> Telefones { get; set; } = new HashSet<MTelefone>();

    private MFuncionario() { }

    public MFuncionario(string nome, string endereco, string email, MTipoFuncionario tipo)
    {
        this.Nome = nome;
        this.Endereco = endereco;
        this.Email = email;
        this.Tipo = tipo;
    }
}
