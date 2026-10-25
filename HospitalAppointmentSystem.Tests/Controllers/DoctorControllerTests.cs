using AppLayerAPI.Controllers;
using BLL.Models;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace HospitalAppointmentSystem.Tests.Controllers
{
    public class DoctorControllerTests
    {
        private readonly Mock<DoctorService> _mockService;
        private readonly DoctorController _controller;

        public DoctorControllerTests()
        {
            _mockService = new Mock<DoctorService>();
            _controller = new DoctorController(_mockService.Object);
        }

        [Fact]
        public void All_ShouldReturnOkObjectResult_WithDoctorList()
        {
            // Arrange
            var doctors = new List<DoctorModel>
            {
                new DoctorModel { Id = 1, Name = "Dr. Tanvir Ahmed", Speciality = "Cardiology", Phone = "01711", Email = "tanvir@test.com" }
            };
            _mockService.Setup(s => s.All()).Returns(doctors);

            // Act
            var actionResult = _controller.All();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(actionResult);
            var returnedData = Assert.IsAssignableFrom<List<DoctorModel>>(okResult.Value);
            Assert.Single(returnedData);
            Assert.Equal("Dr. Tanvir Ahmed", returnedData[0].Name);
        }

        [Fact]
        public void Get_ShouldReturnOkObjectResult_WithDoctor()
        {
            // Arrange
            var doctor = new DoctorModel { Id = 1, Name = "Dr. Tanvir Ahmed", Speciality = "Cardiology", Phone = "01711", Email = "tanvir@test.com" };
            _mockService.Setup(s => s.GetById(1)).Returns(doctor);

            // Act
            var actionResult = _controller.Get(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(actionResult);
            var returnedData = Assert.IsType<DoctorModel>(okResult.Value);
            Assert.Equal(1, returnedData.Id);
            Assert.Equal("Dr. Tanvir Ahmed", returnedData.Name);
        }

        [Fact]
        public void Create_ShouldReturnOkObjectResult_WhenDoctorCreated()
        {
            // Arrange
            var newDoctor = new DoctorModel { Name = "Dr. New", Speciality = "Pediatrics", Phone = "01888", Email = "new@test.com" };
            _mockService.Setup(s => s.Create(newDoctor)).Returns(true);

            // Act
            var actionResult = _controller.Create(newDoctor);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(actionResult);
            Assert.Equal(true, okResult.Value);
        }

        [Fact]
        public void Delete_ShouldReturnOkObjectResult_WhenDoctorDeleted()
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
