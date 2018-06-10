using ManifestModels;
using System.Collections.Generic;

namespace ManifestRepository
{
    public interface IAppServerRepository : IRepository<AppServer>
    {
        void AddAppServers(List<AppServer> appServer);
        List<AppServer> GetAppServer();
    }
}
