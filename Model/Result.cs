namespace AutoViewWebAdsCSharp.Model
{
    public class Result<T>
    {
        public string Message { get; set; } = "";
        public bool Success { get; set; } = false;

        public T Data { get; set; }

        public static Result<T> CreateResponse(bool success = false, string message = "")
        {
            return new Result<T>
            {
                Success = success,
                Message = message,
            };
        }

    }
}
