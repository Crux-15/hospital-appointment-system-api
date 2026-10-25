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
    public class AppointmentServiceTests
    {
        private readonly Mock<AppointmentRepo> _mockRepo;
        private readonly IMapper _mapper;
        private readonly AppointmentService _service;

        public AppointmentServiceTests()
        {
            _mockRepo = new Mock<AppointmentRepo>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = config.CreateMapper();

            _service = new AppointmentService(_mockRepo.Object, _mapper);
        }

        [Fact]
        public void GetPending_ShouldReturnOnlyPendingAppointments()
        {
            // Arrange
            var pendingList = new List<Appointment>
            {
                new Appointment { Id = 1, DoctorId = 1, PatientId = 1, Status = "Pending", AppointmentDate = DateTime.Now },
                new Appointment { Id = 2, DoctorId = 2, PatientId = 2, Status = "Pending", AppointmentDate = DateTime.Now.AddDays(1) }
            };
            _mockRepo.Setup(r => r.GetPending()).Returns(pendingList);

            // Act
            var result = _service.GetPending();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.All(result, item => Assert.Equal("Pending", item.Status));
            _mockRepo.Verify(r => r.GetPending(), Times.Once);
        }

        [Fact]
        public void GetToday_ShouldReturnAppointmentsForToday()
        {
            // Arrange
            var todayList = new List<Appointment>
            {
                new Appointment { Id = 1, DoctorId = 1, PatientId = 1, Status = "Confirmed", AppointmentDate = DateTime.Today }
            };
            _mockRepo.Setup(r => r.GetToday()).Returns(todayList);

            // Act
            var result = _service.GetToday();

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(1, result[0].Id);
        }

        [Fact]
        public void UpdateStatus_ShouldCallRepoAndReturnTrue()
        {
            // Arrange
            _mockRepo.Setup(r => r.UpdateStatus(1, "Confirmed")).Returns(true);

            // Act
            var result = _service.UpdateStatus(1, "Confirmed");

            // Assert
            Assert.True(result);
            _mockRepo.Verify(r => r.UpdateStatus(1, "Confirmed"), Times.Once);
        }

        [Fact]
        public void Create_ShouldCallRepoCreateAndReturnTrue()
        {
            // Arrange
            var model = new AppointmentModel
            {
                DoctorId = 1,
                PatientId = 1,
                AppointmentDate = DateTime.Now.AddDays(1),
                Status = "Pending",
                Notes = "Routine checkup"
            };
            _mockRepo.Setup(r => r.Create(It.IsAny<Appointment>())).Returns(true);

            // Act
            var result = _service.Create(model);

            // Assert
            Assert.True(result);
            _mockRepo.Verify(r => r.Create(It.Is<Appointment>(a => a.Notes == "Routine checkup")), Times.Once);
        }
    }
}
