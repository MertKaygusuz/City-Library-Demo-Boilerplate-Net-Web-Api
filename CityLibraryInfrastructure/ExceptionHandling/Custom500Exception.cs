using System.Net;

namespace CityLibraryInfrastructure.ExceptionHandling
{
    public class Custom500Exception : CustomHttpException
    {
        public Custom500Exception(string Message)
          : base(Message)
        {

        }

        public override HttpStatusCode HttpStatusCode => HttpStatusCode.InternalServerError;
    }
}
