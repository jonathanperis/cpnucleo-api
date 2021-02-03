﻿using Cpnucleo.RazorPages.Services.Interfaces;
using Cpnucleo.RazorPages.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Cpnucleo.RazorPages.Pages.Workflow
{
    [Authorize]
    public class IncluirModel : PageBase
    {
        private readonly ICrudService<WorkflowViewModel> _workflowService;

        public IncluirModel(ICrudService<WorkflowViewModel> workflowService)
        {
            _workflowService = workflowService;
        }

        [BindProperty]
        public WorkflowViewModel Workflow { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Page();
                }

                await _workflowService.IncluirAsync(Token, Workflow);

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