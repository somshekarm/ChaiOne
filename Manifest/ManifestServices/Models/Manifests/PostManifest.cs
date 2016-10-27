using ManifestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManifestResource.Models.Manifests
{
    public class PostManifest
    {        
        public Guid ID { get; set; }
        public string OfficerName { get; set; }
        public string OriginLocation { get; set; }
        public string ReceivingLocation { get; set; }
        public DateTime? EstimatedTimeofArrival { get; set; }

        public DateTime? CreatedOn { get; set; }

        public DateTime? UpdatedOn { get; set; }
        public int SiteId { get; set; }

        public IEnumerable<PostSeal> PostSeals { get; set; }

        public ManifestStatus ManifestStatus { get; set; }

        public string Name { get;  set; }
    }
}