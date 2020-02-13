namespace Minniowa.Users.Application
{
    public abstract class Result
    {
        public Result(int code, object data)
        {
            Code = code;
            Data = data;
        }
        public int Code { get; private set; }

        public object Data { get; private set; }
    }
}
