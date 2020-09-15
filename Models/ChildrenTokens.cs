using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Starwars_API.Models
{
    public class ChildrenTokens
    {
        public string count { get; set; }
        public string next { get; set; }
        public string previous { get; set; }
        public List<object> results { get; set; }
        public List<object> films { get; set; }

    }
}
