using AppLayerAPI.Controllers;
using BLL.Models;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace HospitalAppointmentSystem.Tests.Controllers
{
    public class AppointmentControllerTests
    {
        private readonly Mock<AppointmentService> _mockService;
        private readonly AppointmentController _controller;

        public AppointmentControllerTests()
        {
            _mockService = new Mock<AppointmentService>();
            _controller = new AppointmentController(_mockService.Object);
        }

        [Fact]
        public void Pending_ShouldReturnOkObjectResult_WithPendingList()
        {
            // Arrange
            var list = new List<AppointmentModel>
            {
                new AppointmentModel { Id = 1, DoctorId = 1, PatientId = 1, Status = "Pending" }
            };
            _mockService.Setup(s => s.GetPending()).Returns(list);

            // Act
            var actionResult = _controller.Pending();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(actionResult);
            var data = Assert.IsAssignableFrom<List<AppointmentModel>>(okResult.Value);
            Assert.Single(data);
            Assert.Equal("Pending", data[0].Status);
        }

        [Fact]
        public void UpdateStatus_ShouldReturnOkObjectResult_WhenStatusUpdated()
        {
            // Arrange
            _mockService.Setup(s => s.UpdateStatus(1, "Confirmed")).Returns(true);

            // Act
            var actionResult = _controller.UpdateStatus(1, "Confirmed");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(actionResult);
            Assert.Equal(true, okResult.Value);
        }

        [Fact]
        public void Delete_ShouldReturnOkObjectResult_WhenDeleted()
        {
            // Arrange
            _mockService.Setup(s => s.Delete(1)).Returns(true);

            // Act
            var actionResult = _controller.Delete(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(actionResult);
            Assert.Equal(true, okResult.Value);
        }
    }
}
