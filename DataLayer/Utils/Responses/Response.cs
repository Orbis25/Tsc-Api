namespace DataLayer.Utils.Responses
{
    public class Response<T>
    {
        public bool IsSuccess => string.IsNullOrEmpty(ErrorMessage);
        public string ErrorMessage { get; set; }
        public string Code { get; set; }
        public T Data { get; set; }
    }
}
