﻿using Cpnucleo.RazorPages.Services.Interfaces;
using Cpnucleo.RazorPages.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Cpnucleo.RazorPages.Pages.Projeto
{
    [Authorize]
    public class RemoverModel : PageBase
    {
        private readonly ICrudService<ProjetoViewModel> _projetoService;

        public RemoverModel(ICrudService<ProjetoViewModel> projetoService)
        {
            _projetoService = projetoService;
        }

        [BindProperty]
        public ProjetoViewModel Projeto { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            try
            {
                Projeto = await _projetoService.ConsultarAsync(Token, id);

                return Page();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return Page();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                await _projetoService.RemoverAsync(Token, Projeto.Id);

                return RedirectToPage("Listar");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return Page();
            }
        }
    }
}