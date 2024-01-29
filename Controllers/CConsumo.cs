namespace HotelEF.Controllers;

using HotelEF.Models;
using HotelEF.Handlers;
using HotelEF.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

[Route("api/consumo")]
public class CConsumo : Controller
{
    private struct Consumo
    {
        public string Tipo { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }

        public Consumo(string tipo, string descricao, decimal valor)
        {
            this.Tipo = tipo;
            this.Descricao = descricao;
            this.Valor = valor;
        }
    }

    [HttpGet]
    public ActionResult<IEnumerable<object>> GetConsumoEstadia([FromQuery] int codigoEstadia)
    {
        using (Contexts.CTXHotelEF ctx = new Contexts.CTXHotelEF())
        {
            MEstadia? estadia = ctx.Estadias.Find(codigoEstadia);

            if (estadia == null)
                return Resultado.De(new APINaoEncontradoException($"Estadia de código '{codigoEstadia}' não encontrada"));

            IEnumerable<MConsumoLavanderia> consumosLavanderia = ctx.ConsumosLavanderia.Where(consumoLavenderia => consumoLavenderia.Estadia == estadia)
                .Include(consumoLavenderia => consumoLavenderia.ServicoLavanderia);

            IEnumerable<MConsumoRefeicao> consumosRefeicao = ctx.ConsumosRefeicao.Where(consumosRefeicao => consumosRefeicao.Estadia == estadia);

            decimal custoEstadia = 0;
            List<Consumo> consumos = new List<Consumo>();

            foreach (MConsumoLavanderia consumoLavanderia in consumosLavanderia)
            {
                MServicoLavanderia? servico = consumoLavanderia.ServicoLavanderia;

                if (servico == null|| servico.Descricao == null)
                    continue;

                custoEstadia += servico.Custo;
                consumos.Add(new Consumo("Lavanderia", servico.Descricao, servico.Custo));
            }

            foreach (MConsumoRefeicao consumoRefeicao in consumosRefeicao)
            {
                if (consumoRefeicao == null || consumoRefeicao.Descricao == null)
                    continue;

                custoEstadia += consumoRefeicao.Custo;
                consumos.Add(new Consumo("Refeição", consumoRefeicao.Descricao, consumoRefeicao.Custo));
            }

            consumos.Add(new Consumo("Total", "Totalização dos consumos acima", custoEstadia));
            return Resultado.De(consumos);
        }
    }

#region ConsumoRefeicao
    private IIncludableQueryable<MConsumoRefeicao, MEstadia?> EagerConsumosRefeicao(Contexts.CTXHotelEF ctx)
    {
        return ctx.ConsumosRefeicao
            .Include(consumoRefeicao => consumoRefeicao.Estadia);
    }

    private MConsumoRefeicao? EagerConsumosRefeicao(Contexts.CTXHotelEF ctx, int? codigo)
    {
        return EagerConsumosRefeicao(ctx).FirstOrDefault(consumoRefeicao => consumoRefeicao.Codigo == codigo);
    }

    [HttpGet("refeicao")]
    public ActionResult<IEnumerable<MConsumoRefeicao>> GetConsumoRefeicao([FromQuery] int? codigoConsumoRefeicao)
    {
        using (Contexts.CTXHotelEF ctx = new Contexts.CTXHotelEF())
        {
            if (codigoConsumoRefeicao == null)
                return Resultado.De(EagerConsumosRefeicao(ctx).ToList());
            else
            {
                MConsumoRefeicao? consumoRefeicao = EagerConsumosRefeicao(ctx, codigoConsumoRefeicao);

                if (consumoRefeicao == null)
                    return Resultado.De(new APINaoEncontradoException($"Consumo de refeição de código '{codigoConsumoRefeicao}' não encontrado"));

                return Resultado.De(consumoRefeicao);
            }
        }
    }

