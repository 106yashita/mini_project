namespace EventManagementAPI.Models.DTOs
{
    public class ResponseDTO
    {
        public int EventRequestId { get; set; }
        public string ResponseStatus { get; set; }
        public int Amount { get; set; }
        public string ResponseMessage { get; set; }
    }
}
