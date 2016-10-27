using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManifestModels
{
    public class Seal : BaseModel
    {
        public Seal()
        {
            this.ID = Guid.NewGuid();
        }
        [JsonConstructor]
        public Seal(string sealNumber, string sealType, Guid manifestId, bool archiveStatus, SealStatus sealStatus = SealStatus.NeedsApproval)
        {
            this.ID = Guid.NewGuid();
            SealNumber = sealNumber;
            SealType = sealType;            
            ManifestId = manifestId;
            SealStatus = sealStatus;
            ArchiveStatus = archiveStatus;
            Images = new List<Image>();
        }

        public Seal( string sealNumber, string sealType,string note, DateTime? createdOn, DateTime? updatedOn, Guid manifestId, bool archiveStatus, SealStatus sealStatus = SealStatus.NeedsApproval)
            :this(sealNumber,sealType,manifestId,archiveStatus)
        {
            this.ID = Guid.NewGuid();
            SealNumber = sealNumber;
            SealType = sealType;
            Note = note;
            CreatedOn = createdOn;
            UpdatedOn = updatedOn;
            ManifestId = manifestId;
            SealStatus = sealStatus;
            ArchiveStatus = archiveStatus;
        }
        public Guid ID { get; private set; }
        public string SealNumber { get; private set; }
        public string SealType { get; private set; }
        public string Note { get; private set; }
        
        public DateTime? CreatedOn { get; private set; }
        
        public DateTime? UpdatedOn { get; private set; }
                
        public Guid ManifestId { get; private set; }        
        public SealStatus SealStatus { get; private set; }

        public bool ArchiveStatus { get; private set; }

        public ICollection<Image> Images { get; private set; }

        public void AddImage(Image image)
        {
            Images.Add(image);
        }
        
        public void Update(Seal latestSeal)
        {
            if (ManifestId == latestSeal.ManifestId)
            {
                SealNumber = latestSeal.SealNumber;
                SealType = latestSeal.SealType;
                Note = latestSeal.Note;
                CreatedOn = latestSeal.CreatedOn;
                UpdatedOn = latestSeal.UpdatedOn;
                SealStatus = latestSeal.SealStatus;
                ArchiveStatus = latestSeal.ArchiveStatus;
            }
        }
    }
}
