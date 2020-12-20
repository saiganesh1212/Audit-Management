using AuthService.Controllers;
using AuthService.Models;
using AuthService.Provider;
using AuthService.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using System;

namespace AuthTest
{
    
        public class Tests
        {
            private AuthController _authController;
            private Mock<IAuthProvider> _authProvider;
            private Mock<IAuthRepo> _authRepo;
            private Mock<IConfiguration> _config;
            private string _jwt = "Success";
            private string _nullString = null;

            
            private AuthProvider _provider;
        
            [SetUp]
            public void Setup()
            {
                _authProvider = new Mock<IAuthProvider>();
                _authController = new AuthController(_authProvider.Object);
                _config = new Mock<IConfiguration>();
                _config.Setup(p => p["Jwt:Key"]).Returns("ThisismySecretKey");
                _authRepo = new Mock<IAuthRepo>();

                
                _provider = new AuthProvider(_authRepo.Object, _config.Object);
                 
            }

            

        //Tests involving Mock

            [Test]
            public void ControllerLoginSuccessTest()
            {
                _authProvider.Setup(x => x.GetUser(It.IsAny<User>())).Returns(_jwt);
                _authRepo.Setup(x => x.Login(It.IsAny<User>())).Returns(true);
                User user = new User() { UserId = 101, Username = "Suresh", Password = "Sur@" };
                var response = _authController.Login(user) as OkObjectResult;
                Assert.AreEqual(200, response.StatusCode);
                Assert.IsNotNull(response.ToString());
            }

            [Test]
            public void ControllerLoginFailureTest()
            {

                _authProvider.Setup(x => x.GetUser(It.IsAny<User>())).Returns(_nullString);
                _authRepo.Setup(x => x.Login(It.IsAny<User>())).Returns(false);
                User user = new User() { UserId = 102, Username = "Sai", Password = "Passw@" };
                var response = _authController.Login(user) as ObjectResult;
                Assert.AreEqual(404, response.StatusCode);
                Assert.AreEqual("User does not exist", response.Value);

            }

            [Test]
            public void ControllerExceptionTest()
            {
            _authProvider.Setup(x => x.GetUser(It.IsAny<User>())).Throws(new Exception());
            User user = new User() { UserId = 102, Username = "Sai", Password = "Passw@" };
            var response = _authController.Login(user) as ObjectResult;
            Assert.AreEqual(500, response.StatusCode);
            Assert.AreEqual("Unexpected error occured during login", response.Value);
            }

            [Test]
            public void ProviderFailureTest()
            {
            _authRepo.Setup(x => x.Login(It.IsAny<User>())).Returns(false);
            User user = new User() { UserId = 1, Username = "Gane", Password = "Sai@1212" };
            var result = _provider.GetUser(user);
            Assert.IsNull(result);
            }

            [Test]
            public void ProviderSuccessTest()
            {
            _authRepo.Setup(x => x.Login(It.IsAny<User>())).Returns(true);
            User user = new User() { UserId = 1, Username = "Ganesh", Password = "Sai@121" };
            var result = _provider.GetUser(user);
            Assert.IsNotNull(result);
            }
           
            [Test]
            public void ProviderExceptionTest()
            {
            _authRepo.Setup(x => x.Login(It.IsAny<User>())).Throws(new Exception());
            User user = new User() { UserId = 1, Username = "Ganesh", Password = "Sai@121" };
            var result = _provider.GetUser(user);
            Assert.IsNull(result);
            }

            


    }
    }