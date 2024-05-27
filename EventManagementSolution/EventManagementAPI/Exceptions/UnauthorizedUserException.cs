using System.Runtime.Serialization;

namespace EventManagementAPI.Exceptions
{
    [Serializable]
    internal class UnauthorizedUserException : Exception
    {
        string msg;
        public UnauthorizedUserException(string? message) : base(message)
        {
            msg = message;
        }
        public override string Message => msg;

    }
}