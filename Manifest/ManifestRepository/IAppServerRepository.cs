using ManifestModels;

namespace ManifestRepository
{
    interface IAppServerRepository : IRepository<AppServer>
    {
        void AddAppServer(AppServer appServer);
    }
}
