using NUnit.Framework;
using Moq;
using AuditSeverity.Provider;
using AuditSeverity.Controllers;
using AuditSeverity.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Net.Http;
using System.Net;
using Microsoft.AspNetCore.Http;
using AuditSeverity.Repository;

namespace AuditSeverityTest
{
    public class Tests
    {
        private Mock<IAuditSeverityProvider> _provider;
        private AuditSeverityController _controller;
        private Mock<IAuditSeverityRepository> _repomock;
        private AuditSeverityProvider _auditprov;
        [SetUp]
        public void Setup()
        {
            _provider = new Mock<IAuditSeverityProvider>();
            _controller = new AuditSeverityController(_provider.Object);
            _repomock = new Mock<IAuditSeverityRepository>();
            _auditprov = new AuditSeverityProvider(_repomock.Object);
        }
        [Test]
        public void ControllerSuccessTest()
        {
            AuditResponse auditResponse = new AuditResponse()
            {
                AuditId = 348568,
                RemedialActionDuration = "Within week",
                ProjectExecutionStatus = "Green"
            };
            _provider.Setup(x => x.GetAuditType(It.IsAny<AuditRequest>())).Returns(Task.FromResult<AuditResponse>(auditResponse));
            AuditRequest request = new AuditRequest()
            {
                AuditDetail = new AuditDetail()
                {
                    AuditType = "Internal",
                    AuditDate = new System.DateTime(2020, 10, 1)
                },
                ApplicationOwnerName = "Alisha",
                ProjectManagerName = "Jayashree",

            };
            var result = _controller.GetAuditResponse(request) as OkObjectResult;
            Assert.AreEqual(200, result.StatusCode);

        }
        [Test]
        public void ControllerFailureTest()
        {

            _provider.Setup(x => x.GetAuditType(It.IsAny<AuditRequest>())).Returns(Task.FromResult<AuditResponse>((AuditResponse)null));
            AuditRequest request = new AuditRequest()
            {
                AuditDetail = new AuditDetail()
                {
                    AuditType = "SOX",
                    AuditDate = new System.DateTime(2020, 10, 1)
                },
                ApplicationOwnerName = "Alisha",
                ProjectManagerName = "Jayashree",

            };
            var result = _controller.GetAuditResponse(request) as ObjectResult;
            Assert.AreEqual(404, result.StatusCode);

        }

        [Test]
        public void ControllerExceptionTest()
        {
            _provider.Setup(x => x.GetAuditType(It.IsAny<AuditRequest>())).Throws(new Exception());
            AuditRequest request = new AuditRequest()
            {
                AuditDetail = new AuditDetail()
                {
                    AuditType = "SOX",
                    AuditDate = new System.DateTime(2020, 10, 1)
                },
                ApplicationOwnerName = "Alisha",
                ProjectManagerName = "Jayashree",

            };
            var result = _controller.GetAuditResponse(request) as ObjectResult;
            Assert.AreEqual(500, result.StatusCode);
            Assert.AreEqual("Some error occured during processing", result.Value);
        }
        [Test]
        public void ProviderSuccessTest()
        {
            AuditResponse auditResponse = new AuditResponse()
            {
                AuditId = 123456789,
                RemedialActionDuration = "No Action Needed",
                ProjectExecutionStatus = "Green"
            };
            AuditRequest request = new AuditRequest()
            {
                AuditDetail = new AuditDetail()
                {
                    AuditType = "Internal",
                    AuditDate = new System.DateTime(2020, 05, 11)
                },
                ApplicationOwnerName = "Alisha",
                ProjectManagerName = "Meghana",

            };
            _repomock.Setup(x => x.GetBenchMarkValues(It.IsAny<AuditRequest>())).Returns(Task.FromResult<AuditResponse>(auditResponse));
            var result = _auditprov.GetAuditType(request);
            Assert.NotNull(result);
        }
        [Test]
        public void ProviderFailureTest()
        {
            AuditRequest request = new AuditRequest()
            {
                AuditDetail = new AuditDetail()
                {
                    AuditType = "Internal",
                    AuditDate = new System.DateTime(2020, 05, 11)
                },
                ApplicationOwnerName = "Alisha",
                ProjectManagerName = "Meghana",

            };
            _repomock.Setup(x => x.GetBenchMarkValues(It.IsAny<AuditRequest>())).Returns(Task.FromResult<AuditResponse>((AuditResponse)null));
            var result = _auditprov.GetAuditType(request);
            Assert.AreEqual(null,result.Result);
        }
        [Test]
        public void ProviderExceptionTest()
        {
            AuditRequest request = new AuditRequest()
            {
                AuditDetail = new AuditDetail()
                {
                    AuditType = "Internal",
                    AuditDate = new System.DateTime(2020, 05, 11)
                },
                ApplicationOwnerName = "Alisha",
                ProjectManagerName = "Meghana",

            };

            _repomock.Setup(x => x.GetBenchMarkValues(It.IsAny<AuditRequest>())).Throws(new Exception());
            var result = _auditprov.GetAuditType(request).Result;
            Assert.IsNull(result);
        }

    }
}