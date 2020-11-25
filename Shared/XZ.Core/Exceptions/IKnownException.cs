using System;

namespace XZ.Core.Exceptions
{
    public interface IKnownException
    {
        string Message { get; }

        int ErrorCode { get; }

        object[] ErrorData { get; }
    }
}
