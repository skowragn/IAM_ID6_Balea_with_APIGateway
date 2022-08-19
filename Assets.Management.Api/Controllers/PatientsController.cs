using Assets.Core.Assets.Management.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Assets.Management.Common;
using Assets.Management.Common.Attributes;
using Assets.Management.Common.Models;

namespace Assets.Management.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PatientsController : ControllerBase
    {
        // GET: api/<PatientController>
        [HttpGet]
        [Authorize(AllPermissions.PerformSurgery)]
        [AuthorizeRoles(nameof(Roles.Doctor), nameof(Roles.Nurse))]
        public List<PatientDto?> Get()
        {
            var patientList = BuildPatientList();
            return patientList.ToList();
        }

        // GET api/<PatientController>/5
        [HttpGet("{id}")]
        public PatientDto? GetPatientById(int id)
        {
            var patientList = BuildPatientList();
            var item = patientList.FirstOrDefault(item => item != null && item.Id == id);
            return item;
        }

        // POST api/<PatientController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<PatientController>/5
        [HttpPut("")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PatientController>/5
        [HttpDelete("")]
        public void Delete(int id)
        {
        }

        private static IEnumerable<PatientDto?> BuildPatientList()
        {
            return
                new List<PatientDto?>()
                {
                    new ()
                    {
                        Id = 1,
                        FirstName = "Adam",
                        LastName = "Nowcki",
                        ServiceDate = DateTime.Now,
                        Summary = "Gesund"
                    },

                    new ()
                    {
                        Id = 2,
                        FirstName = "Alice",
                        LastName = "Smith",
                        ServiceDate = DateTime.Now,
                        Summary = "in progress"
                    },
                    new ()
                    {
                        Id = 3,
                        FirstName = "Anna",
                        LastName = "Kowalska",
                        ServiceDate = DateTime.Now,
                        Summary = "Gesund"
                    },
                    new ()
                    {
                        Id = 4,
                        FirstName = "Monica",
                        LastName = "Nowcki",
                        ServiceDate = DateTime.MaxValue,
                        Summary = "Gesund"
                    },
                    new ()
                    {
                        Id = 5,
                        FirstName = "Maria",
                        LastName = "Skowron",
                        ServiceDate = DateTime.Now,
                        Summary = "Gesund"
                    },
                };
        }

    }
}
