using System.Runtime.Serialization;

namespace EventManagementAPI.Exceptions
{
    [Serializable]
    internal class RequestNotAcceptedException : Exception
    {
        string message;
        public RequestNotAcceptedException()
        {
            message = "Request is not accepted by the admin";
        }
        public override string Message => message;
    }
}