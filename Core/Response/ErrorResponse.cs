namespace Core.Response
{
    public class ErrorResponse : ServiceResponse
    {
        public ErrorResponse()
        {
            Success = false;
        }
    }
}
