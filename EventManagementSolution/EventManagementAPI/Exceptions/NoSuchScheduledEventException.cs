using System.Runtime.Serialization;

namespace EventManagementAPI.Exceptions
{
    [Serializable]
    internal class NoSuchScheduledEventException : Exception
    {
        string message;
        public NoSuchScheduledEventException()
        {
            message = "No such Scheduled Event Exists";
        }
        public override string Message => message;


    }
}