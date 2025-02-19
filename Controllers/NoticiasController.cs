using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using noticiasmvc.AppContext.Entities;
using noticiasmvc.Dtos;
using noticiasmvc.Services;
using noticiasmvc.ViewModels;

namespace noticiasmvc.Controllers
{
    [Route("[controller]")]
    public class NoticiasController : Controller
    {
        private readonly IBaseService<Noticia> noticiaService;
        private readonly IMapper mapper;

        public NoticiasController(
            IBaseService<Noticia> noticiaService,
            IMapper mapper
        )
        {
            this.noticiaService = noticiaService;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            return View(await noticiaService.GetAllAsync(x => 
                x
                    .Include(x => x.Usuario)
                    .Include(x => x.NoticiaTags)
                        .ThenInclude(x => x.Tag)
                )
            );
        }

        [HttpGet("Create")]
        public async Task<IActionResult> Create()
        {
            return View("NoticiaForm", new NoticiaFormViewModel
            {
                Title = "Criar Noticia",
                Id = 0
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]NoticiaFormDto dto)
        {
            var noticia = mapper.Map<Noticia>(dto);
            noticia.UsuarioId = Int32.Parse(HttpContext.User.Identity.Name);
            await noticiaService.AddAsync(noticia);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Edit([FromRoute]int id)
        {
            return View("NoticiaForm", new NoticiaFormViewModel
            {
                Title = "Editar Noticia",
                Id = id
            });
        }

        [HttpGet("{id}/entity")]
        public async Task<Noticia?> GetModel([FromRoute]int id)
        {
            return (await noticiaService.GetAllAsync(x => x.Where(x => x.Id == id).Include(x => x.NoticiaTags))).FirstOrDefault();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromRoute]int id,[FromBody]NoticiaFormDto dto)
        {
            var noticia = (await noticiaService.GetAllAsync(x => x.Where(x => x.Id == id).Include(x => x.NoticiaTags))).FirstOrDefault();
            if (noticia == null)
                return NotFound();

                
            noticia.UsuarioId = Int32.Parse(HttpContext.User.Identity.Name);

            await noticiaService.UpdateAsync(mapper.Map(dto, noticia));
            return NoContent();
        }

        [HttpPost("delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            await noticiaService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}