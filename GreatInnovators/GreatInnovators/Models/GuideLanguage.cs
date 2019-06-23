using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreatInnovators.Models
{
    public class GuideLanguage
    {
        public Guid GuideId { get; set; }

        public Guide Guide { get; set; }

        public Guid LanguageId { get; set; } = Guid.NewGuid();

        public Language Language { get; set; }
    }
}
