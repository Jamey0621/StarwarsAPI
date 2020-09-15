using DocumentFormat.OpenXml.Office2010.ExcelAc;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Starwars_API.Models
{
    public class PersonViewModel
    {
        public string Name { get; set; }

        [DisplayName("Birth Year")]
        public String BirthYear { get; set; }
        public string Hight { get; set; }
        public string Mass { get; set; }

        [DisplayName("Eye Color")]
        public string EyeColor { get; set; }

        [DisplayName("Film Count")]
        public string FilmCount { get; set; }

        public Array Species { get; set; }

    }
}
