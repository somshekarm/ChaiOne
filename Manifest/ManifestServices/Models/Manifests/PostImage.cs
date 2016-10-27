using ManifestModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManifestResource.Models.Manifests
{
    public class PostImage
    {
        public Guid ID { get; set; }        

        [Required(ErrorMessage ="File is required.")]
        public string File { get; set; }
                
        public Guid SealId { get; set; }

        public DateTime? CreatedOn { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public int ImageableId { get; set; }
        public ImageFileType ImageFileType { get; set; }
    }
}
