using ActivitySystem.Controllers;
using ActivitySystem.Models;
using NUnit.Framework;
using NSubstitute;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using ActivitySystem.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ActivitySystem.Test
{
    [TestFixture()]
    public class HomeControllerTests
    {
        private IActivityRepository _fakeActivityRepository;
        private IEnrollRepository _fakeEnrollRepository;
        private ILogger<HomeController> _fakeLogger;
        private IWebHostEnvironment _fakeEnv;
        private UserManager<ApplicationUser> _fakeUserManager;
        private HomeController _target;

        [SetUp]
        public void Setup()
        {
            _fakeActivityRepository = Substitute.For<IActivityRepository>();
            _target = new HomeController(_fakeLogger, _fakeActivityRepository, _fakeEnv,_fakeUserManager,_fakeEnrollRepository);
        }

        [Test]
        public void TestIndex()
        {
            // Arrange
            var activities = _fakeActivityRepository.GetActivities();
            ActivityListViewModel expected = new ActivityListViewModel
            {
                Activities = activities
            };

            // Act
            var viewResult = _target.Index() as ViewResult;
            var actual = (ActivityListViewModel)viewResult.Model;

            // Assert
            Assert.AreEqual(expected.Activities, actual.Activities);

        }
    }
}