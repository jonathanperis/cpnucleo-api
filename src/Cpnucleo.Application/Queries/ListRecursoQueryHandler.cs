namespace Cpnucleo.Application.Queries;

public sealed class ListRecursoQueryHandler(IApplicationDbContext context) : IRequestHandler<ListRecursoQuery, ListRecursoViewModel>
{
    public async ValueTask<ListRecursoViewModel> Handle(ListRecursoQuery request, CancellationToken cancellationToken)
    {
        var recursos = await context.Recursos
            .AsNoTracking()
            .Where(x => x.Ativo)
            .OrderBy(x => x.DataInclusao)
            .Select(x => x.MapToDto())
            .ToListAsync(cancellationToken);

        if (recursos is null)
        {
            return new ListRecursoViewModel(OperationResult.NotFound);
        }

        return new ListRecursoViewModel(OperationResult.Success, recursos);
    }
}