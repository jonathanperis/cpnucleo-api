namespace Cpnucleo.Application.Queries;

public sealed class GetRecursoTarefaQueryHandler(IApplicationDbContext context) : IRequestHandler<GetRecursoTarefaQuery, GetRecursoTarefaViewModel>
{
    public async ValueTask<GetRecursoTarefaViewModel> Handle(GetRecursoTarefaQuery request, CancellationToken cancellationToken)
    {
        var recursoTarefa = await context.RecursoTarefas
            .AsNoTracking()
            .Include(x => x.Recurso)
            .Include(x => x.Tarefa)
            .Where(x => x.Id == request.Id && x.Ativo)
            .Select(x => x.MapToDto())
            .FirstOrDefaultAsync(cancellationToken);

        if (recursoTarefa is null)
        {
            return new GetRecursoTarefaViewModel(OperationResult.NotFound);
        }

        return new GetRecursoTarefaViewModel(OperationResult.Success, recursoTarefa);
    }
}