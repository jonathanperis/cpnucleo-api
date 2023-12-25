namespace Cpnucleo.Application.Queries;

public sealed class ListProjetoQueryHandler(IApplicationDbContext context) : IRequestHandler<ListProjetoQuery, ListProjetoViewModel>
{
    public async ValueTask<ListProjetoViewModel> Handle(ListProjetoQuery request, CancellationToken cancellationToken)
    {
        var projetos = await context.Projetos
            .AsNoTracking()
            .Include(x => x.Sistema)
            .Where(x => x.Ativo)
            .OrderBy(x => x.DataInclusao)
            .Select(x => x.MapToDto())
            .ToListAsync(cancellationToken);

        if (projetos is null)
        {
            return new ListProjetoViewModel(OperationResult.NotFound);
        }

        return new ListProjetoViewModel(OperationResult.Success, projetos);
    }
}