using AutoMapper;
using BLL;
using BLL.Models;
using BLL.Services;
using DAL.EF.Tables;
using DAL.Repository;
using Moq;
using Xunit;

namespace HospitalAppointmentSystem.Tests.Services
{
    public class PatientServiceTests
    {
        private readonly Mock<PatientRepo> _mockRepo;
        private readonly IMapper _mapper;
        private readonly PatientService _service;

        public PatientServiceTests()
        {
            _mockRepo = new Mock<PatientRepo>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = config.CreateMapper();

            _service = new PatientService(_mockRepo.Object, _mapper);
        }

        [Fact]
        public void All_ShouldReturnMappedPatientList()
        {
            // Arrange
            var patients = new List<Patient>
            {
                new Patient { Id = 1, Name = "Rahim Uddin", Age = 28, Gender = "Male", Phone = "01933", Email = "rahim@test.com" },
                new Patient { Id = 2, Name = "Karima Begum", Age = 34, Gender = "Female", Phone = "01644", Email = "karima@test.com" }
            };
            _mockRepo.Setup(r => r.Get()).Returns(patients);

            // Act
            var result = _service.All();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("Rahim Uddin", result[0].Name);
            _mockRepo.Verify(r => r.Get(), Times.Once);
        }

        [Fact]
        public void GetById_ShouldReturnPatient_WhenExists()
        {
            // Arrange
            var patient = new Patient { Id = 1, Name = "Rahim Uddin", Age = 28, Gender = "Male", Phone = "01933", Email = "rahim@test.com" };
            _mockRepo.Setup(r => r.Get(1)).Returns(patient);

            // Act
            var result = _service.GetById(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Rahim Uddin", result.Name);
            Assert.Equal(28, result.Age);
        }

        [Fact]
        public void Create_ShouldReturnTrue_WhenRepoSucceeds()
        {
            // Arrange
            var model = new PatientModel { Name = "New Patient", Age = 40, Gender = "Male", Phone = "01788", Email = "patient@test.com" };
            _mockRepo.Setup(r => r.Create(It.IsAny<Patient>())).Returns(true);

            // Act
            var result = _service.Create(model);

            // Assert
            Assert.True(result);
            _mockRepo.Verify(r => r.Create(It.Is<Patient>(p => p.Name == "New Patient")), Times.Once);
        }
    }
}
