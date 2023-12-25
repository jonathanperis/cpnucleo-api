namespace Cpnucleo.Application.Queries;

public sealed class ListRecursoProjetoByProjetoQueryHandler(IApplicationDbContext context) : IRequestHandler<ListRecursoProjetoByProjetoQuery, ListRecursoProjetoByProjetoViewModel>
{
    public async ValueTask<ListRecursoProjetoByProjetoViewModel> Handle(ListRecursoProjetoByProjetoQuery request, CancellationToken cancellationToken)
    {
        var recursoProjetos = await context.RecursoProjetos
            .AsNoTracking()
            .Include(x => x.Recurso)
            .Include(x => x.Projeto)
            .Where(x => x.IdProjeto == request.IdProjeto && x.Ativo)
            .OrderBy(x => x.DataInclusao)
            .Select(x => x.MapToDto())
            .ToListAsync(cancellationToken);

        if (recursoProjetos is null)
        {
            return new ListRecursoProjetoByProjetoViewModel(OperationResult.NotFound);
        }

        return new ListRecursoProjetoByProjetoViewModel(OperationResult.Success, recursoProjetos);
    }
}