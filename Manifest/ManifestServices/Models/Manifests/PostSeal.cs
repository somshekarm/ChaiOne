using ManifestModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManifestResource.Models.Manifests
{
    public class PostSeal
    {
        public Guid ID { get; set; }

        [Required(ErrorMessage ="SealNUmber is required.")]
        public string SealNumber { get; set; }

        [Required(ErrorMessage ="Seal Type is required.")]
        public string SealType { get; set; }
        public string Note { get; set; }

        public DateTime? CreatedOn { get; set; }

        public DateTime? UpdatedOn { get; set; }

        [Required(ErrorMessage = "ManifestId is required")]
        public Guid ManifestId { get; set; }
        public SealStatus SealStatus { get; set; }

        [Required(ErrorMessage ="Archive Status is required.")]
        public bool ArchiveStatus { get; set; }

        public IEnumerable<PostImage> PostImages { get; set; }
    }
}
