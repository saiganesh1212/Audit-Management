using AuditBenchmark.Controllers;
using AuditBenchmark.Models;
using AuditBenchmark.Provider;
using AuditBenchmark.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;

namespace AuditBenchmarkTesting
{
    [TestFixture]
    public class Tests
    {
        private Mock<IAuditBenchmarkProvider> _pro;
        private AuditBenchmarkController _controllerObj;
        private Mock<IAuditBenchmarkRepository> _repo;
        private AuditBenchmarkProvider _providerObj;

        [SetUp]
        public void Setup()
        {

            _pro = new Mock<IAuditBenchmarkProvider>();
            _controllerObj = new AuditBenchmarkController(_pro.Object);
            _repo = new Mock<IAuditBenchmarkRepository>();
            _providerObj = new AuditBenchmarkProvider(_repo.Object);
        }
        //Controller Success Test
        [Test]
        public void ControllerSuccessTest()
        {
            List<AuditBenchmark.Models.AuditBenchmark> list=new List<AuditBenchmark.Models.AuditBenchmark>{new AuditBenchmark.Models.AuditBenchmark(){AuditType = "Internal", BenchmarkNoAnswers = 3} };
            _pro.Setup(x => x.GetAll()).Returns(list);
            var result = _controllerObj.Get() as ObjectResult;
            Assert.AreEqual(200, result.StatusCode);

        }

        [Test]
        public void ControllerFailTest()
        {
            _pro.Setup(x => x.GetAll()).Returns((List<AuditBenchmark.Models.AuditBenchmark>)null);
            var result = _controllerObj.Get() as ObjectResult;
            Assert.AreEqual(404, result.StatusCode);
            Assert.AreEqual("BenchMark does not exist", result.Value);
        }
        [Test]
        public void ControlerExceptionTest()
        {
            _pro.Setup(x => x.GetAll()).Throws(new Exception());
            var result = _controllerObj.Get() as ObjectResult;
            Assert.AreEqual(500, result.StatusCode);
            Assert.AreEqual("Unexpected error occured", result.Value);
        }
        //Provider Success Test
        [Test]
        public void ProviderSuccessForInternalAuditTypeTest()
        {
            _repo.Setup(x => x.GetAll()).Returns(new List<AuditBenchmark.Models.AuditBenchmark>{new AuditBenchmark.Models.AuditBenchmark()
            {
                AuditType = "Internal", BenchmarkNoAnswers = 3
            }
            });
            var result = _providerObj.GetAll();
            Assert.IsNotNull(result);
        }
        [Test]
        public void ProviderSuccessForSOXAuditTypeTest()
        {
            _repo.Setup(x => x.GetAll()).Returns(new List<AuditBenchmark.Models.AuditBenchmark>{new AuditBenchmark.Models.AuditBenchmark()
            {
                AuditType = "SOX", BenchmarkNoAnswers = 1
            } });
            var result = _providerObj.GetAll();
            Assert.IsNotNull(result);
        }
        [Test]
        public void ProviderFailureTest()
        {
            List<AuditBenchmark.Models.AuditBenchmark> nullList = null;
            _repo.Setup(p => p.GetAll()).Returns(nullList);
            var result = _providerObj.GetAll();
            Assert.IsNull(result);
        }
        [Test]
        public void ProviderExceptionTest()
        {
            List<AuditBenchmark.Models.AuditBenchmark> nullList = null;
            _repo.Setup(x => x.GetAll()).Returns(nullList);
            var result = _providerObj.GetAll();
            Assert.AreEqual(null, result);
        }

    }
}