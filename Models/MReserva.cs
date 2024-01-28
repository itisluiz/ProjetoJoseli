namespace HotelEF.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class MReserva
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Codigo { get; set; }

    public DateTime? Criacao { get; set; }

    public DateTime? Prevista { get; set; }
    
    public MQuarto? Quarto { get; set; }

    public MCliente? Cliente { get; set; }

    public MFuncionario? Funcionario { get; set; }

    private MReserva() { }

    public MReserva(DateTime prevista, MQuarto quarto, MCliente cliente, MFuncionario funcionario)
    {
        this.Criacao = DateTime.Now;
        this.Prevista = prevista;
        this.Quarto = quarto;
        this.Cliente = cliente;
        this.Funcionario = funcionario;
    }
}
