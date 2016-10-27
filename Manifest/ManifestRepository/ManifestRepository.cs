using ManifestDbContext;
using ManifestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManifestRepository
{
    public class ManifestRepository : Repository<Manifest>, IManifestRepository
    {
        private ManifestUnitOfWork manifestUnitOfWork;
        public ManifestRepository(ManifestUnitOfWork manifestUnitOfWork) : base(manifestUnitOfWork)
        {
            this.manifestUnitOfWork = manifestUnitOfWork;
        }
        
        public List<Manifest> GetAllManifest()
        {
            return manifestUnitOfWork.ManifestCollection.ToList();
        }       

        public List<Seal> GetSealByManifest(Guid manifestId)
        {
            return manifestUnitOfWork.Seals(manifestId);
        }

        public void AddManifest(Manifest manifest)
        {
            manifestUnitOfWork.AddManifest(manifest);
        }

        public void AddSeal(Seal seal)
        {
            manifestUnitOfWork.AddSeal(seal);
        }

        public void AddImage(Image image)
        {
            manifestUnitOfWork.AddImage(image);
        }

        public void UpdateManifest(Manifest oldManifest, Manifest latestManifest)
        {
            manifestUnitOfWork.UpdateManifest(oldManifest, latestManifest);
        }

        public void UpdateSeal(Seal oldSeal, Seal latestSeal)
        {
            manifestUnitOfWork.UpdateSeal(oldSeal, latestSeal);
        }

    }
}
