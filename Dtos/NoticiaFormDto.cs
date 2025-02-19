using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace noticiasmvc.Dtos
{
    public class NoticiaFormDto
    {
        public string Titulo { get; set; }

        public string Texto { get; set; }
        public List<int> Tags { get; set; }

    }
}