

namespace XZ.Core.Logs
{
    public interface IMyLog
    {
        void ShowLogInfo(string info, params object[] values);
        void ShowError(string error, params object[] values);
        void ShowWarning(string warning, params object[] values);
    }
}
