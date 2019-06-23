using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreatInnovators.Models
{
    public class Language
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public ICollection<GuideLanguage> Guides { get; set; }

        public String Name { get; set; }
    }
}
