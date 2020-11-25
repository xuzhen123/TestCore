using System;
using System.Collections.Generic;
using System.Text;
using XZ.Infrastructure;
using XZ.Main.Domain;

namespace XZ.Main.Repository
{
    public interface IAppSysPmsRepository : IRepository<SysOperation>
    {
        List<SysOperation> GetAppSysPms(string sysAppId, string name);
    }
}
