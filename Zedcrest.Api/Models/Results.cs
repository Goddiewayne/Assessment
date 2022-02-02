namespace Zedcrest.Api.Models
{
    public class Response
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Status_Code { get; set; }
        public object Data { get; set; }
    }
}
