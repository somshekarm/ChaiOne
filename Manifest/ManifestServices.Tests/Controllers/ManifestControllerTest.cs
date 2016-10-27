using ManifestModels;
using ManifestRepository;
using ManifestResource.Controllers;
using ManifestResource.Models.Manifests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace ManifestResource.Tests.Controllers
{
    [TestClass]
    public class ManifestControllerTest
    {
        [TestMethod]
        public void GetAllManifest()
        {
            //Arrange            
            var manifestRepository = new Mock<ManifestRepository.IManifestRepository>(MockBehavior.Strict);
            var manifestCollection = new List<Manifest> { new Manifest(), new Manifest() };
            var manifests = manifestRepository.Setup(x => x.GetAll()).Returns(manifestCollection);
            var manifestController = new ManifestsController(manifestRepository.Object);
            //Act           
            var httpResponse = manifestController.GetManifests();
            var responseManifest = httpResponse.Content.ReadAsStringAsync();

            JavaScriptSerializer jss = new JavaScriptSerializer();
            var  returnedManifestCollection = jss.Deserialize<List<Manifest>>(responseManifest.Result);

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, httpResponse.StatusCode);
            Assert.IsTrue(returnedManifestCollection.Count() == 2);
        }

        [TestMethod]
        public void Expect_Exception_When_Getting_All_Manifests()
        {
            //Arrange            
            var manifestRepository = new Mock<ManifestRepository.IManifestRepository>(MockBehavior.Strict);
            var manifests = manifestRepository.Setup(x => x.GetAll()).Throws(new Exception("Unknown error"));

            //Act
            var manifestController = new ManifestsController(manifestRepository.Object);
            var httpResponse = manifestController.GetManifests();
            var responseManifest = httpResponse.Content.ReadAsStringAsync();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            var returnedException = jss.Deserialize<Exception>(responseManifest.Result);

            //Assert
            Assert.AreEqual(HttpStatusCode.NotFound, httpResponse.StatusCode);
            Assert.IsNotNull(returnedException);
        }

        [TestMethod]
        public void PostSealForAManifest()
        {
            //Arrange            
            var manifest = new Manifest("ABC", "DEF", "GHI", "LMN");
            var postSeal = new PostSeal { SealNumber = "Seal1", SealType = "Seal Type1", ManifestId = manifest.ID, ArchiveStatus = true };
            postSeal.PostImages = new List<PostImage>();
            var manifestRepository = new Mock<ManifestRepository.IManifestRepository>();
            manifestRepository.Setup(x => x.Get(manifest.ID)).Returns(manifest);
            manifestRepository.Setup(x => x.AddSeal(new Seal())).Verifiable();

            //Act
            var manifestController = new ManifestsController(manifestRepository.Object);
            var httpResponse = manifestController.PostSealForAManifest(postSeal, manifest.ID);
            var responseManifest = httpResponse.Content.ReadAsStringAsync();

            JavaScriptSerializer jss = new JavaScriptSerializer();
            var returnsealUri = jss.Deserialize<string>(responseManifest.Result);

            //Assert
            Assert.AreEqual(HttpStatusCode.Created, httpResponse.StatusCode);
            Assert.IsNotNull(returnsealUri);
        }

        [TestMethod]
        public void ExpectException_When_Post_Seal_For_A_Manifest_Has_No_Parent()
        {
            //Arrange                                   
            var guid = Guid.NewGuid();
            var manifestRepository = new Mock<ManifestRepository.IManifestRepository>();
            manifestRepository.Setup(x => x.Get(guid)).Throws(new Exception("Unnown Error"));

            //Act
            var manifestController = new ManifestsController(manifestRepository.Object);
            var httpResponse = manifestController.PostSealForAManifest(null, guid);
            var responseManifest = httpResponse.Content.ReadAsStringAsync();

            JavaScriptSerializer jss = new JavaScriptSerializer();
            var exception = jss.Deserialize<Exception>(responseManifest.Result);

            //Assert
            Assert.AreEqual(HttpStatusCode.NotFound, httpResponse.StatusCode);
            Assert.IsNotNull(exception);

        }

        
    }
}
