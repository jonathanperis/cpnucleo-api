namespace Cpnucleo.Application.Queries;

public sealed class ListRecursoProjetoQueryHandler(IApplicationDbContext context) : IRequestHandler<ListRecursoProjetoQuery, ListRecursoProjetoViewModel>
{
    public async ValueTask<ListRecursoProjetoViewModel> Handle(ListRecursoProjetoQuery request, CancellationToken cancellationToken)
    {
        var recursoProjetos = await context.RecursoProjetos
            .AsNoTracking()
            .Include(x => x.Recurso)
            .Include(x => x.Projeto)
            .Where(x => x.Ativo)
            .OrderBy(x => x.DataInclusao)
            .Select(x => x.MapToDto())
            .ToListAsync(cancellationToken);

        if (recursoProjetos is null)
        {
            return new ListRecursoProjetoViewModel(OperationResult.NotFound);
        }

        return new ListRecursoProjetoViewModel(OperationResult.Success, recursoProjetos);
    }
}