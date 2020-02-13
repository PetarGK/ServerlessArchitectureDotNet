namespace Minniowa.Users.Application
{
    public class SuccessResult<T> : Result
    {
        public SuccessResult(T data) : base(200, data)
        {
        }
    }

    public class SuccessResult : Result
    {
        public SuccessResult() : base(200, string.Empty)
        {
        }
    }
}
