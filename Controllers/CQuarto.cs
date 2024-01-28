namespace HotelEF.Controllers;

using HotelEF.Models;
using HotelEF.Handlers;
using HotelEF.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

[Route("api/quarto")]
public class CQuarto : Controller
{
    private IIncludableQueryable<MQuarto, MTipoQuarto?> EagerQuartos(Contexts.CTXHotelEF ctx)
    {
        return ctx.Quartos
            .Include(quarto => quarto.Tipo);
    }

    private MQuarto? EagerQuartos(Contexts.CTXHotelEF ctx, int? codigo)
    {
        return EagerQuartos(ctx).FirstOrDefault(quarto => quarto.Codigo == codigo);
    }
    
    [HttpGet]
    public ActionResult<IEnumerable<MQuarto>> GetQuarto([FromQuery] int? codigoQuarto)
    {
        using (Contexts.CTXHotelEF ctx = new Contexts.CTXHotelEF())
        {
            if (codigoQuarto == null)
                return Resultado.De(EagerQuartos(ctx).ToList());
            else
            {
                MQuarto? quarto = EagerQuartos(ctx, codigoQuarto);

                if (quarto == null)
                    return Resultado.De(new APINaoEncontradoException($"Quarto de código '{codigoQuarto}' não encontrado"));

                return Resultado.De(codigoQuarto);
            }
        }
    }

    [HttpPut]
    public ActionResult<MQuarto> PutQuarto([FromForm] int numero, [FromForm] bool adaptadoEspecial, [FromForm] bool reservavel, [FromForm] int capacidade, [FromForm] int codigoTipo)
    {
        using (Contexts.CTXHotelEF ctx = new Contexts.CTXHotelEF())
        {
            MTipoQuarto? tipoQuarto = ctx.TiposQuarto.Find(codigoTipo);
            
            if (tipoQuarto == null)
                return Resultado.De(new APINaoEncontradoException($"Tipo de quarto de código '{codigoTipo}' não encontrado"));

            try
            {
                MQuarto quarto = ctx.Quartos.Add(new MQuarto(numero, adaptadoEspecial, reservavel, capacidade, tipoQuarto)).Entity;
                ctx.SaveChanges();
                return Resultado.De(quarto);
            }
            catch (Exception excecao)
            {
                return Resultado.De(excecao);
            }
        }
    }

    [HttpPatch]
    public ActionResult<MQuarto> PatchQuarto([FromQuery] int codigoQuarto, [FromForm] int? numero, [FromForm] bool? adaptadoEspecial, [FromForm] bool? reservavel, [FromForm] int? capacidade, [FromForm] int? codigoTipo)
    {
        using (Contexts.CTXHotelEF ctx = new Contexts.CTXHotelEF())
        {
            MQuarto? quarto = EagerQuartos(ctx, codigoQuarto);

            if (quarto == null)
                return Resultado.De(new APINaoEncontradoException($"Quarto de código '{codigoQuarto}' não encontrado"));

            MTipoQuarto? tipo = codigoTipo != null ? ctx.TiposQuarto.Find(codigoTipo) : null;

            if (codigoTipo != null && tipo == null)
                return Resultado.De(new APINaoEncontradoException($"Tipo de quarto de código '{codigoTipo}' não encontrado"));

            try
            { 
                if (numero != null)
                    quarto.Numero = numero;

                if (adaptadoEspecial != null)
                    quarto.AdaptadoEspecial = adaptadoEspecial;

                if (reservavel != null)
                    quarto.Reservavel = reservavel;

                if (capacidade != null)
                    quarto.Capacidade = capacidade;
                 
                if (tipo != null)
                    quarto.Tipo = tipo;

                ctx.SaveChanges();
                return Resultado.De(quarto);
            }
            catch (Exception excecao)
            {
                return Resultado.De(excecao);
            }
        }
    }

    [HttpDelete]
    public ActionResult<MQuarto> DeleteQuarto([FromForm] int codigoQuarto)
    {
        using (Contexts.CTXHotelEF ctx = new Contexts.CTXHotelEF())
        {
            MQuarto? quarto = EagerQuartos(ctx, codigoQuarto);

            if (quarto == null)
                return Resultado.De(new APINaoEncontradoException($"Quarto de código '{codigoQuarto}' não encontrado"));
            try
            { 
                ctx.Quartos.Remove(quarto);
                ctx.SaveChanges();
                return Resultado.De(quarto);
            }
            catch (Exception excecao)
            {
                return Resultado.De(excecao);
            }
        }
    }
}
