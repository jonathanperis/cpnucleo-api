﻿namespace Cpnucleo.Application.Queries;

public sealed class ListTarefaByRecursoQueryHandler(IApplicationDbContext context) : IRequestHandler<ListTarefaByRecursoQuery, ListTarefaByRecursoViewModel>
{
    public async ValueTask<ListTarefaByRecursoViewModel> Handle(ListTarefaByRecursoQuery request, CancellationToken cancellationToken)
    {
        var tarefas = await context.Tarefas
            .AsNoTracking()
            .Include(x => x.Projeto)
            .Include(x => x.Recurso)
            .Include(x => x.Workflow)
            .Include(x => x.TipoTarefa)
            .Where(x => x.IdRecurso == request.IdRecurso && x.Ativo)
            .OrderBy(x => x.DataInclusao)
            .Select(x => x.MapToDto())
            .ToListAsync(cancellationToken);

        if (tarefas is null)
        {
            return new ListTarefaByRecursoViewModel(OperationResult.NotFound);
        }

        await PreencherDadosAdicionaisAsync(tarefas, cancellationToken);

        return new ListTarefaByRecursoViewModel(OperationResult.Success, tarefas);
    }

    private async Task PreencherDadosAdicionaisAsync(List<TarefaDto> lista, CancellationToken cancellationToken)
    {
        var colunas = context.Workflows.Where(x => x.Ativo).Count();

        foreach (var item in lista)
        {
            if (item.Workflow is not null)
            {
                item.Workflow.TamanhoColuna = Workflow.GetTamanhoColuna(colunas);
            }

            item.HorasConsumidas = context.Apontamentos
                .Where(x => x.IdRecurso == item.IdRecurso && x.IdTarefa == item.Id && x.Ativo)
                .Sum(x => x.QtdHoras);

            item.HorasRestantes = item.QtdHoras - item.HorasConsumidas;

            if (item.TipoTarefa is null)
            {
                continue;
            }

            var impedimentos = await context.ImpedimentoTarefas
                .Where(x => x.IdTarefa == item.Id && x.Ativo)
                .OrderBy(x => x.DataInclusao)
                .Select(x => x.MapToDto())
                .ToListAsync(cancellationToken);

            if (impedimentos.Any())
            {
                item.TipoTarefa.Element = TipoTarefaConstants.WarningElement;
            }
            else if (DateTime.UtcNow.Date >= item.DataInicio && DateTime.UtcNow.Date <= item.DataTermino)
            {
                item.TipoTarefa.Element = TipoTarefaConstants.SuccessElement;
            }
            else if (DateTime.UtcNow.Date > item.DataTermino)
            {
                item.TipoTarefa.Element = TipoTarefaConstants.DangerElement;
            }
            else
            {
                item.TipoTarefa.Element = TipoTarefaConstants.InfoElement;
            }
        }
    }
}