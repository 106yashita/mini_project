using System.Runtime.Serialization;

namespace EventManagementAPI.Exceptions
{

    [Serializable]
    internal class NoSuchUserException : Exception
    {
        string message;
        public NoSuchUserException()
        {
            message = "No such User Exists";
        }
        public override string Message => message;

    }
}