using System.Runtime.Serialization;

namespace EventManagementAPI.Exceptions
{
    [Serializable]
    public class RequestNotAcceptedException : Exception
    {
        string message;
        public RequestNotAcceptedException()
        {
            message = "Request is not accepted by the admin";
        }
        public override string Message => message;
    }
}