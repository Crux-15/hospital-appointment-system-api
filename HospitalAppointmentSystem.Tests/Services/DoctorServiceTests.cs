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
    public class DoctorServiceTests
    {
        private readonly Mock<DoctorRepo> _mockRepo;
        private readonly IMapper _mapper;
        private readonly DoctorService _service;

        public DoctorServiceTests()
        {
            _mockRepo = new Mock<DoctorRepo>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = config.CreateMapper();

            _service = new DoctorService(_mockRepo.Object, _mapper);
        }

        [Fact]
        public void All_ShouldReturnMappedDoctorList_WhenDoctorsExist()
        {
            // Arrange
            var fakeDoctors = new List<Doctor>
            {
                new Doctor { Id = 1, Name = "Dr. Tanvir Ahmed", Speciality = "Cardiology", Phone = "01711", Email = "tanvir@test.com" },
                new Doctor { Id = 2, Name = "Dr. Sarah Rahman", Speciality = "Neurology", Phone = "01822", Email = "sarah@test.com" }
            };
            _mockRepo.Setup(r => r.Get()).Returns(fakeDoctors);

            // Act
            var result = _service.All();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("Dr. Tanvir Ahmed", result[0].Name);
            Assert.Equal("Cardiology", result[0].Speciality);
            _mockRepo.Verify(r => r.Get(), Times.Once);
        }

        [Fact]
        public void GetById_ShouldReturnDoctor_WhenDoctorExists()
        {
            // Arrange
            var doctor = new Doctor { Id = 1, Name = "Dr. Tanvir Ahmed", Speciality = "Cardiology", Phone = "01711", Email = "tanvir@test.com" };
            _mockRepo.Setup(r => r.Get(1)).Returns(doctor);

            // Act
            var result = _service.GetById(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Dr. Tanvir Ahmed", result.Name);
        }

        [Fact]
        public void GetById_ShouldReturnNull_WhenDoctorDoesNotExist()
        {
            // Arrange
            _mockRepo.Setup(r => r.Get(99)).Returns((Doctor?)null);

            // Act
            var result = _service.GetById(99);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Create_ShouldReturnTrue_WhenRepoSucceeds()
        {
            // Arrange
            var model = new DoctorModel { Name = "Dr. New", Speciality = "Dental", Phone = "01999", Email = "new@test.com" };
            _mockRepo.Setup(r => r.Create(It.IsAny<Doctor>())).Returns(true);

            // Act
            var result = _service.Create(model);

            // Assert
            Assert.True(result);
            _mockRepo.Verify(r => r.Create(It.Is<Doctor>(d => d.Name == "Dr. New")), Times.Once);
        }

        [Fact]
        public void Delete_ShouldReturnTrue_WhenDoctorDeleted()
        {
            // Arrange
            _mockRepo.Setup(r => r.Delete(1)).Returns(true);

            // Act
            var result = _service.Delete(1);

            // Assert
            Assert.True(result);
            _mockRepo.Verify(r => r.Delete(1), Times.Once);
        }

        [Fact]
        public void GetDoctorInfos_ShouldComputeTotalAppointmentsViaAutoMapper()
        {
            // Arrange
            var doctorWithVisits = new Doctor
            {
                Id = 1,
                Name = "Dr. Tanvir Ahmed",
                Speciality = "Cardiology",
                Phone = "01711",
                Email = "tanvir@test.com",
                Appointments = new List<Appointment>
                {
                    new Appointment { Id = 1, Status = "Pending" },
                    new Appointment { Id = 2, Status = "Confirmed" }
                }
            };
            _mockRepo.Setup(r => r.GetWithAppointments()).Returns(new List<Doctor> { doctorWithVisits });

            // Act
            var result = _service.GetDoctorInfos();

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(2, result[0].TotalAppointments);
        }
    }
}
