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
                Services = new List<Service>{new Service { Title = "Shive Maker", Cost = "bar of soap"} }
            }
        };

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
    }
}
