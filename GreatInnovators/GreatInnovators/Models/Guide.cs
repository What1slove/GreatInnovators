using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreatInnovators.Models
{
    public class Guide
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public ICollection<GuideCity> Cities { get; set; }
        public ICollection<GuideLanguage> Languages { get; set; }

        public String FirstName { get; set; }
        public String LastName { get; set; }
        public Int32 Age { get; set; }

        //public List<String> Languages { get; set; }
        public String Country { get; set; }
        //public List<String> Cities { get; set; }

        public String PhoneNumber { get; set; }
        public String Email { get; set; }
        public String Password { get; set; }
        public Double Rating { get; set; }
        public enum Status { Free, Busy, Offline}

        public String FilePath { get; set; }

        public String AdditionalInfo { get; set; }
    }
}
