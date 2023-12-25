namespace Cpnucleo.Application.Queries;

public sealed class ListApontamentoQueryHandler(IApplicationDbContext context) : IRequestHandler<ListApontamentoQuery, ListApontamentoViewModel>
{
    public async ValueTask<ListApontamentoViewModel> Handle(ListApontamentoQuery request, CancellationToken cancellationToken)
    {
        var apontamentos = await context.Apontamentos
            .AsNoTracking()
            .Include(x => x.Tarefa)
            .Where(x => x.Ativo)
            .OrderBy(x => x.DataInclusao)
            .Select(x => x.MapToDto())
            .ToListAsync(cancellationToken);

        if (apontamentos is null)
        {
            return new ListApontamentoViewModel(OperationResult.NotFound);
        }

        return new ListApontamentoViewModel(OperationResult.Success, apontamentos);
    }
}