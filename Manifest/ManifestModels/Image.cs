using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManifestModels
{
    public class Image : BaseModel
    {
        public Image()
        {
            this.ID = Guid.NewGuid();
        }
        
        public Image(string file, Guid sealID)
        {
            ID = Guid.NewGuid();
            SealId = sealID;
            File = file;
        }

        public Image(string file, Guid sealID,DateTime? createdOn, DateTime? updatedOn, int imageableId, ImageFileType imageFileType )
            :this(file,sealID)
        {
            this.ID = Guid.NewGuid();           
            File = file;
            CreatedOn = createdOn;
            UpdatedOn = updatedOn;
            ImageFileType = ImageFileType;
            SealId = sealID;
            ImageableId = imageableId;
        }

        public Guid ID { get; private set; }
        public string File { get; private set; }
   
        public DateTime? CreatedOn { get; private set; }
       
        public DateTime? UpdatedOn { get; private set; }

        public int ImageableId { get; private set; }
        public ImageFileType ImageFileType { get; private set; }

        public Guid SealId { get; private set; }

        public void Update(Image latestImage)
        {
            if(SealId == latestImage.SealId)
            {                
                File = latestImage.File;
                CreatedOn = latestImage.CreatedOn;
                UpdatedOn = latestImage.UpdatedOn;
                ImageFileType = latestImage.ImageFileType;
                ImageableId = latestImage.ImageableId;
            }
        }
    }
}
