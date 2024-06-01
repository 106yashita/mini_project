using System.Runtime.Serialization;

namespace EventManagementAPI.Exceptions
{
    [Serializable]
    public class ResponseNotAcceptedException : Exception
    {
        string message;
        public ResponseNotAcceptedException()
        {
            message = "Response is not accepted by the user";
        }
        public override string Message => message;
    }
}