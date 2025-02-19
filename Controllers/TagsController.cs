using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using noticiasmvc.AppContext.Entities;
using noticiasmvc.Dtos;
using noticiasmvc.Services;
using noticiasmvc.ViewModels;

namespace noticiasmvc.Controllers
{
    [Route("[controller]")]
    public class TagsController : Controller
    {
        private readonly IBaseService<Tag> _tagService;
        private readonly IMapper mapper;

        public TagsController(IBaseService<Tag> tagService, IMapper mapper)
        {
            _tagService = tagService;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var tags = await _tagService.GetAllAsync();
            return View(tags);
        }
        
        [HttpGet("all")]
        public async Task<IEnumerable<Tag>> GetAll()
        {
            return await _tagService.GetAllAsync();
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View("TagForm", new TagFormViewModel
            {
                Tag = new Tag(),
                Title = "Criar Tag"
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(TagFormDto tag)
        {
            if (ModelState.IsValid)
            {
                await _tagService.AddAsync(mapper.Map<Tag>(tag));
                return RedirectToAction(nameof(Index));
            }
            var xx = ModelState.Select(x => x.Value.Errors).Where(x => x.Count > 0);
            return View("TagForm", new TagFormViewModel
            {
                Tag = mapper.Map<Tag>(tag),
                Title = "Criar Tag"
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var tag = await _tagService.GetByIdAsync(id);
            if (tag == null)
            {
                return NotFound();
            }
            return View("TagForm", new TagFormViewModel
            {
                Tag = tag,
                Title = "Editar Tag",
                Action = id.ToString()
            });
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id, TagFormDto dto)
        {
            if (ModelState.IsValid)
            {
                var tag = await _tagService.GetByIdAsync(id);
                mapper.Map(dto, tag);
                await _tagService.UpdateAsync(tag);
                return RedirectToAction(nameof(Index));
            }
            return View("TagForm", new TagFormViewModel
            {
                Tag = mapper.Map<Tag>(dto),
                Title = "Editar Tag",
                Action = id.ToString()
            });
        }

        [HttpPost("delete/{id}")]
        public async Task<IActionResult> DeleteConfirmed([FromRoute]int id)
        {
            await _tagService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}