using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using noticiasmvc.AppContext.Entities;

namespace noticiasmvc.AppContext
{
    public class NoticiasDbContext : DbContext
    {
        public NoticiasDbContext(DbContextOptions<NoticiasDbContext> options) : base(options)
        {
        }

        public DbSet<Noticia> Noticias { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<NoticiaTag> NoticiasTags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Usuario>()
                .HasMany(x => x.Noticias)
                .WithOne(x => x.Usuario);

            modelBuilder.Entity<Noticia>()
                .HasMany(x => x.NoticiaTags)
                .WithOne(x => x.Noticia);

            modelBuilder.Entity<Tag>()
                .HasMany(x => x.NoticiasTags)
                .WithOne(x => x.Tag);
        }
    }
}