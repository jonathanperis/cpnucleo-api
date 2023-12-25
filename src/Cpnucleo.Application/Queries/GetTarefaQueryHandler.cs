namespace Cpnucleo.Application.Queries;

public sealed class GetTarefaQueryHandler(IApplicationDbContext context) : IRequestHandler<GetTarefaQuery, GetTarefaViewModel>
{
    public async ValueTask<GetTarefaViewModel> Handle(GetTarefaQuery request, CancellationToken cancellationToken)
    {
        var tarefa = await context.Tarefas
            .AsNoTracking()
            .Include(x => x.Projeto)
            .Include(x => x.Recurso)
            .Include(x => x.Workflow)
            .Include(x => x.TipoTarefa)
            .Where(x => x.Id == request.Id && x.Ativo)
            .Select(x => x.MapToDto())
            .FirstOrDefaultAsync(cancellationToken);

        if (tarefa is null)
        {
            return new GetTarefaViewModel(OperationResult.NotFound);
        }

        return new GetTarefaViewModel(OperationResult.Success, tarefa);
    }
}