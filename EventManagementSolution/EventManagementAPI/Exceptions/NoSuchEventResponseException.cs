using System.Runtime.Serialization;

namespace EventManagementAPI.Exceptions
{
    [Serializable]
    public class NoSuchEventResponseException : Exception
    {
        string message;
        public NoSuchEventResponseException()
        {
            message = "No such Event Response Exists";
        }
        public override string Message => message;
    }

}