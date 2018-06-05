using System;
using System.Collections.Generic;
using System.Data.Entity;
using ManifestModels;

namespace ManifestDbContext
{
    public interface IManifestUnitOfWork
    {
        DbSet<Image> Image { get; set; }
        DbSet<Manifest> Manifest { get; set; }
        ICollection<Manifest> ManifestCollection { get; }
        DbSet<Seal> Seal { get; set; }
        DbSet<AppServer> AppServer {get; set;}       

        void AddImage(Image imageDataEntity);
        void AddManifest(Manifest manifestDataEntity);
        void AddSeal(Seal sealDataEntity);

        void AddAppServer(AppServer appServer);
        void CompleteWork();
        List<Seal> Seals(Guid manifestId);        
        void UpdateManifest(Manifest oldManifest, Manifest latestManifest);
        void UpdateSeal(Seal oldSeal, Seal latestSeal);
    }
}