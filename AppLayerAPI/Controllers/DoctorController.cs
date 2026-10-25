using BLL.Models;
using BLL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppLayerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        DoctorService service;
        public DoctorController(DoctorService service)
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

        [HttpGet("all/appointments")]
        public IActionResult AllWithAppointments()
        {
            var data = service.GetWithAppointments();
            return Ok(data);
        }

        [HttpGet("all/info")]
        public IActionResult DoctorInfos()
        {
            var data = service.GetDoctorInfos();
            return Ok(data);
        }

        [HttpPost("create")]
        public IActionResult Create(DoctorModel m)
        {
            var result = service.Create(m);
            return Ok(result);
        }

        [HttpPut("update/{id}")]
        public IActionResult Update(int id, DoctorModel m)
        {
            m.Id = id;
            var result = service.Update(m);
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
