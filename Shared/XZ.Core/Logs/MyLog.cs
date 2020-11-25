using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace XZ.Core.Logs
{
    public class MyLog: IMyLog
    {
        ILogger<MyLog> _mylog;
        public MyLog(ILogger<MyLog> mylog) => _mylog = mylog;

        public void ShowLogInfo(string info, params object[] values)
        {
            _mylog.LogInformation(info, values);
        }

        public void ShowError(string error, params object[] values)
        {
            _mylog.LogError(error, values);
        }

        public void ShowWarning(string warning, params object[] values)
        {
            _mylog.LogWarning(warning, values);
        }
    }
}
