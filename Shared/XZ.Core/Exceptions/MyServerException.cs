using System;

namespace XZ.Core.Exceptions
{
    public class MyServerException : Exception, IKnownException
    {
        public int ErrorCode { get; private set; }

        public object[] ErrorData { get; private set; }

        public MyServerException(string message, int errorCode, params object[] errorData) : base(message)
        {
            this.ErrorCode = errorCode;
            this.ErrorData = errorData;
        }
    }
}
