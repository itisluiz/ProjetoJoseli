namespace HotelEF.Models;

using Microsoft.EntityFrameworkCore;

public class CTXHotelEF : DbContext
{
    public DbSet<MCliente> Clientes { get; set; } = null!;
    public DbSet<MConsumoLavanderia> ConsumosLavanderia { get; set; } = null!;
    public DbSet<MConsumoRefeicao> ConsumosRefeicao { get; set; } = null!;
    public DbSet<MEstadia> Estadias { get; set; } = null!;
    public DbSet<MFilial> Filiais { get; set; } = null!;
    public DbSet<MFuncionario> Funcionarios { get; set; } = null!;
    public DbSet<MNacionalidade> Nacionalidades { get; set; } = null!;
    public DbSet<MNotaFiscal> NotasFiscais { get; set; } = null!;
    public DbSet<MQuarto> Quartos { get; set; } = null!;
    public DbSet<MQuartosFilial> QuartosFiliais { get; set; } = null!;
    public DbSet<MReserva> Reservas { get; set; } = null!;
    public DbSet<MServicoLavanderia> ServicosLavanderia { get; set; } = null!;
    public DbSet<MTelefone> Telefones { get; set; } = null!;
    public DbSet<MTipoFuncionario> TiposFuncionario { get; set; } = null!;
    public DbSet<MTipoPagamento> TiposPagamento { get; set; } = null!;
    public DbSet<MTipoQuarto> TiposQuarto { get; set; } = null!;


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=DESKTOP-QPN2S96\SQLEXPRESS;Database=HotelEF;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;");
    }
}
