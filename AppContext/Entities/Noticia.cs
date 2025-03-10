using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace noticiasmvc.AppContext.Entities
{
    public class Noticia
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(250)]
        public string Titulo { get; set; }

        [Required]
        public string Texto { get; set; }

        [Required]
        public int UsuarioId { get; set; }

        public virtual Usuario Usuario { get; set; }

        public virtual ICollection<NoticiaTag> NoticiaTags { get; set; }
    }
}