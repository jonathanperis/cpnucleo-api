namespace Cpnucleo.Application.Queries;

public sealed class ListRecursoTarefaQueryHandler(IApplicationDbContext context) : IRequestHandler<ListRecursoTarefaQuery, ListRecursoTarefaViewModel>
{
    public async ValueTask<ListRecursoTarefaViewModel> Handle(ListRecursoTarefaQuery request, CancellationToken cancellationToken)
    {
        var recursoTarefas = await context.RecursoTarefas
            .AsNoTracking()
            .Include(x => x.Recurso)
            .Include(x => x.Tarefa)
            .Where(x => x.Ativo)
            .OrderBy(x => x.DataInclusao)
            .Select(x => x.MapToDto())
            .ToListAsync(cancellationToken);

        if (recursoTarefas is null)
        {
            return new ListRecursoTarefaViewModel(OperationResult.NotFound);
        }

        return new ListRecursoTarefaViewModel(OperationResult.Success, recursoTarefas);
    }
}