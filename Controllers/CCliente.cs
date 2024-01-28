
namespace HotelEF.Controllers;

using HotelEF.Models;
using HotelEF.Handlers;
using HotelEF.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

[Route("api/cliente")]
public class CCliente : Controller
{
    private IIncludableQueryable<MCliente, ISet<MTelefone>> EagerClientes(Contexts.CTXHotelEF ctx)
    {
        return ctx.Clientes
            .Include(cliente => cliente.Nacionalidade)
            .Include(cliente => cliente.Telefones);
    }

    private MCliente? EagerClientes(Contexts.CTXHotelEF ctx, int? codigo)
    {
        return EagerClientes(ctx).FirstOrDefault(cliente => cliente.Codigo == codigo);
    }

    [HttpGet]
    public ActionResult<IEnumerable<MCliente>> GetCliente([FromQuery] int? codigoCliente)
    {
        using (Contexts.CTXHotelEF ctx = new Contexts.CTXHotelEF())
        {
            if (codigoCliente == null)
                return Resultado.De(EagerClientes(ctx).ToList());
            else
            {
                MCliente? cliente = EagerClientes(ctx, codigoCliente);

                if (cliente == null)
                    return Resultado.De(new APINaoEncontradoException($"Cliente de código '{codigoCliente}' não encontrado"));

                return Resultado.De(cliente);
            }
        }
    }

    [HttpPut]
    public ActionResult<MCliente> PutCliente([FromForm] string nome, [FromForm] string endereco, [FromForm] string email, [FromForm] int codigoNacionalidade)
    {
        using (Contexts.CTXHotelEF ctx = new Contexts.CTXHotelEF())
        {
            MNacionalidade? nacionalidade = ctx.Nacionalidades.Find(codigoNacionalidade);

            if (nacionalidade == null)
                return Resultado.De(new APINaoEncontradoException($"Nacionalidade de código '{codigoNacionalidade}' não encontrada"));

            try
            { 
                MCliente cliente = ctx.Clientes.Add(new MCliente(nome, endereco, email, nacionalidade)).Entity;
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
    public ActionResult<MCliente> PatchCliente([FromQuery] int codigoCliente, [FromForm] string? nome, [FromForm] string? endereco, [FromForm] string? email, [FromForm] int? codigoNacionalidade)
    {
        using (Contexts.CTXHotelEF ctx = new Contexts.CTXHotelEF())
        {
            MCliente? cliente = EagerClientes(ctx, codigoCliente);

            if (cliente == null)
                return Resultado.De(new APINaoEncontradoException($"Cliente de código '{codigoCliente}' não encontrado"));

            MNacionalidade? nacionalidade = codigoNacionalidade != null ? ctx.Nacionalidades.Find(codigoNacionalidade) : null;

            if (codigoNacionalidade != null && nacionalidade == null)
                return Resultado.De(new APINaoEncontradoException($"Nacionalidade de código '{codigoNacionalidade}' não encontrada"));

            try
            { 
                if (nome != null)
                    cliente.Nome = nome;

                if (endereco != null)
                    cliente.Endereco = endereco;

                if (email != null)
                    cliente.Email = email;

                if (nacionalidade != null)
                    cliente.Nacionalidade = nacionalidade;

                ctx.SaveChanges();
                return Resultado.De(cliente);
            }
            catch (Exception excecao)
            {
                return Resultado.De(excecao);
            }
        }
    }

    [HttpDelete]
    public ActionResult<MCliente> DeleteCliente([FromForm] int codigoCliente)
    {
        using (Contexts.CTXHotelEF ctx = new Contexts.CTXHotelEF())
        {
            MCliente? cliente = EagerClientes(ctx, codigoCliente);

            if (cliente == null)
                return Resultado.De(new APINaoEncontradoException($"Cliente de código '{codigoCliente}' não encontrado"));
            try
            { 
                ctx.Clientes.Remove(cliente);
                ctx.SaveChanges();
                return Resultado.De(cliente);
            }
            catch (Exception excecao)
            {
                return Resultado.De(excecao);
            }
        }
    }

#region telefone
    [HttpPut("telefone")]
    public ActionResult<MTelefone> PutClienteTelefone([FromQuery] int codigoCliente, [FromForm] string numero)
    {
        using (Contexts.CTXHotelEF ctx = new Contexts.CTXHotelEF())
        {
            MCliente? cliente = EagerClientes(ctx, codigoCliente);

            if (cliente == null)
                return Resultado.De(new APINaoEncontradoException($"Cliente de código '{codigoCliente}' não encontrado"));

            try
            { 
                MTelefone telefone = new MTelefone(numero);
                cliente.Telefones.Add(telefone);
                ctx.SaveChanges();
                return Resultado.De(telefone);
            }
            catch (Exception excecao)
            {
                return Resultado.De(excecao);
            }
        }
    }

    [HttpDelete("telefone")]
    public ActionResult<MTelefone> DeleteClienteTelefone([FromQuery] int codigoCliente, [FromForm] int codigoTelefone)
    {
        using (Contexts.CTXHotelEF ctx = new Contexts.CTXHotelEF())
        {
            MCliente? cliente = EagerClientes(ctx, codigoCliente);

            if (cliente == null)
                return Resultado.De(new APINaoEncontradoException($"Cliente de código '{codigoCliente}' não encontrado"));

            MTelefone? telefone = cliente.Telefones.FirstOrDefault(telefone => telefone.Codigo == codigoTelefone);

            if (telefone == null)
                return Resultado.De(new APINaoEncontradoException($"Cliente de código '{codigoCliente}' não possuí um telefone de código '{codigoTelefone}'"));
            
            try
            { 
                cliente.Telefones.Remove(telefone);
                ctx.SaveChanges();
                return Resultado.De(telefone);
            }
            catch (Exception excecao)
            {
                return Resultado.De(excecao);
            }
        }
    }
#endregion
}
