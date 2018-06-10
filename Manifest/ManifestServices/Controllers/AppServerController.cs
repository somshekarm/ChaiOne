using ManifestModels;
using ManifestRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.Cors;
using ManifestServices.Models;
using System.Web.Script.Serialization;

namespace ManifestServices.Controllers
{
    public class AppServerController : ApiController
    {

        public AppServerController(IAppServerRepository appServerRepository)
        {
            this.appServerRepository = appServerRepository;
        }
        public IAppServerRepository appServerRepository { get; private set; }

        /// <summary>
        /// Creates AppServer
        /// </summary>
        /// <param name="appserverModels">Name of the AppServer.</param>
        /// <returns></returns>
        [Route("api/AppServer")]
        public HttpResponseMessage PostAppServer(List<AppServerModel> appserverModels)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = new HttpResponseMessage(HttpStatusCode.Created);
                    var appServers = new List<AppServer>();
                    foreach (var appserverModel in appserverModels)
                    {
                        var appServer = new AppServer(appserverModel.Name);
                        appServer.AddUri(string.Format("/api/appserver/{0}", appServer.Id));
                        appServers.Add(appServer);
                    }
                    appServerRepository.AddAppServers(appServers);
                    response.Content = new ObjectContent(typeof(string), "Server names aded", GlobalConfiguration.Configuration.Formatters.JsonFormatter);
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


        [Route("api/AppServer")]
        public HttpResponseMessage GetAppServer()
        {
            try
            {
                var response = new HttpResponseMessage(HttpStatusCode.Created);
                var appServers =  appServerRepository.GetAppServer();

                response.Content = new ObjectContent(typeof(List<AppServer>), appServers, GlobalConfiguration.Configuration.Formatters.JsonFormatter);
                return response;
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
