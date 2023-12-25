namespace Cpnucleo.Application.Queries;

public sealed class GetSistemaQueryHandler(IApplicationDbContext context) : IRequestHandler<GetSistemaQuery, GetSistemaViewModel>
{
    public async ValueTask<GetSistemaViewModel> Handle(GetSistemaQuery request, CancellationToken cancellationToken)
    {
        var sistema = await context.Sistemas
            .AsNoTracking()
            .Where(x => x.Id == request.Id && x.Ativo)
            .Select(x => x.MapToDto())
            .FirstOrDefaultAsync(cancellationToken);

        if (sistema is null)
        {
            return new GetSistemaViewModel(OperationResult.NotFound);
        }

        return new GetSistemaViewModel(OperationResult.Success, sistema);
    }
}