namespace Parallel.Universe.Blog.Api.Services.Results
{
    public interface IResult
    {
        string Message { get; }
        bool Success { get; }
    }

    public class Result : IResult
    {
        public Result(string message, bool success)
        {
            Message = message;
            Success = success;
        }

        public string Message { get; }
        public bool Success { get; }
    }
}
