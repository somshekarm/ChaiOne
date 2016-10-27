using ManifestModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ManifestResource.Models.Manifests
{
    public class PostManifest
    {        
        public Guid ID { get; set; }

        [Required(ErrorMessage ="Officer name is required.")]
        public string OfficerName { get; set; }

        [Required(ErrorMessage ="Origin location is required.")]
        public string OriginLocation { get; set; }

        [Required(ErrorMessage ="Receiving locations is required.")]
        public string ReceivingLocation { get; set; }

        public DateTime? EstimatedTimeofArrival { get; set; }

        public DateTime? CreatedOn { get; set; }

        public DateTime? UpdatedOn { get; set; }
        public int SiteId { get; set; }

        public IEnumerable<PostSeal> PostSeals { get; set; }

        public ManifestStatus ManifestStatus { get; set; }

        [Required(ErrorMessage ="Name of the Manifest is required.")]
        public string Name { get;  set; }
    }
}