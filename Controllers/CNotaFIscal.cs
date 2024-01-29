namespace HotelEF.Controllers;

using HotelEF.Models;
using HotelEF.Handlers;
using HotelEF.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

[Route("api/notafiscal")]
public class CNotaFiscal : Controller
{
    private IIncludableQueryable<MNotaFiscal, MTipoPagamento?> EagerNotasFiscais(Contexts.CTXHotelEF ctx)
    {
        return ctx.NotasFiscais
            .Include(notaFiscal => notaFiscal.Estadia)
            .Include(notaFiscal => notaFiscal.TipoPagamento);
    }

    private MNotaFiscal? EagerNotasFiscais(Contexts.CTXHotelEF ctx, string? NFe)
    {
        return EagerNotasFiscais(ctx).FirstOrDefault(notaFiscal => notaFiscal.NFe == NFe);
    }

    [HttpGet]
    public ActionResult<IEnumerable<MNotaFiscal>> GetNotaFiscal([FromQuery] string? nfeNotaFiscal)
    {
        using (Contexts.CTXHotelEF ctx = new Contexts.CTXHotelEF())
        {
            if (nfeNotaFiscal == null)
                return Resultado.De(EagerNotasFiscais(ctx).ToList());
            else
            {
                MNotaFiscal? notaFiscal = EagerNotasFiscais(ctx, nfeNotaFiscal);

                if (notaFiscal == null)
                    return Resultado.De(new APINaoEncontradoException($"Nota fiscal de NFe '{nfeNotaFiscal}' não encontrada"));

                return Resultado.De(notaFiscal);
            }
        }
    }

    [HttpPut]
    public ActionResult<MNotaFiscal> PutNotaFiscal([FromForm] string nfeNotaFiscal, [FromForm] int codigoTipoPagamento, [FromForm] int codigoEstadia)
    {
        using (Contexts.CTXHotelEF ctx = new Contexts.CTXHotelEF())
        {
            MTipoPagamento? tipoPagamento = ctx.TiposPagamento.Find(codigoTipoPagamento);

            if (tipoPagamento == null)
                return Resultado.De(new APINaoEncontradoException($"Tipo de pagamento de código '{codigoTipoPagamento}' não encontrado"));

            MEstadia? estadia = ctx.Estadias.Find(codigoEstadia);

            if (estadia == null)
                return Resultado.De(new APINaoEncontradoException($"Estadia de código '{codigoEstadia}' não encontrada"));

            try
            { 
                MNotaFiscal notaFiscal = ctx.NotasFiscais.Add(new MNotaFiscal(nfeNotaFiscal, tipoPagamento, estadia)).Entity;
                ctx.SaveChanges();
                return Resultado.De(notaFiscal);
            }
            catch (Exception excecao)
            {
                return Resultado.De(excecao);
            }
        }
    }

}