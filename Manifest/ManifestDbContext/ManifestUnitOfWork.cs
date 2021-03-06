﻿using ManifestModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ManifestDbContext
{
    public class ManifestUnitOfWork : DbContext, IManifestUnitOfWork
    {
        public DbSet<Manifest> Manifest { get; set; }           
        public DbSet<Seal> Seal { get; set; }
        public DbSet<Image> Image { get; set; }
        public DbSet<AppServer> AppServer { get; set; }


        public ICollection<Manifest> ManifestCollection
        {
            get
            {
                return Manifest.Include("Seals").Include("Seals.Images").ToList();
            }
        }

        public ICollection<AppServer> AppServers
        {
            get
            {
                return AppServer.ToList();
            }
        }

        public List<Seal> Seals(Guid manifestId)
        {
            return ManifestCollection.Where(manifest => manifest.ID == manifestId).First().Seals.ToList();
        }

        public List<AppServer> GetAppServers()
        {
            return AppServers.ToList();
        }

        public void AddManifest(Manifest manifestDataEntity)
        {
            if(manifestDataEntity != null)
            {
                Manifest.Add(manifestDataEntity);
                CompleteWork();
            }
        }
        public void UpdateSeal(Seal oldSeal, Seal latestSeal)
        {
            oldSeal.Update(latestSeal);
            CompleteWork();
        }

        public void UpdateManifest(Manifest oldManifest, Manifest latestManifest)
        {
            oldManifest.Update(latestManifest);
            CompleteWork();
        }

        public void AddSeal(Seal sealDataEntity)
        {
            if(sealDataEntity != null)
            {
                Seal.Add(sealDataEntity);
                CompleteWork();
            }            
        }

        public void AddImage(Image imageDataEntity)
        {
            if(imageDataEntity != null)
            {
                Image.Add(imageDataEntity);
                CompleteWork();
            }
        }

        public void AddAppServers(List<AppServer> appServerDataEntity)
        {
            if(appServerDataEntity != null && appServerDataEntity.Count > 0)
            {
                foreach(var appserver in appServerDataEntity)
                {
                    AppServer.Add(appserver);
                }                    
            }

            CompleteWork();

        }

        public void CompleteWork()  
        {
            SaveChanges();
        }       
    }
}
