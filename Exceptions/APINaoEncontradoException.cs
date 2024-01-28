namespace HotelEF.Exceptions;

public class APINaoEncontradoException : APIException
{
    public APINaoEncontradoException(string mensagem, bool sensivel = false) : base(mensagem, 404, sensivel) { }
}