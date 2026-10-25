using BLL.Models;
using BLL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppLayerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        AppointmentService service;
        public AppointmentController(AppointmentService service)
        {
            this.service = service;
        }

        [HttpGet("all")]
        public IActionResult All()
        {
            var data = service.All();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var data = service.GetById(id);
            return Ok(data);
        }

        [HttpGet("pending")]
        public IActionResult Pending()
        {
            var data = service.GetPending();
            return Ok(data);
        }

        [HttpGet("today")]
        public IActionResult Today()
        {
            var data = service.GetToday();
            return Ok(data);
        }

        [HttpGet("doctor/{doctorId}")]
        public IActionResult ByDoctor(int doctorId)
        {
            var data = service.GetByDoctor(doctorId);
            return Ok(data);
        }

        [HttpGet("patient/{patientId}")]
        public IActionResult ByPatient(int patientId)
        {
            var data = service.GetByPatient(patientId);
            return Ok(data);
        }

        [HttpPost("create")]
        public IActionResult Create(AppointmentModel m)
        {
            var result = service.Create(m);
            return Ok(result);
        }

        [HttpPut("update/{id}")]
        public IActionResult Update(int id, AppointmentModel m)
        {
            m.Id = id;
            var result = service.Update(m);
            return Ok(result);
        }

        [HttpPut("status/{id}")]
        public IActionResult UpdateStatus(int id, string status)
        {
            var result = service.UpdateStatus(id, status);
            return Ok(result);
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var result = service.Delete(id);
            return Ok(result);
        }
    }
}
