using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace noticiasmvc.AppContext.Entities
{
    public class NoticiaTag
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int NoticiaId { get; set; }
        public int TagId { get; set; }
        public virtual Noticia Noticia { get; set; }
        public virtual Tag Tag { get; set; }
    }
}