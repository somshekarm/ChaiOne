using ManifestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManifestResource.Models.Manifests
{
    public class PostSeal
    {
        public Guid ID { get; set; }
        public string SealNumber { get; set; }
        public string SealType { get; set; }
        public string Note { get; set; }

        public DateTime? CreatedOn { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public Guid ManifestId { get; set; }
        public SealStatus SealStatus { get; set; }

        public bool ArchiveStatus { get; set; }

        public IEnumerable<PostImage> PostImages { get; set; }
    }
}
