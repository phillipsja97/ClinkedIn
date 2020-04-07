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
        
    }
}
