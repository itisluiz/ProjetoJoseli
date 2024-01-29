namespace HotelEF.Exceptions;

public class APIHotelException : APIException
{
    public APIHotelException(string mensagem, bool sensivel = false) : base(mensagem, 400, sensivel) { }
}
