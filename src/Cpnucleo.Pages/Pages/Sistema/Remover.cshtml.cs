﻿using Cpnucleo.Pages.Models;
using Cpnucleo.Pages.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Cpnucleo.Pages.Pages.Sistema
{
    [Authorize]
    public class RemoverModel : PageModel
    {
        private readonly IRepository<SistemaItem> _sistemaRepository;

        public RemoverModel(IRepository<SistemaItem> sistemaRepository) => _sistemaRepository = sistemaRepository;

        [BindProperty(SupportsGet = true)]
        public SistemaItem Sistema { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Sistema = await _sistemaRepository.Consultar(Sistema.IdSistema);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            await _sistemaRepository.Remover(Sistema);

            return RedirectToPage("Listar");
        }
    }
}