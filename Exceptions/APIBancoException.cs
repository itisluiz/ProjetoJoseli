namespace HotelEF.Exceptions;

public class APIFalhaDatabaseException : APIException
{
    public APIFalhaDatabaseException(string mensagem, bool sensivel = true) : base(mensagem, 500, sensivel) { }
}
