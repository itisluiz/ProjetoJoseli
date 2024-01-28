namespace HotelEF.Exceptions;

public class APIException : Exception
{
    public int CodigoStatus { get; set; }
    public bool Sensivel { get; set; }

    public APIException(string mensagem, int codigoStatus = 500, bool sensivel = false) : base(mensagem)
    {
        this.CodigoStatus = codigoStatus;
        this.Sensivel = sensivel;
    }
}