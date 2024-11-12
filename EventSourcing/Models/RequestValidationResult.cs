namespace EventSourcing.Models
{
    public class RequestValidationResult
    {
        public bool IsValid { get; set; }
        public string Message { get; set; }
    }
}
