namespace HotelEF.Exceptions;

public class APIValorInvalidoException : APIException
{
    public APIValorInvalidoException(string mensagem, bool sensivel = false) : base(mensagem, 400, sensivel) { }
}
