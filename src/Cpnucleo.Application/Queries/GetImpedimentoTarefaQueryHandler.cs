namespace Cpnucleo.Application.Queries;

public sealed class GetImpedimentoTarefaQueryHandler(IApplicationDbContext context) : IRequestHandler<GetImpedimentoTarefaQuery, GetImpedimentoTarefaViewModel>
{
    public async ValueTask<GetImpedimentoTarefaViewModel> Handle(GetImpedimentoTarefaQuery request, CancellationToken cancellationToken)
    {
        var impedimentoTarefa = await context.ImpedimentoTarefas
            .AsNoTracking()
            .Include(x => x.Tarefa)
            .Where(x => x.Id == request.Id && x.Ativo)
            .Select(x => x.MapToDto())
            .FirstOrDefaultAsync(cancellationToken);

        if (impedimentoTarefa is null)
        {
            return new GetImpedimentoTarefaViewModel(OperationResult.NotFound);
        }

        return new GetImpedimentoTarefaViewModel(OperationResult.Success, impedimentoTarefa);
    }
}