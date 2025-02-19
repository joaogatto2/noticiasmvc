using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using noticiasmvc.AppContext.Entities;

namespace noticiasmvc.ViewModels
{
    public class TagFormViewModel : FormViewModel
    {
        public Tag Tag { get; init; }
    }
}