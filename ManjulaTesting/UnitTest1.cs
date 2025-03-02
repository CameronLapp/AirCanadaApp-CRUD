using AirCanadaApp.Controllers;
using AirCanadaApp.Data;
using AirCanadaApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace AirCanadaApp.Tests
{
    [TestClass]
    public class FlightDatasControllerTests
    {
        private ApplicationDbContext _context = null!;
        private FlightDatasController _controller = null!;

        [TestInitialize]
        public void TestInitialize()
        {
            // Create new in-memory database to pass as dependency to the controller
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _context = new ApplicationDbContext(options);

            // Seed data into the mock database
            _context.FlightData.Add(new FlightData
            {
                FlightDataId = 1,
                FlightNumber = "AC101",
                DepartureCity = "Vancouver",
                ArrivalCity = "Toronto",
                DepartureTime = DateTime.Now,
                ArrivalTime = DateTime.Now.AddHours(5),
                Price = 500.0f
            });
            _context.FlightData.Add(new FlightData
            {
                FlightDataId = 2,
                FlightNumber = "AC102",
                DepartureCity = "Calgary",
                ArrivalCity = "Edmonton",
                DepartureTime = DateTime.Now,
                ArrivalTime = DateTime.Now.AddHours(3),
                Price = 200.0f
            });
            _context.SaveChanges();

            // Instantiate FlightDatasController with mock database
            _controller = new FlightDatasController(_context);
        }

        #region "Index"
        [TestMethod]
        public async Task IndexReturnsView()
        {
            // Act
            var result = (ViewResult)await _controller.Index();

            // Assert
            Assert.AreEqual("Index", result.ViewName);
        }

        [TestMethod]
        public async Task IndexReturnsFlightDataList()
        {
            // Act
            var result = (ViewResult)await _controller.Index();
            var dataModel = (List<FlightData>)result.Model!;

            // Assert
            CollectionAssert.AreEqual(_context.FlightData.ToList(), dataModel);
        }
        #endregion

        #region "Details"
        [TestMethod]
        public async Task DetailsNoIdReturnsNotFound()
        {
            // Act
            var result = (NotFoundResult)await _controller.Details(null);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task DetailsInvalidIdReturnsNotFound()
        {
            // Act
            var result = (NotFoundResult)await _controller.Details(999);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task DetailsValidIdReturnsView()
        {
            // Act
            var result = (ViewResult)await _controller.Details(1);

            // Assert
            Assert.AreEqual("Details", result.ViewName);
        }

        [TestMethod]
        public async Task DetailsValidIdReturnsFlightData()
        {
            // Arrange
            int id = 1;

            // Act
            var result = (ViewResult)await _controller.Details(id);
            var flight = (FlightData)result.Model!;

            // Assert
            Assert.AreEqual(_context.FlightData.Find(id), flight);
        }
        #endregion

        #region "Create"
        [TestMethod]
        public async Task CreateValidFlightDataRedirectsToIndex()
        {
            // Arrange
            var newFlight = new FlightData
            {
                FlightDataId = 3,
                FlightNumber = "AC103",
                DepartureCity = "Toronto",
                ArrivalCity = "Montreal",
                DepartureTime = DateTime.Now,
                ArrivalTime = DateTime.Now.AddHours(2),
                Price = 300.0f
            };

            // Act
            var result = (RedirectToActionResult)await _controller.Create(newFlight);

            // Assert
            Assert.AreEqual("Index", result.ActionName);
        }

        [TestMethod]
        public async Task CreateInvalidFlightDataReturnsView()
        {
            // Arrange
            _controller.ModelState.AddModelError("FlightNumber", "Required");
            var newFlight = new FlightData();

            // Act
            var result = (ViewResult)await _controller.Create(newFlight);

            // Assert
            Assert.IsFalse(_controller.ModelState.IsValid);
            Assert.AreEqual("Create", result.ViewName);
        }
        #endregion
    }
}
