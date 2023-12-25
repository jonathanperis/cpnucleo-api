namespace Cpnucleo.Application.Queries;

public sealed class ListImpedimentoQueryHandler(IApplicationDbContext context) : IRequestHandler<ListImpedimentoQuery, ListImpedimentoViewModel>
{
    public async ValueTask<ListImpedimentoViewModel> Handle(ListImpedimentoQuery request, CancellationToken cancellationToken)
    {
        var impedimentos = await context.Impedimentos
            .AsNoTracking()
            .Where(x => x.Ativo)
            .OrderBy(x => x.DataInclusao)
            .Select(x => x.MapToDto())
            .ToListAsync(cancellationToken);

        if (impedimentos is null)
        {
            return new ListImpedimentoViewModel(OperationResult.NotFound);
        }

        return new ListImpedimentoViewModel(OperationResult.Success, impedimentos);
    }
}