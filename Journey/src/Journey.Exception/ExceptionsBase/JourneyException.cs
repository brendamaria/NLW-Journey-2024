using System.Net;

namespace Journey.Exception.ExceptionsBase
{
    public abstract class JourneyException : SystemException
    {
        public JourneyException(string message) : base(message) { }

        public abstract HttpStatusCode StatusCode();

        public abstract IList<string> Messages();
    }
}
