
namespace HotelEF.Controllers;

using HotelEF.Models;
using HotelEF.Handlers;
using HotelEF.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

[Route("api/estadia")]
public class CEstadia : Controller
{
    private IIncludableQueryable<MEstadia, MReserva?> EagerEstadias(Contexts.CTXHotelEF ctx)
    {
        return ctx.Estadias
            .Include(estadia => estadia.Reserva);
    }

    private MEstadia? EagerEstadias(Contexts.CTXHotelEF ctx, int? codigo)
    {
        return EagerEstadias(ctx).FirstOrDefault(estadia => estadia.Codigo == codigo);
    }
    
    [HttpGet]
    public ActionResult<IEnumerable<MEstadia>> GetEstadia([FromQuery] int? codigoEstadia)
    {
        using (Contexts.CTXHotelEF ctx = new Contexts.CTXHotelEF())
        {
            if (codigoEstadia == null)
                return Resultado.De(EagerEstadias(ctx).ToList());
            else
            {
                MEstadia? estadia = EagerEstadias(ctx, codigoEstadia);

                if (estadia == null)
                    return Resultado.De(new APINaoEncontradoException($"Estadia de código '{codigoEstadia}' não encontrado"));

                return Resultado.De(estadia);
            }
        }
    }

    [HttpPut]
    public ActionResult<MEstadia> PutEstadia([FromForm] int codigoReserva)
    {
        using (Contexts.CTXHotelEF ctx = new Contexts.CTXHotelEF())
        {
            MReserva? reserva = ctx.Reservas.Find(codigoReserva);

            if (reserva == null)
                return Resultado.De(new APINaoEncontradoException($"Reserva de código '{codigoReserva}' não encontrada"));

            try
            { 
                MEstadia estadia = ctx.Estadias.Add(new MEstadia(reserva)).Entity;
                ctx.SaveChanges();
                return Resultado.De(estadia);
            }
            catch (Exception excecao)
            {
                return Resultado.De(excecao);
            }
        }
    }

    [HttpPatch]
    public ActionResult<MEstadia> PatchEstadia([FromQuery] int codigoEstadia, [FromForm] DateTime? checkIn, [FromForm] DateTime? checkOut, [FromForm] int? codigoReserva)
    {
        using (Contexts.CTXHotelEF ctx = new Contexts.CTXHotelEF())
        {
            MEstadia? estadia = EagerEstadias(ctx, codigoEstadia);
            
            if (estadia == null)
                return Resultado.De(new APINaoEncontradoException($"Estadia de código '{codigoEstadia}' não encontrada"));

            MReserva? reserva = codigoReserva != null ? ctx.Reservas.Find(codigoReserva) : null;

            if (codigoReserva != null && reserva == null)
                return Resultado.De(new APINaoEncontradoException($"Reserva de código '{codigoReserva}' não encontrada"));

            try
            { 
                if (checkIn != null)
                    estadia.CheckIn = checkIn;

                if (checkOut != null)
                {
                    if (estadia.Reserva != null && estadia.CheckOut == null)
                    {
                        ctx.Entry(estadia.Reserva).Reference(reserva => reserva.Quarto).Load();

                        if (estadia.Reserva.Quarto != null)
                            estadia.Reserva.Quarto.Reservavel = true;
                    }
                    
                    estadia.CheckOut = checkOut;
                }

                if (reserva != null)
                    estadia.Reserva = reserva;

                ctx.SaveChanges();
                return Resultado.De(estadia);
            }
            catch (Exception excecao)
            {
                return Resultado.De(excecao);
            }
        }
    }

    [HttpDelete]
    public ActionResult<MEstadia> DeleteEstadia([FromForm] int codigoEstadia)
    {
        using (Contexts.CTXHotelEF ctx = new Contexts.CTXHotelEF())
        {
            MEstadia? estadia = EagerEstadias(ctx, codigoEstadia);

            if (estadia == null)
                return Resultado.De(new APINaoEncontradoException($"Estadia de código '{codigoEstadia}' não encontrado"));
            try
            { 
                ctx.Estadias.Remove(estadia);
                ctx.SaveChanges();
                return Resultado.De(estadia);
            }
            catch (Exception excecao)
            {
                return Resultado.De(excecao);
            }
        }
    }

}
