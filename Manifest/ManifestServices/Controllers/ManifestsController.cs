using ManifestDbContext;
using ManifestModels;
using ManifestResource.Models.Manifests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace ManifestResource.Controllers
{
    /// <summary>
    /// Web api controller to implement Manifests class.
    /// </summary>    
    //[Authorize]
    public class ManifestsController : ApiController
    {        

        /// <summary>
        /// Default constructor
        /// </summary>
        public ManifestsController(ManifestRepository.IManifestRepository manifestRepository)
        {
            this.ManifestRepository = manifestRepository;            
        }

        public ManifestRepository.IManifestRepository ManifestRepository { get; private set; }

        /// <summary>
        /// Gets all Manifests.
        /// </summary>
        /// <returns>HttpResponse with status code</returns>
        [Route("api/Manifests")]
        public HttpResponseMessage GetManifests()
        {
            try
            {
                var manifests = ManifestRepository.GetAll();
                var response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new ObjectContent(typeof(IEnumerable<Manifest>), manifests, GlobalConfiguration.Configuration.Formatters.JsonFormatter);                
                return response;
            }
            catch(Exception exception)
            {
                var response = new HttpResponseMessage(HttpStatusCode.NotFound);
                response.Content = new ObjectContent(typeof(Exception), exception, GlobalConfiguration.Configuration.Formatters.JsonFormatter);
                return response;
            }
        }

        /// <summary>
        /// Gets a Manifest for a given ID.
        /// </summary>
        /// <param name="ID">ManifestID to get.</param>
        /// <returns>HttpResponse with status code</returns>
        [Route("api/Manifests/{manifestId}")]
        public HttpResponseMessage GetManifest(Guid ID)
        {
            try
            {
                var manifest = ManifestRepository.Get(ID);
                var response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new ObjectContent(typeof(Manifest), manifest, GlobalConfiguration.Configuration.Formatters.JsonFormatter);
                return response;
            }
            catch(Exception exception)
            {
                var response = new HttpResponseMessage(HttpStatusCode.NotFound);
                response.Content = new ObjectContent(typeof(Exception), exception, GlobalConfiguration.Configuration.Formatters.JsonFormatter);
                return response;
            }
        }

        /// <summary>
        /// Creates new Manifest
        /// </summary>
        /// <param name="postManifest">Manifest resource to be created.</param>
        /// <returns>HttpResponse with status code</returns>
        [Route("api/Manifests")]
        public HttpResponseMessage PostManifest(PostManifest postManifest)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var manifest = new Manifest(postManifest.OfficerName, postManifest.OriginLocation, postManifest.ReceivingLocation, postManifest.Name);
                    foreach (var postSeal in postManifest.PostSeals)
                    {
                        var seal = new Seal(postSeal.SealNumber, postSeal.SealType, postSeal.Note, postSeal.CreatedOn,
                                                    postSeal.UpdatedOn, manifest.ID, postSeal.ArchiveStatus, postSeal.SealStatus);
                        seal.AddUri(string.Format("api/Manifests/{0}/Seals/{1}", manifest.ID, seal.ID));
                        foreach (var postImage in postSeal.PostImages)
                        {
                            var image = new Image(postImage.File, seal.ID, postImage.CreatedOn,
                                                    postImage.UpdatedOn, postImage.ImageableId, postImage.ImageFileType);
                            image.AddUri(string.Format("api/Manifests/{0}/Seals/{1}/Images/{2}", manifest.ID, seal.ID, image.ID));
                            seal.AddImage(image);
                        }

                        manifest.AddSeal(seal);
                    }
                    manifest.AddUri(string.Format("api/Manifests/{0}", manifest.ID));
                    ManifestRepository.AddManifest(manifest);
                    var response = new HttpResponseMessage(HttpStatusCode.Created);
                    response.Content = new ObjectContent(typeof(string), manifest.Uri, GlobalConfiguration.Configuration.Formatters.JsonFormatter);
                    return response;
                }
                else
                {
                    var modelResponse = new HttpResponseMessage(HttpStatusCode.BadRequest);
                    modelResponse.Content = new ObjectContent(typeof(ModelStateDictionary), ModelState, GlobalConfiguration.Configuration.Formatters.JsonFormatter);
                    return modelResponse;
                }
            }
            catch(Exception exception)
            {
                var response = new HttpResponseMessage(HttpStatusCode.NotFound);
                response.Content = new ObjectContent(typeof(Exception), exception, GlobalConfiguration.Configuration.Formatters.JsonFormatter);
                return response;
            }
        }

        /// <summary>
        /// Updates a Manifest for a given Id.
        /// </summary>
        /// <param name="postManifest">Latest Manifest to be updated.</param>
        /// <param name="Id">ManifestId to be updated.</param>
        /// <returns>HttpResponse with status code</returns>
        [Route("api/Manifests/{manifestId}")]
        public HttpResponseMessage PutManifestId(PostManifest postManifest, Guid Id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var latestManifest = new Manifest(postManifest.OfficerName, postManifest.OriginLocation, postManifest.ReceivingLocation, postManifest.EstimatedTimeofArrival, postManifest.CreatedOn, postManifest.UpdatedOn,
                    postManifest.SiteId, postManifest.Name, postManifest.ManifestStatus);
                    var oldManifest = ManifestRepository.Get(Id);
                    ManifestRepository.UpdateManifest(oldManifest, latestManifest);
                    var response = new HttpResponseMessage(HttpStatusCode.OK);
                    return response;
                }
                else
                {
                    var modelResponse = new HttpResponseMessage(HttpStatusCode.BadRequest);
                    modelResponse.Content = new ObjectContent(typeof(ModelStateDictionary), ModelState, GlobalConfiguration.Configuration.Formatters.JsonFormatter);
                    return modelResponse;
                }
            }
            catch(Exception exception)
            {
                var response = new HttpResponseMessage(HttpStatusCode.NotFound);
                response.Content = new ObjectContent(typeof(Exception), exception, GlobalConfiguration.Configuration.Formatters.JsonFormatter);
                return response;
            }
        }

        /// <summary>
        /// Get collection of Seal for a given Manifest Id.
        /// </summary>
        /// <param name="manifestId">Manifest ID to get collection of Seal for a Manifest.</param>
        /// <returns>HttpResponse with status code</returns>
        [Route("api/Manifests/{manifestId}/Seals")]
        public HttpResponseMessage GetSealsByManifest(Guid manifestId)
        {
            try
            {
                var seals = ManifestRepository.GetSealByManifest(manifestId);
                var response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new ObjectContent(typeof(List<Seal>), seals, GlobalConfiguration.Configuration.Formatters.JsonFormatter);
                return response;
            }
            catch(Exception exception)
            {
                var response = new HttpResponseMessage(HttpStatusCode.NotFound);
                response.Content = new ObjectContent(typeof(Exception), exception, GlobalConfiguration.Configuration.Formatters.JsonFormatter);
                return response;
            }
        }

        /// <summary>
        /// Gets a Seal for a given Seal Id.
        /// </summary>
        /// <param name="manifestId">ManifestId to consider for retriving Seal.</param>
        /// <param name="sealId">sealId to consider for retriving a Seal.</param>
        /// <returns>HttpResponse with status code</returns>
        [Route("api/Manifests/{manifestId}/Seals/{SealId}")]
        public HttpResponseMessage GetSealForAManifest(Guid manifestId, Guid sealId)
        {
            try
            {
                var seal = ManifestRepository.GetSealByManifest(manifestId).Where(x => x.ID == sealId).First();
                var response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new ObjectContent(typeof(Seal), seal, GlobalConfiguration.Configuration.Formatters.JsonFormatter);
                return response;
            }
            catch(Exception exception)
            {
                var response = new HttpResponseMessage(HttpStatusCode.NotFound);
                response.Content = new ObjectContent(typeof(Exception), exception, GlobalConfiguration.Configuration.Formatters.JsonFormatter);
                return response;
            }
        }

        /// <summary>
        /// Creates a new Seal for a Manifst.
        /// </summary>
        /// <param name="postSeal">Seal data to be created.</param>
        /// <param name="manifestId">Seal resource will be created for <see cref="Manifest"/>. </param>
        /// <returns>HttpResponse with status code</returns>
        [Route("api/Manifests/{manifestId}/Seals")]
        public HttpResponseMessage PostSealForAManifest(PostSeal postSeal, Guid manifestId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var manifest = ManifestRepository.Get(manifestId);

                    if (manifest != null)
                    {
                        var seal = new Seal(postSeal.SealNumber, postSeal.SealType, postSeal.Note, postSeal.CreatedOn, postSeal.UpdatedOn, manifestId, postSeal.ArchiveStatus, postSeal.SealStatus);
                        foreach (var postImage in postSeal.PostImages)
                        {
                            var image = new Image(postImage.File, seal.ID);
                            image.AddUri(string.Format("api/Manifests/{0}/Seals/{1}/Images/{2}", manifestId, seal.ID, image.ID));
                            seal.AddImage(image);
                        }
                        seal.AddUri(string.Format("api/Manifests/{0}/Seals/{1}", manifestId, seal.ID));
                        ManifestRepository.AddSeal(seal);
                        var response = new HttpResponseMessage(HttpStatusCode.Created);
                        response.Content = new ObjectContent(typeof(string), seal.Uri, GlobalConfiguration.Configuration.Formatters.JsonFormatter);
                        return response;
                    }
                    var errorResponse = new HttpResponseMessage(HttpStatusCode.NotFound);
                    errorResponse.Content = new ObjectContent(typeof(Manifest), null, GlobalConfiguration.Configuration.Formatters.JsonFormatter);
                    return errorResponse;
                }
                else
                {
                    var modelResponse = new HttpResponseMessage(HttpStatusCode.BadRequest);
                    modelResponse.Content = new ObjectContent(typeof(ModelStateDictionary), ModelState, GlobalConfiguration.Configuration.Formatters.JsonFormatter);
                    return modelResponse;
                }
            }
            catch(Exception exception)
            {

                var response = new HttpResponseMessage(HttpStatusCode.NotFound);
                response.Content = new ObjectContent(typeof(Exception), exception, GlobalConfiguration.Configuration.Formatters.JsonFormatter);
                return response;
            }
        }

        /// <summary>
        /// Updates Seal for a Manifest.
        /// </summary>
        /// <param name="postSeal">Latest Seal to be considered for updating.</param>
        /// <param name="manifestId">Manifest Id to consider for updating.</param>
        /// <param name="sealId">Seal Id to consider for updating.</param>
        /// <returns>HttpResponse with status code</returns>
        [Route("api/Manifests/{manifestId}/Seals/{SealId}")]
        public HttpResponseMessage PutSealForAManifest(PostSeal postSeal, Guid manifestId, Guid sealId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var oldSeal = ManifestRepository.GetSealByManifest(manifestId).Where(x => x.ID == sealId).First();
                    var latestSeal = new Seal(postSeal.SealNumber, postSeal.SealType, postSeal.Note, postSeal.CreatedOn, postSeal.UpdatedOn, manifestId, postSeal.ArchiveStatus, postSeal.SealStatus);
                    ManifestRepository.UpdateSeal(oldSeal, latestSeal);
                    var response = new HttpResponseMessage(HttpStatusCode.OK);
                    response.Content = new ObjectContent(typeof(Manifest), null, GlobalConfiguration.Configuration.Formatters.JsonFormatter);
                    return response;
                }
                else
                {
                    var modelResponse = new HttpResponseMessage(HttpStatusCode.BadRequest);
                    modelResponse.Content = new ObjectContent(typeof(ModelStateDictionary), ModelState, GlobalConfiguration.Configuration.Formatters.JsonFormatter);
                    return modelResponse;
                }
            }
            catch (Exception exception)
            {

                var response = new HttpResponseMessage(HttpStatusCode.NotFound);
                response.Content = new ObjectContent(typeof(Exception), exception, GlobalConfiguration.Configuration.Formatters.JsonFormatter);
                return response;
            }
        }

        /// <summary>
        /// Creates a Image for a Seal.
        /// </summary>
        /// <param name="postImage">Image to be created</param>
        /// <param name="manifestId">Manifest Id to consider for creating Image object.</param>
        /// <param name="sealId">Seal Id to consider for creating Image object.</param>
        /// <returns>HttpResponse with status code</returns>
        [Route("api/Manifests/{manifestId}/Seals/{SealId}/Images")]
        public HttpResponseMessage PostImageForSeal(PostImage postImage, Guid manifestId, Guid sealId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var image = new Image(postImage.File, sealId, postImage.CreatedOn, postImage.UpdatedOn, postImage.ImageableId, postImage.ImageFileType);
                    image.AddUri(string.Format("api/Manifests/{0}/Seals/{1}/Images/{2}", manifestId, sealId, image.ID));
                    ManifestRepository.AddImage(image);
                    var response = new HttpResponseMessage(HttpStatusCode.Created);
                    response.Content = new ObjectContent(typeof(string), image.Uri, GlobalConfiguration.Configuration.Formatters.JsonFormatter);
                    return response;
                }
                else
                {
                    var modelResponse = new HttpResponseMessage(HttpStatusCode.BadRequest);
                    modelResponse.Content = new ObjectContent(typeof(ModelStateDictionary), ModelState, GlobalConfiguration.Configuration.Formatters.JsonFormatter);
                    return modelResponse;
                }
            }
            catch (Exception exception)
            {

                var response = new HttpResponseMessage(HttpStatusCode.NotFound);
                response.Content = new ObjectContent(typeof(Exception), exception, GlobalConfiguration.Configuration.Formatters.JsonFormatter);
                return response;
            }
        }
    }
}