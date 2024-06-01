using System.Runtime.Serialization;

namespace EventManagementAPI.Exceptions
{
    [Serializable]
    public class UnableToRegisterException : Exception
    {
        string msg;
        public UnableToRegisterException(string? message) : base(message)
        {
            msg = message;
        }
        public override string Message => msg;
    }
}