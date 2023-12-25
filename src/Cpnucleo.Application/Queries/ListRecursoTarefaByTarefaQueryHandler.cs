namespace Cpnucleo.Application.Queries;

public sealed class ListRecursoTarefaByTarefaQueryHandler(IApplicationDbContext context) : IRequestHandler<ListRecursoTarefaByTarefaQuery, ListRecursoTarefaByTarefaViewModel>
{
    public async ValueTask<ListRecursoTarefaByTarefaViewModel> Handle(ListRecursoTarefaByTarefaQuery request, CancellationToken cancellationToken)
    {
        var recursoTarefas = await context.RecursoTarefas
            .AsNoTracking()
            .Include(x => x.Recurso)
            .Include(x => x.Tarefa)
            .Where(x => x.IdTarefa == request.IdTarefa && x.Ativo)
            .OrderBy(x => x.DataInclusao)
            .Select(x => x.MapToDto())
            .ToListAsync(cancellationToken);

        if (recursoTarefas is null)
        {
            return new ListRecursoTarefaByTarefaViewModel(OperationResult.NotFound);
        }

        return new ListRecursoTarefaByTarefaViewModel(OperationResult.Success, recursoTarefas);
    }
}