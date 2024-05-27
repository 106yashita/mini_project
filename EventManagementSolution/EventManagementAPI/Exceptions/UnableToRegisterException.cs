using System.Runtime.Serialization;

namespace EventManagementAPI.Exceptions
{
    [Serializable]
    internal class UnableToRegisterException : Exception
    {
        string msg;
        public UnableToRegisterException(string? message) : base(message)
        {
            msg = message;
        }
        public override string Message => msg;
    }
}