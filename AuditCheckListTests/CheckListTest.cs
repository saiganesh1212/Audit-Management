using AuditCheckList.Controllers;
using AuditCheckList.Provider;
using AuditCheckList.Repository;
using Castle.Core.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;

namespace AuditCheckListTests
{
    [TestFixture]
    public class Tests
    {
        private Mock<IAuditProvider> _provider;
        private AuditCheckListController controllerObj;
        private Mock<IAuditRepository> _repo;
        private AuditProvider _auditprov;
        [SetUp]
        public void Setup()
        {
            _provider = new Mock<IAuditProvider>();
            controllerObj = new AuditCheckListController(_provider.Object);
            _repo = new Mock<IAuditRepository>();
            _auditprov = new AuditProvider(_repo.Object);
        }
        [Test]
        public void ReturnInternalList()
        {
            _provider.Setup(p => p.GetList("Internal")).Returns(new List<string>
            {
            "Have all Change requests followed SDLC before PROD move?",
            "Have all Change requests been approved by the application owner?",
            "Is data deletion from the system done with application owner approval?"
            });
            var result = controllerObj.Get("Internal");

            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }
        [Test]
        public void ReturnSOXList()
        {
            _provider.Setup(p => p.GetList("SOX")).Returns(new List<string>
            {
           "Have all Change requests followed SDLC before PROD move? ",
            "Have all Change requests been approved by the application owner?",
            "For a major change, was there a database backup taken before and after PROD move?",
            "Has the application owner approval obtained while adding a user to the system?",
            "Is data deletion from the system done with application owner approval"
            });
            var result = controllerObj.Get("SOX");

            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }
        [Test]
        public void ReturnNullList()
        {
            List<string> nullList = null;
            _provider.Setup(p => p.GetList(null)).Returns(nullList);
            var result = controllerObj.Get(null) as ObjectResult;
            Assert.AreEqual(404, result.StatusCode);
            Assert.AreEqual("No questions are present", result.Value);
        }
        [Test]
        public void ProviderFailureTest()
        {
            string s = "internal";
            List<string> nullList = null;
            _repo.Setup(p => p.GetByType(s)).Returns(nullList);
            var result = _auditprov.GetList(s);
            Assert.IsNull(result);
        }
        [Test]
        public void ProviderSuccessTest()
        {
            string s1 = "SOX";
            _repo.Setup(p => p.GetByType(s1)).Returns(new List<string>(){"Have all Change requests followed SDLC before PROD move? ","Have all Change requests been approved by the application owner?"});
            var result = _auditprov.GetList(s1);
            Assert.IsNotNull(result);
        }
        [Test]
        public void ControllerExceptionTest()
        {
            string s = null;
            _provider.Setup(p => p.GetList(s)).Throws(new Exception());
            var response = controllerObj.Get(s) as ObjectResult;
            Assert.AreEqual(500, response.StatusCode);
            Assert.AreEqual("Error in fetching details", response.Value);
        }
        [Test]
        public void RepositoryExceptionTest()
        {
            string s = null;
            List<string> nullList = new List<string>();
            _repo.Setup(p => p.GetByType(s)).Throws(new Exception());
            var result = _auditprov.GetList(s);
            Assert.AreEqual(null, result);
        }
    }
}