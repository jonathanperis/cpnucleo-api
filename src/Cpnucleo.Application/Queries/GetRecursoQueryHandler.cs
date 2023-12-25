namespace Cpnucleo.Application.Queries;

public sealed class GetRecursoQueryHandler(IApplicationDbContext context) : IRequestHandler<GetRecursoQuery, GetRecursoViewModel>
{
    public async ValueTask<GetRecursoViewModel> Handle(GetRecursoQuery request, CancellationToken cancellationToken)
    {
        var recurso = await context.Recursos
            .AsNoTracking()
            .Where(x => x.Id == request.Id && x.Ativo)
            .Select(x => x.MapToDto())
            .FirstOrDefaultAsync(cancellationToken);

        if (recurso is null)
        {
            return new GetRecursoViewModel(OperationResult.NotFound);
        }

        return new GetRecursoViewModel(OperationResult.Success, recurso);
    }
}