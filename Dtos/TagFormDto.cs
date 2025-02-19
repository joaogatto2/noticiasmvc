using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace noticiasmvc.Dtos
{
    public class TagFormDto
    {
        [Required]
        [MaxLength(100)]
        public string Descricao { get; set; }
    }
}