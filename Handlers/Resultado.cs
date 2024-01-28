namespace HotelEF.Handlers;

using HotelEF.Exceptions;
using Microsoft.AspNetCore.Mvc;

public class Resultado : ObjectResult
{
    private class ResultadoExcecao
    {
        public string? Erro { get; set; }
        public string? Mensagem { get; set; }

        public ResultadoExcecao(string erro, string mensagem)
        {
            this.Erro = erro;
            this.Mensagem = mensagem;
        }
    }

    private Resultado(object resultado, int codigoSucesso = 200) : base(null)
    {
        Exception? excecao = resultado as Exception;
       
        if (excecao != null)
        {
            APIException? excecaoAPI = resultado as APIException;

            if (excecaoAPI != null)
            {
                this.StatusCode = excecaoAPI.CodigoStatus;
                if (excecaoAPI.Sensivel)
                {
                    this.Value = new ResultadoExcecao("ErroInterno", "[Mensagem omitida]");
                    return;
                } 
            }

            this.Value = new ResultadoExcecao(excecao.GetType().Name, excecao.Message);
        }
        else
        {
            this.StatusCode = codigoSucesso;
            this.Value = resultado;
        }
    }

    public static Resultado De(object resultado, int codigoSucesso = 200)
    {
        return new Resultado(resultado, codigoSucesso);
    }

}
