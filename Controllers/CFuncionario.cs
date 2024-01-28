
namespace HotelEF.Controllers;

using HotelEF.Models;
using HotelEF.Handlers;
using HotelEF.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

[Route("api/funcionario")]
public class CFuncionario : Controller
{
    private IIncludableQueryable<MFuncionario, ISet<MTelefone>> EagerFuncionarios(Contexts.CTXHotelEF ctx)
    {
        return ctx.Funcionarios
            .Include(cliente => cliente.Tipo)
            .Include(cliente => cliente.Telefones);
    }

    private MFuncionario? EagerFuncionarios(Contexts.CTXHotelEF ctx, int? codigo)
    {
        return EagerFuncionarios(ctx).FirstOrDefault(funcionario => funcionario.Codigo == codigo);
    }

    [HttpGet]
    public ActionResult<IEnumerable<MCliente>> GetFuncionario([FromQuery] int? codigoFuncionario)
    {
        using (Contexts.CTXHotelEF ctx = new Contexts.CTXHotelEF())
        {
            if (codigoFuncionario == null)
                return Resultado.De(EagerFuncionarios(ctx).ToList());
            else
            {
                MFuncionario? funcionario = EagerFuncionarios(ctx, codigoFuncionario);

                if (funcionario == null)
                    return Resultado.De(new APINaoEncontradoException($"Funcionário de código '{codigoFuncionario}' não encontrado"));

                return Resultado.De(funcionario);
            }
        }
    }

    [HttpPut]
    public ActionResult<MFuncionario> PutFuncionario([FromForm] string nome, [FromForm] string endereco, [FromForm] string email, [FromForm] int codigoTipo)
    {
        using (Contexts.CTXHotelEF ctx = new Contexts.CTXHotelEF())
        {
            MTipoFuncionario? tipo = ctx.TiposFuncionario.Find(codigoTipo);

            if (tipo == null)
                return Resultado.De(new APINaoEncontradoException($"Tipo de funcionário de código '{codigoTipo}' não encontrado"));

            try
            {
                MFuncionario funcionario = ctx.Funcionarios.Add(new MFuncionario(nome, endereco, email, tipo)).Entity;
                ctx.SaveChanges();
                return Resultado.De(funcionario);
            }
            catch (Exception excecao)
            {
                return Resultado.De(excecao);
            }
        }
    }

    [HttpPatch]
    public ActionResult<MFuncionario> PatchFuncionario([FromQuery] int codigoFuncionario, [FromForm] string? nome, [FromForm] string? endereco, [FromForm] string? email, [FromForm] int? codigoTipo)
    {
        using (Contexts.CTXHotelEF ctx = new Contexts.CTXHotelEF())
        {
            MFuncionario? funcionario = EagerFuncionarios(ctx, codigoFuncionario);

            if (funcionario == null)
                return Resultado.De(new APINaoEncontradoException($"Funcionário de código '{codigoFuncionario}' não encontrado"));

            MTipoFuncionario? tipo = codigoTipo != null ? ctx.TiposFuncionario.Find(codigoTipo) : null;

            if (codigoTipo != null && tipo == null)
                return Resultado.De(new APINaoEncontradoException($"Tipo de funcionário de código '{codigoTipo}' não encontrado"));

            try
            { 
                if (nome != null)
                    funcionario.Nome = nome;

                if (endereco != null)
                    funcionario.Endereco = endereco;

                if (email != null)
                    funcionario.Email = email;

                if (tipo != null)
                    funcionario.Tipo = tipo;

                ctx.SaveChanges();
                return Resultado.De(funcionario);
            }
            catch (Exception excecao)
            {
                return Resultado.De(excecao);
            }
        }
    }

    [HttpDelete]
    public ActionResult<MCliente> DeleteFuncionario([FromForm] int codigoFuncionario)
    {
        using (Contexts.CTXHotelEF ctx = new Contexts.CTXHotelEF())
        {
            MFuncionario? funcionario = EagerFuncionarios(ctx, codigoFuncionario);

            if (funcionario == null)
                return Resultado.De(new APINaoEncontradoException($"Funcionário de código '{codigoFuncionario}' não encontrado"));
            try
            { 
                ctx.Funcionarios.Remove(funcionario);
                ctx.SaveChanges();
                return Resultado.De(funcionario);
            }
            catch (Exception excecao)
            {
                return Resultado.De(excecao);
            }
        }
    }

#region telefone
    [HttpPut("telefone")]
    public ActionResult<MTelefone> PutFuncionarioTelefone([FromQuery] int codigoFuncionario, [FromForm] string numero)
    {
        using (Contexts.CTXHotelEF ctx = new Contexts.CTXHotelEF())
        {
            MFuncionario? funcionario = EagerFuncionarios(ctx, codigoFuncionario);

            if (funcionario == null)
                return Resultado.De(new APINaoEncontradoException($"Funcionário de código '{codigoFuncionario}' não encontrado"));

            try
            { 
                MTelefone telefone = new MTelefone(numero);
                funcionario.Telefones.Add(telefone);
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
    public ActionResult<MTelefone> DeleteFuncionarioTelefone([FromQuery] int codigoFuncionario, [FromForm] int codigoTelefone)
    {
        using (Contexts.CTXHotelEF ctx = new Contexts.CTXHotelEF())
        {
            MFuncionario? funcionario = EagerFuncionarios(ctx, codigoFuncionario);

            if (funcionario == null)
                return Resultado.De(new APINaoEncontradoException($"Funcionário de código '{codigoFuncionario}' não encontrado"));

            MTelefone? telefone = funcionario.Telefones.FirstOrDefault(telefone => telefone.Codigo == codigoTelefone);

            if (telefone == null)
                return Resultado.De(new APINaoEncontradoException($"Funcionário de código '{codigoFuncionario}' não possuí um telefone de código '{codigoTelefone}'"));
            
            try
            { 
                funcionario.Telefones.Remove(telefone);
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
