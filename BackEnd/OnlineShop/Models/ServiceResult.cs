namespace BackEnd.Models
{
    public class ServiceResult
    {
        public List<string> Errors { get; set; } = new List<string>();

        public bool isSuccess => !Errors.Any();
    }

    public class ServiceResult<T> : ServiceResult
    {
        public T? Data { get; set; }

        public static ServiceResult<T> Success(T data)
        {
            return new ServiceResult<T> { Data = data };
        }

        public static ServiceResult<T> Failure(string error)
        {
            return new ServiceResult<T> { Errors = new List<string> { error } };
        }
    }
}