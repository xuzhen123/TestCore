

namespace XZ.Core.Exceptions
{
    public class KnownException : IKnownException
    {
        public string Message { get; private set; }

        public int ErrorCode { get; private set; }

        public object[] ErrorData { get; private set; }

        public readonly static IKnownException UnKown = new KnownException { ErrorCode = 999, Message = "未知错误" };

        public static IKnownException FromKnownException(IKnownException exception)
        {
            return new KnownException { ErrorCode = exception.ErrorCode, Message = exception.Message, ErrorData = exception.ErrorData };
        }
    }
}
