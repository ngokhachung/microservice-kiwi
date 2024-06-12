namespace Kiwi.Web.Models
{
    public class ResponseModel
    {
        public object? Data { get; set; }
        public bool IsSuccess { get; set; } = true;
        public string Message { get; set; } = "";
    }
}
