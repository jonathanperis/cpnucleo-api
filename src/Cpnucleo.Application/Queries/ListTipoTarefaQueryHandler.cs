namespace Cpnucleo.Application.Queries;

public sealed class ListTipoTarefaQueryHandler(IApplicationDbContext context) : IRequestHandler<ListTipoTarefaQuery, ListTipoTarefaViewModel>
{
    public async ValueTask<ListTipoTarefaViewModel> Handle(ListTipoTarefaQuery request, CancellationToken cancellationToken)
    {
        var tipoTarefas = await context.TipoTarefas
            .AsNoTracking()
            .Where(x => x.Ativo)
            .OrderBy(x => x.DataInclusao)
            .Select(x => x.MapToDto())
            .ToListAsync(cancellationToken);

        if (tipoTarefas is null)
        {
            return new ListTipoTarefaViewModel(OperationResult.NotFound);
        }

        return new ListTipoTarefaViewModel(OperationResult.Success, tipoTarefas);
    }
}