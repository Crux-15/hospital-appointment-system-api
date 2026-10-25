using BLL.Models;
using BLL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppLayerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        PatientService service;
        public PatientController(PatientService service)
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

        [HttpPost("create")]
        public IActionResult Create(PatientModel m)
        {
            var result = service.Create(m);
            return Ok(result);
        }

        [HttpPut("update/{id}")]
        public IActionResult Update(int id, PatientModel m)
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
