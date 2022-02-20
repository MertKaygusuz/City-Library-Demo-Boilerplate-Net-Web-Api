using System;
using System.Net;

namespace CityLibraryInfrastructure.ExceptionHandling
{
    public abstract class CustomHttpException : Exception
    {
        public CustomHttpException(string Message)
       : base(Message)
        {

        }

        public CustomHttpException()
            : this("Internal Server Error!")
        {

        }

        public abstract HttpStatusCode HttpStatusCode { get; }
    }
}
