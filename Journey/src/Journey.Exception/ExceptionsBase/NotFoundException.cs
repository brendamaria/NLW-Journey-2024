using System.Net;

namespace Journey.Exception.ExceptionsBase
{
    public class NotFoundException : JourneyException
    {
        public NotFoundException(string message) : base(message)
        {
        }

        public override IList<string> Messages()
        {
            return [Message];
        }

        public override HttpStatusCode StatusCode()
        {
            return HttpStatusCode.NotFound;
        }
    }
}
