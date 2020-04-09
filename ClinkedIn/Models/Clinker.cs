using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinkedIn.Models
{
    public class Clinker
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public int Age { get; set; }
        public string LockupReason { get; set; }
        public List<string> Interests { get; set; }
        public List<Service> Services { get; set; }
        public List<Clinker> Friends { get; set; }
        public List<Clinker> Enemies { get; set; }

        public Clinker()
        {
            Interests = new List<string>();
            Services = new List<Service>();
            Friends = new List<Clinker>();
            Enemies = new List<Clinker>();
        }
    }
}
