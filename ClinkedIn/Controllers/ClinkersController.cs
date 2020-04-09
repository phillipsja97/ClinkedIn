using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ClinkedIn.DataAccess;
using ClinkedIn.Models;

namespace ClinkedIn.Controllers
{
    [Route("api/clinkers")]
    [ApiController]    
    public class ClinkersController : ControllerBase
    {
        ClinkerRepository _repository = new ClinkerRepository();

        [HttpPost]
        public IActionResult AddClinker(Clinker clinkerToAdd)
        {
            _repository.AddClinker(clinkerToAdd);
            return Created($"Added Clinker {clinkerToAdd.Name}", clinkerToAdd);
        }

        [HttpPost("{id}/services")]
        public IActionResult AddService(int id, Service serviceToAdd)
        {
            var clinkerToUpdate = _repository.GetById(id);
            if (!clinkerToUpdate.Services.Any(s => s.Title == serviceToAdd.Title))
            {
                _repository.AddClinkerService(id, serviceToAdd);
            }
            else
            {
                return BadRequest("Service already exists");
            }

            var updatedClinker = _repository.GetById(id);
            return Ok(updatedClinker);
        }

        [HttpGet("interests/{interest}")]
        public IActionResult GetClinkersByInterest(string interest)
        {
            var interestedClinkers = _repository.GetClinkersByInterest(interest);
            return Ok(interestedClinkers);
        }
        
    }
}
