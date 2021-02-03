﻿using Cpnucleo.RazorPages.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cpnucleo.RazorPages.Services.Interfaces
{
    public interface IRecursoTarefaService : ICrudService<RecursoTarefaViewModel>
    {
        Task<IEnumerable<RecursoTarefaViewModel>> ListarPorTarefaAsync(string token, Guid idTarefa);
    }
}
