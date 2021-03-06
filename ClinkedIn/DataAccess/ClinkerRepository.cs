﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClinkedIn.Models;

namespace ClinkedIn.DataAccess
{
    public class ClinkerRepository
    {
        static List<Clinker> _clinkers = new List<Clinker> {
            new Clinker {
                Id = 1,
                Name = "Nathan",
                Age = 33,
                LockupReason = "Stealing TVs",
                Interests = new List<string>{ "Coding", "Stealing TVs"},
                Services = new List<Service>{ new Service { Title = "Shive Maker", Cost = "bar of soap"} },
                Sentence = new DateTime(2021,12,28)
            },
            new Clinker {
                Id = 2,
                Name = "Martin",
                Age = 33,
                LockupReason = "Pepper Spraying People",
                Interests = new List<string>{ "Coding", "Road Rage"},
                Services = new List<Service>{ new Service { Title = "Instructor", Cost = "arm + leg"} },
                Sentence = new DateTime(2030,06,10)
            },
            new Clinker {
                Id = 3,
                Name = "Jameka",
                Age = 33,
                LockupReason = "Bank Heist",
                Interests = new List<string>{ "Coding", "Robbin' Banks" },
                Services = new List<Service>{ new Service { Title = "Chef", Cost = "ingredients"} },
                Sentence = new DateTime(2025,10,04)
            }
        };

        public List<Clinker> GetAllClinkers()
        {
            var allClinkers = _clinkers;
            return allClinkers;
        }

        public void AddClinkerService(int id, Service service)
        {
            _clinkers[id - 1].Services.Add(service);
            // var clinkerToUpdate = _clinkers.(s => s.Id == id);

        }

        public void AddClinker(Clinker clinker)
        {
            clinker.Id = _clinkers.Max(x => x.Id) + 1;
            _clinkers.Add(clinker);
        }

        public Clinker GetById(int id)
        {
            return _clinkers.FirstOrDefault(c => c.Id == id);
        }

        public List<Clinker> GetClinkersByInterest(string interest)
        { 
            var matchingClinkers = _clinkers.Where(x => x.Interests.Contains(interest));
            return matchingClinkers.ToList();
        }

        public void AddClinkerFriend(int id, Clinker friendToAdd)
        {
            _clinkers[id - 1].Friends.Add(friendToAdd);
        }

        public void AddClinkerEnemy(int id, Clinker enemyToAdd)
        {
            _clinkers[id - 1].Enemies.Add(enemyToAdd);
        }

        public string SentenceCountdown(int id)
        {
            var clinkerSentenced = GetById(id);
            DateTime daysLeft = clinkerSentenced.Sentence;
            DateTime startDate = DateTime.Now;

            TimeSpan sentenceLeft = daysLeft - startDate;
            return string.Format($"{clinkerSentenced.Name} has {sentenceLeft.Days} Days Left In Sentence");
        }

        public void DeleteClinker(Clinker clinkerToDelete)
        {
            _clinkers.Remove(clinkerToDelete);
        }

        public void DeleteService(int id, string service)
        {
            var selectedClinker = GetById(id);
            var serviceToDelete = selectedClinker.Services.Find(s => s.Title == service);
            _clinkers[id - 1].Services.Remove(serviceToDelete);
        }

        public void DeleteFriend(int id, int friendId)
        {
            var friendToDelete = GetById(friendId);
            _clinkers[id - 1].Friends.Remove(friendToDelete);
        }

        public void DeleteEnemy(int id, int enemyId)
        {
            var enemyToDelete = GetById(enemyId);
            _clinkers[id - 1].Enemies.Remove(enemyToDelete);
        }

        public void DeleteInterest(int id, string interest)
        {
            var selectedClinker = GetById(id);
            var interestToDelete = selectedClinker.Interests.Find(i => i == interest);
            _clinkers[id - 1].Interests.Remove(interestToDelete);
        }
    }
}
