using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManifestModels
{
    public class Manifest : BaseModel
    {
        public Manifest() 
        {
            this.ID = Guid.NewGuid();
        }
        
        public Manifest(string officerName, string originLocation, string receivingLocation, string name, ManifestStatus manifestStatus = ManifestStatus.NeedsApproval)
        {
            this.ID = Guid.NewGuid();
            OfficerName = officerName;
            OriginLocation = originLocation;
            ReceivingLocation = receivingLocation;
            ManifestStatus = manifestStatus;
            Name = name;
            Seals = new List<Seal>();
        }
        public Manifest(string officerName, string originLocation, string receivingLocation, DateTime? estimatedTimeOfArrival, DateTime? createdOn, DateTime? updatedOn, 
            int siteId, string name, ManifestStatus manifestStatus = ManifestStatus.NeedsApproval) : this(officerName,originLocation,receivingLocation,name)
        {
            this.ID = Guid.NewGuid();
            OfficerName = officerName;
            OriginLocation = originLocation;
            ReceivingLocation = receivingLocation;
            EstimatedTimeofArrival = estimatedTimeOfArrival;
            CreatedOn = createdOn;
            UpdatedOn = updatedOn;
            SiteId = siteId;
            ManifestStatus = manifestStatus;
            Name = name;            
        }
        public Guid ID { get; private set; }
        public string OfficerName { get; private set; }
        public string OriginLocation { get; private set; }
        public string ReceivingLocation { get; private set; }
        public DateTime? EstimatedTimeofArrival { get; private set; }
        
        public DateTime? CreatedOn { get; private set; }
        
        public DateTime? UpdatedOn { get; private set; }
        public int SiteId { get; set; }

        public ICollection<Seal> Seals { get; private  set; }

        public ManifestStatus ManifestStatus { get; private set; }

        public string Name { get; private set; }

        public void AddSeal(Seal seal)
        {            
            Seals.Add(seal);            
        }

        public void Update(Manifest latestManifest)
        {
            OfficerName = latestManifest.OfficerName;
            OriginLocation = latestManifest.OriginLocation;
            ReceivingLocation = latestManifest.ReceivingLocation;
            EstimatedTimeofArrival = latestManifest.EstimatedTimeofArrival;
            CreatedOn = latestManifest.CreatedOn;
            UpdatedOn = latestManifest.UpdatedOn;
            SiteId = latestManifest.SiteId;
            ManifestStatus = latestManifest.ManifestStatus;
            Name = latestManifest.Name;
        }
    }
}