    [HttpPut("refeicao")]
    public ActionResult<MConsumoRefeicao> PutConsumoRefeicao([FromForm] bool entregaQuarto, [FromForm] decimal custo, [FromForm] string descricao, [FromForm] int codigoEstadia)
    {
        using (Contexts.CTXHotelEF ctx = new Contexts.CTXHotelEF())
        {
            MEstadia? estadia = ctx.Estadias.Find(codigoEstadia);

            if (estadia == null)
                return Resultado.De(new APINaoEncontradoException($"Estadia de código '{codigoEstadia}' não encontrada"));

            try
            { 
                MConsumoRefeicao consumoRefeicao = ctx.ConsumosRefeicao.Add(new MConsumoRefeicao(entregaQuarto, custo, descricao, estadia)).Entity;
                ctx.SaveChanges();
                return Resultado.De(consumoRefeicao);
            }
            catch (Exception excecao)
            {
                return Resultado.De(excecao);
            }
        }
    }

    [HttpPatch("refeicao")]
    public ActionResult<MConsumoRefeicao> PatchConsumoRefeicao([FromQuery] int codigoConsumoRefeicao, [FromForm] bool? entregaQuarto, [FromForm] decimal? custo, [FromForm] string? descricao, [FromForm] DateTime? data, [FromForm] int? codigoEstadia)
    {
        using (Contexts.CTXHotelEF ctx = new Contexts.CTXHotelEF())
        {
            MConsumoRefeicao? consumoRefeicao = EagerConsumosRefeicao(ctx, codigoConsumoRefeicao);

            if (consumoRefeicao == null)
                return Resultado.De(new APINaoEncontradoException($"Consumo de refeição de código '{codigoConsumoRefeicao}' não encontrado"));

            MEstadia? estadia = codigoEstadia != null ? ctx.Estadias.Find(codigoEstadia) : null;

            if (codigoEstadia != null && estadia == null)
                return Resultado.De(new APINaoEncontradoException($"Estadia de código '{codigoEstadia}' não encontrada"));

            try
            { 
                if (entregaQuarto != null)
                    consumoRefeicao.EntregaQuarto = entregaQuarto;

                if (custo != null)
                    consumoRefeicao.Custo = custo.Value;

                if (descricao != null)
                    consumoRefeicao.Descricao = descricao;

                if (data != null)
                    consumoRefeicao.Data = data;
                    
                if (estadia != null)
                    consumoRefeicao.Estadia = estadia;

                ctx.SaveChanges();
                return Resultado.De(consumoRefeicao);
            }
            catch (Exception excecao)
            {
                return Resultado.De(excecao);
            }
        }
    }

    [HttpDelete("refeicao")]
    public ActionResult<MConsumoRefeicao> DeleteConsumoRefeicao([FromForm] int codigoConsumoRefeicao)
    {
        using (Contexts.CTXHotelEF ctx = new Contexts.CTXHotelEF())
        {
            MConsumoRefeicao? consumoRefeicao = EagerConsumosRefeicao(ctx, codigoConsumoRefeicao);

            if (consumoRefeicao == null)
                return Resultado.De(new APINaoEncontradoException($"Consumo de refeição de código '{codigoConsumoRefeicao}' não encontrado"));
            try
            { 
                ctx.ConsumosRefeicao.Remove(consumoRefeicao);
                ctx.SaveChanges();
                return Resultado.De(consumoRefeicao);
            }
            catch (Exception excecao)
            {
                return Resultado.De(excecao);
            }
        }
    }
#endregion

#region ConsumoLavanderia
    private IIncludableQueryable<MConsumoLavanderia, MServicoLavanderia?> EagerConsumosLavanderia(Contexts.CTXHotelEF ctx)
    {
        return ctx.ConsumosLavanderia
            .Include(consumoLavanderia => consumoLavanderia.Estadia)
            .Include(consumoLavanderia => consumoLavanderia.ServicoLavanderia);
    }

    private MConsumoLavanderia? EagerConsumosLavanderia(Contexts.CTXHotelEF ctx, int? codigo)
    {
        return EagerConsumosLavanderia(ctx).FirstOrDefault(consumoLavanderia => consumoLavanderia.Codigo == codigo);
    }

    [HttpGet("lavanderia")]
    public ActionResult<IEnumerable<MConsumoLavanderia>> GetConsumoLavanderia([FromQuery] int? codigoConsumoLavanderia)
    {
        using (Contexts.CTXHotelEF ctx = new Contexts.CTXHotelEF())
        {
            if (codigoConsumoLavanderia == null)
                return Resultado.De(EagerConsumosLavanderia(ctx).ToList());
            else
            {
                MConsumoLavanderia? consumoLavanderia = EagerConsumosLavanderia(ctx, codigoConsumoLavanderia);

                if (consumoLavanderia == null)
                    return Resultado.De(new APINaoEncontradoException($"Consumo na lavanderia de código '{codigoConsumoLavanderia}' não encontrado"));

                return Resultado.De(consumoLavanderia);
            }
        }
    }

