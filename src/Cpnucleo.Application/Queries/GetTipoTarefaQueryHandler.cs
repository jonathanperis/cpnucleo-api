namespace Cpnucleo.Application.Queries;

public sealed class GetTipoTarefaQueryHandler(IApplicationDbContext context) : IRequestHandler<GetTipoTarefaQuery, GetTipoTarefaViewModel>
{
    public async ValueTask<GetTipoTarefaViewModel> Handle(GetTipoTarefaQuery request, CancellationToken cancellationToken)
    {
        var tipoTarefa = await context.TipoTarefas
            .AsNoTracking()
            .Where(x => x.Id == request.Id && x.Ativo)
            .Select(x => x.MapToDto())
            .FirstOrDefaultAsync(cancellationToken);

        if (tipoTarefa is null)
        {
            return new GetTipoTarefaViewModel(OperationResult.NotFound);
        }

        return new GetTipoTarefaViewModel(OperationResult.Success, tipoTarefa);
    }
}