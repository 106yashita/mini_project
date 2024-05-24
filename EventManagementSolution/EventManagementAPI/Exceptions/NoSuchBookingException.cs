using System.Runtime.Serialization;

namespace EventManagementAPI.Exceptions
{
    [Serializable]
    internal class NoSuchBookingException : Exception
    {
        string message;
        public NoSuchBookingException()
        {
            message = "No such Booking Exists";
        }
        public override string Message => message;


    }
}