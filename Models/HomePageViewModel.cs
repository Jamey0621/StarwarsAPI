using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Starwars_API.Models
{
    public class HomePageViewModel
    {
        public string SearchName { get; set; }
        public string Next { get; set; }
        public string  Previous { get; set; }
        public List<PersonViewModel> People { get; set; }

    }
}
