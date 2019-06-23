using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreatInnovators.Models
{
    public class GuideCity
    {
        public Guid GuideId { get; set; }

        public Guide Guide { get; set; }

        public Guid CityId { get; set; }

        public City City { get; set; }
    }
}
