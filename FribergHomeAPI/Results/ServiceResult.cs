namespace FribergHomeAPI.Results
{
    // Author: Christoffer
    public class ServiceResult<T>
    {
        public bool Success { get; init; }
        public T? Data { get; init; }
        public IEnumerable<string> Errors { get; init; } = Enumerable.Empty<string>();

        private ServiceResult() { }

        public static ServiceResult<T> SuccessResult(T data) => new() { Success = true, Data = data };
        public static ServiceResult<T> Failure(IEnumerable<string> errors) => new() { Success = false, Errors = errors };
        public static ServiceResult<T> Failure(string error) => new() { Success = false, Errors = [error] };
    }
}
