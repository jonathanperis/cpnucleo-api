namespace Cpnucleo.Application.Queries;

public sealed class ListImpedimentoTarefaQueryHandler(IApplicationDbContext context) : IRequestHandler<ListImpedimentoTarefaQuery, ListImpedimentoTarefaViewModel>
{
    public async ValueTask<ListImpedimentoTarefaViewModel> Handle(ListImpedimentoTarefaQuery request, CancellationToken cancellationToken)
    {
        var impedimentoTarefas = await context.ImpedimentoTarefas
            .AsNoTracking()
            .Include(x => x.Tarefa)
            .Where(x => x.Ativo)
            .OrderBy(x => x.DataInclusao)
            .Select(x => x.MapToDto())
            .ToListAsync(cancellationToken);

        if (impedimentoTarefas is null)
        {
            return new ListImpedimentoTarefaViewModel(OperationResult.NotFound);
        }

        return new ListImpedimentoTarefaViewModel(OperationResult.Success, impedimentoTarefas);
    }
}