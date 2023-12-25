namespace Cpnucleo.Application.Queries;

public sealed class GetApontamentoQueryHandler(IApplicationDbContext context) : IRequestHandler<GetApontamentoQuery, GetApontamentoViewModel>
{
    public async ValueTask<GetApontamentoViewModel> Handle(GetApontamentoQuery request, CancellationToken cancellationToken)
    {
        var apontamento = await context.Apontamentos
            .AsNoTracking()
            .Include(x => x.Tarefa)
            .Where(x => x.Id == request.Id && x.Ativo)
            .Select(x => x.MapToDto())
            .FirstOrDefaultAsync(cancellationToken);

        if (apontamento is null)
        {
            return new GetApontamentoViewModel(OperationResult.NotFound);
        }

        return new GetApontamentoViewModel(OperationResult.Success, apontamento);
    }
}