namespace FribergHomeAPI.Results
{
    // Author: Christoffer
    public class ServiceResult<T>
    {
        public bool Success { get; init; }
        public T? Data { get; init; }
        public IEnumerable<ServiceResultError> Errors { get; init; } = Enumerable.Empty<ServiceResultError>();

        private ServiceResult() { }

        public static ServiceResult<T> SuccessResult(T data) => new() { Success = true, Data = data };
        public static ServiceResult<T> Failure(IEnumerable<ServiceResultError> errors) => new() { Success = false, Errors = errors };
        public static ServiceResult<T> Failure(string error) => new() { Success = false, Errors = [new ServiceResultError { Code = "", Description = error}] };
        public static ServiceResult<T> Failure(ServiceResultError error) => new() { Success = false, Errors = [error] };
    }
    public class ServiceResult
    {
        public bool Success { get; init; }
        public IEnumerable<ServiceResultError> Errors { get; init; } = Enumerable.Empty<ServiceResultError>();

        private ServiceResult() { }

        public static ServiceResult SuccessResult() => new() { Success = true };
        public static ServiceResult Failure(IEnumerable<ServiceResultError> errors) => new() { Success = false, Errors = errors };
        public static ServiceResult Failure(string error) => new() { Success = false, Errors = [new ServiceResultError { Code = "", Description = error }] };
        public static ServiceResult Failure(ServiceResultError error) => new() { Success = false, Errors = [error] };
    }
}
