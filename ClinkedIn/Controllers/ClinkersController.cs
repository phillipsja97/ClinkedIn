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

        [HttpGet]
        public IActionResult GetAllClinkers()
        {
            var allClinkers = _repository.GetAllClinkers();
            return Ok(allClinkers);
        }
                     
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

        [HttpDelete("{id}/services/{service}")]
        public IActionResult DeleteAService(int id, string service)
        {
            var clinkerToDeleteService = _repository.GetById(id);
            if (clinkerToDeleteService.Services.Any(s => s.Title  == service))
            {
                _repository.DeleteService(id, service);
            }
            else
            {
                return BadRequest("That service doesn't exist");
            }
            return Ok("That service has been deleted");
        }

        [HttpGet("{id}/friendsOfFriends")]
        public IActionResult GetFriendsOfFriends(int id)
        {
            var clinkersFriendsToGet = _repository.GetById(id);
            var clinkersFriends = new List<Clinker>();
            clinkersFriends.AddRange(clinkersFriendsToGet.Friends);
            var clinkersFriendsOfFriends = clinkersFriends.Select(clinker => clinker.Friends);
            return Ok(clinkersFriendsOfFriends);
        }

        [HttpGet("interests/{interest}")]
        public IActionResult GetClinkersByInterest(string interest)
        {
            var interestedClinkers = _repository.GetClinkersByInterest(interest);
            if (interestedClinkers.Count() == 0)
            {
                return NotFound("No clinkers found with that interest.");
            }
            return Ok(interestedClinkers);
        }

        [HttpDelete("{id}/interests/{interest}")]
        public IActionResult DeleteAnInterest(int id, string interest)
        {
            var clinkerToDeleteInterest = _repository.GetById(id);
            if (clinkerToDeleteInterest.Interests.Any(i => i == interest))
            {
                _repository.DeleteInterest(id, interest);
            }
            else
            {
                return NotFound("That interest doesn't exist");
            }
            return Ok("That interest has been deleted");
        }

        [HttpPost("{id}/friends/{friendToAddId}")]
        public IActionResult AddFriend(int id, int friendToAddId)
        {
            var clinkerToGiveFriend = _repository.GetById(id);
            var friendToAdd = _repository.GetById(friendToAddId);
            if (!clinkerToGiveFriend.Friends.Any(c => c.Name == friendToAdd.Name))
            {
                _repository.AddClinkerFriend(id, friendToAdd);
            }
            else
            {
                return BadRequest($"Already friends with {friendToAdd.Name}");
            }

            var updatedClinker = _repository.GetById(id);
            return Ok(updatedClinker);
        }

        [HttpPost("{id}/enemies/{enemyToAddId}")]
        public IActionResult AddEnemy(int id, int enemyToAddId)
        {
            var clinkerToGiveEnemy = _repository.GetById(id);
            var enemyToAdd = _repository.GetById(enemyToAddId);
            if (!clinkerToGiveEnemy.Enemies.Any(c => c.Name == enemyToAdd.Name))
            {
                _repository.AddClinkerEnemy(id, enemyToAdd);
            }
            else
            {
                return BadRequest($"Already enemies with {enemyToAdd.Name}");
            }

            var updatedClinker = _repository.GetById(id);
            return Ok(updatedClinker);
        }

        [HttpDelete("{id}/enemy/{enemyId}")]
        public IActionResult DeleteAnEnemy(int id, int enemyId)
        {
            var clinkerRemovingEnemy = _repository.GetById(id);
            var enemyBeingRemoved = _repository.GetById(enemyId);
            if (clinkerRemovingEnemy.Enemies.Contains(enemyBeingRemoved))
            {
                _repository.DeleteEnemy(id, enemyId);
            }
            else
            {
                return BadRequest($"{enemyBeingRemoved.Name} couldn't be found in {clinkerRemovingEnemy.Name}'s enemy list.");
            }
            return Ok($"Removed {enemyBeingRemoved.Name} from {clinkerRemovingEnemy.Name}'s enemy list.");
        }

        [HttpGet("{id}/sentence")]
        public IActionResult ClinkerSentence(int id)
        {
            var clinkerSentence = _repository.SentenceCountdown(id);
            return Ok(clinkerSentence);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateClinker(int id, Clinker clinker)
        {
            var clinkerToUpdate = _repository.GetById(id);

            if (clinkerToUpdate == null)
            {
                return NotFound("Clinker doesn't exist");
            }

            clinkerToUpdate.Name = clinker.Name;
            clinkerToUpdate.Age = clinker.Age;
            clinkerToUpdate.LockupReason = clinker.LockupReason;
            clinkerToUpdate.Interests = clinker.Interests;
            clinkerToUpdate.Services = clinker.Services;
            clinkerToUpdate.Friends = clinker.Friends;
            clinkerToUpdate.Enemies = clinker.Enemies;

            return Ok(clinkerToUpdate);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteClinker(int id)
        {
            var clinkerToDelete = _repository.GetById(id);

            if (clinkerToDelete == null)
            {
                return NotFound("Can't delete a clinker that doesn't exist.");
            }

            _repository.DeleteClinker(clinkerToDelete);
            return Ok($"That clinker has been deleted from the system.");
        }

        [HttpDelete("{id}/friends/{friendId}")]
        public IActionResult DeleteAFriend(int id, int friendId)
        {
            var clinkerRemovingFriend = _repository.GetById(id);
            var friendBeingRemoved = _repository.GetById(friendId);
            if (clinkerRemovingFriend.Friends.Contains(friendBeingRemoved))
            {
                _repository.DeleteFriend(id, friendId);
            }
            else
            {
                return BadRequest($"{friendBeingRemoved.Name} couldn't be found in {clinkerRemovingFriend.Name}'s friends list.");
            }
            return Ok($"Removed {friendBeingRemoved.Name} from {clinkerRemovingFriend.Name}'s friends list.");
        }
    }
}
