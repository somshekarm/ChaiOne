using System;
using System.Collections.Generic;
using ManifestModels;

namespace ManifestRepository
{
    public interface IManifestRepository : IRepository<Manifest>
    {
        void AddImage(Image image);
        void AddManifest(Manifest manifest);
        void AddSeal(Seal seal);
        List<Manifest> GetAllManifest();
        List<Seal> GetSealByManifest(Guid manifestId);
        void UpdateManifest(Manifest oldManifest, Manifest latestManifest);
        void UpdateSeal(Seal oldSeal, Seal latestSeal);
    }
}