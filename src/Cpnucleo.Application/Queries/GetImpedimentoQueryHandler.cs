namespace Cpnucleo.Application.Queries;

public sealed class GetImpedimentoQueryHandler(IApplicationDbContext context) : IRequestHandler<GetImpedimentoQuery, GetImpedimentoViewModel>
{
    public async ValueTask<GetImpedimentoViewModel> Handle(GetImpedimentoQuery request, CancellationToken cancellationToken)
    {
        var impedimento = await context.Impedimentos
            .AsNoTracking()
            .Where(x => x.Id == request.Id && x.Ativo)
            .Select(x => x.MapToDto())
            .FirstOrDefaultAsync(cancellationToken);

        if (impedimento is null)
        {
            return new GetImpedimentoViewModel(OperationResult.NotFound);
        }

        return new GetImpedimentoViewModel(OperationResult.Success, impedimento);
    }
}