    [HttpPut("lavanderia")]
    public ActionResult<MConsumoLavanderia> PutConsumoLavanderia([FromForm] int codigoEstadia, [FromForm] int codigoServicoLavanderia)
    {
        using (Contexts.CTXHotelEF ctx = new Contexts.CTXHotelEF())
        {
            MEstadia? estadia = ctx.Estadias.Find(codigoEstadia);

            if (estadia == null)
                return Resultado.De(new APINaoEncontradoException($"Estadia de código '{codigoEstadia}' não encontrada"));

            MServicoLavanderia? servicoLavanderia = ctx.ServicosLavanderia.Find(codigoServicoLavanderia);

            if (servicoLavanderia == null)
                return Resultado.De(new APINaoEncontradoException($"Tipo de serviço de lavanderia de código '{codigoServicoLavanderia}' não encontrado"));

            try
            { 
                MConsumoLavanderia consumoLavanderia = ctx.ConsumosLavanderia.Add(new MConsumoLavanderia(estadia, servicoLavanderia)).Entity;
                ctx.SaveChanges();
                return Resultado.De(consumoLavanderia);
            }
            catch (Exception excecao)
            {
                return Resultado.De(excecao);
            }
        }
    }

    [HttpPatch("lavanderia")]
    public ActionResult<MCliente> PatchConsumoLavanderia([FromForm] int codigoConsumoLavanderia, [FromForm] DateTime? data, [FromForm] int? codigoEstadia, [FromForm] int? codigoServicoLavanderia)
    {
        using (Contexts.CTXHotelEF ctx = new Contexts.CTXHotelEF())
        {
            MConsumoLavanderia? consumoLavanderia = EagerConsumosLavanderia(ctx, codigoConsumoLavanderia);

            if (consumoLavanderia == null)
                return Resultado.De(new APINaoEncontradoException($"Consumo na lavanderia de código '{codigoConsumoLavanderia}' não encontrado"));

            MEstadia? estadia = codigoEstadia != null ? ctx.Estadias.Find(codigoEstadia) : null;

            if (codigoEstadia != null && estadia == null)
                return Resultado.De(new APINaoEncontradoException($"Estadia de código '{codigoEstadia}' não encontrada"));

            MServicoLavanderia? servicoLavanderia = ctx.ServicosLavanderia.Find(codigoServicoLavanderia);

            if (codigoServicoLavanderia != null && servicoLavanderia == null)
                return Resultado.De(new APINaoEncontradoException($"Tipo de serviço de lavanderia de código '{codigoServicoLavanderia}' não encontrado"));

            try
            { 
                if (data != null)
                    consumoLavanderia.Data = data;
                    
                if (estadia != null)
                    consumoLavanderia.Estadia = estadia;

                if (servicoLavanderia != null)
                    consumoLavanderia.ServicoLavanderia = servicoLavanderia;

                ctx.SaveChanges();
                return Resultado.De(consumoLavanderia);
            }
            catch (Exception excecao)
            {
                return Resultado.De(excecao);
            }
        }
    }

    [HttpDelete("lavanderia")]
    public ActionResult<MConsumoLavanderia> DeleteConsumoLavanderia([FromForm] int codigoConsumoLavanderia)
    {
        using (Contexts.CTXHotelEF ctx = new Contexts.CTXHotelEF())
        {
            MConsumoLavanderia? consumoLavanderia = EagerConsumosLavanderia(ctx, codigoConsumoLavanderia);

            if (consumoLavanderia == null)
                return Resultado.De(new APINaoEncontradoException($"Consumo na lavanderia de código '{codigoConsumoLavanderia}' não encontrado"));

            try
            { 
                ctx.ConsumosLavanderia.Remove(consumoLavanderia);
                ctx.SaveChanges();
                return Resultado.De(consumoLavanderia);
            }
            catch (Exception excecao)
            {
                return Resultado.De(excecao);
            }
        }
    }
#endregion
}