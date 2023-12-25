namespace Cpnucleo.Application.Queries;

public sealed class GetRecursoProjetoQueryHandler(IApplicationDbContext context) : IRequestHandler<GetRecursoProjetoQuery, GetRecursoProjetoViewModel>
{
    public async ValueTask<GetRecursoProjetoViewModel> Handle(GetRecursoProjetoQuery request, CancellationToken cancellationToken)
    {
        var recursoProjeto = await context.RecursoProjetos
            .AsNoTracking()
            .Include(x => x.Recurso)
            .Include(x => x.Projeto)
            .Where(x => x.Id == request.Id && x.Ativo)
            .Select(x => x.MapToDto())
            .FirstOrDefaultAsync(cancellationToken);

        if (recursoProjeto is null)
        {
            return new GetRecursoProjetoViewModel(OperationResult.NotFound);
        }

        return new GetRecursoProjetoViewModel(OperationResult.Success, recursoProjeto);
    }
}