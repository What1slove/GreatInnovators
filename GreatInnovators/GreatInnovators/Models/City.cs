using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreatInnovators.Models
{
    public class City
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public ICollection<GuideCity> Guides { get; set; }

        public String Name { get; set; }
    }
}
