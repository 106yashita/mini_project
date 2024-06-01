using System.Runtime.Serialization;

namespace EventManagementAPI.Exceptions
{
    [Serializable]
    public class NoSuchEventRequestException : Exception
    {
        string message;
        public NoSuchEventRequestException()
        {
            message = "No such Event Request Exists";
        }
        public override string Message => message;
    }
}