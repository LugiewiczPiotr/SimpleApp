namespace SimpleApp.WebApi.Infrastructure.Middleware
{
    public class ExceptionHandlerResponse
    {
        public ExceptionHandlerResponse(string exceptionType, string exceptionMessage)
        {
            ExceptionType = exceptionType;
            ExceptionMessage = exceptionMessage;
        }

        public string ExceptionType { get; set; }
        public string ExceptionMessage { get; set; }
    }
}
