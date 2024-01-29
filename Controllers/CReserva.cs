namespace HotelEF.Controllers;

using HotelEF.Models;
using HotelEF.Handlers;
using HotelEF.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

[Route("api/reserva")]
public class CReserva : Controller
{
    private IIncludableQueryable<MReserva, MQuarto?> EagerReserva(Contexts.CTXHotelEF ctx)
    {
        return ctx.Reservas
            .Include(reserva => reserva.Cliente)
            .Include(reserva => reserva.Funcionario)
            .Include(reserva => reserva.Quarto);
    }

    private MReserva? EagerReserva(Contexts.CTXHotelEF ctx, int? codigo)
    {
        return EagerReserva(ctx).FirstOrDefault(reserva => reserva.Codigo == codigo);
    }

    [HttpGet]
    public ActionResult<IEnumerable<MReserva>> GetReserva([FromQuery] int? codigoReserva)
    {
        using (Contexts.CTXHotelEF ctx = new Contexts.CTXHotelEF())
        {
            if (codigoReserva == null)
                return Resultado.De(EagerReserva(ctx).ToList());
            else
            {
                MReserva? reserva = EagerReserva(ctx, codigoReserva);

                if (reserva == null)
                    return Resultado.De(new APINaoEncontradoException($"Reserva de código '{codigoReserva}' não encontrada"));

                return Resultado.De(reserva);
            }
        }
    }

    [HttpPut]
    public ActionResult<MReserva> PutReserva([FromForm] DateTime prevista, [FromForm] int codigoQuarto, [FromForm] int codigoCliente, [FromForm] int codigoFuncionario)
    {
        using (Contexts.CTXHotelEF ctx = new Contexts.CTXHotelEF())
        {
            MQuarto? quarto = ctx.Quartos.Find(codigoQuarto);

            if (quarto == null)
                return Resultado.De(new APINaoEncontradoException($"Quarto de código '{codigoQuarto}' não encontrado"));
            else if (!quarto.Reservavel)
                return Resultado.De(new APINaoEncontradoException($"Quarto de código '{codigoQuarto}' não está em estado reservável"));

            MCliente? cliente = ctx.Clientes.Find(codigoCliente);
 
            if (cliente == null)
                return Resultado.De(new APINaoEncontradoException($"Cliente de código '{codigoCliente}' não encontrado"));

            MFuncionario? funcionario = ctx.Funcionarios.Find(codigoFuncionario);

            if (funcionario == null)
                return Resultado.De(new APINaoEncontradoException($"Funcionário de código '{codigoFuncionario}' não encontrado"));

            try
            {
                MReserva reserva = ctx.Reservas.Add(new MReserva(prevista, quarto, cliente, funcionario)).Entity;
                quarto.Reservavel = false;

                ctx.SaveChanges();
                return Resultado.De(cliente);
            }
            catch (Exception excecao)
            {
                return Resultado.De(excecao);
            }
        }
    }

    [HttpPatch]
    public ActionResult<MReserva> PatchReserva([FromQuery] int codigoReserva, [FromForm] DateTime? prevista, [FromForm] int? codigoQuarto, [FromForm] int? codigoCliente, [FromForm] int? codigoFuncionario)
    {
        using (Contexts.CTXHotelEF ctx = new Contexts.CTXHotelEF())
        {
            MReserva? reserva = EagerReserva(ctx, codigoReserva);

            if (reserva == null)
                return Resultado.De(new APINaoEncontradoException($"Reserva de código '{codigoReserva}' não encontrada"));

            MQuarto? quarto = codigoQuarto != null ? ctx.Quartos.Find(codigoQuarto) : null;

            if (codigoQuarto != null && quarto == null)
                return Resultado.De(new APINaoEncontradoException($"Quarto de código '{codigoQuarto}' não encontrado"));
            else if (quarto != null && !quarto.Reservavel)
                return Resultado.De(new APINaoEncontradoException($"Quarto de código '{codigoQuarto}' não está em estado reservável"));

            MCliente? cliente = codigoCliente != null ? ctx.Clientes.Find(codigoCliente) : null;

            if (codigoCliente != null && cliente == null)
                return Resultado.De(new APINaoEncontradoException($"Cliente de código '{codigoCliente}' não encontrado"));

            MFuncionario? funcionario = codigoFuncionario != null ? ctx.Funcionarios.Find(codigoFuncionario) : null;

            if (codigoFuncionario != null && funcionario == null)
                return Resultado.De(new APINaoEncontradoException($"Funcionário de código '{codigoCliente}' não encontrado"));

            try
            { 
                if (prevista != null)
                    reserva.Prevista = prevista;

                if (quarto != null)
                {
                    if (reserva.Quarto != null)
                        reserva.Quarto.Reservavel = true;

                    reserva.Quarto = quarto;
                    quarto.Reservavel = false;
                }

                if (cliente != null)
                    reserva.Cliente = cliente;

                if (funcionario != null)
                    reserva.Funcionario = funcionario;
                 
                ctx.SaveChanges();
                return Resultado.De(reserva);
            }
            catch (Exception excecao)
            {
                return Resultado.De(excecao);
            }
        }
    }


    [HttpDelete]
    public ActionResult<MReserva> DeleteReserva([FromForm] int codigoReserva)
    {
        using (Contexts.CTXHotelEF ctx = new Contexts.CTXHotelEF())
        {
            MReserva? reserva = EagerReserva(ctx, codigoReserva);

            if (reserva == null)
                return Resultado.De(new APINaoEncontradoException($"Reserva de código '{codigoReserva}' não encontrada"));
            try
            { 
                ctx.Reservas.Remove(reserva);
                ctx.SaveChanges();
                return Resultado.De(reserva);
            }
            catch (Exception excecao)
            {
                return Resultado.De(excecao);
            }
        }
    }
}
