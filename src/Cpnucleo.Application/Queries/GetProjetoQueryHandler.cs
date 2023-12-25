namespace Cpnucleo.Application.Queries;

public sealed class GetProjetoQueryHandler(IApplicationDbContext context) : IRequestHandler<GetProjetoQuery, GetProjetoViewModel>
{
    public async ValueTask<GetProjetoViewModel> Handle(GetProjetoQuery request, CancellationToken cancellationToken)
    {
        var projeto = await context.Projetos
            .AsNoTracking()
            .Include(x => x.Sistema)
            .Where(x => x.Id == request.Id && x.Ativo)
            .Select(x => x.MapToDto())
            .FirstOrDefaultAsync(cancellationToken);

        if (projeto is null)
        {
            return new GetProjetoViewModel(OperationResult.NotFound);
        }

        return new GetProjetoViewModel(OperationResult.Success, projeto);
    }
}