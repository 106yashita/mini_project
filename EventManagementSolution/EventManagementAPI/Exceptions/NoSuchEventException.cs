using System.Runtime.Serialization;

namespace EventManagementAPI.Exceptions
{
    [Serializable]
    public class NoSuchEventException : Exception
    {
        string message;
        public NoSuchEventException()
        {
            message = "No such Event Exists";
        }
        public override string Message => message;

    }
}