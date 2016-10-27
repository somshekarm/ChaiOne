using ManifestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManifestRepository
{
    public static class PopulateManifestDummyData
    {
        public static List<Manifest> PopulateManifest()
        {
            var manifestList = new List<Manifest>();
            var manifest1 = new Manifest("AB1C", "Houston", "Dallas", "Manifest 1");
            var seal1 = new Seal("SealNumber1", "SealType",manifest1.ID, true);
            var image = new Image();
            seal1.AddImage(image);
            manifest1.AddSeal(seal1);

            var manifest2 = new Manifest("ABC2", "Houston", "Dallas", "Manifest 2");
            var seal2 = new Seal("SealNumber2", "SealType", manifest2.ID, true);
            var image2 = new Image();
            seal2.AddImage(image2);
            manifest2.AddSeal(seal2);


            return new List<Manifest> { manifest1, manifest2 };
        }
    }
}
