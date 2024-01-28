namespace HotelEF.Controllers;

using HotelEF.Models;
using HotelEF.Handlers;
using HotelEF.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

[Route("api/filial")]
public class CFilial : Controller
{
    private IIncludableQueryable<MFilial, MTipoQuarto?> EagerFiliais(Contexts.CTXHotelEF ctx)
    {
        return ctx.Filiais
            .Include(filial => filial.Quartos)
            .ThenInclude(quartos => quartos.TipoQuarto);
    }

    private MFilial? EagerFiliais(Contexts.CTXHotelEF ctx, int? codigo)
    {
        return EagerFiliais(ctx).FirstOrDefault(filial => filial.Codigo == codigo);
    }

    [HttpGet]
    public ActionResult<IEnumerable<MFilial>> GetFilial([FromQuery] int? codigoFilial)
    {
        using (Contexts.CTXHotelEF ctx = new Contexts.CTXHotelEF())
        {
            if (codigoFilial == null)
                return Resultado.De(EagerFiliais(ctx).ToList());
            else
            {
                MFilial? filial = EagerFiliais(ctx, codigoFilial);

                if (filial == null)
                    return Resultado.De(new APINaoEncontradoException($"Filial de código '{codigoFilial}' não encontrada"));

                return Resultado.De(filial);
            }
        }
    }

    [HttpPut]
    public ActionResult<MFilial> PutFilial([FromForm] string nome, [FromForm] string endereco, [FromForm] float estrelas)
    {
        using (Contexts.CTXHotelEF ctx = new Contexts.CTXHotelEF())
        {
            try
            { 
                MFilial filial = ctx.Filiais.Add(new MFilial(nome, endereco, estrelas)).Entity;
                ctx.SaveChanges();
                return Resultado.De(filial);
            }
            catch (Exception excecao)
            {
                return Resultado.De(excecao);
            }
        }
    }

    [HttpPatch]
    public ActionResult<MFilial> PatchFilial([FromQuery] int codigoFilial, [FromForm] string? nome, [FromForm] string? endereco, [FromForm] float? estrelas)
    {
        using (Contexts.CTXHotelEF ctx = new Contexts.CTXHotelEF())
        {
            MFilial? filial = EagerFiliais(ctx, codigoFilial);

            if (filial == null)
                return Resultado.De(new APINaoEncontradoException($"Filial de código '{codigoFilial}' não encontrada"));

            try
            { 
                if (nome != null)
                    filial.Nome = nome;

                if (endereco != null)
                    filial.Endereco = endereco;

                if (estrelas != null)
                    filial.Estrelas = estrelas;

                ctx.SaveChanges();
                return Resultado.De(filial);
            }
            catch (Exception excecao)
            {
                return Resultado.De(excecao);
            }
        }
    }

    [HttpDelete]
    public ActionResult<MFilial> DeleteFilial([FromForm] int codigoFilial)
    {
        using (Contexts.CTXHotelEF ctx = new Contexts.CTXHotelEF())
        {
            MFilial? filial = EagerFiliais(ctx, codigoFilial);

            if (filial == null)
                return Resultado.De(new APINaoEncontradoException($"Filial de código '{codigoFilial}' não encontrada"));
            try
            { 
                ctx.Filiais.Remove(filial);
                ctx.SaveChanges();
                return Resultado.De(filial);
            }
            catch (Exception excecao)
            {
                return Resultado.De(excecao);
            }
        }
    }

#region quartos
    [HttpPost("quartos")]
    public ActionResult<MQuartosFilial> PostFilialQuartos([FromQuery] int codigoFilial, [FromForm] int codigoTipoQuarto, [FromForm] int quantidade)
    {
        using (Contexts.CTXHotelEF ctx = new Contexts.CTXHotelEF())
        {
            MFilial? filial = EagerFiliais(ctx, codigoFilial);

            if (filial == null)
                return Resultado.De(new APINaoEncontradoException($"Filial de código '{codigoFilial}' não encontrada"));

            MTipoQuarto? tipoQuarto = ctx.TiposQuarto.Find(codigoTipoQuarto);

            if (tipoQuarto == null)
                return Resultado.De(new APINaoEncontradoException($"Tipo de quarto de código '{codigoTipoQuarto}' não encontrado"));

            try
            {
                MQuartosFilial? quartosFilial = filial.Quartos.FirstOrDefault(quartoFilial => quartoFilial.TipoQuarto == tipoQuarto);

                if (quartosFilial == null)
                {
                    if (quantidade <= 0)
                        return Resultado.De(new APINaoEncontradoException($"Impossível remover tipo de quarto '{codigoTipoQuarto}' não atribuido"));

                    quartosFilial = new MQuartosFilial(tipoQuarto, quantidade);
                    filial.Quartos.Add(quartosFilial);
                }
                else
                {
                    if (quantidade <= 0)
                        ctx.QuartosFiliais.Remove(quartosFilial);
                    else
                        quartosFilial.Quantidade = quantidade;
                }
                    
                ctx.SaveChanges();
                return Resultado.De(quartosFilial);
            }
            catch (Exception excecao)
            {
                return Resultado.De(excecao);
            }
        }
    }
#endregion

}