using System.Net;

namespace CityLibraryInfrastructure.ExceptionHandling
{
    public class CustomStatusException : CustomHttpException
    {
        private int StatusCode { get; init; }

        public CustomStatusException(string Message, int StatusCode)
         : base(Message)
        {
            this.StatusCode = StatusCode;
        }

        public override HttpStatusCode HttpStatusCode => (HttpStatusCode)StatusCode;
    }
}
