using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreatInnovators.Models.ViewModels
{
    public class GuideEditModel
    {
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public Int32 Age { get; set; }

        public String Languages { get; set; }
        public String Country { get; set; }
        public String Cities { get; set; }

        public String PhoneNumber { get; set; }
        public String Email { get; set; }
        public String Password { get; set; }

        public String FilePath { get; set; }
        public IFormFile File { get; set; }

        public String AdditionalInfo { get; set; }
    }
}
