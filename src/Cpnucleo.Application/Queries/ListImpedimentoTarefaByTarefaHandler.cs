namespace Cpnucleo.Application.Queries;

public sealed class ListImpedimentoTarefaByTarefaQueryHandler(IApplicationDbContext context) : IRequestHandler<ListImpedimentoTarefaByTarefaQuery, ListImpedimentoTarefaByTarefaViewModel>
{
    public async ValueTask<ListImpedimentoTarefaByTarefaViewModel> Handle(ListImpedimentoTarefaByTarefaQuery request, CancellationToken cancellationToken)
    {
        var impedimentoTarefas = await context.ImpedimentoTarefas
            .AsNoTracking()
            .Include(x => x.Tarefa)
            .Where(x => x.IdTarefa == request.IdTarefa && x.Ativo)
            .OrderBy(x => x.DataInclusao)
            .Select(x => x.MapToDto())
            .ToListAsync(cancellationToken);

        if (impedimentoTarefas is null)
        {
            return new ListImpedimentoTarefaByTarefaViewModel(OperationResult.NotFound);
        }

        return new ListImpedimentoTarefaByTarefaViewModel(OperationResult.Success, impedimentoTarefas);
    }
}